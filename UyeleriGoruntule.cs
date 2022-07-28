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
    public partial class UyeleriGoruntule : Form
    {
        public UyeleriGoruntule()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-AMBCH9N\SQLEXPRESS;Initial Catalog=uyeBeeFit;Integrated Security=True");
        
        private void uyeler()
        {
            baglanti.Open();
            string query = "select * from Uyeler";
            SqlDataAdapter sda = new SqlDataAdapter(query,baglanti); 
            SqlCommandBuilder builder = new SqlCommandBuilder(); 
            var ds=new DataSet(); 
            sda.Fill(ds);   
            UyeDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UyeleriGoruntule_Load(object sender, EventArgs e)
        {
            uyeler();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }
        public void AdFiltrele()
        {
            baglanti.Open();
            string query = "select *from Uyeler where UyeAdSoyad='" + AraUyeTb.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            UyeDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            AdFiltrele();
            AraUyeTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uyeler();
        }

        private void AraUyeTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }
    }
}
