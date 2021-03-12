using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000032 RID: 50
	internal enum AmDbLockReason
	{
		// Token: 0x040000EF RID: 239
		Mount = 1,
		// Token: 0x040000F0 RID: 240
		Dismount,
		// Token: 0x040000F1 RID: 241
		Move,
		// Token: 0x040000F2 RID: 242
		Remount,
		// Token: 0x040000F3 RID: 243
		UpdatePerfCounter
	}
}
