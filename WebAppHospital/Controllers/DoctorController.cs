using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppHospital.Aplicacion.Servicios;
using WebAppHospital.Dominio.DTOs;
using WebAppHospital.Dominio.ManejoErrores;
using WebAppHospital.Filtros;
using WebAppHospital.Models;

namespace WebAppHospital.Controllers
{
    [AuthFilter]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class DoctorController : Controller
    {
        IndexModelDoctor DoctorModel = new IndexModelDoctor();

        private readonly string _connectionString;
        private readonly ServicioDoctor _servicioDoctor;

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(DoctorController));


        public DoctorController()
        {
            this._connectionString = ConfigurationManager.ConnectionStrings["myConnectionStringLocal"].ConnectionString;
            this._servicioDoctor = new ServicioDoctor(_connectionString);
        }

        public ActionResult Index(int pagina = 1)
        {

            var doctores = new List<DoctorModel>();

            foreach (var doctor in _servicioDoctor.ConsultarDoctores(
                new DtoConsultaPag() { Tamanio_Pagina = DoctorModel.Pg.TAMANIO_PAGINA, Pagina = pagina }))
            {
                doctores.Add(new DoctorModel()
                {


                    Id = doctor.Id,
                    Nombre = doctor.Nombre,
                    ApellidoPaterno = doctor.ApellidoPaterno,
                    ApellidoMaterno = doctor.ApellidoMaterno,
                    Especialidad = doctor.Especialidad,
                    Departamento = doctor.Departamento,
                    NumCedula = doctor.NumCedula

                });
            }

            ConsultarDoctotores(_servicioDoctor.ConsultarTotalDoctores(),doctores,pagina);
            return View(DoctorModel);
        }
 
        public ActionResult DetalleDoctor(Guid idDoctor)
        {
            DtoDoctor doctorC;
            DoctorModel doctorModel;
            try
            {


                doctorC = _servicioDoctor.ConsultaDoctor(idDoctor);

                doctorModel = new DoctorModel()
                {


                    Id = doctorC.Id,
                    Nombre = doctorC.Nombre,
                    ApellidoPaterno = doctorC.ApellidoPaterno,
                    ApellidoMaterno = doctorC.ApellidoMaterno,
                    Especialidad = doctorC.Especialidad,
                    Departamento = doctorC.Departamento,
                    NumCedula = doctorC.NumCedula

                };

                return View(doctorModel);
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

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearNuevo(DoctorModel doctorModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Crear");
                }



                _servicioDoctor.RegistrarDoctor(new DtoDoctor() {

                    Nombre = doctorModel.Nombre,
                    ApellidoMaterno = doctorModel.ApellidoMaterno,
                    ApellidoPaterno = doctorModel.ApellidoPaterno,
                    Especialidad = doctorModel.Especialidad,
                    Departamento = doctorModel.Departamento,
                    NumCedula = doctorModel.NumCedula
                }
                );

                TempData["RegistroExDoc"] = true;

                return RedirectToAction("Crear", "Doctor");
            }
            catch (ExcepcionComun e)
            {

                ViewBag.ErrorCo = e.Mensaje;
                return View("Index");

            }
            catch (Exception e)
            {
                _logger.Error("Método - Crear - Controller - Doctor", e);
                return RedirectToAction("Error", "Login");

            }
        }

        public ActionResult Editar(Guid idDoctor)
        {
            DtoDoctor doctorC;
            DoctorModel doctorModel;
            try
            {


                doctorC = _servicioDoctor.ConsultaDoctor(idDoctor);

                doctorModel = new DoctorModel()
                {


                    Id = doctorC.Id,
                    Nombre = doctorC.Nombre,
                    ApellidoPaterno = doctorC.ApellidoPaterno,
                    ApellidoMaterno = doctorC.ApellidoMaterno,
                    Especialidad = doctorC.Especialidad,
                    Departamento = doctorC.Departamento,
                    NumCedula = doctorC.NumCedula

                };

                return View(doctorModel);
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
        public ActionResult EditarDoctor(DoctorModel doctorModel)
        {
            DtoDoctor dtoDoctor;
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Editar");
                }

                dtoDoctor = new DtoDoctor()
                {


                    Id = doctorModel.Id,
                    Nombre = doctorModel.Nombre,
                    ApellidoPaterno = doctorModel.ApellidoPaterno,
                    ApellidoMaterno = doctorModel.ApellidoMaterno,
                    Especialidad = doctorModel.Especialidad,
                    Departamento = doctorModel.Departamento,
                    NumCedula = doctorModel.NumCedula

                };

                _servicioDoctor.EditarDoctor(dtoDoctor);

                TempData["EdicionDocEx"] = true;

                return View("Editar");
            }
            catch (ExcepcionComun e)
            {
                TempData["ErrorCo"] = e.Mensaje;
                return RedirectToAction("Editar", "Doctor");

            }
            catch (Exception e)
            {
                _logger.Error("Método - EditarDoc - Controller - Doctor", e);

                return RedirectToAction("Error", "Login");

            }
        }

