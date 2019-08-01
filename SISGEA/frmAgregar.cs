using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Diagnostics;

namespace SISGEA
{
    public partial class txtAreaExterna : Form
    {
        Conexion c = new Conexion();
        string _condicion;
        public txtAreaExterna(string usuario, string tipo_usuario)
        {
            InitializeComponent();
            lblusuario.Text = usuario;
            lblTipoUsuario.Text = tipo_usuario;
        }

        private void Desactivar()
        {
            cmbSerial.Enabled = false;
            txtNumero.Enabled = false;
            txtAnio.Enabled = false;
            cmbArea.Enabled = false;
            cmbRepetido.Enabled = false;
            btnExaminar.Enabled = false;
            txtExaminar.Enabled = false;
            txtDescripcion.Enabled = false;
            btnGuardar.Enabled = false;
            cmbTipoDocumento.Enabled = false;
            txtExterno.Enabled = false;
            rbInterno.Enabled = false;
            rbExterno.Enabled = false;
            txtEntidad.Enabled = false;
            txtFolios.Enabled = false;
        }

        private void Activar()
        {
            cmbSerial.Enabled = true;
            txtNumero.Enabled = true;
            txtAnio.Enabled = true;
            cmbArea.Enabled = true;
            cmbRepetido.Enabled = true;
            btnExaminar.Enabled = true;
            txtExaminar.Enabled = true;
            txtDescripcion.Enabled = true;
            btnGuardar.Enabled = true;
            cmbTipoDocumento.Enabled = true;
            rbInterno.Enabled = true;
            rbExterno.Enabled = true;
            txtEntidad.Enabled = true;
            txtFolios.Enabled = true;
        }

        private void Limpiar()
        {
            cmbSerial.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtAnio.Text = string.Empty;
            cmbArea.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtExaminar.Text = string.Empty;
            txtEntidad.Text = string.Empty;
            cmbRepetido.SelectedIndex = -1;
            cmbArea.SelectedIndex = -1;
            cmbSerial.SelectedIndex = -1;
            cmbTipoDocumento.SelectedIndex = -1;
            rbInterno.Checked = false;
            rbExterno.Checked = false;
            txtEntidad.Text = string.Empty;
            txtExterno.Text = string.Empty;
            txtFolios.Text = string.Empty;
        }

       
        public void cargar_Serial()
        {
            string cnn = ConfigurationManager.ConnectionStrings["SISGEA_DBConnectionString"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cnn);
            DataSet ds = new DataSet();
            //indicamos la consulta en SQL y conexion sql
            SqlDataAdapter da = new SqlDataAdapter("select nom_serial from serial", cnn);
            //se indica el nombre de la tabla
            da.Fill(ds, "serial");
            cmbSerial.DataSource = ds.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            cmbSerial.ValueMember = "nom_serial";
        }

