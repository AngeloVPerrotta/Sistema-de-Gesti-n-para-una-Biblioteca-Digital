using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblioteca
{
    public class ClsLibro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int ID { get; set; }
        public int Anio { get; set; }

        public ClsLibro(int id, string titulo, string autor, int anio)
        {
            Titulo = titulo;
            Autor = autor;
            ID = id;
            Anio = anio;
        }
    }
}
