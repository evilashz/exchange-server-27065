using System;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000126 RID: 294
	[Flags]
	public enum ScopeTypes
	{
		// Token: 0x040004D5 RID: 1237
		None = 0,
		// Token: 0x040004D6 RID: 1238
		Internal = 2,
		// Token: 0x040004D7 RID: 1239
		External = 4,
		// Token: 0x040004D8 RID: 1240
		ExternalPartner = 8,
		// Token: 0x040004D9 RID: 1241
		ExternalNonPartner = 16
	}
}
