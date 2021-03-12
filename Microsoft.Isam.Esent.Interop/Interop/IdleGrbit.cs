using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200025B RID: 603
	[Flags]
	public enum IdleGrbit
	{
		// Token: 0x0400042B RID: 1067
		None = 0,
		// Token: 0x0400042C RID: 1068
		FlushBuffers = 1,
		// Token: 0x0400042D RID: 1069
		Compact = 2,
		// Token: 0x0400042E RID: 1070
		GetStatus = 4
	}
}
