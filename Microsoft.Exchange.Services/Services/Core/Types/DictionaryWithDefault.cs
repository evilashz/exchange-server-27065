using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200074D RID: 1869
	public class DictionaryWithDefault<TKey, TValue>
	{
		// Token: 0x06003811 RID: 14353 RVA: 0x000C6964 File Offset: 0x000C4B64
		public DictionaryWithDefault(TValue defaultValue)
		{
			this.specificValues = new Dictionary<TKey, TValue>();
			this.defaultValue = defaultValue;
		}

		// Token: 0x17000D3C RID: 3388
		public TValue this[TKey key]
		{
			get
			{
				TValue result = default(TValue);
				if (this.specificValues.TryGetValue(key, out result))
				{
					return result;
				}
				return this.defaultValue;
			}
			set
			{
				if (!value.Equals(this.defaultValue))
				{
					this.specificValues[key] = value;
					return;
				}
				if (this.specificValues.ContainsKey(key))
				{
					this.specificValues.Remove(key);
				}
			}
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000C6A00 File Offset: 0x000C4C00
		public TValue GetValue(TKey key)
		{
			return this[key];
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000C6A09 File Offset: 0x000C4C09
		public void Add(TKey key, TValue value)
		{
			if (this.specificValues.ContainsKey(key))
			{
				throw new ArgumentException();
			}
			this[key] = value;
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000C6A27 File Offset: 0x000C4C27
		public TValue DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x04001F25 RID: 7973
		private Dictionary<TKey, TValue> specificValues;

		// Token: 0x04001F26 RID: 7974
		private TValue defaultValue;
	}
}
