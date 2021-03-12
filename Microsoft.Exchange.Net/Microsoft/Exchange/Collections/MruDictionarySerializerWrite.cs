using System;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000692 RID: 1682
	// (Invoke) Token: 0x06001E94 RID: 7828
	internal delegate bool MruDictionarySerializerWrite<TKey, TValue>(TKey key, TValue value, out string[] values);
}
