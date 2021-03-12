using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.ComplianceTask;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x0200006B RID: 107
	public abstract class DarTaskFactory
	{
		// Token: 0x060002FB RID: 763
		public abstract DarTaskAggregate CreateTaskAggregate(string taskType);

		// Token: 0x060002FC RID: 764 RVA: 0x0000A708 File Offset: 0x00008908
		public virtual DarTask CreateTask(string taskType)
		{
			if (taskType != null)
			{
				if (taskType == "Common.NoOp")
				{
					return new NoOpTask();
				}
				if (taskType == "Common.Iteration")
				{
					return new DarIterationTask();
				}
				if (taskType == "Common.TaskGenerator")
				{
					return new TaskGenerator();
				}
				if (taskType == "Common.Retention")
				{
					return new RetentionTask();
				}
			}
			throw new InvalidOperationException("Unsupported TaskType");
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A9BC File Offset: 0x00008BBC
		public IEnumerable<string> GetAllTaskTypes()
		{
			yield return "Common.BindingApplication";
			yield return "Common.NoOp";
			yield return "Common.Iteration";
			yield return "Common.Retention";
			yield return "Common.TaskGenerator";
			foreach (string taskType in this.GetWorkloadSpecificTaskTypes())
			{
				yield return taskType;
			}
			yield break;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000AA7C File Offset: 0x00008C7C
		protected virtual IEnumerable<string> GetWorkloadSpecificTaskTypes()
		{
			yield break;
		}
	}
}
