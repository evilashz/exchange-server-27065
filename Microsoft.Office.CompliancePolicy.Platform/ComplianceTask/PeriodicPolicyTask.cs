using System;
using Microsoft.Office.CompliancePolicy.ComplianceData;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Office.CompliancePolicy.ComplianceTask
{
	// Token: 0x0200005D RID: 93
	public abstract class PeriodicPolicyTask : ComplianceTask
	{
		// Token: 0x06000286 RID: 646 RVA: 0x00007DB9 File Offset: 0x00005FB9
		public PeriodicPolicyTask()
		{
			this.Category = DarTaskCategory.Low;
			base.TaskRetryTotalCount = 20;
			base.TaskRetryInterval = new TimeSpan(0, 0, 300);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00007DED File Offset: 0x00005FED
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00007DF5 File Offset: 0x00005FF5
		public virtual int MaxLevel
		{
			get
			{
				return this.maxLevel;
			}
			set
			{
				this.maxLevel = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00007DFE File Offset: 0x00005FFE
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00007E06 File Offset: 0x00006006
		[SerializableTaskData]
		public string Scope { get; set; }

		// Token: 0x0600028B RID: 651 RVA: 0x00007E0F File Offset: 0x0000600F
		public override void CompleteTask(DarTaskManager darTaskManager)
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007E14 File Offset: 0x00006014
		public override DarTaskExecutionResult Execute(DarTaskManager darTaskManager)
		{
			ComplianceItemContainer complianceItemContainer = this.ComplianceServiceProvider.GetComplianceItemContainer(base.TenantId, this.Scope);
			ComplianceService complianceService = new ComplianceService(this, darTaskManager);
			return complianceService.ApplyPeriodicPolicies(complianceItemContainer);
		}

		// Token: 0x0400013B RID: 315
		private const int DefaultRetryCount = 20;

		// Token: 0x0400013C RID: 316
		private const int DefaultRetryIntervalInSeconds = 300;

		// Token: 0x0400013D RID: 317
		private int maxLevel = int.MaxValue;
	}
}
