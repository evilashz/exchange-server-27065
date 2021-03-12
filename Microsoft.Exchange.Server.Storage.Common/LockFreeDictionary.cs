using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200004D RID: 77
	public sealed class LockFreeDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000CEDA File Offset: 0x0000B0DA
		public LockFreeDictionary() : this(null)
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		public LockFreeDictionary(IEnumerable<KeyValuePair<TKey, TValue>> initialValues)
		{
			this.dictionary = new Dictionary<TKey, TValue>();
			if (initialValues != null)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in initialValues)
				{
					this.dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		public TValue this[TKey key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000CF69 File Offset: 0x0000B169
		public ICollection<TKey> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000CF76 File Offset: 0x0000B176
		public ICollection<TValue> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000CF83 File Offset: 0x0000B183
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000CF94 File Offset: 0x0000B194
		public void Add(TKey key, TValue value)
		{
			Dictionary<TKey, TValue> dictionary;
			Dictionary<TKey, TValue> dictionary2;
			do
			{
				dictionary = this.dictionary;
				dictionary2 = new Dictionary<TKey, TValue>(dictionary.Count + 1);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
				{
					dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
				}
				dictionary2[key] = value;
			}
			while (Interlocked.CompareExchange<Dictionary<TKey, TValue>>(ref this.dictionary, dictionary2, dictionary) != dictionary);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D01C File Offset: 0x0000B21C
		public bool TryAdd(TKey key, TValue value)
		{
			if (this.dictionary.ContainsKey(key))
			{
				return false;
			}
			for (;;)
			{
				Dictionary<TKey, TValue> dictionary = this.dictionary;
				Dictionary<TKey, TValue> dictionary2 = new Dictionary<TKey, TValue>(dictionary.Count + 1);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
				{
					dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
				}
				if (dictionary2.ContainsKey(key))
				{
					break;
				}
				dictionary2.Add(key, value);
				if (Interlocked.CompareExchange<Dictionary<TKey, TValue>>(ref this.dictionary, dictionary2, dictionary) == dictionary)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		public bool Remove(TKey key)
		{
			if (this.dictionary.ContainsKey(key))
			{
				Dictionary<TKey, TValue> dictionary;
				Dictionary<TKey, TValue> dictionary2;
				do
				{
					dictionary = this.dictionary;
					dictionary2 = new Dictionary<TKey, TValue>(dictionary.Count);
					bool flag = false;
					foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
					{
						TKey key2 = keyValuePair.Key;
						if (key2.Equals(key))
						{
							flag = true;
						}
						else
						{
							dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				while (Interlocked.CompareExchange<Dictionary<TKey, TValue>>(ref this.dictionary, dictionary2, dictionary) != dictionary);
				return true;
			}
			return false;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000D180 File Offset: 0x0000B380
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000D18F File Offset: 0x0000B38F
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000D19C File Offset: 0x0000B39C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000D19F File Offset: 0x0000B39F
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000D1A6 File Offset: 0x0000B3A6
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000D1AD File Offset: 0x0000B3AD
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).Contains(item);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000D1BB File Offset: 0x0000B3BB
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000D1CA File Offset: 0x0000B3CA
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000D1DE File Offset: 0x0000B3DE
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000D1EB File Offset: 0x0000B3EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.dictionary).GetEnumerator();
		}

		// Token: 0x040004E2 RID: 1250
		private Dictionary<TKey, TValue> dictionary;
	}
}
