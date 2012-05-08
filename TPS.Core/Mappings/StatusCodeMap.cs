using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public sealed class StatusCodeMap : FluentNHibernate.Mapping.ClassMap<StatusCode>
    {
        public StatusCodeMap()
        {
            Table("StatusCodes");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

            Cache.ReadOnly();
        }
    }
}