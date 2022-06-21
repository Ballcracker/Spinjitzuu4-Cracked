using System;
using System.Runtime.InteropServices;

namespace spinjitzuu4
{
	// Token: 0x0200001F RID: 31
	internal class Mouse
	{
		// Token: 0x060000A3 RID: 163
		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetCursorPos(int x, int y);

		// Token: 0x060000A4 RID: 164
		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(out Mouse.MousePoint lpMousePoint);

		// Token: 0x060000A5 RID: 165
		[DllImport("User32.dll")]
		private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

		// Token: 0x060000A6 RID: 166 RVA: 0x00002AF3 File Offset: 0x00000CF3
		public static void SetCursorPosition(int x, int y)
		{
			Mouse.SetCursorPos(x, y);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002AFD File Offset: 0x00000CFD
		public static void SetCursorPosition(Mouse.MousePoint point)
		{
			Mouse.SetCursorPos(point.X, point.Y);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00014BC0 File Offset: 0x00012DC0
		public static Mouse.MousePoint GetCursorPosition()
		{
			Mouse.MousePoint result;
			if (!Mouse.GetCursorPos(out result))
			{
				result = new Mouse.MousePoint(0, 0);
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00014BE0 File Offset: 0x00012DE0
		public static void MouseEvent(Mouse.MouseEventFlags value)
		{
			Mouse.MousePoint cursorPosition = Mouse.GetCursorPosition();
			Mouse.mouse_event((int)value, cursorPosition.X, cursorPosition.Y, 0, 0);
		}

		// Token: 0x02000020 RID: 32
		[Flags]
		public enum MouseEventFlags
		{
			// Token: 0x0400036B RID: 875
			LeftDown = 2,
			// Token: 0x0400036C RID: 876
			LeftUp = 4,
			// Token: 0x0400036D RID: 877
			MiddleDown = 32,
			// Token: 0x0400036E RID: 878
			MiddleUp = 64,
			// Token: 0x0400036F RID: 879
			Move = 1,
			// Token: 0x04000370 RID: 880
			Absolute = 32768,
			// Token: 0x04000371 RID: 881
			RightDown = 8,
			// Token: 0x04000372 RID: 882
			RightUp = 16
		}

		// Token: 0x02000021 RID: 33
		public struct MousePoint
		{
			// Token: 0x060000AC RID: 172 RVA: 0x00002B11 File Offset: 0x00000D11
			public MousePoint(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}

			// Token: 0x04000373 RID: 883
			public int X;

			// Token: 0x04000374 RID: 884
			public int Y;
		}
	}
}
