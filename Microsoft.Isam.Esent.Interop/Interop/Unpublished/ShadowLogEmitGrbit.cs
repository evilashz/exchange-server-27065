using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000074 RID: 116
	[Flags]
	public enum ShadowLogEmitGrbit
	{
		// Token: 0x04000267 RID: 615
		None = 0,
		// Token: 0x04000268 RID: 616
		FirstCall = 1,
		// Token: 0x04000269 RID: 617
		LastCall = 2,
		// Token: 0x0400026A RID: 618
		Cancel = 4,
		// Token: 0x0400026B RID: 619
		DataBuffers = 8,
		// Token: 0x0400026C RID: 620
		LogComplete = 16
	}
}
