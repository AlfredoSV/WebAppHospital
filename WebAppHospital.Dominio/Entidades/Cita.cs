using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.Entidades
{
    public class Cita
    {
        public Guid Id { get; set; }
        public Guid IdPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public Guid IdDoctor { get; set; }
        public string NombreDoctor { get; set; }
        public string Comentarios { get; set; }
        public string HorarioCita { get; set; }
        public DateTime FechaCita { get; set; }

        public Cita(Guid idPaciente, Guid idDoctor, DateTime fechaCita, string comentarios, string horarioCita)
        {
            Id = Guid.NewGuid();
            IdPaciente = idPaciente;
            IdDoctor = idDoctor;
            Comentarios = comentarios;
            HorarioCita = horarioCita;
            FechaCita = fechaCita;
        }

        public Cita(Guid id, Guid idPaciente, Guid idDoctor, DateTime fechaCita, string comentarios, string horarioCita)
        {
            Id = id;
            IdPaciente = idPaciente;
            IdDoctor = idDoctor;
            Comentarios = comentarios;
            HorarioCita = horarioCita;
            FechaCita = fechaCita;
        }

        public static Cita Crear( Guid idPaciente, Guid idDoctor, DateTime fechaCita, string comentarios, string horarioCita)
        {
            return new Cita(  idPaciente,   idDoctor, fechaCita,   comentarios,   horarioCita  );
        }

        public static Cita Crear(Guid id, Guid idPaciente, Guid idDoctor, DateTime fechaCita, string comentarios, string horarioCita)
        {
            return new Cita(id, idPaciente, idDoctor, fechaCita, comentarios, horarioCita);
        }

        public Cita(Guid id, Guid idPaciente, string nombrePaciente, Guid idDoctor, string nombreDoctor, string comentarios, string horarioCita, DateTime fechaCita)
        {
            Id = id;
            IdPaciente = idPaciente;
            NombrePaciente = nombrePaciente;
            IdDoctor = idDoctor;
            NombreDoctor = nombreDoctor;
            Comentarios = comentarios;
            HorarioCita = horarioCita;
            FechaCita = fechaCita;
        }

        public Cita(Guid id, Guid idPaciente, string nombrePaciente, Guid idDoctor, string nombreDoctor, string comentarios, DateTime fechaCita)
        {
            Id = id;
            IdPaciente = idPaciente;
            NombrePaciente = nombrePaciente;
            IdDoctor = idDoctor;
            NombreDoctor = nombreDoctor;
            Comentarios = comentarios;

            FechaCita = fechaCita;
        }

        public static Cita Crear(Guid id, Guid idPaciente, string nombrePaciente, Guid idDoctor, string nombreDoctor, string comentarios, string horarioCita, DateTime fechaCita)
        {
            return new Cita(  id,   idPaciente,   nombrePaciente,   idDoctor,   nombreDoctor,   comentarios,   horarioCita,   fechaCita);
        }

        public static Cita Crear(Guid id, Guid idPaciente, string nombrePaciente, Guid idDoctor, string nombreDoctor, string comentarios, DateTime fechaCita)
        {
            return new Cita( id,  idPaciente,  nombrePaciente,  idDoctor,  nombreDoctor,  comentarios,  fechaCita);
        }
    }
}
