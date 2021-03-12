using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200023C RID: 572
	internal sealed class ListLoadBalancer<T>
	{
		// Token: 0x06001911 RID: 6417 RVA: 0x00065713 File Offset: 0x00063913
		public ListLoadBalancer(bool randomLoadBalancingOffsetEnabled) : this(null, randomLoadBalancingOffsetEnabled)
		{
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0006571D File Offset: 0x0006391D
		public ListLoadBalancer(IList<T> list, bool randomLoadBalancingOffsetEnabled)
		{
			if (list != null)
			{
				this.list = new List<T>(list);
			}
			if (randomLoadBalancingOffsetEnabled)
			{
				this.roundRobinBase = RoutingUtils.GetRandomNumber(10000);
				return;
			}
			this.roundRobinBase = -1;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0006574F File Offset: 0x0006394F
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0006575A File Offset: 0x0006395A
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

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x00065771 File Offset: 0x00063971
		public ICollection<T> NonLoadBalancedCollection
		{
			get
			{
				return this.NonLoadBalancedList;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x00065779 File Offset: 0x00063979
		public List<T> NonLoadBalancedList
		{
			get
			{
				if (this.list == null)
				{
					return ListLoadBalancer<T>.emptyList;
				}
				return this.list;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x0006578F File Offset: 0x0006398F
		public LoadBalancedCollection<T> LoadBalancedCollection
		{
			get
			{
				return new LoadBalancedCollection<T>(this.NonLoadBalancedList, Interlocked.Increment(ref this.roundRobinBase));
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000657A7 File Offset: 0x000639A7
		public void AddItem(T item)
		{
			RoutingUtils.AddItemToLazyList<T>(item, ref this.list);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000657B5 File Offset: 0x000639B5
		public void RemoveItem(T item)
		{
			if (this.list != null)
			{
				this.list.Remove(item);
			}
		}

		// Token: 0x04000C05 RID: 3077
		private static readonly List<T> emptyList = new List<T>();

		// Token: 0x04000C06 RID: 3078
		private List<T> list;

		// Token: 0x04000C07 RID: 3079
		private int roundRobinBase;
	}
}
