using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GestionBiblioteca
{
    public partial class FormGestionLibros : Form
    {
        public FormGestionLibros()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormGestionLibros_Load(object sender, EventArgs e)
        {
            CargarGrilla();       // Cargar los libros al abrir
            LimpiarCampos();      // Limpiar los campos por si quedó algo
            dgvLibros.AutoGenerateColumns = true;
            this.BackColor = Color.WhiteSmoke; // Gris claro moderno
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            if (DatosSistema.Libros.Any(l => l.ID == id))
            {
                MessageBox.Show("Ya existe un libro con ese ID.");
                return;
            }

            var nuevo = new ClsLibro(id, txtTitulo.Text, txtAutor.Text, int.Parse(txtAnio.Text));
            DatosSistema.Libros.Add(nuevo);
            GuardarLibrosCSV();
            CargarGrilla();
        }

        private void GuardarLibrosCSV()
        {
            var lineas = new List<string> { "ID_Libro,Titulo,Autor,Año" };
            foreach (var l in DatosSistema.Libros)
                lineas.Add($"{l.ID},{l.Titulo},{l.Autor},{l.Anio}");
            File.WriteAllLines("libros.csv", lineas);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);

            var libro = DatosSistema.Libros.FirstOrDefault(l => l.ID == id);
            if (libro != null)
            {
                libro.Titulo = txtTitulo.Text;
                libro.Autor = txtAutor.Text;
                libro.Anio = int.Parse(txtAnio.Text);
                CargarGrilla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);

            var libro = DatosSistema.Libros.FirstOrDefault(l => l.ID == id);
            if (libro != null)
            {
                DatosSistema.Libros.Remove(libro);
                CargarGrilla();
                LimpiarCampos();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvLibros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dgvLibros.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                txtTitulo.Text = dgvLibros.Rows[e.RowIndex].Cells["Titulo"].Value.ToString();
                txtAutor.Text = dgvLibros.Rows[e.RowIndex].Cells["Autor"].Value.ToString();
                txtAnio.Text = dgvLibros.Rows[e.RowIndex].Cells["Anio"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lineas = new List<string> { "ID_Libro,Titulo,Autor,Año" };

            foreach (var l in DatosSistema.Libros)
            {
                lineas.Add($"{l.ID},{l.Titulo},{l.Autor},{l.Anio}");
            }

            File.WriteAllLines("libros.csv", lineas);
            MessageBox.Show("Cambios guardados correctamente.");
        }
        private void CargarGrilla()
        {
            dgvLibros.DataSource = null;
            dgvLibros.DataSource = DatosSistema.Libros;
        }
        private void LimpiarCampos()
        {
            txtID.Clear();
            txtTitulo.Clear();
            txtAutor.Clear();
            txtAnio.Clear();
        }

    }
}
