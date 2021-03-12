using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200025D RID: 605
	[Flags]
	public enum ColInfoGrbit
	{
		// Token: 0x04000434 RID: 1076
		None = 0,
		// Token: 0x04000435 RID: 1077
		NonDerivedColumnsOnly = -2147483648,
		// Token: 0x04000436 RID: 1078
		MinimalInfo = 1073741824,
		// Token: 0x04000437 RID: 1079
		SortByColumnid = 536870912
	}
}
