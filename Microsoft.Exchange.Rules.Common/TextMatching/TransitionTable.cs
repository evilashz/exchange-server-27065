using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x0200004E RID: 78
	internal class TransitionTable
	{
		// Token: 0x06000224 RID: 548 RVA: 0x000097D1 File Offset: 0x000079D1
		public void Add(int ch, TrieNode state)
		{
			this.table.Add(ch, state);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000097E0 File Offset: 0x000079E0
		public TrieNode GetState(int ch)
		{
			TrieNode result = null;
			if (this.table.TryGetValue(ch, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009804 File Offset: 0x00007A04
		public void Compile(StateNode node, DFACodeGenerator codegen)
		{
			foreach (int num in this.table.Keys)
			{
				codegen.AddTransition(node, this.table[num], num);
			}
		}

		// Token: 0x040000F8 RID: 248
		private Dictionary<int, TrieNode> table = new Dictionary<int, TrieNode>();
	}
}
