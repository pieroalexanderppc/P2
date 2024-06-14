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

namespace Proyecto_Final_Blood_Bank
{
	public partial class BANCO_DE_SANGRE : Form
	{
		public BANCO_DE_SANGRE()
		{
			InitializeComponent();
			lblVolver.MouseEnter += OnMouseEnter;
			lblVolver.MouseLeave += OnMouseLeave;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
		}
		private void Form_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

        private string connectionString = "Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True";

        private void CargarGrilla()
{
		using (SqlConnection con = new SqlConnection(connectionString))
		{
			string consulta = "Select * from BancoDeSangre";
			SqlDataAdapter adaptador = new SqlDataAdapter(consulta, con);
			DataTable dt = new DataTable();
			adaptador.Fill(dt);
			dgvBancoSangre.DataSource = dt;
			dgvBancoSangre.Columns[0].Width = 45;
			dgvBancoSangre.Columns[1].Width = 45;
			dgvBancoSangre.Columns[2].Width = 45;
		}
	}

	private void CargarGrilla2()
{
		using (SqlConnection con = new SqlConnection(connectionString))
		{
			string consulta = "select dni, nombre, apellido, tipo, rh from PACIENTES";
			SqlDataAdapter adaptador = new SqlDataAdapter(consulta, con);
			DataTable dt = new DataTable();
			adaptador.Fill(dt);
			dgvPacientes.DataSource = dt;
			dgvPacientes.Columns[3].Width = 45;
			dgvPacientes.Columns[4].Width = 45;
		}
	}






		private void label3_Click(object sender, EventArgs e)
		{
			string temp_usuario = lblUsuario.Text;
			string comando = "SELECT estado FROM Personal WHERE Usuario=@usuario";
			using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True"))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand(comando, con))
				{
					cmd.Parameters.AddWithValue("@usuario", temp_usuario);
					cmd.ExecuteNonQuery();

					SqlDataReader datos;
					datos = cmd.ExecuteReader();
					if (datos.Read())
					{
						string estado = datos["estado"].ToString();
						switch (estado)
						{
							case "N":
								new OPCIONES_1().Show();
								this.Hide();
								break;
							case "P":
								new OPCIONES_2().Show();
								this.Hide();
								break;
							case "A":
								new OPCIONES().Show();
								this.Hide();
								break;
							default:
								// Handle any other cases or errors here
								break;
						}
					}
				}
			}
		}
		private void OnMouseEnter(object sender, EventArgs e)
		{
			lblVolver.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
		}

		private void OnMouseLeave(object sender, EventArgs e)
		{
			lblVolver.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				// Establecer la conexión a la base de datos
				using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True"))
				{
					con.Open();

					// Consulta SQL parametrizada
					string consulta = "SELECT dni, nombre, apellido, tipo, rh FROM Pacientes WHERE dni LIKE @buscar OR nombre LIKE @buscar";

					// Crear el adaptador de datos con consulta parametrizada
					SqlDataAdapter adaptador = new SqlDataAdapter(consulta, con);

					// Añadir parámetros
					adaptador.SelectCommand.Parameters.AddWithValue("@buscar", "%" + txtBuscar.Text + "%");

					// Llenar DataTable con los resultados de la consulta
					DataTable dt = new DataTable();
					adaptador.Fill(dt);

					// Asignar el DataTable como fuente de datos del DataGridView
					dgvPacientes.DataSource = dt;

					// Ajustar anchos de columnas
					dgvPacientes.Columns[3].Width = 45;
					dgvPacientes.Columns[4].Width = 45;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al buscar pacientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private Rectangle RecTitulo;
		private Rectangle RecVolver;
		private Rectangle RecUsuario;
		private Rectangle ReclblNombre;
		private Rectangle RectxtNombre;
		private Rectangle RecBuscar;
		private Rectangle RecGrillaPacientes;
		private Rectangle RecGrillaStock;

		private Size formOriginalSize;

		private void BANCO_DE_SANGRE_Load(object sender, EventArgs e)
		{
			lblUsuario.Text = LOGIN.NombreUsuario;
			CargarGrilla();
			CargarGrilla2();

			formOriginalSize = this.Size;
			RecTitulo = new Rectangle(lblTitulo.Location.X, lblTitulo.Location.Y, lblTitulo.Width, lblTitulo.Height);
			RecVolver = new Rectangle(lblVolver.Location.X, lblVolver.Location.Y, lblVolver.Width, lblVolver.Height);
			RecUsuario = new Rectangle(lblUsuario.Location.X, lblUsuario.Location.Y, lblUsuario.Width, lblUsuario.Height);
			ReclblNombre = new Rectangle(lblNombre.Location.X, lblNombre.Location.Y, lblNombre.Width, lblNombre.Height);
			RectxtNombre = new Rectangle(txtBuscar.Location.X, txtBuscar.Location.Y, txtBuscar.Width, txtBuscar.Height);
			RecBuscar = new Rectangle(btnBuscar.Location.X, btnBuscar.Location.Y, btnBuscar.Width, btnBuscar.Height);
			RecGrillaPacientes = new Rectangle(dgvPacientes.Location.X, dgvPacientes.Location.Y, dgvPacientes.Width, dgvPacientes.Height);
			RecGrillaStock = new Rectangle(dgvBancoSangre.Location.X, dgvBancoSangre.Location.Y, dgvBancoSangre.Width, dgvBancoSangre.Height);

		}

		private void resizeControl(Rectangle OriginalControlRect, Control control)
		{
			float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
			float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);


			int newX = (int)(OriginalControlRect.X * xRatio);
			int newY = (int)(OriginalControlRect.Y * yRatio);

			int newWidth = (int)(OriginalControlRect.Width * xRatio);
			int newHeight = (int)(OriginalControlRect.Height * yRatio);

			control.Location = new Point(newX, newY);
			control.Size = new Size(newWidth, newHeight);

		}

		private void BANCO_DE_SANGRE_Resize(object sender, EventArgs e)
		{
			resizeControl(RecTitulo, lblTitulo);
			resizeControl(RecVolver, lblVolver);
			resizeControl(RecUsuario, lblUsuario);
			resizeControl(ReclblNombre, lblNombre);
			resizeControl(RectxtNombre, txtBuscar);
			resizeControl(RecBuscar, btnBuscar);
			resizeControl(RecGrillaPacientes, dgvPacientes);
			resizeControl(RecGrillaStock, dgvBancoSangre);


		}

        private void dgvBancoSangre_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && Convert.ToDouble(e.Value) <= 10)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
    }
}
