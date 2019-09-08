using Services.Interface;
using Services.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        INDNInterestsService _ndnInterestService;
        public HomeController()
        {
            _ndnInterestService = new NDNInterestService();
        }
        public ActionResult Index()
        {
            ViewBag.NDNInterest = _ndnInterestService.GetAllNDNInterest().Count;
            ViewBag.NFDDev = 0;
            ViewBag.NDNLib = 0;
            ViewBag.ndnSIM = 0;

            ViewBag.NDNApp = 0;
            ViewBag.MiniNDN = 0;
            ViewBag.NLSR = 0;
            ViewBag.Total = ViewBag.NDNInterest;
            return View();
        }


    }
}