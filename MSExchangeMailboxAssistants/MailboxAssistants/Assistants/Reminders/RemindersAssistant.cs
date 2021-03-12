using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000251 RID: 593
	internal sealed class RemindersAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06001639 RID: 5689 RVA: 0x0007D948 File Offset: 0x0007BB48
		public RemindersAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.Log = RemindersAssistantLog.Instance;
			ReminderMessageManager reminderMessageManager = new ReminderMessageManager(this.Log, new ReminderTimeCalculatorFactory());
			this.EventProcessors = new List<IEventProcessor>
			{
				new EventTimeBasedInboxReminderProcessor(reminderMessageManager, this.Log),
				new EmailModernReminderProcessor(reminderMessageManager, this.Log),
				new ToDoModernReminderProcessor(reminderMessageManager, this.Log),
				new MailboxMoveProcessor(reminderMessageManager, this.Log),
				new ReminderReceiveProcessor(reminderMessageManager, this.Log)
			};
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0007D9E1 File Offset: 0x0007BBE1
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0007D9E9 File Offset: 0x0007BBE9
		private List<IEventProcessor> EventProcessors { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0007D9F2 File Offset: 0x0007BBF2
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0007D9FA File Offset: 0x0007BBFA
		private IRemindersAssistantLog Log { get; set; }

		// Token: 0x0600163E RID: 5694 RVA: 0x0007DA04 File Offset: 0x0007BC04
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			ArgumentValidator.ThrowIfNull("mapiEvent", mapiEvent);
			ExTraceGlobals.GeneralTracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "RemindersAssistant.IsEventInteresting event: {0}", mapiEvent);
			foreach (IEventProcessor eventProcessor in this.EventProcessors)
			{
				if (eventProcessor.IsEventInteresting(mapiEvent))
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string>((long)this.GetHashCode(), "IsEventInteresting is true for event processor {0}", eventProcessor.Name);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0007DAA0 File Offset: 0x0007BCA0
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ArgumentValidator.ThrowIfNull("mapiEvent", mapiEvent);
			ArgumentValidator.ThrowIfNull("itemStore", itemStore);
			ArgumentValidator.ThrowIfNull("customDataToLog", customDataToLog);
			ExTraceGlobals.GeneralTracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "RemindersAssistant.HandleEventInternal event: {0}", mapiEvent);
			string value = string.Empty;
			try
			{
				VariantConfigurationSnapshot variantConfigurationSnapshot = this.GetVariantConfigurationSnapshot(itemStore.MailboxOwner, itemStore.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
				if (variantConfigurationSnapshot != null)
				{
					bool flag = Globals.IsItemInDumpster(itemStore, item);
					if (flag)
					{
						ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "RemindersAssistant.HandleEventInternal running on a dumpster item");
					}
					bool flag2 = false;
					foreach (IEventProcessor eventProcessor in this.EventProcessors)
					{
						if (eventProcessor.IsEnabled(variantConfigurationSnapshot) && eventProcessor.IsEventInteresting(mapiEvent))
						{
							ExTraceGlobals.GeneralTracer.TraceDebug<string>((long)this.GetHashCode(), "Calling HandleEventInternal for event processor {0}", eventProcessor.Name);
							eventProcessor.HandleEvent(mapiEvent, itemStore, item, flag, customDataToLog);
							flag2 = true;
						}
					}
					if (!flag2)
					{
						value = "FlightNotEnabled";
					}
				}
				else
				{
					value = "UserNotFound";
				}
			}
			catch (CorruptDataException e)
			{
				this.Log.LogEntry(itemStore, e, false, "CorruptDataException occurred in HandleEvent", new object[0]);
				return;
			}
			catch (Exception e2)
			{
				this.Log.LogEntry(itemStore, e2, false, "Exception occurred in HandleEvent", new object[0]);
				throw;
			}
			customDataToLog.Add(new KeyValuePair<string, object>("SkipEventReason", value));
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0007DC24 File Offset: 0x0007BE24
		private VariantConfigurationSnapshot GetVariantConfigurationSnapshot(IExchangePrincipal mbxOwner, IRecipientSession recipientSession)
		{
			VariantConfigurationSnapshot result = null;
			ADUser aduser = DirectoryHelper.ReadADRecipient(mbxOwner.MailboxInfo.MailboxGuid, mbxOwner.MailboxInfo.IsArchive, recipientSession) as ADUser;
			if (aduser != null)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "User found, retrieving flighting information", mbxOwner);
				result = VariantConfiguration.GetSnapshot(aduser.GetContext(null), null, null);
			}
			return result;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0007DC7F File Offset: 0x0007BE7F
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0007DC87 File Offset: 0x0007BE87
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0007DC8F File Offset: 0x0007BE8F
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000D15 RID: 3349
		private const string SkipEventUserNotFound = "UserNotFound";

		// Token: 0x04000D16 RID: 3350
		private const string SkipEventFlightNotEnabled = "FlightNotEnabled";
	}
}
