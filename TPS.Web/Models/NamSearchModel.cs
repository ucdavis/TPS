using System.Collections.Generic;
using TPS.Core.Domain;

namespace TPS.Web.Models
{
    public class NamSearchModel
    {
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
    }
}