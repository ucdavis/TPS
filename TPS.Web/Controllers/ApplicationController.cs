using System.Web.Http;
using UCDArch.Web.Attributes;
using UCDArch.Web.Controller;

namespace TPS.Web.Controllers
{
    [Authorize]
    [Version(MajorVersion = 4)] //TPS version, maybe 4?
    public class ApplicationController : SuperController { }
}