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
    public partial class FormMDI : Form
    {
        public FormMDI()
        {
            InitializeComponent();
            this.BackColor = Color.Gainsboro;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            this.Opacity = 0;
            Timer fadeIn = new Timer();
            fadeIn.Interval = 10;
            fadeIn.Tick += (s, e) =>
            {
                if (this.Opacity < 1) this.Opacity += 0.05;
                else fadeIn.Stop();
            };
            fadeIn.Start();

        }
        private void FormMDI_Load(object sender, EventArgs e)
        {
            menuPrincipalToolStripMenuItem.Enabled = false;
            logoutToolStripMenuItem.Enabled = false;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            loginToolStripMenuItem.BackColor = Color.DodgerBlue;
            loginToolStripMenuItem.ForeColor = Color.White;
            loginToolStripMenuItem.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private ClsUsuario usuarioLogueado;
        private ClsFamilia familiaDelUsuario;

        public void SetUsuario(ClsUsuario usuario)
        {
            usuarioLogueado = usuario;

            // Cargar familia desde familias.csv
            var lineasFamilia = File.ReadAllLines("familias.csv");
            var familiaNombre = lineasFamilia
                .FirstOrDefault(l => l.StartsWith(usuario.FamiliaID.ToString() + ","))
                ?.Split(',')[1];

            familiaDelUsuario = new ClsFamilia(usuario.FamiliaID, familiaNombre);

            // Cargar permisos desde permisos.csv
            var permisos = File.ReadAllLines("permisos.csv");

            foreach (var linea in permisos.Skip(1))
            {
                var partes = linea.Split(',');
                if (int.Parse(partes[0]) == usuario.FamiliaID)
                {
                    familiaDelUsuario.AgregarPermiso(partes[1]);
                }
            }

            // Habilitar menús según permisos
            menuPrincipalToolStripMenuItem.Enabled = true;
            logoutToolStripMenuItem.Enabled = true;

            gestionLibrosToolStripMenuItem.Enabled = familiaDelUsuario.TienePermiso("GestionLibros");
            consultaLibrosToolStripMenuItem.Enabled = familiaDelUsuario.TienePermiso("GestionUsuarios");
            consultaLibrosToolStripMenuItem.Enabled = familiaDelUsuario.TienePermiso("ConsultaLibros");
        }

        private void gestionLibrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionLibros form = new FormGestionLibros();
            form.MdiParent = this;
            form.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Close();
        }

        private void consultaLibrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConsultaLibros form = new FormConsultaLibros();
            form.MdiParent = this;
            form.Show();
        }

        private void gestionUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionUsuarios form = new GestionUsuarios();
            form.MdiParent = this;
            form.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMDI_Load_1(object sender, EventArgs e)
        {

        }
    }
}
