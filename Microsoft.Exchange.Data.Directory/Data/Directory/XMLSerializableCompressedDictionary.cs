using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A37 RID: 2615
	[Serializable]
	public class XMLSerializableCompressedDictionary<TKey, TValue> : XMLSerializableDictionary<TKey, TValue> where TValue : class
	{
		// Token: 0x0600781B RID: 30747 RVA: 0x0018B9CD File Offset: 0x00189BCD
		protected override XMLSerializableDictionaryProxy<Dictionary<TKey, TValue>, TKey, TValue>.InternalKeyValuePair CreateKeyValuePair(TKey key, TValue value)
		{
			return new XMLSerializableDictionaryProxy<Dictionary<TKey, TValue>, TKey, TValue>.InternalKeyValuePair(key, value, 1024);
		}
	}
}
