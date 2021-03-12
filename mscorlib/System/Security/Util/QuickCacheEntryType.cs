using System;

namespace System.Security.Util
{
	// Token: 0x0200034C RID: 844
	[Flags]
	[Serializable]
	internal enum QuickCacheEntryType
	{
		// Token: 0x04001102 RID: 4354
		FullTrustZoneMyComputer = 16777216,
		// Token: 0x04001103 RID: 4355
		FullTrustZoneIntranet = 33554432,
		// Token: 0x04001104 RID: 4356
		FullTrustZoneInternet = 67108864,
		// Token: 0x04001105 RID: 4357
		FullTrustZoneTrusted = 134217728,
		// Token: 0x04001106 RID: 4358
		FullTrustZoneUntrusted = 268435456,
		// Token: 0x04001107 RID: 4359
		FullTrustAll = 536870912
	}
}
