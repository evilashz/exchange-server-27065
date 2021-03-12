using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000492 RID: 1170
	[DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<!0, !1>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
	{
		// Token: 0x0600391A RID: 14618 RVA: 0x000D94D4 File Offset: 0x000D76D4
		[__DynamicallyInvokable]
		public Dictionary() : this(0, null)
		{
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000D94DE File Offset: 0x000D76DE
		[__DynamicallyInvokable]
		public Dictionary(int capacity) : this(capacity, null)
		{
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000D94E8 File Offset: 0x000D76E8
		[__DynamicallyInvokable]
		public Dictionary(IEqualityComparer<TKey> comparer) : this(0, comparer)
		{
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000D94F2 File Offset: 0x000D76F2
		[__DynamicallyInvokable]
		public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
			this.comparer = (comparer ?? EqualityComparer<TKey>.Default);
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000D9520 File Offset: 0x000D7720
		[__DynamicallyInvokable]
		public Dictionary(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
		{
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000D952C File Offset: 0x000D772C
		[__DynamicallyInvokable]
		public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000D95A0 File Offset: 0x000D77A0
		protected Dictionary(SerializationInfo info, StreamingContext context)
		{
			HashHelpers.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000D95B4 File Offset: 0x000D77B4
		[__DynamicallyInvokable]
		public IEqualityComparer<TKey> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x000D95BC File Offset: 0x000D77BC
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.count - this.freeCount;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000D95CB File Offset: 0x000D77CB
		[__DynamicallyInvokable]
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000D95E7 File Offset: 0x000D77E7
		[__DynamicallyInvokable]
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x000D9603 File Offset: 0x000D7803
		[__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<!0, !1>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003926 RID: 14630 RVA: 0x000D961F File Offset: 0x000D781F
		[__DynamicallyInvokable]
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000D963B File Offset: 0x000D783B
		[__DynamicallyInvokable]
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06003928 RID: 14632 RVA: 0x000D9657 File Offset: 0x000D7857
		[__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<!0, !1>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x170008C0 RID: 2240
		[__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.FindEntry(key);
				if (num >= 0)
				{
					return this.entries[num].value;
				}
				ThrowHelper.ThrowKeyNotFoundException();
				return default(TValue);
			}
			[__DynamicallyInvokable]
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000D96B8 File Offset: 0x000D78B8
		[__DynamicallyInvokable]
		public void Add(TKey key, TValue value)
		{
			this.Insert(key, value, true);
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000D96C3 File Offset: 0x000D78C3
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000D96DC File Offset: 0x000D78DC
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[num].value, keyValuePair.Value);
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000D9724 File Offset: 0x000D7924
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[num].value, keyValuePair.Value))
			{
				this.Remove(keyValuePair.Key);
				return true;
			}
			return false;
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000D9778 File Offset: 0x000D7978
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this.count > 0)
			{
				for (int i = 0; i < this.buckets.Length; i++)
				{
					this.buckets[i] = -1;
				}
				Array.Clear(this.entries, 0, this.count);
				this.freeList = -1;
				this.count = 0;
				this.freeCount = 0;
				this.version++;
			}
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000D97DF File Offset: 0x000D79DF
		[__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			return this.FindEntry(key) >= 0;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000D97F0 File Offset: 0x000D79F0
		[__DynamicallyInvokable]
		public bool ContainsValue(TValue value)
		{
			if (value == null)
			{
				for (int i = 0; i < this.count; i++)
				{
					if (this.entries[i].hashCode >= 0 && this.entries[i].value == null)
					{
						return true;
					}
				}
			}
			else
			{
				EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
				for (int j = 0; j < this.count; j++)
				{
					if (this.entries[j].hashCode >= 0 && @default.Equals(this.entries[j].value, value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000D9890 File Offset: 0x000D7A90
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index < 0 || index > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			int num = this.count;
			Dictionary<TKey, TValue>.Entry[] array2 = this.entries;
			for (int i = 0; i < num; i++)
			{
				if (array2[i].hashCode >= 0)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(array2[i].key, array2[i].value);
				}
			}
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000D991D File Offset: 0x000D7B1D
		[__DynamicallyInvokable]
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000D9926 File Offset: 0x000D7B26
		[__DynamicallyInvokable]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000D9934 File Offset: 0x000D7B34
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
			}
			info.AddValue("Version", this.version);
			info.AddValue("Comparer", HashHelpers.GetEqualityComparerForSerialization(this.comparer), typeof(IEqualityComparer<TKey>));
			info.AddValue("HashSize", (this.buckets == null) ? 0 : this.buckets.Length);
			if (this.buckets != null)
			{
				KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("KeyValuePairs", array, typeof(KeyValuePair<TKey, TValue>[]));
			}
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000D99CC File Offset: 0x000D7BCC
		private int FindEntry(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.buckets != null)
			{
				int num = this.comparer.GetHashCode(key) & int.MaxValue;
				for (int i = this.buckets[num % this.buckets.Length]; i >= 0; i = this.entries[i].next)
				{
					if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000D9A64 File Offset: 0x000D7C64
		private void Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this.buckets = new int[prime];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i] = -1;
			}
			this.entries = new Dictionary<TKey, TValue>.Entry[prime];
			this.freeList = -1;
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000D9AB4 File Offset: 0x000D7CB4
		private void Insert(TKey key, TValue value, bool add)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.buckets == null)
			{
				this.Initialize(0);
			}
			int num = this.comparer.GetHashCode(key) & int.MaxValue;
			int num2 = num % this.buckets.Length;
			int num3 = 0;
			for (int i = this.buckets[num2]; i >= 0; i = this.entries[i].next)
			{
				if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
				{
					if (add)
					{
						ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
					}
					this.entries[i].value = value;
					this.version++;
					return;
				}
				num3++;
			}
			int num4;
			if (this.freeCount > 0)
			{
				num4 = this.freeList;
				this.freeList = this.entries[num4].next;
				this.freeCount--;
			}
			else
			{
				if (this.count == this.entries.Length)
				{
					this.Resize();
					num2 = num % this.buckets.Length;
				}
				num4 = this.count;
				this.count++;
			}
			this.entries[num4].hashCode = num;
			this.entries[num4].next = this.buckets[num2];
			this.entries[num4].key = key;
			this.entries[num4].value = value;
			this.buckets[num2] = num4;
			this.version++;
			if (num3 > 100 && HashHelpers.IsWellKnownEqualityComparer(this.comparer))
			{
				this.comparer = (IEqualityComparer<TKey>)HashHelpers.GetRandomizedEqualityComparer(this.comparer);
				this.Resize(this.entries.Length, true);
			}
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000D9C94 File Offset: 0x000D7E94
		public virtual void OnDeserialization(object sender)
		{
			SerializationInfo serializationInfo;
			HashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				return;
			}
			int @int = serializationInfo.GetInt32("Version");
			int int2 = serializationInfo.GetInt32("HashSize");
			this.comparer = (IEqualityComparer<TKey>)serializationInfo.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			if (int2 != 0)
			{
				this.buckets = new int[int2];
				for (int i = 0; i < this.buckets.Length; i++)
				{
					this.buckets[i] = -1;
				}
				this.entries = new Dictionary<TKey, TValue>.Entry[int2];
				this.freeList = -1;
				KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])serializationInfo.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
				if (array == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j].Key == null)
					{
						ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
					}
					this.Insert(array[j].Key, array[j].Value, true);
				}
			}
			else
			{
				this.buckets = null;
			}
			this.version = @int;
			HashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000D9DC0 File Offset: 0x000D7FC0
		private void Resize()
		{
			this.Resize(HashHelpers.ExpandPrime(this.count), false);
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000D9DD4 File Offset: 0x000D7FD4
		private void Resize(int newSize, bool forceNewHashCodes)
		{
			int[] array = new int[newSize];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			Dictionary<TKey, TValue>.Entry[] array2 = new Dictionary<TKey, TValue>.Entry[newSize];
			Array.Copy(this.entries, 0, array2, 0, this.count);
			if (forceNewHashCodes)
			{
				for (int j = 0; j < this.count; j++)
				{
					if (array2[j].hashCode != -1)
					{
						array2[j].hashCode = (this.comparer.GetHashCode(array2[j].key) & int.MaxValue);
					}
				}
			}
			for (int k = 0; k < this.count; k++)
			{
				if (array2[k].hashCode >= 0)
				{
					int num = array2[k].hashCode % newSize;
					array2[k].next = array[num];
					array[num] = k;
				}
			}
			this.buckets = array;
			this.entries = array2;
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000D9EBC File Offset: 0x000D80BC
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.buckets != null)
			{
				int num = this.comparer.GetHashCode(key) & int.MaxValue;
				int num2 = num % this.buckets.Length;
				int num3 = -1;
				for (int i = this.buckets[num2]; i >= 0; i = this.entries[i].next)
				{
					if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
					{
						if (num3 < 0)
						{
							this.buckets[num2] = this.entries[i].next;
						}
						else
						{
							this.entries[num3].next = this.entries[i].next;
						}
						this.entries[i].hashCode = -1;
						this.entries[i].next = this.freeList;
						this.entries[i].key = default(TKey);
						this.entries[i].value = default(TValue);
						this.freeList = i;
						this.freeCount++;
						this.version++;
						return true;
					}
					num3 = i;
				}
			}
			return false;
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000DA024 File Offset: 0x000D8224
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.FindEntry(key);
			if (num >= 0)
			{
				value = this.entries[num].value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000DA060 File Offset: 0x000D8260
		internal TValue GetValueOrDefault(TKey key)
		{
			int num = this.FindEntry(key);
			if (num >= 0)
			{
				return this.entries[num].value;
			}
			return default(TValue);
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x000DA094 File Offset: 0x000D8294
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000DA097 File Offset: 0x000D8297
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000DA0A4 File Offset: 0x000D82A4
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0 || index > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			if (array is DictionaryEntry[])
			{
				DictionaryEntry[] array3 = array as DictionaryEntry[];
				Dictionary<TKey, TValue>.Entry[] array4 = this.entries;
				for (int i = 0; i < this.count; i++)
				{
					if (array4[i].hashCode >= 0)
					{
						array3[index++] = new DictionaryEntry(array4[i].key, array4[i].value);
					}
				}
				return;
			}
			object[] array5 = array as object[];
			if (array5 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				int num = this.count;
				Dictionary<TKey, TValue>.Entry[] array6 = this.entries;
				for (int j = 0; j < num; j++)
				{
					if (array6[j].hashCode >= 0)
					{
						array5[index++] = new KeyValuePair<TKey, TValue>(array6[j].key, array6[j].value);
					}
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000DA214 File Offset: 0x000D8414
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003943 RID: 14659 RVA: 0x000DA222 File Offset: 0x000D8422
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000DA225 File Offset: 0x000D8425
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x000DA247 File Offset: 0x000D8447
		[__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x000DA24A File Offset: 0x000D844A
		[__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000DA24D File Offset: 0x000D844D
		[__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000DA255 File Offset: 0x000D8455
		[__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x170008C8 RID: 2248
		[__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.FindEntry((TKey)((object)key));
					if (num >= 0)
					{
						return this.entries[num].value;
					}
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
				try
				{
					TKey key2 = (TKey)((object)key);
					try
					{
						this[key2] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
					}
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
				}
			}
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000DA318 File Offset: 0x000D8518
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000DA32C File Offset: 0x000D852C
		[__DynamicallyInvokable]
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
			try
			{
				TKey key2 = (TKey)((object)key);
				try
				{
					this.Add(key2, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
				}
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
			}
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000DA3A4 File Offset: 0x000D85A4
		[__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			return Dictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000DA3BC File Offset: 0x000D85BC
		[__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000DA3CA File Offset: 0x000D85CA
		[__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x04001887 RID: 6279
		private int[] buckets;

		// Token: 0x04001888 RID: 6280
		private Dictionary<TKey, TValue>.Entry[] entries;

		// Token: 0x04001889 RID: 6281
		private int count;

		// Token: 0x0400188A RID: 6282
		private int version;

		// Token: 0x0400188B RID: 6283
		private int freeList;

		// Token: 0x0400188C RID: 6284
		private int freeCount;

		// Token: 0x0400188D RID: 6285
		private IEqualityComparer<TKey> comparer;

		// Token: 0x0400188E RID: 6286
		private Dictionary<TKey, TValue>.KeyCollection keys;

		// Token: 0x0400188F RID: 6287
		private Dictionary<TKey, TValue>.ValueCollection values;

		// Token: 0x04001890 RID: 6288
		private object _syncRoot;

		// Token: 0x04001891 RID: 6289
		private const string VersionName = "Version";

		// Token: 0x04001892 RID: 6290
		private const string HashSizeName = "HashSize";

		// Token: 0x04001893 RID: 6291
		private const string KeyValuePairsName = "KeyValuePairs";

		// Token: 0x04001894 RID: 6292
		private const string ComparerName = "Comparer";

		// Token: 0x02000BA9 RID: 2985
		private struct Entry
		{
			// Token: 0x04003502 RID: 13570
			public int hashCode;

			// Token: 0x04003503 RID: 13571
			public int next;

			// Token: 0x04003504 RID: 13572
			public TKey key;

			// Token: 0x04003505 RID: 13573
			public TValue value;
		}

		// Token: 0x02000BAA RID: 2986
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<KeyValuePair<!0, !1>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06006DD6 RID: 28118 RVA: 0x00179906 File Offset: 0x00177B06
			internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this.dictionary = dictionary;
				this.version = dictionary.version;
				this.index = 0;
				this.getEnumeratorRetType = getEnumeratorRetType;
				this.current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x06006DD7 RID: 28119 RVA: 0x00179938 File Offset: 0x00177B38
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				if (this.version != this.dictionary.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				while (this.index < this.dictionary.count)
				{
					if (this.dictionary.entries[this.index].hashCode >= 0)
					{
						this.current = new KeyValuePair<TKey, TValue>(this.dictionary.entries[this.index].key, this.dictionary.entries[this.index].value);
						this.index++;
						return true;
					}
					this.index++;
				}
				this.index = this.dictionary.count + 1;
				this.current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			// Token: 0x170012EE RID: 4846
			// (get) Token: 0x06006DD8 RID: 28120 RVA: 0x00179A17 File Offset: 0x00177C17
			[__DynamicallyInvokable]
			public KeyValuePair<TKey, TValue> Current
			{
				[__DynamicallyInvokable]
				get
				{
					return this.current;
				}
			}

			// Token: 0x06006DD9 RID: 28121 RVA: 0x00179A1F File Offset: 0x00177C1F
			[__DynamicallyInvokable]
			public void Dispose()
			{
			}

			// Token: 0x170012EF RID: 4847
			// (get) Token: 0x06006DDA RID: 28122 RVA: 0x00179A24 File Offset: 0x00177C24
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					if (this.getEnumeratorRetType == 1)
					{
						return new DictionaryEntry(this.current.Key, this.current.Value);
					}
					return new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
				}
			}

			// Token: 0x06006DDB RID: 28123 RVA: 0x00179AA9 File Offset: 0x00177CA9
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this.version != this.dictionary.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x170012F0 RID: 4848
			// (get) Token: 0x06006DDC RID: 28124 RVA: 0x00179AD8 File Offset: 0x00177CD8
			[__DynamicallyInvokable]
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return new DictionaryEntry(this.current.Key, this.current.Value);
				}
			}

			// Token: 0x170012F1 RID: 4849
			// (get) Token: 0x06006DDD RID: 28125 RVA: 0x00179B2E File Offset: 0x00177D2E
			[__DynamicallyInvokable]
			object IDictionaryEnumerator.Key
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.current.Key;
				}
			}

			// Token: 0x170012F2 RID: 4850
			// (get) Token: 0x06006DDE RID: 28126 RVA: 0x00179B64 File Offset: 0x00177D64
			[__DynamicallyInvokable]
			object IDictionaryEnumerator.Value
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.current.Value;
				}
			}

			// Token: 0x04003506 RID: 13574
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x04003507 RID: 13575
			private int version;

			// Token: 0x04003508 RID: 13576
			private int index;

			// Token: 0x04003509 RID: 13577
			private KeyValuePair<TKey, TValue> current;

			// Token: 0x0400350A RID: 13578
			private int getEnumeratorRetType;

			// Token: 0x0400350B RID: 13579
			internal const int DictEntry = 1;

			// Token: 0x0400350C RID: 13580
			internal const int KeyValuePair = 2;
		}

		// Token: 0x02000BAB RID: 2987
		[DebuggerTypeProxy(typeof(Mscorlib_DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class KeyCollection : ICollection<!0>, IEnumerable<!0>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06006DDF RID: 28127 RVA: 0x00179B9A File Offset: 0x00177D9A
			[__DynamicallyInvokable]
			public KeyCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this.dictionary = dictionary;
			}

			// Token: 0x06006DE0 RID: 28128 RVA: 0x00179BB2 File Offset: 0x00177DB2
			[__DynamicallyInvokable]
			public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06006DE1 RID: 28129 RVA: 0x00179BC0 File Offset: 0x00177DC0
			[__DynamicallyInvokable]
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].key;
					}
				}
			}

			// Token: 0x170012F3 RID: 4851
			// (get) Token: 0x06006DE2 RID: 28130 RVA: 0x00179C4B File Offset: 0x00177E4B
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x170012F4 RID: 4852
			// (get) Token: 0x06006DE3 RID: 28131 RVA: 0x00179C58 File Offset: 0x00177E58
			[__DynamicallyInvokable]
			bool ICollection<!0>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06006DE4 RID: 28132 RVA: 0x00179C5B File Offset: 0x00177E5B
			[__DynamicallyInvokable]
			void ICollection<!0>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06006DE5 RID: 28133 RVA: 0x00179C64 File Offset: 0x00177E64
			[__DynamicallyInvokable]
			void ICollection<!0>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06006DE6 RID: 28134 RVA: 0x00179C6D File Offset: 0x00177E6D
			[__DynamicallyInvokable]
			bool ICollection<!0>.Contains(TKey item)
			{
				return this.dictionary.ContainsKey(item);
			}

			// Token: 0x06006DE7 RID: 28135 RVA: 0x00179C7B File Offset: 0x00177E7B
			[__DynamicallyInvokable]
			bool ICollection<!0>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
				return false;
			}

			// Token: 0x06006DE8 RID: 28136 RVA: 0x00179C85 File Offset: 0x00177E85
			[__DynamicallyInvokable]
			IEnumerator<TKey> IEnumerable<!0>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06006DE9 RID: 28137 RVA: 0x00179C97 File Offset: 0x00177E97
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06006DEA RID: 28138 RVA: 0x00179CAC File Offset: 0x00177EAC
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].key;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
			}

			// Token: 0x170012F5 RID: 4853
			// (get) Token: 0x06006DEB RID: 28139 RVA: 0x00179DA4 File Offset: 0x00177FA4
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			// Token: 0x170012F6 RID: 4854
			// (get) Token: 0x06006DEC RID: 28140 RVA: 0x00179DA7 File Offset: 0x00177FA7
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400350D RID: 13581
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x02000CD4 RID: 3284
			[__DynamicallyInvokable]
			[Serializable]
			public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
			{
				// Token: 0x060070E7 RID: 28903 RVA: 0x001846FA File Offset: 0x001828FA
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this.dictionary = dictionary;
					this.version = dictionary.version;
					this.index = 0;
					this.currentKey = default(TKey);
				}

				// Token: 0x060070E8 RID: 28904 RVA: 0x00184722 File Offset: 0x00182922
				[__DynamicallyInvokable]
				public void Dispose()
				{
				}

				// Token: 0x060070E9 RID: 28905 RVA: 0x00184724 File Offset: 0x00182924
				[__DynamicallyInvokable]
				public bool MoveNext()
				{
					if (this.version != this.dictionary.version)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
					}
					while (this.index < this.dictionary.count)
					{
						if (this.dictionary.entries[this.index].hashCode >= 0)
						{
							this.currentKey = this.dictionary.entries[this.index].key;
							this.index++;
							return true;
						}
						this.index++;
					}
					this.index = this.dictionary.count + 1;
					this.currentKey = default(TKey);
					return false;
				}

				// Token: 0x17001371 RID: 4977
				// (get) Token: 0x060070EA RID: 28906 RVA: 0x001847DD File Offset: 0x001829DD
				[__DynamicallyInvokable]
				public TKey Current
				{
					[__DynamicallyInvokable]
					get
					{
						return this.currentKey;
					}
				}

				// Token: 0x17001372 RID: 4978
				// (get) Token: 0x060070EB RID: 28907 RVA: 0x001847E5 File Offset: 0x001829E5
				[__DynamicallyInvokable]
				object IEnumerator.Current
				{
					[__DynamicallyInvokable]
					get
					{
						if (this.index == 0 || this.index == this.dictionary.count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
						}
						return this.currentKey;
					}
				}

				// Token: 0x060070EC RID: 28908 RVA: 0x00184816 File Offset: 0x00182A16
				[__DynamicallyInvokable]
				void IEnumerator.Reset()
				{
					if (this.version != this.dictionary.version)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
					}
					this.index = 0;
					this.currentKey = default(TKey);
				}

				// Token: 0x04003870 RID: 14448
				private Dictionary<TKey, TValue> dictionary;

				// Token: 0x04003871 RID: 14449
				private int index;

				// Token: 0x04003872 RID: 14450
				private int version;

				// Token: 0x04003873 RID: 14451
				private TKey currentKey;
			}
		}

		// Token: 0x02000BAC RID: 2988
		[DebuggerTypeProxy(typeof(Mscorlib_DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class ValueCollection : ICollection<!1>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06006DED RID: 28141 RVA: 0x00179DB4 File Offset: 0x00177FB4
			[__DynamicallyInvokable]
			public ValueCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this.dictionary = dictionary;
			}

			// Token: 0x06006DEE RID: 28142 RVA: 0x00179DCC File Offset: 0x00177FCC
			[__DynamicallyInvokable]
			public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06006DEF RID: 28143 RVA: 0x00179DDC File Offset: 0x00177FDC
			[__DynamicallyInvokable]
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].value;
					}
				}
			}

			// Token: 0x170012F7 RID: 4855
			// (get) Token: 0x06006DF0 RID: 28144 RVA: 0x00179E67 File Offset: 0x00178067
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x170012F8 RID: 4856
			// (get) Token: 0x06006DF1 RID: 28145 RVA: 0x00179E74 File Offset: 0x00178074
			[__DynamicallyInvokable]
			bool ICollection<!1>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06006DF2 RID: 28146 RVA: 0x00179E77 File Offset: 0x00178077
			[__DynamicallyInvokable]
			void ICollection<!1>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06006DF3 RID: 28147 RVA: 0x00179E80 File Offset: 0x00178080
			[__DynamicallyInvokable]
			bool ICollection<!1>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
				return false;
			}

			// Token: 0x06006DF4 RID: 28148 RVA: 0x00179E8A File Offset: 0x0017808A
			[__DynamicallyInvokable]
			void ICollection<!1>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06006DF5 RID: 28149 RVA: 0x00179E93 File Offset: 0x00178093
			[__DynamicallyInvokable]
			bool ICollection<!1>.Contains(TValue item)
			{
				return this.dictionary.ContainsValue(item);
			}

			// Token: 0x06006DF6 RID: 28150 RVA: 0x00179EA1 File Offset: 0x001780A1
			[__DynamicallyInvokable]
			IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06006DF7 RID: 28151 RVA: 0x00179EB3 File Offset: 0x001780B3
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06006DF8 RID: 28152 RVA: 0x00179EC8 File Offset: 0x001780C8
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].value;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
			}

			// Token: 0x170012F9 RID: 4857
			// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x00179FC0 File Offset: 0x001781C0
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			// Token: 0x170012FA RID: 4858
			// (get) Token: 0x06006DFA RID: 28154 RVA: 0x00179FC3 File Offset: 0x001781C3
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400350E RID: 13582
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x02000CD5 RID: 3285
			[__DynamicallyInvokable]
			[Serializable]
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x060070ED RID: 28909 RVA: 0x00184845 File Offset: 0x00182A45
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this.dictionary = dictionary;
					this.version = dictionary.version;
					this.index = 0;
					this.currentValue = default(TValue);
				}

				// Token: 0x060070EE RID: 28910 RVA: 0x0018486D File Offset: 0x00182A6D
				[__DynamicallyInvokable]
				public void Dispose()
				{
				}

				// Token: 0x060070EF RID: 28911 RVA: 0x00184870 File Offset: 0x00182A70
				[__DynamicallyInvokable]
				public bool MoveNext()
				{
					if (this.version != this.dictionary.version)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
					}
					while (this.index < this.dictionary.count)
					{
						if (this.dictionary.entries[this.index].hashCode >= 0)
						{
							this.currentValue = this.dictionary.entries[this.index].value;
							this.index++;
							return true;
						}
						this.index++;
					}
					this.index = this.dictionary.count + 1;
					this.currentValue = default(TValue);
					return false;
				}

				// Token: 0x17001373 RID: 4979
				// (get) Token: 0x060070F0 RID: 28912 RVA: 0x00184929 File Offset: 0x00182B29
				[__DynamicallyInvokable]
				public TValue Current
				{
					[__DynamicallyInvokable]
					get
					{
						return this.currentValue;
					}
				}

				// Token: 0x17001374 RID: 4980
				// (get) Token: 0x060070F1 RID: 28913 RVA: 0x00184931 File Offset: 0x00182B31
				[__DynamicallyInvokable]
				object IEnumerator.Current
				{
					[__DynamicallyInvokable]
					get
					{
						if (this.index == 0 || this.index == this.dictionary.count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
						}
						return this.currentValue;
					}
				}

				// Token: 0x060070F2 RID: 28914 RVA: 0x00184962 File Offset: 0x00182B62
				[__DynamicallyInvokable]
				void IEnumerator.Reset()
				{
					if (this.version != this.dictionary.version)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
					}
					this.index = 0;
					this.currentValue = default(TValue);
				}

				// Token: 0x04003874 RID: 14452
				private Dictionary<TKey, TValue> dictionary;

				// Token: 0x04003875 RID: 14453
				private int index;

				// Token: 0x04003876 RID: 14454
				private int version;

				// Token: 0x04003877 RID: 14455
				private TValue currentValue;
			}
		}
	}
}
