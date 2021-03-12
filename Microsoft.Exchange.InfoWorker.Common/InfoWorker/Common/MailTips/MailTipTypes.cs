using System;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000125 RID: 293
	[Flags]
	public enum MailTipTypes
	{
		// Token: 0x040004C7 RID: 1223
		None = 0,
		// Token: 0x040004C8 RID: 1224
		AllUseThisForSerializationOnly = 1,
		// Token: 0x040004C9 RID: 1225
		OutOfOfficeMessage = 2,
		// Token: 0x040004CA RID: 1226
		MailboxFullStatus = 4,
		// Token: 0x040004CB RID: 1227
		CustomMailTip = 8,
		// Token: 0x040004CC RID: 1228
		ExternalMemberCount = 16,
		// Token: 0x040004CD RID: 1229
		TotalMemberCount = 32,
		// Token: 0x040004CE RID: 1230
		MaxMessageSize = 64,
		// Token: 0x040004CF RID: 1231
		DeliveryRestriction = 128,
		// Token: 0x040004D0 RID: 1232
		ModerationStatus = 256,
		// Token: 0x040004D1 RID: 1233
		InvalidRecipient = 512,
		// Token: 0x040004D2 RID: 1234
		Scope = 1024,
		// Token: 0x040004D3 RID: 1235
		All = 2046
	}
}
