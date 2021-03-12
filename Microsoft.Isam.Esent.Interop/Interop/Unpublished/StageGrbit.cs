using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200007B RID: 123
	[Flags]
	public enum StageGrbit
	{
		// Token: 0x0400029B RID: 667
		None = 0,
		// Token: 0x0400029C RID: 668
		TestEnvLocalMode = 4,
		// Token: 0x0400029D RID: 669
		TestEnvAlphaMode = 16,
		// Token: 0x0400029E RID: 670
		TestEnvBetaMode = 64,
		// Token: 0x0400029F RID: 671
		SelfhostLocalMode = 1024,
		// Token: 0x040002A0 RID: 672
		SelfhostAlphaMode = 4096,
		// Token: 0x040002A1 RID: 673
		SelfhostBetaMode = 16384,
		// Token: 0x040002A2 RID: 674
		ProdLocalMode = 262144,
		// Token: 0x040002A3 RID: 675
		ProdAlphaMode = 1048576,
		// Token: 0x040002A4 RID: 676
		ProdBetaMode = 4194304
	}
}
