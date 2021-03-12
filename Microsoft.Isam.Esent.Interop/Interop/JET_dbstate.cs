using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000280 RID: 640
	public enum JET_dbstate
	{
		// Token: 0x04000521 RID: 1313
		JustCreated = 1,
		// Token: 0x04000522 RID: 1314
		DirtyShutdown,
		// Token: 0x04000523 RID: 1315
		CleanShutdown,
		// Token: 0x04000524 RID: 1316
		BeingConverted,
		// Token: 0x04000525 RID: 1317
		ForceDetach
	}
}
