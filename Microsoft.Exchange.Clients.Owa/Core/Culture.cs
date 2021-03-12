using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000FC RID: 252
	public static class Culture
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x0003D15C File Offset: 0x0003B35C
		private static Dictionary<int, string> LoadFontFileNameDictionary()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			dictionary[31748] = (dictionary[3076] = (dictionary[5124] = (dictionary[1028] = "owafont_zh_cht.css")));
			dictionary[4] = (dictionary[4100] = (dictionary[2052] = "owafont_zh_chs.css"));
			dictionary[17] = (dictionary[1041] = "owafont_ja.css");
			dictionary[18] = (dictionary[1042] = "owafont_ko.css");
			dictionary[1066] = "owafont_vi.css";
			return dictionary;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0003D220 File Offset: 0x0003B420
		public static string GetCssFontFileNameFromCulture()
		{
			return Culture.GetCssFontFileNameFromCulture(false);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0003D228 File Offset: 0x0003B428
		public static string GetCssFontFileNameFromCulture(bool isBasicExperience)
		{
			CultureInfo userCulture = Culture.GetUserCulture();
			string text = Culture.GetCssFontFileNameFromCulture(userCulture);
			if (isBasicExperience)
			{
				text = "basic_" + text;
			}
			return text;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0003D254 File Offset: 0x0003B454
		public static string GetDefaultCultureCssFontFileName(OwaContext owaContext)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			CultureInfo defaultCulture = Culture.GetDefaultCulture(owaContext);
			return Culture.GetCssFontFileNameFromCulture(defaultCulture);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0003D27C File Offset: 0x0003B47C
		private static string GetCssFontFileNameFromCulture(CultureInfo culture)
		{
			string text = null;
			Culture.fontFileNameTable.TryGetValue(culture.LCID, out text);
			if (string.IsNullOrEmpty(text))
			{
				return "owafont.css";
			}
			return text;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
		private static Dictionary<string, int> LoadLanguageMap()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			dictionary["ar"] = 1025;
			dictionary["bg"] = 1026;
			dictionary["ca"] = 1027;
			dictionary["cs"] = 1029;
			dictionary["da"] = 1030;
			dictionary["de"] = 1031;
			dictionary["el"] = 1032;
			dictionary["en"] = 1033;
			dictionary["fi"] = 1035;
			dictionary["fr"] = 1036;
			dictionary["he"] = 1037;
			dictionary["hu"] = 1038;
			dictionary["is"] = 1039;
			dictionary["it"] = 1040;
			dictionary["ja"] = 1041;
			dictionary["ko"] = 1042;
			dictionary["ko-kp"] = 1042;
			dictionary["nl"] = 1043;
			dictionary["no"] = 1044;
			dictionary["nb"] = 1044;
			dictionary["nn"] = 1044;
			dictionary["nn-no"] = 1044;
			dictionary["pl"] = 1045;
			dictionary["ro"] = 1048;
			dictionary["ro-md"] = 1048;
			dictionary["ru"] = 1049;
			dictionary["ru-mo"] = 1049;
			dictionary["hr"] = 1050;
			dictionary["sk"] = 1051;
			dictionary["sv"] = 1053;
			dictionary["th"] = 1054;
			dictionary["tr"] = 1055;
			dictionary["ur"] = 1056;
			dictionary["id"] = 1057;
			dictionary["uk"] = 1058;
			dictionary["sl"] = 1060;
			dictionary["et"] = 1061;
			dictionary["lv"] = 1062;
			dictionary["lt"] = 1063;
			dictionary["fa"] = 1065;
			dictionary["vi"] = 1066;
			dictionary["eu"] = 1069;
			dictionary["hi"] = 1081;
			dictionary["ms"] = 1086;
			dictionary["kk"] = 1087;
			dictionary["sw"] = 1089;
			dictionary["bn"] = 1093;
			dictionary["gu"] = 1095;
			dictionary["or"] = 1096;
			dictionary["ta"] = 1097;
			dictionary["te"] = 1098;
			dictionary["kn"] = 1099;
			dictionary["ml"] = 1100;
			dictionary["mr"] = 1102;
			dictionary["cy"] = 1106;
			dictionary["gl"] = 1110;
			dictionary["am"] = 1118;
			dictionary["fil-Latn"] = 1124;
			dictionary["fil"] = 1124;
			dictionary["zh"] = 2052;
			dictionary["zh-Hans-cn"] = 2052;
			dictionary["zh-Hans"] = 2052;
			dictionary["pt"] = 2070;
			dictionary["sr"] = 2074;
			dictionary["zh-Hant-tw"] = 3076;
			dictionary["zh-Hant"] = 3076;
			dictionary["es"] = 3082;
			dictionary["es-us"] = 3082;
			return dictionary;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0003D70C File Offset: 0x0003B90C
		private static Dictionary<int, Culture.SingularPluralRegularExpression> LoadRemindersDueInRegularExpressionsMap()
		{
			Dictionary<int, Culture.SingularPluralRegularExpression> dictionary = new Dictionary<int, Culture.SingularPluralRegularExpression>();
			Culture.SingularPluralRegularExpression value = new Culture.SingularPluralRegularExpression("^1$|[^1]1$", "^[234]$|[^1][234]$");
			Culture.SingularPluralRegularExpression value2 = new Culture.SingularPluralRegularExpression(".", "^[234]$");
			dictionary[1029] = value2;
			dictionary[1051] = value2;
			dictionary[1060] = value2;
			dictionary[1058] = value2;
			dictionary[1045] = new Culture.SingularPluralRegularExpression(".", "^[234]$|[^1][234]$");
			dictionary[1049] = value;
			dictionary[2074] = value;
			dictionary[3098] = value;
			dictionary[1063] = value;
			dictionary[1062] = value;
			return dictionary;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0003D7EC File Offset: 0x0003B9EC
		private static Dictionary<string, string[]> LoadOneLetterDayNamesMap()
		{
			Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			string[] array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					229,
					101
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					0,
					78
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					140,
					78
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					9,
					78
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					219,
					86
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					148,
					78
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					109,
					81
				})
			};
			dictionary["zh-MO"] = array;
			dictionary["zh-TW"] = array;
			dictionary["zh-CN"] = array;
			dictionary["zh-SG"] = array;
			array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					45,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					70,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					43,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					49,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					46,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					51,
					6
				})
			};
			dictionary["ar"] = array;
			dictionary["ar-SA"] = array;
			dictionary["ar-IQ"] = array;
			dictionary["ar-EG"] = array;
			dictionary["ar-LY"] = array;
			dictionary["ar-DZ"] = array;
			dictionary["ar-MA"] = array;
			dictionary["ar-TN"] = array;
			dictionary["ar-OM"] = array;
			dictionary["ar-YE"] = array;
			dictionary["ar-SY"] = array;
			dictionary["ar-JO"] = array;
			dictionary["ar-LB"] = array;
			dictionary["ar-KW"] = array;
			dictionary["ar-AE"] = array;
			dictionary["ar-BH"] = array;
			dictionary["ar-QA"] = array;
			array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					204,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					47,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					51,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					134,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					126,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					52,
					6
				})
			};
			dictionary["fa"] = array;
			dictionary["fa-IR"] = array;
			array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					208,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					209,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					210,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					211,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					212,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					213,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					233,
					5
				})
			};
			dictionary["he"] = array;
			dictionary["he-IL"] = array;
			dictionary["hi"] = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					48,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					56,
					9,
					75,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					46,
					9,
					2,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					9,
					65,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					23,
					9,
					65,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					54,
					9,
					65,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					54,
					9
				})
			};
			dictionary["th"] = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					45,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					8,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					45,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					30,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					30,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					40,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					42,
					14
				})
			};
			array = new string[]
			{
				"I",
				"A",
				"R",
				"Z",
				"G",
				"O",
				"L"
			};
			dictionary["eu"] = array;
			dictionary["eu-ES"] = array;
			array = new string[]
			{
				"D",
				"L",
				"M",
				"X",
				"J",
				"V",
				"S"
			};
			dictionary["ca"] = array;
			dictionary["ca-ES"] = array;
			array = new string[]
			{
				"s",
				"m",
				"t",
				"w",
				"t",
				"f",
				"s"
			};
			dictionary["vi"] = array;
			dictionary["vi-VN"] = array;
			array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					39,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					126,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					69,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					40,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					71,
					6
				})
			};
			dictionary["ur"] = array;
			dictionary["ur-PK"] = array;
			CultureInfo[] supportedCultures = Culture.GetSupportedCultures();
			for (int i = 0; i < supportedCultures.Length; i++)
			{
				if (!dictionary.ContainsKey(supportedCultures[i].Name))
				{
					string[] abbreviatedDayNames = supportedCultures[i].DateTimeFormat.AbbreviatedDayNames;
					array = new string[7];
					for (int j = 0; j < abbreviatedDayNames.Length; j++)
					{
						array[j] = abbreviatedDayNames[j][0].ToString();
					}
					dictionary[supportedCultures[i].Name] = array;
				}
			}
			return dictionary;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0003E1CC File Offset: 0x0003C3CC
		public static Culture.SingularPluralRegularExpression GetSingularPluralRegularExpressions(int lcid)
		{
			if (Culture.regularExpressionMap.ContainsKey(lcid))
			{
				return Culture.regularExpressionMap[lcid];
			}
			return Culture.defaultRegularExpression;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0003E1EC File Offset: 0x0003C3EC
		public static CultureInfo[] GetSupportedCultures()
		{
			return Culture.GetSupportedCultures(false);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0003E1F4 File Offset: 0x0003C3F4
		public static CultureInfo[] GetSupportedCultures(bool sortByName)
		{
			if (!sortByName)
			{
				return Culture.supportedCultureInfosSortedByLcid;
			}
			return Culture.CreateSortedSupportedCultures(sortByName);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0003E205 File Offset: 0x0003C405
		private static int CompareCultureNames(CultureInfo x, CultureInfo y)
		{
			return string.Compare(x.NativeName, y.NativeName, StringComparison.CurrentCulture);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0003E21C File Offset: 0x0003C41C
		private static int CompareCultureLCIDs(CultureInfo x, CultureInfo y)
		{
			return x.LCID.CompareTo(y.LCID);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0003E240 File Offset: 0x0003C440
		internal static string[] GetSupportedBrowserLanguageArray()
		{
			string[] array = new string[Culture.supportedCultureInfos.Count];
			int num = 0;
			foreach (CultureInfo cultureInfo in Culture.supportedCultureInfos)
			{
				array[num++] = cultureInfo.Name;
			}
			Array.Sort<string>(array, StringComparer.OrdinalIgnoreCase);
			return array;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0003E2B8 File Offset: 0x0003C4B8
		public static bool IsSupportedCulture(CultureInfo culture)
		{
			return Culture.supportedCultureInfos.Contains(culture);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0003E2C5 File Offset: 0x0003C4C5
		public static bool IsSupportedCulture(int lcid)
		{
			return lcid > 0 && Culture.IsSupportedCulture(CultureInfo.GetCultureInfo(lcid));
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0003E2D8 File Offset: 0x0003C4D8
		public static CultureInfo GetSupportedBrowserLanguage(string language)
		{
			if (Culture.languageMap.ContainsKey(language) && Array.BinarySearch<string>(Culture.supportedBrowserLanguages, CultureInfo.GetCultureInfo(Culture.languageMap[language]).Name, StringComparer.OrdinalIgnoreCase) >= 0)
			{
				return Culture.GetCultureInfoInstance(Culture.languageMap[language]);
			}
			if (Array.BinarySearch<string>(Culture.supportedBrowserLanguages, language, StringComparer.OrdinalIgnoreCase) >= 0)
			{
				return Culture.GetCultureInfoInstance(CultureInfo.GetCultureInfo(language).LCID);
			}
			return null;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0003E34F File Offset: 0x0003C54F
		public static bool IsRtl
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0003E365 File Offset: 0x0003C565
		public static void UpdateUserCulture(UserContext userContext, CultureInfo culture)
		{
			Culture.UpdateUserCulture(userContext, culture, true);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0003E370 File Offset: 0x0003C570
		public static void UpdateUserCulture(UserContext userContext, CultureInfo culture, bool updateAD)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (userContext == null)
			{
				throw new OwaInvalidOperationException("Shouldn't call UpdateUserCulture without a session");
			}
			if (updateAD)
			{
				PreferredCultures preferredCultures = new PreferredCultures(userContext.ExchangePrincipal.PreferredCultures);
				preferredCultures.AddSupportedCulture(culture, new Predicate<CultureInfo>(Culture.IsSupportedCulture));
				Culture.SetPreferredCulture(userContext.ExchangePrincipal, preferredCultures, userContext.MailboxSession.GetADRecipientSession(false, ConsistencyMode.FullyConsistent));
				userContext.ExchangePrincipal = userContext.ExchangePrincipal.WithPreferredCultures(preferredCultures);
			}
			userContext.UserCulture = culture;
			Culture.InternalSetThreadCulture(culture);
			userContext.RecreateMailboxSession(OwaContext.Get(HttpContext.Current));
			userContext.RecreatePublicFolderSessions();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0003E410 File Offset: 0x0003C610
		internal static string LookUpHelpDirectoryForCulture(CultureInfo culture)
		{
			string text = null;
			string str = HttpRuntime.AppDomainAppPath + "help\\";
			while (!culture.Equals(CultureInfo.InvariantCulture))
			{
				text = culture.Name;
				if (Directory.Exists(str + text))
				{
					break;
				}
				culture = culture.Parent;
			}
			return text;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0003E45C File Offset: 0x0003C65C
		public static string GetUserHelpLanguage()
		{
			CultureInfo userCulture = Culture.GetUserCulture();
			string text = Culture.LookUpHelpDirectoryForCulture(userCulture);
			if (text == null)
			{
				text = Culture.LookUpHelpDirectoryForCulture(Culture.GetDefaultCulture(OwaContext.Current));
			}
			if (text == null)
			{
				text = "en";
			}
			ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Help subdirectory: ", text);
			return text;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0003E4A7 File Offset: 0x0003C6A7
		internal static CultureInfo GetPreferredCulture(ADUser adUser, UserContext userContext)
		{
			return Culture.GetPreferredCulture(adUser.Languages, userContext);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0003E4B5 File Offset: 0x0003C6B5
		internal static CultureInfo GetPreferredCulture(ExchangePrincipal exchangePrincipal, UserContext userContext)
		{
			return Culture.GetPreferredCulture(exchangePrincipal.PreferredCultures, userContext);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0003E4C4 File Offset: 0x0003C6C4
		private static CultureInfo GetPreferredCulture(IEnumerable<CultureInfo> cultures, UserContext userContext)
		{
			IEnumerator<CultureInfo> enumerator = cultures.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (Culture.IsSupportedCulture(enumerator.Current))
				{
					return Culture.GetCultureInfoInstance(enumerator.Current.LCID);
				}
			}
			int defaultClientLanguage;
			if (userContext != null)
			{
				defaultClientLanguage = userContext.DefaultClientLanguage;
			}
			else
			{
				defaultClientLanguage = OwaConfigurationManager.Configuration.DefaultClientLanguage;
			}
			if (defaultClientLanguage > 0)
			{
				if (Culture.IsSupportedCulture(defaultClientLanguage))
				{
					return Culture.GetCultureInfoInstance(defaultClientLanguage);
				}
				ExTraceGlobals.CoreTracer.TraceDebug<int>(0L, "DefaultClientLanguage is unsupported culture (LCID: {0})", defaultClientLanguage);
			}
			return null;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0003E540 File Offset: 0x0003C740
		internal static void SetPreferredCulture(ExchangePrincipal exchangePrincipal, IEnumerable<CultureInfo> preferredCultures, IRecipientSession recipientSession)
		{
			ADUser aduser = recipientSession.Read(exchangePrincipal.ObjectId) as ADUser;
			if (aduser != null)
			{
				aduser.Languages.Clear();
				foreach (CultureInfo item in preferredCultures)
				{
					aduser.Languages.Add(item);
				}
				recipientSession.Save(aduser);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0003E5B4 File Offset: 0x0003C7B4
		internal static CultureInfo GetCultureInfoInstance(int lcid)
		{
			CultureInfo cultureInfo = new CultureInfo(lcid);
			Calendar calendar = new GregorianCalendar();
			cultureInfo.DateTimeFormat.Calendar = calendar;
			return cultureInfo;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0003E5DD File Offset: 0x0003C7DD
		internal static void InternalSetThreadCulture(CultureInfo culture)
		{
			Culture.InternalSetThreadCulture(culture, OwaContext.Current);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0003E5EA File Offset: 0x0003C7EA
		internal static void InternalSetAsyncThreadCulture(CultureInfo culture)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug<int>(0L, "Culture.InternalSetAsyncThreadCulture, LCID={0}", culture.LCID);
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0003E61C File Offset: 0x0003C81C
		internal static void InternalSetAsyncThreadCulture(CultureInfo culture, UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<int>(0L, "Culture.InternalSetAsyncThreadCulture, LCID={0}", culture.LCID);
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
			if (!userContext.IsProxy && userContext.UserOptions != null)
			{
				culture.DateTimeFormat.ShortTimePattern = userContext.UserOptions.TimeFormat;
				culture.DateTimeFormat.ShortDatePattern = userContext.UserOptions.DateFormat;
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0003E6A0 File Offset: 0x0003C8A0
		internal static void InternalSetThreadCulture(CultureInfo culture, OwaContext owaContext)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug<int>(0L, "Culture.InternalSetThreadCulture, LCID={0}", culture.LCID);
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
			owaContext.Culture = culture;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0003E6D6 File Offset: 0x0003C8D6
		internal static bool IsThreadCultureSet(OwaContext owaContext)
		{
			return owaContext.Culture != null && Thread.CurrentThread.CurrentCulture == owaContext.Culture && Thread.CurrentThread.CurrentUICulture == owaContext.Culture;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0003E707 File Offset: 0x0003C907
		public static CultureInfo GetUserCulture()
		{
			return Culture.GetUserCulture(OwaContext.Current);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0003E713 File Offset: 0x0003C913
		public static CultureInfo GetUserCulture(OwaContext owaContext)
		{
			return Culture.InternalGetThreadCulture(owaContext);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0003E71C File Offset: 0x0003C91C
		internal static void SetThreadCulture(OwaContext owaContext)
		{
			if (!Culture.IsThreadCultureSet(owaContext))
			{
				if (owaContext.Culture != null)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "OwaContext.Culture was already set for this request but not for this thread (proxy scenario). Setting the Culture request \"{0}\" in the current thread.", owaContext.Culture.ToString());
					Culture.InternalSetThreadCulture(owaContext.Culture, owaContext);
					return;
				}
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "OwaContext.Culture was never set for this request. Setting the Culture request and culture thread to default culture.");
				Culture.InternalSetThreadCulture(Culture.GetDefaultCulture(owaContext), owaContext);
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0003E77F File Offset: 0x0003C97F
		internal static CultureInfo InternalGetThreadCulture(OwaContext owaContext)
		{
			Culture.SetThreadCulture(owaContext);
			return Thread.CurrentThread.CurrentCulture;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0003E794 File Offset: 0x0003C994
		public static CultureInfo GetDefaultCulture(OwaContext owaContext)
		{
			int num = 0;
			if (owaContext.SessionContext != null)
			{
				num = owaContext.SessionContext.LogonAndErrorLanguage;
			}
			else if (OwaConfigurationManager.Configuration != null)
			{
				num = OwaConfigurationManager.Configuration.LogonAndErrorLanguage;
			}
			if (num > 0)
			{
				if (Culture.IsSupportedCulture(num))
				{
					return Culture.GetCultureInfoInstance(num);
				}
				ExTraceGlobals.CoreTracer.TraceDebug<int>(0L, "LogonAndErrorLanguage is unsupported culture (LCID: {0})", num);
			}
			CultureInfo browserDefaultCulture = Culture.GetBrowserDefaultCulture(owaContext);
			if (browserDefaultCulture != null)
			{
				return browserDefaultCulture;
			}
			return Globals.ServerCulture;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0003E804 File Offset: 0x0003CA04
		public static CultureInfo GetBrowserDefaultCulture(OwaContext owaContext)
		{
			string[] userLanguages = owaContext.HttpContext.Request.UserLanguages;
			if (userLanguages != null)
			{
				int num = Math.Min(5, userLanguages.Length);
				for (int i = 0; i < num; i++)
				{
					string text = Culture.ValidateLanguageTag(userLanguages[i]);
					if (text != null)
					{
						CultureInfo supportedBrowserLanguage = Culture.GetSupportedBrowserLanguage(text);
						if (supportedBrowserLanguage != null)
						{
							return supportedBrowserLanguage;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0003E858 File Offset: 0x0003CA58
		private static string ValidateLanguageTag(string tag)
		{
			if (tag.Length < 1 || tag.Length > 44)
			{
				return null;
			}
			int num = 0;
			while (num < tag.Length && char.IsWhiteSpace(tag[num]))
			{
				num++;
			}
			if (num == tag.Length)
			{
				return null;
			}
			int num2 = num;
			for (int i = 0; i < 3; i++)
			{
				int num3 = 0;
				while (num3 < 8 && num2 < tag.Length && ((tag[num2] >= 'a' && tag[num2] <= 'z') || (tag[num2] >= 'A' && tag[num2] <= 'Z')))
				{
					num3++;
					num2++;
				}
				if (num2 == tag.Length || tag[num2] != '-')
				{
					break;
				}
				num2++;
			}
			if (num2 != tag.Length && tag[num2] != ';' && !char.IsWhiteSpace(tag[num2]))
			{
				return null;
			}
			return tag.Substring(num, num2 - num);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003E93F File Offset: 0x0003CB3F
		public static string[] GetOneLetterDayNames()
		{
			return Culture.GetOneLetterDayNames(Culture.GetUserCulture());
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0003E94B File Offset: 0x0003CB4B
		public static string[] GetOneLetterDayNames(CultureInfo culture)
		{
			return Culture.oneLetterDayNamesMap[culture.Name];
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0003E95D File Offset: 0x0003CB5D
		public static string AMDesignator
		{
			get
			{
				return Culture.GetUserCulture().DateTimeFormat.AMDesignator;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0003E96E File Offset: 0x0003CB6E
		public static string PMDesignator
		{
			get
			{
				return Culture.GetUserCulture().DateTimeFormat.PMDesignator;
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0003E980 File Offset: 0x0003CB80
		private static List<CultureInfo> CreateCultureInfosFromNames(string[] cultureNames)
		{
			List<CultureInfo> list = new List<CultureInfo>(cultureNames.Length);
			foreach (string name in cultureNames)
			{
				list.Add(CultureInfo.GetCultureInfo(name));
			}
			return list;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0003E9B8 File Offset: 0x0003CBB8
		private static CultureInfo[] CreateSortedSupportedCultures(bool sortByName)
		{
			CultureInfo[] array = new CultureInfo[Culture.supportedCultureInfos.Count];
			int num = 0;
			foreach (CultureInfo cultureInfo in Culture.supportedCultureInfos)
			{
				array[num++] = CultureInfo.GetCultureInfo(cultureInfo.LCID);
			}
			if (sortByName)
			{
				Array.Sort<CultureInfo>(array, new Comparison<CultureInfo>(Culture.CompareCultureNames));
			}
			else
			{
				Array.Sort<CultureInfo>(array, new Comparison<CultureInfo>(Culture.CompareCultureLCIDs));
			}
			return array;
		}

		// Token: 0x040005F5 RID: 1525
		public const string LtrDirectionMark = "&#x200E;";

		// Token: 0x040005F6 RID: 1526
		public const string RtlDirectionMark = "&#x200F;";

		// Token: 0x040005F7 RID: 1527
		private const string DefaultSingularExpression = "^1$";

		// Token: 0x040005F8 RID: 1528
		private const string DefaultPluralExpression = ".";

		// Token: 0x040005F9 RID: 1529
		private const string CzechPluralExpression = "^[234]$";

		// Token: 0x040005FA RID: 1530
		private const string RussianOrPolishPluralExpression = "^[234]$|[^1][234]$";

		// Token: 0x040005FB RID: 1531
		private const string RussianSingularExpression = "^1$|[^1]1$";

		// Token: 0x040005FC RID: 1532
		private const string DefaultCssFontFileName = "owafont.css";

		// Token: 0x040005FD RID: 1533
		private const int LanguageThreshold = 5;

		// Token: 0x040005FE RID: 1534
		private static readonly List<CultureInfo> supportedCultureInfos = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));

		// Token: 0x040005FF RID: 1535
		private static readonly CultureInfo[] supportedCultureInfosSortedByLcid = Culture.CreateSortedSupportedCultures(false);

		// Token: 0x04000600 RID: 1536
		private static readonly string[] supportedBrowserLanguages = Culture.GetSupportedBrowserLanguageArray();

		// Token: 0x04000601 RID: 1537
		private static readonly Dictionary<string, int> languageMap = Culture.LoadLanguageMap();

		// Token: 0x04000602 RID: 1538
		private static Culture.SingularPluralRegularExpression defaultRegularExpression = new Culture.SingularPluralRegularExpression("^1$", ".");

		// Token: 0x04000603 RID: 1539
		private static readonly Dictionary<int, Culture.SingularPluralRegularExpression> regularExpressionMap = Culture.LoadRemindersDueInRegularExpressionsMap();

		// Token: 0x04000604 RID: 1540
		private static readonly Dictionary<string, string[]> oneLetterDayNamesMap = Culture.LoadOneLetterDayNamesMap();

		// Token: 0x04000605 RID: 1541
		private static Dictionary<int, string> fontFileNameTable = Culture.LoadFontFileNameDictionary();

		// Token: 0x020000FD RID: 253
		public struct SingularPluralRegularExpression
		{
			// Token: 0x06000877 RID: 2167 RVA: 0x0003EAC2 File Offset: 0x0003CCC2
			internal SingularPluralRegularExpression(string singularExpression, string pluralExpression)
			{
				this.singularExpression = singularExpression;
				this.pluralExpression = pluralExpression;
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000878 RID: 2168 RVA: 0x0003EAD2 File Offset: 0x0003CCD2
			internal string SingularExpression
			{
				get
				{
					return this.singularExpression;
				}
			}

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000879 RID: 2169 RVA: 0x0003EADA File Offset: 0x0003CCDA
			internal string PluralExpression
			{
				get
				{
					return this.pluralExpression;
				}
			}

			// Token: 0x04000606 RID: 1542
			private string singularExpression;

			// Token: 0x04000607 RID: 1543
			private string pluralExpression;
		}
	}
}
