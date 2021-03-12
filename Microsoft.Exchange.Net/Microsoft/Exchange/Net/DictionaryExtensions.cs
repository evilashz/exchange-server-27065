using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BE4 RID: 3044
	public static class DictionaryExtensions
	{
		// Token: 0x06004281 RID: 17025 RVA: 0x000B0F7C File Offset: 0x000AF17C
		public static Dictionary<TKey, TValue> ShallowCopy<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			Dictionary<TKey, TValue> dictionary2 = new Dictionary<TKey, TValue>(dictionary.Count, dictionary.Comparer);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary2;
		}
	}
}
