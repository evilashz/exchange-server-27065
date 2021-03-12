using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000254 RID: 596
	[Flags]
	public enum CreateIndexGrbit
	{
		// Token: 0x04000409 RID: 1033
		None = 0,
		// Token: 0x0400040A RID: 1034
		IndexUnique = 1,
		// Token: 0x0400040B RID: 1035
		IndexPrimary = 2,
		// Token: 0x0400040C RID: 1036
		IndexDisallowNull = 4,
		// Token: 0x0400040D RID: 1037
		IndexIgnoreNull = 8,
		// Token: 0x0400040E RID: 1038
		IndexIgnoreAnyNull = 32,
		// Token: 0x0400040F RID: 1039
		IndexIgnoreFirstNull = 64,
		// Token: 0x04000410 RID: 1040
		IndexLazyFlush = 128,
		// Token: 0x04000411 RID: 1041
		IndexEmpty = 256,
		// Token: 0x04000412 RID: 1042
		IndexUnversioned = 512,
		// Token: 0x04000413 RID: 1043
		IndexSortNullsHigh = 1024
	}
}
