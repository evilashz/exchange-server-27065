using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000101 RID: 257
	public static class DateTimeUtilities
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x0003ED0F File Offset: 0x0003CF0F
		public static string GetJavascriptDate(ExDateTime date)
		{
			return date.ToString(DateTimeUtilities.JSDateFormat, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0003ED22 File Offset: 0x0003CF22
		public static string GetIsoDateFormat(ExDateTime date)
		{
			return date.ToString(DateTimeUtilities.IsoDateFormatString, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0003ED35 File Offset: 0x0003CF35
		public static string GetHoursFormat(string timeFormat)
		{
			if (timeFormat == null)
			{
				throw new ArgumentNullException("timeFormat");
			}
			return DateFormatParser.GetPart(timeFormat, DateFormatPart.HoursFormat);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0003ED4C File Offset: 0x0003CF4C
		public static string GetDaysFormat(string dateFormat)
		{
			if (dateFormat == null)
			{
				throw new ArgumentNullException("dateFormat");
			}
			return DateFormatParser.GetPart(dateFormat, DateFormatPart.DaysFormat);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0003ED64 File Offset: 0x0003CF64
		public static ExDateTime ParseIsoDate(string isoDate, ExTimeZone timeZone)
		{
			if (isoDate == null)
			{
				throw new ArgumentNullException("isoDate");
			}
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			if (isoDate.Length != DateTimeUtilities.IsoDateFormatString.Length)
			{
				throw new OwaParsingErrorException(string.Format(CultureInfo.InvariantCulture, "The expected length of a Iso date format is {0} but the actual length is {1}. The actual format is {2}.", new object[]
				{
					DateTimeUtilities.IsoDateFormatString.Length,
					isoDate.Length,
					isoDate
				}));
			}
			ExDateTime result;
			try
			{
				result = new ExDateTime(timeZone, DateTime.ParseExact(isoDate, DateTimeUtilities.IsoDateFormatString, DateTimeFormatInfo.InvariantInfo));
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw new OwaParsingErrorException("The date represented by the input string, is out of range.", innerException);
			}
			catch (FormatException innerException2)
			{
				throw new OwaParsingErrorException("The date represented by the input string, is out of range.", innerException2);
			}
			return result;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0003EE30 File Offset: 0x0003D030
		public static ExDateTime[] GetWeekFromDay(ExDateTime day, DayOfWeek weekStartDay, int workDays, bool getWorkingWeek)
		{
			int num = day.DayOfWeek - weekStartDay;
			if (num < 0)
			{
				num += 7;
			}
			if (num != 0)
			{
				day = day.IncrementDays(-num);
			}
			int num2 = 7;
			if (getWorkingWeek)
			{
				num2 = 0;
				for (int i = 0; i < 7; i++)
				{
					if ((workDays >> i & 1) != 0)
					{
						num2++;
					}
				}
			}
			ExDateTime[] array;
			if (num2 == 0)
			{
				array = new ExDateTime[]
				{
					day
				};
			}
			else
			{
				array = new ExDateTime[num2];
				int num3 = 0;
				for (int j = 0; j < 7; j++)
				{
					if (getWorkingWeek)
					{
						if ((workDays >> (int)day.DayOfWeek & 1) != 0)
						{
							array[num3++] = day;
						}
					}
					else
					{
						array[num3++] = day;
					}
					day = day.IncrementDays(1);
				}
			}
			return array;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0003EEF9 File Offset: 0x0003D0F9
		public static bool IsWorkingDay(ExDateTime date, int workDays)
		{
			return (workDays >> (int)date.DayOfWeek & 1) != 0;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0003EF10 File Offset: 0x0003D110
		public static bool IsToday(ExDateTime date)
		{
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			return date.Year == localTime.Year && date.Month == localTime.Month && date.Day == localTime.Day;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0003EF58 File Offset: 0x0003D158
		public static string FormatDuration(int minutes)
		{
			if (minutes == 0)
			{
				return string.Format(Strings.MinuteFormat, minutes);
			}
			double num = (double)minutes % DateTimeUtilities.MinutesInWeek;
			if (num == 0.0)
			{
				num = (double)minutes / DateTimeUtilities.MinutesInWeek;
				return string.Format((1.0 < num) ? Strings.WeeksFormat : Strings.WeekFormat, num);
			}
			num = (double)minutes % DateTimeUtilities.MinutesInDay;
			if (num == 0.0)
			{
				num = (double)minutes / DateTimeUtilities.MinutesInDay;
				return string.Format((1.0 < num) ? Strings.DaysFormat : Strings.DayFormat, num);
			}
			num = (double)minutes % DateTimeUtilities.MinutesInHour;
			if (num == 0.0)
			{
				num = (double)minutes / DateTimeUtilities.MinutesInHour;
				return string.Format((1.0 < num) ? Strings.HoursFormat : Strings.HourFormat, num);
			}
			if (90 <= minutes)
			{
				num = (double)(minutes % 30);
				if (num == 0.0)
				{
					double num2 = (double)minutes / DateTimeUtilities.MinutesInHour;
					return string.Format(Strings.HoursFormat, num2);
				}
			}
			return string.Format((1 < minutes) ? Strings.MinutesFormat : Strings.MinuteFormat, minutes);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0003F094 File Offset: 0x0003D294
		public static void SetSessionTimeZone(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			ExTimeZone timeZone = null;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(userContext.UserOptions.TimeZone, out timeZone))
			{
				throw new OwaInvalidOperationException("Invalid time zone name : " + userContext.UserOptions.TimeZone);
			}
			userContext.TimeZone = timeZone;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0003F0EC File Offset: 0x0003D2EC
		public static ExDateTime GetLocalTime()
		{
			return DateTimeUtilities.GetLocalTime(OwaContext.Current.SessionContext);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0003F0FD File Offset: 0x0003D2FD
		public static ExDateTime GetLocalTime(ISessionContext sessionContext)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("sessionContext");
			}
			return ExDateTime.GetNow(sessionContext.TimeZone);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0003F118 File Offset: 0x0003D318
		public static int GetDayOfWeek(UserContext userContext, ExDateTime dateTime)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return (7 + (dateTime.DayOfWeek - userContext.UserOptions.WeekStartDay)) % 7;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0003F140 File Offset: 0x0003D340
		public static bool IsValidTimeZoneKeyName(string timeZoneKeyName)
		{
			ExTimeZone exTimeZone = null;
			return ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(timeZoneKeyName, out exTimeZone);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0003F15C File Offset: 0x0003D35C
		public static string GetLongDatePatternWithWeekDay(CultureInfo cultureInfo)
		{
			if (!DateTimeUtilities.longDatePatternWithWeekDay.ContainsKey(cultureInfo.LCID))
			{
				lock (DateTimeUtilities.lockObject)
				{
					if (!DateTimeUtilities.longDatePatternWithWeekDay.ContainsKey(cultureInfo.LCID))
					{
						string[] allDateTimePatterns = cultureInfo.DateTimeFormat.GetAllDateTimePatterns('D');
						foreach (string text in allDateTimePatterns)
						{
							if (text.Contains("ddd"))
							{
								DateTimeUtilities.longDatePatternWithWeekDay.Add(cultureInfo.LCID, text);
								break;
							}
						}
						if (!DateTimeUtilities.longDatePatternWithWeekDay.ContainsKey(cultureInfo.LCID))
						{
							DateTimeUtilities.longDatePatternWithWeekDay.Add(cultureInfo.LCID, cultureInfo.DateTimeFormat.LongDatePattern);
						}
					}
				}
			}
			return DateTimeUtilities.longDatePatternWithWeekDay[cultureInfo.LCID];
		}

		// Token: 0x04000610 RID: 1552
		private static readonly double MinutesInHour = 60.0;

		// Token: 0x04000611 RID: 1553
		private static readonly double MinutesInDay = DateTimeUtilities.MinutesInHour * 24.0;

		// Token: 0x04000612 RID: 1554
		private static readonly double MinutesInWeek = DateTimeUtilities.MinutesInDay * 7.0;

		// Token: 0x04000613 RID: 1555
		private static Dictionary<int, string> longDatePatternWithWeekDay = new Dictionary<int, string>(CultureInfo.GetCultures(CultureTypes.AllCultures).Length);

		// Token: 0x04000614 RID: 1556
		private static object lockObject = new object();

		// Token: 0x04000615 RID: 1557
		internal static string IsoDateFormatString = "yyyy-MM-ddTHH:mm:ss";

		// Token: 0x04000616 RID: 1558
		internal static string JSDateFormat = "MMM dd, yyy HH:mm:ss UTC";

		// Token: 0x04000617 RID: 1559
		public static readonly DateTime ExampleDate = new DateTime(1999, 1, 21);
	}
}
