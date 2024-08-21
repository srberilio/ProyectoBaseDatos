using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ProyectoBaseDatos
{
    public partial class frmRegistro : Form
    {
        //Conexion a SQL
        string conexionString = "Data Source=(DESCRIPTION=" +
            "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
            "(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=hr;" +
            "Password=BaseDatos123456;";
        public frmRegistro()
        {
            InitializeComponent();
        }
        private void limpiar()
        {
            //Deja los TextBox vacios
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
        }

        private void btnRegistrarme_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtener el contenido de los TextBox
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string correo = txtCorreo.Text;
                int telefono = Convert.ToInt32(txtTelefono.Text);
                //Consulta SQL de insertar
                string sql = "INSERT INTO CLIENTES (NOMBRE, APELLIDO, CORREO_ELECTRONICO, TELEFONO) VALUES " +
                    "(:nombre, :apellido,: correo, :telefono)";
                using (OracleConnection conn = new OracleConnection(conexionString))
                {
                    conn.Open();
                    using (OracleCommand comd = new OracleCommand(sql, conn))
                    {
                        comd.Parameters.Add(new OracleParameter("Nombre", nombre));
                        comd.Parameters.Add(new OracleParameter("Apellido", apellido));
                        comd.Parameters.Add(new OracleParameter("CORREO_ELECTRONICO", correo));
                        comd.Parameters.Add(new OracleParameter("Telefono", telefono));

                        int rowsInserted = comd.ExecuteNonQuery();
                        if (rowsInserted > 0)
                        {
                            MessageBox.Show("Usuario registrado en la base de datos.");
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudieron insertar datos.");
                            conn.Close();
                        }
                    }
                    limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void txtLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
