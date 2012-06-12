using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web.Http;
using TPS.Core.Domain;
using TPS.Web.Models;
using UCDArch.Core;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.ActionResults;

namespace TPS.Web.Controllers
{
    public class NamsController : ApiController
    {
        private readonly IRepositoryWithTypedId<DataNamFlattened, int> _dataNamRepository;

        public NamsController()
        {
            _dataNamRepository = SmartServiceLocator<IRepository<DataNamFlattened>>.GetService();
        }

        // This doesn't currently get called, so it has to be done in the default c'tor.
        public NamsController(IRepositoryWithTypedId<DataNamFlattened, int> dataNamRepository)
        {
            _dataNamRepository = dataNamRepository;
        }

        // GET /api/default1

        public IEnumerable<DataNamFlattened> GetAllNams()
        {
            //var nams = _dataNamRepository.Queryable.AsQueryable().Where(x => x.NamNumber == "10011D").OrderBy(x => x.NamNumber);
            var nams = _dataNamRepository.GetAll().OrderBy(x => x.NamNumber);

            return nams;
        }
    }
}