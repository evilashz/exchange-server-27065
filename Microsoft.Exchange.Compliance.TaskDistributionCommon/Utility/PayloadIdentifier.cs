using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Utility
{
	// Token: 0x02000068 RID: 104
	internal struct PayloadIdentifier
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000ED3A File Offset: 0x0000CF3A
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000ED42 File Offset: 0x0000CF42
		public Guid JobRunId { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000ED4B File Offset: 0x0000CF4B
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000ED53 File Offset: 0x0000CF53
		public int TaskId { get; set; }
	}
}
