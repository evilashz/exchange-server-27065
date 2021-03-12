using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000248 RID: 584
	internal class MailboxMoveProcessor : IEventProcessor
	{
		// Token: 0x060015E3 RID: 5603 RVA: 0x0007B407 File Offset: 0x00079607
		public MailboxMoveProcessor(ReminderMessageManager reminderMessageManager, IRemindersAssistantLog log)
		{
			ArgumentValidator.ThrowIfNull("reminderMessageManager", reminderMessageManager);
			ArgumentValidator.ThrowIfNull("log", log);
			this.Name = base.GetType().Name;
			this.ReminderMessageManager = reminderMessageManager;
			this.Log = log;
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0007B444 File Offset: 0x00079644
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0007B44C File Offset: 0x0007964C
		public string Name { get; private set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0007B455 File Offset: 0x00079655
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0007B45D File Offset: 0x0007965D
		private ReminderMessageManager ReminderMessageManager { get; set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0007B466 File Offset: 0x00079666
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0007B46E File Offset: 0x0007966E
		private IRemindersAssistantLog Log { get; set; }

		// Token: 0x060015EA RID: 5610 RVA: 0x0007B478 File Offset: 0x00079678
		public bool IsEnabled(VariantConfigurationSnapshot snapshot)
		{
			return snapshot.MailboxAssistants.EmailReminders.Enabled || snapshot.MailboxAssistants.QuickCapture.Enabled || snapshot.MailboxAssistants.FlagPlus.Enabled;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0007B4C4 File Offset: 0x000796C4
		public bool IsEventInteresting(IMapiEvent mapiEvent)
		{
			MapiEventTypeFlags mapiEventTypeFlags = MapiEventTypeFlags.MailboxMoveSucceeded;
			return (mapiEvent.EventMask & mapiEventTypeFlags) == mapiEventTypeFlags;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0007B4E4 File Offset: 0x000796E4
		public void HandleEvent(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, bool isItemInDumpster, List<KeyValuePair<string, object>> customDataToLog)
		{
			this.Log.LogEntry(itemStore, "Resubmitting reminder messages on MailboxMoveSucceeded", new object[0]);
			Stopwatch stopwatch = Stopwatch.StartNew();
			int num = this.ReminderMessageManager.ResubmitReminderMessages(itemStore);
			stopwatch.Stop();
			customDataToLog.Add(new KeyValuePair<string, object>("Resubmit.Latency", stopwatch.ElapsedMilliseconds.ToString()));
			customDataToLog.Add(new KeyValuePair<string, object>("Resubmit.Count", num));
		}
	}
}
