using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000030 RID: 48
	internal class SearchResultEncodedId : SearchResult
	{
		// Token: 0x060001BE RID: 446 RVA: 0x0000D54F File Offset: 0x0000B74F
		internal SearchResultEncodedId(string text, int resultCapacity = 256)
		{
			this.results = new LowAllocSet(resultCapacity);
			this.terminalNodes = new LowAllocSet(resultCapacity);
			this.text = text;
			this.textLength = text.Length;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000D582 File Offset: 0x0000B782
		public override int HitCount
		{
			get
			{
				return this.results.Count;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000D58F File Offset: 0x0000B78F
		public static long GetEncodedId(long id, BoundaryType type)
		{
			return id * 10L + (long)type;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000D599 File Offset: 0x0000B799
		public static BoundaryType GetBoundaryType(long id)
		{
			return (BoundaryType)(id % 10L);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000D5A1 File Offset: 0x0000B7A1
		public override IEnumerable<Offset> GetOffsetsForID(long id)
		{
			throw new InvalidOperationException(Strings.OffsetsUnavailable);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000D5B2 File Offset: 0x0000B7B2
		public override IEnumerable<long> GetFoundIDs()
		{
			return this.results.Values;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000D5BF File Offset: 0x0000B7BF
		public override bool HasId(long id)
		{
			return this.results.Contains(id);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		internal override void AddResult(List<long> ids, long nodeId, int start, int end)
		{
			if (!this.terminalNodes.Contains(nodeId))
			{
				bool flag = true;
				foreach (long num in ids)
				{
					BoundaryType boundaryType = SearchResultEncodedId.GetBoundaryType(num);
					bool flag2 = start <= 0 || boundaryType == BoundaryType.None || StringHelper.IsLeftHandSideDelimiter(this.text[start - 1], boundaryType);
					bool flag3 = end >= this.textLength - 1 || boundaryType == BoundaryType.None || StringHelper.IsRightHandSideDelimiter(this.text[end + 1], boundaryType);
					if (flag2 && flag3)
					{
						this.results.Add(num);
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.terminalNodes.Add(nodeId);
				}
			}
		}

		// Token: 0x0400010A RID: 266
		private readonly string text;

		// Token: 0x0400010B RID: 267
		private readonly int textLength;

		// Token: 0x0400010C RID: 268
		private LowAllocSet results;

		// Token: 0x0400010D RID: 269
		private LowAllocSet terminalNodes;
	}
}
