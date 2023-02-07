using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppHospital.Dominio.Entidades;
using WebAppHospital.Dominio.DTOs;
using WebAppHospital.Dominio.ManejoErrores;
using WebAppHospital.Infraestructura.Repositorios;

namespace WebAppHospital.Aplicacion.Servicios
{
    public class ServicioUsuario
    {
        private readonly string _connectionString;
        private readonly RepositorioUsuarios _repositorioUsuarios;
        private readonly ServicioEncriptacion _servicioEncriptacion;


        public ServicioUsuario(string connectionString)
        {
            this._connectionString = connectionString;
            this._repositorioUsuarios = new RepositorioUsuarios(connectionString);
            this._servicioEncriptacion = new ServicioEncriptacion();
        }

        public DtoUsuario ValidarUsuario(DtoUsuario dtoUsuario)
        {
            Usuario usuarioCon;
            DtoUsuario usuarioValidacion = null ;


            try
            {
                usuarioCon = this._repositorioUsuarios.ConsultarUsuario(dtoUsuario.UsuarioNom);
                
                if(usuarioCon != null)
                {
                    if (!this._servicioEncriptacion.DecryptCipherTextToPlainText(usuarioCon.Contrasenia)
                                        .Equals(dtoUsuario.Contrasenia))
                        throw new ExcepcionComun("Credenciales incorrectas", "Usuario/Contraseñas incorrecta", "Favor de validar las credenciales");


                    if (!usuarioCon.Activo)
                        throw new ExcepcionComun("Usuario inactivo", "Usuario no disponible", "El usuario se encuentra deshabilitado, favor de consultar con sistemas.");

                    usuarioValidacion = new DtoUsuario()
                    {
                        Id = usuarioCon.Id,
                        Activo = usuarioCon.Activo,
                        UsuarioNom = usuarioCon.UsuarioNom
                    };

                }else
                    throw new ExcepcionComun("Credenciales incorrectas", "Usuario/Contraseñas incorrecta", "Favor de validar las credenciales");


                return usuarioValidacion;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void RegistrarNuevoUsuario(DtoUsuario dtoUsuario)
        {
            Usuario usuarioReg;
            try
            {
                
                if(_repositorioUsuarios.ConsultarUsuario(dtoUsuario.UsuarioNom) != null)
                    throw new ExcepcionComun("Nombre usuario utilizado", "Usuario no disponible", "El nombre de usuario que selecciono ya se encuentra registrado, favor de ingresar otro.");
           

                usuarioReg = Usuario.Crear(dtoUsuario.Nombre, dtoUsuario.ApellidoPaterno, dtoUsuario.ApellidoMaterno, 
                   _servicioEncriptacion.EncryptPlainTextToCipherText(dtoUsuario.Contrasenia), dtoUsuario.UsuarioNom);

                _repositorioUsuarios.InsertarUsuario(usuarioReg);


            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }

}
