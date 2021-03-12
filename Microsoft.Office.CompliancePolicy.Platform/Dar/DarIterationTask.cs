using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.ComplianceData;
using Microsoft.Office.CompliancePolicy.ComplianceTask;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x02000078 RID: 120
	public class DarIterationTask : ComplianceTask
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B297 File Offset: 0x00009497
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000B29F File Offset: 0x0000949F
		[SerializableTaskData]
		public int StartPage { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B2A8 File Offset: 0x000094A8
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000B2B0 File Offset: 0x000094B0
		[SerializableTaskData]
		public int ExecutedPage { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B2B9 File Offset: 0x000094B9
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000B2C1 File Offset: 0x000094C1
		[SerializableTaskData]
		public string Scope { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B2CA File Offset: 0x000094CA
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000B2D2 File Offset: 0x000094D2
		[SerializableTaskData]
		public int Count { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000B2DB File Offset: 0x000094DB
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000B2E3 File Offset: 0x000094E3
		[SerializableTaskData]
		public string FailReason { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B2EC File Offset: 0x000094EC
		public override string TaskType
		{
			get
			{
				return "Common.Iteration";
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public override DarTaskExecutionResult Execute(DarTaskManager darTaskManager)
		{
			if (this.Scope == null)
			{
				this.FailReason = "Iteration scope is undefined";
				darTaskManager.ExecutionLog.LogError("DarIterator", null, this.CorrelationId, new InvalidOperationException("Iteration task is scheduled for running, but iterating scope has not been specified"), null, new KeyValuePair<string, object>[0]);
				return DarTaskExecutionResult.Failed;
			}
			DarIterator darIterator = DarIterator.Get(this.Scope, base.TenantId, this.ComplianceServiceProvider);
			return darIterator.RunOnNextPage(this, darTaskManager);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000B35E File Offset: 0x0000955E
		public override void CompleteTask(DarTaskManager darTaskManager)
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B360 File Offset: 0x00009560
		public virtual void ProcessCurrent(ComplianceItem complianceItem)
		{
			this.Count++;
		}
	}
}
