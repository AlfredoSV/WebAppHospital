using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppHospital.Dominio.DTOs;
using WebAppHospital.Dominio.Entidades;

namespace WebAppHospital.Infraestructura.Repositorios
{
    public class RepositorioDoctores
    {
        private readonly string _connectionString;

        public RepositorioDoctores(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Doctor ConsultarDoctor(Guid idDoctor)
        {

            Doctor doctor;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select * from Doctor where Id = @Id ", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = idDoctor;
                var dr = cmd.ExecuteReader();

                dr.Read();

                doctor = Doctor.Crear(dr.GetGuid(0), dr.GetString(1), dr.GetString(2),
                                            dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6));


                return doctor;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public IEnumerable<Doctor> ConsultarDoctores(DtoConsultaPag dtoConsultaPag)
        {

            List<Doctor> doctores = new List<Doctor>();
            var con = new SqlConnection(_connectionString);
            var cmd = con.CreateCommand();
            cmd.CommandText = "ConsultaDoctores";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NUM_PAGINA", dtoConsultaPag.Pagina);
            cmd.Parameters.AddWithValue("@TAM_PAGINA", dtoConsultaPag.Tamanio_Pagina);
         
            try
            {
                con.Open();

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        doctores.Add(Doctor.Crear(dr.GetGuid(0), dr.GetString(1), dr.GetString(2),
                                            dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6)));
                    }
                }

                return doctores;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                
                con.Close();
            }
        }


        public IEnumerable<Doctor> ConsultarTodosLosDoctores()
        {

            List<Doctor> doctores = new List<Doctor>();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Select * from Doctor",con);

            try
            {
                con.Open();

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        doctores.Add(Doctor.Crear(dr.GetGuid(0), dr.GetString(1), dr.GetString(2),
                                            dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6)));
                    }
                }

                return doctores;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {

                con.Close();
            }
        }


        public int ConsultarTotalDoctores()
        {

            int totalDdoctores;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select count(*) from Doctor", con);
            try
            {
                con.Open();



                var dr = cmd.ExecuteReader();
                dr.Read();
                totalDdoctores = dr.GetInt32(0);

                return totalDdoctores;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void InsertarDoctor(Doctor doctor)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"INSERT INTO [dbo].[Doctor]
           ([Id]
           ,[Nombre]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[Especialidad]
           ,[Departamento]
           ,[NumCedula])
     VALUES  (@Id
           ,@Nombre
           ,@ApellidoPaterno
           ,@ApellidoMaterno
           ,@Especialidad
           ,@Departamento
           ,@NumCedula)", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = doctor.Id;
                cmd.Parameters.Add("Nombre", SqlDbType.VarChar).Value = doctor.Nombre;
                cmd.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = doctor.ApellidoPaterno;
                cmd.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = doctor.ApellidoMaterno;
                cmd.Parameters.Add("Especialidad", SqlDbType.VarChar).Value = doctor.Especialidad;
                cmd.Parameters.Add("Departamento", SqlDbType.VarChar).Value = doctor.Departamento;
                cmd.Parameters.Add("NumCedula", SqlDbType.VarChar).Value = doctor.NumCedula;
                cmd.ExecuteNonQuery();

            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void ActualizarDoctor(Doctor doctor)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"UPDATE [dbo].[Doctor]
   SET 
      [Nombre] = @Nombre
      ,[ApellidoPaterno] = @ApellidoPaterno
      ,[ApellidoMaterno] = @ApellidoMaterno
      ,[Especialidad] = @Especialidad
      ,[Departamento] = @Departamento
      ,[NumCedula] = @NumCedula
 WHERE [Id] = @Id", con);
            try
            {
                con.Open();

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = doctor.Id;
                cmd.Parameters.Add("Nombre", SqlDbType.VarChar).Value = doctor.Nombre;
                cmd.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = doctor.ApellidoPaterno;
                cmd.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = doctor.ApellidoMaterno;
                cmd.Parameters.Add("Especialidad", SqlDbType.VarChar).Value = doctor.Especialidad;
                cmd.Parameters.Add("Departamento", SqlDbType.VarChar).Value = doctor.Departamento;
                cmd.Parameters.Add("NumCedula", SqlDbType.VarChar).Value = doctor.NumCedula;
                cmd.ExecuteNonQuery();

            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Eliminar(Guid IdDoctor)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Delete from doctor where id = @Id", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = IdDoctor;

                cmd.ExecuteNonQuery();

            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
