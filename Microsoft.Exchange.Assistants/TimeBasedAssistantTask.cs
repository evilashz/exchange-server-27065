using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200008B RID: 139
	internal class TimeBasedAssistantTask : SystemTaskBase
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00014EB9 File Offset: 0x000130B9
		internal TimeBasedAssistantTask(SystemWorkloadBase workload, TimeBasedDatabaseDriver driver, ResourceReservation reservation) : base(workload, reservation)
		{
			this.driver = driver;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00014ECA File Offset: 0x000130CA
		internal IEnumerable<ResourceKey> ResourceDependencies
		{
			get
			{
				return this.driver.ResourceDependencies;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00014ED8 File Offset: 0x000130D8
		protected override TaskStepResult Execute()
		{
			DateTime utcNow = DateTime.UtcNow;
			do
			{
				this.context = this.driver.ProcessNextTask(this.context);
			}
			while ((this.context != null || this.driver.HasTask()) && DateTime.UtcNow - utcNow < Configuration.BatchDuration);
			if (this.context == null)
			{
				return TaskStepResult.Complete;
			}
			return TaskStepResult.Yield;
		}

		// Token: 0x04000261 RID: 609
		private TimeBasedDatabaseDriver driver;

		// Token: 0x04000262 RID: 610
		private AssistantTaskContext context;
	}
}
