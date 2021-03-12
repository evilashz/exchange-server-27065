using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000314 RID: 788
	internal class Node
	{
		// Token: 0x0600174A RID: 5962 RVA: 0x0006C23C File Offset: 0x0006A43C
		public Node(string key, string parentKey, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Attempt to create node with null key.", "key");
			}
			if (parentKey == null)
			{
				throw new ArgumentNullException("Attempt to create node with null parent key.", "parentKey");
			}
			this.key = key;
			this.parentKey = parentKey;
			this.value = value;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0006C28A File Offset: 0x0006A48A
		public void AddChild(Node newNode)
		{
			if (this.Children == null)
			{
				this.children = new List<Node>();
			}
			this.children.Add(newNode);
			newNode.Parent = this;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0006C2B2 File Offset: 0x0006A4B2
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x0006C2BA File Offset: 0x0006A4BA
		public Node Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0006C2C3 File Offset: 0x0006A4C3
		public ReadOnlyCollection<Node> Children
		{
			get
			{
				if (this.children == null)
				{
					this.children = new List<Node>();
				}
				return new ReadOnlyCollection<Node>(this.children);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0006C2E3 File Offset: 0x0006A4E3
		public string ParentKey
		{
			get
			{
				return this.parentKey;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0006C2EB File Offset: 0x0006A4EB
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x0006C2F3 File Offset: 0x0006A4F3
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x0006C2FB File Offset: 0x0006A4FB
		public bool HasChildren
		{
			get
			{
				return this.Children != null && this.Children.Count > 0;
			}
		}

		// Token: 0x04000ECF RID: 3791
		private string key;

		// Token: 0x04000ED0 RID: 3792
		private string parentKey;

		// Token: 0x04000ED1 RID: 3793
		private object value;

		// Token: 0x04000ED2 RID: 3794
		private Node parent;

		// Token: 0x04000ED3 RID: 3795
		private List<Node> children;
	}
}
