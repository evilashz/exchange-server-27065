using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000100 RID: 256
	internal class SkippedLogsDeleter : TimerComponent
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x0002F592 File Offset: 0x0002D792
		public SkippedLogsDeleter() : base(TimeSpan.Zero, TimeSpan.FromSeconds((double)RegistryParameters.SkippedLogsDeletionIntervalSecs), "SkippedLogsDeleter")
		{
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002F5BC File Offset: 0x0002D7BC
		public void UpdateDiscoveredConfigurations(List<ReplayConfiguration> allConfigurations)
		{
			List<ReplayConfiguration> allConfigurationsCached = new List<ReplayConfiguration>(allConfigurations);
			lock (this.m_cacheLock)
			{
				this.m_allConfigurationsCached = allConfigurationsCached;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002F604 File Offset: 0x0002D804
		protected override void TimerCallbackInternal()
		{
			List<ReplayConfiguration> list = null;
			lock (this.m_cacheLock)
			{
				if (this.m_allConfigurationsCached == null || this.m_allConfigurationsCached.Count == 0)
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug((long)this.GetHashCode(), "SkippedLogsDeleter: No configurations have been discovered, so nothing to do! Exiting.");
					return;
				}
				list = new List<ReplayConfiguration>(this.m_allConfigurationsCached);
			}
			foreach (ReplayConfiguration replayConfiguration in list)
			{
				if (base.PrepareToStopCalled)
				{
					break;
				}
				if (replayConfiguration != null && (replayConfiguration.Type == ReplayConfigType.RemoteCopySource || replayConfiguration.Type == ReplayConfigType.RemoteCopyTarget))
				{
					AgedOutDirectoryHelper.DeleteSkippedLogs(replayConfiguration.E00LogBackupPath, replayConfiguration.DatabaseName, false);
				}
			}
		}

		// Token: 0x0400044F RID: 1103
		private List<ReplayConfiguration> m_allConfigurationsCached;

		// Token: 0x04000450 RID: 1104
		private object m_cacheLock = new object();
	}
}
