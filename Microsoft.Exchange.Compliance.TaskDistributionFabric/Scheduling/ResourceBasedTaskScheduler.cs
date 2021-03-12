using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Scheduling
{
	// Token: 0x02000023 RID: 35
	internal class ResourceBasedTaskScheduler : TaskScheduler
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000053C8 File Offset: 0x000035C8
		static ResourceBasedTaskScheduler()
		{
			int maxThreadCount;
			int num;
			ThreadPool.GetMaxThreads(out maxThreadCount, out num);
			UserWorkloadManager.Initialize(maxThreadCount, TaskDistributionSettings.MaxQueuePerBlock, TaskDistributionSettings.MaxQueuePerBlock, TimeSpan.FromHours(4.0), null);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000540F File Offset: 0x0000360F
		private ResourceBasedTaskScheduler()
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005417 File Offset: 0x00003617
		public static TaskScheduler Instance
		{
			get
			{
				return ResourceBasedTaskScheduler.instance;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000054C0 File Offset: 0x000036C0
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			yield break;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000054DD File Offset: 0x000036DD
		protected override void QueueTask(Task task)
		{
			UserWorkloadManager.Singleton.TrySubmitNewTask(new ResourceBasedTaskScheduler.TaskDistributionWrappedTask(task, new Func<Task, bool>(base.TryExecuteTask)));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000054FC File Offset: 0x000036FC
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return false;
		}

		// Token: 0x04000045 RID: 69
		private static TaskScheduler instance = new ResourceBasedTaskScheduler();

		// Token: 0x02000024 RID: 36
		private class TaskDistributionWrappedTask : ITask
		{
			// Token: 0x060000B6 RID: 182 RVA: 0x000054FF File Offset: 0x000036FF
			public TaskDistributionWrappedTask(Task task, Func<Task, bool> tryExecuteTask)
			{
				this.task = task;
				this.input = (task.AsyncState as ComplianceMessage);
				this.tryExecuteTask = tryExecuteTask;
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005526 File Offset: 0x00003726
			// (set) Token: 0x060000B8 RID: 184 RVA: 0x00005533 File Offset: 0x00003733
			string ITask.Description
			{
				get
				{
					return base.GetType().Name;
				}
				set
				{
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005535 File Offset: 0x00003735
			TimeSpan ITask.MaxExecutionTime
			{
				get
				{
					return TaskDistributionSettings.ApplicationExecutionTime;
				}
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000BA RID: 186 RVA: 0x0000553C File Offset: 0x0000373C
			// (set) Token: 0x060000BB RID: 187 RVA: 0x00005544 File Offset: 0x00003744
			object ITask.State { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000BC RID: 188 RVA: 0x0000554D File Offset: 0x0000374D
			WorkloadSettings ITask.WorkloadSettings
			{
				get
				{
					return new WorkloadSettings(WorkloadType.DarRuntime, true);
				}
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00005557 File Offset: 0x00003757
			IBudget ITask.Budget
			{
				get
				{
					return StandardBudget.Acquire(new UnthrottledBudgetKey("TaskDistribution", BudgetType.Anonymous));
				}
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00005569 File Offset: 0x00003769
			TaskExecuteResult ITask.CancelStep(LocalizedException exception)
			{
				return TaskExecuteResult.ProcessingComplete;
			}

			// Token: 0x060000BF RID: 191 RVA: 0x0000556C File Offset: 0x0000376C
			void ITask.Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
			{
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x0000556E File Offset: 0x0000376E
			TaskExecuteResult ITask.Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
			{
				if (this.tryExecuteTask(this.task))
				{
					return TaskExecuteResult.ProcessingComplete;
				}
				return TaskExecuteResult.Undefined;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00005586 File Offset: 0x00003786
			IActivityScope ITask.GetActivityScope()
			{
				return ActivityContext.Start(null);
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x0000558E File Offset: 0x0000378E
			void ITask.Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
			{
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00005590 File Offset: 0x00003790
			void ITask.Cancel()
			{
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00005594 File Offset: 0x00003794
			ResourceKey[] ITask.GetResources()
			{
				if (this.input != null && this.input.MessageTarget != null && this.input.MessageTarget.Database != Guid.Empty)
				{
					return new ResourceKey[]
					{
						ProcessorResourceKey.Local,
						new MdbResourceHealthMonitorKey(this.input.MessageTarget.Database),
						new MdbReplicationResourceHealthMonitorKey(this.input.MessageTarget.Database),
						new CiAgeOfLastNotificationResourceKey(this.input.MessageTarget.Database)
					};
				}
				return new ResourceKey[]
				{
					ProcessorResourceKey.Local
				};
			}

			// Token: 0x04000046 RID: 70
			private Task task;

			// Token: 0x04000047 RID: 71
			private Func<Task, bool> tryExecuteTask;

			// Token: 0x04000048 RID: 72
			private ComplianceMessage input;
		}
	}
}
