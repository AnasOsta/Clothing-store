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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection conn = null;
        OleDbCommand cmd = null;
        Form3 f2 = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb;");
            this.Size = new System.Drawing.Size(480, 290);
            userControl11.Hide();
            button2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from kullanici where adi=@adi and sifre=@sifre", conn);
            da.SelectCommand.Parameters.AddWithValue("adi", kullaniciAdi.Text);
            da.SelectCommand.Parameters.AddWithValue("sifre", sifre.Text);

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Giriş yapıldı...");
                groupBox1.Hide();
                userControl11.Show();
                this.Size = new System.Drawing.Size(790, 550);

                int yetki = Convert.ToInt32(dt.Rows[0]["yetkino"].ToString());


                if (yetki > 0)
                        button2.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı/Şifre...");
            }

            int kullaniciNo = Convert.ToInt32(dt.Rows[0]["kullanicino"].ToString());
            string query = "INSERT INTO log(kullanicino, tarih) VALUES(@kullanicino, @tarih)";

            

            //insert into ogrenci Start
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@kullanicino", kullaniciNo);
            cmd.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            f2 = new Form3();
            f2.Show();
        }
    }
}
