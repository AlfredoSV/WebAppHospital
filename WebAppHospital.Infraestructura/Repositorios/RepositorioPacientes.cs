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
    public class RepositorioPacientes
    {
        private readonly string _connectionString;

        public RepositorioPacientes(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Paciente ConsultarPaciente(Guid idPaciente)
        {

            Paciente paciente;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select * from Paciente where Id = @IdPaciente ", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("IdPaciente ", SqlDbType.UniqueIdentifier).Value = idPaciente;
                var dr = cmd.ExecuteReader();
                paciente = null;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        paciente = Paciente.Crear(dr.GetGuid(0), dr.GetString(1), dr.GetString(2),
                                            dr.GetString(3), dr.GetInt32(4), Convert.ToDecimal(dr["peso"] == DBNull.Value ? 0m : dr["peso"]),  dr.GetString(6));
                    }
                }
                return paciente;
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

        public IEnumerable<Paciente> ConsultarPacientes(DtoConsultaPag dtoConsultaPag)
        {

            List<Paciente> pacientes = new List<Paciente>();
            var con = new SqlConnection(_connectionString);
            var cmd = con.CreateCommand();
            cmd.CommandText = "ConsultaPacientes";
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
                        pacientes.Add(Paciente.Crear(dr.GetGuid(0), dr.GetString(1), dr.GetString(2),
                                            dr.GetString(3), dr.GetInt32(4), Convert.ToDecimal(dr["peso"] == DBNull.Value ? 0m : dr["peso"]), dr.GetString(6)));
                    }
                }

                return pacientes;
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

        public IEnumerable<Paciente> ConsultarTodosLosPacientes()
        {

            List<Paciente> pacientes = new List<Paciente>();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select * from Paciente", con);

            try
            {
                con.Open();

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pacientes.Add(Paciente.Crear(dr.GetGuid(0), dr.GetString(1), dr.GetString(2),
                                            dr.GetString(3), dr.GetInt32(4), Convert.ToDecimal(dr["peso"] == DBNull.Value ? 0m : dr["peso"]), dr.GetString(6)));
                    }
                }

                return pacientes;
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


        public int ConsultarTotalPacientes()
        {

            int totalPacientes;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select count(*) from Paciente", con);
            try
            {
                con.Open();



                var dr = cmd.ExecuteReader();
                dr.Read();
                totalPacientes = dr.GetInt32(0);

                return totalPacientes;
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


        public void InsertarPaciente(Paciente paciente)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"INSERT INTO [dbo].[Paciente]
           ([Id]
           ,[Nombre]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[Edad]
           ,[Peso]
           ,[Sexo])
     VALUES
           (@Id
           ,@Nombre
           ,@ApellidoPaterno
           ,@ApellidoMaterno
           ,@Edad
           ,@Peso
           ,@Sexo)", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = paciente.Id;
                cmd.Parameters.Add("Nombre", SqlDbType.VarChar).Value = paciente.Nombre;
                cmd.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = paciente.ApellidoPaterno;
                cmd.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = paciente.ApellidoMaterno;
                cmd.Parameters.Add("Edad", SqlDbType.VarChar).Value = paciente.Edad;
                cmd.Parameters.Add("Peso", SqlDbType.Bit).Value = paciente.Peso;
                cmd.Parameters.Add("Sexo", SqlDbType.VarChar).Value = paciente.Sexo;

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

        public void ActualizarPaciente(Paciente paciente)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"UPDATE [dbo].[Paciente]
   SET 
      [Nombre] = @Nombre
      ,[ApellidoPaterno] = @ApellidoPaterno
      ,[ApellidoMaterno] = @ApellidoMaterno
      ,[Edad] = @Edad
      ,[Peso] = @Peso
      ,[Sexo] = @Sexo
 WHERE [Id] = @Id", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = paciente.Id;
                cmd.Parameters.Add("Nombre", SqlDbType.VarChar).Value = paciente.Nombre;
                cmd.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = paciente.ApellidoPaterno;
                cmd.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = paciente.ApellidoMaterno;
                cmd.Parameters.Add("Edad", SqlDbType.Int).Value = paciente.Edad;
                cmd.Parameters.Add("Peso", SqlDbType.Decimal).Value = paciente.Peso;
                cmd.Parameters.Add("Sexo", SqlDbType.VarChar).Value = paciente.Sexo;
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

        public void EliminarPaciente(Guid idPaciente)
        {


            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Delete from Paciente where Id=@Id", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = idPaciente;

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
