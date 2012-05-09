using System;
using System.Collections.Generic;
using System.Linq;
using TPS.Core.Domain;
using TPS.Web.Helpers;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace TPS.Web.Models
{
    public class NamSearchModel
    {
        public System.Linq.Expressions.Expression<Func<DataNam, bool>> NamSearchExpression { get; set; }

        public string NamNumber { get; set; }

        public Vlan Vlan { get; set; }

        public IList<Vlan> Vlans
        { get; set; }

        public Building Building { get; set; }

        public IList<Building> Buildings
        { get; set; }

        public Department Department { get; set; }

        public IList<Department> Departments
        { get; set; }

        public IList<DataNam> DataNams { get; set; }

        public static NamSearchModel Create(IRepository repository)
        {
            return Create(repository, null, null, null, null, null);
        }

        public static NamSearchModel Create(IRepository repository, string namNumber, string roomNumber, string vlanId, string buildingId, string departmentId)
        {
            Check.Require(repository != null, "Repository must be supplied");

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
                                    DataNams = new List<DataNam>(),
                                    Vlans = repository.OfType<Vlan>().Queryable.OrderBy(t => t.Name).ToList(),
                                    Buildings = repository.OfType<Building>().Queryable.OrderBy(t => t.Name).ToList(),
                                    Departments = repository.OfType<Department>().Queryable.OrderBy(t => t.Name).ToList()
                                };

            if (hasSearchParameters)
            {
                viewModel.NamSearchExpression = PredicateBuilder.True<DataNam>();

                if (hasNamNumber)
                {
                    viewModel.NamSearchExpression = viewModel.NamSearchExpression.And(p => p.NamNumber.Equals(namNumber));
                }
                if (hasRoomNumber)
                {
                    viewModel.NamSearchExpression = viewModel.NamSearchExpression.And(p => p.Room.Equals(roomNumber));
                }
                if (hasVlanId)
                {
                    viewModel.NamSearchExpression = viewModel.NamSearchExpression.And(p => p.Vlan.Id == int.Parse(vlanId));
                }
                if (hasBuildingId)
                {
                    viewModel.NamSearchExpression =
                        viewModel.NamSearchExpression.And(p => p.Building.Id == int.Parse(buildingId));
                }
                if (hasDepartmentId)
                {
                    viewModel.NamSearchExpression =
                       viewModel.NamSearchExpression.And(p => p.Department.Id.Equals(departmentId));
                }

                viewModel.DataNams = repository.OfType<DataNam>()
                    .Queryable
                    .Where(viewModel.NamSearchExpression)
                    .OrderBy(t => t.NamNumber)
                    .ToList();
            }

            return viewModel;
        }
    }
}