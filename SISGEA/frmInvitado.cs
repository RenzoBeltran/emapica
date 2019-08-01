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
    public partial class frmInvitado : Form
    {
        public frmInvitado()
        {
            InitializeComponent();
        }
        public frmInvitado(string usuario, string tipo_usuario)
        {
            InitializeComponent();
            lblusuario.Text = usuario;
            lblTipoUsuario.Text = tipo_usuario;
        }

        private void frmInvitado_Load(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnMantenimiento.Enabled = false; 
            DateTime fecha = DateTime.Now;
            lblfecha.Text = fecha.ToShortDateString();
            lblfecha.Refresh();
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            frmBuscarInvitado frmBuscarInvitado = new frmBuscarInvitado(lblusuario.Text, lblTipoUsuario.Text);
            frmBuscarInvitado.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            frmIngreso frmIngreso = new frmIngreso();
            DialogResult mensaje;
            mensaje = MessageBox.Show("¿Está seguro de salir?", "ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (mensaje == DialogResult.Cancel)
            {
                this.Show();
            }
            else
            {
                this.Hide();
                frmIngreso.Show();

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
