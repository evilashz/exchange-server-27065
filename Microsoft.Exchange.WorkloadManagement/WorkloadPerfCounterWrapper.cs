using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement.EventLogs;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000012 RID: 18
	internal class WorkloadPerfCounterWrapper
	{
		// Token: 0x060000AA RID: 170 RVA: 0x000039A0 File Offset: 0x00001BA0
		public WorkloadPerfCounterWrapper(SystemWorkloadBase workload)
		{
			if (workload == null)
			{
				throw new ArgumentNullException("workload");
			}
			this.Workload = workload;
			string text = null;
			try
			{
				text = ResourceLoadPerfCounterWrapper.GetDefaultInstanceName();
				text = text + "_" + workload.WorkloadType;
				if (!string.Equals(workload.WorkloadType.ToString(), workload.Id, StringComparison.InvariantCultureIgnoreCase))
				{
					text = text + "_" + workload.Id;
				}
				this.perfCounters = MSExchangeWorkloadManagementWorkload.GetInstance(text);
				ExTraceGlobals.CommonTracer.TraceDebug<string>((long)this.GetHashCode(), "[WorkloadPerfCounterWrapper.ctor] Creating perf counter wrapper instance for '{0}'", text);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CommonTracer.TraceError<string, Exception>((long)this.GetHashCode(), "[WorkloadPerfCounterWrapper.ctor] Failed to create perf counter instance '{0}'.  Exception: {1}", text ?? "<NULL>", ex);
				WorkloadManagerEventLogger.LogEvent(WorkloadManagementEventLogConstants.Tuple_WorkloadPerformanceCounterInitializationFailure, workload.WorkloadType.ToString(), new object[]
				{
					workload.WorkloadType,
					workload.Id,
					ex
				});
				this.perfCounters = null;
			}
			this.UpdateActiveTasks(0L);
			this.UpdateBlockedTasks(0L);
			this.UpdateQueuedTasks(0L);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003B00 File Offset: 0x00001D00
		private WorkloadPerfCounterWrapper()
		{
			this.Workload = null;
			this.perfCounters = null;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003B55 File Offset: 0x00001D55
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003B5D File Offset: 0x00001D5D
		internal SystemWorkloadBase Workload { get; private set; }

		// Token: 0x060000AE RID: 174 RVA: 0x00003B8C File Offset: 0x00001D8C
		internal void UpdateActiveTasks(long newCount)
		{
			this.SafeTouchCounter(delegate
			{
				this.perfCounters.ActiveTasks.RawValue = newCount;
			});
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003BE4 File Offset: 0x00001DE4
		internal void UpdateQueuedTasks(long newCount)
		{
			this.SafeTouchCounter(delegate
			{
				this.perfCounters.QueuedTasks.RawValue = newCount;
			});
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C52 File Offset: 0x00001E52
		internal void UpdateTaskCompletion(TimeSpan taskLength)
		{
			this.SafeTouchCounter(delegate
			{
				this.tasksPerMinute.Add(1U);
				this.perfCounters.TasksPerMinute.RawValue = (long)((ulong)this.tasksPerMinute.GetValue());
				this.perfCounters.CompletedTasks.Increment();
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003C79 File Offset: 0x00001E79
		internal void UpdateTaskYielded()
		{
			this.SafeTouchCounter(delegate
			{
				this.perfCounters.YieldedTasks.Increment();
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003CEC File Offset: 0x00001EEC
		internal void UpdateTaskStepLength(TimeSpan taskStepLength)
		{
			this.SafeTouchCounter(delegate
			{
				this.averageTaskStepLength.Add(Environment.TickCount, (uint)taskStepLength.TotalMilliseconds);
				this.perfCounters.AverageTaskStepLength.RawValue = (long)this.averageTaskStepLength.GetValue();
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003D44 File Offset: 0x00001F44
		internal void UpdateBlockedTasks(long newValue)
		{
			this.SafeTouchCounter(delegate
			{
				this.perfCounters.BlockedTasks.RawValue = newValue;
			});
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003DA4 File Offset: 0x00001FA4
		internal void UpdateActiveState(bool active)
		{
			this.SafeTouchCounter(delegate
			{
				this.perfCounters.Active.RawValue = (active ? 1L : 0L);
			});
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003DF0 File Offset: 0x00001FF0
		internal void Remove()
		{
			this.SafeTouchCounter(delegate
			{
				MSExchangeWorkloadManagementWorkload.RemoveInstance(this.perfCounters.Name);
				this.perfCounters = null;
			});
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003E04 File Offset: 0x00002004
		private void SafeTouchCounter(Action action)
		{
			if (this.perfCounters != null)
			{
				lock (this.instanceLock)
				{
					if (this.perfCounters != null)
					{
						action();
					}
				}
			}
		}

		// Token: 0x0400004C RID: 76
		public static readonly WorkloadPerfCounterWrapper Empty = new WorkloadPerfCounterWrapper();

		// Token: 0x0400004D RID: 77
		private object instanceLock = new object();

		// Token: 0x0400004E RID: 78
		private MSExchangeWorkloadManagementWorkloadInstance perfCounters;

		// Token: 0x0400004F RID: 79
		private FixedTimeSum tasksPerMinute = new FixedTimeSum(5000, 12);

		// Token: 0x04000050 RID: 80
		private FixedTimeAverage averageTaskStepLength = new FixedTimeAverage(5000, 12, Environment.TickCount);
	}
}
