using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppHospital.Dominio.Entidades;

namespace WebAppHospital.Models
{
    public class IndexModelPaciente
    {
        public IndexModelPaciente()
        {
            if (Pg == null)
                Pg = new PaginacionModel();
        }
        public IEnumerable<PacienteModel> Pacientes { get; set; }

        
        public PaginacionModel Pg;

    }
}
