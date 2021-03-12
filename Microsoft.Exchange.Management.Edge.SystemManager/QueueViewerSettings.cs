using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000014 RID: 20
	[SettingsProvider(typeof(ExchangeSettingsProvider))]
	public class QueueViewerSettings : ExchangeSettings
	{
		// Token: 0x06000067 RID: 103 RVA: 0x0000657B File Offset: 0x0000477B
		public QueueViewerSettings(IComponent owner) : base(owner)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00006584 File Offset: 0x00004784
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00006596 File Offset: 0x00004796
		[DefaultSettingValue("00:00:30")]
		[UserScopedSetting]
		public TimeSpan RefreshInterval
		{
			get
			{
				return (TimeSpan)this["RefreshInterval"];
			}
			set
			{
				this["RefreshInterval"] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000065A9 File Offset: 0x000047A9
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000065BB File Offset: 0x000047BB
		[DefaultSettingValue("true")]
		[UserScopedSetting]
		public bool AutoRefreshEnabled
		{
			get
			{
				return (bool)this["AutoRefreshEnabled"];
			}
			set
			{
				this["AutoRefreshEnabled"] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000065CE File Offset: 0x000047CE
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000065E0 File Offset: 0x000047E0
		[DefaultSettingValue("1000")]
		[UserScopedSetting]
		public int PageSize
		{
			get
			{
				return (int)this["PageSize"];
			}
			set
			{
				this["PageSize"] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000065F3 File Offset: 0x000047F3
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00006605 File Offset: 0x00004805
		[UserScopedSetting]
		[DefaultSettingValue("false")]
		public bool UseDefaultServer
		{
			get
			{
				return (bool)this["UseDefaultServer"];
			}
			set
			{
				this["UseDefaultServer"] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00006618 File Offset: 0x00004818
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000662A File Offset: 0x0000482A
		[DefaultSettingValue("localhost")]
		[UserScopedSetting]
		public string DefaultServerName
		{
			get
			{
				return (string)this["DefaultServerName"];
			}
			set
			{
				this["DefaultServerName"] = value;
			}
		}
	}
}
