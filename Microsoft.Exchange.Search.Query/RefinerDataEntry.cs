using System;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x0200001B RID: 27
	internal class RefinerDataEntry
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00007218 File Offset: 0x00005418
		internal RefinerDataEntry(string displayName, long hitCount, string refinementQuery)
		{
			InstantSearch.ThrowOnNullOrEmptyArgument(displayName, "displayName");
			InstantSearch.ThrowOnNullOrEmptyArgument(refinementQuery, "refinementQuery");
			this.DisplayName = displayName;
			this.HitCount = hitCount;
			this.RefinementQuery = refinementQuery;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000724B File Offset: 0x0000544B
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00007253 File Offset: 0x00005453
		public string DisplayName { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000725C File Offset: 0x0000545C
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00007264 File Offset: 0x00005464
		public long HitCount { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000726D File Offset: 0x0000546D
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00007275 File Offset: 0x00005475
		public string RefinementQuery { get; private set; }

		// Token: 0x06000153 RID: 339 RVA: 0x00007280 File Offset: 0x00005480
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"DisplayName=",
				this.DisplayName,
				", HitCount=",
				this.HitCount,
				", RefinementQuery=",
				this.RefinementQuery
			});
		}
	}
}
