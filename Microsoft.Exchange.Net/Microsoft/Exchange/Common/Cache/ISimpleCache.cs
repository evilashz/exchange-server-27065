using System;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000685 RID: 1669
	internal interface ISimpleCache<K, V>
	{
		// Token: 0x06001E39 RID: 7737
		bool TryGetValue(K key, out V value);

		// Token: 0x06001E3A RID: 7738
		bool TryAddValue(K key, V value);
	}
}
