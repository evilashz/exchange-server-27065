using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap
{
	// Token: 0x020000E4 RID: 228
	[Flags]
	internal enum IMAPAggregationFlags
	{
		// Token: 0x040003AD RID: 941
		UseSsl = 1,
		// Token: 0x040003AE RID: 942
		UseTls = 2,
		// Token: 0x040003AF RID: 943
		SecurityMask = 3,
		// Token: 0x040003B0 RID: 944
		ConflictResolutionClientWin = 16,
		// Token: 0x040003B1 RID: 945
		ConflictResolutionServerWin = 32,
		// Token: 0x040003B2 RID: 946
		UseBasicAuth = 0,
		// Token: 0x040003B3 RID: 947
		UseNtlmAuth = 256,
		// Token: 0x040003B4 RID: 948
		AuthenticationMask = 256
	}
}
