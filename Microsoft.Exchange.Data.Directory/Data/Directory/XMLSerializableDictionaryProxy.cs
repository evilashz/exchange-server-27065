using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000677 RID: 1655
	[Serializable]
	public class XMLSerializableDictionaryProxy<T, TKey, TValue> : XMLSerializableBase where T : IDictionary<TKey, TValue>, new() where TValue : class
	{
		// Token: 0x06004D34 RID: 19764 RVA: 0x0011D208 File Offset: 0x0011B408
		public XMLSerializableDictionaryProxy()
		{
			this.Dictionary = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0011D241 File Offset: 0x0011B441
		public XMLSerializableDictionaryProxy(T dictionary)
		{
			this.Dictionary = dictionary;
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x0011D250 File Offset: 0x0011B450
		public static implicit operator T(XMLSerializableDictionaryProxy<T, TKey, TValue> dictionaryProxy)
		{
			return dictionaryProxy.Dictionary;
		}

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06004D37 RID: 19767 RVA: 0x0011D258 File Offset: 0x0011B458
		// (set) Token: 0x06004D38 RID: 19768 RVA: 0x0011D260 File Offset: 0x0011B460
		[XmlIgnore]
		internal T Dictionary { get; private set; }

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x06004D39 RID: 19769 RVA: 0x0011D26C File Offset: 0x0011B46C
		[XmlIgnore]
		internal int Count
		{
			get
			{
				T dictionary = this.Dictionary;
				return dictionary.Count;
			}
		}

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x06004D3A RID: 19770 RVA: 0x0011D290 File Offset: 0x0011B490
		[XmlIgnore]
		internal bool IsReadOnly
		{
			get
			{
				T dictionary = this.Dictionary;
				return dictionary.IsReadOnly;
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x06004D3B RID: 19771 RVA: 0x0011D2B4 File Offset: 0x0011B4B4
		[XmlIgnore]
		internal ICollection<TKey> Keys
		{
			get
			{
				T dictionary = this.Dictionary;
				return dictionary.Keys;
			}
		}

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x0011D2D8 File Offset: 0x0011B4D8
		[XmlIgnore]
		internal ICollection<TValue> Values
		{
			get
			{
				T dictionary = this.Dictionary;
				return dictionary.Values;
			}
		}

		// Token: 0x17001969 RID: 6505
		[XmlIgnore]
		internal TValue this[TKey key]
		{
			get
			{
				T dictionary = this.Dictionary;
				return dictionary[key];
			}
			set
			{
				T dictionary = this.Dictionary;
				dictionary[key] = value;
			}
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06004D3F RID: 19775 RVA: 0x0011D344 File Offset: 0x0011B544
		// (set) Token: 0x06004D40 RID: 19776 RVA: 0x0011D3B4 File Offset: 0x0011B5B4
		[XmlArray(ElementName = "Pairs")]
		[XmlArrayItem(ElementName = "Pair")]
		public XMLSerializableDictionaryProxy<T, TKey, TValue>.InternalKeyValuePair[] Pairs
		{
			get
			{
				List<XMLSerializableDictionaryProxy<T, TKey, TValue>.InternalKeyValuePair> list = new List<XMLSerializableDictionaryProxy<T, TKey, TValue>.InternalKeyValuePair>(this.Count);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					list.Add(this.CreateKeyValuePair(keyValuePair.Key, keyValuePair.Value));
				}
				return list.ToArray();
			}
			set
			{
				this.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						XMLSerializableDictionaryProxy<T, TKey, TValue>.InternalKeyValuePair internalKeyValuePair = value[i];
						this.Add(internalKeyValuePair.Key, internalKeyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x0011D3F0 File Offset: 0x0011B5F0
		protected virtual XMLSerializableDictionaryProxy<T, TKey, TValue>.InternalKeyValuePair CreateKeyValuePair(TKey key, TValue value)
		{
			return new XMLSerializableDictionaryProxy<T, TKey, TValue>.InternalKeyValuePair(key, value, int.MaxValue);
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x0011D400 File Offset: 0x0011B600
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			T dictionary = this.Dictionary;
			dictionary.Add(item);
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x0011D424 File Offset: 0x0011B624
		public void Add(TKey key, TValue value)
		{
			T dictionary = this.Dictionary;
			dictionary.Add(key, value);
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0011D448 File Offset: 0x0011B648
		public void Clear()
		{
			T dictionary = this.Dictionary;
			dictionary.Clear();
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0011D46C File Offset: 0x0011B66C
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			T dictionary = this.Dictionary;
			return dictionary.Contains(item);
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x0011D490 File Offset: 0x0011B690
		public bool ContainsKey(TKey key)
		{
			T dictionary = this.Dictionary;
			return dictionary.ContainsKey(key);
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x0011D4B4 File Offset: 0x0011B6B4
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			T dictionary = this.Dictionary;
			dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0011D4D8 File Offset: 0x0011B6D8
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			T dictionary = this.Dictionary;
			return dictionary.GetEnumerator();
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0011D4FC File Offset: 0x0011B6FC
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			T dictionary = this.Dictionary;
			return dictionary.Remove(item);
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x0011D520 File Offset: 0x0011B720
		public bool Remove(TKey key)
		{
			T dictionary = this.Dictionary;
			return dictionary.Remove(key);
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x0011D544 File Offset: 0x0011B744
		public bool TryGetValue(TKey key, out TValue value)
		{
			T dictionary = this.Dictionary;
			return dictionary.TryGetValue(key, out value);
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x0011D568 File Offset: 0x0011B768
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				stringBuilder.AppendFormat("{0}{1}={2}", (stringBuilder.Length > 0) ? "," : "", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x02000678 RID: 1656
		[Serializable]
		public class InternalKeyValuePair : XMLSerializableBase
		{
			// Token: 0x06004D4D RID: 19789 RVA: 0x0011D5F0 File Offset: 0x0011B7F0
			public InternalKeyValuePair()
			{
			}

			// Token: 0x06004D4E RID: 19790 RVA: 0x0011D5F8 File Offset: 0x0011B7F8
			public InternalKeyValuePair(TKey key, TValue value, int compressionThreshold = 2147483647)
			{
				this.compressionThreshold = compressionThreshold;
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x1700196B RID: 6507
			// (get) Token: 0x06004D4F RID: 19791 RVA: 0x0011D615 File Offset: 0x0011B815
			// (set) Token: 0x06004D50 RID: 19792 RVA: 0x0011D61D File Offset: 0x0011B81D
			[XmlElement("K")]
			public TKey Key { get; set; }

			// Token: 0x1700196C RID: 6508
			// (get) Token: 0x06004D51 RID: 19793 RVA: 0x0011D626 File Offset: 0x0011B826
			// (set) Token: 0x06004D52 RID: 19794 RVA: 0x0011D62E File Offset: 0x0011B82E
			[XmlElement("V")]
			public virtual TValue UncompressedValue { get; set; }

			// Token: 0x1700196D RID: 6509
			// (get) Token: 0x06004D53 RID: 19795 RVA: 0x0011D637 File Offset: 0x0011B837
			// (set) Token: 0x06004D54 RID: 19796 RVA: 0x0011D63F File Offset: 0x0011B83F
			[XmlElement("CV")]
			public XMLSerializableCompressed<TValue> CompressedValue { get; set; }

			// Token: 0x1700196E RID: 6510
			// (get) Token: 0x06004D55 RID: 19797 RVA: 0x0011D648 File Offset: 0x0011B848
			// (set) Token: 0x06004D56 RID: 19798 RVA: 0x0011D664 File Offset: 0x0011B864
			[XmlIgnore]
			public TValue Value
			{
				get
				{
					if (this.CompressedValue != null)
					{
						return this.CompressedValue.Value;
					}
					return this.UncompressedValue;
				}
				set
				{
					if (value == null)
					{
						this.UncompressedValue = default(TValue);
						this.CompressedValue = null;
						return;
					}
					string text = value as string;
					if (text != null && text.Length > this.compressionThreshold)
					{
						if (this.CompressedValue == null)
						{
							this.CompressedValue = new XMLSerializableCompressed<TValue>();
						}
						this.CompressedValue.Value = value;
						this.UncompressedValue = default(TValue);
						return;
					}
					this.UncompressedValue = value;
					this.CompressedValue = null;
				}
			}

			// Token: 0x06004D57 RID: 19799 RVA: 0x0011D6EA File Offset: 0x0011B8EA
			public override string ToString()
			{
				return string.Format("{0}={1}", this.Key, this.Value);
			}

			// Token: 0x0400349E RID: 13470
			private readonly int compressionThreshold;
		}
	}
}
