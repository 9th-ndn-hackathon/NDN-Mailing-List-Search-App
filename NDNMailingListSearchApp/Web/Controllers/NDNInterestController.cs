using Models.DatabaseModels;
using Services.Interface;
using Services.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class NDNInterestController : Controller
    {
        INDNInterestsService _ndnInterestService;
        public NDNInterestController()
        {
            _ndnInterestService = new NDNInterestService();
        }

        public ActionResult Index(string keyword)
        {
            List<NDNInterest> ndnInterests = new List<NDNInterest>();
            if (!string.IsNullOrEmpty(keyword))
            {
                ndnInterests = _ndnInterestService.Search(keyword);
            }

            return View(ndnInterests);
        }
    }
}