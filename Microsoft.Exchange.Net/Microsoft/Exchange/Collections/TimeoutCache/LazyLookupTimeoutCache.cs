using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x020006A3 RID: 1699
	internal abstract class LazyLookupTimeoutCache<K, T> : DisposeTrackableBase
	{
		// Token: 0x06001F4E RID: 8014 RVA: 0x0003B17C File Offset: 0x0003937C
		protected LazyLookupTimeoutCache(int buckets, int maxBucketSize, bool shouldCallbackOnDispose, TimeSpan absoluteLiveTime) : this(buckets, maxBucketSize, shouldCallbackOnDispose, TimeoutType.Absolute, absoluteLiveTime, absoluteLiveTime)
		{
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0003B18C File Offset: 0x0003938C
		protected LazyLookupTimeoutCache(int buckets, int maxBucketSize, bool shouldCallbackOnDispose, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime) : this(buckets, maxBucketSize, shouldCallbackOnDispose, TimeoutType.Sliding, slidingLiveTime, absoluteLiveTime)
		{
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0003B19C File Offset: 0x0003939C
		private LazyLookupTimeoutCache(int buckets, int maxBucketSize, bool shouldCallbackOnDispose, TimeoutType timeoutType, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime)
		{
			this.timeoutCache = new TimeoutCache<K, T>(buckets, maxBucketSize, shouldCallbackOnDispose, new PreprocessKeyDelegate<K>(this.PreprocessKey), new ShouldRemoveDelegate<K, T>(this.HandleShouldRemove), new HandleBeforeAdd<K, T>(this.HandleBeforeAdd));
			this.timeoutType = timeoutType;
			this.slidingLiveTime = slidingLiveTime;
			this.absoluteLiveTime = absoluteLiveTime;
			this.handleRemoveDelegate = new RemoveItemDelegate<K, T>(this.HandleRemove);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0003B20F File Offset: 0x0003940F
		internal virtual void Clear()
		{
			this.timeoutCache.Clear();
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0003B21C File Offset: 0x0003941C
		protected virtual bool HandleShouldRemove(K key, T value)
		{
			return true;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0003B21F File Offset: 0x0003941F
		protected virtual bool HandleBeforeAdd(K key, T value, TimeoutCacheBucket<K, T> bucket)
		{
			return true;
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0003B222 File Offset: 0x00039422
		protected virtual K PreprocessKey(K key)
		{
			return key;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0003B225 File Offset: 0x00039425
		protected virtual void HandleRemove(K key, T value, RemoveReason reason)
		{
			if (reason != RemoveReason.Removed)
			{
				this.CleanupValue(key, value);
			}
		}

		// Token: 0x06001F56 RID: 8022
		protected abstract T CreateOnCacheMiss(K key, ref bool shouldAdd);

		// Token: 0x06001F57 RID: 8023 RVA: 0x0003B233 File Offset: 0x00039433
		protected virtual void CleanupValue(K key, T value)
		{
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0003B238 File Offset: 0x00039438
		protected virtual bool TryPerformAdd(K key, T value)
		{
			bool result;
			try
			{
				if (this.timeoutType == TimeoutType.Absolute)
				{
					this.timeoutCache.AddAbsolute(key, value, this.absoluteLiveTime, this.handleRemoveDelegate);
				}
				else if (this.absoluteLiveTime == TimeSpan.MaxValue)
				{
					this.timeoutCache.AddSliding(key, value, this.slidingLiveTime, this.handleRemoveDelegate);
				}
				else
				{
					this.timeoutCache.AddLimitedSliding(key, value, this.slidingLiveTime, this.absoluteLiveTime, this.handleRemoveDelegate);
				}
				result = true;
			}
			catch (DuplicateKeyException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0003B2D0 File Offset: 0x000394D0
		protected virtual void AfterCacheHit(K key, T value)
		{
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0003B2D2 File Offset: 0x000394D2
		internal T Remove(K key)
		{
			return this.timeoutCache.Remove(key);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0003B2E0 File Offset: 0x000394E0
		internal bool Contains(K key)
		{
			return this.timeoutCache.Contains(key);
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x0003B2EE File Offset: 0x000394EE
		internal int Count
		{
			get
			{
				return this.timeoutCache.Count;
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0003B2FC File Offset: 0x000394FC
		internal T Get(K key)
		{
			T t = default(T);
			key = this.PreprocessKey(key);
			if (this.timeoutCache.TryGetValue(key, out t))
			{
				this.AfterCacheHit(key, t);
				return t;
			}
			bool flag = true;
			t = this.CreateOnCacheMiss(key, ref flag);
			if (flag)
			{
				bool flag2 = false;
				T value = default(T);
				while (!this.TryPerformAdd(key, t))
				{
					T t2;
					if (this.timeoutCache.TryGetValue(key, out t2))
					{
						flag2 = true;
						value = t;
						t = t2;
						break;
					}
				}
				if (flag2)
				{
					this.CleanupValue(key, value);
				}
			}
			return t;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0003B37A File Offset: 0x0003957A
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.timeoutCache != null)
			{
				this.timeoutCache.Dispose();
				this.timeoutCache = null;
			}
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0003B399 File Offset: 0x00039599
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LazyLookupTimeoutCache<K, T>>(this);
		}

		// Token: 0x04001E9A RID: 7834
		private TimeoutCache<K, T> timeoutCache;

		// Token: 0x04001E9B RID: 7835
		private readonly TimeoutType timeoutType;

		// Token: 0x04001E9C RID: 7836
		private readonly TimeSpan slidingLiveTime;

		// Token: 0x04001E9D RID: 7837
		private readonly TimeSpan absoluteLiveTime;

		// Token: 0x04001E9E RID: 7838
		private RemoveItemDelegate<K, T> handleRemoveDelegate;
	}
}
