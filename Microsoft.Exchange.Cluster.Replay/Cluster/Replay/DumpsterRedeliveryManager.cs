using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay.Dumpster;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000E4 RID: 228
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DumpsterRedeliveryManager : TimerComponent
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x0002B5D6 File Offset: 0x000297D6
		public DumpsterRedeliveryManager() : base(TimeSpan.Zero, TimeSpan.FromSeconds((double)RegistryParameters.DumpsterRedeliveryManagerTimerIntervalInSecs), "DumpsterRedeliveryManager")
		{
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002B60C File Offset: 0x0002980C
		public void UpdateDiscoveredConfigurations(List<ReplayConfiguration> allConfigurations)
		{
			List<ReplayConfiguration> allConfigurationsCached = new List<ReplayConfiguration>(allConfigurations);
			lock (this.m_cacheLock)
			{
				this.m_allConfigurationsCached = allConfigurationsCached;
			}
			ExTraceGlobals.DumpsterTracer.TraceDebug((long)this.GetHashCode(), "DumpsterRedeliveryManager: Dumpster configuration was updated. Updater loop will now be inititated.");
			base.StartNow();
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0002B670 File Offset: 0x00029870
		protected override void TimerCallbackInternal()
		{
			List<ReplayConfiguration> list = null;
			lock (this.m_cacheLock)
			{
				if (this.m_allConfigurationsCached == null || this.m_allConfigurationsCached.Count == 0)
				{
					ExTraceGlobals.DumpsterTracer.TraceDebug((long)this.GetHashCode(), "DumpsterRedeliveryManager: No configurations have been discovered, so nothing to do! Exiting.");
					return;
				}
				list = new List<ReplayConfiguration>(this.m_allConfigurationsCached);
			}
			lock (this)
			{
				if (!base.PrepareToStopCalled)
				{
					foreach (ReplayConfiguration replayConfiguration in list)
					{
						if (replayConfiguration != null)
						{
							if (replayConfiguration.Type == ReplayConfigType.RemoteCopySource && replayConfiguration.IsSourceMachineEqual(AmServerName.LocalComputerName))
							{
								Exception ex = null;
								try
								{
									if (DumpsterRedeliveryWrapper.IsRedeliveryRequired(replayConfiguration))
									{
										ExTraceGlobals.DumpsterTracer.TraceDebug<string, string>(0L, "DumpsterRedeliveryManager: Queued dumpster work item for: {0}({1})", replayConfiguration.Name, replayConfiguration.Identity);
										this.m_numOutstandingRequests++;
										this.m_doneEvent.Reset();
										ThreadPool.QueueUserWorkItem(new WaitCallback(this.ScheduleDumpsterRedelivery), replayConfiguration);
									}
									else
									{
										ExTraceGlobals.DumpsterTracer.TraceDebug<string, string>(0L, "DumpsterRedeliveryManager: Skipping dumpster request for {0}({1}) since DumpsterRedeliveryRequired is 'false'.", replayConfiguration.Name, replayConfiguration.Identity);
									}
								}
								catch (IOException ex2)
								{
									ex = ex2;
								}
								catch (ClusterException ex3)
								{
									ex = ex3;
								}
								catch (DumpsterRedeliveryException ex4)
								{
									ex = ex4;
								}
								if (ex != null)
								{
									ExTraceGlobals.DumpsterTracer.TraceError<string, string, Exception>((long)this.GetHashCode(), "DumpsterRedeliveryManager: Failed checking for dumpster request for {0}({1}). Exception: {2}", replayConfiguration.Name, replayConfiguration.Identity, ex);
									ReplayCrimsonEvents.DumpsterRedeliveryForDatabaseFailed.LogPeriodic<string, Guid, bool, string, Exception>(replayConfiguration.Identity, DiagCore.DefaultEventSuppressionInterval, replayConfiguration.DatabaseName, replayConfiguration.IdentityGuid, false, ex.Message, ex);
								}
							}
							else
							{
								ExTraceGlobals.DumpsterTracer.TraceDebug(0L, "DumpsterRedeliveryManager: Skipping dumpster request for {0}({1}) since node is not active. Config type is {2} and source machine is {3}", new object[]
								{
									replayConfiguration.Name,
									replayConfiguration.Identity,
									replayConfiguration.Type,
									replayConfiguration.SourceMachine
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002B914 File Offset: 0x00029B14
		private void ScheduleDumpsterRedelivery(object obj)
		{
			try
			{
				ReplayConfiguration replayConfiguration = obj as ReplayConfiguration;
				ExTraceGlobals.DumpsterTracer.TraceDebug<string, string>(0L, "DumpsterRedeliveryManager: ScheduleDumpsterRedelivery for {0}({1}).", replayConfiguration.Name, replayConfiguration.Identity);
				DumpsterRedeliveryWrapper.DoRedeliveryIfRequired(replayConfiguration);
			}
			finally
			{
				lock (this)
				{
					this.m_numOutstandingRequests--;
					if (this.m_numOutstandingRequests == 0)
					{
						this.m_doneEvent.Set();
					}
				}
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002B9A4 File Offset: 0x00029BA4
		protected override void StopInternal()
		{
			base.StopInternal();
			this.m_doneEvent.WaitOne();
			this.m_doneEvent.Close();
			this.m_doneEvent = null;
		}

		// Token: 0x040003D7 RID: 983
		private List<ReplayConfiguration> m_allConfigurationsCached;

		// Token: 0x040003D8 RID: 984
		private object m_cacheLock = new object();

		// Token: 0x040003D9 RID: 985
		private ManualResetEvent m_doneEvent = new ManualResetEvent(true);

		// Token: 0x040003DA RID: 986
		private int m_numOutstandingRequests;
	}
}
