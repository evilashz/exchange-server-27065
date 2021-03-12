using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002B2 RID: 690
	public enum JET_prep
	{
		// Token: 0x040007DA RID: 2010
		Insert,
		// Token: 0x040007DB RID: 2011
		Replace = 2,
		// Token: 0x040007DC RID: 2012
		Cancel,
		// Token: 0x040007DD RID: 2013
		ReplaceNoLock,
		// Token: 0x040007DE RID: 2014
		InsertCopy,
		// Token: 0x040007DF RID: 2015
		InsertCopyDeleteOriginal = 7
	}
}
