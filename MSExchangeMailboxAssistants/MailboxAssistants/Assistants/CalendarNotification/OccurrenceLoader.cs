using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000DE RID: 222
	internal static class OccurrenceLoader
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x0003F0D0 File Offset: 0x0003D2D0
		public static List<CalendarInfo> Load(ExDateTime creationRequestTime, ExTimeZone timeZoneAdjustment, MailboxSession session, MeetingRequest mtgReq, CalendarItemBase calItemBase, StorageWorkingHours workingHours, ExDateTime actualizationTime, ExDateTime endTime)
		{
			List<CalendarInfo> list = new List<CalendarInfo>();
			Interval<ExDateTime> timeslot = new Interval<ExDateTime>(actualizationTime, false, endTime, true);
			ExDateTime? exDateTime = null;
			ExDateTime? exDateTime2 = null;
			if (mtgReq != null)
			{
				CalendarInfo.GetOldFields(mtgReq, out exDateTime, out exDateTime2);
			}
			if (CalendarItemType.RecurringMaster != calItemBase.CalendarItemType || ((CalendarItem)calItemBase).Recurrence == null)
			{
				CalendarInfo calendarInfo = CalendarInfo.FromCalendarItemBase(creationRequestTime, timeZoneAdjustment, calItemBase);
				calendarInfo.UpdateOldFields(exDateTime, exDateTime2);
				if (OccurrenceLoader.IsInteresing(timeslot, calendarInfo.OldStartTime, calendarInfo.OldEndTime, calendarInfo.StartTime, calendarInfo.EndTime, workingHours))
				{
					list.Add(calendarInfo);
				}
				else
				{
					ExTraceGlobals.AssistantTracer.TraceDebug((long)typeof(ReminderLoader).GetHashCode(), "The calendar update/reminder is out of the valid scope. subj: {0}, calItemId: {1}, calItemOccId: {2}, event_t: {3}, rmd_t: {4}, s_t: {5}, e_t: {6}, S_T: {7} E_T: {8}, scope_s: {9}, scope_e: {10}", new object[]
					{
						calendarInfo.NormalizedSubject,
						calendarInfo.CalendarItemIdentity,
						calendarInfo.CalendarItemOccurrenceIdentity,
						calendarInfo.CreationRequestTime,
						calendarInfo.ReminderTime,
						calendarInfo.OldStartTime,
						calendarInfo.OldEndTime,
						calendarInfo.StartTime,
						calendarInfo.EndTime,
						actualizationTime,
						endTime
					});
				}
				return list;
			}
			foreach (OccurrenceInfo occurrenceInfo in ((CalendarItem)calItemBase).Recurrence.GetOccurrenceInfoList(actualizationTime, endTime))
			{
				if (!OccurrenceLoader.IsInteresing(timeslot, exDateTime, exDateTime2, occurrenceInfo.StartTime, occurrenceInfo.EndTime, workingHours))
				{
					ExTraceGlobals.AssistantTracer.TraceDebug((long)typeof(ReminderLoader).GetHashCode(), "The calendar update/reminder is out of the valid scope.calItemId: {0}, calItemOccId: {1}, event_t: {2}, rmd_m: {3}, s_t: {4}, e_t: {5}, S_T: {6} E_T: {7}, scope_s: {8}, scope_e: {9}", new object[]
					{
						(calItemBase.Id == null) ? null : calItemBase.Id.ObjectId,
						occurrenceInfo.OccurrenceDateId,
						actualizationTime,
						(calItemBase.Reminder == null) ? -1 : calItemBase.Reminder.MinutesBeforeStart,
						exDateTime,
						exDateTime2,
						occurrenceInfo.StartTime,
						occurrenceInfo.EndTime,
						actualizationTime,
						endTime
					});
				}
				else
				{
					CalendarInfo calendarInfo2 = null;
					if (occurrenceInfo.VersionedId == null)
					{
						calendarInfo2 = CalendarInfo.FromMasterCalendarItemAndOccurrenceInfo(creationRequestTime, timeZoneAdjustment, (CalendarItem)calItemBase, occurrenceInfo);
					}
					else
					{
						using (CalendarItemOccurrence calendarItemOccurrence = ((CalendarItem)calItemBase).OpenOccurrenceByOriginalStartTime(occurrenceInfo.OriginalStartTime, new PropertyDefinition[0]))
						{
							calendarInfo2 = CalendarInfo.FromCalendarItemBase(creationRequestTime, timeZoneAdjustment, calendarItemOccurrence);
						}
					}
					calendarInfo2.UpdateOldFields(exDateTime, exDateTime2);
					list.Add(calendarInfo2);
				}
			}
			return list;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0003F400 File Offset: 0x0003D600
		private static bool IsInteresing(Interval<ExDateTime> timeslot, ExDateTime? oldStartTime, ExDateTime? oldEndTime, ExDateTime startTime, ExDateTime endTime, StorageWorkingHours workingHours)
		{
			Interval<ExDateTime> other = new Interval<ExDateTime>(startTime, false, endTime, startTime < endTime);
			bool flag = timeslot.IsOverlapped(other);
			bool flag2 = workingHours == null || Utils.InWorkingHours(other.Minimum, other.Maximum, workingHours);
			bool flag3 = false;
			bool flag4 = false;
			if (oldStartTime != null && oldEndTime != null)
			{
				Interval<ExDateTime> other2 = new Interval<ExDateTime>(oldStartTime.Value, false, oldEndTime.Value, oldStartTime.Value < oldEndTime.Value);
				flag3 = timeslot.IsOverlapped(other2);
				flag4 = (workingHours == null || Utils.InWorkingHours(other2.Minimum, other2.Maximum, workingHours));
			}
			return (flag3 && flag4) || (flag && flag2);
		}
	}
}
