using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200023A RID: 570
	[Flags]
	public enum CommitTransactionGrbit
	{
		// Token: 0x04000377 RID: 887
		None = 0,
		// Token: 0x04000378 RID: 888
		LazyFlush = 1,
		// Token: 0x04000379 RID: 889
		WaitLastLevel0Commit = 2
	}
}
