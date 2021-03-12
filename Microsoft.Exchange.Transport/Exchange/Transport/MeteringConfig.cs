using System;
using Microsoft.Exchange.Data.Metering;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002DA RID: 730
	internal class MeteringConfig : ICountTrackerConfig
	{
		// Token: 0x06002052 RID: 8274 RVA: 0x0007BCA0 File Offset: 0x00079EA0
		public MeteringConfig()
		{
			this.Enabled = TransportAppConfig.GetConfigBool("MeteringEnabled", true);
			this.MaxEntityCount = TransportAppConfig.GetConfigInt("MeteringMaxEntityCount", 1, int.MaxValue, 10000);
			this.MaxEntitiesPerGroup = TransportAppConfig.GetConfigInt("MeteringMaxEntitiesPerGroup", 1, int.MaxValue, 500);
			this.PromotionInterval = TransportAppConfig.GetConfigTimeSpan("MeteringPromotionInterval", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromSeconds(5.0));
			this.IdleCachedConfigCleanupInterval = TransportAppConfig.GetConfigTimeSpan("MeteringIdleCachedConfigCleanupInterval", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromMinutes(1.0));
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x0007BD4A File Offset: 0x00079F4A
		// (set) Token: 0x06002054 RID: 8276 RVA: 0x0007BD52 File Offset: 0x00079F52
		public bool Enabled { get; private set; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0007BD5B File Offset: 0x00079F5B
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x0007BD63 File Offset: 0x00079F63
		public int MaxEntityCount { get; private set; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0007BD6C File Offset: 0x00079F6C
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x0007BD74 File Offset: 0x00079F74
		public int MaxEntitiesPerGroup { get; private set; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0007BD7D File Offset: 0x00079F7D
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x0007BD85 File Offset: 0x00079F85
		public TimeSpan PromotionInterval { get; private set; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x0007BD8E File Offset: 0x00079F8E
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x0007BD96 File Offset: 0x00079F96
		public TimeSpan IdleCachedConfigCleanupInterval { get; private set; }
	}
}
