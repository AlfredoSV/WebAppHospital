using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppHospital.Dominio.DTOs;
using WebAppHospital.Dominio.Entidades;

namespace WebAppHospital.Infraestructura.Repositorios
{
    public class RepositorioCitas
    {
        private readonly string _connectionString;

        public RepositorioCitas(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Cita ConsultarCita(Guid idCita)
        {

            Cita cita;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"SELECT c.Id,p.Id,p.Nombre,d.Id,d.Nombre,c.Comentarios,c.Horario,c.FechaCita FROM Doctor d  inner join Cita c on c.IdDoctor = d.Id
inner join Paciente p on p.Id = c.IdPaciente where c.Id = @Id ", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = idCita;
                var dr = cmd.ExecuteReader();

                dr.Read();
                cita = Cita.Crear(dr.GetGuid(0), dr.GetGuid(1), dr.GetString(2),
                                           dr.GetGuid(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetDateTime(7));
                return cita;
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

        public int ConsultarCitaPorIdDoctor(Guid idCita)
        {

            int citas;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select count(*) from Cita where idDoctor = @IdDoctor ", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("IdDoctor", SqlDbType.UniqueIdentifier).Value = idCita;
                var dr = cmd.ExecuteReader();

                dr.Read();
                citas = dr.GetInt32(0);

                return citas;
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

        public int ConsultarCitaPorIdPaciente(Guid idPaciente)
        {

            int citas;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select count(*) from Cita where idPaciente = @IdPaciente ", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("IdPaciente", SqlDbType.UniqueIdentifier).Value = idPaciente;
                var dr = cmd.ExecuteReader();

                dr.Read();
                citas = dr.GetInt32(0);

                return citas;
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

        public int ConsultarTotalCitas()
        {

            int totalDdoctores;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select count(*) from Cita", con);
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

        public IEnumerable<Cita> ConsultarCitas(DtoConsultaPag dtoConsultaPag)
        {

            List<Cita> citas = new List<Cita>();
            var con = new SqlConnection(_connectionString);
            var cmd = con.CreateCommand();
            cmd.CommandText = "ConsultaCitas";
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
                        
                        citas.Add(Cita.Crear(dr.GetGuid(0), dr.GetGuid(1), dr.GetString(2),
                                            dr.GetGuid(3), dr.GetString(4),dr.GetString(5), dr.GetDateTime(6)));
                    }
                }

                return citas;
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

        public void InsertarCita(Cita cita)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"INSERT INTO [dbo].[Cita]
           ([Id]
           ,[IdPaciente]
           ,[IdDoctor]
           
           ,[FechaCita]
           ,[Comentarios],[Horario])
     VALUES
           (@Id 
           ,@IdPaciente 
           ,@IdDoctor 
            
           ,@FechaCita 
           ,@Comentarios,@Horario )", con);
            try
            {
                con.Open();

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = cita.Id;
                cmd.Parameters.Add("IdPaciente", SqlDbType.UniqueIdentifier).Value = cita.IdPaciente;
                cmd.Parameters.Add("IdDoctor", SqlDbType.UniqueIdentifier).Value = cita.IdDoctor;
                cmd.Parameters.Add("Horario", SqlDbType.VarChar).Value = cita.HorarioCita;
                cmd.Parameters.Add("FechaCita", SqlDbType.VarChar).Value = cita.FechaCita;
                cmd.Parameters.Add("Comentarios", SqlDbType.VarChar).Value = cita.Comentarios;
                
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

        public void ActualizarCita(Cita cita)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"UPDATE [dbo].[Cita]
   SET 
      [IdPaciente] = @IdPaciente 
      ,[IdDoctor] = @IdDoctor 
      ,[HorariosCitas] = @HorariosCitas 
      ,[FechaCita] = @FechaCita 
      ,[Comentarios] = @Comentarios 
 WHERE @[Id] = @Id ", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = cita.Id;
                cmd.Parameters.Add("IdPaciente", SqlDbType.UniqueIdentifier).Value = cita.IdPaciente;
                cmd.Parameters.Add("IdDoctor", SqlDbType.UniqueIdentifier).Value = cita.IdDoctor;
                cmd.Parameters.Add("HorariosCitas", SqlDbType.UniqueIdentifier).Value = cita.HorarioCita;
                cmd.Parameters.Add("FechaCita", SqlDbType.VarChar).Value = cita.FechaCita;
                cmd.Parameters.Add("Comentarios", SqlDbType.Bit).Value = cita.Comentarios;



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

        public void EliminarCita(Guid idCita)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Delete from Cita where Id = @Id", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = idCita;

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
