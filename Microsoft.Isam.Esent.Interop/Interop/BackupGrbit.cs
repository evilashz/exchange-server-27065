using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000236 RID: 566
	[Flags]
	public enum BackupGrbit
	{
		// Token: 0x04000369 RID: 873
		None = 0,
		// Token: 0x0400036A RID: 874
		Incremental = 1,
		// Token: 0x0400036B RID: 875
		Atomic = 4
	}
}
