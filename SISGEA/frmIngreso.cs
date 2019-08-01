using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SISGEA
{
    public partial class frmIngreso : Form


    {
        public frmIngreso()
        {
            InitializeComponent();
        }

        public void login()
        {
            try
            {
                string cnn = ConfigurationManager.ConnectionStrings["SISGEA.Properties.Settings.Cadena"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cnn))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT user_name, tipo_usuario from usuario  WHERE user_name='" + txtUsuario.Text + "' AND password='" + txtPassword.Text + "'", conexion))
                    {
                        //SQLDataReader se ejecuta para ver si encuentra los valores a través del if
                        // SqlDataReader dr = cmd.ExecuteReader();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count ==1)
                        {
                           this.Hide();
                           if (dt.Rows[0][1].ToString() == "Administrador")
                            {
                                frmPrincipal frmPrincipal = new frmPrincipal(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());
                                frmPrincipal.Show();
                            }
                            else if(dt.Rows[0][1].ToString() == "Invitado")
                            {
                                frmInvitado frmInvitado = new frmInvitado(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());
                                frmInvitado.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Datos incorrectos. Por favor verifique su usuario y contraseña.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            login();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //frmPrincipal frmPrincipal = new frmPrincipal(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());
            DialogResult mensaje;
            mensaje = MessageBox.Show("¿Está seguro de salir?", "ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (mensaje == DialogResult.Cancel)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
            
        }

        private void frmIngreso_Resize(object sender, EventArgs e)
        {
           
        }

        private void frmIngreso_Load(object sender, EventArgs e)
        {

        }

        private void frmIngreso_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.login();
            }
        }
    }
}
