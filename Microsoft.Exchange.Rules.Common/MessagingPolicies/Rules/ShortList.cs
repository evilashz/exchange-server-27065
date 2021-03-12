using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class ShortList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00009944 File Offset: 0x00007B44
		public ShortList()
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000994C File Offset: 0x00007B4C
		public ShortList(IEnumerable<T> enumerable) : this()
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			foreach (T item in enumerable)
			{
				this.Add(item);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000099A8 File Offset: 0x00007BA8
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000099B0 File Offset: 0x00007BB0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000099B3 File Offset: 0x00007BB3
		private List<T> List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new List<T>();
				}
				return this.list;
			}
		}

		// Token: 0x17000091 RID: 145
		public T this[int index]
		{
			get
			{
				if (index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index", "Index is out of range");
				}
				if (index > 0)
				{
					return this.list[index - 1];
				}
				return this.firstItem;
			}
			set
			{
				if (index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index", "Index is out of range");
				}
				if (index > 0)
				{
					this.list[index - 1] = value;
				}
				else
				{
					this.firstItem = value;
				}
				this.version++;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009A54 File Offset: 0x00007C54
		public int IndexOf(T item)
		{
			if (this.count == 0)
			{
				return -1;
			}
			if (ShortList<T>.comparer.Equals(this.firstItem, item))
			{
				return 0;
			}
			if (this.list != null)
			{
				int num = this.list.IndexOf(item);
				if (num != -1)
				{
					return num + 1;
				}
			}
			return -1;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public void Insert(int index, T item)
		{
			if (index > this.count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is out of range");
			}
			if (index > 0)
			{
				this.List.Insert(index - 1, item);
			}
			else
			{
				if (this.count > 0)
				{
					this.List.Insert(0, this.firstItem);
				}
				this.firstItem = item;
			}
			this.count++;
			this.version++;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009B1C File Offset: 0x00007D1C
		public void RemoveAt(int index)
		{
			if (index >= this.count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is out of range");
			}
			if (index > 0)
			{
				this.list.RemoveAt(index - 1);
			}
			else if (this.count > 1)
			{
				this.firstItem = this.list[0];
				this.list.RemoveAt(0);
			}
			else
			{
				this.firstItem = default(T);
			}
			this.count--;
			this.version++;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009BA7 File Offset: 0x00007DA7
		public void Add(T item)
		{
			this.Insert(this.count, item);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009BB6 File Offset: 0x00007DB6
		public void Clear()
		{
			if (this.list != null)
			{
				this.list.Clear();
			}
			this.firstItem = default(T);
			this.count = 0;
			this.version++;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009BEC File Offset: 0x00007DEC
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009BFC File Offset: 0x00007DFC
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length - arrayIndex < this.count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this.count == 0)
			{
				return;
			}
			array[arrayIndex] = this.firstItem;
			if (this.list != null)
			{
				this.list.CopyTo(array, arrayIndex + 1);
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009C5C File Offset: 0x00007E5C
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num != -1)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009C7F File Offset: 0x00007E7F
		public ShortList<T>.Enumerator GetEnumerator()
		{
			return new ShortList<T>.Enumerator(this);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009C87 File Offset: 0x00007E87
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009C94 File Offset: 0x00007E94
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000FC RID: 252
		private static EqualityComparer<T> comparer = EqualityComparer<T>.Default;

		// Token: 0x040000FD RID: 253
		private int count;

		// Token: 0x040000FE RID: 254
		private int version;

		// Token: 0x040000FF RID: 255
		private T firstItem;

		// Token: 0x04000100 RID: 256
		private List<T> list;

		// Token: 0x02000051 RID: 81
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000241 RID: 577 RVA: 0x00009CAD File Offset: 0x00007EAD
			internal Enumerator(ShortList<T> list)
			{
				this.list = list;
				this.index = 0;
				this.version = this.list.version;
				this.current = default(T);
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000242 RID: 578 RVA: 0x00009CDA File Offset: 0x00007EDA
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000243 RID: 579 RVA: 0x00009CE2 File Offset: 0x00007EE2
			object IEnumerator.Current
			{
				get
				{
					if (this.index == 0 || this.index == this.list.count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current;
				}
			}

			// Token: 0x06000244 RID: 580 RVA: 0x00009D17 File Offset: 0x00007F17
			public void Dispose()
			{
				this.list = null;
				this.current = default(T);
			}

			// Token: 0x06000245 RID: 581 RVA: 0x00009D2C File Offset: 0x00007F2C
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("List has been changed during enumeration.");
				}
				if (this.index < this.list.count)
				{
					this.current = this.list[this.index];
					this.index++;
					return true;
				}
				this.index = this.list.count + 1;
				this.current = default(T);
				return false;
			}

			// Token: 0x06000246 RID: 582 RVA: 0x00009DB1 File Offset: 0x00007FB1
			void IEnumerator.Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("List has been changed during enumeration.");
				}
				this.index = 0;
				this.current = default(T);
			}

			// Token: 0x04000101 RID: 257
			private ShortList<T> list;

			// Token: 0x04000102 RID: 258
			private int index;

			// Token: 0x04000103 RID: 259
			private int version;

			// Token: 0x04000104 RID: 260
			private T current;
		}
	}
}
