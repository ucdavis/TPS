using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public sealed class MaskMap : FluentNHibernate.Mapping.ClassMap<Mask>
    {
        public MaskMap()
        {
            Table("Mask");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

            Cache.ReadOnly();
        }
    }
}