using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200003E RID: 62
	[Flags]
	internal enum ReplicationFlags
	{
		// Token: 0x040003F1 RID: 1009
		None = 0,
		// Token: 0x040003F2 RID: 1010
		NowStatus = 1,
		// Token: 0x040003F3 RID: 1011
		StatusRequest = 2,
		// Token: 0x040003F4 RID: 1012
		ExpressStatusRequest = 4,
		// Token: 0x040003F5 RID: 1013
		NewBackfillTimeout = 8,
		// Token: 0x040003F6 RID: 1014
		OutstandingBackfillTimeout = 16
	}
}
