using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000973 RID: 2419
	internal sealed class CultureProcessor
	{
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x00080B13 File Offset: 0x0007ED13
		internal CultureInfo DefaultCulture
		{
			get
			{
				return this.defaultCulture;
			}
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x00080B1B File Offset: 0x0007ED1B
		private CultureProcessor()
		{
			this.SetDefaultCulture();
			this.SetSupportedCultures();
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x00080B30 File Offset: 0x0007ED30
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

		// Token: 0x0600345B RID: 13403 RVA: 0x00080B5C File Offset: 0x0007ED5C
		private void SetDefaultCulture()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			if (ClientCultures.IsCultureSupportedForDsn(currentCulture))
			{
				this.defaultCulture = currentCulture;
				return;
			}
			this.defaultCulture = CultureInfo.CreateSpecificCulture("en-US");
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x00080B90 File Offset: 0x0007ED90
		private void SetSupportedCultures()
		{
			this.supportedCultures = new Dictionary<string, CultureInfo>(StringComparer.OrdinalIgnoreCase);
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures);
			for (int i = 0; i < cultures.Length; i++)
			{
				if (ClientCultures.IsCultureSupportedForDsn(cultures[i]))
				{
					this.supportedCultures.Add(cultures[i].Name, cultures[i]);
				}
			}
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x00080BE4 File Offset: 0x0007EDE4
		internal CultureInfo GetCultureInfo(HeaderList headers, bool returnDefaultIfMissing)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.GetHashCode(), "Getting CultureInfo");
			if (headers != null)
			{
				Header contentLanguageHeader = headers.FindFirst(HeaderId.ContentLanguage);
				Header acceptLanguageHeader = headers.FindFirst("Accept-Language");
				return CultureProcessor.Instance.GetCulture(acceptLanguageHeader, contentLanguageHeader, returnDefaultIfMissing);
			}
			if (returnDefaultIfMissing)
			{
				return this.defaultCulture;
			}
			return null;
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x00080C38 File Offset: 0x0007EE38
		private CultureInfo GetCulture(Header acceptLanguageHeader, Header contentLanguageHeader, bool returnDefaultIfMissing)
		{
			string languages;
			if (contentLanguageHeader != null && contentLanguageHeader.TryGetValue(out languages))
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Using Content-language");
				CultureInfo cultureInfo = this.FindLanguage(languages, false);
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			string languages2;
			if (acceptLanguageHeader != null && acceptLanguageHeader.TryGetValue(out languages2))
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Using Accept-language");
				CultureInfo cultureInfo = this.FindLanguage(languages2, true);
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			if (returnDefaultIfMissing)
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug<CultureInfo>(0L, "Using default: {0}", this.defaultCulture);
				return this.defaultCulture;
			}
			ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "No cultureinfo found. Return null.");
			return null;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x00080CD0 File Offset: 0x0007EED0
		private CultureInfo FindLanguage(string languages, bool useQValue)
		{
			int num = 0;
			double num2 = 0.0;
			CultureInfo result = null;
			if (string.IsNullOrEmpty(languages) || languages.Length > 16384)
			{
				return null;
			}
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Finding langage with {0}", languages);
			CultureInfo cultureInfo;
			for (;;)
			{
				int num3 = languages.IndexOf(',', num);
				double num4;
				bool flag = this.TryParseExchangeLanguage(languages, num, (num3 == -1) ? (languages.Length - 1) : (num3 - 1), out cultureInfo, out num4);
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
						ExTraceGlobals.RightsManagementTracer.TraceDebug<double, CultureInfo>(0L, "got q={0} for {1}, setting current best", num4, cultureInfo);
						num2 = num4;
						result = cultureInfo;
					}
				}
				if (num3 == -1 || num3 + 1 >= languages.Length)
				{
					goto IL_DE;
				}
				num = num3 + 1;
			}
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Got q value on {0}, but it's not expected", languages);
			return null;
			Block_7:
			ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "qvalue not found");
			return cultureInfo;
			IL_DE:
			if (num2 > 0.0)
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x00080DD4 File Offset: 0x0007EFD4
		private bool TryParseExchangeLanguage(string languageTag, int beginIndex, int endIndex, out CultureInfo cultureInfo, out double qvalue)
		{
			cultureInfo = null;
			qvalue = -1.0;
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string, int, int>(0L, "parsing {0} from index {1} to {2}", languageTag, beginIndex, endIndex);
			if (endIndex - beginIndex + 1 < 2)
			{
				return false;
			}
			int num = CultureProcessor.FindNextNonWhiteSpace(languageTag, beginIndex, endIndex);
			if (num == -1)
			{
				return false;
			}
			int num2 = languageTag.IndexOf(';', num, endIndex - num + 1);
			if (num2 != -1)
			{
				int num3 = CultureProcessor.FindNextNonWhiteSpace(languageTag, num2 + 1, endIndex);
				if (num3 == -1 || num3 + 2 > endIndex)
				{
					ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "q value too short, ignored");
					return false;
				}
				if (char.ToLowerInvariant(languageTag[num3]) != 'q' || languageTag[num3 + 1] != '=')
				{
					ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "q value syntax invalid, ignored");
					return false;
				}
				string s = languageTag.Substring(num3 + 2, endIndex - num3 - 1);
				if (!double.TryParse(s, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out qvalue) || qvalue <= 0.0 || qvalue > 1.0)
				{
					ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "0 qvalue, Invalid parse or invalid qvalue");
					return false;
				}
				string text = languageTag.Substring(num, num2 - num);
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string, double>(0L, "Got culture name: {0} with q={1}", text, qvalue);
				cultureInfo = this.GetExchangeSupportedCulture(text);
			}
			else
			{
				string text2 = languageTag.Substring(num, endIndex - num + 1);
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Got culture name: {0}", text2);
				cultureInfo = this.GetExchangeSupportedCulture(text2);
			}
			return null != cultureInfo;
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00080F40 File Offset: 0x0007F140
		private CultureInfo GetExchangeSupportedCulture(string language)
		{
			CultureInfo result;
			if (this.supportedCultures.TryGetValue(language, out result))
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Supported Culture: {0}", language);
				return result;
			}
			if (language.Length <= 2)
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Not supported: {0}", language);
				return null;
			}
			if (language.Length > 2 && language[2] != '-')
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Invalid syntax: {0}", language);
				return null;
			}
			if (!this.supportedCultures.TryGetValue(language.Substring(0, 2), out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x04002C61 RID: 11361
		private const int MaxLanguageHeaderLength = 16384;

		// Token: 0x04002C62 RID: 11362
		private const string AcceptLanguageHeaderName = "Accept-Language";

		// Token: 0x04002C63 RID: 11363
		private CultureInfo defaultCulture;

		// Token: 0x04002C64 RID: 11364
		private Dictionary<string, CultureInfo> supportedCultures;

		// Token: 0x04002C65 RID: 11365
		internal static CultureProcessor Instance = new CultureProcessor();
	}
}
