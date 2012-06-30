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
    public class DepartmentsController : ApiController
    {
        private readonly IRepositoryWithTypedId<DepartmentFlattened, string> _departmentRepository;

        public DepartmentsController()
        {
            _departmentRepository = SmartServiceLocator<IRepositoryWithTypedId<DepartmentFlattened, string>>.GetService();
        }

        // This doesn't currently get called, so it has to be done in the default c'tor.
        public DepartmentsController(IRepositoryWithTypedId<DepartmentFlattened, string> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // GET /api/default1

        public IEnumerable<DepartmentFlattened> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll().OrderBy(x => x.Department);

            return departments;
        }
    }
}