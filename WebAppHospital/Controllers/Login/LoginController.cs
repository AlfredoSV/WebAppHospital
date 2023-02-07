using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppHospital.Aplicacion.Servicios;
using WebAppHospital.Dominio.DTOs;
using WebAppHospital.Dominio.Entidades;
using WebAppHospital.Dominio.ManejoErrores;
using WebAppHospital.Filtros;
using WebAppHospital.Models;

namespace WebAppHospital.Controllers.LoginN
{
    [AuthOuFilter]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class LoginController : Controller
    {

        private readonly ServicioUsuario _servicioUsuario;
        private readonly string _connectionString;

        public LoginController()
        {

            this._connectionString = ConfigurationManager.ConnectionStrings["myConnectionStringLocal"].ConnectionString;
            this._servicioUsuario = new ServicioUsuario(_connectionString);
        }


        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSes()
        {
            this.Session.Remove("UsuarioId");
            this.Session.Remove("UsuarioNombre");
            return RedirectToAction("Index","Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ingresar(Login login)
        {


            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Index");
                }

                var usuario = _servicioUsuario.ValidarUsuario(new DtoUsuario()
                {
                    Contrasenia = login.Contrasenia,
                    UsuarioNom =
                    login.Usuario
                });

                this.Session.Add("UsuarioId", usuario.Id);
                this.Session.Add("UsuarioNombre",usuario.UsuarioNom);

                return RedirectToAction("Index", "Inicio");
            }
            catch (ExcepcionComun e)
            {

                ViewBag.ErrorCo = e.Mensaje;
                return View("Index");

            }
            catch (Exception e)
            {

                return RedirectToAction("Error", "Login");

            }




        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarNuevo(LoginRegistro loginR)
        {


            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Registro");
                }

                _servicioUsuario.RegistrarNuevoUsuario(new DtoUsuario
                {
                    Nombre = loginR.Nombre,
                    ApellidoMaterno = loginR.ApellidoMaterno,
                    ApellidoPaterno = loginR.ApellidoPaterno,
                    Contrasenia = loginR.Contrasenia,
                    UsuarioNom = loginR.Usuario
                });

                TempData["RegistroEx"] = true;

                return RedirectToAction("Registro", "Login");
            }
            catch (ExcepcionComun e)
            {
                TempData["ErrorCo"]  = e.Mensaje;
                return RedirectToAction("Registro", "Login");

            }
            catch (Exception e)
            {

                return RedirectToAction("Error", "Login");

            }



        }

        [HttpGet]
        public ActionResult Error()
        {
            return View("Error");
        }
    }
}