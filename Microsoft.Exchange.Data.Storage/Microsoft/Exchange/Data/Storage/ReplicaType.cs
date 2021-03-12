using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000235 RID: 565
	[Flags]
	internal enum ReplicaType
	{
		// Token: 0x040010D6 RID: 4310
		LegacyReplicas = 1,
		// Token: 0x040010D7 RID: 4311
		Exchange2007OrLaterReplicas = 2,
		// Token: 0x040010D8 RID: 4312
		CurrentServerVersionReplicas = 4,
		// Token: 0x040010D9 RID: 4313
		AllReplicas = 3
	}
}
