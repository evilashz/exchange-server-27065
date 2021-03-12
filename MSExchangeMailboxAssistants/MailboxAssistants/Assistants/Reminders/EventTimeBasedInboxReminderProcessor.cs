using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000246 RID: 582
	internal class EventTimeBasedInboxReminderProcessor : ReminderProcessorBase
	{
		// Token: 0x060015D8 RID: 5592 RVA: 0x0007AFD9 File Offset: 0x000791D9
		public EventTimeBasedInboxReminderProcessor(ReminderMessageManager reminderMessageManager, IRemindersAssistantLog log) : base(reminderMessageManager, log)
		{
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0007AFE4 File Offset: 0x000791E4
		public override bool IsEnabled(VariantConfigurationSnapshot snapshot)
		{
			return snapshot.MailboxAssistants.EmailReminders.Enabled;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0007B004 File Offset: 0x00079204
		public override bool IsEventInteresting(IMapiEvent mapiEvent)
		{
			bool flag = mapiEvent.ItemType == ObjectType.MAPI_MESSAGE;
			bool flag2 = !base.IsNonIPMFolder(mapiEvent.ExtendedEventFlags);
			bool flag3 = base.IsModernRemindersModified(mapiEvent.ExtendedEventFlags) || this.IsContentIndexingPropertyModified(mapiEvent.ExtendedEventFlags) || this.IsAppointmentTimeModified(mapiEvent.ExtendedEventFlags);
			bool flag4 = ObjectClass.IsCalendarItem(mapiEvent.ObjectClass);
			bool flag5 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectModified) && flag3;
			bool flag6 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectMoved);
			bool flag7 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectDeleted);
			bool flag8 = flag4 && (flag5 || flag6 || flag7);
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EventTimeBasedInboxReminderProcessor.IsEventInteresting - relevantPropertyChange={0}, modified={1}, moved={2}, deleted={3}, relevantOperation={4}, relevantItemType={5}, relevantFolder={6}", new object[]
			{
				flag3,
				flag5,
				flag6,
				flag7,
				flag8,
				flag,
				flag2
			});
			return flag && flag2 && flag8;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0007B110 File Offset: 0x00079310
		protected override void OnObjectModified(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EventTimeBasedInboxReminderProcessor.OnObjectModified");
			if (item != null)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EventTimeBasedInboxReminderProcessor.OnObjectModified - item exists");
				if (!(item is ICalendarItem))
				{
					return;
				}
				ICalendarItem calendarItem = (ICalendarItem)item;
				if (calendarItem.GlobalObjectId == null)
				{
					base.Log.LogEntry(itemStore, "EventTimeBasedInboxReminder.OnObjectModified - Calendar item missing GOID", new object[0]);
					return;
				}
				base.Log.LogEntry(itemStore, "EventTimeBasedInboxReminder.OnObjectModified - Start Time:{0}, End Time:{1}", new object[]
				{
					calendarItem.StartTime.UniversalTime,
					calendarItem.EndTime.UniversalTime
				});
				this.DeleteReminderMessages(itemStore, calendarItem, customDataToLog);
				this.CreateReminderMessages(itemStore, calendarItem, customDataToLog);
			}
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0007B1D8 File Offset: 0x000793D8
		protected override void OnObjectDeleted(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EventTimeBasedInboxReminderProcessor.OnObjectDeleted");
			if (item is ICalendarItem && item.StoreObjectId != null)
			{
				ICalendarItem calendarItem = (ICalendarItem)item;
				if (calendarItem.GlobalObjectId == null)
				{
					base.Log.LogEntry(itemStore, "EventTimeBasedInboxReminder.OnObjectDeleted - Calendar item missing GOID", new object[0]);
					return;
				}
				base.Log.LogEntry(itemStore, "EventTimeBasedInboxReminder.OnObjectDeleted - Start Time:{0}, End Time:{1}", new object[]
				{
					calendarItem.StartTime.UniversalTime,
					calendarItem.EndTime.UniversalTime
				});
				this.DeleteReminderMessages(itemStore, calendarItem, customDataToLog);
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0007B2B0 File Offset: 0x000794B0
		private void DeleteReminderMessages(IMailboxSession itemStore, ICalendarItem calendarItem, List<KeyValuePair<string, object>> customDataToLog)
		{
			int numRemindersDeleted = 0;
			customDataToLog.Add(new KeyValuePair<string, object>("EvtRemDel.Latency", base.ExecuteAndMeasure(delegate
			{
				numRemindersDeleted = this.ReminderMessageManager.DeleteReminderMessages(itemStore, calendarItem);
			}).ToString()));
			customDataToLog.Add(new KeyValuePair<string, object>("EvtRemDel.Count", numRemindersDeleted));
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0007B35C File Offset: 0x0007955C
		private void CreateReminderMessages(IMailboxSession itemStore, ICalendarItem calendarItem, List<KeyValuePair<string, object>> customDataToLog)
		{
			Reminders<EventTimeBasedInboxReminder> reminders = calendarItem.EventTimeBasedInboxReminders;
			int numRemindersCreated = 0;
			long num = base.ExecuteAndMeasure(delegate
			{
				numRemindersCreated = this.ReminderMessageManager.CreateReminderMessages(itemStore, calendarItem, reminders, customDataToLog);
			});
			customDataToLog.Add(new KeyValuePair<string, object>("EvtRemAdd.Latency", num.ToString()));
			customDataToLog.Add(new KeyValuePair<string, object>("EvtRemAdd.Count", numRemindersCreated));
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0007B3F2 File Offset: 0x000795F2
		private bool IsContentIndexingPropertyModified(MapiExtendedEventFlags extendedEventFlags)
		{
			return (extendedEventFlags & MapiExtendedEventFlags.NoContentIndexingPropertyModified) == MapiExtendedEventFlags.None;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0007B3FC File Offset: 0x000795FC
		private bool IsAppointmentTimeModified(MapiExtendedEventFlags extendedEventFlags)
		{
			return (extendedEventFlags & MapiExtendedEventFlags.AppointmentTimeNotModified) == MapiExtendedEventFlags.None;
		}
	}
}
