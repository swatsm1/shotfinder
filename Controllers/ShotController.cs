using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShotFinderMVC.Class;
using ShotFinderMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShotFinderMVC.Controllers
{
    public class ShotController : Controller
    {
      

        // GET: ShotController

        private static readonly HttpClient _client;

        static ShotController()
        {
            _client = new HttpClient();
        }
        public ActionResult Index()
        {
            try
            {
                StoresViewModel StoreModel = new StoresViewModel();

                StoreModel = null;
                return View();
              
            }
            catch (Exception ex)
            {
                if (!(TempData == null))
                {
                    TempData["Message"] = ($"Error { ex.Message }");
                    return RedirectToAction("500", "Error");
                }
                else

                    return RedirectToAction("500", "Error", ex.Message);

            }
        }
        public async Task<ActionResult> FilterIndex(string SearchString)
        {
            try
            {
                StoresViewModel StoreModel = new StoresViewModel();


                if (String.IsNullOrEmpty(SearchString))
                {
                    ModelState.AddModelError("SearchString", "Zipcode must contain a value.");
                    return (ActionResult)View(StoreModel);
                }

                List<Stores> mystores = new List<Stores>();

                ///Web Service to Consume
                ///https://www.riteaid.com/services/ext/v2/vaccine/checkSlots?storeNumber=1870

                ///https://www.riteaid.com/services/ext/v2/stores/getStores?address=08889&attrFilter=PREF-112&fetchMechanismVersion=2&radius=50

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://www.riteaid.com/services/ext/v2/stores/getStores?address=" + SearchString + "&attrFilter=PREF-112&fetchMechanismVersion=2&radius=50"),
                    Method = HttpMethod.Get,
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
                request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
                request.Headers.Referrer = new Uri("https://www.riteaid.com/pharmacy/apt-scheduler");

                var responseTask = _client.SendAsync(request);
                responseTask.Wait();

                StoreData.Root Stores;

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = await result.Content.ReadAsStringAsync();

                    Stores = JsonConvert.DeserializeObject<StoreData.Root>(readTask);

                    if (Stores.IsNotNull())
                    {
                        if (!string.IsNullOrEmpty((string)Stores.ErrCde))
                        {
                            throw new Exception("Web service error -> " + Stores.ErrMsg);
                        }

                    }
                    else
                    {
                        var error = new Errors()
                        {
                            message = "[GetStores Call] Stores object returned a Null reference"
                        };


                        var view = new StoresViewModel()
                        {
                            errors = error
                        };

                        return View(view);
                    }

                    
                }
                else //web api sent error response 
                {

                    //log response status here..

                    Errors error = await globalErrorsAsync(result);

                    if (error.statuscode > 0)
                    {
                        return RedirectToAction("Error500", "Errors", error);

                    }

                    return (ActionResult)View();
                }

                StoreData.Data Data = Stores.Data;

                foreach (StoreData.Store store in Data.stores)
                {

                    //HTTP GET

                    request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri("https://www.riteaid.com/services/ext/v2/vaccine/checkSlots?storeNumber=" + store.storeNumber),
                        Method = HttpMethod.Get,
                    };
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
                    request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    request.Headers.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
                    request.Headers.Referrer = new Uri("https://www.riteaid.com/pharmacy/apt-scheduler");

                    responseTask = _client.SendAsync(request);
                    responseTask.Wait();
                    result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        Root slots = new Root();

                        var readTask = await result.Content.ReadAsStringAsync();

                        slots = JsonConvert.DeserializeObject<Root>(readTask);

                        if (slots.IsNotNull())
                        {
                            if (!string.IsNullOrEmpty((string)slots.ErrCde))
                            {
                                throw new Exception("[Check Slots ]Web service error -> " + slots.ErrMsg);
                            }

                        }
                        else
                        {
                            var error = new Errors()
                            {
                                message = "[Check Slots] Slots object returned a Null reference"
                            };


                            var view = new StoresViewModel()
                            {
                                errors = error
                            };

                            return View(view);
                        }


                        if (slots.Data.IsNull())
                        {
                           
                        
                            var error = new Errors()
                            {
                                message = "[Check Slots] Slots.Data object returned a Null reference"
                            };


                            var view = new StoresViewModel()
                            {
                                errors = error
                            };

                            return View(view);
                        }

                        var SlotData = slots.Data;
                        var StoreSlot = SlotData.slots;

                        string reason = string.Empty;

                        
                        if (!string.IsNullOrEmpty((string)slots.ErrCde))
                        {
                            throw new Exception("Web service error -> " + slots.ErrMsg);
                        }



                        if (StoreSlot._1 == true )
                        {
                            reason = "Store Has Appointments";
                        }
                        else
                            reason = "No Appointments";

                        if (StoreSlot._1 == true )
                           if( StoreSlot._2 == true)
                            {
                                reason = "Store Has Appointment times";
                            }
                            


                        Stores stores = new Stores()
                        {
                            Address = store.address,
                            StoreNumber = store.storeNumber,
                            city=store.city,
                            State=store.state,
                            Zip=store.fullZipCode,
                            Status = reason

                        };


                        mystores.Add(stores);




                    }
                    else //web api sent error response 
                    {

                        //log response status here..

                        Errors error = await globalErrorsAsync(result);

                        if (error.statuscode > 0)
                        {
                            return RedirectToAction("Error500", "Errors", error);
                        }

                        return (ActionResult)View();
                    }


                }

                StoreModel.ShotStores = mystores;


                return (ActionResult)View(StoreModel);
            }
            catch (Exception ex)
            {
                if (!(TempData == null))
                {
                    TempData["Message"] = ($"Error { ex.Message }");
                    return RedirectToAction("Error500", "Errors");
                }
                else

                    return RedirectToAction("Error500", "Errors", ex.Message);
            }
        }

        // GET: ShotController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShotController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShotController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShotController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShotController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShotController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShotController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private async Task<Errors> globalErrorsAsync(HttpResponseMessage result)
        {
            Errors errors = new Errors();

            errors.statuscode = (int)result.StatusCode;
            errors.message = result.StatusCode.ToString();

            string content = await result.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(content))
            {
                var readTask = await result.Content.ReadAsStringAsync();
                string[] sError = readTask.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (!(sError == null))
                {
                    if (sError.Length >= 2)
                    {
                        errors.message = sError[1].ToString();

                        if (TempData == null)
                        {

                            errors.message = sError[0].ToString();
                            return errors;
                        }
                        else

                        if (sError[0].ToString().Contains("Exists"))
                        {

                        }
                        else
                        {
                            TempData["Message"] = ($"Server error.{readTask.ToString()}  Please contact administrator.");
                        }

                        string[] lineError = sError[0].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                        if (!(lineError == null))
                        {
                            if (lineError.Length >= 2)
                            {
                                errors.message = lineError[1].ToString();
                            }
                        }

                    }
                }

            }
            else
            {
                if (TempData == null)
                {
                    return errors;
                }
                else

                    TempData["Message"] = ($"The Request Returned and Error Status Code : {errors.statuscode} : Reason : {errors.message}.");
            }

            return errors;


        }
    }
}
