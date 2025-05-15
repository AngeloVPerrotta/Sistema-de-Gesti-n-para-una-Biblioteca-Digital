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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

        }

        private void Ingresar_Click(object sender, EventArgs e)
        {
            string nombre = txtUsuario.Text;
            string clave = txtContraseña.Text;

            // Leer usuarios.csv
            var lineas = File.ReadAllLines("usuarios.csv");

            foreach (string linea in lineas.Skip(1)) // saltea el encabezado
            {
                var datos = linea.Split(',');
                if (datos[1] == nombre && datos[2] == clave)
                {
                    int familiaID = int.Parse(datos[3]);
                    var usuario = new ClsUsuario(int.Parse(datos[0]), nombre, clave, familiaID);

                    // Pasar el usuario al MDI
                    FormMDI mdi = new FormMDI();
                    mdi.SetUsuario(usuario); // método que implementamos abajo
                    mdi.Show();
                    this.Hide();
                    return;
                }
            }

            MessageBox.Show("Usuario o clave incorrectos.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            Ingresar.FlatStyle = FlatStyle.Flat;
            Ingresar.BackColor = Color.DodgerBlue;
            Ingresar.ForeColor = Color.White;
            Ingresar.FlatAppearance.BorderSize = 0;
            Ingresar.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            PictureBox logo = new PictureBox();
            logo.Image = Image.FromFile("logo.png"); // no hace falta ruta completa
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.Size = new Size(100, 100);
            logo.Location = new Point(20, 20);
            logo.BorderStyle = BorderStyle.None;
            pictureBox.Visible = true;
            pictureBox.Size = new Size(100, 100);
            pictureBox.Location = new Point(20, 20);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = Image.FromFile("logo.png");
            this.Controls.Add(logo);

            if (File.Exists("logo.png"));


            }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRegistro registro = new FormRegistro(); // asegurate de crear este formulario
            registro.ShowDialog(); // Bloquea hasta que se cierre
        }
    }
}
