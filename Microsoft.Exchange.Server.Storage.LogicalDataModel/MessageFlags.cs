using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200002A RID: 42
	[Flags]
	public enum MessageFlags
	{
		// Token: 0x0400029F RID: 671
		None = 0,
		// Token: 0x040002A0 RID: 672
		Read = 1,
		// Token: 0x040002A1 RID: 673
		Unmodified = 2,
		// Token: 0x040002A2 RID: 674
		Submit = 4,
		// Token: 0x040002A3 RID: 675
		Unsent = 8,
		// Token: 0x040002A4 RID: 676
		HasAttachment = 16,
		// Token: 0x040002A5 RID: 677
		FromMe = 32,
		// Token: 0x040002A6 RID: 678
		Associated = 64,
		// Token: 0x040002A7 RID: 679
		Resend = 128,
		// Token: 0x040002A8 RID: 680
		ReadNotificationPending = 256,
		// Token: 0x040002A9 RID: 681
		NonReadNotificationPending = 512,
		// Token: 0x040002AA RID: 682
		EverRead = 1024,
		// Token: 0x040002AB RID: 683
		Irm = 2048,
		// Token: 0x040002AC RID: 684
		OriginX400 = 4096,
		// Token: 0x040002AD RID: 685
		OriginInternet = 8192,
		// Token: 0x040002AE RID: 686
		OriginMiscExternal = 32768,
		// Token: 0x040002AF RID: 687
		NeedSpecialRecipientProcessing = 131072
	}
}
