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
    public class PacienteController : Controller
    {

        IndexModelPaciente PacienteModel = new IndexModelPaciente();

        private readonly string _connectionString;
        private readonly ServicioPaciente  _servicioPaciente;


        public PacienteController()
        {
            this._connectionString = ConfigurationManager.ConnectionStrings["myConnectionStringLocal"].ConnectionString;
            this._servicioPaciente = new ServicioPaciente(_connectionString);
        }

        public ActionResult Index(int pagina = 1)
        {

            var pacientes = new List<PacienteModel>();

            foreach (var paciente in _servicioPaciente.ConsultarPacientes(
                new DtoConsultaPag() { Tamanio_Pagina = PacienteModel.Pg.TAMANIO_PAGINA, Pagina = pagina }))
            {
                pacientes.Add(new PacienteModel()
                {


                    Id = paciente.Id,
                    Nombre = paciente.Nombre,
                    ApellidoPaterno = paciente.ApellidoPaterno,
                    ApellidoMaterno = paciente.ApellidoMaterno,
                    Edad = paciente.Edad,
                    Peso = paciente.Peso,
                    Sexo = paciente.Sexo


                });
            }

            ConsultarPacientes(_servicioPaciente.ConsultarTotalPacientes(), pacientes, pagina);
            return View(PacienteModel);
        }

        public ActionResult DetallePaciente(Guid idPaciente)
        {
            DtoPaciente paciente;
            PacienteModel pacienteModel;
            try
            {


                paciente = _servicioPaciente.ConsultarPaciente(idPaciente);

                pacienteModel = new PacienteModel()
                {


                    Id = paciente.Id,
                    Nombre = paciente.Nombre,
                    ApellidoPaterno = paciente.ApellidoPaterno,
                    ApellidoMaterno = paciente.ApellidoMaterno,
                    Edad = paciente.Edad,
                    Peso = paciente.Peso,
                    Sexo = paciente.Sexo

                }
                ;

                return View(pacienteModel);
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
        public ActionResult CrearNuevo(PacienteModel pacienteModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Crear");
                }



                _servicioPaciente.RegistrarPaciente(new DtoPaciente()
                {



                    Nombre = pacienteModel.Nombre,
                    ApellidoPaterno = pacienteModel.ApellidoPaterno,
                    ApellidoMaterno = pacienteModel.ApellidoMaterno,
                    Edad = pacienteModel.Edad,
                    Peso = pacienteModel.Peso,
                    Sexo = pacienteModel.Sexo

                }

                );

                TempData["RegistroPacEx"] = true;

                return RedirectToAction("Crear", "Paciente");
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

        public ActionResult Editar(Guid idPaciente)
        {
            DtoPaciente paciente;
            PacienteModel pacienteModel;
            try
            {


                paciente = _servicioPaciente.ConsultarPaciente(idPaciente);

                pacienteModel = new PacienteModel()
                {


                    Id = paciente.Id,
                    Nombre = paciente.Nombre,
                    ApellidoPaterno = paciente.ApellidoPaterno,
                    ApellidoMaterno = paciente.ApellidoMaterno,
                    Edad = paciente.Edad,
                    Peso = paciente.Peso,
                    Sexo = paciente.Sexo

                };

                return View(pacienteModel);
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
        public ActionResult EditarPaciente(PacienteModel pacienteModel)
        {
            DtoPaciente dtoPaciente;
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Editar");
                }

                dtoPaciente = new DtoPaciente()
                {


                    Id = pacienteModel.Id,
                    Nombre = pacienteModel.Nombre,
                    ApellidoPaterno = pacienteModel.ApellidoPaterno,
                    ApellidoMaterno = pacienteModel.ApellidoMaterno,
                    Edad = pacienteModel.Edad,
                    Peso = pacienteModel.Peso,
                    Sexo = pacienteModel.Sexo

                };


                _servicioPaciente.EditarPaciente(dtoPaciente);

                TempData["EdicionPacEx"] = true;

                return View("Editar");
            }
            catch (ExcepcionComun e)
            {
                TempData["ErrorCo"] = e.Mensaje;
                return RedirectToAction("Editar", "Doctor");

            }
            catch (Exception e)
            {

                return RedirectToAction("Error", "Login");

            }
        }

        [HttpPost]
        public ActionResult EliminarPaciente(Guid idPaciente)
        {
            try
            {

                _servicioPaciente.BorrarPaciente(idPaciente);

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

        private void ConsultarPacientes(int totalPacientes, IEnumerable<PacienteModel> pacientes, int pagina = 1)
        {


            PacienteModel.Pg.Total_Paginas = (int)Math.Ceiling(((decimal)totalPacientes / (decimal)PacienteModel.Pg.TAMANIO_PAGINA));

            if (PacienteModel.Pg.Total_Paginas == 0)
            {
                PacienteModel.Pg.Siguiente = false;
                PacienteModel.Pg.Anterior = false;
                PacienteModel.Pg.HayRegistros = false;
                PacienteModel.Pacientes = new List<PacienteModel>();

            }
            else
            {
                PacienteModel.Pg.Pagina_Actual = pagina;
                PacienteModel.Pg.HayRegistros = true;


                if (PacienteModel.Pg.Pagina_Actual == 1 && PacienteModel.Pg.Total_Paginas > 1)
                {
                    PacienteModel.Pg.Anterior = false;
                    PacienteModel.Pg.Siguiente = true;
                }
                else if (PacienteModel.Pg.Pagina_Actual == 1 && PacienteModel.Pg.Total_Paginas > 1)
                {
                    PacienteModel.Pg.Anterior = false;
                    PacienteModel.Pg.Siguiente = true;
                }
                else if (PacienteModel.Pg.Total_Paginas == 1)
                {
                    PacienteModel.Pg.Anterior = false;
                    PacienteModel.Pg.Siguiente = false;
                }
                else if (PacienteModel.Pg.Pagina_Actual == PacienteModel.Pg.Total_Paginas && PacienteModel.Pg.Total_Paginas > 1)
                {
                    PacienteModel.Pg.Anterior = true;
                    PacienteModel.Pg.Siguiente = false;

                }
                else
                {
                    PacienteModel.Pg.Anterior = true;
                    PacienteModel.Pg.Siguiente = true;

                }

                //PacienteModel.Doctores = PacienteModel.Doctores.Skip((pagina - 1) * PacienteModel.Pg.TAMANIO_PAGINA).Take(PacienteModel.Pg.TAMANIO_PAGINA).ToList();

                PacienteModel.Pacientes = pacientes;

                PacienteModel.Pg.Bloques_De_Paginas = (int)Math.Ceiling((decimal)PacienteModel.Pg.Total_Paginas / (decimal)PacienteModel.Pg.TAMANIO_BLOQUES);
                PacienteModel.Pg.Bloque_Actual = ((decimal)PacienteModel.Pg.Pagina_Actual) / (decimal)(PacienteModel.Pg.Bloque_Actual * PacienteModel.Pg.TAMANIO_BLOQUES) <= 1 ? PacienteModel.Pg.Bloque_Actual : (PacienteModel.Pg.Bloque_Actual += 1);
                PacienteModel.Pg.Min_Bloque = PacienteModel.Pg.Bloque_Actual * PacienteModel.Pg.TAMANIO_BLOQUES - 2;
                PacienteModel.Pg.Max_Bloque = PacienteModel.Pg.Bloque_Actual * PacienteModel.Pg.TAMANIO_BLOQUES;
            }

        }
    }
}
