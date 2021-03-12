using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000F5 RID: 245
	internal class TextNotificationFactory : NotificationFactoryBase
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x00042C66 File Offset: 0x00040E66
		internal override bool IsFeatureEnabled(UserSettings settings)
		{
			return settings.Text != null && settings.Text.Enabled;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00042C7D File Offset: 0x00040E7D
		internal override bool IsReminderEnabled(UserSettings settings)
		{
			return this.IsFeatureEnabled(settings) && settings.Text.TextNotification.CalendarNotificationSettings.ReminderSettings.Enabled;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00042CA4 File Offset: 0x00040EA4
		internal override bool IsSummaryEnabled(UserSettings settings)
		{
			return this.IsFeatureEnabled(settings) && settings.Text.TextNotification.CalendarNotificationSettings.SummarySettings.Enabled;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00042CCC File Offset: 0x00040ECC
		internal override bool IsInterestedInCalendarChangeEvent(UserSettings settings)
		{
			return this.IsFeatureEnabled(settings) && (settings.Text.TextNotification.CalendarNotificationSettings.UpdateSettings.Enabled || settings.Text.TextNotification.CalendarNotificationSettings.ReminderSettings.Enabled);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00042D1C File Offset: 0x00040F1C
		internal override bool IsInterestedInCalendarMeetingEvent(UserSettings settings)
		{
			return this.IsFeatureEnabled(settings) && settings.Text.TextNotification.CalendarNotificationSettings.UpdateSettings.Enabled;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00042D44 File Offset: 0x00040F44
		internal override void LoadUserSettingsFromMailboxSession(MailboxSession session, UserSettings settings)
		{
			MailboxRegionalConfiguration mailboxRegionalConfiguration;
			TextMessagingAccount textMessagingAccount;
			CalendarNotification textNotification;
			StorageWorkingHours workingHours;
			if (Utils.TryLoadRegionalConfiguration(session, out mailboxRegionalConfiguration) && Utils.TryLoadTextMessagingAccount(session, out textMessagingAccount) && Utils.TextMessagingAccountAllowTextNotification(textMessagingAccount) && Utils.TryLoadTextNotification(session, out textNotification) && Utils.TryLoadWorkingHours(session, out workingHours))
			{
				settings.TimeZone = mailboxRegionalConfiguration.TimeZone;
				mailboxRegionalConfiguration.Language = textMessagingAccount.NotificationPreferredCulture;
				settings.Text = new TextNotificationSettings(mailboxRegionalConfiguration, textNotification, workingHours);
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00042DA7 File Offset: 0x00040FA7
		internal override string BuildSettingsItemBody(UserSettings settings)
		{
			return SettingsItemBodyParser.BuildTextSettingsItemBody(settings.Text);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00042DB4 File Offset: 0x00040FB4
		protected override StoreObjectId GetSettingFolderId(SystemMailbox systemMailbox)
		{
			return systemMailbox.TextSettingsFolderId;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00042DBC File Offset: 0x00040FBC
		protected override void UpdateSettingsFromSettingsItemBody(UserSettings settings, string settingsItemBody)
		{
			TextNotificationSettings textNotificationSettings;
			if (this.TryGetTextSettingsFromSettingsItemBody(settingsItemBody, settings.LegacyDN, out textNotificationSettings))
			{
				settings.Text = textNotificationSettings;
				settings.TimeZone = textNotificationSettings.RegionalConfiguration.TimeZone;
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00042DF4 File Offset: 0x00040FF4
		internal override bool TryCreateEmitter(CalendarInfo calendarInfo, MailboxData mailboxData, out ICalendarNotificationEmitter emitter)
		{
			emitter = null;
			if (this.IsFeatureEnabled(mailboxData.Settings) && mailboxData.Settings.Text.TextNotification.CalendarNotificationSettings.ReminderSettings.Enabled)
			{
				if (mailboxData.Settings.Text.TextNotification.CalendarNotificationSettings.ReminderSettings.Duration.NonWorkHoursExcluded && !Utils.InWorkingHours(calendarInfo.StartTime, calendarInfo.EndTime, mailboxData.Settings.Text.WorkingHours))
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Reminder is not in working hours, subj: {0}, user: {1}", calendarInfo.NormalizedSubject, mailboxData.Settings.LegacyDN);
				}
				else
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Create text emitter for calendar subj: {0}, user: {1}", calendarInfo.NormalizedSubject, mailboxData.Settings.LegacyDN);
					emitter = new TextNotificationFactory.TextMessagingEmitter(mailboxData);
				}
			}
			return emitter != null;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00042EE4 File Offset: 0x000410E4
		private bool TryGetTextSettingsFromSettingsItemBody(string settingsItemBody, string legacyDN, out TextNotificationSettings textSettings)
		{
			textSettings = null;
			try
			{
				textSettings = SettingsItemBodyParser.ParseTextSettingsItemBody(settingsItemBody);
			}
			catch (FormatException)
			{
				ExTraceGlobals.SystemMailboxTracer.TraceDebug<string>((long)this.GetHashCode(), "User {0}'s setting item is corrupted, skipped.", legacyDN);
			}
			return textSettings != null;
		}

		// Token: 0x020000F6 RID: 246
		internal class TextMessagingEmitter : ICalendarNotificationEmitter
		{
			// Token: 0x06000A28 RID: 2600 RVA: 0x00042F38 File Offset: 0x00041138
			public TextMessagingEmitter(MailboxData mailboxData)
			{
				this.MailboxData = mailboxData;
			}

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00042F47 File Offset: 0x00041147
			// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00042F4F File Offset: 0x0004114F
			private MailboxData MailboxData { get; set; }

			// Token: 0x06000A2B RID: 2603 RVA: 0x00042F58 File Offset: 0x00041158
			public void Emit(MailboxSession session, CalendarNotificationType type, IList<CalendarInfo> events)
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Text Emitter is invoked, subject: {0} for user {1}", events[0].NormalizedSubject, this.MailboxData.Settings.LegacyDN);
				if (type == CalendarNotificationType.Uninteresting)
				{
					return;
				}
				Emitter emitter = null;
				foreach (Emitter emitter2 in this.MailboxData.Settings.Text.TextNotification.CalendarNotificationSettings.Emitters)
				{
					if (EmitterType.TextMessaging == emitter2.Type)
					{
						emitter = emitter2;
						break;
					}
				}
				if (emitter == null || emitter.PhoneNumbers.Count == 0)
				{
					return;
				}
				MailboxRegionalConfiguration regionalConfiguration = this.MailboxData.Settings.Text.RegionalConfiguration;
				CultureInfo cultureInfo = regionalConfiguration.Language ?? CultureInfo.InvariantCulture;
				string text = regionalConfiguration.TimeFormat;
				if (string.IsNullOrEmpty(text))
				{
					text = cultureInfo.DateTimeFormat.ShortTimePattern;
				}
				string text2 = regionalConfiguration.DateFormat;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = cultureInfo.DateTimeFormat.ShortDatePattern;
				}
				bool flag = false;
				try
				{
					if (session == null)
					{
						ExchangePrincipal mailboxOwner = ExchangePrincipal.FromLocalServerMailboxGuid(this.MailboxData.Settings.GetADSettings(), this.MailboxData.DatabaseGuid, this.MailboxData.MailboxGuid);
						session = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=TBA;Action=Emit");
						flag = true;
					}
					ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)this.GetHashCode(), "Text Emitter is emitting for user {0}", this.MailboxData.Settings.LegacyDN);
					if (CalendarNotificationType.Summary == type)
					{
						CalendarNotificationContentVersion1Point0 calendarNotificationContentVersion1Point = new CalendarNotificationContentVersion1Point0();
						calendarNotificationContentVersion1Point.CalNotifType = type;
						calendarNotificationContentVersion1Point.CalNotifTypeDesc = Strings.notifTypeSummary.ToString(cultureInfo);
						string agendaDateFormat = TextNotificationFactory.TextMessagingEmitter.GetAgendaDateFormat(text2);
						string timeOfStartTime = string.Empty;
						foreach (CalendarInfo calendarInfo in events)
						{
							timeOfStartTime = calendarInfo.StartTime.ToString("H:mm", cultureInfo);
							if (calendarInfo.EndTime.Subtract(calendarInfo.StartTime).Equals(TimeSpan.FromDays(1.0)) && calendarInfo.StartTime.Hour == 0 && calendarInfo.StartTime.Minute == 0)
							{
								timeOfStartTime = string.Empty;
							}
							calendarNotificationContentVersion1Point.CalEvents.Add(new CalendarEvent(cultureInfo.DateTimeFormat.GetDayName(calendarInfo.StartTime.DayOfWeek), calendarInfo.StartTime.ToString(agendaDateFormat, cultureInfo), timeOfStartTime, cultureInfo.DateTimeFormat.GetDayName(calendarInfo.EndTime.DayOfWeek), calendarInfo.EndTime.ToString(agendaDateFormat, cultureInfo), calendarInfo.EndTime.ToString("H:mm", cultureInfo), calendarInfo.NormalizedSubject ?? string.Empty, calendarInfo.Location ?? string.Empty));
						}
						TextNotificationFactory.TextMessagingEmitter.SendTextMessage(session, emitter.PhoneNumbers, calendarNotificationContentVersion1Point.ToString(), CalNotifsCounters.NumberOfAgendasSent);
					}
					else
					{
						foreach (CalendarInfo calendarInfo2 in events)
						{
							CalendarNotificationContentVersion1Point0 calendarNotificationContentVersion1Point2 = new CalendarNotificationContentVersion1Point0();
							calendarNotificationContentVersion1Point2.CalNotifType = type;
							bool flag2 = false;
							ExPerformanceCounter perfcounter;
							switch (type)
							{
							case CalendarNotificationType.Reminder:
								calendarNotificationContentVersion1Point2.CalNotifTypeDesc = Strings.notifTypeReminder.ToString(cultureInfo);
								perfcounter = CalNotifsCounters.NumberOfTextRemindersSent;
								break;
							case CalendarNotificationType.NewUpdate:
								calendarNotificationContentVersion1Point2.CalNotifTypeDesc = Strings.notifTypeNewUpdate.ToString(cultureInfo);
								perfcounter = CalNotifsCounters.NumberOfUpdatesSent;
								flag2 = true;
								break;
							case CalendarNotificationType.ChangedUpdate:
								calendarNotificationContentVersion1Point2.CalNotifTypeDesc = Strings.notifTypeChangedUpdate.ToString(cultureInfo);
								perfcounter = CalNotifsCounters.NumberOfUpdatesSent;
								flag2 = true;
								break;
							case CalendarNotificationType.DeletedUpdate:
								calendarNotificationContentVersion1Point2.CalNotifTypeDesc = Strings.notifTypeDeletedUpdate.ToString(cultureInfo);
								perfcounter = CalNotifsCounters.NumberOfUpdatesSent;
								flag2 = true;
								break;
							default:
								throw new InvalidOperationException("unsupported CalendarNotificationType");
							}
							if (flag2)
							{
								CalendarChangeProcessor.UpdateAverageProcessingLatency(calendarInfo2.CreationRequestTime);
							}
							calendarNotificationContentVersion1Point2.CalEvents.Add(new CalendarEvent(cultureInfo.DateTimeFormat.GetDayName(calendarInfo2.StartTime.DayOfWeek), calendarInfo2.StartTime.ToString(text2, cultureInfo), calendarInfo2.StartTime.ToString(text, cultureInfo), cultureInfo.DateTimeFormat.GetDayName(calendarInfo2.EndTime.DayOfWeek), calendarInfo2.EndTime.ToString(text2, cultureInfo), calendarInfo2.EndTime.ToString(text, cultureInfo), calendarInfo2.NormalizedSubject ?? string.Empty, calendarInfo2.Location ?? string.Empty));
							TextNotificationFactory.TextMessagingEmitter.SendTextMessage(session, emitter.PhoneNumbers, calendarNotificationContentVersion1Point2.ToString(), perfcounter);
						}
					}
					ExTraceGlobals.AssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Text Emitter is emitted, subject: {0}, user: {1}", events[0].NormalizedSubject, this.MailboxData.Settings.LegacyDN);
				}
				finally
				{
					if (flag)
					{
						session.Dispose();
						session = null;
					}
				}
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x000434E8 File Offset: 0x000416E8
			private static void SendTextMessage(MailboxSession session, IList<E164Number> recipients, string content, ExPerformanceCounter perfcounter)
			{
				StoreObjectId destFolderId = session.GetDefaultFolderId(DefaultFolderType.Drafts) ?? session.GetDefaultFolderId(DefaultFolderType.Outbox);
				using (MessageItem messageItem = MessageItem.Create(session, destFolderId))
				{
					messageItem.ClassName = "IPM.Note.Mobile.SMS.Alert.Calendar";
					messageItem.From = new Participant(session.MailboxOwner);
					foreach (E164Number e164Number in recipients)
					{
						messageItem.Recipients.Add(new Participant(null, e164Number.Number, "MOBILE"), RecipientItemType.To);
					}
					using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextPlain))
					{
						textWriter.Write(content);
					}
					messageItem.Send();
					perfcounter.Increment();
					CalNotifsCounters.NumberOfNotificationsSent.Increment();
				}
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x000435E0 File Offset: 0x000417E0
			private static string GetAgendaDateFormat(string dateFormat)
			{
				char value = '/';
				int num = 0;
				while (num < dateFormat.Length && char.IsLetter(dateFormat[num]))
				{
					num++;
				}
				if (num < dateFormat.Length)
				{
					value = dateFormat[num];
				}
				StringBuilder stringBuilder = new StringBuilder(3);
				if (dateFormat.IndexOfAny(new char[]
				{
					'M',
					'm'
				}) < dateFormat.IndexOfAny(new char[]
				{
					'D',
					'd'
				}))
				{
					stringBuilder.Append('M');
					stringBuilder.Append(value);
					stringBuilder.Append('d');
				}
				else
				{
					stringBuilder.Append('d');
					stringBuilder.Append(value);
					stringBuilder.Append('M');
				}
				return stringBuilder.ToString();
			}
		}
	}
}
