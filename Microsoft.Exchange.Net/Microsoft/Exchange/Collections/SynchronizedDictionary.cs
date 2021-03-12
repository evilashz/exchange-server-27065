using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200069A RID: 1690
	internal sealed class SynchronizedDictionary<K, V> : IDictionary<K, V>, ICollection<KeyValuePair<K, V>>, IEnumerable<KeyValuePair<K, V>>, IEnumerable
	{
		// Token: 0x06001EE5 RID: 7909 RVA: 0x0003A1EE File Offset: 0x000383EE
		public SynchronizedDictionary() : this(0, null)
		{
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0003A1F8 File Offset: 0x000383F8
		public SynchronizedDictionary(int capacity) : this(capacity, null)
		{
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0003A202 File Offset: 0x00038402
		public SynchronizedDictionary(IEqualityComparer<K> comparer) : this(0, comparer)
		{
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0003A20C File Offset: 0x0003840C
		public SynchronizedDictionary(int capacity, IEqualityComparer<K> comparer)
		{
			this.dictionary = new Dictionary<K, V>(capacity, comparer);
			this.readerWriterLock = new FastReaderWriterLock();
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0003A22C File Offset: 0x0003842C
		public int Count
		{
			get
			{
				int count;
				try
				{
					this.readerWriterLock.AcquireReaderLock(-1);
					count = this.dictionary.Count;
				}
				finally
				{
					this.readerWriterLock.ReleaseReaderLock();
				}
				return count;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0003A270 File Offset: 0x00038470
		public ICollection<K> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0003A27D File Offset: 0x0003847D
		public ICollection<V> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x1700082D RID: 2093
		public V this[K key]
		{
			get
			{
				V result;
				try
				{
					this.readerWriterLock.AcquireReaderLock(-1);
					result = this.dictionary[key];
				}
				finally
				{
					this.readerWriterLock.ReleaseReaderLock();
				}
				return result;
			}
			set
			{
				try
				{
					this.readerWriterLock.AcquireWriterLock(-1);
					this.dictionary[key] = value;
				}
				finally
				{
					this.readerWriterLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0003A318 File Offset: 0x00038518
		public void Add(K key, V value)
		{
			try
			{
				this.readerWriterLock.AcquireWriterLock(-1);
				this.dictionary.Add(key, value);
			}
			finally
			{
				this.readerWriterLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0003A35C File Offset: 0x0003855C
		public V AddIfNotExists(K key, SynchronizedDictionary<K, V>.CreationalMethod creationalMethod)
		{
			V v;
			if (this.TryGetValue(key, out v))
			{
				return v;
			}
			V result;
			try
			{
				this.readerWriterLock.AcquireWriterLock(-1);
				if (this.dictionary.TryGetValue(key, out v))
				{
					result = v;
				}
				else
				{
					v = creationalMethod(key);
					this.dictionary[key] = v;
					result = v;
				}
			}
			finally
			{
				this.readerWriterLock.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0003A3CC File Offset: 0x000385CC
		public void Clear()
		{
			try
			{
				this.readerWriterLock.AcquireWriterLock(-1);
				this.dictionary.Clear();
			}
			finally
			{
				this.readerWriterLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0003A410 File Offset: 0x00038610
		public bool ContainsKey(K key)
		{
			bool result;
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				result = this.dictionary.ContainsKey(key);
			}
			finally
			{
				this.readerWriterLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0003A458 File Offset: 0x00038658
		public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0003A46C File Offset: 0x0003866C
		public bool Remove(K key)
		{
			bool result;
			try
			{
				this.readerWriterLock.AcquireWriterLock(-1);
				result = this.dictionary.Remove(key);
			}
			finally
			{
				this.readerWriterLock.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0003A4B4 File Offset: 0x000386B4
		public bool TryGetValue(K key, out V value)
		{
			bool result;
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				result = this.dictionary.TryGetValue(key, out value);
			}
			finally
			{
				this.readerWriterLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0003A4FC File Offset: 0x000386FC
		public void ForEach(Predicate<V> predicate, Action<K, V> action)
		{
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				foreach (KeyValuePair<K, V> keyValuePair in this.dictionary)
				{
					if (predicate == null || predicate(keyValuePair.Value))
					{
						action(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			finally
			{
				this.readerWriterLock.ReleaseReaderLock();
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0003A594 File Offset: 0x00038794
		public void ForKey(K key, Action<K, V> action)
		{
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				V arg;
				if (this.dictionary.TryGetValue(key, out arg))
				{
					action(key, arg);
				}
			}
			finally
			{
				this.readerWriterLock.ReleaseReaderLock();
			}
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0003A5E4 File Offset: 0x000387E4
		public void ForEachKey(IEnumerable<K> keys, V defaultValue, Action<K, V> action)
		{
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				foreach (K k in keys)
				{
					V arg;
					if (!this.dictionary.TryGetValue(k, out arg))
					{
						arg = defaultValue;
					}
					action(k, arg);
				}
			}
			finally
			{
				this.readerWriterLock.ReleaseReaderLock();
			}
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0003A680 File Offset: 0x00038880
		public void RemoveAll(Predicate<V> predicate)
		{
			List<K> keysToRemove = new List<K>(this.Count);
			this.ForEach(predicate, delegate(K key, V value)
			{
				keysToRemove.Add(key);
			});
			if (keysToRemove.Count != 0)
			{
				try
				{
					this.readerWriterLock.AcquireWriterLock(-1);
					foreach (K key2 in keysToRemove)
					{
						V obj;
						if (this.dictionary.TryGetValue(key2, out obj) && (predicate == null || predicate(obj)))
						{
							this.dictionary.Remove(key2);
						}
					}
				}
				finally
				{
					this.readerWriterLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0003A754 File Offset: 0x00038954
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<K, V> item)
		{
			try
			{
				this.readerWriterLock.AcquireWriterLock(-1);
				((ICollection<KeyValuePair<!0, !1>>)this.dictionary).Add(item);
			}
			finally
			{
				this.readerWriterLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0003A798 File Offset: 0x00038998
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<K, V> item)
		{
			bool result;
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				result = ((ICollection<KeyValuePair<!0, !1>>)this.dictionary).Contains(item);
			}
			finally
			{
				this.readerWriterLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0003A7E0 File Offset: 0x000389E0
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
		{
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				((ICollection<KeyValuePair<!0, !1>>)this.dictionary).CopyTo(array, arrayIndex);
			}
			finally
			{
				this.readerWriterLock.ReleaseReaderLock();
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0003A824 File Offset: 0x00038A24
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<K, V>>)this.dictionary).IsReadOnly;
			}
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0003A834 File Offset: 0x00038A34
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<K, V> item)
		{
			bool result;
			try
			{
				this.readerWriterLock.AcquireWriterLock(-1);
				result = ((ICollection<KeyValuePair<!0, !1>>)this.dictionary).Remove(item);
			}
			finally
			{
				this.readerWriterLock.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0003A87C File Offset: 0x00038A7C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x04001E7D RID: 7805
		private Dictionary<K, V> dictionary;

		// Token: 0x04001E7E RID: 7806
		private FastReaderWriterLock readerWriterLock;

		// Token: 0x0200069B RID: 1691
		// (Invoke) Token: 0x06001F00 RID: 7936
		internal delegate V CreationalMethod(K key);
	}
}
