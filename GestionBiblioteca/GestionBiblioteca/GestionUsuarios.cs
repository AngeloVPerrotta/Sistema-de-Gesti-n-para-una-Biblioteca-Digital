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
    public partial class GestionUsuarios : Form
    {
        public GestionUsuarios()
        {
            InitializeComponent();
        }
        private void GestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarFamilias();
            CargarGrilla();
            LimpiarCampos();
            this.BackColor = Color.WhiteSmoke; // Gris claro moderno
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);


        }
        private void CargarGrilla()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = DatosSistema.Usuarios;
        }

        private void CargarFamilias()
        {
            cmbFamilia.DataSource = null;
            cmbFamilia.DataSource = DatosSistema.Familias;
            cmbFamilia.DisplayMember = "Nombre";
            cmbFamilia.ValueMember = "ID";
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtClave.Clear();
            cmbFamilia.SelectedIndex = 0;
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dgvUsuarios.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                txtNombre.Text = dgvUsuarios.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtClave.Text = dgvUsuarios.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                cmbFamilia.SelectedValue = dgvUsuarios.Rows[e.RowIndex].Cells["FamiliaID"].Value;
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ClsFamilia familiaSeleccionada = cmbFamilia.SelectedItem as ClsFamilia;

            if (familiaSeleccionada == null)
            {
                MessageBox.Show("Debe seleccionar una familia.");
                return;
            }

            var usuarioNuevo = new ClsUsuario(
                int.Parse(txtID.Text),
                txtNombre.Text,
                txtClave.Text,
                familiaSeleccionada.ID
            );

            DatosSistema.Usuarios.Add(usuarioNuevo);
            CargarGrilla();
            LimpiarCampos();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            var usuario = DatosSistema.Usuarios.FirstOrDefault(u => u.ID == id);
            if (usuario != null)
            {
                usuario.Nombre = txtNombre.Text;
                usuario.Clave = txtClave.Text;
                usuario.FamiliaID = int.Parse(cmbFamilia.SelectedValue.ToString());
                CargarGrilla();

            }
            int familiaID = int.Parse(cmbFamilia.SelectedValue.ToString());

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            var usuario = DatosSistema.Usuarios.FirstOrDefault(u => u.ID == id);
            if (usuario != null)
            {
                DatosSistema.Usuarios.Remove(usuario);
                CargarGrilla();
                LimpiarCampos();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var lineas = new List<string> { "ID,Nombre,Clave,Familia" };
            foreach (var u in DatosSistema.Usuarios)
            {
                lineas.Add($"{u.ID},{u.Nombre},{u.Clave},{u.FamiliaID}");
            }

            File.WriteAllLines("usuarios.csv", lineas);
            MessageBox.Show("Cambios guardados correctamente.");
        }

        private void cmbFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFamilia.SelectedItem is ClsFamilia familia)
            {
                // Mostrás el ID seleccionado por consola (o lo que quieras)
                Console.WriteLine($"ID seleccionado: {familia.ID}, Nombre: {familia.Nombre}");
            }
        }
    }
}

