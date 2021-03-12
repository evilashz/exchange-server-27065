using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000DC RID: 220
	internal static class ReminderLoader
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x0003ED90 File Offset: 0x0003CF90
		public static IList<CalendarInfo> Load(ExDateTime creationRequestTime, ExTimeZone timeZoneAdjustment, MailboxSession session, StoreObjectId calFldrId, StorageWorkingHours workingHours, ExDateTime actualizationTime, ExDateTime endTime)
		{
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Reminders);
			if (defaultFolderId == null)
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<IExchangePrincipal, string>((long)typeof(ReminderLoader).GetHashCode(), "cannot open reminder folder for user {0}, Time {1}", session.MailboxOwner, ExDateTime.GetNow(timeZoneAdjustment).ToLongTimeString());
				return new CalendarInfo[0];
			}
			List<CalendarInfo> list = new List<CalendarInfo>();
			StoreObjectId storeObjectId = calFldrId;
			if (calFldrId.IsFolderId && StoreObjectType.Folder != calFldrId.ObjectType)
			{
				storeObjectId = calFldrId.Clone();
				storeObjectId.UpdateItemType(StoreObjectType.Folder);
			}
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.ReminderNextTime, actualizationTime),
				new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ReminderNextTime, endTime),
				new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.ReminderIsSet, true),
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ParentItemId, storeObjectId)
			});
			SortBy[] sortColumns = new SortBy[]
			{
				new SortBy(ItemSchema.ReminderNextTime, SortOrder.Ascending)
			};
			new Interval<ExDateTime>(actualizationTime, false, endTime, true);
			using (SearchFolder searchFolder = SearchFolder.Bind(session, defaultFolderId))
			{
				using (QueryResult queryResult = searchFolder.ItemQuery(ItemQueryType.None, queryFilter, sortColumns, CalendarInfo.InterestedProperties))
				{
					ExDateTime exDateTime = ExDateTime.MinValue;
					foreach (object[] propVals in queryResult.GetRows(100))
					{
						CalendarInfo calendarInfo = CalendarInfo.FromInterestedProperties(creationRequestTime, timeZoneAdjustment, session, true, propVals);
						if (CalendarItemType.RecurringMaster == calendarInfo.CalendarItemType)
						{
							using (CalendarItem calendarItem = CalendarItem.Bind(session, calendarInfo.CalendarItemIdentity))
							{
								using (CalendarItemOccurrence calendarItemOccurrence = (CalendarItemOccurrence)calendarItem.Reminder.GetPertinentItem(actualizationTime))
								{
									if (calendarItemOccurrence != null)
									{
										calendarInfo = CalendarInfo.FromCalendarItemBase(creationRequestTime, timeZoneAdjustment, calendarItemOccurrence);
									}
								}
							}
						}
						if (!(calendarInfo.ReminderTime < actualizationTime) && calendarInfo.IsInteresting(CalendarNotificationType.Reminder) && (workingHours == null || Utils.InWorkingHours(calendarInfo.StartTime, calendarInfo.EndTime, workingHours)))
						{
							if (ExDateTime.MinValue == exDateTime)
							{
								exDateTime = calendarInfo.ReminderTime;
							}
							else if (calendarInfo.ReminderTime > exDateTime)
							{
								break;
							}
							list.Add(calendarInfo);
						}
					}
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x04000652 RID: 1618
		private const int RowsRetrieving = 100;
	}
}
