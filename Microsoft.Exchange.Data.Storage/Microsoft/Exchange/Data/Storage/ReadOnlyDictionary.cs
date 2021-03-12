using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AFB RID: 2811
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x0600660D RID: 26125 RVA: 0x001B13FF File Offset: 0x001AF5FF
		public ReadOnlyDictionary()
		{
			this.wrappedDictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x001B1412 File Offset: 0x001AF612
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.wrappedDictionary = dictionary;
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x0600660F RID: 26127 RVA: 0x001B142F File Offset: 0x001AF62F
		public IDictionary<TKey, TValue> WrappedDictionary
		{
			get
			{
				return this.wrappedDictionary;
			}
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x06006610 RID: 26128 RVA: 0x001B1437 File Offset: 0x001AF637
		public int Count
		{
			get
			{
				return this.wrappedDictionary.Count;
			}
		}

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x06006611 RID: 26129 RVA: 0x001B1444 File Offset: 0x001AF644
		public bool IsReadOnly
		{
			get
			{
				return this.wrappedDictionary.IsReadOnly;
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x06006612 RID: 26130 RVA: 0x001B1451 File Offset: 0x001AF651
		public ICollection<TKey> Keys
		{
			get
			{
				return this.wrappedDictionary.Keys;
			}
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x06006613 RID: 26131 RVA: 0x001B145E File Offset: 0x001AF65E
		public ICollection<TValue> Values
		{
			get
			{
				return this.wrappedDictionary.Values;
			}
		}

		// Token: 0x06006614 RID: 26132 RVA: 0x001B146B File Offset: 0x001AF66B
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.wrappedDictionary.GetEnumerator();
		}

		// Token: 0x06006615 RID: 26133 RVA: 0x001B1478 File Offset: 0x001AF678
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006616 RID: 26134 RVA: 0x001B1480 File Offset: 0x001AF680
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			throw this.CreateReadOnlyException(item);
		}

		// Token: 0x06006617 RID: 26135 RVA: 0x001B1489 File Offset: 0x001AF689
		public void Clear()
		{
			throw this.CreateReadOnlyException();
		}

		// Token: 0x06006618 RID: 26136 RVA: 0x001B1491 File Offset: 0x001AF691
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.wrappedDictionary.Contains(item);
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x001B149F File Offset: 0x001AF69F
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.wrappedDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600661A RID: 26138 RVA: 0x001B14AE File Offset: 0x001AF6AE
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw this.CreateReadOnlyException(item);
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x001B14B7 File Offset: 0x001AF6B7
		public bool ContainsKey(TKey key)
		{
			return this.wrappedDictionary.ContainsKey(key);
		}

		// Token: 0x0600661C RID: 26140 RVA: 0x001B14C5 File Offset: 0x001AF6C5
		public void Add(TKey key, TValue value)
		{
			throw this.CreateReadOnlyException(new KeyValuePair<TKey, TValue>(key, value));
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x001B14D4 File Offset: 0x001AF6D4
		public bool Remove(TKey key)
		{
			throw this.CreateReadOnlyException(key);
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x001B14DD File Offset: 0x001AF6DD
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.wrappedDictionary.TryGetValue(key, out value);
		}

		// Token: 0x17001C2C RID: 7212
		public TValue this[TKey key]
		{
			get
			{
				return this.wrappedDictionary[key];
			}
			set
			{
				throw this.CreateReadOnlyException(new KeyValuePair<TKey, TValue>(key, value));
			}
		}

		// Token: 0x06006621 RID: 26145 RVA: 0x001B1509 File Offset: 0x001AF709
		private Exception CreateReadOnlyException()
		{
			throw new InvalidOperationException("Modification attempt on a read-only dictionary");
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x001B1515 File Offset: 0x001AF715
		private Exception CreateReadOnlyException(TKey key)
		{
			throw new InvalidOperationException(string.Format("Modification attempt on a read-only dictionary for key: {0},", key));
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x001B152C File Offset: 0x001AF72C
		private Exception CreateReadOnlyException(KeyValuePair<TKey, TValue> item)
		{
			throw new InvalidOperationException(string.Format("Modification attempt on a read-only dictionary for item: {0}", item));
		}

		// Token: 0x04003A06 RID: 14854
		private readonly IDictionary<TKey, TValue> wrappedDictionary;
	}
}
