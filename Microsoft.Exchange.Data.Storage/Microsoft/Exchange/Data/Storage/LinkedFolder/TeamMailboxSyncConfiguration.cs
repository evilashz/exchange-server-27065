using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200099F RID: 2463
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TeamMailboxSyncConfiguration : Configuration
	{
		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06005AE4 RID: 23268 RVA: 0x0017CBA7 File Offset: 0x0017ADA7
		// (set) Token: 0x06005AE5 RID: 23269 RVA: 0x0017CBAF File Offset: 0x0017ADAF
		public TimeSpan CacheAbsoluteExpiry { get; private set; }

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x0017CBB8 File Offset: 0x0017ADB8
		// (set) Token: 0x06005AE7 RID: 23271 RVA: 0x0017CBC0 File Offset: 0x0017ADC0
		public TimeSpan CacheSlidingExpiry { get; private set; }

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x0017CBC9 File Offset: 0x0017ADC9
		// (set) Token: 0x06005AE9 RID: 23273 RVA: 0x0017CBD1 File Offset: 0x0017ADD1
		public int CacheBucketCount { get; private set; }

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06005AEA RID: 23274 RVA: 0x0017CBDA File Offset: 0x0017ADDA
		// (set) Token: 0x06005AEB RID: 23275 RVA: 0x0017CBE2 File Offset: 0x0017ADE2
		public int CacheBucketSize { get; private set; }

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06005AEC RID: 23276 RVA: 0x0017CBEB File Offset: 0x0017ADEB
		// (set) Token: 0x06005AED RID: 23277 RVA: 0x0017CBF3 File Offset: 0x0017ADF3
		public int SharePointQueryPageSize { get; private set; }

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0017CBFC File Offset: 0x0017ADFC
		// (set) Token: 0x06005AEF RID: 23279 RVA: 0x0017CC04 File Offset: 0x0017AE04
		public TimeSpan MinSyncInterval { get; private set; }

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06005AF0 RID: 23280 RVA: 0x0017CC0D File Offset: 0x0017AE0D
		// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x0017CC15 File Offset: 0x0017AE15
		public bool UseOAuth { get; private set; }

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x0017CC1E File Offset: 0x0017AE1E
		// (set) Token: 0x06005AF3 RID: 23283 RVA: 0x0017CC26 File Offset: 0x0017AE26
		public bool HttpDebugEnabled { get; private set; }

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0017CC30 File Offset: 0x0017AE30
		public TeamMailboxSyncConfiguration(TimeSpan cacheAbsoluteExpiry, TimeSpan cacheSlidingExpiry, int cacheBucketCount, int cacheBucketSize, int maxAllowedQueueLength, int maxAllowedPendingJobCount, TimeSpan dispatcherWakeUpInterval, TimeSpan minSyncInterval, int sharePointQueryPageSize, bool useOAuth, bool httpDebugEnabled) : base(maxAllowedQueueLength, maxAllowedPendingJobCount, dispatcherWakeUpInterval)
		{
			this.CacheAbsoluteExpiry = cacheAbsoluteExpiry;
			this.CacheSlidingExpiry = cacheSlidingExpiry;
			this.CacheBucketSize = cacheBucketSize;
			this.CacheBucketCount = cacheBucketCount;
			this.MinSyncInterval = minSyncInterval;
			this.SharePointQueryPageSize = sharePointQueryPageSize;
			this.UseOAuth = useOAuth;
			this.HttpDebugEnabled = httpDebugEnabled;
		}
	}
}
