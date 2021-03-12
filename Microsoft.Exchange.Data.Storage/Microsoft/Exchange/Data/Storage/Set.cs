using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000453 RID: 1107
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class Set<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000C9879 File Offset: 0x000C7A79
		public int Count
		{
			get
			{
				return this.dataSet.Count;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000C9886 File Offset: 0x000C7A86
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000C9889 File Offset: 0x000C7A89
		public Set(int capacity)
		{
			this.dataSet = new Dictionary<T, bool>(capacity);
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000C989D File Offset: 0x000C7A9D
		public Set()
		{
			this.dataSet = new Dictionary<T, bool>();
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000C98B0 File Offset: 0x000C7AB0
		public Set(IList<T> list) : this(list.Count)
		{
			foreach (T element in list)
			{
				this.Add(element);
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000C9904 File Offset: 0x000C7B04
		public IEnumerator<T> GetEnumerator()
		{
			return this.dataSet.Keys.GetEnumerator();
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000C991B File Offset: 0x000C7B1B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000C9923 File Offset: 0x000C7B23
		public void Add(T element)
		{
			this.dataSet.Add(element, false);
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000C9932 File Offset: 0x000C7B32
		public void SafeAdd(T element)
		{
			if (!this.dataSet.ContainsKey(element))
			{
				this.dataSet.Add(element, false);
			}
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000C9950 File Offset: 0x000C7B50
		public T[] ToArray()
		{
			T[] array = new T[this.dataSet.Keys.Count];
			this.dataSet.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000C9986 File Offset: 0x000C7B86
		public void Clear()
		{
			this.dataSet.Clear();
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000C9993 File Offset: 0x000C7B93
		public bool Contains(T element)
		{
			return this.dataSet.ContainsKey(element);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000C99A1 File Offset: 0x000C7BA1
		public bool Remove(T element)
		{
			return this.dataSet.Remove(element);
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000C99AF File Offset: 0x000C7BAF
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.dataSet.Keys.CopyTo(array, arrayIndex);
		}

		// Token: 0x04001AA4 RID: 6820
		private readonly Dictionary<T, bool> dataSet;
	}
}
