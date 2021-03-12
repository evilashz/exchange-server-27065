using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200002D RID: 45
	internal class AutoReadThroughCache<TKey, TValue>
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x0000508C File Offset: 0x0000328C
		public AutoReadThroughCache(Func<TKey, TValue> retrieveFunction)
		{
			if (retrieveFunction == null)
			{
				throw new ArgumentNullException();
			}
			this.retrieveFunction = retrieveFunction;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000050B0 File Offset: 0x000032B0
		public TValue Get(TKey key)
		{
			Dictionary<TKey, TValue> dictionary = this.cache;
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				tvalue = this.retrieveFunction(key);
				this.cache = new Dictionary<TKey, TValue>(dictionary)
				{
					{
						key,
						tvalue
					}
				};
			}
			return tvalue;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000050F4 File Offset: 0x000032F4
		public void ForEach(Action<TKey, TValue> operation)
		{
			Dictionary<TKey, TValue> dictionary = this.cache;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				operation(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x040000A5 RID: 165
		private readonly Func<TKey, TValue> retrieveFunction;

		// Token: 0x040000A6 RID: 166
		private Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
	}
}
