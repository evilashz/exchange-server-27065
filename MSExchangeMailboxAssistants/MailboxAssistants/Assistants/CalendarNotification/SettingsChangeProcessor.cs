using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000ED RID: 237
	internal sealed class SettingsChangeProcessor
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x0004137C File Offset: 0x0003F57C
		public SettingsChangeProcessor(DatabaseInfo databaseInfo)
		{
			this.databaseInfo = databaseInfo;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00041404 File Offset: 0x0003F604
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (this.IsMailboxCreatedOrConnected(mapiEvent) || this.IsMailboxDeletedOrDisconnected(mapiEvent))
			{
				return true;
			}
			if (ObjectType.MAPI_MESSAGE != mapiEvent.ItemType)
			{
				return false;
			}
			if (this.OwaUserOptionConfigurationObjectClass.Equals(mapiEvent.ObjectClass, StringComparison.InvariantCultureIgnoreCase) || this.TextMessagingAccountConfigurationObjectClass.Equals(mapiEvent.ObjectClass, StringComparison.InvariantCultureIgnoreCase) || this.TextNotificationConfigurationObjectClass.Equals(mapiEvent.ObjectClass, StringComparison.InvariantCultureIgnoreCase) || this.VoiceNotificationConfigurationObjectClass.Equals(mapiEvent.ObjectClass, StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			if (this.WorkingHoursConfigurationObjectClass.Equals(mapiEvent.ObjectClass, StringComparison.InvariantCultureIgnoreCase))
			{
				MailboxData fromCache = MailboxData.GetFromCache(mapiEvent.MailboxGuid);
				if (fromCache == null || fromCache.DefaultCalendarFolderId == null)
				{
					return true;
				}
				using (fromCache.CreateReadLock())
				{
					if (object.Equals(StoreObjectId.DummyId, fromCache.DefaultCalendarFolderId))
					{
						return true;
					}
					return ArrayComparer<byte>.Comparer.Equals(mapiEvent.ParentEntryId, fromCache.DefaultCalendarFolderId.ProviderLevelItemId);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00041508 File Offset: 0x0003F708
		public void HandleEvent(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			if (ObjectType.MAPI_MESSAGE == mapiEvent.ItemType && item == null)
			{
				ExTraceGlobals.UserSettingsTracer.TraceDebug<string>((long)this.GetHashCode(), "Event is MAPI_MESSAGE but item is null, message class is {0}", mapiEvent.ObjectClass);
				return;
			}
			if (!itemStore.Capabilities.CanHaveUserConfigurationManager)
			{
				ExTraceGlobals.UserSettingsTracer.TraceDebug<string>((long)this.GetHashCode(), "Mailbox sesstion does not have UserConfigurationManager capability. Possibly an alternate mailbox, {0}.", itemStore.MailboxOwnerLegacyDN);
				return;
			}
			if (this.IsMailboxDeletedOrDisconnected(mapiEvent))
			{
				ExTraceGlobals.UserSettingsTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "Processing Disabling event for mailbox {0} on database {1}", itemStore.MailboxOwnerLegacyDN, itemStore.MdbGuid);
				UserSettings settings = new UserSettings(itemStore);
				SystemMailbox systemMailbox = this.GetSystemMailbox();
				if (systemMailbox != null)
				{
					NotificationFactories.Instance.UpdateSettingUnderSystemMailbox(settings, systemMailbox);
				}
				SettingsChangeListener.Instance.RaiseSettingsChangedEvent(settings, new InfoFromUserMailboxSession(itemStore));
				return;
			}
			if (this.IsMailboxCreatedOrConnected(mapiEvent))
			{
				ExTraceGlobals.UserSettingsTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "Processing Enabling event for mailbox {0} on database {1}", itemStore.MailboxOwnerLegacyDN, itemStore.MdbGuid);
				using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(itemStore))
				{
					TextMessagingAccount account = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(itemStore.MailboxOwner.ObjectId);
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, itemStore.GetADSessionSettings(), 194, "HandleEvent", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\CalendarNotification\\SettingsChangeProcessor.cs");
					ADRecipient adrecipient = tenantOrRootOrgRecipientSession.Read(itemStore.MailboxOwner.ObjectId);
					if (adrecipient != null)
					{
						TextMessagingHelper.UpdateAndSaveTextMessgaingStateOnAdUser(account, adrecipient, tenantOrRootOrgRecipientSession);
					}
				}
			}
			this.HandleSettingsEvent(itemStore);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00041678 File Offset: 0x0003F878
		private bool IsMailboxCreatedOrConnected(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxCreated) != (MapiEventTypeFlags)0 || (mapiEvent.EventMask & MapiEventTypeFlags.MailboxReconnected) != (MapiEventTypeFlags)0 || ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveFailed) != (MapiEventTypeFlags)0 && (mapiEvent.EventFlags & MapiEventFlags.Source) != MapiEventFlags.None) || ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveSucceeded) != (MapiEventTypeFlags)0 && (mapiEvent.EventFlags & MapiEventFlags.Destination) != MapiEventFlags.None);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000416E4 File Offset: 0x0003F8E4
		private bool IsMailboxDeletedOrDisconnected(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxDeleted) != (MapiEventTypeFlags)0 || (mapiEvent.EventMask & MapiEventTypeFlags.MailboxDisconnected) != (MapiEventTypeFlags)0 || ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveStarted) != (MapiEventTypeFlags)0 && (mapiEvent.EventFlags & MapiEventFlags.Source) != MapiEventFlags.None);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00041734 File Offset: 0x0003F934
		private void HandleSettingsEvent(MailboxSession session)
		{
			UserSettings userSettings = Utils.LoadUserSettingsFromMailboxSession(session);
			SystemMailbox systemMailbox = this.GetSystemMailbox();
			if (systemMailbox != null)
			{
				NotificationFactories.Instance.UpdateSettingUnderSystemMailbox(userSettings, systemMailbox);
			}
			SettingsChangeListener.Instance.RaiseSettingsChangedEvent(userSettings, new InfoFromUserMailboxSession(session));
			if (userSettings.Voice != null && userSettings.Voice.Enabled)
			{
				ExTraceGlobals.UserSettingsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} Voice notification is enabled on user {1}", ExDateTime.Now.ToLongTimeString(), userSettings.LegacyDN);
			}
			else
			{
				ExTraceGlobals.UserSettingsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} Voice notification is disabled on user {1}", ExDateTime.Now.ToLongTimeString(), userSettings.LegacyDN);
			}
			if (userSettings.Text != null && userSettings.Text.Enabled)
			{
				ExTraceGlobals.UserSettingsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} Text notification is enabled on user {1}", ExDateTime.Now.ToLongTimeString(), userSettings.LegacyDN);
				return;
			}
			ExTraceGlobals.UserSettingsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} Text notification is disabled on user {1}", ExDateTime.Now.ToLongTimeString(), userSettings.LegacyDN);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00041844 File Offset: 0x0003FA44
		private SystemMailbox GetSystemMailbox()
		{
			if (this.systemMailbox == null)
			{
				try
				{
					ExTraceGlobals.UserSettingsTracer.TraceDebug<string>((long)this.GetHashCode(), "Getting system mailbox instance for database '{0}'", this.databaseInfo.DisplayName);
					this.systemMailbox = SystemMailbox.GetInstance(this.databaseInfo);
				}
				catch (Exception ex)
				{
					ExTraceGlobals.UserSettingsTracer.TraceDebug<string, Exception>((long)this.GetHashCode(), "Error getting system mailbox instance for database '{0}': {1}", this.databaseInfo.DisplayName, ex);
					if (!CalendarNotificationAssistant.TryHandleException((long)this.GetHashCode(), "Retrieving system mailbox for SettingsChangeProcessor", this.databaseInfo.DisplayName, ex))
					{
						throw;
					}
				}
			}
			return this.systemMailbox;
		}

		// Token: 0x04000682 RID: 1666
		private readonly string OwaUserOptionConfigurationObjectClass = new UserConfigurationName("OWA.UserOptions", ConfigurationNameKind.Name).FullName;

		// Token: 0x04000683 RID: 1667
		private readonly string TextMessagingAccountConfigurationObjectClass = new UserConfigurationName("TextMessaging.001", ConfigurationNameKind.Name).FullName;

		// Token: 0x04000684 RID: 1668
		private readonly string TextNotificationConfigurationObjectClass = new UserConfigurationName("CalendarNotification.001", ConfigurationNameKind.Name).FullName;

		// Token: 0x04000685 RID: 1669
		private readonly string VoiceNotificationConfigurationObjectClass = new UserConfigurationName("Um.General", ConfigurationNameKind.Name).FullName;

		// Token: 0x04000686 RID: 1670
		private readonly string WorkingHoursConfigurationObjectClass = new UserConfigurationName("WorkHours", ConfigurationNameKind.Name).FullName;

		// Token: 0x04000687 RID: 1671
		private SystemMailbox systemMailbox;

		// Token: 0x04000688 RID: 1672
		private readonly DatabaseInfo databaseInfo;
	}
}
