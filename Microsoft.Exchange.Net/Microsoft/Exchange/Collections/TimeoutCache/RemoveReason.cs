using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x02000051 RID: 81
	internal enum RemoveReason
	{
		// Token: 0x04000160 RID: 352
		Expired,
		// Token: 0x04000161 RID: 353
		Removed,
		// Token: 0x04000162 RID: 354
		PreemptivelyExpired,
		// Token: 0x04000163 RID: 355
		Cleanup
	}
}
