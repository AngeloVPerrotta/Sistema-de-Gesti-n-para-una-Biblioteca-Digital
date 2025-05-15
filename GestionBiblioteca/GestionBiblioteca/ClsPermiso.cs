using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblioteca
{
    public class ClsPermiso
    {
        public string Patente { get; set; }

        public ClsPermiso(string patente)
        {
            Patente = patente;
        }
    }
}
