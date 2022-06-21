using System;
using System.Windows.Forms;

namespace spinjitzuu4
{
	// Token: 0x02000025 RID: 37
	internal static class Program
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00002B4D File Offset: 0x00000D4D
		[STAThread]
		internal static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new LoginForm());
		}
	}
}