        public void cargar_Area()
        {
            string cnn = ConfigurationManager.ConnectionStrings["SISGEA_DBConnectionString"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cnn);
            DataSet ds = new DataSet();
            //indicamos la consulta en SQL y conexion sql
            SqlDataAdapter da = new SqlDataAdapter("select nombre_area from area", cnn);
            //se indica el nombre de la tabla
            da.Fill(ds, "area");
            cmbArea.DataSource = ds.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            cmbArea.ValueMember = "nombre_area";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
            frmPrincipal frmPrincipal = new frmPrincipal(lblusuario.Text, lblTipoUsuario.Text);
            DialogResult mensaje;
           mensaje= MessageBox.Show("¿Está seguro de salir?", "ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (mensaje == DialogResult.Cancel)
            {
                this.Show();
            }
            else {
                frmPrincipal.Show();
                this.Hide();
            }
            
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            Conexion c = new Conexion();
            c.llenarSerial(cmbSerial);
            c.llenarArea(cmbArea);
            Desactivar();
            Limpiar();
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            string repetido;

            try
            {
                switch (cmbRepetido.SelectedItem.ToString())
                {
                    case "0":
                        repetido = txtNumero.Text;
                        txtNumero.Text = repetido;
                        break;
                    case "1":
                        repetido = txtNumero.Text + " -A";
                        txtNumero.Text = repetido;
                        break;
                    case "2":
                        repetido = txtNumero.Text + " -B";
                        txtNumero.Text = repetido;
                        break;
                    case "3":
                        repetido = txtNumero.Text + " -C";
                        txtNumero.Text = repetido;
                        break;
                    case "5":
                        repetido = txtNumero.Text + " -D";
                        txtNumero.Text = repetido;
                        break;
                    default:
                        repetido = txtNumero.Text + " -E";
                        txtNumero.Text = repetido;
                        break;
                }
                //Asignando un valor a los radiobutton

                if (rbInterno.Checked)
                {
                    _condicion = "Interno";
                    
                }
                if (rbExterno.Checked)
                {
                    _condicion = "Externo";
                }
                //Ingresando datos a la BD
                if (c.validarDocumento(txtNumero.Text, Convert.ToInt32(txtAnio.Text), cmbSerial.SelectedItem.ToString(), cmbArea.SelectedItem.ToString()) == 0)
                {
                    try
                    {
                        //Verificando que todos los datos estén llenos antes de guardar

                        if (rbInterno.Checked == true) {
                            if (txtNumero.Text != "" && txtAnio.Text != "" && txtExaminar.Text != "" && txtDescripcion.Text != "" && cmbArea.SelectedItem != null && cmbSerial.SelectedItem != null && cmbTipoDocumento.SelectedItem != null && txtFolios.Text != "") {
                                MessageBox.Show(c.insertar(txtNumero.Text, Convert.ToInt32(txtAnio.Text), txtDescripcion.Text, txtExaminar.Text, fecha, cmbSerial.SelectedItem.ToString(), cmbArea.SelectedItem.ToString(), cmbTipoDocumento.SelectedItem.ToString(), _condicion, txtExterno.Text, txtEntidad.Text, Convert.ToInt32(txtFolios.Text)), "COMUNICADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Desactivar();
                            }
                            else
                            {
                                MessageBox.Show("Por favor, ingresar todos los datos. ", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }

                        if (rbExterno.Checked == true)
                        {
                            if ((txtNumero.Text != "" && txtAnio.Text != "" && txtExterno.Text != "" && txtEntidad.Text != "" && txtExaminar.Text != "" && txtDescripcion.Text != "" && txtFolios.Text != "") && (rbInterno.Checked == false || rbExterno.Checked == false))
                            {
                                MessageBox.Show(c.insertar(txtNumero.Text, Convert.ToInt32(txtAnio.Text), txtDescripcion.Text, txtExaminar.Text, fecha, cmbSerial.SelectedItem.ToString(), cmbArea.SelectedItem.ToString(), cmbTipoDocumento.SelectedItem.ToString(), _condicion, txtExterno.Text, txtEntidad.Text, Convert.ToInt32(txtFolios.Text)), "COMUNICADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Desactivar();
                            }
                            else
                            {
                                MessageBox.Show("Por favor, ingresar todos los datos. ", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        if(rbExterno.Checked==false && rbInterno.Checked == false)
                        {
                            MessageBox.Show("Por favor, ingresar todos los datos. ", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Por favor, ingresar todos los datos. ", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Datos repetidos. Por favor, seleccione otra opción de REPETIDO e ingrese nuevamente el número de documento", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNumero.Text = "";

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, ingresar todos los datos. ", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            btnNuevo.Enabled = true;

        }

        private void cmbSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Activar();
            Limpiar();
            btnNuevo.Enabled = false;
            txtEntidad.Enabled = false;
            cmbRepetido.Enabled = false;
            cmbRepetido.SelectedIndex = 0;
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscar = new OpenFileDialog();

            if (buscar.ShowDialog() == DialogResult.OK)
            {
                txtExaminar.Text = buscar.FileName;
            }
        
        }

        private void rbExterno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExterno.Checked)
            {
                txtExterno.Enabled = true;
                cmbRepetido.Enabled = false;
                cmbRepetido.SelectedIndex = 0;
                cmbTipoDocumento.SelectedIndex = 0;
                cmbTipoDocumento.Enabled = false;
                txtEntidad.Enabled = true;

            }
        }

        private void rbInterno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInterno.Checked)
            {
                txtExterno.Enabled = false;
                cmbRepetido.Enabled = true;
                cmbTipoDocumento.Enabled = true;
                txtEntidad.Enabled = false;

            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
