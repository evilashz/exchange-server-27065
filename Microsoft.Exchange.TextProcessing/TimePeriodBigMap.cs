using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200001E RID: 30
	internal class TimePeriodBigMap<TKey, TValue> where TValue : IInitialize, new()
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000BB02 File Offset: 0x00009D02
		public TimePeriodBigMap(bool mergePrevious, int internalNoOfStore, int initialCapacity = 71993) : this(internalNoOfStore, TimePeriodBigMap<TKey, TValue>.defaultSwapTime, TimePeriodBigMap<TKey, TValue>.defaultMergeTime, TimePeriodBigMap<TKey, TValue>.defaultCleanTime, initialCapacity)
		{
			this.mergePrevious = mergePrevious;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000BB22 File Offset: 0x00009D22
		public TimePeriodBigMap() : this(31, TimePeriodBigMap<TKey, TValue>.defaultSwapTime, TimePeriodBigMap<TKey, TValue>.defaultMergeTime, TimePeriodBigMap<TKey, TValue>.defaultCleanTime, 71993)
		{
			this.mergePrevious = true;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000BB48 File Offset: 0x00009D48
		public TimePeriodBigMap(int internalNoOfStore, TimeSpan swapTime, TimeSpan mergeTime, TimeSpan cleanTime, int initialCapacity = 71993)
		{
			if (mergeTime >= cleanTime || cleanTime >= swapTime)
			{
				throw new ArgumentException("it should be mergeTime < cleanTime < swapTime");
			}
			this.maps = new BigMap<TKey, TValue>[2];
			this.maps[0] = new BigMap<TKey, TValue>(internalNoOfStore, initialCapacity);
			this.maps[1] = new BigMap<TKey, TValue>(internalNoOfStore, initialCapacity);
			this.SwapTime = swapTime;
			this.MergeTime = mergeTime;
			this.CleanTime = cleanTime;
			this.mergePrevious = true;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000BBC3 File Offset: 0x00009DC3
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000BBCB File Offset: 0x00009DCB
		public TimeSpan SwapTime { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public TimeSpan MergeTime { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000BBE5 File Offset: 0x00009DE5
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000BBED File Offset: 0x00009DED
		public TimeSpan CleanTime { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000BBF6 File Offset: 0x00009DF6
		public int Count
		{
			get
			{
				return this.maps[0].Count + this.maps[1].Count;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000BC14 File Offset: 0x00009E14
		public BigMap<TKey, TValue> GetCurrentMap(DateTime dateTime)
		{
			int num;
			return this.GetCurrentMap(dateTime, out num);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000BC2C File Offset: 0x00009E2C
		public BigMap<TKey, TValue> GetCurrentMap(DateTime dateTime, out int currentIdx)
		{
			currentIdx = (int)((dateTime.Ticks / this.SwapTime.Ticks + 1L) % 2L);
			BigMap<TKey, TValue> bigMap = this.maps[currentIdx];
			if (bigMap.TimeStamp != this.StartBoundaryTime(dateTime))
			{
				bigMap.Clear();
				bigMap.TimeStamp = this.StartBoundaryTime(dateTime);
			}
			return bigMap;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000BC8C File Offset: 0x00009E8C
		public TValue GetValue(DateTime dateTime, TKey key)
		{
			int num;
			BigMap<TKey, TValue> currentMap = this.GetCurrentMap(dateTime, out num);
			if (dateTime - currentMap.TimeStamp > this.CleanTime)
			{
				BigMap<TKey, TValue> previousMap = this.GetPreviousMap(dateTime);
				if (previousMap.TimeStamp != currentMap.TimeStamp + this.SwapTime)
				{
					previousMap.Clear();
					previousMap.TimeStamp = currentMap.TimeStamp + this.SwapTime;
				}
			}
			TValue tvalue;
			if (currentMap.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			if (this.mergePrevious && dateTime - currentMap.TimeStamp < this.MergeTime)
			{
				BigMap<TKey, TValue> previousMap2 = this.GetPreviousMap(dateTime);
				if (previousMap2.TimeStamp == this.PreviousStartBoundaryTime(dateTime))
				{
					previousMap2.TryGetValue(key, out tvalue);
				}
			}
			if (object.Equals(tvalue, default(TValue)))
			{
				tvalue = ((default(TValue) == null) ? Activator.CreateInstance<TValue>() : default(TValue));
			}
			return currentMap.AddOrGet(key, tvalue, delegate
			{
			});
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000BDBC File Offset: 0x00009FBC
		public bool TryGetValue(DateTime dateTime, TKey key, out TValue value)
		{
			BigMap<TKey, TValue> currentMap = this.GetCurrentMap(dateTime);
			if (dateTime - currentMap.TimeStamp > this.CleanTime)
			{
				BigMap<TKey, TValue> previousMap = this.GetPreviousMap(dateTime);
				if (previousMap.TimeStamp != currentMap.TimeStamp + this.SwapTime)
				{
					previousMap.Clear();
					previousMap.TimeStamp = currentMap.TimeStamp + this.SwapTime;
				}
			}
			if (currentMap.TryGetValue(key, out value))
			{
				return true;
			}
			if (this.mergePrevious && dateTime - currentMap.TimeStamp < this.MergeTime)
			{
				BigMap<TKey, TValue> previousMap2 = this.GetPreviousMap(dateTime);
				if (previousMap2.TimeStamp == this.PreviousStartBoundaryTime(dateTime) && previousMap2.TryGetValue(key, out value) && !object.Equals(value, default(TValue)))
				{
					currentMap.GetOrAdd(key, value);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public TValue GetOrAdd(DateTime dateTime, TKey key, TValue value)
		{
			BigMap<TKey, TValue> currentMap = this.GetCurrentMap(dateTime);
			return currentMap.GetOrAdd(key, value);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public TValue GetOrAdd(DateTime dateTime, TKey key, Func<TKey, TValue> valueFactory)
		{
			BigMap<TKey, TValue> currentMap = this.GetCurrentMap(dateTime);
			return currentMap.GetOrAdd(key, valueFactory);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public TValue AddOrUpdate(DateTime dateTime, TKey key, TValue value, Func<TKey, TValue, TValue> updateValueFactory)
		{
			BigMap<TKey, TValue> currentMap = this.GetCurrentMap(dateTime);
			return currentMap.AddOrUpdate(key, value, updateValueFactory);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000BF14 File Offset: 0x0000A114
		internal BigMap<TKey, TValue> GetPreviousMap(DateTime dateTime)
		{
			return this.maps[(int)(dateTime.Ticks / this.SwapTime.Ticks % 2L)];
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000BF44 File Offset: 0x0000A144
		private DateTime StartBoundaryTime(DateTime dateTime)
		{
			return new DateTime(dateTime.Ticks - dateTime.Ticks % this.SwapTime.Ticks);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000BF74 File Offset: 0x0000A174
		private DateTime PreviousStartBoundaryTime(DateTime dateTime)
		{
			return new DateTime(dateTime.Ticks - dateTime.Ticks % this.SwapTime.Ticks - this.SwapTime.Ticks);
		}

		// Token: 0x040000C1 RID: 193
		private static TimeSpan defaultSwapTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000C2 RID: 194
		private static TimeSpan defaultMergeTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x040000C3 RID: 195
		private static TimeSpan defaultCleanTime = TimeSpan.FromMinutes(2.0);

		// Token: 0x040000C4 RID: 196
		private readonly BigMap<TKey, TValue>[] maps;

		// Token: 0x040000C5 RID: 197
		private readonly bool mergePrevious;
	}
}
