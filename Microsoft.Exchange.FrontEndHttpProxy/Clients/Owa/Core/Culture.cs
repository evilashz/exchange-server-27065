using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200004B RID: 75
	public static class Culture
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000BF12 File Offset: 0x0000A112
		public static bool IsRtl
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000BF28 File Offset: 0x0000A128
		public static string GetCssFontFileNameFromCulture()
		{
			return Culture.GetCssFontFileNameFromCulture(false);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000BF30 File Offset: 0x0000A130
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

		// Token: 0x0600022E RID: 558 RVA: 0x0000BF5C File Offset: 0x0000A15C
		public static CultureInfo GetBrowserDefaultCulture(HttpRequest httpRequest)
		{
			string[] userLanguages = httpRequest.UserLanguages;
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

		// Token: 0x0600022F RID: 559 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		public static string GetCssFontFileNameFromCulture(CultureInfo culture)
		{
			string text = null;
			Culture.fontFileNameTable.TryGetValue(culture.LCID, out text);
			if (string.IsNullOrEmpty(text))
			{
				return "owafont.css";
			}
			return text;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000BFD9 File Offset: 0x0000A1D9
		public static Culture.SingularPluralRegularExpression GetSingularPluralRegularExpressions(int lcid)
		{
			if (Culture.regularExpressionMap.ContainsKey(lcid))
			{
				return Culture.regularExpressionMap[lcid];
			}
			return Culture.defaultRegularExpression;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000BFF9 File Offset: 0x0000A1F9
		public static CultureInfo[] GetSupportedCultures()
		{
			return Culture.GetSupportedCultures(false);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000C001 File Offset: 0x0000A201
		public static CultureInfo[] GetSupportedCultures(bool sortByName)
		{
			if (!sortByName)
			{
				return Culture.supportedCultureInfosSortedByLcid;
			}
			return Culture.CreateSortedSupportedCultures(sortByName);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000C012 File Offset: 0x0000A212
		public static bool IsSupportedCulture(CultureInfo culture)
		{
			return Culture.supportedCultureInfos.Contains(culture);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C01F File Offset: 0x0000A21F
		public static bool IsSupportedCulture(int lcid)
		{
			return lcid > 0 && Culture.IsSupportedCulture(CultureInfo.GetCultureInfo(lcid));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000C034 File Offset: 0x0000A234
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

		// Token: 0x06000236 RID: 566 RVA: 0x0000C0AC File Offset: 0x0000A2AC
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

		// Token: 0x06000237 RID: 567 RVA: 0x0000C124 File Offset: 0x0000A324
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

		// Token: 0x06000238 RID: 568 RVA: 0x0000C170 File Offset: 0x0000A370
		internal static CultureInfo GetUserCulture()
		{
			return Thread.CurrentThread.CurrentCulture;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000C17C File Offset: 0x0000A37C
		internal static CultureInfo GetCultureInfoInstance(int lcid)
		{
			CultureInfo cultureInfo = new CultureInfo(lcid);
			Calendar calendar = new GregorianCalendar();
			cultureInfo.DateTimeFormat.Calendar = calendar;
			return cultureInfo;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
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

		// Token: 0x0600023B RID: 571 RVA: 0x0000C26C File Offset: 0x0000A46C
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

		// Token: 0x0600023C RID: 572 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
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

		// Token: 0x0600023D RID: 573 RVA: 0x0000C7AC File Offset: 0x0000A9AC
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

		// Token: 0x0600023E RID: 574 RVA: 0x0000D18C File Offset: 0x0000B38C
		private static int CompareCultureNames(CultureInfo x, CultureInfo y)
		{
			return string.Compare(x.NativeName, y.NativeName, StringComparison.CurrentCulture);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		private static int CompareCultureLCIDs(CultureInfo x, CultureInfo y)
		{
			return x.LCID.CompareTo(y.LCID);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
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

		// Token: 0x06000241 RID: 577 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		private static List<CultureInfo> CreateCultureInfosFromNames(string[] cultureNames)
		{
			List<CultureInfo> list = new List<CultureInfo>(cultureNames.Length);
			foreach (string name in cultureNames)
			{
				list.Add(CultureInfo.GetCultureInfo(name));
			}
			return list;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
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

		// Token: 0x04000122 RID: 290
		public const string LtrDirectionMark = "&#x200E;";

		// Token: 0x04000123 RID: 291
		public const string RtlDirectionMark = "&#x200F;";

		// Token: 0x04000124 RID: 292
		private const int LanguageThreshold = 5;

		// Token: 0x04000125 RID: 293
		private const string DefaultSingularExpression = "^1$";

		// Token: 0x04000126 RID: 294
		private const string DefaultPluralExpression = ".";

		// Token: 0x04000127 RID: 295
		private const string CzechPluralExpression = "^[234]$";

		// Token: 0x04000128 RID: 296
		private const string RussianOrPolishPluralExpression = "^[234]$|[^1][234]$";

		// Token: 0x04000129 RID: 297
		private const string RussianSingularExpression = "^1$|[^1]1$";

		// Token: 0x0400012A RID: 298
		private const string DefaultCssFontFileName = "owafont.css";

		// Token: 0x0400012B RID: 299
		private static readonly List<CultureInfo> supportedCultureInfos = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));

		// Token: 0x0400012C RID: 300
		private static readonly CultureInfo[] supportedCultureInfosSortedByLcid = Culture.CreateSortedSupportedCultures(false);

		// Token: 0x0400012D RID: 301
		private static readonly string[] supportedBrowserLanguages = Culture.GetSupportedBrowserLanguageArray();

		// Token: 0x0400012E RID: 302
		private static readonly Dictionary<string, int> languageMap = Culture.LoadLanguageMap();

		// Token: 0x0400012F RID: 303
		private static readonly Dictionary<int, Culture.SingularPluralRegularExpression> regularExpressionMap = Culture.LoadRemindersDueInRegularExpressionsMap();

		// Token: 0x04000130 RID: 304
		private static readonly Dictionary<string, string[]> oneLetterDayNamesMap = Culture.LoadOneLetterDayNamesMap();

		// Token: 0x04000131 RID: 305
		private static Culture.SingularPluralRegularExpression defaultRegularExpression = new Culture.SingularPluralRegularExpression("^1$", ".");

		// Token: 0x04000132 RID: 306
		private static Dictionary<int, string> fontFileNameTable = Culture.LoadFontFileNameDictionary();

		// Token: 0x0200004C RID: 76
		public struct SingularPluralRegularExpression
		{
			// Token: 0x06000244 RID: 580 RVA: 0x0000D3EE File Offset: 0x0000B5EE
			internal SingularPluralRegularExpression(string singularExpression, string pluralExpression)
			{
				this.singularExpression = singularExpression;
				this.pluralExpression = pluralExpression;
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000245 RID: 581 RVA: 0x0000D3FE File Offset: 0x0000B5FE
			internal string SingularExpression
			{
				get
				{
					return this.singularExpression;
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000246 RID: 582 RVA: 0x0000D406 File Offset: 0x0000B606
			internal string PluralExpression
			{
				get
				{
					return this.pluralExpression;
				}
			}

			// Token: 0x04000133 RID: 307
			private string singularExpression;

			// Token: 0x04000134 RID: 308
			private string pluralExpression;
		}
	}
}
