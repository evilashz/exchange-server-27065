using System;

namespace Microsoft.Exchange.Data.Storage.Approval
{
	// Token: 0x0200008D RID: 141
	internal enum DecisionConflict
	{
		// Token: 0x040002A9 RID: 681
		NoConflict,
		// Token: 0x040002AA RID: 682
		DifferentApproverDifferentDecision,
		// Token: 0x040002AB RID: 683
		DifferentApproverSameDecision,
		// Token: 0x040002AC RID: 684
		SameApproverAndDecision,
		// Token: 0x040002AD RID: 685
		SameApproverDifferentDecision,
		// Token: 0x040002AE RID: 686
		HasApproverMissingDecision,
		// Token: 0x040002AF RID: 687
		AlreadyCancelled,
		// Token: 0x040002B0 RID: 688
		AlreadyExpired,
		// Token: 0x040002B1 RID: 689
		Unauthorized,
		// Token: 0x040002B2 RID: 690
		MissingItem,
		// Token: 0x040002B3 RID: 691
		Unknown
	}
}
