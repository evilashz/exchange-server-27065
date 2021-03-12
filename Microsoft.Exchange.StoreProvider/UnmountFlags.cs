using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000039 RID: 57
	[Flags]
	internal enum UnmountFlags
	{
		// Token: 0x040003C5 RID: 965
		None = 0,
		// Token: 0x040003C6 RID: 966
		ForceDatabaseDeletion = 8,
		// Token: 0x040003C7 RID: 967
		SkipCacheFlush = 16
	}
}
