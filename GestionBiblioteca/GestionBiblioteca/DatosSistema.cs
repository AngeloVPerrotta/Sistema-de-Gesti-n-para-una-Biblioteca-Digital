using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GestionBiblioteca
{
    public static class DatosSistema
    {
        public static List<ClsUsuario> Usuarios = new List<ClsUsuario>();
        public static List<ClsFamilia> Familias = new List<ClsFamilia>();
        public static List<ClsLibro> Libros = new List<ClsLibro>();


        public static void CargarUsuarios(string ruta)
        {
            var lineas = File.ReadAllLines(ruta);

            foreach (var linea in lineas.Skip(1)) // salta el encabezado
            {
                var partes = linea.Split(',');
                int id = int.Parse(partes[0]);
                string nombre = partes[1];
                string clave = partes[2];
                int familiaID = int.Parse(partes[3]);

                DatosSistema.Usuarios.Add(new ClsUsuario(id, nombre, clave, familiaID));
            }
        }

        public static void CargarLibros(string ruta)
        {
            var lineas = File.ReadAllLines(ruta);

            foreach (var linea in lineas.Skip(1))
            {
                var partes = linea.Split(',');
                int id = int.Parse(partes[0]);
                string titulo = partes[1];
                string autor = partes[2];
                int anio = int.Parse(partes[3]);

                Libros.Add(new ClsLibro(id, titulo, autor, anio));
            }
        }


        public static void CargarFamiliasYPermisos(string rutaFamilias, string rutaPermisos)
        {
            var lineasFamilias = File.ReadAllLines(rutaFamilias);
            var lineasPermisos = File.ReadAllLines(rutaPermisos);

            // Crear las familias primero
            foreach (var linea in lineasFamilias.Skip(1))
            {
                var partes = linea.Split(',');
                int id = int.Parse(partes[0]);
                string nombre = partes[1];
                DatosSistema.Familias.Add(new ClsFamilia(id, nombre));
            }

            // Asignar los permisos
            foreach (var linea in lineasPermisos.Skip(1))
            {
                var partes = linea.Split(',');
                int familiaID = int.Parse(partes[0]);
                string patente = partes[1];

                var familia = DatosSistema.Familias.FirstOrDefault(f => f.ID == familiaID);
                if (familia != null)
                {
                    familia.AgregarPermiso(patente);
                }
            }
        }

    }
}
