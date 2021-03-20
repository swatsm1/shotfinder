using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Assert = NUnit.Framework.Assert;
using ShotFinderMVC.Controllers;
using ShotFinderMVC.Class;
using ShotFinderMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShotFinderTests
{
    [TestClass]
    public class UnitTestShotStore
    {
       

        [Test]
        public void GetTaskDetailsbySearchArgs()
        {
            string search = "08889";

            // Arrange
            ShotController controller = new ShotController();
            // Act
            Task<ActionResult> result = controller.FilterIndex(search) as Task<ActionResult>;
            // Assert

            ActionResult myActResult = result.Result;

            var model = ModelFromActionResult<List<StoresViewModel>>(myActResult);

            //Assert.IsTrue((model.errors.message ?? "").Contains("Exists"));

            Assert.IsTrue((model.ShotStores[0].Zip ?? "").Contains("08889"));
        }
        [Test]
        public void GetTaskDetailsbySearchArgsNoZIPCode()
        {
            string search = "";

            // Arrange
            ShotController controller = new ShotController();
            // Act
            Task<ActionResult> result = controller.FilterIndex(search) as Task<ActionResult>;
            // Assert

            ActionResult myActResult = result.Result;

            var model = ModelFromActionResult<List<StoresViewModel>>(myActResult);

            //Assert.IsTrue((model.errors.message ?? "").Contains("Exists"));

            Assert.IsTrue((model.errors.message ?? "").Contains("Zipcode must contain a value."));
        }

        public StoresViewModel ModelFromActionResult<T>(ActionResult actionResult)
        {
            object model = null;
            if (actionResult.GetType() == typeof(ViewResult))
            {
                ViewResult viewResult = (ViewResult)actionResult;
                StoresViewModel TMmodel = new StoresViewModel();
                Errors errors = new Errors();



                if (viewResult.ViewData.ModelState.IsValid == false)
                {

                    foreach (ModelState modelState in viewResult.ViewData.ModelState.Values)
                    {
                        foreach (System.Web.Mvc.ModelError error in modelState.Errors)
                        {
                            errors.message = error.ErrorMessage;
                            TMmodel.errors = errors;
                            model = TMmodel;
                        }
                    }

                }
                else
                {

                    model = viewResult.Model;

                }


            }
            else if (actionResult.GetType() == typeof(PartialViewResult))
            {
                PartialViewResult partialViewResult = (PartialViewResult)actionResult;
                model = partialViewResult.Model;
            }
            else if (actionResult.GetType() == typeof(RedirectResult))
            {
                RedirectResult partialViewResult = (RedirectResult)actionResult;
                StoresViewModel TMmodel = new StoresViewModel();
                Errors errors = new Errors();

                //if (partialViewResult.RouteValues.ContainsKey("message"))
                    //{
                    //    errors.message = (string)partialViewResult.RouteValues["message"];
                    //}

                    //if (partialViewResult.RouteValues.ContainsKey("statuscode"))
                    //{
                    //    errors.statuscode = (int)partialViewResult.RouteValues["statuscode"];
                    //}


                    TMmodel.errors = errors;
                model = TMmodel;
                //errors.message = partialViewResult.RouteValues[1];
            }
            else
            {
                throw new InvalidOperationException(string.Format("Actionresult of type {0} is not supported by ModelFromResult extractor.", actionResult.GetType()));
            }
            

            return (StoresViewModel)model;
        }

    }

   
   
}
