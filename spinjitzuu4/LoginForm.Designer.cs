namespace spinjitzuu4
{
	// Token: 0x0200000A RID: 10
	public partial class LoginForm : global::MetroSet_UI.Forms.MetroSetForm
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000294F File Offset: 0x00000B4F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00013840 File Offset: 0x00011A40
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::spinjitzuu4.LoginForm));
			this.loginBtn = new global::MetroSet_UI.Controls.MetroSetButton();
			this.passwordTxt = new global::MetroSet_UI.Controls.MetroSetTextBox();
			this.usernameTxt = new global::MetroSet_UI.Controls.MetroSetTextBox();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.registerLbl = new global::System.Windows.Forms.LinkLabel();
			this.loginStatusLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.rememberMe = new global::MetroSet_UI.Controls.MetroSetCheckBox();
			this.spaceglideKeyLbl = new global::Bunifu.UI.WinForms.BunifuLabel();
			this.progressBar = new global::Bunifu.UI.WinForms.BunifuProgressBar();
			this.backgroundWorker1 = new global::System.ComponentModel.BackgroundWorker();
			this.linkLabel1 = new global::System.Windows.Forms.LinkLabel();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.loginBtn.DisabledBackColor = global::System.Drawing.Color.FromArgb(120, 65, 177, 225);
			this.loginBtn.DisabledBorderColor = global::System.Drawing.Color.FromArgb(120, 65, 177, 225);
			this.loginBtn.DisabledForeColor = global::System.Drawing.Color.Gray;
			this.loginBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.loginBtn.HoverBorderColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.loginBtn.HoverColor = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.loginBtn.HoverTextColor = global::System.Drawing.Color.White;
			this.loginBtn.IsDerivedStyle = false;
			this.loginBtn.Location = new global::System.Drawing.Point(65, 258);
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
			this.loginBtn.TabIndex = 2;
			this.loginBtn.Text = "Login";
			this.loginBtn.ThemeAuthor = "Narwin";
			this.loginBtn.ThemeName = "MetroDark";
			this.loginBtn.Click += new global::System.EventHandler(this.loginBtn_Click);
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
			this.passwordTxt.Location = new global::System.Drawing.Point(65, 173);
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
			this.usernameTxt.Location = new global::System.Drawing.Point(65, 120);
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
			this.pictureBox1.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.pictureBox1.Image = global::spinjitzuu4.Properties.Resources.watermark;
			this.pictureBox1.Location = new global::System.Drawing.Point(65, 1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(285, 90);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			this.registerLbl.AutoSize = true;
			this.registerLbl.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.registerLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.registerLbl.LinkColor = global::System.Drawing.Color.White;
			this.registerLbl.Location = new global::System.Drawing.Point(114, 318);
			this.registerLbl.Name = "registerLbl";
			this.registerLbl.Size = new global::System.Drawing.Size(179, 16);
			this.registerLbl.TabIndex = 5;
			this.registerLbl.TabStop = true;
			this.registerLbl.Text = "Don't have account? Sign Up";
			this.registerLbl.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerLbl_LinkClicked);
			this.loginStatusLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.loginStatusLbl.IsDerivedStyle = true;
			this.loginStatusLbl.Location = new global::System.Drawing.Point(65, 94);
			this.loginStatusLbl.Name = "loginStatusLbl";
			this.loginStatusLbl.Size = new global::System.Drawing.Size(285, 23);
			this.loginStatusLbl.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.loginStatusLbl.StyleManager = null;
			this.loginStatusLbl.TabIndex = 6;
			this.loginStatusLbl.Text = "-";
			this.loginStatusLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.loginStatusLbl.ThemeAuthor = "Narwin";
			this.loginStatusLbl.ThemeName = "MetroDark";
			this.rememberMe.BackColor = global::System.Drawing.Color.Transparent;
			this.rememberMe.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.rememberMe.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.rememberMe.Checked = false;
			this.rememberMe.CheckSignColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.rememberMe.CheckState = global::MetroSet_UI.Enums.CheckState.Unchecked;
			this.rememberMe.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.rememberMe.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.rememberMe.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.rememberMe.IsDerivedStyle = false;
			this.rememberMe.Location = new global::System.Drawing.Point(65, 237);
			this.rememberMe.Name = "rememberMe";
			this.rememberMe.SignStyle = global::MetroSet_UI.Enums.SignStyle.Sign;
			this.rememberMe.Size = new global::System.Drawing.Size(20, 16);
			this.rememberMe.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.rememberMe.StyleManager = null;
			this.rememberMe.TabIndex = 7;
			this.rememberMe.ThemeAuthor = "Narwin";
			this.rememberMe.ThemeName = "MetroDark";
			this.spaceglideKeyLbl.AllowParentOverrides = false;
			this.spaceglideKeyLbl.AutoEllipsis = false;
			this.spaceglideKeyLbl.CursorType = null;
			this.spaceglideKeyLbl.Font = new global::System.Drawing.Font("Century Gothic", 9.1f);
			this.spaceglideKeyLbl.ForeColor = global::System.Drawing.Color.White;
			this.spaceglideKeyLbl.Location = new global::System.Drawing.Point(87, 233);
			this.spaceglideKeyLbl.Name = "spaceglideKeyLbl";
			this.spaceglideKeyLbl.RightToLeft = global::System.Windows.Forms.RightToLeft.No;
			this.spaceglideKeyLbl.Size = new global::System.Drawing.Size(109, 20);
			this.spaceglideKeyLbl.TabIndex = 8;
			this.spaceglideKeyLbl.Text = "Remember Me";
			this.spaceglideKeyLbl.TextAlignment = global::System.Drawing.ContentAlignment.TopLeft;
			this.spaceglideKeyLbl.TextFormat = global::Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
			this.progressBar.AllowAnimations = false;
			this.progressBar.Animation = 0;
			this.progressBar.AnimationSpeed = 220;
			this.progressBar.AnimationStep = 10;
			this.progressBar.BackColor = global::System.Drawing.Color.FromArgb(223, 223, 223);
			this.progressBar.BackgroundImage = (global::System.Drawing.Image)componentResourceManager.GetObject("progressBar.BackgroundImage");
			this.progressBar.BorderColor = global::System.Drawing.Color.FromArgb(223, 223, 223);
			this.progressBar.BorderRadius = 9;
			this.progressBar.BorderThickness = 1;
			this.progressBar.Location = new global::System.Drawing.Point(65, 217);
			this.progressBar.Maximum = 100;
			this.progressBar.MaximumValue = 100;
			this.progressBar.Minimum = 0;
			this.progressBar.MinimumValue = 0;
			this.progressBar.Name = "progressBar";
			this.progressBar.Orientation = global::System.Windows.Forms.Orientation.Horizontal;
			this.progressBar.ProgressBackColor = global::System.Drawing.Color.FromArgb(223, 223, 223);
			this.progressBar.ProgressColorLeft = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.progressBar.ProgressColorRight = global::System.Drawing.Color.FromArgb(255, 128, 0);
			this.progressBar.Size = new global::System.Drawing.Size(285, 10);
			this.progressBar.TabIndex = 9;
			this.progressBar.Value = 0;
			this.progressBar.ValueByTransition = 0;
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.DoWork += new global::System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.ProgressChanged += new global::System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new global::System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.linkLabel1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.linkLabel1.LinkColor = global::System.Drawing.Color.White;
			this.linkLabel1.Location = new global::System.Drawing.Point(84, 344);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new global::System.Drawing.Size(239, 16);
			this.linkLabel1.TabIndex = 10;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Want to extend your licence? Click here";
			this.linkLabel1.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			base.AllowResize = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(10f, 20f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(36, 36, 36);
			base.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			base.ClientSize = new global::System.Drawing.Size(413, 378);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.progressBar);
			base.Controls.Add(this.spaceglideKeyLbl);
			base.Controls.Add(this.rememberMe);
			base.Controls.Add(this.loginStatusLbl);
			base.Controls.Add(this.registerLbl);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.usernameTxt);
			base.Controls.Add(this.passwordTxt);
			base.Controls.Add(this.loginBtn);
			base.DropShadowEffect = false;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximumSize = new global::System.Drawing.Size(429, 417);
			this.MinimumSize = new global::System.Drawing.Size(429, 417);
			base.Name = "LoginForm";
			base.ShowLeftRect = false;
			base.ShowTitle = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Style = global::MetroSet_UI.Enums.Style.Dark;
			this.Text = "spinjitzuu - login";
			base.TextColor = global::System.Drawing.Color.White;
			base.ThemeName = "MetroDark";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
			base.Load += new global::System.EventHandler(this.LoginForm_Load);
			base.Shown += new global::System.EventHandler(this.LoginForm_Shown);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000D9 RID: 217
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000DA RID: 218
		private global::MetroSet_UI.Controls.MetroSetButton loginBtn;

		// Token: 0x040000DB RID: 219
		private global::MetroSet_UI.Controls.MetroSetTextBox passwordTxt;

		// Token: 0x040000DC RID: 220
		private global::MetroSet_UI.Controls.MetroSetTextBox usernameTxt;

		// Token: 0x040000DD RID: 221
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x040000DE RID: 222
		private global::System.Windows.Forms.LinkLabel registerLbl;

		// Token: 0x040000DF RID: 223
		private global::MetroSet_UI.Controls.MetroSetLabel loginStatusLbl;

		// Token: 0x040000E0 RID: 224
		private global::MetroSet_UI.Controls.MetroSetCheckBox rememberMe;

		// Token: 0x040000E1 RID: 225
		private global::Bunifu.UI.WinForms.BunifuLabel spaceglideKeyLbl;

		// Token: 0x040000E2 RID: 226
		private global::Bunifu.UI.WinForms.BunifuProgressBar progressBar;

		// Token: 0x040000E3 RID: 227
		private global::System.ComponentModel.BackgroundWorker backgroundWorker1;

		// Token: 0x040000E4 RID: 228
		private global::System.Windows.Forms.LinkLabel linkLabel1;
	}
}
