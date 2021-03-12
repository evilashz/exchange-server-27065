using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Globalization
{
	// Token: 0x020003A0 RID: 928
	[ComVisible(true)]
	[Serializable]
	public class JapaneseCalendar : Calendar
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x000B82C1 File Offset: 0x000B64C1
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseCalendar.calendarMinValue;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000B82C8 File Offset: 0x000B64C8
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x000B82CF File Offset: 0x000B64CF
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000B82D4 File Offset: 0x000B64D4
		internal static EraInfo[] GetEraInfo()
		{
			if (JapaneseCalendar.japaneseEraInfo == null)
			{
				JapaneseCalendar.japaneseEraInfo = JapaneseCalendar.GetErasFromRegistry();
				if (JapaneseCalendar.japaneseEraInfo == null)
				{
					JapaneseCalendar.japaneseEraInfo = new EraInfo[]
					{
						new EraInfo(4, 1989, 1, 8, 1988, 1, 8011, "平成", "平", "H"),
						new EraInfo(3, 1926, 12, 25, 1925, 1, 64, "昭和", "昭", "S"),
						new EraInfo(2, 1912, 7, 30, 1911, 1, 15, "大正", "大", "T"),
						new EraInfo(1, 1868, 1, 1, 1867, 1, 45, "明治", "明", "M")
					};
				}
			}
			return JapaneseCalendar.japaneseEraInfo;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000B83C0 File Offset: 0x000B65C0
		[SecuritySafeCritical]
		private static EraInfo[] GetErasFromRegistry()
		{
			int num = 0;
			EraInfo[] array = null;
			try
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras"));
				permissionSet.Assert();
				RegistryKey registryKey = RegistryKey.GetBaseKey(RegistryKey.HKEY_LOCAL_MACHINE).OpenSubKey("System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras", false);
				if (registryKey == null)
				{
					return null;
				}
				string[] valueNames = registryKey.GetValueNames();
				if (valueNames != null && valueNames.Length != 0)
				{
					array = new EraInfo[valueNames.Length];
					for (int i = 0; i < valueNames.Length; i++)
					{
						EraInfo eraFromValue = JapaneseCalendar.GetEraFromValue(valueNames[i], registryKey.GetValue(valueNames[i]).ToString());
						if (eraFromValue != null)
						{
							array[num] = eraFromValue;
							num++;
						}
					}
				}
			}
			catch (SecurityException)
			{
				return null;
			}
			catch (IOException)
			{
				return null;
			}
			catch (UnauthorizedAccessException)
			{
				return null;
			}
			if (num < 4)
			{
				return null;
			}
			Array.Resize<EraInfo>(ref array, num);
			Array.Sort<EraInfo>(array, new Comparison<EraInfo>(JapaneseCalendar.CompareEraRanges));
			for (int j = 0; j < array.Length; j++)
			{
				array[j].era = array.Length - j;
				if (j == 0)
				{
					array[0].maxEraYear = 9999 - array[0].yearOffset;
				}
				else
				{
					array[j].maxEraYear = array[j - 1].yearOffset + 1 - array[j].yearOffset;
				}
			}
			return array;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000B852C File Offset: 0x000B672C
		private static int CompareEraRanges(EraInfo a, EraInfo b)
		{
			return b.ticks.CompareTo(a.ticks);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000B8540 File Offset: 0x000B6740
		private static EraInfo GetEraFromValue(string value, string data)
		{
			if (value == null || data == null)
			{
				return null;
			}
			if (value.Length != 10)
			{
				return null;
			}
			int num;
			int startMonth;
			int startDay;
			if (!Number.TryParseInt32(value.Substring(0, 4), NumberStyles.None, NumberFormatInfo.InvariantInfo, out num) || !Number.TryParseInt32(value.Substring(5, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out startMonth) || !Number.TryParseInt32(value.Substring(8, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out startDay))
			{
				return null;
			}
			string[] array = data.Split(new char[]
			{
				'_'
			});
			if (array.Length != 4)
			{
				return null;
			}
			if (array[0].Length == 0 || array[1].Length == 0 || array[2].Length == 0 || array[3].Length == 0)
			{
				return null;
			}
			return new EraInfo(0, num, startMonth, startDay, num - 1, 1, 0, array[0], array[1], array[3]);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000B8603 File Offset: 0x000B6803
		internal static Calendar GetDefaultInstance()
		{
			if (JapaneseCalendar.s_defaultInstance == null)
			{
				JapaneseCalendar.s_defaultInstance = new JapaneseCalendar();
			}
			return JapaneseCalendar.s_defaultInstance;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000B8624 File Offset: 0x000B6824
		public JapaneseCalendar()
		{
			try
			{
				new CultureInfo("ja-JP");
			}
			catch (ArgumentException innerException)
			{
				throw new TypeInitializationException(base.GetType().FullName, innerException);
			}
			this.helper = new GregorianCalendarHelper(this, JapaneseCalendar.GetEraInfo());
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002FF0 RID: 12272 RVA: 0x000B8678 File Offset: 0x000B6878
		internal override int ID
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000B867B File Offset: 0x000B687B
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000B868A File Offset: 0x000B688A
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000B8699 File Offset: 0x000B6899
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000B86A9 File Offset: 0x000B68A9
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000B86B8 File Offset: 0x000B68B8
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000B86C6 File Offset: 0x000B68C6
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000B86D4 File Offset: 0x000B68D4
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000B86E2 File Offset: 0x000B68E2
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000B86F1 File Offset: 0x000B68F1
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000B8701 File Offset: 0x000B6901
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000B870F File Offset: 0x000B690F
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000B871D File Offset: 0x000B691D
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000B872B File Offset: 0x000B692B
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000B873D File Offset: 0x000B693D
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000B874C File Offset: 0x000B694C
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000B875B File Offset: 0x000B695B
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000B876C File Offset: 0x000B696C
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000B8794 File Offset: 0x000B6994
		public override int ToFourDigitYear(int year)
		{
			if (year <= 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (year > this.helper.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, this.helper.MaxYear));
			}
			return year;
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x000B87FE File Offset: 0x000B69FE
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000B880C File Offset: 0x000B6A0C
		internal static string[] EraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].eraName;
			}
			return array;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000B8848 File Offset: 0x000B6A48
		internal static string[] AbbrevEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].abbrevEraName;
			}
			return array;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000B8884 File Offset: 0x000B6A84
		internal static string[] EnglishEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].englishEraName;
			}
			return array;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000B88C0 File Offset: 0x000B6AC0
		internal override bool IsValidYear(int year, int era)
		{
			return this.helper.IsValidYear(year, era);
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000B88CF File Offset: 0x000B6ACF
		// (set) Token: 0x06003009 RID: 12297 RVA: 0x000B88F4 File Offset: 0x000B6AF4
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 99);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.helper.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 99, this.helper.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x0400144D RID: 5197
		internal static readonly DateTime calendarMinValue = new DateTime(1868, 9, 8);

		// Token: 0x0400144E RID: 5198
		internal static volatile EraInfo[] japaneseEraInfo;

		// Token: 0x0400144F RID: 5199
		private const string c_japaneseErasHive = "System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras";

		// Token: 0x04001450 RID: 5200
		private const string c_japaneseErasHivePermissionList = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras";

		// Token: 0x04001451 RID: 5201
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x04001452 RID: 5202
		internal GregorianCalendarHelper helper;

		// Token: 0x04001453 RID: 5203
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 99;
	}
}
