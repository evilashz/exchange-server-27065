using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000231 RID: 561
	[Flags]
	internal enum ELCFolderFlags
	{
		// Token: 0x040010B7 RID: 4279
		None = 0,
		// Token: 0x040010B8 RID: 4280
		Provisioned = 1,
		// Token: 0x040010B9 RID: 4281
		Protected = 2,
		// Token: 0x040010BA RID: 4282
		MustDisplayComment = 4,
		// Token: 0x040010BB RID: 4283
		Quota = 8,
		// Token: 0x040010BC RID: 4284
		ELCRoot = 16,
		// Token: 0x040010BD RID: 4285
		TrackFolderSize = 32,
		// Token: 0x040010BE RID: 4286
		DumpsterFolder = 64
	}
}
