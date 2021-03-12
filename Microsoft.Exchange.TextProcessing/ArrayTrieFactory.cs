using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000025 RID: 37
	internal static class ArrayTrieFactory
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		public static ArrayTrie Create(List<KeywordEntry> entries, BoundaryType boundaryType)
		{
			ArrayTrie arrayTrie = new ArrayTrie(boundaryType);
			IntermediateNode intermediateNode = new IntermediateNode();
			IntermediateNodePool pool = new IntermediateNodePool();
			entries.Sort((KeywordEntry a, KeywordEntry b) => string.Compare(a.Keyword, b.Keyword, StringComparison.OrdinalIgnoreCase));
			foreach (KeywordEntry keywordEntry in entries)
			{
				ArrayTrieFactory.AddKeyword(arrayTrie, intermediateNode, pool, StringHelper.NormalizeKeyword(keywordEntry.Keyword), keywordEntry.Identifier);
			}
			arrayTrie.RootIndex = ArrayTrieFactory.FoldTree(arrayTrie, intermediateNode, pool);
			ArrayTrieNode arrayTrieNode = arrayTrie.Nodes[arrayTrie.RootIndex];
			for (int i = 0; i < (int)arrayTrieNode.ChildrenNodeCount; i++)
			{
				ArrayTrieEdge arrayTrieEdge = arrayTrie.Edges[arrayTrieNode.ChildrenNodesStart + i];
				arrayTrie.RootChildrenIndexes[(int)arrayTrieEdge.Character] = arrayTrieEdge.Index;
			}
			return arrayTrie;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		private static void AddKeyword(ArrayTrie trie, IntermediateNode root, IntermediateNodePool pool, string keyword, long id)
		{
			if (string.IsNullOrEmpty(keyword))
			{
				throw new ArgumentException(Strings.InvalidTerm);
			}
			IntermediateNode intermediateNode = root;
			for (int i = 0; i < keyword.Length; i++)
			{
				if (intermediateNode.IsMultiNode)
				{
					throw new InvalidOperationException(Strings.CurrentNodeNotSingleNode);
				}
				IntermediateNode child = intermediateNode.GetChild(keyword[i], pool);
				if (child == null)
				{
					ArrayTrieFactory.FoldChildren(trie, intermediateNode, pool);
					intermediateNode = intermediateNode.AddChild(keyword[i], pool.Create(keyword.Substring(i + 1)));
					break;
				}
				intermediateNode = child;
			}
			intermediateNode.TerminalIds.Add(id);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C750 File Offset: 0x0000A950
		private static void FoldChildren(ArrayTrie trie, IntermediateNode node, IntermediateNodePool pool)
		{
			if (node.ChildrenMap.Count > 1)
			{
				throw new InvalidOperationException(Strings.IntermediateNodeHasMultipleChildren);
			}
			if (node.ChildrenMap.Count == 1)
			{
				KeyValuePair<char, IntermediateNode> keyValuePair = node.ChildrenMap.First<KeyValuePair<char, IntermediateNode>>();
				node.AddFoldedNode(keyValuePair.Key, ArrayTrieFactory.FoldTree(trie, keyValuePair.Value, pool));
				pool.Reclaim(keyValuePair.Value);
				node.ChildrenMap.Clear();
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		private static int FoldTree(ArrayTrie trie, IntermediateNode node, IntermediateNodePool pool)
		{
			ArrayTrieFactory.FoldChildren(trie, node, pool);
			ArrayTrieNode item = default(ArrayTrieNode);
			item.ChildrenNodesStart = trie.Edges.Count;
			item.ChildrenNodeCount = (ushort)node.GetFoldedNodes().Count;
			foreach (KeyValuePair<char, int> keyValuePair in node.GetFoldedNodes())
			{
				trie.Edges.Add(new ArrayTrieEdge
				{
					Character = keyValuePair.Key,
					Index = keyValuePair.Value
				});
				item.Flags |= StringHelper.FindMask(keyValuePair.Key);
			}
			item.KeywordStart = trie.Keywords.Count;
			item.KeywordLength = (ushort)node.Keyword.Length;
			foreach (char item2 in node.Keyword)
			{
				trie.Keywords.Add(item2);
			}
			item.TerminalIdsStart = trie.TerminalIds.Count;
			item.TerminalIdsCount = (ushort)node.TerminalIds.Count;
			foreach (long item3 in node.TerminalIds)
			{
				trie.TerminalIds.Add(item3);
			}
			trie.Nodes.Add(item);
			return trie.Nodes.Count - 1;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C978 File Offset: 0x0000AB78
		private static string NormalizeKeyword(string keyword)
		{
			return ArrayTrieFactory.RemoveSpaces.Replace(StringHelper.NormalizeString(keyword).Trim(), " ");
		}

		// Token: 0x040000EA RID: 234
		private static readonly Regex RemoveSpaces = new Regex("\\s+", RegexOptions.Compiled);
	}
}
