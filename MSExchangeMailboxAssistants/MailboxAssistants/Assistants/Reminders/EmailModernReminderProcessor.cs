using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000245 RID: 581
	internal class EmailModernReminderProcessor : ModernReminderProcessor
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x0007AD4A File Offset: 0x00078F4A
		public EmailModernReminderProcessor(IReminderMessageManager reminderMessageManager, IRemindersAssistantLog log) : base(reminderMessageManager, log)
		{
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0007AD54 File Offset: 0x00078F54
		public override bool IsEnabled(VariantConfigurationSnapshot snapshot)
		{
			return snapshot.MailboxAssistants.FlagPlus.Enabled;
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0007AD74 File Offset: 0x00078F74
		protected override bool IsCreateInteresting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0007AD77 File Offset: 0x00078F77
		protected override bool IsTypeInteresting(string objectClass)
		{
			return ObjectClass.IsMessage(objectClass, false);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0007AD80 File Offset: 0x00078F80
		protected override void OnObjectModified(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EmailModernReminderProcessor.OnObjectModified");
			if (item != null)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EmailModernReminderProcessor.OnObjectModified - item exists");
				IToDoItem messageItem = (IToDoItem)item;
				this.DeleteReminderMessages(itemStore, messageItem, customDataToLog);
				this.CreateReminderMessages(itemStore, messageItem, customDataToLog);
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0007ADD8 File Offset: 0x00078FD8
		protected override void OnObjectDeleted(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EmailModernReminderProcessor.OnObjectDeleted");
			if (item != null)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EmailModernReminderProcessor.OnObjectDeleted - item exists");
				IToDoItem messageItem = (IToDoItem)item;
				this.DeleteReminderMessages(itemStore, messageItem, customDataToLog);
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0007AE54 File Offset: 0x00079054
		private void DeleteReminderMessages(IMailboxSession itemStore, IToDoItem messageItem, List<KeyValuePair<string, object>> customDataToLog)
		{
			int numRemindersDeleted = 0;
			long num = 0L;
			GlobalObjectId globalObjectId = messageItem.GetGlobalObjectId();
			if (globalObjectId != null)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EmailModernReminderProcessor.DeleteReminderMessages - global object id exists");
				num = base.ExecuteAndMeasure(delegate
				{
					numRemindersDeleted = this.ReminderMessageManager.DeleteReminderMessages(itemStore, messageItem);
				});
			}
			customDataToLog.Add(new KeyValuePair<string, object>("EmlRemDel.Latency", num.ToString()));
			customDataToLog.Add(new KeyValuePair<string, object>("EmlRemDel.Count", numRemindersDeleted));
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0007AF2C File Offset: 0x0007912C
		private void CreateReminderMessages(IMailboxSession itemStore, IToDoItem messageItem, List<KeyValuePair<string, object>> customDataToLog)
		{
			int numRemindersCreated = 0;
			long num = 0L;
			Reminders<ModernReminder> reminders = messageItem.ModernReminders;
			if (reminders != null)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "EmailModernReminderProcessor.CreateReminderMessages - Reminders exist");
				num = base.ExecuteAndMeasure(delegate
				{
					numRemindersCreated = this.ReminderMessageManager.CreateReminderMessages(itemStore, messageItem, reminders);
				});
			}
			customDataToLog.Add(new KeyValuePair<string, object>("EmlRemAdd.Latency", num.ToString()));
			customDataToLog.Add(new KeyValuePair<string, object>("EmlRemAdd.Count", numRemindersCreated));
		}
	}
}
