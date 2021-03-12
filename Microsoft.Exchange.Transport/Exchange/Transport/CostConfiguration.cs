using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200032C RID: 812
	internal class CostConfiguration
	{
		// Token: 0x06002341 RID: 9025 RVA: 0x00086295 File Offset: 0x00084495
		public CostConfiguration(IWaitConditionManagerConfig config, IProcessingQuotaComponent quotaOverride, Func<DateTime> timeProvider)
		{
			this.config = config;
			this.quotaOverride = quotaOverride;
			this.timeProvider = timeProvider;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000862B2 File Offset: 0x000844B2
		public CostConfiguration(IWaitConditionManagerConfig config, bool reversedCollection, int maxThreads, long processingCapacity, IProcessingQuotaComponent quotaOverride, Func<DateTime> timeProvider) : this(config, quotaOverride, timeProvider)
		{
			this.reversedCollection = reversedCollection;
			this.maxThreads = maxThreads;
			this.processingCapacity = processingCapacity;
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000862D5 File Offset: 0x000844D5
		public IWaitConditionManagerConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x000862DD File Offset: 0x000844DD
		public bool ProcessingHistoryEnabled
		{
			get
			{
				return this.config.ProcessingTimeThrottlingEnabled;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000862EC File Offset: 0x000844EC
		public bool MemoryCollectionEnabled
		{
			get
			{
				return this.config.ThrottlingMemoryMaxThreshold.ToBytes() > 0UL;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x00086310 File Offset: 0x00084510
		public TimeSpan HistoryInterval
		{
			get
			{
				return this.config.ThrottlingHistoryInterval;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x0008631D File Offset: 0x0008451D
		public TimeSpan BucketSize
		{
			get
			{
				return this.config.ThrottlingHistoryBucketSize;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x0008632A File Offset: 0x0008452A
		public TimeSpan MinInterestingProcessingInterval
		{
			get
			{
				return this.config.ThrottlingProcessingMinThreshold;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x00086337 File Offset: 0x00084537
		public ByteQuantifiedSize MinInterestingMemorySize
		{
			get
			{
				return this.config.ThrottlingMemoryMinThreshold;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x00086344 File Offset: 0x00084544
		public ByteQuantifiedSize MemoryThreshold
		{
			get
			{
				return this.config.ThrottlingMemoryMaxThreshold;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x00086351 File Offset: 0x00084551
		public IProcessingQuotaComponent QuotaOverride
		{
			get
			{
				return this.quotaOverride;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00086359 File Offset: 0x00084559
		public Func<DateTime> TimeProvider
		{
			get
			{
				return this.timeProvider;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x00086361 File Offset: 0x00084561
		public bool ReversedCost
		{
			get
			{
				return this.reversedCollection;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x00086369 File Offset: 0x00084569
		public int MaxThreads
		{
			get
			{
				return this.maxThreads;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x00086371 File Offset: 0x00084571
		public long ProcessingCapacity
		{
			get
			{
				return this.processingCapacity;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x00086379 File Offset: 0x00084579
		public bool AboveThresholdBehaviorEnabled
		{
			get
			{
				return this.config.AboveThresholdThrottlingBehaviorEnabled;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x00086386 File Offset: 0x00084586
		public int MaxAllowedCapacity
		{
			get
			{
				return this.config.MaxAllowedCapacityPercentage;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x00086393 File Offset: 0x00084593
		public TimeSpan EmptyCostRemovalInterval
		{
			get
			{
				return this.config.EmptyThrottlingCostRemovalInterval;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000863A0 File Offset: 0x000845A0
		public bool OverrideEnabled
		{
			get
			{
				return this.config.QuotaOverrideEnabled;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x000863AD File Offset: 0x000845AD
		public bool TestOverrideEnabled
		{
			get
			{
				return this.config.TestQuotaOverrideEnabled;
			}
		}

		// Token: 0x04001252 RID: 4690
		private readonly IWaitConditionManagerConfig config;

		// Token: 0x04001253 RID: 4691
		private readonly bool reversedCollection;

		// Token: 0x04001254 RID: 4692
		private readonly int maxThreads;

		// Token: 0x04001255 RID: 4693
		private readonly long processingCapacity;

		// Token: 0x04001256 RID: 4694
		private readonly IProcessingQuotaComponent quotaOverride;

		// Token: 0x04001257 RID: 4695
		private readonly Func<DateTime> timeProvider;
	}
}
