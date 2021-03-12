using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.TextProcessing;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000034 RID: 52
	internal class Trie
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000DAC2 File Offset: 0x0000BCC2
		public Trie(BoundaryType boundaryType = BoundaryType.Normal, bool storeOffsets = true)
		{
			this.boundaryType = boundaryType;
			this.storeOffsets = storeOffsets;
			this.nodeCollection.Add(TrieNode.Default);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		public void Add(string keyword, long id)
		{
			if (string.IsNullOrEmpty(keyword))
			{
				throw new ArgumentException(Strings.InvalidTerm);
			}
			string text = StringHelper.NormalizeKeyword(keyword);
			this.AddNormalizedKeyword((text == keyword) ? keyword : text, id);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000DB38 File Offset: 0x0000BD38
		public SearchResult SearchText(string data)
		{
			if (data == null)
			{
				throw new ArgumentException(Strings.InvalidData);
			}
			SearchResult result = SearchResult.Create(this.storeOffsets);
			this.SearchNormalizedText(StringHelper.NormalizeString(data), result);
			return result;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000DB72 File Offset: 0x0000BD72
		public void SearchText(string data, SearchResult result)
		{
			if (data == null)
			{
				throw new ArgumentException(Strings.InvalidData);
			}
			this.SearchNormalizedText(StringHelper.NormalizeString(data), result);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000DB94 File Offset: 0x0000BD94
		private void SearchNormalizedText(string data, SearchResult result)
		{
			if (this.childrenNodes == null)
			{
				return;
			}
			ActiveStatePool activeStatePool = new ActiveStatePool();
			List<ActiveState> currentStates = new List<ActiveState>(32);
			List<ActiveState> list = new List<ActiveState>(32);
			bool flag = true;
			for (int i = 0; i < data.Length; i++)
			{
				char c = data[i];
				ActiveState.TransitionStates(c, i, this.boundaryType, this.nodeCollection, activeStatePool, currentStates, list, ref result);
				if (flag)
				{
					int num = this.childrenNodes[(int)c];
					if (num != 0)
					{
						list.Add(activeStatePool.GetActiveState(this.nodeCollection[num], num, 1));
					}
				}
				flag = StringHelper.IsLeftHandSideDelimiter(c, this.boundaryType);
				this.Swap<List<ActiveState>>(ref currentStates, ref list);
				list.Clear();
			}
			ActiveState.TransitionStates(' ', data.Length, this.boundaryType, this.nodeCollection, activeStatePool, currentStates, list, ref result);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000DC70 File Offset: 0x0000BE70
		private void AddNormalizedKeyword(string keyword, long id)
		{
			if (this.childrenNodes == null)
			{
				this.childrenNodes = new int[65536];
			}
			int num = this.AddToRoot(keyword, id);
			if (num != 0)
			{
				int i = 1;
				TrieNode value = this.nodeCollection[num];
				while (i < keyword.Length)
				{
					if (value.IsMultiNode())
					{
						ExTraceGlobals.SmartTrieTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Current trie node is a multinode which is unexpected. Adding word '{0}' and at index '{1}'", keyword, i);
						throw new InvalidOperationException(Strings.CurrentNodeNotSingleNode);
					}
					int child = value.GetChild(keyword[i], this.nodeCollection);
					if (child == 0)
					{
						TrieNode item = new TrieNode(keyword, i + 1);
						item.AddTerminalId(id);
						this.nodeCollection.Add(item);
						value.AddChild(keyword[i], this.nodeCollection.Count - 1);
						this.nodeCollection[num] = value;
						return;
					}
					value = this.nodeCollection[child];
					num = child;
					if (value.IsMultiNode())
					{
						TrieNode item2 = new TrieNode(value.Keyword, (int)(value.StringIndex + 1));
						item2.AddTerminalIds(value.TerminalIDs);
						this.nodeCollection.Add(item2);
						TrieNode value2 = new TrieNode(keyword[i]);
						value2.AddChild(value.Keyword[i + 1], this.nodeCollection.Count - 1);
						this.nodeCollection[child] = value2;
					}
					value = this.nodeCollection[child];
					if (i + 1 == keyword.Length)
					{
						value.AddTerminalId(id);
						this.nodeCollection[child] = value;
					}
					i++;
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000DE18 File Offset: 0x0000C018
		private int AddToRoot(string keyword, long id)
		{
			char c = keyword[0];
			int num = this.childrenNodes[(int)c];
			if (num == 0)
			{
				TrieNode item = new TrieNode(keyword, 1);
				item.AddTerminalId(id);
				this.nodeCollection.Add(item);
				this.childrenNodes[(int)c] = this.nodeCollection.Count - 1;
				return 0;
			}
			TrieNode trieNode = this.nodeCollection[num];
			if (trieNode.IsMultiNode())
			{
				TrieNode item2 = new TrieNode(c);
				item2.AddChild(trieNode.Keyword[(int)trieNode.StringIndex], num);
				this.nodeCollection.Add(item2);
				TrieNode value = new TrieNode(trieNode.Keyword, (int)(trieNode.StringIndex + 1));
				value.AddTerminalIds(trieNode.TerminalIDs);
				this.nodeCollection[num] = value;
				int num2 = this.nodeCollection.Count - 1;
				this.childrenNodes[(int)c] = num2;
				return num2;
			}
			return num;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000DF08 File Offset: 0x0000C108
		private void Swap<T>(ref T first, ref T second)
		{
			T t = first;
			first = second;
			second = t;
		}

		// Token: 0x04000125 RID: 293
		private readonly bool storeOffsets;

		// Token: 0x04000126 RID: 294
		private int[] childrenNodes;

		// Token: 0x04000127 RID: 295
		private BoundaryType boundaryType;

		// Token: 0x04000128 RID: 296
		private RopeList<TrieNode> nodeCollection = new RopeList<TrieNode>(1024);
	}
}
