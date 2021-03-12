using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	internal sealed class MultiMailboxSearchRefinersResult : MultiMailboxSearchResultItem
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0000A268 File Offset: 0x00009668
		internal MultiMailboxSearchRefinersResult(string name, long value) : base(MultiMailboxSearchBase.CurrentVersion)
		{
			this.entryName = name;
			this.entryCount = value;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0000A244 File Offset: 0x00009644
		internal MultiMailboxSearchRefinersResult(int version, string name, long value) : base(version)
		{
			this.entryName = name;
			this.entryCount = value;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0000A290 File Offset: 0x00009690
		internal long Value
		{
			get
			{
				return this.entryCount;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0000A2A4 File Offset: 0x000096A4
		internal string Name
		{
			get
			{
				return this.entryName;
			}
		}

		// Token: 0x04000B1A RID: 2842
		private readonly long entryCount;

		// Token: 0x04000B1B RID: 2843
		private readonly string entryName;
	}
}
