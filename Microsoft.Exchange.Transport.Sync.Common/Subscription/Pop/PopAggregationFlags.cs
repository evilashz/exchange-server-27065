using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop
{
	// Token: 0x020000EE RID: 238
	[Flags]
	internal enum PopAggregationFlags
	{
		// Token: 0x040003CF RID: 975
		UseSsl = 1,
		// Token: 0x040003D0 RID: 976
		UseTls = 2,
		// Token: 0x040003D1 RID: 977
		SecurityMask = 3,
		// Token: 0x040003D2 RID: 978
		SyncDelete = 16,
		// Token: 0x040003D3 RID: 979
		LeaveOnServer = 64,
		// Token: 0x040003D4 RID: 980
		EnableSendAs = 256,
		// Token: 0x040003D5 RID: 981
		UseBasicAuth = 0,
		// Token: 0x040003D6 RID: 982
		UseSpaAuth = 8192,
		// Token: 0x040003D7 RID: 983
		AuthenticationMask = 28672
	}
}
