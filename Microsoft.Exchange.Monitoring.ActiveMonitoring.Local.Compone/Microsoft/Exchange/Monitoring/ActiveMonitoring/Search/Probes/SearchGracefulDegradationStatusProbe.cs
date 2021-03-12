using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000473 RID: 1139
	public class SearchGracefulDegradationStatusProbe : SearchProbeBase
	{
		// Token: 0x06001CBE RID: 7358 RVA: 0x000A8EF0 File Offset: 0x000A70F0
		protected override void InternalDoWork(CancellationToken cancellationToken)
		{
			DateTime? dateTime = null;
			try
			{
				dateTime = SearchMonitoringHelper.GetRecentGracefulDegradationExecutionTime();
			}
			catch (TimeoutException innerException)
			{
				throw new SearchProbeFailureException(Strings.SearchGetDiagnosticInfoTimeout((int)SearchMonitoringHelper.GetDiagnosticInfoCallTimeout.TotalSeconds), innerException);
			}
			if (dateTime != null && dateTime < DateTime.UtcNow - TimeSpan.FromMinutes(60.0))
			{
				throw new SearchProbeFailureException(Strings.SearchGracefulDegradationStatus(dateTime.ToString()));
			}
		}
	}
}
