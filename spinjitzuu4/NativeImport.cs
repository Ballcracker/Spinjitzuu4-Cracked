using System;
using System.Runtime.InteropServices;
using System.Text;

namespace spinjitzuu4
{
	// Token: 0x02000022 RID: 34
	internal class NativeImport
	{
		// Token: 0x060000AE RID: 174
		[DllImport("User32.dll")]
		internal static extern bool ReleaseCapture();

		// Token: 0x060000AF RID: 175
		[DllImport("User32.dll")]
		public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] KeyboardDirectInput.INPUT[] pInputs, int cbSize);

		// Token: 0x060000B0 RID: 176
		[DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

		// Token: 0x060000B1 RID: 177
		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetCursorPos(int x, int y);

		// Token: 0x060000B2 RID: 178
		[DllImport("User32.dll")]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x060000B3 RID: 179
		[DllImport("User32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		// Token: 0x060000B4 RID: 180 RVA: 0x00014C08 File Offset: 0x00012E08
		public static string GetActiveWindowTitle()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (NativeImport.GetWindowText(NativeImport.GetForegroundWindow(), stringBuilder, 256) > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x060000B5 RID: 181
		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

		// Token: 0x060000B6 RID: 182
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AllocConsole();

		// Token: 0x060000B7 RID: 183
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetConsoleWindow();

		// Token: 0x060000B8 RID: 184
		[DllImport("User32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		// Token: 0x060000B9 RID: 185
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool FreeConsole();

		// Token: 0x060000BA RID: 186
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, uint hTemplateFile);

		// Token: 0x060000BB RID: 187
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

		// Token: 0x060000BC RID: 188
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

		// Token: 0x060000BD RID: 189
		[DllImport("kernel32.dll")]
		public static extern bool SetStdHandle(int nStdHandle, IntPtr hHandle);

		// Token: 0x060000BE RID: 190
		[DllImport("User32.dll")]
		public static extern ushort GetAsyncKeyState(int vKey);

		// Token: 0x060000BF RID: 191
		[DllImport("User32.dll")]
		public static extern short GetKeyState(int nVirtKey);

		// Token: 0x060000C0 RID: 192
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

		// Token: 0x060000C1 RID: 193
		[DllImport("User32.dll")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x060000C2 RID: 194
		[DllImport("User32.dll")]
		public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

		// Token: 0x060000C3 RID: 195
		[DllImport("User32.dll", SetLastError = true)]
		public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

		// Token: 0x060000C4 RID: 196
		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		// Token: 0x060000C5 RID: 197
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string moduleName);

		// Token: 0x060000C6 RID: 198
		[DllImport("User32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		// Token: 0x060000C7 RID: 199
		[DllImport("User32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);
	}
}
