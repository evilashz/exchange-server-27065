using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000240 RID: 576
	internal abstract class BaseWebCache<K, T>
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x0004B220 File Offset: 0x00049420
		public BaseWebCache(string keyPrefix, SlidingOrAbsoluteTimeout slidingOrAbsoluteTimeout, int timeoutInMinutes) : this(keyPrefix, slidingOrAbsoluteTimeout, timeoutInMinutes, false)
		{
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0004B22C File Offset: 0x0004942C
		public BaseWebCache(string keyPrefix, SlidingOrAbsoluteTimeout slidingOrAbsoluteTimeout, int timeoutInMinutes, bool requireRemoveCallback)
		{
			this.keyPrefix = keyPrefix;
			this.slidingOrAbsoluteTimeout = slidingOrAbsoluteTimeout;
			this.timeoutInMinutes = timeoutInMinutes;
			this.requireRemoveCallback = requireRemoveCallback;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0004B251 File Offset: 0x00049451
		protected virtual bool ValidateAddition(K key, T value)
		{
			return true;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0004B254 File Offset: 0x00049454
		protected virtual string KeyToString(K key)
		{
			return key.ToString();
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0004B263 File Offset: 0x00049463
		protected virtual string BuildKey(K key)
		{
			return this.keyPrefix + this.KeyToString(key);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0004B277 File Offset: 0x00049477
		public virtual bool Add(K key, T value)
		{
			return this.AddInternal(key, value, false);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0004B282 File Offset: 0x00049482
		public virtual bool ForceAdd(K key, T value)
		{
			return this.AddInternal(key, value, true);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0004B290 File Offset: 0x00049490
		private bool AddInternal(K key, T value, bool replace)
		{
			if (!this.ValidateAddition(key, value))
			{
				return false;
			}
			DateTime absoluteExpiration;
			TimeSpan slidingExpiration;
			if (this.slidingOrAbsoluteTimeout == SlidingOrAbsoluteTimeout.Absolute)
			{
				absoluteExpiration = ExDateTime.Now.AddMinutes((double)this.timeoutInMinutes).UniversalTime;
				slidingExpiration = Cache.NoSlidingExpiration;
			}
			else
			{
				absoluteExpiration = Cache.NoAbsoluteExpiration;
				slidingExpiration = TimeSpan.FromMinutes((double)this.timeoutInMinutes);
			}
			string key2 = this.BuildKey(key);
			if (!this.Contains(key))
			{
				HttpRuntime.Cache.Add(key2, value, null, absoluteExpiration, slidingExpiration, CacheItemPriority.Low, this.requireRemoveCallback ? new CacheItemRemovedCallback(this.HandleRemove) : null);
				return true;
			}
			if (replace)
			{
				HttpRuntime.Cache.Insert(key2, value, null, absoluteExpiration, slidingExpiration, CacheItemPriority.Low, this.requireRemoveCallback ? new CacheItemRemovedCallback(this.HandleRemove) : null);
				return true;
			}
			return false;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0004B35F File Offset: 0x0004955F
		public virtual T Get(K key)
		{
			return (T)((object)HttpRuntime.Cache.Get(this.BuildKey(key)));
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0004B377 File Offset: 0x00049577
		protected virtual void HandleRemove(string key, object value, CacheItemRemovedReason reason)
		{
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0004B379 File Offset: 0x00049579
		public virtual bool Contains(K key)
		{
			return HttpRuntime.Cache.Get(this.BuildKey(key)) != null;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0004B394 File Offset: 0x00049594
		internal virtual void ClearCache()
		{
			List<string> list = new List<string>();
			foreach (object obj in HttpRuntime.Cache)
			{
				string text = ((DictionaryEntry)obj).Key as string;
				if (text != null && text.StartsWith(this.keyPrefix))
				{
					list.Add(text);
				}
			}
			foreach (string key in list)
			{
				HttpRuntime.Cache.Remove(key);
			}
		}

		// Token: 0x04000BA0 RID: 2976
		private string keyPrefix;

		// Token: 0x04000BA1 RID: 2977
		private SlidingOrAbsoluteTimeout slidingOrAbsoluteTimeout;

		// Token: 0x04000BA2 RID: 2978
		private int timeoutInMinutes;

		// Token: 0x04000BA3 RID: 2979
		private bool requireRemoveCallback;
	}
}
