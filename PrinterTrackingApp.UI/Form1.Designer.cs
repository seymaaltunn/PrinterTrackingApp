namespace PrinterTrackingApp.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvYazicilar = new DataGridView();
            btnYaziciEkle = new Button();
            btnYaziciSil = new Button();
            btnYaziciDuzenle = new Button();
            btnSayacOlcumuAl = new Button();
            btnTumunuOlc = new Button();
            btnLogPaneli = new Button();
            btnUyarilar = new Button();
            chkGercekSnmp = new CheckBox();
            progressOlcum = new ProgressBar();
            lblOlcumDurumu = new Label();
            lblAktifYazici = new Label();
            lblKritikToner = new Label();
            lblKritikDrum = new Label();
            lblBasarisizOlcum = new Label();
            lblSonOlcum = new Label();
            chkOtomatikOlcum = new CheckBox();
            numOlcumDakika = new NumericUpDown();
            lblDakika = new Label();
            lblOtomatikDurum = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvYazicilar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numOlcumDakika).BeginInit();
            SuspendLayout();


            dgvYazicilar.AllowUserToAddRows = false;
            dgvYazicilar.AllowUserToDeleteRows = false;
            dgvYazicilar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvYazicilar.BackgroundColor = Color.FromArgb(245, 247, 250);
            dgvYazicilar.BorderStyle = BorderStyle.None;
            dgvYazicilar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvYazicilar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvYazicilar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 78, 120);
            dgvYazicilar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvYazicilar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvYazicilar.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(31, 78, 120);
            dgvYazicilar.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvYazicilar.ColumnHeadersHeight = 42;
            dgvYazicilar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvYazicilar.DefaultCellStyle.BackColor = Color.White;
            dgvYazicilar.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgvYazicilar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(221, 235, 247);
            dgvYazicilar.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 55, 85);
            dgvYazicilar.DefaultCellStyle.Padding = new Padding(4);
            dgvYazicilar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvYazicilar.Dock = DockStyle.Bottom;
            dgvYazicilar.EnableHeadersVisualStyles = false;
            dgvYazicilar.GridColor = Color.FromArgb(225, 230, 236);
            dgvYazicilar.Location = new Point(0, 325);
            dgvYazicilar.MultiSelect = false;
            dgvYazicilar.Name = "dgvYazicilar";
            dgvYazicilar.ReadOnly = false;
            dgvYazicilar.RowHeadersVisible = false;
            dgvYazicilar.RowHeadersWidth = 51;
            dgvYazicilar.RowTemplate.Height = 34;
            dgvYazicilar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvYazicilar.Size = new Size(1180, 395);
            dgvYazicilar.TabIndex = 1;


            btnYaziciEkle.BackColor = Color.FromArgb(40, 167, 69);
            btnYaziciEkle.FlatAppearance.BorderSize = 0;
            btnYaziciEkle.FlatStyle = FlatStyle.Flat;
            btnYaziciEkle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnYaziciEkle.ForeColor = Color.White;
            btnYaziciEkle.Location = new Point(28, 24);
            btnYaziciEkle.Name = "btnYaziciEkle";
            btnYaziciEkle.Size = new Size(205, 34);
            btnYaziciEkle.Text = "+  Yeni Yazıcı Ekle";
            btnYaziciEkle.UseVisualStyleBackColor = false;
            btnYaziciEkle.Click += btnYaziciEkle_Click;


            btnYaziciSil.BackColor = Color.FromArgb(217, 83, 79);
            btnYaziciSil.FlatAppearance.BorderSize = 0;
            btnYaziciSil.FlatStyle = FlatStyle.Flat;
            btnYaziciSil.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnYaziciSil.ForeColor = Color.White;
            btnYaziciSil.Location = new Point(28, 66);
            btnYaziciSil.Name = "btnYaziciSil";
            btnYaziciSil.Size = new Size(205, 34);
            btnYaziciSil.Text = "Seçili Yazıcıyı Sil";
            btnYaziciSil.UseVisualStyleBackColor = false;
            btnYaziciSil.Click += btnYaziciSil_Click;


            btnYaziciDuzenle.BackColor = Color.FromArgb(47, 117, 181);
            btnYaziciDuzenle.FlatAppearance.BorderSize = 0;
            btnYaziciDuzenle.FlatStyle = FlatStyle.Flat;
            btnYaziciDuzenle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnYaziciDuzenle.ForeColor = Color.White;
            btnYaziciDuzenle.Location = new Point(28, 108);
            btnYaziciDuzenle.Name = "btnYaziciDuzenle";
            btnYaziciDuzenle.Size = new Size(205, 34);
            btnYaziciDuzenle.Text = "Seçili Yazıcıyı Düzenle";
            btnYaziciDuzenle.UseVisualStyleBackColor = false;
            btnYaziciDuzenle.Click += btnYaziciDuzenle_Click;


            btnSayacOlcumuAl.BackColor = Color.FromArgb(31, 78, 120);
            btnSayacOlcumuAl.FlatAppearance.BorderSize = 0;
            btnSayacOlcumuAl.FlatStyle = FlatStyle.Flat;
            btnSayacOlcumuAl.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnSayacOlcumuAl.ForeColor = Color.White;
            btnSayacOlcumuAl.Location = new Point(260, 24);
            btnSayacOlcumuAl.Name = "btnSayacOlcumuAl";
            btnSayacOlcumuAl.Size = new Size(215, 34);
            btnSayacOlcumuAl.Text = "Seçili Yazıcıyı Ölç";
            btnSayacOlcumuAl.UseVisualStyleBackColor = false;
            btnSayacOlcumuAl.Click += btnSayacOlcumuAl_Click;


            btnTumunuOlc.BackColor = Color.FromArgb(31, 78, 120);
            btnTumunuOlc.FlatAppearance.BorderSize = 0;
            btnTumunuOlc.FlatStyle = FlatStyle.Flat;
            btnTumunuOlc.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnTumunuOlc.ForeColor = Color.White;
            btnTumunuOlc.Location = new Point(260, 66);
            btnTumunuOlc.Name = "btnTumunuOlc";
            btnTumunuOlc.Size = new Size(215, 34);
            btnTumunuOlc.Text = "Tüm Aktif Yazıcıları Ölç";
            btnTumunuOlc.UseVisualStyleBackColor = false;
            btnTumunuOlc.Click += btnTumunuOlc_Click;


            btnLogPaneli.BackColor = Color.FromArgb(88, 96, 105);
            btnLogPaneli.FlatAppearance.BorderSize = 0;
            btnLogPaneli.FlatStyle = FlatStyle.Flat;
            btnLogPaneli.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnLogPaneli.ForeColor = Color.White;
            btnLogPaneli.Location = new Point(260, 108);
            btnLogPaneli.Name = "btnLogPaneli";
            btnLogPaneli.Size = new Size(104, 34);
            btnLogPaneli.Text = "Log Paneli";
            btnLogPaneli.UseVisualStyleBackColor = false;
            btnLogPaneli.Click += btnLogPaneli_Click;


            btnUyarilar.BackColor = Color.FromArgb(240, 173, 78);
            btnUyarilar.FlatAppearance.BorderSize = 0;
            btnUyarilar.FlatStyle = FlatStyle.Flat;
            btnUyarilar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnUyarilar.ForeColor = Color.White;
            btnUyarilar.Location = new Point(371, 108);
            btnUyarilar.Name = "btnUyarilar";
            btnUyarilar.Size = new Size(104, 34);
            btnUyarilar.Text = "Kritik Uyarılar";
            btnUyarilar.UseVisualStyleBackColor = false;
            btnUyarilar.Click += btnUyarilar_Click;


            chkGercekSnmp.AutoSize = true;
            chkGercekSnmp.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            chkGercekSnmp.ForeColor = Color.FromArgb(31, 78, 120);
            chkGercekSnmp.Location = new Point(510, 27);
            chkGercekSnmp.Name = "chkGercekSnmp";
            chkGercekSnmp.Size = new Size(160, 24);
            chkGercekSnmp.Text = "Gerçek SNMP kullan";
            chkGercekSnmp.UseVisualStyleBackColor = true;


            progressOlcum.Location = new Point(510, 62);
            progressOlcum.Name = "progressOlcum";
            progressOlcum.Size = new Size(630, 24);


            lblOlcumDurumu.AutoSize = true;
            lblOlcumDurumu.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblOlcumDurumu.ForeColor = Color.FromArgb(88, 96, 105);
            lblOlcumDurumu.Location = new Point(510, 96);
            lblOlcumDurumu.Name = "lblOlcumDurumu";
            lblOlcumDurumu.Size = new Size(118, 20);
            lblOlcumDurumu.Text = "Ölçüm bekleniyor.";


            lblAktifYazici.BackColor = Color.FromArgb(226, 239, 218);
            lblAktifYazici.BorderStyle = BorderStyle.FixedSingle;
            lblAktifYazici.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblAktifYazici.ForeColor = Color.FromArgb(47, 84, 35);
            lblAktifYazici.Location = new Point(28, 205);
            lblAktifYazici.Name = "lblAktifYazici";
            lblAktifYazici.Size = new Size(180, 58);
            lblAktifYazici.Text = "Aktif Yazıcı\n0";
            lblAktifYazici.TextAlign = ContentAlignment.MiddleCenter;


            lblKritikToner.BackColor = Color.FromArgb(255, 242, 204);
            lblKritikToner.BorderStyle = BorderStyle.FixedSingle;
            lblKritikToner.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblKritikToner.ForeColor = Color.FromArgb(127, 96, 0);
            lblKritikToner.Location = new Point(218, 205);
            lblKritikToner.Name = "lblKritikToner";
            lblKritikToner.Size = new Size(180, 58);
            lblKritikToner.Text = "Kritik Toner\n0";
            lblKritikToner.TextAlign = ContentAlignment.MiddleCenter;


            lblKritikDrum.BackColor = Color.FromArgb(255, 230, 204);
            lblKritikDrum.BorderStyle = BorderStyle.FixedSingle;
            lblKritikDrum.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblKritikDrum.ForeColor = Color.FromArgb(132, 60, 12);
            lblKritikDrum.Location = new Point(408, 205);
            lblKritikDrum.Name = "lblKritikDrum";
            lblKritikDrum.Size = new Size(180, 58);
            lblKritikDrum.Text = "Kritik Drum\n0";
            lblKritikDrum.TextAlign = ContentAlignment.MiddleCenter;


            lblBasarisizOlcum.BackColor = Color.FromArgb(244, 204, 204);
            lblBasarisizOlcum.BorderStyle = BorderStyle.FixedSingle;
            lblBasarisizOlcum.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblBasarisizOlcum.ForeColor = Color.FromArgb(156, 0, 6);
            lblBasarisizOlcum.Location = new Point(598, 205);
            lblBasarisizOlcum.Name = "lblBasarisizOlcum";
            lblBasarisizOlcum.Size = new Size(210, 58);
            lblBasarisizOlcum.Text = "Son Ölçüm Başarısız\n0";
            lblBasarisizOlcum.TextAlign = ContentAlignment.MiddleCenter;


            lblSonOlcum.AutoSize = true;
            lblSonOlcum.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblSonOlcum.ForeColor = Color.FromArgb(31, 78, 120);
            lblSonOlcum.Location = new Point(28, 276);
            lblSonOlcum.Name = "lblSonOlcum";
            lblSonOlcum.Size = new Size(117, 20);
            lblSonOlcum.Text = "Henüz ölçüm yok.";


            chkOtomatikOlcum.AutoSize = true;
            chkOtomatikOlcum.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            chkOtomatikOlcum.ForeColor = Color.FromArgb(31, 78, 120);
            chkOtomatikOlcum.Location = new Point(840, 205);
            chkOtomatikOlcum.Name = "chkOtomatikOlcum";
            chkOtomatikOlcum.Size = new Size(142, 24);
            chkOtomatikOlcum.Text = "Otomatik Ölçüm";
            chkOtomatikOlcum.UseVisualStyleBackColor = true;
            chkOtomatikOlcum.CheckedChanged += chkOtomatikOlcum_CheckedChanged;


            numOlcumDakika.BackColor = Color.White;
            numOlcumDakika.Location = new Point(1000, 203);
            numOlcumDakika.Maximum = 1440;
            numOlcumDakika.Minimum = 1;
            numOlcumDakika.Name = "numOlcumDakika";
            numOlcumDakika.Size = new Size(70, 27);
            numOlcumDakika.Value = 30;
            numOlcumDakika.ValueChanged += numOlcumDakika_ValueChanged;


            lblDakika.AutoSize = true;
            lblDakika.ForeColor = Color.FromArgb(88, 96, 105);
            lblDakika.Location = new Point(1075, 206);
            lblDakika.Name = "lblDakika";
            lblDakika.Size = new Size(53, 20);
            lblDakika.Text = "dakika";


            lblOtomatikDurum.AutoSize = true;
            lblOtomatikDurum.ForeColor = Color.FromArgb(88, 96, 105);
            lblOtomatikDurum.Location = new Point(840, 240);
            lblOtomatikDurum.Name = "lblOtomatikDurum";
            lblOtomatikDurum.Size = new Size(165, 20);
            lblOtomatikDurum.Text = "Otomatik ölçüm kapalı.";


            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1180, 720);
            Controls.Add(lblOtomatikDurum);
            Controls.Add(lblDakika);
            Controls.Add(numOlcumDakika);
            Controls.Add(chkOtomatikOlcum);
            Controls.Add(lblSonOlcum);
            Controls.Add(lblBasarisizOlcum);
            Controls.Add(lblKritikDrum);
            Controls.Add(lblKritikToner);
            Controls.Add(lblAktifYazici);
            Controls.Add(lblOlcumDurumu);
            Controls.Add(progressOlcum);
            Controls.Add(chkGercekSnmp);
            Controls.Add(btnUyarilar);
            Controls.Add(btnLogPaneli);
            Controls.Add(btnTumunuOlc);
            Controls.Add(btnSayacOlcumuAl);
            Controls.Add(btnYaziciDuzenle);
            Controls.Add(btnYaziciSil);
            Controls.Add(btnYaziciEkle);
            Controls.Add(dgvYazicilar);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1050, 650);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yazıcı Takip Sistemi";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvYazicilar).EndInit();
            ((System.ComponentModel.ISupportInitialize)numOlcumDakika).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvYazicilar;
        private Button btnYaziciEkle;
        private Button btnYaziciSil;
        private Button btnYaziciDuzenle;
        private Button btnSayacOlcumuAl;
        private Button btnTumunuOlc;
        private Button btnLogPaneli;
        private Button btnUyarilar;
        private CheckBox chkGercekSnmp;
        private ProgressBar progressOlcum;
        private Label lblOlcumDurumu;
        private Label lblAktifYazici;
        private Label lblKritikToner;
        private Label lblKritikDrum;
        private Label lblBasarisizOlcum;
        private Label lblSonOlcum;
        private CheckBox chkOtomatikOlcum;
        private NumericUpDown numOlcumDakika;
        private Label lblDakika;
        private Label lblOtomatikDurum;
    }
}
