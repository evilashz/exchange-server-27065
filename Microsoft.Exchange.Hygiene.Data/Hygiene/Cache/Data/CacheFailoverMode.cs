using System;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000050 RID: 80
	internal enum CacheFailoverMode
	{
		// Token: 0x04000200 RID: 512
		CacheOnly,
		// Token: 0x04000201 RID: 513
		CacheThenDatabase,
		// Token: 0x04000202 RID: 514
		DatabaseOnly,
		// Token: 0x04000203 RID: 515
		Default,
		// Token: 0x04000204 RID: 516
		DefaultThenDatabase,
		// Token: 0x04000205 RID: 517
		BloomFilter
	}
}
