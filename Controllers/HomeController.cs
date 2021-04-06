using ShotFinder.Class;
using ShotFinderMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShotFinder.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

      
        [HttpGet]
        public  ActionResult StoreAction(string SearchString)
        {
            StoresViewModel StoreModel = new StoresViewModel();


            if (String.IsNullOrEmpty(SearchString))
            {
                ModelState.AddModelError("SearchString", "Store must be selected.");
                return (ActionResult)View(StoreModel);
            }

            switch (SearchString)
            {
                case "0":
                    return RedirectToAction("FilterIndex", "Shot");
                case "1":
                    return RedirectToAction("CVSFilter", "Shot");

            }

            return View();

        }

        [HttpGet]
        public ActionResult CVSFilter(string SearchString)
        {

            return RedirectToAction("FilterIndex", "Shot", SearchString);
           
        }


    }
}
