using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000238 RID: 568
	[Flags]
	internal enum TransportModerationFlags
	{
		// Token: 0x04000D30 RID: 3376
		None = 0,
		// Token: 0x04000D31 RID: 3377
		BypassNestedModerationEnabled = 1,
		// Token: 0x04000D32 RID: 3378
		SendModerationNotificationsInternally = 2,
		// Token: 0x04000D33 RID: 3379
		SendModerationNotificationsExternally = 4
	}
}
