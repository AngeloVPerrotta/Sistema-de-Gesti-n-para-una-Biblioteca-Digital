using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblioteca
{
    public class ClsUsuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int FamiliaID { get; set; }

        public ClsUsuario(int id, string nombre, string clave, int familiaID)
        {
            ID = id;
            Nombre = nombre;
            Clave = clave;
            FamiliaID = familiaID;
        }
    }
}
