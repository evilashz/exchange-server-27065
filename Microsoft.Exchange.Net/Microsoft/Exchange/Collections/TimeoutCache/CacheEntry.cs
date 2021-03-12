using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x0200069C RID: 1692
	internal class CacheEntry<K, T>
	{
		// Token: 0x06001F03 RID: 7939 RVA: 0x0003A890 File Offset: 0x00038A90
		private CacheEntry(TimeoutType timeoutType, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime, K key, T value, RemoveItemDelegate<K, T> callback)
		{
			if (absoluteLiveTime.TotalMilliseconds <= 0.0)
			{
				throw new ArgumentException("absoluteLiveTime must represent a positive (and non-zero) time interval.");
			}
			if (slidingLiveTime.TotalMilliseconds <= 0.0)
			{
				throw new ArgumentException("slidingLiveTime must represent a positive (and non-zero) time interval.");
			}
			this.key = key;
			this.value = value;
			this.callback = callback;
			this.timeoutType = timeoutType;
			this.InShouldRemoveCycle = false;
			this.absoluteExpirationTime = ((absoluteLiveTime == TimeSpan.MaxValue) ? DateTime.MaxValue : DateTime.UtcNow.Add(absoluteLiveTime));
			if (this.timeoutType == TimeoutType.Sliding)
			{
				this.slidingLiveTime = slidingLiveTime;
				this.nextExpirationTime = DateTime.UtcNow.Add(this.slidingLiveTime);
			}
			else
			{
				this.slidingLiveTime = absoluteLiveTime;
				this.nextExpirationTime = this.absoluteExpirationTime;
			}
			this.touchedExpirationTime = this.nextExpirationTime;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0003A978 File Offset: 0x00038B78
		private CacheEntry(DateTime absoluteExpirationTime, K key, T value, RemoveItemDelegate<K, T> callback)
		{
			TimeSpan timeSpan = (absoluteExpirationTime == DateTime.MaxValue) ? TimeSpan.MaxValue : (absoluteExpirationTime - DateTime.UtcNow);
			if (timeSpan.TotalMilliseconds < 0.0)
			{
				throw new ArgumentException("absoluteExpirationTime is in the past.");
			}
			this.absoluteExpirationTime = absoluteExpirationTime;
			this.callback = callback;
			this.key = key;
			this.value = value;
			this.nextExpirationTime = absoluteExpirationTime;
			this.touchedExpirationTime = this.nextExpirationTime;
			this.slidingLiveTime = timeSpan;
			this.timeoutType = TimeoutType.Absolute;
			this.InShouldRemoveCycle = false;
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0003AA0D File Offset: 0x00038C0D
		internal T Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x0003AA15 File Offset: 0x00038C15
		internal K Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0003AA1D File Offset: 0x00038C1D
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0003AA27 File Offset: 0x00038C27
		internal bool InShouldRemoveCycle
		{
			get
			{
				return this.inShouldRemoveCycle;
			}
			set
			{
				this.inShouldRemoveCycle = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0003AA32 File Offset: 0x00038C32
		internal DateTime AbsoluteExpirationTime
		{
			get
			{
				return this.absoluteExpirationTime;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x0003AA3A File Offset: 0x00038C3A
		internal DateTime NextExpirationTime
		{
			get
			{
				return this.nextExpirationTime;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0003AA42 File Offset: 0x00038C42
		internal TimeSpan SlidingLiveTime
		{
			get
			{
				return this.slidingLiveTime;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0003AA4A File Offset: 0x00038C4A
		internal DateTime TouchedExpirationTime
		{
			get
			{
				return this.touchedExpirationTime;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0003AA52 File Offset: 0x00038C52
		internal RemoveItemDelegate<K, T> Callback
		{
			get
			{
				return this.callback;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0003AA5A File Offset: 0x00038C5A
		internal TimeoutType TimeoutType
		{
			get
			{
				return this.timeoutType;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0003AA62 File Offset: 0x00038C62
		// (set) Token: 0x06001F10 RID: 7952 RVA: 0x0003AA6A File Offset: 0x00038C6A
		internal CacheEntry<K, T> Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0003AA73 File Offset: 0x00038C73
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x0003AA7B File Offset: 0x00038C7B
		internal CacheEntry<K, T> Previous
		{
			get
			{
				return this.previous;
			}
			set
			{
				this.previous = value;
			}
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0003AA84 File Offset: 0x00038C84
		public static CacheEntry<K, T> CreateAbsolute(DateTime expireExact, K key, T value, RemoveItemDelegate<K, T> callback)
		{
			return new CacheEntry<K, T>(expireExact, key, value, callback);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0003AA8F File Offset: 0x00038C8F
		public static CacheEntry<K, T> CreateAbsolute(TimeSpan absoluteLiveTime, K key, T value, RemoveItemDelegate<K, T> callback)
		{
			return new CacheEntry<K, T>(TimeoutType.Absolute, absoluteLiveTime, absoluteLiveTime, key, value, callback);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0003AA9C File Offset: 0x00038C9C
		public static CacheEntry<K, T> CreateSliding(TimeSpan slidingLiveTime, K key, T value, RemoveItemDelegate<K, T> callback)
		{
			return new CacheEntry<K, T>(TimeoutType.Sliding, slidingLiveTime, TimeSpan.MaxValue, key, value, callback);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0003AAAD File Offset: 0x00038CAD
		public static CacheEntry<K, T> CreateLimitedSliding(TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime, K key, T value, RemoveItemDelegate<K, T> callback)
		{
			if (slidingLiveTime.TotalMilliseconds > absoluteLiveTime.TotalMilliseconds)
			{
				throw new ArgumentException("absoluteLiveTime must be greater than slidingLiveTime");
			}
			return new CacheEntry<K, T>(TimeoutType.Sliding, slidingLiveTime, absoluteLiveTime, key, value, callback);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0003AAD6 File Offset: 0x00038CD6
		internal void Extend()
		{
			this.nextExpirationTime = this.GetLimitedExtensionTime();
			this.touchedExpirationTime = this.nextExpirationTime;
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0003AAF0 File Offset: 0x00038CF0
		internal void UnrestrictedExtend()
		{
			this.nextExpirationTime = this.GetExtensionTime();
			this.touchedExpirationTime = this.nextExpirationTime;
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0003AB0A File Offset: 0x00038D0A
		internal void ExtendToTouchTime()
		{
			if (this.timeoutType == TimeoutType.Sliding)
			{
				this.nextExpirationTime = this.touchedExpirationTime;
			}
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0003AB21 File Offset: 0x00038D21
		internal void Touch()
		{
			if (this.timeoutType == TimeoutType.Sliding)
			{
				this.touchedExpirationTime = this.GetLimitedExtensionTime();
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0003AB38 File Offset: 0x00038D38
		private DateTime GetLimitedExtensionTime()
		{
			if (this.nextExpirationTime == this.absoluteExpirationTime)
			{
				return this.nextExpirationTime;
			}
			DateTime dateTime = DateTime.UtcNow.Add(this.slidingLiveTime);
			if (!(dateTime < this.absoluteExpirationTime))
			{
				return this.absoluteExpirationTime;
			}
			return dateTime;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0003AB8C File Offset: 0x00038D8C
		private DateTime GetExtensionTime()
		{
			return DateTime.UtcNow.Add(this.slidingLiveTime);
		}

		// Token: 0x04001E7F RID: 7807
		private readonly K key;

		// Token: 0x04001E80 RID: 7808
		private readonly T value;

		// Token: 0x04001E81 RID: 7809
		private readonly DateTime absoluteExpirationTime;

		// Token: 0x04001E82 RID: 7810
		private readonly TimeoutType timeoutType;

		// Token: 0x04001E83 RID: 7811
		private readonly TimeSpan slidingLiveTime;

		// Token: 0x04001E84 RID: 7812
		private readonly RemoveItemDelegate<K, T> callback;

		// Token: 0x04001E85 RID: 7813
		private DateTime nextExpirationTime;

		// Token: 0x04001E86 RID: 7814
		private DateTime touchedExpirationTime;

		// Token: 0x04001E87 RID: 7815
		private CacheEntry<K, T> next;

		// Token: 0x04001E88 RID: 7816
		private CacheEntry<K, T> previous;

		// Token: 0x04001E89 RID: 7817
		private volatile bool inShouldRemoveCycle;
	}
}
