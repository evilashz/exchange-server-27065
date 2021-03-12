using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Probes
{
	// Token: 0x020001D3 RID: 467
	internal static class ObserverHeartbeatResultExtension
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x00056470 File Offset: 0x00054670
		internal static bool Succeeded(this ObserverHeartbeatResult result)
		{
			if (result <= ObserverHeartbeatResult.MachineNotResponsive)
			{
				if (result != ObserverHeartbeatResult.Success && result != ObserverHeartbeatResult.MachineNotResponsive)
				{
					return false;
				}
			}
			else if (result != ObserverHeartbeatResult.MonitoringOffline && result != ObserverHeartbeatResult.NoLongerObserver)
			{
				return false;
			}
			return true;
		}
	}
}
