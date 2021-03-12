using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000027 RID: 39
	internal sealed class ArrayTrieState
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0000CC47 File Offset: 0x0000AE47
		public ArrayTrieState(ArrayTrieNode node, int initialCount, int nodeIndex)
		{
			this.node = node;
			this.transitionCount = initialCount;
			this.nodeIndex = nodeIndex;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000CC64 File Offset: 0x0000AE64
		private bool Terminal
		{
			get
			{
				return this.node.IsTerminal(this.nodeParam);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000CC78 File Offset: 0x0000AE78
		public static void TransitionStates(ArrayTrie trie, char ch, int position, ArrayTrieStatePool pool, List<ArrayTrieState> currentStates, List<ArrayTrieState> newStates, ref SearchResult result)
		{
			foreach (ArrayTrieState arrayTrieState in currentStates)
			{
				if (arrayTrieState.Terminal && StringHelper.IsRightHandSideDelimiter(ch, trie.BoundaryType))
				{
					result.AddResult(arrayTrieState.GetTerminalIds(trie), (long)arrayTrieState.nodeIndex, position - arrayTrieState.transitionCount - 1, position - 1);
				}
				if (arrayTrieState.Transition(trie, ch))
				{
					arrayTrieState.transitionCount++;
					newStates.Add(arrayTrieState);
				}
				else
				{
					pool.FreeActiveState(arrayTrieState);
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000CD24 File Offset: 0x0000AF24
		public void Reinitialize(ArrayTrieNode node, int initialCount, int nodeIndex)
		{
			this.node = node;
			this.transitionCount = initialCount;
			this.nodeIndex = nodeIndex;
			this.nodeParam = 0;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000CD44 File Offset: 0x0000AF44
		private bool Transition(ArrayTrie trie, char ch)
		{
			ArrayTrieNode arrayTrieNode;
			int num;
			if (this.node.Transition(trie, ch, this.nodeParam, out arrayTrieNode, out num, ref this.nodeIndex))
			{
				this.node = arrayTrieNode;
				this.nodeParam = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000CD84 File Offset: 0x0000AF84
		private List<long> GetTerminalIds(ArrayTrie trie)
		{
			if (this.node.TerminalIdsCount == 0)
			{
				return null;
			}
			List<long> list = new List<long>((int)this.node.TerminalIdsCount);
			for (int i = 0; i < (int)this.node.TerminalIdsCount; i++)
			{
				list.Add(trie.TerminalIds[this.node.TerminalIdsStart + i]);
			}
			return list;
		}

		// Token: 0x040000F3 RID: 243
		private ArrayTrieNode node;

		// Token: 0x040000F4 RID: 244
		private int nodeParam;

		// Token: 0x040000F5 RID: 245
		private int nodeIndex;

		// Token: 0x040000F6 RID: 246
		private int transitionCount;
	}
}
