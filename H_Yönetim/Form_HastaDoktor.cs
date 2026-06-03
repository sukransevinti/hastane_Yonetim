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
    public partial class Form_HastaDoktor : Form
    {
        DbHastaneContext db = new DbHastaneContext();
        public Form_HastaDoktor()
        {
            InitializeComponent();
        }

        private void Form_HastaDoktor_Load(object sender, EventArgs e)
        {
            try
            {
                var hastalar = (from h in db.Hastalar
                                select new
                                {
                                    ID = h.HastaID,
                                    AdSoyad = h.HastaAd + " " + h.HastaSoyad
                                }).ToList();

                cbx_hasta.DataSource = hastalar;
                cbx_hasta.DisplayMember = "AdSoyad";
                cbx_hasta.ValueMember = "ID";

                var doktorlar = (from d in db.Doktorlar
                                 select new
                                 {
                                     ID = d.DoktorID,
                                     AdSoyad = d.DoktorAd + " " + d.DoktorSoyad
                                 }).ToList();

                cbx_doktor.DataSource = doktorlar;
                cbx_doktor.DisplayMember = "AdSoyad";
                cbx_doktor.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yükleme Hatası = {ex.Message}");
            }
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
               
                var hastalar = db.Hastalar
                    .OrderBy(h => h.HastaAd)
                    .Select(h => new
                    {
                        h.HastaID,
                        FullName = h.HastaAd + " " + h.HastaSoyad
                    })
                    .ToList();

                cbx_hasta.DataSource = hastalar;
                cbx_hasta.DisplayMember = "FullName";
                cbx_hasta.ValueMember = "HastaID";

                
                var doktorlar = db.Doktorlar
                    .OrderBy(d => d.DoktorAd)
                    .Select(d => new
                    {
                        d.DoktorID,
                        ProductInfo = d.DoktorAd + " " + d.DoktorSoyad
                    })
                    .ToList();

                cbx_doktor.DataSource = doktorlar;
                cbx_doktor.DisplayMember = "ProductInfo";
                cbx_doktor.ValueMember = "DoktorID";

               
                var gridListe = (from h in db.Hastalar
                                 join d in db.Doktorlar on h.Doktor_DoktorID equals d.DoktorID into doktorGrup
                                 from d in doktorGrup.DefaultIfEmpty() 
                                 join b in db.Bolumler on d.DoktorBolum equals b.BolumID into bolumGrup
                                 from b in bolumGrup.DefaultIfEmpty() 
                                 select new
                                 {
                                     Pasaport_ID = h.HastaID,
                                     Hasta_Adı = h.HastaAd,
                                     Hasta_Soyadı = h.HastaSoyad,
                                     TC_Kimlik = h.HastaTC,
                                     Telefon = h.HastaTel,
                                     Atanan_Doktor = d != null ? d.DoktorAd + " " + d.DoktorSoyad : "Doktor Atanmamış",
                                     Doktor_Bölümü = b != null ? b.BolumAd : "Bölüm Yok"
                                 }).ToList();

                dataGridView1.DataSource = gridListe;

               
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

        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dataGridView1.CurrentRow != null)
                {
                    
                    int secilenHastaID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Pasaport_ID"].Value);

                    
                    var hasta = db.Hastalar.FirstOrDefault(h => h.HastaID == secilenHastaID);

                    if (hasta != null)
                    {
                       
                        hasta.Doktor_DoktorID = null;

                        db.SaveChanges();
                        MessageBox.Show("Doktor - Hasta Eşleştirmesi Başarıyla Silindi (Kaldırıldı)!");

                       
                        btn_listele.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt bulunamadı!");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen tablodan silmek istediğiniz satırı seçin!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme Hatası = {ex.Message}");
            }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                int secilenHastaID = Convert.ToInt32(cbx_hasta.SelectedValue);
                int secilenDoktorID = Convert.ToInt32(cbx_doktor.SelectedValue);

                using (DbHastaneContext db = new DbHastaneContext())
                {
                    var hasta = db.Hastalar.FirstOrDefault(h => h.HastaID == secilenHastaID);

                    if (hasta != null)
                    {
                        hasta.Doktor_DoktorID = secilenDoktorID;
                        db.SaveChanges();
                        MessageBox.Show("Doktor - Hasta Eşleştirmesi Başarıyla Eklendi!");
                    }
                    else
                    {
                        MessageBox.Show("Seçilen hasta bulunamadı!");
                    }
                }
                btn_listele.PerformClick();
            }
            catch (Exception ex)
            {
                string anaHata = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show($"Ekleme Hatası = {anaHata}");
            }
        }
    }
}
    


