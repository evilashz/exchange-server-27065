using System;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x0200012A RID: 298
	[Flags]
	public enum PerRecipientMailTipsUsage
	{
		// Token: 0x040004E9 RID: 1257
		None = 0,
		// Token: 0x040004EA RID: 1258
		AutoReply = 1,
		// Token: 0x040004EB RID: 1259
		MailboxFull = 2,
		// Token: 0x040004EC RID: 1260
		CustomMailTip = 4,
		// Token: 0x040004ED RID: 1261
		External = 8,
		// Token: 0x040004EE RID: 1262
		Restricted = 16,
		// Token: 0x040004EF RID: 1263
		Moderated = 32,
		// Token: 0x040004F0 RID: 1264
		Invalid = 64,
		// Token: 0x040004F1 RID: 1265
		Scope = 128
	}
}
