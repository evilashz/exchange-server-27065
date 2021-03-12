using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000EE RID: 238
	internal static class SettingsItemBodyParser
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x000418EC File Offset: 0x0003FAEC
		internal static string BuildTextSettingsItemBody(TextNotificationSettings settings)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.AppendLine("V1.2");
			MailboxRegionalConfiguration regionalConfiguration = settings.RegionalConfiguration;
			stringBuilder.AppendLine(regionalConfiguration.TimeZone.ExTimeZone.Id);
			stringBuilder.AppendLine((regionalConfiguration.Language != null) ? regionalConfiguration.Language.LCID.ToString() : "0");
			stringBuilder.AppendLine(regionalConfiguration.TimeFormat);
			stringBuilder.AppendLine(regionalConfiguration.DateFormat);
			CalendarNotification textNotification = settings.TextNotification;
			stringBuilder.AppendLine(textNotification.MeetingReminderNotification ? "1" : "0");
			stringBuilder.AppendLine(textNotification.MeetingReminderSendDuringWorkHour ? "1" : "0");
			stringBuilder.AppendLine(textNotification.NextDays.ToString());
			stringBuilder.AppendLine(textNotification.CalendarUpdateNotification ? "1" : "0");
			stringBuilder.AppendLine(textNotification.CalendarUpdateSendDuringWorkHour ? "1" : "0");
			stringBuilder.AppendLine(textNotification.DailyAgendaNotification ? "1" : "0");
			stringBuilder.AppendLine(((int)textNotification.DailyAgendaNotificationSendTime.TotalSeconds).ToString());
			stringBuilder.AppendLine(textNotification.TextMessagingPhoneNumber.ToString());
			StorageWorkingHours workingHours = settings.WorkingHours;
			stringBuilder.AppendLine(workingHours.TimeZone.Id);
			stringBuilder.AppendLine(((int)workingHours.DaysOfWeek).ToString());
			stringBuilder.AppendLine(workingHours.StartTimeInMinutes.ToString());
			stringBuilder.AppendLine(workingHours.EndTimeInMinutes.ToString());
			SettingsItemBodyParser.CheckSettingsItemBodyLength(stringBuilder.Length);
			return stringBuilder.ToString();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00041AB4 File Offset: 0x0003FCB4
		internal static string BuildVoiceSettingsItemBody(VoiceNotificationSettings settings)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.AppendLine("V1.0");
			stringBuilder.AppendLine(settings.TimeZone.ExTimeZone.Id);
			stringBuilder.AppendLine(settings.Enabled ? "1" : "0");
			SettingsItemBodyParser.CheckSettingsItemBodyLength(stringBuilder.Length);
			return stringBuilder.ToString();
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00041B1C File Offset: 0x0003FD1C
		internal static VoiceNotificationSettings ParseVoiceSettingsItemBody(string body)
		{
			VoiceNotificationSettings result;
			using (StringReader stringReader = new StringReader(body))
			{
				if (!string.Equals(SettingsItemBodyParser.GetNextLine(stringReader), "V1.0"))
				{
					SettingsItemBodyParser.Tracer.TraceDebug(0L, "Unknown user  voice settings version, skipped");
					throw new FormatException();
				}
				ExTimeZoneValue timeZone = new ExTimeZoneValue(SettingsItemBodyParser.GetNextTimeZone(stringReader));
				bool nextBoolean = SettingsItemBodyParser.GetNextBoolean(stringReader);
				VoiceNotificationSettings voiceNotificationSettings = new VoiceNotificationSettings(nextBoolean, timeZone);
				result = voiceNotificationSettings;
			}
			return result;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00041B98 File Offset: 0x0003FD98
		internal static TextNotificationSettings ParseTextSettingsItemBody(string body)
		{
			TextNotificationSettings result;
			using (StringReader stringReader = new StringReader(body))
			{
				if (SettingsItemBodyParser.GetNextLine(stringReader) != "V1.2")
				{
					SettingsItemBodyParser.Tracer.TraceDebug(0L, "Unknown user settings version, skipped");
					throw new FormatException();
				}
				MailboxRegionalConfiguration mailboxRegionalConfiguration = new MailboxRegionalConfiguration();
				mailboxRegionalConfiguration.TimeZone = new ExTimeZoneValue(SettingsItemBodyParser.GetNextTimeZone(stringReader));
				int nextInteger = SettingsItemBodyParser.GetNextInteger(stringReader);
				if (nextInteger != 0)
				{
					mailboxRegionalConfiguration.Language = new CultureInfo(nextInteger);
				}
				mailboxRegionalConfiguration.TimeFormat = SettingsItemBodyParser.GetNextLine(stringReader);
				mailboxRegionalConfiguration.DateFormat = SettingsItemBodyParser.GetNextLine(stringReader);
				CalendarNotification calendarNotification = new CalendarNotification();
				calendarNotification.MeetingReminderNotification = SettingsItemBodyParser.GetNextBoolean(stringReader);
				calendarNotification.MeetingReminderSendDuringWorkHour = SettingsItemBodyParser.GetNextBoolean(stringReader);
				calendarNotification.NextDays = SettingsItemBodyParser.GetNextInteger(stringReader);
				calendarNotification.CalendarUpdateNotification = SettingsItemBodyParser.GetNextBoolean(stringReader);
				calendarNotification.CalendarUpdateSendDuringWorkHour = SettingsItemBodyParser.GetNextBoolean(stringReader);
				calendarNotification.DailyAgendaNotification = SettingsItemBodyParser.GetNextBoolean(stringReader);
				calendarNotification.DailyAgendaNotificationSendTime = TimeSpan.FromSeconds((double)SettingsItemBodyParser.GetNextInteger(stringReader));
				E164Number textMessagingPhoneNumber;
				if (!E164Number.TryParse(SettingsItemBodyParser.GetNextLine(stringReader), out textMessagingPhoneNumber))
				{
					SettingsItemBodyParser.Tracer.TraceDebug(0L, "Invalid phone number, skipped");
					throw new FormatException();
				}
				calendarNotification.TextMessagingPhoneNumber = textMessagingPhoneNumber;
				ExTimeZone nextTimeZone = SettingsItemBodyParser.GetNextTimeZone(stringReader);
				int nextInteger2 = SettingsItemBodyParser.GetNextInteger(stringReader);
				int nextInteger3 = SettingsItemBodyParser.GetNextInteger(stringReader);
				int nextInteger4 = SettingsItemBodyParser.GetNextInteger(stringReader);
				StorageWorkingHours workingHours = StorageWorkingHours.Create(nextTimeZone, nextInteger2, nextInteger3, nextInteger4);
				result = new TextNotificationSettings(mailboxRegionalConfiguration, calendarNotification, workingHours);
			}
			return result;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00041D0C File Offset: 0x0003FF0C
		private static void CheckSettingsItemBodyLength(int length)
		{
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00041D10 File Offset: 0x0003FF10
		private static string GetNextLine(StringReader reader)
		{
			string text = reader.ReadLine();
			if (string.IsNullOrEmpty(text))
			{
				throw new FormatException();
			}
			return text;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00041D34 File Offset: 0x0003FF34
		private static bool GetNextBoolean(StringReader reader)
		{
			string nextLine = SettingsItemBodyParser.GetNextLine(reader);
			if (nextLine == "1")
			{
				return true;
			}
			if (nextLine == "0")
			{
				return false;
			}
			throw new FormatException();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00041D6C File Offset: 0x0003FF6C
		private static int GetNextInteger(StringReader reader)
		{
			string nextLine = SettingsItemBodyParser.GetNextLine(reader);
			int result;
			if (int.TryParse(nextLine, out result))
			{
				return result;
			}
			throw new FormatException();
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00041D94 File Offset: 0x0003FF94
		private static ExTimeZone GetNextTimeZone(StringReader reader)
		{
			ExTimeZone exTimeZone;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(SettingsItemBodyParser.GetNextLine(reader), out exTimeZone))
			{
				SettingsItemBodyParser.Tracer.TraceDebug(0L, "Unknown time zone, change to default");
				exTimeZone = ((ExTimeZoneValue)WorkingHoursSchema.WorkingHoursTimeZone.DefaultValue).ExTimeZone;
			}
			return exTimeZone;
		}

		// Token: 0x04000689 RID: 1673
		private const int SettingItemBodyMaxLength = 256;

		// Token: 0x0400068A RID: 1674
		private const string SettingsItemBodyVersion = "V1.2";

		// Token: 0x0400068B RID: 1675
		private static readonly Trace Tracer = ExTraceGlobals.UserSettingsTracer;
	}
}
