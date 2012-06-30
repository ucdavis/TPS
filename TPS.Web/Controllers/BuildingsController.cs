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
    public class BuildingsController : ApiController
    {
        private readonly IRepositoryWithTypedId<BuildingFlattened, string> _buildingRepository;

        public BuildingsController()
        {
            _buildingRepository = SmartServiceLocator<IRepositoryWithTypedId<BuildingFlattened, string>>.GetService();
        }

        // This doesn't currently get called, so it has to be done in the default c'tor.
        public BuildingsController(IRepositoryWithTypedId<BuildingFlattened, string> buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        // GET /api/default1

        public IEnumerable<BuildingFlattened> GetAllBuildings()
        {
            var buildings = _buildingRepository.GetAll().OrderBy(x => x.Building);

            return buildings;
        }
    }
}