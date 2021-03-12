using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000250 RID: 592
	internal class ReminderReceiveProcessor : IEventProcessor
	{
		// Token: 0x0600162F RID: 5679 RVA: 0x0007D81F File Offset: 0x0007BA1F
		public ReminderReceiveProcessor(ReminderMessageManager reminderMessageManager, IRemindersAssistantLog log)
		{
			ArgumentValidator.ThrowIfNull("reminderMessageManager", reminderMessageManager);
			ArgumentValidator.ThrowIfNull("log", log);
			this.Name = base.GetType().Name;
			this.ReminderMessageManager = reminderMessageManager;
			this.Log = log;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0007D85C File Offset: 0x0007BA5C
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x0007D864 File Offset: 0x0007BA64
		public string Name { get; private set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0007D86D File Offset: 0x0007BA6D
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x0007D875 File Offset: 0x0007BA75
		private ReminderMessageManager ReminderMessageManager { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0007D87E File Offset: 0x0007BA7E
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x0007D886 File Offset: 0x0007BA86
		private IRemindersAssistantLog Log { get; set; }

		// Token: 0x06001636 RID: 5686 RVA: 0x0007D890 File Offset: 0x0007BA90
		public bool IsEnabled(VariantConfigurationSnapshot snapshot)
		{
			return snapshot.MailboxAssistants.EmailReminders.Enabled;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0007D8B0 File Offset: 0x0007BAB0
		public bool IsEventInteresting(IMapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) == MapiEventTypeFlags.ObjectCreated && ((mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.NonIPMFolder) == MapiExtendedEventFlags.None && mapiEvent.ItemType == ObjectType.MAPI_MESSAGE) && ObjectClass.IsOfClass(mapiEvent.ObjectClass, "IPM.Note.Reminder.Event");
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0007D8EC File Offset: 0x0007BAEC
		public void HandleEvent(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, bool isItemInDumpster, List<KeyValuePair<string, object>> customDataToLog)
		{
			this.Log.LogEntry(itemStore, "Event Reminder item received", new object[0]);
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.ReminderMessageManager.ReceivedReminderMessage(itemStore, item);
			stopwatch.Stop();
			customDataToLog.Add(new KeyValuePair<string, object>("RemRcv.Latency", stopwatch.ElapsedMilliseconds.ToString()));
		}
	}
}
