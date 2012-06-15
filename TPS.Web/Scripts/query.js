/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/. */

/**
 * Represents a queriable index. Call methods on the returned object
 * to create queries.
 *
 * Example:
 *
 *   let query = Index("make").eq("BMW");
 *
 */
function Index(name) {
  function queryMaker(op) {
    return function () {
      return IndexQuery(name, op, arguments);
    };
  }
  return {
    eq:      queryMaker("eq"),
    neq:     queryMaker("neq"),
    gt:      queryMaker("gt"),
    gteq:    queryMaker("gteq"),
    lt:      queryMaker("lt"),
    lteq:    queryMaker("lteq"),
    between: queryMaker("between"),
    betweeq: queryMaker("betweeq"),
    oneof:   function oneof() {
      let values = Array.slice(arguments);
      let query = IndexQuery(name, "eq", [values.shift()]);
      while (values.length) {
        query = query.or(IndexQuery(name, "eq", [values.shift()]));
      }
      return query;
    }
  };
}

/**
 * Helper that notifies a 'success' event on a request, with a given
 * result object. This is typically either a cursor or a result array.
 */
function notifySuccess(request, result) {
  let event = {type: "success",
               target: request}; //TODO complete event interface
  request.readyState = IDBRequest.DONE;
  request.result = result;
  if (typeof request.onsuccess == "function") {
    request.onsuccess(event);
  }
};

/**
 * Create a cursor object.
 */
function Cursor(store, request, keys, keyOnly) {
  let cursor = {
    continue: function continue_() {
      if (!keys.length) {
        notifySuccess(request, undefined);
        return;
      }
      let key = keys.shift();
      if (keyOnly) {
        cursor.key = key;
        notifySuccess(request, cursor);
        return;
      }
      let r = store.get(key);
      r.onsuccess = function onsuccess() {
        cursor.key = key;
        cursor.value = r.result;
        notifySuccess(request, cursor);
      };
    }
    //TODO complete cursor interface
  };
  return cursor;
}

/**
 * Create a request object.
 */
function Request() {
  return {
    result: undefined,
    onsuccess: null,
    onerror: null,
    readyState: IDBRequest.LOADING
    // TODO complete request interface
  };
}

/**
 * Create a request that will receive a cursor.
 *
 * This will also kick off the query, instantiate the Cursor when the
 * results are available, and notify the first 'success' event.
 */
function CursorRequest(store, queryFunc, keyOnly) {
  let request = Request();
  queryFunc(store, function (keys) {
    let cursor = Cursor(store, request, keys, keyOnly);
    cursor.continue();
  });
  return request;
}

/**
 * Create a request that will receive a result array.
 *
 * This will also kick off the query, build up the result array, and
 * notify the 'success' event.
 */
function ResultRequest(store, queryFunc, keyOnly) {
  let request = Request();
  queryFunc(store, function (keys) {
    if (keyOnly || !keys.length) {
      notifySuccess(request, keys);
      return;
    }
    let results = [];
    function getNext() {
      let r = store.get(keys.shift());
      r.onsuccess = function onsuccess() {
        results.push(r.result);
        if (!keys.length) {
          notifySuccess(request, results);
          return;
        }
        getNext();
      };
    }
    getNext();
  });
  return request;
}

/**
 * Provide a generic way to create a query object from a query function.
 * Depending on the implementation of that query function, the query could
 * produce results from an index, combine results from other queries, etc.
 */
function Query(queryFunc, toString) {

  let query = {

    // Sadly we need to expose this to make Intersection and Union work :(
    _queryFunc: queryFunc,

    and: function and(query2) {
      return Intersection(query, query2);
    },

    or: function or(query2) {
      return Union(query, query2);
    },

    openCursor: function openCursor(store) {
      return CursorRequest(store, queryFunc, false);
    },

    openKeyCursor: function openKeyCursor(store) {
      return CursorRequest(store, queryFunc, true);
    },

    getAll: function getAll(store) {
      return ResultRequest(store, queryFunc, false);
    },

    getAllKeys: function getAllKeys(store) {
      return ResultRequest(store, queryFunc, true);
    },

    toString: toString
  };
  return query;
};

/**
 * Create a query object that queries an index.
 */
function IndexQuery(indexName, operation, values) {
  let negate = false;
  let op = operation;
  if (op == "neq") {
    op = "eq";
    negate = true;
  }

  function makeRange() {
    let range;
    switch (op) {
      case "eq":
        range = IDBKeyRange.only(values[0]);
        break;
      case "lt":
        range = IDBKeyRange.upperBound(values[0], true);
        break;
      case "lteq":
        range = IDBKeyRange.upperBound(values[0]);
        break;
      case "gt":
        range = IDBKeyRange.lowerBound(values[0], true);
        break;
      case "gteq":
        range = IDBKeyRange.lowerBound(values[0]);
        range.upperOpen = true;
        break;
      case "between":
        range = IDBKeyRange.bound(values[0], values[1], true, true);
        break;
      case "betweeq":
        range = IDBKeyRange.bound(values[0], values[1]);
        break;
    }
    return range;
  }

  function queryKeys(store, callback) {
    let index = store.index(indexName);
    let range = makeRange();
    let request = index.getAllKeys(range);
    request.onsuccess = function onsuccess(event) {
      let result = request.result;
      if (!negate) {
        callback(result);
        return;
      }

      // Deal with the negation case. This means we fetch all keys and then
      // subtract the original result from it.
      request = index.getAllKeys();
      request.onsuccess = function onsuccess(event) {
        let all = request.result;
        callback(arraySub(all, result));
      };
    };
  }

  let args = arguments;
  function toString() {
    return "IndexQuery(" + Array.slice(args).toSource().slice(1, -1) + ")";
  }

  return Query(queryKeys, toString);
}

/**
 * Create a query object that performs the intersection of two given queries.
 */
function Intersection(query1, query2) {
  function queryKeys(store, callback) {
    query1._queryFunc(store, function (keys1) {
      query2._queryFunc(store, function (keys2) {
        callback(arrayIntersect(keys1, keys2));
      });
    });
  }

  function toString() {
    return "Intersection(" + query1.toString() + ", " + query2.toString() + ")";
  }

  return Query(queryKeys, toString);
}

/**
 * Create a query object that performs the union of two given queries.
 */
function Union(query1, query2) {
  function queryKeys(store, callback) {
    query1._queryFunc(store, function (keys1) {
      query2._queryFunc(store, function (keys2) {
        callback(arrayUnion(keys1, keys2));
      });
    });
  }

  function toString() {
    return "Union(" + query1.toString() + ", " + query2.toString() + ")";
  }

  return Query(queryKeys, toString);
}


function arraySub(minuend, subtrahend) {
  if (!minuend.length || !subtrahend.length) {
    return minuend;
  }
  return minuend.filter(function(item) {
    return subtrahend.indexOf(item) == -1;
  });
}

function arrayUnion(foo, bar) {
  if (!foo.length) {
    return bar;
  }
  if (!bar.length) {
    return foo;
  }
  return foo.concat(arraySub(bar, foo));
}

function arrayIntersect(foo, bar) {
  if (!foo.length) {
    return foo;
  }
  if (!bar.length) {
    return bar;
  }
  return foo.filter(function(item) {
    return bar.indexOf(item) != -1;
  });
}
