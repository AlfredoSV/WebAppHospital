using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppHospital.Dominio.Entidades;

namespace WebAppHospital.Models
{
    public class IndexModelCita
    {
        public IndexModelCita()
        {
            if (Pg == null)
                Pg = new PaginacionModel();
        }
        public IEnumerable<CitaModel> Citas { get; set; }

        
        public PaginacionModel Pg;

    }
}
