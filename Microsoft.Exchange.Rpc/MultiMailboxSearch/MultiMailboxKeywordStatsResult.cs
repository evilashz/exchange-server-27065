using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000177 RID: 375
	[Serializable]
	internal sealed class MultiMailboxKeywordStatsResult : MultiMailboxSearchResultItem
	{
		// Token: 0x06000920 RID: 2336 RVA: 0x0000A1DC File Offset: 0x000095DC
		internal MultiMailboxKeywordStatsResult(int version, string keyword, long count, long size) : base(version)
		{
			this.count = count;
			this.keyword = keyword;
			this.size = size;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000A1AC File Offset: 0x000095AC
		internal MultiMailboxKeywordStatsResult(string keyword, long count, long size) : base(MultiMailboxSearchBase.CurrentVersion)
		{
			this.count = count;
			this.keyword = keyword;
			this.size = size;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0000A208 File Offset: 0x00009608
		internal long Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0000A21C File Offset: 0x0000961C
		internal string Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0000A230 File Offset: 0x00009630
		internal long Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x04000B17 RID: 2839
		private long count;

		// Token: 0x04000B18 RID: 2840
		private string keyword;

		// Token: 0x04000B19 RID: 2841
		private long size;
	}
}
