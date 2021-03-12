using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000037 RID: 55
	public enum SubmissionResponsibility : long
	{
		// Token: 0x04000314 RID: 788
		None,
		// Token: 0x04000315 RID: 789
		SpoolerDone,
		// Token: 0x04000316 RID: 790
		MdbDone = 16L,
		// Token: 0x04000317 RID: 791
		PreProcessingDone = 256L,
		// Token: 0x04000318 RID: 792
		LocalDeliveryDone = 4096L
	}
}
