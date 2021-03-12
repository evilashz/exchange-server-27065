using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x0200004D RID: 77
	internal sealed class Set<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000971C File Offset: 0x0000791C
		public Set()
		{
			this.hash = new Dictionary<T, string>();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000972F File Offset: 0x0000792F
		public int Count
		{
			get
			{
				return this.hash.Count;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000973C File Offset: 0x0000793C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000973F File Offset: 0x0000793F
		public void Add(T item)
		{
			if (!this.hash.ContainsKey(item))
			{
				this.hash.Add(item, string.Empty);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009760 File Offset: 0x00007960
		public void Clear()
		{
			this.hash.Clear();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000976D File Offset: 0x0000796D
		public bool Remove(T item)
		{
			return this.hash.Remove(item);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000977B File Offset: 0x0000797B
		public bool Contains(T item)
		{
			return this.hash.ContainsKey(item);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009789 File Offset: 0x00007989
		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009790 File Offset: 0x00007990
		public IEnumerator<T> GetEnumerator()
		{
			return this.hash.Keys.GetEnumerator();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000097A7 File Offset: 0x000079A7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.hash.Keys.GetEnumerator();
		}

		// Token: 0x040000F7 RID: 247
		private Dictionary<T, string> hash;
	}
}
