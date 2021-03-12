using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x0200011F RID: 287
	internal static class CheckConflict
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x0004C6E4 File Offset: 0x0004A8E4
		public static bool CheckCalendarConflict(MailboxSession itemStore, CalendarItemBase request, ExDateTime? endOfBookingWindow, out List<AdjacencyOrConflictInfo> conflictList)
		{
			conflictList = new List<AdjacencyOrConflictInfo>();
			if (endOfBookingWindow != null && endOfBookingWindow < request.StartTime)
			{
				endOfBookingWindow = null;
			}
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(itemStore, DefaultFolderType.Calendar))
			{
				AdjacencyOrConflictInfo[] adjacentOrConflictingItems = calendarFolder.GetAdjacentOrConflictingItems(request, endOfBookingWindow);
				if (adjacentOrConflictingItems != null)
				{
					ExDateTime now = ExDateTime.Now;
					foreach (AdjacencyOrConflictInfo adjacencyOrConflictInfo in adjacentOrConflictingItems)
					{
						if ((adjacencyOrConflictInfo.AdjacencyOrConflictType & AdjacencyOrConflictType.Conflicts) == AdjacencyOrConflictType.Conflicts && adjacencyOrConflictInfo.FreeBusyStatus != BusyType.Free && !(adjacencyOrConflictInfo.OccurrenceInfo.EndTime <= now))
						{
							conflictList.Add(adjacencyOrConflictInfo);
						}
					}
				}
			}
			return conflictList.Count != 0;
		}
	}
}
