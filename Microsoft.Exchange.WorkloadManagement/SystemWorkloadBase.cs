using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SystemWorkloadBase
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00008810 File Offset: 0x00006A10
		public bool Registered
		{
			get
			{
				bool result;
				lock (this.instanceLock)
				{
					result = (this.ClassificationBlock != null);
				}
				return result;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00008858 File Offset: 0x00006A58
		public WorkloadClassification Classification
		{
			get
			{
				WorkloadClassification result;
				lock (this.instanceLock)
				{
					result = ((this.ClassificationBlock == null) ? VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).WorkloadManagement.GetObject<IWorkloadSettings>(this.WorkloadType, new object[0]).Classification : this.ClassificationBlock.WorkloadClassification);
				}
				return result;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001FA RID: 506
		public abstract WorkloadType WorkloadType { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001FB RID: 507
		public abstract string Id { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001FC RID: 508
		public abstract int TaskCount { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001FD RID: 509
		public abstract int BlockedTaskCount { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000088D8 File Offset: 0x00006AD8
		public bool IsEnabled
		{
			get
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
				IWorkloadSettings @object = snapshot.WorkloadManagement.GetObject<IWorkloadSettings>(this.WorkloadType, new object[0]);
				if (!@object.Enabled)
				{
					return false;
				}
				if (@object.EnabledDuringBlackout)
				{
					return true;
				}
				IBlackoutSettings blackout = snapshot.WorkloadManagement.Blackout;
				if (!(blackout.StartTime != blackout.EndTime))
				{
					return true;
				}
				DateTime utcNow = DateTime.UtcNow;
				DateTime t = utcNow.Date + blackout.StartTime;
				DateTime t2 = utcNow.Date + blackout.EndTime;
				if (t >= t2)
				{
					t2 = t2.AddDays(1.0);
				}
				return !(t < utcNow) || !(utcNow < t2);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000089B4 File Offset: 0x00006BB4
		public bool Paused
		{
			get
			{
				bool result;
				lock (this.instanceLock)
				{
					if (this.pausedUntil != null)
					{
						TimeSpan t = this.pausedUntil.Value - DateTime.UtcNow;
						if (t < TimeSpan.Zero)
						{
							this.pausedUntil = null;
							result = false;
						}
						else
						{
							result = true;
						}
					}
					else
					{
						result = false;
					}
				}
				return result;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008A38 File Offset: 0x00006C38
		public int MaxConcurrency
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).WorkloadManagement.GetObject<IWorkloadSettings>(this.WorkloadType, new object[0]).MaxConcurrency;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008A74 File Offset: 0x00006C74
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00008A7B File Offset: 0x00006C7B
		internal static TimeSpan PauseDuration
		{
			get
			{
				return SystemWorkloadBase.pauseDuration;
			}
			set
			{
				SystemWorkloadBase.pauseDuration = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008A83 File Offset: 0x00006C83
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00008A8C File Offset: 0x00006C8C
		internal ClassificationBlock ClassificationBlock
		{
			get
			{
				return this.classificationBlock;
			}
			set
			{
				lock (this.instanceLock)
				{
					if (this.perfCounter != null)
					{
						this.perfCounter.Remove();
					}
					this.classificationBlock = value;
					this.perfCounter = ((value == null) ? WorkloadPerfCounterWrapper.Empty : new WorkloadPerfCounterWrapper(this));
				}
				this.perfCounter.UpdateActiveState(true);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008B04 File Offset: 0x00006D04
		internal int ActiveThreadCount
		{
			get
			{
				return this.activeThreads;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008B0C File Offset: 0x00006D0C
		public override string ToString()
		{
			return this.WorkloadType.ToString();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008B20 File Offset: 0x00006D20
		internal void Wake()
		{
			lock (this.instanceLock)
			{
				this.pausedUntil = null;
			}
			if (this.perfCounter != null)
			{
				this.perfCounter.UpdateActiveState(true);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008B7C File Offset: 0x00006D7C
		internal void Pause()
		{
			lock (this.instanceLock)
			{
				this.pausedUntil = new DateTime?(DateTime.UtcNow.Add(SystemWorkloadBase.PauseDuration));
			}
			if (this.perfCounter != null)
			{
				this.perfCounter.UpdateActiveState(false);
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008BE8 File Offset: 0x00006DE8
		internal SystemTaskBase[] GetRunningTasks()
		{
			SystemTaskBase[] result;
			lock (this.instanceLock)
			{
				if (this.runningTasks == null || this.runningTasks.Count < 1)
				{
					result = null;
				}
				else
				{
					SystemTaskBase[] array = new SystemTaskBase[this.runningTasks.Count];
					this.runningTasks.Values.CopyTo(array, 0);
					result = array;
				}
			}
			return result;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008C64 File Offset: 0x00006E64
		internal SystemTaskBase InternalGetTask()
		{
			if (!this.IsEnabled)
			{
				return null;
			}
			int num = 0;
			SystemTaskBase systemTaskBase = null;
			bool flag = false;
			lock (this.instanceLock)
			{
				if (this.runningTasks.Count + this.requestedTaskCount + 1 <= this.MaxConcurrency)
				{
					this.requestedTaskCount++;
					flag = true;
				}
			}
			if (!flag)
			{
				ExTraceGlobals.SchedulerTracer.TraceDebug<string>((long)this.GetHashCode(), "[SystemWorkloadBase.GetTask] Maximum limit for workload {0} is reached.", this.Id);
				return null;
			}
			try
			{
				systemTaskBase = this.GetTask(this.resourceReservationContext);
			}
			finally
			{
				if (systemTaskBase == null)
				{
					lock (this.instanceLock)
					{
						this.requestedTaskCount--;
					}
				}
			}
			if (systemTaskBase == null)
			{
				ExTraceGlobals.SchedulerTracer.TraceDebug<string>((long)this.GetHashCode(), "[SystemWorkloadBase.GetTask] Workload {0} returned null for GetTask.", this.Id);
				return null;
			}
			lock (this.instanceLock)
			{
				try
				{
					if (systemTaskBase.ResourceReservation == null || !systemTaskBase.ResourceReservation.IsActive)
					{
						throw new InvalidOperationException(string.Format("Task {0} from workload {1} has invalid resource reservation. Resources should be reserved for a task before task is returned from GetTask.", systemTaskBase, systemTaskBase.Workload));
					}
					this.runningTasks.Add(systemTaskBase.Identity, systemTaskBase);
					num = this.runningTasks.Count;
				}
				finally
				{
					this.requestedTaskCount--;
				}
			}
			if (this.perfCounter != null)
			{
				this.perfCounter.UpdateActiveTasks((long)num);
				this.perfCounter.UpdateBlockedTasks((long)this.BlockedTaskCount);
				this.perfCounter.UpdateQueuedTasks((long)this.TaskCount);
			}
			return systemTaskBase;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008E48 File Offset: 0x00007048
		internal void InternalCompleteTask(SystemTaskBase task)
		{
			this.RemoveRunningTask(task);
			this.CompleteTask(task);
			this.perfCounter.UpdateTaskCompletion(task.ExecutionTime);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008E69 File Offset: 0x00007069
		internal void InternalYieldTask(SystemTaskBase task)
		{
			this.RemoveRunningTask(task);
			this.YieldTask(task);
			this.perfCounter.UpdateTaskYielded();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008E84 File Offset: 0x00007084
		internal void UpdateTaskStepLength(TimeSpan newStepLength)
		{
			this.perfCounter.UpdateTaskStepLength(newStepLength);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008E92 File Offset: 0x00007092
		internal void IncrementActiveThreadCount()
		{
			Interlocked.Increment(ref this.activeThreads);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008EA0 File Offset: 0x000070A0
		internal void DecrementActiveThreadCount()
		{
			Interlocked.Decrement(ref this.activeThreads);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008EAE File Offset: 0x000070AE
		internal void SetResourceReservationContext(ResourceReservationContext context)
		{
			if (context != null && this.resourceReservationContext != null)
			{
				throw new InvalidOperationException("Resource reservation context is already set.");
			}
			this.resourceReservationContext = context;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008ECD File Offset: 0x000070CD
		protected virtual void CompleteTask(SystemTaskBase task)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008ECF File Offset: 0x000070CF
		protected virtual void YieldTask(SystemTaskBase task)
		{
		}

		// Token: 0x06000213 RID: 531
		protected abstract SystemTaskBase GetTask(ResourceReservationContext context);

		// Token: 0x06000214 RID: 532 RVA: 0x00008ED4 File Offset: 0x000070D4
		private void RemoveRunningTask(SystemTaskBase task)
		{
			int count;
			lock (this.instanceLock)
			{
				this.runningTasks.Remove(task.Identity);
				count = this.runningTasks.Count;
			}
			this.perfCounter.UpdateActiveTasks((long)count);
		}

		// Token: 0x04000102 RID: 258
		private static readonly TimeSpan DefaultPauseDuration = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000103 RID: 259
		private static TimeSpan pauseDuration = SystemWorkloadBase.DefaultPauseDuration;

		// Token: 0x04000104 RID: 260
		private WorkloadPerfCounterWrapper perfCounter = WorkloadPerfCounterWrapper.Empty;

		// Token: 0x04000105 RID: 261
		private object instanceLock = new object();

		// Token: 0x04000106 RID: 262
		private Dictionary<Guid, SystemTaskBase> runningTasks = new Dictionary<Guid, SystemTaskBase>();

		// Token: 0x04000107 RID: 263
		private int activeThreads;

		// Token: 0x04000108 RID: 264
		private int requestedTaskCount;

		// Token: 0x04000109 RID: 265
		private DateTime? pausedUntil;

		// Token: 0x0400010A RID: 266
		private ClassificationBlock classificationBlock;

		// Token: 0x0400010B RID: 267
		private ResourceReservationContext resourceReservationContext;
	}
}
