using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using DGVPrinterHelper;

namespace SISGEA
{
    public partial class frmBuscarInvitado : Form
    {
        SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-9L8J17H\\RENZOSERVICES;Initial Catalog=SISGEA_DB;User ID=emapica;Password=chelero01");
        public frmBuscarInvitado(string usuario, string tipo_usuario)
        {
            InitializeComponent();
            lblusuario.Text = usuario;
            lblTipoUsuario.Text = tipo_usuario;
        }
        private void Limpiar()
        {
            cmbSerial.Text = string.Empty;
            textBox1.Text = string.Empty;
            txtAno.Text = string.Empty;
            cmbArea.Text = string.Empty;
            cmbArea.SelectedIndex = -1;
            cmbSerial.SelectedIndex = -1;
            rbInterno.Checked = false;
            rbExterno.Checked = false;
            txtEntidad.Text = string.Empty;
            cmbOrigen.Text = string.Empty;
            cmbOrigen.SelectedIndex = -1;
            dgvDocumento.DataSource = null;


        }




        private void btnSalir1_Click(object sender, EventArgs e)
        {
            frmInvitado frmInvitado = new frmInvitado(lblusuario.Text, lblTipoUsuario.Text);
            DialogResult mensaje;
            mensaje = MessageBox.Show("¿Está seguro de salir?", "ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (mensaje == DialogResult.Cancel)
            {
                this.Show();
            }
            else
            {
                frmInvitado.Show();
                this.Hide();
            }
        }

        private void frmBuscarInvitado_Load(object sender, EventArgs e)
        {
            Conexion c = new Conexion();
            c.llenarSerial(cmbSerial);
            c.llenarArea(cmbArea);

            this.Limpiar();
            DateTime fecha = DateTime.Now;
            lblfecha.Text = fecha.ToShortDateString();
            lblfecha.Refresh();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
         
            if (cmbSerial.Text != "" || textBox1.Text != "" || txtAno.Text != "" || cmbArea.Text != "" || cmbOrigen.Text != "" || rbInterno.Checked == true)
            {
                SqlDataAdapter da = new SqlDataAdapter("select id as 'ID', serial as 'Serial', num_doc as 'Número documento',anio as 'Año',area as 'Área' ,descripcion as 'Descripción', fecha_registro as 'Fecha de registro',ruta_doc as 'Ruta',tipo_documento as 'Origen',  folios as 'Número de folios' from documento where serial = ALL(select serial from documento where serial ='" + cmbSerial.SelectedItem + "') AND num_doc = ALL(select num_doc from documento where num_doc ='" + textBox1.Text + "') AND anio = ALL(select anio from documento where anio ='" +txtAno.Text +"') AND area = ALL(select area from documento where area ='" + cmbArea.SelectedItem + "') AND tipo_documento = ALL(select tipo_documento from documento where tipo_documento ='" + cmbOrigen.SelectedItem + "') AND tipo_documento = ALL(select tipo_documento from documento where tipo_documento = '" + cmbOrigen.SelectedItem + "') AND condicion_doc =ALL(select condicion_doc from documento where condicion_doc ='" + rbInterno.Text + "')", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SISGEA_DB");
                this.dgvDocumento.DataSource = ds.Tables[0];
            }
            if (cmbSerial.Text != "" || textBox1.Text != "" || txtAno.Text != "" || cmbArea.Text != "" || cmbOrigen.Text != "" || rbExterno.Checked == true || txtEntidad.Text != "")
            {
                SqlDataAdapter da = new SqlDataAdapter("select id as 'ID', serial as 'Serial', num_doc as 'Número documento',anio as 'Año',area as 'Área' ,descripcion as 'Descripción', fecha_registro as 'Fecha de registro',ruta_doc as 'Ruta',tipo_documento as 'Origen',  folios as 'Número de folios' from documento where serial = ALL(select serial from documento where serial ='" + cmbSerial.SelectedItem + "') AND num_doc = ALL(select num_doc from documento where num_doc ='" + textBox1.Text + "') AND anio = ALL(select anio from documento where anio ='" + txtAno.Text + "') AND area = ALL(select area from documento where area ='" + cmbArea.SelectedItem + "') AND tipo_documento = ALL(select tipo_documento from documento where tipo_documento ='" + cmbOrigen.SelectedItem + "') AND tipo_documento = ALL(select tipo_documento from documento where tipo_documento = '" + cmbOrigen.SelectedItem + "') AND condicion_doc =ALL(select condicion_doc from documento where condicion_doc ='" + rbExterno.Text + "') AND entidad = ALL(select entidad from documento where entidad = '"+txtEntidad.Text+"')", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SISGEA_DB");
                this.dgvDocumento.DataSource = ds.Tables[0];
            }
        }
        

        private void txtLimpiar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                string direccion = this.dgvDocumento.CurrentRow.Cells[7].Value.ToString();
                Process.Start(direccion);
            }
            catch (Exception)
            {
                MessageBox.Show("Ruta no encontrada");
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvDocumento.Rows.Count < 1)
            {
                MessageBox.Show("Por favor indique que registros mostrar");
            }
            else
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = "Reporte de Documentos"; //give your report name
                printer.SubTitle = String.Format("Fecha: {0}", DateTime.Now.ToString("G"));
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true; // if you need page numbers you can keep this as true other wise false
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "Pie de Pàgina"; //this is the footer
                printer.FooterSpacing = 15;
                printer.printDocument.DefaultPageSettings.Landscape = true;
                printer.PrintDataGridView(dgvDocumento);
            }
        }
    }
}
