using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000199 RID: 409
	[Flags]
	internal enum SharedTenantConfigurationState : long
	{
		// Token: 0x040009E4 RID: 2532
		UnSupported = 0L,
		// Token: 0x040009E5 RID: 2533
		NotShared = 1L,
		// Token: 0x040009E6 RID: 2534
		Shared = 2L,
		// Token: 0x040009E7 RID: 2535
		Static = 4L,
		// Token: 0x040009E8 RID: 2536
		Dehydrated = 8L
	}
}
