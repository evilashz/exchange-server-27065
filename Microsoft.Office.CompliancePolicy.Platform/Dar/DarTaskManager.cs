using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x0200006C RID: 108
	public class DarTaskManager
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000AAA1 File Offset: 0x00008CA1
		public DarTaskManager(DarServiceProvider darServiceProvider)
		{
			this.darServiceProvider = darServiceProvider;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public DarServiceProvider ServiceProvider
		{
			get
			{
				return this.darServiceProvider;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		public ExecutionLog ExecutionLog
		{
			get
			{
				if (this.darServiceProvider == null)
				{
					return null;
				}
				return this.darServiceProvider.ExecutionLog;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public static string GetPerfCounterFromTaskState(DarTaskState state)
		{
			switch (state)
			{
			case DarTaskState.None:
				return "DarTasksInStateNone";
			case DarTaskState.Ready:
				return "DarTasksInStateReady";
			case DarTaskState.Running:
				return "DarTasksInStateRunning";
			case DarTaskState.Completed:
				return "DarTasksInStateCompleted";
			case DarTaskState.Failed:
				return "DarTasksInStateFailed";
			case DarTaskState.Cancelled:
				return "DarTasksInStateCancelled";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000AB28 File Offset: 0x00008D28
		public void Enqueue(DarTask task)
		{
			task.TaskState = DarTaskState.Ready;
			task.TaskQueuedTime = DateTime.UtcNow;
			task.SaveStateToSerializedData(this);
			this.darServiceProvider.ExecutionLog.LogInformation("DarTaskManager", null, task.CorrelationId, string.Format("Enqueuing {0}", task), new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTaskManager0.ToString())
			});
			this.darServiceProvider.DarTaskQueue.Enqueue(task);
			this.darServiceProvider.PerformanceCounter.Increment("DarTasksInStateReady");
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		public void Cancel(string taskId)
		{
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000ABC7 File Offset: 0x00008DC7
		public void Pause(string taskId)
		{
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000ABC9 File Offset: 0x00008DC9
		public void Resume(string taskId)
		{
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public IEnumerable<DarTask> Dequeue(int taskCount, DarTaskCategory category, object availableResource = null)
		{
			IEnumerable<DarTask> enumerable = this.DeserializeTaskData(this.darServiceProvider.DarTaskQueue.Dequeue(taskCount, category, availableResource));
			this.darServiceProvider.ExecutionLog.LogInformation("DarTaskManager", null, null, string.Format("Dequeued {0}/{1} task(s), category:{2}, availableResources:{3}. Dequeued Tasks: {4}", new object[]
			{
				enumerable.Count<DarTask>(),
				taskCount.ToString(),
				category.ToString(),
				availableResource ?? "<null>",
				string.Join<DarTask>(",", enumerable = enumerable.ToArray<DarTask>())
			}), new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTaskManager1.ToString())
			});
			return enumerable;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000AC8F File Offset: 0x00008E8F
		public DarTaskExecutionCommand ShouldContinue(DarTask task, out string additionalInformation)
		{
			return this.darServiceProvider.DarWorkloadHost.ShouldContinue(task, out additionalInformation);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000ACA3 File Offset: 0x00008EA3
		public void UpdateTaskState(DarTask darTask)
		{
			this.UpdateTaskState(darTask, (DarTaskExecutionResult)(-1));
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public void UpdateTaskState(DarTask darTask, DarTaskExecutionResult executionResult)
		{
			if (darTask == null)
			{
				throw new ArgumentNullException("darTask");
			}
			this.darServiceProvider.ExecutionLog.LogInformation("DarTaskManager", null, darTask.CorrelationId, string.Format("Updating task {0} state to {1} and serializedTaskData is {2}", darTask.Id, darTask.TaskState, darTask.SerializedTaskData), new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTaskManager2.ToString())
			});
			this.darServiceProvider.DarTaskQueue.UpdateTask(darTask);
			if (darTask.TaskState == DarTaskState.Completed)
			{
				TimeSpan timeSpan = darTask.TaskCompletionTime.Subtract(darTask.TaskQueuedTime);
				this.darServiceProvider.ExecutionLog.LogInformation("DarTaskManager", null, darTask.CorrelationId, string.Format("From queue time to finish task took {0}", timeSpan), new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTaskManager3.ToString())
				});
			}
			if (darTask.PreviousTaskState == DarTaskState.Ready && darTask.TaskState == DarTaskState.Running && darTask.TaskRetryCurrentCount == 0)
			{
				this.ServiceProvider.PerformanceCounter.IncrementBy("DarTasksInStateReady", -1L);
				this.ServiceProvider.PerformanceCounter.Increment("DarTasksInStateRunning");
			}
			if (darTask.PreviousTaskState == DarTaskState.Running && (darTask.TaskState == DarTaskState.Cancelled || darTask.TaskState == DarTaskState.Failed || darTask.TaskState == DarTaskState.Completed))
			{
				this.ServiceProvider.PerformanceCounter.IncrementBy("DarTasksInStateRunning", -1L);
				this.ServiceProvider.PerformanceCounter.Increment(DarTaskManager.GetPerfCounterFromTaskState(darTask.TaskState));
				if (darTask.TaskState == DarTaskState.Completed)
				{
					this.ServiceProvider.PerformanceCounter.IncrementBy("DarTaskAverageDuration", (darTask.TaskCompletionTime - darTask.TaskQueuedTime).Ticks, "DarTaskAverageDurationBase");
				}
			}
			if (darTask.PreviousTaskState != darTask.TaskState && executionResult == DarTaskExecutionResult.TransientError && darTask.TaskRetryCurrentCount == 1)
			{
				this.ServiceProvider.PerformanceCounter.Increment("DarTasksTransientFailed");
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AEC3 File Offset: 0x000090C3
		public IEnumerable<DarTask> GetCompletedTasks(DateTime minCompletionTime, string taskType = null, string tenantId = null)
		{
			return this.DeserializeTaskData(this.darServiceProvider.DarTaskQueue.GetCompletedTasks(minCompletionTime, taskType, tenantId));
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B084 File Offset: 0x00009284
		private IEnumerable<DarTask> DeserializeTaskData(IEnumerable<DarTask> rawTasks)
		{
			foreach (DarTask task in rawTasks)
			{
				task.RestoreStateFromSerializedData(this);
				yield return task;
			}
			yield break;
		}

		// Token: 0x0400016E RID: 366
		private const string LoggingClientId = "DarTaskManager";

		// Token: 0x0400016F RID: 367
		private readonly DarServiceProvider darServiceProvider;
	}
}
