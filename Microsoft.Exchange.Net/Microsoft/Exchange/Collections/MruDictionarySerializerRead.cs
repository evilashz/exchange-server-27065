using System;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000693 RID: 1683
	// (Invoke) Token: 0x06001E98 RID: 7832
	internal delegate bool MruDictionarySerializerRead<TKey, TValue>(string[] values, out TKey key, out TValue value);
}
