using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuildABandHub.ActionFilters
{
    public class GlobalRouting : IActionFilter
    {
        private readonly ClaimsPrincipal _claimsPrincipal; 
        public GlobalRouting(ClaimsPrincipal claimsPrincipal) 
        {
            _claimsPrincipal = claimsPrincipal; 
        }

        public void OnActionExecuting(ActionExecutingContext context) 
        {
            var controller = context.RouteData.Values["controller"];
            if (controller.Equals("Home")) 
            {
                if (_claimsPrincipal.IsInRole("Musician")) 
                {
                    //context.Result = new RedirectToActionResult("Index", "Musicians", null);
                }
                else if (_claimsPrincipal.IsInRole("Band"))
                {   
                    //context.Result = new RedirectToActionResult("Index", "Bands", null); 
                } 
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
