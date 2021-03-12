using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000679 RID: 1657
	[Serializable]
	public class XMLSerializableDictionary<TKey, TValue> : XMLSerializableDictionaryProxy<Dictionary<TKey, TValue>, TKey, TValue> where TValue : class
	{
	}
}
