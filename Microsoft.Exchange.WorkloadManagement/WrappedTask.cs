using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000042 RID: 66
	internal class WrappedTask
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		public WrappedTask(ITask task)
		{
			this.hashCode = base.GetHashCode();
			this.Initialize(task);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000C026 File Offset: 0x0000A226
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000C02E File Offset: 0x0000A22E
		public ITask Task { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000C037 File Offset: 0x0000A237
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000C03F File Offset: 0x0000A23F
		internal TaskExecuteResult PreviousStepResult { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000C048 File Offset: 0x0000A248
		internal TimeSpan TotalTime
		{
			get
			{
				return WrappedTask.GetElapsed(this.startTickCount);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000C055 File Offset: 0x0000A255
		internal TimeSpan ExecuteTime
		{
			get
			{
				return this.executeElapsed + WrappedTask.GetElapsed(this.executeStart);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C06D File Offset: 0x0000A26D
		internal TimeSpan QueueTime
		{
			get
			{
				return this.queueElapsed + WrappedTask.GetElapsed(this.queueStart);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000C085 File Offset: 0x0000A285
		internal TimeSpan DelayTime
		{
			get
			{
				return this.delayElapsed + WrappedTask.GetElapsed(this.delayStart);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C09D File Offset: 0x0000A29D
		internal int QueuedCount
		{
			get
			{
				return this.queueCount;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000C0A5 File Offset: 0x0000A2A5
		internal int DelayedCount
		{
			get
			{
				return this.delayCount;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000C0AD File Offset: 0x0000A2AD
		internal int ExecuteCount
		{
			get
			{
				return this.executeCount;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000C0B5 File Offset: 0x0000A2B5
		internal TaskLocation TaskLocation
		{
			get
			{
				if (this.delayStart != null)
				{
					return TaskLocation.DelayCache;
				}
				if (this.queueStart != null)
				{
					return TaskLocation.Queue;
				}
				return TaskLocation.Thread;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000C0DE File Offset: 0x0000A2DE
		internal ResourceKey[] PreviousStepResources { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000C0E7 File Offset: 0x0000A2E7
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000C0EF File Offset: 0x0000A2EF
		internal TimeSpan UnaccountedForDelay { get; set; }

		// Token: 0x0600029C RID: 668 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public void Complete()
		{
			WorkloadManagementLogger.SetQueueTime(this.QueueTime, null);
			TimeSpan queueAndDelayTime;
			TimeSpan totalTime;
			this.GetTaskTimes(out queueAndDelayTime, out totalTime);
			this.Task.Complete(queueAndDelayTime, totalTime);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C158 File Offset: 0x0000A358
		public TaskExecuteResult Execute()
		{
			TimeSpan totalTime;
			TimeSpan queueAndDelayTime;
			this.GetTaskTimes(out queueAndDelayTime, out totalTime);
			TaskExecuteResult result = TaskExecuteResult.Undefined;
			this.LocalTimedCall(delegate
			{
				result = this.Task.Execute(queueAndDelayTime, totalTime);
			});
			return result;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		public void Timeout()
		{
			WorkloadManagementLogger.SetQueueTime(this.QueueTime, null);
			TimeSpan queueAndDelayTime;
			TimeSpan totalTime;
			this.GetTaskTimes(out queueAndDelayTime, out totalTime);
			this.Task.Timeout(queueAndDelayTime, totalTime);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C1D5 File Offset: 0x0000A3D5
		public void Cancel()
		{
			this.Task.Cancel();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C21C File Offset: 0x0000A41C
		public TaskExecuteResult CancelStep(LocalizedException exception)
		{
			TaskExecuteResult result = TaskExecuteResult.Undefined;
			this.LocalTimedCall(delegate
			{
				result = this.Task.CancelStep(exception);
				if (result == TaskExecuteResult.Undefined)
				{
					throw new ArgumentException("[WrappedTask::CancelStep] ITask.CancelStep cannot return TaskExecuteResult.Undefined.");
				}
			});
			return result;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C25C File Offset: 0x0000A45C
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C264 File Offset: 0x0000A464
		internal WLMTaskDetails GetTaskDetails()
		{
			WLMTaskDetails wlmtaskDetails = new WLMTaskDetails();
			if (this.Task != null)
			{
				lock (this.instanceLock)
				{
					if (this.Task != null)
					{
						wlmtaskDetails.Description = this.Task.Description;
						TimeSpan totalTime = this.TotalTime;
						wlmtaskDetails.TotalTime = totalTime.ToString();
						wlmtaskDetails.StartTimeUTC = DateTime.UtcNow - totalTime;
						wlmtaskDetails.ExecuteTime = this.ExecuteTime.ToString();
						wlmtaskDetails.ExecuteCount = this.ExecuteCount;
						wlmtaskDetails.QueueTime = this.QueueTime.ToString();
						wlmtaskDetails.QueueCount = this.QueuedCount;
						wlmtaskDetails.DelayTime = this.DelayTime.ToString();
						wlmtaskDetails.DelayCount = this.DelayedCount;
						wlmtaskDetails.Location = this.TaskLocation.ToString();
						wlmtaskDetails.BudgetOwner = this.Task.Budget.Owner.ToString();
					}
				}
			}
			return wlmtaskDetails;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		internal void Initialize(ITask task)
		{
			lock (this.instanceLock)
			{
				this.Task = task;
				this.taskTimeout = (task as ITaskTimeout);
				this.startTickCount = new long?(Stopwatch.GetTimestamp());
				this.PreviousStepResult = TaskExecuteResult.Undefined;
				this.UnaccountedForDelay = TimeSpan.Zero;
				this.executeStart = null;
				this.queueStart = null;
				this.delayStart = null;
				this.executeElapsed = TimeSpan.Zero;
				this.queueElapsed = TimeSpan.Zero;
				this.delayElapsed = TimeSpan.Zero;
				this.queueCount = 0;
				this.delayCount = 0;
				this.executeCount = 0;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C46C File Offset: 0x0000A66C
		internal void StartQueue()
		{
			this.queueStart = new long?(Stopwatch.GetTimestamp());
			this.queueCount++;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C48C File Offset: 0x0000A68C
		internal void EndQueue()
		{
			this.queueElapsed += WrappedTask.GetElapsed(this.queueStart);
			this.queueStart = null;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C4B6 File Offset: 0x0000A6B6
		internal void StartDelay()
		{
			this.delayStart = new long?(Stopwatch.GetTimestamp());
			this.delayCount++;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C4D6 File Offset: 0x0000A6D6
		internal void EndDelay()
		{
			this.delayElapsed += WrappedTask.GetElapsed(this.delayStart);
			this.delayStart = null;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C500 File Offset: 0x0000A700
		internal void StartExecute()
		{
			this.executeStart = new long?(Stopwatch.GetTimestamp());
			this.executeCount++;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C520 File Offset: 0x0000A720
		internal void EndExecute()
		{
			this.executeElapsed += WrappedTask.GetElapsed(this.executeStart);
			this.executeStart = null;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C54A File Offset: 0x0000A74A
		private static long ConvertToDateTimeTicks(long stopwatchTicks)
		{
			return (long)((double)stopwatchTicks * WrappedTask.dateTimeToStopwatchTicksRatio);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C558 File Offset: 0x0000A758
		private static TimeSpan GetElapsed(long? start)
		{
			if (start != null)
			{
				try
				{
					long value = WrappedTask.ConvertToDateTimeTicks(Stopwatch.GetTimestamp() - start.Value);
					return TimeSpan.FromTicks(value);
				}
				catch (InvalidOperationException)
				{
					return TimeSpan.Zero;
				}
			}
			return TimeSpan.Zero;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		private TimeSpan GetActionTimeout(CostType costType)
		{
			if (this.taskTimeout != null)
			{
				return this.taskTimeout.GetActionTimeout(costType);
			}
			return Budget.GetMaxActionTime(costType);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000C5C9 File Offset: 0x0000A7C9
		private void GetTaskTimes(out TimeSpan queueAndDelayTime, out TimeSpan totalTime)
		{
			totalTime = this.TotalTime;
			queueAndDelayTime = this.QueueTime + this.DelayTime;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		private void LocalTimedCall(Action action)
		{
			IActivityScope activityScope = null;
			IActivityScope activityScope2 = null;
			IBudget budget = this.Task.Budget;
			bool flag = budget != null && budget.LocalCostHandle == null;
			if (flag)
			{
				this.StartExecute();
				budget.StartLocal(this.Task.Description, default(TimeSpan));
				budget.LocalCostHandle.MaxLiveTime = this.GetActionTimeout(CostType.CAS);
			}
			try
			{
				action();
			}
			finally
			{
				if (flag)
				{
					try
					{
						activityScope = ActivityContext.GetCurrentActivityScope();
						activityScope2 = this.Task.GetActivityScope();
						if (activityScope2 != null)
						{
							ActivityContext.SetThreadScope(activityScope2);
						}
						budget.EndLocal();
					}
					finally
					{
						if (activityScope != null && activityScope != activityScope2)
						{
							ActivityContext.SetThreadScope(activityScope);
						}
						else if (activityScope2 != null)
						{
							ActivityContext.ClearThreadScope();
						}
					}
					this.EndExecute();
				}
			}
		}

		// Token: 0x04000150 RID: 336
		private static readonly double dateTimeToStopwatchTicksRatio = 10000000.0 / (double)Stopwatch.Frequency;

		// Token: 0x04000151 RID: 337
		private readonly int hashCode;

		// Token: 0x04000152 RID: 338
		private TimeSpan executeElapsed = TimeSpan.Zero;

		// Token: 0x04000153 RID: 339
		private long? startTickCount;

		// Token: 0x04000154 RID: 340
		private ITaskTimeout taskTimeout;

		// Token: 0x04000155 RID: 341
		private object instanceLock = new object();

		// Token: 0x04000156 RID: 342
		private long? executeStart = null;

		// Token: 0x04000157 RID: 343
		private long? queueStart = null;

		// Token: 0x04000158 RID: 344
		private long? delayStart = null;

		// Token: 0x04000159 RID: 345
		private TimeSpan queueElapsed = TimeSpan.Zero;

		// Token: 0x0400015A RID: 346
		private TimeSpan delayElapsed = TimeSpan.Zero;

		// Token: 0x0400015B RID: 347
		private int queueCount;

		// Token: 0x0400015C RID: 348
		private int delayCount;

		// Token: 0x0400015D RID: 349
		private int executeCount;
	}
}
