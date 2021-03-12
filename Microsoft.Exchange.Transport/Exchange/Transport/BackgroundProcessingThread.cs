using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.Storage.IPFiltering;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200000C RID: 12
	internal class BackgroundProcessingThread : BackgroundProcessingThreadBase, IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000026CF File Offset: 0x000008CF
		public BackgroundProcessingThread(BackgroundProcessingThread.ServerComponentStateChangedHandler serverComponentStateChangedHandler) : base(TimeSpan.FromSeconds(1.0))
		{
			this.serverComponentStateChangedHandler = serverComponentStateChangedHandler;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000026EC File Offset: 0x000008EC
		public string CurrentState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026EF File Offset: 0x000008EF
		public void Load()
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026F1 File Offset: 0x000008F1
		public void Unload()
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000026F3 File Offset: 0x000008F3
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000026F6 File Offset: 0x000008F6
		public void Pause()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000026F8 File Offset: 0x000008F8
		public void Continue()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026FC File Offset: 0x000008FC
		protected override void Run()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.filterPruned = utcNow;
			this.lastScan = utcNow;
			this.lastTimeThrottlingManagerSwept = utcNow;
			this.lastCatMonitored = utcNow;
			this.lastDsnMonitored = utcNow;
			this.lastQueueLevelLogTime = utcNow;
			this.lastTimeBasedPerfCounterUpdate = utcNow;
			this.lastServiceStateCheckTime = utcNow;
			if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub)
			{
				this.serviceStateHelper = new ServiceStateHelper(Components.Configuration, ServerComponentStates.GetComponentId(ServerComponentEnum.HubTransport));
			}
			else if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.Edge)
			{
				this.serviceStateHelper = new ServiceStateHelper(Components.Configuration, ServerComponentStates.GetComponentId(ServerComponentEnum.EdgeTransport));
			}
			base.Run();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002798 File Offset: 0x00000998
		protected override void RunMain(DateTime now)
		{
			Components.SmtpInComponent.UpdateTime(now);
			IStoreDriver storeDriver;
			bool flag = Components.TryGetStoreDriver(out storeDriver);
			if (now - this.filterPruned > BackgroundProcessingThread.FilterInterval)
			{
				IPFilterLists.Cleanup();
				this.filterPruned = now;
			}
			if (now - this.lastScan > BackgroundProcessingThread.SlowScanInterval)
			{
				TimeSpan t = now - BackgroundProcessingThread.SystemStartTime;
				if (!BackgroundProcessingThread.reportedCrashingClearEvent && t > BackgroundProcessingThread.CrashingClearEventTime)
				{
					BackgroundProcessingThread.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ServerLivingForConsiderableTime, null, new object[]
					{
						t.ToString()
					});
					BackgroundProcessingThread.reportedCrashingClearEvent = true;
				}
				if (flag)
				{
					Components.StoreDriver.ExpireOldSubmissionConnections();
				}
				QueueManager.StartUpdateAllQueues();
				Components.CategorizerComponent.TimedUpdate();
				IProcessingQuotaComponent processingQuotaComponent;
				if (Components.TryGetProcessingQuotaComponent(out processingQuotaComponent))
				{
					processingQuotaComponent.TimedUpdate();
				}
				this.lastScan = now;
			}
			if (now - this.lastTimeBasedPerfCounterUpdate > BackgroundProcessingThread.TimeBasedPerfCounterUpdateInterval)
			{
				Components.QueueManager.GetQueuedRecipientsByAge().TimedUpdate();
				IQueueQuotaComponent queueQuotaComponent;
				if (Components.TryGetQueueQuotaComponent(out queueQuotaComponent))
				{
					queueQuotaComponent.TimedUpdate();
				}
				this.lastTimeBasedPerfCounterUpdate = now;
			}
			if (this.serviceStateHelper != null && now - this.lastServiceStateCheckTime > BackgroundProcessingThread.ServiceStateCheckInterval)
			{
				Components.RemoteDeliveryComponent.GetEndToEndLatencyBuckets().Flush();
				bool flag2 = this.serviceStateHelper.CheckState(this.startupServiceState);
				if (flag2 && this.serverComponentStateChangedHandler != null)
				{
					this.serverComponentStateChangedHandler();
				}
				this.lastServiceStateCheckTime = now;
			}
			if (now - this.lastTimeThrottlingManagerSwept > BackgroundProcessingThread.FiveMinuteInterval)
			{
				Components.MessageThrottlingComponent.MessageThrottlingManager.CleanupIdleEntries();
				Components.UnhealthyTargetFilterComponent.CleanupExpiredEntries();
				this.lastTimeThrottlingManagerSwept = now;
			}
			if (Components.ResourceManager.IsMonitoringEnabled && now - Components.ResourceManager.LastTimeResourceMonitored > Components.ResourceManager.MonitorInterval)
			{
				Components.ResourceManager.OnMonitorResource(null);
			}
			if (now - this.lastCatMonitored > BackgroundProcessingThread.CategorizerMonitorInterval)
			{
				Components.CategorizerComponent.MonitorJobs();
				this.lastCatMonitored = now;
			}
			if (now - this.lastDsnMonitored > BackgroundProcessingThread.FiveMinuteInterval)
			{
				Components.DsnGenerator.MonitorJobs();
				this.lastDsnMonitored = now;
			}
			if (now - this.lastQueueLevelLogTime > Components.Configuration.AppConfig.QueueConfiguration.QueueLoggingInterval)
			{
				this.LogQueueInfo();
				this.lastQueueLevelLogTime = now;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A0F File Offset: 0x00000C0F
		private void LogQueueInfo()
		{
			DiagnosticsAggregationHelper.LogQueueInfo(Components.Configuration.LocalServer.TransportServer.QueueLogPath);
		}

		// Token: 0x0400000F RID: 15
		public static readonly TimeSpan SlowScanInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x04000010 RID: 16
		private static readonly TimeSpan TimeBasedPerfCounterUpdateInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x04000011 RID: 17
		private static readonly TimeSpan FiveMinuteInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000012 RID: 18
		private static readonly TimeSpan FilterInterval = TimeSpan.FromHours(6.0);

		// Token: 0x04000013 RID: 19
		private static readonly TimeSpan CrashingClearEventTime = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000014 RID: 20
		private static readonly TimeSpan ServiceStateCheckInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000015 RID: 21
		private static readonly DateTime SystemStartTime = DateTime.UtcNow;

		// Token: 0x04000016 RID: 22
		private static readonly TimeSpan CategorizerMonitorInterval = Components.TransportAppConfig.Resolver.JobHealthUpdateInterval;

		// Token: 0x04000017 RID: 23
		private static bool reportedCrashingClearEvent = false;

		// Token: 0x04000018 RID: 24
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000019 RID: 25
		private DateTime filterPruned;

		// Token: 0x0400001A RID: 26
		private DateTime lastScan;

		// Token: 0x0400001B RID: 27
		private DateTime lastTimeThrottlingManagerSwept;

		// Token: 0x0400001C RID: 28
		private DateTime lastCatMonitored;

		// Token: 0x0400001D RID: 29
		private DateTime lastDsnMonitored;

		// Token: 0x0400001E RID: 30
		private DateTime lastQueueLevelLogTime;

		// Token: 0x0400001F RID: 31
		private DateTime lastTimeBasedPerfCounterUpdate;

		// Token: 0x04000020 RID: 32
		private DateTime lastServiceStateCheckTime;

		// Token: 0x04000021 RID: 33
		private ServiceStateHelper serviceStateHelper;

		// Token: 0x04000022 RID: 34
		private BackgroundProcessingThread.ServerComponentStateChangedHandler serverComponentStateChangedHandler;

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x06000049 RID: 73
		public delegate void ServerComponentStateChangedHandler();
	}
}
