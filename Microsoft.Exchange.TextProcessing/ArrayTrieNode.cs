using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000026 RID: 38
	internal struct ArrayTrieNode
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000C9A6 File Offset: 0x0000ABA6
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000C9AE File Offset: 0x0000ABAE
		public int ChildrenNodesStart { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000C9B7 File Offset: 0x0000ABB7
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000C9BF File Offset: 0x0000ABBF
		public ushort ChildrenNodeCount { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		public int KeywordStart { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000C9D9 File Offset: 0x0000ABD9
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000C9E1 File Offset: 0x0000ABE1
		public ushort KeywordLength { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000C9F2 File Offset: 0x0000ABF2
		public int TerminalIdsStart { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000C9FB File Offset: 0x0000ABFB
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000CA03 File Offset: 0x0000AC03
		public ushort TerminalIdsCount { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000CA14 File Offset: 0x0000AC14
		public ushort Flags { get; set; }

		// Token: 0x06000184 RID: 388 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public bool Transition(ArrayTrie trie, char ch, int param, out ArrayTrieNode newNode, out int newParam, ref int nodeIndex)
		{
			if (StringHelper.IsWhitespaceCharacter(ch))
			{
				ch = ' ';
			}
			if (' ' == ch && (this.IsWhitespaceSpinnerNode(trie) || (param > 0 && ' ' == trie.Keywords[this.KeywordStart + param - 1])))
			{
				newNode = this;
				newParam = param;
				return true;
			}
			if (this.IsWhitespaceSpinnerNode(trie) || param >= (int)this.KeywordLength)
			{
				newNode = default(ArrayTrieNode);
				newParam = 0;
				return this.ChildrenNodeCount > 0 && this.TryFindChild(trie, ch, this.ChildrenNodesStart, this.ChildrenNodesStart + (int)this.ChildrenNodeCount - 1, out newNode, ref nodeIndex);
			}
			if (trie.Keywords[this.KeywordStart + param] == ch)
			{
				newNode = this;
				newParam = param + 1;
				return true;
			}
			newNode = default(ArrayTrieNode);
			newParam = 0;
			return false;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000CAFA File Offset: 0x0000ACFA
		public bool IsTerminal(int param)
		{
			return (int)this.KeywordLength == param && this.TerminalIdsCount > 0;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000CB10 File Offset: 0x0000AD10
		private bool TryFindChild(ArrayTrie trie, char ch, int start, int end, out ArrayTrieNode child, ref int nodeIndex)
		{
			child = default(ArrayTrieNode);
			uint num = (uint)StringHelper.FindMask(ch);
			if ((num & (uint)this.Flags) != num)
			{
				return false;
			}
			if (end < start)
			{
				return false;
			}
			if (end == start)
			{
				if (ch == trie.Edges[start].Character)
				{
					nodeIndex = trie.Edges[start].Index;
					child = trie.Nodes[nodeIndex];
					return true;
				}
				return false;
			}
			else
			{
				int num2 = (start + end) / 2;
				if (ch > trie.Edges[num2].Character)
				{
					return this.TryFindChild(trie, ch, num2 + 1, end, out child, ref nodeIndex);
				}
				if (ch < trie.Edges[num2].Character)
				{
					return this.TryFindChild(trie, ch, start, num2 - 1, out child, ref nodeIndex);
				}
				nodeIndex = trie.Edges[num2].Index;
				child = trie.Nodes[nodeIndex];
				return true;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000CC14 File Offset: 0x0000AE14
		private bool IsWhitespaceSpinnerNode(ArrayTrie trie)
		{
			return this.KeywordLength == 1 && trie.Keywords[this.KeywordStart].Equals(' ');
		}
	}
}
