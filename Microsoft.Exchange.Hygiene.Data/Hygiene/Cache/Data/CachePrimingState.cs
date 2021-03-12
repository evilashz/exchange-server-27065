using System;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000059 RID: 89
	internal enum CachePrimingState
	{
		// Token: 0x04000222 RID: 546
		Unavailable,
		// Token: 0x04000223 RID: 547
		Priming,
		// Token: 0x04000224 RID: 548
		Stale,
		// Token: 0x04000225 RID: 549
		Unhealthy,
		// Token: 0x04000226 RID: 550
		Healthy,
		// Token: 0x04000227 RID: 551
		Unknown,
		// Token: 0x04000228 RID: 552
		PrimingWithFile
	}
}
