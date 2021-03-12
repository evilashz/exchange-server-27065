using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000254 RID: 596
	internal class ToDoModernReminderProcessor : ModernReminderProcessor
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x0007DDF4 File Offset: 0x0007BFF4
		public ToDoModernReminderProcessor(IReminderMessageManager reminderMessageManager, IRemindersAssistantLog log) : base(reminderMessageManager, log)
		{
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0007DE00 File Offset: 0x0007C000
		public override bool IsEnabled(VariantConfigurationSnapshot snapshot)
		{
			return snapshot.MailboxAssistants.QuickCapture.Enabled;
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0007DE20 File Offset: 0x0007C020
		protected override bool IsCreateInteresting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0007DE23 File Offset: 0x0007C023
		protected override bool IsTypeInteresting(string objectClass)
		{
			return ObjectClass.IsTask(objectClass);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0007DEAC File Offset: 0x0007C0AC
		protected override void OnObjectModified(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ToDoModernReminderProcessor.OnObjectModified");
			if (item != null)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ToDoModernReminderProcessor.OnObjectModified - item exists");
				IToDoItem toDoItem = (IToDoItem)item;
				Reminders<ModernReminder> reminders = toDoItem.ModernReminders;
				if (reminders != null)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ToDoModernReminderProcessor.OnObjectModified - Reminders exist -> schedule reminder");
					int numRemindersScheduled = 0;
					customDataToLog.Add(new KeyValuePair<string, object>("ToDoRemSchd.Latency", base.ExecuteAndMeasure(delegate
					{
						numRemindersScheduled = this.ReminderMessageManager.ScheduleReminder(itemStore, toDoItem, reminders);
					}).ToString()));
					customDataToLog.Add(new KeyValuePair<string, object>("ToDoRemSchd.Count", numRemindersScheduled));
					return;
				}
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ToDoModernReminderProcessor.OnObjectModified - Modern reminders don't exist -> clearing the reminder");
				customDataToLog.Add(new KeyValuePair<string, object>("ToDoRemClr.Latency", base.ExecuteAndMeasure(delegate
				{
					this.ReminderMessageManager.ClearReminder(itemStore, toDoItem);
				}).ToString()));
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0007DFFF File Offset: 0x0007C1FF
		protected override void OnObjectDeleted(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ToDoModernReminderProcessor.OnObjectDeleted");
		}
	}
}
