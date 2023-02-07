using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppHospital.Dominio.DTOs;
using WebAppHospital.Dominio.Entidades;
using WebAppHospital.Dominio.ManejoErrores;
using WebAppHospital.Infraestructura.Repositorios;

namespace WebAppHospital.Aplicacion.Servicios
{
    public class ServicioCita
    {
        private readonly string _connectionString;
        private readonly RepositorioCitas _repositorioCitas;


        public ServicioCita(string connectionString)
        {
            this._connectionString = connectionString;
            _repositorioCitas = new RepositorioCitas(this._connectionString);

        }



        public IEnumerable<DtoCita> ConsultarCitas(DtoConsultaPag dtoConsultaPag)
        {

            int totalCitas;
            var citas = new List<DtoCita>();

            try
            {
                totalCitas = _repositorioCitas.ConsultarTotalCitas();

                foreach (var cita in _repositorioCitas.ConsultarCitas(dtoConsultaPag))
                {
                    citas.Add(new DtoCita()
                    {


                        Id = cita.Id,
                        IdPaciente = cita.IdPaciente,
                        IdDoctor = cita.IdDoctor,
                        HorarioCita = cita.HorarioCita,
                        FechaCita = cita.FechaCita,
                        Comentarios = cita.Comentarios,
                        NombreDoctor = cita.NombreDoctor,
                        NombrePaciente = cita.NombrePaciente

                    });
                }


                return citas;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int ConsultarTotalCitas()
        {


            try
            {
                return _repositorioCitas.ConsultarTotalCitas();


            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void RegistrarCita(DtoCita DtoCita)
        {
            Cita CitaReg;
            try
            {


                CitaReg = Cita.Crear( DtoCita.IdPaciente,
                    DtoCita.IdDoctor, DtoCita.FechaCita, DtoCita.Comentarios, DtoCita.HorarioCita);

                _repositorioCitas.InsertarCita(CitaReg);


            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void EditarCita(DtoCita DtoCita)
        {
            Cita CitaReg;
            try
            {

                CitaReg = Cita.Crear(DtoCita.Id, DtoCita.IdPaciente, DtoCita.IdDoctor, DtoCita.FechaCita, DtoCita.Comentarios,
                  DtoCita.HorarioCita);

                _repositorioCitas.ActualizarCita(CitaReg);


            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public DtoCita ConsultarCita(Guid idCita)
        {

            Cita cita;

            try
            {

                cita = _repositorioCitas.ConsultarCita(idCita);

                if (cita == null)
                    throw new ExcepcionComun("No existe este Cita", "Id no disponible", "Favor de validar si la Cita existe.");


                return new DtoCita()
                {


                    Id = cita.Id,
                    IdPaciente = cita.IdPaciente,
                    IdDoctor = cita.IdDoctor,
                    HorarioCita = cita.HorarioCita,
                    FechaCita = cita.FechaCita,
                    Comentarios = cita.Comentarios,
                    NombreDoctor = cita.NombreDoctor,
                    NombrePaciente = cita.NombrePaciente

                };
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void BorrarCita(Guid idCita)
        {


            try
            {

             
                _repositorioCitas.EliminarCita(idCita);


            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
