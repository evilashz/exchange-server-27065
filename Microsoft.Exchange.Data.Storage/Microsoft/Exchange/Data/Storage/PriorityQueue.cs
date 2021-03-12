using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000450 RID: 1104
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PriorityQueue<T> where T : IComparable<T>
	{
		// Token: 0x0600311B RID: 12571 RVA: 0x000C940D File Offset: 0x000C760D
		public PriorityQueue()
		{
			this.data = new List<T>();
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000C9420 File Offset: 0x000C7620
		public void Enqueue(T item)
		{
			this.data.Add(item);
			int num;
			for (int i = this.data.Count - 1; i > 0; i = num)
			{
				num = (i - 1) / 2;
				T t = this.data[i];
				if (t.CompareTo(this.data[num]) >= 0)
				{
					return;
				}
				T value = this.data[i];
				this.data[i] = this.data[num];
				this.data[num] = value;
			}
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000C94B4 File Offset: 0x000C76B4
		public T Dequeue()
		{
			int num = this.data.Count - 1;
			T result = this.data[0];
			this.data[0] = this.data[num];
			this.data.RemoveAt(num);
			num--;
			int num2 = 0;
			for (;;)
			{
				int num3 = num2 * 2 + 1;
				if (num3 > num)
				{
					break;
				}
				int num4 = num3 + 1;
				if (num4 <= num)
				{
					T t = this.data[num4];
					if (t.CompareTo(this.data[num3]) < 0)
					{
						num3 = num4;
					}
				}
				T t2 = this.data[num2];
				if (t2.CompareTo(this.data[num3]) <= 0)
				{
					break;
				}
				T value = this.data[num2];
				this.data[num2] = this.data[num3];
				this.data[num3] = value;
				num2 = num3;
			}
			return result;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000C95B4 File Offset: 0x000C77B4
		public T Peek()
		{
			return this.data[0];
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000C95CF File Offset: 0x000C77CF
		public int Count()
		{
			return this.data.Count;
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000C95F8 File Offset: 0x000C77F8
		public override string ToString()
		{
			string arg = this.data.Aggregate(string.Empty, (string current, T t) => current + t.ToString() + " ");
			return arg + "count = " + this.data.Count;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000C9650 File Offset: 0x000C7850
		public bool IsConsistent()
		{
			if (this.data.Count == 0)
			{
				return true;
			}
			int num = this.data.Count - 1;
			for (int i = 0; i < this.data.Count; i++)
			{
				int num2 = 2 * i + 1;
				int num3 = 2 * i + 2;
				if (num2 <= num)
				{
					T t = this.data[i];
					if (t.CompareTo(this.data[num2]) > 0)
					{
						return false;
					}
				}
				if (num3 <= num)
				{
					T t2 = this.data[i];
					if (t2.CompareTo(this.data[num3]) > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04001AA0 RID: 6816
		private readonly List<T> data;
	}
}
