using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x0200024D RID: 589
	internal interface IReminderMessageManager
	{
		// Token: 0x06001603 RID: 5635
		int DeleteReminderMessages(IMailboxSession itemStore, ICalendarItem calendarItem);

		// Token: 0x06001604 RID: 5636
		int CreateReminderMessages(IMailboxSession itemStore, ICalendarItem calendarItem, Reminders<EventTimeBasedInboxReminder> reminders, List<KeyValuePair<string, object>> customDataToLog);

		// Token: 0x06001605 RID: 5637
		void ScheduleNextOccurrenceReminder(IMailboxSession session, ICalendarItem calendarItem, Guid reminderId);

		// Token: 0x06001606 RID: 5638
		int DeleteReminderMessages(IMailboxSession itemStore, IToDoItem messageItem);

		// Token: 0x06001607 RID: 5639
		int CreateReminderMessages(IMailboxSession itemStore, IToDoItem messageItem, Reminders<ModernReminder> reminders);

		// Token: 0x06001608 RID: 5640
		int ScheduleReminder(IMailboxSession itemStore, IToDoItem toDoItem, Reminders<ModernReminder> reminders);

		// Token: 0x06001609 RID: 5641
		void ClearReminder(IMailboxSession itemStore, IToDoItem toDoItem);

		// Token: 0x0600160A RID: 5642
		void ReceivedReminderMessage(IMailboxSession session, IStoreObject item);

		// Token: 0x0600160B RID: 5643
		int ResubmitReminderMessages(IMailboxSession session);

		// Token: 0x0600160C RID: 5644
		Dictionary<Guid, VersionedId> GetReminderMessages(IMailboxSession session, GlobalObjectId reminderItemGlobalObjectId, string objectClass);
	}
}
