using System;
using System.Drawing;
using System.Windows.Forms;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace spinjitzuu4.Modules
{
	// Token: 0x0200003F RID: 63
	internal class GPUPixelScan
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00003304 File Offset: 0x00001504
		public static Point getPlayerPosition()
		{
			return GPUPixelScan.playerPosition;
		}

		// Token: 0x04000422 RID: 1058
		private static Factory1 factory;

		// Token: 0x04000423 RID: 1059
		private static Adapter1 adapter;

		// Token: 0x04000424 RID: 1060
		private static Output output;

		// Token: 0x04000425 RID: 1061
		private static Output1 output1;

		// Token: 0x04000426 RID: 1062
		private static int width;

		// Token: 0x04000427 RID: 1063
		private static int height;

		// Token: 0x04000428 RID: 1064
		private static Texture2DDescription textureDesc;

		// Token: 0x04000429 RID: 1065
		private static Texture2D screenTexture;

		// Token: 0x0400042A RID: 1066
		private static SharpDX.Direct3D11.Device device;

		// Token: 0x0400042B RID: 1067
		private static OutputDuplication duplicatedOutput;

		// Token: 0x0400042C RID: 1068
		public static int offsetX = 40;

		// Token: 0x0400042D RID: 1069
		public static int offsetY = 100;

		// Token: 0x0400042E RID: 1070
		public static Color MY_HP_COLOR = ColorTranslator.FromHtml("#312C00");

		// Token: 0x0400042F RID: 1071
		public static Point playerPosition = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
	}
}
