using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISGEA
{
    public partial class frmGestionarUsuarios : Form
    {
        Conexion c = new Conexion();
        SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-9L8J17H\\RENZOSERVICES;Initial Catalog=SISGEA_DB;User ID=emapica;Password=chelero01");
        public frmGestionarUsuarios(string usuario, string tipo_usuario)
        {
            InitializeComponent();
            lblusuario.Text = usuario;
            lblTipoUsuario.Text = tipo_usuario;

        }

        private void Limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtNivelUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            cmbNivelUsuario.SelectedIndex = -1;
        }
   
        private void Habilitar()
        {
            txtUsuario.Enabled = true;
            cmbNivelUsuario.Enabled = true;
            txtContrasena.Enabled = true;
            btnGuardar.Enabled = true;
            cmbNivelUsuario.Enabled = true; 
            
        }
        private void Deshabilitar()
        {
            txtUsuario.Enabled = false;
            cmbNivelUsuario.Enabled = false;
            txtContrasena.Enabled = false;
            btnGuardar.Enabled = false;
            txtNivelUsuario.Enabled = false;
            cmbNivelUsuario.Enabled = false;

        }

        private void LlenarDataGrid()
        {
            //Cargamos datos a dgvUsuarios
            SqlCommand cmd = new SqlCommand("Select idusuario, user_name, password, tipo_usuario  from usuario", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvUsuarios.DataSource = dt;
            cnn.Close();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void frmGestionarUsuarios_Load(object sender, EventArgs e)
        {
            LlenarDataGrid();
            DateTime fecha = DateTime.Now;
            lblfecha.Text = fecha.ToShortDateString();
            lblfecha.Refresh();
            Deshabilitar();
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Habilitar();
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            int poc;
            poc = dgvUsuarios.CurrentRow.Index;
            txtUsuario.Text = dgvUsuarios[1, poc].Value.ToString();
            txtNivelUsuario.Text = dgvUsuarios[3, poc].Value.ToString();
            txtContrasena.Text = dgvUsuarios[2, poc].Value.ToString();
            cmbNivelUsuario.SelectedIndex = 0;
            cmbNivelUsuario.Text = txtNivelUsuario.Text;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "" && txtContrasena.Text != "" && cmbNivelUsuario.Text != "")
            {
                int poc;
                poc = dgvUsuarios.CurrentRow.Index;
                int id = Convert.ToInt32(dgvUsuarios[0, poc].Value);
                DialogResult mensaje;
                mensaje = MessageBox.Show("¿Está seguro de modificar los datos de usuario de '"+txtUsuario.Text+"'?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (mensaje == DialogResult.Yes)
                {

                        string sql = "UPDATE usuario SET user_name='" + txtUsuario.Text +
                            "', password='" + txtContrasena.Text +
                            "', " + "tipo_usuario='" + cmbNivelUsuario.Text + "' WHERE idusuario=" + id + "";

                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    try
                    {
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                            MessageBox.Show("Registro actualizado correctamente", "DATOS MODIFICADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LlenarDataGrid();
                        
                        
                    }
                    catch 
                    {
                        MessageBox.Show("Error al actualizar los datos del usuario '"+txtUsuario.Text+"'", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cnn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor llene todos los campos", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Deshabilitar();
            btnEliminar.Enabled = false;
            btnSalir.Enabled = true;
            btnModificar.Enabled = false;
            Limpiar();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje;
            mensaje = MessageBox.Show("¿Está seguro de eliminar el usuario?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mensaje == DialogResult.Yes)
            {
                int poc;
                poc = dgvUsuarios.CurrentRow.Index;
                int id = Convert.ToInt32(dgvUsuarios[0, poc].Value);
                string sql = "DELETE FROM usuario WHERE idusuario='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.Text;
                cnn.Open();
               
                try
                {
                    int i=cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro eliminado correctamente", "DATOS ELIMINADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenarDataGrid();
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
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

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
