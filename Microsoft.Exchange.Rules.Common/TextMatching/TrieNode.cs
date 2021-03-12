using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x0200004F RID: 79
	internal sealed class TrieNode : StateNode
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0000986C File Offset: 0x00007A6C
		public TrieNode(int state) : base(state, false)
		{
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000988C File Offset: 0x00007A8C
		public Dictionary<int, TrieNode> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009894 File Offset: 0x00007A94
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000989C File Offset: 0x00007A9C
		public TrieNode Fail
		{
			get
			{
				return this.fail;
			}
			set
			{
				this.fail = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000098A5 File Offset: 0x00007AA5
		public TransitionTable TransitionTable
		{
			get
			{
				return this.transitionTable;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000098B0 File Offset: 0x00007AB0
		public TrieNode Transit(int ch)
		{
			TrieNode result = null;
			if (this.Children.TryGetValue(ch, out result))
			{
				return result;
			}
			if (base.State == 0)
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000098DC File Offset: 0x00007ADC
		public void Compile(DFACodeGenerator codegen)
		{
			this.transitionTable.Compile(this, codegen);
			foreach (TrieNode trieNode in this.children.Values)
			{
				trieNode.Compile(codegen);
			}
		}

		// Token: 0x040000F9 RID: 249
		private TrieNode fail;

		// Token: 0x040000FA RID: 250
		private TransitionTable transitionTable = new TransitionTable();

		// Token: 0x040000FB RID: 251
		private Dictionary<int, TrieNode> children = new Dictionary<int, TrieNode>();
	}
}
