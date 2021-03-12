﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;

namespace System
{
	// Token: 0x02000164 RID: 356
	internal static class DateTimeFormat
	{
		// Token: 0x060015DC RID: 5596 RVA: 0x000404BB File Offset: 0x0003E6BB
		internal static void FormatDigits(StringBuilder outputBuffer, int value, int len)
		{
			DateTimeFormat.FormatDigits(outputBuffer, value, len, false);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000404C8 File Offset: 0x0003E6C8
		[SecuritySafeCritical]
		internal unsafe static void FormatDigits(StringBuilder outputBuffer, int value, int len, bool overrideLengthLimit)
		{
			if (!overrideLengthLimit && len > 2)
			{
				len = 2;
			}
			char* ptr = stackalloc char[checked(unchecked((UIntPtr)16) * 2)];
			char* ptr2 = ptr + 16;
			int num = value;
			do
			{
				*(--ptr2) = (char)(num % 10 + 48);
				num /= 10;
			}
			while (num != 0 && ptr2 != ptr);
			int num2 = (int)((long)(ptr + 16 - ptr2));
			while (num2 < len && ptr2 != ptr)
			{
				*(--ptr2) = '0';
				num2++;
			}
			outputBuffer.Append(ptr2, num2);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00040538 File Offset: 0x0003E738
		private static void HebrewFormatDigits(StringBuilder outputBuffer, int digits)
		{
			outputBuffer.Append(HebrewNumber.ToString(digits));
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00040548 File Offset: 0x0003E748
		internal static int ParseRepeatPattern(string format, int pos, char patternChar)
		{
			int length = format.Length;
			int num = pos + 1;
			while (num < length && format[num] == patternChar)
			{
				num++;
			}
			return num - pos;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00040577 File Offset: 0x0003E777
		private static string FormatDayOfWeek(int dayOfWeek, int repeat, DateTimeFormatInfo dtfi)
		{
			if (repeat == 3)
			{
				return dtfi.GetAbbreviatedDayName((DayOfWeek)dayOfWeek);
			}
			return dtfi.GetDayName((DayOfWeek)dayOfWeek);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0004058C File Offset: 0x0003E78C
		private static string FormatMonth(int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000405A4 File Offset: 0x0003E7A4
		private static string FormatHebrewMonthName(DateTime time, int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (dtfi.Calendar.IsLeapYear(dtfi.Calendar.GetYear(time)))
			{
				return dtfi.internalGetMonthName(month, MonthNameStyles.LeapYear, repeatCount == 3);
			}
			if (month >= 7)
			{
				month++;
			}
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000405F4 File Offset: 0x0003E7F4
		internal static int ParseQuoteString(string format, int pos, StringBuilder result)
		{
			int length = format.Length;
			int num = pos;
			char c = format[pos++];
			bool flag = false;
			while (pos < length)
			{
				char c2 = format[pos++];
				if (c2 == c)
				{
					flag = true;
					break;
				}
				if (c2 == '\\')
				{
					if (pos >= length)
					{
						throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
					}
					result.Append(format[pos++]);
				}
				else
				{
					result.Append(c2);
				}
			}
			if (!flag)
			{
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Format_BadQuote"), c));
			}
			return pos - num;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00040694 File Offset: 0x0003E894
		internal static int ParseNextChar(string format, int pos)
		{
			if (pos >= format.Length - 1)
			{
				return -1;
			}
			return (int)format[pos + 1];
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000406AC File Offset: 0x0003E8AC
		private static bool IsUseGenitiveForm(string format, int index, int tokenLen, char patternToMatch)
		{
			int num = 0;
			int num2 = index - 1;
			while (num2 >= 0 && format[num2] != patternToMatch)
			{
				num2--;
			}
			if (num2 >= 0)
			{
				while (--num2 >= 0 && format[num2] == patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			num2 = index + tokenLen;
			while (num2 < format.Length && format[num2] != patternToMatch)
			{
				num2++;
			}
			if (num2 < format.Length)
			{
				num = 0;
				while (++num2 < format.Length && format[num2] == patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00040744 File Offset: 0x0003E944
		private static string FormatCustomized(DateTime dateTime, string format, DateTimeFormatInfo dtfi, TimeSpan offset)
		{
			Calendar calendar = dtfi.Calendar;
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			bool flag = calendar.ID == 8;
			bool flag2 = calendar.ID == 3;
			bool timeOnly = true;
			int i = 0;
			while (i < format.Length)
			{
				char c = format[i];
				int num2;
				if (c <= 'K')
				{
					if (c <= '/')
					{
						if (c <= '%')
						{
							if (c != '"')
							{
								if (c != '%')
								{
									goto IL_64F;
								}
								int num = DateTimeFormat.ParseNextChar(format, i);
								if (num >= 0 && num != 37)
								{
									stringBuilder.Append(DateTimeFormat.FormatCustomized(dateTime, ((char)num).ToString(), dtfi, offset));
									num2 = 2;
									goto IL_65B;
								}
								throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
							}
						}
						else if (c != '\'')
						{
							if (c != '/')
							{
								goto IL_64F;
							}
							stringBuilder.Append(dtfi.DateSeparator);
							num2 = 1;
							goto IL_65B;
						}
						StringBuilder stringBuilder2 = new StringBuilder();
						num2 = DateTimeFormat.ParseQuoteString(format, i, stringBuilder2);
						stringBuilder.Append(stringBuilder2);
					}
					else if (c <= 'F')
					{
						if (c != ':')
						{
							if (c != 'F')
							{
								goto IL_64F;
							}
							goto IL_1E3;
						}
						else
						{
							stringBuilder.Append(dtfi.TimeSeparator);
							num2 = 1;
						}
					}
					else if (c != 'H')
					{
						if (c != 'K')
						{
							goto IL_64F;
						}
						num2 = 1;
						DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, stringBuilder);
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(stringBuilder, dateTime.Hour, num2);
					}
				}
				else if (c <= 'm')
				{
					if (c <= '\\')
					{
						if (c != 'M')
						{
							if (c != '\\')
							{
								goto IL_64F;
							}
							int num = DateTimeFormat.ParseNextChar(format, i);
							if (num < 0)
							{
								throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
							}
							stringBuilder.Append((char)num);
							num2 = 2;
						}
						else
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int month = calendar.GetMonth(dateTime);
							if (num2 <= 2)
							{
								if (flag)
								{
									DateTimeFormat.HebrewFormatDigits(stringBuilder, month);
								}
								else
								{
									DateTimeFormat.FormatDigits(stringBuilder, month, num2);
								}
							}
							else if (flag)
							{
								stringBuilder.Append(DateTimeFormat.FormatHebrewMonthName(dateTime, month, num2, dtfi));
							}
							else if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None && num2 >= 4)
							{
								stringBuilder.Append(dtfi.internalGetMonthName(month, DateTimeFormat.IsUseGenitiveForm(format, i, num2, 'd') ? MonthNameStyles.Genitive : MonthNameStyles.Regular, false));
							}
							else
							{
								stringBuilder.Append(DateTimeFormat.FormatMonth(month, num2, dtfi));
							}
							timeOnly = false;
						}
					}
					else
					{
						switch (c)
						{
						case 'd':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num2 <= 2)
							{
								int dayOfMonth = calendar.GetDayOfMonth(dateTime);
								if (flag)
								{
									DateTimeFormat.HebrewFormatDigits(stringBuilder, dayOfMonth);
								}
								else
								{
									DateTimeFormat.FormatDigits(stringBuilder, dayOfMonth, num2);
								}
							}
							else
							{
								int dayOfWeek = (int)calendar.GetDayOfWeek(dateTime);
								stringBuilder.Append(DateTimeFormat.FormatDayOfWeek(dayOfWeek, num2, dtfi));
							}
							timeOnly = false;
							break;
						case 'e':
							goto IL_64F;
						case 'f':
							goto IL_1E3;
						case 'g':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							stringBuilder.Append(dtfi.GetEraName(calendar.GetEra(dateTime)));
							break;
						case 'h':
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num3 = dateTime.Hour % 12;
							if (num3 == 0)
							{
								num3 = 12;
							}
							DateTimeFormat.FormatDigits(stringBuilder, num3, num2);
							break;
						}
						default:
							if (c != 'm')
							{
								goto IL_64F;
							}
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							DateTimeFormat.FormatDigits(stringBuilder, dateTime.Minute, num2);
							break;
						}
					}
				}
				else if (c <= 't')
				{
					if (c != 's')
					{
						if (c != 't')
						{
							goto IL_64F;
						}
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num2 == 1)
						{
							if (dateTime.Hour < 12)
							{
								if (dtfi.AMDesignator.Length >= 1)
								{
									stringBuilder.Append(dtfi.AMDesignator[0]);
								}
							}
							else if (dtfi.PMDesignator.Length >= 1)
							{
								stringBuilder.Append(dtfi.PMDesignator[0]);
							}
						}
						else
						{
							stringBuilder.Append((dateTime.Hour < 12) ? dtfi.AMDesignator : dtfi.PMDesignator);
						}
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(stringBuilder, dateTime.Second, num2);
					}
				}
				else if (c != 'y')
				{
					if (c != 'z')
					{
						goto IL_64F;
					}
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					DateTimeFormat.FormatCustomizedTimeZone(dateTime, offset, format, num2, timeOnly, stringBuilder);
				}
				else
				{
					int year = calendar.GetYear(dateTime);
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (flag2 && !AppContextSwitches.FormatJapaneseFirstYearAsANumber && year == 1 && ((i + num2 < format.Length && format[i + num2] == "年"[0]) || (i + num2 < format.Length - 1 && format[i + num2] == '\'' && format[i + num2 + 1] == "年"[0])))
					{
						stringBuilder.Append("元"[0]);
					}
					else if (dtfi.HasForceTwoDigitYears)
					{
						DateTimeFormat.FormatDigits(stringBuilder, year, (num2 <= 2) ? num2 : 2);
					}
					else if (calendar.ID == 8)
					{
						DateTimeFormat.HebrewFormatDigits(stringBuilder, year);
					}
					else if (num2 <= 2)
					{
						DateTimeFormat.FormatDigits(stringBuilder, year % 100, num2);
					}
					else
					{
						string format2 = "D" + num2;
						stringBuilder.Append(year.ToString(format2, CultureInfo.InvariantCulture));
					}
					timeOnly = false;
				}
				IL_65B:
				i += num2;
				continue;
				IL_1E3:
				num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
				if (num2 > 7)
				{
					throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
				}
				long num4 = dateTime.Ticks % 10000000L;
				num4 /= (long)Math.Pow(10.0, (double)(7 - num2));
				if (c == 'f')
				{
					stringBuilder.Append(((int)num4).ToString(DateTimeFormat.fixedNumberFormats[num2 - 1], CultureInfo.InvariantCulture));
					goto IL_65B;
				}
				int num5 = num2;
				while (num5 > 0 && num4 % 10L == 0L)
				{
					num4 /= 10L;
					num5--;
				}
				if (num5 > 0)
				{
					stringBuilder.Append(((int)num4).ToString(DateTimeFormat.fixedNumberFormats[num5 - 1], CultureInfo.InvariantCulture));
					goto IL_65B;
				}
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '.')
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					goto IL_65B;
				}
				goto IL_65B;
				IL_64F:
				stringBuilder.Append(c);
				num2 = 1;
				goto IL_65B;
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00040DC8 File Offset: 0x0003EFC8
		private static void FormatCustomizedTimeZone(DateTime dateTime, TimeSpan offset, string format, int tokenLen, bool timeOnly, StringBuilder result)
		{
			bool flag = offset == DateTimeFormat.NullOffset;
			if (flag)
			{
				if (timeOnly && dateTime.Ticks < 864000000000L)
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(DateTime.Now, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else if (dateTime.Kind == DateTimeKind.Utc)
				{
					DateTimeFormat.InvalidFormatForUtc(format, dateTime);
					dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
					offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
			}
			if (offset >= TimeSpan.Zero)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			if (tokenLen <= 1)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, "{0:0}", offset.Hours);
				return;
			}
			result.AppendFormat(CultureInfo.InvariantCulture, "{0:00}", offset.Hours);
			if (tokenLen >= 3)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, ":{0:00}", offset.Minutes);
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00040EC8 File Offset: 0x0003F0C8
		private static void FormatCustomizedRoundripTimeZone(DateTime dateTime, TimeSpan offset, StringBuilder result)
		{
			if (offset == DateTimeFormat.NullOffset)
			{
				DateTimeKind kind = dateTime.Kind;
				if (kind == DateTimeKind.Utc)
				{
					result.Append("Z");
					return;
				}
				if (kind != DateTimeKind.Local)
				{
					return;
				}
				offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			if (offset >= TimeSpan.Zero)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			result.AppendFormat(CultureInfo.InvariantCulture, "{0:00}:{1:00}", offset.Hours, offset.Minutes);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00040F60 File Offset: 0x0003F160
		internal static string GetRealFormat(string format, DateTimeFormatInfo dtfi)
		{
			char c = format[0];
			if (c > 'U')
			{
				if (c != 'Y')
				{
					switch (c)
					{
					case 'd':
						return dtfi.ShortDatePattern;
					case 'e':
						goto IL_159;
					case 'f':
						return dtfi.LongDatePattern + " " + dtfi.ShortTimePattern;
					case 'g':
						return dtfi.GeneralShortTimePattern;
					default:
						switch (c)
						{
						case 'm':
							goto IL_109;
						case 'n':
						case 'p':
						case 'q':
						case 'v':
						case 'w':
						case 'x':
							goto IL_159;
						case 'o':
							goto IL_112;
						case 'r':
							goto IL_11A;
						case 's':
							return dtfi.SortableDateTimePattern;
						case 't':
							return dtfi.ShortTimePattern;
						case 'u':
							return dtfi.UniversalSortableDateTimePattern;
						case 'y':
							break;
						default:
							goto IL_159;
						}
						break;
					}
				}
				return dtfi.YearMonthPattern;
			}
			switch (c)
			{
			case 'D':
				return dtfi.LongDatePattern;
			case 'E':
				goto IL_159;
			case 'F':
				return dtfi.FullDateTimePattern;
			case 'G':
				return dtfi.GeneralLongTimePattern;
			default:
				switch (c)
				{
				case 'M':
					break;
				case 'N':
				case 'P':
				case 'Q':
				case 'S':
					goto IL_159;
				case 'O':
					goto IL_112;
				case 'R':
					goto IL_11A;
				case 'T':
					return dtfi.LongTimePattern;
				case 'U':
					return dtfi.FullDateTimePattern;
				default:
					goto IL_159;
				}
				break;
			}
			IL_109:
			return dtfi.MonthDayPattern;
			IL_112:
			return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
			IL_11A:
			return dtfi.RFC1123Pattern;
			IL_159:
			throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000410D8 File Offset: 0x0003F2D8
		private static string ExpandPredefinedFormat(string format, ref DateTime dateTime, ref DateTimeFormatInfo dtfi, ref TimeSpan offset)
		{
			char c = format[0];
			if (c != 'U')
			{
				if (c != 's')
				{
					if (c == 'u')
					{
						if (offset != DateTimeFormat.NullOffset)
						{
							dateTime -= offset;
						}
						else if (dateTime.Kind == DateTimeKind.Local)
						{
							DateTimeFormat.InvalidFormatForLocal(format, dateTime);
						}
						dtfi = DateTimeFormatInfo.InvariantInfo;
					}
				}
				else
				{
					dtfi = DateTimeFormatInfo.InvariantInfo;
				}
			}
			else
			{
				if (offset != DateTimeFormat.NullOffset)
				{
					throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
				}
				dtfi = (DateTimeFormatInfo)dtfi.Clone();
				if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
				{
					dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
				}
				dateTime = dateTime.ToUniversalTime();
			}
			format = DateTimeFormat.GetRealFormat(format, dtfi);
			return format;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000411C6 File Offset: 0x0003F3C6
		internal static string Format(DateTime dateTime, string format, DateTimeFormatInfo dtfi)
		{
			return DateTimeFormat.Format(dateTime, format, dtfi, DateTimeFormat.NullOffset);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000411D8 File Offset: 0x0003F3D8
		internal static string Format(DateTime dateTime, string format, DateTimeFormatInfo dtfi, TimeSpan offset)
		{
			if (format == null || format.Length == 0)
			{
				bool flag = false;
				if (dateTime.Ticks < 864000000000L)
				{
					int id = dtfi.Calendar.ID;
					switch (id)
					{
					case 3:
					case 4:
					case 6:
					case 8:
						break;
					case 5:
					case 7:
						goto IL_63;
					default:
						if (id != 13 && id - 22 > 1)
						{
							goto IL_63;
						}
						break;
					}
					flag = true;
					dtfi = DateTimeFormatInfo.InvariantInfo;
				}
				IL_63:
				if (offset == DateTimeFormat.NullOffset)
				{
					if (flag)
					{
						format = "s";
					}
					else
					{
						format = "G";
					}
				}
				else if (flag)
				{
					format = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";
				}
				else
				{
					format = dtfi.DateTimeOffsetPattern;
				}
			}
			if (format.Length == 1)
			{
				char c = format[0];
				if (c <= 'R')
				{
					if (c != 'O')
					{
						if (c != 'R')
						{
							goto IL_E2;
						}
						goto IL_D4;
					}
				}
				else if (c != 'o')
				{
					if (c != 'r')
					{
						goto IL_E2;
					}
					goto IL_D4;
				}
				return StringBuilderCache.GetStringAndRelease(DateTimeFormat.FastFormatRoundtrip(dateTime, offset));
				IL_D4:
				return StringBuilderCache.GetStringAndRelease(DateTimeFormat.FastFormatRfc1123(dateTime, offset, dtfi));
				IL_E2:
				format = DateTimeFormat.ExpandPredefinedFormat(format, ref dateTime, ref dtfi, ref offset);
			}
			return DateTimeFormat.FormatCustomized(dateTime, format, dtfi, offset);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000412E0 File Offset: 0x0003F4E0
		internal static StringBuilder FastFormatRfc1123(DateTime dateTime, TimeSpan offset, DateTimeFormatInfo dtfi)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(29);
			if (offset != DateTimeFormat.NullOffset)
			{
				dateTime -= offset;
			}
			int num;
			int num2;
			int num3;
			dateTime.GetDatePart(out num, out num2, out num3);
			stringBuilder.Append(DateTimeFormat.InvariantAbbreviatedDayNames[(int)dateTime.DayOfWeek]);
			stringBuilder.Append(',');
			stringBuilder.Append(' ');
			DateTimeFormat.AppendNumber(stringBuilder, (long)num3, 2);
			stringBuilder.Append(' ');
			stringBuilder.Append(DateTimeFormat.InvariantAbbreviatedMonthNames[num2 - 1]);
			stringBuilder.Append(' ');
			DateTimeFormat.AppendNumber(stringBuilder, (long)num, 4);
			stringBuilder.Append(' ');
			DateTimeFormat.AppendHHmmssTimeOfDay(stringBuilder, dateTime);
			stringBuilder.Append(' ');
			stringBuilder.Append("GMT");
			return stringBuilder;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00041398 File Offset: 0x0003F598
		internal static StringBuilder FastFormatRoundtrip(DateTime dateTime, TimeSpan offset)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(28);
			int num;
			int num2;
			int num3;
			dateTime.GetDatePart(out num, out num2, out num3);
			DateTimeFormat.AppendNumber(stringBuilder, (long)num, 4);
			stringBuilder.Append('-');
			DateTimeFormat.AppendNumber(stringBuilder, (long)num2, 2);
			stringBuilder.Append('-');
			DateTimeFormat.AppendNumber(stringBuilder, (long)num3, 2);
			stringBuilder.Append('T');
			DateTimeFormat.AppendHHmmssTimeOfDay(stringBuilder, dateTime);
			stringBuilder.Append('.');
			long val = dateTime.Ticks % 10000000L;
			DateTimeFormat.AppendNumber(stringBuilder, val, 7);
			DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, stringBuilder);
			return stringBuilder;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00041424 File Offset: 0x0003F624
		private static void AppendHHmmssTimeOfDay(StringBuilder result, DateTime dateTime)
		{
			DateTimeFormat.AppendNumber(result, (long)dateTime.Hour, 2);
			result.Append(':');
			DateTimeFormat.AppendNumber(result, (long)dateTime.Minute, 2);
			result.Append(':');
			DateTimeFormat.AppendNumber(result, (long)dateTime.Second, 2);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00041470 File Offset: 0x0003F670
		internal static void AppendNumber(StringBuilder builder, long val, int digits)
		{
			for (int i = 0; i < digits; i++)
			{
				builder.Append('0');
			}
			int num = 1;
			while (val > 0L && num <= digits)
			{
				builder[builder.Length - num] = (char)(48L + val % 10L);
				val /= 10L;
				num++;
			}
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000414C4 File Offset: 0x0003F6C4
		internal static string[] GetAllDateTimes(DateTime dateTime, char format, DateTimeFormatInfo dtfi)
		{
			string[] allDateTimePatterns;
			string[] array;
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
				case 'F':
				case 'G':
					break;
				case 'E':
					goto IL_140;
				default:
					switch (format)
					{
					case 'M':
					case 'T':
						break;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_140;
					case 'O':
					case 'R':
						goto IL_11E;
					case 'U':
					{
						DateTime dateTime2 = dateTime.ToUniversalTime();
						allDateTimePatterns = dtfi.GetAllDateTimePatterns(format);
						array = new string[allDateTimePatterns.Length];
						for (int i = 0; i < allDateTimePatterns.Length; i++)
						{
							array[i] = DateTimeFormat.Format(dateTime2, allDateTimePatterns[i], dtfi);
						}
						return array;
					}
					default:
						goto IL_140;
					}
					break;
				}
			}
			else if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
				case 'f':
				case 'g':
					break;
				case 'e':
					goto IL_140;
				default:
					switch (format)
					{
					case 'm':
					case 't':
					case 'y':
						break;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_140;
					case 'o':
					case 'r':
					case 's':
					case 'u':
						goto IL_11E;
					default:
						goto IL_140;
					}
					break;
				}
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns(format);
			array = new string[allDateTimePatterns.Length];
			for (int j = 0; j < allDateTimePatterns.Length; j++)
			{
				array[j] = DateTimeFormat.Format(dateTime, allDateTimePatterns[j], dtfi);
			}
			return array;
			IL_11E:
			return new string[]
			{
				DateTimeFormat.Format(dateTime, new string(new char[]
				{
					format
				}), dtfi)
			};
			IL_140:
			throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00041624 File Offset: 0x0003F824
		internal static string[] GetAllDateTimes(DateTime dateTime, DateTimeFormatInfo dtfi)
		{
			List<string> list = new List<string>(132);
			for (int i = 0; i < DateTimeFormat.allStandardFormats.Length; i++)
			{
				string[] allDateTimes = DateTimeFormat.GetAllDateTimes(dateTime, DateTimeFormat.allStandardFormats[i], dtfi);
				for (int j = 0; j < allDateTimes.Length; j++)
				{
					list.Add(allDateTimes[j]);
				}
			}
			string[] array = new string[list.Count];
			list.CopyTo(0, array, 0, list.Count);
			return array;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00041695 File Offset: 0x0003F895
		internal static void InvalidFormatForLocal(string format, DateTime dateTime)
		{
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00041697 File Offset: 0x0003F897
		[SecuritySafeCritical]
		internal static void InvalidFormatForUtc(string format, DateTime dateTime)
		{
			Mda.DateTimeInvalidLocalFormat();
		}

		// Token: 0x0400074C RID: 1868
		internal const int MaxSecondsFractionDigits = 7;

		// Token: 0x0400074D RID: 1869
		internal static readonly TimeSpan NullOffset = TimeSpan.MinValue;

		// Token: 0x0400074E RID: 1870
		internal static char[] allStandardFormats = new char[]
		{
			'd',
			'D',
			'f',
			'F',
			'g',
			'G',
			'm',
			'M',
			'o',
			'O',
			'r',
			'R',
			's',
			't',
			'T',
			'u',
			'U',
			'y',
			'Y'
		};

		// Token: 0x0400074F RID: 1871
		internal const string RoundtripFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";

		// Token: 0x04000750 RID: 1872
		internal const string RoundtripDateTimeUnfixed = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";

		// Token: 0x04000751 RID: 1873
		private const int DEFAULT_ALL_DATETIMES_SIZE = 132;

		// Token: 0x04000752 RID: 1874
		internal static readonly DateTimeFormatInfo InvariantFormatInfo = CultureInfo.InvariantCulture.DateTimeFormat;

		// Token: 0x04000753 RID: 1875
		internal static readonly string[] InvariantAbbreviatedMonthNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedMonthNames;

		// Token: 0x04000754 RID: 1876
		internal static readonly string[] InvariantAbbreviatedDayNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedDayNames;

		// Token: 0x04000755 RID: 1877
		internal const string Gmt = "GMT";

		// Token: 0x04000756 RID: 1878
		internal static string[] fixedNumberFormats = new string[]
		{
			"0",
			"00",
			"000",
			"0000",
			"00000",
			"000000",
			"0000000"
		};
	}
}
