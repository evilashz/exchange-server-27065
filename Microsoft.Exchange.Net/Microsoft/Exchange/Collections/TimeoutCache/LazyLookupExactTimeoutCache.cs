using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x020006A2 RID: 1698
	internal abstract class LazyLookupExactTimeoutCache<K, T> : DisposeTrackableBase
	{
		// Token: 0x06001F38 RID: 7992 RVA: 0x0003AEB5 File Offset: 0x000390B5
		protected LazyLookupExactTimeoutCache(int maxCount, bool shouldCallbackOnDispose, TimeSpan absoluteLiveTime, CacheFullBehavior cacheFullBehavior) : this(maxCount, shouldCallbackOnDispose, TimeoutType.Absolute, absoluteLiveTime, absoluteLiveTime, cacheFullBehavior)
		{
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0003AEC4 File Offset: 0x000390C4
		protected LazyLookupExactTimeoutCache(int maxCount, bool shouldCallbackOnDispose, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime, CacheFullBehavior cacheFullBehavior) : this(maxCount, shouldCallbackOnDispose, TimeoutType.Sliding, slidingLiveTime, absoluteLiveTime, cacheFullBehavior)
		{
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0003AED4 File Offset: 0x000390D4
		private LazyLookupExactTimeoutCache(int maxCount, bool shouldCallbackOnDispose, TimeoutType timeoutType, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime, CacheFullBehavior cacheFullBehavior)
		{
			this.timeoutCache = new ExactTimeoutCache<K, T>(new RemoveItemDelegate<K, T>(this.HandleRemove), new ShouldRemoveDelegate<K, T>(this.HandleShouldRemove), new UnhandledExceptionDelegate(this.HandleThreadException), maxCount, shouldCallbackOnDispose, cacheFullBehavior);
			this.timeoutType = timeoutType;
			this.slidingLiveTime = slidingLiveTime;
			this.absoluteLiveTime = absoluteLiveTime;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0003AF34 File Offset: 0x00039134
		internal virtual void Clear()
		{
			base.CheckDisposed();
			this.timeoutCache.Clear();
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0003AF47 File Offset: 0x00039147
		protected virtual void AfterCacheHit(K key, T value)
		{
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0003AF49 File Offset: 0x00039149
		protected virtual void BeforeGet(K key)
		{
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0003AF4B File Offset: 0x0003914B
		protected virtual bool HandleShouldRemove(K key, T value)
		{
			return true;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0003AF4E File Offset: 0x0003914E
		protected virtual void HandleThreadException(Exception e)
		{
			if (!(e is ThreadAbortException) && !(e is AppDomainUnloadedException))
			{
				ExWatson.SendReport(e, ReportOptions.ReportTerminateAfterSend, null);
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0003AF68 File Offset: 0x00039168
		protected virtual void HandleRemove(K key, T value, RemoveReason reason)
		{
			if (reason != RemoveReason.Removed)
			{
				this.CleanupValue(key, value);
			}
		}

		// Token: 0x06001F41 RID: 8001
		protected abstract T CreateOnCacheMiss(K key, ref bool shouldAdd);

		// Token: 0x06001F42 RID: 8002 RVA: 0x0003AF76 File Offset: 0x00039176
		protected virtual void CleanupValue(K key, T value)
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0003AF78 File Offset: 0x00039178
		private bool TryPerformAdd(K key, T value)
		{
			bool result;
			try
			{
				if (this.timeoutType == TimeoutType.Absolute)
				{
					result = this.timeoutCache.TryAddAbsolute(key, value, this.absoluteLiveTime);
				}
				else
				{
					if (this.absoluteLiveTime == TimeSpan.MaxValue)
					{
						this.timeoutCache.TryAddSliding(key, value, this.slidingLiveTime);
					}
					else
					{
						this.timeoutCache.TryAddLimitedSliding(key, value, this.absoluteLiveTime, this.slidingLiveTime);
					}
					result = true;
				}
			}
			catch (DuplicateKeyException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0003B000 File Offset: 0x00039200
		internal List<K> Keys
		{
			get
			{
				base.CheckDisposed();
				return this.timeoutCache.Keys;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0003B013 File Offset: 0x00039213
		internal List<T> Values
		{
			get
			{
				base.CheckDisposed();
				return this.timeoutCache.Values;
			}
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0003B026 File Offset: 0x00039226
		internal T Remove(K key)
		{
			base.CheckDisposed();
			return this.timeoutCache.Remove(key);
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0003B03A File Offset: 0x0003923A
		internal bool Contains(K key)
		{
			base.CheckDisposed();
			return this.timeoutCache.Contains(key);
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x0003B04E File Offset: 0x0003924E
		internal int Count
		{
			get
			{
				base.CheckDisposed();
				return this.timeoutCache.Count;
			}
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0003B064 File Offset: 0x00039264
		internal bool TryAdd(K key, ref T value)
		{
			base.CheckDisposed();
			this.BeforeGet(key);
			T t;
			if (this.timeoutCache.TryGetValue(key, out t))
			{
				this.AfterCacheHit(key, t);
				value = t;
				return false;
			}
			return this.InternalTryAdd(key, ref value);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0003B0A8 File Offset: 0x000392A8
		internal T Get(K key)
		{
			base.CheckDisposed();
			T t = default(T);
			this.BeforeGet(key);
			if (this.timeoutCache.TryGetValue(key, out t))
			{
				this.AfterCacheHit(key, t);
				return t;
			}
			bool flag = true;
			t = this.CreateOnCacheMiss(key, ref flag);
			if (flag)
			{
				this.InternalTryAdd(key, ref t);
			}
			return t;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x0003B100 File Offset: 0x00039300
		private bool InternalTryAdd(K key, ref T value)
		{
			bool flag = false;
			T value2 = default(T);
			while (!this.TryPerformAdd(key, value))
			{
				T t;
				if (this.timeoutCache.TryGetValue(key, out t))
				{
					flag = true;
					value2 = value;
					value = t;
					break;
				}
			}
			if (flag)
			{
				this.CleanupValue(key, value2);
			}
			return !flag;
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0003B155 File Offset: 0x00039355
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.timeoutCache != null)
			{
				this.timeoutCache.Dispose();
				this.timeoutCache = null;
			}
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0003B174 File Offset: 0x00039374
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LazyLookupExactTimeoutCache<K, T>>(this);
		}

		// Token: 0x04001E96 RID: 7830
		private ExactTimeoutCache<K, T> timeoutCache;

		// Token: 0x04001E97 RID: 7831
		private readonly TimeoutType timeoutType;

		// Token: 0x04001E98 RID: 7832
		private readonly TimeSpan slidingLiveTime;

		// Token: 0x04001E99 RID: 7833
		private readonly TimeSpan absoluteLiveTime;
	}
}
