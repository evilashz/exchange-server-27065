using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200001D RID: 29
	internal class AmClusterServiceMonitor : AmServiceMonitor
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000747B File Offset: 0x0000567B
		internal AmClusterServiceMonitor() : base("Clussvc")
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007488 File Offset: 0x00005688
		protected override void OnStop()
		{
			AmTrace.Debug("Cluster service stop detected. Notifying system manager", new object[0]);
			AmEvtClussvcStopped amEvtClussvcStopped = new AmEvtClussvcStopped();
			amEvtClussvcStopped.Notify(true);
			AmSystemManager.Instance.ConfigManager.TriggerRefresh(true);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000074C3 File Offset: 0x000056C3
		protected override void OnStart()
		{
			AmTrace.Debug("Cluster service start detected. Notifying config manager to refresh configuration", new object[0]);
			AmSystemManager.Instance.ConfigManager.TriggerRefresh(true);
		}
	}
}
