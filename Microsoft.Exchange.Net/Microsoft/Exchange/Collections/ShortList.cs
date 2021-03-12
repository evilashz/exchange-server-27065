using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000697 RID: 1687
	[Serializable]
	public class ShortList<T> : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06001EC4 RID: 7876 RVA: 0x00039AA3 File Offset: 0x00037CA3
		public ShortList()
		{
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00039AAC File Offset: 0x00037CAC
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

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x00039B08 File Offset: 0x00037D08
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x00039B10 File Offset: 0x00037D10
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x00039B13 File Offset: 0x00037D13
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

		// Token: 0x17000827 RID: 2087
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

		// Token: 0x06001ECB RID: 7883 RVA: 0x00039BB4 File Offset: 0x00037DB4
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

		// Token: 0x06001ECC RID: 7884 RVA: 0x00039C00 File Offset: 0x00037E00
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

		// Token: 0x06001ECD RID: 7885 RVA: 0x00039C7C File Offset: 0x00037E7C
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

		// Token: 0x06001ECE RID: 7886 RVA: 0x00039D07 File Offset: 0x00037F07
		public void Add(T item)
		{
			this.Insert(this.count, item);
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00039D16 File Offset: 0x00037F16
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

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00039D4C File Offset: 0x00037F4C
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00039D5C File Offset: 0x00037F5C
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

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00039DBC File Offset: 0x00037FBC
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

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00039DDF File Offset: 0x00037FDF
		public ShortList<T>.Enumerator GetEnumerator()
		{
			return new ShortList<T>.Enumerator(this);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00039DE7 File Offset: 0x00037FE7
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00039DF4 File Offset: 0x00037FF4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001E70 RID: 7792
		private static EqualityComparer<T> comparer = EqualityComparer<T>.Default;

		// Token: 0x04001E71 RID: 7793
		private int count;

		// Token: 0x04001E72 RID: 7794
		private int version;

		// Token: 0x04001E73 RID: 7795
		private T firstItem;

		// Token: 0x04001E74 RID: 7796
		private List<T> list;

		// Token: 0x02000698 RID: 1688
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06001ED7 RID: 7895 RVA: 0x00039E0D File Offset: 0x0003800D
			internal Enumerator(ShortList<T> list)
			{
				this.list = list;
				this.index = 0;
				this.version = this.list.version;
				this.current = default(T);
			}

			// Token: 0x17000828 RID: 2088
			// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00039E3A File Offset: 0x0003803A
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000829 RID: 2089
			// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x00039E42 File Offset: 0x00038042
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

			// Token: 0x06001EDA RID: 7898 RVA: 0x00039E77 File Offset: 0x00038077
			public void Dispose()
			{
				this.list = null;
				this.current = default(T);
			}

			// Token: 0x06001EDB RID: 7899 RVA: 0x00039E8C File Offset: 0x0003808C
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

			// Token: 0x06001EDC RID: 7900 RVA: 0x00039F11 File Offset: 0x00038111
			void IEnumerator.Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("List has been changed during enumeration.");
				}
				this.index = 0;
				this.current = default(T);
			}

			// Token: 0x04001E75 RID: 7797
			private ShortList<T> list;

			// Token: 0x04001E76 RID: 7798
			private int index;

			// Token: 0x04001E77 RID: 7799
			private int version;

			// Token: 0x04001E78 RID: 7800
			private T current;
		}
	}
}
