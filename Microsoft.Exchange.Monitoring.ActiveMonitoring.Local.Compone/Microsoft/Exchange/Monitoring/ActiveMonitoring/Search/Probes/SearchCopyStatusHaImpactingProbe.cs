using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000470 RID: 1136
	public class SearchCopyStatusHaImpactingProbe : SearchProbeBase
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x000A8D1D File Offset: 0x000A6F1D
		protected override bool SkipOnAutoDagExcludeFromMonitoring
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000A8D20 File Offset: 0x000A6F20
		protected override void InternalDoWork(CancellationToken cancellationToken)
		{
			string targetResource = base.Definition.TargetResource;
			IndexStatus indexStatus = null;
			try
			{
				indexStatus = SearchMonitoringHelper.GetCachedLocalDatabaseIndexStatus(targetResource, true);
			}
			catch (IndexStatusException ex)
			{
				base.Result.StateAttribute1 = "IndexStatusException";
				throw new SearchProbeFailureException(new LocalizedString(ex.ToString()));
			}
			base.Result.StateAttribute1 = indexStatus.IndexingState.ToString();
			if (indexStatus.IndexingState != ContentIndexStatusType.Healthy && indexStatus.IndexingState != ContentIndexStatusType.HealthyAndUpgrading && indexStatus.IndexingState != ContentIndexStatusType.AutoSuspended)
			{
				throw new SearchProbeFailureException(new LocalizedString(indexStatus.IndexingState.ToString()));
			}
		}
	}
}
