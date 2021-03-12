using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200002A RID: 42
	internal sealed class IntermediateNodePool
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		public void Reclaim(IntermediateNode node)
		{
			if (node.ChildrenMap != null)
			{
				foreach (IntermediateNode node2 in node.ChildrenMap.Values)
				{
					this.Reclaim(node2);
				}
				node.ChildrenMap.Clear();
			}
			this.pool.Add(node);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000D150 File Offset: 0x0000B350
		public IntermediateNode Create(char ch)
		{
			if (this.pool.Count > 0)
			{
				IntermediateNode intermediateNode = this.pool[this.pool.Count - 1];
				this.pool.RemoveAt(this.pool.Count - 1);
				intermediateNode.Reinitialize(ch);
				return intermediateNode;
			}
			return new IntermediateNode(ch);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		public IntermediateNode Create(string value = "")
		{
			if (this.pool.Count > 0)
			{
				IntermediateNode intermediateNode = this.pool[this.pool.Count - 1];
				this.pool.RemoveAt(this.pool.Count - 1);
				intermediateNode.Reinitialize(value);
				return intermediateNode;
			}
			return new IntermediateNode(value);
		}

		// Token: 0x040000FC RID: 252
		private List<IntermediateNode> pool = new List<IntermediateNode>(16);
	}
}
