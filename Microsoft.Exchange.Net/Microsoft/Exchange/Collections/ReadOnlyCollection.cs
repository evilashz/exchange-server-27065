using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public sealed class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable, ISerializable
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00008C53 File Offset: 0x00006E53
		public ReadOnlyCollection(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this.list = list;
			this.array = (list as T[]);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008C7C File Offset: 0x00006E7C
		private ReadOnlyCollection(SerializationInfo info, StreamingContext context)
		{
			this.list = (IList<T>)info.GetValue("list", typeof(IList<T>));
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008CA7 File Offset: 0x00006EA7
		public int Count
		{
			get
			{
				if (this.array != null)
				{
					return this.array.Length;
				}
				return this.list.Count;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00008CC5 File Offset: 0x00006EC5
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00008CC8 File Offset: 0x00006EC8
		public bool IsSynchronized
		{
			get
			{
				ICollection collection = this.list as ICollection;
				return collection != null && collection.IsSynchronized;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00008CEC File Offset: 0x00006EEC
		public object SyncRoot
		{
			get
			{
				ICollection collection = this.list as ICollection;
				if (collection != null)
				{
					return collection.SyncRoot;
				}
				throw new NotSupportedException("Internal collection does not implement ICollection.");
			}
		}

		// Token: 0x17000059 RID: 89
		public T this[int index]
		{
			get
			{
				if (this.array != null)
				{
					return this.array[index];
				}
				return this.list[index];
			}
		}

		// Token: 0x1700005A RID: 90
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x1700005B RID: 91
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008D6B File Offset: 0x00006F6B
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008D77 File Offset: 0x00006F77
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008D83 File Offset: 0x00006F83
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008D8F File Offset: 0x00006F8F
		void IList.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008D9B File Offset: 0x00006F9B
		public bool Contains(T item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008DA9 File Offset: 0x00006FA9
		bool IList.Contains(object value)
		{
			return ((IList)this.list).Contains(value);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008DBC File Offset: 0x00006FBC
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008DCB File Offset: 0x00006FCB
		public void CopyTo(Array array, int index)
		{
			this.CopyTo(array as T[], index);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008DDA File Offset: 0x00006FDA
		public ReadOnlyCollection<T>.Enumerator GetEnumerator()
		{
			if (this.array != null)
			{
				return new ReadOnlyCollection<T>.Enumerator(this.array);
			}
			return new ReadOnlyCollection<T>.Enumerator(this.list);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008DFB File Offset: 0x00006FFB
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008E08 File Offset: 0x00007008
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008E15 File Offset: 0x00007015
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("list", this.list, typeof(IList<!0>));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008E32 File Offset: 0x00007032
		public int IndexOf(T item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008E40 File Offset: 0x00007040
		int IList.IndexOf(object value)
		{
			return ((IList)this.list).IndexOf(value);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008E53 File Offset: 0x00007053
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008E5F File Offset: 0x0000705F
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008E6B File Offset: 0x0000706B
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008E77 File Offset: 0x00007077
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008E83 File Offset: 0x00007083
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008E8F File Offset: 0x0000708F
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0400012D RID: 301
		private const string ListSerializationName = "list";

		// Token: 0x0400012E RID: 302
		private IList<T> list;

		// Token: 0x0400012F RID: 303
		private T[] array;

		// Token: 0x02000041 RID: 65
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060001B6 RID: 438 RVA: 0x00008E9B File Offset: 0x0000709B
			internal Enumerator(IList<T> list)
			{
				this.currentIndex = -1;
				this.currentItem = default(T);
				this.list = list;
				this.array = null;
				this.count = list.Count;
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x00008ECA File Offset: 0x000070CA
			internal Enumerator(T[] array)
			{
				this.currentIndex = -1;
				this.currentItem = default(T);
				this.list = null;
				this.array = array;
				this.count = array.Length;
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008EF6 File Offset: 0x000070F6
			public T Current
			{
				get
				{
					return this.currentItem;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008EFE File Offset: 0x000070FE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060001BA RID: 442 RVA: 0x00008F0B File Offset: 0x0000710B
			public void Dispose()
			{
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00008F10 File Offset: 0x00007110
			public bool MoveNext()
			{
				if (++this.currentIndex < this.count)
				{
					this.currentItem = ((this.array != null) ? this.array[this.currentIndex] : this.list[this.currentIndex]);
					return true;
				}
				return false;
			}

			// Token: 0x060001BC RID: 444 RVA: 0x00008F6B File Offset: 0x0000716B
			public void Reset()
			{
				this.currentIndex = -1;
				this.currentItem = default(T);
			}

			// Token: 0x04000130 RID: 304
			private int currentIndex;

			// Token: 0x04000131 RID: 305
			private T currentItem;

			// Token: 0x04000132 RID: 306
			private IList<T> list;

			// Token: 0x04000133 RID: 307
			private T[] array;

			// Token: 0x04000134 RID: 308
			private int count;
		}
	}
}
