using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppHospital.Filtros;

namespace WebAppHospital.Controllers
{
    [AuthFilter]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class InicioController : Controller
    {

        public ActionResult Index()
        {
            TempData["UsuarioNombre"] = this.Session["UsuarioNombre"];
            return View();
        }
    }
}