using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.Entidades
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Contrasenia { get; set; }
        public Boolean Activo { get; set; }
        public string UsuarioNom { get; set; }

        public Usuario(string contrasenia, string usuarioNom)
        {
            Contrasenia = contrasenia;
            UsuarioNom = usuarioNom;
        }

        public Usuario(Guid id, string nombre, string apellidoPaterno, string apellidoMaterno, string contrasenia, bool activo,string usuarioNom)
        {
            Id = id;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Contrasenia = contrasenia;
            Activo = activo;
            UsuarioNom = usuarioNom;
        }

        public Usuario(string nombre, string apellidoPaterno, string apellidoMaterno, string contrasenia, string usuarioNom)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Contrasenia = contrasenia;
            Activo = true;
            UsuarioNom = usuarioNom;
        }

        public static Usuario Crear(Guid id, string nombre, string apellidoPaterno, string apellidoMaterno, string contrasenia, bool activo, string usuarioNom)
        {
            return new Usuario(id, nombre, apellidoPaterno, apellidoMaterno, contrasenia, activo,usuarioNom);
        }

        public static Usuario Crear(string nombre, string apellidoPaterno, string apellidoMaterno, string contrasenia, string usuarioNom)
        {
            return new Usuario( nombre, apellidoPaterno, apellidoMaterno, contrasenia, usuarioNom);
        }



    }
}
