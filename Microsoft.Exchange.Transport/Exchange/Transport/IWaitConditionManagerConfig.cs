using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002C6 RID: 710
	internal interface IWaitConditionManagerConfig
	{
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001F11 RID: 7953
		bool TenantThrottlingEnabled { get; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001F12 RID: 7954
		bool SenderThrottlingEnabled { get; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001F13 RID: 7955
		bool QuotaOverrideEnabled { get; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001F14 RID: 7956
		bool TestQuotaOverrideEnabled { get; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001F15 RID: 7957
		bool ProcessingTimeThrottlingEnabled { get; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001F16 RID: 7958
		TimeSpan ThrottlingHistoryInterval { get; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001F17 RID: 7959
		TimeSpan ThrottlingHistoryBucketSize { get; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001F18 RID: 7960
		TimeSpan ThrottlingProcessingMinThreshold { get; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001F19 RID: 7961
		ByteQuantifiedSize ThrottlingMemoryMinThreshold { get; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001F1A RID: 7962
		ByteQuantifiedSize ThrottlingMemoryMaxThreshold { get; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001F1B RID: 7963
		bool AboveThresholdThrottlingBehaviorEnabled { get; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001F1C RID: 7964
		int MaxAllowedCapacityPercentage { get; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001F1D RID: 7965
		TimeSpan EmptyThrottlingCostRemovalInterval { get; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001F1E RID: 7966
		int LockedMessageDehydrationThreshold { get; }
	}
}
