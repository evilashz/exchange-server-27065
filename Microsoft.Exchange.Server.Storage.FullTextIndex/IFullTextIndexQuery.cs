using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x02000010 RID: 16
	public interface IFullTextIndexQuery
	{
		// Token: 0x0600009B RID: 155
		List<FullTextIndexRow> ExecuteFullTextIndexQuery(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, string query, CultureInfo culture, Guid correlationId, PagingImsFlowExecutor.QueryLoggingContext loggingContext);

		// Token: 0x0600009C RID: 156
		List<FullTextIndexRow> ExecutePagedFullTextIndexQuery(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, string query, CultureInfo culture, Guid correlationId, bool needConversationId, PagingImsFlowExecutor.QueryLoggingContext loggingContext, PagedQueryResults pagedQueryResults);

		// Token: 0x0600009D RID: 157
		IEnumerable<FullTextDiagnosticRow> ExecuteDiagnosticQuery(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, string query, CultureInfo culture, Guid correlationId, string sortOrder, ICollection<string> additionalColumns, PagingImsFlowExecutor.QueryLoggingContext loggingContext);

		// Token: 0x0600009E RID: 158
		int GetPageSize();
	}
}
