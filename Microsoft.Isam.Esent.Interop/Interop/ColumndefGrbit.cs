using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000252 RID: 594
	[Flags]
	public enum ColumndefGrbit
	{
		// Token: 0x040003F4 RID: 1012
		None = 0,
		// Token: 0x040003F5 RID: 1013
		ColumnFixed = 1,
		// Token: 0x040003F6 RID: 1014
		ColumnTagged = 2,
		// Token: 0x040003F7 RID: 1015
		ColumnNotNULL = 4,
		// Token: 0x040003F8 RID: 1016
		ColumnVersion = 8,
		// Token: 0x040003F9 RID: 1017
		ColumnAutoincrement = 16,
		// Token: 0x040003FA RID: 1018
		ColumnUpdatable = 32,
		// Token: 0x040003FB RID: 1019
		ColumnMultiValued = 1024,
		// Token: 0x040003FC RID: 1020
		ColumnEscrowUpdate = 2048,
		// Token: 0x040003FD RID: 1021
		ColumnUnversioned = 4096,
		// Token: 0x040003FE RID: 1022
		ColumnMaybeNull = 8192,
		// Token: 0x040003FF RID: 1023
		ColumnFinalize = 16384,
		// Token: 0x04000400 RID: 1024
		ColumnUserDefinedDefault = 32768,
		// Token: 0x04000401 RID: 1025
		TTKey = 64,
		// Token: 0x04000402 RID: 1026
		TTDescending = 128
	}
}
