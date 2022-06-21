using System;
using System.Drawing;

namespace spinjitzuu4.Scripts
{
	// Token: 0x0200002A RID: 42
	internal class AutoQSS
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00016970 File Offset: 0x00014B70
		public static bool CheckCC()
		{
			Rectangle rect = new Rectangle(AutoQSS.CCPoint.X, AutoQSS.CCPoint.Y, 2, 2);
			Point[] array = PixelBot.Search(rect, AutoQSS.RGB_CC_COLOR_FEAR, 1);
			Point[] array2 = PixelBot.Search(rect, AutoQSS.RGB_CC_COLOR_SILENCE, 1);
			return array.Length != 0 || array2.Length != 0;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002DF2 File Offset: 0x00000FF2
		public static void Cleanse()
		{
			if (AutoQSS.CheckCC())
			{
				KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_1);
				KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_1);
			}
		}

		// Token: 0x04000398 RID: 920
		public static Point CCPoint = new Point(923, 460);

		// Token: 0x04000399 RID: 921
		private static readonly Color RGB_CC_COLOR_FEAR = Color.FromArgb(9, 168, 249);

		// Token: 0x0400039A RID: 922
		private static readonly Color RGB_CC_COLOR_SILENCE = Color.FromArgb(66, 63, 66);
	}
}
