using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009CB RID: 2507
	internal class SetCounterIf
	{
		// Token: 0x06007416 RID: 29718 RVA: 0x0017F15C File Offset: 0x0017D35C
		public SetCounterIf(ICounterWrapper counter, CounterCompareType compareType, int interval)
		{
			this.Counter = counter;
			this.CompareType = compareType;
			this.Interval = interval;
		}

		// Token: 0x06007417 RID: 29719 RVA: 0x0017F184 File Offset: 0x0017D384
		internal void SetLastRefreshForTest(int lastRefresh)
		{
			this.lastUpdateTicks = lastRefresh;
		}

		// Token: 0x1700295E RID: 10590
		// (get) Token: 0x06007418 RID: 29720 RVA: 0x0017F18D File Offset: 0x0017D38D
		// (set) Token: 0x06007419 RID: 29721 RVA: 0x0017F195 File Offset: 0x0017D395
		internal long OldValue { get; private set; }

		// Token: 0x1700295F RID: 10591
		// (get) Token: 0x0600741A RID: 29722 RVA: 0x0017F19E File Offset: 0x0017D39E
		// (set) Token: 0x0600741B RID: 29723 RVA: 0x0017F1A6 File Offset: 0x0017D3A6
		internal CounterCompareType CompareType { get; private set; }

		// Token: 0x17002960 RID: 10592
		// (get) Token: 0x0600741C RID: 29724 RVA: 0x0017F1AF File Offset: 0x0017D3AF
		// (set) Token: 0x0600741D RID: 29725 RVA: 0x0017F1B7 File Offset: 0x0017D3B7
		internal ICounterWrapper Counter { get; private set; }

		// Token: 0x17002961 RID: 10593
		// (get) Token: 0x0600741E RID: 29726 RVA: 0x0017F1C0 File Offset: 0x0017D3C0
		// (set) Token: 0x0600741F RID: 29727 RVA: 0x0017F1C8 File Offset: 0x0017D3C8
		internal int Interval { get; private set; }

		// Token: 0x06007420 RID: 29728 RVA: 0x0017F1D1 File Offset: 0x0017D3D1
		internal void Set(long newValue)
		{
			this.Set(newValue, Environment.TickCount);
		}

		// Token: 0x06007421 RID: 29729 RVA: 0x0017F1E0 File Offset: 0x0017D3E0
		internal void Set(long newValue, int nowTicks)
		{
			if (newValue == this.OldValue)
			{
				return;
			}
			if (this.Counter != null)
			{
				bool flag = false;
				if (this.Interval != 2147483647 && TickDiffer.Elapsed(this.lastUpdateTicks, nowTicks).TotalMilliseconds > (double)this.Interval)
				{
					flag = true;
				}
				else
				{
					switch (this.CompareType)
					{
					case CounterCompareType.Lower:
						flag = (newValue < this.OldValue);
						break;
					case CounterCompareType.Higher:
						flag = (newValue > this.OldValue);
						break;
					case CounterCompareType.Changed:
						flag = true;
						break;
					}
				}
				if (flag)
				{
					this.Counter.SetRawValue(newValue);
					this.OldValue = newValue;
					this.lastUpdateTicks = nowTicks;
				}
			}
		}

		// Token: 0x06007422 RID: 29730 RVA: 0x0017F288 File Offset: 0x0017D488
		internal void ForceSet(long newValue)
		{
			this.Counter.SetRawValue(newValue);
			this.OldValue = newValue;
			this.lastUpdateTicks = Environment.TickCount;
		}

		// Token: 0x06007423 RID: 29731 RVA: 0x0017F2A8 File Offset: 0x0017D4A8
		internal void SetOldValue(long oldValue)
		{
			this.OldValue = oldValue;
		}

		// Token: 0x04004AFD RID: 19197
		public const int NoIntervalCheck = 2147483647;

		// Token: 0x04004AFE RID: 19198
		private int lastUpdateTicks = Environment.TickCount;

		// Token: 0x020009CC RID: 2508
		internal class CounterWrapper : ICounterWrapper
		{
			// Token: 0x06007424 RID: 29732 RVA: 0x0017F2B1 File Offset: 0x0017D4B1
			public CounterWrapper(ExPerformanceCounter counter)
			{
				if (counter == null)
				{
					throw new ArgumentNullException("counter");
				}
				this.counter = counter;
			}

			// Token: 0x06007425 RID: 29733 RVA: 0x0017F2CE File Offset: 0x0017D4CE
			public void SetRawValue(long value)
			{
				this.counter.RawValue = value;
			}

			// Token: 0x04004B03 RID: 19203
			private ExPerformanceCounter counter;
		}
	}
}
