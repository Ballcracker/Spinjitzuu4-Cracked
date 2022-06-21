using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using Bunifu.UI.WinForms;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums;
using MetroSet_UI.Forms;
using spinjitzuu4.Authorization;
using spinjitzuu4.Properties;

namespace spinjitzuu4
{
	// Token: 0x0200000A RID: 10
	public partial class LoginForm : MetroSetForm
	{
		// Token: 0x06000076 RID: 118 RVA: 0x0000286A File Offset: 0x00000A6A
		public LoginForm()
		{
			base.Icon = Resources.spinlogo;
			this.InitializeComponent();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002885 File Offset: 0x00000A85
		private void LoginForm_Load(object sender, EventArgs e)
		{
			this.loginStatusLbl.Text = "Connecting to the server";
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00013410 File Offset: 0x00011610
		public static DateTime UnixTimeToDateTime(long unixtime)
		{
			DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
			result = result.AddSeconds((double)unixtime).ToLocalTime();
			return result;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00013444 File Offset: 0x00011644
		private void InitData()
		{
			if (Settings.Default.username != string.Empty && Settings.Default.readme)
			{
				this.usernameTxt.Text = Settings.Default.username;
				this.passwordTxt.Text = Settings.Default.password;
				this.rememberMe.Checked = true;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000134AC File Offset: 0x000116AC
		private void loginBtn_Click(object sender, EventArgs e)
		{
			if (LoginForm.isConnected)
			{
				LoginForm.username = this.usernameTxt.Text;
				string text = this.passwordTxt.Text;
				if (this.rememberMe.Checked)
				{
					Settings.Default.readme = true;
					Settings.Default.username = LoginForm.username;
					Settings.Default.password = text;
					Settings.Default.Save();
				}
				if (!this.rememberMe.Checked)
				{
					Settings.Default.readme = false;
					Settings.Default.username = "";
					Settings.Default.password = "";
					Settings.Default.Save();
				}
				this.loginStatusLbl.Text = "Trying to login";
				LoginForm.KeyAuthApp.login(LoginForm.username, text);
				if (!LoginForm.KeyAuthApp.response.success)
				{
					System.Windows.MessageBox.Show("\n Status: " + LoginForm.KeyAuthApp.response.message);
				}
				if (LoginForm.KeyAuthApp.response.success)
				{
					System.Windows.MessageBox.Show("\n Status: Logged\n Hello " + LoginForm.KeyAuthApp.user_data.username);
					GUI.Username = LoginForm.KeyAuthApp.user_data.username;
					GUI.HWID = LoginForm.KeyAuthApp.user_data.hwid;
					for (int i = 0; i < LoginForm.KeyAuthApp.user_data.subscriptions.Count; i++)
					{
						Console.WriteLine(string.Concat(new string[]
						{
							" Subscription name: ",
							LoginForm.KeyAuthApp.user_data.subscriptions[i].subscription,
							" - Expires at: ",
							LoginForm.UnixTimeToDateTime(long.Parse(LoginForm.KeyAuthApp.user_data.subscriptions[i].expiry)).ToString(),
							" - Time left in seconds: ",
							LoginForm.KeyAuthApp.user_data.subscriptions[i].timeleft
						}));
						GUI.Licence_Name = LoginForm.KeyAuthApp.user_data.subscriptions[0].subscription;
						GUI.Licence_Expiry = LoginForm.UnixTimeToDateTime(long.Parse(LoginForm.KeyAuthApp.user_data.subscriptions[i].expiry)).ToString();
						GUI.Licence_Timeleft = LoginForm.KeyAuthApp.user_data.subscriptions[i].timeleft;
					}
					base.Hide();
					new GUI().ShowDialog();
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002898 File Offset: 0x00000A98
		private void registerLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			base.Hide();
			new RegisterForm().ShowDialog();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00013744 File Offset: 0x00011944
		private void autoUpdate()
		{
			if (LoginForm.KeyAuthApp.response.message == "invalidver")
			{
				if (!string.IsNullOrEmpty(LoginForm.KeyAuthApp.app_data.downloadLink))
				{
					this.loginStatusLbl.Text = " Downloading new version directly..";
					this.loginStatusLbl.Text = " New version will be opened shortly..";
					WebClient webClient = new WebClient();
					string executablePath = System.Windows.Forms.Application.ExecutablePath;
					webClient.DownloadFile(LoginForm.KeyAuthApp.app_data.downloadLink, executablePath);
					Process.Start(executablePath);
					Environment.Exit(0);
				}
				System.Windows.MessageBox.Show("\n Status: Version of this program does not match the one online. Furthermore, the download link online isn't set. You will need to manually obtain the download link from the developer.");
				Environment.Exit(0);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000137E4 File Offset: 0x000119E4
		private static string random_string()
		{
			string text = null;
			Random random = new Random();
			for (int i = 0; i < 5; i++)
			{
				text += Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * random.NextDouble() + 65.0))).ToString();
			}
			return text;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000028AB File Offset: 0x00000AAB
		private void LoginForm_Shown(object sender, EventArgs e)
		{
			this.progressBar.Value = 0;
			this.progressBar.Maximum = 100;
			this.backgroundWorker1.RunWorkerAsync();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000028D1 File Offset: 0x00000AD1
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			LoginForm.KeyAuthApp.init();
			this.backgroundWorker1.ReportProgress(0);
			this.autoUpdate();
			this.backgroundWorker1.ReportProgress(0);
			this.InitData();
			this.backgroundWorker1.ReportProgress(0);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000290D File Offset: 0x00000B0D
		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progressBar.Value += 33;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002923 File Offset: 0x00000B23
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.loginStatusLbl.Text = "Connected";
			LoginForm.isConnected = true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000293C File Offset: 0x00000B3C
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			base.Hide();
			new UpgradeForm().ShowDialog();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002515 File Offset: 0x00000715
		private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			base.Dispose();
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x040000D6 RID: 214
		public static string username;

		// Token: 0x040000D7 RID: 215
		public static bool isConnected = false;

		// Token: 0x040000D8 RID: 216
		private static api KeyAuthApp = new api("spinjitzuu", "L1she6ywjL", "f04b68c9ee556d11b057c9c960df7bcd403cfa6875353baf9540981e1791fcf8", GUI.getVersion());
	}
}
