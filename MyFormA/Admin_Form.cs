﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFormA
{
    class Admin_Form : System.Windows.Forms.Form
    {
        static string conn_KinoDB = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\finalForm4\MyFormA\AppData\Kino_DB.mdf;Integrated Security=True";
        SqlConnection connect_to_DB = new SqlConnection(conn_KinoDB);

        SqlCommand command;
        SqlDataAdapter adapter;
        Button film_uuenda,film_naita,film_insert;
        public Admin_Form()
        {
            this.Size = new System.Drawing.Size(1800, 1400);
            /*Button pilet_naita = new Button
            {
                Location = new System.Drawing.Point(50, 50),
                Size = new System.Drawing.Size(80, 25),
                Text = "Ostetud \npiletid"
            };
            this.Controls.Add(pilet_naita);
            pilet_naita.Click += Pilet_naita_Click;*/
            film_naita = new Button
            {
                Location = new System.Drawing.Point(500, 180),
                Size = new System.Drawing.Size(150, 225),
                Text = "Näita filmid"
            };
            this.Controls.Add(film_naita);
            film_naita.Click += Film_naita_Click;

            film_insert = new Button
            {
                Location = new System.Drawing.Point(700, 250),
                Size = new System.Drawing.Size(150, 225),
                Text = "Insert"
            };
            this.Controls.Add(film_insert);
            film_insert.Click += Film_insert_Click; ;

            film_uuenda = new Button
            {
                Location = new System.Drawing.Point(600, 75),
                Size = new System.Drawing.Size(200, 425),
                Text = "Uuendamine",
                Visible = false

            };
            this.Controls.Add(film_uuenda);
            film_uuenda.Click += Film_uuenda_Click;
        }
        int Id;
        private void Film_insert_Click(object sender, EventArgs e)
        {

            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "" && poster.Image != null)
            {
                connect_to_DB.Open();
                command = new SqlCommand("INSERT INTO Filmid VALUES Film Film=@film,Aasta=@aasta,Poster=@poster WHERE Id=@id", connect_to_DB);

                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@film", film_txt.Text);
                command.Parameters.AddWithValue("@aasta", aasta_txt.Text);
                command.Parameters.AddWithValue("@poster", poster_txt.Text);
                //string file_pilt = poster_txt.Text + ".jpg";
                //command.Parameters.AddWithValue("@poster", file_pilt);
                command.ExecuteNonQuery();
                connect_to_DB.Close();
                ClearData();
                Data();
                MessageBox.Show("Andmed uuendatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }
        }

        
        private void Film_uuenda_Click(object sender, EventArgs e)
        {


            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "" && poster.Image != null)
            {
                connect_to_DB.Open();
                command = new SqlCommand("UPDATE Filmid SET Film Film=@film,Aasta=@aasta,Poster=@poster WHERE Id=@id", connect_to_DB);

                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@film", film_txt.Text);
                command.Parameters.AddWithValue("@aasta", aasta_txt.Text);
                command.Parameters.AddWithValue("@poster", poster_txt.Text);
                //string file_pilt = poster_txt.Text + ".jpg";
                //command.Parameters.AddWithValue("@poster", file_pilt);
                command.ExecuteNonQuery();
                connect_to_DB.Close();
                ClearData();
                Data();
                MessageBox.Show("Andmed uuendatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }

        }

        private void Pilet_naita_Click(object sender, EventArgs e)
        {
            connect_to_DB.Open();
            DataTable tabel_p = new DataTable();
            DataGridView dataGridView_p = new DataGridView();
            DataSet dataset_p = new DataSet();
            SqlDataAdapter adapter_p = new SqlDataAdapter("SELECT Rida,Koht,id_Film FROM [dbo].[Piletid]; SELECT Filmi_nimetus FROM [dbo].[Filmid]", connect_to_DB);

            
            //adapter_p.TableMappings.Add("Piletid", "Rida");
            //adapter_p.TableMappings.Add("Filmid", "Filmi_nimetus");
            //adapter_p.Fill(dataset_p);
            adapter_p.Fill(tabel_p);
            dataGridView_p.DataSource = tabel_p;
            dataGridView_p.Location = new System.Drawing.Point(10, 75);
            dataGridView_p.Size = new System.Drawing.Size(400, 200);


            SqlDataAdapter adapter_f = new SqlDataAdapter("SELECT Film FROM [dbo].[Filmid]", connect_to_DB);
            DataTable tabel_f = new DataTable();
            DataSet dataset_f = new DataSet();
            adapter_f.Fill(tabel_f);
            /*fkc = new ForeignKeyConstraint(tabel_f.Columns["Id"], tabel_p.Columns["Film_Id"]);
            tabel_p.Constraints.Add(fkc);*/


            DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
            ComboBox com_f = new ComboBox();
            foreach (DataRow row in tabel_f.Rows)
            {
                com_f.Items.Add(row["Film"]);
                cbc.Items.Add(row["Film"]);
            }
            cbc.Value = com_f;
            connect_to_DB.Close();
            this.Controls.Add(dataGridView_p);
            this.Controls.Add(com_f);
        }


        TextBox film_txt, aasta_txt, poster_txt;
        PictureBox poster;
        DataGridView dataGridView;
        private void Film_naita_Click(object sender, EventArgs e)
        {
            film_naita.Visible = false;
            film_uuenda.Visible = true;
            film_insert.Visible = true;
            film_txt = new TextBox
            { Location = new System.Drawing.Point(450, 75) };
            aasta_txt = new TextBox
            { Location = new System.Drawing.Point(450, 100) };
            poster_txt = new TextBox
            { Location = new System.Drawing.Point(450, 125) };
            poster = new PictureBox
            {
                Size = new System.Drawing.Size(180, 250),
                Location = new System.Drawing.Point(450, 150)

            };

            Data();
            this.Controls.Add(dataGridView);
            this.Controls.Add(film_txt);
            this.Controls.Add(aasta_txt);
            this.Controls.Add(poster_txt);
            this.Controls.Add(poster);

        }
        public void Data()
        {
            connect_to_DB.Open();
            DataTable tabel = new DataTable();
            dataGridView = new DataGridView();
            dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [dbo].[Filmid]", connect_to_DB);//, Kategooria WHERE Toodetable.Kategooria_Id=Kategooria.Id
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;
            dataGridView.Location = new System.Drawing.Point(10, 75);
            dataGridView.Size = new System.Drawing.Size(400, 200);
            connect_to_DB.Close();
        }
        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            film_txt.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            aasta_txt.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            poster_txt.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            poster.Image = Image.FromFile(@"..\..\Posterid\" + dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
            //string v = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
            //comboBox1.SelectedIndex = Int32.Parse(v) - 1;
        }
        private void ClearData()
        {
            //Id = 0;
            film_txt.Text = "";
            aasta_txt.Text = "";
            poster_txt.Text = "";
            //save.FileName = "";
            poster.Image = Image.FromFile("../../Posterid/Start.jpg");

        }
    }
}

