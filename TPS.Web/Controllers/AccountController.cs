﻿using System.Web.Mvc;
using System.Web.Security;
using UCDArch.Web.Authentication;

namespace TPS.Web.Controllers
{
    /// <summary>
    /// Controller for the Account class
    /// </summary>
    public class AccountController : Controller
    {
        public ActionResult LogIn(string returnUrl)
        {
            string resultUrl = CASHelper.Login(); //Do the CAS Login

            if (resultUrl != null)
            {
                return Redirect(resultUrl);
            }

            TempData["URL"] = returnUrl;

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("https://cas.ucdavis.edu/cas/logout");
        }
    }
}