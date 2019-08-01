using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;


public class Conexion
{

    SqlConnection cnn;
    SqlCommand cmd;
    SqlDataReader dr;
   // DataSet ds;
    // public static string Cn = SISGEA.Properties.Settings.Default.Cadena;
    public Conexion()
    {
        try
        {

            cnn = new SqlConnection("Data Source=DESKTOP-9L8J17H\\RENZOSERVICES;Initial Catalog=SISGEA_DB;User ID=emapica;Password=chelero01");
            cnn.Open();
        }
        catch (Exception ex)
        {
            MessageBox.Show("No conectado " + ex.ToString());
        }
    }

    public void llenarSerial(ComboBox cb)
    {
        try
        {
            cmd = new SqlCommand("SELECT nom_serial FROM serial", cnn);
            //Con el ExecuteReader se llena lo que se lee en la consulta
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cb.Items.Add(dr["nom_serial"].ToString());
            }
            cb.SelectedIndex = 0;
            dr.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se cargaron los datos al campo Serial" + ex.ToString());
        }
    }

    public void llenarArea(ComboBox cb)
    {
        try
        {
            cmd = new SqlCommand("SELECT nombre_area FROM area", cnn);
            //Con el ExecuteReader se llena lo que se lee en la consulta
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cb.Items.Add(dr["nombre_area"].ToString());
            }
            cb.SelectedIndex = 0;
            dr.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se cargaron los datos al campo Area" + ex.ToString());
        }
    }
   

    public void llenarUsuarios(ComboBox cb)
    {
        try
        {
            cmd = new SqlCommand("SELECT user_name FROM usuario", cnn);
            //Con el ExecuteReader se llena lo que se lee en la consulta
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cb.Items.Add(dr["user_name"].ToString());
            }
            cb.SelectedIndex = 0;
            dr.Close();
        }
        catch (Exception ex){
            MessageBox.Show("No se cargaron los datos al campo Area" + ex.ToString());
        }
    }
    
    public string insertar(string num_doc, int anio, string descripcion, string ruta, DateTime fecha_registro, string serial, string area, string tipo_documento, string condicion_doc, string area_externa, string entidad, int folios) {

        
        string salida = "Los datos se guardaron correctamente";
        

        try
        {
            cmd = new SqlCommand("insert into documento (num_doc, anio, descripcion, ruta_doc, fecha_registro, serial, area, tipo_documento, condicion_doc, area_externa, entidad, folios) values('"+num_doc+ "', " + anio+", '"+descripcion+ "', '" + ruta + "', '" + fecha_registro + "', '" + serial + "', '" + area + "', '" + tipo_documento + "', '" + condicion_doc + "', '" + area_externa + "', '" + entidad + "','"+folios+"')", cnn);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            // MessageBox.Show("Error en:" + ex);
            salida = "No se conectó" + ex.ToString();
        }
        return salida;
    }

   public string insertarUsuario(string user_name, string password, string tipo_usuario)
    {
        //string sql = "SELECT user_name FROM usuario WHERE user_name='"+user_name+"'";
        //SqlCommand cmd = new SqlCommand(sql, cnn);
        // cmd.ExecuteNonQuery();
       
        string salida = "Los datos se guardaron correctamente";
        try
        {
            cmd = new SqlCommand("insert into usuario (user_name, password, tipo_usuario) values('" + user_name + "', '" + password + "', '" + tipo_usuario + "')", cnn);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
             MessageBox.Show("Error en la conexión:" + ex);
        }
        return salida;
    }

    public int validarDocumento(string num_doc, int anio, string serial, string area)
    {
        string validar = "";
        int contador = 0;
        try
        {
            cmd = new SqlCommand("select num_doc, anio, serial, area from documento where num_doc='"+num_doc+"' and anio="+anio+" and serial='"+serial+"' and area='"+area+"'", cnn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                contador++;
            }
            dr.Close();
        }
        catch (Exception ex)
        {
            // MessageBox.Show("Error en:" + ex);
            validar = "Error" + ex.ToString();
        }
        return contador;
    }
    public int validarUsuario(string user_name)
    {
        string validar = "";
        int contador = 0;
        try
        {
            cmd = new SqlCommand("select user_name from usuario where user_name='" + user_name + "'", cnn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                contador++;
            }
            dr.Close();
        }
        catch (Exception ex)
        {
            validar = "Error" + ex.ToString();
        }
        return contador;
    }
    public void ActualizarUsuario(TextBox tx1, TextBox tx2, ComboBox cb, DataGridView dgv)
    {
        int poc;
        poc = dgv.CurrentRow.Index;
        try
        {
            cmd = new SqlCommand("update usuario set user_name = " + tx1 + ", password = " + tx2 + ", tipo_usuario =" + cb + " where idusuario = "+dgv+"", cnn);
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            MessageBox.Show("Error en la conexión:" + ex);
            //salida = "No se conectó" + ex.ToString();
        }
        cnn.Close();
    }

}
