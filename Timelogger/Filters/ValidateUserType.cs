using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static Timelogger.Utils.Enums;

namespace Timelogger.Filters
{
    public class ValidationUserType : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.HttpContext.Request.Query.ContainsKey("usertype"))
            {
                context.Result = new BadRequestObjectResult(new { Errors = "An usertype is required" });
                return;
            }
            if (!Enum.TryParse(context.HttpContext.Request.Query.FirstOrDefault(x => x.Key == "usertype").Value, out UserTypes userTypes) || !Enum.GetNames(typeof(UserTypes)).ToList().Contains(userTypes.ToString()))
            {
                context.Result = new BadRequestObjectResult(new { Errors = "A valid usertype is required" });
                return;
            }

            switch (userTypes)
            {
                case UserTypes.Freelancer:
                    context.RouteData.Values["controller"] = "ProjectsFreelancerController";
                    //context.Result = new RedirectToActionResult(context.ActionDescriptor.RouteValues["action"], "ProjectsFreelancerController", new { });
                    break;
                case UserTypes.Customer:
                    context.Result = new RedirectToActionResult(context.ActionDescriptor.RouteValues["action"], "ProjectsCustomerController", new { });
                    break;
            }
        }
    }
}
