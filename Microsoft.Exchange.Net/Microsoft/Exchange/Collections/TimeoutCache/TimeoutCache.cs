using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x020006A6 RID: 1702
	internal class TimeoutCache<K, T> : IDisposable
	{
		// Token: 0x06001F68 RID: 8040 RVA: 0x0003B3A4 File Offset: 0x000395A4
		internal TimeoutCache(int numberOfBuckets, int maxSizeForBuckets, bool shouldCallbackOnDispose, PreprocessKeyDelegate<K> handlePreprocessKey, ShouldRemoveDelegate<K, T> shouldRemoveDelegate, HandleBeforeAdd<K, T> handleBeforeAddDelegate)
		{
			if (numberOfBuckets < 1 || numberOfBuckets > 20)
			{
				throw new ArgumentException(string.Format("numberOfBuckets must be between {0}-{1} inclusive.", 1, 20), "numberOfBuckets");
			}
			if (maxSizeForBuckets < 1)
			{
				throw new ArgumentException(string.Format("maxSizeForBuckets must be {0} or greater.", 1));
			}
			this.handlePreprocessKey = handlePreprocessKey;
			this.handleBeforeAddDelegate = handleBeforeAddDelegate;
			this.buckets = new TimeoutCacheBucket<K, T>[numberOfBuckets];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i] = new TimeoutCacheBucket<K, T>(shouldRemoveDelegate, maxSizeForBuckets, shouldCallbackOnDispose);
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0003B43C File Offset: 0x0003963C
		internal TimeoutCache(int numberOfBuckets, int maxSizeForBuckets, bool shouldCallbackOnDispose) : this(numberOfBuckets, maxSizeForBuckets, shouldCallbackOnDispose, null, null, null)
		{
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0003B44A File Offset: 0x0003964A
		private K PreProcessKey(K key)
		{
			if (this.handlePreprocessKey != null)
			{
				return this.handlePreprocessKey(key);
			}
			return key;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0003B464 File Offset: 0x00039664
		internal T Get(K key)
		{
			K key2 = this.PreProcessKey(key);
			return this.GetBucket(key2).Get(key2);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0003B488 File Offset: 0x00039688
		internal bool TryGetValue(K key, out T value)
		{
			K key2 = this.PreProcessKey(key);
			return this.GetBucket(key2).TryGetValue(key2, out value);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0003B4AC File Offset: 0x000396AC
		internal void AddAbsolute(K key, T value, TimeSpan expiration, RemoveItemDelegate<K, T> callback)
		{
			K key2 = this.PreProcessKey(key);
			TimeoutCacheBucket<K, T> bucket = this.GetBucket(key2);
			if (this.BeforeAdd(key2, value, bucket))
			{
				bucket.AddAbsolute(key2, value, expiration, callback);
			}
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0003B4E0 File Offset: 0x000396E0
		internal void AddAbsolute(K key, T value, DateTime absoluteExpiration, RemoveItemDelegate<K, T> callback)
		{
			K key2 = this.PreProcessKey(key);
			TimeoutCacheBucket<K, T> bucket = this.GetBucket(key2);
			if (this.BeforeAdd(key2, value, bucket))
			{
				bucket.AddAbsolute(key2, value, absoluteExpiration, callback);
			}
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0003B514 File Offset: 0x00039714
		internal void AddSliding(K key, T value, TimeSpan slidingExpiration, RemoveItemDelegate<K, T> callback)
		{
			K key2 = this.PreProcessKey(key);
			TimeoutCacheBucket<K, T> bucket = this.GetBucket(key2);
			if (this.BeforeAdd(key2, value, bucket))
			{
				bucket.AddSliding(key2, value, slidingExpiration, callback);
			}
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0003B548 File Offset: 0x00039748
		internal void AddLimitedSliding(K key, T value, TimeSpan slidingExpiration, TimeSpan absoluteExpiration, RemoveItemDelegate<K, T> callback)
		{
			K key2 = this.PreProcessKey(key);
			TimeoutCacheBucket<K, T> bucket = this.GetBucket(key2);
			if (this.BeforeAdd(key2, value, bucket))
			{
				bucket.AddLimitedSliding(key2, value, absoluteExpiration, slidingExpiration, callback);
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0003B580 File Offset: 0x00039780
		internal void InsertAbsolute(K key, T value, TimeSpan expiration, RemoveItemDelegate<K, T> callback)
		{
			K key2 = this.PreProcessKey(key);
			TimeoutCacheBucket<K, T> bucket = this.GetBucket(key2);
			if (this.BeforeAdd(key2, value, bucket))
			{
				bucket.InsertAbsolute(key2, value, expiration, callback);
			}
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0003B5B4 File Offset: 0x000397B4
		internal void InsertAbsolute(K key, T value, DateTime absoluteExpiration, RemoveItemDelegate<K, T> callback)
		{
			K key2 = this.PreProcessKey(key);
			TimeoutCacheBucket<K, T> bucket = this.GetBucket(key2);
			if (this.BeforeAdd(key2, value, bucket))
			{
				bucket.InsertAbsolute(key2, value, absoluteExpiration, callback);
			}
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0003B5E8 File Offset: 0x000397E8
		internal void InsertSliding(K key, T value, TimeSpan slidingExpiration, RemoveItemDelegate<K, T> callback)
		{
			K key2 = this.PreProcessKey(key);
			TimeoutCacheBucket<K, T> bucket = this.GetBucket(key2);
			if (this.BeforeAdd(key2, value, bucket))
			{
				bucket.InsertSliding(key2, value, slidingExpiration, callback);
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0003B61C File Offset: 0x0003981C
		internal void InsertLimitedSliding(K key, T value, TimeSpan slidingExpiration, TimeSpan absoluteExpiration, RemoveItemDelegate<K, T> callback)
		{
			K key2 = this.PreProcessKey(key);
			TimeoutCacheBucket<K, T> bucket = this.GetBucket(key2);
			if (this.BeforeAdd(key2, value, bucket))
			{
				bucket.InsertLimitedSliding(key2, value, absoluteExpiration, slidingExpiration, callback);
			}
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0003B654 File Offset: 0x00039854
		internal T Remove(K key)
		{
			K key2 = this.PreProcessKey(key);
			return this.GetBucket(key2).Remove(key2);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0003B678 File Offset: 0x00039878
		internal bool Contains(K key)
		{
			K key2 = this.PreProcessKey(key);
			return this.GetBucket(key2).Contains(key2);
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x0003B69C File Offset: 0x0003989C
		internal int Count
		{
			get
			{
				int num = 0;
				foreach (TimeoutCacheBucket<K, T> timeoutCacheBucket in this.buckets)
				{
					num += timeoutCacheBucket.Count;
				}
				return num;
			}
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0003B6CE File Offset: 0x000398CE
		protected virtual bool BeforeAdd(K key, T value, TimeoutCacheBucket<K, T> bucket)
		{
			return this.handleBeforeAddDelegate == null || this.handleBeforeAddDelegate(key, value, bucket);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0003B6E8 File Offset: 0x000398E8
		private TimeoutCacheBucket<K, T> GetBucket(K key)
		{
			return this.buckets[this.ComputeIndex(key)];
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0003B6F8 File Offset: 0x000398F8
		private int ComputeIndex(K key)
		{
			return Math.Abs(key.GetHashCode()) % this.buckets.Length;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0003B718 File Offset: 0x00039918
		protected virtual void Dispose(bool isDisposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (this.buckets != null)
			{
				foreach (TimeoutCacheBucket<K, T> timeoutCacheBucket in this.buckets)
				{
					timeoutCacheBucket.Dispose();
				}
			}
			this.disposed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0003B764 File Offset: 0x00039964
		internal void Clear()
		{
			if (this.buckets != null)
			{
				foreach (TimeoutCacheBucket<K, T> timeoutCacheBucket in this.buckets)
				{
					timeoutCacheBucket.Clear();
				}
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0003B798 File Offset: 0x00039998
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04001E9F RID: 7839
		private const int minBuckets = 1;

		// Token: 0x04001EA0 RID: 7840
		private const int maxBuckets = 20;

		// Token: 0x04001EA1 RID: 7841
		private const int minMaxSize = 1;

		// Token: 0x04001EA2 RID: 7842
		private HandleBeforeAdd<K, T> handleBeforeAddDelegate;

		// Token: 0x04001EA3 RID: 7843
		private PreprocessKeyDelegate<K> handlePreprocessKey;

		// Token: 0x04001EA4 RID: 7844
		private TimeoutCacheBucket<K, T>[] buckets;

		// Token: 0x04001EA5 RID: 7845
		private bool disposed;
	}
}
