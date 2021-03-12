using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;
using Microsoft.Exchange.Transport.Scheduler.Processing;

namespace Microsoft.Exchange.Transport.Scheduler
{
	// Token: 0x02000023 RID: 35
	internal class ProcessingScheduler : IProcessingScheduler, IProcessingSchedulerAdmin, IQueueLogProvider
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003C20 File Offset: 0x00001E20
		public ProcessingScheduler(int concurrencyLevel, IMessageProcessor processor, bool running = true) : this(concurrencyLevel, processor, ProcessingScheduler.DefaultNoMetering, ProcessingScheduler.DefaultQueueFactory, ProcessingScheduler.DefaultNoThrottling, ProcessingScheduler.DefaultScopedQueuesManager, ProcessingScheduler.DefaultDiagnostics, running)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003C50 File Offset: 0x00001E50
		public ProcessingScheduler(int concurrencyLevel, IMessageProcessor processor, ISchedulerMetering metering, IQueueFactory queueFactory, ISchedulerThrottler throttler, IScopedQueuesManager scopedQueuesManager, bool running = true) : this(concurrencyLevel, processor, metering, queueFactory, throttler, scopedQueuesManager, ProcessingScheduler.DefaultDiagnostics, running)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003C78 File Offset: 0x00001E78
		public ProcessingScheduler(int concurrencyLevel, IMessageProcessor processor, ISchedulerMetering metering, IQueueFactory queueFactory, ISchedulerThrottler throttler, IScopedQueuesManager scopedQueuesManager, ISchedulerDiagnostics schedulerDiagnostics, bool running = true)
		{
			this.eventHandlers = new SchedulingEventHandler[Enum.GetNames(typeof(SchedulingState)).Length];
			for (int i = 0; i < this.eventHandlers.Length; i++)
			{
				this.eventHandlers[i] = delegate(SchedulingState param0, ISchedulableMessage param1)
				{
				};
			}
			this.metering = metering;
			this.throttler = throttler;
			this.schedulerDiagnostics = schedulerDiagnostics;
			this.fifoQueue = new LoggingQueueWrapper(queueFactory.CreateNewQueueInstance());
			this.scopedQueuesManager = scopedQueuesManager;
			this.running = (running ? 1 : 0);
			this.jobDispatcher = new JobDispatcher(concurrencyLevel, processor, metering, schedulerDiagnostics);
			this.jobDispatcher.Start(this);
			this.schedulerDiagnostics.RegisterQueueLogging(this);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003D5C File Offset: 0x00001F5C
		public bool IsRunning
		{
			get
			{
				return this.running == 1;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D68 File Offset: 0x00001F68
		public void Submit(ISchedulableMessage message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			lock (this.queueSyncObject)
			{
				this.fifoQueue.Enqueue(message);
			}
			this.schedulerDiagnostics.Received();
			this.NotifyHandlers(SchedulingState.Received, message);
			if (this.running == 1)
			{
				this.jobDispatcher.WorkAvailable();
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public void SubscribeToEvent(SchedulingState state, SchedulingEventHandler handler)
		{
			ArgumentValidator.ThrowIfNull("handler", handler);
			ArgumentValidator.ThrowIfOutOfRange<int>("state", (int)state, 0, this.eventHandlers.Length);
			lock (this.eventHandlerSyncObject)
			{
				SchedulingEventHandler[] array;
				(array = this.eventHandlers)[(int)state] = (SchedulingEventHandler)Delegate.Combine(array[(int)state], handler);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003E54 File Offset: 0x00002054
		public void UnsubscribeFromEvent(SchedulingState state, SchedulingEventHandler handler)
		{
			ArgumentValidator.ThrowIfNull("handler", handler);
			ArgumentValidator.ThrowIfOutOfRange<int>("state", (int)state, 0, this.eventHandlers.Length);
			lock (this.eventHandlerSyncObject)
			{
				SchedulingEventHandler[] array;
				(array = this.eventHandlers)[(int)state] = (SchedulingEventHandler)Delegate.Remove(array[(int)state], handler);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003EC8 File Offset: 0x000020C8
		public bool TryGetNext(out ISchedulableMessage message)
		{
			if (this.running == 0)
			{
				message = null;
				return false;
			}
			this.UpdateComponents();
			if (this.scopedQueuesManager.TryGetNext(out message))
			{
				this.NotifyHandlers(SchedulingState.Unscoped, message);
				this.NotifyHandlers(SchedulingState.Dispatched, message);
				return true;
			}
			ISchedulableMessage schedulableMessage;
			while (this.fifoQueue.TryDequeue(out schedulableMessage))
			{
				IEnumerable<IMessageScope> scopes = this.throttler.GetThrottlingScopes(schedulableMessage.Scopes).ToList<IMessageScope>();
				IMessageScope throttledScope;
				if (this.scopedQueuesManager.IsAlreadyScoped(scopes, out throttledScope))
				{
					this.scopedQueuesManager.Add(schedulableMessage, throttledScope);
					this.schedulerDiagnostics.Throttled();
					this.NotifyHandlers(SchedulingState.Scoped, schedulableMessage);
				}
				else
				{
					if (!this.throttler.ShouldThrottle(scopes, out throttledScope))
					{
						message = schedulableMessage;
						this.NotifyHandlers(SchedulingState.Dispatched, schedulableMessage);
						return true;
					}
					this.scopedQueuesManager.Add(schedulableMessage, throttledScope);
					this.schedulerDiagnostics.Throttled();
					this.NotifyHandlers(SchedulingState.Scoped, schedulableMessage);
				}
			}
			message = null;
			return false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003FAB File Offset: 0x000021AB
		public void Pause()
		{
			Interlocked.CompareExchange(ref this.running, 0, 1);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003FBB File Offset: 0x000021BB
		public void Resume()
		{
			if (Interlocked.CompareExchange(ref this.running, 1, 0) == 0)
			{
				this.jobDispatcher.WorkAvailable();
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003FD8 File Offset: 0x000021D8
		public bool Shutdown(int timeoutMilliseconds = -1)
		{
			ShutdownCommand shutdownCommand = new ShutdownCommand(this);
			this.jobDispatcher.EnqueueCommand(shutdownCommand);
			return shutdownCommand.WaitForDone(timeoutMilliseconds);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004000 File Offset: 0x00002200
		public IEnumerable<QueueLogInfo> FlushLogs(DateTime checkpoint, ISchedulerMetering metering)
		{
			QueueLogInfo queueLogInfo = new QueueLogInfo("Unscoped", checkpoint)
			{
				UsageData = metering.GetTotalUsage()
			};
			this.fifoQueue.Flush(checkpoint, queueLogInfo);
			return new QueueLogInfo[]
			{
				queueLogInfo
			};
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004040 File Offset: 0x00002240
		public SchedulerDiagnosticsInfo GetDiagnosticsInfo()
		{
			return this.schedulerDiagnostics.GetDiagnosticsInfo();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000404D File Offset: 0x0000224D
		internal void StartShutdown()
		{
			this.Pause();
			this.jobDispatcher.StartShutdown();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004060 File Offset: 0x00002260
		internal bool WaitForShutdown(int timeoutMilliseconds = -1)
		{
			return this.jobDispatcher.WaitForShutdown(timeoutMilliseconds);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000406E File Offset: 0x0000226E
		private void UpdateComponents()
		{
			this.RefreshIfNeeded(this.metering);
			this.RefreshIfNeeded(this.scopedQueuesManager);
			this.RefreshIfNeeded(this.schedulerDiagnostics);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004094 File Offset: 0x00002294
		private void RefreshIfNeeded(object component)
		{
			RefreshableComponent refreshableComponent = component as RefreshableComponent;
			if (refreshableComponent != null)
			{
				refreshableComponent.RefreshIfNecessary();
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040B4 File Offset: 0x000022B4
		private void NotifyHandlers(SchedulingState state, ISchedulableMessage message)
		{
			SchedulingEventHandler schedulingEventHandler = this.eventHandlers[(int)state];
			schedulingEventHandler(state, message);
		}

		// Token: 0x04000056 RID: 86
		public static readonly ISchedulerMetering DefaultNoMetering = new NoOpMetering();

		// Token: 0x04000057 RID: 87
		public static readonly ISchedulerThrottler DefaultNoThrottling = new NoOpThrottler();

		// Token: 0x04000058 RID: 88
		public static readonly IQueueFactory DefaultQueueFactory = new ConcurrentQueueFactory();

		// Token: 0x04000059 RID: 89
		public static readonly ISchedulerDiagnostics DefaultDiagnostics = new SchedulerDiagnostics();

		// Token: 0x0400005A RID: 90
		public static readonly IScopedQueuesManager DefaultScopedQueuesManager = new ScopedQueuesManager(TimeSpan.FromMinutes(2.0), TimeSpan.FromMinutes(15.0), ProcessingScheduler.DefaultQueueFactory, ProcessingScheduler.DefaultNoThrottling, ProcessingScheduler.DefaultDiagnostics, () => DateTime.UtcNow);

		// Token: 0x0400005B RID: 91
		private readonly LoggingQueueWrapper fifoQueue;

		// Token: 0x0400005C RID: 92
		private readonly JobDispatcher jobDispatcher;

		// Token: 0x0400005D RID: 93
		private readonly IScopedQueuesManager scopedQueuesManager;

		// Token: 0x0400005E RID: 94
		private readonly ISchedulerMetering metering;

		// Token: 0x0400005F RID: 95
		private readonly ISchedulerThrottler throttler;

		// Token: 0x04000060 RID: 96
		private readonly ISchedulerDiagnostics schedulerDiagnostics;

		// Token: 0x04000061 RID: 97
		private readonly SchedulingEventHandler[] eventHandlers;

		// Token: 0x04000062 RID: 98
		private readonly object eventHandlerSyncObject = new object();

		// Token: 0x04000063 RID: 99
		private readonly object queueSyncObject = new object();

		// Token: 0x04000064 RID: 100
		private int running;
	}
}
