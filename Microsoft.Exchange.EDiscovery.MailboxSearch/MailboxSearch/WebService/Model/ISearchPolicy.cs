using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000033 RID: 51
	internal interface ISearchPolicy
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000260 RID: 608
		CallerInfo CallerInfo { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000261 RID: 609
		IRecipientSession RecipientSession { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000262 RID: 610
		ExchangeRunspaceConfiguration RunspaceConfiguration { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000263 RID: 611
		IThrottlingSettings ThrottlingSettings { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000264 RID: 612
		IExecutionSettings ExecutionSettings { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000265 RID: 613
		IBudget Budget { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000266 RID: 614
		Recorder Recorder { get; }

		// Token: 0x06000267 RID: 615
		ActivityScope GetActivityScope();
	}
}
