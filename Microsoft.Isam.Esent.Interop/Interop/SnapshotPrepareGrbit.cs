using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000234 RID: 564
	[Flags]
	public enum SnapshotPrepareGrbit
	{
		// Token: 0x04000363 RID: 867
		None = 0,
		// Token: 0x04000364 RID: 868
		IncrementalSnapshot = 1,
		// Token: 0x04000365 RID: 869
		CopySnapshot = 2
	}
}
