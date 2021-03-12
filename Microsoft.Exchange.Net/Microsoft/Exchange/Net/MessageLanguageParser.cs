using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DsnAndQuotaGeneration;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000073 RID: 115
	internal class MessageLanguageParser
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x00010BCF File Offset: 0x0000EDCF
		static MessageLanguageParser()
		{
			MessageLanguageParser.SetSupportedCultures();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010C04 File Offset: 0x0000EE04
		private MessageLanguageParser()
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010C0C File Offset: 0x0000EE0C
		public static string GetLanguageHeaderValue(Header languageHeader)
		{
			string result;
			if (languageHeader == null || !languageHeader.TryGetValue(out result))
			{
				ExTraceGlobals.DsnTracer.TraceDebug(0L, "Cannot get value of the header.");
				return string.Empty;
			}
			return result;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010C40 File Offset: 0x0000EE40
		public static CultureInfo FindLanguage(string languages, bool useQValue)
		{
			int num = 0;
			double num2 = 0.0;
			CultureInfo result = null;
			if (string.IsNullOrEmpty(languages) || languages.Length > 16384)
			{
				return null;
			}
			ExTraceGlobals.DsnTracer.TraceDebug<string>(0L, "Finding langage with {0}", languages);
			CultureInfo cultureInfo;
			for (;;)
			{
				int num3 = languages.IndexOf(',', num);
				double num4;
				bool flag = MessageLanguageParser.TryParseExchangeLanguage(languages, num, (num3 == -1) ? (languages.Length - 1) : (num3 - 1), out cultureInfo, out num4);
				if (flag)
				{
					if (-1.0 != num4 && !useQValue)
					{
						break;
					}
					if (!useQValue)
					{
						return cultureInfo;
					}
					if (-1.0 == num4)
					{
						goto Block_7;
					}
					if (num4 > num2)
					{
						ExTraceGlobals.DsnTracer.TraceDebug<double, CultureInfo>(0L, "got q={0} for {1}, setting current best", num4, cultureInfo);
						num2 = num4;
						result = cultureInfo;
					}
				}
				if (num3 == -1 || num3 + 1 >= languages.Length)
				{
					goto IL_DD;
				}
				num = num3 + 1;
			}
			ExTraceGlobals.DsnTracer.TraceDebug<string>(0L, "Got q value on {0}, but it's not expected", languages);
			return null;
			Block_7:
			ExTraceGlobals.DsnTracer.TraceDebug(0L, "qvalue not found");
			return cultureInfo;
			IL_DD:
			if (num2 > 0.0)
			{
				return result;
			}
			return null;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010D44 File Offset: 0x0000EF44
		public static CultureInfo GetDefaultCulture()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			if (currentCulture.IsNeutralCulture || !MessageLanguageParser.IsCultureSupported(currentCulture))
			{
				currentCulture = MessageLanguageParser.enUsCulture;
			}
			return currentCulture;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010D6E File Offset: 0x0000EF6E
		public static CultureInfo GetCulture(Header acceptLanguageHeader, Header contentLanguageHeader, bool enableLanguageDetection, CultureInfo configuredDefaultCulture)
		{
			return MessageLanguageParser.GetCultureFromLanguageHeaderValues(MessageLanguageParser.GetLanguageHeaderValue(acceptLanguageHeader), MessageLanguageParser.GetLanguageHeaderValue(contentLanguageHeader), enableLanguageDetection, configuredDefaultCulture);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010D84 File Offset: 0x0000EF84
		public static CultureInfo GetCultureFromLanguageHeaderValues(string acceptLanguageValue, string contentLanguageValue, bool enableLanguageDetection, CultureInfo fallbackCulture)
		{
			if (fallbackCulture == null || !MessageLanguageParser.IsCultureSupported(fallbackCulture))
			{
				fallbackCulture = MessageLanguageParser.GetDefaultCulture();
			}
			if (!enableLanguageDetection)
			{
				return fallbackCulture;
			}
			if (!string.IsNullOrEmpty(acceptLanguageValue))
			{
				ExTraceGlobals.DsnTracer.TraceDebug(0L, "Using Accept-language");
				CultureInfo cultureInfo = MessageLanguageParser.FindLanguage(acceptLanguageValue, true);
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			if (!string.IsNullOrEmpty(contentLanguageValue))
			{
				ExTraceGlobals.DsnTracer.TraceDebug(0L, "Using Content-language");
				CultureInfo cultureInfo = MessageLanguageParser.FindLanguage(contentLanguageValue, false);
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			ExTraceGlobals.DsnTracer.TraceDebug<CultureInfo>(0L, "Using default: {0}", fallbackCulture);
			return fallbackCulture;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010E08 File Offset: 0x0000F008
		private static void SetAliasCulture(string cultureName, string cultureAlias)
		{
			if (MessageLanguageParser.supportedCultures.ContainsKey(cultureName) && !MessageLanguageParser.supportedCultures.ContainsKey(cultureAlias))
			{
				try
				{
					CultureInfo value = new CultureInfo(cultureAlias);
					MessageLanguageParser.supportedCultures.Add(cultureAlias, value);
				}
				catch (ArgumentException)
				{
					CultureInfo value2 = MessageLanguageParser.supportedCultures[cultureName];
					MessageLanguageParser.supportedCultures.Add(cultureAlias, value2);
				}
			}
			if (MessageLanguageParser.supportedCultures.ContainsKey(cultureAlias) && !MessageLanguageParser.supportedCultures.ContainsKey(cultureName))
			{
				try
				{
					CultureInfo value3 = new CultureInfo(cultureName);
					MessageLanguageParser.supportedCultures.Add(cultureName, value3);
				}
				catch (ArgumentException)
				{
					CultureInfo value4 = MessageLanguageParser.supportedCultures[cultureAlias];
					MessageLanguageParser.supportedCultures.Add(cultureName, value4);
				}
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010EC8 File Offset: 0x0000F0C8
		private static bool TryParseExchangeLanguage(string languageTag, int beginIndex, int endIndex, out CultureInfo cultureInfo, out double qvalue)
		{
			cultureInfo = null;
			qvalue = -1.0;
			ExTraceGlobals.DsnTracer.TraceDebug<string, int, int>(0L, "parsing {0} from index {1} to {2}", languageTag, beginIndex, endIndex);
			if (endIndex - beginIndex + 1 < 2)
			{
				return false;
			}
			int num = MessageLanguageParser.FindNextNonWhiteSpace(languageTag, beginIndex, endIndex);
			if (num == -1)
			{
				return false;
			}
			int num2 = languageTag.IndexOf(';', num, endIndex - num + 1);
			if (num2 != -1)
			{
				int num3 = MessageLanguageParser.FindNextNonWhiteSpace(languageTag, num2 + 1, endIndex);
				if (num3 == -1 || num3 + 2 > endIndex)
				{
					ExTraceGlobals.DsnTracer.TraceDebug(0L, "q value too short, ignored");
					return false;
				}
				if (char.ToLowerInvariant(languageTag[num3]) != 'q' || languageTag[num3 + 1] != '=')
				{
					ExTraceGlobals.DsnTracer.TraceDebug(0L, "q value syntax invalid, ignored");
					return false;
				}
				string s = languageTag.Substring(num3 + 2, endIndex - num3 - 1);
				if (!double.TryParse(s, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out qvalue) || qvalue <= 0.0 || qvalue > 1.0)
				{
					ExTraceGlobals.DsnTracer.TraceDebug(0L, "0 qvalue, Invalid parse or invalid qvalue");
					return false;
				}
				string text = languageTag.Substring(num, num2 - num);
				ExTraceGlobals.DsnTracer.TraceDebug<string, double>(0L, "Got culture name: {0} with q={1}", text, qvalue);
				cultureInfo = MessageLanguageParser.GetExchangeSupportedCulutre(text);
			}
			else
			{
				string text2 = languageTag.Substring(num, endIndex - num + 1);
				ExTraceGlobals.DsnTracer.TraceDebug<string>(0L, "Got culture name: {0}", text2);
				cultureInfo = MessageLanguageParser.GetExchangeSupportedCulutre(text2);
			}
			return null != cultureInfo;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00011030 File Offset: 0x0000F230
		private static int FindNextNonWhiteSpace(string language, int beginIndex, int endIndex)
		{
			for (int i = beginIndex; i <= endIndex; i++)
			{
				if (!char.IsWhiteSpace(language[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001105C File Offset: 0x0000F25C
		private static CultureInfo GetExchangeSupportedCulutre(string language)
		{
			CultureInfo result;
			if (MessageLanguageParser.supportedCultures.TryGetValue(language, out result))
			{
				ExTraceGlobals.DsnTracer.TraceDebug<string>(0L, "Supported Culture: {0}", language);
				return result;
			}
			if (language.Length <= 2)
			{
				ExTraceGlobals.DsnTracer.TraceDebug<string>(0L, "Not supported: {0}", language);
				return null;
			}
			if (language.Length > 2 && language[2] != '-')
			{
				ExTraceGlobals.DsnTracer.TraceDebug<string>(0L, "Invalid syntax: {0}", language);
				return null;
			}
			if (!MessageLanguageParser.supportedCultures.TryGetValue(language.Substring(0, 2), out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000110EA File Offset: 0x0000F2EA
		public static bool IsCultureSupported(CultureInfo culture)
		{
			return !StandaloneFuzzing.IsEnabled && ClientCultures.IsCultureSupportedForDsn(culture);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000110FB File Offset: 0x0000F2FB
		public static bool IsCultureSupportedForCustomization(CultureInfo culture)
		{
			return !StandaloneFuzzing.IsEnabled && ClientCultures.IsCultureSupportedForDsnCustomization(culture);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001110C File Offset: 0x0000F30C
		public static CultureInfo GetCultureToUse(CultureInfo cultureInfo)
		{
			CultureInfo result;
			if (!cultureInfo.IsNeutralCulture)
			{
				result = cultureInfo;
			}
			else
			{
				if (cultureInfo.LCID != MessageLanguageParser.CultureChsLcid && cultureInfo.LCID != MessageLanguageParser.CultureChtLcid)
				{
					try
					{
						return CultureInfo.CreateSpecificCulture(cultureInfo.Name);
					}
					catch (ArgumentException)
					{
						return MessageLanguageParser.GetDefaultCulture();
					}
				}
				result = MessageLanguageParser.GetDefaultCulture();
			}
			return result;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00011170 File Offset: 0x0000F370
		private static void SetSupportedCultures()
		{
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures);
			for (int i = 0; i < cultures.Length; i++)
			{
				if (MessageLanguageParser.IsCultureSupported(cultures[i]))
				{
					MessageLanguageParser.supportedCultures.Add(cultures[i].Name, cultures[i]);
				}
			}
			if (MessageLanguageParser.supportedCultures.Count == 0)
			{
				CultureInfo cultureInfo = new CultureInfo("en-US");
				MessageLanguageParser.supportedCultures.Add(cultureInfo.Name, cultureInfo);
				return;
			}
			MessageLanguageParser.SetAliasCulture("zh-chs", "zh-hans");
			MessageLanguageParser.SetAliasCulture("zh-cht", "zh-hant");
		}

		// Token: 0x040001F2 RID: 498
		private const int MaxLanguageHeaderLength = 16384;

		// Token: 0x040001F3 RID: 499
		private static CultureInfo enUsCulture = new CultureInfo("en-US");

		// Token: 0x040001F4 RID: 500
		private static Dictionary<string, CultureInfo> supportedCultures = new Dictionary<string, CultureInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001F5 RID: 501
		private static readonly int CultureChtLcid = 31748;

		// Token: 0x040001F6 RID: 502
		private static readonly int CultureChsLcid = 4;
	}
}
