using Autofac;
using Autofac.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShotFinder
{
    public class MvcApplication : System.Web.HttpApplication 
    {
           
        private static HashSet<string> _fileExtensionsToCache;

        private static HashSet<string> FileExtensionsToCache
        {
            get
            {
                if (_fileExtensionsToCache == null)
                {
                    _fileExtensionsToCache = new HashSet<string>
                    {
                        ".css",
                        ".js",
                        ".gif",
                        ".jpg",
                        ".png"
                    };
                }

                return _fileExtensionsToCache;
            }
        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

          

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
            {
                Response.RedirectPermanent("https://" + Request.ServerVariables["HTTP_HOST"]
            + HttpContext.Current.Request.RawUrl);
            }

            switch (Request.Url.Scheme)
            {
                case "https":
                    Response.AddHeader("Strict-Transport-Security", "max-age=300");
                    break;
                case "http":
                    var path = "https://" + Request.Url.Host + Request.Url.PathAndQuery;
                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", path);
                    break;
            }



            this.Response.Headers.Remove("Server");
            this.Response.Headers["X-FRAME-OPTIONS"] = "DENY";
            this.Response.Headers.Add("X-Content-Type-Options", "nosniff");

            //this.Response.AddHeader("Strict-Transport-Security", "max-age=31536000");

            var cache = HttpContext.Current.Response.Cache;

            if (FileExtensionsToCache.Contains(Request.CurrentExecutionFilePathExtension))
            {
                cache.SetExpires(DateTime.UtcNow.AddDays(1));
                cache.SetValidUntilExpires(true);
                cache.SetCacheability(HttpCacheability.Private);
            }
            else
            {
                cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                cache.SetValidUntilExpires(false);
                cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                cache.SetCacheability(HttpCacheability.NoCache);
                cache.SetNoStore();
            }


        }

    }
}
