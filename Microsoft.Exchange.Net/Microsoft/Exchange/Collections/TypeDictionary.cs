using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x020006A7 RID: 1703
	[ComVisible(false)]
	internal class TypeDictionary<V>
	{
		// Token: 0x06001F7E RID: 8062 RVA: 0x0003B7A4 File Offset: 0x000399A4
		internal TypeDictionary(IList<V> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this.map = new Dictionary<Type, V>(list.Count);
			foreach (V value in list)
			{
				Type type = value.GetType();
				if (this.map.ContainsKey(type))
				{
					throw new InvalidOperationException("Elements in list with duplicated types are not supported by TypeDictionary.");
				}
				this.map[type] = value;
			}
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0003B840 File Offset: 0x00039A40
		internal T Lookup<T>() where T : V
		{
			V v;
			if (this.map.TryGetValue(typeof(T), out v))
			{
				return (T)((object)v);
			}
			return default(T);
		}

		// Token: 0x04001EA6 RID: 7846
		private Dictionary<Type, V> map;
	}
}
