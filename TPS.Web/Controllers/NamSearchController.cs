using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPS.Core.Domain;
using UCDArch.Core.PersistanceSupport;

namespace TPS.Web.Controllers
{
    public class NamSearchController : ApplicationController
    {
        private readonly IRepositoryWithTypedId<Vlan, int> _vlanRepository;

        private readonly IRepositoryWithTypedId<DataNam, int> _dataNamRepository;

        public NamSearchController(IRepositoryWithTypedId<Vlan, int> vlanRepository, IRepositoryWithTypedId<DataNam, int> dataNamRepository)
        {
            _vlanRepository = vlanRepository;
            _dataNamRepository = dataNamRepository;
        }

        //
        // GET: /NamSearch/

        public ActionResult Index()
        {
            var namSearchModel = Models.NamSearchModel.Create(Repository);

            return View(namSearchModel);
        }

        public ActionResult SearchResults(string namNumber, string roomNumber, string vlan, string building, string department)
        {
            var namSearchModel = Models.NamSearchModel.Create(Repository, namNumber, roomNumber, vlan, building, department);

            return View(namSearchModel);
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