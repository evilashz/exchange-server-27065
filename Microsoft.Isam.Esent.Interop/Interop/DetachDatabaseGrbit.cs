using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200022E RID: 558
	[Flags]
	public enum DetachDatabaseGrbit
	{
		// Token: 0x0400034E RID: 846
		None = 0,
		// Token: 0x0400034F RID: 847
		[Obsolete("ForceDetach is no longer used.")]
		ForceDetach = 1,
		// Token: 0x04000350 RID: 848
		[Obsolete("ForceClose is no longer used.")]
		ForceClose = 2,
		// Token: 0x04000351 RID: 849
		[Obsolete("ForceCloseAndDetach is no longer used.")]
		ForceCloseAndDetach = 3
	}
}