        [HttpPost]
        public ActionResult EliminarDoctor(Guid idDoctor)
        {
            try
            {

                _servicioDoctor.BorrarDoctor(idDoctor);

                return new HttpStatusCodeResult(200);
            }
            catch (ExcepcionComun e)
            {
                return new HttpStatusCodeResult(5000, e.Mensaje);

            }
            catch (Exception e)
            {

                return RedirectToAction("Error", "Login");

            }
        }

        private void ConsultarDoctotores( int totalDoctores,IEnumerable<DoctorModel> doctores, int pagina = 1)
        {


            DoctorModel.Pg.Total_Paginas = (int)Math.Ceiling(((decimal)totalDoctores / (decimal)DoctorModel.Pg.TAMANIO_PAGINA));

            if (DoctorModel.Pg.Total_Paginas == 0)
            {
                DoctorModel.Pg.Siguiente = false;
                DoctorModel.Pg.Anterior = false;
                DoctorModel.Pg.HayRegistros = false;
                DoctorModel.Doctores = new List<DoctorModel>();

            }
            else
            {
                DoctorModel.Pg.Pagina_Actual = pagina;
                DoctorModel.Pg.HayRegistros = true;


                if (DoctorModel.Pg.Pagina_Actual == 1 && DoctorModel.Pg.Total_Paginas > 1)
                {
                    DoctorModel.Pg.Anterior = false;
                    DoctorModel.Pg.Siguiente = true;
                }
                else if (DoctorModel.Pg.Pagina_Actual == 1 && DoctorModel.Pg.Total_Paginas > 1)
                {
                    DoctorModel.Pg.Anterior = false;
                    DoctorModel.Pg.Siguiente = true;
                }
                else if (DoctorModel.Pg.Total_Paginas == 1)
                {
                    DoctorModel.Pg.Anterior = false;
                    DoctorModel.Pg.Siguiente = false;
                }
                else if (DoctorModel.Pg.Pagina_Actual == DoctorModel.Pg.Total_Paginas && DoctorModel.Pg.Total_Paginas > 1)
                {
                    DoctorModel.Pg.Anterior = true;
                    DoctorModel.Pg.Siguiente = false;

                }
                else
                {
                    DoctorModel.Pg.Anterior = true;
                    DoctorModel.Pg.Siguiente = true;

                }

                //DoctorModel.Doctores = DoctorModel.Doctores.Skip((pagina - 1) * DoctorModel.Pg.TAMANIO_PAGINA).Take(DoctorModel.Pg.TAMANIO_PAGINA).ToList();

                DoctorModel.Doctores = doctores;

                DoctorModel.Pg.Bloques_De_Paginas = (int)Math.Ceiling((decimal)DoctorModel.Pg.Total_Paginas / (decimal)DoctorModel.Pg.TAMANIO_BLOQUES);
                DoctorModel.Pg.Bloque_Actual = ((decimal)DoctorModel.Pg.Pagina_Actual) / (decimal)(DoctorModel.Pg.Bloque_Actual * DoctorModel.Pg.TAMANIO_BLOQUES) <= 1 ? DoctorModel.Pg.Bloque_Actual : (DoctorModel.Pg.Bloque_Actual += 1);
                DoctorModel.Pg.Min_Bloque = DoctorModel.Pg.Bloque_Actual * DoctorModel.Pg.TAMANIO_BLOQUES - 2;
                DoctorModel.Pg.Max_Bloque = DoctorModel.Pg.Bloque_Actual * DoctorModel.Pg.TAMANIO_BLOQUES;
            }

        }
    }
}
