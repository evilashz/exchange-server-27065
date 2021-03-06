using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002DF RID: 735
	internal class SearchMessageTrackingReportImpl
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000622AB File Offset: 0x000604AB
		internal TrackingErrorCollection Errors
		{
			get
			{
				return this.directoryContext.Errors;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000622B8 File Offset: 0x000604B8
		public bool WholeForestSearchExecuted
		{
			get
			{
				return this.wholeForestSearchExecuted;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x000622C0 File Offset: 0x000604C0
		internal List<MessageTrackingSearchResult> ResultList
		{
			get
			{
				return this.resultList;
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000622C8 File Offset: 0x000604C8
		private static void SaveSubjectSearchTokenAndReset(ref List<string> results, ref StringBuilder currentSubstring)
		{
			if (currentSubstring != null)
			{
				if (currentSubstring.Length > 0)
				{
					if (results == null)
					{
						results = new List<string>();
					}
					results.Add(currentSubstring.ToString());
					currentSubstring.Length = 0;
					return;
				}
			}
			else
			{
				currentSubstring = new StringBuilder();
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00062301 File Offset: 0x00060501
		private static int TryGetResultListLength(List<MessageTrackingSearchResult> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00062310 File Offset: 0x00060510
		private static TrackingBaseException TryExecuteTask(Action action, TrackingErrorCollection errors)
		{
			TrackingBaseException ex = null;
			try
			{
				action();
			}
			catch (TrackingFatalException ex2)
			{
				ex = ex2;
			}
			catch (TrackingTransientException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<TrackingBaseException>(0, "TrackingException occurred: {0}", ex);
				if (!ex.IsAlreadyLogged)
				{
					errors.Errors.Add(ex.TrackingError);
				}
			}
			return ex;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0006237C File Offset: 0x0006057C
		private static Dictionary<string, MessageTrackingSearchResult> GetMessageIdMap(List<MessageTrackingSearchResult> results)
		{
			if (results == null)
			{
				return null;
			}
			Dictionary<string, MessageTrackingSearchResult> dictionary = new Dictionary<string, MessageTrackingSearchResult>(Math.Min(1, results.Count / 2));
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in results)
			{
				string text = messageTrackingSearchResult.MessageTrackingReportId.MessageId;
				if (!string.IsNullOrEmpty(text) && !dictionary.ContainsKey(text))
				{
					dictionary[text] = messageTrackingSearchResult;
				}
			}
			return dictionary;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00062404 File Offset: 0x00060604
		private string[] ParseSubjectTokens(string subjectKeywords)
		{
			if (string.IsNullOrEmpty(subjectKeywords))
			{
				return null;
			}
			bool flag = false;
			List<string> list = null;
			StringBuilder stringBuilder = null;
			foreach (char c in subjectKeywords)
			{
				char c2 = c;
				if (c2 == '"')
				{
					if (!flag)
					{
						SearchMessageTrackingReportImpl.SaveSubjectSearchTokenAndReset(ref list, ref stringBuilder);
						flag = true;
					}
					else
					{
						SearchMessageTrackingReportImpl.SaveSubjectSearchTokenAndReset(ref list, ref stringBuilder);
						flag = false;
					}
				}
				else if (char.IsWhiteSpace(c) && !flag)
				{
					SearchMessageTrackingReportImpl.SaveSubjectSearchTokenAndReset(ref list, ref stringBuilder);
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder != null && stringBuilder.Length > 0)
			{
				if (list == null)
				{
					list = new List<string>(1);
				}
				list.Add(stringBuilder.ToString());
			}
			if (list == null)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x000624B8 File Offset: 0x000606B8
		private static bool IsSearchResultFirstHop(MessageTrackingSearchResult result)
		{
			return string.IsNullOrEmpty(result.FirstHopServer) || result.MessageTrackingReportId.Server.Equals(result.FirstHopServer, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000624E0 File Offset: 0x000606E0
		private static List<MessageTrackingSearchResult> FilterForFirstHopResults(ICollection<MessageTrackingSearchResult> results)
		{
			List<MessageTrackingSearchResult> list = new List<MessageTrackingSearchResult>(results.Count);
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in results)
			{
				if (!SearchMessageTrackingReportImpl.IsSearchResultFirstHop(messageTrackingSearchResult))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<MessageTrackingReportId, string>(0, "result with id={0} dropped because it is not a result from the first hop hub of {1}", messageTrackingSearchResult.MessageTrackingReportId, messageTrackingSearchResult.FirstHopServer);
				}
				else
				{
					list.Add(messageTrackingSearchResult);
				}
			}
			return list;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0006255C File Offset: 0x0006075C
		private bool TrackingAsSender
		{
			get
			{
				return this.senderIfTrackingAsRecip == null;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00062567 File Offset: 0x00060767
		private TrackedUser Sender
		{
			get
			{
				if (this.TrackingAsSender)
				{
					return this.mailbox;
				}
				return this.senderIfTrackingAsRecip;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00062580 File Offset: 0x00060780
		private SmtpAddress? RecipientCriterionForExpand
		{
			get
			{
				if (!this.expandTree || this.RecipientAddressArray == null || this.RecipientAddressArray.Length == 0)
				{
					return null;
				}
				return new SmtpAddress?(this.RecipientAddressArray[0]);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x000625C8 File Offset: 0x000607C8
		private SmtpAddress[] RecipientAddressArray
		{
			get
			{
				if (this.recipientAddressArray == null)
				{
					if (this.recipients == null || this.recipients.Length == 0)
					{
						return null;
					}
					this.recipientAddressArray = new SmtpAddress[this.recipients.Length];
					for (int i = 0; i < this.recipients.Length; i++)
					{
						this.recipientAddressArray[i] = this.recipients[i].SmtpAddress;
					}
				}
				return this.recipientAddressArray;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0006263C File Offset: 0x0006083C
		private ProxyAddressCollection[] RecipientProxyAddresses
		{
			get
			{
				if (this.recipientProxyAddresses == null)
				{
					if (this.recipients == null || this.recipients.Length == 0)
					{
						return null;
					}
					this.recipientProxyAddresses = new ProxyAddressCollection[this.recipients.Length];
					for (int i = 0; i < this.recipients.Length; i++)
					{
						this.recipientProxyAddresses[i] = this.recipients[i].ProxyAddresses;
					}
				}
				return this.recipientProxyAddresses;
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000626A8 File Offset: 0x000608A8
		internal SearchMessageTrackingReportImpl(DirectoryContext directoryContext, SearchScope scope, TrackedUser mailbox, TrackedUser senderIfTrackingAsRecip, string serverHint, TrackedUser[] recipients, LogCache logCache, string subjectKeyword, string messageId, Unlimited<uint> resultSize, bool expandTree, bool searchAsRecip, bool useFirstHopIfAvailable, bool moderationResultSearch)
		{
			this.directoryContext = directoryContext;
			this.defaultDomain = ServerCache.Instance.GetDefaultDomain(this.directoryContext.OrganizationId);
			string fqdn = ServerCache.Instance.GetLocalServer().Fqdn;
			this.scope = scope;
			this.mailbox = mailbox;
			this.senderIfTrackingAsRecip = senderIfTrackingAsRecip;
			this.serverHint = serverHint;
			this.recipients = recipients;
			this.start = CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime;
			this.end = CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime;
			this.subjectKeyword = subjectKeyword;
			this.tokenizedSubjectKeywords = this.ParseSubjectTokens(subjectKeyword);
			this.messageId = messageId;
			this.resultSize = resultSize;
			this.trackingDiscovery = new TrackingDiscovery(directoryContext);
			this.expandTree = expandTree;
			this.searchAsRecip = searchAsRecip;
			this.useFirstHopIfAvailable = useFirstHopIfAvailable;
			this.logCache = (logCache ?? new LogCache(this.start, this.end, this.directoryContext.TrackingBudget));
			this.moderationResultSearch = moderationResultSearch;
			if (this.moderationResultSearch && string.IsNullOrEmpty(this.messageId))
			{
				throw new ArgumentException("MessageId must be specified to search for moderation results");
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x000627D8 File Offset: 0x000609D8
		internal List<MessageTrackingSearchResult> Execute()
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Searching for messages, Sender:{0}, Mid:{1}, Scope:{2}, ExpandTree:{3}, SearchAsRecip: {4}, useFirstHopIfAvailable: {5}, moderationResultSearch: {6}", new object[]
			{
				(this.Sender == null) ? "<null>" : this.Sender.SmtpAddress.ToString(),
				(this.messageId == null) ? "<null>" : this.messageId,
				Names<SearchScope>.Map[(int)this.scope],
				this.expandTree,
				this.searchAsRecip,
				this.useFirstHopIfAvailable,
				this.moderationResultSearch
			});
			uint budgetUsed = this.directoryContext.TrackingBudget.BudgetUsed;
			List<MessageTrackingSearchResult> result;
			try
			{
				InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportExecuted.Increment();
				TimeSpan elapsed = this.directoryContext.TrackingBudget.Elapsed;
				try
				{
					this.FindMessage();
				}
				finally
				{
					long incrementValue = (long)(this.directoryContext.TrackingBudget.Elapsed - elapsed).TotalMilliseconds;
					InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportProcessingTime.IncrementBy(incrementValue);
				}
				if (this.resultList != null)
				{
					this.resultList.Sort(new Comparison<MessageTrackingSearchResult>(MessageTrackingSearchResult.CompareSearchResultsByTimeStampDescending));
				}
				result = this.resultList;
			}
			finally
			{
				this.directoryContext.TrackingBudget.RestoreBudgetTo(budgetUsed);
			}
			return result;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00062950 File Offset: 0x00060B50
		public List<MessageTrackingSearchResult> RunAuthorizationFilter(bool isOwaJumpOffPointRequest, ExDateTime? clientSubmitTime, string storeItemSubject)
		{
			if (this.TrackingAsSender)
			{
				return this.resultList;
			}
			if (this.resultList == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No results to filter", new object[0]);
				return null;
			}
			if (isOwaJumpOffPointRequest && this.resultList.Count == 1)
			{
				this.resultList[0].Subject = storeItemSubject;
				this.resultList[0].SubmittedDateTime = ((clientSubmitTime != null) ? ((DateTime)clientSubmitTime.Value) : DateTime.MinValue);
				return this.resultList;
			}
			List<MessageTrackingSearchResult> list = this.FilterMessagesToRecipient(this.resultList);
			TraceWrapper.SearchLibraryTracer.TraceDebug<int, int>(this.GetHashCode(), "List trimmed from: {0} to {1}", this.resultList.Count, (list != null) ? list.Count : 0);
			return list;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00062A22 File Offset: 0x00060C22
		private TrackingAuthority SenderAuthority()
		{
			if (this.Sender == null)
			{
				return UndefinedTrackingAuthority.Instance;
			}
			return this.trackingDiscovery.FindUserLocation(this.Sender);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00062A44 File Offset: 0x00060C44
		private void FindMessage()
		{
			if (this.TrackAsSender())
			{
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Searching from recipient side", new object[0]);
			TrackingAuthority serverHintAuthority = this.GetServerHintAuthority();
			TrackingAuthority trackingAuthority;
			if (!this.TrackingAsSender)
			{
				trackingAuthority = (serverHintAuthority ?? this.trackingDiscovery.FindUserLocation(this.mailbox));
				TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress, string>(this.GetHashCode(), "Recipient is {0}, Using authority {1}", this.mailbox.SmtpAddress, Names<TrackingAuthorityKind>.Map[(int)trackingAuthority.TrackingAuthorityKind]);
				if (trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.Undefined || trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.LegacyExchangeServer)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Recipient authority is {0}, will search in current site only", Names<TrackingAuthorityKind>.Map[(int)trackingAuthority.TrackingAuthorityKind]);
					trackingAuthority = CurrentSiteTrackingAuthority.Instance;
				}
				bool usingServerHint = serverHintAuthority != null;
				this.FindMessageBackwards(this.scope, trackingAuthority, usingServerHint, ref this.resultList);
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Tracking as sender, recipient hint and serverhint will be used if possible", new object[0]);
			bool flag = serverHintAuthority != null;
			if (flag)
			{
				trackingAuthority = serverHintAuthority;
			}
			else if (this.recipients != null && this.recipients.Length == 1)
			{
				trackingAuthority = this.trackingDiscovery.FindUserLocation(this.recipients[0]);
				if (trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.Undefined || trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.LegacyExchangeServer)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Recipient authority is {0}, will search in current site only", Names<TrackingAuthorityKind>.Map[(int)trackingAuthority.TrackingAuthorityKind]);
					trackingAuthority = CurrentSiteTrackingAuthority.Instance;
				}
			}
			else
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Recipient hint and serverhint not available, using current site.", new object[0]);
				trackingAuthority = CurrentSiteTrackingAuthority.Instance;
			}
			this.FindMessageBackwards(this.scope, trackingAuthority, flag, ref this.resultList);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00062C18 File Offset: 0x00060E18
		private bool TrackAsSender()
		{
			if (this.searchAsRecip || this.Sender == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Sender was out of scope, falling back to searching as recipient", new object[0]);
				return false;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Retrieving sender tracking authorities", new object[0]);
			bool result = false;
			IList<TrackingAuthority> authoritiesByPriority = this.trackingDiscovery.GetAuthoritiesByPriority(this.Sender);
			using (IEnumerator<TrackingAuthority> enumerator = authoritiesByPriority.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TrackingAuthority authority = enumerator.Current;
					if (authority.TrackingAuthorityKind == TrackingAuthorityKind.LegacyExchangeServer)
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Sender's mailbox server was legacy (pre Exchange 2010)", new object[0]);
						this.Errors.Add(ErrorCode.LegacySender, string.Empty, string.Empty, string.Empty);
						return true;
					}
					if (!authority.IsAllowedScope(this.scope))
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Authority: {0} was out of current scope", Names<TrackingAuthorityKind>.Map[(int)authority.TrackingAuthorityKind]);
					}
					else
					{
						result = true;
						TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Sender tracking authority was: {0}", Names<TrackingAuthorityKind>.Map[(int)authority.TrackingAuthorityKind]);
						TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
						{
							this.FindMessage(this.scope, authority, ref this.resultList);
						}, this.Errors);
						if (ex != null)
						{
							if (ex.IsOverBudgetError)
							{
								TraceWrapper.SearchLibraryTracer.TraceError<TrackingBaseException>(this.GetHashCode(), "Stop iterating through authorities due to over-budget error: {0}", ex);
								return true;
							}
							TraceWrapper.SearchLibraryTracer.TraceError<TrackingBaseException>(this.GetHashCode(), "Cannot get messages from authority, it will be skipped: {0}", ex);
						}
						else if (!string.IsNullOrEmpty(this.messageId) && this.resultList != null && this.resultList.Count > 0)
						{
							TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Short-circuiting search, we were searching by message-id and we already got a result", new object[0]);
							return true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00062E40 File Offset: 0x00061040
		private TrackingAuthority GetServerHintAuthority()
		{
			if (string.IsNullOrEmpty(this.serverHint))
			{
				return null;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Locating authority for ServerHint: {0}", this.serverHint);
			IPAddress ipaddress = null;
			if (IPAddress.TryParse(this.serverHint, out ipaddress))
			{
				return null;
			}
			ServerInfo serverInfo = ServerCache.Instance.FindMailboxOrHubServer(this.serverHint, 34UL);
			if (serverInfo.Status == ServerStatus.NotFound || !serverInfo.IsSearchable)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "ServerHint not searchable", new object[0]);
				return null;
			}
			TrackingAuthority trackingAuthority = this.trackingDiscovery.FindLocationForOrgServer(serverInfo);
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "ServerHint authority: {0}", Names<TrackingAuthorityKind>.Map[(int)trackingAuthority.TrackingAuthorityKind]);
			if (trackingAuthority.TrackingAuthorityKind != TrackingAuthorityKind.CurrentSite && trackingAuthority.TrackingAuthorityKind != TrackingAuthorityKind.LegacyExchangeServer && trackingAuthority.TrackingAuthorityKind != TrackingAuthorityKind.RemoteSiteInCurrentOrg)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "ServerHint authority not an org-server", new object[0]);
				return null;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "ServerHint authority is an org-server", new object[0]);
			return trackingAuthority;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00062F50 File Offset: 0x00061150
		private void FindMessage(SearchScope scope, TrackingAuthority authority, ref List<MessageTrackingSearchResult> resultList)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Trying to find message with scope: {0}, authority: {1}", Names<SearchScope>.Map[(int)scope], Names<TrackingAuthorityKind>.Map[(int)authority.TrackingAuthorityKind]);
			if (authority.IsAllowedScope(scope))
			{
				this.FindMessagesForAuthority(authority, ref resultList);
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Scope not allowed for this authority", new object[0]);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00062FB4 File Offset: 0x000611B4
		private void FindMessageBackwards(SearchScope scope, TrackingAuthority recipientAuthority, bool usingServerHint, ref List<MessageTrackingSearchResult> results)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Trying to run FindMessageBackwards with scope: {0}, recip-authority: {1}", Names<SearchScope>.Map[(int)scope], Names<TrackingAuthorityKind>.Map[(int)recipientAuthority.TrackingAuthorityKind]);
			if (scope == SearchScope.World)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Scope downgraded from World to Organization since World is unsupported for backwards tracking", new object[0]);
				scope = SearchScope.Organization;
			}
			if (!recipientAuthority.IsAllowedScope(scope))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Scope {0} is not valid for authority {1}", Names<SearchScope>.Map[(int)scope], Names<TrackingAuthorityKind>.Map[(int)recipientAuthority.TrackingAuthorityKind]);
				return;
			}
			List<MessageTrackingSearchResult> list = null;
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Trying to find SMTP RECEIVE Message in recipient's authority", new object[0]);
			this.FindSMTPReceiveMessagesForAuthority(recipientAuthority, usingServerHint ? this.serverHint : string.Empty, ref list);
			if (scope == SearchScope.Site || this.expandTree)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, bool>(this.GetHashCode(), "Search scope = {0}, ExpandTree = {1}, so we're done searching messages received by recipient", Names<SearchScope>.Map[(int)scope], this.expandTree);
				results = list;
				return;
			}
			if (usingServerHint && list != null && list.Count > 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Found message using server-hint: {0}", this.serverHint);
				results = list;
				return;
			}
			if (this.useFirstHopIfAvailable && !string.IsNullOrEmpty(this.messageId) && list != null && list.Count > 0)
			{
				List<MessageTrackingSearchResult> list2 = null;
				bool flag;
				this.FollowResultsToFirstHopHub(list, scope, out list2, out flag);
				if (!flag)
				{
					results = list2;
					return;
				}
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No results found for recipient in recipient's authority. Broaden search to Org", new object[0]);
			this.FindSmtpReceiveMessageInAllSitesForOrg();
			if (this.resultList == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Message was not found anywhere in Org, ending search", new object[0]);
			}
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00063198 File Offset: 0x00061398
		internal void FindSmtpReceiveMessageInAllSitesForOrg()
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Trying to find SMTP RECEIVE messages in all sites in Org", new object[0]);
			IEnumerable<ADObjectId> allSitesInOrg = ServerCache.Instance.GetAllSitesInOrg(this.directoryContext.GlobalConfigSession);
			using (IEnumerator<ADObjectId> enumerator = allSitesInOrg.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ADObjectId siteId = enumerator.Current;
					TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(this.GetHashCode(), "Trying to find SMTP RECEIVE messages in Site: {0}", siteId);
					TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
					{
						TrackingAuthority authorityForSite = this.trackingDiscovery.GetAuthorityForSite(siteId);
						this.FindSMTPReceiveMessagesForAuthority(authorityForSite, string.Empty, ref this.resultList);
					}, this.Errors);
					if (ex != null && ex.IsOverBudgetError)
					{
						TraceWrapper.SearchLibraryTracer.TraceError<TrackingBaseException>(this.GetHashCode(), "Stop FindSmtpReceiveMessageInAllSitesForOrg due to over-budget error: {0}", ex);
						return;
					}
				}
			}
			if (this.resultList != null)
			{
				this.resultList = SearchMessageTrackingReportImpl.FilterForFirstHopResults(this.resultList);
			}
			this.wholeForestSearchExecuted = true;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000632A0 File Offset: 0x000614A0
		private void FollowResultsToFirstHopHub(List<MessageTrackingSearchResult> resultsToFollow, SearchScope currentSearchScope, out List<MessageTrackingSearchResult> finalResults, out bool forestSearchNeeded)
		{
			if (resultsToFollow == null)
			{
				throw new ArgumentNullException("resultsToFollow should not be null");
			}
			if (this.messageId == null)
			{
				throw new InvalidOperationException("Can only follow the first hop hub if searching by messageid");
			}
			if (this.TrackingAsSender && (this.recipients == null || this.recipients.Length == 0))
			{
				throw new InvalidOperationException("Backwards tracking from sender could not have been called without any recipients");
			}
			if (this.expandTree)
			{
				throw new InvalidOperationException("Backwards tracking cannot be called with expand tree");
			}
			finalResults = new List<MessageTrackingSearchResult>(resultsToFollow.Count);
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			forestSearchNeeded = false;
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in resultsToFollow)
			{
				string firstHopServer = messageTrackingSearchResult.FirstHopServer;
				if (string.IsNullOrEmpty(firstHopServer))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No first hop server on the event. Need to do all sites search.", new object[0]);
					forestSearchNeeded = true;
					return;
				}
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Evaluating result pointing to first hop {0}.", firstHopServer);
				if (SearchMessageTrackingReportImpl.IsSearchResultFirstHop(messageTrackingSearchResult))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<MessageTrackingReportId>(this.GetHashCode(), "Do not need to follow to first hop because the result {0} is already the first hop.", messageTrackingSearchResult.MessageTrackingReportId);
					finalResults.Add(messageTrackingSearchResult);
					hashSet.Add(firstHopServer);
				}
				else if (!ServerCache.Instance.FindMailboxOrHubServer(firstHopServer, 34UL).IsSearchable)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string, MessageTrackingReportId>(this.GetHashCode(), "First hop hub server {0} is not searchable. Assuming {1} is the first hop", firstHopServer, messageTrackingSearchResult.MessageTrackingReportId);
					finalResults.Add(messageTrackingSearchResult);
					hashSet.Add(firstHopServer);
				}
				else if (hashSet.Contains(firstHopServer))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "First hop hub {0} has already been searched.", firstHopServer);
				}
				else
				{
					List<MessageTrackingSearchResult> smtpReceiveLogsFromServer = this.GetSmtpReceiveLogsFromServer(firstHopServer, currentSearchScope);
					foreach (MessageTrackingSearchResult messageTrackingSearchResult2 in smtpReceiveLogsFromServer)
					{
						if (SearchMessageTrackingReportImpl.IsSearchResultFirstHop(messageTrackingSearchResult2))
						{
							finalResults.Add(messageTrackingSearchResult2);
						}
					}
					hashSet.Add(firstHopServer);
				}
			}
			this.TryImproveResultsUsingPreviousHop(ref finalResults);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000634E4 File Offset: 0x000616E4
		private void TryImproveResultsUsingPreviousHop(ref List<MessageTrackingSearchResult> finalResults)
		{
			List<MessageTrackingSearchResult> list = new List<MessageTrackingSearchResult>();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in finalResults)
			{
				list.Add(messageTrackingSearchResult);
				TraceWrapper.SearchLibraryTracer.TraceDebug<MessageTrackingReportId>(this.GetHashCode(), "Trying to improve result: {0}", messageTrackingSearchResult.MessageTrackingReportId);
				if (string.IsNullOrEmpty(messageTrackingSearchResult.PreviousHopServer))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No previous hop", new object[0]);
				}
				else
				{
					ServerInfo hubServer = ServerCache.Instance.GetHubServer(messageTrackingSearchResult.PreviousHopServer);
					if (hubServer.Status != ServerStatus.Searchable)
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Previous hop server {0} not searchable", messageTrackingSearchResult.PreviousHopServer);
					}
					else
					{
						SearchScope searchScope = this.scope;
						if (searchScope == SearchScope.World)
						{
							searchScope = SearchScope.Organization;
						}
						List<MessageTrackingSearchResult> results = null;
						TrackingAuthority trackingAuthority = this.trackingDiscovery.FindLocationForOrgServer(hubServer);
						if (!trackingAuthority.IsAllowedScope(searchScope))
						{
							TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Authority: {0} outside of scope: {1}", Names<TrackingAuthorityKind>.Map[(int)trackingAuthority.TrackingAuthorityKind], Names<SearchScope>.Map[(int)this.scope]);
						}
						else if (hashSet.Contains(hubServer.Key))
						{
							TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Already got results from this server, skipping: {0}", hubServer.Key);
						}
						else
						{
							hashSet.Add(hubServer.Key);
							SearchMessageTrackingReportImpl searchImpl = new SearchMessageTrackingReportImpl(this.directoryContext, searchScope, this.mailbox, this.senderIfTrackingAsRecip, hubServer.Key, null, this.logCache, null, this.messageId, Unlimited<uint>.UnlimitedValue, false, true, false, false);
							TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
							{
								results = searchImpl.Execute();
							}, this.Errors);
							if (ex != null && ex.IsOverBudgetError)
							{
								TraceWrapper.SearchLibraryTracer.TraceError<TrackingBaseException>(this.GetHashCode(), "Stop TryImproveResultsUsingPreviousHop due to over-budget error: {0}", ex);
								return;
							}
							if (results == null || results.Count == 0)
							{
								TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "No results from previous hop", new object[0]);
							}
							else
							{
								TraceWrapper.SearchLibraryTracer.TraceError<int>(this.GetHashCode(), "{0} results from previous hop, replacing", results.Count);
								foreach (MessageTrackingSearchResult messageTrackingSearchResult2 in results)
								{
									TraceWrapper.SearchLibraryTracer.TraceError<MessageTrackingReportId>(this.GetHashCode(), "Adding result from server: {0}", messageTrackingSearchResult2.MessageTrackingReportId);
									list.RemoveAt(list.Count - 1);
									list.AddRange(results);
								}
							}
						}
					}
				}
			}
			finalResults = list;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000637E0 File Offset: 0x000619E0
		private List<MessageTrackingSearchResult> GetSmtpReceiveLogsFromServer(string server, SearchScope currentSearchScope)
		{
			List<MessageTrackingSearchResult> list = new List<MessageTrackingSearchResult>(1);
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Finding message using first hop server: {0}", server);
			ServerInfo serverInfo = ServerCache.Instance.FindMailboxOrHubServer(server, 32UL);
			if (!serverInfo.IsSearchable)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Error: first hop hub server {0} is not searchable.", server);
				return list;
			}
			TrackingAuthority trackingAuthority = this.trackingDiscovery.FindLocationForOrgServer(serverInfo);
			if (!trackingAuthority.IsAllowedScope(currentSearchScope))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "server {0} is not in scope for the current search.", server);
				return list;
			}
			this.FindSMTPReceiveMessagesForAuthority(trackingAuthority, server, ref list);
			TraceWrapper.SearchLibraryTracer.TraceDebug<int, string>(this.GetHashCode(), "Found {0} results in server {1}.", list.Count, server);
			return list;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0006388F File Offset: 0x00061A8F
		private void FindMessagesForAuthority(TrackingAuthority authority, ref List<MessageTrackingSearchResult> resultList)
		{
			if (this.Sender != null)
			{
				this.FindStoreDriverReceiveMessagesForAuthority(authority, ref resultList);
				return;
			}
			this.FindSMTPReceiveMessagesForAuthority(authority, string.Empty, ref resultList);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000638B0 File Offset: 0x00061AB0
		private void FindStoreDriverReceiveMessagesForAuthority(TrackingAuthority authority, ref List<MessageTrackingSearchResult> resultList)
		{
			switch (authority.TrackingAuthorityKind)
			{
			case TrackingAuthorityKind.CurrentSite:
				this.RpcTrackMessagesFromSenderMailboxInLocalSite(this.Sender, null, ref resultList);
				return;
			case TrackingAuthorityKind.RemoteSiteInCurrentOrg:
			case TrackingAuthorityKind.RemoteForest:
			case TrackingAuthorityKind.RemoteTrustedOrg:
				this.WSFindStoredriverReceiveMessagesForAuthority((WebServiceTrackingAuthority)authority, ref resultList);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000638F8 File Offset: 0x00061AF8
		private void FindSMTPReceiveMessagesForAuthority(TrackingAuthority authority, string serverToSearch, ref List<MessageTrackingSearchResult> resultList)
		{
			switch (authority.TrackingAuthorityKind)
			{
			case TrackingAuthorityKind.CurrentSite:
				this.RpcTrackSMTPReceiveMessagesWithinSite(serverToSearch, ref resultList);
				return;
			case TrackingAuthorityKind.RemoteSiteInCurrentOrg:
			case TrackingAuthorityKind.RemoteTrustedOrg:
				this.WSFindSMTPReceiveMessagesForAuthority((WebServiceTrackingAuthority)authority, serverToSearch, ref resultList);
				break;
			case TrackingAuthorityKind.RemoteForest:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0006393C File Offset: 0x00061B3C
		private void WSFindStoredriverReceiveMessagesForAuthority(WebServiceTrackingAuthority authority, ref List<MessageTrackingSearchResult> resultList)
		{
			if (this.Sender == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "WSFindStoredriverReceiveMessagesForAuthority: sender is null", new object[0]);
				throw new InvalidOperationException("Sender cannot be null");
			}
			this.WSTrackMessagesForAuthority(new SmtpAddress?(this.Sender.SmtpAddress), authority, null, ref resultList);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00063990 File Offset: 0x00061B90
		private void WSFindSMTPReceiveMessagesForAuthority(WebServiceTrackingAuthority authority, string serverToSearch, ref List<MessageTrackingSearchResult> resultList)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Searching for SMTP RECEIVE messages over web-service", new object[0]);
			this.WSTrackMessagesForAuthority((this.Sender == null) ? null : new SmtpAddress?(this.Sender.SmtpAddress), authority, serverToSearch, ref resultList);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x000639E4 File Offset: 0x00061BE4
		private void WSTrackMessagesForAuthority(SmtpAddress? senderAddress, WebServiceTrackingAuthority authority, string serverToSearch, ref List<MessageTrackingSearchResult> resultList)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<Uri>(this.GetHashCode(), "Searching over web-service, Uri: {0}", authority.Uri);
			IWebServiceBinding ewsBinding = authority.GetEwsBinding(this.directoryContext);
			FindMessageTrackingReportResponseMessageType findMessageTrackingReportResponseMessageType = ewsBinding.FindMessageTrackingReport(authority.Domain, senderAddress, this.RecipientCriterionForExpand, serverToSearch, null, authority.AssociatedScope, this.messageId, this.subjectKeyword, this.expandTree, this.searchAsRecip, this.moderationResultSearch, this.start, this.end, this.directoryContext.TrackingBudget);
			if (findMessageTrackingReportResponseMessageType != null && findMessageTrackingReportResponseMessageType.MessageTrackingSearchResults != null)
			{
				FindMessageTrackingSearchResultType[] messageTrackingSearchResults = findMessageTrackingReportResponseMessageType.MessageTrackingSearchResults;
				TraceWrapper.SearchLibraryTracer.TraceDebug<int>(this.GetHashCode(), "Got {0} results", messageTrackingSearchResults.Length);
				foreach (FindMessageTrackingSearchResultType wsResult in messageTrackingSearchResults)
				{
					this.AddMessageTrackingSearchResult(wsResult, ref resultList, ewsBinding.TargetInfoForDisplay);
				}
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No result returned by web-service", new object[0]);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00063B7C File Offset: 0x00061D7C
		private void RpcTrackMessagesFromSenderMailboxInLocalSite(TrackedUser submitter, TrackedUser purportedSenderToMatch, ref List<MessageTrackingSearchResult> resultList)
		{
			if (!submitter.IsMailbox)
			{
				throw new InvalidOperationException("RpcTrackMessagesFromSenderMailboxInLocalSite cannot be called for a non-mailbox sender");
			}
			Dictionary<ADObjectId, IList<ServerInfo>> userMailboxLocationsBySite = this.trackingDiscovery.GetUserMailboxLocationsBySite(submitter.ADUser);
			ADObjectId localServerSiteId = ServerCache.Instance.GetLocalServerSiteId(this.directoryContext);
			IList<ServerInfo> list = null;
			if (!userMailboxLocationsBySite.TryGetValue(localServerSiteId, out list))
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "No local mailbox servers found in this site", new object[0]);
				return;
			}
			List<MessageTrackingSearchResult> localResultList = resultList;
			using (IEnumerator<ServerInfo> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ServerInfo server = enumerator.Current;
					if (!this.directoryContext.TrackingBudget.IsUnderBudget())
					{
						TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Not getting STOREDRIVER SUBMIT, over-budget", new object[0]);
						return;
					}
					TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
					{
						if (this.moderationResultSearch)
						{
							this.RpcTrackModerationResultInServerInLocalSite(server, this.messageId, ref localResultList);
							return;
						}
						this.RpcTrackMessagesFromSenderMailboxInLocalSite(server, submitter, purportedSenderToMatch, ref localResultList);
					}, this.Errors);
					if (ex != null)
					{
						if (ex.IsOverBudgetError)
						{
							TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Not looking at all servers for STOREDRIVER SUBMIT, over-budget", new object[0]);
							return;
						}
						TraceWrapper searchLibraryTracer = TraceWrapper.SearchLibraryTracer;
						int hashCode = this.GetHashCode();
						string formatString = "Could not get logs from server, continuing to search other servers: {0}";
						ServerInfo server2 = server;
						searchLibraryTracer.TraceError<string>(hashCode, formatString, server2.Key);
					}
				}
			}
			resultList = localResultList;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00063D68 File Offset: 0x00061F68
		private void RpcTrackModerationResultInServerInLocalSite(ServerInfo server, string messageId, ref List<MessageTrackingSearchResult> resultList)
		{
			if (!server.IsSearchable)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Server {0} unsearchable, RpcTrackMessagesFromSenderMailboxInLocalSite skipping it", server.Key);
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Searching for moderation result for {0} on server: {1}", messageId, server.Key);
			List<MessageTrackingLogEntry> entries = null;
			ILogReader reader = RpcLogReader.GetLogReader(server.Key, this.directoryContext);
			if (reader == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Log reader not available.", new object[0]);
				return;
			}
			TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
			{
				entries = this.logCache.GetMessageLog(RpcReason.Submit, reader, TrackingLogPrefix.MSGTRK, null, MessageTrackingSource.APPROVAL, SearchMessageTrackingReportImpl.ModeratorDecisionEventsFilterSet, null, this.messageId);
			}, this.Errors);
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, TrackingBaseException>(this.GetHashCode(), "Cannot get moderation result events from server {0}. Exception: {1}.", server.Key, ex);
				return;
			}
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in entries)
			{
				if (!this.directoryContext.IsTenantInScope(messageTrackingLogEntry.TenantId))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string, OrganizationId, string>(this.GetHashCode(), "row skipped for msg-id: {0}.  Searching for tenant {1}, but row is for {2}", messageTrackingLogEntry.MessageId, this.directoryContext.OrganizationId, messageTrackingLogEntry.TenantId);
				}
				else
				{
					if ((messageTrackingLogEntry.EventId == MessageTrackingEvent.MODERATORAPPROVE || messageTrackingLogEntry.EventId == MessageTrackingEvent.MODERATORREJECT || messageTrackingLogEntry.EventId == MessageTrackingEvent.MODERATORSALLNDR) && (messageTrackingLogEntry.RecipientAddresses == null || messageTrackingLogEntry.RecipientAddresses.Length == 0))
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "row skipped for moderator decision with msg-id: {0} because it has no recipients.  Logs from E14 RTM error message dropped. ", messageTrackingLogEntry.MessageId);
						this.Errors.Add(ErrorCode.ModerationDecisionLogsFromE14Rtm, messageTrackingLogEntry.Server, string.Empty, string.Empty);
						break;
					}
					if (resultList == null)
					{
						resultList = new List<MessageTrackingSearchResult>(1);
					}
					resultList.Add(this.CreateMessageTrackingSearchResult(messageTrackingLogEntry));
					break;
				}
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<int>(this.GetHashCode(), "Returning {0} results for moderation result.", (resultList == null) ? 0 : resultList.Count);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00063FC4 File Offset: 0x000621C4
		private void RpcTrackMessagesFromSenderMailboxInLocalSite(ServerInfo server, TrackedUser submitter, TrackedUser purportedSenderToMatch, ref List<MessageTrackingSearchResult> resultList)
		{
			if (!server.IsSearchable)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Server {0} unsearchable, RpcTrackMessagesFromSenderMailboxInLocalSite skipping it", server.Key);
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Searching for STOREDRIVER SUBMIT logs on server: {0}", server.Key);
			List<MessageTrackingLogEntry> entries = null;
			ILogReader reader = RpcLogReader.GetLogReader(server.Key, this.directoryContext);
			if (reader == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Log reader not available.", new object[0]);
				return;
			}
			TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
			{
				entries = this.logCache.GetMessageLog(RpcReason.Submit, reader, TrackingLogPrefix.MSGTRK, submitter.ProxyAddresses, MessageTrackingSource.STOREDRIVER, SearchMessageTrackingReportImpl.SubmitEventFilterSet, null, this.messageId);
			}, this.Errors);
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, TrackingBaseException>(this.GetHashCode(), "Cannot get submit events from server {0}. Exception: {1}.", server.Key, ex);
				return;
			}
			ICollection<MessageTrackingSearchResult> collection;
			if (VersionConverter.GetMtrSchemaVersion(server) >= MtrSchemaVersion.E15RTM)
			{
				collection = this.ProcessRpcSubmitEventsInLocalSite(entries, purportedSenderToMatch);
			}
			else
			{
				collection = this.ProcessRpcSubmitEventsInLocalSitePre15(entries, purportedSenderToMatch, submitter);
			}
			if (collection != null && collection.Count > 0)
			{
				if (resultList == null)
				{
					resultList = new List<MessageTrackingSearchResult>(collection.Count);
				}
				resultList.AddRange(collection);
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000640FC File Offset: 0x000622FC
		private ICollection<MessageTrackingSearchResult> ProcessRpcSubmitEventsInLocalSite(List<MessageTrackingLogEntry> entries, TrackedUser purportedSenderToMatch)
		{
			Dictionary<string, MessageTrackingSearchResult> dictionary = new Dictionary<string, MessageTrackingSearchResult>(1);
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in entries)
			{
				MessageTrackingLogRow logRow = messageTrackingLogEntry.LogRow;
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Found STOREDRIVER SUBMIT event for message-id: {0} on Server: {1}", logRow.MessageId, logRow.ServerFqdn);
				if (string.IsNullOrEmpty(logRow.ServerHostName))
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Could not look up Hub server based on SUBMIT event.  ServerHostName null or empty.", logRow.ToString());
				}
				else if (purportedSenderToMatch == null || this.VerifyEventMatchesPurportedSender(logRow, purportedSenderToMatch))
				{
					if (!this.directoryContext.IsTenantInScope(logRow.TenantId))
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug<string, OrganizationId, string>(this.GetHashCode(), "row skipped for msg-id: {0}.  Searching for tenant {1}, but row is for {2}", logRow.MessageId, this.directoryContext.OrganizationId, logRow.TenantId);
					}
					else
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Adding tracking search result for SUBMIT event on server: {0}, messageid: {1}", logRow.ServerFqdn, logRow.MessageId);
						this.AddEventAndKeepLatestOnDuplicates(messageTrackingLogEntry, ref dictionary);
					}
				}
			}
			return dictionary.Values;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000642A8 File Offset: 0x000624A8
		private ICollection<MessageTrackingSearchResult> ProcessRpcSubmitEventsInLocalSitePre15(List<MessageTrackingLogEntry> entries, TrackedUser purportedSenderToMatch, TrackedUser submitter)
		{
			Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>();
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in entries)
			{
				MessageTrackingLogRow logRow = messageTrackingLogEntry.LogRow;
				HashSet<string> hashSet = null;
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Found STOREDRIVER SUBMIT event for message-id: {0} on Server: {1}", logRow.MessageId, logRow.ServerFqdn);
				if (string.IsNullOrEmpty(logRow.ServerHostName))
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Could not look up Hub server based on SUBMIT event.  ServerHostName null or empty.", logRow.ToString());
				}
				else
				{
					if (!dictionary.TryGetValue(logRow.ServerHostName, out hashSet))
					{
						hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
						dictionary[logRow.ServerHostName] = hashSet;
					}
					if (!hashSet.Contains(this.messageId))
					{
						hashSet.Add(logRow.MessageId);
					}
				}
			}
			Dictionary<string, MessageTrackingSearchResult> dictionary2 = new Dictionary<string, MessageTrackingSearchResult>(1);
			foreach (string text in dictionary.Keys)
			{
				List<MessageTrackingLogEntry> receiveEvents = null;
				if (!ServerCache.Instance.FindMailboxOrHubServer(text, 32UL).IsSearchable)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Server unsearchable", new object[0]);
				}
				else
				{
					ILogReader reader = RpcLogReader.GetLogReader(text, this.directoryContext);
					if (reader == null)
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Log reader not available.", new object[0]);
					}
					else
					{
						HashSet<string> hashSet2 = dictionary[text];
						TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
						{
							receiveEvents = this.logCache.GetMessageLog(RpcReason.Receive, reader, TrackingLogPrefix.MSGTRK, submitter.ProxyAddresses, MessageTrackingSource.STOREDRIVER, SearchMessageTrackingReportImpl.ReceiveEventFilterSet, this.GetRecipientProxyFilter(this.recipientProxyAddresses), this.messageId);
						}, this.Errors);
						if (ex != null)
						{
							if (ex.IsOverBudgetError)
							{
								TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Not getting Hub RECEIVE events due to overbudget", new object[0]);
								return null;
							}
							TraceWrapper.SearchLibraryTracer.TraceError<string, TrackingBaseException>(this.GetHashCode(), "Skipping server. Cannot get events from hub server {0}. Exception: {1}.", text, ex);
						}
						else
						{
							foreach (MessageTrackingLogEntry messageTrackingLogEntry2 in receiveEvents)
							{
								MessageTrackingLogRow logRow2 = messageTrackingLogEntry2.LogRow;
								if (purportedSenderToMatch == null || this.VerifyEventMatchesPurportedSender(logRow2, purportedSenderToMatch))
								{
									if (!this.directoryContext.IsTenantInScope(logRow2.TenantId))
									{
										TraceWrapper.SearchLibraryTracer.TraceDebug<string, OrganizationId, string>(this.GetHashCode(), "row skipped for msg-id: {0}.  Searching for tenant {1}, but row is for {2}", logRow2.MessageId, this.directoryContext.OrganizationId, logRow2.TenantId);
									}
									else if (!hashSet2.Contains(logRow2.MessageId))
									{
										TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Skipped message-id: {0}, doesn't correspond to any SUBMIT event in this batch", logRow2.MessageId);
									}
									else
									{
										TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Adding tracking search result for receive event on server: {0}, messageid: {1}", logRow2.ServerFqdn, logRow2.MessageId);
										this.AddEventAndKeepLatestOnDuplicates(messageTrackingLogEntry2, ref dictionary2);
									}
								}
							}
						}
					}
				}
			}
			return dictionary2.Values;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00064624 File Offset: 0x00062824
		private ProxyAddressCollection[] GetRecipientProxyFilter(ProxyAddressCollection[] recipientProxyAddresses)
		{
			if (!this.expandTree && this.recipientProxyAddresses != null && recipientProxyAddresses.Length != 0)
			{
				return recipientProxyAddresses;
			}
			return null;
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00064640 File Offset: 0x00062840
		private void AddEventAndKeepLatestOnDuplicates(MessageTrackingLogEntry entry, ref Dictionary<string, MessageTrackingSearchResult> eventsDictionary)
		{
			MessageTrackingSearchResult messageTrackingSearchResult = this.ApplyRecipCriteriaAndConvertToMessageTrackingSearchResult(entry);
			if (messageTrackingSearchResult == null)
			{
				return;
			}
			string text = messageTrackingSearchResult.MessageTrackingReportId.MessageId;
			if (eventsDictionary == null)
			{
				eventsDictionary = new Dictionary<string, MessageTrackingSearchResult>();
				eventsDictionary.Add(text, messageTrackingSearchResult);
				return;
			}
			MessageTrackingSearchResult messageTrackingSearchResult2;
			if (!eventsDictionary.TryGetValue(text, out messageTrackingSearchResult2))
			{
				eventsDictionary.Add(text, messageTrackingSearchResult);
				return;
			}
			if (messageTrackingSearchResult2.SubmittedDateTime <= messageTrackingSearchResult.SubmittedDateTime)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Replacing previous submit event with message id '{0}' with one that has later submit time ", text);
				eventsDictionary[text] = messageTrackingSearchResult;
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000646C4 File Offset: 0x000628C4
		private void RpcTrackSMTPReceiveMessagesWithinSite(string serverToSearch, ref List<MessageTrackingSearchResult> resultList)
		{
			List<string> list = null;
			HashSet<string> hashSet = null;
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Searching for messages over RPC in local site", new object[0]);
			int num;
			if (!string.IsNullOrEmpty(serverToSearch))
			{
				num = 1;
				hashSet = new HashSet<string>();
				hashSet.Add(serverToSearch);
				list = new List<string>(1);
				list.Add(serverToSearch);
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Using server for server hint", new object[0]);
			}
			else
			{
				num = ServerCache.Instance.GetHubServersInSite(ServerCache.Instance.GetLocalServerSiteId(this.directoryContext), out list, out hashSet);
			}
			if (num == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "No HUB servers found in local site", new object[0]);
				return;
			}
			foreach (string serverNameOrFqdn in list)
			{
				ServerInfo serverInfo = ServerCache.Instance.FindMailboxOrHubServer(serverNameOrFqdn, 32UL);
				this.RpcTrackSMTPReceiveMessagesForSiteServer(serverInfo, hashSet, ref resultList);
			}
			if (resultList == null || resultList.Count == 0)
			{
				foreach (string serverNameOrFqdn2 in list)
				{
					ServerInfo serverInfo2 = ServerCache.Instance.FindMailboxOrHubServer(serverNameOrFqdn2, 2UL);
					this.RpcTrackStoreDriverDeliverMessagesForSiteServer(serverInfo2, hashSet, ref resultList);
				}
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00064890 File Offset: 0x00062A90
		private void RpcTrackSMTPReceiveMessagesForSiteServer(ServerInfo serverInfo, HashSet<string> serverNamesTable, ref List<MessageTrackingSearchResult> resultList)
		{
			if (!serverInfo.IsSearchable)
			{
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Getting SMTP RECEIVE events on HUB: {0}", serverInfo.Key);
			ILogReader reader = RpcLogReader.GetLogReader(serverInfo.Key, this.directoryContext);
			if (reader == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Cannot get Log reader for searchable hub {0}.", serverInfo.Key);
				return;
			}
			List<MessageTrackingLogEntry> entries = null;
			TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
			{
				entries = this.logCache.GetMessageLog(RpcReason.SmtpReceive, reader, TrackingLogPrefix.MSGTRK, (this.Sender != null) ? this.Sender.ProxyAddresses : null, MessageTrackingSource.SMTP, SearchMessageTrackingReportImpl.SmtpReceiveOrRejectEventsFilterSet, null, this.messageId);
			}, this.Errors);
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, TrackingBaseException>(this.GetHashCode(), "Cannot get events from server {0}. Exception: {1}.", serverInfo.Key, ex);
				return;
			}
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in entries)
			{
				MessageTrackingLogRow logRow = messageTrackingLogEntry.LogRow;
				if (logRow.EventId != MessageTrackingEvent.RECEIVE || !this.ShouldSkipReceiveEvent(logRow, serverNamesTable))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Found SMTP RECEIVE event with message-id: {0}", logRow.MessageId);
					this.AddMessageTrackingSearchResultAndApplyRecipCriteria(messageTrackingLogEntry, ref resultList);
				}
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000649CC File Offset: 0x00062BCC
		private bool ShouldSkipReceiveEvent(MessageTrackingLogRow row, HashSet<string> serverNamesTable)
		{
			string customData = row.GetCustomData<string>("ProxiedClientHostname", string.Empty);
			bool flag = string.IsNullOrEmpty(customData) ? serverNamesTable.Contains(row.ClientHostName) : serverNamesTable.Contains(customData.Trim());
			if (flag)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Discarding SMTP RECEIVE event from server {0} in same site", row.ClientHostName);
			}
			return flag;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00064A94 File Offset: 0x00062C94
		private void RpcTrackStoreDriverDeliverMessagesForSiteServer(ServerInfo serverInfo, HashSet<string> serverNamesTable, ref List<MessageTrackingSearchResult> resultList)
		{
			if (!serverInfo.IsSearchable)
			{
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Getting STOREDRIVER DELIVER events on HUB: {0}", serverInfo.Key);
			ILogReader reader = RpcLogReader.GetLogReader(serverInfo.Key, this.directoryContext);
			if (reader == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Cannot get Log reader for searchable hub {0}.", serverInfo.Key);
				return;
			}
			List<MessageTrackingLogEntry> entries = null;
			TrackingBaseException ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
			{
				entries = this.logCache.GetMessageLog(RpcReason.None, reader, TrackingLogPrefix.MSGTRK, (this.Sender != null) ? this.Sender.ProxyAddresses : null, MessageTrackingSource.STOREDRIVER, SearchMessageTrackingReportImpl.ReceiveOrDeliverEventsFilterSet, null, this.messageId);
			}, this.Errors);
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, TrackingBaseException>(this.GetHashCode(), "Cannot get events from server {0}. Exception: {1}.", serverInfo.Key, ex);
				return;
			}
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in entries)
			{
				MessageTrackingLogRow logRow = messageTrackingLogEntry.LogRow;
				if (logRow.EventId == MessageTrackingEvent.DELIVER || logRow.EventId == MessageTrackingEvent.DUPLICATEDELIVER)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Found STOREDRIVER DELIVER event with message-id: {0}", logRow.MessageId);
					this.AddMessageTrackingSearchResultAndApplyRecipCriteria(messageTrackingLogEntry, ref resultList);
				}
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00064BD0 File Offset: 0x00062DD0
		private MessageTrackingSearchResult ApplyRecipCriteriaAndConvertToMessageTrackingSearchResult(MessageTrackingLogEntry entry)
		{
			if (!entry.LogRow.IsLogCompatible)
			{
				string text = string.Format("Skipping result because it can not be interpreted by this server. Log is from Server: {0}, Message-Id: {1}", entry.Server, entry.MessageId);
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), text, new object[0]);
				this.Errors.Add(ErrorCode.LogVersionIncompatible, entry.Server, string.Empty, text);
				return null;
			}
			MessageTrackingSearchResult messageTrackingSearchResult = this.CreateMessageTrackingSearchResult(entry);
			if (messageTrackingSearchResult == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, string>(this.GetHashCode(), "Tracking failed. Could not convert row to search-result, Server: {0}, Message-Id: {1}", entry.Server, entry.MessageId);
				TrackingFatalException.AddAndRaiseED(this.Errors, ErrorCode.UnexpectedErrorPermanent, "Could not convert row from server {0} to search result", new object[]
				{
					entry.Server
				});
			}
			if (this.expandTree && !this.IsTrackeeMessageRecipient(messageTrackingSearchResult))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Skipping row, because trackee is not a recipient", entry.Server, entry.MessageId);
				return null;
			}
			return messageTrackingSearchResult;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00064CBC File Offset: 0x00062EBC
		private void AddMessageTrackingSearchResultAndApplyRecipCriteria(MessageTrackingLogEntry entry, ref List<MessageTrackingSearchResult> resultList)
		{
			MessageTrackingSearchResult messageTrackingSearchResult = this.ApplyRecipCriteriaAndConvertToMessageTrackingSearchResult(entry);
			if (messageTrackingSearchResult == null)
			{
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Adding tracking search result for receive event on server: {0}, messageid: {1}", entry.Server, entry.MessageId);
			this.AddSearchResultToResultList(messageTrackingSearchResult, ref resultList);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00064D00 File Offset: 0x00062F00
		private void AddMessageTrackingSearchResult(FindMessageTrackingSearchResultType wsResult, ref List<MessageTrackingSearchResult> resultList, string targetInfoForDisplay)
		{
			if (!this.TrackingAsSender)
			{
				MessageTrackingReportId messageTrackingReportId;
				if (!MessageTrackingReportId.TryParse(wsResult.MessageTrackingReportId, out messageTrackingReportId))
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Could not parse MessageTrackingReportId from {0}.", wsResult.MessageTrackingReportId);
					return;
				}
				if (this.mailbox != null && this.mailbox.ADUser != null)
				{
					Guid exchangeGuid = this.mailbox.ADUser.ExchangeGuid;
					MessageTrackingReportId messageTrackingReportId2 = new MessageTrackingReportId(messageTrackingReportId.MessageId, messageTrackingReportId.Server, messageTrackingReportId.InternalMessageId, exchangeGuid, messageTrackingReportId.Domain, false);
					TraceWrapper.SearchLibraryTracer.TraceDebug<string, MessageTrackingReportId>(this.GetHashCode(), "Patching message-id: {0} --> {1}", wsResult.MessageTrackingReportId, messageTrackingReportId2);
					wsResult.MessageTrackingReportId = messageTrackingReportId2.ToString();
				}
			}
			MessageTrackingSearchResult searchResult = MessageTrackingSearchResult.Create(wsResult, targetInfoForDisplay);
			this.AddSearchResultToResultList(searchResult, ref resultList);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00064DC1 File Offset: 0x00062FC1
		private void AddSearchResultToResultList(MessageTrackingSearchResult searchResult, ref List<MessageTrackingSearchResult> resultList)
		{
			if (resultList == null)
			{
				resultList = new List<MessageTrackingSearchResult>();
			}
			resultList.Add(searchResult);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00064E24 File Offset: 0x00063024
		private List<MessageTrackingSearchResult> FilterMessagesToRecipient(List<MessageTrackingSearchResult> initialResults)
		{
			List<MessageTrackingSearchResult> results = null;
			Dictionary<string, MessageTrackingSearchResult> dictionary = null;
			TrackingAuthority senderAuthority = null;
			TrackedUser[] array = new TrackedUser[]
			{
				this.mailbox
			};
			TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress>(this.GetHashCode(), "Filtering messages to recipient: {0}", this.mailbox.SmtpAddress);
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Trying to get SenderAuthority", new object[0]);
			SearchMessageTrackingReportImpl.TryExecuteTask(delegate
			{
				senderAuthority = this.SenderAuthority();
			}, this.Errors);
			int count;
			TrackingBaseException ex;
			if (senderAuthority != null && senderAuthority.IsAllowedScope(SearchScope.World))
			{
				count = this.Errors.Errors.Count;
				SearchMessageTrackingReportImpl senderSiteSearch = new SearchMessageTrackingReportImpl(this.directoryContext, SearchScope.World, this.senderIfTrackingAsRecip, null, null, array, this.logCache, this.subjectKeyword, this.messageId, this.resultSize, true, false, true, false);
				ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
				{
					results = senderSiteSearch.Execute();
				}, this.Errors);
				if (count < this.Errors.Errors.Count)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Encountered errors searching sender-site - some messages may be dropped from result list as we could not validate that the sender actually sent them", new object[0]);
				}
				if (!this.directoryContext.TrackingBudget.IsUnderBudget() || (ex != null && ex.IsOverBudgetError))
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Over budget error during recipient side search, aborting", new object[0]);
					return results;
				}
				dictionary = SearchMessageTrackingReportImpl.GetMessageIdMap(results);
			}
			count = this.Errors.Errors.Count;
			SearchMessageTrackingReportImpl recipientSiteSearch = new SearchMessageTrackingReportImpl(this.directoryContext, SearchScope.World, this.mailbox, this.senderIfTrackingAsRecip, null, array, this.logCache, this.subjectKeyword, this.messageId, this.resultSize, true, true, true, false);
			ex = SearchMessageTrackingReportImpl.TryExecuteTask(delegate
			{
				results = recipientSiteSearch.Execute();
			}, this.Errors);
			if (count < this.Errors.Errors.Count)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Encountered errors searching recipient-site. Not all results may be available", new object[0]);
			}
			if (!this.directoryContext.TrackingBudget.IsUnderBudget() || (ex != null && ex.IsOverBudgetError))
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Over budget error during recipient side search, aborting", new object[0]);
				return results;
			}
			Dictionary<string, MessageTrackingSearchResult> messageIdMap = SearchMessageTrackingReportImpl.GetMessageIdMap(results);
			List<MessageTrackingSearchResult> list = new List<MessageTrackingSearchResult>();
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in this.resultList)
			{
				string key = messageTrackingSearchResult.MessageTrackingReportId.MessageId;
				MessageTrackingSearchResult messageTrackingSearchResult2 = null;
				if ((messageIdMap != null && messageIdMap.TryGetValue(key, out messageTrackingSearchResult2)) || (dictionary != null && dictionary.TryGetValue(key, out messageTrackingSearchResult2)))
				{
					messageTrackingSearchResult.Subject = messageTrackingSearchResult2.Subject;
					list.Add(messageTrackingSearchResult);
				}
				else
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<MessageTrackingReportId, string, SmtpAddress>(this.GetHashCode(), "Dropping message: {0} [Subject: {1}], as we could not verify that it was addressed to recipient: {2}", messageTrackingSearchResult.MessageTrackingReportId, messageTrackingSearchResult.Subject, this.mailbox.SmtpAddress);
				}
			}
			return list;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00065194 File Offset: 0x00063394
		private bool IsTrackeeMessageRecipient(MessageTrackingSearchResult searchResult)
		{
			SmtpAddress? recipientCriterionForExpand = this.RecipientCriterionForExpand;
			if (recipientCriterionForExpand == null)
			{
				return true;
			}
			if (searchResult.RecipientAddresses == null)
			{
				return false;
			}
			foreach (SmtpAddress smtpAddress in searchResult.RecipientAddresses)
			{
				if (recipientCriterionForExpand.Equals(smtpAddress))
				{
					return true;
				}
			}
			int count = this.Errors.Errors.Count;
			GetMessageTrackingReportImpl summaryImpl = new GetMessageTrackingReportImpl(this.directoryContext, SearchScope.Site, searchResult.MessageTrackingReportId, this.logCache, this.GetConstraints());
			MessageTrackingReport report = null;
			SearchMessageTrackingReportImpl.TryExecuteTask(delegate
			{
				report = summaryImpl.Execute();
			}, this.Errors);
			if (count < this.Errors.Errors.Count)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<MessageTrackingReportId, string>(this.GetHashCode(), "Report-id {0} had errors. The report is probably incomplete and may cause this message to be not shown in the result list (Subject={1})", searchResult.MessageTrackingReportId, searchResult.Subject);
			}
			if (report != null)
			{
				IEnumerable<RecipientTrackingEvent> recipientTrackingEvents = report.RecipientTrackingEvents;
				foreach (RecipientTrackingEvent recipientTrackingEvent in recipientTrackingEvents)
				{
					if (recipientCriterionForExpand.Equals(recipientTrackingEvent.RecipientAddress))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0006530C File Offset: 0x0006350C
		private bool VerifyEventMatchesPurportedSender(MessageTrackingLogRow row, TrackedUser purportedSender)
		{
			KeyValuePair<string, object>[] customData = row.CustomData;
			if (customData == null || customData.Length == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Attempted to verify purpored sender on a tracking log entry with null or empty CustomData field.", new object[0]);
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in customData)
			{
				if (string.Equals(keyValuePair.Key, "PurportedSender", StringComparison.OrdinalIgnoreCase))
				{
					string text = keyValuePair.Value as string;
					if (text != null)
					{
						return purportedSender.ProxyAddresses.Contains(ProxyAddress.Parse("smtp", text));
					}
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(0, "Tracking log entry contained null or invalid value for PurportedSender property: {0}.", (string)keyValuePair.Value);
				}
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Attempted to verify purpored sender, but CustomData did not have a PurportedSenderValue", new object[0]);
			return false;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000653DC File Offset: 0x000635DC
		private ReportConstraints GetConstraints()
		{
			if (this.constraints == null)
			{
				this.constraints = new ReportConstraints();
				this.constraints.Status = null;
				this.constraints.BypassDelegateChecking = false;
				this.constraints.DetailLevel = MessageTrackingDetailLevel.Basic;
				this.constraints.DoNotResolve = false;
				this.constraints.RecipientPathFilter = this.RecipientAddressArray;
				this.constraints.ReportTemplate = ReportTemplate.Summary;
				this.constraints.ResultSize = 1000U;
				this.constraints.Sender = this.Sender.SmtpAddress;
				this.constraints.TrackingAsSender = this.TrackingAsSender;
				this.constraints.ReturnQueueEvents = false;
			}
			return this.constraints;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000654A0 File Offset: 0x000636A0
		internal List<MessageTrackingSearchResult> FilterResultsBySubjectAndRecipients(List<MessageTrackingSearchResult> messages, IList<CultureInfo> cultures)
		{
			if (messages == null)
			{
				return messages;
			}
			bool flag = this.tokenizedSubjectKeywords != null && this.tokenizedSubjectKeywords.Length > 0;
			bool flag2 = this.recipients != null && this.recipients.Length > 0;
			if (!flag && !flag2)
			{
				return messages;
			}
			List<MessageTrackingSearchResult> list = null;
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in messages)
			{
				if (flag && !this.SubjectMatchesFilter(messageTrackingSearchResult.Subject, cultures))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Skipping result for because of subject filter", new object[0]);
				}
				else if (flag2 && !this.AnyAddressMatchesRecipientsFilter(messageTrackingSearchResult.RecipientAddresses))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Skipping result for because of recipients filter", new object[0]);
				}
				else
				{
					if (list == null)
					{
						list = new List<MessageTrackingSearchResult>();
					}
					list.Add(messageTrackingSearchResult);
				}
			}
			return list;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0006558C File Offset: 0x0006378C
		private MessageTrackingSearchResult CreateMessageTrackingSearchResult(MessageTrackingLogEntry entry)
		{
			string[] array = entry.RecipientAddresses ?? new string[0];
			SmtpAddress[] array2 = new SmtpAddress[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = SmtpAddress.Parse(array[i]);
			}
			SmtpAddress fromAddress = SmtpAddress.Parse(entry.SenderAddress);
			TrackedUser trackedUser = TrackedUser.Create(entry.SenderAddress, this.directoryContext.TenantGalSession);
			if (trackedUser == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Sender was resolved but invalid", new object[0]);
				trackedUser = TrackedUser.CreateUnresolved(entry.SenderAddress);
			}
			Guid guid;
			if (this.mailbox != null && this.mailbox.ADUser != null)
			{
				guid = this.mailbox.ADUser.ExchangeGuid;
			}
			else if (trackedUser.ADUser != null)
			{
				guid = trackedUser.ADUser.ExchangeGuid;
			}
			else
			{
				guid = Guid.Empty;
			}
			MessageTrackingReportId identity = new MessageTrackingReportId(entry.MessageId, entry.Server, entry.InternalMessageId, guid, this.defaultDomain, this.TrackingAsSender);
			string previousHopServer = null;
			if (!string.IsNullOrEmpty(entry.ClientHostName))
			{
				previousHopServer = entry.ClientHostName;
			}
			return new MessageTrackingSearchResult(identity, fromAddress, trackedUser.DisplayName, array2, entry.Subject, entry.Time, previousHopServer, entry.LogRow.GetCustomData<string>("FirstForestHop", string.Empty));
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000656E0 File Offset: 0x000638E0
		private bool SubjectMatchesFilter(string messageSubject, IList<CultureInfo> cultures)
		{
			if (string.IsNullOrEmpty(messageSubject))
			{
				return false;
			}
			messageSubject = messageSubject.Replace("\"", string.Empty);
			foreach (string value in this.tokenizedSubjectKeywords)
			{
				if (cultures == null || cultures.Count == 0)
				{
					if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(messageSubject, value, CompareOptions.IgnoreCase | CompareOptions.IgnoreWidth) == -1)
					{
						return false;
					}
				}
				else
				{
					bool flag = false;
					foreach (CultureInfo cultureInfo in cultures)
					{
						if (cultureInfo.CompareInfo.IndexOf(messageSubject, value, CompareOptions.IgnoreCase | CompareOptions.IgnoreWidth) != -1)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000657B0 File Offset: 0x000639B0
		private bool AnyAddressMatchesRecipientsFilter(ICollection<SmtpAddress> addresses)
		{
			if (this.recipients == null)
			{
				throw new InvalidOperationException("There is no recipient filter");
			}
			if (addresses == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "AnyRecipientsMatchesFilter - event has no addresses to filter for", new object[0]);
				return false;
			}
			foreach (SmtpAddress address in addresses)
			{
				foreach (TrackedUser trackedUser in this.recipients)
				{
					foreach (ProxyAddress proxyAddress in trackedUser.ProxyAddresses)
					{
						if (ProxyAddressPrefix.Smtp.Equals(proxyAddress.Prefix) && string.Equals(proxyAddress.AddressString, (string)address, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04000DC4 RID: 3524
		private const int MaxMessageIdPerQuery = 10;

		// Token: 0x04000DC5 RID: 3525
		private const string PurportedSenderPropertyName = "PurportedSender";

		// Token: 0x04000DC6 RID: 3526
		private const string FirstForestHopPropertyName = "FirstForestHop";

		// Token: 0x04000DC7 RID: 3527
		private const string ProxiedClientHostNamePropertyName = "ProxiedClientHostname";

		// Token: 0x04000DC8 RID: 3528
		internal static readonly HashSet<MessageTrackingEvent> ReceiveEventFilterSet = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.RECEIVE
		};

		// Token: 0x04000DC9 RID: 3529
		internal static readonly HashSet<MessageTrackingEvent> SubmitEventFilterSet = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.SUBMIT
		};

		// Token: 0x04000DCA RID: 3530
		internal static readonly HashSet<MessageTrackingEvent> ReceiveOrDeliverEventsFilterSet = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.RECEIVE,
			MessageTrackingEvent.DELIVER,
			MessageTrackingEvent.DUPLICATEDELIVER,
			MessageTrackingEvent.PROCESS
		};

		// Token: 0x04000DCB RID: 3531
		internal static readonly HashSet<MessageTrackingEvent> HAReceiveOrResubmitEventsFilterSet = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.HARECEIVE,
			MessageTrackingEvent.RESUBMIT
		};

		// Token: 0x04000DCC RID: 3532
		private static readonly HashSet<MessageTrackingEvent> SmtpReceiveOrRejectEventsFilterSet = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.RECEIVE,
			MessageTrackingEvent.FAIL
		};

		// Token: 0x04000DCD RID: 3533
		internal static readonly HashSet<MessageTrackingEvent> ModeratorDecisionEventsFilterSet = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.MODERATORAPPROVE,
			MessageTrackingEvent.MODERATORREJECT,
			MessageTrackingEvent.MODERATIONEXPIRE
		};

		// Token: 0x04000DCE RID: 3534
		private static object initLock = new object();

		// Token: 0x04000DCF RID: 3535
		private static BitArray allColumns = MessageTrackingLogRow.GetColumnFilter(new MessageTrackingField[]
		{
			MessageTrackingField.EventId,
			MessageTrackingField.MessageId,
			MessageTrackingField.ServerHostname,
			MessageTrackingField.InternalMessageId,
			MessageTrackingField.MessageSubject,
			MessageTrackingField.Timestamp,
			MessageTrackingField.RecipientAddress,
			MessageTrackingField.Source,
			MessageTrackingField.ClientHostname,
			MessageTrackingField.SenderAddress,
			MessageTrackingField.CustomData,
			MessageTrackingField.TenantId
		});

		// Token: 0x04000DD0 RID: 3536
		private static BitArray serverHostNameOnly = MessageTrackingLogRow.GetColumnFilter(new MessageTrackingField[]
		{
			MessageTrackingField.ServerHostname,
			MessageTrackingField.MessageId
		});

		// Token: 0x04000DD1 RID: 3537
		private SearchScope scope;

		// Token: 0x04000DD2 RID: 3538
		private string defaultDomain;

		// Token: 0x04000DD3 RID: 3539
		private TrackedUser mailbox;

		// Token: 0x04000DD4 RID: 3540
		private string serverHint;

		// Token: 0x04000DD5 RID: 3541
		private TrackedUser senderIfTrackingAsRecip;

		// Token: 0x04000DD6 RID: 3542
		private TrackedUser[] recipients;

		// Token: 0x04000DD7 RID: 3543
		private SmtpAddress[] recipientAddressArray;

		// Token: 0x04000DD8 RID: 3544
		private ProxyAddressCollection[] recipientProxyAddresses;

		// Token: 0x04000DD9 RID: 3545
		private DirectoryContext directoryContext;

		// Token: 0x04000DDA RID: 3546
		private TrackingDiscovery trackingDiscovery;

		// Token: 0x04000DDB RID: 3547
		private DateTime start;

		// Token: 0x04000DDC RID: 3548
		private DateTime end;

		// Token: 0x04000DDD RID: 3549
		private string subjectKeyword;

		// Token: 0x04000DDE RID: 3550
		private string[] tokenizedSubjectKeywords;

		// Token: 0x04000DDF RID: 3551
		private string messageId;

		// Token: 0x04000DE0 RID: 3552
		private bool expandTree;

		// Token: 0x04000DE1 RID: 3553
		private bool searchAsRecip;

		// Token: 0x04000DE2 RID: 3554
		private Unlimited<uint> resultSize;

		// Token: 0x04000DE3 RID: 3555
		private ReportConstraints constraints;

		// Token: 0x04000DE4 RID: 3556
		private List<MessageTrackingSearchResult> resultList;

		// Token: 0x04000DE5 RID: 3557
		private bool wholeForestSearchExecuted;

		// Token: 0x04000DE6 RID: 3558
		private bool useFirstHopIfAvailable;

		// Token: 0x04000DE7 RID: 3559
		private bool moderationResultSearch;

		// Token: 0x04000DE8 RID: 3560
		private LogCache logCache;
	}
}
