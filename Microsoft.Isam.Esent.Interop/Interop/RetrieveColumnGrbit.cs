using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000241 RID: 577
	[Flags]
	public enum RetrieveColumnGrbit
	{
		// Token: 0x040003AB RID: 939
		None = 0,
		// Token: 0x040003AC RID: 940
		RetrieveCopy = 1,
		// Token: 0x040003AD RID: 941
		RetrieveFromIndex = 2,
		// Token: 0x040003AE RID: 942
		RetrieveFromPrimaryBookmark = 4,
		// Token: 0x040003AF RID: 943
		RetrieveTag = 8,
		// Token: 0x040003B0 RID: 944
		RetrieveNull = 16,
		// Token: 0x040003B1 RID: 945
		RetrieveIgnoreDefault = 32
	}
}
