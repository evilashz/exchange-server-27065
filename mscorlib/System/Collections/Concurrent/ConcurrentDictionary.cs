using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x02000481 RID: 1153
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<!0, !1>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06003810 RID: 14352 RVA: 0x000D6204 File Offset: 0x000D4404
		private static bool IsValueWriteAtomic()
		{
			Type typeFromHandle = typeof(TValue);
			if (typeFromHandle.IsClass)
			{
				return true;
			}
			switch (Type.GetTypeCode(typeFromHandle))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Single:
				return true;
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Double:
				return IntPtr.Size == 8;
			default:
				return false;
			}
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000D6273 File Offset: 0x000D4473
		[__DynamicallyInvokable]
		public ConcurrentDictionary() : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000D6288 File Offset: 0x000D4488
		[__DynamicallyInvokable]
		public ConcurrentDictionary(int concurrencyLevel, int capacity) : this(concurrencyLevel, capacity, false, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000D6298 File Offset: 0x000D4498
		[__DynamicallyInvokable]
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : this(collection, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000D62A6 File Offset: 0x000D44A6
		[__DynamicallyInvokable]
		public ConcurrentDictionary(IEqualityComparer<TKey> comparer) : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, comparer)
		{
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000D62B7 File Offset: 0x000D44B7
		[__DynamicallyInvokable]
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000D62D5 File Offset: 0x000D44D5
		[__DynamicallyInvokable]
		public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : this(concurrencyLevel, 31, false, comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000D6308 File Offset: 0x000D4508
		private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				if (keyValuePair.Key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				if (!this.TryAddInternal(keyValuePair.Key, keyValuePair.Value, false, false, out tvalue))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_SourceContainsDuplicateKeys"));
				}
			}
			if (this.m_budget == 0)
			{
				this.m_budget = this.m_tables.m_buckets.Length / this.m_tables.m_locks.Length;
			}
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000D63BC File Offset: 0x000D45BC
		[__DynamicallyInvokable]
		public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer) : this(concurrencyLevel, capacity, false, comparer)
		{
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000D63C8 File Offset: 0x000D45C8
		internal ConcurrentDictionary(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<TKey> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel", this.GetResource("ConcurrentDictionary_ConcurrencyLevelMustBePositive"));
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", this.GetResource("ConcurrentDictionary_CapacityMustNotBeNegative"));
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			if (capacity < concurrencyLevel)
			{
				capacity = concurrencyLevel;
			}
			object[] array = new object[concurrencyLevel];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new object();
			}
			int[] countPerLock = new int[array.Length];
			ConcurrentDictionary<TKey, TValue>.Node[] array2 = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
			this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(array2, array, countPerLock, comparer);
			this.m_growLockArray = growLockArray;
			this.m_budget = array2.Length / array.Length;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000D6478 File Offset: 0x000D4678
		[__DynamicallyInvokable]
		public bool TryAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			return this.TryAddInternal(key, value, false, true, out tvalue);
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000D64A4 File Offset: 0x000D46A4
		[__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000D64D0 File Offset: 0x000D46D0
		[__DynamicallyInvokable]
		public bool TryRemove(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.TryRemoveInternal(key, out value, false, default(TValue));
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000D6504 File Offset: 0x000D4704
		private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
		{
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
				IEqualityComparer<TKey> comparer = tables.m_comparer;
				int num;
				int num2;
				this.GetBucketAndLockNo(comparer.GetHashCode(key), out num, out num2, tables.m_buckets.Length, tables.m_locks.Length);
				object obj = tables.m_locks[num2];
				lock (obj)
				{
					if (tables != this.m_tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[num];
					while (node2 != null)
					{
						if (comparer.Equals(node2.m_key, key))
						{
							if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, node2.m_value))
							{
								value = default(TValue);
								return false;
							}
							if (node == null)
							{
								Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[num], node2.m_next);
							}
							else
							{
								node.m_next = node2.m_next;
							}
							value = node2.m_value;
							tables.m_countPerLock[num2]--;
							return true;
						}
						else
						{
							node = node2;
							node2 = node2.m_next;
						}
					}
				}
				break;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000D664C File Offset: 0x000D484C
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
			IEqualityComparer<TKey> comparer = tables.m_comparer;
			int num;
			int num2;
			this.GetBucketAndLockNo(comparer.GetHashCode(key), out num, out num2, tables.m_buckets.Length, tables.m_locks.Length);
			for (ConcurrentDictionary<TKey, TValue>.Node node = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[num]); node != null; node = node.m_next)
			{
				if (comparer.Equals(node.m_key, key))
				{
					value = node.m_value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000D66E8 File Offset: 0x000D48E8
		[__DynamicallyInvokable]
		public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			bool result;
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
				IEqualityComparer<TKey> comparer = tables.m_comparer;
				int hashCode = comparer.GetHashCode(key);
				int num;
				int num2;
				this.GetBucketAndLockNo(hashCode, out num, out num2, tables.m_buckets.Length, tables.m_locks.Length);
				object obj = tables.m_locks[num2];
				lock (obj)
				{
					if (tables != this.m_tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[num];
					while (node2 != null)
					{
						if (comparer.Equals(node2.m_key, key))
						{
							if (@default.Equals(node2.m_value, comparisonValue))
							{
								if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
								{
									node2.m_value = newValue;
								}
								else
								{
									ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, newValue, hashCode, node2.m_next);
									if (node == null)
									{
										tables.m_buckets[num] = node3;
									}
									else
									{
										node.m_next = node3;
									}
								}
								return true;
							}
							return false;
						}
						else
						{
							node = node2;
							node2 = node2.m_next;
						}
					}
					result = false;
				}
				break;
			}
			return result;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000D682C File Offset: 0x000D4A2C
		[__DynamicallyInvokable]
		public void Clear()
		{
			int toExclusive = 0;
			try
			{
				this.AcquireAllLocks(ref toExclusive);
				ConcurrentDictionary<TKey, TValue>.Tables tables = new ConcurrentDictionary<TKey, TValue>.Tables(new ConcurrentDictionary<TKey, TValue>.Node[31], this.m_tables.m_locks, new int[this.m_tables.m_countPerLock.Length], this.m_tables.m_comparer);
				this.m_tables = tables;
				this.m_budget = Math.Max(1, tables.m_buckets.Length / tables.m_locks.Length);
			}
			finally
			{
				this.ReleaseLocks(0, toExclusive);
			}
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000D68C4 File Offset: 0x000D4AC4
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentDictionary_IndexIsNegative"));
			}
			int toExclusive = 0;
			try
			{
				this.AcquireAllLocks(ref toExclusive);
				int num = 0;
				int num2 = 0;
				while (num2 < this.m_tables.m_locks.Length && num >= 0)
				{
					num += this.m_tables.m_countPerLock[num2];
					num2++;
				}
				if (array.Length - num < index || num < 0)
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
				}
				this.CopyToPairs(array, index);
			}
			finally
			{
				this.ReleaseLocks(0, toExclusive);
			}
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000D6978 File Offset: 0x000D4B78
		[__DynamicallyInvokable]
		public KeyValuePair<TKey, TValue>[] ToArray()
		{
			int toExclusive = 0;
			checked
			{
				KeyValuePair<TKey, TValue>[] result;
				try
				{
					this.AcquireAllLocks(ref toExclusive);
					int num = 0;
					for (int i = 0; i < this.m_tables.m_locks.Length; i++)
					{
						num += this.m_tables.m_countPerLock[i];
					}
					if (num == 0)
					{
						result = Array.Empty<KeyValuePair<TKey, TValue>>();
					}
					else
					{
						KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[num];
						this.CopyToPairs(array, 0);
						result = array;
					}
				}
				finally
				{
					this.ReleaseLocks(0, toExclusive);
				}
				return result;
			}
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000D69FC File Offset: 0x000D4BFC
		private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_tables.m_buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000D6A54 File Offset: 0x000D4C54
		private void CopyToEntries(DictionaryEntry[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_tables.m_buckets)
			{
				while (node != null)
				{
					array[index] = new DictionaryEntry(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000D6AB8 File Offset: 0x000D4CB8
		private void CopyToObjects(object[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_tables.m_buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000D6B11 File Offset: 0x000D4D11
		[__DynamicallyInvokable]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			ConcurrentDictionary<TKey, TValue>.Node[] buckets = this.m_tables.m_buckets;
			int num;
			for (int i = 0; i < buckets.Length; i = num + 1)
			{
				ConcurrentDictionary<TKey, TValue>.Node current;
				for (current = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref buckets[i]); current != null; current = current.m_next)
				{
					yield return new KeyValuePair<TKey, TValue>(current.m_key, current.m_value);
				}
				current = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000D6B20 File Offset: 0x000D4D20
		private bool TryAddInternal(TKey key, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables;
			IEqualityComparer<TKey> comparer;
			bool flag;
			bool flag3;
			for (;;)
			{
				tables = this.m_tables;
				comparer = tables.m_comparer;
				int hashCode = comparer.GetHashCode(key);
				int num;
				int num2;
				this.GetBucketAndLockNo(hashCode, out num, out num2, tables.m_buckets.Length, tables.m_locks.Length);
				flag = false;
				bool flag2 = false;
				flag3 = false;
				try
				{
					if (acquireLock)
					{
						Monitor.Enter(tables.m_locks[num2], ref flag2);
					}
					if (tables != this.m_tables)
					{
						continue;
					}
					int num3 = 0;
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[num]; node2 != null; node2 = node2.m_next)
					{
						if (comparer.Equals(node2.m_key, key))
						{
							if (updateIfExists)
							{
								if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
								{
									node2.m_value = value;
								}
								else
								{
									ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, value, hashCode, node2.m_next);
									if (node == null)
									{
										tables.m_buckets[num] = node3;
									}
									else
									{
										node.m_next = node3;
									}
								}
								resultingValue = value;
							}
							else
							{
								resultingValue = node2.m_value;
							}
							return false;
						}
						node = node2;
						num3++;
					}
					if (num3 > 100 && HashHelpers.IsWellKnownEqualityComparer(comparer))
					{
						flag = true;
						flag3 = true;
					}
					Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[num], new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashCode, tables.m_buckets[num]));
					checked
					{
						tables.m_countPerLock[num2]++;
						if (tables.m_countPerLock[num2] > this.m_budget)
						{
							flag = true;
						}
					}
				}
				finally
				{
					if (flag2)
					{
						Monitor.Exit(tables.m_locks[num2]);
					}
				}
				break;
			}
			if (flag)
			{
				if (flag3)
				{
					this.GrowTable(tables, (IEqualityComparer<TKey>)HashHelpers.GetRandomizedEqualityComparer(comparer), true, this.m_keyRehashCount);
				}
				else
				{
					this.GrowTable(tables, tables.m_comparer, false, this.m_keyRehashCount);
				}
			}
			resultingValue = value;
			return true;
		}

		// Token: 0x17000877 RID: 2167
		[__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				TValue result;
				if (!this.TryGetValue(key, out result))
				{
					throw new KeyNotFoundException();
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				this.TryAddInternal(key, value, true, true, out tvalue);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x000D6D58 File Offset: 0x000D4F58
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				int toExclusive = 0;
				int countInternal;
				try
				{
					this.AcquireAllLocks(ref toExclusive);
					countInternal = this.GetCountInternal();
				}
				finally
				{
					this.ReleaseLocks(0, toExclusive);
				}
				return countInternal;
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000D6D94 File Offset: 0x000D4F94
		private int GetCountInternal()
		{
			int num = 0;
			for (int i = 0; i < this.m_tables.m_countPerLock.Length; i++)
			{
				num += this.m_tables.m_countPerLock[i];
			}
			return num;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000D6DD4 File Offset: 0x000D4FD4
		[__DynamicallyInvokable]
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			TValue result;
			if (this.TryGetValue(key, out result))
			{
				return result;
			}
			this.TryAddInternal(key, valueFactory(key), false, true, out result);
			return result;
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000D6E24 File Offset: 0x000D5024
		[__DynamicallyInvokable]
		public TValue GetOrAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue result;
			this.TryAddInternal(key, value, false, true, out result);
			return result;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000D6E54 File Offset: 0x000D5054
		public TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			TValue result;
			if (!this.TryGetValue(key, out result))
			{
				this.TryAddInternal(key, valueFactory(key, factoryArgument), false, true, out result);
			}
			return result;
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000D6EA4 File Offset: 0x000D50A4
		public TValue AddOrUpdate<TArg>(TKey key, Func<TKey, TArg, TValue> addValueFactory, Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue result;
				if (this.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue, factoryArgument);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, addValueFactory(key, factoryArgument), false, true, out result))
				{
					return result;
				}
			}
			return tvalue2;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000D6F1C File Offset: 0x000D511C
		[__DynamicallyInvokable]
		public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else
				{
					tvalue2 = addValueFactory(key);
					TValue result;
					if (this.TryAddInternal(key, tvalue2, false, true, out result))
					{
						return result;
					}
				}
			}
			return tvalue2;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000D6F90 File Offset: 0x000D5190
		[__DynamicallyInvokable]
		public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue result;
				if (this.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, addValue, false, true, out result))
				{
					return result;
				}
			}
			return tvalue2;
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x000D6FF0 File Offset: 0x000D51F0
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				int toExclusive = 0;
				try
				{
					this.AcquireAllLocks(ref toExclusive);
					for (int i = 0; i < this.m_tables.m_countPerLock.Length; i++)
					{
						if (this.m_tables.m_countPerLock[i] != 0)
						{
							return false;
						}
					}
				}
				finally
				{
					this.ReleaseLocks(0, toExclusive);
				}
				return true;
			}
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000D7058 File Offset: 0x000D5258
		[__DynamicallyInvokable]
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			if (!this.TryAdd(key, value))
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_KeyAlreadyExisted"));
			}
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000D7078 File Offset: 0x000D5278
		[__DynamicallyInvokable]
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			TValue tvalue;
			return this.TryRemove(key, out tvalue);
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000D708E File Offset: 0x000D528E
		[__DynamicallyInvokable]
		public ICollection<TKey> Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000D7096 File Offset: 0x000D5296
		[__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<!0, !1>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x000D709E File Offset: 0x000D529E
		[__DynamicallyInvokable]
		public ICollection<TValue> Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x000D70A6 File Offset: 0x000D52A6
		[__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<!0, !1>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000D70AE File Offset: 0x000D52AE
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			((IDictionary<!0, !1>)this).Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000D70C4 File Offset: 0x000D52C4
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			TValue x;
			return this.TryGetValue(keyValuePair.Key, out x) && EqualityComparer<TValue>.Default.Equals(x, keyValuePair.Value);
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000D70F6 File Offset: 0x000D52F6
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000D70FC File Offset: 0x000D52FC
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			if (keyValuePair.Key == null)
			{
				throw new ArgumentNullException(this.GetResource("ConcurrentDictionary_ItemKeyIsNull"));
			}
			TValue tvalue;
			return this.TryRemoveInternal(keyValuePair.Key, out tvalue, true, keyValuePair.Value);
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000D713F File Offset: 0x000D533F
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000D7148 File Offset: 0x000D5348
		[__DynamicallyInvokable]
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
			}
			TValue value2;
			try
			{
				value2 = (TValue)((object)value);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
			}
			((IDictionary<!0, !1>)this).Add((TKey)((object)key), value2);
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000D71B8 File Offset: 0x000D53B8
		[__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000D71DE File Offset: 0x000D53DE
		[__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x000D71E6 File Offset: 0x000D53E6
		[__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x000D71E9 File Offset: 0x000D53E9
		[__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000D71EC File Offset: 0x000D53EC
		[__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000D71F4 File Offset: 0x000D53F4
		[__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key is TKey)
			{
				TValue tvalue;
				this.TryRemove((TKey)((object)key), out tvalue);
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000D7226 File Offset: 0x000D5426
		[__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x17000883 RID: 2179
		[__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				if (key is TKey && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (!(key is TKey))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
				}
				if (!(value is TValue))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
				}
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000D72CC File Offset: 0x000D54CC
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentDictionary_IndexIsNegative"));
			}
			int toExclusive = 0;
			try
			{
				this.AcquireAllLocks(ref toExclusive);
				ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
				int num = 0;
				int num2 = 0;
				while (num2 < tables.m_locks.Length && num >= 0)
				{
					num += tables.m_countPerLock[num2];
					num2++;
				}
				if (array.Length - num < index || num < 0)
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
				}
				KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
				if (array2 != null)
				{
					this.CopyToPairs(array2, index);
				}
				else
				{
					DictionaryEntry[] array3 = array as DictionaryEntry[];
					if (array3 != null)
					{
						this.CopyToEntries(array3, index);
					}
					else
					{
						object[] array4 = array as object[];
						if (array4 == null)
						{
							throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayIncorrectType"), "array");
						}
						this.CopyToObjects(array4, index);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, toExclusive);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x000D73D0 File Offset: 0x000D55D0
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000D73D3 File Offset: 0x000D55D3
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000D73E4 File Offset: 0x000D55E4
		private void GrowTable(ConcurrentDictionary<TKey, TValue>.Tables tables, IEqualityComparer<TKey> newComparer, bool regenerateHashKeys, int rehashCount)
		{
			int toExclusive = 0;
			try
			{
				this.AcquireLocks(0, 1, ref toExclusive);
				if (regenerateHashKeys && rehashCount == this.m_keyRehashCount)
				{
					tables = this.m_tables;
				}
				else
				{
					if (tables != this.m_tables)
					{
						return;
					}
					long num = 0L;
					for (int i = 0; i < tables.m_countPerLock.Length; i++)
					{
						num += (long)tables.m_countPerLock[i];
					}
					if (num < (long)(tables.m_buckets.Length / 4))
					{
						this.m_budget = 2 * this.m_budget;
						if (this.m_budget < 0)
						{
							this.m_budget = int.MaxValue;
						}
						return;
					}
				}
				int num2 = 0;
				bool flag = false;
				object[] array;
				checked
				{
					try
					{
						num2 = tables.m_buckets.Length * 2 + 1;
						while (num2 % 3 == 0 || num2 % 5 == 0 || num2 % 7 == 0)
						{
							num2 += 2;
						}
						if (num2 > 2146435071)
						{
							flag = true;
						}
					}
					catch (OverflowException)
					{
						flag = true;
					}
					if (flag)
					{
						num2 = 2146435071;
						this.m_budget = int.MaxValue;
					}
					this.AcquireLocks(1, tables.m_locks.Length, ref toExclusive);
					array = tables.m_locks;
				}
				if (this.m_growLockArray && tables.m_locks.Length < 1024)
				{
					array = new object[tables.m_locks.Length * 2];
					Array.Copy(tables.m_locks, array, tables.m_locks.Length);
					for (int j = tables.m_locks.Length; j < array.Length; j++)
					{
						array[j] = new object();
					}
				}
				ConcurrentDictionary<TKey, TValue>.Node[] array2 = new ConcurrentDictionary<TKey, TValue>.Node[num2];
				int[] array3 = new int[array.Length];
				for (int k = 0; k < tables.m_buckets.Length; k++)
				{
					checked
					{
						ConcurrentDictionary<TKey, TValue>.Node next;
						for (ConcurrentDictionary<TKey, TValue>.Node node = tables.m_buckets[k]; node != null; node = next)
						{
							next = node.m_next;
							int hashcode = node.m_hashcode;
							if (regenerateHashKeys)
							{
								hashcode = newComparer.GetHashCode(node.m_key);
							}
							int num3;
							int num4;
							this.GetBucketAndLockNo(hashcode, out num3, out num4, array2.Length, array.Length);
							array2[num3] = new ConcurrentDictionary<TKey, TValue>.Node(node.m_key, node.m_value, hashcode, array2[num3]);
							array3[num4]++;
						}
					}
				}
				if (regenerateHashKeys)
				{
					this.m_keyRehashCount++;
				}
				this.m_budget = Math.Max(1, array2.Length / array.Length);
				this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(array2, array, array3, newComparer);
			}
			finally
			{
				this.ReleaseLocks(0, toExclusive);
			}
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000D766C File Offset: 0x000D586C
		private void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % lockCount;
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000D7681 File Offset: 0x000D5881
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return PlatformHelper.ProcessorCount;
			}
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000D7688 File Offset: 0x000D5888
		private void AcquireAllLocks(ref int locksAcquired)
		{
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentDictionary_AcquiringAllLocks(this.m_tables.m_buckets.Length);
			}
			this.AcquireLocks(0, 1, ref locksAcquired);
			this.AcquireLocks(1, this.m_tables.m_locks.Length, ref locksAcquired);
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000D76DC File Offset: 0x000D58DC
		private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
		{
			object[] locks = this.m_tables.m_locks;
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				bool flag = false;
				try
				{
					Monitor.Enter(locks[i], ref flag);
				}
				finally
				{
					if (flag)
					{
						locksAcquired++;
					}
				}
			}
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000D772C File Offset: 0x000D592C
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this.m_tables.m_locks[i]);
			}
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000D775C File Offset: 0x000D595C
		private ReadOnlyCollection<TKey> GetKeys()
		{
			int toExclusive = 0;
			ReadOnlyCollection<TKey> result;
			try
			{
				this.AcquireAllLocks(ref toExclusive);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TKey> list = new List<TKey>(countInternal);
				for (int i = 0; i < this.m_tables.m_buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_tables.m_buckets[i]; node != null; node = node.m_next)
					{
						list.Add(node.m_key);
					}
				}
				result = new ReadOnlyCollection<TKey>(list);
			}
			finally
			{
				this.ReleaseLocks(0, toExclusive);
			}
			return result;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000D77FC File Offset: 0x000D59FC
		private ReadOnlyCollection<TValue> GetValues()
		{
			int toExclusive = 0;
			ReadOnlyCollection<TValue> result;
			try
			{
				this.AcquireAllLocks(ref toExclusive);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TValue> list = new List<TValue>(countInternal);
				for (int i = 0; i < this.m_tables.m_buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_tables.m_buckets[i]; node != null; node = node.m_next)
					{
						list.Add(node.m_value);
					}
				}
				result = new ReadOnlyCollection<TValue>(list);
			}
			finally
			{
				this.ReleaseLocks(0, toExclusive);
			}
			return result;
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000D789C File Offset: 0x000D5A9C
		[Conditional("DEBUG")]
		private void Assert(bool condition)
		{
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000D789E File Offset: 0x000D5A9E
		private string GetResource(string key)
		{
			return Environment.GetResourceString(key);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000D78A8 File Offset: 0x000D5AA8
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
			this.m_serializationArray = this.ToArray();
			this.m_serializationConcurrencyLevel = tables.m_locks.Length;
			this.m_serializationCapacity = tables.m_buckets.Length;
			this.m_comparer = (IEqualityComparer<TKey>)HashHelpers.GetEqualityComparerForSerialization(tables.m_comparer);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000D78FC File Offset: 0x000D5AFC
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			KeyValuePair<TKey, TValue>[] serializationArray = this.m_serializationArray;
			ConcurrentDictionary<TKey, TValue>.Node[] buckets = new ConcurrentDictionary<TKey, TValue>.Node[this.m_serializationCapacity];
			int[] countPerLock = new int[this.m_serializationConcurrencyLevel];
			object[] array = new object[this.m_serializationConcurrencyLevel];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new object();
			}
			this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(buckets, array, countPerLock, this.m_comparer);
			this.InitializeFromCollection(serializationArray);
			this.m_serializationArray = null;
		}

		// Token: 0x04001860 RID: 6240
		[NonSerialized]
		private volatile ConcurrentDictionary<TKey, TValue>.Tables m_tables;

		// Token: 0x04001861 RID: 6241
		internal IEqualityComparer<TKey> m_comparer;

		// Token: 0x04001862 RID: 6242
		[NonSerialized]
		private readonly bool m_growLockArray;

		// Token: 0x04001863 RID: 6243
		[OptionalField]
		private int m_keyRehashCount;

		// Token: 0x04001864 RID: 6244
		[NonSerialized]
		private int m_budget;

		// Token: 0x04001865 RID: 6245
		private KeyValuePair<TKey, TValue>[] m_serializationArray;

		// Token: 0x04001866 RID: 6246
		private int m_serializationConcurrencyLevel;

		// Token: 0x04001867 RID: 6247
		private int m_serializationCapacity;

		// Token: 0x04001868 RID: 6248
		private const int DEFAULT_CAPACITY = 31;

		// Token: 0x04001869 RID: 6249
		private const int MAX_LOCK_NUMBER = 1024;

		// Token: 0x0400186A RID: 6250
		private static readonly bool s_isValueWriteAtomic = ConcurrentDictionary<TKey, TValue>.IsValueWriteAtomic();

		// Token: 0x02000B8D RID: 2957
		private class Tables
		{
			// Token: 0x06006D44 RID: 27972 RVA: 0x00178471 File Offset: 0x00176671
			internal Tables(ConcurrentDictionary<TKey, TValue>.Node[] buckets, object[] locks, int[] countPerLock, IEqualityComparer<TKey> comparer)
			{
				this.m_buckets = buckets;
				this.m_locks = locks;
				this.m_countPerLock = countPerLock;
				this.m_comparer = comparer;
			}

			// Token: 0x040034AB RID: 13483
			internal readonly ConcurrentDictionary<TKey, TValue>.Node[] m_buckets;

			// Token: 0x040034AC RID: 13484
			internal readonly object[] m_locks;

			// Token: 0x040034AD RID: 13485
			internal volatile int[] m_countPerLock;

			// Token: 0x040034AE RID: 13486
			internal readonly IEqualityComparer<TKey> m_comparer;
		}

		// Token: 0x02000B8E RID: 2958
		private class Node
		{
			// Token: 0x06006D45 RID: 27973 RVA: 0x00178498 File Offset: 0x00176698
			internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
			{
				this.m_key = key;
				this.m_value = value;
				this.m_next = next;
				this.m_hashcode = hashcode;
			}

			// Token: 0x040034AF RID: 13487
			internal TKey m_key;

			// Token: 0x040034B0 RID: 13488
			internal TValue m_value;

			// Token: 0x040034B1 RID: 13489
			internal volatile ConcurrentDictionary<TKey, TValue>.Node m_next;

			// Token: 0x040034B2 RID: 13490
			internal int m_hashcode;
		}

		// Token: 0x02000B8F RID: 2959
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006D46 RID: 27974 RVA: 0x001784BF File Offset: 0x001766BF
			internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
			{
				this.m_enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x170012C2 RID: 4802
			// (get) Token: 0x06006D47 RID: 27975 RVA: 0x001784D4 File Offset: 0x001766D4
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					object key = keyValuePair.Key;
					keyValuePair = this.m_enumerator.Current;
					return new DictionaryEntry(key, keyValuePair.Value);
				}
			}

			// Token: 0x170012C3 RID: 4803
			// (get) Token: 0x06006D48 RID: 27976 RVA: 0x00178518 File Offset: 0x00176718
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170012C4 RID: 4804
			// (get) Token: 0x06006D49 RID: 27977 RVA: 0x00178540 File Offset: 0x00176740
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170012C5 RID: 4805
			// (get) Token: 0x06006D4A RID: 27978 RVA: 0x00178565 File Offset: 0x00176765
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06006D4B RID: 27979 RVA: 0x00178572 File Offset: 0x00176772
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06006D4C RID: 27980 RVA: 0x0017857F File Offset: 0x0017677F
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x040034B3 RID: 13491
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}
	}
}
