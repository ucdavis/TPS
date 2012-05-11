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

        public ActionResult SearchResults(string namNumber, string roomNumber, string vlanId, string buildingId, string departmentId)
        {
            var hasNamNumber = !(String.IsNullOrEmpty(namNumber) || namNumber.Equals("0"));
            var hasRoomNumber = !(String.IsNullOrEmpty(roomNumber) || roomNumber.Equals("0"));
            var hasVlanId = !(String.IsNullOrEmpty(vlanId) || vlanId.Equals("0"));
            var hasBuildingId = !(String.IsNullOrEmpty(buildingId) || buildingId.Equals("0"));
            var hasDepartmentId = !(String.IsNullOrEmpty(departmentId) || departmentId.Equals("0"));
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
                        NamSearchExpression.And(p => p.Vlan.Id == int.Parse(vlanId));
                }
                if (hasBuildingId)
                {
                    NamSearchExpression =
                        NamSearchExpression.And(p => p.Building.Id == int.Parse(buildingId));
                }
                if (hasDepartmentId)
                {
                    NamSearchExpression =
                        NamSearchExpression.And(p => p.Department.Id.Equals(departmentId));
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
    }
}