using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBiblioteca
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Cargar los datos
                DatosSistema.CargarUsuarios("usuarios.csv");
                DatosSistema.CargarFamiliasYPermisos("familias.csv", "permisos.csv");
                DatosSistema.CargarLibros("libros.csv");

            Application.Run(new FormLogin());
        }
        

    }
}

