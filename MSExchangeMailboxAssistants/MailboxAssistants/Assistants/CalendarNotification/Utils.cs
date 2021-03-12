using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000FB RID: 251
	internal class Utils
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x0004438C File Offset: 0x0004258C
		internal static ADRecipient GetADRecipient(Guid tenantGuid, string userLegacyDN)
		{
			ADSessionSettings sessionSettings = (tenantGuid == Guid.Empty) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromExternalDirectoryOrganizationId(tenantGuid);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 48, "GetADRecipient", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\CalendarNotification\\Utils.cs");
			ADRecipient result = null;
			try
			{
				result = tenantOrRootOrgRecipientSession.FindByLegacyExchangeDN(userLegacyDN);
			}
			catch (NonUniqueRecipientException)
			{
				Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "User {0} AD object is not unique", userLegacyDN);
			}
			catch (ADTransientException)
			{
				Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "Due to ADTransientException we can't get AD object for User {0} ", userLegacyDN);
			}
			catch (ADOperationException)
			{
				Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "Due to ADOperationException we can't get AD object for User {0} ", userLegacyDN);
			}
			return result;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0004446C File Offset: 0x0004266C
		internal static bool TryLoadRegionalConfiguration(MailboxSession session, out MailboxRegionalConfiguration regionalConfiguration)
		{
			regionalConfiguration = null;
			ADUser aduser = Utils.GetADRecipient(UserSettings.GetExternalDirectoryOrganizationId(session), session.MailboxOwnerLegacyDN) as ADUser;
			if (aduser == null)
			{
				Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "Can't find AD recipient by mailbox owner's DN {0}", session.MailboxOwnerLegacyDN);
				return false;
			}
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(aduser);
			mailboxStoreTypeProvider.MailboxSession = session;
			try
			{
				regionalConfiguration = (MailboxRegionalConfiguration)mailboxStoreTypeProvider.Read<MailboxRegionalConfiguration>(session.MailboxOwner.ObjectId);
			}
			catch (FormatException arg)
			{
				Utils.Tracer.TraceError<ADObjectId, FormatException>((long)typeof(MailboxData).GetHashCode(), "MailboxRegionalConfiguration cannot be retrieved for user {0}. Error: {1}", session.MailboxOwner.ObjectId, arg);
				return false;
			}
			if (regionalConfiguration == null || null == regionalConfiguration.TimeZone || regionalConfiguration.TimeZone.ExTimeZone == null)
			{
				Utils.Tracer.TraceError<ADObjectId>((long)typeof(MailboxData).GetHashCode(), "User {0} doesn't set a valid RegionalConfiguration or TimeZone", session.MailboxOwner.ObjectId);
				return false;
			}
			return true;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00044578 File Offset: 0x00042778
		internal static bool TryLoadTextMessagingAccount(MailboxSession session, out TextMessagingAccount textMessagingAccount)
		{
			textMessagingAccount = null;
			using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(session))
			{
				textMessagingAccount = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(session.MailboxOwner.ObjectId);
			}
			return textMessagingAccount != null;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000445CC File Offset: 0x000427CC
		internal static bool TryLoadTextNotification(MailboxSession session, out CalendarNotification textNotification)
		{
			textNotification = null;
			using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(session))
			{
				textNotification = (CalendarNotification)versionedXmlDataProvider.Read<CalendarNotification>(session.MailboxOwner.ObjectId);
			}
			return textNotification != null;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00044620 File Offset: 0x00042820
		internal static bool TryLoadWorkingHours(MailboxSession session, out StorageWorkingHours workingHours)
		{
			workingHours = null;
			try
			{
				StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Calendar);
				if (defaultFolderId != null)
				{
					workingHours = StorageWorkingHours.LoadFrom(session, defaultFolderId);
				}
				else
				{
					Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "User {0}'s working hours data could not be retrieved - folderId is null", session.MailboxOwnerLegacyDN);
				}
			}
			catch (CorruptDataException)
			{
				Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "User {0}'s working hours data is corrupted", session.MailboxOwnerLegacyDN);
				workingHours = StorageWorkingHours.Create(((ExTimeZoneValue)WorkingHoursSchema.WorkingHoursTimeZone.DefaultValue).ExTimeZone, (int)WorkingHoursSchema.WorkDays.DefaultValue, (int)((TimeSpan)WorkingHoursSchema.WorkingHoursStartTime.DefaultValue).TotalMinutes, (int)((TimeSpan)WorkingHoursSchema.WorkingHoursEndTime.DefaultValue).TotalMinutes);
			}
			return workingHours != null;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0004470C File Offset: 0x0004290C
		internal static UserSettings LoadUserSettingsFromMailboxSession(MailboxSession session)
		{
			return NotificationFactories.Instance.LoadUserSettingsFromMailboxSession(session);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0004471C File Offset: 0x0004291C
		internal static bool VoiceNotificationIsEnabled(MailboxSession session)
		{
			try
			{
				using (UserConfiguration mailboxConfiguration = session.UserConfigurationManager.GetMailboxConfiguration("Um.General", UserConfigurationTypes.Dictionary))
				{
					IDictionary dictionary = mailboxConfiguration.GetDictionary();
					if (dictionary.Contains("VoiceNotificationStatus"))
					{
						VoiceNotificationStatus voiceNotificationStatus = (VoiceNotificationStatus)dictionary["VoiceNotificationStatus"];
						return voiceNotificationStatus == VoiceNotificationStatus.Enabled;
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "User {0}'s voice notification data can't be found", session.MailboxOwnerLegacyDN);
			}
			catch (CorruptDataException)
			{
				Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "User {0}'s voice notification data is corrupted", session.MailboxOwnerLegacyDN);
			}
			catch (InvalidOperationException)
			{
				Utils.Tracer.TraceError<string>((long)typeof(MailboxData).GetHashCode(), "Unable to get user {0}'s voice notification data", session.MailboxOwnerLegacyDN);
			}
			return false;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00044828 File Offset: 0x00042A28
		internal static bool TextMessagingAccountAllowTextNotification(TextMessagingAccount textMessagingAccount)
		{
			return !textMessagingAccount.EasEnabled && textMessagingAccount.NotificationPhoneNumberVerified;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0004483A File Offset: 0x00042A3A
		internal static bool TextNotificationIsEnabled(CalendarNotification textNotification)
		{
			return (textNotification.CalendarUpdateNotification || textNotification.DailyAgendaNotification || textNotification.MeetingReminderNotification) && textNotification.TextMessagingPhoneNumber != null;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00044864 File Offset: 0x00042A64
		internal static bool AreInterestedFieldsEqual(CalendarNotification value1, CalendarNotification value2)
		{
			bool result;
			if (Utils.CheckMembers(value1, value2, out result))
			{
				return value1.CalendarUpdateNotification == value2.CalendarUpdateNotification && value1.CalendarUpdateSendDuringWorkHour == value2.CalendarUpdateSendDuringWorkHour && value1.DailyAgendaNotification == value2.DailyAgendaNotification && value1.DailyAgendaNotificationSendTime == value2.DailyAgendaNotificationSendTime && value1.MeetingReminderNotification == value2.MeetingReminderNotification && value1.MeetingReminderSendDuringWorkHour == value2.MeetingReminderSendDuringWorkHour && value1.NextDays == value2.NextDays && value1.TextMessagingPhoneNumber == value2.TextMessagingPhoneNumber;
			}
			return result;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000448F8 File Offset: 0x00042AF8
		internal static bool AreInterestedFieldsEqual(StorageWorkingHours value1, StorageWorkingHours value2)
		{
			bool result;
			if (Utils.CheckMembers(value1, value2, out result))
			{
				return value1.TimeZone == value2.TimeZone && value1.DaysOfWeek == value2.DaysOfWeek && value1.StartTimeInMinutes == value2.StartTimeInMinutes && value1.EndTimeInMinutes == value2.EndTimeInMinutes;
			}
			return result;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0004494C File Offset: 0x00042B4C
		internal static bool AreInterestedFieldsEqual(MailboxRegionalConfiguration value1, MailboxRegionalConfiguration value2)
		{
			bool result;
			if (Utils.CheckMembers(value1, value2, out result))
			{
				return value1.TimeZone == value2.TimeZone && value1.Language.Equals(value2.Language) && string.Equals(value1.TimeFormat, value2.TimeFormat, StringComparison.InvariantCulture) && string.Equals(value1.DateFormat, value2.DateFormat, StringComparison.InvariantCulture);
			}
			return result;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000449B4 File Offset: 0x00042BB4
		internal static bool AreInterestedFieldsEqual(TextMessagingAccount value1, TextMessagingAccount value2)
		{
			bool result;
			if (Utils.CheckMembers(value1, value2, out result))
			{
				return value1.EasEnabled == value2.EasEnabled && value1.NotificationPhoneNumberVerified == value2.NotificationPhoneNumberVerified && value1.MobileOperatorId == value2.MobileOperatorId;
			}
			return result;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000449FC File Offset: 0x00042BFC
		internal static bool AreInterestedFieldsEqual(VoiceNotificationSettings voiceSettings1, VoiceNotificationSettings voiceSettings2)
		{
			bool result;
			if (Utils.CheckMembers(voiceSettings1, voiceSettings2, out result))
			{
				return voiceSettings1.TimeZone == voiceSettings2.TimeZone && voiceSettings1.Enabled == voiceSettings2.Enabled;
			}
			return result;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00044A3C File Offset: 0x00042C3C
		internal static bool AreInterestedFieldsEqual(TextNotificationSettings textSettings1, TextNotificationSettings textSettings2)
		{
			bool result;
			if (Utils.CheckMembers(textSettings1, textSettings2, out result))
			{
				return Utils.AreInterestedFieldsEqual(textSettings1.RegionalConfiguration, textSettings2.RegionalConfiguration) && Utils.AreInterestedFieldsEqual(textSettings1.TextNotification, textSettings2.TextNotification) && Utils.AreInterestedFieldsEqual(textSettings1.WorkingHours, textSettings2.WorkingHours);
			}
			return result;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00044A90 File Offset: 0x00042C90
		internal static bool AreInterestedFieldsEqual(UserSettings settings1, UserSettings settings2)
		{
			bool result;
			if (Utils.CheckMembers(settings1, settings2, out result))
			{
				return Utils.AreInterestedFieldsEqual(settings1.Text, settings2.Text) && Utils.AreInterestedFieldsEqual(settings1.Voice, settings2.Voice);
			}
			return result;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00044AD0 File Offset: 0x00042CD0
		internal static bool InWorkingHours(ExDateTime startTime, ExDateTime endTime, StorageWorkingHours workingHours)
		{
			if (startTime > endTime)
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<ExDateTime, ExDateTime>((long)typeof(CalendarChangeProcessor).GetHashCode(), "processing invalid start time / end time. start: {0}, end:{1}", startTime, endTime);
				return false;
			}
			ExDateTime d = workingHours.TimeZone.ConvertDateTime(startTime);
			ExDateTime t = workingHours.TimeZone.ConvertDateTime(endTime);
			ExDateTime exDateTime = d + TimeSpan.FromDays(1.0);
			while (exDateTime < t)
			{
				if ((1 << (int)exDateTime.DayOfWeek & (int)workingHours.DaysOfWeek) != 0)
				{
					return true;
				}
				exDateTime += TimeSpan.FromDays(1.0);
			}
			bool flag = 0 != (1 << (int)d.DayOfWeek & (int)workingHours.DaysOfWeek);
			bool flag2 = 0 != (1 << (int)t.DayOfWeek & (int)workingHours.DaysOfWeek);
			if (!flag && !flag2)
			{
				return false;
			}
			bool flag3 = d.Date == t.Date;
			List<Interval<long>> list = new List<Interval<long>>();
			List<Interval<long>> list2 = new List<Interval<long>>();
			long ticks = d.TimeOfDay.Ticks;
			long ticks2 = t.TimeOfDay.Ticks;
			if (flag3)
			{
				list.Add(new Interval<long>(ticks, false, ticks2, ticks < ticks2));
			}
			else
			{
				if (flag)
				{
					list.Add(new Interval<long>(ticks, false, TimeSpan.FromDays(1.0).Ticks, false));
				}
				if (flag2)
				{
					list.Add(new Interval<long>(0L, false, ticks2, 0L < ticks2));
				}
			}
			long ticks3 = TimeSpan.FromMinutes((double)workingHours.StartTimeInMinutes).Ticks;
			long ticks4 = TimeSpan.FromMinutes((double)workingHours.EndTimeInMinutes).Ticks;
			if (ticks3 <= ticks4)
			{
				list2.Add(new Interval<long>(ticks3, false, ticks4, ticks3 < ticks4));
			}
			else
			{
				list2.Add(new Interval<long>(ticks3, false, (long)((int)TimeSpan.FromDays(1.0).TotalMinutes), false));
				list2.Add(new Interval<long>(0L, false, ticks4, 0L < ticks4));
			}
			foreach (Interval<long> interval in list)
			{
				foreach (Interval<long> other in list2)
				{
					if (interval.IsOverlapped(other))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00044D70 File Offset: 0x00042F70
		private static bool CheckMembers(object object1, object object2, out bool equal)
		{
			equal = false;
			if (object.ReferenceEquals(object1, object2))
			{
				equal = true;
				return false;
			}
			if (object1 == null || object2 == null)
			{
				equal = false;
				return false;
			}
			return true;
		}

		// Token: 0x040006B3 RID: 1715
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;
	}
}
