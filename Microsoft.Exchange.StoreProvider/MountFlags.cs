using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000038 RID: 56
	[Flags]
	internal enum MountFlags
	{
		// Token: 0x040003BF RID: 959
		None = 0,
		// Token: 0x040003C0 RID: 960
		ForceDatabaseCreation = 1,
		// Token: 0x040003C1 RID: 961
		AllowDatabasePatch = 2,
		// Token: 0x040003C2 RID: 962
		AcceptDataLoss = 4,
		// Token: 0x040003C3 RID: 963
		LogReplay = 8
	}
}
