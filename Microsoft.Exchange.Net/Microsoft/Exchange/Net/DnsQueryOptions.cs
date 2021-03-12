using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BE5 RID: 3045
	[Flags]
	internal enum DnsQueryOptions
	{
		// Token: 0x040038DD RID: 14557
		None = 0,
		// Token: 0x040038DE RID: 14558
		AcceptTruncatedResponse = 1,
		// Token: 0x040038DF RID: 14559
		UseTcpOnly = 2,
		// Token: 0x040038E0 RID: 14560
		NoRecursion = 4,
		// Token: 0x040038E1 RID: 14561
		BypassCache = 8,
		// Token: 0x040038E2 RID: 14562
		FailureTolerant = 16
	}
}
