using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020003AB RID: 939
	internal static class TimeSpanParse
	{
		// Token: 0x060030D1 RID: 12497 RVA: 0x000BAA65 File Offset: 0x000B8C65
		internal static void ValidateStyles(TimeSpanStyles style, string parameterName)
		{
			if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTimeSpanStyles"), parameterName);
			}
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000BAA80 File Offset: 0x000B8C80
		private static bool TryTimeToTicks(bool positive, TimeSpanParse.TimeSpanToken days, TimeSpanParse.TimeSpanToken hours, TimeSpanParse.TimeSpanToken minutes, TimeSpanParse.TimeSpanToken seconds, TimeSpanParse.TimeSpanToken fraction, out long result)
		{
			if (days.IsInvalidNumber(10675199, -1) || hours.IsInvalidNumber(23, -1) || minutes.IsInvalidNumber(59, -1) || seconds.IsInvalidNumber(59, -1) || fraction.IsInvalidNumber(9999999, 7))
			{
				result = 0L;
				return false;
			}
			long num = ((long)days.num * 3600L * 24L + (long)hours.num * 3600L + (long)minutes.num * 60L + (long)seconds.num) * 1000L;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				result = 0L;
				return false;
			}
			long num2 = (long)fraction.num;
			if (num2 != 0L)
			{
				long num3 = 1000000L;
				if (fraction.zeroes > 0)
				{
					long num4 = (long)Math.Pow(10.0, (double)fraction.zeroes);
					num3 /= num4;
				}
				while (num2 < num3)
				{
					num2 *= 10L;
				}
			}
			result = num * 10000L + num2;
			if (positive && result < 0L)
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000BAB98 File Offset: 0x000B8D98
		internal static TimeSpan Parse(string input, IFormatProvider formatProvider)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000BABD0 File Offset: 0x000B8DD0
		internal static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000BAC10 File Offset: 0x000B8E10
		internal static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000BAC48 File Offset: 0x000B8E48
		internal static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000BAC8C File Offset: 0x000B8E8C
		internal static TimeSpan ParseExactMultiple(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000BACC4 File Offset: 0x000B8EC4
		internal static bool TryParseExactMultiple(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000BAD08 File Offset: 0x000B8F08
		private static bool TryParseTimeSpan(string input, TimeSpanParse.TimeSpanStandardStyles style, IFormatProvider formatProvider, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "input");
				return false;
			}
			input = input.Trim();
			if (input == string.Empty)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = default(TimeSpanParse.TimeSpanTokenizer);
			timeSpanTokenizer.Init(input);
			TimeSpanParse.TimeSpanRawInfo timeSpanRawInfo = default(TimeSpanParse.TimeSpanRawInfo);
			timeSpanRawInfo.Init(DateTimeFormatInfo.GetInstance(formatProvider));
			TimeSpanParse.TimeSpanToken nextToken = timeSpanTokenizer.GetNextToken();
			while (nextToken.ttt != TimeSpanParse.TTT.End)
			{
				if (!timeSpanRawInfo.ProcessToken(ref nextToken, ref result))
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
				nextToken = timeSpanTokenizer.GetNextToken();
			}
			if (!timeSpanTokenizer.EOL)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			if (!TimeSpanParse.ProcessTerminalState(ref timeSpanRawInfo, style, ref result))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			return true;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000BADDC File Offset: 0x000B8FDC
		private static bool ProcessTerminalState(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.lastSeenTTT == TimeSpanParse.TTT.Num)
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				timeSpanToken.ttt = TimeSpanParse.TTT.Sep;
				timeSpanToken.sep = string.Empty;
				if (!raw.ProcessToken(ref timeSpanToken, ref result))
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
			}
			switch (raw.NumCount)
			{
			case 1:
				return TimeSpanParse.ProcessTerminal_D(ref raw, style, ref result);
			case 2:
				return TimeSpanParse.ProcessTerminal_HM(ref raw, style, ref result);
			case 3:
				return TimeSpanParse.ProcessTerminal_HM_S_D(ref raw, style, ref result);
			case 4:
				return TimeSpanParse.ProcessTerminal_HMS_F_D(ref raw, style, ref result);
			case 5:
				return TimeSpanParse.ProcessTerminal_DHMSF(ref raw, style, ref result);
			default:
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000BAE88 File Offset: 0x000B9088
		private static bool ProcessTerminal_DHMSF(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 6 || raw.NumCount != 5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], raw.numbers[4], out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000BAFB0 File Offset: 0x000B91B0
		private static bool ProcessTerminal_HMS_F_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 5 || raw.NumCount != 4 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			long num = 0L;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			if (flag)
			{
				if (raw.FullHMSFMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSFMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}
			if (flag5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
			return false;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000BB4DC File Offset: 0x000B96DC
		private static bool ProcessTerminal_HM_S_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 4 || raw.NumCount != 3 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			long num = 0L;
			if (flag)
			{
				if (raw.FullHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}
			if (flag5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
			return false;
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000BB994 File Offset: 0x000B9B94
		private static bool ProcessTerminal_HM(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 3 || raw.NumCount != 2 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullHMMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			long num = 0L;
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			if (!TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, TimeSpanParse.zero, out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000BBAB0 File Offset: 0x000B9CB0
		private static bool ProcessTerminal_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 2 || raw.NumCount != 1 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullDMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullDMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			long num = 0L;
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], TimeSpanParse.zero, TimeSpanParse.zero, TimeSpanParse.zero, TimeSpanParse.zero, out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000BBBC4 File Offset: 0x000B9DC4
		private static bool TryParseExactTimeSpan(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "input");
				return false;
			}
			if (format == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "format");
				return false;
			}
			if (format.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
				return false;
			}
			if (format.Length != 1)
			{
				return TimeSpanParse.TryParseByFormat(input, format, styles, ref result);
			}
			if (format[0] == 'c' || format[0] == 't' || format[0] == 'T')
			{
				return TimeSpanParse.TryParseTimeSpanConstant(input, ref result);
			}
			TimeSpanParse.TimeSpanStandardStyles style;
			if (format[0] == 'g')
			{
				style = TimeSpanParse.TimeSpanStandardStyles.Localized;
			}
			else
			{
				if (format[0] != 'G')
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
					return false;
				}
				style = (TimeSpanParse.TimeSpanStandardStyles.Localized | TimeSpanParse.TimeSpanStandardStyles.RequireFull);
			}
			return TimeSpanParse.TryParseTimeSpan(input, style, formatProvider, ref result);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000BBC90 File Offset: 0x000B9E90
		private static bool TryParseByFormat(string input, string format, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			int number = 0;
			int number2 = 0;
			int number3 = 0;
			int number4 = 0;
			int leadingZeroes = 0;
			int number5 = 0;
			int i = 0;
			int num = 0;
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = default(TimeSpanParse.TimeSpanTokenizer);
			timeSpanTokenizer.Init(input, -1);
			while (i < format.Length)
			{
				char c = format[i];
				if (c <= 'F')
				{
					if (c <= '%')
					{
						if (c != '"')
						{
							if (c != '%')
							{
								goto IL_2C5;
							}
							int num2 = DateTimeFormat.ParseNextChar(format, i);
							if (num2 >= 0 && num2 != 37)
							{
								num = 1;
								goto IL_2D3;
							}
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
							return false;
						}
					}
					else if (c != '\'')
					{
						if (c != 'F')
						{
							goto IL_2C5;
						}
						num = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num > 7 || flag5)
						{
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
							return false;
						}
						TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, num, out leadingZeroes, out number5);
						flag5 = true;
						goto IL_2D3;
					}
					StringBuilder stringBuilder = new StringBuilder();
					if (!DateTimeParse.TryParseQuoteString(format, i, stringBuilder, out num))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.FormatWithParameter, "Format_BadQuote", c);
						return false;
					}
					if (!TimeSpanParse.ParseExactLiteral(ref timeSpanTokenizer, stringBuilder))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
						return false;
					}
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case 'd':
						{
							num = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num3 = 0;
							if (num > 8 || flag || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, (num < 2) ? 1 : num, (num < 2) ? 8 : num, out num3, out number))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
								return false;
							}
							flag = true;
							break;
						}
						case 'e':
						case 'g':
							goto IL_2C5;
						case 'f':
							num = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num > 7 || flag5 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, num, out leadingZeroes, out number5))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
								return false;
							}
							flag5 = true;
							break;
						case 'h':
							num = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num > 2 || flag2 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, out number2))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
								return false;
							}
							flag2 = true;
							break;
						default:
							goto IL_2C5;
						}
					}
					else
					{
						int num2 = DateTimeFormat.ParseNextChar(format, i);
						if (num2 < 0 || timeSpanTokenizer.NextChar != (char)num2)
						{
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
							return false;
						}
						num = 2;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						goto IL_2C5;
					}
					num = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num > 2 || flag4 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, out number4))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
						return false;
					}
					flag4 = true;
				}
				else
				{
					num = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num > 2 || flag3 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, out number3))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
						return false;
					}
					flag3 = true;
				}
				IL_2D3:
				i += num;
				continue;
				IL_2C5:
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
				return false;
			}
			if (!timeSpanTokenizer.EOL)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			long num4 = 0L;
			bool flag6 = (styles & TimeSpanStyles.AssumeNegative) == TimeSpanStyles.None;
			if (TimeSpanParse.TryTimeToTicks(flag6, new TimeSpanParse.TimeSpanToken(number), new TimeSpanParse.TimeSpanToken(number2), new TimeSpanParse.TimeSpanToken(number3), new TimeSpanParse.TimeSpanToken(number4), new TimeSpanParse.TimeSpanToken(leadingZeroes, number5), out num4))
			{
				if (!flag6)
				{
					num4 = -num4;
				}
				result.parsedTimeSpan._ticks = num4;
				return true;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
			return false;
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000BBFFC File Offset: 0x000BA1FC
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, out int result)
		{
			result = 0;
			int num = 0;
			int maxDigitLength = (minDigitLength == 1) ? 2 : minDigitLength;
			return TimeSpanParse.ParseExactDigits(ref tokenizer, minDigitLength, maxDigitLength, out num, out result);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000BC024 File Offset: 0x000BA224
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, int maxDigitLength, out int zeroes, out int result)
		{
			result = 0;
			zeroes = 0;
			int i;
			for (i = 0; i < maxDigitLength; i++)
			{
				char nextChar = tokenizer.NextChar;
				if (nextChar < '0' || nextChar > '9')
				{
					tokenizer.BackOne();
					break;
				}
				result = result * 10 + (int)(nextChar - '0');
				if (result == 0)
				{
					zeroes++;
				}
			}
			return i >= minDigitLength;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000BC080 File Offset: 0x000BA280
		private static bool ParseExactLiteral(ref TimeSpanParse.TimeSpanTokenizer tokenizer, StringBuilder enquotedString)
		{
			for (int i = 0; i < enquotedString.Length; i++)
			{
				if (enquotedString[i] != tokenizer.NextChar)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000BC0B0 File Offset: 0x000BA2B0
		private static bool TryParseTimeSpanConstant(string input, ref TimeSpanParse.TimeSpanResult result)
		{
			return default(TimeSpanParse.StringParser).TryParse(input, ref result);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000BC0D0 File Offset: 0x000BA2D0
		private static bool TryParseExactMultipleTimeSpan(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "input");
				return false;
			}
			if (formats == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "formats");
				return false;
			}
			if (input.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			if (formats.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
				return false;
			}
			for (int i = 0; i < formats.Length; i++)
			{
				if (formats[i] == null || formats[i].Length == 0)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
					return false;
				}
				TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
				timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
				if (TimeSpanParse.TryParseExactTimeSpan(input, formats[i], formatProvider, styles, ref timeSpanResult))
				{
					result.parsedTimeSpan = timeSpanResult.parsedTimeSpan;
					return true;
				}
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
			return false;
		}

		// Token: 0x0400148A RID: 5258
		internal const int unlimitedDigits = -1;

		// Token: 0x0400148B RID: 5259
		internal const int maxFractionDigits = 7;

		// Token: 0x0400148C RID: 5260
		internal const int maxDays = 10675199;

		// Token: 0x0400148D RID: 5261
		internal const int maxHours = 23;

		// Token: 0x0400148E RID: 5262
		internal const int maxMinutes = 59;

		// Token: 0x0400148F RID: 5263
		internal const int maxSeconds = 59;

		// Token: 0x04001490 RID: 5264
		internal const int maxFraction = 9999999;

		// Token: 0x04001491 RID: 5265
		private static readonly TimeSpanParse.TimeSpanToken zero = new TimeSpanParse.TimeSpanToken(0);

		// Token: 0x02000B3D RID: 2877
		private enum TimeSpanThrowStyle
		{
			// Token: 0x04003397 RID: 13207
			None,
			// Token: 0x04003398 RID: 13208
			All
		}

		// Token: 0x02000B3E RID: 2878
		private enum ParseFailureKind
		{
			// Token: 0x0400339A RID: 13210
			None,
			// Token: 0x0400339B RID: 13211
			ArgumentNull,
			// Token: 0x0400339C RID: 13212
			Format,
			// Token: 0x0400339D RID: 13213
			FormatWithParameter,
			// Token: 0x0400339E RID: 13214
			Overflow
		}

		// Token: 0x02000B3F RID: 2879
		[Flags]
		private enum TimeSpanStandardStyles
		{
			// Token: 0x040033A0 RID: 13216
			None = 0,
			// Token: 0x040033A1 RID: 13217
			Invariant = 1,
			// Token: 0x040033A2 RID: 13218
			Localized = 2,
			// Token: 0x040033A3 RID: 13219
			RequireFull = 4,
			// Token: 0x040033A4 RID: 13220
			Any = 3
		}

		// Token: 0x02000B40 RID: 2880
		private enum TTT
		{
			// Token: 0x040033A6 RID: 13222
			None,
			// Token: 0x040033A7 RID: 13223
			End,
			// Token: 0x040033A8 RID: 13224
			Num,
			// Token: 0x040033A9 RID: 13225
			Sep,
			// Token: 0x040033AA RID: 13226
			NumOverflow
		}

		// Token: 0x02000B41 RID: 2881
		private struct TimeSpanToken
		{
			// Token: 0x06006B07 RID: 27399 RVA: 0x00171AD0 File Offset: 0x0016FCD0
			public TimeSpanToken(int number)
			{
				this.ttt = TimeSpanParse.TTT.Num;
				this.num = number;
				this.zeroes = 0;
				this.sep = null;
			}

			// Token: 0x06006B08 RID: 27400 RVA: 0x00171AEE File Offset: 0x0016FCEE
			public TimeSpanToken(int leadingZeroes, int number)
			{
				this.ttt = TimeSpanParse.TTT.Num;
				this.num = number;
				this.zeroes = leadingZeroes;
				this.sep = null;
			}

			// Token: 0x06006B09 RID: 27401 RVA: 0x00171B0C File Offset: 0x0016FD0C
			public bool IsInvalidNumber(int maxValue, int maxPrecision)
			{
				return this.num > maxValue || (maxPrecision != -1 && (this.zeroes > maxPrecision || (this.num != 0 && this.zeroes != 0 && (long)this.num >= (long)maxValue / (long)Math.Pow(10.0, (double)(this.zeroes - 1)))));
			}

			// Token: 0x040033AB RID: 13227
			internal TimeSpanParse.TTT ttt;

			// Token: 0x040033AC RID: 13228
			internal int num;

			// Token: 0x040033AD RID: 13229
			internal int zeroes;

			// Token: 0x040033AE RID: 13230
			internal string sep;
		}

		// Token: 0x02000B42 RID: 2882
		private struct TimeSpanTokenizer
		{
			// Token: 0x06006B0A RID: 27402 RVA: 0x00171B6E File Offset: 0x0016FD6E
			internal void Init(string input)
			{
				this.Init(input, 0);
			}

			// Token: 0x06006B0B RID: 27403 RVA: 0x00171B78 File Offset: 0x0016FD78
			internal void Init(string input, int startPosition)
			{
				this.m_pos = startPosition;
				this.m_value = input;
			}

			// Token: 0x06006B0C RID: 27404 RVA: 0x00171B88 File Offset: 0x0016FD88
			internal TimeSpanParse.TimeSpanToken GetNextToken()
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				char c = this.CurrentChar;
				if (c == '\0')
				{
					timeSpanToken.ttt = TimeSpanParse.TTT.End;
					return timeSpanToken;
				}
				if (c >= '0' && c <= '9')
				{
					timeSpanToken.ttt = TimeSpanParse.TTT.Num;
					timeSpanToken.num = 0;
					timeSpanToken.zeroes = 0;
					while (((long)timeSpanToken.num & (long)((ulong)-268435456)) == 0L)
					{
						timeSpanToken.num = timeSpanToken.num * 10 + (int)c - 48;
						if (timeSpanToken.num == 0)
						{
							timeSpanToken.zeroes++;
						}
						if (timeSpanToken.num < 0)
						{
							timeSpanToken.ttt = TimeSpanParse.TTT.NumOverflow;
							return timeSpanToken;
						}
						c = this.NextChar;
						if (c < '0' || c > '9')
						{
							return timeSpanToken;
						}
					}
					timeSpanToken.ttt = TimeSpanParse.TTT.NumOverflow;
					return timeSpanToken;
				}
				timeSpanToken.ttt = TimeSpanParse.TTT.Sep;
				int pos = this.m_pos;
				int num = 0;
				while (c != '\0' && (c < '0' || '9' < c))
				{
					c = this.NextChar;
					num++;
				}
				timeSpanToken.sep = this.m_value.Substring(pos, num);
				return timeSpanToken;
			}

			// Token: 0x17001239 RID: 4665
			// (get) Token: 0x06006B0D RID: 27405 RVA: 0x00171C82 File Offset: 0x0016FE82
			internal bool EOL
			{
				get
				{
					return this.m_pos >= this.m_value.Length - 1;
				}
			}

			// Token: 0x06006B0E RID: 27406 RVA: 0x00171C9C File Offset: 0x0016FE9C
			internal void BackOne()
			{
				if (this.m_pos > 0)
				{
					this.m_pos--;
				}
			}

			// Token: 0x1700123A RID: 4666
			// (get) Token: 0x06006B0F RID: 27407 RVA: 0x00171CB5 File Offset: 0x0016FEB5
			internal char NextChar
			{
				get
				{
					this.m_pos++;
					return this.CurrentChar;
				}
			}

			// Token: 0x1700123B RID: 4667
			// (get) Token: 0x06006B10 RID: 27408 RVA: 0x00171CCB File Offset: 0x0016FECB
			internal char CurrentChar
			{
				get
				{
					if (this.m_pos > -1 && this.m_pos < this.m_value.Length)
					{
						return this.m_value[this.m_pos];
					}
					return '\0';
				}
			}

			// Token: 0x040033AF RID: 13231
			private int m_pos;

			// Token: 0x040033B0 RID: 13232
			private string m_value;
		}

		// Token: 0x02000B43 RID: 2883
		private struct TimeSpanRawInfo
		{
			// Token: 0x1700123C RID: 4668
			// (get) Token: 0x06006B11 RID: 27409 RVA: 0x00171CFC File Offset: 0x0016FEFC
			internal TimeSpanFormat.FormatLiterals PositiveInvariant
			{
				get
				{
					return TimeSpanFormat.PositiveInvariantFormatLiterals;
				}
			}

			// Token: 0x1700123D RID: 4669
			// (get) Token: 0x06006B12 RID: 27410 RVA: 0x00171D03 File Offset: 0x0016FF03
			internal TimeSpanFormat.FormatLiterals NegativeInvariant
			{
				get
				{
					return TimeSpanFormat.NegativeInvariantFormatLiterals;
				}
			}

			// Token: 0x1700123E RID: 4670
			// (get) Token: 0x06006B13 RID: 27411 RVA: 0x00171D0A File Offset: 0x0016FF0A
			internal TimeSpanFormat.FormatLiterals PositiveLocalized
			{
				get
				{
					if (!this.m_posLocInit)
					{
						this.m_posLoc = default(TimeSpanFormat.FormatLiterals);
						this.m_posLoc.Init(this.m_fullPosPattern, false);
						this.m_posLocInit = true;
					}
					return this.m_posLoc;
				}
			}

			// Token: 0x1700123F RID: 4671
			// (get) Token: 0x06006B14 RID: 27412 RVA: 0x00171D3F File Offset: 0x0016FF3F
			internal TimeSpanFormat.FormatLiterals NegativeLocalized
			{
				get
				{
					if (!this.m_negLocInit)
					{
						this.m_negLoc = default(TimeSpanFormat.FormatLiterals);
						this.m_negLoc.Init(this.m_fullNegPattern, false);
						this.m_negLocInit = true;
					}
					return this.m_negLoc;
				}
			}

			// Token: 0x06006B15 RID: 27413 RVA: 0x00171D74 File Offset: 0x0016FF74
			internal bool FullAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.AppCompatLiteral == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x06006B16 RID: 27414 RVA: 0x00171E00 File Offset: 0x00170000
			internal bool PartialAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.AppCompatLiteral == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x06006B17 RID: 27415 RVA: 0x00171E78 File Offset: 0x00170078
			internal bool FullMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 6 && this.NumCount == 5 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.MinuteSecondSep == this.literals[3] && pattern.SecondFractionSep == this.literals[4] && pattern.End == this.literals[5];
			}

			// Token: 0x06006B18 RID: 27416 RVA: 0x00171F21 File Offset: 0x00170121
			internal bool FullDMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 2 && this.NumCount == 1 && pattern.Start == this.literals[0] && pattern.End == this.literals[1];
			}

			// Token: 0x06006B19 RID: 27417 RVA: 0x00171F64 File Offset: 0x00170164
			internal bool FullHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 3 && this.NumCount == 2 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.End == this.literals[2];
			}

			// Token: 0x06006B1A RID: 27418 RVA: 0x00171FC8 File Offset: 0x001701C8
			internal bool FullDHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x06006B1B RID: 27419 RVA: 0x00172040 File Offset: 0x00170240
			internal bool FullHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.MinuteSecondSep == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x06006B1C RID: 27420 RVA: 0x001720B8 File Offset: 0x001702B8
			internal bool FullDHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.MinuteSecondSep == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x06006B1D RID: 27421 RVA: 0x00172148 File Offset: 0x00170348
			internal bool FullHMSFMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.MinuteSecondSep == this.literals[2] && pattern.SecondFractionSep == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x06006B1E RID: 27422 RVA: 0x001721D8 File Offset: 0x001703D8
			internal void Init(DateTimeFormatInfo dtfi)
			{
				this.lastSeenTTT = TimeSpanParse.TTT.None;
				this.tokenCount = 0;
				this.SepCount = 0;
				this.NumCount = 0;
				this.literals = new string[6];
				this.numbers = new TimeSpanParse.TimeSpanToken[5];
				this.m_fullPosPattern = dtfi.FullTimeSpanPositivePattern;
				this.m_fullNegPattern = dtfi.FullTimeSpanNegativePattern;
				this.m_posLocInit = false;
				this.m_negLocInit = false;
			}

			// Token: 0x06006B1F RID: 27423 RVA: 0x00172240 File Offset: 0x00170440
			internal bool ProcessToken(ref TimeSpanParse.TimeSpanToken tok, ref TimeSpanParse.TimeSpanResult result)
			{
				if (tok.ttt == TimeSpanParse.TTT.NumOverflow)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge", null);
					return false;
				}
				if (tok.ttt != TimeSpanParse.TTT.Sep && tok.ttt != TimeSpanParse.TTT.Num)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", null);
					return false;
				}
				TimeSpanParse.TTT ttt = tok.ttt;
				if (ttt != TimeSpanParse.TTT.Num)
				{
					if (ttt == TimeSpanParse.TTT.Sep && !this.AddSep(tok.sep, ref result))
					{
						return false;
					}
				}
				else
				{
					if (this.tokenCount == 0 && !this.AddSep(string.Empty, ref result))
					{
						return false;
					}
					if (!this.AddNum(tok, ref result))
					{
						return false;
					}
				}
				this.lastSeenTTT = tok.ttt;
				return true;
			}

			// Token: 0x06006B20 RID: 27424 RVA: 0x001722DC File Offset: 0x001704DC
			private bool AddSep(string sep, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this.SepCount >= 6 || this.tokenCount >= 11)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", null);
					return false;
				}
				string[] array = this.literals;
				int sepCount = this.SepCount;
				this.SepCount = sepCount + 1;
				array[sepCount] = sep;
				this.tokenCount++;
				return true;
			}

			// Token: 0x06006B21 RID: 27425 RVA: 0x00172334 File Offset: 0x00170534
			private bool AddNum(TimeSpanParse.TimeSpanToken num, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this.NumCount >= 5 || this.tokenCount >= 11)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", null);
					return false;
				}
				TimeSpanParse.TimeSpanToken[] array = this.numbers;
				int numCount = this.NumCount;
				this.NumCount = numCount + 1;
				array[numCount] = num;
				this.tokenCount++;
				return true;
			}

			// Token: 0x040033B1 RID: 13233
			internal TimeSpanParse.TTT lastSeenTTT;

			// Token: 0x040033B2 RID: 13234
			internal int tokenCount;

			// Token: 0x040033B3 RID: 13235
			internal int SepCount;

			// Token: 0x040033B4 RID: 13236
			internal int NumCount;

			// Token: 0x040033B5 RID: 13237
			internal string[] literals;

			// Token: 0x040033B6 RID: 13238
			internal TimeSpanParse.TimeSpanToken[] numbers;

			// Token: 0x040033B7 RID: 13239
			private TimeSpanFormat.FormatLiterals m_posLoc;

			// Token: 0x040033B8 RID: 13240
			private TimeSpanFormat.FormatLiterals m_negLoc;

			// Token: 0x040033B9 RID: 13241
			private bool m_posLocInit;

			// Token: 0x040033BA RID: 13242
			private bool m_negLocInit;

			// Token: 0x040033BB RID: 13243
			private string m_fullPosPattern;

			// Token: 0x040033BC RID: 13244
			private string m_fullNegPattern;

			// Token: 0x040033BD RID: 13245
			private const int MaxTokens = 11;

			// Token: 0x040033BE RID: 13246
			private const int MaxLiteralTokens = 6;

			// Token: 0x040033BF RID: 13247
			private const int MaxNumericTokens = 5;
		}

		// Token: 0x02000B44 RID: 2884
		private struct TimeSpanResult
		{
			// Token: 0x06006B22 RID: 27426 RVA: 0x0017238F File Offset: 0x0017058F
			internal void Init(TimeSpanParse.TimeSpanThrowStyle canThrow)
			{
				this.parsedTimeSpan = default(TimeSpan);
				this.throwStyle = canThrow;
			}

			// Token: 0x06006B23 RID: 27427 RVA: 0x001723A4 File Offset: 0x001705A4
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID)
			{
				this.SetFailure(failure, failureMessageID, null, null);
			}

			// Token: 0x06006B24 RID: 27428 RVA: 0x001723B0 File Offset: 0x001705B0
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, null);
			}

			// Token: 0x06006B25 RID: 27429 RVA: 0x001723BC File Offset: 0x001705BC
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				this.m_failureArgumentName = failureArgumentName;
				if (this.throwStyle != TimeSpanParse.TimeSpanThrowStyle.None)
				{
					throw this.GetTimeSpanParseException();
				}
			}

			// Token: 0x06006B26 RID: 27430 RVA: 0x001723EC File Offset: 0x001705EC
			internal Exception GetTimeSpanParseException()
			{
				switch (this.m_failure)
				{
				case TimeSpanParse.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureArgumentName, Environment.GetResourceString(this.m_failureMessageID));
				case TimeSpanParse.ParseFailureKind.Format:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID));
				case TimeSpanParse.ParseFailureKind.FormatWithParameter:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID, new object[]
					{
						this.m_failureMessageFormatArgument
					}));
				case TimeSpanParse.ParseFailureKind.Overflow:
					return new OverflowException(Environment.GetResourceString(this.m_failureMessageID));
				default:
					return new FormatException(Environment.GetResourceString("Format_InvalidString"));
				}
			}

			// Token: 0x040033C0 RID: 13248
			internal TimeSpan parsedTimeSpan;

			// Token: 0x040033C1 RID: 13249
			internal TimeSpanParse.TimeSpanThrowStyle throwStyle;

			// Token: 0x040033C2 RID: 13250
			internal TimeSpanParse.ParseFailureKind m_failure;

			// Token: 0x040033C3 RID: 13251
			internal string m_failureMessageID;

			// Token: 0x040033C4 RID: 13252
			internal object m_failureMessageFormatArgument;

			// Token: 0x040033C5 RID: 13253
			internal string m_failureArgumentName;
		}

		// Token: 0x02000B45 RID: 2885
		private struct StringParser
		{
			// Token: 0x06006B27 RID: 27431 RVA: 0x00172484 File Offset: 0x00170684
			internal void NextChar()
			{
				if (this.pos < this.len)
				{
					this.pos++;
				}
				this.ch = ((this.pos < this.len) ? this.str[this.pos] : '\0');
			}

			// Token: 0x06006B28 RID: 27432 RVA: 0x001724D8 File Offset: 0x001706D8
			internal char NextNonDigit()
			{
				for (int i = this.pos; i < this.len; i++)
				{
					char c = this.str[i];
					if (c < '0' || c > '9')
					{
						return c;
					}
				}
				return '\0';
			}

			// Token: 0x06006B29 RID: 27433 RVA: 0x00172518 File Offset: 0x00170718
			internal bool TryParse(string input, ref TimeSpanParse.TimeSpanResult result)
			{
				result.parsedTimeSpan._ticks = 0L;
				if (input == null)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "input");
					return false;
				}
				this.str = input;
				this.len = input.Length;
				this.pos = -1;
				this.NextChar();
				this.SkipBlanks();
				bool flag = false;
				if (this.ch == '-')
				{
					flag = true;
					this.NextChar();
				}
				long num;
				if (this.NextNonDigit() == ':')
				{
					if (!this.ParseTime(out num, ref result))
					{
						return false;
					}
				}
				else
				{
					int num2;
					if (!this.ParseInt(10675199, out num2, ref result))
					{
						return false;
					}
					num = (long)num2 * 864000000000L;
					if (this.ch == '.')
					{
						this.NextChar();
						long num3;
						if (!this.ParseTime(out num3, ref result))
						{
							return false;
						}
						num += num3;
					}
				}
				if (flag)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
				}
				else if (num < 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
				this.SkipBlanks();
				if (this.pos < this.len)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}

			// Token: 0x06006B2A RID: 27434 RVA: 0x00172638 File Offset: 0x00170838
			internal bool ParseInt(int max, out int i, ref TimeSpanParse.TimeSpanResult result)
			{
				i = 0;
				int num = this.pos;
				while (this.ch >= '0' && this.ch <= '9')
				{
					if (((long)i & (long)((ulong)-268435456)) != 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
					i = i * 10 + (int)this.ch - 48;
					if (i < 0)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
					this.NextChar();
				}
				if (num == this.pos)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
				if (i > max)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
				return true;
			}

			// Token: 0x06006B2B RID: 27435 RVA: 0x001726D4 File Offset: 0x001708D4
			internal bool ParseTime(out long time, ref TimeSpanParse.TimeSpanResult result)
			{
				time = 0L;
				int num;
				if (!this.ParseInt(23, out num, ref result))
				{
					return false;
				}
				time = (long)num * 36000000000L;
				if (this.ch != ':')
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
				this.NextChar();
				if (!this.ParseInt(59, out num, ref result))
				{
					return false;
				}
				time += (long)num * 600000000L;
				if (this.ch == ':')
				{
					this.NextChar();
					if (this.ch != '.')
					{
						if (!this.ParseInt(59, out num, ref result))
						{
							return false;
						}
						time += (long)num * 10000000L;
					}
					if (this.ch == '.')
					{
						this.NextChar();
						int num2 = 10000000;
						while (num2 > 1 && this.ch >= '0' && this.ch <= '9')
						{
							num2 /= 10;
							time += (long)((int)(this.ch - '0') * num2);
							this.NextChar();
						}
					}
				}
				return true;
			}

			// Token: 0x06006B2C RID: 27436 RVA: 0x001727C1 File Offset: 0x001709C1
			internal void SkipBlanks()
			{
				while (this.ch == ' ' || this.ch == '\t')
				{
					this.NextChar();
				}
			}

			// Token: 0x040033C6 RID: 13254
			private string str;

			// Token: 0x040033C7 RID: 13255
			private char ch;

			// Token: 0x040033C8 RID: 13256
			private int pos;

			// Token: 0x040033C9 RID: 13257
			private int len;
		}
	}
}
