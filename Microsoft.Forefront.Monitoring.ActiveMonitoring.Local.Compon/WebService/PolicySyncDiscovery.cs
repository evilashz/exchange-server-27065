using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x02000285 RID: 645
	public sealed class PolicySyncDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600151F RID: 5407 RVA: 0x000410F8 File Offset: 0x0003F2F8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (FfoLocalEndpointManager.IsWebServiceInstalled)
			{
				GenericWorkItemHelper.CreateAllDefinitions(new string[]
				{
					"PolicySyncProbes.xml"
				}, base.Broker, base.TraceContext, base.Result);
			}
		}
	}
}
