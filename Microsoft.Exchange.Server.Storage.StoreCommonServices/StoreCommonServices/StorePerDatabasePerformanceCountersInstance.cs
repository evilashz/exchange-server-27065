using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200000B RID: 11
	internal sealed class StorePerDatabasePerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00005C58 File Offset: 0x00003E58
		internal StorePerDatabasePerformanceCountersInstance(string instanceName, StorePerDatabasePerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeIS Store")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalSearchesInProgress = new BufferedPerformanceCounter(base.CategoryName, "Total searches in progress", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSearchesInProgress, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesInProgress);
				ExPerformanceCounter exPerformanceCounter = new BufferedPerformanceCounter(base.CategoryName, "Search/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalSearches = new BufferedPerformanceCounter(base.CategoryName, "Total searches", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSearches, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalSearches);
				this.TotalSearchesBelow500msec = new BufferedPerformanceCounter(base.CategoryName, "Total search queries completed in 0-0.5 sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSearchesBelow500msec, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesBelow500msec);
				this.TotalSearchesBetween500msecTo2sec = new BufferedPerformanceCounter(base.CategoryName, "Total search queries completed in 0.5-2 sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSearchesBetween500msecTo2sec, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesBetween500msecTo2sec);
				this.TotalSearchesBetween2To10sec = new BufferedPerformanceCounter(base.CategoryName, "Total search queries completed in 2-10 sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSearchesBetween2To10sec, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesBetween2To10sec);
				this.TotalSearchesBetween10SecTo60Sec = new BufferedPerformanceCounter(base.CategoryName, "Total search queries completed in 10-60 sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSearchesBetween10SecTo60Sec, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesBetween10SecTo60Sec);
				this.TotalSearchesQueriesGreaterThan60Seconds = new BufferedPerformanceCounter(base.CategoryName, "Total search queries completed in > 60 sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSearchesQueriesGreaterThan60Seconds, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesQueriesGreaterThan60Seconds);
				ExPerformanceCounter exPerformanceCounter2 = new BufferedPerformanceCounter(base.CategoryName, "Successful search/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalSuccessfulSearches = new BufferedPerformanceCounter(base.CategoryName, "Total number of successful search queries", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSuccessfulSearches, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalSuccessfulSearches);
				this.SearchQueryResultRate = new BufferedPerformanceCounter(base.CategoryName, "Search results/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SearchQueryResultRate, new ExPerformanceCounter[0]);
				list.Add(this.SearchQueryResultRate);
				this.AverageSearchResultSize = new BufferedPerformanceCounter(base.CategoryName, "Average search results per query", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSearchResultSize, new ExPerformanceCounter[0]);
				list.Add(this.AverageSearchResultSize);
				this.AverageSearchResultSizeBase = new BufferedPerformanceCounter(base.CategoryName, "Base for AverageSearchQueryResultSize", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSearchResultSizeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageSearchResultSizeBase);
				ExPerformanceCounter exPerformanceCounter3 = new BufferedPerformanceCounter(base.CategoryName, "MultiMailbox Preview Search/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				ExPerformanceCounter exPerformanceCounter4 = new BufferedPerformanceCounter(base.CategoryName, "MultiMailbox Keyword Stats Search/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				ExPerformanceCounter exPerformanceCounter5 = new BufferedPerformanceCounter(base.CategoryName, "MultiMailbox Search Full Text Index Query/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.AverageMultiMailboxSearchTimeSpentInFTIndex = new BufferedPerformanceCounter(base.CategoryName, "Average MultiMailbox Search time spent in FullTextIndex", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxSearchTimeSpentInFTIndex, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchTimeSpentInFTIndex);
				this.AverageMultiMailboxSearchTimeSpentInFTIndexBase = new BufferedPerformanceCounter(base.CategoryName, "Base for AverageMultiMailboxSearchTimeSpentInFTIndex", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxSearchTimeSpentInFTIndexBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchTimeSpentInFTIndexBase);
				this.AverageMultiMailboxSearchTimeSpentInStore = new BufferedPerformanceCounter(base.CategoryName, "Average MultiMailbox Search time spent in Store calls", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxSearchTimeSpentInStore, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchTimeSpentInStore);
				this.AverageMultiMailboxSearchTimeSpentInStoreBase = new BufferedPerformanceCounter(base.CategoryName, "Average Execution Time Spent in Store Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxSearchTimeSpentInStoreBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchTimeSpentInStoreBase);
				this.AverageMultiMailboxPreviewSearchLatency = new BufferedPerformanceCounter(base.CategoryName, "Average Search Execution Time", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxPreviewSearchLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxPreviewSearchLatency);
				this.AverageMultiMailboxPreviewSearchLatencyBase = new BufferedPerformanceCounter(base.CategoryName, "Average MultiMailbox Preview Search Execution Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxPreviewSearchLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxPreviewSearchLatencyBase);
				this.AverageMultiMailboxKeywordStatsSearchLatency = new BufferedPerformanceCounter(base.CategoryName, "Average Keyword Stats Search Execution Time", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxKeywordStatsSearchLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxKeywordStatsSearchLatency);
				this.AverageMultiMailboxKeywordStatsSearchLatencyBase = new BufferedPerformanceCounter(base.CategoryName, "Average Keyword Stats Execution Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxKeywordStatsSearchLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxKeywordStatsSearchLatencyBase);
				this.AverageMultiMailboxPreviewSearchResultSize = new BufferedPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Failed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxPreviewSearchResultSize, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxPreviewSearchResultSize);
				this.AverageMultiMailboxPreviewSearchResultSizeBase = new BufferedPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Failed Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxPreviewSearchResultSizeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxPreviewSearchResultSizeBase);
				this.AverageMultiMailboxSearchQueryLength = new BufferedPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Query Length", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxSearchQueryLength, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchQueryLength);
				this.AverageMultiMailboxSearchQueryLengthBase = new BufferedPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Query Length Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxSearchQueryLengthBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchQueryLengthBase);
				this.AverageMultiMailboxSearchKeywordCount = new BufferedPerformanceCounter(base.CategoryName, "Average number of Keywords in MultiMailbox Search", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxSearchKeywordCount, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchKeywordCount);
				this.AverageMultiMailboxSearchKeywordCountBase = new BufferedPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Keyword Count Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMultiMailboxSearchKeywordCountBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchKeywordCountBase);
				this.TotalMultiMailboxPreviewSearches = new BufferedPerformanceCounter(base.CategoryName, "Total MultiMailbox preview searches", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMultiMailboxPreviewSearches, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalMultiMailboxPreviewSearches);
				this.TotalMultiMailboxKeywordStatsSearches = new BufferedPerformanceCounter(base.CategoryName, "Total MultiMailbox keyword statistics searches", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMultiMailboxKeywordStatsSearches, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TotalMultiMailboxKeywordStatsSearches);
				this.TotalMultiMailboxPreviewSearchesTimedOut = new BufferedPerformanceCounter(base.CategoryName, "Total multi mailbox preview searches timed out", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMultiMailboxPreviewSearchesTimedOut, new ExPerformanceCounter[0]);
				list.Add(this.TotalMultiMailboxPreviewSearchesTimedOut);
				this.TotalMultiMailboxSearchFTIQueryExecution = new BufferedPerformanceCounter(base.CategoryName, "Total multi mailbox searches Full Text Index Query Execution", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMultiMailboxSearchFTIQueryExecution, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.TotalMultiMailboxSearchFTIQueryExecution);
				this.TotalMultiMailboxKeywordStatsSearchesTimedOut = new BufferedPerformanceCounter(base.CategoryName, "Total multi mailbox keyword statistics searches timed out", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMultiMailboxKeywordStatsSearchesTimedOut, new ExPerformanceCounter[0]);
				list.Add(this.TotalMultiMailboxKeywordStatsSearchesTimedOut);
				this.MultiMailboxPreviewSearchesFailed = new BufferedPerformanceCounter(base.CategoryName, "Total failed multi mailbox Preview Searches", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MultiMailboxPreviewSearchesFailed, new ExPerformanceCounter[0]);
				list.Add(this.MultiMailboxPreviewSearchesFailed);
				this.MultiMailboxKeywordStatsSearchesFailed = new BufferedPerformanceCounter(base.CategoryName, "Total failed multi mailbox keyword statistics Searches", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MultiMailboxKeywordStatsSearchesFailed, new ExPerformanceCounter[0]);
				list.Add(this.MultiMailboxKeywordStatsSearchesFailed);
				this.MultiMailboxSearchesFTIndexFailed = new BufferedPerformanceCounter(base.CategoryName, "Total Multi Mailbox searches failed due to FullText failure", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MultiMailboxSearchesFTIndexFailed, new ExPerformanceCounter[0]);
				list.Add(this.MultiMailboxSearchesFTIndexFailed);
				this.LazyIndexesCreatedRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy indexes created/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesCreatedRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesCreatedRate);
				this.LazyIndexesDeletedRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy indexes deleted/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesDeletedRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesDeletedRate);
				this.LazyIndexesFullRefreshRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy index full refresh/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesFullRefreshRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesFullRefreshRate);
				this.LazyIndexesIncrementalRefreshRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy index incremental refresh/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesIncrementalRefreshRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesIncrementalRefreshRate);
				this.LazyIndexesVersionInvalidationRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy index invalidation/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesVersionInvalidationRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesVersionInvalidationRate);
				this.LazyIndexesLocaleVersionInvalidationRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy index invalidation due to locale version change/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesLocaleVersionInvalidationRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesLocaleVersionInvalidationRate);
				this.LazyIndexesTotalPopulate = new BufferedPerformanceCounter(base.CategoryName, "Lazy index total populations.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesTotalPopulate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesTotalPopulate);
				this.LazyIndexesPopulateFromIndex = new BufferedPerformanceCounter(base.CategoryName, "Lazy index populations from index.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesPopulateFromIndex, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesPopulateFromIndex);
				this.LazyIndexesPopulateChunked = new BufferedPerformanceCounter(base.CategoryName, "Lazy index chunked populations.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesPopulateChunked, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesPopulateChunked);
				this.LazyIndexesPopulateNonChunked = new BufferedPerformanceCounter(base.CategoryName, "Lazy index non-chunked populations.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesPopulateNonChunked, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesPopulateNonChunked);
				this.LazyIndexesPopulateTxNotPulsed = new BufferedPerformanceCounter(base.CategoryName, "Lazy index populations without transaction pulsing.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesPopulateTxNotPulsed, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesPopulateTxNotPulsed);
				this.FoldersOpenedRate = new BufferedPerformanceCounter(base.CategoryName, "Folders opened/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FoldersOpenedRate, new ExPerformanceCounter[0]);
				list.Add(this.FoldersOpenedRate);
				this.FoldersCreatedRate = new BufferedPerformanceCounter(base.CategoryName, "Folders created/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FoldersCreatedRate, new ExPerformanceCounter[0]);
				list.Add(this.FoldersCreatedRate);
				this.FoldersDeletedRate = new BufferedPerformanceCounter(base.CategoryName, "Folders deleted/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FoldersDeletedRate, new ExPerformanceCounter[0]);
				list.Add(this.FoldersDeletedRate);
				this.MessagesOpenedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages opened/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesOpenedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesOpenedRate);
				this.MessagesCreatedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages created/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesCreatedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesCreatedRate);
				this.MessagesUpdatedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages updated/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesUpdatedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesUpdatedRate);
				this.MessagesDeletedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages deleted/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesDeletedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeletedRate);
				this.SubobjectsOpenedRate = new BufferedPerformanceCounter(base.CategoryName, "Subobjects opened/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubobjectsOpenedRate, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsOpenedRate);
				this.SubobjectsCreatedRate = new BufferedPerformanceCounter(base.CategoryName, "Subobjects created/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubobjectsCreatedRate, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsCreatedRate);
				this.SubobjectsDeletedRate = new BufferedPerformanceCounter(base.CategoryName, "Subobjects deleted/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubobjectsDeletedRate, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsDeletedRate);
				this.SubobjectsCleanedRate = new BufferedPerformanceCounter(base.CategoryName, "Subobjects cleaned/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubobjectsCleanedRate, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsCleanedRate);
				this.TopMessagesCleanedRate = new BufferedPerformanceCounter(base.CategoryName, "TopMessages cleaned/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TopMessagesCleanedRate, new ExPerformanceCounter[0]);
				list.Add(this.TopMessagesCleanedRate);
				this.PropertyPromotionRate = new BufferedPerformanceCounter(base.CategoryName, "Property promotions/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PropertyPromotionRate, new ExPerformanceCounter[0]);
				list.Add(this.PropertyPromotionRate);
				this.PropertyPromotionTasks = new BufferedPerformanceCounter(base.CategoryName, "Property Promotion Tasks", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PropertyPromotionTasks, new ExPerformanceCounter[0]);
				list.Add(this.PropertyPromotionTasks);
				this.PropertyPromotionMessageRate = new BufferedPerformanceCounter(base.CategoryName, "Property promotion messages/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PropertyPromotionMessageRate, new ExPerformanceCounter[0]);
				list.Add(this.PropertyPromotionMessageRate);
				this.ActiveMailboxes = new BufferedPerformanceCounter(base.CategoryName, "Active mailboxes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMailboxes, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMailboxes);
				this.FolderHierarchyLoadNonRecursiveRate = new BufferedPerformanceCounter(base.CategoryName, "Non recursive folder hierarchy reloads/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FolderHierarchyLoadNonRecursiveRate, new ExPerformanceCounter[0]);
				list.Add(this.FolderHierarchyLoadNonRecursiveRate);
				this.FolderHierarchyLoadRecursiveRate = new BufferedPerformanceCounter(base.CategoryName, "Recursive folder hierarchy reloads/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FolderHierarchyLoadRecursiveRate, new ExPerformanceCounter[0]);
				list.Add(this.FolderHierarchyLoadRecursiveRate);
				this.SubobjectsInTombstone = new BufferedPerformanceCounter(base.CategoryName, "Sub objects in tombstone", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubobjectsInTombstone, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsInTombstone);
				this.TopMessagesInTombstone = new BufferedPerformanceCounter(base.CategoryName, "Top messages in tombstone", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TopMessagesInTombstone, new ExPerformanceCounter[0]);
				list.Add(this.TopMessagesInTombstone);
				this.TotalObjectsSizeInTombstone = new BufferedPerformanceCounter(base.CategoryName, "Total objects size in tombstone (bytes)", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalObjectsSizeInTombstone, new ExPerformanceCounter[0]);
				list.Add(this.TotalObjectsSizeInTombstone);
				this.MessageDeliveriesRate = new BufferedPerformanceCounter(base.CategoryName, "Messages Delivered/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessageDeliveriesRate, new ExPerformanceCounter[0]);
				list.Add(this.MessageDeliveriesRate);
				this.MessagesSubmittedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages Submitted/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesSubmittedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesSubmittedRate);
				this.MapiMessagesCreatedRate = new BufferedPerformanceCounter(base.CategoryName, "MAPI Messages Created/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MapiMessagesCreatedRate, new ExPerformanceCounter[0]);
				list.Add(this.MapiMessagesCreatedRate);
				this.MapiMessagesOpenedRate = new BufferedPerformanceCounter(base.CategoryName, "MAPI Messages Opened/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MapiMessagesOpenedRate, new ExPerformanceCounter[0]);
				list.Add(this.MapiMessagesOpenedRate);
				this.MapiMessagesModifiedRate = new BufferedPerformanceCounter(base.CategoryName, "MAPI Messages Modified/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MapiMessagesModifiedRate, new ExPerformanceCounter[0]);
				list.Add(this.MapiMessagesModifiedRate);
				this.RPCsInProgress = new BufferedPerformanceCounter(base.CategoryName, "RPC Requests", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RPCsInProgress, new ExPerformanceCounter[0]);
				list.Add(this.RPCsInProgress);
				this.PercentRPCsInProgress = new BufferedPerformanceCounter(base.CategoryName, "% RPC Requests", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PercentRPCsInProgress, new ExPerformanceCounter[0]);
				list.Add(this.PercentRPCsInProgress);
				this.PercentRPCsInProgressBase = new BufferedPerformanceCounter(base.CategoryName, "% RPC Requests Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PercentRPCsInProgressBase, new ExPerformanceCounter[0]);
				list.Add(this.PercentRPCsInProgressBase);
				this.RateOfRPCs = new BufferedPerformanceCounter(base.CategoryName, "RPC Packets/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfRPCs, new ExPerformanceCounter[0]);
				list.Add(this.RateOfRPCs);
				this.RateOfROPs = new BufferedPerformanceCounter(base.CategoryName, "RPC Operations/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfROPs, new ExPerformanceCounter[0]);
				list.Add(this.RateOfROPs);
				this.AverageRPCLatency = new BufferedPerformanceCounter(base.CategoryName, "RPC Average Latency", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageRPCLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageRPCLatency);
				this.AverageRPCLatencyBase = new BufferedPerformanceCounter(base.CategoryName, "Average Time spent in an RPC Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageRPCLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageRPCLatencyBase);
				this.ContextHandlePools = new BufferedPerformanceCounter(base.CategoryName, "RPC Pool: Pools", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ContextHandlePools, new ExPerformanceCounter[0]);
				list.Add(this.ContextHandlePools);
				this.ContextHandlePoolHandles = new BufferedPerformanceCounter(base.CategoryName, "RPC Pool: Context Handles", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ContextHandlePoolHandles, new ExPerformanceCounter[0]);
				list.Add(this.ContextHandlePoolHandles);
				this.ContextHandlePoolParkedCalls = new BufferedPerformanceCounter(base.CategoryName, "RPC Pool: Parked Async Notification Calls", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ContextHandlePoolParkedCalls, new ExPerformanceCounter[0]);
				list.Add(this.ContextHandlePoolParkedCalls);
				this.MailboxMaintenances = new BufferedPerformanceCounter(base.CategoryName, "Mailbox Level Maintenance Items", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailboxMaintenances, new ExPerformanceCounter[0]);
				list.Add(this.MailboxMaintenances);
				this.MailboxesWithMaintenances = new BufferedPerformanceCounter(base.CategoryName, "Mailboxes With Maintenance Items", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailboxesWithMaintenances, new ExPerformanceCounter[0]);
				list.Add(this.MailboxesWithMaintenances);
				this.RateOfMailboxMaintenances = new BufferedPerformanceCounter(base.CategoryName, "Mailbox Level Maintenances/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfMailboxMaintenances, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxMaintenances);
				this.RateOfDatabaseMaintenances = new BufferedPerformanceCounter(base.CategoryName, "Database Level Maintenances/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDatabaseMaintenances, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseMaintenances);
				this.ProcessId = new BufferedPerformanceCounter(base.CategoryName, "Process ID", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ProcessId, new ExPerformanceCounter[0]);
				list.Add(this.ProcessId);
				this.LostDiagnosticEntries = new BufferedPerformanceCounter(base.CategoryName, "Lost Diagnostic Entries", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LostDiagnosticEntries, new ExPerformanceCounter[0]);
				list.Add(this.LostDiagnosticEntries);
				this.QuarantinedMailboxCount = new BufferedPerformanceCounter(base.CategoryName, "Quarantined Mailbox Count", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.QuarantinedMailboxCount, new ExPerformanceCounter[0]);
				list.Add(this.QuarantinedMailboxCount);
				this.ScheduledISIntegCorruptionDetectedCount = new BufferedPerformanceCounter(base.CategoryName, "Scheduled ISInteg Detected Count", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ScheduledISIntegCorruptionDetectedCount, new ExPerformanceCounter[0]);
				list.Add(this.ScheduledISIntegCorruptionDetectedCount);
				this.ScheduledISIntegCorruptionFixedCount = new BufferedPerformanceCounter(base.CategoryName, "Scheduled ISInteg Fixed Count", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ScheduledISIntegCorruptionFixedCount, new ExPerformanceCounter[0]);
				list.Add(this.ScheduledISIntegCorruptionFixedCount);
				this.ScheduledISIntegMailboxRate = new BufferedPerformanceCounter(base.CategoryName, "Scheduled ISInteg/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ScheduledISIntegMailboxRate, new ExPerformanceCounter[0]);
				list.Add(this.ScheduledISIntegMailboxRate);
				this.ISIntegStorePendingJobs = new BufferedPerformanceCounter(base.CategoryName, "Integrity Check Pending Jobs", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ISIntegStorePendingJobs, new ExPerformanceCounter[0]);
				list.Add(this.ISIntegStorePendingJobs);
				this.ISIntegStoreTotalJobs = new BufferedPerformanceCounter(base.CategoryName, "Integrity Check Total Jobs", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ISIntegStoreTotalJobs, new ExPerformanceCounter[0]);
				list.Add(this.ISIntegStoreTotalJobs);
				this.ISIntegStoreFailedJobs = new BufferedPerformanceCounter(base.CategoryName, "Integrity Check Failed Jobs", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ISIntegStoreFailedJobs, new ExPerformanceCounter[0]);
				list.Add(this.ISIntegStoreFailedJobs);
				this.NumberOfActiveBackgroundTasks = new BufferedPerformanceCounter(base.CategoryName, "Number of active background tasks", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfActiveBackgroundTasks, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfActiveBackgroundTasks);
				this.AddressInfoSize = new BufferedPerformanceCounter(base.CategoryName, "Size of Address Info cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AddressInfoSize, new ExPerformanceCounter[0]);
				list.Add(this.AddressInfoSize);
				this.RateOfAddressInfoLookups = new BufferedPerformanceCounter(base.CategoryName, "Cache lookups in the AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfAddressInfoLookups, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoLookups);
				this.RateOfAddressInfoMisses = new BufferedPerformanceCounter(base.CategoryName, "Cache misses in the AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfAddressInfoMisses, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoMisses);
				this.RateOfAddressInfoHits = new BufferedPerformanceCounter(base.CategoryName, "Cache hits in the AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfAddressInfoHits, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoHits);
				this.RateOfAddressInfoInserts = new BufferedPerformanceCounter(base.CategoryName, "Cache inserts in the AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfAddressInfoInserts, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoInserts);
				this.RateOfAddressInfoDeletes = new BufferedPerformanceCounter(base.CategoryName, "Cache deletes in the AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfAddressInfoDeletes, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoDeletes);
				this.SizeOfAddressInfoExpirationQueue = new BufferedPerformanceCounter(base.CategoryName, "Size of the expiration queue for the AddressInfo cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SizeOfAddressInfoExpirationQueue, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfAddressInfoExpirationQueue);
				this.ForeignAddressInfoSize = new BufferedPerformanceCounter(base.CategoryName, "Size of Foreign Address Info cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ForeignAddressInfoSize, new ExPerformanceCounter[0]);
				list.Add(this.ForeignAddressInfoSize);
				this.RateOfForeignAddressInfoLookups = new BufferedPerformanceCounter(base.CategoryName, "Cache lookups in the Foreign AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignAddressInfoLookups, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoLookups);
				this.RateOfForeignAddressInfoMisses = new BufferedPerformanceCounter(base.CategoryName, "Cache misses in the Foreign AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignAddressInfoMisses, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoMisses);
				this.RateOfForeignAddressInfoHits = new BufferedPerformanceCounter(base.CategoryName, "Cache hits in the Foreign AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignAddressInfoHits, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoHits);
				this.RateOfForeignAddressInfoInserts = new BufferedPerformanceCounter(base.CategoryName, "Cache inserts in the Foreign AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignAddressInfoInserts, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoInserts);
				this.RateOfForeignAddressInfoDeletes = new BufferedPerformanceCounter(base.CategoryName, "Cache deletes in the Foreign AddressInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignAddressInfoDeletes, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoDeletes);
				this.SizeOfForeignAddressInfoExpirationQueue = new BufferedPerformanceCounter(base.CategoryName, "Size of the expiration queue for the Foreign AddressInfo cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SizeOfForeignAddressInfoExpirationQueue, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfForeignAddressInfoExpirationQueue);
				this.MailboxInfoSize = new BufferedPerformanceCounter(base.CategoryName, "Size of Mailbox Info cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailboxInfoSize, new ExPerformanceCounter[0]);
				list.Add(this.MailboxInfoSize);
				this.RateOfMailboxInfoLookups = new BufferedPerformanceCounter(base.CategoryName, "Cache lookups in the MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfMailboxInfoLookups, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoLookups);
				this.RateOfMailboxInfoMisses = new BufferedPerformanceCounter(base.CategoryName, "Cache misses in the MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfMailboxInfoMisses, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoMisses);
				this.RateOfMailboxInfoHits = new BufferedPerformanceCounter(base.CategoryName, "Cache hits in the MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfMailboxInfoHits, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoHits);
				this.RateOfMailboxInfoInserts = new BufferedPerformanceCounter(base.CategoryName, "Cache inserts in the MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfMailboxInfoInserts, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoInserts);
				this.RateOfMailboxInfoDeletes = new BufferedPerformanceCounter(base.CategoryName, "Cache deletes in the MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfMailboxInfoDeletes, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoDeletes);
				this.SizeOfMailboxInfoExpirationQueue = new BufferedPerformanceCounter(base.CategoryName, "Size of the expiration queue for the MailboxInfo cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SizeOfMailboxInfoExpirationQueue, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfMailboxInfoExpirationQueue);
				this.ForeignMailboxInfoSize = new BufferedPerformanceCounter(base.CategoryName, "Size of Foreign Mailbox Info cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ForeignMailboxInfoSize, new ExPerformanceCounter[0]);
				list.Add(this.ForeignMailboxInfoSize);
				this.RateOfForeignMailboxInfoLookups = new BufferedPerformanceCounter(base.CategoryName, "Cache lookups in the Foreign MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignMailboxInfoLookups, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoLookups);
				this.RateOfForeignMailboxInfoMisses = new BufferedPerformanceCounter(base.CategoryName, "Cache misses in the Foreign MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignMailboxInfoMisses, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoMisses);
				this.RateOfForeignMailboxInfoHits = new BufferedPerformanceCounter(base.CategoryName, "Cache hits in the Foreign MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignMailboxInfoHits, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoHits);
				this.RateOfForeignMailboxInfoInserts = new BufferedPerformanceCounter(base.CategoryName, "Cache inserts in the Foreign MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignMailboxInfoInserts, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoInserts);
				this.RateOfForeignMailboxInfoDeletes = new BufferedPerformanceCounter(base.CategoryName, "Cache deletes in the Foreign MailboxInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfForeignMailboxInfoDeletes, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoDeletes);
				this.SizeOfForeignMailboxInfoExpirationQueue = new BufferedPerformanceCounter(base.CategoryName, "Size of the expiration queue for the Foreign MailboxInfo cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SizeOfForeignMailboxInfoExpirationQueue, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfForeignMailboxInfoExpirationQueue);
				this.DatabaseInfoSize = new BufferedPerformanceCounter(base.CategoryName, "Size of Database Info cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DatabaseInfoSize, new ExPerformanceCounter[0]);
				list.Add(this.DatabaseInfoSize);
				this.RateOfDatabaseInfoLookups = new BufferedPerformanceCounter(base.CategoryName, "Cache lookups in the DatabaseInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDatabaseInfoLookups, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoLookups);
				this.RateOfDatabaseInfoMisses = new BufferedPerformanceCounter(base.CategoryName, "Cache misses in the DatabaseInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDatabaseInfoMisses, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoMisses);
				this.RateOfDatabaseInfoHits = new BufferedPerformanceCounter(base.CategoryName, "Cache hits in the DatabaseInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDatabaseInfoHits, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoHits);
				this.RateOfDatabaseInfoInserts = new BufferedPerformanceCounter(base.CategoryName, "Cache inserts in the DatabaseInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDatabaseInfoInserts, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoInserts);
				this.RateOfDatabaseInfoDeletes = new BufferedPerformanceCounter(base.CategoryName, "Cache deletes in the DatabaseInfo cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDatabaseInfoDeletes, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoDeletes);
				this.SizeOfDatabaseInfoExpirationQueue = new BufferedPerformanceCounter(base.CategoryName, "Size of the expiration queue for the DatabaseInfo cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SizeOfDatabaseInfoExpirationQueue, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfDatabaseInfoExpirationQueue);
				this.OrgContainerSize = new BufferedPerformanceCounter(base.CategoryName, "Size of Organization Container cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OrgContainerSize, new ExPerformanceCounter[0]);
				list.Add(this.OrgContainerSize);
				this.RateOfOrgContainerLookups = new BufferedPerformanceCounter(base.CategoryName, "Cache lookups in the Organization Container cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfOrgContainerLookups, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerLookups);
				this.RateOfOrgContainerMisses = new BufferedPerformanceCounter(base.CategoryName, "Cache misses in the Organization Container cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfOrgContainerMisses, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerMisses);
				this.RateOfOrgContainerHits = new BufferedPerformanceCounter(base.CategoryName, "Cache hits in the Organization Container cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfOrgContainerHits, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerHits);
				this.RateOfOrgContainerInserts = new BufferedPerformanceCounter(base.CategoryName, "Cache inserts in the Organization Container cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfOrgContainerInserts, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerInserts);
				this.RateOfOrgContainerDeletes = new BufferedPerformanceCounter(base.CategoryName, "Cache deletes in the Organization Container cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfOrgContainerDeletes, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerDeletes);
				this.SizeOfOrgContainerExpirationQueue = new BufferedPerformanceCounter(base.CategoryName, "Size of the expiration queue for the Organization Container cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SizeOfOrgContainerExpirationQueue, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfOrgContainerExpirationQueue);
				this.DistributionListMembershipSize = new BufferedPerformanceCounter(base.CategoryName, "Size of DistributionListMembership cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DistributionListMembershipSize, new ExPerformanceCounter[0]);
				list.Add(this.DistributionListMembershipSize);
				this.RateOfDistributionListMembershipLookups = new BufferedPerformanceCounter(base.CategoryName, "Cache lookups in the DistributionListMembership cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDistributionListMembershipLookups, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipLookups);
				this.RateOfDistributionListMembershipMisses = new BufferedPerformanceCounter(base.CategoryName, "Cache misses in the DistributionListMembership cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDistributionListMembershipMisses, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipMisses);
				this.RateOfDistributionListMembershipHits = new BufferedPerformanceCounter(base.CategoryName, "Cache hits in the DistributionListMembership cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDistributionListMembershipHits, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipHits);
				this.RateOfDistributionListMembershipInserts = new BufferedPerformanceCounter(base.CategoryName, "Cache inserts in the DistributionListMembership cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDistributionListMembershipInserts, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipInserts);
				this.RateOfDistributionListMembershipDeletes = new BufferedPerformanceCounter(base.CategoryName, "Cache deletes in the DistributionListMembership cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfDistributionListMembershipDeletes, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipDeletes);
				this.SizeOfDistributionListMembershipExpirationQueue = new BufferedPerformanceCounter(base.CategoryName, "Size of the expiration queue for the DistributionListMembership cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SizeOfDistributionListMembershipExpirationQueue, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfDistributionListMembershipExpirationQueue);
				this.LogicalIndexSize = new BufferedPerformanceCounter(base.CategoryName, "Size of LogicalIndex cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LogicalIndexSize, new ExPerformanceCounter[0]);
				list.Add(this.LogicalIndexSize);
				this.RateOfLogicalIndexLookups = new BufferedPerformanceCounter(base.CategoryName, "Cache lookups in the LogicalIndex cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfLogicalIndexLookups, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexLookups);
				this.RateOfLogicalIndexMisses = new BufferedPerformanceCounter(base.CategoryName, "Cache misses in the LogicalIndex cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfLogicalIndexMisses, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexMisses);
				this.RateOfLogicalIndexHits = new BufferedPerformanceCounter(base.CategoryName, "Cache hits in the LogicalIndex cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfLogicalIndexHits, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexHits);
				this.RateOfLogicalIndexInserts = new BufferedPerformanceCounter(base.CategoryName, "Cache inserts in the LogicalIndex cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfLogicalIndexInserts, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexInserts);
				this.RateOfLogicalIndexDeletes = new BufferedPerformanceCounter(base.CategoryName, "Cache deletes in the LogicalIndex cache/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RateOfLogicalIndexDeletes, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexDeletes);
				this.SizeOfLogicalIndexExpirationQueue = new BufferedPerformanceCounter(base.CategoryName, "Size of the expiration queue for the LogicalIndex cache", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SizeOfLogicalIndexExpirationQueue, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfLogicalIndexExpirationQueue);
				this.NumberOfScheduledLogicalIndexMaintenanceTasks = new BufferedPerformanceCounter(base.CategoryName, "Number of scheduled LogicalIndex maintenance tasks", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfScheduledLogicalIndexMaintenanceTasks, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfScheduledLogicalIndexMaintenanceTasks);
				this.NumberOfProcessingLogicalIndexMaintenanceTasks = new BufferedPerformanceCounter(base.CategoryName, "Number of processing LogicalIndex maintenance tasks", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfProcessingLogicalIndexMaintenanceTasks, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfProcessingLogicalIndexMaintenanceTasks);
				this.NumberOfMailboxesMarkedForWlmLogicalIndexMaintenanceTableMaintenance = new BufferedPerformanceCounter(base.CategoryName, "Number of mailboxes marked for WLM LogicalIndex maintenance table maintenance", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfMailboxesMarkedForWlmLogicalIndexMaintenanceTableMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfMailboxesMarkedForWlmLogicalIndexMaintenanceTableMaintenance);
				this.NumberOfActiveWlmLogicalIndexMaintenanceTableMaintenances = new BufferedPerformanceCounter(base.CategoryName, "Number of active WLM LogicalIndex maintenance table maintenances", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfActiveWlmLogicalIndexMaintenanceTableMaintenances, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfActiveWlmLogicalIndexMaintenanceTableMaintenances);
				long num = this.TotalSearches.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter6 in list)
					{
						exPerformanceCounter6.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00007DD0 File Offset: 0x00005FD0
		internal StorePerDatabasePerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeIS Store")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalSearchesInProgress = new ExPerformanceCounter(base.CategoryName, "Total searches in progress", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesInProgress);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Search/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalSearches = new ExPerformanceCounter(base.CategoryName, "Total searches", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalSearches);
				this.TotalSearchesBelow500msec = new ExPerformanceCounter(base.CategoryName, "Total search queries completed in 0-0.5 sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesBelow500msec);
				this.TotalSearchesBetween500msecTo2sec = new ExPerformanceCounter(base.CategoryName, "Total search queries completed in 0.5-2 sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesBetween500msecTo2sec);
				this.TotalSearchesBetween2To10sec = new ExPerformanceCounter(base.CategoryName, "Total search queries completed in 2-10 sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesBetween2To10sec);
				this.TotalSearchesBetween10SecTo60Sec = new ExPerformanceCounter(base.CategoryName, "Total search queries completed in 10-60 sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesBetween10SecTo60Sec);
				this.TotalSearchesQueriesGreaterThan60Seconds = new ExPerformanceCounter(base.CategoryName, "Total search queries completed in > 60 sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSearchesQueriesGreaterThan60Seconds);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Successful search/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalSuccessfulSearches = new ExPerformanceCounter(base.CategoryName, "Total number of successful search queries", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalSuccessfulSearches);
				this.SearchQueryResultRate = new ExPerformanceCounter(base.CategoryName, "Search results/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SearchQueryResultRate);
				this.AverageSearchResultSize = new ExPerformanceCounter(base.CategoryName, "Average search results per query", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSearchResultSize);
				this.AverageSearchResultSizeBase = new ExPerformanceCounter(base.CategoryName, "Base for AverageSearchQueryResultSize", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSearchResultSizeBase);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "MultiMailbox Preview Search/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "MultiMailbox Keyword Stats Search/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "MultiMailbox Search Full Text Index Query/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.AverageMultiMailboxSearchTimeSpentInFTIndex = new ExPerformanceCounter(base.CategoryName, "Average MultiMailbox Search time spent in FullTextIndex", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchTimeSpentInFTIndex);
				this.AverageMultiMailboxSearchTimeSpentInFTIndexBase = new ExPerformanceCounter(base.CategoryName, "Base for AverageMultiMailboxSearchTimeSpentInFTIndex", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchTimeSpentInFTIndexBase);
				this.AverageMultiMailboxSearchTimeSpentInStore = new ExPerformanceCounter(base.CategoryName, "Average MultiMailbox Search time spent in Store calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchTimeSpentInStore);
				this.AverageMultiMailboxSearchTimeSpentInStoreBase = new ExPerformanceCounter(base.CategoryName, "Average Execution Time Spent in Store Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchTimeSpentInStoreBase);
				this.AverageMultiMailboxPreviewSearchLatency = new ExPerformanceCounter(base.CategoryName, "Average Search Execution Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxPreviewSearchLatency);
				this.AverageMultiMailboxPreviewSearchLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average MultiMailbox Preview Search Execution Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxPreviewSearchLatencyBase);
				this.AverageMultiMailboxKeywordStatsSearchLatency = new ExPerformanceCounter(base.CategoryName, "Average Keyword Stats Search Execution Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxKeywordStatsSearchLatency);
				this.AverageMultiMailboxKeywordStatsSearchLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Keyword Stats Execution Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxKeywordStatsSearchLatencyBase);
				this.AverageMultiMailboxPreviewSearchResultSize = new ExPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Failed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxPreviewSearchResultSize);
				this.AverageMultiMailboxPreviewSearchResultSizeBase = new ExPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Failed Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxPreviewSearchResultSizeBase);
				this.AverageMultiMailboxSearchQueryLength = new ExPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Query Length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchQueryLength);
				this.AverageMultiMailboxSearchQueryLengthBase = new ExPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Query Length Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchQueryLengthBase);
				this.AverageMultiMailboxSearchKeywordCount = new ExPerformanceCounter(base.CategoryName, "Average number of Keywords in MultiMailbox Search", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchKeywordCount);
				this.AverageMultiMailboxSearchKeywordCountBase = new ExPerformanceCounter(base.CategoryName, "Average MultiMailbox Search Keyword Count Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMultiMailboxSearchKeywordCountBase);
				this.TotalMultiMailboxPreviewSearches = new ExPerformanceCounter(base.CategoryName, "Total MultiMailbox preview searches", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalMultiMailboxPreviewSearches);
				this.TotalMultiMailboxKeywordStatsSearches = new ExPerformanceCounter(base.CategoryName, "Total MultiMailbox keyword statistics searches", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TotalMultiMailboxKeywordStatsSearches);
				this.TotalMultiMailboxPreviewSearchesTimedOut = new ExPerformanceCounter(base.CategoryName, "Total multi mailbox preview searches timed out", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalMultiMailboxPreviewSearchesTimedOut);
				this.TotalMultiMailboxSearchFTIQueryExecution = new ExPerformanceCounter(base.CategoryName, "Total multi mailbox searches Full Text Index Query Execution", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.TotalMultiMailboxSearchFTIQueryExecution);
				this.TotalMultiMailboxKeywordStatsSearchesTimedOut = new ExPerformanceCounter(base.CategoryName, "Total multi mailbox keyword statistics searches timed out", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalMultiMailboxKeywordStatsSearchesTimedOut);
				this.MultiMailboxPreviewSearchesFailed = new ExPerformanceCounter(base.CategoryName, "Total failed multi mailbox Preview Searches", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MultiMailboxPreviewSearchesFailed);
				this.MultiMailboxKeywordStatsSearchesFailed = new ExPerformanceCounter(base.CategoryName, "Total failed multi mailbox keyword statistics Searches", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MultiMailboxKeywordStatsSearchesFailed);
				this.MultiMailboxSearchesFTIndexFailed = new ExPerformanceCounter(base.CategoryName, "Total Multi Mailbox searches failed due to FullText failure", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MultiMailboxSearchesFTIndexFailed);
				this.LazyIndexesCreatedRate = new ExPerformanceCounter(base.CategoryName, "Lazy indexes created/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesCreatedRate);
				this.LazyIndexesDeletedRate = new ExPerformanceCounter(base.CategoryName, "Lazy indexes deleted/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesDeletedRate);
				this.LazyIndexesFullRefreshRate = new ExPerformanceCounter(base.CategoryName, "Lazy index full refresh/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesFullRefreshRate);
				this.LazyIndexesIncrementalRefreshRate = new ExPerformanceCounter(base.CategoryName, "Lazy index incremental refresh/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesIncrementalRefreshRate);
				this.LazyIndexesVersionInvalidationRate = new ExPerformanceCounter(base.CategoryName, "Lazy index invalidation/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesVersionInvalidationRate);
				this.LazyIndexesLocaleVersionInvalidationRate = new ExPerformanceCounter(base.CategoryName, "Lazy index invalidation due to locale version change/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesLocaleVersionInvalidationRate);
				this.LazyIndexesTotalPopulate = new ExPerformanceCounter(base.CategoryName, "Lazy index total populations.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesTotalPopulate);
				this.LazyIndexesPopulateFromIndex = new ExPerformanceCounter(base.CategoryName, "Lazy index populations from index.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesPopulateFromIndex);
				this.LazyIndexesPopulateChunked = new ExPerformanceCounter(base.CategoryName, "Lazy index chunked populations.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesPopulateChunked);
				this.LazyIndexesPopulateNonChunked = new ExPerformanceCounter(base.CategoryName, "Lazy index non-chunked populations.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesPopulateNonChunked);
				this.LazyIndexesPopulateTxNotPulsed = new ExPerformanceCounter(base.CategoryName, "Lazy index populations without transaction pulsing.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesPopulateTxNotPulsed);
				this.FoldersOpenedRate = new ExPerformanceCounter(base.CategoryName, "Folders opened/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FoldersOpenedRate);
				this.FoldersCreatedRate = new ExPerformanceCounter(base.CategoryName, "Folders created/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FoldersCreatedRate);
				this.FoldersDeletedRate = new ExPerformanceCounter(base.CategoryName, "Folders deleted/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FoldersDeletedRate);
				this.MessagesOpenedRate = new ExPerformanceCounter(base.CategoryName, "Messages opened/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesOpenedRate);
				this.MessagesCreatedRate = new ExPerformanceCounter(base.CategoryName, "Messages created/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesCreatedRate);
				this.MessagesUpdatedRate = new ExPerformanceCounter(base.CategoryName, "Messages updated/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesUpdatedRate);
				this.MessagesDeletedRate = new ExPerformanceCounter(base.CategoryName, "Messages deleted/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeletedRate);
				this.SubobjectsOpenedRate = new ExPerformanceCounter(base.CategoryName, "Subobjects opened/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsOpenedRate);
				this.SubobjectsCreatedRate = new ExPerformanceCounter(base.CategoryName, "Subobjects created/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsCreatedRate);
				this.SubobjectsDeletedRate = new ExPerformanceCounter(base.CategoryName, "Subobjects deleted/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsDeletedRate);
				this.SubobjectsCleanedRate = new ExPerformanceCounter(base.CategoryName, "Subobjects cleaned/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsCleanedRate);
				this.TopMessagesCleanedRate = new ExPerformanceCounter(base.CategoryName, "TopMessages cleaned/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TopMessagesCleanedRate);
				this.PropertyPromotionRate = new ExPerformanceCounter(base.CategoryName, "Property promotions/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PropertyPromotionRate);
				this.PropertyPromotionTasks = new ExPerformanceCounter(base.CategoryName, "Property Promotion Tasks", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PropertyPromotionTasks);
				this.PropertyPromotionMessageRate = new ExPerformanceCounter(base.CategoryName, "Property promotion messages/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PropertyPromotionMessageRate);
				this.ActiveMailboxes = new ExPerformanceCounter(base.CategoryName, "Active mailboxes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMailboxes);
				this.FolderHierarchyLoadNonRecursiveRate = new ExPerformanceCounter(base.CategoryName, "Non recursive folder hierarchy reloads/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FolderHierarchyLoadNonRecursiveRate);
				this.FolderHierarchyLoadRecursiveRate = new ExPerformanceCounter(base.CategoryName, "Recursive folder hierarchy reloads/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FolderHierarchyLoadRecursiveRate);
				this.SubobjectsInTombstone = new ExPerformanceCounter(base.CategoryName, "Sub objects in tombstone", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubobjectsInTombstone);
				this.TopMessagesInTombstone = new ExPerformanceCounter(base.CategoryName, "Top messages in tombstone", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TopMessagesInTombstone);
				this.TotalObjectsSizeInTombstone = new ExPerformanceCounter(base.CategoryName, "Total objects size in tombstone (bytes)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalObjectsSizeInTombstone);
				this.MessageDeliveriesRate = new ExPerformanceCounter(base.CategoryName, "Messages Delivered/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageDeliveriesRate);
				this.MessagesSubmittedRate = new ExPerformanceCounter(base.CategoryName, "Messages Submitted/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesSubmittedRate);
				this.MapiMessagesCreatedRate = new ExPerformanceCounter(base.CategoryName, "MAPI Messages Created/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MapiMessagesCreatedRate);
				this.MapiMessagesOpenedRate = new ExPerformanceCounter(base.CategoryName, "MAPI Messages Opened/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MapiMessagesOpenedRate);
				this.MapiMessagesModifiedRate = new ExPerformanceCounter(base.CategoryName, "MAPI Messages Modified/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MapiMessagesModifiedRate);
				this.RPCsInProgress = new ExPerformanceCounter(base.CategoryName, "RPC Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RPCsInProgress);
				this.PercentRPCsInProgress = new ExPerformanceCounter(base.CategoryName, "% RPC Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentRPCsInProgress);
				this.PercentRPCsInProgressBase = new ExPerformanceCounter(base.CategoryName, "% RPC Requests Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentRPCsInProgressBase);
				this.RateOfRPCs = new ExPerformanceCounter(base.CategoryName, "RPC Packets/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfRPCs);
				this.RateOfROPs = new ExPerformanceCounter(base.CategoryName, "RPC Operations/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfROPs);
				this.AverageRPCLatency = new ExPerformanceCounter(base.CategoryName, "RPC Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageRPCLatency);
				this.AverageRPCLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Time spent in an RPC Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageRPCLatencyBase);
				this.ContextHandlePools = new ExPerformanceCounter(base.CategoryName, "RPC Pool: Pools", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ContextHandlePools);
				this.ContextHandlePoolHandles = new ExPerformanceCounter(base.CategoryName, "RPC Pool: Context Handles", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ContextHandlePoolHandles);
				this.ContextHandlePoolParkedCalls = new ExPerformanceCounter(base.CategoryName, "RPC Pool: Parked Async Notification Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ContextHandlePoolParkedCalls);
				this.MailboxMaintenances = new ExPerformanceCounter(base.CategoryName, "Mailbox Level Maintenance Items", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxMaintenances);
				this.MailboxesWithMaintenances = new ExPerformanceCounter(base.CategoryName, "Mailboxes With Maintenance Items", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxesWithMaintenances);
				this.RateOfMailboxMaintenances = new ExPerformanceCounter(base.CategoryName, "Mailbox Level Maintenances/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxMaintenances);
				this.RateOfDatabaseMaintenances = new ExPerformanceCounter(base.CategoryName, "Database Level Maintenances/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseMaintenances);
				this.ProcessId = new ExPerformanceCounter(base.CategoryName, "Process ID", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessId);
				this.LostDiagnosticEntries = new ExPerformanceCounter(base.CategoryName, "Lost Diagnostic Entries", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LostDiagnosticEntries);
				this.QuarantinedMailboxCount = new ExPerformanceCounter(base.CategoryName, "Quarantined Mailbox Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QuarantinedMailboxCount);
				this.ScheduledISIntegCorruptionDetectedCount = new ExPerformanceCounter(base.CategoryName, "Scheduled ISInteg Detected Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ScheduledISIntegCorruptionDetectedCount);
				this.ScheduledISIntegCorruptionFixedCount = new ExPerformanceCounter(base.CategoryName, "Scheduled ISInteg Fixed Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ScheduledISIntegCorruptionFixedCount);
				this.ScheduledISIntegMailboxRate = new ExPerformanceCounter(base.CategoryName, "Scheduled ISInteg/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ScheduledISIntegMailboxRate);
				this.ISIntegStorePendingJobs = new ExPerformanceCounter(base.CategoryName, "Integrity Check Pending Jobs", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ISIntegStorePendingJobs);
				this.ISIntegStoreTotalJobs = new ExPerformanceCounter(base.CategoryName, "Integrity Check Total Jobs", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ISIntegStoreTotalJobs);
				this.ISIntegStoreFailedJobs = new ExPerformanceCounter(base.CategoryName, "Integrity Check Failed Jobs", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ISIntegStoreFailedJobs);
				this.NumberOfActiveBackgroundTasks = new ExPerformanceCounter(base.CategoryName, "Number of active background tasks", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfActiveBackgroundTasks);
				this.AddressInfoSize = new ExPerformanceCounter(base.CategoryName, "Size of Address Info cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AddressInfoSize);
				this.RateOfAddressInfoLookups = new ExPerformanceCounter(base.CategoryName, "Cache lookups in the AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoLookups);
				this.RateOfAddressInfoMisses = new ExPerformanceCounter(base.CategoryName, "Cache misses in the AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoMisses);
				this.RateOfAddressInfoHits = new ExPerformanceCounter(base.CategoryName, "Cache hits in the AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoHits);
				this.RateOfAddressInfoInserts = new ExPerformanceCounter(base.CategoryName, "Cache inserts in the AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoInserts);
				this.RateOfAddressInfoDeletes = new ExPerformanceCounter(base.CategoryName, "Cache deletes in the AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfAddressInfoDeletes);
				this.SizeOfAddressInfoExpirationQueue = new ExPerformanceCounter(base.CategoryName, "Size of the expiration queue for the AddressInfo cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfAddressInfoExpirationQueue);
				this.ForeignAddressInfoSize = new ExPerformanceCounter(base.CategoryName, "Size of Foreign Address Info cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ForeignAddressInfoSize);
				this.RateOfForeignAddressInfoLookups = new ExPerformanceCounter(base.CategoryName, "Cache lookups in the Foreign AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoLookups);
				this.RateOfForeignAddressInfoMisses = new ExPerformanceCounter(base.CategoryName, "Cache misses in the Foreign AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoMisses);
				this.RateOfForeignAddressInfoHits = new ExPerformanceCounter(base.CategoryName, "Cache hits in the Foreign AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoHits);
				this.RateOfForeignAddressInfoInserts = new ExPerformanceCounter(base.CategoryName, "Cache inserts in the Foreign AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoInserts);
				this.RateOfForeignAddressInfoDeletes = new ExPerformanceCounter(base.CategoryName, "Cache deletes in the Foreign AddressInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignAddressInfoDeletes);
				this.SizeOfForeignAddressInfoExpirationQueue = new ExPerformanceCounter(base.CategoryName, "Size of the expiration queue for the Foreign AddressInfo cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfForeignAddressInfoExpirationQueue);
				this.MailboxInfoSize = new ExPerformanceCounter(base.CategoryName, "Size of Mailbox Info cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxInfoSize);
				this.RateOfMailboxInfoLookups = new ExPerformanceCounter(base.CategoryName, "Cache lookups in the MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoLookups);
				this.RateOfMailboxInfoMisses = new ExPerformanceCounter(base.CategoryName, "Cache misses in the MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoMisses);
				this.RateOfMailboxInfoHits = new ExPerformanceCounter(base.CategoryName, "Cache hits in the MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoHits);
				this.RateOfMailboxInfoInserts = new ExPerformanceCounter(base.CategoryName, "Cache inserts in the MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoInserts);
				this.RateOfMailboxInfoDeletes = new ExPerformanceCounter(base.CategoryName, "Cache deletes in the MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfMailboxInfoDeletes);
				this.SizeOfMailboxInfoExpirationQueue = new ExPerformanceCounter(base.CategoryName, "Size of the expiration queue for the MailboxInfo cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfMailboxInfoExpirationQueue);
				this.ForeignMailboxInfoSize = new ExPerformanceCounter(base.CategoryName, "Size of Foreign Mailbox Info cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ForeignMailboxInfoSize);
				this.RateOfForeignMailboxInfoLookups = new ExPerformanceCounter(base.CategoryName, "Cache lookups in the Foreign MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoLookups);
				this.RateOfForeignMailboxInfoMisses = new ExPerformanceCounter(base.CategoryName, "Cache misses in the Foreign MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoMisses);
				this.RateOfForeignMailboxInfoHits = new ExPerformanceCounter(base.CategoryName, "Cache hits in the Foreign MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoHits);
				this.RateOfForeignMailboxInfoInserts = new ExPerformanceCounter(base.CategoryName, "Cache inserts in the Foreign MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoInserts);
				this.RateOfForeignMailboxInfoDeletes = new ExPerformanceCounter(base.CategoryName, "Cache deletes in the Foreign MailboxInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfForeignMailboxInfoDeletes);
				this.SizeOfForeignMailboxInfoExpirationQueue = new ExPerformanceCounter(base.CategoryName, "Size of the expiration queue for the Foreign MailboxInfo cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfForeignMailboxInfoExpirationQueue);
				this.DatabaseInfoSize = new ExPerformanceCounter(base.CategoryName, "Size of Database Info cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DatabaseInfoSize);
				this.RateOfDatabaseInfoLookups = new ExPerformanceCounter(base.CategoryName, "Cache lookups in the DatabaseInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoLookups);
				this.RateOfDatabaseInfoMisses = new ExPerformanceCounter(base.CategoryName, "Cache misses in the DatabaseInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoMisses);
				this.RateOfDatabaseInfoHits = new ExPerformanceCounter(base.CategoryName, "Cache hits in the DatabaseInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoHits);
				this.RateOfDatabaseInfoInserts = new ExPerformanceCounter(base.CategoryName, "Cache inserts in the DatabaseInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoInserts);
				this.RateOfDatabaseInfoDeletes = new ExPerformanceCounter(base.CategoryName, "Cache deletes in the DatabaseInfo cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDatabaseInfoDeletes);
				this.SizeOfDatabaseInfoExpirationQueue = new ExPerformanceCounter(base.CategoryName, "Size of the expiration queue for the DatabaseInfo cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfDatabaseInfoExpirationQueue);
				this.OrgContainerSize = new ExPerformanceCounter(base.CategoryName, "Size of Organization Container cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrgContainerSize);
				this.RateOfOrgContainerLookups = new ExPerformanceCounter(base.CategoryName, "Cache lookups in the Organization Container cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerLookups);
				this.RateOfOrgContainerMisses = new ExPerformanceCounter(base.CategoryName, "Cache misses in the Organization Container cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerMisses);
				this.RateOfOrgContainerHits = new ExPerformanceCounter(base.CategoryName, "Cache hits in the Organization Container cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerHits);
				this.RateOfOrgContainerInserts = new ExPerformanceCounter(base.CategoryName, "Cache inserts in the Organization Container cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerInserts);
				this.RateOfOrgContainerDeletes = new ExPerformanceCounter(base.CategoryName, "Cache deletes in the Organization Container cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfOrgContainerDeletes);
				this.SizeOfOrgContainerExpirationQueue = new ExPerformanceCounter(base.CategoryName, "Size of the expiration queue for the Organization Container cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfOrgContainerExpirationQueue);
				this.DistributionListMembershipSize = new ExPerformanceCounter(base.CategoryName, "Size of DistributionListMembership cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DistributionListMembershipSize);
				this.RateOfDistributionListMembershipLookups = new ExPerformanceCounter(base.CategoryName, "Cache lookups in the DistributionListMembership cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipLookups);
				this.RateOfDistributionListMembershipMisses = new ExPerformanceCounter(base.CategoryName, "Cache misses in the DistributionListMembership cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipMisses);
				this.RateOfDistributionListMembershipHits = new ExPerformanceCounter(base.CategoryName, "Cache hits in the DistributionListMembership cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipHits);
				this.RateOfDistributionListMembershipInserts = new ExPerformanceCounter(base.CategoryName, "Cache inserts in the DistributionListMembership cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipInserts);
				this.RateOfDistributionListMembershipDeletes = new ExPerformanceCounter(base.CategoryName, "Cache deletes in the DistributionListMembership cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfDistributionListMembershipDeletes);
				this.SizeOfDistributionListMembershipExpirationQueue = new ExPerformanceCounter(base.CategoryName, "Size of the expiration queue for the DistributionListMembership cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfDistributionListMembershipExpirationQueue);
				this.LogicalIndexSize = new ExPerformanceCounter(base.CategoryName, "Size of LogicalIndex cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LogicalIndexSize);
				this.RateOfLogicalIndexLookups = new ExPerformanceCounter(base.CategoryName, "Cache lookups in the LogicalIndex cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexLookups);
				this.RateOfLogicalIndexMisses = new ExPerformanceCounter(base.CategoryName, "Cache misses in the LogicalIndex cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexMisses);
				this.RateOfLogicalIndexHits = new ExPerformanceCounter(base.CategoryName, "Cache hits in the LogicalIndex cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexHits);
				this.RateOfLogicalIndexInserts = new ExPerformanceCounter(base.CategoryName, "Cache inserts in the LogicalIndex cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexInserts);
				this.RateOfLogicalIndexDeletes = new ExPerformanceCounter(base.CategoryName, "Cache deletes in the LogicalIndex cache/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RateOfLogicalIndexDeletes);
				this.SizeOfLogicalIndexExpirationQueue = new ExPerformanceCounter(base.CategoryName, "Size of the expiration queue for the LogicalIndex cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SizeOfLogicalIndexExpirationQueue);
				this.NumberOfScheduledLogicalIndexMaintenanceTasks = new ExPerformanceCounter(base.CategoryName, "Number of scheduled LogicalIndex maintenance tasks", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfScheduledLogicalIndexMaintenanceTasks);
				this.NumberOfProcessingLogicalIndexMaintenanceTasks = new ExPerformanceCounter(base.CategoryName, "Number of processing LogicalIndex maintenance tasks", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfProcessingLogicalIndexMaintenanceTasks);
				this.NumberOfMailboxesMarkedForWlmLogicalIndexMaintenanceTableMaintenance = new ExPerformanceCounter(base.CategoryName, "Number of mailboxes marked for WLM LogicalIndex maintenance table maintenance", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfMailboxesMarkedForWlmLogicalIndexMaintenanceTableMaintenance);
				this.NumberOfActiveWlmLogicalIndexMaintenanceTableMaintenances = new ExPerformanceCounter(base.CategoryName, "Number of active WLM LogicalIndex maintenance table maintenances", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfActiveWlmLogicalIndexMaintenanceTableMaintenances);
				long num = this.TotalSearches.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter6 in list)
					{
						exPerformanceCounter6.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000098A8 File Offset: 0x00007AA8
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x040000B5 RID: 181
		public readonly ExPerformanceCounter TotalSearches;

		// Token: 0x040000B6 RID: 182
		public readonly ExPerformanceCounter TotalSearchesInProgress;

		// Token: 0x040000B7 RID: 183
		public readonly ExPerformanceCounter TotalSearchesBelow500msec;

		// Token: 0x040000B8 RID: 184
		public readonly ExPerformanceCounter TotalSearchesBetween500msecTo2sec;

		// Token: 0x040000B9 RID: 185
		public readonly ExPerformanceCounter TotalSearchesBetween2To10sec;

		// Token: 0x040000BA RID: 186
		public readonly ExPerformanceCounter TotalSearchesBetween10SecTo60Sec;

		// Token: 0x040000BB RID: 187
		public readonly ExPerformanceCounter TotalSearchesQueriesGreaterThan60Seconds;

		// Token: 0x040000BC RID: 188
		public readonly ExPerformanceCounter TotalSuccessfulSearches;

		// Token: 0x040000BD RID: 189
		public readonly ExPerformanceCounter SearchQueryResultRate;

		// Token: 0x040000BE RID: 190
		public readonly ExPerformanceCounter AverageSearchResultSize;

		// Token: 0x040000BF RID: 191
		public readonly ExPerformanceCounter AverageSearchResultSizeBase;

		// Token: 0x040000C0 RID: 192
		public readonly ExPerformanceCounter AverageMultiMailboxSearchTimeSpentInFTIndex;

		// Token: 0x040000C1 RID: 193
		public readonly ExPerformanceCounter AverageMultiMailboxSearchTimeSpentInFTIndexBase;

		// Token: 0x040000C2 RID: 194
		public readonly ExPerformanceCounter AverageMultiMailboxSearchTimeSpentInStore;

		// Token: 0x040000C3 RID: 195
		public readonly ExPerformanceCounter AverageMultiMailboxSearchTimeSpentInStoreBase;

		// Token: 0x040000C4 RID: 196
		public readonly ExPerformanceCounter AverageMultiMailboxPreviewSearchLatency;

		// Token: 0x040000C5 RID: 197
		public readonly ExPerformanceCounter AverageMultiMailboxPreviewSearchLatencyBase;

		// Token: 0x040000C6 RID: 198
		public readonly ExPerformanceCounter AverageMultiMailboxKeywordStatsSearchLatency;

		// Token: 0x040000C7 RID: 199
		public readonly ExPerformanceCounter AverageMultiMailboxKeywordStatsSearchLatencyBase;

		// Token: 0x040000C8 RID: 200
		public readonly ExPerformanceCounter AverageMultiMailboxPreviewSearchResultSize;

		// Token: 0x040000C9 RID: 201
		public readonly ExPerformanceCounter AverageMultiMailboxPreviewSearchResultSizeBase;

		// Token: 0x040000CA RID: 202
		public readonly ExPerformanceCounter AverageMultiMailboxSearchQueryLength;

		// Token: 0x040000CB RID: 203
		public readonly ExPerformanceCounter AverageMultiMailboxSearchQueryLengthBase;

		// Token: 0x040000CC RID: 204
		public readonly ExPerformanceCounter AverageMultiMailboxSearchKeywordCount;

		// Token: 0x040000CD RID: 205
		public readonly ExPerformanceCounter AverageMultiMailboxSearchKeywordCountBase;

		// Token: 0x040000CE RID: 206
		public readonly ExPerformanceCounter TotalMultiMailboxPreviewSearches;

		// Token: 0x040000CF RID: 207
		public readonly ExPerformanceCounter TotalMultiMailboxKeywordStatsSearches;

		// Token: 0x040000D0 RID: 208
		public readonly ExPerformanceCounter TotalMultiMailboxPreviewSearchesTimedOut;

		// Token: 0x040000D1 RID: 209
		public readonly ExPerformanceCounter TotalMultiMailboxSearchFTIQueryExecution;

		// Token: 0x040000D2 RID: 210
		public readonly ExPerformanceCounter TotalMultiMailboxKeywordStatsSearchesTimedOut;

		// Token: 0x040000D3 RID: 211
		public readonly ExPerformanceCounter MultiMailboxPreviewSearchesFailed;

		// Token: 0x040000D4 RID: 212
		public readonly ExPerformanceCounter MultiMailboxKeywordStatsSearchesFailed;

		// Token: 0x040000D5 RID: 213
		public readonly ExPerformanceCounter MultiMailboxSearchesFTIndexFailed;

		// Token: 0x040000D6 RID: 214
		public readonly ExPerformanceCounter LazyIndexesCreatedRate;

		// Token: 0x040000D7 RID: 215
		public readonly ExPerformanceCounter LazyIndexesDeletedRate;

		// Token: 0x040000D8 RID: 216
		public readonly ExPerformanceCounter LazyIndexesFullRefreshRate;

		// Token: 0x040000D9 RID: 217
		public readonly ExPerformanceCounter LazyIndexesIncrementalRefreshRate;

		// Token: 0x040000DA RID: 218
		public readonly ExPerformanceCounter LazyIndexesVersionInvalidationRate;

		// Token: 0x040000DB RID: 219
		public readonly ExPerformanceCounter LazyIndexesLocaleVersionInvalidationRate;

		// Token: 0x040000DC RID: 220
		public readonly ExPerformanceCounter LazyIndexesTotalPopulate;

		// Token: 0x040000DD RID: 221
		public readonly ExPerformanceCounter LazyIndexesPopulateFromIndex;

		// Token: 0x040000DE RID: 222
		public readonly ExPerformanceCounter LazyIndexesPopulateChunked;

		// Token: 0x040000DF RID: 223
		public readonly ExPerformanceCounter LazyIndexesPopulateNonChunked;

		// Token: 0x040000E0 RID: 224
		public readonly ExPerformanceCounter LazyIndexesPopulateTxNotPulsed;

		// Token: 0x040000E1 RID: 225
		public readonly ExPerformanceCounter FoldersOpenedRate;

		// Token: 0x040000E2 RID: 226
		public readonly ExPerformanceCounter FoldersCreatedRate;

		// Token: 0x040000E3 RID: 227
		public readonly ExPerformanceCounter FoldersDeletedRate;

		// Token: 0x040000E4 RID: 228
		public readonly ExPerformanceCounter MessagesOpenedRate;

		// Token: 0x040000E5 RID: 229
		public readonly ExPerformanceCounter MessagesCreatedRate;

		// Token: 0x040000E6 RID: 230
		public readonly ExPerformanceCounter MessagesUpdatedRate;

		// Token: 0x040000E7 RID: 231
		public readonly ExPerformanceCounter MessagesDeletedRate;

		// Token: 0x040000E8 RID: 232
		public readonly ExPerformanceCounter SubobjectsOpenedRate;

		// Token: 0x040000E9 RID: 233
		public readonly ExPerformanceCounter SubobjectsCreatedRate;

		// Token: 0x040000EA RID: 234
		public readonly ExPerformanceCounter SubobjectsDeletedRate;

		// Token: 0x040000EB RID: 235
		public readonly ExPerformanceCounter SubobjectsCleanedRate;

		// Token: 0x040000EC RID: 236
		public readonly ExPerformanceCounter TopMessagesCleanedRate;

		// Token: 0x040000ED RID: 237
		public readonly ExPerformanceCounter PropertyPromotionRate;

		// Token: 0x040000EE RID: 238
		public readonly ExPerformanceCounter PropertyPromotionTasks;

		// Token: 0x040000EF RID: 239
		public readonly ExPerformanceCounter PropertyPromotionMessageRate;

		// Token: 0x040000F0 RID: 240
		public readonly ExPerformanceCounter ActiveMailboxes;

		// Token: 0x040000F1 RID: 241
		public readonly ExPerformanceCounter FolderHierarchyLoadNonRecursiveRate;

		// Token: 0x040000F2 RID: 242
		public readonly ExPerformanceCounter FolderHierarchyLoadRecursiveRate;

		// Token: 0x040000F3 RID: 243
		public readonly ExPerformanceCounter SubobjectsInTombstone;

		// Token: 0x040000F4 RID: 244
		public readonly ExPerformanceCounter TopMessagesInTombstone;

		// Token: 0x040000F5 RID: 245
		public readonly ExPerformanceCounter TotalObjectsSizeInTombstone;

		// Token: 0x040000F6 RID: 246
		public readonly ExPerformanceCounter MessageDeliveriesRate;

		// Token: 0x040000F7 RID: 247
		public readonly ExPerformanceCounter MessagesSubmittedRate;

		// Token: 0x040000F8 RID: 248
		public readonly ExPerformanceCounter MapiMessagesCreatedRate;

		// Token: 0x040000F9 RID: 249
		public readonly ExPerformanceCounter MapiMessagesOpenedRate;

		// Token: 0x040000FA RID: 250
		public readonly ExPerformanceCounter MapiMessagesModifiedRate;

		// Token: 0x040000FB RID: 251
		public readonly ExPerformanceCounter RPCsInProgress;

		// Token: 0x040000FC RID: 252
		public readonly ExPerformanceCounter PercentRPCsInProgress;

		// Token: 0x040000FD RID: 253
		public readonly ExPerformanceCounter PercentRPCsInProgressBase;

		// Token: 0x040000FE RID: 254
		public readonly ExPerformanceCounter RateOfRPCs;

		// Token: 0x040000FF RID: 255
		public readonly ExPerformanceCounter RateOfROPs;

		// Token: 0x04000100 RID: 256
		public readonly ExPerformanceCounter AverageRPCLatency;

		// Token: 0x04000101 RID: 257
		public readonly ExPerformanceCounter AverageRPCLatencyBase;

		// Token: 0x04000102 RID: 258
		public readonly ExPerformanceCounter ContextHandlePools;

		// Token: 0x04000103 RID: 259
		public readonly ExPerformanceCounter ContextHandlePoolHandles;

		// Token: 0x04000104 RID: 260
		public readonly ExPerformanceCounter ContextHandlePoolParkedCalls;

		// Token: 0x04000105 RID: 261
		public readonly ExPerformanceCounter MailboxMaintenances;

		// Token: 0x04000106 RID: 262
		public readonly ExPerformanceCounter MailboxesWithMaintenances;

		// Token: 0x04000107 RID: 263
		public readonly ExPerformanceCounter RateOfMailboxMaintenances;

		// Token: 0x04000108 RID: 264
		public readonly ExPerformanceCounter RateOfDatabaseMaintenances;

		// Token: 0x04000109 RID: 265
		public readonly ExPerformanceCounter ProcessId;

		// Token: 0x0400010A RID: 266
		public readonly ExPerformanceCounter LostDiagnosticEntries;

		// Token: 0x0400010B RID: 267
		public readonly ExPerformanceCounter QuarantinedMailboxCount;

		// Token: 0x0400010C RID: 268
		public readonly ExPerformanceCounter ScheduledISIntegCorruptionDetectedCount;

		// Token: 0x0400010D RID: 269
		public readonly ExPerformanceCounter ScheduledISIntegCorruptionFixedCount;

		// Token: 0x0400010E RID: 270
		public readonly ExPerformanceCounter ScheduledISIntegMailboxRate;

		// Token: 0x0400010F RID: 271
		public readonly ExPerformanceCounter ISIntegStorePendingJobs;

		// Token: 0x04000110 RID: 272
		public readonly ExPerformanceCounter ISIntegStoreTotalJobs;

		// Token: 0x04000111 RID: 273
		public readonly ExPerformanceCounter ISIntegStoreFailedJobs;

		// Token: 0x04000112 RID: 274
		public readonly ExPerformanceCounter NumberOfActiveBackgroundTasks;

		// Token: 0x04000113 RID: 275
		public readonly ExPerformanceCounter AddressInfoSize;

		// Token: 0x04000114 RID: 276
		public readonly ExPerformanceCounter RateOfAddressInfoLookups;

		// Token: 0x04000115 RID: 277
		public readonly ExPerformanceCounter RateOfAddressInfoMisses;

		// Token: 0x04000116 RID: 278
		public readonly ExPerformanceCounter RateOfAddressInfoHits;

		// Token: 0x04000117 RID: 279
		public readonly ExPerformanceCounter RateOfAddressInfoInserts;

		// Token: 0x04000118 RID: 280
		public readonly ExPerformanceCounter RateOfAddressInfoDeletes;

		// Token: 0x04000119 RID: 281
		public readonly ExPerformanceCounter SizeOfAddressInfoExpirationQueue;

		// Token: 0x0400011A RID: 282
		public readonly ExPerformanceCounter ForeignAddressInfoSize;

		// Token: 0x0400011B RID: 283
		public readonly ExPerformanceCounter RateOfForeignAddressInfoLookups;

		// Token: 0x0400011C RID: 284
		public readonly ExPerformanceCounter RateOfForeignAddressInfoMisses;

		// Token: 0x0400011D RID: 285
		public readonly ExPerformanceCounter RateOfForeignAddressInfoHits;

		// Token: 0x0400011E RID: 286
		public readonly ExPerformanceCounter RateOfForeignAddressInfoInserts;

		// Token: 0x0400011F RID: 287
		public readonly ExPerformanceCounter RateOfForeignAddressInfoDeletes;

		// Token: 0x04000120 RID: 288
		public readonly ExPerformanceCounter SizeOfForeignAddressInfoExpirationQueue;

		// Token: 0x04000121 RID: 289
		public readonly ExPerformanceCounter MailboxInfoSize;

		// Token: 0x04000122 RID: 290
		public readonly ExPerformanceCounter RateOfMailboxInfoLookups;

		// Token: 0x04000123 RID: 291
		public readonly ExPerformanceCounter RateOfMailboxInfoMisses;

		// Token: 0x04000124 RID: 292
		public readonly ExPerformanceCounter RateOfMailboxInfoHits;

		// Token: 0x04000125 RID: 293
		public readonly ExPerformanceCounter RateOfMailboxInfoInserts;

		// Token: 0x04000126 RID: 294
		public readonly ExPerformanceCounter RateOfMailboxInfoDeletes;

		// Token: 0x04000127 RID: 295
		public readonly ExPerformanceCounter SizeOfMailboxInfoExpirationQueue;

		// Token: 0x04000128 RID: 296
		public readonly ExPerformanceCounter ForeignMailboxInfoSize;

		// Token: 0x04000129 RID: 297
		public readonly ExPerformanceCounter RateOfForeignMailboxInfoLookups;

		// Token: 0x0400012A RID: 298
		public readonly ExPerformanceCounter RateOfForeignMailboxInfoMisses;

		// Token: 0x0400012B RID: 299
		public readonly ExPerformanceCounter RateOfForeignMailboxInfoHits;

		// Token: 0x0400012C RID: 300
		public readonly ExPerformanceCounter RateOfForeignMailboxInfoInserts;

		// Token: 0x0400012D RID: 301
		public readonly ExPerformanceCounter RateOfForeignMailboxInfoDeletes;

		// Token: 0x0400012E RID: 302
		public readonly ExPerformanceCounter SizeOfForeignMailboxInfoExpirationQueue;

		// Token: 0x0400012F RID: 303
		public readonly ExPerformanceCounter DatabaseInfoSize;

		// Token: 0x04000130 RID: 304
		public readonly ExPerformanceCounter RateOfDatabaseInfoLookups;

		// Token: 0x04000131 RID: 305
		public readonly ExPerformanceCounter RateOfDatabaseInfoMisses;

		// Token: 0x04000132 RID: 306
		public readonly ExPerformanceCounter RateOfDatabaseInfoHits;

		// Token: 0x04000133 RID: 307
		public readonly ExPerformanceCounter RateOfDatabaseInfoInserts;

		// Token: 0x04000134 RID: 308
		public readonly ExPerformanceCounter RateOfDatabaseInfoDeletes;

		// Token: 0x04000135 RID: 309
		public readonly ExPerformanceCounter SizeOfDatabaseInfoExpirationQueue;

		// Token: 0x04000136 RID: 310
		public readonly ExPerformanceCounter OrgContainerSize;

		// Token: 0x04000137 RID: 311
		public readonly ExPerformanceCounter RateOfOrgContainerLookups;

		// Token: 0x04000138 RID: 312
		public readonly ExPerformanceCounter RateOfOrgContainerMisses;

		// Token: 0x04000139 RID: 313
		public readonly ExPerformanceCounter RateOfOrgContainerHits;

		// Token: 0x0400013A RID: 314
		public readonly ExPerformanceCounter RateOfOrgContainerInserts;

		// Token: 0x0400013B RID: 315
		public readonly ExPerformanceCounter RateOfOrgContainerDeletes;

		// Token: 0x0400013C RID: 316
		public readonly ExPerformanceCounter SizeOfOrgContainerExpirationQueue;

		// Token: 0x0400013D RID: 317
		public readonly ExPerformanceCounter DistributionListMembershipSize;

		// Token: 0x0400013E RID: 318
		public readonly ExPerformanceCounter RateOfDistributionListMembershipLookups;

		// Token: 0x0400013F RID: 319
		public readonly ExPerformanceCounter RateOfDistributionListMembershipMisses;

		// Token: 0x04000140 RID: 320
		public readonly ExPerformanceCounter RateOfDistributionListMembershipHits;

		// Token: 0x04000141 RID: 321
		public readonly ExPerformanceCounter RateOfDistributionListMembershipInserts;

		// Token: 0x04000142 RID: 322
		public readonly ExPerformanceCounter RateOfDistributionListMembershipDeletes;

		// Token: 0x04000143 RID: 323
		public readonly ExPerformanceCounter SizeOfDistributionListMembershipExpirationQueue;

		// Token: 0x04000144 RID: 324
		public readonly ExPerformanceCounter LogicalIndexSize;

		// Token: 0x04000145 RID: 325
		public readonly ExPerformanceCounter RateOfLogicalIndexLookups;

		// Token: 0x04000146 RID: 326
		public readonly ExPerformanceCounter RateOfLogicalIndexMisses;

		// Token: 0x04000147 RID: 327
		public readonly ExPerformanceCounter RateOfLogicalIndexHits;

		// Token: 0x04000148 RID: 328
		public readonly ExPerformanceCounter RateOfLogicalIndexInserts;

		// Token: 0x04000149 RID: 329
		public readonly ExPerformanceCounter RateOfLogicalIndexDeletes;

		// Token: 0x0400014A RID: 330
		public readonly ExPerformanceCounter SizeOfLogicalIndexExpirationQueue;

		// Token: 0x0400014B RID: 331
		public readonly ExPerformanceCounter NumberOfScheduledLogicalIndexMaintenanceTasks;

		// Token: 0x0400014C RID: 332
		public readonly ExPerformanceCounter NumberOfProcessingLogicalIndexMaintenanceTasks;

		// Token: 0x0400014D RID: 333
		public readonly ExPerformanceCounter NumberOfMailboxesMarkedForWlmLogicalIndexMaintenanceTableMaintenance;

		// Token: 0x0400014E RID: 334
		public readonly ExPerformanceCounter NumberOfActiveWlmLogicalIndexMaintenanceTableMaintenances;
	}
}
