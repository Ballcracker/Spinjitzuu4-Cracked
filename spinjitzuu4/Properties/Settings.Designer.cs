using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace spinjitzuu4.Properties
{
	// Token: 0x02000028 RID: 40
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.1.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00002D5C File Offset: 0x00000F5C
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00002D63 File Offset: 0x00000F63
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00002D76 File Offset: 0x00000F76
		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string username
		{
			get
			{
				return (string)this["username"];
			}
			set
			{
				this["username"] = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002D85 File Offset: 0x00000F85
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00002D98 File Offset: 0x00000F98
		[DefaultSettingValue("")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public string password
		{
			get
			{
				return (string)this["password"];
			}
			set
			{
				this["password"] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002DA7 File Offset: 0x00000FA7
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00002DBA File Offset: 0x00000FBA
		[UserScopedSetting]
		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		public bool readme
		{
			get
			{
				return (bool)this["readme"];
			}
			set
			{
				this["readme"] = value;
			}
		}

		// Token: 0x04000397 RID: 919
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
