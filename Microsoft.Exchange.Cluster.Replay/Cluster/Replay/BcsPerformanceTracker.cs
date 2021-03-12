using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ReplayEventLog;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002C2 RID: 706
	internal class BcsPerformanceTracker : FailoverPerformanceTrackerBase<BcsOperation>
	{
		// Token: 0x06001B54 RID: 6996 RVA: 0x000759FE File Offset: 0x00073BFE
		public BcsPerformanceTracker(AmDbAction.PrepareSubactionArgsDelegate prepareSubAction) : base("BcsPerf")
		{
			this.m_prepareSubAction = prepareSubAction;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00075A14 File Offset: 0x00073C14
		public override void LogEvent()
		{
			if (this.m_prepareSubAction != null)
			{
				ReplayCrimsonEvents.BcsCompleted.LogGeneric(this.m_prepareSubAction(new object[]
				{
					base.GetDuration(BcsOperation.BcsOverall),
					base.GetDuration(BcsOperation.HasDatabaseBeenMounted),
					base.GetDuration(BcsOperation.GetDatabaseCopies),
					base.GetDuration(BcsOperation.DetermineServersToContact),
					base.GetDuration(BcsOperation.GetCopyStatusRpc)
				}));
			}
		}

		// Token: 0x04000B3C RID: 2876
		private AmDbAction.PrepareSubactionArgsDelegate m_prepareSubAction;
	}
}
