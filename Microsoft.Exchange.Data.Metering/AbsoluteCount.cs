using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000003 RID: 3
	internal class AbsoluteCount<TEntityType, TCountType> : Count<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000026EB File Offset: 0x000008EB
		public AbsoluteCount(ICountedEntity<TEntityType> entity, IAbsoluteCountConfig config, TCountType measure) : this(entity, config, measure, () => DateTime.UtcNow)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002713 File Offset: 0x00000913
		public AbsoluteCount(ICountedEntity<TEntityType> entity, IAbsoluteCountConfig config, TCountType measure, Func<DateTime> timeProvider) : base(entity, config, measure, timeProvider)
		{
			this.absoluteConfig = config;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002727 File Offset: 0x00000927
		public override long Total
		{
			get
			{
				return this.absoluteValue;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000272F File Offset: 0x0000092F
		public override long Average
		{
			get
			{
				if (base.Trendline != null)
				{
					return base.Trendline.GetAverage();
				}
				return this.absoluteValue;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000274B File Offset: 0x0000094B
		public override string ToString()
		{
			return string.Format("AbsoluteCount for Entity {0}, Measure:{1}", base.Entity, base.Measure);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002768 File Offset: 0x00000968
		protected override Count<TEntityType, TCountType> InternalMerge(Count<TEntityType, TCountType> count)
		{
			AbsoluteCount<TEntityType, TCountType> absoluteCount = count as AbsoluteCount<TEntityType, TCountType>;
			if (absoluteCount == null)
			{
				throw new InvalidOperationException("count with an absoluteConfig should be an AbsoluteCount");
			}
			if (base.NeedsUpdate)
			{
				base.TimedUpdate();
			}
			if (absoluteCount.NeedsUpdate)
			{
				absoluteCount.TimedUpdate();
			}
			this.absoluteValue += absoluteCount.absoluteValue;
			if (base.Trendline == null)
			{
				base.Trendline = absoluteCount.Trendline;
			}
			else if (absoluteCount.Trendline != null && base.Trendline.OldestPointTime > absoluteCount.Trendline.OldestPointTime)
			{
				base.Trendline = absoluteCount.Trendline;
			}
			base.UpdateAccessTime();
			absoluteCount.CopyPropertiesTo(this);
			return this;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000280E File Offset: 0x00000A0E
		protected override void InternalAddValue(long increment)
		{
			base.UpdateAccessTime();
			this.absoluteValue += increment;
			this.AddToTrendline();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000282A File Offset: 0x00000A2A
		protected override bool InternalSetValue(long value)
		{
			base.UpdateAccessTime();
			this.absoluteValue = value;
			this.AddToTrendline();
			return true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002840 File Offset: 0x00000A40
		private void AddToTrendline()
		{
			if (this.absoluteConfig.HistoryLookbackWindow <= TimeSpan.Zero)
			{
				return;
			}
			if (base.Trendline == null)
			{
				base.Trendline = new Trendline(this.absoluteConfig.HistoryLookbackWindow, this.absoluteConfig.IdleCleanupInterval, base.TimeProvider);
			}
			if (base.Trendline != null)
			{
				base.Trendline.AddDataPoint(this.absoluteValue);
			}
		}

		// Token: 0x0400000D RID: 13
		private readonly IAbsoluteCountConfig absoluteConfig;

		// Token: 0x0400000E RID: 14
		private long absoluteValue;
	}
}
