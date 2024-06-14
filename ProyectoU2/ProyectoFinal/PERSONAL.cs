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
	public partial class PERSONAL : Form
	{
		public PERSONAL()
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

		private void CargarGrilla()
		{
			SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True");
			string consulta = "Select Usuario,estado from Personal";
			SqlDataAdapter adaptador = new SqlDataAdapter(consulta, con);
			DataTable dt = new DataTable();
			adaptador.Fill(dt);
			dgvPersonal.DataSource = dt;
			dgvPersonal.Columns[1].Width = 45;

		}


		private void label3_Click(object sender, EventArgs e)
		{
			string temp_usuario = lblUsuario.Text;
			string comando = "SELECT estado FROM Personal WHERE Usuario = @usuario";

			// Establecer la conexión a la base de datos
			using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True"))
			{
				con.Open();

				// Crear el comando SQL parametrizado
				using (SqlCommand cmd = new SqlCommand(comando, con))
				{
					cmd.Parameters.AddWithValue("@usuario", temp_usuario);

					// Ejecutar la consulta
					using (SqlDataReader datos = cmd.ExecuteReader())
					{
						if (datos.Read())
						{
							string estado = datos["estado"].ToString();

							// Mostrar las opciones según el estado obtenido
							if (estado == "N")
							{
								new OPCIONES_1().Show();
								this.Hide();
							}
							else if (estado == "P")
							{
								new OPCIONES_2().Show();
								this.Hide();
							}
							else if (estado == "A")
							{
								new OPCIONES().Show();
								this.Hide();
							}
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

		private void dgvPersonal_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			txtUsuario.Text = Convert.ToString(dgvPersonal.CurrentRow.Cells["Usuario"].Value);
			string estatus = Convert.ToString(dgvPersonal.CurrentRow.Cells["estado"].Value);


			if (estatus.Equals("P"))
			{

				cmbEstado.SelectedIndex = 1;

			}
			else if (estatus.Equals("A"))
			{
				cmbEstado.SelectedIndex = 0;


			}
			else
			{
				cmbEstado.SelectedIndex = 2;
			}
		}


		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				string temp_usuario = txtUsuario.Text;
				string comando = "UPDATE Personal SET estado='P' WHERE Usuario = @usuario";

				// Establecer la conexión a la base de datos
				using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True"))
				{
					con.Open();

					// Crear el comando SQL parametrizado
					using (SqlCommand cmd = new SqlCommand(comando, con))
					{
						cmd.Parameters.AddWithValue("@usuario", temp_usuario);

						// Ejecutar la consulta
						cmd.ExecuteNonQuery();

						MessageBox.Show("Se ha ascendido con éxito.", "Banco de Sangre", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}

				// Actualizar la grilla después de modificar
				CargarGrilla();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al modificar el registro: " + ex.Message, "Banco de Sangre", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnBajar_Click(object sender, EventArgs e)
		{
			try
			{
				string temp_usuario = txtUsuario.Text;
				string comando = "UPDATE Personal SET estado='N' WHERE Usuario = @usuario";

				// Establecer la conexión a la base de datos
				using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Hospital;Integrated Security=True"))
				{
					con.Open();

					// Crear el comando SQL parametrizado
					using (SqlCommand cmd = new SqlCommand(comando, con))
					{
						cmd.Parameters.AddWithValue("@usuario", temp_usuario);

						// Ejecutar la consulta
						cmd.ExecuteNonQuery();

						MessageBox.Show("Se ha bajado el rango con éxito.", "Banco de Sangre", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}

				// Actualizar la grilla después de modificar
				CargarGrilla();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al modificar el registro: " + ex.Message, "Banco de Sangre", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private Rectangle RecTitulo;
		private Rectangle RecVolver;
		private Rectangle ReclblUsuario;
		private Rectangle ReclblUsuario2;
		private Rectangle RectxtUsuario;
		private Rectangle RecbtnSubir;
		private Rectangle ReclblEstado;
		private Rectangle ReccmbEstado;
		private Rectangle RecBtnBajar;
		private Rectangle RecdgvPersonal;

		private Size formOriginalSize;

		private void PERSONAL_Load(object sender, EventArgs e)
		{
			lblUsuario.Text = LOGIN.NombreUsuario;
			CargarGrilla();


			formOriginalSize = this.Size;
			RecTitulo = new Rectangle(lblTitulo.Location.X, lblTitulo.Location.Y, lblTitulo.Width, lblTitulo.Height);
			RecVolver = new Rectangle(lblVolver.Location.X, lblVolver.Location.Y, lblVolver.Width, lblVolver.Height);
			ReclblUsuario = new Rectangle(lblUsuario.Location.X, lblUsuario.Location.Y, lblUsuario.Width, lblUsuario.Height);
			ReclblUsuario2 = new Rectangle(lblUsuario2.Location.X, lblUsuario2.Location.Y, lblUsuario2.Width, lblUsuario2.Height);
			RectxtUsuario = new Rectangle(txtUsuario.Location.X, txtUsuario.Location.Y, txtUsuario.Width, txtUsuario.Height);
			RecbtnSubir = new Rectangle(btnSubir.Location.X, btnSubir.Location.Y, btnSubir.Width, btnSubir.Height);
			ReclblEstado = new Rectangle(lblEstado.Location.X, lblEstado.Location.Y, lblEstado.Width, lblEstado.Height);
			ReccmbEstado = new Rectangle(cmbEstado.Location.X, cmbEstado.Location.Y, cmbEstado.Width, cmbEstado.Height);

			RecBtnBajar = new Rectangle(btnBajar.Location.X, btnBajar.Location.Y, btnBajar.Width, btnBajar.Height);
			RecdgvPersonal = new Rectangle(dgvPersonal.Location.X, dgvPersonal.Location.Y, dgvPersonal.Width, dgvPersonal.Height);
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

		private void PERSONAL_Resize(object sender, EventArgs e)
		{
			resizeControl(RecTitulo, lblTitulo);
			resizeControl(RecVolver, lblVolver);
			resizeControl(ReclblUsuario, lblUsuario);
			resizeControl(ReclblUsuario2, lblUsuario2);
			resizeControl(RectxtUsuario, txtUsuario);
			resizeControl(RecbtnSubir, btnSubir);
			resizeControl(ReclblEstado, lblEstado);
			resizeControl(ReccmbEstado, cmbEstado);
			resizeControl(RecBtnBajar, btnBajar);
			resizeControl(RecdgvPersonal, dgvPersonal);
		}
	}
}
