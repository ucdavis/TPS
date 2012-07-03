using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web.Http;
using TPS.Core.Domain;
using TPS.Web.Helpers;
using TPS.Web.Models;
using UCDArch.Core;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.ActionResults;

namespace TPS.Web.Controllers
{
    public class NamsController : ApiController
    {
        private readonly IRepositoryWithTypedId<DataNamFlattened, int> _dataNamRepository;

        public System.Linq.Expressions.Expression<Func<DataNamFlattened, bool>> NamSearchExpression { get; set; }

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
            var nams = _dataNamRepository.GetAll().OrderBy(x => x.NamNumber);

            return nams;
        }

        public IEnumerable<DataNamFlattened> GetNamsByParams(string namNumber, string roomNumber, string vlan,
                                                             string building, string departmentNumber)
        {
            var retval = new List<DataNamFlattened>();

            var hasNamNumber = !(String.IsNullOrEmpty(namNumber) || namNumber.Equals("0"));
            var hasRoomNumber = !(String.IsNullOrEmpty(roomNumber) || roomNumber.Equals("0"));
            var hasVlan = !(String.IsNullOrEmpty(vlan) || vlan.Equals("0"));
            var hasBuilding = !(String.IsNullOrEmpty(building) || building.Equals("0"));
            var hasDepartmentNumber = !(String.IsNullOrEmpty(departmentNumber) || departmentNumber.Equals("0"));
            var hasSearchParameters = hasNamNumber || hasRoomNumber || hasVlan || hasBuilding || hasDepartmentNumber
                                          ? true
                                          : false;
            if (hasSearchParameters)
            {
                NamSearchExpression = PredicateBuilder.True<DataNamFlattened>();

                if (hasNamNumber)
                {
                    NamSearchExpression = NamSearchExpression.And(p => p.NamNumber.Equals(namNumber));
                }
                if (hasRoomNumber)
                {
                    NamSearchExpression = NamSearchExpression.And(p => p.Room.Equals(roomNumber));
                }
                if (hasVlan)
                {
                    NamSearchExpression =
                        NamSearchExpression.And(p => p.Vlan.Equals(vlan));
                }
                if (hasBuilding)
                {
                    NamSearchExpression =
                        NamSearchExpression.And(p => p.Building.Equals(building));
                }
                if (hasDepartmentNumber)
                {
                    NamSearchExpression =
                        NamSearchExpression.And(p => p.DepartmentNumber.Equals(departmentNumber));
                }
                retval = _dataNamRepository
                   .Queryable
                   .Where(NamSearchExpression)
                   .OrderBy(t => t.NamNumber)
                   .ToList();
            }
            return retval;
        }
    }
}