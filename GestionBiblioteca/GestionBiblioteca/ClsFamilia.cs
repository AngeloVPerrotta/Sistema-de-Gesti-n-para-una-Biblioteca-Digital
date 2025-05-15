using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblioteca
{
    public class ClsFamilia
    {
        public int ID { get; set; }        // ← clave para el ValueMember
        public string Nombre { get; set; } // ← lo que se muestra

        public List<ClsPermiso> Permisos { get; set; }

        public ClsFamilia(int id, string nombre)
        {
            ID = id;
            Nombre = nombre;
            Permisos = new List<ClsPermiso>();
        }

        public void AgregarPermiso(string patente)
        {
            Permisos.Add(new ClsPermiso(patente));
        }

        public bool TienePermiso(string permisoBuscado)
        {
            foreach (var permiso in Permisos)
            {
                if (permiso.Patente == permisoBuscado)
                    return true;
            }
            return false;
        }
    }
}
