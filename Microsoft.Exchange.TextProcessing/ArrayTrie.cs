using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000023 RID: 35
	internal sealed class ArrayTrie
	{
		// Token: 0x0600015A RID: 346 RVA: 0x0000C37C File Offset: 0x0000A57C
		public ArrayTrie(BoundaryType boundaryType)
		{
			this.RootIndex = 0;
			this.Nodes = new RopeList<ArrayTrieNode>(1024);
			this.TerminalIds = new RopeList<long>(1024);
			this.Keywords = new RopeList<char>(1024);
			this.Edges = new RopeList<ArrayTrieEdge>(1024);
			this.RootChildrenIndexes = new int[65536];
			this.BoundaryType = boundaryType;
			for (int i = 0; i < this.RootChildrenIndexes.Length; i++)
			{
				this.RootChildrenIndexes[i] = -1;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000C409 File Offset: 0x0000A609
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000C411 File Offset: 0x0000A611
		public int RootIndex { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000C41A File Offset: 0x0000A61A
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000C422 File Offset: 0x0000A622
		public RopeList<ArrayTrieNode> Nodes { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000C42B File Offset: 0x0000A62B
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000C433 File Offset: 0x0000A633
		public RopeList<long> TerminalIds { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000C43C File Offset: 0x0000A63C
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000C444 File Offset: 0x0000A644
		public RopeList<char> Keywords { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000C44D File Offset: 0x0000A64D
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000C455 File Offset: 0x0000A655
		public RopeList<ArrayTrieEdge> Edges { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000C45E File Offset: 0x0000A65E
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000C466 File Offset: 0x0000A666
		public int[] RootChildrenIndexes { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000C46F File Offset: 0x0000A66F
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000C477 File Offset: 0x0000A677
		public BoundaryType BoundaryType { get; private set; }

		// Token: 0x06000169 RID: 361 RVA: 0x0000C480 File Offset: 0x0000A680
		public void SearchText(string data, SearchResult result)
		{
			if (data == null)
			{
				throw new ArgumentException(Strings.InvalidData);
			}
			string text = StringHelper.NormalizeString(data);
			ArrayTrieStatePool arrayTrieStatePool = new ArrayTrieStatePool();
			List<ArrayTrieState> currentStates = new List<ArrayTrieState>(32);
			List<ArrayTrieState> list = new List<ArrayTrieState>(32);
			bool flag = true;
			for (int i = 0; i < text.Length; i++)
			{
				ArrayTrieState.TransitionStates(this, text[i], i, arrayTrieStatePool, currentStates, list, ref result);
				if (flag)
				{
					int num = this.RootChildrenIndexes[(int)text[i]];
					if (num != -1)
					{
						list.Add(arrayTrieStatePool.GetActiveState(this.Nodes[num], 0, num));
					}
				}
				flag = StringHelper.IsLeftHandSideDelimiter(text[i], this.BoundaryType);
				this.Swap<List<ArrayTrieState>>(ref list, ref currentStates);
				list.Clear();
			}
			ArrayTrieState.TransitionStates(this, ' ', text.Length, arrayTrieStatePool, currentStates, list, ref result);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000C558 File Offset: 0x0000A758
		private void Swap<T>(ref T first, ref T second)
		{
			T t = first;
			first = second;
			second = t;
		}
	}
}
