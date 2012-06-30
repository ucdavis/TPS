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
    public class VlansController : ApiController
    {
        private readonly IRepositoryWithTypedId<VlanFlattened, string> _vlanRepository;

        public VlansController()
        {
            _vlanRepository = SmartServiceLocator<IRepositoryWithTypedId<VlanFlattened, string>>.GetService();
        }

        // This doesn't currently get called, so it has to be done in the default c'tor.
        public VlansController(IRepositoryWithTypedId<VlanFlattened, string> vlanRepository)
        {
            _vlanRepository = vlanRepository;
        }

        // GET /api/default1

        public IEnumerable<VlanFlattened> GetAllVlans()
        {
            var vlans = _vlanRepository.GetAll().OrderBy(x => x.Vlan);

            return vlans;
        }
    }
}