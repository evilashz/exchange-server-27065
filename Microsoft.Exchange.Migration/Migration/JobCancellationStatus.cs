using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000045 RID: 69
	internal enum JobCancellationStatus
	{
		// Token: 0x040000E7 RID: 231
		NotCancelled,
		// Token: 0x040000E8 RID: 232
		CancelledByUserRequest,
		// Token: 0x040000E9 RID: 233
		CancelledDueToHighFailureCount
	}
}
