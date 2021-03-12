using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000365 RID: 869
	internal sealed class SearchMailboxes : MultiStepServiceCommand<SearchMailboxesRequest, SearchMailboxesData>, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001844 RID: 6212 RVA: 0x00082DD0 File Offset: 0x00080FD0
		public SearchMailboxes(CallContext callContext, SearchMailboxesRequest request) : base(callContext, request)
		{
			this.disposeTracker = this.GetDisposeTracker();
			if (MailboxSearchFlighting.IsFlighted(callContext, "SearchMailboxes", out this.policy))
			{
				CallContext.Current.ProtocolLog.AppendGenericInfo("SearchStartTime", ExDateTime.UtcNow);
				this.isFlighted = true;
				this.stepCount = 1;
				return;
			}
			this.requestId = MailboxSearchHelper.GetQueryCorrelationId();
			ExTraceGlobals.SearchTracer.TraceInformation<Guid, string>(this.GetHashCode(), 0L, "Correlation Id:{0}. Executing search with client id: {1}", this.requestId, ActivityContext.GetCurrentActivityScope().GetProperty(ActivityStandardMetadata.ClientRequestId));
			CallContext.Current.ProtocolLog.AppendGenericInfo("DiscoveryCorrelationId", this.requestId);
			this.SaveRequestData(request);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00082F67 File Offset: 0x00081167
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SearchMailboxes>(this);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00082F6F File Offset: 0x0008116F
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00082F84 File Offset: 0x00081184
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00082FA6 File Offset: 0x000811A6
		internal override int StepCount
		{
			get
			{
				return this.stepCount;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00082FAE File Offset: 0x000811AE
		private bool HasAdditionalProperties
		{
			get
			{
				return this.additionalExtendedProperties != null && this.additionalExtendedProperties.Length > 0;
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00082FC8 File Offset: 0x000811C8
		internal override void PreExecuteCommand()
		{
			if (this.isFlighted)
			{
				return;
			}
			if (this.mailboxQueries != null && this.mailboxQueries.Length > Factory.Current.MaxAllowedMailboxQueriesPerRequest)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)3784063568U, new InvalidOperationException(CoreResources.TooManyMailboxQueryObjects(this.mailboxQueries.Length, Factory.Current.MaxAllowedMailboxQueriesPerRequest)));
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			MailboxSearchHelper.PerformCommonAuthorization(base.CallContext.IsExternalUser, out this.runspaceConfig, out this.recipientSession);
			stopwatch.Stop();
			CallContext.Current.ProtocolLog.AppendGenericInfo("AuthorizationTimeTaken", stopwatch.ElapsedMilliseconds);
			if (this.recipientSession != null && !Factory.Current.IsDiscoverySearchEnabled(this.recipientSession))
			{
				ExTraceGlobals.SearchTracer.TraceInformation(this.GetHashCode(), 0L, "Discovery Searches are disabled.");
				throw new DiscoverySearchesDisabledException();
			}
			if (this.mailboxQueries == null)
			{
				this.SetMailboxQueryFromMailboxDiscoverySearch();
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000830B7 File Offset: 0x000812B7
		internal override ServiceResult<SearchMailboxesData> Execute()
		{
			if (this.isFlighted)
			{
				this.response = MailboxSearchFlighting.SearchMailboxes(this.policy, base.Request);
				return new ServiceResult<SearchMailboxesData>(new SearchMailboxesData());
			}
			return this.ProcessRequest();
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00083138 File Offset: 0x00081338
		internal override IExchangeWebMethodResponse GetResponse()
		{
			if (this.isFlighted)
			{
				if (this.response != null && this.response.ResponseMessages != null && this.response.ResponseMessages.Items != null && this.response.ResponseMessages.Items.Length > 0)
				{
					SearchMailboxesResponseMessage searchMailboxesResponseMessage = this.response.ResponseMessages.Items[0] as SearchMailboxesResponseMessage;
					if (searchMailboxesResponseMessage != null && searchMailboxesResponseMessage.SearchMailboxesResult != null)
					{
						CallContext.Current.ProtocolLog.AppendGenericInfo("ResultCount", searchMailboxesResponseMessage.SearchMailboxesResult.ItemCount);
						CallContext.Current.ProtocolLog.AppendGenericInfo("ResultSize", searchMailboxesResponseMessage.SearchMailboxesResult.Size);
						if (searchMailboxesResponseMessage.SearchMailboxesResult.MailboxStats != null)
						{
							CallContext.Current.ProtocolLog.AppendGenericInfo("MailboxesSearchedCount", searchMailboxesResponseMessage.SearchMailboxesResult.MailboxStats.Count<MailboxStatisticsItem>());
						}
					}
					CallContext.Current.ProtocolLog.AppendGenericInfo("SearchEndTime", ExDateTime.UtcNow);
				}
				return this.response;
			}
			List<ServiceResult<SearchMailboxesResult>> list = new List<ServiceResult<SearchMailboxesResult>>();
			List<MailboxQuery> list2 = new List<MailboxQuery>();
			List<MailboxQuery> list3 = new List<MailboxQuery>();
			ServiceError error = null;
			ResultAggregator resultAggregator = new ResultAggregator();
			Stopwatch stopwatch = Stopwatch.StartNew();
			List<FailedSearchMailbox> list4 = new List<FailedSearchMailbox>();
			int num = 0;
			ServiceResult<SearchMailboxesData>[] results = base.Results;
			for (int i = 0; i < results.Length; i++)
			{
				ServiceResult<SearchMailboxesData> sr = results[i];
				if (sr == null)
				{
					ExTraceGlobals.SearchTracer.TraceError<Guid>(this.GetHashCode(), 0L, "Correlation Id:{0}. The service result returned from Execute should not be null.", this.requestId);
					ServiceCommandBase.ThrowIfNull(sr, "sr", "SearchMailboxes::GetResponse");
				}
				else if (sr.Code == ServiceResultCode.Success)
				{
					if (sr.Value.ResultAggregator != null)
					{
						list2.Add(sr.Value.MailboxQuery);
						resultAggregator.MergeSearchResult(sr.Value.ResultAggregator);
					}
					if (sr.Value.NonSearchableMailboxes != null)
					{
						list4.AddRange(sr.Value.NonSearchableMailboxes);
					}
				}
				else
				{
					error = sr.Error;
					list3.Add(this.mailboxQueries[num]);
					MailboxSearchScope[] mailboxSearchScopes = this.mailboxQueries[num].MailboxSearchScopes;
					for (int j = 0; j < mailboxSearchScopes.Length; j++)
					{
						MailboxSearchScope scope = mailboxSearchScopes[j];
						if ((from x in list4
						where x.Mailbox.Equals(scope.Mailbox, StringComparison.OrdinalIgnoreCase) && x.ErrorMessage.Equals(sr.Error.MessageText, StringComparison.OrdinalIgnoreCase)
						select x).FirstOrDefault<FailedSearchMailbox>() == null)
						{
							list4.Add(new FailedSearchMailbox
							{
								Mailbox = scope.Mailbox,
								ErrorMessage = sr.Error.MessageText,
								IsArchive = (scope.SearchScope == MailboxSearchLocation.ArchiveOnly)
							});
						}
					}
				}
				num++;
			}
			if (list2.Count > 0)
			{
				SearchMailboxesResult value = this.CreateSearchMailboxesResult(list2.ToArray(), resultAggregator, list4);
				ServiceResult<SearchMailboxesResult> item = new ServiceResult<SearchMailboxesResult>(ServiceResultCode.Success, value, null);
				list.Insert(0, item);
			}
			else if (list3.Count > 0)
			{
				SearchMailboxesResult value2 = this.CreateSearchMailboxesResult(list3.ToArray(), null, list4);
				list.Add(new ServiceResult<SearchMailboxesResult>(ServiceResultCode.Error, value2, error));
			}
			SearchMailboxesResponse searchMailboxesResponse = new SearchMailboxesResponse();
			searchMailboxesResponse.AddResponses(list.ToArray());
			stopwatch.Stop();
			CallContext.Current.ProtocolLog.AppendGenericInfo("SearchResponseProcessingTime", stopwatch.ElapsedMilliseconds);
			return searchMailboxesResponse;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00083502 File Offset: 0x00081702
		private void Dispose(bool fromDispose)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00083528 File Offset: 0x00081728
		private void SaveRequestData(SearchMailboxesRequest request)
		{
			this.searchId = request.SearchId;
			this.mailboxId = request.MailboxId;
			this.mailboxQueries = request.SearchQueries;
			this.stepCount = ((this.mailboxQueries != null) ? this.mailboxQueries.Length : 1);
			this.resultType = request.ResultType;
			this.sortBy = request.SortBy;
			this.language = request.Language;
			this.performDeduplication = request.Deduplication;
			this.pageSize = ((request.PageSize <= 0) ? 100 : request.PageSize);
			this.pageItemReference = request.PageItemReference;
			this.pageDirection = ((request.PageDirection == SearchPageDirectionType.Previous) ? PageDirection.Previous : PageDirection.Next);
			this.baseShape = Microsoft.Exchange.Services.Core.Types.PreviewItemBaseShape.Default;
			if (request.PreviewItemResponseShape != null)
			{
				this.baseShape = request.PreviewItemResponseShape.BaseShape;
				this.additionalExtendedProperties = request.PreviewItemResponseShape.AdditionalProperties;
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0008360C File Offset: 0x0008180C
		private ServiceResult<SearchMailboxesData> ProcessRequest()
		{
			this.currentMailboxQuery = this.mailboxQueries[base.CurrentStep];
			if (string.IsNullOrEmpty(this.currentMailboxQuery.Query))
			{
				throw new ServiceArgumentException((CoreResources.IDs)2226875331U);
			}
			if (this.HasAdditionalProperties)
			{
				this.ProcessAdditionalExtendedProperties();
			}
			SearchType searchType = SearchType.Preview;
			if ((this.resultType & SearchResultType.StatisticsOnly) == SearchResultType.StatisticsOnly)
			{
				searchType = SearchType.Statistics;
			}
			else if ((this.resultType & SearchResultType.PreviewOnly) == SearchResultType.PreviewOnly)
			{
				searchType = SearchType.Preview;
			}
			int maxAllowedMailboxes = Factory.Current.GetMaxAllowedMailboxes(this.recipientSession, searchType);
			if (this.currentMailboxQuery.MailboxSearchScopes != null && !Factory.Current.IsSearchAllowed(this.recipientSession, searchType, this.currentMailboxQuery.MailboxSearchScopes.Length))
			{
				ExTraceGlobals.SearchTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Max mailboxes allowed per search call is {1}, the {2} search request for the query:{3} contained {4} mailboxes to search on.", new object[]
				{
					this.requestId,
					maxAllowedMailboxes,
					(searchType == SearchType.Preview) ? "preview" : "statistics",
					this.currentMailboxQuery.Query,
					this.currentMailboxQuery.MailboxSearchScopes.Length
				});
				throw new TooManyMailboxesException(this.currentMailboxQuery.MailboxSearchScopes.Length, maxAllowedMailboxes);
			}
			int num = 0;
			Stopwatch stopwatch = Stopwatch.StartNew();
			List<FailedSearchMailbox> nonSearchableMailboxes;
			List<MailboxInfo> list = this.ExpandAndFilterMailboxList(this.currentMailboxQuery, out num, out nonSearchableMailboxes);
			stopwatch.Stop();
			CallContext.Current.ProtocolLog.AppendGenericInfo("MailboxesSelectionTime", stopwatch.ElapsedMilliseconds);
			if (list == null || list.Count == 0)
			{
				ExTraceGlobals.SearchTracer.TraceError<Guid>((long)this.GetHashCode(), "Correlation Id:{0}. No mailbox to be searched after expanding and filtering the mailbox list.", this.requestId);
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorNoMailboxSpecifiedForSearchOperation);
			}
			if (!Factory.Current.IsSearchAllowed(this.recipientSession, searchType, num))
			{
				ExTraceGlobals.SearchTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Max mailboxes allowed per search call is {1}, the {2} search request for the query:{3} contained {4} mailboxes to search on.", new object[]
				{
					this.requestId,
					maxAllowedMailboxes,
					(searchType == SearchType.Preview) ? "preview" : "statistics",
					this.currentMailboxQuery.Query,
					num
				});
				throw new TooManyMailboxesException(num, maxAllowedMailboxes);
			}
			CallContext.Current.ProtocolLog.AppendGenericInfo("QueryLength", this.currentMailboxQuery.Query.Length);
			CallContext.Current.ProtocolLog.AppendGenericInfo("MailboxesSearchedCount", list.Count);
			CallContext.Current.ProtocolLog.AppendGenericInfo("SearchType", this.resultType.ToString());
			CallContext.Current.ProtocolLog.AppendGenericInfo("PerformDeduplication", this.performDeduplication.ToString());
			CallContext.Current.ProtocolLog.AppendGenericInfo("SearchStartTime", ExDateTime.UtcNow);
			ResultAggregator resultAggregator = new ResultAggregator();
			stopwatch.Restart();
			ISearchResult searchResult = this.ExecuteSearch(list, this.currentMailboxQuery.Query, searchType);
			if (searchResult != null)
			{
				resultAggregator.MergeSearchResult(searchResult);
			}
			stopwatch.Stop();
			CallContext.Current.ProtocolLog.AppendGenericInfo("SearchEndTime", ExDateTime.UtcNow);
			CallContext.Current.ProtocolLog.AppendGenericInfo("SearchExecutionTime", stopwatch.ElapsedMilliseconds);
			foreach (KeyValuePair<string, object> keyValuePair in resultAggregator.ProtocolLog)
			{
				CallContext.Current.ProtocolLog.AppendGenericInfo(keyValuePair.Key, keyValuePair.Value.ToString());
			}
			return new ServiceResult<SearchMailboxesData>(new SearchMailboxesData
			{
				MailboxQuery = this.currentMailboxQuery,
				NonSearchableMailboxes = nonSearchableMailboxes,
				ResultAggregator = resultAggregator
			});
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000839F8 File Offset: 0x00081BF8
		private List<MailboxInfo> ExpandAndFilterMailboxList(MailboxQuery mailboxQuery, out int totalMailboxesToSearch, out List<FailedSearchMailbox> nonSearchableMailboxes)
		{
			List<MailboxInfo> list = new List<MailboxInfo>();
			int num = 0;
			List<PropertyDefinition> list2 = new List<PropertyDefinition>(MailboxInfo.PropertyDefinitionCollection);
			list2.AddRange(MailboxSearchHelper.AdditionalProperties);
			PropertyDefinition[] properties = list2.ToArray();
			nonSearchableMailboxes = new List<FailedSearchMailbox>(1);
			ICollection<string> source = from m in mailboxQuery.MailboxSearchScopes
			select m.Mailbox;
			this.dictADRawEntries = MailboxSearchHelper.FindADEntriesByLegacyExchangeDNs(this.recipientSession, source.ToArray<string>(), properties);
			foreach (MailboxSearchScope mailboxSearchScope in mailboxQuery.MailboxSearchScopes)
			{
				ADRawEntry adrawEntry = null;
				if (this.dictADRawEntries.TryGetValue(mailboxSearchScope.Mailbox, out adrawEntry))
				{
					if (MailboxSearchHelper.IsMembershipGroup(adrawEntry))
					{
						Dictionary<ADObjectId, ADRawEntry> dictionary = MailboxSearchHelper.ProcessGroupExpansion(this.recipientSession, adrawEntry, this.recipientSession.SessionSettings.CurrentOrganizationId, properties);
						using (Dictionary<ADObjectId, ADRawEntry>.Enumerator enumerator = dictionary.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<ADObjectId, ADRawEntry> keyValuePair = enumerator.Current;
								if (keyValuePair.Value != null)
								{
									this.VerifyAndAddADObjectIdToCollection(list, keyValuePair.Value, mailboxSearchScope.SearchScope, nonSearchableMailboxes, ref num);
								}
							}
							goto IL_172;
						}
					}
					this.VerifyAndAddADObjectIdToCollection(list, adrawEntry, mailboxSearchScope.SearchScope, nonSearchableMailboxes, ref num);
				}
				else
				{
					ExTraceGlobals.SearchTracer.TraceError<string, Guid>((long)this.GetHashCode(), "Correlation Id:{0}. This mailbox is not found in AD: {1}", mailboxSearchScope.Mailbox, this.requestId);
					nonSearchableMailboxes.Add(new FailedSearchMailbox(mailboxSearchScope.Mailbox, 0, CoreResources.ErrorSearchableObjectNotFound));
				}
				IL_172:;
			}
			totalMailboxesToSearch = num;
			return list;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00083C1C File Offset: 0x00081E1C
		private void VerifyAndAddADObjectIdToCollection(List<MailboxInfo> mailboxes, ADRawEntry recipient, MailboxSearchLocation searchScope, List<FailedSearchMailbox> nonSearchableMailboxes, ref int totalMailboxCount)
		{
			bool flag = MailboxSearchHelper.HasPermissionToSearchMailbox(this.runspaceConfig, recipient);
			bool flag2 = MailboxSearchHelper.IsValidRecipientType(recipient);
			bool flag3 = MailboxSearchHelper.HasValidVersion(recipient);
			if (flag && flag2 && flag3)
			{
				MailboxInfo primaryMailbox = null;
				MailboxInfo archiveMailbox = null;
				if (searchScope == MailboxSearchLocation.ArchiveOnly)
				{
					totalMailboxCount++;
				}
				if (searchScope == MailboxSearchLocation.All || searchScope == MailboxSearchLocation.PrimaryOnly)
				{
					primaryMailbox = new MailboxInfo(recipient, MailboxType.Primary);
					totalMailboxCount++;
				}
				if ((searchScope == MailboxSearchLocation.All || searchScope == MailboxSearchLocation.ArchiveOnly) && !Guid.Empty.Equals((Guid)recipient[ADUserSchema.ArchiveGuid]))
				{
					archiveMailbox = new MailboxInfo(recipient, MailboxType.Archive);
				}
				if (primaryMailbox != null)
				{
					if (!mailboxes.Any((MailboxInfo m) => m.Type == primaryMailbox.Type && string.Compare(m.OwnerId.DistinguishedName, primaryMailbox.OwnerId.DistinguishedName, StringComparison.CurrentCultureIgnoreCase) == 0))
					{
						mailboxes.Add(primaryMailbox);
					}
				}
				if (archiveMailbox != null)
				{
					if (!mailboxes.Any((MailboxInfo m) => m.Type == archiveMailbox.Type && string.Compare(m.OwnerId.DistinguishedName, archiveMailbox.OwnerId.DistinguishedName, StringComparison.CurrentCultureIgnoreCase) == 0))
					{
						mailboxes.Add(archiveMailbox);
						return;
					}
				}
			}
			else
			{
				string errorMessage = string.Empty;
				if (!flag2)
				{
					errorMessage = CoreResources.GetLocalizedString((CoreResources.IDs)3611326890U);
				}
				else if (!flag3)
				{
					errorMessage = CoreResources.GetLocalizedString(CoreResources.IDs.ErrorMailboxVersionNotSupported);
				}
				else if (!flag)
				{
					errorMessage = CoreResources.GetLocalizedString((CoreResources.IDs)2354781453U);
				}
				string mailbox = (string)recipient[ADRecipientSchema.LegacyExchangeDN];
				nonSearchableMailboxes.Add(new FailedSearchMailbox
				{
					Mailbox = mailbox,
					ErrorMessage = errorMessage
				});
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00083DB8 File Offset: 0x00081FB8
		private ISearchResult ExecuteSearch(List<MailboxInfo> mailboxes, string queryString, SearchType searchType)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, this.recipientSession.SessionSettings, 723, "ExecuteSearch", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\SearchMailboxes.cs");
			CultureInfo culture = CultureInfo.InvariantCulture;
			if (!string.IsNullOrEmpty(this.language))
			{
				try
				{
					culture = new CultureInfo(this.language);
				}
				catch (CultureNotFoundException)
				{
					ExTraceGlobals.SearchTracer.TraceError<string>((long)this.GetHashCode(), "Culture info: \"{0}\" returns CultureNotFoundException", this.language);
					throw new ServiceArgumentException(CoreResources.IDs.ErrorQueryLanguageNotValid);
				}
			}
			SearchCriteria criteria = null;
			try
			{
				criteria = new SearchCriteria(queryString, null, culture, searchType, this.recipientSession, tenantOrTopologyConfigurationSession, this.requestId, (this.policy != null && this.policy.ExecutionSettings != null) ? this.policy.ExecutionSettings.ExcludedFolders : new List<DefaultFolderType>());
			}
			catch (ParserException ex)
			{
				ExTraceGlobals.SearchTracer.TraceError<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Query: \"{1}\" returns ParserException: {2}", this.requestId, queryString, ex.ToString());
				throw new ServiceArgumentException((CoreResources.IDs)3021008902U);
			}
			catch (TooManyKeywordsException ex2)
			{
				ExTraceGlobals.SearchTracer.TraceError<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Query: \"{1}\" returns TooManyKeywordsException: {2}", this.requestId, queryString, ex2.ToString());
				throw new TooManyKeywordsException(ex2);
			}
			SortBy requestSortBy = this.GetRequestSortBy();
			ReferenceItem referenceItem = null;
			if (!string.IsNullOrEmpty(this.pageItemReference))
			{
				referenceItem = ReferenceItem.Parse(requestSortBy, this.pageItemReference);
			}
			PagingInfo pagingInfo = new PagingInfo(this.GetRequiredItemProperties(), requestSortBy, this.pageSize, this.pageDirection, referenceItem, MailboxSearchHelper.GetTimeZone(), this.performDeduplication, this.GetBaseShape(), this.GetExtendedPropertyInfoList());
			CallerInfo callerInfo = new CallerInfo(MailboxSearchHelper.IsOpenAsAdmin(base.CallContext), MailboxSearchConverter.GetCommonAccessToken(base.CallContext), base.CallContext.EffectiveCaller.ClientSecurityContext, base.CallContext.EffectiveCaller.PrimarySmtpAddress, this.recipientSession.SessionSettings.CurrentOrganizationId, base.CallContext.UserAgent, this.requestId, MailboxSearchHelper.GetUserRolesFromAuthZClientInfo(base.CallContext.EffectiveCaller), MailboxSearchHelper.GetApplicationRolesFromAuthZClientInfo(base.CallContext.EffectiveCaller));
			ISearchResult result;
			using (MultiMailboxSearch multiMailboxSearch = new MultiMailboxSearch(criteria, mailboxes, pagingInfo, callerInfo, this.recipientSession.SessionSettings.CurrentOrganizationId))
			{
				AsyncResult asyncResult = multiMailboxSearch.BeginSearch(null, null) as AsyncResult;
				TimeSpan defaultSearchTimeout = Factory.Current.GetDefaultSearchTimeout(this.recipientSession);
				CallContext.Current.ProtocolLog.AppendGenericInfo("SearchTimeoutInterval", defaultSearchTimeout.Minutes);
				bool flag = asyncResult.AsyncWaitHandle.WaitOne(defaultSearchTimeout);
				if (!flag)
				{
					multiMailboxSearch.AbortSearch();
				}
				CallContext.Current.ProtocolLog.AppendGenericInfo("SearchTimedOut", !flag);
				result = multiMailboxSearch.EndSearch(asyncResult);
			}
			return result;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x000840A4 File Offset: 0x000822A4
		private SearchPreviewItem ConvertPreviewItemToSearchPreviewItem(PreviewItem pi)
		{
			ItemId itemId = this.GetItemId(pi.Id, pi.MailboxGuid);
			SearchPreviewItem searchPreviewItem = new SearchPreviewItem
			{
				Id = itemId
			};
			if (this.baseShape == Microsoft.Exchange.Services.Core.Types.PreviewItemBaseShape.Compact || this.baseShape == Microsoft.Exchange.Services.Core.Types.PreviewItemBaseShape.Default)
			{
				searchPreviewItem.ParentId = this.GetItemId(pi.ParentItemId, pi.MailboxGuid);
				searchPreviewItem.Mailbox = this.GetMailbox(pi.MailboxGuid);
				searchPreviewItem.UniqueHash = ((pi.ItemHash != null) ? pi.ItemHash.ToString() : null);
				searchPreviewItem.SortValue = ((pi.SortValue != null) ? pi.SortValue.ToString() : null);
				searchPreviewItem.Size = new ulong?((ulong)((long)pi.Size));
			}
			if (this.baseShape == Microsoft.Exchange.Services.Core.Types.PreviewItemBaseShape.Default)
			{
				searchPreviewItem.ItemClass = pi.ItemClass;
				searchPreviewItem.Sender = pi.Sender;
				searchPreviewItem.Subject = pi.Subject;
				searchPreviewItem.ToRecipients = pi.ToRecipients;
				searchPreviewItem.CcRecipients = pi.CcRecipients;
				searchPreviewItem.BccRecipients = pi.BccRecipients;
				searchPreviewItem.CreatedTime = this.GetDateTimeAsString(pi.CreationTime);
				searchPreviewItem.ReceivedTime = this.GetDateTimeAsString(pi.ReceivedTime);
				searchPreviewItem.SentTime = this.GetDateTimeAsString(pi.SentTime);
				searchPreviewItem.Preview = pi.Preview;
				string owaLink = string.Empty;
				if (pi.OwaLink != null && !string.IsNullOrEmpty(pi.OwaLink.AbsoluteUri))
				{
					owaLink = LinkUtils.UpdateOwaLinkToItem(pi.OwaLink, itemId.Id).AbsoluteUri;
				}
				searchPreviewItem.OwaLink = owaLink;
				searchPreviewItem.Importance = this.GetImportance(pi.Importance).ToString();
				searchPreviewItem.Read = new bool?(pi.Read);
				searchPreviewItem.HasAttachment = new bool?(pi.HasAttachment);
			}
			if (this.HasAdditionalProperties)
			{
				searchPreviewItem.ExtendedProperties = this.ConvertPropertyValuesToExtendedProperties(pi.AdditionalPropertyValues);
			}
			return searchPreviewItem;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0008429C File Offset: 0x0008249C
		private SearchPreviewItem[] ProcessSearchResultRows(PreviewItem[] searchResultRows)
		{
			SearchPreviewItem[] array = new SearchPreviewItem[searchResultRows.Length];
			if (this.GetRequestSortBy().ColumnDefinition == ItemSchema.ReceivedTime && searchResultRows[searchResultRows.Length - 1].ReceivedTime == ExDateTime.MinValue && searchResultRows[0].ReceivedTime != ExDateTime.MinValue)
			{
				int num = 0;
				for (int i = 0; i < searchResultRows.Length; i++)
				{
					array[i] = this.ConvertPreviewItemToSearchPreviewItem(searchResultRows[i]);
					if (searchResultRows[i].ReceivedTime != ExDateTime.MinValue)
					{
						num = i;
					}
				}
				if (num != array.Length - 1)
				{
					SearchPreviewItem searchPreviewItem = array[num];
					array[num] = array[array.Length - 1];
					array[array.Length - 1] = searchPreviewItem;
				}
			}
			else
			{
				for (int j = 0; j < searchResultRows.Length; j++)
				{
					array[j] = this.ConvertPreviewItemToSearchPreviewItem(searchResultRows[j]);
				}
			}
			return array;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00084364 File Offset: 0x00082564
		private List<PropertyDefinition> GetRequiredItemProperties()
		{
			if (this.itemProperties == null)
			{
				this.itemProperties = new List<PropertyDefinition>();
				if (this.baseShape == Microsoft.Exchange.Services.Core.Types.PreviewItemBaseShape.Compact)
				{
					this.itemProperties.AddRange(this.compactPreviewDataProperties);
				}
				else
				{
					this.itemProperties.AddRange(this.previewDataProperties);
				}
			}
			return this.itemProperties;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000843B8 File Offset: 0x000825B8
		private void ProcessAdditionalExtendedProperties()
		{
			this.xsoEwsPropertiesMap = new Dictionary<PropertyDefinition, ExtendedPropertyUri>(this.additionalExtendedProperties.Count<ExtendedPropertyUri>());
			for (int i = 0; i < this.additionalExtendedProperties.Length; i++)
			{
				PropertyDefinition key = this.additionalExtendedProperties[i].ToPropertyDefinition();
				if (!this.xsoEwsPropertiesMap.ContainsKey(key))
				{
					this.xsoEwsPropertiesMap.Add(key, this.additionalExtendedProperties[i]);
				}
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00084420 File Offset: 0x00082620
		private ExtendedPropertyType[] ConvertPropertyValuesToExtendedProperties(Dictionary<PropertyDefinition, object> propertyValues)
		{
			if (propertyValues == null)
			{
				return null;
			}
			List<ExtendedPropertyType> list = new List<ExtendedPropertyType>(propertyValues.Count);
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in propertyValues)
			{
				if (keyValuePair.Value != null && !(keyValuePair.Value is PropertyError))
				{
					ExtendedPropertyUri propertyUri = this.xsoEwsPropertiesMap[keyValuePair.Key];
					ExtendedPropertyType item;
					if (keyValuePair.Value is Array)
					{
						item = new ExtendedPropertyType(propertyUri, this.ConvertObjectArrayToStringArray((object[])keyValuePair.Value));
					}
					else
					{
						item = new ExtendedPropertyType(propertyUri, keyValuePair.Value.ToString());
					}
					list.Add(item);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000844F8 File Offset: 0x000826F8
		private string[] ConvertObjectArrayToStringArray(object[] objs)
		{
			string[] array = new string[objs.Length];
			for (int i = 0; i < objs.Length; i++)
			{
				array[i] = (objs[i] as string);
			}
			return array;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00084528 File Offset: 0x00082728
		private SortBy GetRequestSortBy()
		{
			PropertyDefinition propertyDefinition = ItemSchema.ReceivedTime;
			SortOrder sortOrder = SortOrder.Descending;
			if (this.sortBy != null && this.sortBy.SortByProperty != null)
			{
				PropertyUri propertyUri = this.sortBy.SortByProperty as PropertyUri;
				if (propertyUri == null)
				{
					throw new ServiceArgumentException((CoreResources.IDs)2566235088U);
				}
				propertyDefinition = this.GetPropertyDefinitionFromPropertyUri(propertyUri);
				if (propertyDefinition == null)
				{
					throw new ServiceArgumentException((CoreResources.IDs)2841035169U);
				}
				sortOrder = ((this.sortBy.Order == SortDirection.Descending) ? SortOrder.Descending : SortOrder.Ascending);
			}
			return new SortBy(propertyDefinition, sortOrder);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000845AD File Offset: 0x000827AD
		private Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch.PreviewItemBaseShape GetBaseShape()
		{
			if (this.baseShape == Microsoft.Exchange.Services.Core.Types.PreviewItemBaseShape.Compact)
			{
				return Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch.PreviewItemBaseShape.Compact;
			}
			return Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch.PreviewItemBaseShape.Default;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000845BC File Offset: 0x000827BC
		private List<ExtendedPropertyInfo> GetExtendedPropertyInfoList()
		{
			List<ExtendedPropertyInfo> list = null;
			if (this.xsoEwsPropertiesMap != null && this.xsoEwsPropertiesMap.Count > 0)
			{
				list = new List<ExtendedPropertyInfo>(this.xsoEwsPropertiesMap.Count);
				foreach (KeyValuePair<PropertyDefinition, ExtendedPropertyUri> keyValuePair in this.xsoEwsPropertiesMap)
				{
					list.Add(this.GetExtendedPropertyInfo(keyValuePair.Value, keyValuePair.Key));
				}
			}
			return list;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0008464C File Offset: 0x0008284C
		private ExtendedPropertyInfo GetExtendedPropertyInfo(ExtendedPropertyUri extendedPropertyUri, PropertyDefinition propertyDefinition)
		{
			ExtendedPropertyInfo extendedPropertyInfo = new ExtendedPropertyInfo();
			extendedPropertyInfo.XsoPropertyDefinition = propertyDefinition;
			extendedPropertyInfo.PropertySetId = extendedPropertyUri.PropertySetIdGuid;
			extendedPropertyInfo.PropertyName = extendedPropertyUri.PropertyName;
			extendedPropertyInfo.PropertyType = extendedPropertyUri.PropertyTypeString;
			if (extendedPropertyUri.PropertyIdSpecified)
			{
				extendedPropertyInfo.PropertyId = new int?(extendedPropertyUri.PropertyId);
			}
			if (!string.IsNullOrEmpty(extendedPropertyUri.PropertyTag))
			{
				extendedPropertyInfo.PropertyTagId = new int?((int)extendedPropertyUri.PropertyTagId);
			}
			return extendedPropertyInfo;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000846C4 File Offset: 0x000828C4
		private PropertyDefinition GetPropertyDefinitionFromPropertyUri(PropertyUri propUri)
		{
			PropertyDefinition result = null;
			Schema schema = null;
			string uriString = propUri.UriString;
			if (uriString.StartsWith("folder:", StringComparison.OrdinalIgnoreCase))
			{
				schema = FolderSchema.GetSchema();
			}
			else if (uriString.StartsWith("item:", StringComparison.OrdinalIgnoreCase))
			{
				schema = ItemSchema.GetSchema();
			}
			else if (uriString.StartsWith("message:", StringComparison.OrdinalIgnoreCase))
			{
				schema = MessageSchema.GetSchema();
			}
			else if (uriString.StartsWith("meeting:", StringComparison.OrdinalIgnoreCase))
			{
				schema = MeetingMessageSchema.GetSchema();
			}
			else if (uriString.StartsWith("meetingRequest:", StringComparison.OrdinalIgnoreCase))
			{
				schema = MeetingRequestSchema.GetSchema();
			}
			else if (uriString.StartsWith("calendar:", StringComparison.OrdinalIgnoreCase))
			{
				schema = CalendarItemSchema.GetSchema(true);
			}
			else if (uriString.StartsWith("task:", StringComparison.OrdinalIgnoreCase))
			{
				schema = TaskSchema.GetSchema();
			}
			else if (uriString.StartsWith("contacts:", StringComparison.OrdinalIgnoreCase))
			{
				schema = ContactSchema.GetSchema();
			}
			else if (uriString.StartsWith("conversation:", StringComparison.OrdinalIgnoreCase))
			{
				schema = ConversationSchema.GetSchema();
			}
			else if (uriString.StartsWith("distributionlist:", StringComparison.OrdinalIgnoreCase))
			{
				schema = DistributionListSchema.GetSchema();
			}
			else if (uriString.StartsWith("postitem:", StringComparison.OrdinalIgnoreCase))
			{
				schema = PostItemSchema.GetSchema();
			}
			if (schema != null)
			{
				PropertyInformation propertyInformation = null;
				if (schema.TryGetPropertyInformationByPath(propUri, out propertyInformation))
				{
					PropertyDefinition[] propertyDefinitions = propertyInformation.GetPropertyDefinitions(null);
					if (propertyDefinitions != null && propertyDefinitions.Length > 0)
					{
						result = propertyDefinitions[0];
					}
				}
			}
			return result;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00084808 File Offset: 0x00082A08
		private ItemId GetItemId(StoreId itemId, Guid mailboxGuid)
		{
			StoreId storeId = this.GetPropertyValue(itemId, null) as StoreId;
			Guid mailboxGuid2 = (Guid)this.GetPropertyValue(mailboxGuid, Guid.Empty);
			MailboxId mailboxId = new MailboxId(mailboxGuid2);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, mailboxId, null);
			return new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00084864 File Offset: 0x00082A64
		private PreviewItemMailbox GetMailbox(Guid mailboxGuid)
		{
			Guid mailboxGuid2 = (Guid)this.GetPropertyValue(mailboxGuid, Guid.Empty);
			ADUser aduser = null;
			if (ADIdentityInformationCache.Singleton.TryGetADUser(mailboxGuid2, base.CallContext.ADRecipientSessionContext, out aduser))
			{
				PreviewItemMailbox previewItemMailbox = new PreviewItemMailbox();
				previewItemMailbox.MailboxId = aduser.LegacyExchangeDN;
				if (!string.IsNullOrEmpty(aduser.PrimarySmtpAddress.ToString()))
				{
					previewItemMailbox.PrimarySmtpAddress = aduser.PrimarySmtpAddress.ToString();
				}
				return previewItemMailbox;
			}
			return null;
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x000848F4 File Offset: 0x00082AF4
		private string GetDateTimeAsString(object propertyValue)
		{
			object propertyValue2 = this.GetPropertyValue(propertyValue, null);
			if (propertyValue2 != null)
			{
				return ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)propertyValue2);
			}
			return null;
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0008491C File Offset: 0x00082B1C
		private ImportanceType GetImportance(object propertyValue)
		{
			string text = this.GetPropertyValue(propertyValue, null) as string;
			string a;
			if (!string.IsNullOrEmpty(text) && (a = text.ToLower()) != null)
			{
				if (a == "low")
				{
					return ImportanceType.Low;
				}
				if (a == "high")
				{
					return ImportanceType.High;
				}
			}
			return ImportanceType.Normal;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0008496A File Offset: 0x00082B6A
		private object GetPropertyValue(object propertyValue, object defaultValueIfNull)
		{
			if (propertyValue != null && !(propertyValue is PropertyError))
			{
				return propertyValue;
			}
			return defaultValueIfNull;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0008497C File Offset: 0x00082B7C
		private SearchMailboxesResult CreateSearchMailboxesResult(MailboxQuery[] searchQueries, ResultAggregator resultAggregator, List<FailedSearchMailbox> nonSearchableMailboxes)
		{
			SearchMailboxesResult searchMailboxesResult = new SearchMailboxesResult();
			searchMailboxesResult.SearchQueries = searchQueries;
			searchMailboxesResult.ResultType = this.resultType;
			searchMailboxesResult.Size = 0UL;
			if (resultAggregator == null)
			{
				if (nonSearchableMailboxes != null && nonSearchableMailboxes.Count > 0)
				{
					CallContext.Current.ProtocolLog.AppendGenericInfo("FailedMailboxes", nonSearchableMailboxes.Count);
					searchMailboxesResult.FailedMailboxes = nonSearchableMailboxes.ToArray();
				}
				return searchMailboxesResult;
			}
			if (resultAggregator.KeywordStatistics != null && resultAggregator.KeywordStatistics.Count > 0)
			{
				KeywordStatisticsSearchResult[] array = new KeywordStatisticsSearchResult[resultAggregator.KeywordStatistics.Count];
				int num = 0;
				foreach (KeyValuePair<string, IKeywordHit> keyValuePair in resultAggregator.KeywordStatistics)
				{
					KeywordStatisticsSearchResult keywordStatisticsSearchResult = new KeywordStatisticsSearchResult
					{
						Keyword = keyValuePair.Value.Phrase,
						ItemHits = (int)keyValuePair.Value.Count,
						Size = keyValuePair.Value.Size.ToBytes()
					};
					array[num++] = keywordStatisticsSearchResult;
				}
				searchMailboxesResult.KeywordStats = array;
			}
			searchMailboxesResult.PageItemCount = 0;
			searchMailboxesResult.PageItemSize = 0UL;
			if (resultAggregator.PreviewResult != null && resultAggregator.PreviewResult.ResultRows != null && resultAggregator.PreviewResult.ResultRows.Length > 0)
			{
				searchMailboxesResult.Items = this.ProcessSearchResultRows(resultAggregator.PreviewResult.ResultRows);
				searchMailboxesResult.PageItemCount = searchMailboxesResult.Items.Length;
				foreach (SearchPreviewItem searchPreviewItem in searchMailboxesResult.Items)
				{
					searchMailboxesResult.PageItemSize += ((searchPreviewItem.Size != null) ? searchPreviewItem.Size.Value : 0UL);
				}
			}
			searchMailboxesResult.ItemCount = resultAggregator.TotalResultCount;
			searchMailboxesResult.Size = resultAggregator.TotalResultSize.ToBytes();
			CallContext.Current.ProtocolLog.AppendGenericInfo("ResultCount", searchMailboxesResult.ItemCount);
			CallContext.Current.ProtocolLog.AppendGenericInfo("ResultSize", searchMailboxesResult.Size);
			List<FailedSearchMailbox> list = new List<FailedSearchMailbox>();
			if (nonSearchableMailboxes != null)
			{
				list.AddRange(nonSearchableMailboxes);
			}
			if (resultAggregator.PreviewErrors != null && resultAggregator.PreviewErrors.Count > 0)
			{
				foreach (Pair<MailboxInfo, Exception> pair in resultAggregator.PreviewErrors)
				{
					FailedSearchMailbox item = new FailedSearchMailbox
					{
						Mailbox = pair.First.LegacyExchangeDN,
						IsArchive = (pair.First.Type == MailboxType.Archive),
						ErrorMessage = pair.Second.Message
					};
					list.Add(item);
				}
			}
			CallContext.Current.ProtocolLog.AppendGenericInfo("FailedMailboxes", list.Count);
			if (list.Count > 0)
			{
				searchMailboxesResult.FailedMailboxes = list.ToArray();
			}
			if (this.baseShape != Microsoft.Exchange.Services.Core.Types.PreviewItemBaseShape.Default)
			{
				return searchMailboxesResult;
			}
			if (resultAggregator.MailboxStats != null && resultAggregator.MailboxStats.Count > 0)
			{
				List<MailboxStatisticsItem> list2 = new List<MailboxStatisticsItem>(resultAggregator.MailboxStats.Count);
				foreach (MailboxStatistics mailboxStatistics in resultAggregator.MailboxStats)
				{
					list2.Add(new MailboxStatisticsItem
					{
						MailboxId = mailboxStatistics.MailboxInfo.LegacyExchangeDN,
						DisplayName = mailboxStatistics.MailboxInfo.DisplayName,
						ItemCount = (long)mailboxStatistics.Count,
						Size = mailboxStatistics.Size.ToBytes()
					});
				}
				searchMailboxesResult.MailboxStats = ((list2.Count > 0) ? list2.ToArray() : null);
			}
			if (resultAggregator.RefinersResult != null && resultAggregator.RefinersResult.Count > 0)
			{
				List<SearchRefinerItem> list3 = new List<SearchRefinerItem>(resultAggregator.RefinersResult.Count);
				foreach (KeyValuePair<string, List<IRefinerResult>> keyValuePair2 in resultAggregator.RefinersResult)
				{
					if (keyValuePair2.Value != null && keyValuePair2.Value.Count > 0)
					{
						foreach (IRefinerResult refinerResult in keyValuePair2.Value)
						{
							list3.Add(new SearchRefinerItem
							{
								Name = keyValuePair2.Key,
								Value = refinerResult.Value,
								Count = refinerResult.Count,
								Token = string.Format(":{0}", refinerResult.Value)
							});
						}
					}
				}
				searchMailboxesResult.Refiners = ((list3.Count > 0) ? list3.ToArray() : null);
			}
			return searchMailboxesResult;
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00084F38 File Offset: 0x00083138
		private void SetMailboxQueryFromMailboxDiscoverySearch()
		{
			IDiscoverySearchDataProvider discoverySearchDataProvider = new DiscoverySearchDataProvider(this.recipientSession.SessionSettings.CurrentOrganizationId);
			if (!string.IsNullOrEmpty(this.searchId))
			{
				MailboxDiscoverySearch mailboxDiscoverySearch = discoverySearchDataProvider.Find<MailboxDiscoverySearch>(this.searchId);
				if (mailboxDiscoverySearch != null)
				{
					MailboxSearchLocation searchScope = MailboxSearchLocation.All;
					if (!string.IsNullOrEmpty(mailboxDiscoverySearch.Language))
					{
						this.language = mailboxDiscoverySearch.Language;
					}
					IEnumerable<MailboxSearchScope> source2;
					if (string.IsNullOrEmpty(this.mailboxId))
					{
						if (mailboxDiscoverySearch.Sources.Count == 0)
						{
							List<FailedSearchMailbox> list;
							List<SearchableMailbox> searchableMailboxes = MailboxSearchHelper.GetSearchableMailboxes(mailboxDiscoverySearch, true, this.recipientSession, this.runspaceConfig, out list);
							source2 = from mailbox in searchableMailboxes
							select new MailboxSearchScope
							{
								Mailbox = mailbox.ReferenceId,
								SearchScope = searchScope
							};
						}
						else
						{
							source2 = from source in mailboxDiscoverySearch.Sources
							select new MailboxSearchScope
							{
								Mailbox = source,
								SearchScope = searchScope
							};
						}
					}
					else
					{
						source2 = new MailboxSearchScope[]
						{
							new MailboxSearchScope
							{
								Mailbox = this.mailboxId,
								SearchScope = searchScope
							}
						};
					}
					this.mailboxQueries = new MailboxQuery[1];
					this.mailboxQueries[0] = new MailboxQuery();
					this.mailboxQueries[0].MailboxSearchScopes = source2.ToArray<MailboxSearchScope>();
					this.mailboxQueries[0].Query = mailboxDiscoverySearch.CalculatedQuery;
					this.performDeduplication = true;
				}
			}
			if (this.mailboxQueries == null)
			{
				throw new ServiceArgumentException((CoreResources.IDs)2179607746U);
			}
		}

		// Token: 0x04001040 RID: 4160
		private const int DefaultPageSize = 100;

		// Token: 0x04001041 RID: 4161
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04001042 RID: 4162
		private readonly PropertyDefinition[] compactPreviewDataProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.ParentItemId,
			ItemSchema.Id,
			ItemSchema.Size
		};

		// Token: 0x04001043 RID: 4163
		private readonly PropertyDefinition[] previewDataProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.ParentItemId,
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.HasAttachment,
			ItemSchema.Size,
			ItemSchema.BodyTag,
			ItemSchema.InternetMessageId,
			ItemSchema.Subject,
			MessageItemSchema.IsRead,
			ItemSchema.SentTime,
			ItemSchema.ReceivedTime,
			MessageItemSchema.SenderDisplayName,
			MessageItemSchema.SenderSmtpAddress,
			ItemSchema.Importance,
			ItemSchema.Categories,
			ItemSchema.DisplayCc,
			ItemSchema.DisplayBcc,
			ItemSchema.DisplayTo,
			StoreObjectSchema.CreationTime
		};

		// Token: 0x04001044 RID: 4164
		private bool disposed;

		// Token: 0x04001045 RID: 4165
		private string searchId;

		// Token: 0x04001046 RID: 4166
		private string mailboxId;

		// Token: 0x04001047 RID: 4167
		private MailboxQuery[] mailboxQueries;

		// Token: 0x04001048 RID: 4168
		private MailboxQuery currentMailboxQuery;

		// Token: 0x04001049 RID: 4169
		private SearchResultType resultType;

		// Token: 0x0400104A RID: 4170
		private SortResults sortBy;

		// Token: 0x0400104B RID: 4171
		private string language;

		// Token: 0x0400104C RID: 4172
		private bool performDeduplication;

		// Token: 0x0400104D RID: 4173
		private int pageSize;

		// Token: 0x0400104E RID: 4174
		private string pageItemReference;

		// Token: 0x0400104F RID: 4175
		private PageDirection pageDirection;

		// Token: 0x04001050 RID: 4176
		private int stepCount;

		// Token: 0x04001051 RID: 4177
		private ExchangeRunspaceConfiguration runspaceConfig;

		// Token: 0x04001052 RID: 4178
		private IRecipientSession recipientSession;

		// Token: 0x04001053 RID: 4179
		private Dictionary<string, ADRawEntry> dictADRawEntries;

		// Token: 0x04001054 RID: 4180
		private List<PropertyDefinition> itemProperties;

		// Token: 0x04001055 RID: 4181
		private Microsoft.Exchange.Services.Core.Types.PreviewItemBaseShape baseShape;

		// Token: 0x04001056 RID: 4182
		private ExtendedPropertyUri[] additionalExtendedProperties;

		// Token: 0x04001057 RID: 4183
		private Dictionary<PropertyDefinition, ExtendedPropertyUri> xsoEwsPropertiesMap;

		// Token: 0x04001058 RID: 4184
		private readonly Guid requestId;

		// Token: 0x04001059 RID: 4185
		private readonly bool isFlighted;

		// Token: 0x0400105A RID: 4186
		private readonly ISearchPolicy policy;

		// Token: 0x0400105B RID: 4187
		private SearchMailboxesResponse response;
	}
}
