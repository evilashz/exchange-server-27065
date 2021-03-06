using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x02000023 RID: 35
	public class OabLruCache<TKey, TValue> : LRUCache<TKey, TValue> where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000070F4 File Offset: 0x000052F4
		public OabLruCache(int capacity, Func<TKey, TValue> loadItem, Func<TValue, IEnumerable<TKey>> loadExtraKeys, Predicate<TValue> forceReload) : base(capacity, loadItem, null, null, null, null, null, null)
		{
			this.loadExtraKeys = (loadExtraKeys ?? OabLruCache<TKey, TValue>.LoadExtraKeysDefault);
			this.forceReload = (forceReload ?? OabLruCache<TKey, TValue>.forceReloadDefault);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007138 File Offset: 0x00005338
		protected override bool TryLoadFromCache(TKey key, out TValue value)
		{
			return base.TryLoadFromCache(key, out value) && !this.forceReload(value);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000715C File Offset: 0x0000535C
		protected override TValue AddNewItem(TKey key, TValue value, ref bool elementEvicted)
		{
			TValue tvalue = base.AddNewItem(key, value, ref elementEvicted);
			foreach (TKey key2 in this.loadExtraKeys(tvalue))
			{
				base.AddNewItem(key2, tvalue, ref elementEvicted);
			}
			return tvalue;
		}

		// Token: 0x04000140 RID: 320
		private static readonly IEnumerable<TKey> emptyList = new List<TKey>();

		// Token: 0x04000141 RID: 321
		private static readonly Func<TValue, IEnumerable<TKey>> LoadExtraKeysDefault = (TValue value) => OabLruCache<TKey, TValue>.emptyList;

		// Token: 0x04000142 RID: 322
		private static readonly Predicate<TValue> forceReloadDefault = (TValue value) => false;

		// Token: 0x04000143 RID: 323
		private readonly Func<TValue, IEnumerable<TKey>> loadExtraKeys;

		// Token: 0x04000144 RID: 324
		private readonly Predicate<TValue> forceReload;
	}
}
