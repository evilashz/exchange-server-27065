using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200028E RID: 654
	public enum EventType
	{
		// Token: 0x04000C22 RID: 3106
		SmtpReceive,
		// Token: 0x04000C23 RID: 3107
		SmtpSend,
		// Token: 0x04000C24 RID: 3108
		Fail,
		// Token: 0x04000C25 RID: 3109
		Deliver,
		// Token: 0x04000C26 RID: 3110
		Resolve,
		// Token: 0x04000C27 RID: 3111
		Expand,
		// Token: 0x04000C28 RID: 3112
		Redirect,
		// Token: 0x04000C29 RID: 3113
		Submit,
		// Token: 0x04000C2A RID: 3114
		Defer,
		// Token: 0x04000C2B RID: 3115
		InitMessageCreated,
		// Token: 0x04000C2C RID: 3116
		ModeratorRejected,
		// Token: 0x04000C2D RID: 3117
		ModeratorApprove,
		// Token: 0x04000C2E RID: 3118
		Pending,
		// Token: 0x04000C2F RID: 3119
		Transferred
	}
}
