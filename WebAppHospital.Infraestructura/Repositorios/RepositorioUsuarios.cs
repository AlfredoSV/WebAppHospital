using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppHospital.Dominio.Entidades;

namespace WebAppHospital.Infraestructura.Repositorios
{
    public class RepositorioUsuarios
    {
        private readonly string _connectionString;

        public RepositorioUsuarios(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Usuario ConsultarUsuario(string usuarioNom)
        {

            Usuario usuario = null;
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"Select * from Usuario where UsuarioNom = @Usuario ", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Usuario", SqlDbType.VarChar).Value = usuarioNom;
                var dr = cmd.ExecuteReader();
               
                if (dr.HasRows)
                {
                    dr.Read();
                    usuario = Usuario.Crear(dr.GetGuid(0), dr.GetString(1), dr.GetString(2),
                    dr.GetString(3), dr.GetString(4), dr.GetBoolean(5), dr.GetString(6));
                }
                    

                return usuario;
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

        public void InsertarUsuario(Usuario usuario)
        {

 
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"INSERT INTO [dbo].[Usuario](
           [Id]
           ,[Nombre]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[Contrase]
           ,[Activo]
           ,[UsuarioNom])
     VALUES(
            @Id 
           ,@Nombre
           ,@ApellidoPaterno 
           ,@ApellidoMaterno
           ,@Contrase
           ,@Activo
           ,@UsuarioNom)", con);
            try
            {
                con.Open();
                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = usuario.Id;
                cmd.Parameters.Add("Nombre", SqlDbType.VarChar).Value = usuario.Nombre;
                cmd.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = usuario.ApellidoPaterno;
                cmd.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = usuario.ApellidoMaterno;
                cmd.Parameters.Add("Contrase", SqlDbType.VarChar).Value = usuario.Contrasenia;
                cmd.Parameters.Add("Activo", SqlDbType.Bit).Value = usuario.Activo;
                cmd.Parameters.Add("UsuarioNom", SqlDbType.VarChar).Value = usuario.UsuarioNom;

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
