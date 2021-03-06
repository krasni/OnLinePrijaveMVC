using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLinePrijaveMVC.Mvc.Attributes
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTransfer).FullName;
    }

    public class ExportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Export if we are redirecting
            if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
            {
                filterContext.Controller.TempData[Key] = filterContext.Controller.ViewData.ModelState;
            }

            base.OnActionExecuted(filterContext);
        }
    }

    public class ImportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ModelStateDictionary modelState = filterContext.Controller.TempData[Key] as ModelStateDictionary;

            //Only Import if we are viewing
            if (filterContext.Result is ViewResult)
            {
                filterContext.Controller.ViewData.ModelState.Merge(modelState);
            }
            else
            {
                //Otherwise remove it.
                filterContext.Controller.TempData.Remove(Key);
            }

            base.OnActionExecuted(filterContext);
        }
    }
}