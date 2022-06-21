using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums;
using MetroSet_UI.Forms;
using spinjitzuu4.Authorization;
using spinjitzuu4.Properties;

namespace spinjitzuu4
{
	// Token: 0x02000026 RID: 38
	public partial class UpgradeForm : MetroSetForm
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00002B65 File Offset: 0x00000D65
		public UpgradeForm()
		{
			base.Icon = Resources.spinlogo;
			this.InitializeComponent();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002B80 File Offset: 0x00000D80
		private void UpgradeForm_Load(object sender, EventArgs e)
		{
			this.registerStatusLbl.Text = "Connecting to the server";
			this.backgroundWorker1.RunWorkerAsync();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002B9E File Offset: 0x00000D9E
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			UpgradeForm.KeyAuthApp.init();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002BAA File Offset: 0x00000DAA
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.registerStatusLbl.Text = "Connected";
			UpgradeForm.isConnected = true;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00015E84 File Offset: 0x00014084
		private void loginBtn_Click(object sender, EventArgs e)
		{
			string text = this.usernameTxt.Text;
			string text2 = this.licenseTxt.Text;
			if (UpgradeForm.isConnected)
			{
				UpgradeForm.KeyAuthApp.upgrade(text, text2);
				if (!UpgradeForm.KeyAuthApp.response.success)
				{
					MessageBox.Show("\n Status: " + UpgradeForm.KeyAuthApp.response.message);
				}
				if (UpgradeForm.KeyAuthApp.response.success)
				{
					MessageBox.Show("\n Status: Account upgraded successfully!");
					base.Hide();
					new LoginForm().ShowDialog();
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000027E6 File Offset: 0x000009E6
		private void loginLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			base.Hide();
			new LoginForm().ShowDialog();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002515 File Offset: 0x00000715
		private void UpgradeForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			base.Dispose();
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x0400038B RID: 907
		public static bool isConnected = false;

		// Token: 0x0400038C RID: 908
		private static api KeyAuthApp = new api("spinjitzuu", "L1she6ywjL", "f04b68c9ee556d11b057c9c960df7bcd403cfa6875353baf9540981e1791fcf8", GUI.getVersion());
	}
}
