using System;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Office.CompliancePolicy.ComplianceTask
{
	// Token: 0x0200005E RID: 94
	public class DLPTask : PeriodicPolicyTask
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00007E48 File Offset: 0x00006048
		public DLPTask()
		{
			this.Category = DarTaskCategory.Low;
			base.TaskRetryTotalCount = 20;
			base.TaskRetryInterval = new TimeSpan(0, 0, 300);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00007E71 File Offset: 0x00006071
		public override string TaskType
		{
			get
			{
				return "Common.DLPApplication";
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007E78 File Offset: 0x00006078
		public override int MaxLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0400013F RID: 319
		private const int DefaultRetryCount = 20;

		// Token: 0x04000140 RID: 320
		private const int DefaultRetryIntervalInSeconds = 300;
	}
}
