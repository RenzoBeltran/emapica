using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace SISGEA
{
    public partial class frmAgregarUsuario : Form
    {
        Conexion c = new Conexion();
        public frmAgregarUsuario(string usuario, string tipo_usuario)
        {
            InitializeComponent();
            lblusuario.Text = usuario;
            lblTipoUsuario.Text = tipo_usuario;
        }

        void Activar()
        {
            txtNivelUsuario.Enabled = true;
            txtContrasena.Enabled = true;
            cmbNivelUsuario.SelectedIndex = -1;
            btnGuardar.Enabled = false;
        }
        void Desactivar()
        {
            txtNivelUsuario.Enabled = false;
            txtContrasena.Enabled = false;
            cmbNivelUsuario.SelectedIndex = -1;
            btnGuardar.Enabled = false;
        }
        void Limpiar()
        {
            txtNivelUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            cmbNivelUsuario.SelectedIndex = -1;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            frmMantenimiento frmMantenimiento = new frmMantenimiento(lblusuario.Text, lblTipoUsuario.Text);
            DialogResult mensaje;
            mensaje = MessageBox.Show("¿Está seguro de salir?", "ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (mensaje == DialogResult.Cancel)
            {
                this.Show();
            }
            else
            {
                this.Close();
                frmMantenimiento.Show();  
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            

            if (c.validarUsuario(txtNivelUsuario.Text) == 0)
            {

                try
                    {
                        if (txtNivelUsuario.Text!= "" && txtContrasena.Text != "")
                        {
                            MessageBox.Show(c.insertarUsuario(txtNivelUsuario.Text, txtContrasena.Text, cmbNivelUsuario.Text),"COMUNICADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Desactivar();
                        }
                    else
                    {
                        MessageBox.Show("Por favor llene todos los campos", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al guadar usuario");
                    }
            }
            else
            {
                MessageBox.Show("Usuario duplicado", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmAgregarUsuario_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            lblfecha.Text = fecha.ToShortDateString();
            lblfecha.Refresh();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Activar();
            Limpiar();
            btnGuardar.Enabled = true;
        }

        private void chkbMantenimiento_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
