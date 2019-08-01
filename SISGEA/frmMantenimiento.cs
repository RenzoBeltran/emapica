using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISGEA
{
    public partial class frmMantenimiento : Form
    {
        public frmMantenimiento(string usuario, string tipo_usuario)
        {
            InitializeComponent();
            lblusuario.Text = usuario;
            lblTipoUsuario.Text = tipo_usuario;
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPrincipal frmPrincipal = new frmPrincipal(lblusuario.Text, lblTipoUsuario.Text);
            DialogResult mensaje;
            mensaje = MessageBox.Show("¿Está seguro de salir?", "ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (mensaje == DialogResult.Cancel)
            {
                this.Show();
            }
            else
            {
                this.Close();
                frmPrincipal.Show();
            }
        }

        private void salirToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmPrincipal frmPrincipal = new frmPrincipal(lblusuario.Text, lblTipoUsuario.Text);
            DialogResult mensaje;
            mensaje = MessageBox.Show("¿Está seguro de salir?", "ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (mensaje == DialogResult.Cancel)
            {
                this.Show();
            }
            else
            {
                frmPrincipal.Show();
                this.Hide();
            }
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAgregarUsuario frmAgregarUsuario = new frmAgregarUsuario(lblusuario.Text, lblTipoUsuario.Text);

            frmAgregarUsuario.Show();
            this.Close();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionarUsuarios frmGestionarUsuarios = new frmGestionarUsuarios(lblusuario.Text, lblTipoUsuario.Text);
            this.Close();
            frmGestionarUsuarios.Show();
        }

        private void archivosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmGestionarArchivos frmGestionarArchivos = new frmGestionarArchivos(lblusuario.Text, lblTipoUsuario.Text);
            this.Close();
            frmGestionarArchivos.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmMantenimiento_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            lblfecha.Text = fecha.ToShortDateString();
            lblfecha.Refresh();
        }
    }
}
