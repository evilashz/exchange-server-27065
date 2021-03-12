using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000257 RID: 599
	[Flags]
	public enum TempTableGrbit
	{
		// Token: 0x0400041B RID: 1051
		None = 0,
		// Token: 0x0400041C RID: 1052
		Indexed = 1,
		// Token: 0x0400041D RID: 1053
		Unique = 2,
		// Token: 0x0400041E RID: 1054
		Updatable = 4,
		// Token: 0x0400041F RID: 1055
		Scrollable = 8,
		// Token: 0x04000420 RID: 1056
		SortNullsHigh = 16,
		// Token: 0x04000421 RID: 1057
		ForceMaterialization = 32,
		// Token: 0x04000422 RID: 1058
		ErrorOnDuplicateInsertion = 32
	}
}
