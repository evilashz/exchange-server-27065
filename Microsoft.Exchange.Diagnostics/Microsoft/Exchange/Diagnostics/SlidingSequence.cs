using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000E8 RID: 232
	public class SlidingSequence<T> : SlidingWindow, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x0001AD84 File Offset: 0x00018F84
		public SlidingSequence(TimeSpan slidingWindowLength, TimeSpan bucketLength) : this(slidingWindowLength, bucketLength, () => DateTime.UtcNow)
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001AE18 File Offset: 0x00019018
		public SlidingSequence(TimeSpan slidingWindowLength, TimeSpan bucketLength, Func<DateTime> currentTimeProvider) : base(slidingWindowLength, bucketLength, currentTimeProvider)
		{
			this.valueBuckets = new List<T>[this.NumberOfBuckets];
			this.isComparable = typeof(T).GetInterfaces().Any(delegate(Type x)
			{
				if (x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IComparable<>))
				{
					return x.GetGenericArguments().All((Type t) => t == typeof(T));
				}
				return false;
			});
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001AE76 File Offset: 0x00019076
		public new DateTime OldestDataTime
		{
			get
			{
				return base.OldestDataTime;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001AE7E File Offset: 0x0001907E
		protected override Array ValueBuckets
		{
			get
			{
				return this.valueBuckets;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001AE86 File Offset: 0x00019086
		public void AddValue(T value)
		{
			base.ExpireBucketsIfNecessary();
			this.AddToBucket(this.currentBucket, value);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001AE9C File Offset: 0x0001909C
		public void AddValue(T value, DateTime time)
		{
			DateTime dateTime = this.currentTimeProvider();
			if (time > dateTime)
			{
				throw new ArgumentOutOfRangeException("time", "Time must be in the past");
			}
			DateTime timeStampUtc = base.RoundToBucketLength(time);
			DateTime now = base.RoundToBucketLength(dateTime);
			base.ExpireBucketsIfNecessary();
			int targetBucket = base.GetTargetBucket(now, timeStampUtc);
			if (targetBucket == -1)
			{
				return;
			}
			this.AddToBucket(targetBucket, value);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001AEFC File Offset: 0x000190FC
		public T GetLast()
		{
			base.ExpireBucketsIfNecessary();
			if (this.valueBuckets[this.currentBucket] != null)
			{
				return this.valueBuckets[this.currentBucket][this.valueBuckets[this.currentBucket].Count - 1];
			}
			for (int num = this.currentBucket - 1; num != this.currentBucket; num--)
			{
				if (num < 0)
				{
					num = this.valueBuckets.Length - 1;
				}
				if (this.valueBuckets[num] != null)
				{
					if (this.valueBuckets[num].Count != 0)
					{
						return this.valueBuckets[num][this.valueBuckets[num].Count - 1];
					}
					this.valueBuckets[num] = null;
				}
			}
			return default(T);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001AFB8 File Offset: 0x000191B8
		public T GetMax()
		{
			if (!this.isComparable)
			{
				return default(T);
			}
			base.ExpireBucketsIfNecessary();
			return this.RecalculateMax();
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001AFE4 File Offset: 0x000191E4
		public T GetMin()
		{
			if (!this.isComparable)
			{
				return default(T);
			}
			base.ExpireBucketsIfNecessary();
			return this.RecalculateMin();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001B1E8 File Offset: 0x000193E8
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			base.ExpireBucketsIfNecessary();
			for (int i = this.currentBucket; i >= 0; i--)
			{
				if (this.valueBuckets[i] != null)
				{
					for (int j = this.valueBuckets[i].Count - 1; j >= 0; j--)
					{
						yield return this.valueBuckets[i][j];
					}
				}
			}
			for (int k = this.valueBuckets.Length - 1; k > this.currentBucket; k--)
			{
				if (this.valueBuckets[k] != null)
				{
					for (int l = this.valueBuckets[k].Count - 1; l >= 0; l--)
					{
						yield return this.valueBuckets[k][l];
					}
				}
			}
			yield break;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001B2A0 File Offset: 0x000194A0
		public IEnumerator GetEnumerator()
		{
			foreach (!0 ! in ((IEnumerable<!0>)this))
			{
				yield return !;
			}
			yield break;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001B2BC File Offset: 0x000194BC
		protected override void ExpireBucket(int bucket)
		{
			if (this.isComparable)
			{
				if (this.maxSet && this.valueBuckets[bucket] != null && ((IComparable)((object)this.valueBuckets[bucket].Max<T>())).CompareTo(this.max) == 0)
				{
					this.max = default(T);
					this.maxSet = false;
				}
				if (this.minSet && this.valueBuckets[bucket] != null && ((IComparable)((object)this.valueBuckets[bucket].Min<T>())).CompareTo(this.min) == 0)
				{
					this.min = default(T);
					this.minSet = false;
				}
			}
			this.valueBuckets[bucket] = null;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001B37C File Offset: 0x0001957C
		private void AddToBucket(int bucket, T value)
		{
			if (this.valueBuckets[bucket] == null)
			{
				this.valueBuckets[bucket] = new List<T>();
			}
			this.valueBuckets[bucket].Add(value);
			base.SetLastUpdateTime();
			if (this.isComparable)
			{
				this.RecalculateMax();
				this.RecalculateMin();
				if (((IComparable<T>)((object)value)).CompareTo(this.max) > 0)
				{
					this.max = value;
				}
				if (((IComparable<T>)((object)value)).CompareTo(this.min) < 0)
				{
					this.min = value;
				}
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001B410 File Offset: 0x00019610
		private T RecalculateMax()
		{
			if (!this.maxSet)
			{
				if (this.Any((T t) => true))
				{
					this.max = this.Max<T>();
					this.maxSet = true;
				}
			}
			return this.max;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001B468 File Offset: 0x00019668
		private T RecalculateMin()
		{
			if (!this.minSet)
			{
				if (this.Any((T t) => true))
				{
					this.min = this.Min<T>();
					this.minSet = true;
				}
			}
			return this.min;
		}

		// Token: 0x04000471 RID: 1137
		private readonly List<T>[] valueBuckets;

		// Token: 0x04000472 RID: 1138
		private readonly bool isComparable;

		// Token: 0x04000473 RID: 1139
		private T max;

		// Token: 0x04000474 RID: 1140
		private bool maxSet;

		// Token: 0x04000475 RID: 1141
		private T min;

		// Token: 0x04000476 RID: 1142
		private bool minSet;
	}
}
