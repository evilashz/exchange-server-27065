using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Search.Engine;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000472 RID: 1138
	public class SearchMemoryUsageOverThresholdProbe : SearchProbeBase
	{
		// Token: 0x06001CBC RID: 7356 RVA: 0x000A8E7C File Offset: 0x000A707C
		protected override void InternalDoWork(CancellationToken cancellationToken)
		{
			long num = SearchConfig.Instance.SearchWorkingSetMemoryUsageThreshold + SearchConfig.Instance.SearchWorkingSetMemoryUsageThreshold * (long)SearchConfig.Instance.SearchWorkingSetMemoryUsageFloatingRate / 100L;
			long searchMemoryUsage = SearchMemoryModel.GetSearchMemoryUsage();
			if (searchMemoryUsage > num)
			{
				throw new SearchProbeFailureException(Strings.SearchMemoryUsageOverThreshold(((double)searchMemoryUsage / 1024.0 / 1024.0).ToString("0.00")));
			}
		}
	}
}
