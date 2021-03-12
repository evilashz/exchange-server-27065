using System;

namespace Microsoft.Exchange.Data.Storage.Approval
{
	// Token: 0x0200008C RID: 140
	[Flags]
	internal enum ApprovalStatus
	{
		// Token: 0x0400029D RID: 669
		Unhandled = 1,
		// Token: 0x0400029E RID: 670
		Cancelled = 2,
		// Token: 0x0400029F RID: 671
		Ndred = 4,
		// Token: 0x040002A0 RID: 672
		Expired = 8,
		// Token: 0x040002A1 RID: 673
		Approved = 16,
		// Token: 0x040002A2 RID: 674
		Rejected = 32,
		// Token: 0x040002A3 RID: 675
		Succeeded = 64,
		// Token: 0x040002A4 RID: 676
		Failed = 128,
		// Token: 0x040002A5 RID: 677
		Oofed = 256,
		// Token: 0x040002A6 RID: 678
		OofOrNdrHandled = 512,
		// Token: 0x040002A7 RID: 679
		DecisionIndepedentFlags = 1048320
	}
}
