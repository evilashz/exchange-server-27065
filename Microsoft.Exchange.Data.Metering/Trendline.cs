using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000014 RID: 20
	internal class Trendline : ITrendline
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00005520 File Offset: 0x00003720
		public Trendline(TimeSpan historyInterval, TimeSpan idleCleanupInterval, Func<DateTime> timeProvider)
		{
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("historyInterval", historyInterval, TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("idleCleanupInterval", idleCleanupInterval, TimeSpan.Zero, TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			this.historyInterval = historyInterval;
			this.idleCleanupInterval = idleCleanupInterval;
			this.timeProvider = timeProvider;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005598 File Offset: 0x00003798
		public Trendline(IEnumerable<long> values, TimeSpan bucketLength, TimeSpan idleCleanupInterval, Func<DateTime> timeProvider)
		{
			ArgumentValidator.ThrowIfNull("values", values);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("bucketLength", bucketLength, TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("idleCleanupInterval", idleCleanupInterval, TimeSpan.Zero, TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			this.idleCleanupInterval = idleCleanupInterval;
			this.timeProvider = timeProvider;
			int num = values.Count<long>();
			this.historyInterval = new TimeSpan((long)num * bucketLength.Ticks);
			DateTime currentTime = new DateTime(this.timeProvider().Ticks - (long)num * bucketLength.Ticks);
			foreach (long value in values)
			{
				this.AddDataPoint(value, currentTime);
				currentTime = currentTime.AddTicks(bucketLength.Ticks);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000056A8 File Offset: 0x000038A8
		public bool IsEmpty
		{
			get
			{
				return this.counter == null || this.CheckIfCounterExpired(this.timeProvider());
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000056C8 File Offset: 0x000038C8
		public DateTime OldestPointTime
		{
			get
			{
				if (this.missingFirstPoint)
				{
					return DateTime.MaxValue;
				}
				if (this.HasAtLeastTwoPoints && !this.counter.IsEmpty)
				{
					lock (this.syncObject)
					{
						bool flag;
						if (flag)
						{
							return this.counter.OldestDataTime;
						}
					}
					return DateTime.MaxValue;
				}
				if (this.timeProvider() - this.updateTime < this.historyInterval)
				{
					return this.updateTime;
				}
				return DateTime.MaxValue;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005770 File Offset: 0x00003970
		private bool HasAtLeastTwoPoints
		{
			get
			{
				return this.isIncreasing != null;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005790 File Offset: 0x00003990
		public bool WasAbove(long high)
		{
			if (this.counter == null)
			{
				return false;
			}
			bool result;
			lock (this.syncObject)
			{
				bool flag;
				if (flag)
				{
					if (this.ExpireOldPoints())
					{
						bool flag2;
						if (this.currentPoint <= high)
						{
							if (this.HasAtLeastTwoPoints)
							{
								flag2 = this.counter.Any((long i) => i > high);
							}
							else
							{
								flag2 = false;
							}
						}
						else
						{
							flag2 = true;
						}
						result = flag2;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005848 File Offset: 0x00003A48
		public bool WasBelow(long low)
		{
			if (this.counter == null)
			{
				return false;
			}
			bool result;
			lock (this.syncObject)
			{
				bool flag;
				if (flag)
				{
					if (this.ExpireOldPoints())
					{
						bool flag2;
						if (this.currentPoint >= low)
						{
							if (this.HasAtLeastTwoPoints)
							{
								flag2 = this.counter.Any((long i) => i < low);
							}
							else
							{
								flag2 = false;
							}
						}
						else
						{
							flag2 = true;
						}
						result = flag2;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000058F8 File Offset: 0x00003AF8
		public bool HasCrossedBelowAfterLastCrossingAbove(long high, long low)
		{
			return this.EnumeratePointsCompareHighLow(high, low, this.currentPoint < low, (bool b) => Trendline.DelegateResult.SetStateTrue, delegate(bool b)
			{
				if (!b)
				{
					return Trendline.DelegateResult.ReturnFalse;
				}
				return Trendline.DelegateResult.ReturnTrue;
			});
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000595C File Offset: 0x00003B5C
		public bool HasCrossedAboveAfterLastCrossingBelow(long low, long high)
		{
			return this.EnumeratePointsCompareHighLow(high, low, this.currentPoint > high, delegate(bool b)
			{
				if (!b)
				{
					return Trendline.DelegateResult.ReturnFalse;
				}
				return Trendline.DelegateResult.ReturnTrue;
			}, (bool b) => Trendline.DelegateResult.SetStateTrue);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000059BC File Offset: 0x00003BBC
		public bool StillAboveLowAfterCrossingHigh(long high, long low)
		{
			if (low >= high)
			{
				return false;
			}
			if (!this.missingFirstPoint && this.currentPoint < low)
			{
				return false;
			}
			if (!this.missingFirstPoint && this.currentPoint > high)
			{
				return true;
			}
			return this.EnumeratePointsCompareHighLow(high, low, false, (bool b) => Trendline.DelegateResult.ReturnFalse, (bool b) => Trendline.DelegateResult.ReturnTrue);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005A40 File Offset: 0x00003C40
		public bool StillBelowHighAfterCrossingLow(long low, long high)
		{
			if (low >= high)
			{
				return false;
			}
			if (!this.missingFirstPoint && this.currentPoint > high)
			{
				return false;
			}
			if (!this.missingFirstPoint && this.currentPoint < low)
			{
				return true;
			}
			return this.EnumeratePointsCompareHighLow(high, low, false, (bool b) => Trendline.DelegateResult.ReturnTrue, (bool b) => Trendline.DelegateResult.ReturnFalse);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005ABC File Offset: 0x00003CBC
		public long GetMax()
		{
			if (this.counter == null)
			{
				return 0L;
			}
			long result;
			lock (this.syncObject)
			{
				bool flag;
				if (flag)
				{
					if (this.ExpireOldPoints())
					{
						if (this.HasAtLeastTwoPoints)
						{
							long max = this.counter.GetMax();
							result = Math.Max(max, this.currentPoint);
						}
						else
						{
							result = this.currentPoint;
						}
					}
					else
					{
						result = 0L;
					}
				}
				else
				{
					result = 0L;
				}
			}
			return result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005B44 File Offset: 0x00003D44
		public long GetMin()
		{
			if (this.counter == null)
			{
				return 0L;
			}
			long result;
			lock (this.syncObject)
			{
				bool flag;
				if (flag)
				{
					if (this.ExpireOldPoints())
					{
						if (this.HasAtLeastTwoPoints)
						{
							long min = this.counter.GetMin();
							result = Math.Min(min, this.currentPoint);
						}
						else
						{
							result = this.currentPoint;
						}
					}
					else
					{
						result = 0L;
					}
				}
				else
				{
					result = 0L;
				}
			}
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005BCC File Offset: 0x00003DCC
		public long GetAverage()
		{
			if (this.counter == null)
			{
				return 0L;
			}
			long result;
			lock (this.syncObject)
			{
				bool flag;
				if (flag)
				{
					if (this.ExpireOldPoints())
					{
						if (this.HasAtLeastTwoPoints)
						{
							result = Convert.ToInt64(this.counter.Concat(new List<long>
							{
								this.currentPoint
							}).Average());
						}
						else
						{
							result = this.currentPoint;
						}
					}
					else
					{
						result = 0L;
					}
				}
				else
				{
					result = 0L;
				}
			}
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005C64 File Offset: 0x00003E64
		public void AddDataPoint(long value)
		{
			this.AddDataPoint(value, this.timeProvider());
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005C78 File Offset: 0x00003E78
		private void AddDataPoint(long value, DateTime currentTime)
		{
			if (this.missingFirstPoint)
			{
				this.RecordFirstPoint(value, currentTime);
				return;
			}
			if (value == this.currentPoint)
			{
				this.updateTime = currentTime;
				return;
			}
			lock (this.syncObject)
			{
				bool flag;
				if (flag)
				{
					if (this.isIncreasing == null)
					{
						this.counter.AddValue(this.currentPoint, this.updateTime);
						this.previousPoint = this.currentPoint;
						this.isIncreasing = new bool?(value > this.currentPoint);
						this.UpdateCurrentPoint(value, currentTime);
					}
					else if ((this.currentPoint < value && this.isIncreasing.Value) || (this.currentPoint > value && !this.isIncreasing.Value))
					{
						this.UpdateCurrentPoint(value, currentTime);
					}
					else if ((this.currentPoint > value && this.isIncreasing.Value) || (this.currentPoint < value && !this.isIncreasing.Value))
					{
						this.RecordPoint(value, currentTime);
					}
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005D98 File Offset: 0x00003F98
		private void UpdateCurrentPoint(long value, DateTime currentTime)
		{
			this.currentPoint = value;
			this.updateTime = currentTime;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005DA8 File Offset: 0x00003FA8
		private void RecordPoint(long value, DateTime currentTime)
		{
			if (this.ExpireOldPoints())
			{
				this.previousPoint = this.currentPoint;
				this.UpdateCurrentPoint(value, currentTime);
				this.isIncreasing = !this.isIncreasing;
				this.counter.AddValue(this.previousPoint, this.updateTime);
				return;
			}
			this.RecordFirstPoint(value, currentTime);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005E24 File Offset: 0x00004024
		private void RecordFirstPoint(long value, DateTime currentTime)
		{
			if (this.counter == null)
			{
				lock (this.syncObject)
				{
					if (this.counter == null)
					{
						this.counter = new SlidingSequence<long>(this.historyInterval, TimeSpan.FromMilliseconds(Math.Max(1000.0, this.historyInterval.TotalMilliseconds / 30.0)), this.timeProvider);
					}
				}
			}
			this.UpdateCurrentPoint(value, currentTime);
			this.missingFirstPoint = false;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005EC0 File Offset: 0x000040C0
		private bool ExpireOldPoints()
		{
			DateTime dateTime = this.timeProvider();
			if (dateTime - this.updateTime >= this.historyInterval)
			{
				if (this.CheckIfCounterExpired(dateTime))
				{
					this.counter = null;
				}
				this.currentPoint = 0L;
				this.previousPoint = 0L;
				this.isIncreasing = null;
				this.missingFirstPoint = true;
				return false;
			}
			if (this.isIncreasing != null && this.counter.GetLast() != this.previousPoint)
			{
				this.counter.AddValue(this.previousPoint, this.updateTime.Subtract(TimeSpan.FromSeconds(1.0)));
			}
			return true;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005F72 File Offset: 0x00004172
		private bool CheckIfCounterExpired(DateTime now)
		{
			return now - this.updateTime >= this.idleCleanupInterval + this.historyInterval;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005F98 File Offset: 0x00004198
		private bool EnumeratePointsCompareHighLow(long high, long low, bool initState, Func<bool, Trendline.DelegateResult> actionOnLow, Func<bool, Trendline.DelegateResult> actionOnHigh)
		{
			if (low >= high)
			{
				return false;
			}
			if (this.counter == null)
			{
				return false;
			}
			bool result;
			lock (this.syncObject)
			{
				bool flag;
				if (flag)
				{
					if (this.ExpireOldPoints() && this.HasAtLeastTwoPoints)
					{
						foreach (object obj in this.counter)
						{
							long num = (long)obj;
							if (num > high)
							{
								Trendline.DelegateResult delegateResult = actionOnHigh(initState);
								if (delegateResult == Trendline.DelegateResult.ReturnFalse)
								{
									return false;
								}
								if (delegateResult == Trendline.DelegateResult.ReturnTrue)
								{
									return true;
								}
								if (delegateResult == Trendline.DelegateResult.SetStateFalse)
								{
									initState = false;
								}
								else if (delegateResult == Trendline.DelegateResult.SetStateTrue)
								{
									initState = true;
								}
							}
							if (num < low)
							{
								Trendline.DelegateResult delegateResult2 = actionOnLow(initState);
								if (delegateResult2 == Trendline.DelegateResult.ReturnFalse)
								{
									return false;
								}
								if (delegateResult2 == Trendline.DelegateResult.ReturnTrue)
								{
									return true;
								}
								if (delegateResult2 == Trendline.DelegateResult.SetStateFalse)
								{
									initState = false;
								}
								else if (delegateResult2 == Trendline.DelegateResult.SetStateTrue)
								{
									initState = true;
								}
							}
						}
					}
					result = false;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04000065 RID: 101
		private readonly TimeSpan historyInterval;

		// Token: 0x04000066 RID: 102
		private readonly TimeSpan idleCleanupInterval;

		// Token: 0x04000067 RID: 103
		private readonly Func<DateTime> timeProvider;

		// Token: 0x04000068 RID: 104
		private readonly object syncObject = new object();

		// Token: 0x04000069 RID: 105
		private SlidingSequence<long> counter;

		// Token: 0x0400006A RID: 106
		private long currentPoint;

		// Token: 0x0400006B RID: 107
		private DateTime updateTime;

		// Token: 0x0400006C RID: 108
		private long previousPoint;

		// Token: 0x0400006D RID: 109
		private bool? isIncreasing;

		// Token: 0x0400006E RID: 110
		private bool missingFirstPoint = true;

		// Token: 0x02000015 RID: 21
		private enum DelegateResult
		{
			// Token: 0x04000078 RID: 120
			Nothing,
			// Token: 0x04000079 RID: 121
			ReturnTrue,
			// Token: 0x0400007A RID: 122
			ReturnFalse,
			// Token: 0x0400007B RID: 123
			SetStateTrue,
			// Token: 0x0400007C RID: 124
			SetStateFalse
		}
	}
}
