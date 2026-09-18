namespace PrinterTrackingApp.UI.Forms
{
    partial class YaziciEkleForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtIpAdresi = new TextBox();
            txtMarka = new TextBox();
            txtModel = new TextBox();
            txtKonum = new TextBox();
            txtBaskiTeknolojisi = new TextBox();
            chkAktifMi = new CheckBox();
            chkRenkliMi = new CheckBox();
            chkFotokopiVarMi = new CheckBox();
            grpSnmp = new GroupBox();
            lblSnmpVersion = new Label();
            cmbSnmpVersion = new ComboBox();
            lblSnmpCommunity = new Label();
            txtSnmpCommunity = new TextBox();
            chkSnmpAktifMi = new CheckBox();
            btnKaydet = new Button();
            grpSnmp.SuspendLayout();
            SuspendLayout();

            label1.AutoSize = true; label1.Location = new Point(61, 36); label1.Name = "label1"; label1.Size = new Size(67, 20); label1.Text = "IP Adresi";

            label2.AutoSize = true; label2.Location = new Point(61, 86); label2.Name = "label2"; label2.Size = new Size(50, 20); label2.Text = "Marka";

            label3.AutoSize = true; label3.Location = new Point(61, 145); label3.Name = "label3"; label3.Size = new Size(52, 20); label3.Text = "Model";

            label4.AutoSize = true; label4.Location = new Point(61, 214); label4.Name = "label4"; label4.Size = new Size(56, 20); label4.Text = "Konum";

            label5.AutoSize = true; label5.Location = new Point(61, 278); label5.Name = "label5"; label5.Size = new Size(117, 20); label5.Text = "Baskı Teknolojisi"; label5.Click += label5_Click;

            txtIpAdresi.Location = new Point(256, 36); txtIpAdresi.Name = "txtIpAdresi"; txtIpAdresi.Size = new Size(190, 27);
            txtMarka.Location = new Point(256, 86); txtMarka.Name = "txtMarka"; txtMarka.Size = new Size(190, 27);
            txtModel.Location = new Point(256, 145); txtModel.Name = "txtModel"; txtModel.Size = new Size(190, 27);
            txtKonum.Location = new Point(256, 211); txtKonum.Name = "txtKonum"; txtKonum.Size = new Size(190, 27);
            txtBaskiTeknolojisi.Location = new Point(256, 275); txtBaskiTeknolojisi.Name = "txtBaskiTeknolojisi"; txtBaskiTeknolojisi.Size = new Size(190, 27);

            chkAktifMi.AutoSize = true; chkAktifMi.Checked = true; chkAktifMi.CheckState = CheckState.Checked; chkAktifMi.Location = new Point(66, 338); chkAktifMi.Name = "chkAktifMi"; chkAktifMi.Size = new Size(62, 24); chkAktifMi.Text = "Aktif"; chkAktifMi.UseVisualStyleBackColor = true;
            chkRenkliMi.AutoSize = true; chkRenkliMi.Location = new Point(156, 338); chkRenkliMi.Name = "chkRenkliMi"; chkRenkliMi.Size = new Size(71, 24); chkRenkliMi.Text = "Renkli"; chkRenkliMi.UseVisualStyleBackColor = true;
            chkFotokopiVarMi.AutoSize = true; chkFotokopiVarMi.Location = new Point(256, 338); chkFotokopiVarMi.Name = "chkFotokopiVarMi"; chkFotokopiVarMi.Size = new Size(115, 24); chkFotokopiVarMi.Text = "Fotokopi Var"; chkFotokopiVarMi.UseVisualStyleBackColor = true;

            grpSnmp.Controls.Add(lblSnmpVersion); grpSnmp.Controls.Add(cmbSnmpVersion); grpSnmp.Controls.Add(lblSnmpCommunity); grpSnmp.Controls.Add(txtSnmpCommunity); grpSnmp.Controls.Add(chkSnmpAktifMi);
            grpSnmp.Location = new Point(61, 386); grpSnmp.Name = "grpSnmp"; grpSnmp.Size = new Size(455, 155); grpSnmp.TabStop = false; grpSnmp.Text = "SNMP Ayarları";

            chkSnmpAktifMi.AutoSize = true; chkSnmpAktifMi.Checked = true; chkSnmpAktifMi.CheckState = CheckState.Checked; chkSnmpAktifMi.Location = new Point(19, 31); chkSnmpAktifMi.Name = "chkSnmpAktifMi"; chkSnmpAktifMi.Size = new Size(103, 24); chkSnmpAktifMi.Text = "SNMP Aktif"; chkSnmpAktifMi.UseVisualStyleBackColor = true;

            lblSnmpCommunity.AutoSize = true; lblSnmpCommunity.Location = new Point(19, 72); lblSnmpCommunity.Name = "lblSnmpCommunity"; lblSnmpCommunity.Size = new Size(89, 20); lblSnmpCommunity.Text = "Community";
            txtSnmpCommunity.Location = new Point(142, 69); txtSnmpCommunity.Name = "txtSnmpCommunity"; txtSnmpCommunity.Size = new Size(180, 27); txtSnmpCommunity.Text = "public";

            lblSnmpVersion.AutoSize = true; lblSnmpVersion.Location = new Point(19, 112); lblSnmpVersion.Name = "lblSnmpVersion"; lblSnmpVersion.Size = new Size(94, 20); lblSnmpVersion.Text = "SNMP Version";
            cmbSnmpVersion.DropDownStyle = ComboBoxStyle.DropDownList; cmbSnmpVersion.FormattingEnabled = true; cmbSnmpVersion.Items.AddRange(new object[] { "1", "2c" }); cmbSnmpVersion.Location = new Point(142, 109); cmbSnmpVersion.Name = "cmbSnmpVersion"; cmbSnmpVersion.Size = new Size(180, 28);

            btnKaydet.Location = new Point(256, 570); btnKaydet.Name = "btnKaydet"; btnKaydet.Size = new Size(120, 34); btnKaydet.Text = "Kaydet"; btnKaydet.UseVisualStyleBackColor = true; btnKaydet.Click += btnKaydet_Click;

            AutoScaleDimensions = new SizeF(8F, 20F); AutoScaleMode = AutoScaleMode.Font; ClientSize = new Size(600, 635);
            Controls.Add(btnKaydet); Controls.Add(grpSnmp); Controls.Add(chkFotokopiVarMi); Controls.Add(chkRenkliMi); Controls.Add(chkAktifMi); Controls.Add(txtBaskiTeknolojisi); Controls.Add(txtKonum); Controls.Add(txtModel); Controls.Add(txtMarka); Controls.Add(txtIpAdresi); Controls.Add(label5); Controls.Add(label4); Controls.Add(label3); Controls.Add(label2); Controls.Add(label1);
            Name = "YaziciEkleForm"; StartPosition = FormStartPosition.CenterParent; Text = "Yazıcı Ekle"; Load += YaziciEkleForm_Load;
            grpSnmp.ResumeLayout(false); grpSnmp.PerformLayout(); ResumeLayout(false); PerformLayout();
        }
        #endregion

        private Label label1; private Label label2; private Label label3; private Label label4; private Label label5;
        private TextBox txtIpAdresi; private TextBox txtMarka; private TextBox txtModel; private TextBox txtKonum; private TextBox txtBaskiTeknolojisi;
        private CheckBox chkAktifMi; private CheckBox chkRenkliMi; private CheckBox chkFotokopiVarMi;
        private GroupBox grpSnmp; private CheckBox chkSnmpAktifMi; private Label lblSnmpCommunity; private TextBox txtSnmpCommunity; private Label lblSnmpVersion; private ComboBox cmbSnmpVersion;
        private Button btnKaydet;
    }
}
