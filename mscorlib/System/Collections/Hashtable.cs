using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	// Token: 0x0200046A RID: 1130
	[DebuggerTypeProxy(typeof(Hashtable.HashtableDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Hashtable : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
	{
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x000D3A32 File Offset: 0x000D1C32
		// (set) Token: 0x06003741 RID: 14145 RVA: 0x000D3A6C File Offset: 0x000D1C6C
		[Obsolete("Please use EqualityComparer property.")]
		protected IHashCodeProvider hcp
		{
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).HashCodeProvider;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
			}
			set
			{
				if (this._keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = (CompatibleComparer)this._keycomparer;
					this._keycomparer = new CompatibleComparer(compatibleComparer.Comparer, value);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(null, value);
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000D3ACA File Offset: 0x000D1CCA
		// (set) Token: 0x06003743 RID: 14147 RVA: 0x000D3B04 File Offset: 0x000D1D04
		[Obsolete("Please use KeyComparer properties.")]
		protected IComparer comparer
		{
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).Comparer;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
			}
			set
			{
				if (this._keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = (CompatibleComparer)this._keycomparer;
					this._keycomparer = new CompatibleComparer(value, compatibleComparer.HashCodeProvider);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(value, null);
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06003744 RID: 14148 RVA: 0x000D3B62 File Offset: 0x000D1D62
		protected IEqualityComparer EqualityComparer
		{
			get
			{
				return this._keycomparer;
			}
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000D3B6A File Offset: 0x000D1D6A
		internal Hashtable(bool trash)
		{
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000D3B72 File Offset: 0x000D1D72
		public Hashtable() : this(0, 1f)
		{
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000D3B80 File Offset: 0x000D1D80
		public Hashtable(int capacity) : this(capacity, 1f)
		{
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000D3B90 File Offset: 0x000D1D90
		public Hashtable(int capacity, float loadFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (loadFactor < 0.1f || loadFactor > 1f)
			{
				throw new ArgumentOutOfRangeException("loadFactor", Environment.GetResourceString("ArgumentOutOfRange_HashtableLoadFactor", new object[]
				{
					0.1,
					1.0
				}));
			}
			this.loadFactor = 0.72f * loadFactor;
			double num = (double)((float)capacity / this.loadFactor);
			if (num > 2147483647.0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_HTCapacityOverflow"));
			}
			int num2 = (num > 3.0) ? HashHelpers.GetPrime((int)num) : 3;
			this.buckets = new Hashtable.bucket[num2];
			this.loadsize = (int)(this.loadFactor * (float)num2);
			this.isWriterInProgress = false;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000D3C79 File Offset: 0x000D1E79
		[Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead.")]
		public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer) : this(capacity, loadFactor)
		{
			if (hcp == null && comparer == null)
			{
				this._keycomparer = null;
				return;
			}
			this._keycomparer = new CompatibleComparer(comparer, hcp);
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000D3CA0 File Offset: 0x000D1EA0
		public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer) : this(capacity, loadFactor)
		{
			this._keycomparer = equalityComparer;
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000D3CB1 File Offset: 0x000D1EB1
		[Obsolete("Please use Hashtable(IEqualityComparer) instead.")]
		public Hashtable(IHashCodeProvider hcp, IComparer comparer) : this(0, 1f, hcp, comparer)
		{
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000D3CC1 File Offset: 0x000D1EC1
		public Hashtable(IEqualityComparer equalityComparer) : this(0, 1f, equalityComparer)
		{
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000D3CD0 File Offset: 0x000D1ED0
		[Obsolete("Please use Hashtable(int, IEqualityComparer) instead.")]
		public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer) : this(capacity, 1f, hcp, comparer)
		{
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000D3CE0 File Offset: 0x000D1EE0
		public Hashtable(int capacity, IEqualityComparer equalityComparer) : this(capacity, 1f, equalityComparer)
		{
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000D3CEF File Offset: 0x000D1EEF
		public Hashtable(IDictionary d) : this(d, 1f)
		{
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000D3CFD File Offset: 0x000D1EFD
		public Hashtable(IDictionary d, float loadFactor) : this(d, loadFactor, null)
		{
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000D3D08 File Offset: 0x000D1F08
		[Obsolete("Please use Hashtable(IDictionary, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, IHashCodeProvider hcp, IComparer comparer) : this(d, 1f, hcp, comparer)
		{
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000D3D18 File Offset: 0x000D1F18
		public Hashtable(IDictionary d, IEqualityComparer equalityComparer) : this(d, 1f, equalityComparer)
		{
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000D3D28 File Offset: 0x000D1F28
		[Obsolete("Please use Hashtable(IDictionary, float, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer) : this((d != null) ? d.Count : 0, loadFactor, hcp, comparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", Environment.GetResourceString("ArgumentNull_Dictionary"));
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000D3D88 File Offset: 0x000D1F88
		public Hashtable(IDictionary d, float loadFactor, IEqualityComparer equalityComparer) : this((d != null) ? d.Count : 0, loadFactor, equalityComparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", Environment.GetResourceString("ArgumentNull_Dictionary"));
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000D3DE4 File Offset: 0x000D1FE4
		protected Hashtable(SerializationInfo info, StreamingContext context)
		{
			HashHelpers.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000D3DF8 File Offset: 0x000D1FF8
		private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
		{
			uint num = (uint)(this.GetHash(key) & int.MaxValue);
			seed = num;
			incr = 1U + seed * 101U % (uint)(hashsize - 1);
			return num;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000D3E25 File Offset: 0x000D2025
		public virtual void Add(object key, object value)
		{
			this.Insert(key, value, true);
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000D3E30 File Offset: 0x000D2030
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public virtual void Clear()
		{
			if (this.count == 0 && this.occupancy == 0)
			{
				return;
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i].hash_coll = 0;
				this.buckets[i].key = null;
				this.buckets[i].val = null;
			}
			this.count = 0;
			this.occupancy = 0;
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000D3EC8 File Offset: 0x000D20C8
		public virtual object Clone()
		{
			Hashtable.bucket[] array = this.buckets;
			Hashtable hashtable = new Hashtable(this.count, this._keycomparer);
			hashtable.version = this.version;
			hashtable.loadFactor = this.loadFactor;
			hashtable.count = 0;
			int i = array.Length;
			while (i > 0)
			{
				i--;
				object key = array[i].key;
				if (key != null && key != array)
				{
					hashtable[key] = array[i].val;
				}
			}
			return hashtable;
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000D3F47 File Offset: 0x000D2147
		public virtual bool Contains(object key)
		{
			return this.ContainsKey(key);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000D3F50 File Offset: 0x000D2150
		public virtual bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			Hashtable.bucket[] array = this.buckets;
			uint num2;
			uint num3;
			uint num = this.InitHash(key, array.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)array.Length);
			for (;;)
			{
				Hashtable.bucket bucket = array[num5];
				if (bucket.key == null)
				{
					break;
				}
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					return true;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)array.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= array.Length)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000D3FF4 File Offset: 0x000D21F4
		public virtual bool ContainsValue(object value)
		{
			if (value == null)
			{
				int num = this.buckets.Length;
				while (--num >= 0)
				{
					if (this.buckets[num].key != null && this.buckets[num].key != this.buckets && this.buckets[num].val == null)
					{
						return true;
					}
				}
			}
			else
			{
				int num2 = this.buckets.Length;
				while (--num2 >= 0)
				{
					object val = this.buckets[num2].val;
					if (val != null && val.Equals(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000D4090 File Offset: 0x000D2290
		private void CopyKeys(Array array, int arrayIndex)
		{
			Hashtable.bucket[] array2 = this.buckets;
			int num = array2.Length;
			while (--num >= 0)
			{
				object key = array2[num].key;
				if (key != null && key != this.buckets)
				{
					array.SetValue(key, arrayIndex++);
				}
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000D40D8 File Offset: 0x000D22D8
		private void CopyEntries(Array array, int arrayIndex)
		{
			Hashtable.bucket[] array2 = this.buckets;
			int num = array2.Length;
			while (--num >= 0)
			{
				object key = array2[num].key;
				if (key != null && key != this.buckets)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(key, array2[num].val);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000D413C File Offset: 0x000D233C
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
			}
			this.CopyEntries(array, arrayIndex);
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000D41BC File Offset: 0x000D23BC
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this.count];
			int num = 0;
			Hashtable.bucket[] array2 = this.buckets;
			int num2 = array2.Length;
			while (--num2 >= 0)
			{
				object key = array2[num2].key;
				if (key != null && key != this.buckets)
				{
					array[num++] = new KeyValuePairs(key, array2[num2].val);
				}
			}
			return array;
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000D4224 File Offset: 0x000D2424
		private void CopyValues(Array array, int arrayIndex)
		{
			Hashtable.bucket[] array2 = this.buckets;
			int num = array2.Length;
			while (--num >= 0)
			{
				object key = array2[num].key;
				if (key != null && key != this.buckets)
				{
					array.SetValue(array2[num].val, arrayIndex++);
				}
			}
		}

		// Token: 0x1700084B RID: 2123
		public virtual object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				Hashtable.bucket[] array = this.buckets;
				uint num2;
				uint num3;
				uint num = this.InitHash(key, array.Length, out num2, out num3);
				int num4 = 0;
				int num5 = (int)(num2 % (uint)array.Length);
				Hashtable.bucket bucket;
				for (;;)
				{
					int num6 = 0;
					int num7;
					do
					{
						num7 = this.version;
						bucket = array[num5];
						if (++num6 % 8 == 0)
						{
							Thread.Sleep(1);
						}
					}
					while (this.isWriterInProgress || num7 != this.version);
					if (bucket.key == null)
					{
						break;
					}
					if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
					{
						goto Block_7;
					}
					num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)array.Length));
					if (bucket.hash_coll >= 0 || ++num4 >= array.Length)
					{
						goto IL_D2;
					}
				}
				return null;
				Block_7:
				return bucket.val;
				IL_D2:
				return null;
			}
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000D4364 File Offset: 0x000D2564
		private void expand()
		{
			int newsize = HashHelpers.ExpandPrime(this.buckets.Length);
			this.rehash(newsize, false);
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000D4387 File Offset: 0x000D2587
		private void rehash()
		{
			this.rehash(this.buckets.Length, false);
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000D4398 File Offset: 0x000D2598
		private void UpdateVersion()
		{
			this.version++;
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000D43AC File Offset: 0x000D25AC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void rehash(int newsize, bool forceNewHashCode)
		{
			this.occupancy = 0;
			Hashtable.bucket[] newBuckets = new Hashtable.bucket[newsize];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				Hashtable.bucket bucket = this.buckets[i];
				if (bucket.key != null && bucket.key != this.buckets)
				{
					int hashcode = (forceNewHashCode ? this.GetHash(bucket.key) : bucket.hash_coll) & int.MaxValue;
					this.putEntry(newBuckets, bucket.key, bucket.val, hashcode);
				}
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			this.buckets = newBuckets;
			this.loadsize = (int)(this.loadFactor * (float)newsize);
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000D4468 File Offset: 0x000D2668
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000D4471 File Offset: 0x000D2671
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000D447A File Offset: 0x000D267A
		protected virtual int GetHash(object key)
		{
			if (this._keycomparer != null)
			{
				return this._keycomparer.GetHashCode(key);
			}
			return key.GetHashCode();
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000D4497 File Offset: 0x000D2697
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000D449A File Offset: 0x000D269A
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000D449D File Offset: 0x000D269D
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000D44A0 File Offset: 0x000D26A0
		protected virtual bool KeyEquals(object item, object key)
		{
			if (this.buckets == item)
			{
				return false;
			}
			if (item == key)
			{
				return true;
			}
			if (this._keycomparer != null)
			{
				return this._keycomparer.Equals(item, key);
			}
			return item != null && item.Equals(key);
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000D44D5 File Offset: 0x000D26D5
		public virtual ICollection Keys
		{
			get
			{
				if (this.keys == null)
				{
					this.keys = new Hashtable.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000D44F1 File Offset: 0x000D26F1
		public virtual ICollection Values
		{
			get
			{
				if (this.values == null)
				{
					this.values = new Hashtable.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000D4510 File Offset: 0x000D2710
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void Insert(object key, object nvalue, bool add)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			if (this.count >= this.loadsize)
			{
				this.expand();
			}
			else if (this.occupancy > this.loadsize && this.count > 100)
			{
				this.rehash();
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this.buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = -1;
			int num6 = (int)(num2 % (uint)this.buckets.Length);
			for (;;)
			{
				if (num5 == -1 && this.buckets[num6].key == this.buckets && this.buckets[num6].hash_coll < 0)
				{
					num5 = num6;
				}
				if (this.buckets[num6].key == null || (this.buckets[num6].key == this.buckets && ((long)this.buckets[num6].hash_coll & (long)((ulong)-2147483648)) == 0L))
				{
					break;
				}
				if ((long)(this.buckets[num6].hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(this.buckets[num6].key, key))
				{
					goto Block_15;
				}
				if (num5 == -1 && this.buckets[num6].hash_coll >= 0)
				{
					Hashtable.bucket[] array = this.buckets;
					int num7 = num6;
					array[num7].hash_coll = (array[num7].hash_coll | int.MinValue);
					this.occupancy++;
				}
				num6 = (int)(((long)num6 + (long)((ulong)num3)) % (long)((ulong)this.buckets.Length));
				if (++num4 >= this.buckets.Length)
				{
					goto Block_22;
				}
			}
			if (num5 != -1)
			{
				num6 = num5;
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			this.buckets[num6].val = nvalue;
			this.buckets[num6].key = key;
			Hashtable.bucket[] array2 = this.buckets;
			int num8 = num6;
			array2[num8].hash_coll = (array2[num8].hash_coll | (int)num);
			this.count++;
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
			if (num4 > 100 && HashHelpers.IsWellKnownEqualityComparer(this._keycomparer) && (this._keycomparer == null || !(this._keycomparer is RandomizedObjectEqualityComparer)))
			{
				this._keycomparer = HashHelpers.GetRandomizedEqualityComparer(this._keycomparer);
				this.rehash(this.buckets.Length, true);
			}
			return;
			Block_15:
			if (add)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", new object[]
				{
					this.buckets[num6].key,
					key
				}));
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			this.buckets[num6].val = nvalue;
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
			if (num4 > 100 && HashHelpers.IsWellKnownEqualityComparer(this._keycomparer) && (this._keycomparer == null || !(this._keycomparer is RandomizedObjectEqualityComparer)))
			{
				this._keycomparer = HashHelpers.GetRandomizedEqualityComparer(this._keycomparer);
				this.rehash(this.buckets.Length, true);
			}
			return;
			Block_22:
			if (num5 != -1)
			{
				Thread.BeginCriticalRegion();
				this.isWriterInProgress = true;
				this.buckets[num5].val = nvalue;
				this.buckets[num5].key = key;
				Hashtable.bucket[] array3 = this.buckets;
				int num9 = num5;
				array3[num9].hash_coll = (array3[num9].hash_coll | (int)num);
				this.count++;
				this.UpdateVersion();
				this.isWriterInProgress = false;
				Thread.EndCriticalRegion();
				if (this.buckets.Length > 100 && HashHelpers.IsWellKnownEqualityComparer(this._keycomparer) && (this._keycomparer == null || !(this._keycomparer is RandomizedObjectEqualityComparer)))
				{
					this._keycomparer = HashHelpers.GetRandomizedEqualityComparer(this._keycomparer);
					this.rehash(this.buckets.Length, true);
				}
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HashInsertFailed"));
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000D48FC File Offset: 0x000D2AFC
		private void putEntry(Hashtable.bucket[] newBuckets, object key, object nvalue, int hashcode)
		{
			uint num = (uint)(1 + hashcode * 101 % (newBuckets.Length - 1));
			int num2 = hashcode % newBuckets.Length;
			while (newBuckets[num2].key != null && newBuckets[num2].key != this.buckets)
			{
				if (newBuckets[num2].hash_coll >= 0)
				{
					int num3 = num2;
					newBuckets[num3].hash_coll = (newBuckets[num3].hash_coll | int.MinValue);
					this.occupancy++;
				}
				num2 = (int)(((long)num2 + (long)((ulong)num)) % (long)((ulong)newBuckets.Length));
			}
			newBuckets[num2].val = nvalue;
			newBuckets[num2].key = key;
			int num4 = num2;
			newBuckets[num4].hash_coll = (newBuckets[num4].hash_coll | hashcode);
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000D49B0 File Offset: 0x000D2BB0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this.buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)this.buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = this.buckets[num5];
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					break;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)this.buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= this.buckets.Length)
				{
					return;
				}
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			Hashtable.bucket[] array = this.buckets;
			int num6 = num5;
			array[num6].hash_coll = (array[num6].hash_coll & int.MinValue);
			if (this.buckets[num5].hash_coll != 0)
			{
				this.buckets[num5].key = this.buckets;
			}
			else
			{
				this.buckets[num5].key = null;
			}
			this.buckets[num5].val = null;
			this.count--;
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06003774 RID: 14196 RVA: 0x000D4AFD File Offset: 0x000D2CFD
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000D4B1F File Offset: 0x000D2D1F
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000D4B27 File Offset: 0x000D2D27
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Hashtable Synchronized(Hashtable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			return new Hashtable.SyncHashtable(table);
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000D4B40 File Offset: 0x000D2D40
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				int num = this.version;
				info.AddValue("LoadFactor", this.loadFactor);
				info.AddValue("Version", this.version);
				IEqualityComparer equalityComparer = (IEqualityComparer)HashHelpers.GetEqualityComparerForSerialization(this._keycomparer);
				if (equalityComparer == null)
				{
					info.AddValue("Comparer", null, typeof(IComparer));
					info.AddValue("HashCodeProvider", null, typeof(IHashCodeProvider));
				}
				else if (equalityComparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = equalityComparer as CompatibleComparer;
					info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
					info.AddValue("HashCodeProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				}
				else
				{
					info.AddValue("KeyComparer", equalityComparer, typeof(IEqualityComparer));
				}
				info.AddValue("HashSize", this.buckets.Length);
				object[] array = new object[this.count];
				object[] array2 = new object[this.count];
				this.CopyKeys(array, 0);
				this.CopyValues(array2, 0);
				info.AddValue("Keys", array, typeof(object[]));
				info.AddValue("Values", array2, typeof(object[]));
				if (this.version != num)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
			}
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000D4CEC File Offset: 0x000D2EEC
		public virtual void OnDeserialization(object sender)
		{
			if (this.buckets != null)
			{
				return;
			}
			SerializationInfo serializationInfo;
			HashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidOnDeser"));
			}
			int num = 0;
			IComparer comparer = null;
			IHashCodeProvider hashCodeProvider = null;
			object[] array = null;
			object[] array2 = null;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num2 <= 1613443821U)
				{
					if (num2 != 891156946U)
					{
						if (num2 != 1228509323U)
						{
							if (num2 == 1613443821U)
							{
								if (name == "Keys")
								{
									array = (object[])serializationInfo.GetValue("Keys", typeof(object[]));
								}
							}
						}
						else if (name == "KeyComparer")
						{
							this._keycomparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
						}
					}
					else if (name == "Comparer")
					{
						comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
					}
				}
				else if (num2 <= 2484309429U)
				{
					if (num2 != 2370642523U)
					{
						if (num2 == 2484309429U)
						{
							if (name == "HashCodeProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashCodeProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Values")
					{
						array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
					}
				}
				else if (num2 != 3356145248U)
				{
					if (num2 == 3483216242U)
					{
						if (name == "LoadFactor")
						{
							this.loadFactor = serializationInfo.GetSingle("LoadFactor");
						}
					}
				}
				else if (name == "HashSize")
				{
					num = serializationInfo.GetInt32("HashSize");
				}
			}
			this.loadsize = (int)(this.loadFactor * (float)num);
			if (this._keycomparer == null && (comparer != null || hashCodeProvider != null))
			{
				this._keycomparer = new CompatibleComparer(comparer, hashCodeProvider);
			}
			this.buckets = new Hashtable.bucket[num];
			if (array == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_MissingKeys"));
			}
			if (array2 == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_MissingValues"));
			}
			if (array.Length != array2.Length)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_KeyValueDifferentSizes"));
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_NullKey"));
				}
				this.Insert(array[i], array2[i], true);
			}
			this.version = serializationInfo.GetInt32("Version");
			HashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x04001826 RID: 6182
		internal const int HashPrime = 101;

		// Token: 0x04001827 RID: 6183
		private const int InitialSize = 3;

		// Token: 0x04001828 RID: 6184
		private const string LoadFactorName = "LoadFactor";

		// Token: 0x04001829 RID: 6185
		private const string VersionName = "Version";

		// Token: 0x0400182A RID: 6186
		private const string ComparerName = "Comparer";

		// Token: 0x0400182B RID: 6187
		private const string HashCodeProviderName = "HashCodeProvider";

		// Token: 0x0400182C RID: 6188
		private const string HashSizeName = "HashSize";

		// Token: 0x0400182D RID: 6189
		private const string KeysName = "Keys";

		// Token: 0x0400182E RID: 6190
		private const string ValuesName = "Values";

		// Token: 0x0400182F RID: 6191
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04001830 RID: 6192
		private Hashtable.bucket[] buckets;

		// Token: 0x04001831 RID: 6193
		private int count;

		// Token: 0x04001832 RID: 6194
		private int occupancy;

		// Token: 0x04001833 RID: 6195
		private int loadsize;

		// Token: 0x04001834 RID: 6196
		private float loadFactor;

		// Token: 0x04001835 RID: 6197
		private volatile int version;

		// Token: 0x04001836 RID: 6198
		private volatile bool isWriterInProgress;

		// Token: 0x04001837 RID: 6199
		private ICollection keys;

		// Token: 0x04001838 RID: 6200
		private ICollection values;

		// Token: 0x04001839 RID: 6201
		private IEqualityComparer _keycomparer;

		// Token: 0x0400183A RID: 6202
		private object _syncRoot;

		// Token: 0x02000B80 RID: 2944
		private struct bucket
		{
			// Token: 0x04003483 RID: 13443
			public object key;

			// Token: 0x04003484 RID: 13444
			public object val;

			// Token: 0x04003485 RID: 13445
			public int hash_coll;
		}

		// Token: 0x02000B81 RID: 2945
		[Serializable]
		private class KeyCollection : ICollection, IEnumerable
		{
			// Token: 0x06006CC7 RID: 27847 RVA: 0x00176FD4 File Offset: 0x001751D4
			internal KeyCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06006CC8 RID: 27848 RVA: 0x00176FE4 File Offset: 0x001751E4
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - arrayIndex < this._hashtable.count)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
				}
				this._hashtable.CopyKeys(array, arrayIndex);
			}

			// Token: 0x06006CC9 RID: 27849 RVA: 0x00177063 File Offset: 0x00175263
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 1);
			}

			// Token: 0x17001295 RID: 4757
			// (get) Token: 0x06006CCA RID: 27850 RVA: 0x00177071 File Offset: 0x00175271
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x17001296 RID: 4758
			// (get) Token: 0x06006CCB RID: 27851 RVA: 0x0017707E File Offset: 0x0017527E
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x17001297 RID: 4759
			// (get) Token: 0x06006CCC RID: 27852 RVA: 0x0017708B File Offset: 0x0017528B
			public virtual int Count
			{
				get
				{
					return this._hashtable.count;
				}
			}

			// Token: 0x04003486 RID: 13446
			private Hashtable _hashtable;
		}

		// Token: 0x02000B82 RID: 2946
		[Serializable]
		private class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06006CCD RID: 27853 RVA: 0x00177098 File Offset: 0x00175298
			internal ValueCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06006CCE RID: 27854 RVA: 0x001770A8 File Offset: 0x001752A8
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - arrayIndex < this._hashtable.count)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
				}
				this._hashtable.CopyValues(array, arrayIndex);
			}

			// Token: 0x06006CCF RID: 27855 RVA: 0x00177127 File Offset: 0x00175327
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 2);
			}

			// Token: 0x17001298 RID: 4760
			// (get) Token: 0x06006CD0 RID: 27856 RVA: 0x00177135 File Offset: 0x00175335
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x17001299 RID: 4761
			// (get) Token: 0x06006CD1 RID: 27857 RVA: 0x00177142 File Offset: 0x00175342
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x1700129A RID: 4762
			// (get) Token: 0x06006CD2 RID: 27858 RVA: 0x0017714F File Offset: 0x0017534F
			public virtual int Count
			{
				get
				{
					return this._hashtable.count;
				}
			}

			// Token: 0x04003487 RID: 13447
			private Hashtable _hashtable;
		}

		// Token: 0x02000B83 RID: 2947
		[Serializable]
		private class SyncHashtable : Hashtable, IEnumerable
		{
			// Token: 0x06006CD3 RID: 27859 RVA: 0x0017715C File Offset: 0x0017535C
			internal SyncHashtable(Hashtable table) : base(false)
			{
				this._table = table;
			}

			// Token: 0x06006CD4 RID: 27860 RVA: 0x0017716C File Offset: 0x0017536C
			internal SyncHashtable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this._table = (Hashtable)info.GetValue("ParentTable", typeof(Hashtable));
				if (this._table == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
				}
			}

			// Token: 0x06006CD5 RID: 27861 RVA: 0x001771BC File Offset: 0x001753BC
			[SecurityCritical]
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					info.AddValue("ParentTable", this._table, typeof(Hashtable));
				}
			}

			// Token: 0x1700129B RID: 4763
			// (get) Token: 0x06006CD6 RID: 27862 RVA: 0x00177224 File Offset: 0x00175424
			public override int Count
			{
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x1700129C RID: 4764
			// (get) Token: 0x06006CD7 RID: 27863 RVA: 0x00177231 File Offset: 0x00175431
			public override bool IsReadOnly
			{
				get
				{
					return this._table.IsReadOnly;
				}
			}

			// Token: 0x1700129D RID: 4765
			// (get) Token: 0x06006CD8 RID: 27864 RVA: 0x0017723E File Offset: 0x0017543E
			public override bool IsFixedSize
			{
				get
				{
					return this._table.IsFixedSize;
				}
			}

			// Token: 0x1700129E RID: 4766
			// (get) Token: 0x06006CD9 RID: 27865 RVA: 0x0017724B File Offset: 0x0017544B
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700129F RID: 4767
			public override object this[object key]
			{
				get
				{
					return this._table[key];
				}
				set
				{
					object syncRoot = this._table.SyncRoot;
					lock (syncRoot)
					{
						this._table[key] = value;
					}
				}
			}

			// Token: 0x170012A0 RID: 4768
			// (get) Token: 0x06006CDC RID: 27868 RVA: 0x001772A8 File Offset: 0x001754A8
			public override object SyncRoot
			{
				get
				{
					return this._table.SyncRoot;
				}
			}

			// Token: 0x06006CDD RID: 27869 RVA: 0x001772B8 File Offset: 0x001754B8
			public override void Add(object key, object value)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Add(key, value);
				}
			}

			// Token: 0x06006CDE RID: 27870 RVA: 0x00177304 File Offset: 0x00175504
			public override void Clear()
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Clear();
				}
			}

			// Token: 0x06006CDF RID: 27871 RVA: 0x00177350 File Offset: 0x00175550
			public override bool Contains(object key)
			{
				return this._table.Contains(key);
			}

			// Token: 0x06006CE0 RID: 27872 RVA: 0x0017735E File Offset: 0x0017555E
			public override bool ContainsKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				return this._table.ContainsKey(key);
			}

			// Token: 0x06006CE1 RID: 27873 RVA: 0x00177384 File Offset: 0x00175584
			public override bool ContainsValue(object key)
			{
				object syncRoot = this._table.SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = this._table.ContainsValue(key);
				}
				return result;
			}

			// Token: 0x06006CE2 RID: 27874 RVA: 0x001773D4 File Offset: 0x001755D4
			public override void CopyTo(Array array, int arrayIndex)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006CE3 RID: 27875 RVA: 0x00177420 File Offset: 0x00175620
			public override object Clone()
			{
				object syncRoot = this._table.SyncRoot;
				object result;
				lock (syncRoot)
				{
					result = Hashtable.Synchronized((Hashtable)this._table.Clone());
				}
				return result;
			}

			// Token: 0x06006CE4 RID: 27876 RVA: 0x00177478 File Offset: 0x00175678
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x06006CE5 RID: 27877 RVA: 0x00177485 File Offset: 0x00175685
			public override IDictionaryEnumerator GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x170012A1 RID: 4769
			// (get) Token: 0x06006CE6 RID: 27878 RVA: 0x00177494 File Offset: 0x00175694
			public override ICollection Keys
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection keys;
					lock (syncRoot)
					{
						keys = this._table.Keys;
					}
					return keys;
				}
			}

			// Token: 0x170012A2 RID: 4770
			// (get) Token: 0x06006CE7 RID: 27879 RVA: 0x001774E0 File Offset: 0x001756E0
			public override ICollection Values
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection values;
					lock (syncRoot)
					{
						values = this._table.Values;
					}
					return values;
				}
			}

			// Token: 0x06006CE8 RID: 27880 RVA: 0x0017752C File Offset: 0x0017572C
			public override void Remove(object key)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Remove(key);
				}
			}

			// Token: 0x06006CE9 RID: 27881 RVA: 0x00177578 File Offset: 0x00175778
			public override void OnDeserialization(object sender)
			{
			}

			// Token: 0x06006CEA RID: 27882 RVA: 0x0017757A File Offset: 0x0017577A
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._table.ToKeyValuePairsArray();
			}

			// Token: 0x04003488 RID: 13448
			protected Hashtable _table;
		}

		// Token: 0x02000B84 RID: 2948
		[Serializable]
		private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06006CEB RID: 27883 RVA: 0x00177587 File Offset: 0x00175787
			internal HashtableEnumerator(Hashtable hashtable, int getObjRetType)
			{
				this.hashtable = hashtable;
				this.bucket = hashtable.buckets.Length;
				this.version = hashtable.version;
				this.current = false;
				this.getObjectRetType = getObjRetType;
			}

			// Token: 0x06006CEC RID: 27884 RVA: 0x001775C0 File Offset: 0x001757C0
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x170012A3 RID: 4771
			// (get) Token: 0x06006CED RID: 27885 RVA: 0x001775C8 File Offset: 0x001757C8
			public virtual object Key
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					return this.currentKey;
				}
			}

			// Token: 0x06006CEE RID: 27886 RVA: 0x001775E8 File Offset: 0x001757E8
			public virtual bool MoveNext()
			{
				if (this.version != this.hashtable.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				while (this.bucket > 0)
				{
					this.bucket--;
					object key = this.hashtable.buckets[this.bucket].key;
					if (key != null && key != this.hashtable.buckets)
					{
						this.currentKey = key;
						this.currentValue = this.hashtable.buckets[this.bucket].val;
						this.current = true;
						return true;
					}
				}
				this.current = false;
				return false;
			}

			// Token: 0x170012A4 RID: 4772
			// (get) Token: 0x06006CEF RID: 27887 RVA: 0x00177697 File Offset: 0x00175897
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x170012A5 RID: 4773
			// (get) Token: 0x06006CF0 RID: 27888 RVA: 0x001776C4 File Offset: 0x001758C4
			public virtual object Current
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					if (this.getObjectRetType == 1)
					{
						return this.currentKey;
					}
					if (this.getObjectRetType == 2)
					{
						return this.currentValue;
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x170012A6 RID: 4774
			// (get) Token: 0x06006CF1 RID: 27889 RVA: 0x0017771F File Offset: 0x0017591F
			public virtual object Value
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.currentValue;
				}
			}

			// Token: 0x06006CF2 RID: 27890 RVA: 0x00177740 File Offset: 0x00175940
			public virtual void Reset()
			{
				if (this.version != this.hashtable.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.current = false;
				this.bucket = this.hashtable.buckets.Length;
				this.currentKey = null;
				this.currentValue = null;
			}

			// Token: 0x04003489 RID: 13449
			private Hashtable hashtable;

			// Token: 0x0400348A RID: 13450
			private int bucket;

			// Token: 0x0400348B RID: 13451
			private int version;

			// Token: 0x0400348C RID: 13452
			private bool current;

			// Token: 0x0400348D RID: 13453
			private int getObjectRetType;

			// Token: 0x0400348E RID: 13454
			private object currentKey;

			// Token: 0x0400348F RID: 13455
			private object currentValue;

			// Token: 0x04003490 RID: 13456
			internal const int Keys = 1;

			// Token: 0x04003491 RID: 13457
			internal const int Values = 2;

			// Token: 0x04003492 RID: 13458
			internal const int DictEntry = 3;
		}

		// Token: 0x02000B85 RID: 2949
		internal class HashtableDebugView
		{
			// Token: 0x06006CF3 RID: 27891 RVA: 0x0017779A File Offset: 0x0017599A
			public HashtableDebugView(Hashtable hashtable)
			{
				if (hashtable == null)
				{
					throw new ArgumentNullException("hashtable");
				}
				this.hashtable = hashtable;
			}

			// Token: 0x170012A7 RID: 4775
			// (get) Token: 0x06006CF4 RID: 27892 RVA: 0x001777B7 File Offset: 0x001759B7
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this.hashtable.ToKeyValuePairsArray();
				}
			}

			// Token: 0x04003493 RID: 13459
			private Hashtable hashtable;
		}
	}
}
