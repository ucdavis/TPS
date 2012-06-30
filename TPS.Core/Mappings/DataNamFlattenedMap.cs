using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public class DataNamFlattenedMap : FluentNHibernate.Mapping.ClassMap<DataNamFlattened>
    {
        public DataNamFlattenedMap()
        {
            Table("DataNamsFlattened_v");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.NamNumber);
            //Map(x => x.Area);
            Map(x => x.Building);
            Map(x => x.Room);
            Map(x => x.Department);
            Map(x => x.DepartmentNumber);
            //Map(x => x.CaanZone);
            Map(x => x.BillingId);
            Map(x => x.Vlan);
            //Map(x => x.Division);
            //Map(x => x.College);
            Map(x => x.Status);
            Map(x => x.PortNumber);

            Cache.ReadOnly();
        }
    }
}