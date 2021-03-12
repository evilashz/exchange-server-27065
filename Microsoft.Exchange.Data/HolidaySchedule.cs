using System;
using System.Globalization;
using System.Management.Automation;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001C8 RID: 456
	[Serializable]
	public class HolidaySchedule
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0003095B File Offset: 0x0002EB5B
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00030963 File Offset: 0x0002EB63
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0003096C File Offset: 0x0002EB6C
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00030974 File Offset: 0x0002EB74
		public string Greeting
		{
			get
			{
				return this.introductoryGreeting;
			}
			set
			{
				this.introductoryGreeting = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0003097D File Offset: 0x0002EB7D
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x00030985 File Offset: 0x0002EB85
		public DateTime StartDate
		{
			get
			{
				return this.scheduleDate;
			}
			set
			{
				this.scheduleDate = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0003098E File Offset: 0x0002EB8E
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x00030996 File Offset: 0x0002EB96
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x000309A0 File Offset: 0x0002EBA0
		static HolidaySchedule()
		{
			DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
			HolidaySchedule.shortPatterns = new string[]
			{
				dateTimeFormat.ShortDatePattern
			};
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x000309CE File Offset: 0x0002EBCE
		private HolidaySchedule()
		{
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000309D6 File Offset: 0x0002EBD6
		public HolidaySchedule(string holidayName, string greeting, DateTime start, DateTime end)
		{
			this.name = holidayName;
			this.introductoryGreeting = greeting;
			this.scheduleDate = start;
			this.endDate = end;
			this.Validate();
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00030A04 File Offset: 0x0002EC04
		public HolidaySchedule(PSObject importedObject)
		{
			this.name = (string)importedObject.Properties["Name"].Value;
			this.introductoryGreeting = (string)importedObject.Properties["Greeting"].Value;
			string datestring = (string)importedObject.Properties["StartDate"].Value;
			this.scheduleDate = HolidaySchedule.StringToDate(datestring, false);
			datestring = (string)importedObject.Properties["EndDate"].Value;
			this.endDate = HolidaySchedule.StringToDate(datestring, false);
			this.Validate();
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00030AAD File Offset: 0x0002ECAD
		public static HolidaySchedule Parse(string schedule)
		{
			return HolidaySchedule.HolidayFromString(schedule, false);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00030AB6 File Offset: 0x0002ECB6
		public static HolidaySchedule ParseADString(string schedule)
		{
			return HolidaySchedule.HolidayFromString(schedule, true);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00030AC0 File Offset: 0x0002ECC0
		private static HolidaySchedule HolidayFromString(string schedule, bool fromAD)
		{
			if (schedule == null || schedule.Length == 0)
			{
				throw new FormatException(DataStrings.InvalidHolidayScheduleFormat);
			}
			string[] array = schedule.Split(new char[]
			{
				','
			});
			if (array == null || array.Length < 3 || array.Length > 4)
			{
				throw new FormatException(DataStrings.InvalidHolidayScheduleFormat);
			}
			DateTime dateTime = DateTime.MinValue;
			dateTime = HolidaySchedule.StringToDate(array[2], fromAD);
			DateTime end = dateTime;
			if (array.Length > 3)
			{
				end = HolidaySchedule.StringToDate(array[3], fromAD);
			}
			return new HolidaySchedule(array[0], array[1], dateTime, end);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00030B4F File Offset: 0x0002ED4F
		public override string ToString()
		{
			return this.HolidayToString(false);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00030B58 File Offset: 0x0002ED58
		public string ToADString()
		{
			return this.HolidayToString(true);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00030B64 File Offset: 0x0002ED64
		private string HolidayToString(bool toADString)
		{
			return string.Format("{0},{1},{2},{3}", new object[]
			{
				this.Name,
				this.Greeting,
				this.DateToString(this.StartDate, toADString),
				this.DateToString(this.EndDate, toADString)
			});
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00030BB8 File Offset: 0x0002EDB8
		private string DateToString(DateTime dt, bool toADString)
		{
			string result;
			if (toADString)
			{
				result = dt.ToString("yyyyMMddHHmmss'.0Z'", DateTimeFormatInfo.InvariantInfo);
			}
			else
			{
				result = dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
			}
			return result;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00030BF6 File Offset: 0x0002EDF6
		public void Validate()
		{
			HolidaySchedule.ValidateName(this.name);
			HolidaySchedule.ValidateGreeting(this.introductoryGreeting);
			HolidaySchedule.ValidateSchedule(this.scheduleDate, this.endDate);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00030C20 File Offset: 0x0002EE20
		private static DateTime StringToDate(string datestring, bool fromADString)
		{
			DateTime result;
			try
			{
				if (fromADString)
				{
					result = DateTime.ParseExact(datestring, "yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
				}
				else
				{
					result = DateTime.ParseExact(datestring, HolidaySchedule.shortPatterns, null, DateTimeStyles.NoCurrentDateDefault);
				}
			}
			catch (FormatException innerException)
			{
				throw new FormatException(DataStrings.InvalidDateFormat(datestring, HolidaySchedule.shortPatterns[0]), innerException);
			}
			return result;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00030C80 File Offset: 0x0002EE80
		private static void ValidateName(string holidayName)
		{
			if (holidayName == null || holidayName.Length == 0)
			{
				throw new StrongTypeFormatException(DataStrings.ConstraintViolationStringLengthIsEmpty, "Name");
			}
			if (holidayName.Length > 64)
			{
				throw new StrongTypeFormatException(DataStrings.ConstraintViolationStringLengthTooLong(64, holidayName.Length), "Name");
			}
			if (holidayName.IndexOf(",") != -1)
			{
				throw new StrongTypeFormatException(DataStrings.InvalidCharInString("HolidayName", ","), "Name");
			}
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00030D04 File Offset: 0x0002EF04
		private static void ValidateGreeting(string greeting)
		{
			if (string.IsNullOrEmpty(greeting))
			{
				throw new StrongTypeFormatException(DataStrings.ConstraintViolationStringLengthIsEmpty, "Greeting");
			}
			if (greeting.IndexOf(",") != -1)
			{
				throw new StrongTypeFormatException(DataStrings.InvalidCharInString("Greeting", ","), "Greeting");
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00030D5B File Offset: 0x0002EF5B
		private static void ValidateSchedule(DateTime start, DateTime end)
		{
			if (start > end)
			{
				throw new FormatException(DataStrings.ScheduleDateInvalid(start, end));
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00030D78 File Offset: 0x0002EF78
		public override bool Equals(object obj)
		{
			HolidaySchedule holidaySchedule = obj as HolidaySchedule;
			return holidaySchedule != null && string.Equals(this.ToADString(), holidaySchedule.ToADString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00030DA3 File Offset: 0x0002EFA3
		public override int GetHashCode()
		{
			return this.ToADString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x04000982 RID: 2434
		private const int NAME = 0;

		// Token: 0x04000983 RID: 2435
		private const int GREETING = 1;

		// Token: 0x04000984 RID: 2436
		private const int START = 2;

		// Token: 0x04000985 RID: 2437
		private const int END = 3;

		// Token: 0x04000986 RID: 2438
		private const string strName = "Name";

		// Token: 0x04000987 RID: 2439
		private const string strGreeting = "Greeting";

		// Token: 0x04000988 RID: 2440
		private const string strStart = "StartDate";

		// Token: 0x04000989 RID: 2441
		private const string strEnd = "EndDate";

		// Token: 0x0400098A RID: 2442
		private string name;

		// Token: 0x0400098B RID: 2443
		private string introductoryGreeting;

		// Token: 0x0400098C RID: 2444
		private DateTime scheduleDate;

		// Token: 0x0400098D RID: 2445
		private DateTime endDate;

		// Token: 0x0400098E RID: 2446
		private static string[] shortPatterns;
	}
}
