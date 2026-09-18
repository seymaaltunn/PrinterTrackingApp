using PrinterTrackingApp.Business.Services;

namespace PrinterTrackingApp.UI.Forms
{
    public partial class LogForm : Form
    {
        private readonly LogService _logService = new();

        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            dtpBaslangic.Value = DateTime.Today.AddDays(-30);
            dtpBitis.Value = DateTime.Today;
            LoglariYenile();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            LoglariYenile();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtIpAdresi.Clear();
            dtpBaslangic.Value = DateTime.Today.AddDays(-30);
            dtpBitis.Value = DateTime.Today;
            LoglariYenile();
        }

        private void LoglariYenile()
        {
            try
            {
                string? ip = string.IsNullOrWhiteSpace(txtIpAdresi.Text)
                    ? null
                    : txtIpAdresi.Text.Trim();

                DateTime baslangic = dtpBaslangic.Value.Date;
                DateTime bitis = dtpBitis.Value.Date;

                if (baslangic > bitis)
                {
                    MessageBox.Show("Başlangıç tarihi bitiş tarihinden büyük olamaz.", "Filtre");
                    return;
                }

                dgvSayac.DataSource = _logService.SayacLoglariniGetir(baslangic, bitis, ip);
                dgvToner.DataSource = _logService.TonerLoglariniGetir(baslangic, bitis, ip);
                dgvDrum.DataSource = _logService.DrumLoglariniGetir(baslangic, bitis, ip);
                dgvIslem.DataSource = _logService.IslemLoglariniGetir(baslangic, bitis, ip);

                lblSonuc.Text =
                    $"Sayaç: {dgvSayac.Rows.Count} | Toner: {dgvToner.Rows.Count} | " +
                    $"Drum: {dgvDrum.Rows.Count} | İşlem: {dgvIslem.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Loglar alınırken hata oluştu:\n\n{ex.Message}",
                    "Log Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
