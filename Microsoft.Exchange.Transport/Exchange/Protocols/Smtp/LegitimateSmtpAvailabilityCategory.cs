using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D4 RID: 1236
	internal enum LegitimateSmtpAvailabilityCategory
	{
		// Token: 0x04001D02 RID: 7426
		SuccessfulSubmission,
		// Token: 0x04001D03 RID: 7427
		RejectDueToMaxInboundConnectionLimit,
		// Token: 0x04001D04 RID: 7428
		RejectDueToWLIDDown,
		// Token: 0x04001D05 RID: 7429
		RejectDueToADDown,
		// Token: 0x04001D06 RID: 7430
		RejectDueToBackPressure,
		// Token: 0x04001D07 RID: 7431
		RejectDueToIOException,
		// Token: 0x04001D08 RID: 7432
		RejectDueToTLSError,
		// Token: 0x04001D09 RID: 7433
		RejectDueToMaxLocalLoopCount
	}
}
