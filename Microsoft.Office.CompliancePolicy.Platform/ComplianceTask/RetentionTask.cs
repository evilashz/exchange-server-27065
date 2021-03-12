using System;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Office.CompliancePolicy.ComplianceTask
{
	// Token: 0x02000064 RID: 100
	public class RetentionTask : PeriodicPolicyTask
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x00009F18 File Offset: 0x00008118
		public RetentionTask()
		{
			this.Category = DarTaskCategory.Low;
			base.TaskRetryTotalCount = 20;
			base.TaskRetryInterval = new TimeSpan(0, 0, 300);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00009F41 File Offset: 0x00008141
		public override string TaskType
		{
			get
			{
				return "Common.Retention";
			}
		}

		// Token: 0x04000150 RID: 336
		private const int DefaultRetryCount = 20;

		// Token: 0x04000151 RID: 337
		private const int DefaultRetryIntervalInSeconds = 300;
	}
}
