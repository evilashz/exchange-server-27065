using System;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000024 RID: 36
	[Flags]
	internal enum EnablementActions
	{
		// Token: 0x040000AB RID: 171
		NoAction = 0,
		// Token: 0x040000AC RID: 172
		AutoEnabled = 1,
		// Token: 0x040000AD RID: 173
		SentInvitation = 2,
		// Token: 0x040000AE RID: 174
		SentReminder = 4,
		// Token: 0x040000AF RID: 175
		AddedToReadyBreadCrumb = 8,
		// Token: 0x040000B0 RID: 176
		AddedToNotReadyBreadCrumb = 16,
		// Token: 0x040000B1 RID: 177
		ScheduledAutoEnablementNotice = 32,
		// Token: 0x040000B2 RID: 178
		SentAutoEnablementNotice = 64
	}
}
