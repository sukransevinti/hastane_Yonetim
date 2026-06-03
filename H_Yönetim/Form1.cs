using H_Yonetim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace H_Yönetim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbHastaneContext db = new DbHastaneContext();
        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbHastaneContext db = new DbHastaneContext())
                {
                    var liste = (from h in db.Hastalar
                                 select new
                                 {
                                     Pasaport_ID = h.HastaID,
                                     Hasta_Adı = h.HastaAd,
                                     Hasta_Soyadı = h.HastaSoyad,
                                     TC_Kimlik = h.HastaTC,
                                     Telefon = h.HastaTel
                                 }).ToList();

                    dataGridView1.DataSource = liste;
                }

                if (dataGridView1.Columns["Pasaport_ID"] != null)
                {
                    dataGridView1.Columns["Pasaport_ID"].Visible = false;
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }



        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbHastaneContext db = new DbHastaneContext())
                {
                    Hasta yeniHasta = new Hasta();

                    yeniHasta.HastaAd = textBox1.Text.Trim();
                    yeniHasta.HastaSoyad = textBox2.Text.Trim();
                    yeniHasta.HastaTC = maskedTextBox1.Text.Replace(" ", "").Replace("-", "");
                    yeniHasta.HastaTel = maskedTextBox2.Text.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

                    
                    yeniHasta.Doktor_DoktorID = null;

                    db.Hastalar.Add(yeniHasta);
                    db.SaveChanges();
                }

                MessageBox.Show("Yeni Hasta Başarıyla Eklendi!");
                btn_listele.PerformClick();
            }
            catch (Exception ex)
            {
                string anaHata = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show($"Ekleme Hatası = {anaHata}");
            }
        }
        


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

               
                textBox1.Text = row.Cells[1].Value?.ToString();
                textBox2.Text = row.Cells[2].Value?.ToString();
                maskedTextBox1.Text = row.Cells[3].Value?.ToString();
                maskedTextBox2.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int secilenHastaID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Pasaport_ID"].Value);

                    using (DbHastaneContext db = new DbHastaneContext())
                    {
                        var guncellenecekHasta = db.Hastalar.FirstOrDefault(h => h.HastaID == secilenHastaID);

                        if (guncellenecekHasta != null)
                        {
                            guncellenecekHasta.HastaAd = textBox1.Text.Trim();
                            guncellenecekHasta.HastaSoyad = textBox2.Text.Trim();
                            guncellenecekHasta.HastaTC = maskedTextBox1.Text.Replace(" ", "").Replace("-", "");
                            guncellenecekHasta.HastaTel = maskedTextBox2.Text.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

                            db.SaveChanges();
                            MessageBox.Show("Hasta Bilgileri Başarıyla Güncellendi!");
                        }
                        else
                        {
                            MessageBox.Show("Güncellenecek hasta kaydı bulunamadı!");
                        }
                    }
                    btn_listele.PerformClick();
                }
                else
                {
                    MessageBox.Show("Lütfen tablodan güncellenecek hastayı seçin!");
                }
            }
            catch (Exception ex)
            {
                string anaHata = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show($"Güncelleme Hatası = {anaHata}");
            }

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int secilenHastaID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Pasaport_ID"].Value);

                    using (DbHastaneContext db = new DbHastaneContext())
                    {
                        var silinecekHasta = db.Hastalar.FirstOrDefault(h => h.HastaID == secilenHastaID);

                        if (silinecekHasta != null)
                        {
                            db.Hastalar.Remove(silinecekHasta);
                            db.SaveChanges();
                            MessageBox.Show("Hasta Kaydı Başarıyla Silindi!");
                        }
                        else
                        {
                            MessageBox.Show("Silinecek hasta kaydı bulunamadı.");
                        }
                    }
                    btn_listele.PerformClick();
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz hastayı tablodan seçin!");
                }
            }
            catch (Exception ex)
            {
                string anaHata = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show($"Silme Hatası = {anaHata}");
            }

        }
           
        

        private void btn_form_hd_Click(object sender, EventArgs e)
        {
            Form_HastaDoktor form2_hd = new Form_HastaDoktor();
            this.Hide();
            form2_hd.ShowDialog();
            this.Close();
        }
    }
}

    

