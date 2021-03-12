using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000315 RID: 789
	internal class Node<T>
	{
		// Token: 0x0600222B RID: 8747 RVA: 0x00080F76 File Offset: 0x0007F176
		public Node(T item)
		{
			this.data = item;
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x00080F85 File Offset: 0x0007F185
		public T Value
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x00080F8D File Offset: 0x0007F18D
		// (set) Token: 0x0600222E RID: 8750 RVA: 0x00080F95 File Offset: 0x0007F195
		public Node<T> Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x040011D8 RID: 4568
		private T data;

		// Token: 0x040011D9 RID: 4569
		private Node<T> next;
	}
}
