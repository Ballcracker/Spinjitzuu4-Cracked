using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace spinjitzuu4.Properties
{
	// Token: 0x02000027 RID: 39
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	internal class Resources
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000226F File Offset: 0x0000046F
		internal Resources()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002C0F File Offset: 0x00000E0F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("spinjitzuu4.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000004 RID: 4
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00002C3C File Offset: 0x00000E3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002C44 File Offset: 0x00000E44
		internal static Bitmap banner
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("banner", Resources.resourceCulture);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002C60 File Offset: 0x00000E60
		internal static Bitmap gangster
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("gangster", Resources.resourceCulture);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002C7C File Offset: 0x00000E7C
		internal static Bitmap healthbar
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("healthbar", Resources.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002C98 File Offset: 0x00000E98
		internal static Icon icon
		{
			get
			{
				return (Icon)Resources.ResourceManager.GetObject("icon", Resources.resourceCulture);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002CB4 File Offset: 0x00000EB4
		internal static Bitmap logo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002CD0 File Offset: 0x00000ED0
		internal static byte[] r3fynpbzgn061
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("r3fynpbzgn061", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002CEC File Offset: 0x00000EEC
		internal static Bitmap spinjitzuuLogo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("spinjitzuuLogo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002D08 File Offset: 0x00000F08
		internal static Icon spinlogo
		{
			get
			{
				return (Icon)Resources.ResourceManager.GetObject("spinlogo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002D24 File Offset: 0x00000F24
		internal static Bitmap watermark
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("watermark", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002D40 File Offset: 0x00000F40
		internal static Bitmap xx
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("xx", Resources.resourceCulture);
			}
		}

		// Token: 0x04000395 RID: 917
		private static ResourceManager resourceMan;

		// Token: 0x04000396 RID: 918
		private static CultureInfo resourceCulture;
	}
}
