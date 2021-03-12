using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200012D RID: 301
	public class SearchExecutionDiagnostics : ExecutionDiagnostics
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x0003B477 File Offset: 0x00039677
		private SearchExecutionDiagnostics(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, ExchangeId searchFolderId)
		{
			this.statistics = SearchExecutionDiagnostics.SearchOperationStatistics.Create();
			base.DatabaseGuid = databaseGuid;
			base.MailboxNumber = mailboxNumber;
			base.MailboxGuid = mailboxGuid;
			this.correlationId = CorrelationIdHelper.GetCorrelationId(base.MailboxNumber, searchFolderId.ToLong());
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0003B4B7 File Offset: 0x000396B7
		public Guid CorrelationId
		{
			get
			{
				return this.correlationId;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0003B4BF File Offset: 0x000396BF
		protected override bool HasDataToLog
		{
			get
			{
				return this.IsSlowOperation || base.HasDataToLog;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0003B4D4 File Offset: 0x000396D4
		private bool IsSlowOperation
		{
			get
			{
				return ConfigurationSchema.SearchTraceFastOperationThreshold.Value <= this.statistics.SlowestResponse || ConfigurationSchema.SearchTraceFastTotalThreshold.Value <= this.statistics.TotalResponse || ConfigurationSchema.SearchTraceStoreOperationThreshold.Value <= this.statistics.SlowestLinked || ConfigurationSchema.SearchTraceStoreTotalThreshold.Value <= this.statistics.TotalLink || ConfigurationSchema.SearchTraceFirstLinkedThreshold.Value <= this.statistics.FirstLinked || ConfigurationSchema.SearchTraceGetRestrictionThreshold.Value <= this.searchRestrictionComputation || ConfigurationSchema.SearchTraceGetQueryPlanThreshold.Value <= this.searchQueryPlanComputation;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0003B5A1 File Offset: 0x000397A1
		public static SearchExecutionDiagnostics Create(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, ExchangeId searchFolderId)
		{
			return new SearchExecutionDiagnostics(databaseGuid, mailboxGuid, mailboxNumber, searchFolderId);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0003B5AC File Offset: 0x000397AC
		public override void FormatCommonInformation(TraceContentBuilder cb, int indentLevel, Guid correlationId)
		{
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Database guid: " + base.DatabaseGuid);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Mailbox number: " + base.MailboxNumber);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Client type: " + base.ClientType);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Correlation ID: " + correlationId);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003B624 File Offset: 0x00039824
		internal void OnGetRestriction(object restriction)
		{
			this.clientRestriction = restriction;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003B630 File Offset: 0x00039830
		internal void OnInitiateQuery(ExchangeId searchFolderId, TimeSpan searchRestrictionCalculationTime, TimeSpan searchPlanCalculationTime, bool pulseSearchPopulation, bool nestedSearchFolder, SearchState initialSearchState, SetSearchCriteriaFlags searchCriteriaFlags, int maxResultCount, ExchangeId firstScopeFolder, string friendlyFolderName, int scopeFoldersCount, int expandedScopeFoldersCount, SimpleQueryOperator.SimpleQueryOperatorDefinition planDefinition)
		{
			this.queryStartTime = DateTime.UtcNow - this.searchQueryPlanComputation;
			this.correlationId = CorrelationIdHelper.GetCorrelationId(base.MailboxNumber, searchFolderId.ToLong());
			this.searchRestrictionComputation = searchRestrictionCalculationTime;
			this.searchQueryPlanComputation = searchPlanCalculationTime;
			this.isPulsedSearchPopulation = pulseSearchPopulation;
			this.isNestedSearchFolder = nestedSearchFolder;
			this.initialSearchState = initialSearchState;
			this.searchCriteriaFlags = searchCriteriaFlags;
			this.maxCountForPulsedPopulation = maxResultCount;
			this.firstScopeFolderId = firstScopeFolder.ToString();
			this.firstScopeFolderFriendlyName = friendlyFolderName;
			this.scopeFoldersCount = scopeFoldersCount;
			this.expandedScopeFoldersCount = expandedScopeFoldersCount;
			this.planDefinition = planDefinition;
			this.storeResidual = SearchExecutionDiagnostics.FormatResidual(this.planDefinition);
			this.statistics.Clear();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0003B6F0 File Offset: 0x000398F0
		internal void OnIssueFastRequest(int pageSize, FqlQuery fastQuery)
		{
			this.fastPageSize = pageSize;
			this.fastQuery = fastQuery.Value;
			this.scrubbedQuery = fastQuery.ScrubbedValue;
			this.termLength = fastQuery.TermLength;
			this.numberFastTrips++;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0003B72C File Offset: 0x0003992C
		internal void OnReceiveFastResponse(int rowsReceivedFromFast, TimeSpan elapsedTime)
		{
			SearchExecutionDiagnostics.SearchOperationFastResponse operation = SearchExecutionDiagnostics.SearchOperationFastResponse.Create(rowsReceivedFromFast, elapsedTime);
			this.statistics.Add(operation);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0003B74D File Offset: 0x0003994D
		internal void OnReceiveFastError(string errorMessage)
		{
			this.errorMessage = errorMessage;
			this.OnReceiveFastError(errorMessage, TimeSpan.Zero);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0003B764 File Offset: 0x00039964
		internal void OnReceiveFastError(string errorMessage, TimeSpan elapsedTime)
		{
			SearchExecutionDiagnostics.SearchOperationFastError operation = SearchExecutionDiagnostics.SearchOperationFastError.Create(errorMessage, elapsedTime);
			this.statistics.Add(operation);
			this.errorMessage = errorMessage;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0003B78C File Offset: 0x0003998C
		internal void OnSearchOperation(string prefix, TimeSpan elapsedTime)
		{
			SearchExecutionDiagnostics.SearchOperation operation = SearchExecutionDiagnostics.SearchOperation.Create(prefix, elapsedTime);
			this.statistics.Add(operation);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003B7B0 File Offset: 0x000399B0
		internal void OnLinkedResults(int messagesLinked, int fullTextRowsProcessed, int totalMessagesLinked, TimeSpan elapsedTime)
		{
			SearchExecutionDiagnostics.SearchOperationLinkedRows operation = SearchExecutionDiagnostics.SearchOperationLinkedRows.Create(messagesLinked, fullTextRowsProcessed, totalMessagesLinked, elapsedTime);
			this.timeToLinkFirstResults = DateTime.UtcNow - this.queryStartTime;
			this.statistics.Add(operation);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0003B7EC File Offset: 0x000399EC
		internal void OnCompleteQuery(int totalMessagesLinked, bool linkCountReached, uint totalFullTextIndexRowsProcessed, SearchState finalSearchState, string clientActionString)
		{
			this.totalMessagesLinked = totalMessagesLinked;
			this.isMaximumLinkCountReached = linkCountReached;
			this.totalFullTextRowsProcessed = totalFullTextIndexRowsProcessed;
			this.finalSearchState = finalSearchState;
			this.clientActionString = clientActionString;
			base.DumpDiagnosticIfNeeded(ExecutionDiagnostics.GetLogger(LoggerType.FullTextIndex), LoggerManager.TraceGuids.FullTextIndexDetail, this.correlationId);
			DateTime utcNow = DateTime.UtcNow;
			FullTextIndexLogger.LogSingleLineQuery(this.correlationId, this.queryStartTime, utcNow, base.DatabaseGuid, base.MailboxGuid, base.MailboxNumber, (int)base.ClientType, this.scrubbedQuery, this.errorMessage != null, this.errorMessage, this.isMaximumLinkCountReached, this.storeResidual, this.numberFastTrips, this.isPulsedSearchPopulation, this.firstScopeFolderId, this.maxCountForPulsedPopulation, this.scopeFoldersCount, (int)this.initialSearchState, (int)this.finalSearchState, (int)this.searchCriteriaFlags, this.isNestedSearchFolder, this.statistics.FirstFastResults, this.statistics.FastResultsToLinkFirstSet, this.statistics.TotalFastResults, totalMessagesLinked, this.searchRestrictionComputation, this.searchQueryPlanComputation, this.statistics.FirstFastResultsTime, this.statistics.FastTimeToLinkResults, this.statistics.FastTrips, this.statistics.FirstRowsLinked ? ((int)this.timeToLinkFirstResults.TotalMilliseconds) : ((int)(utcNow - this.queryStartTime).TotalMilliseconds), (int)this.statistics.TotalResponse.TotalMilliseconds, this.expandedScopeFoldersCount, this.firstScopeFolderFriendlyName, this.totalFullTextRowsProcessed, this.clientActionString, this.fastQuery, this.termLength);
			this.statistics.Clear();
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0003B982 File Offset: 0x00039B82
		internal void OnBeforeSearchPopulationTaskStep()
		{
			FullTextIndexLogger.LogOtherSuboperation(base.DatabaseGuid, base.MailboxNumber, (int)base.ClientType, this.correlationId, 1);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0003B9A2 File Offset: 0x00039BA2
		internal void OnInsideSearchPopulationTaskStep()
		{
			FullTextIndexLogger.LogOtherSuboperation(base.DatabaseGuid, base.MailboxNumber, (int)base.ClientType, this.correlationId, 2);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0003B9C2 File Offset: 0x00039BC2
		internal TimeSpan GetFastTotalResponseTime()
		{
			return this.statistics.TotalResponse;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0003B9D0 File Offset: 0x00039BD0
		protected override void FormatThresholdInformation(TraceContentBuilder cb, int indentLevel)
		{
			long value = (long)this.statistics.SlowestResponse.TotalMilliseconds;
			long value2 = (long)this.statistics.TotalResponse.TotalMilliseconds;
			long value3 = (long)this.statistics.SlowestLinked.TotalMilliseconds;
			long value4 = (long)this.statistics.TotalLink.TotalMilliseconds;
			long value5 = (long)this.statistics.FirstLinked.TotalMilliseconds;
			long value6 = (long)this.searchRestrictionComputation.TotalMilliseconds;
			long value7 = (long)this.searchQueryPlanComputation.TotalMilliseconds;
			long threshold = (long)ConfigurationSchema.SearchTraceFastOperationThreshold.Value.TotalMilliseconds;
			long threshold2 = (long)ConfigurationSchema.SearchTraceFastTotalThreshold.Value.TotalMilliseconds;
			long threshold3 = (long)ConfigurationSchema.SearchTraceStoreOperationThreshold.Value.TotalMilliseconds;
			long threshold4 = (long)ConfigurationSchema.SearchTraceStoreTotalThreshold.Value.TotalMilliseconds;
			long threshold5 = (long)ConfigurationSchema.SearchTraceFirstLinkedThreshold.Value.TotalMilliseconds;
			long threshold6 = (long)ConfigurationSchema.SearchTraceGetRestrictionThreshold.Value.TotalMilliseconds;
			long threshold7 = (long)ConfigurationSchema.SearchTraceGetQueryPlanThreshold.Value.TotalMilliseconds;
			ExecutionDiagnostics.FormatLine(cb, 0, "Trace Thresholds:");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Slowest FTI Response", value, threshold, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Total FTI Response", value2, threshold2, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Slowest Store Linking", value3, threshold3, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Total Store Linking", value4, threshold4, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "First Messages Linked", value5, threshold5, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Restriction Computation", value6, threshold6, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Query Plan Computation", value7, threshold7, "ms");
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0003BBA0 File Offset: 0x00039DA0
		protected override void FormatOperationInformation(TraceContentBuilder cb, int indentLevel)
		{
			ExecutionDiagnostics.FormatLine(cb, 0, "Search characteristics:");
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Pulsed: " + this.isPulsedSearchPopulation);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Nested: " + this.isNestedSearchFolder);
			if (this.planDefinition != null && this.planDefinition.MaxRows > 0)
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "IsTopN: True (" + this.planDefinition.MaxRows.ToString() + ")");
			}
			else
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "IsTopN: False");
			}
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Max count: " + this.maxCountForPulsedPopulation);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Page size: " + this.fastPageSize);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Client action: " + this.clientActionString);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Search criteria flags: " + this.searchCriteriaFlags.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Initial search state: " + this.initialSearchState.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Final search state: " + this.finalSearchState.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "First scope folder ID: " + this.firstScopeFolderId);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "First scope folder Name: " + this.firstScopeFolderFriendlyName);
			long value = (long)this.searchRestrictionComputation.TotalMilliseconds;
			long value2 = (long)this.searchQueryPlanComputation.TotalMilliseconds;
			long value3 = (long)this.statistics.FirstLinked.TotalMilliseconds;
			ExecutionDiagnostics.FormatLine(cb, 0, "Execution information:");
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Scope folders top count: " + this.scopeFoldersCount.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Scope folders expanded count: " + this.expandedScopeFoldersCount.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Restriction computation time: " + SearchExecutionDiagnostics.Time.ToString(value) + " ms");
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Query plan computation time: " + SearchExecutionDiagnostics.Time.ToString(value2) + " ms");
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Time to first messages linked: " + SearchExecutionDiagnostics.Time.ToString(value3) + " ms");
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Total full text rows processed: " + this.totalFullTextRowsProcessed.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Total messages linked: " + this.totalMessagesLinked.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Maximum link count reached: " + this.isMaximumLinkCountReached.ToString());
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0003BE3C File Offset: 0x0003A03C
		protected override void FormatDiagnosticInformation(TraceContentBuilder cb, int indentLevel)
		{
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Search Operations:");
			foreach (SearchExecutionDiagnostics.SearchOperationBase searchOperationBase in this.statistics)
			{
				cb.Indent(indentLevel + 1);
				searchOperationBase.AppendToTraceContentBuilder(cb);
			}
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "FAST Query:");
			ExecutionDiagnostics.FormatLine(cb, indentLevel + 1, this.scrubbedQuery);
			if (this.clientRestriction != null)
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Client restriction:");
				ExecutionDiagnostics.FormatLine(cb, indentLevel + 1, this.clientRestriction.ToString());
			}
			if (this.planDefinition != null)
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Query plan:");
				ExecutionDiagnostics.FormatLine(cb, indentLevel + 1, this.planDefinition.ToString(StringFormatOptions.SkipParametersData));
			}
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Store residual:");
			ExecutionDiagnostics.FormatLine(cb, indentLevel + 1, this.storeResidual);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0003BF28 File Offset: 0x0003A128
		private static string FormatResidual(SimpleQueryOperator.SimpleQueryOperatorDefinition planDefinition)
		{
			if (planDefinition != null)
			{
				SearchCriteria criteria = planDefinition.Criteria;
				if (criteria != null)
				{
					StringBuilder stringBuilder = new StringBuilder(256);
					criteria.AppendToString(stringBuilder, StringFormatOptions.SkipParametersData);
					return stringBuilder.ToString();
				}
			}
			return "NONE";
		}

		// Token: 0x04000676 RID: 1654
		private readonly SearchExecutionDiagnostics.SearchOperationStatistics statistics;

		// Token: 0x04000677 RID: 1655
		private Guid correlationId;

		// Token: 0x04000678 RID: 1656
		private bool isPulsedSearchPopulation;

		// Token: 0x04000679 RID: 1657
		private bool isNestedSearchFolder;

		// Token: 0x0400067A RID: 1658
		private DateTime queryStartTime;

		// Token: 0x0400067B RID: 1659
		private TimeSpan searchRestrictionComputation;

		// Token: 0x0400067C RID: 1660
		private TimeSpan searchQueryPlanComputation;

		// Token: 0x0400067D RID: 1661
		private TimeSpan timeToLinkFirstResults;

		// Token: 0x0400067E RID: 1662
		private SearchState initialSearchState;

		// Token: 0x0400067F RID: 1663
		private SetSearchCriteriaFlags searchCriteriaFlags;

		// Token: 0x04000680 RID: 1664
		private int maxCountForPulsedPopulation;

		// Token: 0x04000681 RID: 1665
		private string firstScopeFolderId;

		// Token: 0x04000682 RID: 1666
		private string firstScopeFolderFriendlyName;

		// Token: 0x04000683 RID: 1667
		private string errorMessage;

		// Token: 0x04000684 RID: 1668
		private int scopeFoldersCount;

		// Token: 0x04000685 RID: 1669
		private int expandedScopeFoldersCount;

		// Token: 0x04000686 RID: 1670
		private int numberFastTrips;

		// Token: 0x04000687 RID: 1671
		private object clientRestriction;

		// Token: 0x04000688 RID: 1672
		private SimpleQueryOperator.SimpleQueryOperatorDefinition planDefinition;

		// Token: 0x04000689 RID: 1673
		private int fastPageSize;

		// Token: 0x0400068A RID: 1674
		private string fastQuery;

		// Token: 0x0400068B RID: 1675
		private string scrubbedQuery;

		// Token: 0x0400068C RID: 1676
		private string termLength;

		// Token: 0x0400068D RID: 1677
		private int totalMessagesLinked;

		// Token: 0x0400068E RID: 1678
		private bool isMaximumLinkCountReached;

		// Token: 0x0400068F RID: 1679
		private uint totalFullTextRowsProcessed;

		// Token: 0x04000690 RID: 1680
		private SearchState finalSearchState;

		// Token: 0x04000691 RID: 1681
		private string clientActionString;

		// Token: 0x04000692 RID: 1682
		private string storeResidual;

		// Token: 0x0200012E RID: 302
		private enum SearchOtherSuboperationType
		{
			// Token: 0x04000694 RID: 1684
			BeforeSearchPopulationTaskStep = 1,
			// Token: 0x04000695 RID: 1685
			InsideSearchPopulationTaskStep
		}

		// Token: 0x0200012F RID: 303
		public class SearchOperationStatistics : IEnumerable<SearchExecutionDiagnostics.SearchOperationBase>, IEnumerable
		{
			// Token: 0x06000BA9 RID: 2985 RVA: 0x0003BF61 File Offset: 0x0003A161
			private SearchOperationStatistics()
			{
				this.operations = new List<SearchExecutionDiagnostics.SearchOperationBase>(10);
				this.fastTrips = new StringBuilder(50);
				this.isFirstFastResult = true;
			}

			// Token: 0x1700030B RID: 779
			// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0003BF8A File Offset: 0x0003A18A
			public TimeSpan SlowestResponse
			{
				get
				{
					return this.slowestResponseTime;
				}
			}

			// Token: 0x1700030C RID: 780
			// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0003BF92 File Offset: 0x0003A192
			public TimeSpan SlowestLinked
			{
				get
				{
					return this.slowestLinkedTime;
				}
			}

			// Token: 0x1700030D RID: 781
			// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0003BF9A File Offset: 0x0003A19A
			public TimeSpan TotalResponse
			{
				get
				{
					return this.totalResponseTime;
				}
			}

			// Token: 0x1700030E RID: 782
			// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0003BFA2 File Offset: 0x0003A1A2
			public TimeSpan TotalLink
			{
				get
				{
					return this.totalLinkTime;
				}
			}

			// Token: 0x1700030F RID: 783
			// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0003BFAA File Offset: 0x0003A1AA
			public TimeSpan FirstLinked
			{
				get
				{
					return this.firstLinkedTime;
				}
			}

			// Token: 0x17000310 RID: 784
			// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0003BFB2 File Offset: 0x0003A1B2
			public bool FirstRowsLinked
			{
				get
				{
					return this.firstRowsLinked;
				}
			}

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0003BFBA File Offset: 0x0003A1BA
			public int FirstFastResultsTime
			{
				get
				{
					return this.firstFastResultsTime;
				}
			}

			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0003BFC2 File Offset: 0x0003A1C2
			public int FirstFastResults
			{
				get
				{
					return this.firstFastResults;
				}
			}

			// Token: 0x17000313 RID: 787
			// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0003BFCA File Offset: 0x0003A1CA
			public int FastResultsToLinkFirstSet
			{
				get
				{
					return this.fastResultsToLinkFirstSet;
				}
			}

			// Token: 0x17000314 RID: 788
			// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0003BFD2 File Offset: 0x0003A1D2
			public int FastTimeToLinkResults
			{
				get
				{
					return this.fastTimeToLinkResults;
				}
			}

			// Token: 0x17000315 RID: 789
			// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0003BFDA File Offset: 0x0003A1DA
			public string FastTrips
			{
				get
				{
					return this.fastTrips.ToString();
				}
			}

			// Token: 0x17000316 RID: 790
			// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0003BFE7 File Offset: 0x0003A1E7
			public int TotalFastResults
			{
				get
				{
					return this.totalFastResults;
				}
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x0003BFEF File Offset: 0x0003A1EF
			public static SearchExecutionDiagnostics.SearchOperationStatistics Create()
			{
				return new SearchExecutionDiagnostics.SearchOperationStatistics();
			}

			// Token: 0x06000BB7 RID: 2999 RVA: 0x0003BFF8 File Offset: 0x0003A1F8
			public void Add(SearchExecutionDiagnostics.SearchOperationBase operation)
			{
				SearchExecutionDiagnostics.SearchOperationFastResponse searchOperationFastResponse = operation as SearchExecutionDiagnostics.SearchOperationFastResponse;
				SearchExecutionDiagnostics.SearchOperationLinkedRows searchOperationLinkedRows = operation as SearchExecutionDiagnostics.SearchOperationLinkedRows;
				this.operations.Add(operation);
				if (this.totalRowsLinked <= 0)
				{
					this.firstLinkedTime += operation.ElapsedTime;
				}
				if (searchOperationLinkedRows != null)
				{
					if (!this.firstRowsLinked)
					{
						this.fastTimeToLinkResults = (int)this.totalResponseTime.TotalMilliseconds;
						this.fastResultsToLinkFirstSet = this.totalFastResults;
					}
					this.totalLinkTime += operation.ElapsedTime;
					this.slowestLinkedTime = TimeSpan.FromTicks(Math.Max(this.slowestLinkedTime.Ticks, operation.ElapsedTime.Ticks));
					this.firstRowsLinked = (this.totalRowsLinked <= 0 && searchOperationLinkedRows.MessagesLinked > 0);
					this.totalRowsLinked += searchOperationLinkedRows.MessagesLinked;
				}
				if (searchOperationFastResponse != null)
				{
					this.totalResponseTime += operation.ElapsedTime;
					this.totalFastResults += searchOperationFastResponse.RowsReceived;
					this.slowestResponseTime = TimeSpan.FromTicks(Math.Max(this.slowestResponseTime.Ticks, operation.ElapsedTime.Ticks));
					this.fastTrips.AppendFormat("{0};", searchOperationFastResponse.ElapsedTime);
					if (this.isFirstFastResult)
					{
						this.isFirstFastResult = false;
						this.firstFastResultsTime = (int)searchOperationFastResponse.ElapsedTime.TotalMilliseconds;
						this.firstFastResults = searchOperationFastResponse.RowsReceived;
					}
				}
			}

			// Token: 0x06000BB8 RID: 3000 RVA: 0x0003C17F File Offset: 0x0003A37F
			public IEnumerator<SearchExecutionDiagnostics.SearchOperationBase> GetEnumerator()
			{
				return this.operations.GetEnumerator();
			}

			// Token: 0x06000BB9 RID: 3001 RVA: 0x0003C18C File Offset: 0x0003A38C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.operations.GetEnumerator();
			}

			// Token: 0x06000BBA RID: 3002 RVA: 0x0003C19C File Offset: 0x0003A39C
			public void Clear()
			{
				this.operations.Clear();
				this.firstLinkedTime = TimeSpan.Zero;
				this.slowestResponseTime = TimeSpan.Zero;
				this.slowestLinkedTime = TimeSpan.Zero;
				this.totalResponseTime = TimeSpan.Zero;
				this.totalLinkTime = TimeSpan.Zero;
				this.firstRowsLinked = false;
				this.totalRowsLinked = 0;
			}

			// Token: 0x04000696 RID: 1686
			private readonly IList<SearchExecutionDiagnostics.SearchOperationBase> operations;

			// Token: 0x04000697 RID: 1687
			private TimeSpan firstLinkedTime;

			// Token: 0x04000698 RID: 1688
			private TimeSpan slowestResponseTime;

			// Token: 0x04000699 RID: 1689
			private TimeSpan slowestLinkedTime;

			// Token: 0x0400069A RID: 1690
			private TimeSpan totalResponseTime;

			// Token: 0x0400069B RID: 1691
			private TimeSpan totalLinkTime;

			// Token: 0x0400069C RID: 1692
			private bool firstRowsLinked;

			// Token: 0x0400069D RID: 1693
			private bool isFirstFastResult;

			// Token: 0x0400069E RID: 1694
			private int fastTimeToLinkResults;

			// Token: 0x0400069F RID: 1695
			private int totalRowsLinked;

			// Token: 0x040006A0 RID: 1696
			private int firstFastResultsTime;

			// Token: 0x040006A1 RID: 1697
			private int firstFastResults;

			// Token: 0x040006A2 RID: 1698
			private int fastResultsToLinkFirstSet;

			// Token: 0x040006A3 RID: 1699
			private int totalFastResults;

			// Token: 0x040006A4 RID: 1700
			private StringBuilder fastTrips;
		}

		// Token: 0x02000130 RID: 304
		public abstract class SearchOperationBase
		{
			// Token: 0x06000BBB RID: 3003 RVA: 0x0003C1F9 File Offset: 0x0003A3F9
			protected SearchOperationBase(string prefix, TimeSpan elapsedTime)
			{
				this.prefix = prefix;
				this.elapsedTime = elapsedTime;
			}

			// Token: 0x17000317 RID: 791
			// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0003C20F File Offset: 0x0003A40F
			public TimeSpan ElapsedTime
			{
				get
				{
					return this.elapsedTime;
				}
			}

			// Token: 0x06000BBD RID: 3005 RVA: 0x0003C218 File Offset: 0x0003A418
			public virtual void AppendToTraceContentBuilder(TraceContentBuilder cb)
			{
				long value = (long)this.elapsedTime.TotalMicroseconds();
				cb.Append(this.prefix);
				cb.Append(": ");
				cb.Append(SearchExecutionDiagnostics.Time.ToString(value));
				cb.Append(" us");
			}

			// Token: 0x040006A5 RID: 1701
			private readonly string prefix;

			// Token: 0x040006A6 RID: 1702
			private readonly TimeSpan elapsedTime;
		}

		// Token: 0x02000131 RID: 305
		public sealed class SearchOperation : SearchExecutionDiagnostics.SearchOperationBase
		{
			// Token: 0x06000BBE RID: 3006 RVA: 0x0003C260 File Offset: 0x0003A460
			private SearchOperation(string prefix, TimeSpan elapsedTime) : base(prefix, elapsedTime)
			{
			}

			// Token: 0x06000BBF RID: 3007 RVA: 0x0003C26A File Offset: 0x0003A46A
			public static SearchExecutionDiagnostics.SearchOperation Create(string prefix, TimeSpan elapsedTime)
			{
				return new SearchExecutionDiagnostics.SearchOperation(prefix, elapsedTime);
			}

			// Token: 0x06000BC0 RID: 3008 RVA: 0x0003C273 File Offset: 0x0003A473
			public override void AppendToTraceContentBuilder(TraceContentBuilder cb)
			{
				base.AppendToTraceContentBuilder(cb);
				cb.AppendLine();
			}
		}

		// Token: 0x02000132 RID: 306
		public sealed class SearchOperationFastResponse : SearchExecutionDiagnostics.SearchOperationBase
		{
			// Token: 0x17000318 RID: 792
			// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0003C282 File Offset: 0x0003A482
			public int RowsReceived
			{
				get
				{
					return this.rowsReceived;
				}
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x0003C28A File Offset: 0x0003A48A
			private SearchOperationFastResponse(int rowsReceived, TimeSpan elapsedTime) : base("FRSP", elapsedTime)
			{
				this.rowsReceived = rowsReceived;
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x0003C29F File Offset: 0x0003A49F
			public static SearchExecutionDiagnostics.SearchOperationFastResponse Create(int rowsReceived, TimeSpan elapsedTime)
			{
				return new SearchExecutionDiagnostics.SearchOperationFastResponse(rowsReceived, elapsedTime);
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
			public override void AppendToTraceContentBuilder(TraceContentBuilder cb)
			{
				base.AppendToTraceContentBuilder(cb);
				cb.Append(", ");
				cb.Append(this.rowsReceived);
				cb.Append(" rows");
				cb.AppendLine();
			}

			// Token: 0x040006A7 RID: 1703
			private readonly int rowsReceived;
		}

		// Token: 0x02000133 RID: 307
		public sealed class SearchOperationLinkedRows : SearchExecutionDiagnostics.SearchOperationBase
		{
			// Token: 0x06000BC5 RID: 3013 RVA: 0x0003C2D9 File Offset: 0x0003A4D9
			private SearchOperationLinkedRows(int messagesLinked, int rowsProcessed, int totalLinked, TimeSpan elapsedTime) : base("SLNK", elapsedTime)
			{
				this.messagesLinked = messagesLinked;
				this.rowsProcessed = rowsProcessed;
				this.totalLinked = totalLinked;
			}

			// Token: 0x17000319 RID: 793
			// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0003C2FD File Offset: 0x0003A4FD
			public int MessagesLinked
			{
				get
				{
					return this.messagesLinked;
				}
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x0003C305 File Offset: 0x0003A505
			public static SearchExecutionDiagnostics.SearchOperationLinkedRows Create(int messagesLinked, int rowsProcessed, int totalLinked, TimeSpan elapsedTime)
			{
				return new SearchExecutionDiagnostics.SearchOperationLinkedRows(messagesLinked, rowsProcessed, totalLinked, elapsedTime);
			}

			// Token: 0x06000BC8 RID: 3016 RVA: 0x0003C310 File Offset: 0x0003A510
			public override void AppendToTraceContentBuilder(TraceContentBuilder cb)
			{
				base.AppendToTraceContentBuilder(cb);
				cb.Append(", ");
				cb.Append(this.messagesLinked);
				cb.Append(" linked, ");
				cb.Append(this.rowsProcessed);
				cb.Append(" rows, ");
				cb.Append(this.totalLinked);
				cb.Append(" total");
				cb.AppendLine();
			}

			// Token: 0x040006A8 RID: 1704
			private readonly int messagesLinked;

			// Token: 0x040006A9 RID: 1705
			private readonly int rowsProcessed;

			// Token: 0x040006AA RID: 1706
			private readonly int totalLinked;
		}

		// Token: 0x02000134 RID: 308
		public sealed class SearchOperationFastError : SearchExecutionDiagnostics.SearchOperationBase
		{
			// Token: 0x06000BC9 RID: 3017 RVA: 0x0003C37A File Offset: 0x0003A57A
			private SearchOperationFastError(string error, TimeSpan elapsedTime) : base("FERR", elapsedTime)
			{
				this.errorMessage = error;
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x0003C38F File Offset: 0x0003A58F
			public static SearchExecutionDiagnostics.SearchOperationFastError Create(string error, TimeSpan elapsedTime)
			{
				return new SearchExecutionDiagnostics.SearchOperationFastError(error, elapsedTime);
			}

			// Token: 0x06000BCB RID: 3019 RVA: 0x0003C398 File Offset: 0x0003A598
			public override void AppendToTraceContentBuilder(TraceContentBuilder cb)
			{
				base.AppendToTraceContentBuilder(cb);
				cb.Append(", ");
				cb.Append(this.errorMessage);
				cb.AppendLine();
			}

			// Token: 0x040006AB RID: 1707
			private readonly string errorMessage;
		}

		// Token: 0x02000135 RID: 309
		private static class Time
		{
			// Token: 0x06000BCC RID: 3020 RVA: 0x0003C3BE File Offset: 0x0003A5BE
			public static string ToString(long value)
			{
				return value.ToString("N0", CultureInfo.InvariantCulture);
			}

			// Token: 0x06000BCD RID: 3021 RVA: 0x0003C3D1 File Offset: 0x0003A5D1
			public static string ToString(double value)
			{
				return value.ToString("N0", CultureInfo.InvariantCulture);
			}
		}
	}
}
