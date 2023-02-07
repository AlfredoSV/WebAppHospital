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
    public class ServicioPaciente
    {
        private readonly string _connectionString;
        private readonly RepositorioPacientes _repositorioPacientes;
        private readonly RepositorioCitas _repositorioCitas;


        public ServicioPaciente(string connectionString)
        {
            this._connectionString = connectionString;
            _repositorioPacientes = new RepositorioPacientes(this._connectionString);
            _repositorioCitas = new RepositorioCitas(this._connectionString);

        }

        public IEnumerable<DtoPaciente> ConsultarTodosLosPacientes()
        {

         
            var pacientes = new List<DtoPaciente>();

            try
            {
                
                foreach (var paciente in _repositorioPacientes.ConsultarTodosLosPacientes())
                {
                    pacientes.Add(new DtoPaciente()
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


                return pacientes;
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public IEnumerable<DtoPaciente> ConsultarPacientes(DtoConsultaPag dtoConsultaPag)
        {

            int totalPacientes;
            var pacientes = new List<DtoPaciente>();

            try
            {
                totalPacientes = _repositorioPacientes.ConsultarTotalPacientes();

                foreach (var paciente in _repositorioPacientes.ConsultarPacientes(dtoConsultaPag))
                {
                    pacientes.Add(new DtoPaciente()
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


                return pacientes;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int ConsultarTotalPacientes()
        {


            try
            {
                return _repositorioPacientes.ConsultarTotalPacientes();


            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void RegistrarPaciente(DtoPaciente dtoPaciente)
        {
            Paciente pacienteReg;
            try
            {


                pacienteReg = Paciente.Crear(dtoPaciente.Nombre, dtoPaciente.ApellidoPaterno, dtoPaciente.ApellidoMaterno,
                   dtoPaciente.Edad, dtoPaciente.Peso, dtoPaciente.Sexo);

                _repositorioPacientes.InsertarPaciente(pacienteReg);


            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void EditarPaciente(DtoPaciente dtoPaciente)
        {
            Paciente pacienteReg;
            try
            {

                pacienteReg = Paciente.Crear(dtoPaciente.Id,dtoPaciente.Nombre, dtoPaciente.ApellidoPaterno, dtoPaciente.ApellidoMaterno,
                  dtoPaciente.Edad, dtoPaciente.Peso, dtoPaciente.Sexo);

                _repositorioPacientes.ActualizarPaciente(pacienteReg);


            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public DtoPaciente ConsultarPaciente(Guid idPaciente)
        {

            Paciente paciente;

            try
            {

                paciente = _repositorioPacientes.ConsultarPaciente(idPaciente);

                if (paciente == null)
                    throw new ExcepcionComun("No existe este paciente", "Id no disponible", "Favor de validar si el paciente existe.");


                return new DtoPaciente()
                {


                    Id = paciente.Id,
                    Nombre = paciente.Nombre,
                    ApellidoPaterno = paciente.ApellidoPaterno,
                    ApellidoMaterno = paciente.ApellidoMaterno,
                    Edad = paciente.Edad,
                    Peso = paciente.Peso,
                    Sexo = paciente.Sexo

                };
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void BorrarPaciente(Guid idPaciente)
        {

            int citasPaciente;

            try
            {

                citasPaciente = _repositorioCitas.ConsultarCitaPorIdPaciente(idPaciente);

                if (citasPaciente > 0)
                    throw new ExcepcionComun("No es posible eliminar este paciente.", "Eliminación no posible.", "Este paciente tiene citas relacionadas, favor de eliminar primero esas citas.");

                _repositorioPacientes.EliminarPaciente(idPaciente);


            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
