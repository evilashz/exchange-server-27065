using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001E2 RID: 482
	internal class SpokenDateTimeFormatter
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x0003FEF1 File Offset: 0x0003E0F1
		internal static string GetSpokenTimeFormat(CultureInfo c)
		{
			return PromptConfigBase.PromptResourceManager.GetString(SpokenDateTimeFormatter.SpokenTimeFormatId, c);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003FF03 File Offset: 0x0003E103
		internal static string GetSpokenShortTimeFormat(CultureInfo c)
		{
			return PromptConfigBase.PromptResourceManager.GetString(SpokenDateTimeFormatter.SpokenShortTimeFormatId, c);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003FF15 File Offset: 0x0003E115
		internal static string GetSpokenDateFormat(CultureInfo c)
		{
			return PromptConfigBase.PromptResourceManager.GetString(SpokenDateTimeFormatter.SpokenDateFormatId, c);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003FF27 File Offset: 0x0003E127
		internal static string GetSpokenDateTimeFormat(CultureInfo c)
		{
			return PromptConfigBase.PromptResourceManager.GetString(SpokenDateTimeFormatter.SpokenDateTimeFormatId, c);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003FF39 File Offset: 0x0003E139
		internal static string GetSpokenDayTimeFormat(CultureInfo c)
		{
			return PromptConfigBase.PromptResourceManager.GetString(SpokenDateTimeFormatter.SpokenDayTimeFormatId, c);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003FF4B File Offset: 0x0003E14B
		internal static string GetSpokenTimeFormatBusinessHours(CultureInfo c)
		{
			return PromptConfigBase.PromptResourceManager.GetString(SpokenDateTimeFormatter.SpokenTimeFormatBusinessHoursId, c);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003FF60 File Offset: 0x0003E160
		internal static string ToSsml(CultureInfo c, ExDateTime dt, string spokenFormat, string writtenForm)
		{
			bool flag = false;
			bool flag2 = true;
			if (CommonUtil.Is24HourTimeFormat(c.DateTimeFormat.ShortTimePattern))
			{
				flag = true;
				flag2 = false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < spokenFormat.Length)
			{
				char c2 = spokenFormat[i];
				string text = null;
				char c3 = c2;
				if (c3 <= 'T')
				{
					if (c3 == 'D')
					{
						text = string.Format(CultureInfo.InvariantCulture, "Day-{0}.wav", new object[]
						{
							dt.Day
						});
						goto IL_2AA;
					}
					if (c3 == 'M')
					{
						text = string.Format(CultureInfo.InvariantCulture, "Month-{0}.wav", new object[]
						{
							dt.Month
						});
						goto IL_2AA;
					}
					if (c3 != 'T')
					{
						goto IL_286;
					}
					if (flag2)
					{
						string text2 = SpokenDateTimeFormatter.NormalizeMeridian(dt);
						if (!flag && dt.Hour % 12 == 0 && dt.Minute == 0)
						{
							if (dt.Hour == 12)
							{
								text2 = "Noon";
							}
							else if (dt.Hour == 0)
							{
								text2 = "Midnight";
							}
						}
						text = string.Format(CultureInfo.InvariantCulture, "Meridian-{0}.wav", new object[]
						{
							text2
						});
						goto IL_2AA;
					}
					goto IL_2AA;
				}
				else if (c3 <= 'h')
				{
					if (c3 == 'd')
					{
						string text3 = SpokenDateTimeFormatter.NormalizeDayOfWeek(dt);
						text = string.Format(CultureInfo.InvariantCulture, "DayOfWeek-{0}.wav", new object[]
						{
							text3
						});
						goto IL_2AA;
					}
					if (c3 != 'h')
					{
						goto IL_286;
					}
					string text4 = SpokenDateTimeFormatter.NormalizeHour(c, dt);
					text = (flag ? string.Format(CultureInfo.InvariantCulture, "24-Hours-{0}.wav", new object[]
					{
						text4
					}) : string.Format(CultureInfo.InvariantCulture, "Hours-{0}.wav", new object[]
					{
						text4
					}));
					goto IL_2AA;
				}
				else
				{
					switch (c3)
					{
					case 'm':
					{
						string text5 = SpokenDateTimeFormatter.NormalizeMinutes(dt);
						text = string.Format(CultureInfo.InvariantCulture, "Minutes-{0}.wav", new object[]
						{
							text5
						});
						goto IL_2AA;
					}
					case 'n':
						goto IL_286;
					case 'o':
						if (flag || dt.Minute != 0)
						{
							string text6 = SpokenDateTimeFormatter.NormalizeMinutes(dt);
							text = string.Format(CultureInfo.InvariantCulture, "Minutes-{0}.wav", new object[]
							{
								text6
							});
							goto IL_2AA;
						}
						break;
					default:
						if (c3 != 't')
						{
							goto IL_286;
						}
						if (flag2)
						{
							string text7 = SpokenDateTimeFormatter.NormalizeMeridian(dt);
							text = string.Format(CultureInfo.InvariantCulture, "Meridian-{0}.wav", new object[]
							{
								text7
							});
							goto IL_2AA;
						}
						goto IL_2AA;
					}
				}
				IL_304:
				i++;
				continue;
				IL_2AA:
				if (string.IsNullOrEmpty(text))
				{
					goto IL_304;
				}
				text = Path.Combine(Util.WavPathFromCulture(c), text);
				if (!File.Exists(text))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, null, "SpokenDateFormatter did not find the file '{0}'", new object[]
					{
						text
					});
					return Util.AddProsodyWithVolume(c, writtenForm);
				}
				stringBuilder.AppendFormat("<audio src=\"{0}\" />", text);
				goto IL_304;
				IL_286:
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, null, "SpokenDateFormatter ignoring invalided specifier '{0}'", new object[]
				{
					c2
				});
				goto IL_2AA;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, null, "SpokenDateFormatter returning the following ssml blob '{0}'", new object[]
			{
				stringBuilder
			});
			return stringBuilder.ToString();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000402B0 File Offset: 0x0003E4B0
		internal static string NormalizeHour(CultureInfo cultureInfo, ExDateTime time)
		{
			int num = time.Hour;
			if (!CommonUtil.Is24HourTimeFormat(cultureInfo.DateTimeFormat.ShortTimePattern))
			{
				num = ((num > 12) ? (num - 12) : num);
				num = ((num == 0) ? 12 : num);
			}
			return num.ToString(cultureInfo);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000402F8 File Offset: 0x0003E4F8
		internal static string NormalizeMinutes(ExDateTime time)
		{
			if (time.Minute < 10)
			{
				return "0" + time.Minute.ToString(CultureInfo.InvariantCulture);
			}
			return time.Minute.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00040343 File Offset: 0x0003E543
		internal static string NormalizeMeridian(ExDateTime time)
		{
			if (time.Hour < 12)
			{
				return "AM";
			}
			return "PM";
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0004035C File Offset: 0x0003E55C
		internal static string NormalizeDayOfWeek(ExDateTime time)
		{
			return ((int)time.DayOfWeek).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x04000AD2 RID: 2770
		private static readonly string SpokenTimeFormatId = "SpokenTimeFormat";

		// Token: 0x04000AD3 RID: 2771
		private static readonly string SpokenShortTimeFormatId = "SpokenShortTimeFormat";

		// Token: 0x04000AD4 RID: 2772
		private static readonly string SpokenDateFormatId = "SpokenDateFormat";

		// Token: 0x04000AD5 RID: 2773
		private static readonly string SpokenDateTimeFormatId = "SpokenDateTimeFormat";

		// Token: 0x04000AD6 RID: 2774
		private static readonly string SpokenDayTimeFormatId = "SpokenDayTimeFormat";

		// Token: 0x04000AD7 RID: 2775
		private static readonly string SpokenTimeFormatBusinessHoursId = "SpokenTimeFormatBusinessHours";
	}
}
