using PrinterTrackingApp.Business.Services;
using PrinterTrackingApp.Core.Entities;

namespace PrinterTrackingApp.UI.Forms
{
    public partial class YaziciEkleForm : Form
    {
        private readonly Yazici? _duzenlenecekYazici;

        public YaziciEkleForm()
        {
            InitializeComponent();
            cmbSnmpVersion.SelectedItem = "2c";
            txtSnmpCommunity.Text = "public";
            chkSnmpAktifMi.Checked = true;
        }

        public YaziciEkleForm(Yazici yazici)
        {
            InitializeComponent();

            _duzenlenecekYazici = yazici;

            txtIpAdresi.Text = yazici.IpAdresi;
            txtMarka.Text = yazici.Marka;
            txtModel.Text = yazici.Model;
            txtKonum.Text = yazici.Konum;
            txtBaskiTeknolojisi.Text = yazici.BaskiTeknolojisi;

            chkRenkliMi.Checked = yazici.RenkliMi;
            chkFotokopiVarMi.Checked = yazici.FotokopiVarMi;
            chkAktifMi.Checked = yazici.AktifMi;
            chkSnmpAktifMi.Checked = yazici.SnmpAktifMi;
            txtSnmpCommunity.Text = yazici.SnmpCommunity;
            cmbSnmpVersion.SelectedItem = yazici.SnmpVersion;
            if (cmbSnmpVersion.SelectedIndex < 0)
                cmbSnmpVersion.SelectedItem = "2c";

            Text = "Yazıcı Düzenle";
            btnKaydet.Text = "Güncelle";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIpAdresi.Text) ||
                    string.IsNullOrWhiteSpace(txtMarka.Text) ||
                    string.IsNullOrWhiteSpace(txtModel.Text) ||
                    string.IsNullOrWhiteSpace(txtKonum.Text))
                {
                    MessageBox.Show(
                        "IP adresi, marka, model ve konum alanları boş bırakılamaz.",
                        "Eksik Bilgi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                var yazici = new Yazici
                {
                    Id = _duzenlenecekYazici?.Id ?? 0,

                    IpAdresi = txtIpAdresi.Text.Trim(),
                    Marka = txtMarka.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    Konum = txtKonum.Text.Trim(),

                    BaskiTeknolojisi =
                        string.IsNullOrWhiteSpace(txtBaskiTeknolojisi.Text)
                            ? null
                            : txtBaskiTeknolojisi.Text.Trim(),

                    RenkliMi = chkRenkliMi.Checked,
                    FotokopiVarMi = chkFotokopiVarMi.Checked,
                    AktifMi = chkAktifMi.Checked,

                    GorselYolu = _duzenlenecekYazici?.GorselYolu,

                    OlusturmaTarihi =
                        _duzenlenecekYazici?.OlusturmaTarihi ?? DateTime.Now,

                    SnmpAktifMi = chkSnmpAktifMi.Checked,
                    SnmpCommunity = string.IsNullOrWhiteSpace(txtSnmpCommunity.Text)
                        ? "public"
                        : txtSnmpCommunity.Text.Trim(),
                    SnmpVersion = cmbSnmpVersion.SelectedItem?.ToString() ?? "2c",
                    SonBasariliIletisim = _duzenlenecekYazici?.SonBasariliIletisim
                };

                var yaziciService = new YaziciService();

                if (_duzenlenecekYazici == null)
                {
                    yaziciService.YaziciEkle(yazici);

                    MessageBox.Show(
                        "Yazıcı başarıyla kaydedildi.",
                        "Başarılı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    yaziciService.YaziciGuncelle(yazici);

                    MessageBox.Show(
                        "Yazıcı başarıyla güncellendi.",
                        "Başarılı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"İşlem sırasında bir hata oluştu:\n\n{ex.Message}",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void YaziciEkleForm_Load(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }
    }
}
