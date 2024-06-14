﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Final_Blood_Bank
{
    public partial class OPCIONES_2 : Form
    {
       
        
        public OPCIONES_2()
        {
            InitializeComponent();
            LogOut.MouseEnter += OnMouseEnter;
            LogOut.MouseLeave += OnMouseLeave;
            boldUnderlineFontLarge = new Font("Nirmala UI", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            boldUnderlineFontNormal = new Font("Nirmala UI", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
        }
        private Font boldUnderlineFontLarge;
        private Font boldUnderlineFontNormal;
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void OPCIONES_2_Load(object sender, EventArgs e)
        {
            lblNombre.Text = LOGIN.NombreUsuario;

            formOriginalSize = this.Size;
            ReclblTitulo = new Rectangle(lblTitulo.Location.X, lblTitulo.Location.Y, lblTitulo.Width, lblTitulo.Height);
            RecLogOut = new Rectangle(LogOut.Location.X, LogOut.Location.Y, LogOut.Width, LogOut.Height);
            ReclblNombre = new Rectangle(lblNombre.Location.X, lblNombre.Location.Y, lblNombre.Width, lblNombre.Height);
            RecbtnPacientes = new Rectangle(btnPacientes.Location.X, btnPacientes.Location.Y, btnPacientes.Width, btnPacientes.Height);
            RecbtnBanco = new Rectangle(btnBanco.Location.X, btnBanco.Location.Y, btnBanco.Width, btnBanco.Height);
            RecbtnDonantes = new Rectangle(btnDonantes.Location.X, btnDonantes.Location.Y, btnDonantes.Width, btnDonantes.Height);
        }

        private void btnBanco_Click(object sender, EventArgs e)
        {
            new BANCO_DE_SANGRE().Show();
            this.Hide();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            new REGISTRO_PACIENTE().Show();
            this.Hide();
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            new LOGIN().Show();
            this.Hide();
        }
        private void OnMouseEnter(object sender, EventArgs e)
        {
            LogOut.Font = boldUnderlineFontLarge;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            LogOut.Font = boldUnderlineFontNormal;
        }


        private Size formOriginalSize;
        // Resolucion Dinamica
        private Rectangle ReclblTitulo;
        private Rectangle RecLogOut;
        private Rectangle ReclblNombre;
        private Rectangle RecbtnPacientes;
        private Rectangle RecbtnBanco;
        private Rectangle RecbtnDonantes;
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

        private void OPCIONES_2_Resize(object sender, EventArgs e)
        {
            resizeControl(RecbtnPacientes, btnPacientes);
            resizeControl(ReclblNombre, lblNombre);
            resizeControl(RecLogOut, LogOut);
            resizeControl(ReclblTitulo, lblTitulo);
            resizeControl(RecbtnBanco, btnBanco);
            resizeControl(RecbtnDonantes, btnDonantes);
        }

        private void btnDonantes_Click(object sender, EventArgs e)
        {
            new RegistroDonantes().Show();
            this.Hide();
        }
    }
}
