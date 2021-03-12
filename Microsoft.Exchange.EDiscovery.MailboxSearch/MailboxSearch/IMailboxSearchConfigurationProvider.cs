using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.EDiscovery.Export;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000008 RID: 8
	internal interface IMailboxSearchConfigurationProvider
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69
		IDiscoverySearchDataProvider SearchDataProvider { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000046 RID: 70
		MailboxDiscoverySearch SearchObject { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000047 RID: 71
		ADUser DiscoverySystemMailboxUser { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000048 RID: 72
		IRecipientSession RecipientSession { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000049 RID: 73
		uint MaxMailboxesToSearch { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004A RID: 74
		uint MaxNumberOfMailboxesForKeywordStatistics { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004B RID: 75
		uint MaxMailboxSearches { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004C RID: 76
		uint MaxQueryKeywords { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004D RID: 77
		string SearchName { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004E RID: 78
		// (set) Token: 0x0600004F RID: 79
		string ExecutingUserId { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000050 RID: 80
		string ExecutingUserPrimarySmtpAddress { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000051 RID: 81
		bool UserCanRunMailboxSearch { get; }

		// Token: 0x06000052 RID: 82
		void UpdateSearchObject([CallerMemberName] string callerMember = null, [CallerLineNumber] int callerLine = 0);

		// Token: 0x06000053 RID: 83
		void ResetSearchObject([CallerMemberName] string callerMember = null, [CallerLineNumber] int callerLine = 0);

		// Token: 0x06000054 RID: 84
		string GenerateOWASearchResultsLink();

		// Token: 0x06000055 RID: 85
		string GenerateOWAPreviewResultsLink();

		// Token: 0x06000056 RID: 86
		string GetExecutingUserName();

		// Token: 0x06000057 RID: 87
		void CheckDiscoveryBudget(bool isEstimateOnly, MailboxSearchServer server);

		// Token: 0x06000058 RID: 88
		bool IsKeywordStatsAllowed();

		// Token: 0x06000059 RID: 89
		bool IsPreviewAllowed();

		// Token: 0x0600005A RID: 90
		bool ValidateKeywordsLimit();

		// Token: 0x0600005B RID: 91
		IList<ISource> ValidateAndGetFinalSourceMailboxes(string searchQuery, IList<string> sourceMailboxes, IList<string> notFoundMailboxes, IList<string> versionSkippedMailboxes, IList<string> rbacDeniedMailboxes, IList<string> crossPremiseFailedMailboxes, IDictionary<Uri, string> crossPremiseUrls);

		// Token: 0x0600005C RID: 92
		IList<ISource> GetFinalSources(string searchObjectName, string searchQuery, string executingUserPrimarySmtpAddress, Uri discoveryUserUri);
	}
}
