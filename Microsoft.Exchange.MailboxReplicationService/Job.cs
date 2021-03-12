using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000022 RID: 34
	internal abstract class Job : DisposeTrackableBase, IJob
	{
		// Token: 0x0600014E RID: 334 RVA: 0x000087FE File Offset: 0x000069FE
		public Job()
		{
			this.workItemQueue = new WorkItemQueue();
			this.state = JobState.Runnable;
			this.jobDoneEvent = new ManualResetEvent(true);
			this.creationTime = DateTime.UtcNow;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000883A File Offset: 0x00006A3A
		public virtual bool IsInteractive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000150 RID: 336
		public abstract WorkloadType WorkloadTypeFromJob { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000151 RID: 337
		// (set) Token: 0x06000152 RID: 338
		public abstract ReservationContext Reservation { get; internal set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000883D File Offset: 0x00006A3D
		public bool IsFinished
		{
			get
			{
				return (this.workItemQueue.IsEmpty() && !this.IsInteractive) || (CommonUtils.ServiceIsStopping && this.Reservation == null);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00008868 File Offset: 0x00006A68
		protected TimeSpan SuspendTime
		{
			get
			{
				return DateTime.UtcNow - this.creationTime - this.executionTime;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00008885 File Offset: 0x00006A85
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000888D File Offset: 0x00006A8D
		protected int TraceActivityID
		{
			get
			{
				return this.traceActivityID;
			}
			set
			{
				this.traceActivityID = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00008896 File Offset: 0x00006A96
		protected virtual bool IsInFinalization
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000889C File Offset: 0x00006A9C
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000088E4 File Offset: 0x00006AE4
		private protected ExDateTime ScheduledRunTime
		{
			protected get
			{
				ExDateTime scheduledRunTime;
				lock (this.jobLock)
				{
					scheduledRunTime = this.workItemQueue.ScheduledRunTime;
				}
				return scheduledRunTime;
			}
			private set
			{
				lock (this.jobLock)
				{
					this.workItemQueue.ScheduledRunTime = value;
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600015A RID: 346
		protected abstract IEnumerable<ResourceKey> ResourceDependencies { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000892C File Offset: 0x00006B2C
		bool IJob.ShouldWakeup
		{
			get
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				return this.ScheduledRunTime <= utcNow || CommonUtils.ServiceIsStopping;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008954 File Offset: 0x00006B54
		JobState IJob.State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015D RID: 349
		public abstract JobSortKey JobSortKey { get; }

		// Token: 0x0600015E RID: 350 RVA: 0x0000895C File Offset: 0x00006B5C
		MrsSystemTask IJob.GetTask(SystemWorkloadBase systemWorkload, ResourceReservationContext context)
		{
			return this.GetTask(systemWorkload, context);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008966 File Offset: 0x00006B66
		void IJob.ProcessTaskExecutionResult(MrsSystemTask systemTask)
		{
			this.ProcessTaskExecutionResult(systemTask);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000896F File Offset: 0x00006B6F
		void IJob.RevertToPreviousUnthrottledState()
		{
			this.RevertToPreviousUnthrottledState();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008977 File Offset: 0x00006B77
		void IJob.WaitForJobToBeDone()
		{
			this.jobDoneEvent.WaitOne();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008985 File Offset: 0x00006B85
		void IJob.ResetJob()
		{
			this.ResetJob();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000898D File Offset: 0x00006B8D
		public bool WorkItemQueueIsEmpty()
		{
			return this.workItemQueue.IsEmpty();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000899A File Offset: 0x00006B9A
		public virtual void RevertToPreviousUnthrottledState()
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000899C File Offset: 0x00006B9C
		public override string ToString()
		{
			return base.GetType().Name + this.workItemTrace;
		}

		// Token: 0x06000166 RID: 358
		public abstract void PerformCrashingFailureActions(Exception exception);

		// Token: 0x06000167 RID: 359 RVA: 0x000089B4 File Offset: 0x00006BB4
		protected override void InternalDispose(bool calledFromDispose)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000089B6 File Offset: 0x00006BB6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Job>(this);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000089BE File Offset: 0x00006BBE
		protected virtual SettingsContextBase GetConfigSettingsContext()
		{
			return null;
		}

		// Token: 0x0600016A RID: 362
		public abstract IActivityScope GetCurrentActivityScope();

		// Token: 0x0600016B RID: 363
		protected abstract void StartDeferredDelayIfApplicable();

		// Token: 0x0600016C RID: 364
		protected abstract void ProcessSucceededTask(bool ignoreTaskSuccessfulExecutionTime);

		// Token: 0x0600016D RID: 365
		protected abstract void ProcessFailedTask(Exception lastFailure, out bool shouldContinueRunningJob);

		// Token: 0x0600016E RID: 366 RVA: 0x000089C1 File Offset: 0x00006BC1
		protected virtual void MoveToThrottledState(ResourceKey resource, bool deferDelay)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000089C4 File Offset: 0x00006BC4
		protected void WakeupJob()
		{
			base.CheckDisposed();
			MrsTracer.Service.Function("Job({0}).WakeupJob", new object[]
			{
				base.GetType().Name
			});
			lock (this.jobLock)
			{
				if (this.state == JobState.Waiting)
				{
					this.ScheduledRunTime = ExDateTime.MinValue;
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008A40 File Offset: 0x00006C40
		protected virtual bool CanBeCanceledOrSuspended()
		{
			return true;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008A44 File Offset: 0x00006C44
		protected void ResetWorkItemQueue()
		{
			lock (this.jobLock)
			{
				this.workItemQueue.Clear();
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008A8C File Offset: 0x00006C8C
		protected void ScheduleWorkItem(Action action, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem(TimeSpan.Zero, action, workloadType);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008A9B File Offset: 0x00006C9B
		protected void ScheduleWorkItem<T>(Action<T> action, T arg, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem<T>(TimeSpan.Zero, action, arg, workloadType);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008AAB File Offset: 0x00006CAB
		protected void ScheduleWorkItem<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem<T1, T2>(TimeSpan.Zero, action, arg1, arg2, workloadType);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008ABD File Offset: 0x00006CBD
		protected void ScheduleWorkItem<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem<T1, T2, T3>(TimeSpan.Zero, action, arg1, arg2, arg3, workloadType);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008AD1 File Offset: 0x00006CD1
		protected void ScheduleWorkItem<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem<T1, T2, T3, T4>(TimeSpan.Zero, action, arg1, arg2, arg3, arg4, workloadType);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008AE7 File Offset: 0x00006CE7
		protected void ScheduleWorkItem(TimeSpan delay, Action action, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem(new WorkItem(delay, action, workloadType));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008B14 File Offset: 0x00006D14
		protected void ScheduleWorkItem<T>(TimeSpan delay, Action<T> action, T arg, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem(new WorkItem(delay, delegate()
			{
				action(arg);
			}, workloadType));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008B70 File Offset: 0x00006D70
		protected void ScheduleWorkItem<T1, T2>(TimeSpan delay, Action<T1, T2> action, T1 arg1, T2 arg2, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem(new WorkItem(delay, delegate()
			{
				action(arg1, arg2);
			}, workloadType));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008BDC File Offset: 0x00006DDC
		protected void ScheduleWorkItem<T1, T2, T3>(TimeSpan delay, Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem(new WorkItem(delay, delegate()
			{
				action(arg1, arg2, arg3);
			}, workloadType));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008C54 File Offset: 0x00006E54
		protected void ScheduleWorkItem<T1, T2, T3, T4>(TimeSpan delay, Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WorkloadType workloadType = WorkloadType.Unknown)
		{
			this.ScheduleWorkItem(new WorkItem(delay, delegate()
			{
				action(arg1, arg2, arg3, arg4);
			}, workloadType));
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008CA8 File Offset: 0x00006EA8
		protected void ScheduleWorkItem(WorkItem workitem)
		{
			base.CheckDisposed();
			lock (this.jobLock)
			{
				this.workItemQueue.Add(workitem);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008CF4 File Offset: 0x00006EF4
		private void ResetJob()
		{
			lock (this.jobLock)
			{
				this.state = JobState.Runnable;
				this.jobDoneEvent.Reset();
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008D44 File Offset: 0x00006F44
		private MrsSystemTask GetTask(SystemWorkloadBase systemWorkload, ResourceReservationContext context)
		{
			MrsTracer.ActivityID = this.traceActivityID;
			base.CheckDisposed();
			MrsSystemTask result;
			using (SettingsContextBase.ActivateContext(this as ISettingsContextProvider))
			{
				lock (this.jobLock)
				{
					if (this.IsFinished)
					{
						MrsTracer.Service.Debug("Job({0}) is finished.", new object[]
						{
							base.GetType().Name
						});
						this.state = JobState.Finished;
						this.jobDoneEvent.Set();
						result = null;
					}
					else
					{
						WorkItem workItem = null;
						bool flag2 = true;
						foreach (WorkItem workItem2 in this.workItemQueue.GetCandidateWorkItems())
						{
							if (workItem2.ScheduledRunTime <= ExDateTime.UtcNow || CommonUtils.ServiceIsStopping)
							{
								flag2 = false;
								if (this.GetWorkloadType(workItem2.WorkloadType) == systemWorkload.WorkloadType)
								{
									workItem = workItem2;
									break;
								}
							}
						}
						if (workItem == null)
						{
							if (flag2)
							{
								this.state = JobState.Waiting;
							}
							result = null;
						}
						else
						{
							this.RevertToPreviousUnthrottledState();
							IEnumerable<ResourceKey> enumerable = this.ResourceDependencies;
							if (enumerable == null)
							{
								enumerable = Array<ResourceKey>.Empty;
							}
							ResourceKey resource = null;
							ResourceReservation reservation = this.GetReservation(workItem, systemWorkload, context, enumerable, out resource);
							if (reservation != null)
							{
								if (reservation.DelayFactor > 0.0)
								{
									this.MoveToThrottledState(resource, true);
								}
								this.TraceWorkItem(workItem);
								this.workItemQueue.Remove(workItem);
								result = new MrsSystemTask(this, workItem.Callback, systemWorkload, reservation, workItem is JobCheck);
							}
							else
							{
								this.MoveToThrottledState(resource, false);
								result = null;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008F38 File Offset: 0x00007138
		private ResourceReservation GetReservation(WorkItem workItem, SystemWorkloadBase systemWorkload, ResourceReservationContext context, IEnumerable<ResourceKey> resources, out ResourceKey throttledResource)
		{
			throttledResource = null;
			ResourceReservation result;
			if (CommonUtils.ServiceIsStopping || this.IsInFinalization || workItem is UnthrottledWorkItem)
			{
				result = context.GetUnthrottledReservation(systemWorkload);
			}
			else
			{
				result = context.GetReservation(systemWorkload, resources, out throttledResource);
			}
			return result;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008F7C File Offset: 0x0000717C
		private void ProcessTaskExecutionResult(MrsSystemTask systemTask)
		{
			this.executionTime += systemTask.ExecutionTime;
			if (systemTask.Failure == null)
			{
				this.ProcessSucceededTask(systemTask.IgnoreTaskSuccessfulExecutionTime);
				return;
			}
			using (SettingsContextBase.ActivateContext(this as ISettingsContextProvider))
			{
				bool flag;
				this.ProcessFailedTask(systemTask.Failure, out flag);
				if (!flag)
				{
					lock (this.jobLock)
					{
						MrsTracer.Service.Error("Job({0}) failed.", new object[]
						{
							base.GetType().Name
						});
						this.state = JobState.Failed;
						this.jobDoneEvent.Set();
					}
					this.StartDeferredDelayIfApplicable();
				}
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009058 File Offset: 0x00007258
		private WorkloadType GetWorkloadType(WorkloadType workloadType)
		{
			if (workloadType == WorkloadType.Unknown)
			{
				return this.WorkloadTypeFromJob;
			}
			return workloadType;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009068 File Offset: 0x00007268
		private void TraceWorkItem(WorkItem workItem)
		{
			this.workItemTrace = this.workItemTrace + "=>" + workItem.Callback.Method.Name;
			if (this.workItemTrace.Length > 1024)
			{
				this.workItemTrace = "..." + this.workItemTrace.Substring(this.workItemTrace.Length - 1024 + "...".Length, 1024 - "...".Length);
			}
			MrsTracer.Service.Debug("Workitems trace: {0}", new object[]
			{
				this
			});
		}

		// Token: 0x0400008B RID: 139
		private const int MaxWorkItemTraceSize = 1024;

		// Token: 0x0400008C RID: 140
		private readonly DateTime creationTime;

		// Token: 0x0400008D RID: 141
		private readonly object jobLock = new object();

		// Token: 0x0400008E RID: 142
		private int traceActivityID;

		// Token: 0x0400008F RID: 143
		private WorkItemQueue workItemQueue;

		// Token: 0x04000090 RID: 144
		private string workItemTrace;

		// Token: 0x04000091 RID: 145
		private JobState state;

		// Token: 0x04000092 RID: 146
		private ManualResetEvent jobDoneEvent;

		// Token: 0x04000093 RID: 147
		private TimeSpan executionTime;
	}
}
