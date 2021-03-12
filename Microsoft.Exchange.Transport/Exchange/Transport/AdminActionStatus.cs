using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000150 RID: 336
	internal enum AdminActionStatus : byte
	{
		// Token: 0x0400073E RID: 1854
		None,
		// Token: 0x0400073F RID: 1855
		Suspended,
		// Token: 0x04000740 RID: 1856
		PendingDeleteWithNDR,
		// Token: 0x04000741 RID: 1857
		PendingDeleteWithOutNDR,
		// Token: 0x04000742 RID: 1858
		SuspendedInSubmissionQueue
	}
}
