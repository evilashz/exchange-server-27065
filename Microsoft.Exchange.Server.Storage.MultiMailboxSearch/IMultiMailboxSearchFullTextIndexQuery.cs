using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.FullTextIndex;

namespace Microsoft.Exchange.Server.Storage.MultiMailboxSearch
{
	// Token: 0x02000003 RID: 3
	internal interface IMultiMailboxSearchFullTextIndexQuery
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		List<string> RefinersList { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9
		// (set) Token: 0x0600000A RID: 10
		List<string> ExtraFieldsList { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		Guid QueryCorrelationId { get; set; }

		// Token: 0x0600000D RID: 13
		KeywordStatsResultRow ExecuteFullTextKeywordHitsQuery(Guid databaseGuid, Guid mailboxGuid, string query);

		// Token: 0x0600000E RID: 14
		IList<FullTextIndexRow> ExecuteFullTextIndexQuery(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, string query, int pageSize, string sortSpec, out KeywordStatsResultRow keywordStatsResult, out Dictionary<string, List<RefinersResultRow>> refinersOutput);

		// Token: 0x0600000F RID: 15
		void Abort();
	}
}
