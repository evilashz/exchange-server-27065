using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200002F RID: 47
	internal abstract class SearchResult
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B7 RID: 439
		public abstract int HitCount { get; }

		// Token: 0x060001B8 RID: 440
		public abstract IEnumerable<Offset> GetOffsetsForID(long id);

		// Token: 0x060001B9 RID: 441
		public abstract IEnumerable<long> GetFoundIDs();

		// Token: 0x060001BA RID: 442
		public abstract bool HasId(long id);

		// Token: 0x060001BB RID: 443 RVA: 0x0000D532 File Offset: 0x0000B732
		internal static SearchResult Create(bool storeOffsets)
		{
			if (storeOffsets)
			{
				return new SearchResultWithOffsets();
			}
			return new SearchResultWithoutOffsets(256);
		}

		// Token: 0x060001BC RID: 444
		internal abstract void AddResult(List<long> ids, long nodeId, int start, int end);
	}
}
