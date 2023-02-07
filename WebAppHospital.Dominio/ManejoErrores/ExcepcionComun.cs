using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.ManejoErrores
{
    public class ExcepcionComun : Exception
    {
        public string Titulo { get; set; }
        public string Detalle { get; set; }
        public string StackTracer { get; set; }
        public string Fuente { get; set; }
        public string Mensaje { get; set; }
        public string MensajeExcepcionInterna { get; set; }
        public int ResultadoH { get; set; }
        public ExcepcionComun() { }
        public ExcepcionComun(string titulo, string detalle, Exception innerE) : base(detalle, innerE)
        {
            this.Titulo = titulo;
            this.Detalle = detalle;
        }
        public ExcepcionComun(string titulo, string detalle, string mensaje = "")
        {
            this.Titulo = titulo;
            this.Detalle = detalle;
            this.Mensaje = mensaje;
        }
        public ExcepcionComun(Exception ex, string titulo, string detalle)
        {
            this.Titulo = titulo;
            this.Detalle = detalle;
            this.StackTracer = ex.StackTrace;
            this.Fuente = ex.Source;
            this.Mensaje = ex.Message;
            this.MensajeExcepcionInterna = ex.Message;
            this.ResultadoH = ex.HResult;
        }
    }
}
