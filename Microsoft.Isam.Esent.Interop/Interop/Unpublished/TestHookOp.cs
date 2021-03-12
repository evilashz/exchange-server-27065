using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200006E RID: 110
	public enum TestHookOp
	{
		// Token: 0x04000253 RID: 595
		TestInjection = 1,
		// Token: 0x04000254 RID: 596
		SetNegativeTesting = 5,
		// Token: 0x04000255 RID: 597
		ResetNegativeTesting,
		// Token: 0x04000256 RID: 598
		GetBfLowMemoryCallback = 11,
		// Token: 0x04000257 RID: 599
		TraceTestMarker,
		// Token: 0x04000258 RID: 600
		EvictCache = 18,
		// Token: 0x04000259 RID: 601
		Corrupt,
		// Token: 0x0400025A RID: 602
		EnableAutoInc,
		// Token: 0x0400025B RID: 603
		SetErrorTrap,
		// Token: 0x0400025C RID: 604
		GetTablePgnoFDP
	}
}
