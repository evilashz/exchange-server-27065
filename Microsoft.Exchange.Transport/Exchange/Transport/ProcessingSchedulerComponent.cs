using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.MessageDepot;
using Microsoft.Exchange.Transport.Scheduler;
using Microsoft.Exchange.Transport.Scheduler.Contracts;
using Microsoft.Exchange.Transport.Scheduler.Processing;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200003D RID: 61
	internal class ProcessingSchedulerComponent : IProcessingSchedulerComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00006658 File Offset: 0x00004858
		public void SetLoadTimeDependencies(ITransportConfiguration transportConfiguration, IMessageDepotComponent messageDepotComponent, IMessageProcessor messageProcessor, IMessagingDatabaseComponent messagingDatabaseComponent)
		{
			ArgumentValidator.ThrowIfNull("transportConfiguration", transportConfiguration);
			ArgumentValidator.ThrowIfNull("messageDepotComponent", messageDepotComponent);
			ArgumentValidator.ThrowIfNull("messageProcessor", messageProcessor);
			ArgumentValidator.ThrowIfNull("messagingDatabaseComponent", messagingDatabaseComponent);
			this.transportConfiguration = transportConfiguration;
			this.messageDepotComponent = messageDepotComponent;
			this.messageProcessor = messageProcessor;
			this.messagingDatabaseComponent = messagingDatabaseComponent;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000066B0 File Offset: 0x000048B0
		public void Load()
		{
			if (!this.messageDepotComponent.Enabled)
			{
				return;
			}
			IQueueFactory queueFactory = this.GetQueueFactory();
			ISchedulerMetering meteringInstance = this.GetMeteringInstance();
			IQueueLogWriter queueLogWriter = this.GetQueueLogWriter();
			ISchedulerDiagnostics schedulerDiagnostics = new SchedulerDiagnostics(TimeSpan.FromMinutes(1.0), meteringInstance, queueLogWriter);
			ISchedulerThrottler throttlerInstance = this.GetThrottlerInstance(meteringInstance);
			IScopedQueuesManager scopedQueuesManager = this.GetScopedQueuesManager(throttlerInstance, queueFactory, schedulerDiagnostics);
			this.scheduler = new ProcessingScheduler(CatScheduler.MaxExecutingJobs, this.messageProcessor, meteringInstance, queueFactory, throttlerInstance, scopedQueuesManager, schedulerDiagnostics, false);
			this.schedulerAdmin = new ProcessingSchedulerAdminWrapper(this.scheduler, this.messagingDatabaseComponent);
			this.SetupLatencyTracker();
			this.diagnosticPublisher = new SchedulerDiagnosticPublisher(this.scheduler);
			this.refreshTimer = new GuardedTimer(new TimerCallback(this.TimedUpdate), null, ProcessingSchedulerComponent.RefreshTimeInterval);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006774 File Offset: 0x00004974
		public void Unload()
		{
			if (this.refreshTimer != null)
			{
				this.refreshTimer.Dispose(true);
				this.refreshTimer = null;
			}
			if (this.scheduler != null)
			{
				this.scheduler.Shutdown(-1);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000067A6 File Offset: 0x000049A6
		public string OnUnhandledException(Exception e)
		{
			return string.Empty;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000067AD File Offset: 0x000049AD
		public IProcessingScheduler ProcessingScheduler
		{
			get
			{
				return this.scheduler;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000067B5 File Offset: 0x000049B5
		public IProcessingSchedulerAdmin ProcessingSchedulerAdmin
		{
			get
			{
				return this.schedulerAdmin;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000067BD File Offset: 0x000049BD
		public void Pause()
		{
			this.scheduler.Pause();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000067CA File Offset: 0x000049CA
		public void Resume()
		{
			this.scheduler.Resume();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000067D7 File Offset: 0x000049D7
		public string GetDiagnosticComponentName()
		{
			return "ProcessingScheduler";
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000067E0 File Offset: 0x000049E0
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			if (this.scheduler != null)
			{
				SchedulerDiagnosticsInfo diagnosticsInfo = this.scheduler.GetDiagnosticsInfo();
				xelement.Add(new XElement("Enabled", true));
				xelement.Add(new XElement("Running", this.scheduler.IsRunning));
				xelement.Add(new XElement("Received", diagnosticsInfo.Received));
				xelement.Add(new XElement("Throttled", diagnosticsInfo.Throttled));
				xelement.Add(new XElement("Dispatched", diagnosticsInfo.Dispatched));
				xelement.Add(new XElement("Processed", diagnosticsInfo.Processed));
				xelement.Add(new XElement("ReceiveRate", diagnosticsInfo.ReceiveRate));
				xelement.Add(new XElement("ThrottleRate", diagnosticsInfo.ThrottleRate));
				xelement.Add(new XElement("DispatchRate", diagnosticsInfo.DispatchRate));
				xelement.Add(new XElement("ProcessRate", diagnosticsInfo.ProcessRate));
				xelement.Add(new XElement("ProcessingVelocity", diagnosticsInfo.ProcessingVelocity));
				xelement.Add(new XElement("ScopedQueuesCreatedRate", diagnosticsInfo.ScopedQueuesCreatedRate));
				xelement.Add(new XElement("ScopedQueuesDestroyedRate", diagnosticsInfo.ScopedQueuesDestroyedRate));
				xelement.Add(new XElement("OldestLockTimeStamp", diagnosticsInfo.OldestLockTimeStamp));
				xelement.Add(new XElement("OldestScopedQueueCreateTime", diagnosticsInfo.OldestScopedQueueCreateTime));
			}
			else
			{
				xelement.Add(new XElement("Enabled", false));
			}
			return xelement;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006A14 File Offset: 0x00004C14
		private void SetupLatencyTracker()
		{
			this.scheduler.SubscribeToEvent(SchedulingState.Received, new SchedulingEventHandler(this.TrackLatency));
			this.scheduler.SubscribeToEvent(SchedulingState.Scoped, new SchedulingEventHandler(this.TrackLatency));
			this.scheduler.SubscribeToEvent(SchedulingState.Unscoped, new SchedulingEventHandler(this.TrackLatency));
			this.scheduler.SubscribeToEvent(SchedulingState.Dispatched, new SchedulingEventHandler(this.TrackLatency));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006A84 File Offset: 0x00004C84
		private void TrackLatency(SchedulingState state, ISchedulableMessage message)
		{
			TransportMailItem transportMailItem;
			try
			{
				IMessageDepotItemWrapper messageDepotItemWrapper = this.GetMessageDepot().Get(message.Id);
				transportMailItem = (messageDepotItemWrapper.Item.MessageObject as TransportMailItem);
			}
			catch (ItemNotFoundException)
			{
				return;
			}
			if (transportMailItem != null)
			{
				switch (state)
				{
				case SchedulingState.Received:
					LatencyTracker.BeginTrackLatency(LatencyComponent.ProcessingScheduler, transportMailItem.LatencyTracker);
					return;
				case SchedulingState.Scoped:
					LatencyTracker.BeginTrackLatency(LatencyComponent.ProcessingSchedulerScoped, transportMailItem.LatencyTracker);
					return;
				case SchedulingState.Unscoped:
					LatencyTracker.EndTrackLatency(LatencyComponent.ProcessingSchedulerScoped, transportMailItem.LatencyTracker);
					return;
				case SchedulingState.Dispatched:
					LatencyTracker.EndTrackLatency(LatencyComponent.ProcessingScheduler, transportMailItem.LatencyTracker);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006B20 File Offset: 0x00004D20
		private IMessageDepot GetMessageDepot()
		{
			return this.messageDepotComponent.MessageDepot;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006B2D File Offset: 0x00004D2D
		private ISchedulerMetering GetMeteringInstance()
		{
			return new InMemorySchedulerMetering(TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromSeconds(30.0), TimeSpan.FromSeconds(10.0));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006B6C File Offset: 0x00004D6C
		private ISchedulerThrottler GetThrottlerInstance(ISchedulerMetering metering)
		{
			IThrottlingPolicy[] policies = new IThrottlingPolicy[]
			{
				new OutstandingJobsPolicy(CatScheduler.MaxExecutingJobs, 0.7),
				new MemoryUsagePolicy(10000000L, 0.7),
				new ProcessingTicksPolicy(TimeSpan.FromMinutes(15.0).Ticks)
			};
			MessageScopeType[] array = new MessageScopeType[1];
			MessageScopeType[] relevantTypes = array;
			return new PolicyBasedThrottler(policies, relevantTypes, metering);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006BE0 File Offset: 0x00004DE0
		private IQueueFactory GetQueueFactory()
		{
			return new PriorityQueueFactory(new Dictionary<DeliveryPriority, int>
			{
				{
					DeliveryPriority.High,
					5
				},
				{
					DeliveryPriority.Normal,
					3
				},
				{
					DeliveryPriority.Low,
					2
				},
				{
					DeliveryPriority.None,
					1
				}
			});
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006C19 File Offset: 0x00004E19
		private IScopedQueuesManager GetScopedQueuesManager(ISchedulerThrottler throttler, IQueueFactory queueFactory, ISchedulerDiagnostics diagnostics)
		{
			return new ScopedQueuesManager(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(5.0), queueFactory, throttler, diagnostics, null);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006C40 File Offset: 0x00004E40
		private IQueueLogWriter GetQueueLogWriter()
		{
			Server transportServer = this.transportConfiguration.LocalServer.TransportServer;
			if (transportServer.ProcessingSchedulerLogEnabled && transportServer.ProcessingSchedulerLogPath != null && !string.IsNullOrEmpty(transportServer.ProcessingSchedulerLogPath.PathName))
			{
				return new AsyncQueueLogWriter("Microsoft Exchange Server", "Processing Scheduler Log", "ProcessingScheduler", "ProcessingScheduler", transportServer.ProcessingSchedulerLogPath.PathName, transportServer.ProcessingSchedulerLogMaxAge, TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(60.0), (long)(transportServer.ProcessingSchedulerLogMaxDirectorySize.IsUnlimited ? 0UL : transportServer.ProcessingSchedulerLogMaxDirectorySize.Value.ToBytes()), (long)(transportServer.ProcessingSchedulerLogMaxFileSize.IsUnlimited ? 0UL : transportServer.ProcessingSchedulerLogMaxFileSize.Value.ToBytes()), (int)ByteQuantifiedSize.FromKB(64UL).ToBytes());
			}
			return new NoOpQueueLogWriter();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006D49 File Offset: 0x00004F49
		private void TimedUpdate(object state)
		{
			this.diagnosticPublisher.Publish();
		}

		// Token: 0x040000B4 RID: 180
		private static readonly TimeSpan RefreshTimeInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x040000B5 RID: 181
		private IMessageProcessor messageProcessor;

		// Token: 0x040000B6 RID: 182
		private ProcessingScheduler scheduler;

		// Token: 0x040000B7 RID: 183
		private IProcessingSchedulerAdmin schedulerAdmin;

		// Token: 0x040000B8 RID: 184
		private SchedulerDiagnosticPublisher diagnosticPublisher;

		// Token: 0x040000B9 RID: 185
		private IMessageDepotComponent messageDepotComponent;

		// Token: 0x040000BA RID: 186
		private IMessagingDatabaseComponent messagingDatabaseComponent;

		// Token: 0x040000BB RID: 187
		private ITransportConfiguration transportConfiguration;

		// Token: 0x040000BC RID: 188
		private GuardedTimer refreshTimer;
	}
}
