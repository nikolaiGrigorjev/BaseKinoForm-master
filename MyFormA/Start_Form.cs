using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFormA
{
    class Start_Form : System.Windows.Forms.Form
    {
        public Start_Form()
        {
            
            this.Height = 1200;
            this.Width = 1250;
            this.BackColor=Color.Orange;

            Button st_btn = new Button
            {
                Text = "Venom",
                
                Location = new System.Drawing.Point(700, 600)
            };
            Button AdminPanel = new Button
            {
                Location = new System.Drawing.Point(450, 575),
                Size = new System.Drawing.Size(150, 60),
                BackColor = Color.LightBlue,
                Text = "AdminPanel"
            };
            AdminPanel.Click += AdminPanel_Click;
            this.Controls.Add(AdminPanel);
            st_btn.Click += St_btn_Click;
            this.Controls.Add(st_btn);
            Button st_btn2 = new Button()
            {
                Text = "Titanik",
                
                Location = new System.Drawing.Point(200,600)
                
            };
            st_btn2.Click += St_btn_Click1; 
            this.Controls.Add(st_btn2);


            PictureBox film = new PictureBox
            {
                Image = Image.FromFile(@"..\..\Filmid\titinik.jpg"),
                Location = new System.Drawing.Point(10, 100),
                Size = new System.Drawing.Size(500,500),
                SizeMode = PictureBoxSizeMode.Zoom

            };
            this.Controls.Add(film);
            film.Click += Film_Click;


            PictureBox film2 = new PictureBox
            {
                Image = Image.FromFile(@"..\..\Filmid\venom.jpg"),
                Location = new System.Drawing.Point(500, 100),
                Size = new System.Drawing.Size(500, 500),
                SizeMode = PictureBoxSizeMode.Zoom

            };
            this.Controls.Add(film2);
            film2.Click += Film_Click1;
        }

        private void AdminPanel_Click(object sender, EventArgs e)
        {
            Admin_Form uus_aken = new Admin_Form();
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }

        string filminimetus;

        
        private void Film_Click(object sender, EventArgs e)
        {
            filminimetus = Film();
        }
        string filminimetus2;
        private void Film_Click1(object sender, EventArgs e)
        {
            filminimetus2 = Film2();
        }
        private string Film()
        {
            
            filminimetus = "Titanik";
            return filminimetus;

        }
        private string Film2()
        {   
            
            filminimetus2 = "Venom";
            return filminimetus2;

        }
        private void St_btn_Click1(object sender, EventArgs e)
        {
                FormAgain uus_aken = new FormAgain(5, 3, filminimetus);
                uus_aken.StartPosition = FormStartPosition.CenterScreen;
                uus_aken.ShowDialog();
            
        }
        private void St_btn_Click(object sender, EventArgs e)
        {
            FormAgain2 uus_aken = new FormAgain2(8, 5, filminimetus2);


            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }
    }
}