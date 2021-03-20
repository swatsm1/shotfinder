using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShotFinderMVC.Class
{
    /// <summary>
    /// Null Object Pattern
    /// Since this is Net4.8 and c#7.3
    /// cannot use nullable reference in c#8.0
    /// </summary>
    public class SessionVar
    {
        static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new ApplicationException("No Http Context, No Session to Get!");

                return HttpContext.Current.Session;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            var stringkey = Session[key];
            if (Session[key].IsNull())

                
                return default(T);
            else
                return (T)Session[key];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(string key, T value)
        {
            Session[key] = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            string s = Get<string>(key);
            return s is null ? string.Empty : s;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        
        /// 
        ///public static int? GetInteger(string key)
        ///{
        ///    int? s = Get<int>(key);
        ///    return s is null ? 0 : s;
        ///}

        
       
        public static int GetInteger(string key)
        {
            int s = Get<int>(key);
            return s.IsNull() ? 0 : s;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public static class NullableObjectExtensions
    {
        public static bool IsNull<T>(this T objectInstance) where T : new()
        {
            return objectInstance == null;
        }

        public static bool IsNotNull<T>(this T objectInstance) where T : new()
        {
            return !objectInstance.IsNull();
        }
    }
}