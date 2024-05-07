using Api.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PharmacyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // TODO: Insérer ici la logique de vérification du rôle de la pharmacie
            if (false)
            {
                var tokenUser = context.HttpContext.User;

                if (tokenUser.Claims.FirstOrDefault(c => c.Type == "Role")!.Value != UserRole.Pharmacy.ToString())
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }


            await next();

        }
    }
}
    