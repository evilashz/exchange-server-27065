using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200026F RID: 623
	public class NoGrouping : BaseGroupByType
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x0004E284 File Offset: 0x0004C484
		internal override BasePageResult IssueQuery(QueryFilter query, Folder folder, SortBy[] sortBy, BasePagingType paging, ItemQueryTraversal traversal, PropertyDefinition[] propsToFetch, RequestDetailsLogger logger)
		{
			CalendarFolder calendarFolder = folder as CalendarFolder;
			CalendarPageView calendarPageView = paging as CalendarPageView;
			if (calendarPageView != null)
			{
				calendarPageView.Validate(calendarFolder);
				CalendarViewLatencyInformation calendarViewLatencyInformation = new CalendarViewLatencyInformation();
				object[][] calendarView = calendarFolder.GetCalendarView(calendarPageView.StartDateEx, calendarPageView.EndDateEx, calendarViewLatencyInformation, propsToFetch);
				if (logger != null)
				{
					logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsViewUsed, calendarViewLatencyInformation.IsNewView);
					logger.Set(FindConversationAndItemMetadata.CalendarViewTime, calendarViewLatencyInformation.ViewTime);
					logger.Set(FindConversationAndItemMetadata.CalendarSingleItemsTotalTime, calendarViewLatencyInformation.SingleItemTotalTime);
					logger.Set(FindConversationAndItemMetadata.CalendarSingleItemsCount, calendarViewLatencyInformation.SingleItemQueryCount);
					logger.Set(FindConversationAndItemMetadata.CalendarSingleItemsGetRowsTime, calendarViewLatencyInformation.SingleItemGetRowsTime);
					logger.Set(FindConversationAndItemMetadata.CalendarSingleItemsQueryRowsTime, calendarViewLatencyInformation.SingleItemQueryTime);
					logger.Set(FindConversationAndItemMetadata.CalendarSingleItemsSeekToConditionTime, calendarViewLatencyInformation.SingleQuerySeekToTime);
					logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsCount, calendarViewLatencyInformation.RecurringItemQueryCount);
					logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsTotalTime, calendarViewLatencyInformation.RecurringItemTotalTime);
					logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsExpansionTime, calendarViewLatencyInformation.RecurringExpansionTime);
					logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsGetRowsTime, calendarViewLatencyInformation.RecurringItemGetRowsTime);
					logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsQueryTime, calendarViewLatencyInformation.RecurringItemQueryTime);
					logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsNoInstancesInWindow, calendarViewLatencyInformation.RecurringItemsNoInstancesInWindow);
					if (calendarViewLatencyInformation.MaxRecurringItemLatencyInformation != null)
					{
						logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsMaxSubject, calendarViewLatencyInformation.MaxRecurringItemLatencyInformation.Subject);
						logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsMaxParseTime, calendarViewLatencyInformation.MaxRecurringItemLatencyInformation.BlobParseTime);
						logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsMaxBlobStreamTime, calendarViewLatencyInformation.MaxRecurringItemLatencyInformation.BlobStreamTime);
						logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsMaxExpansionTime, calendarViewLatencyInformation.MaxRecurringItemLatencyInformation.BlobExpansionTime);
						logger.Set(FindConversationAndItemMetadata.CalendarRecurringItemsMaxAddRowsTime, calendarViewLatencyInformation.MaxRecurringItemLatencyInformation.AddRowsForInstancesTime);
					}
				}
				return calendarPageView.ApplyPostQueryPaging(calendarView);
			}
			BasePageResult result;
			using (QueryResult queryResult = folder.ItemQuery((ItemQueryType)traversal, query, sortBy, propsToFetch))
			{
				result = BasePagingType.ApplyPostQueryPaging(queryResult, paging);
			}
			return result;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004E504 File Offset: 0x0004C704
		internal override PropertyDefinition[] GetAdditionalFetchProperties()
		{
			return null;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0004E507 File Offset: 0x0004C707
		internal override QueryType QueryType
		{
			get
			{
				return QueryType.Items;
			}
		}
	}
}
