using System;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000118 RID: 280
	[Flags]
	public enum VirusScanFlags
	{
		// Token: 0x04000586 RID: 1414
		IsOutbound = 1,
		// Token: 0x04000587 RID: 1415
		AllowUserOptOut = 2,
		// Token: 0x04000588 RID: 1416
		SenderNotification = 4,
		// Token: 0x04000589 RID: 1417
		OutboundSenderNotification = 8,
		// Token: 0x0400058A RID: 1418
		RecipientNotification = 16,
		// Token: 0x0400058B RID: 1419
		AdminNotification = 32,
		// Token: 0x0400058C RID: 1420
		OutboundAdminNotification = 64
	}
}
