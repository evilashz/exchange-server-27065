using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000305 RID: 773
	internal class WSAdditionalRecords<K, T> where K : class where T : class
	{
		// Token: 0x060016EF RID: 5871 RVA: 0x0006A549 File Offset: 0x00068749
		internal WSAdditionalRecords(QueryMethod<K, T> queryMethod)
		{
			this.queryMethod = queryMethod;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0006A558 File Offset: 0x00068758
		internal T FindAndCache(K key, bool bypassCache)
		{
			T t = default(T);
			if (this.recordCache != null && this.recordCache.TryGetValue(key.ToString(), out t) && !bypassCache)
			{
				return t;
			}
			if (this.recordCache == null)
			{
				this.recordCache = new Dictionary<string, T>(20, StringComparer.OrdinalIgnoreCase);
			}
			KeyValuePair<K, T>[] array;
			t = this.queryMethod(key, t, out array);
			if (this.recordCache.Count + array.Length > 32)
			{
				this.recordCache.Clear();
			}
			this.recordCache[key.ToString()] = t;
			foreach (KeyValuePair<K, T> keyValuePair in array)
			{
				Dictionary<string, T> dictionary = this.recordCache;
				K key2 = keyValuePair.Key;
				dictionary[key2.ToString()] = keyValuePair.Value;
			}
			return t;
		}

		// Token: 0x04000EAC RID: 3756
		private const int MaxSize = 32;

		// Token: 0x04000EAD RID: 3757
		private QueryMethod<K, T> queryMethod;

		// Token: 0x04000EAE RID: 3758
		private Dictionary<string, T> recordCache;
	}
}
