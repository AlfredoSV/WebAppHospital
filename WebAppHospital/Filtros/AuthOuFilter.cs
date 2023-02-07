using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace WebAppHospital.Filtros
{
    public class AuthOuFilter : ActionFilterAttribute
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            filterContext.HttpContext.Session.Remove("UsuarioId");
            filterContext.HttpContext.Session.Remove("UsuarioNombre");

            /*if (!string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UsuarioID"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }*/
        }


        /*public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
           

            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {

                filterContext.Result = new RedirectToRouteResult(
                                             new RouteValueDictionary{{ "controller", "Inicio" },
                                          { "action", "Index" }

                                          });
            }

        }*/
    }
}
