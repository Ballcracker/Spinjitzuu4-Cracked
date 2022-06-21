using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using spinjitzuu4.Properties;

namespace spinjitzuu4
{
	// Token: 0x02000023 RID: 35
	internal class PixelBot
	{
		// Token: 0x060000CA RID: 202
		[DllImport("User32.dll")]
		private static extern IntPtr GetDC(IntPtr hwnd);

		// Token: 0x060000CB RID: 203
		[DllImport("User32.dll")]
		private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

		// Token: 0x060000CC RID: 204
		[DllImport("gdi32.dll")]
		private static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

		// Token: 0x060000CD RID: 205 RVA: 0x00014C3C File Offset: 0x00012E3C
		public static Color GetPixelColor(int x, int y)
		{
			IntPtr dc = PixelBot.GetDC(IntPtr.Zero);
			uint pixel = PixelBot.GetPixel(dc, x, y);
			PixelBot.ReleaseDC(IntPtr.Zero, dc);
			return Color.FromArgb((int)(pixel & 255U), (int)(pixel & 65280U) >> 8, (int)(pixel & 16711680U) >> 16);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00014C88 File Offset: 0x00012E88
		public Color GetColorAt(int x, int y)
		{
			Rectangle rectangle = new Rectangle(x, y, 1, 1);
			using (Graphics graphics = Graphics.FromImage(this.bmp))
			{
				graphics.CopyFromScreen(rectangle.Location, Point.Empty, rectangle.Size);
			}
			return this.bmp.GetPixel(0, 0);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00014CF0 File Offset: 0x00012EF0
		public unsafe Point[] PixelSearch(Rectangle rect, Color PixelColor, int ShadeVariation)
		{
			ArrayList arrayList = new ArrayList();
			Point[] result;
			using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb))
			{
				if (this.monitor >= Screen.AllScreens.Length)
				{
					this.monitor = 0;
				}
				int left = Screen.AllScreens[this.monitor].Bounds.Left;
				int top = Screen.AllScreens[this.monitor].Bounds.Top;
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.CopyFromScreen(rect.X + left, rect.Y + top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
				}
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
				int[] array = new int[]
				{
					(int)PixelColor.B,
					(int)PixelColor.G,
					(int)PixelColor.R
				};
				for (int i = 0; i < bitmapData.Height; i++)
				{
					byte* ptr = (byte*)((void*)bitmapData.Scan0) + i * bitmapData.Stride;
					for (int j = 0; j < bitmapData.Width; j++)
					{
						if (((int)ptr[j * 3] >= array[0] - ShadeVariation & (int)ptr[j * 3] <= array[0] + ShadeVariation) && ((int)ptr[j * 3 + 1] >= array[1] - ShadeVariation & (int)ptr[j * 3 + 1] <= array[1] + ShadeVariation) && ((int)ptr[j * 3 + 2] >= array[2] - ShadeVariation & (int)ptr[j * 3 + 2] <= array[2] + ShadeVariation))
						{
							arrayList.Add(new Point(j + rect.X, i + rect.Y));
						}
					}
				}
				result = (Point[])arrayList.ToArray(typeof(Point));
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208
		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

		// Token: 0x060000D1 RID: 209 RVA: 0x00014F40 File Offset: 0x00013140
		public static Bitmap GetScreenCapture(Rectangle fov)
		{
			Bitmap bitmap = new Bitmap(fov.Width, fov.Height, PixelFormat.Format24bppRgb);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				using (Graphics graphics2 = Graphics.FromHwnd(IntPtr.Zero))
				{
					IntPtr hdc = graphics2.GetHdc();
					PixelBot.BitBlt(graphics.GetHdc(), 0, 0, fov.Width, fov.Height, hdc, fov.X, fov.Y, 13369376);
					graphics.ReleaseHdc();
					graphics2.ReleaseHdc();
				}
			}
			return bitmap;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00014FF0 File Offset: 0x000131F0
		public unsafe static Point[] Search(Rectangle rect, Color Pixel_Color, int Shade_Variation)
		{
			ArrayList arrayList = new ArrayList();
			Bitmap screenCapture = PixelBot.GetScreenCapture(rect);
			BitmapData bitmapData = screenCapture.LockBits(new Rectangle(0, 0, screenCapture.Width, screenCapture.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int[] array = new int[]
			{
				(int)Pixel_Color.B,
				(int)Pixel_Color.G,
				(int)Pixel_Color.R
			};
			for (int i = 0; i < bitmapData.Height; i++)
			{
				byte* ptr = (byte*)((void*)bitmapData.Scan0) + i * bitmapData.Stride;
				for (int j = 0; j < bitmapData.Width; j++)
				{
					if (((int)ptr[j * 3] >= array[0] - Shade_Variation & (int)ptr[j * 3] <= array[0] + Shade_Variation) && ((int)ptr[j * 3 + 1] >= array[1] - Shade_Variation & (int)ptr[j * 3 + 1] <= array[1] + Shade_Variation) && ((int)ptr[j * 3 + 2] >= array[2] - Shade_Variation & (int)ptr[j * 3 + 2] <= array[2] + Shade_Variation))
					{
						arrayList.Add(new Point(j + rect.X, i + rect.Y));
					}
				}
			}
			screenCapture.Dispose();
			return (Point[])arrayList.ToArray(typeof(Point));
		}

		// Token: 0x060000D3 RID: 211
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr FindWindow(string strClassName, string strWindowName);

		// Token: 0x060000D4 RID: 212
		[DllImport("User32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref Rectangle rectangle);

		// Token: 0x060000D5 RID: 213 RVA: 0x0001515C File Offset: 0x0001335C
		public static Point GetEnemyPositionCenterDynamicScreen()
		{
			PixelBot.LeagueRect = Helper.GetProcessWindowRect("League of Legends");
			double num = (double)Screen.PrimaryScreen.Bounds.Width;
			int height = Screen.PrimaryScreen.Bounds.Height;
			float num2 = float.Parse(API.readAttackRange());
			if (num >= 1440.0)
			{
				if (num2 >= 525f && num2 < 650f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 400, PixelBot.LeagueRect.Y + 50, 1400, 1100), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else if (num2 >= 650f && num2 < 850f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 350, PixelBot.LeagueRect.Y + 35, 1700, 1100), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 150, PixelBot.LeagueRect.Y, 2300, 1200), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
			}
			else if (num == 1080.0)
			{
				if (num2 >= 525f && num2 < 650f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 450, PixelBot.LeagueRect.Y + 70, 910, 750), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else if (num2 >= 650f && num2 < 850f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 385, PixelBot.LeagueRect.Y + 35, 1100, 875), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 200, PixelBot.LeagueRect.Y, 1600, 900), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
			}
			else if (num <= 900.0)
			{
				if (num2 >= 525f && num2 < 650f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 330, PixelBot.LeagueRect.Y + 35, 895, 740), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else if (num2 >= 650f && num2 < 850f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 310, PixelBot.LeagueRect.Y + 15, 935, 740), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(PixelBot.LeagueRect.X + 200, PixelBot.LeagueRect.Y, 1200, 740), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
			}
			Point result = default(Point);
			int height2 = Screen.PrimaryScreen.Bounds.Height;
			int width = Screen.PrimaryScreen.Bounds.Width;
			if (PixelBot.Searched.Length != 0)
			{
				double[] array = new double[PixelBot.Searched.Length];
				Point point = new Point(width / 2, height2 / 2);
				int num3 = 0;
				foreach (Point point2 in PixelBot.Searched)
				{
					double num4 = Math.Sqrt((double)((Math.Abs(point2.X - point.X) ^ 2) + (Math.Abs(point2.Y - point.Y) ^ 2)));
					array[num3] = num4;
					num3++;
				}
				double value = array.Min();
				int num5 = Convert.ToInt32((double)Array.IndexOf<double>(array, value));
				result = new Point(PixelBot.Searched[num5].X + PixelBot.offsetX, PixelBot.Searched[num5].Y + PixelBot.offsetY);
			}
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00015584 File Offset: 0x00013784
		public static Point GetEnemyPositionLegacy()
		{
			PixelBot.LeagueRect = Helper.GetProcessWindowRect("League of Legends");
			Rectangle rect = new Rectangle(PixelBot.LeagueRect.X, PixelBot.LeagueRect.Y, PixelBot.LeagueRect.Width, PixelBot.LeagueRect.Height);
			Convert.ToInt32((double)float.Parse(API.readAttackRange()) / 1.5);
			PixelBot.Searched = PixelBot.Search(rect, PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
			Point result = default(Point);
			int height = Screen.PrimaryScreen.Bounds.Height;
			int width = Screen.PrimaryScreen.Bounds.Width;
			if (PixelBot.Searched.Length != 0)
			{
				double[] array = new double[PixelBot.Searched.Length];
				int num = 0;
				foreach (Point point in PixelBot.Searched)
				{
					double num2 = Math.Sqrt((double)((Math.Abs(point.X - PixelBot.playerPosition.X) ^ 2) + (Math.Abs(point.Y - PixelBot.playerPosition.Y) ^ 2)));
					array[num] = num2;
					num++;
				}
				double value = array.Min();
				int num3 = Convert.ToInt32((double)Array.IndexOf<double>(array, value));
				result = new Point(PixelBot.Searched[num3].X + PixelBot.offsetX, PixelBot.Searched[num3].Y + PixelBot.offsetY);
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002B23 File Offset: 0x00000D23
		public static Color scanHealthBarImage(int point)
		{
			return new Bitmap(Resources.healthbar).GetPixel(point, 0);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000156F8 File Offset: 0x000138F8
		private unsafe void ProcessUsingLockbitsAndUnsafeAndParallel(Bitmap processedBitmap)
		{
			BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
			int bytesPerPixel = Image.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
			int height = bitmapData.Height;
			int widthInBytes = bitmapData.Width * bytesPerPixel;
			byte* PtrFirstPixel = (byte*)((void*)bitmapData.Scan0);
			Parallel.For(0, height, delegate(int y)
			{
				byte* ptr = PtrFirstPixel + y * bitmapData.Stride;
				for (int i = 0; i < widthInBytes; i += bytesPerPixel)
				{
					int num = (int)ptr[i];
					int num2 = (int)ptr[i + 1];
					int num3 = (int)ptr[i + 2];
					ptr[i] = (byte)num;
					ptr[i + 1] = (byte)num2;
					ptr[i + 2] = (byte)num3;
				}
			});
			processedBitmap.UnlockBits(bitmapData);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000157A0 File Offset: 0x000139A0
		private static Color GetPixel(Point position)
		{
			Color pixel;
			using (Bitmap bitmap = new Bitmap(1, 1))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.CopyFromScreen(position, new Point(0, 0), new Size(1, 1));
				}
				pixel = bitmap.GetPixel(0, 0);
			}
			return pixel;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00015810 File Offset: 0x00013A10
		public static int GetLowestHP(Point[] points)
		{
			foreach (Point point in points)
			{
				for (int j = 98; j > 0; j--)
				{
					if (PixelBot.GetPixelColor(point.X + j, point.Y) == PixelBot.scanHealthBarImage(j))
					{
						return j;
					}
				}
			}
			return 0;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00015868 File Offset: 0x00013A68
		public static Point TargetEnemy(Point[] Searched)
		{
			int height = Screen.PrimaryScreen.Bounds.Height;
			int width = Screen.PrimaryScreen.Bounds.Width;
			string a = "lowhp";
			if (a == "closest")
			{
				double[] array = new double[Searched.Length];
				Point point = new Point(width / 2, height / 2);
				int num = 0;
				foreach (Point point2 in Searched)
				{
					double num2 = Math.Sqrt((double)((Math.Abs(point2.X - point.X) ^ 2) + (Math.Abs(point2.Y - point.Y) ^ 2)));
					array[num] = num2;
					num++;
				}
				double value = array.Min();
				int num3 = Convert.ToInt32((double)Array.IndexOf<double>(array, value));
				return new Point(Searched[num3].X + 53, Searched[num3].Y + 100);
			}
			if (a == "lowhp")
			{
				int[] array2 = new int[Searched.Length];
				int num4 = 0;
				for (int i = 0; i < Searched.Length; i++)
				{
					num4++;
				}
				int value2 = array2.Min();
				int num5 = Array.IndexOf<int>(array2, value2);
				return new Point(Searched[num5].X + 53, Searched[num5].Y + 100);
			}
			return Point.Empty;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000159E8 File Offset: 0x00013BE8
		public static Point GetEnemyPositionCenter(double screenWidth)
		{
			float num = float.Parse(API.readAttackRange());
			if (screenWidth >= 1440.0)
			{
				if (num >= 525f && num < 650f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(400, 50, 1400, 1100), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else if (num >= 650f && num < 850f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(350, 35, 1700, 1100), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(150, 0, 2300, 1200), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
			}
			else if (screenWidth == 1080.0)
			{
				if (num >= 525f && num < 650f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(450, 70, 910, 750), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else if (num >= 650f && num < 850f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(385, 35, 1100, 875), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(200, 0, 1600, 900), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
			}
			else if (screenWidth <= 900.0)
			{
				if (num >= 525f && num < 650f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(330, 35, 895, 740), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else if (num >= 650f && num < 850f)
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(310, 15, 935, 740), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
				else
				{
					PixelBot.Searched = PixelBot.Search(new Rectangle(200, 0, 1200, 740), PixelBot.RGB_ENEMY_LEFT_COLOR, 0);
				}
			}
			Point result = default(Point);
			int height = Screen.PrimaryScreen.Bounds.Height;
			int width = Screen.PrimaryScreen.Bounds.Width;
			if (PixelBot.Searched.Length != 0)
			{
				double[] array = new double[PixelBot.Searched.Length];
				Point point = new Point(width / 2, height / 2);
				int num2 = 0;
				foreach (Point point2 in PixelBot.Searched)
				{
					double num3 = Math.Sqrt((double)((Math.Abs(point2.X - point.X) ^ 2) + (Math.Abs(point2.Y - point.Y) ^ 2)));
					array[num2] = num3;
					num2++;
				}
				double value = array.Min();
				int num4 = Convert.ToInt32((double)Array.IndexOf<double>(array, value));
				result = new Point(PixelBot.Searched[num4].X + PixelBot.offsetX, PixelBot.Searched[num4].Y + PixelBot.offsetY);
			}
			return result;
		}

		// Token: 0x04000375 RID: 885
		public static Rectangle LeagueRect;

		// Token: 0x04000376 RID: 886
		private int monitor;

		// Token: 0x04000377 RID: 887
		private Bitmap bmp = new Bitmap(1, 1);

		// Token: 0x04000378 RID: 888
		private static readonly Color RGB_ENEMY_LEVEL_NUMBER_COLOR = Color.FromArgb(203, 98, 88);

		// Token: 0x04000379 RID: 889
		private static readonly Color RGB_ENEMY_LEVEL_EMPTY_COLOR = Color.FromArgb(11, 11, 9);

		// Token: 0x0400037A RID: 890
		private static Point[] Searched;

		// Token: 0x0400037B RID: 891
		private static Rectangle FOV;

		// Token: 0x0400037C RID: 892
		public static int screenX = Screen.PrimaryScreen.Bounds.X;

		// Token: 0x0400037D RID: 893
		public static int screenY = Screen.PrimaryScreen.Bounds.Y;

		// Token: 0x0400037E RID: 894
		public static int offsetX = 40;

		// Token: 0x0400037F RID: 895
		public static int offsetY = 100;

		// Token: 0x04000380 RID: 896
		public static Point playerPosition = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);

		// Token: 0x04000381 RID: 897
		public static Color RGB_ENEMY_COLOR = ColorTranslator.FromHtml("#3A0400");

		// Token: 0x04000382 RID: 898
		private static Color RGB_ENEMY_HP_BAR_COLOR = Color.FromArgb(148, 36, 24);

		// Token: 0x04000383 RID: 899
		private static Color RGB_ENEMY_LEVEL_COLOR = Color.FromArgb(53, 3, 0);

		// Token: 0x04000384 RID: 900
		private static Color RGB_ENEMY_MINION_HP_BAR_COLOR = Color.FromArgb(121, 57, 55);

		// Token: 0x04000385 RID: 901
		public static readonly Color RGB_ENEMY_LEFT_COLOR = Color.FromArgb(203, 97, 88);

		// Token: 0x04000386 RID: 902
		public static readonly Color RGB_PLAYER_LEFT_COLOR = Color.FromArgb(228, 203, 75);
	}
}
