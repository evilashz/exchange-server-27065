using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002A8 RID: 680
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SeederInstanceCleaner : TimerComponent
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x00071D82 File Offset: 0x0006FF82
		public SeederInstanceCleaner(SeederInstances instances) : base(TimeSpan.Zero, TimeSpan.FromMilliseconds((double)SeederInstanceCleaner.retryIntervalMilliSecs), "SeederInstanceCleaner")
		{
			this.m_instances = instances;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00071DA6 File Offset: 0x0006FFA6
		internal SeederInstanceCleaner(SeederInstances instances, int maxDurationMs) : this(instances)
		{
			SeederInstanceCleaner.maxDurationMilliSecs = maxDurationMs;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00071DB8 File Offset: 0x0006FFB8
		protected override void TimerCallbackInternal()
		{
			SeederInstanceContainer[] allInstances = this.m_instances.GetAllInstances();
			foreach (SeederInstanceContainer seederInstanceContainer in allInstances)
			{
				if (base.PrepareToStopCalled)
				{
					return;
				}
				SeederState seedState = seederInstanceContainer.SeedState;
				if (seedState == SeederState.SeedSuccessful || seedState == SeederState.SeedCancelled || seedState == SeederState.SeedFailed)
				{
					DateTime completedTimeUtc = seederInstanceContainer.CompletedTimeUtc;
					long num = (long)Math.Ceiling(DateTime.UtcNow.Subtract(completedTimeUtc).TotalMilliseconds);
					if (num >= (long)SeederInstanceCleaner.maxDurationMilliSecs)
					{
						this.m_instances.RemoveInstance(seederInstanceContainer);
						ExTraceGlobals.SeederServerTracer.TraceDebug<string, SeederState, long>((long)this.GetHashCode(), "SeederInstanceCleaner: Removed stale seed instance '{0}' in state '{1}' of age {2} secs.", seederInstanceContainer.Identity, seedState, num / 1000L);
						ReplayEventLogConstants.Tuple_SeedInstanceCleanupStale.LogEvent(null, new object[]
						{
							seederInstanceContainer.Name,
							num / 1000L
						});
					}
				}
			}
		}

		// Token: 0x04000AA4 RID: 2724
		private static int maxDurationMilliSecs = RegistryParameters.SeederInstanceStaleDuration;

		// Token: 0x04000AA5 RID: 2725
		private static int retryIntervalMilliSecs = Math.Min(SeederInstanceCleaner.maxDurationMilliSecs, 30000);

		// Token: 0x04000AA6 RID: 2726
		private SeederInstances m_instances;
	}
}
