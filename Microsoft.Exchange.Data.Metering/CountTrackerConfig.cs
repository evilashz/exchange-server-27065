using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x0200000C RID: 12
	internal class CountTrackerConfig : ICountTrackerConfig
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00004B6C File Offset: 0x00002D6C
		public CountTrackerConfig(int maxEntityCount, int maxEntitiesPerGroup, TimeSpan promotionInterval, TimeSpan idleCachedConfigCleanupInterval)
		{
			ArgumentValidator.ThrowIfOutOfRange<int>("maxEntityCount", maxEntityCount, 1, int.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<int>("maxEntitiesPerGroup", maxEntitiesPerGroup, 1, maxEntityCount);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("promotionInterval", promotionInterval, TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("idleCachedConfigCleanupInterval", idleCachedConfigCleanupInterval, TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue);
			this.MaxEntityCount = maxEntityCount;
			this.MaxEntitiesPerGroup = maxEntitiesPerGroup;
			this.PromotionInterval = promotionInterval;
			this.IdleCachedConfigCleanupInterval = idleCachedConfigCleanupInterval;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004BF7 File Offset: 0x00002DF7
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004BFF File Offset: 0x00002DFF
		public int MaxEntityCount { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004C08 File Offset: 0x00002E08
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004C10 File Offset: 0x00002E10
		public int MaxEntitiesPerGroup { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004C19 File Offset: 0x00002E19
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004C21 File Offset: 0x00002E21
		public TimeSpan PromotionInterval { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004C2A File Offset: 0x00002E2A
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004C32 File Offset: 0x00002E32
		public TimeSpan IdleCachedConfigCleanupInterval { get; private set; }
	}
}
