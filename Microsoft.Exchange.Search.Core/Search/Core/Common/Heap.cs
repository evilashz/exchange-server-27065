using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200006F RID: 111
	internal class Heap
	{
		// Token: 0x06000296 RID: 662 RVA: 0x000072A2 File Offset: 0x000054A2
		internal Heap(IComparer<IHeapItem> weightComparer)
		{
			this.data = new List<IHeapItem>();
			this.weightComparer = weightComparer;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000072BC File Offset: 0x000054BC
		internal bool IsEmpty
		{
			get
			{
				return this.data.Count == 0;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000072CC File Offset: 0x000054CC
		internal int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000072DC File Offset: 0x000054DC
		public override string ToString()
		{
			if (this.IsEmpty)
			{
				return "(empty)";
			}
			StringBuilder stringBuilder = new StringBuilder("root-> ");
			bool flag = true;
			foreach (IHeapItem value in this.data)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(value);
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007364 File Offset: 0x00005564
		internal bool Contains(IHeapItem item)
		{
			return item.Handle >= 0 && item.Handle < this.data.Count;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007384 File Offset: 0x00005584
		internal void Update(IHeapItem item)
		{
			if (item.Handle > this.data.Count - 1 || item.Handle < 0)
			{
				throw new ArgumentException(string.Format("heapHandle, invalid value: {0}, current size: {1}", item.Handle, this.data.Count));
			}
			this.data[item.Handle] = item;
			if (!this.AdjustUp(item))
			{
				this.AdjustDown(item);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000073FD File Offset: 0x000055FD
		internal void Push(IHeapItem item)
		{
			this.data.Add(item);
			item.Handle = this.data.Count - 1;
			this.AdjustUp(item);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007428 File Offset: 0x00005628
		internal bool TryPop(out IHeapItem item)
		{
			item = null;
			if (this.data.Count == 0)
			{
				return false;
			}
			item = this.data[0];
			this.data[0] = this.data[this.data.Count - 1];
			this.data[0].Handle = 0;
			this.data.RemoveAt(this.data.Count - 1);
			if (this.Count > 0)
			{
				this.AdjustDown(this.data[0]);
			}
			return true;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000074C0 File Offset: 0x000056C0
		internal void Flush()
		{
			this.data.Clear();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000074D0 File Offset: 0x000056D0
		private void Swap(IHeapItem left, IHeapItem right)
		{
			IHeapItem value = this.data[left.Handle];
			this.data[left.Handle] = this.data[right.Handle];
			this.data[right.Handle] = value;
			int handle = left.Handle;
			left.Handle = right.Handle;
			right.Handle = handle;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007540 File Offset: 0x00005740
		private IHeapItem GetLeft(IHeapItem item)
		{
			int num = item.Handle * 2 + 1;
			if (num >= this.data.Count)
			{
				return null;
			}
			return this.data[num];
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007574 File Offset: 0x00005774
		private IHeapItem GetRight(IHeapItem item)
		{
			IHeapItem left = this.GetLeft(item);
			if (left == null)
			{
				return null;
			}
			int num = left.Handle + 1;
			if (num >= this.data.Count)
			{
				return null;
			}
			return this.data[num];
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000075B3 File Offset: 0x000057B3
		private IHeapItem GetParent(IHeapItem item)
		{
			if (item.Handle <= 0)
			{
				return null;
			}
			return this.data[(item.Handle + 1) / 2 - 1];
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000075D8 File Offset: 0x000057D8
		private bool AdjustUp(IHeapItem item)
		{
			bool result = false;
			IHeapItem parent = this.GetParent(item);
			while (parent != null && this.weightComparer.Compare(parent, item) < 0)
			{
				this.Swap(parent, item);
				parent = this.GetParent(item);
				result = true;
			}
			return result;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007618 File Offset: 0x00005818
		private bool AdjustDown(IHeapItem item)
		{
			bool result = false;
			IHeapItem left = this.GetLeft(item);
			IHeapItem right = this.GetRight(item);
			while (left != null || right != null)
			{
				IHeapItem heapItem = item;
				if (left != null && this.weightComparer.Compare(left, heapItem) > 0)
				{
					heapItem = left;
				}
				if (right != null && this.weightComparer.Compare(right, heapItem) > 0)
				{
					heapItem = right;
				}
				if (heapItem == item)
				{
					break;
				}
				this.Swap(item, heapItem);
				result = true;
				left = this.GetLeft(item);
				right = this.GetRight(item);
			}
			return result;
		}

		// Token: 0x0400011D RID: 285
		private List<IHeapItem> data;

		// Token: 0x0400011E RID: 286
		private IComparer<IHeapItem> weightComparer;
	}
}
