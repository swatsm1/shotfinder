using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TMSWebMVC.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Error/500")]

        public ActionResult Error500()
        {
            ///Only Core
            //var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //if (exceptionFeature != null)
            //{
            //    ViewBag.ErrorMessage = exceptionFeature.Error.Message;
            //    ViewBag.RouteOfException = exceptionFeature.Path;
            //}

            return View();
        }
        [Route("Error/404")]

        public ActionResult Error404()
        {
            //var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //if (exceptionFeature != null)
            //{
            //    ViewBag.ErrorMessage = exceptionFeature.Error.Message;
            //    ViewBag.RouteOfException = exceptionFeature.Path;
            //}

            return View();
        }

        [Route("Error/406")]

        public ActionResult Error406()
        {
            //var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //if (exceptionFeature != null)
            //{
            //    ViewBag.ErrorMessage = exceptionFeature.Error.Message;
            //    ViewBag.RouteOfException = exceptionFeature.Path;
            //}

            return View();
        }

    }
}