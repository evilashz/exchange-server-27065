using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200009E RID: 158
	internal sealed class StartupNotificationWatcher : CrimsonWatcher<StartupNotification>
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x0002027D File Offset: 0x0001E47D
		public StartupNotificationWatcher() : base(null, true, "Microsoft-Exchange-ManagedAvailability/StartupNotification")
		{
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0002028C File Offset: 0x0001E48C
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00020294 File Offset: 0x0001E494
		public StartupNotificationWatcher.StartupNotificationArrivedDelegate StartupNotificationArrivedCallback { get; set; }

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002029D File Offset: 0x0001E49D
		protected override void ResultArrivedHandler(StartupNotification startupNotification)
		{
			if (this.StartupNotificationArrivedCallback != null)
			{
				this.StartupNotificationArrivedCallback(startupNotification);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000202B4 File Offset: 0x0001E4B4
		protected override string GetDefaultXPathQuery()
		{
			return CrimsonHelper.BuildXPathQueryString(base.ChannelName, null, base.QueryStartTime, base.QueryEndTime, base.QueryUserPropertyCondition);
		}

		// Token: 0x0200009F RID: 159
		// (Invoke) Token: 0x060007BB RID: 1979
		public delegate void StartupNotificationArrivedDelegate(StartupNotification startupNotification);
	}
}
