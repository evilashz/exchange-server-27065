using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000011 RID: 17
	internal class RollingCount<TEntityType, TCountType> : Count<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000050B3 File Offset: 0x000032B3
		public RollingCount(ICountedEntity<TEntityType> entity, IRollingCountConfig config, TCountType measure) : this(entity, config, measure, () => DateTime.UtcNow)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000050DB File Offset: 0x000032DB
		public RollingCount(ICountedEntity<TEntityType> entity, IRollingCountConfig config, TCountType measure, Func<DateTime> timeProvider) : base(entity, config, measure, timeProvider)
		{
			this.rollingCountConfig = config;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000050F0 File Offset: 0x000032F0
		public override long Total
		{
			get
			{
				if (this.rollingValue == null)
				{
					return 0L;
				}
				if (this.rollingValue.IsEmpty)
				{
					return 0L;
				}
				long sum = this.rollingValue.Sum;
				if (base.Trendline != null)
				{
					base.Trendline.AddDataPoint(sum);
				}
				return sum;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005139 File Offset: 0x00003339
		public override long Average
		{
			get
			{
				if (base.Trendline != null)
				{
					return base.Trendline.GetAverage();
				}
				return this.Total;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005155 File Offset: 0x00003355
		public override string ToString()
		{
			return string.Format("RollingCount for Entity {0}, Measure:{1}", base.Entity, base.Measure);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005174 File Offset: 0x00003374
		protected override Count<TEntityType, TCountType> InternalMerge(Count<TEntityType, TCountType> count)
		{
			RollingCount<TEntityType, TCountType> rollingCount = count as RollingCount<TEntityType, TCountType>;
			if (rollingCount == null)
			{
				throw new InvalidOperationException("count with a rollingConfig should be a RollingCount");
			}
			if (this.rollingValue == null)
			{
				base.CopyPropertiesTo(rollingCount);
				return rollingCount;
			}
			if (rollingCount.rollingValue == null)
			{
				return this;
			}
			if (base.NeedsUpdate)
			{
				base.TimedUpdate();
			}
			if (rollingCount.NeedsUpdate)
			{
				rollingCount.TimedUpdate();
			}
			this.rollingValue.Merge(rollingCount.rollingValue);
			base.Trendline = new Trendline(this.rollingValue.PastTotalValues, this.rollingCountConfig.WindowBucketSize, this.rollingCountConfig.IdleCleanupInterval, base.TimeProvider);
			rollingCount.CopyPropertiesTo(this);
			base.UpdateAccessTime();
			return this;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005220 File Offset: 0x00003420
		protected override void InternalAddValue(long increment)
		{
			base.UpdateAccessTime();
			if (this.rollingValue == null)
			{
				this.rollingValue = new SlidingTotalCounter(this.rollingCountConfig.WindowInterval, this.rollingCountConfig.WindowBucketSize, base.TimeProvider);
			}
			this.rollingValue.AddValue(increment);
			if (base.Trendline == null)
			{
				base.Trendline = new Trendline(this.rollingCountConfig.WindowInterval, this.rollingCountConfig.IdleCleanupInterval, base.TimeProvider);
			}
			base.Trendline.AddDataPoint(this.rollingValue.Sum);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000052B4 File Offset: 0x000034B4
		protected override bool InternalSetValue(long value)
		{
			return false;
		}

		// Token: 0x0400005D RID: 93
		private readonly IRollingCountConfig rollingCountConfig;

		// Token: 0x0400005E RID: 94
		private SlidingTotalCounter rollingValue;
	}
}
