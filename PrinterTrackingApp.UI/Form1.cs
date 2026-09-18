using PrinterTrackingApp.Business.Services;
using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.Core.Interfaces;
using PrinterTrackingApp.UI.Forms;

namespace PrinterTrackingApp.UI
{
    public partial class Form1 : Form
    {
        private readonly FakePrinterMonitor _fakePrinterMonitor = new();
        private readonly DashboardService _dashboardService = new();
        private readonly System.Windows.Forms.Timer _otomatikOlcumTimer = new();
        private bool _topluOlcumCalisiyor;

        public Form1()
        {
            InitializeComponent();

            _otomatikOlcumTimer.Tick += OtomatikOlcumTimer_Tick;
            _otomatikOlcumTimer.Interval = 30 * 60 * 1000;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numOlcumDakika.Value = 30;
            YazicilariListele();
            DashboardYenile();

            TopluAktarButonuEkle();
            YedekButonuEkle();
        }

        private void TopluAktarButonuEkle()
        {
            if (Controls.Find("btnTopluAktar", true).Length > 0) return;
            var btn = new Button
            {
                Name = "btnTopluAktar",
                Text = "CSV'den Toplu Ekle",
                Location = new Point(28, 150),
                Size = new Size(205, 34),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(31, 78, 120),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += BtnTopluAktar_Click;
            Controls.Add(btn);
            btn.BringToFront();
        }

        private void YedekButonuEkle()
        {
            if (Controls.Find("btnVeritabaniYedekle", true).Length > 0) return;

            var btn = new Button
            {
                Name = "btnVeritabaniYedekle",
                Text = "Veritabanını Yedekle",
                Location = new Point(260, 150),
                Size = new Size(215, 34),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(88, 96, 105),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold)
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.Click += BtnVeritabaniYedekle_Click;
            Controls.Add(btn);
            btn.BringToFront();
        }

        private void BtnVeritabaniYedekle_Click(object? sender, EventArgs e)
        {
            var service = new DatabaseBackupService();

            using var dialog = new FolderBrowserDialog
            {
                Description = "SQLite yedeğinin kaydedileceği klasörü seçin",
                SelectedPath = service.VarsayilanYedekKlasoru(),
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                string yol = service.YedekAl(dialog.SelectedPath);
                new IslemLogService().Kaydet(
                    "VERITABANI_YEDEK",
                    $"SQLite veritabanı yedeği oluşturuldu: {yol}",
                    true);

                MessageBox.Show(
                    $"Veritabanı başarıyla yedeklendi.\n\n{yol}",
                    "Yedekleme Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                new IslemLogService().Kaydet(
                    "VERITABANI_YEDEK_HATA",
                    "SQLite veritabanı yedeklenemedi.",
                    false,
                    hataMesaji: ex.Message);

                MessageBox.Show(
                    $"Veritabanı yedeklenirken hata oluştu:\n\n{ex.Message}",
                    "Yedekleme Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnTopluAktar_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "CSV dosyası (*.csv)|*.csv|Tüm dosyalar (*.*)|*.*",
                Title = "Yazıcı listesini seçin"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var sonuc = new TopluYaziciImportService().CsvAktar(dialog.FileName);
                YazicilariListele();
                DashboardYenile();
                string detay = string.Join("\n", sonuc.Mesajlar.Take(12));
                if (sonuc.Mesajlar.Count > 12) detay += $"\n... ve {sonuc.Mesajlar.Count - 12} satır daha";
                MessageBox.Show($"Toplu aktarım tamamlandı.\n\nEklenen: {sonuc.Eklendi}\nAtlanan: {sonuc.Atlandi}\nHatalı: {sonuc.Hatali}\n\n{detay}", "CSV Toplu Aktarım");
            }
            catch (Exception ex) { MessageBox.Show($"CSV aktarımı başarısız:\n\n{ex.Message}", "Hata"); }
        }

        private void btnYaziciEkle_Click(object sender, EventArgs e)
        {
            using var form = new YaziciEkleForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                YazicilariListele();
                DashboardYenile();
            }
        }

        private void YazicilariListele()
        {
            var yaziciService = new YaziciService();
            dgvYazicilar.DataSource = null;
            dgvYazicilar.DataSource = yaziciService.TumYazicilariGetir();
            SecimKolonuEkle();
            GridAyarlariUygula();
        }

        private void SecimKolonuEkle()
        {
            if (dgvYazicilar.Columns["Sec"] != null) return;

            var kolon = new DataGridViewCheckBoxColumn
            {
                Name = "Sec",
                HeaderText = "Seç",
                Width = 50,
                MinimumWidth = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ReadOnly = false,
                ThreeState = false,
                DisplayIndex = 0
            };

            dgvYazicilar.Columns.Insert(0, kolon);
        }

        private void GridAyarlariUygula()
        {
            foreach (DataGridViewColumn kolon in dgvYazicilar.Columns)
                kolon.ReadOnly = kolon.Name != "Sec";

            Gizle("Id");
            Gizle("GorselYolu");
            Gizle("SnmpCommunity");
            Gizle("SnmpVersion");

            Baslik("IpAdresi", "IP Adresi");
            Baslik("RenkliMi", "Renkli");
            Baslik("BaskiTeknolojisi", "Baskı Teknolojisi");
            Baslik("FotokopiVarMi", "Fotokopi");
            Baslik("OlusturmaTarihi", "Oluşturma Tarihi");
            Baslik("AktifMi", "Aktif");
            Baslik("SnmpAktifMi", "SNMP Aktif");
            Baslik("SonBasariliIletisim", "Son Başarılı İletişim");
        }

        private void Gizle(string kolonAdi)
        {
            if (dgvYazicilar.Columns[kolonAdi] != null)
                dgvYazicilar.Columns[kolonAdi].Visible = false;
        }

        private void Baslik(string kolonAdi, string baslik)
        {
            if (dgvYazicilar.Columns[kolonAdi] != null)
                dgvYazicilar.Columns[kolonAdi].HeaderText = baslik;
        }

        private void btnYaziciSil_Click(object sender, EventArgs e)
        {
            var yazici = SeciliYaziciyiGetir();
            if (yazici == null) return;

            if (MessageBox.Show(
                    $"{yazici.IpAdresi} adresli yazıcı silinsin mi?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                new YaziciService().YaziciSil(yazici.Id);
                YazicilariListele();
                DashboardYenile();
                MessageBox.Show("Yazıcı başarıyla silindi.", "Başarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yazıcı silinirken hata oluştu:\n\n{ex.Message}", "Hata");
            }
        }

        private void btnYaziciDuzenle_Click(object sender, EventArgs e)
        {
            var yazici = SeciliYaziciyiGetir();
            if (yazici == null) return;

            using var form = new YaziciEkleForm(yazici);
            if (form.ShowDialog() == DialogResult.OK)
            {
                YazicilariListele();
                DashboardYenile();
            }
        }

        private void btnSayacOlcumuAl_Click(object sender, EventArgs e)
        {
            var yazici = SeciliYaziciyiGetir();
            if (yazici == null) return;

            try
            {
                IPrinterMonitor monitor;

                if (chkGercekSnmp.Checked)
                {
                    if (!yazici.SnmpAktifMi)
                    {
                        MessageBox.Show("Bu yazıcı için SNMP pasif durumda.", "SNMP");
                        return;
                    }

                    monitor = new SnmpPrinterMonitor(yazici.SnmpCommunity, yazici.SnmpVersion);
                }
                else
                {
                    monitor = _fakePrinterMonitor;
                }

                var olcumService = new YaziciOlcumService(monitor);
                var olcum = olcumService.OlcumAl(yazici);
                var snapshot = olcumService.SonSnapshot;

                string tonerBilgisi = snapshot?.SiyahTonerYuzde.HasValue == true
                    ? $"%{snapshot.SiyahTonerYuzde.Value}"
                    : "Desteklenmiyor / okunamadı";

                string drumBilgisi = snapshot?.DrumYuzde.HasValue == true
                    ? $"%{snapshot.DrumYuzde.Value}"
                    : "Desteklenmiyor / okunamadı";

                MessageBox.Show(
                    $"Kaynak: {olcum.VeriKaynagi}\n" +
                    $"Başarılı: {(olcum.OlcumBasariliMi ? "Evet" : "Hayır")}\n" +
                    $"Toplam Sayaç: {(olcum.ToplamSayac?.ToString() ?? "Okunamadı")}\n" +
                    $"Günlük: {(olcum.GunlukBaski?.ToString() ?? "-")}\n" +
                    $"Siyah Toner: {tonerBilgisi}\n" +
                    $"Drum: {drumBilgisi}\n" +
                    $"Zaman: {olcum.OlcumZamani:dd.MM.yyyy HH:mm:ss}\n" +
                    (string.IsNullOrWhiteSpace(olcum.HataMesaji) ? "" : $"\nHata: {olcum.HataMesaji}"),
                    "Yazıcı Ölçümü",
                    MessageBoxButtons.OK,
                    olcum.OlcumBasariliMi ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                YazicilariListele();
                DashboardYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ölçüm sırasında hata oluştu:\n\n{ex.Message}", "Hata");
            }
        }

        private async void btnTumunuOlc_Click(object sender, EventArgs e)
        {
            await TopluOlcumuBaslatAsync(false);
        }

        private async Task TopluOlcumuBaslatAsync(bool sessiz)
        {
            if (_topluOlcumCalisiyor)
                return;

            _topluOlcumCalisiyor = true;
            bool gercekSnmp = chkGercekSnmp.Checked;

            btnTumunuOlc.Enabled = false;
            btnSayacOlcumuAl.Enabled = false;
            progressOlcum.Value = 0;
            lblOlcumDurumu.Text = sessiz
                ? "Otomatik ölçüm çalışıyor..."
                : "Toplu ölçüm hazırlanıyor...";

            try
            {
                var service = new TopluOlcumService();

                var sonuc = await Task.Run(() => service.TumAktifYazicilariOlc(
                    gercekSnmp,
                    _fakePrinterMonitor,
                    (mevcut, toplam, ip) =>
                    {
                        BeginInvoke(new Action(() =>
                        {
                            progressOlcum.Maximum = Math.Max(1, toplam);
                            progressOlcum.Value = Math.Min(mevcut, progressOlcum.Maximum);
                            lblOlcumDurumu.Text = $"{mevcut}/{toplam} - {ip}";
                        }));
                    }));

                lblOlcumDurumu.Text =
                    $"Tamamlandı. Başarılı: {sonuc.Basarili}, Başarısız: {sonuc.Basarisiz}, Atlanan: {sonuc.Atlanan}";

                YazicilariListele();
                DashboardYenile();

                if (!sessiz)
                {
                    MessageBox.Show(
                        $"Toplu ölçüm tamamlandı.\n\n" +
                        $"Toplam: {sonuc.ToplamYazici}\n" +
                        $"Başarılı: {sonuc.Basarili}\n" +
                        $"Başarısız: {sonuc.Basarisiz}\n" +
                        $"Atlanan: {sonuc.Atlanan}\n" +
                        $"Süre: {sonuc.Sure.TotalSeconds:N1} saniye",
                        "Toplu Ölçüm",
                        MessageBoxButtons.OK,
                        sonuc.Basarisiz == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                lblOlcumDurumu.Text = "Toplu ölçüm sırasında hata oluştu.";

                if (!sessiz)
                {
                    MessageBox.Show(
                        $"Toplu ölçüm sırasında hata oluştu:\n\n{ex.Message}",
                        "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                _topluOlcumCalisiyor = false;
                btnTumunuOlc.Enabled = true;
                btnSayacOlcumuAl.Enabled = true;
            }
        }

        private void DashboardYenile()
        {
            try
            {
                var durum = _dashboardService.DurumuGetir();

                lblAktifYazici.Text = $"Aktif Yazıcı\n{durum.AktifYaziciSayisi}";
                lblKritikToner.Text = $"Kritik Toner\n{durum.KritikTonerSayisi}";
                lblKritikDrum.Text = $"Kritik Drum\n{durum.KritikDrumSayisi}";
                lblBasarisizOlcum.Text = $"Son Ölçüm Başarısız\n{durum.SonOlcumuBasarisizSayisi}";
                lblSonOlcum.Text = durum.SonOlcumZamani.HasValue
                    ? $"Son ölçüm: {durum.SonOlcumZamani:dd.MM.yyyy HH:mm:ss}"
                    : "Henüz ölçüm yok.";

                btnUyarilar.Text = durum.ToplamUyari > 0
                    ? $"Kritik Uyarılar ({durum.ToplamUyari})"
                    : "Kritik Uyarılar";
            }
            catch (Exception ex)
            {
                lblSonOlcum.Text = $"Dashboard okunamadı: {ex.Message}";
            }
        }

        private void chkOtomatikOlcum_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtomatikOlcum.Checked)
            {
                int dakika = Convert.ToInt32(numOlcumDakika.Value);
                _otomatikOlcumTimer.Interval = dakika * 60 * 1000;
                _otomatikOlcumTimer.Start();
                lblOtomatikDurum.Text = $"Otomatik ölçüm açık ({dakika} dk).";
            }
            else
            {
                _otomatikOlcumTimer.Stop();
                lblOtomatikDurum.Text = "Otomatik ölçüm kapalı.";
            }
        }

        private void numOlcumDakika_ValueChanged(object sender, EventArgs e)
        {
            if (!chkOtomatikOlcum.Checked)
                return;

            int dakika = Convert.ToInt32(numOlcumDakika.Value);
            _otomatikOlcumTimer.Interval = dakika * 60 * 1000;
            lblOtomatikDurum.Text = $"Otomatik ölçüm açık ({dakika} dk).";
        }

        private async void OtomatikOlcumTimer_Tick(object? sender, EventArgs e)
        {
            await TopluOlcumuBaslatAsync(true);
        }

        private void btnLogPaneli_Click(object sender, EventArgs e)
        {
            using var form = new LogForm();
            form.ShowDialog(this);
            DashboardYenile();
        }

        private void btnUyarilar_Click(object sender, EventArgs e)
        {
            using var form = new UyariForm();
            form.ShowDialog(this);
            DashboardYenile();
        }

        private Yazici? SeciliYaziciyiGetir()
        {
            if (dgvYazicilar.CurrentRow?.DataBoundItem is Yazici yazici)
                return yazici;

            MessageBox.Show(
                "Lütfen bir yazıcı seçin.",
                "Uyarı",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return null;
        }
    }
}
