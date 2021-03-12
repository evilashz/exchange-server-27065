using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200000B RID: 11
	internal class WorkloadExecution : DisposeTrackableBase
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002B52 File Offset: 0x00000D52
		public WorkloadExecution(ITaskProviderManager taskProviderManager) : this(taskProviderManager, true)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002B5C File Offset: 0x00000D5C
		public WorkloadExecution(ITaskProviderManager taskProviderManager, bool useTimer)
		{
			if (taskProviderManager == null)
			{
				throw new ArgumentNullException("taskProviderManager");
			}
			this.taskProviderManager = taskProviderManager;
			if (useTimer && this.RefreshCycle > TimeSpan.Zero)
			{
				using (ActivityContext.SuppressThreadScope())
				{
					this.threadSchedulerTimer = new Timer(new TimerCallback(this.PeriodicScheduler), null, this.RefreshCycle, this.RefreshCycle);
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public TimeSpan RefreshCycle
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).WorkloadManagement.SystemWorkloadManager.RefreshCycle;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002C14 File Offset: 0x00000E14
		public int MaxQueuedTaskCount
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).WorkloadManagement.SystemWorkloadManager.MaxConcurrency;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002C3F File Offset: 0x00000E3F
		public WorkloadExecutionStatus Status
		{
			get
			{
				if (!this.shouldStop)
				{
					return WorkloadExecutionStatus.Started;
				}
				if (this.queuedTaskCount != 0)
				{
					return WorkloadExecutionStatus.Stopping;
				}
				return WorkloadExecutionStatus.Stopped;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002C58 File Offset: 0x00000E58
		public int QueuedTaskCount
		{
			get
			{
				return this.queuedTaskCount;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002C60 File Offset: 0x00000E60
		public int ActiveTaskCount
		{
			get
			{
				return this.activeTaskCount;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C68 File Offset: 0x00000E68
		public void Stop()
		{
			this.shouldStop = true;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002C73 File Offset: 0x00000E73
		public void Start()
		{
			this.shouldStop = false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002C80 File Offset: 0x00000E80
		internal void QueueTaskExecution()
		{
			for (int i = 0; i < this.taskProviderManager.GetProviderCount(); i++)
			{
				if (!this.shouldStop)
				{
					WorkloadExecution.TaskData nextTaskData = this.GetNextTaskData();
					if (nextTaskData != null)
					{
						ExTraceGlobals.ExecutionTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Queueing task {0} for execution.", nextTaskData.Task.Identity);
						using (ActivityContext.SuppressThreadScope())
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExecuteTaskCallback), nextTaskData);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002D10 File Offset: 0x00000F10
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.Stop();
				if (this.threadSchedulerTimer != null)
				{
					this.threadSchedulerTimer.Dispose();
					this.threadSchedulerTimer = null;
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D35 File Offset: 0x00000F35
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WorkloadExecution>(this);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D40 File Offset: 0x00000F40
		protected virtual void ExecuteTask(SystemTaskBase task)
		{
			using (ActivityScope activityScope = WorkloadExecution.GetActivityScope(task))
			{
				DateTime utcNow = DateTime.UtcNow;
				TaskStepResult taskStepResult = task.InternalExecute();
				task.Workload.UpdateTaskStepLength(DateTime.UtcNow - utcNow);
				switch (taskStepResult)
				{
				case TaskStepResult.Complete:
					WorkloadExecution.CompleteTask(task, activityScope);
					break;
				case TaskStepResult.Yield:
					WorkloadExecution.YieldTask(task, activityScope);
					break;
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002DB8 File Offset: 0x00000FB8
		private static ActivityScope GetActivityScope(SystemTaskBase task)
		{
			ActivityContextState activityContextState = task.InternalResume();
			ActivityScope activityScope;
			if (activityContextState != null)
			{
				activityScope = ActivityContext.Resume(activityContextState, task);
			}
			else
			{
				activityScope = ActivityContext.Start(task);
				activityScope.Component = task.Workload.WorkloadType.ToString();
				activityScope.ComponentInstance = task.Workload.Id;
				WorkloadManagementLogger.SetWorkloadMetadataValues(task.Workload.WorkloadType.ToString(), task.Workload.Classification.ToString(), false, false, activityScope);
			}
			return activityScope;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E42 File Offset: 0x00001042
		private static void CompleteTask(SystemTaskBase task, ActivityScope activityScope)
		{
			activityScope.End();
			ActivityContext.ClearThreadScope();
			task.InternalComplete();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E58 File Offset: 0x00001058
		private static void YieldTask(SystemTaskBase task, ActivityScope activityScope)
		{
			ActivityContextState state = activityScope.Suspend();
			ActivityContext.ClearThreadScope();
			task.InternalYield(state);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E78 File Offset: 0x00001078
		private WorkloadExecution.TaskData GetNextTaskData()
		{
			int num = Interlocked.Increment(ref this.queuedTaskCount);
			ITaskProvider taskProvider = null;
			WorkloadExecution.TaskData taskData = null;
			try
			{
				if (num <= this.MaxQueuedTaskCount)
				{
					taskProvider = this.taskProviderManager.GetNextProvider();
					if (taskProvider != null)
					{
						SystemTaskBase nextTask = taskProvider.GetNextTask();
						if (nextTask != null)
						{
							taskData = new WorkloadExecution.TaskData(taskProvider, nextTask);
						}
					}
				}
			}
			finally
			{
				if (taskData == null)
				{
					Interlocked.Decrement(ref this.queuedTaskCount);
					if (taskProvider != null)
					{
						taskProvider.Dispose();
					}
				}
			}
			return taskData;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002EEC File Offset: 0x000010EC
		private void ExecuteTaskCallback(object state)
		{
			Interlocked.Increment(ref this.activeTaskCount);
			WorkloadExecution.TaskData taskData = (WorkloadExecution.TaskData)state;
			try
			{
				taskData.Task.Workload.IncrementActiveThreadCount();
				this.ExecuteTask(taskData.Task);
			}
			finally
			{
				taskData.Task.Workload.DecrementActiveThreadCount();
				taskData.Dispose();
				Interlocked.Decrement(ref this.activeTaskCount);
				Interlocked.Decrement(ref this.queuedTaskCount);
			}
			this.QueueTaskExecution();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F70 File Offset: 0x00001170
		private void PeriodicScheduler(object o)
		{
			ExTraceGlobals.ExecutionTracer.TraceDebug((long)this.GetHashCode(), "Periodic scheduler is queueing a new task for execution");
			this.QueueTaskExecution();
		}

		// Token: 0x0400002E RID: 46
		private ITaskProviderManager taskProviderManager;

		// Token: 0x0400002F RID: 47
		private Timer threadSchedulerTimer;

		// Token: 0x04000030 RID: 48
		private int queuedTaskCount;

		// Token: 0x04000031 RID: 49
		private int activeTaskCount;

		// Token: 0x04000032 RID: 50
		private volatile bool shouldStop = true;

		// Token: 0x0200000C RID: 12
		private class TaskData : DisposeTrackableBase
		{
			// Token: 0x0600007A RID: 122 RVA: 0x00002F8E File Offset: 0x0000118E
			public TaskData(ITaskProvider taskProvider, SystemTaskBase task)
			{
				this.TaskProvider = taskProvider;
				this.Task = task;
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600007B RID: 123 RVA: 0x00002FA4 File Offset: 0x000011A4
			// (set) Token: 0x0600007C RID: 124 RVA: 0x00002FAC File Offset: 0x000011AC
			public ITaskProvider TaskProvider { get; private set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x0600007D RID: 125 RVA: 0x00002FB5 File Offset: 0x000011B5
			// (set) Token: 0x0600007E RID: 126 RVA: 0x00002FBD File Offset: 0x000011BD
			public SystemTaskBase Task { get; private set; }

			// Token: 0x0600007F RID: 127 RVA: 0x00002FC6 File Offset: 0x000011C6
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					this.TaskProvider.Dispose();
				}
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00002FD6 File Offset: 0x000011D6
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<WorkloadExecution.TaskData>(this);
			}
		}
	}
}
