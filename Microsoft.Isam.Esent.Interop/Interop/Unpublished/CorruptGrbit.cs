using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000077 RID: 119
	[Flags]
	public enum CorruptGrbit : uint
	{
		// Token: 0x0400028A RID: 650
		CorruptDatabaseFile = 2147483648U,
		// Token: 0x0400028B RID: 651
		CorruptDatabasePageImage = 1073741824U,
		// Token: 0x0400028C RID: 652
		CorruptPageChksumRand = 1U,
		// Token: 0x0400028D RID: 653
		CorruptPageChksumSafe = 2U,
		// Token: 0x0400028E RID: 654
		CorruptPageSingleFld = 4U,
		// Token: 0x0400028F RID: 655
		CorruptPageRemoveNode = 8U,
		// Token: 0x04000290 RID: 656
		CorruptPageDbtimeDelta = 16U
	}
}
