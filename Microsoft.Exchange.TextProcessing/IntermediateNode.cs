using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000029 RID: 41
	internal class IntermediateNode
	{
		// Token: 0x06000191 RID: 401 RVA: 0x0000CE68 File Offset: 0x0000B068
		public IntermediateNode()
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000CE9C File Offset: 0x0000B09C
		public IntermediateNode(string value)
		{
			this.keyword = value;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		public IntermediateNode(char ch)
		{
			if (StringHelper.IsWhitespaceCharacter(ch))
			{
				this.keyword = " ";
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000CF2A File Offset: 0x0000B12A
		public SortedList<char, IntermediateNode> ChildrenMap
		{
			get
			{
				return this.childrenMap;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000CF32 File Offset: 0x0000B132
		public List<long> TerminalIds
		{
			get
			{
				return this.terminalIds;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000CF3A File Offset: 0x0000B13A
		public string Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000CF42 File Offset: 0x0000B142
		public bool IsMultiNode
		{
			get
			{
				return this.keyword.Length > 0 && !this.keyword.Equals(" ");
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000CF67 File Offset: 0x0000B167
		public void Reinitialize(string value)
		{
			this.terminalIds.Clear();
			this.childrenMap.Clear();
			this.foldedNodes.Clear();
			this.keyword = value;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000CF91 File Offset: 0x0000B191
		public void Reinitialize(char ch)
		{
			this.Reinitialize(StringHelper.IsWhitespaceCharacter(ch) ? " " : string.Empty);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000CFAD File Offset: 0x0000B1AD
		public IntermediateNode AddChild(char ch, IntermediateNode child)
		{
			this.childrenMap[ch] = child;
			return child;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public IntermediateNode GetChild(char ch, IntermediateNodePool pool)
		{
			IntermediateNode child;
			if (this.childrenMap.TryGetValue(ch, out child))
			{
				return this.MakeSimpleChild(ch, child, pool);
			}
			return null;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000D004 File Offset: 0x0000B204
		public void AddFoldedNode(char ch, int index)
		{
			if (this.foldedNodes.Any((KeyValuePair<char, int> item) => item.Key == ch))
			{
				throw new InvalidOperationException();
			}
			this.foldedNodes.Add(new KeyValuePair<char, int>(ch, index));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000D054 File Offset: 0x0000B254
		public List<KeyValuePair<char, int>> GetFoldedNodes()
		{
			return this.foldedNodes;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000D05C File Offset: 0x0000B25C
		private IntermediateNode MakeSimpleChild(char ch, IntermediateNode child, IntermediateNodePool pool)
		{
			if (child.IsMultiNode)
			{
				IntermediateNode intermediateNode = pool.Create(ch);
				this.childrenMap[ch] = intermediateNode;
				this.ReconstructOldKeyword(child, intermediateNode, pool);
				return intermediateNode;
			}
			return child;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000D094 File Offset: 0x0000B294
		private void ReconstructOldKeyword(IntermediateNode original, IntermediateNode replacement, IntermediateNodePool pool)
		{
			IntermediateNode intermediateNode = pool.Create(original.Keyword.Substring(1));
			replacement.AddChild(original.Keyword[0], intermediateNode);
			intermediateNode.TerminalIds.AddRange(original.TerminalIds);
		}

		// Token: 0x040000F8 RID: 248
		private List<long> terminalIds = new List<long>();

		// Token: 0x040000F9 RID: 249
		private SortedList<char, IntermediateNode> childrenMap = new SortedList<char, IntermediateNode>();

		// Token: 0x040000FA RID: 250
		private string keyword = string.Empty;

		// Token: 0x040000FB RID: 251
		private List<KeyValuePair<char, int>> foldedNodes = new List<KeyValuePair<char, int>>();
	}
}
