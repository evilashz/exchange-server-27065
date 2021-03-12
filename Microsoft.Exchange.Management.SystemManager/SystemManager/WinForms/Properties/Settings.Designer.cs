using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms.Properties
{
	// Token: 0x0200010B RID: 267
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00021B25 File Offset: 0x0001FD25
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00021B2C File Offset: 0x0001FD2C
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x00021B3E File Offset: 0x0001FD3E
		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("Automatic")]
		public string EnableVisualEffects
		{
			get
			{
				return (string)this["EnableVisualEffects"];
			}
			set
			{
				this["EnableVisualEffects"] = value;
			}
		}

		// Token: 0x04000427 RID: 1063
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
