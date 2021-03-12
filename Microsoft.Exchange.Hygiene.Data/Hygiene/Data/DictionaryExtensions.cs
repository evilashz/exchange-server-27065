using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000084 RID: 132
	internal static class DictionaryExtensions
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x000105AC File Offset: 0x0000E7AC
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			return dictionary.GetOrAdd(key, () => value);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000105DC File Offset: 0x0000E7DC
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> objectBuilder)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				tvalue = objectBuilder();
				dictionary[key] = tvalue;
			}
			return tvalue;
		}
	}
}
