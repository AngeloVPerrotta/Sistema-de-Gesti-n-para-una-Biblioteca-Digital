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
    public partial class FormRegistro : Form
    {
        public FormRegistro()
        {
            InitializeComponent();
            this.BackColor = Color.WhiteSmoke; // Gris claro moderno
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);


        }

        private void Regsitrarme_Click(object sender, EventArgs e)
        {
            string nombre = txtUsuario.Text.Trim();
            string clave = txtContraseña.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor completá todos los campos.");
                return;
            }

            if (DatosSistema.Usuarios.Any(u => u.Nombre == nombre))
            {
                MessageBox.Show("Ese nombre de usuario ya existe.");
                return;
            }

            int nuevoID = DatosSistema.Usuarios.Any() ? DatosSistema.Usuarios.Max(u => u.ID) + 1 : 1;
            int idLector = 1; // ID de familia "Lector"

            ClsUsuario nuevo = new ClsUsuario(nuevoID, nombre, clave, idLector);
            DatosSistema.Usuarios.Add(nuevo);

            // Guardar en archivo
            var lineas = new List<string> { "ID,Nombre,Clave,Familia" };
            foreach (var u in DatosSistema.Usuarios)
                lineas.Add($"{u.ID},{u.Nombre},{u.Clave},{u.FamiliaID}");

            File.WriteAllLines("usuarios.csv", lineas);

            MessageBox.Show("Usuario registrado como Lector. Ya podés iniciar sesión.");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
