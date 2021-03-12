using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.MultiMailboxSearch;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E6 RID: 486
	internal class Factory : IMultiMailboxSearchFactory
	{
		// Token: 0x06000CAC RID: 3244 RVA: 0x00036802 File Offset: 0x00034A02
		protected Factory()
		{
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0003680A File Offset: 0x00034A0A
		public static IMultiMailboxSearchFactory Current
		{
			get
			{
				return Factory.instance.Value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00036816 File Offset: 0x00034A16
		public static Hookable<IMultiMailboxSearchFactory> Instance
		{
			get
			{
				return Factory.instance;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0003681D File Offset: 0x00034A1D
		public ExEventLog EventLog
		{
			get
			{
				return Factory.eventLog;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00036824 File Offset: 0x00034A24
		public Trace LocalTaskTracer
		{
			get
			{
				return ExTraceGlobals.LocalSearchTracer;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0003682B File Offset: 0x00034A2B
		public Trace MailboxGroupGeneratorTracer
		{
			get
			{
				return ExTraceGlobals.MailboxGroupGeneratorTracer;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00036832 File Offset: 0x00034A32
		public Trace AutodiscoverTracer
		{
			get
			{
				return ExTraceGlobals.AutoDiscoverTracer;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00036839 File Offset: 0x00034A39
		public Trace GeneralTracer
		{
			get
			{
				return ExTraceGlobals.GeneralTracer;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00036840 File Offset: 0x00034A40
		public int MaxAllowedMailboxQueriesPerRequest
		{
			get
			{
				return Factory.DefaultMaxAllowedMailboxQueriesPerRequest;
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00036848 File Offset: 0x00034A48
		public TimeSpan GetDefaultSearchTimeout(IRecipientSession recipientSession)
		{
			int discoverySearchTimeoutPeriod = (int)SearchUtils.GetDiscoverySearchTimeoutPeriod(recipientSession);
			if (discoverySearchTimeoutPeriod <= 0)
			{
				return Factory.DefaultSearchTimeoutInterval;
			}
			return TimeSpan.FromMinutes((double)discoverySearchTimeoutPeriod);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00036870 File Offset: 0x00034A70
		public MailboxSearchGroup CreateMailboxSearchGroup(GroupId groupId, List<MailboxInfo> mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser)
		{
			switch (groupId.GroupType)
			{
			case GroupType.Local:
				return this.CreateAggregatedMailboxSearchGroup(mailboxes.ToArray(), searchCriteria, pagingInfo, executingUser);
			case GroupType.CrossServer:
			case GroupType.CrossPremise:
				return new WebServiceMailboxSearchGroup(groupId, mailboxes.ToArray(), searchCriteria, pagingInfo, executingUser);
			default:
				return null;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000368BD File Offset: 0x00034ABD
		public AggregatedMailboxSearchGroup CreateAggregatedMailboxSearchGroup(MailboxInfo[] mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser)
		{
			return new AggregatedMailboxSearchGroup(mailboxes, searchCriteria, pagingInfo, executingUser);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000368C9 File Offset: 0x00034AC9
		public MailboxInfoList CreateMailboxInfoList(MailboxInfo[] mailboxes)
		{
			return new MailboxInfoList(mailboxes);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000368D1 File Offset: 0x00034AD1
		public ISearchMailboxTask CreateAggregatedMailboxSearchTask(Guid databaseGuid, MailboxInfoList mailbox, SearchType type, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser)
		{
			return new AggregatedMailboxSearchTask(databaseGuid, mailbox, type, searchCriteria, pagingInfo, executingUser);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000368E1 File Offset: 0x00034AE1
		public ISearchMailboxTask CreateAggregatedMailboxSearchTask(Guid databaseGuid, MailboxInfoList mailbox, SearchCriteria searchCriteria, PagingInfo pagingInfo, List<string> keywordList, CallerInfo executingUser)
		{
			return new AggregatedMailboxSearchTask(databaseGuid, mailbox, searchCriteria, pagingInfo, keywordList, executingUser);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000368F1 File Offset: 0x00034AF1
		public MultiMailboxSearchClient CreateSearchRpcClient(Guid databaseGuid, MailboxInfo[] mailboxes, SearchCriteria criteria, CallerInfo executingUserIdentity, PagingInfo pagingInfo)
		{
			return new MultiMailboxSearchClient(databaseGuid, mailboxes, criteria, executingUserIdentity, pagingInfo);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00036900 File Offset: 0x00034B00
		public int GetMaximumThreadsForLocalSearch(int numberOfMailboxes, IRecipientSession session)
		{
			int num = 0;
			int num2 = 0;
			ThreadPool.GetAvailableThreads(out num, out num2);
			return Math.Min(numberOfMailboxes, Math.Min(num / 2, this.GetMaxAllowedSearchThreads(session)));
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00036930 File Offset: 0x00034B30
		public int GetMaximumAllowedPageSizeForLocalSearch(int pageSize, IRecipientSession session)
		{
			int num = this.GetPreviewSearchResultsPageSize(session);
			if (num <= 0)
			{
				num = Factory.DefaultMaxAllowedResultsPageSize;
			}
			return Math.Min(pageSize, num);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00036956 File Offset: 0x00034B56
		public bool IsDiscoverySearchEnabled(IRecipientSession recipientSession)
		{
			return SearchUtils.DiscoveryEnabled(recipientSession);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0003695E File Offset: 0x00034B5E
		public int GetMaxAllowedKeywords(IRecipientSession recipientSession)
		{
			return (int)SearchUtils.GetDiscoveryMaxKeywords(recipientSession);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00036966 File Offset: 0x00034B66
		public int GetPreviewSearchResultsPageSize(IRecipientSession recipientSession)
		{
			return (int)SearchUtils.GetDiscoveryPreviewSearchResultsPageSize(recipientSession);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0003696E File Offset: 0x00034B6E
		public int GetMaxAllowedKeywordsPerPage(IRecipientSession recipientSession)
		{
			return (int)SearchUtils.GetDiscoveryMaxKeywordsPerPage(recipientSession);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00036976 File Offset: 0x00034B76
		public int GetMaxAllowedSearchThreads(IRecipientSession recipientSession)
		{
			return (int)SearchUtils.GetDiscoveryMaxSearchQueueDepth(recipientSession);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0003697E File Offset: 0x00034B7E
		public int GetMaxRefinerResults(IRecipientSession recipientSession)
		{
			return (int)SearchUtils.GetDiscoveryMaxRefinerResults(recipientSession);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00036988 File Offset: 0x00034B88
		public int GetMaxAllowedMailboxes(IRecipientSession recipientSession, SearchType searchType)
		{
			int result = (int)SearchUtils.GetDiscoveryMaxMailboxes(recipientSession);
			if (searchType == SearchType.Preview)
			{
				result = (int)SearchUtils.GetDiscoveryMaxMailboxesForPreviewSearch(recipientSession);
			}
			if (searchType == SearchType.Statistics)
			{
				result = (int)SearchUtils.GetDiscoveryMaxMailboxesForStatsSearch(recipientSession);
			}
			return result;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000369B3 File Offset: 0x00034BB3
		public IEwsEndpointDiscovery GetEwsEndpointDiscovery(List<MailboxInfo> mailboxes, OrganizationId orgId, CallerInfo callerInfo)
		{
			return new EwsEndpointDiscovery(mailboxes, orgId, callerInfo);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000369BD File Offset: 0x00034BBD
		public IEwsClient CreateDiscoveryEwsClient(GroupId groupId, MailboxInfo[] mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo caller)
		{
			return new DiscoveryEwsClient(groupId, mailboxes, searchCriteria, pagingInfo, caller);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000369CB File Offset: 0x00034BCB
		public IAutodiscoveryClient CreateUserSettingAutoDiscoveryClient(List<MailboxInfo> crossPremiseMailboxes, Uri autoDiscoveryEndpoint, ICredentials credentials, CallerInfo callerInfo)
		{
			return new UserSettingAutodiscovery(crossPremiseMailboxes, autoDiscoveryEndpoint, credentials, callerInfo);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000369D7 File Offset: 0x00034BD7
		public INonIndexableDiscoveryEwsClient CreateNonIndexableDiscoveryEwsClient(GroupId groupId, MailboxInfo[] mailboxes, ExTimeZone timeZone, CallerInfo caller)
		{
			return new NonIndexableDiscoveryEwsClient(groupId, mailboxes, timeZone, caller);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000369E4 File Offset: 0x00034BE4
		public bool IsSearchAllowed(IRecipientSession recipientSession, SearchType searchType, int totalMailboxesToSearchCount)
		{
			EDiscoverySearchVerdict ediscoverySearchVerdict = Util.ComputeDiscoverySearchVerdict(recipientSession, searchType, totalMailboxesToSearchCount);
			return ediscoverySearchVerdict.Equals(EDiscoverySearchVerdict.Allowed);
		}

		// Token: 0x04000911 RID: 2321
		internal const string EventLogSourceName = "Exchange Discovery Search";

		// Token: 0x04000912 RID: 2322
		internal static Guid EventLogComponentGuid = new Guid("d34c8ffd-7201-4ffc-8851-1011b950a219");

		// Token: 0x04000913 RID: 2323
		internal static ExEventLog eventLog = new ExEventLog(Factory.EventLogComponentGuid, "Exchange Discovery Search");

		// Token: 0x04000914 RID: 2324
		protected static readonly int DefaultKeywordsBatchSize = 6;

		// Token: 0x04000915 RID: 2325
		private static readonly TimeSpan DefaultSearchTimeoutInterval = TimeSpan.FromMinutes(4.5);

		// Token: 0x04000916 RID: 2326
		private static readonly int DefaultMaxAllowedMailboxQueriesPerRequest = 5;

		// Token: 0x04000917 RID: 2327
		private static readonly int DefaultMaxAllowedResultsPageSize = 500;

		// Token: 0x04000918 RID: 2328
		private static readonly Hookable<IMultiMailboxSearchFactory> instance = Hookable<IMultiMailboxSearchFactory>.Create(true, new Factory());
	}
}
