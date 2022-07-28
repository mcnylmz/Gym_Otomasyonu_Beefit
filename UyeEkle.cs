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
    public partial class UyeEkle : Form
    {
        public UyeEkle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-AMBCH9N\SQLEXPRESS;Initial Catalog=uyeBeeFit;Integrated Security=True");
        

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UyeEkle_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa =new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AdSoyadTb.Text == "" || TelefonTb.Text == "" || CinsiyetCb.Text == "" || YasTb.Text == "" || OdemeTb.Text == "" || ZamanlamaCb.Text == "")
            {
                MessageBox.Show("Gerekli bölümleri boş bıraktınız");
            } 
            
            else
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();

                    string kayityap = ("insert into Uyeler(UyeAdSoyad,UyeTelefon,UyeCinsiyet,UyeYas,UyeOdeme,UyeZamanlama) values (@AdSoyad,@Telefon,@Cinsiyet,@Yas,@Odeme,@Zamanlama)");
                    SqlCommand komut1 = new SqlCommand(kayityap,baglanti);
                    komut1.Parameters.AddWithValue("@AdSoyad", AdSoyadTb.Text);
                    komut1.Parameters.AddWithValue("@Telefon", TelefonTb.Text);
                    komut1.Parameters.AddWithValue("@Cinsiyet", CinsiyetCb.Text);
                    komut1.Parameters.AddWithValue("@Yas", YasTb.Text);
                    komut1.Parameters.AddWithValue("@Odeme", OdemeTb.Text);
                    komut1.Parameters.AddWithValue("@Zamanlama", ZamanlamaCb.Text);
                    komut1.ExecuteNonQuery();

                    MessageBox.Show("Üye Başarıyla Eklendi");

                    AdSoyadTb.Text = "";
                    YasTb.Text = "";
                    TelefonTb.Text = "";
                    OdemeTb.Text = "";
                    ZamanlamaCb.Text = "";
                    CinsiyetCb.Text = "";





                }
                catch (Exception)
                {

                    MessageBox.Show("Veri tabanına bağlanılamadı");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdSoyadTb.Text = ""; 
            TelefonTb.Text = "";
            CinsiyetCb.Text = "";
            YasTb.Text = "";
            OdemeTb.Text = "";
            ZamanlamaCb.Text = "";
            
        }
        
        private void TelefonTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void OdemeTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdSoyadTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
               && !char.IsSeparator(e.KeyChar);
        }

        private void YasTb_KeyPress(object sender, KeyPressEventArgs e)
        {       
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            
        }

        private void OdemeTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void TelefonTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        } 

    }
}
