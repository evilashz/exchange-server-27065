using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000010 RID: 16
	internal class UserWorkloadManagerPerfCounterWrapper
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003224 File Offset: 0x00001424
		public UserWorkloadManagerPerfCounterWrapper(BudgetType budgetType)
		{
			try
			{
				this.delayTimeThreshold = (UserWorkloadManagerPerfCounterWrapper.defaultDelayTimeThreshold ?? DefaultThrottlingAlertValues.DelayTimeThreshold(budgetType));
				this.perfCounters = MSExchangeUserWorkloadManager.GetInstance(budgetType.ToString());
				this.perfCounters.DelayTimeThreshold.RawValue = (long)this.delayTimeThreshold;
				this.lastClearTime = TimeProvider.UtcNow;
				this.perfCounters.AverageTaskWaitTime.RawValue = 0L;
				this.perfCounters.MaxTaskQueueLength.RawValue = (long)UserWorkloadManagerPerfCounterWrapper.maxTasksQueued;
				this.perfCounters.MaxWorkerThreadCount.RawValue = (long)UserWorkloadManagerPerfCounterWrapper.maxThreadCount;
				this.perfCounters.MaxDelayedTasks.RawValue = (long)UserWorkloadManagerPerfCounterWrapper.maxDelayCacheTasks;
				this.perfCounters.TaskQueueLength.RawValue = 0L;
				this.perfCounters.TotalNewTaskRejections.RawValue = 0L;
				this.perfCounters.TotalNewTasks.RawValue = 0L;
				this.perfCounters.TotalTaskExecutionFailures.RawValue = 0L;
				this.perfCounters.MaxDelayPerMinute.RawValue = 0L;
				this.perfCounters.TaskTimeoutsPerMinute.RawValue = 0L;
				this.initialized = true;
			}
			catch (Exception ex)
			{
				this.initialized = false;
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_InitializePerformanceCountersFailed, string.Empty, new object[]
				{
					ex.ToString()
				});
				ExTraceGlobals.ClientThrottlingTracer.TraceError<string, string>(0L, "[UserWorkloadManagerPerfCounterWrapper::PerfCounters] Perf counter initialization failed with exception type: {0}, Messsage: {1}", ex.GetType().FullName, ex.Message);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000033D0 File Offset: 0x000015D0
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000033D7 File Offset: 0x000015D7
		public static bool ConfigInitialized { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000033DF File Offset: 0x000015DF
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000033E6 File Offset: 0x000015E6
		internal static TimeSpan PerfCounterRefreshWindow
		{
			get
			{
				return UserWorkloadManagerPerfCounterWrapper.perfCounterRefreshWindow;
			}
			set
			{
				UserWorkloadManagerPerfCounterWrapper.perfCounterRefreshWindow = value;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000033F0 File Offset: 0x000015F0
		public static void Initialize(int maxTasksQueued, int maxThreadCount, int maxDelayCacheTasks, int? delayTimeThreshold)
		{
			if (UserWorkloadManagerPerfCounterWrapper.ConfigInitialized)
			{
				if (UserWorkloadManagerPerfCounterWrapper.maxTasksQueued == maxTasksQueued && UserWorkloadManagerPerfCounterWrapper.maxThreadCount == maxThreadCount && UserWorkloadManagerPerfCounterWrapper.maxDelayCacheTasks == maxDelayCacheTasks && UserWorkloadManagerPerfCounterWrapper.defaultDelayTimeThreshold == delayTimeThreshold)
				{
					return;
				}
				throw new InvalidOperationException("WorkloadManager PerformanceCounters were already initialized.");
			}
			else
			{
				if (delayTimeThreshold != null && delayTimeThreshold.Value <= 0)
				{
					throw new ArgumentOutOfRangeException("delayTimeThreshold", delayTimeThreshold.Value, "delayTimeThreshold must be greater than 0.");
				}
				try
				{
					UserWorkloadManagerPerfCounterWrapper.maxTasksQueued = maxTasksQueued;
					UserWorkloadManagerPerfCounterWrapper.maxThreadCount = maxThreadCount;
					UserWorkloadManagerPerfCounterWrapper.maxDelayCacheTasks = maxDelayCacheTasks;
					UserWorkloadManagerPerfCounterWrapper.defaultDelayTimeThreshold = delayTimeThreshold;
					UserWorkloadManagerPerfCounterWrapper.ConfigInitialized = true;
				}
				catch (Exception ex)
				{
					UserWorkloadManagerPerfCounterWrapper.ConfigInitialized = false;
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_InitializePerformanceCountersFailed, string.Empty, new object[]
					{
						ex.ToString()
					});
					ExTraceGlobals.ClientThrottlingTracer.TraceError<string, string>(0L, "[UserWorkloadManagerPerfCounterWrapper::PerfCounters] Perf counter initialization failed with exception type: {0}, Messsage: {1}", ex.GetType().FullName, ex.Message);
				}
				return;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003519 File Offset: 0x00001719
		public void IncrementTimeoutsSeen()
		{
			this.SafeUpdateCounter("TaskTimeoutsPerMinute", delegate
			{
				this.ClearDictionaryIfNecessary();
				this.perfCounters.TaskTimeoutsPerMinute.Increment();
			});
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003534 File Offset: 0x00001734
		public void UpdateMaxDelay(BudgetKey key, int delay)
		{
			if (!this.initialized)
			{
				return;
			}
			this.ClearDictionaryIfNecessary();
			bool flag = false;
			lock (this.syncRoot)
			{
				if (this.maxDelay < delay)
				{
					this.maxDelay = delay;
					this.perfCounters.MaxDelayPerMinute.RawValue = (long)delay;
				}
				if (delay >= this.delayTimeThreshold)
				{
					flag = !UserWorkloadManagerPerfCounterWrapper.uniqueDelayedBudgetKeys.ContainsKey(key);
					if (flag)
					{
						UserWorkloadManagerPerfCounterWrapper.uniqueDelayedBudgetKeys[key] = 0;
					}
				}
			}
			if (flag)
			{
				this.perfCounters.UsersDelayedXMillisecondsPerMinute.Increment();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000035DC File Offset: 0x000017DC
		public MSExchangeUserWorkloadManagerInstance GetCounterForTest()
		{
			if (!this.initialized)
			{
				return null;
			}
			return this.perfCounters;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003620 File Offset: 0x00001820
		public void UpdateAverageTaskWaitTime(long newValue)
		{
			this.SafeUpdateCounter("AverageTaskWaitTime", delegate
			{
				this.perfCounters.AverageTaskWaitTime.RawValue = (long)UserWorkloadManagerPerfCounterWrapper.averageTaskWaitTime.Update((float)newValue);
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003680 File Offset: 0x00001880
		public void UpdateTaskQueueLength(long length)
		{
			this.SafeUpdateCounter("TaskQueueLength", delegate
			{
				this.perfCounters.TaskQueueLength.RawValue = length;
			});
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000036CB File Offset: 0x000018CB
		public void UpdateTotalNewTaskRejectionsCount()
		{
			this.SafeUpdateCounter("TotalNewTaskRejections", delegate
			{
				this.perfCounters.TotalNewTaskRejections.Increment();
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000036F7 File Offset: 0x000018F7
		public void UpdateTotalNewTasksCount()
		{
			this.SafeUpdateCounter("TotalNewTasks", delegate
			{
				this.perfCounters.TotalNewTasks.Increment();
			});
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003723 File Offset: 0x00001923
		public void UpdateTotalTaskExecutionFailuresCount()
		{
			this.SafeUpdateCounter("TotalTaskExecutionFailures", delegate
			{
				this.perfCounters.TotalTaskExecutionFailures.Increment();
			});
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003764 File Offset: 0x00001964
		public void UpdateCurrentDelayedTasks(long count)
		{
			this.SafeUpdateCounter("CurrentDelayedTasks", delegate
			{
				this.perfCounters.CurrentDelayedTasks.RawValue = count;
			});
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000379C File Offset: 0x0000199C
		internal void ForceClearDictionary()
		{
			lock (this.syncRoot)
			{
				UserWorkloadManagerPerfCounterWrapper.uniqueDelayedBudgetKeys.Clear();
				this.maxDelay = 0;
			}
			this.perfCounters.MaxDelayPerMinute.RawValue = 0L;
			this.perfCounters.TaskTimeoutsPerMinute.RawValue = 0L;
			this.perfCounters.UsersDelayedXMillisecondsPerMinute.RawValue = 0L;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003820 File Offset: 0x00001A20
		private void SafeUpdateCounter(string counterName, Action updateAction)
		{
			if (this.initialized)
			{
				try
				{
					updateAction();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceError<InvalidOperationException>(0L, "Failed to update " + counterName + " performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003870 File Offset: 0x00001A70
		private void ClearDictionaryIfNecessary()
		{
			if (TimeProvider.UtcNow - this.lastClearTime > UserWorkloadManagerPerfCounterWrapper.perfCounterRefreshWindow)
			{
				this.lastClearTime = TimeProvider.UtcNow;
				this.ForceClearDictionary();
			}
		}

		// Token: 0x0400003C RID: 60
		private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400003D RID: 61
		private static RunningAverageFloat averageTaskWaitTime = new RunningAverageFloat(25);

		// Token: 0x0400003E RID: 62
		private static int? defaultDelayTimeThreshold;

		// Token: 0x0400003F RID: 63
		private static TimeSpan perfCounterRefreshWindow = UserWorkloadManagerPerfCounterWrapper.OneMinute;

		// Token: 0x04000040 RID: 64
		private static ConcurrentDictionary<BudgetKey, int> uniqueDelayedBudgetKeys = new ConcurrentDictionary<BudgetKey, int>();

		// Token: 0x04000041 RID: 65
		private static int maxTasksQueued;

		// Token: 0x04000042 RID: 66
		private static int maxThreadCount;

		// Token: 0x04000043 RID: 67
		private static int maxDelayCacheTasks;

		// Token: 0x04000044 RID: 68
		private readonly int delayTimeThreshold;

		// Token: 0x04000045 RID: 69
		private readonly bool initialized;

		// Token: 0x04000046 RID: 70
		private object syncRoot = new object();

		// Token: 0x04000047 RID: 71
		private int maxDelay;

		// Token: 0x04000048 RID: 72
		private DateTime lastClearTime;

		// Token: 0x04000049 RID: 73
		private MSExchangeUserWorkloadManagerInstance perfCounters;
	}
}
