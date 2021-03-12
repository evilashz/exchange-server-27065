using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.EDiscovery.Export;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x0200000A RID: 10
	internal interface IMailboxSearchTask : IDisposable
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000087 RID: 135
		// (set) Token: 0x06000088 RID: 136
		EventHandler<ExportStatusEventArgs> OnReportStatistics { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		Action<int, long, long, long, List<KeywordHit>> OnEstimateCompleted { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008B RID: 139
		// (set) Token: 0x0600008C RID: 140
		Action<ISearchResults> OnPrepareCompleted { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008D RID: 141
		// (set) Token: 0x0600008E RID: 142
		Action OnExportCompleted { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008F RID: 143
		IExportContext ExportContext { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000090 RID: 144
		ISearchResults SearchResults { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000091 RID: 145
		SearchState CurrentState { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000092 RID: 146
		IList<string> Errors { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000093 RID: 147
		ITargetMailbox TargetMailbox { get; }

		// Token: 0x06000094 RID: 148
		void Abort();

		// Token: 0x06000095 RID: 149
		void Start();
	}
}
