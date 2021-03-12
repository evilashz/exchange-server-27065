using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000040 RID: 64
	internal enum ExecutionStatus
	{
		// Token: 0x040001B3 RID: 435
		Success,
		// Token: 0x040001B4 RID: 436
		SuccessMailItemDeleted,
		// Token: 0x040001B5 RID: 437
		SuccessMailItemDeferred,
		// Token: 0x040001B6 RID: 438
		TransientError,
		// Token: 0x040001B7 RID: 439
		PermanentError
	}
}
