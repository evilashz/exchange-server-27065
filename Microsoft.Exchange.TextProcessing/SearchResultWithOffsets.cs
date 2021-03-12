using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000031 RID: 49
	internal class SearchResultWithOffsets : SearchResult
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		internal SearchResultWithOffsets()
		{
			this.results = new Dictionary<long, List<Offset>>();
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000D6B3 File Offset: 0x0000B8B3
		public override int HitCount
		{
			get
			{
				return this.results.Count;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public override IEnumerable<Offset> GetOffsetsForID(long id)
		{
			return this.results[id];
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D6CE File Offset: 0x0000B8CE
		public override IEnumerable<long> GetFoundIDs()
		{
			return this.results.Keys;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D6DB File Offset: 0x0000B8DB
		public override bool HasId(long id)
		{
			return this.results.ContainsKey(id);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		internal override void AddResult(List<long> ids, long nodeId, int start, int end)
		{
			Offset item = new Offset(start, end);
			foreach (long key in ids)
			{
				if (!this.results.ContainsKey(key))
				{
					this.results[key] = new List<Offset>();
				}
				this.results[key].Add(item);
			}
		}

		// Token: 0x0400010E RID: 270
		private Dictionary<long, List<Offset>> results;
	}
}
