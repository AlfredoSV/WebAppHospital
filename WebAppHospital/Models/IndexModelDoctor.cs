using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppHospital.Dominio.Entidades;

namespace WebAppHospital.Models
{
    public class IndexModelDoctor
    {
        public IndexModelDoctor()
        {
            if (Pg == null)
                Pg = new PaginacionModel();
        }
        public IEnumerable<DoctorModel> Doctores { get; set; }

        
        public PaginacionModel Pg;

    }
}
