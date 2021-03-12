using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000032 RID: 50
	internal class SearchResultWithoutOffsets : SearchResult
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000D770 File Offset: 0x0000B970
		internal SearchResultWithoutOffsets(int resultCapacity = 256)
		{
			this.results = new LowAllocSet(resultCapacity);
			this.terminalNodes = new LowAllocSet(resultCapacity);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000D790 File Offset: 0x0000B990
		public override int HitCount
		{
			get
			{
				return this.results.Count;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000D79D File Offset: 0x0000B99D
		public override IEnumerable<Offset> GetOffsetsForID(long id)
		{
			throw new InvalidOperationException(Strings.OffsetsUnavailable);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D7AE File Offset: 0x0000B9AE
		public override IEnumerable<long> GetFoundIDs()
		{
			return this.results.Values;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D7BB File Offset: 0x0000B9BB
		public override bool HasId(long id)
		{
			return this.results.Contains(id);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		internal override void AddResult(List<long> ids, long nodeId, int start, int end)
		{
			if (!this.terminalNodes.Contains(nodeId))
			{
				this.terminalNodes.Add(nodeId);
				foreach (long value in ids)
				{
					this.results.Add(value);
				}
			}
		}

		// Token: 0x0400010F RID: 271
		private LowAllocSet results;

		// Token: 0x04000110 RID: 272
		private LowAllocSet terminalNodes;
	}
}
