using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200022F RID: 559
	[Flags]
	public enum AttachDatabaseGrbit
	{
		// Token: 0x04000353 RID: 851
		None = 0,
		// Token: 0x04000354 RID: 852
		ReadOnly = 1,
		// Token: 0x04000355 RID: 853
		DeleteCorruptIndexes = 16
	}
}
