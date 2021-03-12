using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002DE RID: 734
	[Flags]
	public enum ObjectInfoFlags
	{
		// Token: 0x04000912 RID: 2322
		None = 0,
		// Token: 0x04000913 RID: 2323
		System = -2147483648,
		// Token: 0x04000914 RID: 2324
		TableFixedDDL = 1073741824,
		// Token: 0x04000915 RID: 2325
		TableTemplate = 536870912,
		// Token: 0x04000916 RID: 2326
		TableDerived = 268435456,
		// Token: 0x04000917 RID: 2327
		TableNoFixedVarColumnsInDerivedTables = 67108864
	}
}
