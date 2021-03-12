using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002EC RID: 748
	internal sealed class FindMailboxStatisticsByKeywords : MultiStepServiceCommand<FindMailboxStatisticsByKeywordsRequest, KeywordStatisticsSearchResult>, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001503 RID: 5379 RVA: 0x0006B052 File Offset: 0x00069252
		public FindMailboxStatisticsByKeywords(CallContext callContext, FindMailboxStatisticsByKeywordsRequest request) : base(callContext, request)
		{
			this.SaveRequestData(request);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0006B06F File Offset: 0x0006926F
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FindMailboxStatisticsByKeywords>(this);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0006B077 File Offset: 0x00069277
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0006B08C File Offset: 0x0006928C
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0006B0AE File Offset: 0x000692AE
		internal override int StepCount
		{
			get
			{
				return this.stepCount;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0006B0B6 File Offset: 0x000692B6
		internal override bool SupportsExternalUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0006B0B9 File Offset: 0x000692B9
		internal override Offer ExpectedOffer
		{
			get
			{
				return Offer.MailboxSearch;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0006B0C0 File Offset: 0x000692C0
		internal override TimeSpan? MaxExecutionTime
		{
			get
			{
				return FindMailboxStatisticsByKeywords.DefaultMaxExecutionTime;
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0006B0C8 File Offset: 0x000692C8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			FindMailboxStatisticsByKeywordsResponse findMailboxStatisticsByKeywordsResponse = new FindMailboxStatisticsByKeywordsResponse();
			findMailboxStatisticsByKeywordsResponse.AddResponses(this.currentMailbox, base.Results);
			return findMailboxStatisticsByKeywordsResponse;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0006B0F0 File Offset: 0x000692F0
		internal override void PreExecuteCommand()
		{
			this.ValidateRequestData();
			this.PerformAuthorization();
			try
			{
				this.MailboxSearch.CreateSearchMailboxCriteria(this.language, this.ConvertCollectionToMultiValuedProperty<string>(this.senders), this.ConvertCollectionToMultiValuedProperty<string>(this.recipients), this.fromDate, this.toDate, this.ConvertCollectionToMultiValuedProperty<KindKeyword>(this.messageTypes), this.userQuery, this.searchDumpster, this.includeUnsearchableItems, this.includePersonalArchive);
			}
			catch (ParserException ex)
			{
				ExTraceGlobals.SearchTracer.TraceError<string, string>((long)this.GetHashCode(), "Query: \"{0}\" returns ParserException: {1}", this.userQuery, ex.ToString());
				throw new ServiceArgumentException((CoreResources.IDs)3021008902U);
			}
			if (this.MailboxSearch.SearchCriteria.SubSearchFilters == null)
			{
				this.stepCount = 1;
			}
			else
			{
				if (this.MailboxSearch.SearchCriteria.SubSearchFilters.Count > 0)
				{
					this.actualKeywords = new string[this.MailboxSearch.SearchCriteria.SubSearchFilters.Count];
					int num = 0;
					foreach (string text in this.MailboxSearch.SearchCriteria.SubSearchFilters.Keys)
					{
						this.actualKeywords[num++] = text;
					}
				}
				this.stepCount = this.MailboxSearch.SearchCriteria.SubSearchFilters.Count + 1;
			}
			if (this.includeUnsearchableItems)
			{
				this.stepCount++;
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0006B284 File Offset: 0x00069484
		internal override ServiceResult<KeywordStatisticsSearchResult> Execute()
		{
			KeywordStatisticsSearchResult keywordStatisticsSearchResult;
			if (base.CurrentStep == 0)
			{
				this.mailboxSearch.CreateSearchFolderAndPerformInitialEstimation();
				keywordStatisticsSearchResult = this.GetSearchResult(this.mailboxSearch.SearchCriteria.UserQuery);
				if (keywordStatisticsSearchResult.ItemHits == 0)
				{
					this.skipProcessingKeywords = true;
				}
			}
			else if (this.includeUnsearchableItems && base.CurrentStep == this.stepCount - 1)
			{
				keywordStatisticsSearchResult = this.GetSearchResult("652beee2-75f7-4ca0-8a02-0698a3919cb9");
			}
			else
			{
				string keyword = this.actualKeywords[base.CurrentStep - 1];
				if (this.skipProcessingKeywords)
				{
					keywordStatisticsSearchResult = new KeywordStatisticsSearchResult
					{
						Keyword = keyword,
						ItemHits = 0,
						Size = 0UL
					};
				}
				else
				{
					this.mailboxSearch.PerformSingleKeywordSearch(keyword);
					keywordStatisticsSearchResult = this.GetSearchResult(keyword);
				}
			}
			return new ServiceResult<KeywordStatisticsSearchResult>(keywordStatisticsSearchResult);
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x0006B346 File Offset: 0x00069546
		private MailboxSearch MailboxSearch
		{
			get
			{
				return this.mailboxSearch;
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0006B34E File Offset: 0x0006954E
		private void Dispose(bool fromDispose)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.disposed)
			{
				if (this.mailboxSearch != null)
				{
					this.mailboxSearch.Dispose();
					this.mailboxSearch = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0006B38C File Offset: 0x0006958C
		private void SaveRequestData(FindMailboxStatisticsByKeywordsRequest request)
		{
			this.mailboxes = request.Mailboxes;
			this.keywords = request.Keywords;
			this.language = this.GetCultureInfo(request.Language);
			this.senders = request.Senders;
			this.recipients = request.Recipients;
			this.fromDate = (request.FromDateSpecified ? request.FromDate : DateTime.MinValue);
			this.toDate = (request.ToDateSpecified ? request.ToDate : DateTime.MinValue);
			this.messageTypes = this.GetMessageTypes(request.MessageTypes);
			this.searchDumpster = (request.SearchDumpsterSpecified && request.SearchDumpster);
			this.includePersonalArchive = (request.IncludePersonalArchiveSpecified && request.IncludePersonalArchive);
			this.includeUnsearchableItems = (request.IncludeUnsearchableItemsSpecified && request.IncludeUnsearchableItems);
			this.currentMailbox = this.mailboxes[0];
			this.userQuery = this.keywords[0];
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0006B484 File Offset: 0x00069684
		private void ValidateRequestData()
		{
			if (this.mailboxes.Length > 1)
			{
				throw new ServiceArgumentException((CoreResources.IDs)3169826345U);
			}
			if (this.userQuery.Length > FindMailboxStatisticsByKeywords.MaxQueryLength)
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidSearchQueryLength);
			}
			if ((!this.fromDate.Equals(DateTime.MinValue) && this.fromDate.Kind != DateTimeKind.Utc) || (!this.toDate.Equals(DateTime.MinValue) && this.toDate.Kind != DateTimeKind.Utc))
			{
				throw new ServiceArgumentException((CoreResources.IDs)2643283981U);
			}
			try
			{
				if (this.currentMailbox.IsArchive)
				{
					new Guid(this.currentMailbox.Id);
				}
				else
				{
					new SmtpAddress(this.currentMailbox.Id);
				}
			}
			catch (Exception)
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidMailboxIdFormat);
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0006B574 File Offset: 0x00069774
		private MultiValuedProperty<T> ConvertCollectionToMultiValuedProperty<T>(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				return null;
			}
			MultiValuedProperty<T> multiValuedProperty = new MultiValuedProperty<T>();
			foreach (T item in collection)
			{
				if (!multiValuedProperty.Contains(item))
				{
					multiValuedProperty.Add(item);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0006B5D4 File Offset: 0x000697D4
		private CultureInfo GetCultureInfo(string cultureInfoString)
		{
			CultureInfo cultureInfo = null;
			try
			{
				if (!string.IsNullOrEmpty(cultureInfoString))
				{
					cultureInfo = new CultureInfo(cultureInfoString);
				}
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.SearchTracer.TraceDebug<string>((long)this.GetHashCode(), "Exception trying to parse culture info string: {0}", ex.ToString());
			}
			if (cultureInfo == null)
			{
				ExTraceGlobals.SearchTracer.TraceDebug((long)this.GetHashCode(), "Invalid or no query language is specified, default to en-us");
				cultureInfo = new CultureInfo("en-us");
			}
			return cultureInfo;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0006B648 File Offset: 0x00069848
		private KindKeyword[] GetMessageTypes(SearchItemKind[] messageTypesFromRequest)
		{
			if (messageTypesFromRequest == null || messageTypesFromRequest.Length == 0)
			{
				return null;
			}
			KindKeyword[] array = new KindKeyword[messageTypesFromRequest.Length];
			int num = 0;
			foreach (SearchItemKind searchItemKind in messageTypesFromRequest)
			{
				array[num++] = (KindKeyword)Enum.Parse(typeof(KindKeyword), searchItemKind.ToString(), true);
			}
			return array;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0006B6AC File Offset: 0x000698AC
		private void PerformAuthorization()
		{
			try
			{
				bool enabled = VariantConfiguration.InvariantNoFlightingSnapshot.Ews.ExternalUser.Enabled;
				if (base.CallContext.IsExternalUser)
				{
					ExternalCallContext externalCallContext = (ExternalCallContext)base.CallContext;
					SmtpAddress emailAddress = externalCallContext.EmailAddress;
					OrganizationId organizationId = DomainToOrganizationIdCache.Singleton.Get(new SmtpDomain(emailAddress.Domain));
					if (organizationId == null)
					{
						ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "Organization cannot be found.");
						throw new ServiceAccessDeniedException();
					}
					if (!enabled)
					{
						ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "External user is not supported in non datacenter scenario.");
						throw new ServiceAccessDeniedException();
					}
					OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
					OrganizationRelationship organizationRelationship = organizationIdCacheValue.GetOrganizationRelationship(emailAddress.Domain);
					if (organizationRelationship == null)
					{
						ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "Organization relationship for the organization cannot be found.");
						throw new ServiceAccessDeniedException();
					}
					if (!organizationRelationship.Enabled)
					{
						ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "Organization relationship is not enabled.");
						throw new ServiceAccessDeniedException();
					}
					this.mailboxSearch = new MailboxSearch(this.currentMailbox.Id, this.currentMailbox.IsArchive, organizationId);
					if (!organizationId.Equals(this.MailboxSearch.OrganizationId))
					{
						ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "User does not belong to same organization.");
						throw new ServiceAccessDeniedException();
					}
				}
				else
				{
					if (enabled)
					{
						ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "Non external user is not supported in datacenter scenario.");
						throw new ServiceAccessDeniedException();
					}
					this.mailboxSearch = new MailboxSearch(this.currentMailbox.Id, this.currentMailbox.IsArchive, OrganizationId.ForestWideOrgId);
					ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangeRunspaceConfigurationCache.Singleton.Get(base.CallContext.EffectiveCaller, null, false);
					if (!exchangeRunspaceConfiguration.HasRoleOfType(RoleType.MailboxSearch))
					{
						ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "User does not have mailbox search role.");
						throw new ServiceAccessDeniedException();
					}
				}
			}
			catch (AuthzException innerException)
			{
				throw new ServiceAccessDeniedException(innerException);
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0006B8B8 File Offset: 0x00069AB8
		private KeywordStatisticsSearchResult GetSearchResult(string keyword)
		{
			KeywordStatisticsSearchResult keywordStatisticsSearchResult = new KeywordStatisticsSearchResult();
			keywordStatisticsSearchResult.Keyword = keyword;
			if (this.mailboxSearch.SearchResult != null && this.mailboxSearch.SearchResult.SubQueryResults.ContainsKey(keyword))
			{
				keywordStatisticsSearchResult.ItemHits = this.mailboxSearch.SearchResult.SubQueryResults[keyword].Count;
				keywordStatisticsSearchResult.Size = this.mailboxSearch.SearchResult.SubQueryResults[keyword].Size.ToBytes();
			}
			return keywordStatisticsSearchResult;
		}

		// Token: 0x04000E45 RID: 3653
		private static readonly TimeSpan? DefaultMaxExecutionTime = new TimeSpan?(TimeSpan.FromMinutes(10.0));

		// Token: 0x04000E46 RID: 3654
		private static readonly int MaxQueryLength = 10240;

		// Token: 0x04000E47 RID: 3655
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000E48 RID: 3656
		private int stepCount;

		// Token: 0x04000E49 RID: 3657
		private UserMailbox[] mailboxes;

		// Token: 0x04000E4A RID: 3658
		private string[] keywords;

		// Token: 0x04000E4B RID: 3659
		private string[] actualKeywords;

		// Token: 0x04000E4C RID: 3660
		private string userQuery;

		// Token: 0x04000E4D RID: 3661
		private CultureInfo language;

		// Token: 0x04000E4E RID: 3662
		private string[] senders;

		// Token: 0x04000E4F RID: 3663
		private string[] recipients;

		// Token: 0x04000E50 RID: 3664
		private DateTime fromDate;

		// Token: 0x04000E51 RID: 3665
		private DateTime toDate;

		// Token: 0x04000E52 RID: 3666
		private KindKeyword[] messageTypes;

		// Token: 0x04000E53 RID: 3667
		private bool searchDumpster;

		// Token: 0x04000E54 RID: 3668
		private bool includePersonalArchive;

		// Token: 0x04000E55 RID: 3669
		private bool includeUnsearchableItems;

		// Token: 0x04000E56 RID: 3670
		private UserMailbox currentMailbox;

		// Token: 0x04000E57 RID: 3671
		private MailboxSearch mailboxSearch;

		// Token: 0x04000E58 RID: 3672
		private bool skipProcessingKeywords;

		// Token: 0x04000E59 RID: 3673
		private bool disposed;
	}
}
