using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000009 RID: 9
	internal enum DropCallReason
	{
		// Token: 0x0400001C RID: 28
		None,
		// Token: 0x0400001D RID: 29
		UserError,
		// Token: 0x0400001E RID: 30
		SystemError,
		// Token: 0x0400001F RID: 31
		GracefulHangup,
		// Token: 0x04000020 RID: 32
		OutboundFailedCall
	}
}
