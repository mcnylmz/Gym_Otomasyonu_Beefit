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
    public partial class GuncelleSil : Form
    {
        public GuncelleSil()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-AMBCH9N\SQLEXPRESS;Initial Catalog=uyeBeeFit;Integrated Security=True");

        private void uyeler()
        {
            baglanti.Open();
            string query = "select * from Uyeler";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            UyeDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }

            private void GuncelleSil_Load(object sender, EventArgs e)
        {
                uyeler();
        }
        int key=0;

        private void UyeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            key = Convert.ToInt32(UyeDGV.SelectedRows[0].Cells[0].Value.ToString());
            AdSoyadTb.Text = UyeDGV.SelectedRows[0].Cells[1].Value.ToString();
            TelefonTb.Text = UyeDGV.SelectedRows[0].Cells[2].Value.ToString();
            CinsiyetCb.Text = UyeDGV.SelectedRows[0].Cells[3].Value.ToString();
            YasTb.Text = UyeDGV.SelectedRows[0].Cells[4].Value.ToString();
            OdemeTb.Text = UyeDGV.SelectedRows[0].Cells[5].Value.ToString();
            ZamanlamaCb.Text = UyeDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa=new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Silinecek üyeyi seçiniz");

            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "delete from Uyeler where UyeID="+key+";"; 
                    SqlCommand komut=new SqlCommand(query,baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Üye başarıyla silindi");
                    baglanti.Close();
                    uyeler();
                    }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (key == 0 || AdSoyadTb.Text == ""|| TelefonTb.Text==""||CinsiyetCb.Text==""||YasTb.Text==""||OdemeTb.Text==""||ZamanlamaCb.Text=="")
            {
                MessageBox.Show("Eksik Bilgi");

            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "update Uyeler set UyeAdSoyad='"+AdSoyadTb.Text+"',UyeTelefon='"+TelefonTb.Text+"',UyeCinsiyet='"+CinsiyetCb.Text+"',UyeYas='"+YasTb.Text+"',UyeOdeme='"+OdemeTb.Text+"',UyeZamanlama='"+ZamanlamaCb.Text+"' where UyeID="+key+";";
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Üye başarıyla güncellendi");
                    baglanti.Close();
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }

        private void AdSoyadTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
               && !char.IsSeparator(e.KeyChar);
        }

        private void TelefonTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void YasTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void OdemeTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
