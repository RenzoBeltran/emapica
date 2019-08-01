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
    public partial class frmPrincipal : Form
    {
       
        public frmPrincipal(string usuario, string tipo_usuario)
        {
            InitializeComponent();
            lblusuario.Text = usuario;
            lblTipoUsuario.Text = tipo_usuario;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            txtnumero frmBuscar = new txtnumero(lblusuario.Text, lblTipoUsuario.Text);
            frmBuscar.Show();
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            txtAreaExterna frmAgregar = new txtAreaExterna(lblusuario.Text, lblTipoUsuario.Text);
            frmAgregar.Show();
            this.Hide();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            lblfecha.Text = fecha.ToShortDateString();
            lblfecha.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMantenimiento frmMantenimiento = new frmMantenimiento(lblusuario.Text, lblTipoUsuario.Text);
            frmMantenimiento.Show();
            this.Hide();
        }
    }
}
