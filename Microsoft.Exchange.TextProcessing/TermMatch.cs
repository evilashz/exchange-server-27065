using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000042 RID: 66
	internal class TermMatch : IMatch
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		internal TermMatch(IEnumerable<string> terms, BoundaryType boundaryType, Dictionary<BoundaryType, TrieInfo> trieMap)
		{
			if (this.IsEmpty(terms))
			{
				throw new ArgumentException(Strings.EmptyTermSet);
			}
			this.boundaryType = boundaryType;
			this.terms = new List<string>(terms);
			this.trieInfo = trieMap[boundaryType];
			this.id = IDGenerator.GetNextID();
			this.AddTermsToTrie();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000EAC9 File Offset: 0x0000CCC9
		internal TermMatch(TermMatch original, Dictionary<BoundaryType, TrieInfo> trieMap) : this(original.terms, original.boundaryType, trieMap)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000EADE File Offset: 0x0000CCDE
		public bool IsMatch(TextScanContext data)
		{
			this.UpdateContextWithTrieSearchResults(data);
			return data.IsMatchedTermSet(this.id);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		private void AddTermsToTrie()
		{
			foreach (string keyword in this.terms)
			{
				this.trieInfo.Trie.Add(keyword, this.id);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000EB54 File Offset: 0x0000CD54
		private bool IsEmpty(IEnumerable<string> collection)
		{
			return collection == null || !collection.GetEnumerator().MoveNext();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		private void UpdateContextWithTrieSearchResults(TextScanContext context)
		{
			if (!context.IsTrieScanComplete(this.trieInfo.ID))
			{
				SearchResult searchResult = this.trieInfo.Trie.SearchText(context.Data);
				foreach (long num in searchResult.GetFoundIDs())
				{
					context.AddMatchedTermSetID(num);
				}
				context.SetTrieScanComplete(this.trieInfo.ID);
			}
		}

		// Token: 0x0400015D RID: 349
		private readonly long id;

		// Token: 0x0400015E RID: 350
		private IEnumerable<string> terms;

		// Token: 0x0400015F RID: 351
		private BoundaryType boundaryType;

		// Token: 0x04000160 RID: 352
		private TrieInfo trieInfo;
	}
}
