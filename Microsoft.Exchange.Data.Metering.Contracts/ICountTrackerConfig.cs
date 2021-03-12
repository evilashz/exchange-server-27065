using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000009 RID: 9
	internal interface ICountTrackerConfig
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000034 RID: 52
		int MaxEntityCount { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53
		int MaxEntitiesPerGroup { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000036 RID: 54
		TimeSpan PromotionInterval { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000037 RID: 55
		TimeSpan IdleCachedConfigCleanupInterval { get; }
	}
}
