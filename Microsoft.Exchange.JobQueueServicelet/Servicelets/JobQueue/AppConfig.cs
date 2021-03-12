using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Servicelets.JobQueue
{
	// Token: 0x02000002 RID: 2
	internal sealed class AppConfig
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AppConfig()
		{
			this.TMSyncCacheSlidingExpiry = AppConfigLoader.GetConfigTimeSpanValue("TMSyncCacheSlidingExpiry", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(15.0));
			this.TMSyncCacheAbsoluteExpiry = AppConfigLoader.GetConfigTimeSpanValue("TMSyncCacheAbsoluteExpiry", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromHours(4.0));
			this.TMSyncCacheBucketCount = AppConfigLoader.GetConfigIntValue("TMSyncCacheBucketCount", 1, int.MaxValue, 10);
			this.TMSyncCacheBucketSize = AppConfigLoader.GetConfigIntValue("TMSyncCacheBucketSize", 1, int.MaxValue, 100);
			this.TMSyncMaxJobQueueLength = AppConfigLoader.GetConfigIntValue("TMSyncMaxJobQueueLength", 1, int.MaxValue, 100);
			this.TMSyncMaxPendingJobs = AppConfigLoader.GetConfigIntValue("TMSyncMaxPendingJobs", 1, int.MaxValue, 5);
			this.TMSyncSharePointQueryPageSize = AppConfigLoader.GetConfigIntValue("TMSyncSharePointQueryPageSize", 1, int.MaxValue, 100);
			this.TMSyncDispatcherWakeupInterval = AppConfigLoader.GetConfigTimeSpanValue("TMSyncDispatcherWakeupInterval", TimeSpan.FromMilliseconds(100.0), TimeSpan.MaxValue, TimeSpan.FromSeconds(5.0));
			this.TMSyncMinSyncInterval = AppConfigLoader.GetConfigTimeSpanValue("TMSyncMinSyncInterval", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromSeconds(30.0));
			this.TMSyncUseOAuth = AppConfigLoader.GetConfigBoolValue("TMSyncUseOAuth", true);
			this.TMSyncHttpDebugEnabled = AppConfigLoader.GetConfigBoolValue("TMSyncHttpDebugEnabled", false);
			this.EnqueueRequestTimeout = AppConfigLoader.GetConfigTimeSpanValue("EnqueueRequestTimeout", TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue, TimeSpan.FromSeconds(300.0));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002268 File Offset: 0x00000468
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002270 File Offset: 0x00000470
		public TimeSpan TMSyncCacheAbsoluteExpiry { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002279 File Offset: 0x00000479
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002281 File Offset: 0x00000481
		public TimeSpan TMSyncCacheSlidingExpiry { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000228A File Offset: 0x0000048A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002292 File Offset: 0x00000492
		public int TMSyncCacheBucketCount { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000229B File Offset: 0x0000049B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000022A3 File Offset: 0x000004A3
		public int TMSyncCacheBucketSize { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022AC File Offset: 0x000004AC
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000022B4 File Offset: 0x000004B4
		public int TMSyncMaxJobQueueLength { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022BD File Offset: 0x000004BD
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000022C5 File Offset: 0x000004C5
		public int TMSyncMaxPendingJobs { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022CE File Offset: 0x000004CE
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000022D6 File Offset: 0x000004D6
		public TimeSpan TMSyncDispatcherWakeupInterval { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022DF File Offset: 0x000004DF
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022E7 File Offset: 0x000004E7
		public TimeSpan TMSyncMinSyncInterval { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022F0 File Offset: 0x000004F0
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022F8 File Offset: 0x000004F8
		public int TMSyncSharePointQueryPageSize { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002301 File Offset: 0x00000501
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002309 File Offset: 0x00000509
		public bool TMSyncUseOAuth { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002312 File Offset: 0x00000512
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000231A File Offset: 0x0000051A
		public bool TMSyncHttpDebugEnabled { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002323 File Offset: 0x00000523
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000232B File Offset: 0x0000052B
		public TimeSpan EnqueueRequestTimeout { get; private set; }
	}
}
