namespace spinjitzuu4
{
	// Token: 0x02000009 RID: 9
	public partial class RegisterForm : global::MetroSet_UI.Forms.MetroSetForm
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000281E File Offset: 0x00000A1E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000128A8 File Offset: 0x00010AA8
		private void InitializeComponent()
		{
			this.RegisterBtn = new global::MetroSet_UI.Controls.MetroSetButton();
			this.loginLbl = new global::System.Windows.Forms.LinkLabel();
			this.usernameTxt = new global::MetroSet_UI.Controls.MetroSetTextBox();
			this.passwordTxt = new global::MetroSet_UI.Controls.MetroSetTextBox();
			this.licenceTxt = new global::MetroSet_UI.Controls.MetroSetTextBox();
			this.backgroundWorker1 = new global::System.ComponentModel.BackgroundWorker();
			this.registerStatusLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.RegisterBtn.DisabledBackColor = global::System.Drawing.Color.FromArgb(120, 65, 177, 225);
			this.RegisterBtn.DisabledBorderColor = global::System.Drawing.Color.FromArgb(120, 65, 177, 225);
			this.RegisterBtn.DisabledForeColor = global::System.Drawing.Color.Gray;
			this.RegisterBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.RegisterBtn.HoverBorderColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.RegisterBtn.HoverColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.RegisterBtn.HoverTextColor = global::System.Drawing.Color.White;
			this.RegisterBtn.IsDerivedStyle = false;
			this.RegisterBtn.Location = new global::System.Drawing.Point(65, 327);
			this.RegisterBtn.Name = "RegisterBtn";
			this.RegisterBtn.NormalBorderColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.RegisterBtn.NormalColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.RegisterBtn.NormalTextColor = global::System.Drawing.Color.White;
			this.RegisterBtn.PressBorderColor = global::System.Drawing.Color.FromArgb(192, 64, 0);
			this.RegisterBtn.PressColor = global::System.Drawing.Color.FromArgb(192, 64, 0);
			this.RegisterBtn.PressTextColor = global::System.Drawing.Color.White;
			this.RegisterBtn.Size = new global::System.Drawing.Size(285, 45);
			this.RegisterBtn.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.RegisterBtn.StyleManager = null;
			this.RegisterBtn.TabIndex = 3;
			this.RegisterBtn.Text = "Register";
			this.RegisterBtn.ThemeAuthor = "Narwin";
			this.RegisterBtn.ThemeName = "MetroDark";
			this.RegisterBtn.Click += new global::System.EventHandler(this.RegisterBtn_Click);
			this.loginLbl.AutoSize = true;
			this.loginLbl.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.loginLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.loginLbl.LinkColor = global::System.Drawing.Color.White;
			this.loginLbl.Location = new global::System.Drawing.Point(109, 392);
			this.loginLbl.Name = "loginLbl";
			this.loginLbl.Size = new global::System.Drawing.Size(198, 16);
			this.loginLbl.TabIndex = 10;
			this.loginLbl.TabStop = true;
			this.loginLbl.Text = "Already have an account? Login";
			this.loginLbl.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loginLbl_LinkClicked);
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
			this.usernameTxt.Location = new global::System.Drawing.Point(65, 126);
			this.usernameTxt.MaxLength = 32767;
			this.usernameTxt.Multiline = false;
			this.usernameTxt.Name = "usernameTxt";
			this.usernameTxt.ReadOnly = false;
			this.usernameTxt.Size = new global::System.Drawing.Size(285, 35);
			this.usernameTxt.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.usernameTxt.StyleManager = null;
			this.usernameTxt.TabIndex = 0;
			this.usernameTxt.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
			this.usernameTxt.ThemeAuthor = "Narwin";
			this.usernameTxt.ThemeName = "MetroDark";
			this.usernameTxt.UseSystemPasswordChar = false;
			this.usernameTxt.WatermarkText = "Username";
			this.passwordTxt.AutoCompleteCustomSource = null;
			this.passwordTxt.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.None;
			this.passwordTxt.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.None;
			this.passwordTxt.BorderColor = global::System.Drawing.Color.FromArgb(110, 110, 110);
			this.passwordTxt.DisabledBackColor = global::System.Drawing.Color.FromArgb(80, 80, 80);
			this.passwordTxt.DisabledBorderColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.passwordTxt.DisabledForeColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.passwordTxt.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 15.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.passwordTxt.HoverColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.passwordTxt.Image = null;
			this.passwordTxt.IsDerivedStyle = false;
			this.passwordTxt.Lines = null;
			this.passwordTxt.Location = new global::System.Drawing.Point(65, 176);
			this.passwordTxt.MaxLength = 32767;
			this.passwordTxt.Multiline = false;
			this.passwordTxt.Name = "passwordTxt";
			this.passwordTxt.ReadOnly = false;
			this.passwordTxt.Size = new global::System.Drawing.Size(285, 35);
			this.passwordTxt.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.passwordTxt.StyleManager = null;
			this.passwordTxt.TabIndex = 1;
			this.passwordTxt.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
			this.passwordTxt.ThemeAuthor = "Narwin";
			this.passwordTxt.ThemeName = "MetroDark";
			this.passwordTxt.UseSystemPasswordChar = true;
			this.passwordTxt.WatermarkText = "Password";
			this.licenceTxt.AutoCompleteCustomSource = null;
			this.licenceTxt.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.None;
			this.licenceTxt.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.None;
			this.licenceTxt.BorderColor = global::System.Drawing.Color.FromArgb(110, 110, 110);
			this.licenceTxt.DisabledBackColor = global::System.Drawing.Color.FromArgb(80, 80, 80);
			this.licenceTxt.DisabledBorderColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.licenceTxt.DisabledForeColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.licenceTxt.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 15.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.licenceTxt.HoverColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.licenceTxt.Image = null;
			this.licenceTxt.IsDerivedStyle = false;
			this.licenceTxt.Lines = null;
			this.licenceTxt.Location = new global::System.Drawing.Point(65, 259);
			this.licenceTxt.MaxLength = 32767;
			this.licenceTxt.Multiline = false;
			this.licenceTxt.Name = "licenceTxt";
			this.licenceTxt.ReadOnly = false;
			this.licenceTxt.Size = new global::System.Drawing.Size(285, 35);
			this.licenceTxt.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.licenceTxt.StyleManager = null;
			this.licenceTxt.TabIndex = 2;
			this.licenceTxt.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
			this.licenceTxt.ThemeAuthor = "Narwin";
			this.licenceTxt.ThemeName = "MetroDark";
			this.licenceTxt.UseSystemPasswordChar = false;
			this.licenceTxt.WatermarkText = "Enter your licence";
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.DoWork += new global::System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new global::System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.registerStatusLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.registerStatusLbl.IsDerivedStyle = true;
			this.registerStatusLbl.Location = new global::System.Drawing.Point(65, 100);
			this.registerStatusLbl.Name = "registerStatusLbl";
			this.registerStatusLbl.Size = new global::System.Drawing.Size(285, 23);
			this.registerStatusLbl.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.registerStatusLbl.StyleManager = null;
			this.registerStatusLbl.TabIndex = 12;
			this.registerStatusLbl.Text = "-";
			this.registerStatusLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.registerStatusLbl.ThemeAuthor = "Narwin";
			this.registerStatusLbl.ThemeName = "MetroDark";
			this.pictureBox1.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.pictureBox1.Image = global::spinjitzuu4.Properties.Resources.watermark;
			this.pictureBox1.Location = new global::System.Drawing.Point(65, 5);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(285, 92);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 13;
			this.pictureBox1.TabStop = false;
			base.AllowResize = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(10f, 20f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			base.ClientSize = new global::System.Drawing.Size(413, 452);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.registerStatusLbl);
			base.Controls.Add(this.licenceTxt);
			base.Controls.Add(this.usernameTxt);
			base.Controls.Add(this.passwordTxt);
			base.Controls.Add(this.loginLbl);
			base.Controls.Add(this.RegisterBtn);
			base.DropShadowEffect = false;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximumSize = new global::System.Drawing.Size(429, 491);
			this.MinimumSize = new global::System.Drawing.Size(429, 491);
			base.Name = "RegisterForm";
			base.ShowLeftRect = false;
			base.ShowTitle = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.Text = "spinjitzuu - register";
			base.TextColor = global::System.Drawing.Color.White;
			base.ThemeName = "MetroDark";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.RegisterForm_FormClosed);
			base.Load += new global::System.EventHandler(this.RegisterForm_Load);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000CD RID: 205
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000CE RID: 206
		private global::MetroSet_UI.Controls.MetroSetButton RegisterBtn;

		// Token: 0x040000CF RID: 207
		private global::System.Windows.Forms.LinkLabel loginLbl;

		// Token: 0x040000D0 RID: 208
		private global::MetroSet_UI.Controls.MetroSetTextBox usernameTxt;

		// Token: 0x040000D1 RID: 209
		private global::MetroSet_UI.Controls.MetroSetTextBox passwordTxt;

		// Token: 0x040000D2 RID: 210
		private global::MetroSet_UI.Controls.MetroSetTextBox licenceTxt;

		// Token: 0x040000D3 RID: 211
		private global::System.ComponentModel.BackgroundWorker backgroundWorker1;

		// Token: 0x040000D4 RID: 212
		private global::MetroSet_UI.Controls.MetroSetLabel registerStatusLbl;

		// Token: 0x040000D5 RID: 213
		private global::System.Windows.Forms.PictureBox pictureBox1;
	}
}
