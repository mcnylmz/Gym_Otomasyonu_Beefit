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

namespace BeeFit
{
    public partial class Odeme : Form
    {
        public Odeme()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-AMBCH9N\SQLEXPRESS;Initial Catalog=uyeBeeFit;Integrated Security=True");
        public void Odeme_Load(object sender, EventArgs e)
        {
            FillName();
            uyeler();
        }

        public void FillName()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select UyeAdSoyad from Uyeler", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader(); 
            DataTable dt = new DataTable(); 
            dt.Columns.Add("UyeAdSoyad",typeof(string));
            dt.Load(rdr);
            AdSoyadCb.ValueMember = "UyeAdSoyad"; 
            AdSoyadCb.DataSource = dt;
            baglanti.Close(); 

        }
        public void AdFiltrele()
        {
            baglanti.Open();
            string query = "select *from Odemeler where OdemeUye='"+AraTb.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            OdemeDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }
        public void uyeler()
        {
            baglanti.Open();
            string query = "select *from Odemeler";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            OdemeDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdSoyadCb.Text = "";
            OdemeTb.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Show();
                  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (AdSoyadCb.Text==""||OdemeTb.Text=="")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                string odemeperiyot = Periyot.Value.Day.ToString()+"/"+Periyot.Value.Month.ToString()+ "/" + Periyot.Value.Year.ToString();
                baglanti.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Odemeler where OdemeUye='" + AdSoyadCb.SelectedValue.ToString() + "'and OdemeAy='" + odemeperiyot + "'", baglanti);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString()=="1")
                {
                    MessageBox.Show("Zaten Ödeme Yapıldı");
                }
                else
                {
                    string query = "insert into Odemeler values('" + odemeperiyot + "','" + AdSoyadCb.SelectedValue.ToString() + "'," + OdemeTb.Text + ")"; 
                    SqlCommand komut=new SqlCommand(query,baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Tutar Başarıyla Ödendi"); 

                }
                baglanti.Close();
                uyeler();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdFiltrele();
            AraTb.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            uyeler();
        }

        private void OdemeTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
