using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000BC RID: 188
	internal class ContactCommon
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x00029708 File Offset: 0x00027908
		static ContactCommon()
		{
			for (int i = 0; i < ContactCommon.propertyStringTable.Length; i++)
			{
				if (ContactCommon.propertyStringTable[i] != null)
				{
					ContactCommon.propertyEnumTable.Add(ContactCommon.propertyStringTable[i], (PropertyId)i);
				}
			}
			for (int j = 0; j < ContactCommon.parameterStringTable.Length; j++)
			{
				if (ContactCommon.parameterStringTable[j] != null)
				{
					ContactCommon.parameterEnumTable.Add(ContactCommon.parameterStringTable[j], (ParameterId)j);
				}
			}
			for (int k = 0; k < ContactCommon.typeStringTable.Length; k++)
			{
				if (ContactCommon.typeStringTable[k] != null)
				{
					ContactCommon.valueEnumTable.Add(ContactCommon.typeStringTable[k], (ContactValueType)k);
				}
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00029A80 File Offset: 0x00027C80
		public static string GetPropertyString(PropertyId p)
		{
			if (p < PropertyId.Unknown || (ulong)p >= (ulong)((long)ContactCommon.propertyStringTable.Length))
			{
				throw new ArgumentOutOfRangeException();
			}
			return ContactCommon.propertyStringTable[(int)((UIntPtr)p)];
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00029AB0 File Offset: 0x00027CB0
		public static string GetValueTypeString(ContactValueType p)
		{
			if (p < ContactValueType.Unknown || (ulong)p >= (ulong)((long)ContactCommon.typeStringTable.Length))
			{
				throw new ArgumentOutOfRangeException();
			}
			return ContactCommon.typeStringTable[(int)((UIntPtr)p)];
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00029AE0 File Offset: 0x00027CE0
		public static string GetParameterString(ParameterId p)
		{
			if (p < ParameterId.Unknown || (ulong)p >= (ulong)((long)ContactCommon.parameterStringTable.Length))
			{
				throw new ArgumentOutOfRangeException();
			}
			return ContactCommon.parameterStringTable[(int)((UIntPtr)p)];
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00029B10 File Offset: 0x00027D10
		public static PropertyId GetPropertyEnum(string p)
		{
			PropertyId result;
			if (ContactCommon.propertyEnumTable.TryGetValue(p, out result))
			{
				return result;
			}
			return PropertyId.Unknown;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00029B30 File Offset: 0x00027D30
		public static ContactValueType GetValueTypeEnum(string c)
		{
			ContactValueType result;
			if (ContactCommon.valueEnumTable.TryGetValue(c, out result))
			{
				return result;
			}
			return ContactValueType.Unknown;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00029B50 File Offset: 0x00027D50
		public static ParameterId GetParameterEnum(string p)
		{
			ParameterId result;
			if (p != null && ContactCommon.parameterEnumTable.TryGetValue(p, out result))
			{
				return result;
			}
			return ParameterId.Unknown;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00029B74 File Offset: 0x00027D74
		public static ContactValueType GetDefaultValueType(PropertyId p)
		{
			if (p < PropertyId.Unknown || (ulong)p >= (ulong)((long)ContactCommon.defaultTypeTable.Length))
			{
				throw new ArgumentOutOfRangeException();
			}
			return ContactCommon.defaultTypeTable[(int)((UIntPtr)p)];
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00029BA4 File Offset: 0x00027DA4
		public static DateTime ParseDate(string s, ComplianceTracker tracker)
		{
			DateTime result;
			if (!DateTime.TryParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result) && !DateTime.TryParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
			{
				return ContactCommon.ParseDateTime(s, tracker);
			}
			return result;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00029BE8 File Offset: 0x00027DE8
		public static DateTime ParseTime(string s, ComplianceTracker tracker)
		{
			int length = s.Length;
			if (length < 6)
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidDateTimeLength);
				return ContactCommon.MinDateTime;
			}
			string format = "HHmmss";
			int num = 6;
			if (s[2] == ':')
			{
				format = "HH:mm:ss";
				num = 8;
			}
			if (length < num)
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidDateTimeLength);
				return ContactCommon.MinDateTime;
			}
			return ContactCommon.InternalParseDateTime(s, length, format, num, tracker);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00029C58 File Offset: 0x00027E58
		public static DateTime ParseDateTime(string s, ComplianceTracker tracker)
		{
			int length = s.Length;
			if (length < 15)
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidDateTimeLength);
				return ContactCommon.MinDateTime;
			}
			string text = "yyyyMMdd";
			int num = 8;
			if (s[4] == '-')
			{
				text = "yyyy-MM-dd";
				num = 10;
			}
			if (s[num + 3] == ':')
			{
				text += "\\THH:mm:ss";
				num += 9;
			}
			else
			{
				text += "\\THHmmss";
				num += 7;
			}
			if (length < num)
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidDateTimeLength);
				return ContactCommon.MinDateTime;
			}
			return ContactCommon.InternalParseDateTime(s, length, text, num, tracker);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00029CF8 File Offset: 0x00027EF8
		public static TimeSpan ParseUtcOffset(string s, ComplianceTracker tracker)
		{
			int length = s.Length;
			if (length != 5 && length != 6)
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidUtcOffsetLength);
				return TimeSpan.Zero;
			}
			bool flag = false;
			if (s[0] == '-')
			{
				flag = true;
			}
			else if (s[0] != '+')
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.ExpectedPlusMinus);
				return TimeSpan.Zero;
			}
			DateTime dateTime;
			if (!DateTime.TryParseExact(s.Substring(1), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) && !DateTime.TryParseExact(s.Substring(1), "HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
				return TimeSpan.Zero;
			}
			TimeSpan result = new TimeSpan(dateTime.Hour, dateTime.Minute, 0);
			if (flag)
			{
				return result.Negate();
			}
			return result;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00029DCA File Offset: 0x00027FCA
		public static string FormatDate(DateTime s)
		{
			return s.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00029DE0 File Offset: 0x00027FE0
		public static string FormatDateTime(DateTime s)
		{
			string format = ContactCommon.RetrieveDateTimeFormatString(s.Millisecond != 0, s.Kind == DateTimeKind.Utc);
			return s.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00029E18 File Offset: 0x00028018
		public static string FormatTime(DateTime s)
		{
			string format = ContactCommon.RetrieveTimeFormatString(s.Millisecond != 0, s.Kind == DateTimeKind.Utc);
			return s.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00029E50 File Offset: 0x00028050
		public static string FormatUtcOffset(TimeSpan ts)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (ts.Hours == 0 && ts.Minutes == 0 && ts.Seconds == 0)
			{
				return "+00:00";
			}
			if (ts.Ticks > 0L)
			{
				stringBuilder.Append('+');
			}
			else
			{
				stringBuilder.Append('-');
				ts = ((ts == TimeSpan.MinValue) ? TimeSpan.MaxValue : ts.Negate());
			}
			if (ts.Hours < 10)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(ts.Hours.ToString());
			stringBuilder.Append(':');
			if (ts.Minutes < 10)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(ts.Minutes.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00029F24 File Offset: 0x00028124
		private static DateTime InternalParseDateTime(string s, int length, string format, int formatLength, ComplianceTracker tracker)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			if (length > formatLength)
			{
				if (s[formatLength] == ',')
				{
					int num = formatLength + 1;
					while (num < length && char.IsDigit(s[num]))
					{
						num++;
					}
					if (num == formatLength + 1)
					{
						tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidDateFormat);
						return ContactCommon.MinDateTime;
					}
					text2 = s.Substring(formatLength + 1, num - (formatLength + 1));
					if (num < length)
					{
						text = s.Substring(num);
					}
				}
				else
				{
					text = s.Substring(formatLength);
				}
				s = s.Substring(0, formatLength);
			}
			DateTime dateTime;
			if (!DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidDateFormat);
				return ContactCommon.MinDateTime;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				if (text2.Length > 3)
				{
					text2 = text2.Substring(0, 3);
				}
				int num2 = 0;
				if (!int.TryParse(text2, out num2))
				{
					tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidDateFormat);
					return ContactCommon.MinDateTime;
				}
				for (int i = text2.Length; i < 3; i++)
				{
					num2 *= 10;
				}
				dateTime += new TimeSpan(0, 0, 0, 0, num2);
			}
			if (!string.IsNullOrEmpty(text) && text != "Z")
			{
				dateTime += ContactCommon.ParseUtcOffset(text, tracker);
			}
			return dateTime;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0002A06E File Offset: 0x0002826E
		private static string RetrieveDateTimeFormatString(bool hasNonZeroMillisecond, bool isUtc)
		{
			if (hasNonZeroMillisecond)
			{
				if (!isUtc)
				{
					return "yyyy-MM-dd\\THH:mm:ss\\,fff";
				}
				return "yyyy-MM-dd\\THH:mm:ss\\,fff\\Z";
			}
			else
			{
				if (!isUtc)
				{
					return "yyyy-MM-dd\\THH:mm:ss";
				}
				return "yyyy-MM-dd\\THH:mm:ss\\Z";
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0002A090 File Offset: 0x00028290
		private static string RetrieveTimeFormatString(bool hasNonZeroMillisecond, bool isUtc)
		{
			if (hasNonZeroMillisecond)
			{
				if (!isUtc)
				{
					return "HH:mm:ss\\,fff";
				}
				return "HH:mm:ss\\,fff\\Z";
			}
			else
			{
				if (!isUtc)
				{
					return "HH:mm:ss";
				}
				return "HH:mm:ss\\Z";
			}
		}

		// Token: 0x0400063A RID: 1594
		private const string DateFormat = "yyyyMMdd";

		// Token: 0x0400063B RID: 1595
		private const string DateFormatDash = "yyyy-MM-dd";

		// Token: 0x0400063C RID: 1596
		private const string TimeSeparator = "\\T";

		// Token: 0x0400063D RID: 1597
		private const string TimeFormat = "HHmmss";

		// Token: 0x0400063E RID: 1598
		private const string TimeFormatColon = "HH:mm:ss";

		// Token: 0x0400063F RID: 1599
		private const string TimeMsFormat = "\\,fff";

		// Token: 0x04000640 RID: 1600
		private const string TimeOffset = "HHmm";

		// Token: 0x04000641 RID: 1601
		private const string TimeOffsetColon = "HH:mm";

		// Token: 0x04000642 RID: 1602
		private const string UtcSuffix = "\\Z";

		// Token: 0x04000643 RID: 1603
		private const string TimeFormatColonMillisecUtc = "HH:mm:ss\\,fff\\Z";

		// Token: 0x04000644 RID: 1604
		private const string TimeFormatColonMillisec = "HH:mm:ss\\,fff";

		// Token: 0x04000645 RID: 1605
		private const string TimeFormatColonUtc = "HH:mm:ss\\Z";

		// Token: 0x04000646 RID: 1606
		private const string DateTimeFormat = "yyyy-MM-dd\\THH:mm:ss";

		// Token: 0x04000647 RID: 1607
		private const string DateTimeFormatMillisec = "yyyy-MM-dd\\THH:mm:ss\\,fff";

		// Token: 0x04000648 RID: 1608
		private const string DateTimeFormatMillisecUtc = "yyyy-MM-dd\\THH:mm:ss\\,fff\\Z";

		// Token: 0x04000649 RID: 1609
		private const string DateTimeFormatUtc = "yyyy-MM-dd\\THH:mm:ss\\Z";

		// Token: 0x0400064A RID: 1610
		private const DateTimeStyles VCardDateTimeStyle = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;

		// Token: 0x0400064B RID: 1611
		private static readonly DateTime MinDateTime = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);

		// Token: 0x0400064C RID: 1612
		private static string[] propertyStringTable = new string[]
		{
			null,
			"PROFILE",
			"NAME",
			"SOURCE",
			"FN",
			"N",
			"NICKNAME",
			"PHOTO",
			"BDAY",
			"ADR",
			"LABEL",
			"TEL",
			"EMAIL",
			"MAILER",
			"TZ",
			"GEO",
			"TITLE",
			"ROLE",
			"LOGO",
			"AGENT",
			"ORG",
			"CATEGORIES",
			"NOTE",
			"PRODID",
			"REV",
			"SORT-STRING",
			"SOUND",
			"UID",
			"URL",
			"VERSION",
			"CLASS",
			"KEY"
		};

		// Token: 0x0400064D RID: 1613
		private static string[] parameterStringTable = new string[]
		{
			null,
			"TYPE",
			"LANGUAGE",
			"ENCODING",
			"VALUE",
			"CHARSET"
		};

		// Token: 0x0400064E RID: 1614
		private static string[] typeStringTable = new string[]
		{
			null,
			"BINARY",
			"BOOLEAN",
			"DATE",
			"DATE-TIME",
			"FLOAT",
			"INTEGER",
			"TEXT",
			"TIME",
			"URI",
			"UTC-OFFSET",
			"VCARD",
			"PHONE-NUMBER"
		};

		// Token: 0x0400064F RID: 1615
		private static ContactValueType[] defaultTypeTable = new ContactValueType[]
		{
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.Uri,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.Binary,
			ContactValueType.Date,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.PhoneNumber,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.UtcOffset,
			ContactValueType.Float,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.Binary,
			ContactValueType.VCard,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.DateTime,
			ContactValueType.Text,
			ContactValueType.Binary,
			ContactValueType.Text,
			ContactValueType.Uri,
			ContactValueType.Text,
			ContactValueType.Text,
			ContactValueType.Binary
		};

		// Token: 0x04000650 RID: 1616
		private static Dictionary<string, PropertyId> propertyEnumTable = new Dictionary<string, PropertyId>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000651 RID: 1617
		private static Dictionary<string, ParameterId> parameterEnumTable = new Dictionary<string, ParameterId>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000652 RID: 1618
		private static Dictionary<string, ContactValueType> valueEnumTable = new Dictionary<string, ContactValueType>(StringComparer.OrdinalIgnoreCase);
	}
}
