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

namespace SISGEA
{
    public partial class frmGestionarArchivos : Form
    {
        string condicion;
        SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-9L8J17H\\RENZOSERVICES;Initial Catalog=SISGEA_DB;User ID=emapica;Password=chelero01");
        public frmGestionarArchivos(string usuario, string tipo_usuario)
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
            txtAexterna.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtRuta.Text = string.Empty;

        }

        private void button2_Click(object sender, EventArgs e)
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

        private void frmGestionarArchivos_Load(object sender, EventArgs e)
        {
            Conexion c = new Conexion();
            c.llenarSerial(cmbSerial);
            c.llenarArea(cmbArea);
           
            this.Limpiar();
            DateTime fecha = DateTime.Now;
            lblfecha.Text = fecha.ToShortDateString();
            lblfecha.Refresh();
            btnModificar1.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnNbusqueda.Enabled = true;
            btnBuscar.Enabled = false;
            cmbSerial.Enabled = false;
            textBox1.Enabled = false;
            txtAno.Enabled = false;
            cmbArea.Enabled = false;
            cmbOrigen.Enabled = false;
            txtDescripcion.Enabled = false;
            txtEntidad.Enabled = false;
            txtRuta.Enabled = false;
            rbExterno.Enabled = false;
            rbInterno.Enabled = false;
            txtAexterna.Enabled = false;
            btnExaminar.Enabled = false;

        }

        private void btnModificar1_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            int poc;
            poc = dgvDocumento.CurrentRow.Index;
            cmbSerial.Text = dgvDocumento[1, poc].Value.ToString();
            textBox1.Text = dgvDocumento[2, poc].Value.ToString();
            txtAno.Text = dgvDocumento[3, poc].Value.ToString();
            cmbArea.Text = dgvDocumento[4, poc].Value.ToString();
            cmbOrigen.Text = dgvDocumento[9, poc].Value.ToString();
            if (dgvDocumento[10, poc].Value.ToString() == "Externo")
            {
                rbExterno.Checked = true;
            }
            if (dgvDocumento[10, poc].Value.ToString() == "Interno")
            {
                rbInterno.Checked = true;
            }
            txtAexterna.Text = dgvDocumento[11, poc].Value.ToString();
            txtEntidad.Text = dgvDocumento[12, poc].Value.ToString();
            txtRuta.Text = dgvDocumento[8, poc].Value.ToString();
            txtDescripcion.Text = dgvDocumento[5, poc].Value.ToString();
            cmbSerial.Enabled = true;
            textBox1.Enabled = true;
            txtAno.Enabled = true;
            cmbArea.Enabled = true;
            cmbOrigen.Enabled = true;
            txtDescripcion.Enabled = true;
            txtRuta.Enabled = true;
            rbExterno.Enabled = true;
            rbInterno.Enabled = true;
          
