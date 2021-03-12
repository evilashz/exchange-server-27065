using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000266 RID: 614
	[Flags]
	internal enum TransportSettingFlag
	{
		// Token: 0x04000E3A RID: 3642
		None = 0,
		// Token: 0x04000E3B RID: 3643
		MessageTrackingReadStatusDisabled = 4,
		// Token: 0x04000E3C RID: 3644
		InternalOnly = 8,
		// Token: 0x04000E3D RID: 3645
		OpenDomainRoutingDisabled = 16,
		// Token: 0x04000E3E RID: 3646
		QueryBaseDNRestrictionEnabled = 32,
		// Token: 0x04000E3F RID: 3647
		AllowArchiveAddressSync = 64,
		// Token: 0x04000E40 RID: 3648
		MessageCopyForSentAsEnabled = 128,
		// Token: 0x04000E41 RID: 3649
		MessageCopyForSendOnBehalfEnabled = 256,
		// Token: 0x04000E42 RID: 3650
		All = 504
	}
}
