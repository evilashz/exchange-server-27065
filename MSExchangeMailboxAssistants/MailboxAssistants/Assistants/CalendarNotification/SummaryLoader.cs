using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000DD RID: 221
	internal static class SummaryLoader
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x0003F034 File Offset: 0x0003D234
		public static IList<CalendarInfo> Load(ExDateTime creationRequestTime, ExTimeZone timeZoneAdjustment, MailboxSession session, StoreObjectId calFldrId, StorageWorkingHours workingHours, ExDateTime actualizationTime, ExDateTime endTime)
		{
			List<CalendarInfo> list = new List<CalendarInfo>();
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(session, calFldrId))
			{
				foreach (object[] propVals in calendarFolder.GetCalendarView(actualizationTime, endTime, CalendarInfo.InterestedProperties))
				{
					CalendarInfo calendarInfo = CalendarInfo.FromInterestedProperties(creationRequestTime, timeZoneAdjustment, session, true, propVals);
					if (calendarInfo.IsInteresting(CalendarNotificationType.Summary) && (workingHours == null || Utils.InWorkingHours(calendarInfo.StartTime, calendarInfo.EndTime, workingHours)))
					{
						list.Add(calendarInfo);
					}
				}
			}
			return list.AsReadOnly();
		}
	}
}
