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
    public class ServicioDoctor
    {
        private readonly string _connectionString;
        private readonly RepositorioDoctores _repositorioDoctores;
        private readonly RepositorioCitas _repositorioCitas;


        public ServicioDoctor(string connectionString)
        {
            this._connectionString = connectionString;
            _repositorioDoctores = new RepositorioDoctores(this._connectionString);
            _repositorioCitas = new RepositorioCitas(this._connectionString);
        }

        public IEnumerable<DtoDoctor> ConsultarDoctores(DtoConsultaPag dtoConsultaPag)
        {

            int totalDoctores;
            var doctores = new List<DtoDoctor>();

            try
            {
                totalDoctores = _repositorioDoctores.ConsultarTotalDoctores();

                foreach (var doctor in _repositorioDoctores.ConsultarDoctores(dtoConsultaPag))
                {
                    doctores.Add(new DtoDoctor()
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


                return doctores;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public IEnumerable<DtoDoctor> ConsultarTodosLosDoctores()
        {

           
            var doctores = new List<DtoDoctor>();

            try
            {
             
                foreach (var doctor in _repositorioDoctores.ConsultarTodosLosDoctores())
                {
                    doctores.Add(new DtoDoctor()
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


                return doctores;
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public int ConsultarTotalDoctores()
        {


            try
            {
                return _repositorioDoctores.ConsultarTotalDoctores();


            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void RegistrarDoctor(DtoDoctor dtoDoctor)
        {
            Doctor doctorReg;
            try
            {



                doctorReg = Doctor.Crear(dtoDoctor.Nombre, dtoDoctor.ApellidoPaterno, dtoDoctor.ApellidoMaterno,
                   dtoDoctor.Especialidad, dtoDoctor.Especialidad, dtoDoctor.Departamento);

                _repositorioDoctores.InsertarDoctor(doctorReg);


            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void EditarDoctor(DtoDoctor dtoDoctor)
        {
            Doctor doctorReg;
            try
            {



                doctorReg = Doctor.Crear(dtoDoctor.Id,dtoDoctor.Nombre, dtoDoctor.ApellidoPaterno, dtoDoctor.ApellidoMaterno,
                   dtoDoctor.Especialidad, dtoDoctor.Especialidad, dtoDoctor.Departamento);

                _repositorioDoctores.ActualizarDoctor(doctorReg);


            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public DtoDoctor ConsultaDoctor(Guid idDoctor)
        {

            Doctor doctor;

            try
            {

                doctor = _repositorioDoctores.ConsultarDoctor(idDoctor);

                if (doctor == null)
                    throw new ExcepcionComun("No existe este doctor", "Id no disponible", "Favor de validar si el usuario existe.");


                return new DtoDoctor()
                {


                    Id = doctor.Id,
                    Nombre = doctor.Nombre,
                    ApellidoPaterno = doctor.ApellidoPaterno,
                    ApellidoMaterno = doctor.ApellidoMaterno,
                    Especialidad = doctor.Especialidad,
                    Departamento = doctor.Departamento,
                    NumCedula = doctor.NumCedula

                };
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void BorrarDoctor(Guid idDoctor)
        {

            int citasDoctor;

            try
            {

                citasDoctor = _repositorioCitas.ConsultarCitaPorIdDoctor(idDoctor);
                
                if (citasDoctor > 0)
                    throw new ExcepcionComun("No es posible eliminar este doctor.", "Eliminación no posible.", "Este doctor tiene citas relacionadas, favor de eliminar primero esas citas.");
                
                _repositorioDoctores.Eliminar(idDoctor);


            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
