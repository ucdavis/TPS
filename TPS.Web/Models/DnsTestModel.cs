using System.Collections.Generic;
using TPS.Core.Domain;

namespace TPS.Web.Models
{
    public class DnsTestModel
    {
        public string HostName { get; set; }

        public IList<string> Addresses
        { get; set; }

        public IList<string> Aliases
        { get; set; }

        public string ExceptionMessage { get; set; }
    }
}