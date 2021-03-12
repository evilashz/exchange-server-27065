using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000137 RID: 311
	internal enum GlsCacheServiceMode
	{
		// Token: 0x040006BC RID: 1724
		CacheDisabled,
		// Token: 0x040006BD RID: 1725
		CacheAsExceptionFallback,
		// Token: 0x040006BE RID: 1726
		CacheAsNotFoundFallback,
		// Token: 0x040006BF RID: 1727
		LiveServiceAsExceptionFallback,
		// Token: 0x040006C0 RID: 1728
		LiveServiceAsNotFoundCallback,
		// Token: 0x040006C1 RID: 1729
		CacheOnly
	}
}
