using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000082 RID: 130
	internal enum ActionId
	{
		// Token: 0x04000357 RID: 855
		None,
		// Token: 0x04000358 RID: 856
		MarkAsRead,
		// Token: 0x04000359 RID: 857
		MarkAsUnRead,
		// Token: 0x0400035A RID: 858
		Move,
		// Token: 0x0400035B RID: 859
		Send,
		// Token: 0x0400035C RID: 860
		Delete,
		// Token: 0x0400035D RID: 861
		Flag,
		// Token: 0x0400035E RID: 862
		FlagClear,
		// Token: 0x0400035F RID: 863
		FlagComplete,
		// Token: 0x04000360 RID: 864
		CreateCalendarEvent,
		// Token: 0x04000361 RID: 865
		UpdateCalendarEvent,
		// Token: 0x04000362 RID: 866
		Max
	}
}
