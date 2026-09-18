namespace PrinterTrackingApp.UI.Forms
{
    partial class UyariForm
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
            dgvUyarilar = new DataGridView();
            lblEsik = new Label();
            numKritikEsik = new NumericUpDown();
            btnYenile = new Button();
            lblSonuc = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvUyarilar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numKritikEsik).BeginInit();
            SuspendLayout();

            dgvUyarilar.AllowUserToAddRows = false;
            dgvUyarilar.AllowUserToDeleteRows = false;
            dgvUyarilar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUyarilar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUyarilar.Dock = DockStyle.Bottom;
            dgvUyarilar.Location = new Point(0, 88);
            dgvUyarilar.MultiSelect = false;
            dgvUyarilar.Name = "dgvUyarilar";
            dgvUyarilar.ReadOnly = true;
            dgvUyarilar.RowHeadersWidth = 51;
            dgvUyarilar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUyarilar.Size = new Size(1050, 512);
            dgvUyarilar.TabIndex = 0;

            lblEsik.AutoSize = true;
            lblEsik.Location = new Point(24, 20);
            lblEsik.Name = "lblEsik";
            lblEsik.Size = new Size(115, 20);
            lblEsik.Text = "Kritik seviye (%):";

            numKritikEsik.Location = new Point(145, 17);
            numKritikEsik.Maximum = 100;
            numKritikEsik.Name = "numKritikEsik";
            numKritikEsik.Size = new Size(80, 27);
            numKritikEsik.TabIndex = 2;
            numKritikEsik.Value = 20;

            btnYenile.Location = new Point(245, 16);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(100, 30);
            btnYenile.TabIndex = 3;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = true;
            btnYenile.Click += btnYenile_Click;

            lblSonuc.AutoSize = true;
            lblSonuc.Location = new Point(24, 56);
            lblSonuc.Name = "lblSonuc";
            lblSonuc.Size = new Size(131, 20);
            lblSonuc.Text = "Uyarılar yükleniyor...";

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 600);
            Controls.Add(lblSonuc);
            Controls.Add(btnYenile);
            Controls.Add(numKritikEsik);
            Controls.Add(lblEsik);
            Controls.Add(dgvUyarilar);
            MinimumSize = new Size(900, 500);
            Name = "UyariForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kritik Uyarılar";
            Load += UyariForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUyarilar).EndInit();
            ((System.ComponentModel.ISupportInitialize)numKritikEsik).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvUyarilar;
        private Label lblEsik;
        private NumericUpDown numKritikEsik;
        private Button btnYenile;
        private Label lblSonuc;
    }
}
