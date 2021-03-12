using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000FC RID: 252
	internal class VoiceNotificationFactory : NotificationFactoryBase
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x00044DA3 File Offset: 0x00042FA3
		internal override bool IsFeatureEnabled(UserSettings settings)
		{
			return settings.Voice != null && settings.Voice.Enabled;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00044DBA File Offset: 0x00042FBA
		internal override bool IsInterestedInCalendarChangeEvent(UserSettings settings)
		{
			return this.IsFeatureEnabled(settings);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00044DC3 File Offset: 0x00042FC3
		internal override bool IsInterestedInCalendarMeetingEvent(UserSettings settings)
		{
			return false;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00044DC6 File Offset: 0x00042FC6
		internal override bool IsReminderEnabled(UserSettings settings)
		{
			return this.IsFeatureEnabled(settings);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00044DCF File Offset: 0x00042FCF
		internal override bool IsSummaryEnabled(UserSettings settings)
		{
			return false;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00044DD4 File Offset: 0x00042FD4
		internal override void LoadUserSettingsFromMailboxSession(MailboxSession session, UserSettings settings)
		{
			MailboxRegionalConfiguration mailboxRegionalConfiguration;
			if (Utils.VoiceNotificationIsEnabled(session) && Utils.TryLoadRegionalConfiguration(session, out mailboxRegionalConfiguration))
			{
				settings.Voice = new VoiceNotificationSettings(true, mailboxRegionalConfiguration.TimeZone);
				settings.TimeZone = mailboxRegionalConfiguration.TimeZone;
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00044E11 File Offset: 0x00043011
		internal override string BuildSettingsItemBody(UserSettings settings)
		{
			return SettingsItemBodyParser.BuildVoiceSettingsItemBody(settings.Voice);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00044E1E File Offset: 0x0004301E
		protected override StoreObjectId GetSettingFolderId(SystemMailbox systemMailbox)
		{
			return systemMailbox.VoiceSettingsFolderId;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00044E28 File Offset: 0x00043028
		protected override void UpdateSettingsFromSettingsItemBody(UserSettings settings, string settingsItemBody)
		{
			VoiceNotificationSettings voiceNotificationSettings;
			if (this.TryGetVoiceSettingsFromSettingsItemBody(settingsItemBody, settings.LegacyDN, out voiceNotificationSettings))
			{
				settings.Voice = voiceNotificationSettings;
				settings.TimeZone = voiceNotificationSettings.TimeZone;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00044E5C File Offset: 0x0004305C
		internal override bool TryCreateEmitter(CalendarInfo calendarInfo, MailboxData mailboxData, out ICalendarNotificationEmitter emitter)
		{
			emitter = null;
			if (this.IsFeatureEnabled(mailboxData.Settings) && calendarInfo.IsVoiceReminderEnabled && !string.IsNullOrEmpty(calendarInfo.VoiceReminderPhoneNumber))
			{
				emitter = new VoiceNotificationFactory.VoiceMessagingEmitter(mailboxData.Settings.ExternalDirectoryOrganizationId, mailboxData.Settings.LegacyDN);
			}
			return emitter != null;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00044EB4 File Offset: 0x000430B4
		private bool TryGetVoiceSettingsFromSettingsItemBody(string settingsItemBody, string legacyDN, out VoiceNotificationSettings voiceSettings)
		{
			voiceSettings = null;
			try
			{
				voiceSettings = SettingsItemBodyParser.ParseVoiceSettingsItemBody(settingsItemBody);
			}
			catch (FormatException ex)
			{
				ExTraceGlobals.SystemMailboxTracer.TraceDebug<string, string>((long)this.GetHashCode(), "User {0}'s setting item is corrupted, skipped. Exception message {1} ", legacyDN, ex.Message);
			}
			return voiceSettings != null;
		}

		// Token: 0x040006B4 RID: 1716
		public const string SettingVersion = "V1.0";

		// Token: 0x020000FD RID: 253
		private class VoiceMessagingEmitter : ICalendarNotificationEmitter
		{
			// Token: 0x06000A81 RID: 2689 RVA: 0x00044F10 File Offset: 0x00043110
			public VoiceMessagingEmitter(Guid tenantGuid, string ownerLegacyDN)
			{
				this.ownerLegacyDN = ownerLegacyDN;
				this.tenantGuid = tenantGuid;
			}

			// Token: 0x06000A82 RID: 2690 RVA: 0x00044F28 File Offset: 0x00043128
			public void Emit(MailboxSession session, CalendarNotificationType type, IList<CalendarInfo> calendarInfos)
			{
				ADRecipient adrecipient = Utils.GetADRecipient(this.tenantGuid, this.ownerLegacyDN);
				if (!UMSubscriber.IsValidSubscriber(adrecipient))
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)this.GetHashCode(), "UM is not enabled for user {0}", this.ownerLegacyDN);
					return;
				}
				using (UMSubscriber umsubscriber = new UMSubscriber(adrecipient))
				{
					if (!umsubscriber.UMMailboxPolicy.AllowVoiceNotification || this.IsThisActionAllowed())
					{
						ExTraceGlobals.AssistantTracer.TraceDebug<string, string, bool>((long)this.GetHashCode(), "Abort Voice Emitter, subject: {0}, user: {1}, UM Mailboxpolicy AllowVoiceNotification = {2}", calendarInfos[0].NormalizedSubject, this.ownerLegacyDN, umsubscriber.UMMailboxPolicy.AllowVoiceNotification);
						return;
					}
					ExTraceGlobals.AssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Voice Emitter is emitting, subject: {0}, user: {1} ", calendarInfos[0].NormalizedSubject, this.ownerLegacyDN);
					UMServerManager.GetServerByDialplan((ADObjectId)umsubscriber.DialPlan.Identity);
				}
				string voiceReminderPhoneNumber = calendarInfos[0].VoiceReminderPhoneNumber;
				CalNotifsCounters.NumberOfVoiceRemindersSent.Increment();
				ExTraceGlobals.AssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Voice Emitter is emitted, subject: {0}, user: {1}", calendarInfos[0].NormalizedSubject, this.ownerLegacyDN);
			}

			// Token: 0x06000A83 RID: 2691 RVA: 0x00045064 File Offset: 0x00043264
			private bool IsThisActionAllowed()
			{
				this.CleanupLastUsageTable();
				ExDateTime minValue = ExDateTime.MinValue;
				if (VoiceNotificationFactory.VoiceMessagingEmitter.lastUsageTable.TryGetValue(this.ownerLegacyDN, out minValue) && ExDateTime.Now - minValue < VoiceNotificationFactory.VoiceMessagingEmitter.minInterval)
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<string, double>((long)this.GetHashCode(), "Aborting this emitter because user: {0} already has one emitter within {1} minutes ", this.ownerLegacyDN, VoiceNotificationFactory.VoiceMessagingEmitter.minInterval.TotalMinutes);
					return true;
				}
				VoiceNotificationFactory.VoiceMessagingEmitter.lastUsageTable.AddOrUpdate(this.ownerLegacyDN, ExDateTime.UtcNow, (string Key, ExDateTime existingVal) => ExDateTime.UtcNow);
				return false;
			}

			// Token: 0x06000A84 RID: 2692 RVA: 0x00045108 File Offset: 0x00043308
			private void CleanupLastUsageTable()
			{
				if (VoiceNotificationFactory.VoiceMessagingEmitter.lastUsageTable.Count >= 1000)
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<int>((long)this.GetHashCode(), "Before cleaning up lastUsageTable has {0} entries ", VoiceNotificationFactory.VoiceMessagingEmitter.lastUsageTable.Count);
					List<string> list = new List<string>(1000);
					foreach (KeyValuePair<string, ExDateTime> keyValuePair in VoiceNotificationFactory.VoiceMessagingEmitter.lastUsageTable)
					{
						if (list.Count >= 800)
						{
							break;
						}
						if (ExDateTime.UtcNow - keyValuePair.Value > VoiceNotificationFactory.VoiceMessagingEmitter.minInterval)
						{
							list.Add(keyValuePair.Key);
						}
					}
					foreach (string key in list)
					{
						ExDateTime exDateTime;
						VoiceNotificationFactory.VoiceMessagingEmitter.lastUsageTable.TryRemove(key, out exDateTime);
					}
					ExTraceGlobals.AssistantTracer.TraceDebug<int, int>((long)this.GetHashCode(), "Now lastUsageTable has {0} entries after we tried to remove {1} entries ", VoiceNotificationFactory.VoiceMessagingEmitter.lastUsageTable.Count, list.Count);
				}
			}

			// Token: 0x040006B5 RID: 1717
			private const int LastUsageTableCleanupThreshold = 1000;

			// Token: 0x040006B6 RID: 1718
			private const int MaxNumberOfEntriesRemoved = 800;

			// Token: 0x040006B7 RID: 1719
			private static readonly TimeSpan minInterval = TimeSpan.FromMinutes(1.0);

			// Token: 0x040006B8 RID: 1720
			private static readonly ConcurrentDictionary<string, ExDateTime> lastUsageTable = new ConcurrentDictionary<string, ExDateTime>();

			// Token: 0x040006B9 RID: 1721
			private readonly string ownerLegacyDN;

			// Token: 0x040006BA RID: 1722
			private readonly Guid tenantGuid;
		}
	}
}
