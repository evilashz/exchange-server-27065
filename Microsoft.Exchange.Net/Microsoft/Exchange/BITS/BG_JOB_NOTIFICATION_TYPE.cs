using System;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000660 RID: 1632
	[Flags]
	internal enum BG_JOB_NOTIFICATION_TYPE : uint
	{
		// Token: 0x04001DC3 RID: 7619
		BG_NOTIFY_JOB_TRANSFERRED = 1U,
		// Token: 0x04001DC4 RID: 7620
		BG_NOTIFY_JOB_ERROR = 2U,
		// Token: 0x04001DC5 RID: 7621
		BG_NOTIFY_DISABLE = 4U,
		// Token: 0x04001DC6 RID: 7622
		BG_NOTIFY_JOB_MODIFICATION = 8U
	}
}
