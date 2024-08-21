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
    public partial class Form1 : Form
    {
        //Conexion a SQL
        string conexionString = "Data Source=(DESCRIPTION=" +
            "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
            "(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=hr;" +
            "Password=BaseDatos123456;";
        
        public Form1()
        {
            InitializeComponent();
        }
        private void limpiar()
        {
            //Deja los TextBox vacios
            txtTitulo.Text = "";
            txtGenero.Text = "";
            txtPlataforma.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtener el contenido de los TextBox
                string titulo = txtTitulo.Text;
                string genero = txtGenero.Text;
                string plataforma = txtPlataforma.Text;
                int precio = Convert.ToInt32(txtPrecio.Text);
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                //Consulta SQL de insertar
                string sql = "INSERT INTO VIDEOJUEGOS (TITULO, GENERO, PLATAFORMA, PRECIO, CANTIDAD) VALUES " +
                    "(:titulo, :nombre,: apellido, :precio, :cantidad)";
                using (OracleConnection conn = new OracleConnection(conexionString))
                {
                    conn.Open();
                    using (OracleCommand comd = new OracleCommand(sql, conn))
                    {
                        comd.Parameters.Add(new OracleParameter("Titulo", titulo));
                        comd.Parameters.Add(new OracleParameter("Genero", genero));
                        comd.Parameters.Add(new OracleParameter("Plataforma", plataforma));
                        comd.Parameters.Add(new OracleParameter("Precio", precio));
                        comd.Parameters.Add(new OracleParameter("Cantidad", cantidad));

                        int rowsInserted = comd.ExecuteNonQuery();
                        if (rowsInserted > 0)
                        {
                            MessageBox.Show("Datos insertados.");
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
                MessageBox.Show("Error "+ex.Message);
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
