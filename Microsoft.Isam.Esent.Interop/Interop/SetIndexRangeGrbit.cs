using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200024A RID: 586
	[Flags]
	public enum SetIndexRangeGrbit
	{
		// Token: 0x040003DC RID: 988
		None = 0,
		// Token: 0x040003DD RID: 989
		RangeInclusive = 1,
		// Token: 0x040003DE RID: 990
		RangeUpperLimit = 2,
		// Token: 0x040003DF RID: 991
		RangeInstantDuration = 4,
		// Token: 0x040003E0 RID: 992
		RangeRemove = 8
	}
}
