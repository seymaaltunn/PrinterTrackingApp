using PrinterTrackingApp.Business.Services;

namespace PrinterTrackingApp.UI.Forms
{
    public partial class UyariForm : Form
    {
        private readonly DashboardService _dashboardService = new();

        public UyariForm()
        {
            InitializeComponent();
        }

        private void UyariForm_Load(object sender, EventArgs e)
        {
            numKritikEsik.Value = DashboardService.VarsayilanKritikEsik;
            UyarilariYukle();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            UyarilariYukle();
        }

        private void UyarilariYukle()
        {
            try
            {
                int esik = Convert.ToInt32(numKritikEsik.Value);
                var uyarilar = _dashboardService.UyarilariGetir(esik);

                dgvUyarilar.DataSource = null;
                dgvUyarilar.DataSource = uyarilar;

                if (dgvUyarilar.Columns["YaziciId"] != null)
                    dgvUyarilar.Columns["YaziciId"].Visible = false;

                if (dgvUyarilar.Columns["IpAdresi"] != null)
                    dgvUyarilar.Columns["IpAdresi"].HeaderText = "IP Adresi";
                if (dgvUyarilar.Columns["MarkaModel"] != null)
                    dgvUyarilar.Columns["MarkaModel"].HeaderText = "Marka / Model";
                if (dgvUyarilar.Columns["Kategori"] != null)
                    dgvUyarilar.Columns["Kategori"].HeaderText = "Kategori";
                if (dgvUyarilar.Columns["Detay"] != null)
                    dgvUyarilar.Columns["Detay"].HeaderText = "Uyarı";
                if (dgvUyarilar.Columns["SeviyeYuzde"] != null)
                    dgvUyarilar.Columns["SeviyeYuzde"].HeaderText = "Seviye %";
                if (dgvUyarilar.Columns["OlcumZamani"] != null)
                    dgvUyarilar.Columns["OlcumZamani"].HeaderText = "Ölçüm Zamanı";

                lblSonuc.Text = uyarilar.Count == 0
                    ? "Kritik uyarı bulunmuyor."
                    : $"Toplam {uyarilar.Count} kritik uyarı var.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Uyarılar alınırken hata oluştu:\n\n{ex.Message}",
                    "Uyarı Paneli",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
