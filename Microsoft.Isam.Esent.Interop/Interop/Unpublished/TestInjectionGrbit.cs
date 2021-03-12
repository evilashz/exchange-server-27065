using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000076 RID: 118
	[Flags]
	public enum TestInjectionGrbit : uint
	{
		// Token: 0x04000283 RID: 643
		ProbabilityPct = 1U,
		// Token: 0x04000284 RID: 644
		ProbabilityCount = 2U,
		// Token: 0x04000285 RID: 645
		ProbabilityPermanent = 4U,
		// Token: 0x04000286 RID: 646
		ProbabilityFailUntil = 8U,
		// Token: 0x04000287 RID: 647
		ProbabilitySuppress = 1073741824U,
		// Token: 0x04000288 RID: 648
		ProbabilityCleanup = 2147483648U
	}
}
