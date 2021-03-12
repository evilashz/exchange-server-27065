using System;
using System.Collections.Concurrent;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200076C RID: 1900
	internal sealed class NameCache
	{
		// Token: 0x06005333 RID: 21299 RVA: 0x001247D4 File Offset: 0x001229D4
		internal object GetCachedValue(string name)
		{
			this.name = name;
			object result;
			if (!NameCache.ht.TryGetValue(name, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x001247FA File Offset: 0x001229FA
		internal void SetCachedValue(object value)
		{
			NameCache.ht[this.name] = value;
		}

		// Token: 0x0400259B RID: 9627
		private static ConcurrentDictionary<string, object> ht = new ConcurrentDictionary<string, object>();

		// Token: 0x0400259C RID: 9628
		private string name;
	}
}
