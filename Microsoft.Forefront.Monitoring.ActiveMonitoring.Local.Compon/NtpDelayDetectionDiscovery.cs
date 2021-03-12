using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000079 RID: 121
	public sealed class NtpDelayDetectionDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000316 RID: 790 RVA: 0x00012724 File Offset: 0x00010924
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsForefrontForOfficeDatacenter)
			{
				base.Result.StateAttribute1 = "NtpDelayDetectionDiscovery: This is not a FFO datacenter machine.";
				return;
			}
			base.Result.StateAttribute1 = "NTPDelayDetectionDiscovery: install NTP probes.";
			GenericWorkItemHelper.CreateAllDefinitions(new string[]
			{
				"NTPEventMonitor.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}
	}
}
