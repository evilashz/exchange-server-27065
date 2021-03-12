using System;

namespace Microsoft.Exchange.Rpc.MailSubmission
{
	// Token: 0x02000291 RID: 657
	internal enum AddResubmitRequestStatus
	{
		// Token: 0x04000D40 RID: 3392
		Success = 1,
		// Token: 0x04000D41 RID: 3393
		Retry,
		// Token: 0x04000D42 RID: 3394
		Error,
		// Token: 0x04000D43 RID: 3395
		AccessError,
		// Token: 0x04000D44 RID: 3396
		InvalidOperation,
		// Token: 0x04000D45 RID: 3397
		Disabled,
		// Token: 0x04000D46 RID: 3398
		MaxRunningRequestsExceeded,
		// Token: 0x04000D47 RID: 3399
		MaxRecentRequestsExceeded,
		// Token: 0x04000D48 RID: 3400
		DuplicateRequest
	}
}
