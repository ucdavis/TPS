using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPS.Core.Domain;
using TPS.Web.Helpers;
using TPS.Web.Models;
using UCDArch.Core.PersistanceSupport;

namespace TPS.Web.Controllers
{
    public class NamSearchController : ApplicationController
    {
        private readonly IRepositoryWithTypedId<Vlan, int> _vlanRepository;

        private readonly IRepositoryWithTypedId<DataNam, int> _dataNamRepository;

        private readonly IRepositoryWithTypedId<Building, int> _buildingRepository;

        private readonly IRepositoryWithTypedId<Department, string> _departmentRepository;

        public System.Linq.Expressions.Expression<Func<DataNam, bool>> NamSearchExpression { get; set; }

        public NamSearchController(IRepositoryWithTypedId<Vlan, int> vlanRepository, IRepositoryWithTypedId<DataNam, int> dataNamRepository,
            IRepositoryWithTypedId<Building, int> buildingRepository, IRepositoryWithTypedId<Department, string> departmentRepository
            )
        {
            _vlanRepository = vlanRepository;
            _dataNamRepository = dataNamRepository;
            _buildingRepository = buildingRepository;
            _departmentRepository = departmentRepository;
        }

        //
        // GET: /NamSearch/

        public ActionResult Index()
        {
            var viewModel = new NamSearchModel
                                {
                                    DataNams = new List<DataNam>(),
                                    Vlans = _vlanRepository.Queryable.OrderBy(t => t.Name).ToList(),
                                    Buildings = _buildingRepository.Queryable.OrderBy(t => t.Name).ToList(),
                                    Departments =
                                        _departmentRepository.Queryable.OrderBy(t => t.Name).ToList()
                                };

            return View(viewModel);
        }

        public ActionResult SearchResults(string namNumber, string roomNumber, string vlan, string building, string department)
        {
            var hasNamNumber = !(String.IsNullOrEmpty(namNumber) || namNumber.Equals("0"));
            var hasRoomNumber = !(String.IsNullOrEmpty(roomNumber) || roomNumber.Equals("0"));
            var hasVlanId = !(String.IsNullOrEmpty(vlan) || vlan.Equals("0"));
            var hasBuildingId = !(String.IsNullOrEmpty(building) || building.Equals("0"));
            var hasDepartmentId = !(String.IsNullOrEmpty(department) || department.Equals("0"));
            var hasSearchParameters = hasNamNumber || hasRoomNumber || hasVlanId || hasBuildingId || hasDepartmentId
                                          ? true
                                          : false;

            var viewModel = new NamSearchModel
                                {
                                    DataNams = new List<DataNam>()
                                };

            if (hasSearchParameters)
            {
                NamSearchExpression = PredicateBuilder.True<DataNam>();

                if (hasNamNumber)
                {
                    NamSearchExpression = NamSearchExpression.And(p => p.NamNumber.Equals(namNumber));
                }
                if (hasRoomNumber)
                {
                    NamSearchExpression = NamSearchExpression.And(p => p.Room.Equals(roomNumber));
                }
                if (hasVlanId)
                {
                    NamSearchExpression =
                        NamSearchExpression.And(p => p.Vlan.Id == int.Parse(vlan));
                }
                if (hasBuildingId)
                {
                    NamSearchExpression =
                        NamSearchExpression.And(p => p.Building.Id == int.Parse(building));
                }
                if (hasDepartmentId)
                {
                    NamSearchExpression =
                        NamSearchExpression.And(p => p.Department.Id.Equals(department));
                }

                viewModel.DataNams = _dataNamRepository
                    .Queryable
                    .Where(NamSearchExpression)
                    .OrderBy(t => t.NamNumber)
                    .ToList();
            }
            return View(viewModel);
        }

        public ActionResult DisplayNam(int id)
        {
            var nam = _dataNamRepository.GetById(id);

            return View(nam);
        }

        public ActionResult DisplayContact(int id)
        {
            var vlan = _vlanRepository.GetNullableById(id);

            return View(vlan);
        }

        public ActionResult DbTest()
        {
            ViewBag.Message = "Welcome to HTML5 IndexedDB Test";

            return View();
        }

        public ActionResult DbjsTest()
        {
            ViewBag.Message = "Welcome to db.js, the HTML5 IndexedDB wrapper Test";

            return View();
        }

        public ActionResult GettingStartedTest()
        {
            ViewBag.Message = "Getting-Started-with-IndexedDB Test";

            return View();
        }
    }
}