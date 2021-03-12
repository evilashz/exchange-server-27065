using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x02000041 RID: 65
	internal sealed class MatchAC
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000078AE File Offset: 0x00005AAE
		public MatchAC(string[] keywords)
		{
			this.BuildKeyWordsTrie(keywords);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000078DC File Offset: 0x00005ADC
		public PatternMatcher Compile()
		{
			DFACodeGenerator dfacodeGenerator = new DFACodeGenerator("matcher", this.newstate);
			dfacodeGenerator.BeginCompile();
			this.root.Compile(dfacodeGenerator);
			this.root = null;
			this.chars = null;
			return dfacodeGenerator.EndCompile();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007920 File Offset: 0x00005B20
		private void BuildKeyWordsTrie(string[] keywords)
		{
			if (keywords == null || keywords.Length == 0)
			{
				throw new ArgumentException(MatchAC.errorMessage);
			}
			foreach (string text in keywords)
			{
				if (!string.IsNullOrEmpty(text))
				{
					this.Enter(text);
				}
			}
			this.ComputeTransitions();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000796C File Offset: 0x00005B6C
		private void Enter(string keyword)
		{
			TrieNode trieNode = this.root;
			TrieNode trieNode2 = null;
			int num = 0;
			while (trieNode.Children.TryGetValue((int)keyword[num], out trieNode2))
			{
				trieNode = trieNode2;
				num++;
				if (num >= keyword.Length)
				{
					trieNode.IsFinal = true;
					return;
				}
			}
			for (int i = num; i < keyword.Length; i++)
			{
				TrieNode trieNode3 = new TrieNode(this.newstate++);
				trieNode.Children[(int)keyword[i]] = trieNode3;
				this.chars.Add((int)keyword[i]);
				trieNode = trieNode3;
			}
			trieNode.IsFinal = true;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007A10 File Offset: 0x00005C10
		private void ComputeTransitions()
		{
			Queue<TrieNode> queue = new Queue<TrieNode>();
			using (Dictionary<int, TrieNode>.KeyCollection.Enumerator enumerator = this.root.Children.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					TrieNode trieNode = this.root.Children[num];
					this.root.TransitionTable.Add(num, trieNode);
					queue.Enqueue(trieNode);
					trieNode.Fail = this.root;
				}
				goto IL_13B;
			}
			IL_7A:
			TrieNode trieNode2 = queue.Dequeue();
			foreach (int ch in this.chars)
			{
				TrieNode trieNode3 = trieNode2.Transit(ch);
				if (trieNode3 != null)
				{
					queue.Enqueue(trieNode3);
					trieNode2.TransitionTable.Add(ch, trieNode3);
					TrieNode fail = trieNode2.Fail;
					TrieNode fail2;
					while ((fail2 = fail.Transit(ch)) == null)
					{
						fail = fail.Fail;
					}
					trieNode3.Fail = fail2;
				}
				else
				{
					TrieNode state = trieNode2.Fail.TransitionTable.GetState(ch) ?? this.root;
					trieNode2.TransitionTable.Add(ch, state);
				}
			}
			IL_13B:
			if (queue.Count <= 0)
			{
				return;
			}
			goto IL_7A;
		}

		// Token: 0x040000C6 RID: 198
		private static string errorMessage = "no keywords";

		// Token: 0x040000C7 RID: 199
		private TrieNode root = new TrieNode(0);

		// Token: 0x040000C8 RID: 200
		private int newstate = 1;

		// Token: 0x040000C9 RID: 201
		private Set<int> chars = new Set<int>();
	}
}
