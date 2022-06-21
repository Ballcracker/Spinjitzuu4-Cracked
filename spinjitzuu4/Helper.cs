using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace spinjitzuu4
{
	// Token: 0x02000002 RID: 2
	internal class Helper
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00003540 File Offset: 0x00001740
		[UIPermission(SecurityAction.Demand, Action = SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public static Rectangle GetWindowClientRectangle(IntPtr WindowHandle)
		{
			Helper.RECT rect;
			Helper.SafeNativeMethods.GetClientRect(WindowHandle, out rect);
			Helper.POINT p;
			Helper.SafeNativeMethods.ClientToScreen(WindowHandle, out p);
			return rect.ToRectangleOffset(p);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002258 File Offset: 0x00000458
		public static Rectangle GetProcessWindowRect(string ProcessName)
		{
			return Helper.GetWindowClientRectangle(Process.GetProcessesByName(ProcessName).FirstOrDefault<Process>().MainWindowHandle);
		}

		// Token: 0x02000003 RID: 3
		public struct RECT
		{
			// Token: 0x06000006 RID: 6 RVA: 0x00002279 File Offset: 0x00000479
			public Rectangle ToRectangleOffset(Helper.POINT p)
			{
				return Rectangle.FromLTRB(p.x, p.y, this.Right + p.x, this.Bottom + p.y);
			}

			// Token: 0x04000001 RID: 1
			public int Left;

			// Token: 0x04000002 RID: 2
			public int Top;

			// Token: 0x04000003 RID: 3
			public int Right;

			// Token: 0x04000004 RID: 4
			public int Bottom;
		}

		// Token: 0x02000004 RID: 4
		public struct POINT
		{
			// Token: 0x06000008 RID: 8 RVA: 0x000022A6 File Offset: 0x000004A6
			public static implicit operator Point(Helper.POINT point)
			{
				return new Point(point.x, point.y);
			}

			// Token: 0x04000005 RID: 5
			public int x;

			// Token: 0x04000006 RID: 6
			public int y;
		}

		// Token: 0x02000005 RID: 5
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNativeMethods
		{
			// Token: 0x0600000A RID: 10
			[DllImport("User32.dll", SetLastError = true)]
			internal static extern bool ClientToScreen(IntPtr hWnd, out Helper.POINT point);

			// Token: 0x0600000B RID: 11
			[DllImport("User32.dll", SetLastError = true)]
			internal static extern bool GetClientRect(IntPtr hWnd, out Helper.RECT lpRect);

			// Token: 0x0600000C RID: 12
			[DllImport("User32.dll", SetLastError = true)]
			internal static extern bool GetWindowRect(IntPtr hwnd, out Helper.RECT lpRect);
		}
	}
}
