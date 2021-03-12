using System;

namespace Microsoft.Exchange.Server.Storage.MultiMailboxSearch
{
	// Token: 0x02000004 RID: 4
	internal sealed class KeywordStatsResultRow
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000020D0 File Offset: 0x000002D0
		internal KeywordStatsResultRow(string keyword, long count, double size)
		{
			this.count = count;
			this.keyword = keyword;
			this.size = size;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020ED File Offset: 0x000002ED
		internal string Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020F5 File Offset: 0x000002F5
		internal long Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020FD File Offset: 0x000002FD
		internal double Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly string keyword;

		// Token: 0x04000002 RID: 2
		private readonly long count;

		// Token: 0x04000003 RID: 3
		private readonly double size;
	}
}
