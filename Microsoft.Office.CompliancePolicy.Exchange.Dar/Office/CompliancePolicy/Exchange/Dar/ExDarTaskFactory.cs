using System;
using System.Linq;
using Microsoft.Exchange.Data.ApplicationLogic.Compliance;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.LocStrings;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar
{
	// Token: 0x02000004 RID: 4
	internal class ExDarTaskFactory : DarTaskFactory
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002235 File Offset: 0x00000435
		public ExDarTaskFactory(DarServiceProvider provider)
		{
			this.provider = provider;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002244 File Offset: 0x00000444
		public override DarTask CreateTask(string taskType)
		{
			if (taskType != null)
			{
				if (taskType == "Common.BindingApplication")
				{
					return new ExBindingTask
					{
						ComplianceProviderExecutionLog = this.provider.ExecutionLog
					};
				}
				if (taskType == "Common.Iteration")
				{
					return new DarIterationTask
					{
						ComplianceServiceProvider = new ExComplianceServiceProvider()
					};
				}
			}
			return base.CreateTask(taskType);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022A8 File Offset: 0x000004A8
		public override DarTaskAggregate CreateTaskAggregate(string taskType)
		{
			if (base.GetAllTaskTypes().Contains(taskType))
			{
				return new DarTaskAggregate
				{
					TaskType = taskType
				};
			}
			throw new ApplicationException(Strings.TaskTypeUnknown);
		}

		// Token: 0x04000004 RID: 4
		private DarServiceProvider provider;
	}
}
