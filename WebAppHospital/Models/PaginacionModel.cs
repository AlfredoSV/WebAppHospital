using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppHospital.Models
{
    public class PaginacionModel
    {
        public  int TAMANIO_PAGINA = 5;
        public  int TAMANIO_MAXIMO_RANGO = 3;
        public  int TAMANIO_BLOQUES = 3;
        public int Pagina_Actual = 1;
        public int Total_Paginas;
        public bool Siguiente = false;
        public bool Anterior = false;
        public bool HayRegistros = false;
        public int Min_Bloque = 1;
        public int Max_Bloque = 3;
        public int Bloques_De_Paginas;
        public int Bloque_Actual = 1;

  
    }
}
