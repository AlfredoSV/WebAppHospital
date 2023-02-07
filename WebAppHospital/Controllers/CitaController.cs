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

namespace WebAppHospital.Controllers
{
    [AuthFilter]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class CitaController : Controller
    {
        IndexModelCita CitaModel = new IndexModelCita();

        private readonly string _connectionString;
        private readonly ServicioCita _servicioCita;
        private readonly ServicioPaciente _servicioPaciente;
        private readonly ServicioDoctor _servicioDoctor;


        public CitaController()
        {
            this._connectionString = ConfigurationManager.ConnectionStrings["myConnectionStringLocal"].ConnectionString;
            this._servicioCita = new ServicioCita(_connectionString);
            this._servicioPaciente = new ServicioPaciente(_connectionString);
            this._servicioDoctor = new ServicioDoctor(_connectionString);
        }

        public ActionResult Index(int pagina = 1)
        {

            var citas = new List<CitaModel>();

            foreach (var cita in _servicioCita.ConsultarCitas(
                new DtoConsultaPag() { Tamanio_Pagina = CitaModel.Pg.TAMANIO_PAGINA, Pagina = pagina }))
            {
                citas.Add(new CitaModel()
                {


                    Id = cita.Id,
                    IdDoctor = cita.IdDoctor,
                    IdPaciente = cita.IdDoctor,
                    Comentarios = cita.Comentarios,
                    Horario = cita.HorarioCita,
                    FechaCita = cita.FechaCita,
                    NombreDoctor = cita.NombreDoctor,
                    NombrePaciente =cita.NombrePaciente



                });
            }

            ConsultarCitas(_servicioCita.ConsultarTotalCitas(), citas, pagina);
            return View(CitaModel);
        }

        public ActionResult DetalleCita(Guid idCita)
        {
            DtoCita cita;
            CitaModel citaModel;
            try
            {


                cita = _servicioCita.ConsultarCita(idCita);

                citaModel = new CitaModel()
                {


                    Id = cita.Id,
                    IdDoctor = cita.IdDoctor,
                    IdPaciente = cita.IdDoctor,
                    Comentarios = cita.Comentarios,
                    Horario = cita.HorarioCita,
                    FechaCita = cita.FechaCita,
                     NombreDoctor = cita.NombreDoctor,
                    NombrePaciente = cita.NombrePaciente


                }
                ;

                return View(citaModel);
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
            
            List<PacienteModelSelect> pacientes = new List<PacienteModelSelect>();
            List<DoctorModelSelect> doctores = new List<DoctorModelSelect>();


            try
            {
                var pacientesR = _servicioPaciente.ConsultarTodosLosPacientes();

                foreach (var paciente in pacientesR)
                {
                    pacientes.Add(
                        new PacienteModelSelect()
                        {
                            Id = paciente.Id,
                            Nombre = paciente.Nombre
                        });

                }

                ViewBag.Pacientes = pacientes;

                var doctoresR = _servicioDoctor.ConsultarTodosLosDoctores();

                foreach (var doctor in doctoresR)
                {
                    doctores.Add(
                        new DoctorModelSelect()
                        {
                            Id = doctor.Id,
                            Nombre = doctor.Nombre
                        });

                }


                ViewBag.Doctores = doctores;


                

                return View();
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
        public ActionResult CrearNuevo(CitaModel CitaModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Crear");
                }



                _servicioCita.RegistrarCita(new DtoCita()
                {


                    Id = CitaModel.Id,
                    IdDoctor = CitaModel.IdDoctor,
                    IdPaciente = CitaModel.IdPaciente,
                    Comentarios = CitaModel.Comentarios,
                    HorarioCita = CitaModel.Horario,
                    FechaCita = CitaModel.FechaCita



                }

                );

                TempData["RegistroCitaEx"] = true;

                return RedirectToAction("Crear", "Cita");
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

        public ActionResult Editar(Guid idCita)
        {
            DtoCita cita;
            CitaModel CitaModel;
            try
            {


                cita = _servicioCita.ConsultarCita(idCita);

                CitaModel = new CitaModel()
                {


                    Id = cita.Id,
                    IdDoctor = cita.IdDoctor,
                    IdPaciente = cita.IdDoctor,
                    Comentarios = cita.Comentarios,
                    Horario = cita.HorarioCita,
                    FechaCita = cita.FechaCita



                };

                return View(CitaModel);
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
        public ActionResult EditarCita(CitaModel CitaModel)
        {
            DtoCita dtoCita;
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Editar");
                }

                dtoCita = new DtoCita()
                {


                    Id = CitaModel.Id,
                    IdDoctor = CitaModel.IdDoctor,
                    IdPaciente = CitaModel.IdDoctor,
                    Comentarios = CitaModel.Comentarios,
                    HorarioCita = CitaModel.Horario,
                    FechaCita = CitaModel.FechaCita



                };


                _servicioCita.EditarCita(dtoCita);

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
        public ActionResult EliminarCita(Guid idCita)
        {
            try
            {

                _servicioCita.BorrarCita(idCita);

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

        private void ConsultarCitas(int totalCitas, IEnumerable<CitaModel> Citas, int pagina = 1)
        {


            CitaModel.Pg.Total_Paginas = (int)Math.Ceiling(((decimal)totalCitas / (decimal)CitaModel.Pg.TAMANIO_PAGINA));

            if (CitaModel.Pg.Total_Paginas == 0)
            {
                CitaModel.Pg.Siguiente = false;
                CitaModel.Pg.Anterior = false;
                CitaModel.Pg.HayRegistros = false;
                CitaModel.Citas = new List<CitaModel>();

            }
            else
            {
                CitaModel.Pg.Pagina_Actual = pagina;
                CitaModel.Pg.HayRegistros = true;


                if (CitaModel.Pg.Pagina_Actual == 1 && CitaModel.Pg.Total_Paginas > 1)
                {
                    CitaModel.Pg.Anterior = false;
                    CitaModel.Pg.Siguiente = true;
                }
                else if (CitaModel.Pg.Pagina_Actual == 1 && CitaModel.Pg.Total_Paginas > 1)
                {
                    CitaModel.Pg.Anterior = false;
                    CitaModel.Pg.Siguiente = true;
                }
                else if (CitaModel.Pg.Total_Paginas == 1)
                {
                    CitaModel.Pg.Anterior = false;
                    CitaModel.Pg.Siguiente = false;
                }
                else if (CitaModel.Pg.Pagina_Actual == CitaModel.Pg.Total_Paginas && CitaModel.Pg.Total_Paginas > 1)
                {
                    CitaModel.Pg.Anterior = true;
                    CitaModel.Pg.Siguiente = false;

                }
                else
                {
                    CitaModel.Pg.Anterior = true;
                    CitaModel.Pg.Siguiente = true;

                }

                //CitaModel.Doctores = CitaModel.Doctores.Skip((pagina - 1) * CitaModel.Pg.TAMANIO_PAGINA).Take(CitaModel.Pg.TAMANIO_PAGINA).ToList();

                CitaModel.Citas = Citas;

                CitaModel.Pg.Bloques_De_Paginas = (int)Math.Ceiling((decimal)CitaModel.Pg.Total_Paginas / (decimal)CitaModel.Pg.TAMANIO_BLOQUES);
                CitaModel.Pg.Bloque_Actual = ((decimal)CitaModel.Pg.Pagina_Actual) / (decimal)(CitaModel.Pg.Bloque_Actual * CitaModel.Pg.TAMANIO_BLOQUES) <= 1 ? CitaModel.Pg.Bloque_Actual : (CitaModel.Pg.Bloque_Actual += 1);
                CitaModel.Pg.Min_Bloque = CitaModel.Pg.Bloque_Actual * CitaModel.Pg.TAMANIO_BLOQUES - 2;
                CitaModel.Pg.Max_Bloque = CitaModel.Pg.Bloque_Actual * CitaModel.Pg.TAMANIO_BLOQUES;
            }

        }
    }
}
