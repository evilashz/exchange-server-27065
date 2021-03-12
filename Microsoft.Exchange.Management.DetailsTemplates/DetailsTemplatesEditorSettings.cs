using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200000E RID: 14
	[SettingsProvider(typeof(ExchangeSettingsProvider))]
	public class DetailsTemplatesEditorSettings : DataListViewSettings
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002D2C File Offset: 0x00000F2C
		public DetailsTemplatesEditorSettings(IComponent owner) : base(owner)
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002D35 File Offset: 0x00000F35
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002D47 File Offset: 0x00000F47
		[UserScopedSetting]
		[DefaultSettingValue("112")]
		public uint EditorXCoordinate
		{
			get
			{
				return (uint)this["EditorXCoordinate"];
			}
			set
			{
				this["EditorXCoordinate"] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D5A File Offset: 0x00000F5A
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002D6C File Offset: 0x00000F6C
		[DefaultSettingValue("84")]
		[UserScopedSetting]
		public uint EditorYCoordinate
		{
			get
			{
				return (uint)this["EditorYCoordinate"];
			}
			set
			{
				this["EditorYCoordinate"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D7F File Offset: 0x00000F7F
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002D91 File Offset: 0x00000F91
		[DefaultSettingValue("800")]
		[UserScopedSetting]
		public uint EditorWidth
		{
			get
			{
				return (uint)this["EditorWidth"];
			}
			set
			{
				this["EditorWidth"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002DA4 File Offset: 0x00000FA4
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002DB6 File Offset: 0x00000FB6
		[UserScopedSetting]
		[DefaultSettingValue("600")]
		public uint EditorHeight
		{
			get
			{
				return (uint)this["EditorHeight"];
			}
			set
			{
				this["EditorHeight"] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002DC9 File Offset: 0x00000FC9
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002DDB File Offset: 0x00000FDB
		[UserScopedSetting]
		[DefaultSettingValue("false")]
		public bool IsEditorMaximized
		{
			get
			{
				return (bool)this["IsEditorMaximized"];
			}
			set
			{
				this["IsEditorMaximized"] = value;
			}
		}
	}
}
