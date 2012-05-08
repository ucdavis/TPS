using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public sealed class TechContactMap : FluentNHibernate.Mapping.ClassMap<TechContact>
    {
        public TechContactMap()
        {
            Table("TechContacts");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.Phone);
            Map(x => x.Email);

            Cache.ReadOnly();
        }
    }
}