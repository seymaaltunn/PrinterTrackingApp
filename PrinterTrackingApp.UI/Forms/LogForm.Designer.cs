namespace PrinterTrackingApp.UI.Forms
{
    partial class LogForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblIp = new Label();
            txtIpAdresi = new TextBox();
            lblBaslangic = new Label();
            dtpBaslangic = new DateTimePicker();
            lblBitis = new Label();
            dtpBitis = new DateTimePicker();
            btnAra = new Button();
            btnTemizle = new Button();
            lblSonuc = new Label();
            tabLoglar = new TabControl();
            tabSayac = new TabPage();
            dgvSayac = new DataGridView();
            tabToner = new TabPage();
            dgvToner = new DataGridView();
            tabDrum = new TabPage();
            dgvDrum = new DataGridView();
            tabIslem = new TabPage();
            dgvIslem = new DataGridView();
            tabLoglar.SuspendLayout();
            tabSayac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSayac).BeginInit();
            tabToner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvToner).BeginInit();
            tabDrum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDrum).BeginInit();
            tabIslem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIslem).BeginInit();
            SuspendLayout();

            lblIp.AutoSize = true;
            lblIp.Location = new Point(20, 22);
            lblIp.Text = "IP Adresi";

            txtIpAdresi.Location = new Point(95, 18);
            txtIpAdresi.Size = new Size(150, 27);

            lblBaslangic.AutoSize = true;
            lblBaslangic.Location = new Point(270, 22);
            lblBaslangic.Text = "Başlangıç";

            dtpBaslangic.Format = DateTimePickerFormat.Short;
            dtpBaslangic.Location = new Point(345, 18);
            dtpBaslangic.Size = new Size(130, 27);

            lblBitis.AutoSize = true;
            lblBitis.Location = new Point(495, 22);
            lblBitis.Text = "Bitiş";

            dtpBitis.Format = DateTimePickerFormat.Short;
            dtpBitis.Location = new Point(535, 18);
            dtpBitis.Size = new Size(130, 27);

            btnAra.Location = new Point(690, 17);
            btnAra.Size = new Size(90, 29);
            btnAra.Text = "Ara";
            btnAra.Click += btnAra_Click;

            btnTemizle.Location = new Point(790, 17);
            btnTemizle.Size = new Size(90, 29);
            btnTemizle.Text = "Temizle";
            btnTemizle.Click += btnTemizle_Click;

            lblSonuc.AutoSize = true;
            lblSonuc.Location = new Point(20, 60);
            lblSonuc.Text = "Loglar yükleniyor...";

            tabLoglar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabLoglar.Controls.Add(tabSayac);
            tabLoglar.Controls.Add(tabToner);
            tabLoglar.Controls.Add(tabDrum);
            tabLoglar.Controls.Add(tabIslem);
            tabLoglar.Location = new Point(12, 88);
            tabLoglar.Size = new Size(1060, 500);

            tabSayac.Controls.Add(dgvSayac);
            tabSayac.Text = "Sayaç Logları";

            GridAyarla(dgvSayac);

            tabToner.Controls.Add(dgvToner);
            tabToner.Text = "Toner Logları";

            GridAyarla(dgvToner);

            tabDrum.Controls.Add(dgvDrum);
            tabDrum.Text = "Drum Logları";

            GridAyarla(dgvDrum);

            tabIslem.Controls.Add(dgvIslem);
            tabIslem.Text = "İşlem Logları";

            GridAyarla(dgvIslem);

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 601);
            Controls.Add(lblSonuc);
            Controls.Add(btnTemizle);
            Controls.Add(btnAra);
            Controls.Add(dtpBitis);
            Controls.Add(lblBitis);
            Controls.Add(dtpBaslangic);
            Controls.Add(lblBaslangic);
            Controls.Add(txtIpAdresi);
            Controls.Add(lblIp);
            Controls.Add(tabLoglar);
            MinimumSize = new Size(900, 500);
            Name = "LogForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Log Paneli";
            Load += LogForm_Load;
            tabLoglar.ResumeLayout(false);
            tabSayac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSayac).EndInit();
            tabToner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvToner).EndInit();
            tabDrum.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDrum).EndInit();
            tabIslem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvIslem).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void GridAyarla(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private Label lblIp;
        private TextBox txtIpAdresi;
        private Label lblBaslangic;
        private DateTimePicker dtpBaslangic;
        private Label lblBitis;
        private DateTimePicker dtpBitis;
        private Button btnAra;
        private Button btnTemizle;
        private Label lblSonuc;
        private TabControl tabLoglar;
        private TabPage tabSayac;
        private DataGridView dgvSayac;
        private TabPage tabToner;
        private DataGridView dgvToner;
        private TabPage tabDrum;
        private DataGridView dgvDrum;
        private TabPage tabIslem;
        private DataGridView dgvIslem;
    }
}
