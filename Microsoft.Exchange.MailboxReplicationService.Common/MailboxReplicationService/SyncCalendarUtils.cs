using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase;
using Microsoft.Exchange.Connections.Eas.Model.Common.Email;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSync;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase;
using Microsoft.Exchange.Connections.Eas.Model.Request.Calendar;
using Microsoft.Exchange.Connections.Eas.Model.Response.Calendar;
using Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001A1 RID: 417
	internal static class SyncCalendarUtils
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0002328D File Offset: 0x0002148D
		public static PropertyTag GlobalObjectId
		{
			get
			{
				return SyncCalendarUtils.GlobalObjectIdPropertyTag;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00023294 File Offset: 0x00021494
		public static PropertyTag Start
		{
			get
			{
				return SyncCalendarUtils.StartPropertyTag;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0002329B File Offset: 0x0002149B
		public static PropertyTag End
		{
			get
			{
				return SyncCalendarUtils.EndPropertyTag;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x000232A2 File Offset: 0x000214A2
		public static PropertyTag AllDayEvent
		{
			get
			{
				return SyncCalendarUtils.AllDayEventPropertyTag;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000232A9 File Offset: 0x000214A9
		public static PropertyTag BusyStatus
		{
			get
			{
				return SyncCalendarUtils.BusyStatusPropertyTag;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x000232B0 File Offset: 0x000214B0
		public static PropertyTag Location
		{
			get
			{
				return SyncCalendarUtils.LocationPropertyTag;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x000232B7 File Offset: 0x000214B7
		public static PropertyTag Reminder
		{
			get
			{
				return SyncCalendarUtils.ReminderPropertyTag;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x000232BE File Offset: 0x000214BE
		public static PropertyTag TimeZoneBlob
		{
			get
			{
				return SyncCalendarUtils.TimeZoneBlobPropertyTag;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x000232C5 File Offset: 0x000214C5
		public static PropertyTag Sensitivity
		{
			get
			{
				return SyncCalendarUtils.SensitivityPropertyTag;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x000232CC File Offset: 0x000214CC
		public static PropertyTag SentRepresentingName
		{
			get
			{
				return SyncCalendarUtils.SentRepresentingNamePropertyTag;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x000232D3 File Offset: 0x000214D3
		public static PropertyTag SentRepresentingEmailAddress
		{
			get
			{
				return SyncCalendarUtils.SentRepresentingEmailAddressPropertyTag;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x000232DA File Offset: 0x000214DA
		public static PropertyTag MeetingStatus
		{
			get
			{
				return SyncCalendarUtils.MeetingStatusPropertyTag;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x000232E1 File Offset: 0x000214E1
		public static PropertyTag AppointmentRecurrenceBlob
		{
			get
			{
				return SyncCalendarUtils.AppointmentRecurrenceBlobPropertyTag;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x000232E8 File Offset: 0x000214E8
		public static PropertyTag ResponseType
		{
			get
			{
				return SyncCalendarUtils.ResponseTypePropertyTag;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x000232EF File Offset: 0x000214EF
		public static PropertyTag RecipientTrackStatus
		{
			get
			{
				return SyncCalendarUtils.RecipientTrackStatusPropertyTag;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x000232F6 File Offset: 0x000214F6
		public static PropertyTag RecipientType
		{
			get
			{
				return SyncCalendarUtils.RecipientTypePropertyTag;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000232FD File Offset: 0x000214FD
		public static PropertyTag EmailAddress
		{
			get
			{
				return SyncCalendarUtils.EmailAddressPropertyTag;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00023304 File Offset: 0x00021504
		public static PropertyTag DisplayName
		{
			get
			{
				return SyncCalendarUtils.DisplayNamePropertyTag;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0002330B File Offset: 0x0002150B
		public static PropertyTag RowId
		{
			get
			{
				return SyncCalendarUtils.RowIdPropertyTag;
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00023314 File Offset: 0x00021514
		internal static ExDateTime ToExDateTime(string value)
		{
			ExDateTime result;
			if (!ExDateTime.TryParseExact(value, "yyyyMMdd\\THHmmss\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
			{
				throw new EasFetchFailedPermanentException("ToExDateTime: " + value);
			}
			return result;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0002334C File Offset: 0x0002154C
		internal static string ToStringDateTime(ExDateTime value)
		{
			return value.ToUtc().ToString("yyyyMMdd\\THHmmss\\Z");
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0002336D File Offset: 0x0002156D
		internal static ExDateTime ToUtcExDateTime(string value)
		{
			return ExTimeZone.UtcTimeZone.ConvertDateTime(SyncCalendarUtils.ToExDateTime(value));
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00023380 File Offset: 0x00021580
		internal static ExTimeZone ToExTimeZone(string timezone)
		{
			byte[] array = Convert.FromBase64String(timezone);
			if (array.Length != 172)
			{
				throw new EasFetchFailedPermanentException("ToExTimeZone: " + timezone);
			}
			int num = 0;
			char[] array2 = new char[32];
			REG_TIMEZONE_INFO regInfo;
			regInfo.Bias = BitConverter.ToInt32(array, num);
			num += 4;
			for (int i = 0; i < 32; i++)
			{
				array2[i] = BitConverter.ToChar(array, num);
				num += 2;
			}
			string keyName = new string(array2);
			regInfo.StandardDate.Year = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.StandardDate.Month = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.StandardDate.DayOfWeek = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.StandardDate.Day = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.StandardDate.Hour = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.StandardDate.Minute = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.StandardDate.Second = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.StandardDate.Milliseconds = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.StandardBias = BitConverter.ToInt32(array, num);
			num += 4;
			for (int j = 0; j < 32; j++)
			{
				array2[j] = BitConverter.ToChar(array, num);
				num += 2;
			}
			regInfo.DaylightDate.Year = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.DaylightDate.Month = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.DaylightDate.DayOfWeek = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.DaylightDate.Day = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.DaylightDate.Hour = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.DaylightDate.Minute = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.DaylightDate.Second = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.DaylightDate.Milliseconds = (ushort)BitConverter.ToInt16(array, num);
			num += 2;
			regInfo.DaylightBias = BitConverter.ToInt32(array, num);
			num += 4;
			if (num != 172)
			{
				return ExTimeZone.CurrentTimeZone;
			}
			ExTimeZone result;
			try
			{
				result = TimeZoneHelper.CreateExTimeZoneFromRegTimeZoneInfo(regInfo, keyName);
			}
			catch (InvalidTimeZoneException innerException)
			{
				throw new EasFetchFailedPermanentException("ToExTimeZone: " + timezone, innerException);
			}
			return result;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x000235F8 File Offset: 0x000217F8
		public static string ToTimeZoneString(ExTimeZone srcTimeZone)
		{
			if (srcTimeZone == null)
			{
				throw new EasSyncFailedPermanentException("Null source time zone");
			}
			byte[] array = new byte[172];
			int num = 0;
			REG_TIMEZONE_INFO reg_TIMEZONE_INFO = TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(srcTimeZone);
			byte[] bytes = BitConverter.GetBytes(reg_TIMEZONE_INFO.Bias);
			bytes.CopyTo(array, num);
			num += Marshal.SizeOf(reg_TIMEZONE_INFO.Bias);
			char[] array2 = srcTimeZone.LocalizableDisplayName.ToString(CultureInfo.InvariantCulture).ToCharArray();
			int num2 = Math.Min(array2.Length, 32);
			for (int i = 0; i < num2; i++)
			{
				BitConverter.GetBytes(array2[i]).CopyTo(array, num);
				num += 2;
			}
			for (int j = num2; j < 32; j++)
			{
				array[num++] = 0;
				array[num++] = 0;
			}
			BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardDate.Year).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardDate.Month).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardDate.DayOfWeek).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardDate.Day).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardDate.Hour).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardDate.Minute).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardDate.Second).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardDate.Milliseconds).CopyTo(array, num);
			num += 2;
			bytes = BitConverter.GetBytes(reg_TIMEZONE_INFO.StandardBias);
			bytes.CopyTo(array, num);
			num += Marshal.SizeOf(reg_TIMEZONE_INFO.StandardBias);
			array2 = srcTimeZone.LocalizableDisplayName.ToString(CultureInfo.InvariantCulture).ToCharArray();
			num2 = Math.Min(array2.Length, 32);
			for (int k = 0; k < num2; k++)
			{
				BitConverter.GetBytes(array2[k]).CopyTo(array, num);
				num += 2;
			}
			for (int l = num2; l < 32; l++)
			{
				array[num++] = 0;
				array[num++] = 0;
			}
			BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightDate.Year).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightDate.Month).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightDate.DayOfWeek).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightDate.Day).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightDate.Hour).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightDate.Minute).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightDate.Second).CopyTo(array, num);
			num += 2;
			BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightDate.Milliseconds).CopyTo(array, num);
			num += 2;
			bytes = BitConverter.GetBytes(reg_TIMEZONE_INFO.DaylightBias);
			bytes.CopyTo(array, num);
			num += Marshal.SizeOf(reg_TIMEZONE_INFO.DaylightBias);
			if (num != 172)
			{
				throw new EasSyncFailedPermanentException("Failed to convert Timezone into bytes. Length=" + num);
			}
			return Convert.ToBase64String(array);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00023964 File Offset: 0x00021B64
		public static ApplicationData ConvertEventToAppData(Event theEvent, IList<Event> exceptionalEvents, IList<string> deletedOccurrences, UserSmtpAddress userSmtpAddress)
		{
			ExTimeZone utcTimeZone;
			if (theEvent.IntendedStartTimeZoneId == "tzone://Microsoft/Utc" || theEvent.IntendedStartTimeZoneId == null)
			{
				utcTimeZone = ExTimeZone.UtcTimeZone;
			}
			else
			{
				ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(theEvent.IntendedStartTimeZoneId, out utcTimeZone);
			}
			bool flag = false;
			byte b = 0;
			if (theEvent.HasAttendees)
			{
				b |= 1;
				flag = true;
			}
			if (theEvent.IsCancelled)
			{
				b |= 4;
			}
			if (((IEventInternal)theEvent).IsReceived)
			{
				b |= 2;
			}
			ApplicationData applicationData = new ApplicationData();
			SyncCalendarUtils.CopyCommonEventData(applicationData, theEvent, flag, userSmtpAddress);
			applicationData.TimeZone = ((utcTimeZone != null) ? SyncCalendarUtils.ToTimeZoneString(utcTimeZone) : null);
			applicationData.MeetingStatus = new byte?(b);
			applicationData.Uid = (string.IsNullOrEmpty(((IEventInternal)theEvent).GlobalObjectId) ? null : new GlobalObjectId(((IEventInternal)theEvent).GlobalObjectId).Uid);
			applicationData.Recurrence = SyncCalendarUtils.GetRecurrenceData(theEvent.PatternedRecurrence);
			applicationData.Exceptions = SyncCalendarUtils.GetExceptionData(exceptionalEvents, deletedOccurrences, flag, theEvent.Start, userSmtpAddress);
			if (flag)
			{
				applicationData.OrganizerEmail = theEvent.Organizer.EmailAddress;
				applicationData.OrganizerName = theEvent.Organizer.Name;
				applicationData.ResponseRequested = new byte?(Convert.ToByte(theEvent.ResponseRequested));
			}
			return applicationData;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00023A98 File Offset: 0x00021C98
		public static byte[] ToRecurrenceBlob(Properties easCalendarItem, ExDateTime start, ExDateTime end, ExTimeZone targetTimeZone)
		{
			Microsoft.Exchange.Connections.Eas.Model.Response.Calendar.Recurrence recurrence = easCalendarItem.Recurrence;
			RecurrencePattern pattern = SyncCalendarUtils.CreateRecurrencePattern(recurrence);
			RecurrenceRange range = SyncCalendarUtils.CreateRecurrenceRange(start, recurrence);
			ExDateTime dt = targetTimeZone.ConvertDateTime(start);
			ExDateTime dt2 = targetTimeZone.ConvertDateTime(end);
			TimeSpan startOffset = dt - dt.Date;
			TimeSpan endOffset = dt2 - dt2.Date;
			InternalRecurrence internalRecurrence = new InternalRecurrence(pattern, range, null, targetTimeZone, ExTimeZone.UtcTimeZone, startOffset, endOffset);
			if (easCalendarItem.Exceptions != null)
			{
				foreach (Microsoft.Exchange.Connections.Eas.Model.Response.Calendar.Exception ex in easCalendarItem.Exceptions)
				{
					ExDateTime originalStartTime = SyncCalendarUtils.ToUtcExDateTime(ex.ExceptionStartTime);
					ExDateTime date = originalStartTime.Date;
					if (ex.Deleted)
					{
						internalRecurrence.TryDeleteOccurrence(date);
					}
					else
					{
						ModificationType modificationType = (ModificationType)0;
						MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
						memoryPropertyBag.SetAllPropertiesLoaded();
						if (ex.Subject != easCalendarItem.CalendarSubject)
						{
							modificationType |= ModificationType.Subject;
							memoryPropertyBag[ItemSchema.Subject] = ex.Subject;
						}
						if (ex.Reminder != easCalendarItem.Reminder)
						{
							modificationType |= ModificationType.ReminderDelta;
							memoryPropertyBag[ItemSchema.ReminderMinutesBeforeStartInternal] = ex.Reminder;
						}
						if (ex.Location != easCalendarItem.Location)
						{
							modificationType |= ModificationType.Location;
							memoryPropertyBag[CalendarItemBaseSchema.Location] = ex.Location;
						}
						if (ex.BusyStatus != easCalendarItem.BusyStatus)
						{
							modificationType |= ModificationType.BusyStatus;
							memoryPropertyBag[CalendarItemBaseSchema.FreeBusyStatus] = ex.BusyStatus;
						}
						if (ex.AllDayEvent != easCalendarItem.AllDayEvent)
						{
							modificationType |= ModificationType.SubType;
							memoryPropertyBag[CalendarItemBaseSchema.MapiIsAllDayEvent] = ex.AllDayEvent;
						}
						ExDateTime startTime = targetTimeZone.ConvertDateTime(SyncCalendarUtils.ToUtcExDateTime(ex.StartTime));
						ExDateTime endTime = targetTimeZone.ConvertDateTime(SyncCalendarUtils.ToUtcExDateTime(ex.EndTime));
						ExceptionInfo exceptionInfo = new ExceptionInfo(null, date, originalStartTime, startTime, endTime, modificationType, memoryPropertyBag);
						internalRecurrence.ModifyOccurrence(exceptionInfo);
					}
				}
			}
			return internalRecurrence.ToByteArray();
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00023CD4 File Offset: 0x00021ED4
		public static Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Recurrence GetRecurrenceData(PatternedRecurrence recurrence)
		{
			if (recurrence == null)
			{
				return null;
			}
			Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Recurrence recurrence2 = new Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Recurrence();
			recurrence2.Interval = new ushort?((ushort)recurrence.Pattern.Interval);
			RecurrencePatternType type = recurrence.Pattern.Type;
			switch (type)
			{
			case RecurrencePatternType.Daily:
			{
				DailyRecurrencePattern dailyRecurrencePattern = (DailyRecurrencePattern)recurrence.Pattern;
				recurrence2.Type = 0;
				break;
			}
			case RecurrencePatternType.Weekly:
			{
				WeeklyRecurrencePattern weeklyRecurrencePattern = (WeeklyRecurrencePattern)recurrence.Pattern;
				recurrence2.Type = 1;
				recurrence2.DayOfWeek = new ushort?(SyncCalendarUtils.GetDayOfWeekValue(weeklyRecurrencePattern.DaysOfWeek));
				break;
			}
			case RecurrencePatternType.AbsoluteMonthly:
			{
				AbsoluteMonthlyRecurrencePattern absoluteMonthlyRecurrencePattern = (AbsoluteMonthlyRecurrencePattern)recurrence.Pattern;
				recurrence2.Type = 2;
				recurrence2.DayOfMonth = new byte?((byte)absoluteMonthlyRecurrencePattern.DayOfMonth);
				break;
			}
			case RecurrencePatternType.RelativeMonthly:
			{
				RelativeMonthlyRecurrencePattern relativeMonthlyRecurrencePattern = (RelativeMonthlyRecurrencePattern)recurrence.Pattern;
				recurrence2.Type = 3;
				recurrence2.DayOfWeek = new ushort?(SyncCalendarUtils.GetDayOfWeekValue(relativeMonthlyRecurrencePattern.DaysOfWeek));
				recurrence2.WeekOfMonth = new byte?((byte)relativeMonthlyRecurrencePattern.Index);
				break;
			}
			case RecurrencePatternType.AbsoluteYearly:
			{
				AbsoluteYearlyRecurrencePattern absoluteYearlyRecurrencePattern = (AbsoluteYearlyRecurrencePattern)recurrence.Pattern;
				recurrence2.Type = 5;
				recurrence2.DayOfMonth = new byte?((byte)absoluteYearlyRecurrencePattern.DayOfMonth);
				recurrence2.MonthOfYear = new byte?((byte)absoluteYearlyRecurrencePattern.Month);
				break;
			}
			case RecurrencePatternType.RelativeYearly:
			{
				RelativeYearlyRecurrencePattern relativeYearlyRecurrencePattern = (RelativeYearlyRecurrencePattern)recurrence.Pattern;
				recurrence2.Type = 6;
				recurrence2.DayOfWeek = new ushort?(SyncCalendarUtils.GetDayOfWeekValue(relativeYearlyRecurrencePattern.DaysOfWeek));
				recurrence2.WeekOfMonth = new byte?((byte)relativeYearlyRecurrencePattern.Index);
				recurrence2.MonthOfYear = new byte?((byte)relativeYearlyRecurrencePattern.Month);
				break;
			}
			default:
				throw new EasSyncFailedPermanentException("Invalid recurrence type: " + type);
			}
			RecurrenceRangeType type2 = recurrence.Range.Type;
			switch (type2)
			{
			case RecurrenceRangeType.EndDate:
			{
				EndDateRecurrenceRange endDateRecurrenceRange = (EndDateRecurrenceRange)recurrence.Range;
				recurrence2.Until = SyncCalendarUtils.ToStringDateTime(endDateRecurrenceRange.EndDate);
				break;
			}
			case RecurrenceRangeType.NoEnd:
				break;
			case RecurrenceRangeType.Numbered:
			{
				NumberedRecurrenceRange numberedRecurrenceRange = (NumberedRecurrenceRange)recurrence.Range;
				recurrence2.Occurrences = new ushort?((ushort)numberedRecurrenceRange.NumberOfOccurrences);
				break;
			}
			default:
				throw new EasSyncFailedPermanentException("Invalid recurrence range type: {0}" + type2);
			}
			return recurrence2;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00023F10 File Offset: 0x00022110
		public static List<Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception> GetExceptionData(IList<Event> exceptionalEvents, IList<string> deletedOccurrences, bool isMeeting, ExDateTime masterStart, UserSmtpAddress userSmtpAddress)
		{
			if (exceptionalEvents == null && deletedOccurrences == null)
			{
				return null;
			}
			List<Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception> list = new List<Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception>();
			if (exceptionalEvents != null)
			{
				foreach (Event exceptionalEvent in exceptionalEvents)
				{
					list.Add(SyncCalendarUtils.GetExceptionData(exceptionalEvent, isMeeting, masterStart, userSmtpAddress));
				}
			}
			if (deletedOccurrences != null)
			{
				foreach (string deletedOccurrenceId in deletedOccurrences)
				{
					list.Add(SyncCalendarUtils.GetDeletedExceptionData(deletedOccurrenceId, masterStart));
				}
			}
			return list;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00023FB8 File Offset: 0x000221B8
		public static void CopyCommonEventData(ICalendarData calendarData, Event theEvent, bool isMeeting, UserSmtpAddress userSmtpAddress)
		{
			EventSchema schema = theEvent.Schema;
			int num = (theEvent.PopupReminderSettings != null && theEvent.PopupReminderSettings.Count > 0) ? theEvent.PopupReminderSettings[0].ReminderMinutesBeforeStart : 0;
			if (theEvent.IsPropertySet(schema.BodyProperty) && theEvent.Body != null && theEvent.Body.Content != null)
			{
				calendarData.Body = new Body
				{
					Data = theEvent.Body.Content,
					Type = new byte?((byte)SyncCalendarUtils.GetEasBodyType(theEvent.Body.ContentType))
				};
			}
			if (theEvent.IsPropertySet(schema.StartProperty))
			{
				calendarData.StartTime = SyncCalendarUtils.ToStringDateTime(theEvent.Start);
			}
			if (theEvent.IsPropertySet(schema.EndProperty))
			{
				calendarData.EndTime = SyncCalendarUtils.ToStringDateTime(theEvent.End);
			}
			if (theEvent.IsPropertySet(schema.SubjectProperty))
			{
				calendarData.CalendarSubject = theEvent.Subject;
			}
			if (theEvent.IsPropertySet(schema.LocationProperty))
			{
				calendarData.Location = ((theEvent.Location != null) ? ((!string.IsNullOrEmpty(theEvent.Location.DisplayName)) ? theEvent.Location.DisplayName : null) : null);
			}
			if (theEvent.IsPropertySet(schema.PopupReminderSettingsProperty))
			{
				calendarData.Reminder = ((num > 0) ? new uint?((uint)num) : null);
			}
			if (theEvent.IsPropertySet(schema.IsAllDayProperty))
			{
				calendarData.AllDayEvent = new byte?(Convert.ToByte(theEvent.IsAllDay));
			}
			if (theEvent.IsPropertySet(schema.ShowAsProperty))
			{
				EasBusyStatus? busyStatus = SyncCalendarUtils.GetBusyStatus(theEvent.ShowAs);
				calendarData.BusyStatus = ((busyStatus != null) ? new byte?((byte)busyStatus.GetValueOrDefault()) : null);
			}
			if (theEvent.IsPropertySet(schema.SensitivityProperty))
			{
				calendarData.Sensitivity = new byte?((byte)theEvent.Sensitivity);
			}
			if (theEvent.IsPropertySet(schema.LastModifiedTimeProperty))
			{
				calendarData.DtStamp = SyncCalendarUtils.ToStringDateTime(theEvent.LastModifiedTime);
			}
			if (theEvent.IsPropertySet(schema.CategoriesProperty))
			{
				calendarData.CalendarCategories = SyncCalendarUtils.GetCategories(theEvent.Categories);
			}
			if (isMeeting && theEvent.IsPropertySet(schema.AttendeesProperty))
			{
				calendarData.Attendees = SyncCalendarUtils.GetAttendees(theEvent.Attendees, userSmtpAddress, theEvent.ResponseStatus);
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00024204 File Offset: 0x00022404
		private static Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase.BodyType GetEasBodyType(Microsoft.Exchange.Entities.DataModel.Items.BodyType contentType)
		{
			switch (contentType)
			{
			case Microsoft.Exchange.Entities.DataModel.Items.BodyType.Text:
				return Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase.BodyType.PlainText;
			case Microsoft.Exchange.Entities.DataModel.Items.BodyType.Html:
				return Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase.BodyType.HTML;
			default:
				throw new EasSyncFailedPermanentException("Invalid contentType value : " + contentType);
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0002423C File Offset: 0x0002243C
		private static EasBusyStatus? GetBusyStatus(FreeBusyStatus showAs)
		{
			switch (showAs)
			{
			case FreeBusyStatus.Unknown:
			case FreeBusyStatus.WorkingElsewhere:
				return null;
			case FreeBusyStatus.Free:
				return new EasBusyStatus?(EasBusyStatus.Free);
			case FreeBusyStatus.Tentative:
				return new EasBusyStatus?(EasBusyStatus.Tentative);
			case FreeBusyStatus.Busy:
				return new EasBusyStatus?(EasBusyStatus.Busy);
			case FreeBusyStatus.Oof:
				return new EasBusyStatus?(EasBusyStatus.OutOfOffice);
			default:
				throw new EasSyncFailedPermanentException("Invalid showAs status : " + showAs);
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000242A8 File Offset: 0x000224A8
		private static List<Category> GetCategories(List<string> edmCategories)
		{
			List<Category> list = null;
			if (edmCategories != null && edmCategories.Count > 0)
			{
				list = new List<Category>();
				foreach (string name in edmCategories)
				{
					list.Add(new Category
					{
						Name = name
					});
				}
			}
			return list;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00024318 File Offset: 0x00022518
		private static List<Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Attendee> GetAttendees(IList<Microsoft.Exchange.Entities.DataModel.Calendaring.Attendee> edmAttendees, UserSmtpAddress userSmtpAddress, ResponseStatus status)
		{
			List<Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Attendee> list = null;
			if (edmAttendees != null && edmAttendees.Count > 0)
			{
				list = new List<Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Attendee>();
				foreach (Microsoft.Exchange.Entities.DataModel.Calendaring.Attendee attendee in edmAttendees)
				{
					Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Attendee attendee2 = new Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Attendee
					{
						AttendeeType = new byte?((byte)attendee.Type),
						Email = attendee.EmailAddress,
						Name = attendee.Name
					};
					if (attendee.Status != null)
					{
						attendee2.AttendeeStatus = new byte?((byte)attendee.Status.Response);
					}
					else if (status != null && userSmtpAddress.Address.Equals(attendee.EmailAddress))
					{
						attendee2.AttendeeStatus = new byte?((byte)status.Response);
					}
					list.Add(attendee2);
				}
			}
			return list;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00024404 File Offset: 0x00022604
		private static Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception GetExceptionData(Event exceptionalEvent, bool isMeeting, ExDateTime masterStart, UserSmtpAddress userSmtpAddress)
		{
			Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception ex = new Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception();
			ex.ExceptionStartTime = SyncCalendarUtils.GetExceptionStartDate(exceptionalEvent.Id, masterStart);
			SyncCalendarUtils.CopyCommonEventData(ex, exceptionalEvent, isMeeting, userSmtpAddress);
			return ex;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00024434 File Offset: 0x00022634
		private static Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception GetDeletedExceptionData(string deletedOccurrenceId, ExDateTime masterStart)
		{
			return new Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception
			{
				ExceptionStartTime = SyncCalendarUtils.GetExceptionStartDate(deletedOccurrenceId, masterStart),
				Deleted = new byte?(1)
			};
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00024464 File Offset: 0x00022664
		private static string GetExceptionStartDate(string exceptionId, ExDateTime masterStart)
		{
			StoreObjectId storeObjectId = IdConverter.Instance.ToStoreObjectId(exceptionId);
			OccurrenceStoreObjectId occurrenceStoreObjectId = storeObjectId as OccurrenceStoreObjectId;
			if (occurrenceStoreObjectId == null)
			{
				throw new EasSyncFailedPermanentException("Exception id is not an occurrence id: " + exceptionId);
			}
			return SyncCalendarUtils.ToStringDateTime(occurrenceStoreObjectId.OccurrenceId.Add(masterStart.UniversalTime.TimeOfDay));
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000244BC File Offset: 0x000226BC
		private static RecurrencePattern CreateRecurrencePattern(Microsoft.Exchange.Connections.Eas.Model.Response.Calendar.Recurrence easRecurrence)
		{
			try
			{
				EasRecurrenceType type = (EasRecurrenceType)easRecurrence.Type;
				switch (type)
				{
				case EasRecurrenceType.Daily:
					if (easRecurrence.DayOfWeek != 0)
					{
						return new WeeklyRecurrencePattern((DaysOfWeek)easRecurrence.DayOfWeek);
					}
					return new DailyRecurrencePattern(easRecurrence.Interval);
				case EasRecurrenceType.Weekly:
					return new WeeklyRecurrencePattern((DaysOfWeek)easRecurrence.DayOfWeek, easRecurrence.Interval);
				case EasRecurrenceType.Monthly:
					return new MonthlyRecurrencePattern(easRecurrence.DayOfMonth, easRecurrence.Interval);
				case EasRecurrenceType.MonthlyTh:
				{
					RecurrenceOrderType order = SyncCalendarUtils.RecurrenceOrderTypeFromWeekOfMonth(easRecurrence.WeekOfMonth);
					return new MonthlyThRecurrencePattern((DaysOfWeek)easRecurrence.DayOfWeek, order, easRecurrence.Interval);
				}
				case EasRecurrenceType.Yearly:
					return new YearlyRecurrencePattern(easRecurrence.DayOfMonth, easRecurrence.MonthOfYear);
				case EasRecurrenceType.YearlyTh:
				{
					RecurrenceOrderType order2 = SyncCalendarUtils.RecurrenceOrderTypeFromWeekOfMonth(easRecurrence.WeekOfMonth);
					return new YearlyThRecurrencePattern((DaysOfWeek)easRecurrence.DayOfWeek, order2, easRecurrence.MonthOfYear);
				}
				}
				throw new EasFetchFailedPermanentException("Invalid recurrence type: " + type);
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw new EasFetchFailedPermanentException("Invalid recurrence", innerException);
			}
			RecurrencePattern result;
			return result;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000245E4 File Offset: 0x000227E4
		private static RecurrenceOrderType RecurrenceOrderTypeFromWeekOfMonth(int weekOfMonth)
		{
			if (weekOfMonth != 5)
			{
				return (RecurrenceOrderType)weekOfMonth;
			}
			return RecurrenceOrderType.Last;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000245F0 File Offset: 0x000227F0
		private static RecurrenceRange CreateRecurrenceRange(ExDateTime start, Microsoft.Exchange.Connections.Eas.Model.Response.Calendar.Recurrence easRecurrence)
		{
			if (easRecurrence.Occurrences != 0)
			{
				return new NumberedRecurrenceRange(start, easRecurrence.Occurrences);
			}
			if (!string.IsNullOrEmpty(easRecurrence.Until))
			{
				ExDateTime endDate = SyncCalendarUtils.ToExDateTime(easRecurrence.Until);
				return new EndDateRecurrenceRange(start, endDate);
			}
			return new NoEndRecurrenceRange(start);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002463C File Offset: 0x0002283C
		private static ushort GetDayOfWeekValue(ISet<DayOfWeek> daysOfWeek)
		{
			ushort num = 0;
			foreach (DayOfWeek day in daysOfWeek)
			{
				num += SyncCalendarUtils.GetEasDayOfWeekValue(day);
			}
			return num;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0002468C File Offset: 0x0002288C
		private static ushort GetEasDayOfWeekValue(DayOfWeek day)
		{
			return (ushort)(1 << (int)day);
		}

		// Token: 0x040008B6 RID: 2230
		private const string DateTimeFormat = "yyyyMMdd\\THHmmss\\Z";

		// Token: 0x040008B7 RID: 2231
		private const int CharArrrayLength = 32;

		// Token: 0x040008B8 RID: 2232
		private const int TimeZoneInformationStructSize = 172;

		// Token: 0x040008B9 RID: 2233
		private const int EasLastWeekOfAMonth = 5;

		// Token: 0x040008BA RID: 2234
		private const int BasePropId = 32768;

		// Token: 0x040008BB RID: 2235
		private static readonly PropertyTag GlobalObjectIdPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32768, PropType.Binary));

		// Token: 0x040008BC RID: 2236
		private static readonly PropertyTag StartPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32769, PropType.SysTime));

		// Token: 0x040008BD RID: 2237
		private static readonly PropertyTag EndPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32770, PropType.SysTime));

		// Token: 0x040008BE RID: 2238
		private static readonly PropertyTag AllDayEventPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32771, PropType.Boolean));

		// Token: 0x040008BF RID: 2239
		private static readonly PropertyTag BusyStatusPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32772, PropType.Int));

		// Token: 0x040008C0 RID: 2240
		private static readonly PropertyTag LocationPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32773, PropType.AnsiString));

		// Token: 0x040008C1 RID: 2241
		private static readonly PropertyTag MeetingStatusPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32774, PropType.Int));

		// Token: 0x040008C2 RID: 2242
		private static readonly PropertyTag ReminderPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32775, PropType.Int));

		// Token: 0x040008C3 RID: 2243
		private static readonly PropertyTag TimeZoneBlobPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32776, PropType.Binary));

		// Token: 0x040008C4 RID: 2244
		private static readonly PropertyTag AppointmentRecurrenceBlobPropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32777, PropType.Binary));

		// Token: 0x040008C5 RID: 2245
		private static readonly PropertyTag ResponseTypePropertyTag = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32778, PropType.Int));

		// Token: 0x040008C6 RID: 2246
		private static readonly PropertyTag RecipientTrackStatusPropertyTag = new PropertyTag(1610547203U);

		// Token: 0x040008C7 RID: 2247
		private static readonly PropertyTag RecipientTypePropertyTag = new PropertyTag(202702851U);

		// Token: 0x040008C8 RID: 2248
		private static readonly PropertyTag EmailAddressPropertyTag = new PropertyTag(805503007U);

		// Token: 0x040008C9 RID: 2249
		private static readonly PropertyTag DisplayNamePropertyTag = new PropertyTag(805371935U);

		// Token: 0x040008CA RID: 2250
		private static readonly PropertyTag RowIdPropertyTag = new PropertyTag(805306371U);

		// Token: 0x040008CB RID: 2251
		private static readonly PropertyTag SensitivityPropertyTag = new PropertyTag(3538947U);

		// Token: 0x040008CC RID: 2252
		private static readonly PropertyTag SentRepresentingNamePropertyTag = new PropertyTag(4325407U);

		// Token: 0x040008CD RID: 2253
		private static readonly PropertyTag SentRepresentingEmailAddressPropertyTag = new PropertyTag(6619167U);

		// Token: 0x040008CE RID: 2254
		public static readonly Dictionary<PropertyTag, NamedProperty> CalendarItemPropertyTagsToNamedProperties = new Dictionary<PropertyTag, NamedProperty>
		{
			{
				SyncCalendarUtils.GlobalObjectId,
				new NamedProperty(WellKnownPropertySet.Meeting, 3U)
			},
			{
				SyncCalendarUtils.Start,
				new NamedProperty(WellKnownPropertySet.Appointment, 33293U)
			},
			{
				SyncCalendarUtils.End,
				new NamedProperty(WellKnownPropertySet.Appointment, 33294U)
			},
			{
				SyncCalendarUtils.AllDayEvent,
				new NamedProperty(WellKnownPropertySet.Appointment, 33301U)
			},
			{
				SyncCalendarUtils.BusyStatus,
				new NamedProperty(WellKnownPropertySet.Appointment, 33285U)
			},
			{
				SyncCalendarUtils.Location,
				new NamedProperty(WellKnownPropertySet.Appointment, 33288U)
			},
			{
				SyncCalendarUtils.Reminder,
				new NamedProperty(WellKnownPropertySet.Common, 34049U)
			},
			{
				SyncCalendarUtils.TimeZoneBlob,
				new NamedProperty(WellKnownPropertySet.Appointment, 33331U)
			},
			{
				SyncCalendarUtils.MeetingStatus,
				new NamedProperty(WellKnownPropertySet.Appointment, 33303U)
			},
			{
				SyncCalendarUtils.AppointmentRecurrenceBlob,
				new NamedProperty(WellKnownPropertySet.Appointment, 33302U)
			},
			{
				SyncCalendarUtils.ResponseType,
				new NamedProperty(WellKnownPropertySet.Appointment, 33304U)
			}
		};

		// Token: 0x040008CF RID: 2255
		public static readonly Dictionary<PropertyTag, NamedProperty> AttendeePropertyTagsToNamedProperties = new Dictionary<PropertyTag, NamedProperty>(0);
	}
}
