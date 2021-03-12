using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000012 RID: 18
	internal class RollingCountConfig : CountConfig, IRollingCountConfig, ICountedConfig, IEquatable<RollingCountConfig>
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x000052B7 File Offset: 0x000034B7
		private RollingCountConfig(bool isPromotable, int minActivityThreshold, TimeSpan timeToLive, TimeSpan idleTimeToLive, bool isRemovable, TimeSpan windowInterval, TimeSpan windowBucketSize, TimeSpan idleCleanupInterval, Func<DateTime> timeProvider) : base(isPromotable, minActivityThreshold, timeToLive, idleTimeToLive, isRemovable, idleCleanupInterval, timeProvider)
		{
			SlidingWindow.ValidateSlidingWindowAndBucketLength(windowInterval, windowBucketSize);
			this.windowInterval = windowInterval;
			this.windowBucketSize = windowBucketSize;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000052E3 File Offset: 0x000034E3
		public TimeSpan WindowInterval
		{
			get
			{
				base.UpdateAccessTime();
				return this.windowInterval;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000052F1 File Offset: 0x000034F1
		public TimeSpan WindowBucketSize
		{
			get
			{
				base.UpdateAccessTime();
				return this.windowBucketSize;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005308 File Offset: 0x00003508
		public static RollingCountConfig Create(bool promotable, int minActivityThreshold, TimeSpan timeToLive, TimeSpan idleTimeToLive, bool removable, TimeSpan idleCleanupInterval, TimeSpan windowInterval, TimeSpan windowBucketSize)
		{
			return RollingCountConfig.Create(promotable, minActivityThreshold, timeToLive, idleTimeToLive, removable, idleCleanupInterval, windowInterval, windowBucketSize, () => DateTime.UtcNow);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005344 File Offset: 0x00003544
		public static RollingCountConfig Create(bool promotable, int minActivityThreshold, TimeSpan timeToLive, TimeSpan idleTimeToLive, bool removable, TimeSpan idleCleanupInterval, TimeSpan windowInterval, TimeSpan windowBucketSize, Func<DateTime> timeProvider)
		{
			RollingCountConfig config = new RollingCountConfig(promotable, minActivityThreshold, timeToLive, idleTimeToLive, removable, windowInterval, windowBucketSize, idleCleanupInterval, timeProvider);
			return (RollingCountConfig)CountConfig.GetCachedObject(config);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005370 File Offset: 0x00003570
		public bool Equals(RollingCountConfig config)
		{
			return !object.ReferenceEquals(null, config) && (object.ReferenceEquals(this, config) || (this.windowInterval.Equals(config.windowInterval) && this.windowBucketSize.Equals(config.windowBucketSize) && base.Equals(config)));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000053C8 File Offset: 0x000035C8
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj is RollingCountConfig && this.Equals(obj as RollingCountConfig)));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000053F8 File Offset: 0x000035F8
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num = (num * 397 ^ this.windowInterval.GetHashCode());
			return num * 397 ^ this.windowBucketSize.GetHashCode();
		}

		// Token: 0x04000060 RID: 96
		private readonly TimeSpan windowInterval;

		// Token: 0x04000061 RID: 97
		private readonly TimeSpan windowBucketSize;
	}
}
