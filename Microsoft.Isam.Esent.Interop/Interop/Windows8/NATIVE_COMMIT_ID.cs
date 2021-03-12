using System;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x02000277 RID: 631
	[Serializable]
	internal struct NATIVE_COMMIT_ID
	{
		// Token: 0x040004DD RID: 1245
		public NATIVE_SIGNATURE signLog;

		// Token: 0x040004DE RID: 1246
		public int reserved;

		// Token: 0x040004DF RID: 1247
		public long commitId;
	}
}
