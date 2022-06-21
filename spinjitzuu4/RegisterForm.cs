using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums;
using MetroSet_UI.Forms;
using spinjitzuu4.Authorization;
using spinjitzuu4.Properties;

namespace spinjitzuu4
{
	// Token: 0x02000009 RID: 9
	public partial class RegisterForm : MetroSetForm
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000027AD File Offset: 0x000009AD
		public RegisterForm()
		{
			base.Icon = Resources.spinlogo;
			this.InitializeComponent();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000027C8 File Offset: 0x000009C8
		private void RegisterForm_Load(object sender, EventArgs e)
		{
			this.registerStatusLbl.Text = "Connecting to the server";
			this.backgroundWorker1.RunWorkerAsync();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00012804 File Offset: 0x00010A04
		private void RegisterBtn_Click(object sender, EventArgs e)
		{
			string text = this.usernameTxt.Text;
			string text2 = this.passwordTxt.Text;
			string text3 = this.licenceTxt.Text;
			if (RegisterForm.isConnected)
			{
				RegisterForm.KeyAuthApp.register(text, text2, text3);
				if (!RegisterForm.KeyAuthApp.response.success)
				{
					System.Windows.MessageBox.Show("\n Status: " + RegisterForm.KeyAuthApp.response.message);
				}
				if (RegisterForm.KeyAuthApp.response.success)
				{
					System.Windows.MessageBox.Show("\n Status: Account created successfully!");
					base.Hide();
					new LoginForm().ShowDialog();
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000027E6 File Offset: 0x000009E6
		private void loginLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			base.Hide();
			new LoginForm().ShowDialog();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000025A2 File Offset: 0x000007A2
		private void metroSetTextBox1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000027F9 File Offset: 0x000009F9
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			RegisterForm.KeyAuthApp.init();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002805 File Offset: 0x00000A05
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.registerStatusLbl.Text = "Connected";
			RegisterForm.isConnected = true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002515 File Offset: 0x00000715
		private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			base.Dispose();
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x040000CB RID: 203
		public static bool isConnected = false;

		// Token: 0x040000CC RID: 204
		private static api KeyAuthApp = new api("spinjitzuu", "L1she6ywjL", "f04b68c9ee556d11b057c9c960df7bcd403cfa6875353baf9540981e1791fcf8", GUI.getVersion());
	}
}
