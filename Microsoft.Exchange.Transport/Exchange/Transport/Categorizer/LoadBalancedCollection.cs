using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200023D RID: 573
	internal sealed class LoadBalancedCollection<T> : ICollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600191B RID: 6427 RVA: 0x000657D8 File Offset: 0x000639D8
		public LoadBalancedCollection(IList<T> list, int roundRobinBase)
		{
			this.list = list;
			this.roundRobinIndex = 0;
			if (this.Count > 0)
			{
				this.roundRobinIndex = roundRobinBase % this.Count;
				if (this.roundRobinIndex < 0)
				{
					this.roundRobinIndex = -this.roundRobinIndex;
				}
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x00065826 File Offset: 0x00063A26
		public int Count
		{
			get
			{
				if (this.list != null)
				{
					return this.list.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0006583D File Offset: 0x00063A3D
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000692 RID: 1682
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", index, "index is out of range");
				}
				return this.list[this.TranslateIndex(index)];
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00065948 File Offset: 0x00063B48
		public IEnumerator<T> GetEnumerator()
		{
			int count = this.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					yield return this.list[this.TranslateIndex(i)];
				}
			}
			yield break;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00065964 File Offset: 0x00063B64
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0006596C File Offset: 0x00063B6C
		public bool Contains(T item)
		{
			return this.list != null && this.list.Contains(item);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00065984 File Offset: 0x00063B84
		public void CopyTo(T[] dest, int index)
		{
			foreach (T t in this)
			{
				dest[index++] = t;
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000659D4 File Offset: 0x00063BD4
		public void Add(T item)
		{
			throw new NotSupportedException("ListLoadBalancer is read-only");
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000659E0 File Offset: 0x00063BE0
		public void Clear()
		{
			throw new NotSupportedException("ListLoadBalancer is read-only");
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000659EC File Offset: 0x00063BEC
		public bool Remove(T item)
		{
			throw new NotSupportedException("ListLoadBalancer is read-only");
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000659F8 File Offset: 0x00063BF8
		private int TranslateIndex(int index)
		{
			return (index + this.roundRobinIndex) % this.Count;
		}

		// Token: 0x04000C08 RID: 3080
		private int roundRobinIndex;

		// Token: 0x04000C09 RID: 3081
		private IList<T> list;
	}
}
