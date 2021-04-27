using AutomatskiPDFMVC.Filters;
using System.Web;
using System.Web.Mvc;

namespace OnLinePrijaveMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleExceptionsAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
