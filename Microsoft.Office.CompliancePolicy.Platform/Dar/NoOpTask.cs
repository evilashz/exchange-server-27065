using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x02000076 RID: 118
	public class NoOpTask : DarTask
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000B0C0 File Offset: 0x000092C0
		public NoOpTask()
		{
			this.TaskData = new NoOpTaskData
			{
				States = new List<DarTaskExecutionResult>
				{
					DarTaskExecutionResult.Completed
				},
				StateHistory = new List<DarTaskState>()
			};
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000B0FF File Offset: 0x000092FF
		public override string TaskType
		{
			get
			{
				return "Common.NoOp";
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B106 File Offset: 0x00009306
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000B10E File Offset: 0x0000930E
		[SerializableTaskData]
		public NoOpTaskData TaskData { get; set; }

		// Token: 0x0600031E RID: 798 RVA: 0x0000B118 File Offset: 0x00009318
		public override DarTaskExecutionResult Execute(DarTaskManager darTaskManager)
		{
			darTaskManager.ExecutionLog.LogInformation("NoOpTask", null, this.CorrelationId, string.Format("NoOp Task Executed Id:{0}, Category:{1}, Priority:{2}, Time:{3}", new object[]
			{
				base.Id,
				this.Category,
				base.Priority,
				DateTime.UtcNow
			}), new KeyValuePair<string, object>[0]);
			DarTaskExecutionResult darTaskExecutionResult = this.TaskData.States.First<DarTaskExecutionResult>();
			this.TaskData.States = this.TaskData.States.Skip(1).ToList<DarTaskExecutionResult>();
			darTaskManager.ExecutionLog.LogInformation("NoOpTask", null, this.CorrelationId, string.Format("NoOp Task Completed Id:{0}, Category:{1}, Priority:{2}, Time:{3}, Result:{4}", new object[]
			{
				base.Id,
				this.Category,
				base.Priority,
				DateTime.UtcNow,
				darTaskExecutionResult
			}), new KeyValuePair<string, object>[0]);
			return darTaskExecutionResult;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B223 File Offset: 0x00009423
		public override void CompleteTask(DarTaskManager darTaskManager)
		{
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B225 File Offset: 0x00009425
		protected override void OnExecuted(DarTaskState executionState)
		{
			this.TaskData.StateHistory.Add(executionState);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B238 File Offset: 0x00009438
		protected override IEnumerable<Type> GetKnownTypes()
		{
			return base.GetKnownTypes().Concat(new Type[]
			{
				typeof(NoOpTaskData)
			});
		}
	}
}
