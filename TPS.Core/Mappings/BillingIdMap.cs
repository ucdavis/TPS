using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public sealed class BillingIdMap : FluentNHibernate.Mapping.ClassMap<BillingId>
    {
        public BillingIdMap()
        {
            Table("BillingIds");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

            Cache.ReadOnly();
        }
    }
}