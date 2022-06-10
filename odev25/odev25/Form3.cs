using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace odev25
{
    public partial class Form3 : Form
    {
        OleDbConnection conn = null;
        OleDbCommand cmd = null;
        public Form3()
        {
            InitializeComponent();
        }

        private void goster()
        {
            //Database start open
            conn.Open();

            //OleDbCommand command = new OleDbCommand("SELECT kullanici.kullanicino, kullanici.adi, kullanici.soyadi, kullanici.cinsiyeti, kullanici.dogumtarihi, kullanici.yetkino, kullanici.sifre, log.tarih FROM kullanici, log", conn);
            OleDbCommand command = new OleDbCommand("SELECT * FROM kullanici", conn);

            // Execute the query and obtain a result set
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());

            dataGridView1.DataSource = dt;
            conn.Close();
            //Database start close
        }
        private void button5_Click(object sender, EventArgs e)
        {
            goster();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb;");
            goster();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "DELETE FROM kullanici WHERE kullanicino=" + id.Text;
            cmd = new OleDbCommand(sql, conn);

            if (cmd.ExecuteNonQuery().ToString() == "1")
                MessageBox.Show("öğrenciyi başarıyla silindi");
            else
                MessageBox.Show("Bir hata oluşturdu");
            conn.Close();
            goster();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ad.Text = dataGridView1.Rows[e.RowIndex].Cells["adi"].Value.ToString();
                soyad.Text = dataGridView1.Rows[e.RowIndex].Cells["soyadi"].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["cinsiyeti"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells["dogumtarihi"].Value.ToString();
                id.Text = dataGridView1.Rows[e.RowIndex].Cells["kullanicino"].Value.ToString();
                
                if(dataGridView1.Rows[e.RowIndex].Cells["yetkino"].Value.ToString() == "0")
                    comboBox2.Text = "Hayır";
                else
                    comboBox2.Text = "Evet";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();

            string sql = "SELECT * FROM kullanici WHERE kullanicino=" + id.Text;
            cmd = new OleDbCommand(sql, conn);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ad.Text == "" || soyad.Text == "" || comboBox2.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Verilerden birini girmeyi unuttun.");
                return;
            }

            conn.Open();
            string query = "INSERT INTO kullanici(adi, soyadi, cinsiyeti, dogumtarihi, yetkino, sifre) VALUES(@adi, @soyadi, @cinsiyeti, @dogumtarihi, @yetkino, @sifre)";

            //insert into ogrenci Start
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@adi", ad.Text);
            cmd.Parameters.AddWithValue("@soyadi", soyad.Text);
            cmd.Parameters.AddWithValue("@cinsiyeti", comboBox1.Text);
            cmd.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Text);
            if (comboBox2.Text == "Gömlek")
                cmd.Parameters.AddWithValue("@yetkino", 4);
            else if(comboBox2.Text == "Çorap")
                cmd.Parameters.AddWithValue("@yetkino", 3);
            else if(comboBox2.Text == "Pantolon")
                cmd.Parameters.AddWithValue("@yetkino", 2);
            else if(comboBox2.Text == "T-Şirt")
                cmd.Parameters.AddWithValue("@yetkino", 1);
            else
                cmd.Parameters.AddWithValue("@yetkino", 0);
            cmd.Parameters.AddWithValue("@sifre", 1234);


            cmd.ExecuteNonQuery();
            //insert into ogrenci End
            MessageBox.Show("Bilgiler başarıyla eklendi");
            conn.Close();
            goster();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "";

            conn.Open();
            if (ad.Text != "")
            {
                query = "UPDATE kullanici SET adi = '" + ad.Text + "' WHERE kullanicino =" + id.Text;
                cmd = new OleDbCommand(query, conn);
            }
            if (soyad.Text != "")
            {
                query = "UPDATE kullanici set soyadi = '" + soyad.Text + "' WHERE kullanicino= " + id.Text;
                cmd = new OleDbCommand(query, conn);
            }
            if (comboBox1.Text != "")
            {
                query = "UPDATE kullanici set cinsiyeti = '" + comboBox1.Text + "' WHERE kullanicino= " + id.Text;
                cmd = new OleDbCommand(query, conn);
            }
            if (comboBox2.Text != "")
            {
                if(comboBox2.Text == "Hayır")
                    query = "UPDATE kullanici set yetkino = '" + "0" + "' WHERE kullanicino= " + id.Text;
                else if (comboBox2.Text == "T-Şirt")
                    query = "UPDATE kullanici set yetkino = '" + "1" + "' WHERE kullanicino= " + id.Text;
                else if (comboBox2.Text == "Pantolon")
                    query = "UPDATE kullanici set yetkino = '" + "2" + "' WHERE kullanicino= " + id.Text;
                else if (comboBox2.Text == "Çorap")
                    query = "UPDATE kullanici set yetkino = '" + "3" + "' WHERE kullanicino= " + id.Text;
                else if (comboBox2.Text == "Gömlek")
                    query = "UPDATE kullanici set yetkino = '" + "4" + "' WHERE kullanicino= " + id.Text;
                cmd = new OleDbCommand(query, conn);
            }
            if (ad.Text == "" && soyad.Text == "" && comboBox1.Text == "" && comboBox2.Text == "")
                MessageBox.Show("Giriş bulunamadı");
            else
                MessageBox.Show("İşlem başarılı oldu.");
            cmd.ExecuteNonQuery();
            conn.Close();
            //goster();
        }
    }
}
