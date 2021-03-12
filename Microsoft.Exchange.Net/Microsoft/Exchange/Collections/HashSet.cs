using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200003B RID: 59
	[ComVisible(false)]
	[Serializable]
	internal class HashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00007DE9 File Offset: 0x00005FE9
		public HashSet() : this(0, null)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007DF3 File Offset: 0x00005FF3
		public HashSet(int capacity) : this(capacity, null)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007DFD File Offset: 0x00005FFD
		public HashSet(IEqualityComparer<T> comparer) : this(0, comparer)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007E07 File Offset: 0x00006007
		public HashSet(int capacity, IEqualityComparer<T> comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
			if (comparer != null)
			{
				this.comparer = comparer;
				return;
			}
			this.comparer = EqualityComparer<T>.Default;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007E3F File Offset: 0x0000603F
		public HashSet(ICollection<T> collection) : this(collection, null)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007E4C File Offset: 0x0000604C
		public HashSet(ICollection<T> collection, IEqualityComparer<T> comparer) : this((collection != null) ? collection.Count : 0, comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (T item in collection)
			{
				this.Add(item);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007EB8 File Offset: 0x000060B8
		public int Count
		{
			get
			{
				return this.usedEntriesCount - this.numberOfEntriesInFreeList;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007EC7 File Offset: 0x000060C7
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007ECA File Offset: 0x000060CA
		public void Add(T item)
		{
			if (!this.TryAdd(item))
			{
				throw new ArgumentException(NetException.DuplicateItem);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007EE8 File Offset: 0x000060E8
		public bool TryAdd(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.buckets == null)
			{
				this.Initialize(0);
			}
			int hashCode = this.GetHashCode(item);
			int num = hashCode % this.buckets.Length;
			for (int i = this.buckets[num]; i >= 0; i = this.entries[i].next)
			{
				if (this.entries[i].hashCode == hashCode && this.comparer.Equals(this.entries[i].item, item))
				{
					return false;
				}
			}
			bool flag;
			int freeEntry = this.GetFreeEntry(out flag);
			if (flag)
			{
				num = hashCode % this.buckets.Length;
			}
			this.entries[freeEntry].hashCode = hashCode;
			this.entries[freeEntry].next = this.buckets[num];
			this.entries[freeEntry].item = item;
			this.buckets[num] = freeEntry;
			this.version++;
			return true;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007FF4 File Offset: 0x000061F4
		public void Clear()
		{
			if (this.usedEntriesCount > 0)
			{
				for (int i = 0; i < this.buckets.Length; i++)
				{
					this.buckets[i] = -1;
				}
				Array.Clear(this.entries, 0, this.usedEntriesCount);
				this.freeListStartIndex = -1;
				this.usedEntriesCount = 0;
				this.numberOfEntriesInFreeList = 0;
				this.version++;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000805C File Offset: 0x0000625C
		public bool Contains(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.buckets != null)
			{
				int hashCode = this.GetHashCode(item);
				for (int i = this.buckets[hashCode % this.buckets.Length]; i >= 0; i = this.entries[i].next)
				{
					if (this.entries[i].hashCode == hashCode && this.comparer.Equals(this.entries[i].item, item))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000080EC File Offset: 0x000062EC
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index > array.Length - this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			for (int i = 0; i < this.usedEntriesCount; i++)
			{
				if (this.entries[i].hashCode >= 0)
				{
					array[index++] = this.entries[i].item;
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008165 File Offset: 0x00006365
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008170 File Offset: 0x00006370
		public bool Remove(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.buckets == null)
			{
				return false;
			}
			int hashCode = this.GetHashCode(item);
			int num = hashCode % this.buckets.Length;
			int num2 = -1;
			for (int i = this.buckets[num]; i >= 0; i = this.entries[i].next)
			{
				if (this.entries[i].hashCode == hashCode && this.comparer.Equals(this.entries[i].item, item))
				{
					if (num2 < 0)
					{
						this.buckets[num] = this.entries[i].next;
					}
					else
					{
						this.entries[num2].next = this.entries[i].next;
					}
					this.PutEntryIntoFreeList(i);
					this.version++;
					return true;
				}
				num2 = i;
			}
			return false;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008264 File Offset: 0x00006464
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008271 File Offset: 0x00006471
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008280 File Offset: 0x00006480
		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000082A4 File Offset: 0x000064A4
		private static int[] AllocateBuckets(int size)
		{
			int[] array = new int[size];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			return array;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000082CC File Offset: 0x000064CC
		private void Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this.buckets = HashSet<T>.AllocateBuckets(prime);
			this.entries = new HashSet<T>.Entry[prime];
			this.freeListStartIndex = -1;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008300 File Offset: 0x00006500
		private void Resize()
		{
			int prime = HashHelpers.GetPrime(this.usedEntriesCount * 2);
			int[] array = HashSet<T>.AllocateBuckets(prime);
			HashSet<T>.Entry[] array2 = new HashSet<T>.Entry[prime];
			Array.Copy(this.entries, 0, array2, 0, this.usedEntriesCount);
			for (int i = 0; i < this.usedEntriesCount; i++)
			{
				int num = array2[i].hashCode % prime;
				array2[i].next = array[num];
				array[num] = i;
			}
			this.buckets = array;
			this.entries = array2;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008384 File Offset: 0x00006584
		private void PutEntryIntoFreeList(int entryIndex)
		{
			this.entries[entryIndex].hashCode = -1;
			this.entries[entryIndex].next = this.freeListStartIndex;
			this.entries[entryIndex].item = default(T);
			this.freeListStartIndex = entryIndex;
			this.numberOfEntriesInFreeList++;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000083E8 File Offset: 0x000065E8
		private int GetFreeEntry(out bool sizeChanged)
		{
			sizeChanged = false;
			int result;
			if (this.numberOfEntriesInFreeList > 0)
			{
				result = this.freeListStartIndex;
				this.freeListStartIndex = this.entries[this.freeListStartIndex].next;
				this.numberOfEntriesInFreeList--;
			}
			else
			{
				if (this.usedEntriesCount == this.entries.Length)
				{
					this.Resize();
					sizeChanged = true;
				}
				result = this.usedEntriesCount;
				this.usedEntriesCount++;
			}
			return result;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008463 File Offset: 0x00006663
		private int GetHashCode(T item)
		{
			return this.comparer.GetHashCode(item) & int.MaxValue;
		}

		// Token: 0x0400010D RID: 269
		private int[] buckets;

		// Token: 0x0400010E RID: 270
		private HashSet<T>.Entry[] entries;

		// Token: 0x0400010F RID: 271
		private int usedEntriesCount;

		// Token: 0x04000110 RID: 272
		private int freeListStartIndex;

		// Token: 0x04000111 RID: 273
		private int numberOfEntriesInFreeList;

		// Token: 0x04000112 RID: 274
		private int version;

		// Token: 0x04000113 RID: 275
		private IEqualityComparer<T> comparer;

		// Token: 0x0200003C RID: 60
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000181 RID: 385 RVA: 0x00008477 File Offset: 0x00006677
			internal Enumerator(HashSet<T> set)
			{
				this.set = set;
				this.version = set.version;
				this.currentEntryIndex = 0;
				this.currentItem = default(T);
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000182 RID: 386 RVA: 0x0000849F File Offset: 0x0000669F
			public T Current
			{
				get
				{
					return this.currentItem;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000183 RID: 387 RVA: 0x000084A7 File Offset: 0x000066A7
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000184 RID: 388 RVA: 0x000084B4 File Offset: 0x000066B4
			public bool MoveNext()
			{
				if (this.version != this.set.version)
				{
					throw new InvalidOperationException(NetException.CollectionChanged);
				}
				while (this.currentEntryIndex < this.set.usedEntriesCount)
				{
					if (this.set.entries[this.currentEntryIndex].hashCode >= 0)
					{
						this.currentItem = this.set.entries[this.currentEntryIndex].item;
						this.currentEntryIndex++;
						return true;
					}
					this.currentEntryIndex++;
				}
				this.currentEntryIndex = this.set.usedEntriesCount + 1;
				this.currentItem = default(T);
				return false;
			}

			// Token: 0x06000185 RID: 389 RVA: 0x00008574 File Offset: 0x00006774
			public void Dispose()
			{
			}

			// Token: 0x06000186 RID: 390 RVA: 0x00008576 File Offset: 0x00006776
			void IEnumerator.Reset()
			{
				this.currentEntryIndex = 0;
				this.currentItem = default(T);
			}

			// Token: 0x04000114 RID: 276
			private HashSet<T> set;

			// Token: 0x04000115 RID: 277
			private int version;

			// Token: 0x04000116 RID: 278
			private int currentEntryIndex;

			// Token: 0x04000117 RID: 279
			private T currentItem;
		}

		// Token: 0x0200003D RID: 61
		[Serializable]
		private struct Entry
		{
			// Token: 0x04000118 RID: 280
			public int hashCode;

			// Token: 0x04000119 RID: 281
			public int next;

			// Token: 0x0400011A RID: 282
			public T item;
		}
	}
}
