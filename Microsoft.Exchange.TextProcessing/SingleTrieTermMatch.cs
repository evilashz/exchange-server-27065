using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000041 RID: 65
	internal class SingleTrieTermMatch : IMatch
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		internal SingleTrieTermMatch(IEnumerable<string> terms, BoundaryType boundaryType, TrieInfo trieInfo)
		{
			if (terms == null || !terms.GetEnumerator().MoveNext())
			{
				throw new ArgumentException(Strings.EmptyTermSet);
			}
			this.boundaryType = boundaryType;
			this.terms = new List<string>(terms);
			this.trieInfo = trieInfo;
			this.id = SearchResultEncodedId.GetEncodedId(IDGenerator.GetNextID(), boundaryType);
			foreach (string keyword in this.terms)
			{
				this.trieInfo.Trie.Add(keyword, this.id);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		internal SingleTrieTermMatch(SingleTrieTermMatch original, TrieInfo trieInfo) : this(original.terms, original.boundaryType, trieInfo)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
		public bool IsMatch(TextScanContext data)
		{
			this.UpdateContextWithTrieSearchResults(data);
			return data.IsMatchedTermSet(this.id);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		private void UpdateContextWithTrieSearchResults(TextScanContext context)
		{
			if (!context.IsTrieScanComplete(this.trieInfo.ID))
			{
				SearchResultEncodedId searchResultEncodedId = new SearchResultEncodedId(context.NormalizedData, 256);
				this.trieInfo.Trie.SearchText(context.NormalizedData, searchResultEncodedId);
				foreach (long num in searchResultEncodedId.GetFoundIDs())
				{
					context.AddMatchedTermSetID(num);
				}
				context.SetTrieScanComplete(this.trieInfo.ID);
			}
		}

		// Token: 0x04000159 RID: 345
		private readonly long id;

		// Token: 0x0400015A RID: 346
		private IEnumerable<string> terms;

		// Token: 0x0400015B RID: 347
		private TrieInfo trieInfo;

		// Token: 0x0400015C RID: 348
		private BoundaryType boundaryType;
	}
}
