namespace spinjitzuu4
{
	// Token: 0x02000026 RID: 38
	public partial class UpgradeForm : global::MetroSet_UI.Forms.MetroSetForm
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00002BC3 File Offset: 0x00000DC3
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00015F1C File Offset: 0x0001411C
		private void InitializeComponent()
		{
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.usernameTxt = new global::MetroSet_UI.Controls.MetroSetTextBox();
			this.licenseTxt = new global::MetroSet_UI.Controls.MetroSetTextBox();
			this.loginBtn = new global::MetroSet_UI.Controls.MetroSetButton();
			this.loginLbl = new global::System.Windows.Forms.LinkLabel();
			this.registerStatusLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.backgroundWorker1 = new global::System.ComponentModel.BackgroundWorker();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.pictureBox1.Image = global::spinjitzuu4.Properties.Resources.watermark;
			this.pictureBox1.Location = new global::System.Drawing.Point(62, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(285, 93);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			this.usernameTxt.AutoCompleteCustomSource = null;
			this.usernameTxt.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.None;
			this.usernameTxt.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.None;
			this.usernameTxt.BorderColor = global::System.Drawing.Color.FromArgb(110, 110, 110);
			this.usernameTxt.DisabledBackColor = global::System.Drawing.Color.FromArgb(80, 80, 80);
			this.usernameTxt.DisabledBorderColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.usernameTxt.DisabledForeColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.usernameTxt.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 15.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.usernameTxt.HoverColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.usernameTxt.Image = null;
			this.usernameTxt.IsDerivedStyle = false;
			this.usernameTxt.Lines = null;
			this.usernameTxt.Location = new global::System.Drawing.Point(62, 124);
			this.usernameTxt.MaxLength = 32767;
			this.usernameTxt.Multiline = false;
			this.usernameTxt.Name = "usernameTxt";
			this.usernameTxt.ReadOnly = false;
			this.usernameTxt.Size = new global::System.Drawing.Size(285, 35);
			this.usernameTxt.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.usernameTxt.StyleManager = null;
			this.usernameTxt.TabIndex = 5;
			this.usernameTxt.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
			this.usernameTxt.ThemeAuthor = "Narwin";
			this.usernameTxt.ThemeName = "MetroDark";
			this.usernameTxt.UseSystemPasswordChar = false;
			this.usernameTxt.WatermarkText = "Username";
			this.licenseTxt.AutoCompleteCustomSource = null;
			this.licenseTxt.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.None;
			this.licenseTxt.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.None;
			this.licenseTxt.BorderColor = global::System.Drawing.Color.FromArgb(110, 110, 110);
			this.licenseTxt.DisabledBackColor = global::System.Drawing.Color.FromArgb(80, 80, 80);
			this.licenseTxt.DisabledBorderColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.licenseTxt.DisabledForeColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.licenseTxt.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 15.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.licenseTxt.HoverColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.licenseTxt.Image = null;
			this.licenseTxt.IsDerivedStyle = false;
			this.licenseTxt.Lines = null;
			this.licenseTxt.Location = new global::System.Drawing.Point(62, 177);
			this.licenseTxt.MaxLength = 32767;
			this.licenseTxt.Multiline = false;
			this.licenseTxt.Name = "licenseTxt";
			this.licenseTxt.ReadOnly = false;
			this.licenseTxt.Size = new global::System.Drawing.Size(285, 35);
			this.licenseTxt.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.licenseTxt.StyleManager = null;
			this.licenseTxt.TabIndex = 6;
			this.licenseTxt.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
			this.licenseTxt.ThemeAuthor = "Narwin";
			this.licenseTxt.ThemeName = "MetroDark";
			this.licenseTxt.UseSystemPasswordChar = true;
			this.licenseTxt.WatermarkText = "License";
			this.loginBtn.DisabledBackColor = global::System.Drawing.Color.FromArgb(120, 65, 177, 225);
			this.loginBtn.DisabledBorderColor = global::System.Drawing.Color.FromArgb(120, 65, 177, 225);
			this.loginBtn.DisabledForeColor = global::System.Drawing.Color.Gray;
			this.loginBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.loginBtn.HoverBorderColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.loginBtn.HoverColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.loginBtn.HoverTextColor = global::System.Drawing.Color.White;
			this.loginBtn.IsDerivedStyle = false;
			this.loginBtn.Location = new global::System.Drawing.Point(62, 258);
			this.loginBtn.Name = "loginBtn";
			this.loginBtn.NormalBorderColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.loginBtn.NormalColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.loginBtn.NormalTextColor = global::System.Drawing.Color.White;
			this.loginBtn.PressBorderColor = global::System.Drawing.Color.FromArgb(202, 64, 0);
			this.loginBtn.PressColor = global::System.Drawing.Color.FromArgb(202, 64, 0);
			this.loginBtn.PressTextColor = global::System.Drawing.Color.White;
			this.loginBtn.Size = new global::System.Drawing.Size(285, 45);
			this.loginBtn.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.loginBtn.StyleManager = null;
			this.loginBtn.TabIndex = 7;
			this.loginBtn.Text = "Upgrade";
			this.loginBtn.ThemeAuthor = "Narwin";
			this.loginBtn.ThemeName = "MetroDark";
			this.loginBtn.Click += new global::System.EventHandler(this.loginBtn_Click);
			this.loginLbl.AutoSize = true;
			this.loginLbl.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.loginLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.loginLbl.LinkColor = global::System.Drawing.Color.White;
			this.loginLbl.Location = new global::System.Drawing.Point(106, 315);
			this.loginLbl.Name = "loginLbl";
			this.loginLbl.Size = new global::System.Drawing.Size(198, 16);
			this.loginLbl.TabIndex = 11;
			this.loginLbl.TabStop = true;
			this.loginLbl.Text = "Already have an account? Login";
			this.loginLbl.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loginLbl_LinkClicked);
			this.registerStatusLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.registerStatusLbl.IsDerivedStyle = true;
			this.registerStatusLbl.Location = new global::System.Drawing.Point(62, 98);
			this.registerStatusLbl.Name = "registerStatusLbl";
			this.registerStatusLbl.Size = new global::System.Drawing.Size(285, 23);
			this.registerStatusLbl.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.registerStatusLbl.StyleManager = null;
			this.registerStatusLbl.TabIndex = 13;
			this.registerStatusLbl.Text = "-";
			this.registerStatusLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.registerStatusLbl.ThemeAuthor = "Narwin";
			this.registerStatusLbl.ThemeName = "MetroDark";
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.DoWork += new global::System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new global::System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			base.ClientSize = new global::System.Drawing.Size(409, 343);
			base.Controls.Add(this.registerStatusLbl);
			base.Controls.Add(this.loginLbl);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.usernameTxt);
			base.Controls.Add(this.licenseTxt);
			base.Controls.Add(this.loginBtn);
			base.DropShadowEffect = false;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximumSize = new global::System.Drawing.Size(429, 386);
			this.MinimumSize = new global::System.Drawing.Size(429, 386);
			base.Name = "UpgradeForm";
			base.ShowIcon = false;
			base.ShowLeftRect = false;
			base.ShowTitle = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.Text = "spinjitzuu - upgrade";
			base.TextColor = global::System.Drawing.Color.White;
			base.ThemeName = "MetroDark";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.UpgradeForm_FormClosed);
			base.Load += new global::System.EventHandler(this.UpgradeForm_Load);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400038D RID: 909
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400038E RID: 910
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x0400038F RID: 911
		private global::MetroSet_UI.Controls.MetroSetTextBox usernameTxt;

		// Token: 0x04000390 RID: 912
		private global::MetroSet_UI.Controls.MetroSetTextBox licenseTxt;

		// Token: 0x04000391 RID: 913
		private global::MetroSet_UI.Controls.MetroSetButton loginBtn;

		// Token: 0x04000392 RID: 914
		private global::System.Windows.Forms.LinkLabel loginLbl;

		// Token: 0x04000393 RID: 915
		private global::MetroSet_UI.Controls.MetroSetLabel registerStatusLbl;

		// Token: 0x04000394 RID: 916
		private global::System.ComponentModel.BackgroundWorker backgroundWorker1;
	}
}