            btnExaminar.Enabled = true;
           


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbSerial.Text != "" || textBox1.Text != "" || txtAno.Text != "" || cmbArea.Text != "" || cmbOrigen.Text != "" || rbInterno.Checked == true)
            {
                SqlDataAdapter da = new SqlDataAdapter("select id as 'ID', serial as 'Serial', num_doc as 'Número documento',anio as 'Año',area as 'Área' ,descripcion as 'Descripción', fecha_registro as 'Fecha de registro',ruta_doc as 'Ruta',tipo_documento as 'Origen',folios as 'Número de folios' from documento where serial = ALL(select serial from documento where serial ='" + cmbSerial.SelectedItem + "') AND num_doc = ALL(select num_doc from documento where num_doc ='" + textBox1.Text + "') AND anio = ALL(select anio from documento where anio ='" + txtAno.Text + "') AND area = ALL(select area from documento where area ='" + cmbArea.SelectedItem + "') AND tipo_documento = ALL(select tipo_documento from documento where tipo_documento ='" + cmbOrigen.SelectedItem + "') AND tipo_documento = ALL(select tipo_documento from documento where tipo_documento = '" + cmbOrigen.SelectedItem + "') AND condicion_doc =ALL(select condicion_doc from documento where condicion_doc ='" + rbInterno.Text + "')", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SISGEA_DB");
                this.dgvDocumento.DataSource = ds.Tables[0];
            }
            if (cmbSerial.Text != "" || textBox1.Text != "" || txtAno.Text != "" || cmbArea.Text != "" || cmbOrigen.Text != "" || rbExterno.Checked == true || txtEntidad.Text != "")
            {
                SqlDataAdapter da = new SqlDataAdapter("select id as 'ID', serial as 'Serial', num_doc as 'Número documento',anio as 'Año',area as 'Área' ,descripcion as 'Descripción', fecha_registro as 'Fecha de registro',ruta_doc as 'Ruta',tipo_documento as 'Origen',folios as 'Número de folios' from documento where serial = ALL(select serial from documento where serial ='" + cmbSerial.SelectedItem + "') AND num_doc = ALL(select num_doc from documento where num_doc ='" + textBox1.Text + "') AND anio = ALL(select anio from documento where anio ='" + txtAno.Text + "') AND area = ALL(select area from documento where area ='" + cmbArea.SelectedItem + "') AND tipo_documento = ALL(select tipo_documento from documento where tipo_documento ='" + cmbOrigen.SelectedItem + "') AND tipo_documento = ALL(select tipo_documento from documento where tipo_documento = '" + cmbOrigen.SelectedItem + "') AND condicion_doc =ALL(select condicion_doc from documento where condicion_doc ='" + rbExterno.Text + "') AND entidad = ALL(select entidad from documento where entidad = '" + txtEntidad.Text + "')", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SISGEA_DB");
                this.dgvDocumento.DataSource = ds.Tables[0];
            }
            cmbSerial.Enabled = false;
            textBox1.Enabled = false;
            txtAno.Enabled = false;
            cmbArea.Enabled = false;
            cmbOrigen.Enabled = false;
            txtDescripcion.Enabled = false;
            txtEntidad.Enabled = false;
            txtRuta.Enabled = false;
            rbExterno.Enabled = false;
            rbInterno.Enabled = false;
            txtAexterna.Enabled = false;
            btnBuscar.Enabled = false;
            btnNbusqueda.Enabled = true;
        }

        private void dgvDocumento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnModificar1.Enabled = true;
            btnModificar.Enabled = true;
            btnBuscar.Enabled = false;
            btnNbusqueda.Enabled = true;
        }

        private void btnNbusqueda_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            btnBuscar.Enabled = true;
            btnModificar1.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnNbusqueda.Enabled = false;
            rbExterno.Checked = false;
            rbInterno.Checked = false;
            cmbSerial.Enabled = true;
            textBox1.Enabled = true;
            txtAno.Enabled = true;
            cmbArea.Enabled = true;
            cmbOrigen.Enabled = true;
            txtDescripcion.Enabled = false;
            txtEntidad.Enabled = true;
            rbExterno.Enabled = true;
            rbInterno.Enabled = true;
            txtAexterna.Enabled = false;

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int poc;
            poc = dgvDocumento.CurrentRow.Index;
            int id = Convert.ToInt32(dgvDocumento[0, poc].Value);
            string ruta = dgvDocumento[8, poc].Value.ToString();

            DialogResult mensaje;
            mensaje = MessageBox.Show("¿Está seguro de eliminar el documento?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mensaje == DialogResult.Yes)
            {
                
                string sql = "DELETE FROM documento WHERE id=" + id + "";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.Text;
                cnn.Open();
                try
                {
                    if (System.IO.File.Exists(ruta))
                    {
                        try
                        {
                            System.IO.File.Delete(ruta);
                        }
                        catch (Exception a)
                        {
                            Console.WriteLine(a.Message);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ruta no encontrada", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro eliminado correctamente", "DATOS ELIMINADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    dgvDocumento.DataSource = null;
                    btnModificar1.Enabled = false;
                    this.Limpiar();
                }
                catch
                {
                    MessageBox.Show("Error al eliminar el usuario", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cnn.Close();
                }
            }
            else
            {
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            if (rbInterno.Checked == true)
            {
               condicion = "Interno";
            }
            if (rbExterno.Checked == true)
            {
               condicion = "Externo";
            }
         

            int poc;
                poc = dgvDocumento.CurrentRow.Index;
                int id = Convert.ToInt32(dgvDocumento[0, poc].Value);
                DialogResult mensaje;
                mensaje = MessageBox.Show("¿Está seguro de modificar los datos del documento?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (mensaje == DialogResult.Yes)
                {

                    string sql = "UPDATE documento SET serial='" + cmbSerial.Text +
                        "', anio='" + txtAno.Text +
                        "',  num_doc = '" + textBox1.Text+ "', area = '"+ cmbArea.Text+ "', descripcion =   '" + txtDescripcion.Text + "', ruta_doc =  '" + txtRuta.Text + "', fecha_modificacion =  '" + fecha + "', tipo_documento =  '" + cmbOrigen.Text + "', condicion_doc =  '" + condicion + "', area_externa = '" + txtAexterna.Text + "', entidad = '" + txtEntidad.Text + "'  WHERE id='" + id + "'";

                    SqlCommand cmd = new SqlCommand(sql, cnn);

                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    try
                    {
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                            MessageBox.Show("Registro actualizado correctamente", "DATOS MODIFICADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSerial.Enabled = false;
                    textBox1.Enabled = false;
                    txtAno.Enabled = false;
                    cmbArea.Enabled = false;
                    cmbOrigen.Enabled = false;
                    txtDescripcion.Enabled = false;
                    txtEntidad.Enabled = false;
                    txtRuta.Enabled = false;
                    rbExterno.Enabled = false;
                    rbInterno.Enabled = false;
                    txtAexterna.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar1.Enabled = false;
                    this.Limpiar();

                }
                    catch
                    {
                        MessageBox.Show("Error al actualizar los datos del documento" , "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cnn.Close();
                    }
                }
            
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscar = new OpenFileDialog();

            if (buscar.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = buscar.FileName;
            }
        }

        private void rbExterno_CheckedChanged(object sender, EventArgs e)
        {
            
                txtAexterna.Enabled = true;
                txtEntidad.Enabled = true;
            
        }

        private void rbInterno_CheckedChanged(object sender, EventArgs e)
        {

            txtAexterna.Text = string.Empty;
            txtEntidad.Text = string.Empty;
            txtAexterna.Enabled = false;
            txtEntidad.Enabled = false;

           
        }
    }
}
