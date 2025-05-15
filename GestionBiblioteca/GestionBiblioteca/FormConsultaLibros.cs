using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBiblioteca
{
    public partial class FormConsultaLibros : Form
    {
        public FormConsultaLibros()
        {
            this.BackColor = Color.WhiteSmoke; // Gris claro moderno
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string campo = cmbCampo.SelectedItem.ToString();
            string texto = txtBuscar.Text.ToLower();

            var resultados = DatosSistema.Libros
                .Where(l =>
                    (campo == "Título" && l.Titulo.ToLower().Contains(texto)) ||
                    (campo == "Autor" && l.Autor.ToLower().Contains(texto)))
                .ToList();

            dgvLibros.DataSource = null;
            dgvLibros.DataSource = resultados;
        }
    }
}
