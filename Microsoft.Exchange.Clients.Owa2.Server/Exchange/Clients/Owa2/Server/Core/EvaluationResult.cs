using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003CB RID: 971
	public enum EvaluationResult
	{
		// Token: 0x040011A0 RID: 4512
		Success,
		// Token: 0x040011A1 RID: 4513
		ClientErrorInvalidStoreItemId,
		// Token: 0x040011A2 RID: 4514
		ClientErrorNoContent,
		// Token: 0x040011A3 RID: 4515
		TooManyPendingRequests,
		// Token: 0x040011A4 RID: 4516
		PermanentError,
		// Token: 0x040011A5 RID: 4517
		ClientErrorItemAlreadyBeingProcessed,
		// Token: 0x040011A6 RID: 4518
		NullOrganization,
		// Token: 0x040011A7 RID: 4519
		UnexpectedPermanentError,
		// Token: 0x040011A8 RID: 4520
		ClientErrorInvalidClientScanResult,
		// Token: 0x040011A9 RID: 4521
		ClientErrorAccessDeniedStoreItemId
	}
}
