using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E5 RID: 485
	internal interface IMultiMailboxSearchFactory
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C91 RID: 3217
		Trace LocalTaskTracer { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C92 RID: 3218
		Trace MailboxGroupGeneratorTracer { get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C93 RID: 3219
		Trace GeneralTracer { get; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C94 RID: 3220
		Trace AutodiscoverTracer { get; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C95 RID: 3221
		ExEventLog EventLog { get; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C96 RID: 3222
		int MaxAllowedMailboxQueriesPerRequest { get; }

		// Token: 0x06000C97 RID: 3223
		TimeSpan GetDefaultSearchTimeout(IRecipientSession recipientSession);

		// Token: 0x06000C98 RID: 3224
		int GetMaxAllowedKeywords(IRecipientSession recipientSession);

		// Token: 0x06000C99 RID: 3225
		int GetMaxAllowedMailboxes(IRecipientSession recipientSession, SearchType searchType);

		// Token: 0x06000C9A RID: 3226
		bool IsSearchAllowed(IRecipientSession recipientSession, SearchType searchType, int totalMailboxesToSearchCount);

		// Token: 0x06000C9B RID: 3227
		bool IsDiscoverySearchEnabled(IRecipientSession recipientSession);

		// Token: 0x06000C9C RID: 3228
		int GetPreviewSearchResultsPageSize(IRecipientSession recipientSession);

		// Token: 0x06000C9D RID: 3229
		int GetMaxAllowedKeywordsPerPage(IRecipientSession recipientSession);

		// Token: 0x06000C9E RID: 3230
		int GetMaxAllowedSearchThreads(IRecipientSession recipientSession);

		// Token: 0x06000C9F RID: 3231
		int GetMaxRefinerResults(IRecipientSession recipientSession);

		// Token: 0x06000CA0 RID: 3232
		MailboxSearchGroup CreateMailboxSearchGroup(GroupId groupId, List<MailboxInfo> mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser);

		// Token: 0x06000CA1 RID: 3233
		int GetMaximumThreadsForLocalSearch(int numberOfMailboxes, IRecipientSession session);

		// Token: 0x06000CA2 RID: 3234
		int GetMaximumAllowedPageSizeForLocalSearch(int pageSize, IRecipientSession session);

		// Token: 0x06000CA3 RID: 3235
		MultiMailboxSearchClient CreateSearchRpcClient(Guid databaseGuid, MailboxInfo[] mailboxes, SearchCriteria criteria, CallerInfo executingUserIdentity, PagingInfo pagingInfo);

		// Token: 0x06000CA4 RID: 3236
		ISearchMailboxTask CreateAggregatedMailboxSearchTask(Guid databaseGuid, MailboxInfoList mailbox, SearchType type, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser);

		// Token: 0x06000CA5 RID: 3237
		ISearchMailboxTask CreateAggregatedMailboxSearchTask(Guid databaseGuid, MailboxInfoList mailbox, SearchCriteria searchCriteria, PagingInfo pagingInfo, List<string> keywordList, CallerInfo executingUser);

		// Token: 0x06000CA6 RID: 3238
		AggregatedMailboxSearchGroup CreateAggregatedMailboxSearchGroup(MailboxInfo[] mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser);

		// Token: 0x06000CA7 RID: 3239
		MailboxInfoList CreateMailboxInfoList(MailboxInfo[] mailboxes);

		// Token: 0x06000CA8 RID: 3240
		IEwsEndpointDiscovery GetEwsEndpointDiscovery(List<MailboxInfo> mailboxes, OrganizationId orgId, CallerInfo callerInfo);

		// Token: 0x06000CA9 RID: 3241
		IEwsClient CreateDiscoveryEwsClient(GroupId groupId, MailboxInfo[] mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo caller);

		// Token: 0x06000CAA RID: 3242
		IAutodiscoveryClient CreateUserSettingAutoDiscoveryClient(List<MailboxInfo> crossPremiseMailboxes, Uri autoDiscoverEndpoint, ICredentials credentials, CallerInfo callerInfo);

		// Token: 0x06000CAB RID: 3243
		INonIndexableDiscoveryEwsClient CreateNonIndexableDiscoveryEwsClient(GroupId groupId, MailboxInfo[] mailboxes, ExTimeZone timeZone, CallerInfo caller);
	}
}
