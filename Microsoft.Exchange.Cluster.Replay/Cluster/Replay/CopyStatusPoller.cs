using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001EA RID: 490
	internal class CopyStatusPoller : TimerComponent, ICopyStatusPoller
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x0004EC94 File Offset: 0x0004CE94
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004EC9C File Offset: 0x0004CE9C
		public CopyStatusPoller(IMonitoringADConfigProvider adConfigProvider, CopyStatusClientLookupTable statusTable, ActiveManager activeManager) : base(TimeSpan.Zero, CopyStatusPoller.CopyStatusPollerInterval, "CopyStatusPoller")
		{
			this.m_adConfigProvider = adConfigProvider;
			this.m_statusTable = statusTable;
			this.m_activeManager = activeManager;
			this.m_rpcThreadsInProgress = new ConcurrentDictionary<AmServerName, bool>();
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004ECF0 File Offset: 0x0004CEF0
		public bool TryWaitForInitialization()
		{
			TimeSpan timeout = TimeSpan.FromMilliseconds((double)RegistryParameters.GetMailboxDatabaseCopyStatusRPCTimeoutInMSec);
			return this.m_firstPollCompleted.WaitOne(timeout) == ManualOneShotEvent.Result.Success;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0004ED18 File Offset: 0x0004CF18
		protected override void TimerCallbackInternal()
		{
			try
			{
				this.Run();
			}
			catch (MonitoringADConfigException ex)
			{
				CopyStatusPoller.Tracer.TraceError<MonitoringADConfigException>((long)this.GetHashCode(), "CopyStatusPoller: Got exception when retrieving AD config: {0}", ex);
				ReplayCrimsonEvents.CopyStatusPollerError.LogPeriodic<string, MonitoringADConfigException>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, ex.Message, ex);
			}
			finally
			{
				this.m_firstPollCompleted.Set();
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004ED94 File Offset: 0x0004CF94
		protected override void StopInternal()
		{
			base.StopInternal();
			this.m_firstPollCompleted.Close();
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0004EDA8 File Offset: 0x0004CFA8
		private void Run()
		{
			IMonitoringADConfig config = this.m_adConfigProvider.GetConfig(true);
			AmMultiNodeCopyStatusFetcher_ForPoller amMultiNodeCopyStatusFetcher_ForPoller = new AmMultiNodeCopyStatusFetcher_ForPoller(config, this.m_activeManager, this.m_rpcThreadsInProgress);
			Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> status = amMultiNodeCopyStatusFetcher_ForPoller.GetStatus(CopyStatusPoller.CopyStatusPollerInterval);
			if (status != null)
			{
				this.m_statusTable.UpdateCopyStatusCachedEntries(status);
			}
		}

		// Token: 0x0400077A RID: 1914
		public const RpcGetDatabaseCopyStatusFlags2 GetCopyStatusRpcFlags = RpcGetDatabaseCopyStatusFlags2.None;

		// Token: 0x0400077B RID: 1915
		private const string FirstCopyStatusPollCompletedEventName = "FirstCopyStatusPollCompletedEvent";

		// Token: 0x0400077C RID: 1916
		private static readonly TimeSpan CopyStatusPollerInterval = TimeSpan.FromMilliseconds((double)RegistryParameters.CopyStatusPollerIntervalInMsec);

		// Token: 0x0400077D RID: 1917
		private ManualOneShotEvent m_firstPollCompleted = new ManualOneShotEvent("FirstCopyStatusPollCompletedEvent");

		// Token: 0x0400077E RID: 1918
		private CopyStatusClientLookupTable m_statusTable;

		// Token: 0x0400077F RID: 1919
		private IMonitoringADConfigProvider m_adConfigProvider;

		// Token: 0x04000780 RID: 1920
		private ActiveManager m_activeManager;

		// Token: 0x04000781 RID: 1921
		private ConcurrentDictionary<AmServerName, bool> m_rpcThreadsInProgress;
	}
}
