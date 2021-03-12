using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000478 RID: 1144
	public class SearchFeedingControllerFailureMonitor : OverallXFailuresMonitor
	{
		// Token: 0x06001CDA RID: 7386 RVA: 0x000A9704 File Offset: 0x000A7904
		protected override void OnAlert()
		{
			base.Result.StateAttribute2 = SearchMonitoringHelper.GetAllLocalDatabaseCopyStatusString();
		}
	}
}
