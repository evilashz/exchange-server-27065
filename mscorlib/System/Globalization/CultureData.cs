using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Globalization
{
	// Token: 0x020003AF RID: 943
	[FriendAccessAllowed]
	internal class CultureData
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x000BCDCC File Offset: 0x000BAFCC
		private static Dictionary<string, string> RegionNames
		{
			get
			{
				if (CultureData.s_RegionNames == null)
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>
					{
						{
							"029",
							"en-029"
						},
						{
							"AE",
							"ar-AE"
						},
						{
							"AF",
							"prs-AF"
						},
						{
							"AL",
							"sq-AL"
						},
						{
							"AM",
							"hy-AM"
						},
						{
							"AR",
							"es-AR"
						},
						{
							"AT",
							"de-AT"
						},
						{
							"AU",
							"en-AU"
						},
						{
							"AZ",
							"az-Cyrl-AZ"
						},
						{
							"BA",
							"bs-Latn-BA"
						},
						{
							"BD",
							"bn-BD"
						},
						{
							"BE",
							"nl-BE"
						},
						{
							"BG",
							"bg-BG"
						},
						{
							"BH",
							"ar-BH"
						},
						{
							"BN",
							"ms-BN"
						},
						{
							"BO",
							"es-BO"
						},
						{
							"BR",
							"pt-BR"
						},
						{
							"BY",
							"be-BY"
						},
						{
							"BZ",
							"en-BZ"
						},
						{
							"CA",
							"en-CA"
						},
						{
							"CH",
							"it-CH"
						},
						{
							"CL",
							"es-CL"
						},
						{
							"CN",
							"zh-CN"
						},
						{
							"CO",
							"es-CO"
						},
						{
							"CR",
							"es-CR"
						},
						{
							"CS",
							"sr-Cyrl-CS"
						},
						{
							"CZ",
							"cs-CZ"
						},
						{
							"DE",
							"de-DE"
						},
						{
							"DK",
							"da-DK"
						},
						{
							"DO",
							"es-DO"
						},
						{
							"DZ",
							"ar-DZ"
						},
						{
							"EC",
							"es-EC"
						},
						{
							"EE",
							"et-EE"
						},
						{
							"EG",
							"ar-EG"
						},
						{
							"ES",
							"es-ES"
						},
						{
							"ET",
							"am-ET"
						},
						{
							"FI",
							"fi-FI"
						},
						{
							"FO",
							"fo-FO"
						},
						{
							"FR",
							"fr-FR"
						},
						{
							"GB",
							"en-GB"
						},
						{
							"GE",
							"ka-GE"
						},
						{
							"GL",
							"kl-GL"
						},
						{
							"GR",
							"el-GR"
						},
						{
							"GT",
							"es-GT"
						},
						{
							"HK",
							"zh-HK"
						},
						{
							"HN",
							"es-HN"
						},
						{
							"HR",
							"hr-HR"
						},
						{
							"HU",
							"hu-HU"
						},
						{
							"ID",
							"id-ID"
						},
						{
							"IE",
							"en-IE"
						},
						{
							"IL",
							"he-IL"
						},
						{
							"IN",
							"hi-IN"
						},
						{
							"IQ",
							"ar-IQ"
						},
						{
							"IR",
							"fa-IR"
						},
						{
							"IS",
							"is-IS"
						},
						{
							"IT",
							"it-IT"
						},
						{
							"IV",
							""
						},
						{
							"JM",
							"en-JM"
						},
						{
							"JO",
							"ar-JO"
						},
						{
							"JP",
							"ja-JP"
						},
						{
							"KE",
							"sw-KE"
						},
						{
							"KG",
							"ky-KG"
						},
						{
							"KH",
							"km-KH"
						},
						{
							"KR",
							"ko-KR"
						},
						{
							"KW",
							"ar-KW"
						},
						{
							"KZ",
							"kk-KZ"
						},
						{
							"LA",
							"lo-LA"
						},
						{
							"LB",
							"ar-LB"
						},
						{
							"LI",
							"de-LI"
						},
						{
							"LK",
							"si-LK"
						},
						{
							"LT",
							"lt-LT"
						},
						{
							"LU",
							"lb-LU"
						},
						{
							"LV",
							"lv-LV"
						},
						{
							"LY",
							"ar-LY"
						},
						{
							"MA",
							"ar-MA"
						},
						{
							"MC",
							"fr-MC"
						},
						{
							"ME",
							"sr-Latn-ME"
						},
						{
							"MK",
							"mk-MK"
						},
						{
							"MN",
							"mn-MN"
						},
						{
							"MO",
							"zh-MO"
						},
						{
							"MT",
							"mt-MT"
						},
						{
							"MV",
							"dv-MV"
						},
						{
							"MX",
							"es-MX"
						},
						{
							"MY",
							"ms-MY"
						},
						{
							"NG",
							"ig-NG"
						},
						{
							"NI",
							"es-NI"
						},
						{
							"NL",
							"nl-NL"
						},
						{
							"NO",
							"nn-NO"
						},
						{
							"NP",
							"ne-NP"
						},
						{
							"NZ",
							"en-NZ"
						},
						{
							"OM",
							"ar-OM"
						},
						{
							"PA",
							"es-PA"
						},
						{
							"PE",
							"es-PE"
						},
						{
							"PH",
							"en-PH"
						},
						{
							"PK",
							"ur-PK"
						},
						{
							"PL",
							"pl-PL"
						},
						{
							"PR",
							"es-PR"
						},
						{
							"PT",
							"pt-PT"
						},
						{
							"PY",
							"es-PY"
						},
						{
							"QA",
							"ar-QA"
						},
						{
							"RO",
							"ro-RO"
						},
						{
							"RS",
							"sr-Latn-RS"
						},
						{
							"RU",
							"ru-RU"
						},
						{
							"RW",
							"rw-RW"
						},
						{
							"SA",
							"ar-SA"
						},
						{
							"SE",
							"sv-SE"
						},
						{
							"SG",
							"zh-SG"
						},
						{
							"SI",
							"sl-SI"
						},
						{
							"SK",
							"sk-SK"
						},
						{
							"SN",
							"wo-SN"
						},
						{
							"SV",
							"es-SV"
						},
						{
							"SY",
							"ar-SY"
						},
						{
							"TH",
							"th-TH"
						},
						{
							"TJ",
							"tg-Cyrl-TJ"
						},
						{
							"TM",
							"tk-TM"
						},
						{
							"TN",
							"ar-TN"
						},
						{
							"TR",
							"tr-TR"
						},
						{
							"TT",
							"en-TT"
						},
						{
							"TW",
							"zh-TW"
						},
						{
							"UA",
							"uk-UA"
						},
						{
							"US",
							"en-US"
						},
						{
							"UY",
							"es-UY"
						},
						{
							"UZ",
							"uz-Cyrl-UZ"
						},
						{
							"VE",
							"es-VE"
						},
						{
							"VN",
							"vi-VN"
						},
						{
							"YE",
							"ar-YE"
						},
						{
							"ZA",
							"af-ZA"
						},
						{
							"ZW",
							"en-ZW"
						}
					};
					CultureData.s_RegionNames = dictionary;
				}
				return CultureData.s_RegionNames;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x000BD5FC File Offset: 0x000BB7FC
		internal static CultureData Invariant
		{
			get
			{
				if (CultureData.s_Invariant == null)
				{
					CultureData cultureData = new CultureData();
					cultureData.bUseOverrides = false;
					cultureData.sRealName = "";
					CultureData.nativeInitCultureData(cultureData);
					cultureData.bUseOverrides = false;
					cultureData.sRealName = "";
					cultureData.sWindowsName = "";
					cultureData.sName = "";
					cultureData.sParent = "";
					cultureData.bNeutral = false;
					cultureData.bFramework = true;
					cultureData.sEnglishDisplayName = "Invariant Language (Invariant Country)";
					cultureData.sNativeDisplayName = "Invariant Language (Invariant Country)";
					cultureData.sSpecificCulture = "";
					cultureData.sISO639Language = "iv";
					cultureData.sLocalizedLanguage = "Invariant Language";
					cultureData.sEnglishLanguage = "Invariant Language";
					cultureData.sNativeLanguage = "Invariant Language";
					cultureData.sRegionName = "IV";
					cultureData.iGeoId = 244;
					cultureData.sEnglishCountry = "Invariant Country";
					cultureData.sNativeCountry = "Invariant Country";
					cultureData.sISO3166CountryName = "IV";
					cultureData.sPositiveSign = "+";
					cultureData.sNegativeSign = "-";
					cultureData.saNativeDigits = new string[]
					{
						"0",
						"1",
						"2",
						"3",
						"4",
						"5",
						"6",
						"7",
						"8",
						"9"
					};
					cultureData.iDigitSubstitution = 1;
					cultureData.iLeadingZeros = 1;
					cultureData.iDigits = 2;
					cultureData.iNegativeNumber = 1;
					cultureData.waGrouping = new int[]
					{
						3
					};
					cultureData.sDecimalSeparator = ".";
					cultureData.sThousandSeparator = ",";
					cultureData.sNaN = "NaN";
					cultureData.sPositiveInfinity = "Infinity";
					cultureData.sNegativeInfinity = "-Infinity";
					cultureData.iNegativePercent = 0;
					cultureData.iPositivePercent = 0;
					cultureData.sPercent = "%";
					cultureData.sPerMille = "‰";
					cultureData.sCurrency = "¤";
					cultureData.sIntlMonetarySymbol = "XDR";
					cultureData.sEnglishCurrency = "International Monetary Fund";
					cultureData.sNativeCurrency = "International Monetary Fund";
					cultureData.iCurrencyDigits = 2;
					cultureData.iCurrency = 0;
					cultureData.iNegativeCurrency = 0;
					cultureData.waMonetaryGrouping = new int[]
					{
						3
					};
					cultureData.sMonetaryDecimal = ".";
					cultureData.sMonetaryThousand = ",";
					cultureData.iMeasure = 0;
					cultureData.sListSeparator = ",";
					cultureData.sAM1159 = "AM";
					cultureData.sPM2359 = "PM";
					cultureData.saLongTimes = new string[]
					{
						"HH:mm:ss"
					};
					cultureData.saShortTimes = new string[]
					{
						"HH:mm",
						"hh:mm tt",
						"H:mm",
						"h:mm tt"
					};
					cultureData.saDurationFormats = new string[]
					{
						"HH:mm:ss"
					};
					cultureData.iFirstDayOfWeek = 0;
					cultureData.iFirstWeekOfYear = 0;
					cultureData.waCalendars = new int[]
					{
						1
					};
					cultureData.calendars = new CalendarData[23];
					cultureData.calendars[0] = CalendarData.Invariant;
					cultureData.iReadingLayout = 0;
					cultureData.sTextInfo = "";
					cultureData.sCompareInfo = "";
					cultureData.sScripts = "Latn;";
					cultureData.iLanguage = 127;
					cultureData.iDefaultAnsiCodePage = 1252;
					cultureData.iDefaultOemCodePage = 437;
					cultureData.iDefaultMacCodePage = 10000;
					cultureData.iDefaultEbcdicCodePage = 37;
					cultureData.sAbbrevLang = "IVL";
					cultureData.sAbbrevCountry = "IVC";
					cultureData.sISO639Language2 = "ivl";
					cultureData.sISO3166CountryName2 = "ivc";
					cultureData.iInputLanguageHandle = 127;
					cultureData.sConsoleFallbackName = "";
					cultureData.sKeyboardsToInstall = "0409:00000409";
					CultureData.s_Invariant = cultureData;
				}
				return CultureData.s_Invariant;
			}
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000BD9C9 File Offset: 0x000BBBC9
		[SecurityCritical]
		private static bool IsResourcePresent(string resourceKey)
		{
			if (CultureData.MscorlibResourceSet == null)
			{
				CultureData.MscorlibResourceSet = new ResourceSet(typeof(Environment).Assembly.GetManifestResourceStream("mscorlib.resources"));
			}
			return CultureData.MscorlibResourceSet.GetString(resourceKey) != null;
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		[FriendAccessAllowed]
		internal static CultureData GetCultureData(string cultureName, bool useUserOverride)
		{
			if (string.IsNullOrEmpty(cultureName))
			{
				return CultureData.Invariant;
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				if (cultureName.Equals("iw", StringComparison.OrdinalIgnoreCase))
				{
					cultureName = "he";
				}
				else if (cultureName.Equals("tl", StringComparison.OrdinalIgnoreCase))
				{
					cultureName = "fil";
				}
				else if (cultureName.Equals("english", StringComparison.OrdinalIgnoreCase))
				{
					cultureName = "en";
				}
			}
			string key = CultureData.AnsiToLower(useUserOverride ? cultureName : (cultureName + "*"));
			Dictionary<string, CultureData> dictionary = CultureData.s_cachedCultures;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, CultureData>();
			}
			else
			{
				object syncRoot = ((ICollection)dictionary).SyncRoot;
				CultureData cultureData;
				lock (syncRoot)
				{
					dictionary.TryGetValue(key, out cultureData);
				}
				if (cultureData != null)
				{
					return cultureData;
				}
			}
			CultureData cultureData2 = CultureData.CreateCultureData(cultureName, useUserOverride);
			if (cultureData2 == null)
			{
				return null;
			}
			object syncRoot2 = ((ICollection)dictionary).SyncRoot;
			lock (syncRoot2)
			{
				dictionary[key] = cultureData2;
			}
			CultureData.s_cachedCultures = dictionary;
			return cultureData2;
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000BDB28 File Offset: 0x000BBD28
		private static CultureData CreateCultureData(string cultureName, bool useUserOverride)
		{
			CultureData cultureData = new CultureData();
			cultureData.bUseOverrides = useUserOverride;
			cultureData.sRealName = cultureName;
			if (!cultureData.InitCultureData() && !cultureData.InitCompatibilityCultureData() && !cultureData.InitLegacyAlternateSortData())
			{
				return null;
			}
			return cultureData;
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000BDB64 File Offset: 0x000BBD64
		private bool InitCultureData()
		{
			if (!CultureData.nativeInitCultureData(this))
			{
				return false;
			}
			if (CultureInfo.IsTaiwanSku)
			{
				this.TreatTaiwanParentChainAsHavingTaiwanAsSpecific();
			}
			return true;
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000BDB80 File Offset: 0x000BBD80
		[SecuritySafeCritical]
		private void TreatTaiwanParentChainAsHavingTaiwanAsSpecific()
		{
			if (this.IsNeutralInParentChainOfTaiwan() && CultureData.IsOsPriorToWin7() && !this.IsReplacementCulture)
			{
				string text = this.SNATIVELANGUAGE;
				text = this.SENGLISHLANGUAGE;
				text = this.SLOCALIZEDLANGUAGE;
				text = this.STEXTINFO;
				text = this.SCOMPAREINFO;
				text = this.FONTSIGNATURE;
				int num = this.IDEFAULTANSICODEPAGE;
				num = this.IDEFAULTOEMCODEPAGE;
				num = this.IDEFAULTMACCODEPAGE;
				this.sSpecificCulture = "zh-TW";
				this.sWindowsName = "zh-TW";
			}
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000BDBF9 File Offset: 0x000BBDF9
		private bool IsNeutralInParentChainOfTaiwan()
		{
			return this.sRealName == "zh" || this.sRealName == "zh-Hant";
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000BDC1F File Offset: 0x000BBE1F
		private static bool IsOsPriorToWin7()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version < CultureData.s_win7Version;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000BDC44 File Offset: 0x000BBE44
		private static bool IsOsWin7OrPrior()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version < new Version(6, 2);
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000BDC6C File Offset: 0x000BBE6C
		private bool InitCompatibilityCultureData()
		{
			string testString = this.sRealName;
			string a = CultureData.AnsiToLower(testString);
			string text;
			string text2;
			if (!(a == "zh-chs"))
			{
				if (!(a == "zh-cht"))
				{
					return false;
				}
				text = "zh-Hant";
				text2 = "zh-CHT";
			}
			else
			{
				text = "zh-Hans";
				text2 = "zh-CHS";
			}
			this.sRealName = text;
			if (!this.InitCultureData())
			{
				return false;
			}
			this.sName = text2;
			this.sParent = text;
			this.bFramework = true;
			return true;
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000BDCE8 File Offset: 0x000BBEE8
		private bool InitLegacyAlternateSortData()
		{
			if (!CompareInfo.IsLegacy20SortingBehaviorRequested)
			{
				return false;
			}
			string testString = this.sRealName;
			string a = CultureData.AnsiToLower(testString);
			if (!(a == "ko-kr_unicod"))
			{
				if (!(a == "ja-jp_unicod"))
				{
					if (!(a == "zh-hk_stroke"))
					{
						return false;
					}
					testString = "zh-HK_stroke";
					this.sRealName = "zh-HK";
					this.iLanguage = 134148;
				}
				else
				{
					testString = "ja-JP_unicod";
					this.sRealName = "ja-JP";
					this.iLanguage = 66577;
				}
			}
			else
			{
				testString = "ko-KR_unicod";
				this.sRealName = "ko-KR";
				this.iLanguage = 66578;
			}
			if (!CultureData.nativeInitCultureData(this))
			{
				return false;
			}
			this.sRealName = testString;
			this.sCompareInfo = testString;
			this.bFramework = true;
			return true;
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000BDDB4 File Offset: 0x000BBFB4
		[SecurityCritical]
		internal static CultureData GetCultureDataForRegion(string cultureName, bool useUserOverride)
		{
			if (string.IsNullOrEmpty(cultureName))
			{
				return CultureData.Invariant;
			}
			CultureData cultureData = CultureData.GetCultureData(cultureName, useUserOverride);
			if (cultureData != null && !cultureData.IsNeutralCulture)
			{
				return cultureData;
			}
			CultureData cultureData2 = cultureData;
			string key = CultureData.AnsiToLower(useUserOverride ? cultureName : (cultureName + "*"));
			Dictionary<string, CultureData> dictionary = CultureData.s_cachedRegions;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, CultureData>();
			}
			else
			{
				object syncRoot = ((ICollection)dictionary).SyncRoot;
				lock (syncRoot)
				{
					dictionary.TryGetValue(key, out cultureData);
				}
				if (cultureData != null)
				{
					return cultureData;
				}
			}
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.InternalOpenSubKey(CultureData.s_RegionKey, false);
				if (registryKey != null)
				{
					try
					{
						object obj = registryKey.InternalGetValue(cultureName, null, false, false);
						if (obj != null)
						{
							string cultureName2 = obj.ToString();
							cultureData = CultureData.GetCultureData(cultureName2, useUserOverride);
						}
					}
					finally
					{
						registryKey.Close();
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (ArgumentException)
			{
			}
			if ((cultureData == null || cultureData.IsNeutralCulture) && CultureData.RegionNames.ContainsKey(cultureName))
			{
				cultureData = CultureData.GetCultureData(CultureData.RegionNames[cultureName], useUserOverride);
			}
			if (cultureData == null || cultureData.IsNeutralCulture)
			{
				CultureInfo[] array = CultureData.SpecificCultures;
				for (int i = 0; i < array.Length; i++)
				{
					if (string.Compare(array[i].m_cultureData.SREGIONNAME, cultureName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						cultureData = array[i].m_cultureData;
						break;
					}
				}
			}
			if (cultureData != null && !cultureData.IsNeutralCulture)
			{
				object syncRoot2 = ((ICollection)dictionary).SyncRoot;
				lock (syncRoot2)
				{
					dictionary[key] = cultureData;
				}
				CultureData.s_cachedRegions = dictionary;
			}
			else
			{
				cultureData = cultureData2;
			}
			return cultureData;
		}

		// Token: 0x0600313F RID: 12607
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string LCIDToLocaleName(int lcid);

		// Token: 0x06003140 RID: 12608 RVA: 0x000BDF84 File Offset: 0x000BC184
		internal static CultureData GetCultureData(int culture, bool bUseUserOverride)
		{
			string text = null;
			CultureData cultureData = null;
			if (CompareInfo.IsLegacy20SortingBehaviorRequested)
			{
				if (culture != 66577)
				{
					if (culture != 66578)
					{
						if (culture == 134148)
						{
							text = "zh-HK_stroke";
						}
					}
					else
					{
						text = "ko-KR_unicod";
					}
				}
				else
				{
					text = "ja-JP_unicod";
				}
			}
			if (text == null)
			{
				text = CultureData.LCIDToLocaleName(culture);
			}
			if (string.IsNullOrEmpty(text))
			{
				if (culture == 127)
				{
					return CultureData.Invariant;
				}
			}
			else
			{
				if (!(text == "zh-Hans"))
				{
					if (text == "zh-Hant")
					{
						text = "zh-CHT";
					}
				}
				else
				{
					text = "zh-CHS";
				}
				cultureData = CultureData.GetCultureData(text, bUseUserOverride);
			}
			if (cultureData == null)
			{
				throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			return cultureData;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000BE035 File Offset: 0x000BC235
		internal static void ClearCachedData()
		{
			CultureData.s_cachedCultures = null;
			CultureData.s_cachedRegions = null;
			CultureData.s_replacementCultureNames = null;
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000BE050 File Offset: 0x000BC250
		[SecuritySafeCritical]
		internal static CultureInfo[] GetCultures(CultureTypes types)
		{
			if (types <= (CultureTypes)0 || (types & ~(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures | CultureTypes.InstalledWin32Cultures | CultureTypes.UserCustomCulture | CultureTypes.ReplacementCultures | CultureTypes.WindowsOnlyCultures | CultureTypes.FrameworkCultures)) != (CultureTypes)0)
			{
				throw new ArgumentOutOfRangeException("types", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), CultureTypes.NeutralCultures, CultureTypes.FrameworkCultures));
			}
			if ((types & CultureTypes.WindowsOnlyCultures) != (CultureTypes)0)
			{
				types &= ~CultureTypes.WindowsOnlyCultures;
			}
			string[] array = null;
			if (CultureData.nativeEnumCultureNames((int)types, JitHelpers.GetObjectHandleOnStack<string[]>(ref array)) == 0)
			{
				return new CultureInfo[0];
			}
			int num = array.Length;
			if ((types & (CultureTypes.NeutralCultures | CultureTypes.FrameworkCultures)) != (CultureTypes)0)
			{
				num += 2;
			}
			CultureInfo[] array2 = new CultureInfo[num];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new CultureInfo(array[i]);
			}
			if ((types & (CultureTypes.NeutralCultures | CultureTypes.FrameworkCultures)) != (CultureTypes)0)
			{
				array2[array.Length] = new CultureInfo("zh-CHS");
				array2[array.Length + 1] = new CultureInfo("zh-CHT");
			}
			return array2;
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000BE10C File Offset: 0x000BC30C
		private static CultureInfo[] SpecificCultures
		{
			get
			{
				if (CultureData.specificCultures == null)
				{
					CultureData.specificCultures = CultureData.GetCultures(CultureTypes.SpecificCultures);
				}
				return CultureData.specificCultures;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000BE12B File Offset: 0x000BC32B
		internal bool IsReplacementCulture
		{
			get
			{
				return CultureData.IsReplacementCultureName(this.SNAME);
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000BE138 File Offset: 0x000BC338
		[SecuritySafeCritical]
		private static bool IsReplacementCultureName(string name)
		{
			string[] array = CultureData.s_replacementCultureNames;
			if (array == null)
			{
				if (CultureData.nativeEnumCultureNames(16, JitHelpers.GetObjectHandleOnStack<string[]>(ref array)) == 0)
				{
					return false;
				}
				Array.Sort<string>(array);
				CultureData.s_replacementCultureNames = array;
			}
			return Array.BinarySearch<string>(array, name) >= 0;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000BE180 File Offset: 0x000BC380
		internal string CultureName
		{
			get
			{
				string a = this.sName;
				if (a == "zh-CHS" || a == "zh-CHT")
				{
					return this.sName;
				}
				return this.sRealName;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000BE1BB File Offset: 0x000BC3BB
		internal bool UseUserOverride
		{
			get
			{
				return this.bUseOverrides;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x000BE1C3 File Offset: 0x000BC3C3
		internal string SNAME
		{
			get
			{
				if (this.sName == null)
				{
					this.sName = string.Empty;
				}
				return this.sName;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000BE1E0 File Offset: 0x000BC3E0
		internal string SPARENT
		{
			[SecurityCritical]
			get
			{
				if (this.sParent == null)
				{
					this.sParent = this.DoGetLocaleInfo(this.sRealName, 109U);
					string a = this.sParent;
					if (!(a == "zh-Hans"))
					{
						if (a == "zh-Hant")
						{
							this.sParent = "zh-CHT";
						}
					}
					else
					{
						this.sParent = "zh-CHS";
					}
				}
				return this.sParent;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000BE24C File Offset: 0x000BC44C
		internal string SLOCALIZEDDISPLAYNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sLocalizedDisplayName == null)
				{
					string text = "Globalization.ci_" + this.sName;
					if (CultureData.IsResourcePresent(text))
					{
						this.sLocalizedDisplayName = Environment.GetResourceString(text);
					}
					if (string.IsNullOrEmpty(this.sLocalizedDisplayName))
					{
						if (this.IsNeutralCulture)
						{
							this.sLocalizedDisplayName = this.SLOCALIZEDLANGUAGE;
						}
						else
						{
							if (CultureInfo.UserDefaultUICulture.Name.Equals(Thread.CurrentThread.CurrentUICulture.Name))
							{
								this.sLocalizedDisplayName = this.DoGetLocaleInfo(2U);
							}
							if (string.IsNullOrEmpty(this.sLocalizedDisplayName))
							{
								this.sLocalizedDisplayName = this.SNATIVEDISPLAYNAME;
							}
						}
					}
				}
				return this.sLocalizedDisplayName;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x000BE2F8 File Offset: 0x000BC4F8
		internal string SENGDISPLAYNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sEnglishDisplayName == null)
				{
					if (this.IsNeutralCulture)
					{
						this.sEnglishDisplayName = this.SENGLISHLANGUAGE;
						string a = this.sName;
						if (a == "zh-CHS" || a == "zh-CHT")
						{
							this.sEnglishDisplayName += " Legacy";
						}
					}
					else
					{
						this.sEnglishDisplayName = this.DoGetLocaleInfo(114U);
						if (string.IsNullOrEmpty(this.sEnglishDisplayName))
						{
							if (this.SENGLISHLANGUAGE.EndsWith(')'))
							{
								this.sEnglishDisplayName = this.SENGLISHLANGUAGE.Substring(0, this.sEnglishLanguage.Length - 1) + ", " + this.SENGCOUNTRY + ")";
							}
							else
							{
								this.sEnglishDisplayName = this.SENGLISHLANGUAGE + " (" + this.SENGCOUNTRY + ")";
							}
						}
					}
				}
				return this.sEnglishDisplayName;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000BE3EC File Offset: 0x000BC5EC
		internal string SNATIVEDISPLAYNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sNativeDisplayName == null)
				{
					if (this.IsNeutralCulture)
					{
						this.sNativeDisplayName = this.SNATIVELANGUAGE;
						string a = this.sName;
						if (!(a == "zh-CHS"))
						{
							if (a == "zh-CHT")
							{
								this.sNativeDisplayName += " 舊版";
							}
						}
						else
						{
							this.sNativeDisplayName += " 旧版";
						}
					}
					else
					{
						if (this.IsIncorrectNativeLanguageForSinhala())
						{
							this.sNativeDisplayName = "සිංහල (ශ්‍රී ලංකා)";
						}
						else
						{
							this.sNativeDisplayName = this.DoGetLocaleInfo(115U);
						}
						if (string.IsNullOrEmpty(this.sNativeDisplayName))
						{
							this.sNativeDisplayName = this.SNATIVELANGUAGE + " (" + this.SNATIVECOUNTRY + ")";
						}
					}
				}
				return this.sNativeDisplayName;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x000BE4C5 File Offset: 0x000BC6C5
		internal string SSPECIFICCULTURE
		{
			get
			{
				return this.sSpecificCulture;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000BE4CD File Offset: 0x000BC6CD
		internal string SISO639LANGNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sISO639Language == null)
				{
					this.sISO639Language = this.DoGetLocaleInfo(89U);
				}
				return this.sISO639Language;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000BE4EB File Offset: 0x000BC6EB
		internal string SISO639LANGNAME2
		{
			[SecurityCritical]
			get
			{
				if (this.sISO639Language2 == null)
				{
					this.sISO639Language2 = this.DoGetLocaleInfo(103U);
				}
				return this.sISO639Language2;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000BE509 File Offset: 0x000BC709
		internal string SABBREVLANGNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sAbbrevLang == null)
				{
					this.sAbbrevLang = this.DoGetLocaleInfo(3U);
				}
				return this.sAbbrevLang;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000BE528 File Offset: 0x000BC728
		internal string SLOCALIZEDLANGUAGE
		{
			[SecurityCritical]
			get
			{
				if (this.sLocalizedLanguage == null)
				{
					if (CultureInfo.UserDefaultUICulture.Name.Equals(Thread.CurrentThread.CurrentUICulture.Name))
					{
						this.sLocalizedLanguage = this.DoGetLocaleInfo(111U);
					}
					if (string.IsNullOrEmpty(this.sLocalizedLanguage))
					{
						this.sLocalizedLanguage = this.SNATIVELANGUAGE;
					}
				}
				return this.sLocalizedLanguage;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x000BE58A File Offset: 0x000BC78A
		internal string SENGLISHLANGUAGE
		{
			[SecurityCritical]
			get
			{
				if (this.sEnglishLanguage == null)
				{
					this.sEnglishLanguage = this.DoGetLocaleInfo(4097U);
				}
				return this.sEnglishLanguage;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000BE5AB File Offset: 0x000BC7AB
		internal string SNATIVELANGUAGE
		{
			[SecurityCritical]
			get
			{
				if (this.sNativeLanguage == null)
				{
					if (this.IsIncorrectNativeLanguageForSinhala())
					{
						this.sNativeLanguage = "සිංහල";
					}
					else
					{
						this.sNativeLanguage = this.DoGetLocaleInfo(4U);
					}
				}
				return this.sNativeLanguage;
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000BE5DD File Offset: 0x000BC7DD
		private bool IsIncorrectNativeLanguageForSinhala()
		{
			return CultureData.IsOsWin7OrPrior() && (this.sName == "si-LK" || this.sName == "si") && !this.IsReplacementCulture;
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x000BE615 File Offset: 0x000BC815
		internal string SREGIONNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sRegionName == null)
				{
					this.sRegionName = this.DoGetLocaleInfo(90U);
				}
				return this.sRegionName;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000BE633 File Offset: 0x000BC833
		internal int ICOUNTRY
		{
			get
			{
				return this.DoGetLocaleInfoInt(5U);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x000BE63C File Offset: 0x000BC83C
		internal int IGEOID
		{
			get
			{
				if (this.iGeoId == -1)
				{
					this.iGeoId = this.DoGetLocaleInfoInt(91U);
				}
				return this.iGeoId;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000BE65C File Offset: 0x000BC85C
		internal string SLOCALIZEDCOUNTRY
		{
			[SecurityCritical]
			get
			{
				if (this.sLocalizedCountry == null)
				{
					string text = "Globalization.ri_" + this.SREGIONNAME;
					if (CultureData.IsResourcePresent(text))
					{
						this.sLocalizedCountry = Environment.GetResourceString(text);
					}
					if (string.IsNullOrEmpty(this.sLocalizedCountry))
					{
						if (CultureInfo.UserDefaultUICulture.Name.Equals(Thread.CurrentThread.CurrentUICulture.Name))
						{
							this.sLocalizedCountry = this.DoGetLocaleInfo(6U);
						}
						if (string.IsNullOrEmpty(this.sLocalizedDisplayName))
						{
							this.sLocalizedCountry = this.SNATIVECOUNTRY;
						}
					}
				}
				return this.sLocalizedCountry;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000BE6EF File Offset: 0x000BC8EF
		internal string SENGCOUNTRY
		{
			[SecurityCritical]
			get
			{
				if (this.sEnglishCountry == null)
				{
					this.sEnglishCountry = this.DoGetLocaleInfo(4098U);
				}
				return this.sEnglishCountry;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x000BE710 File Offset: 0x000BC910
		internal string SNATIVECOUNTRY
		{
			[SecurityCritical]
			get
			{
				if (this.sNativeCountry == null)
				{
					this.sNativeCountry = this.DoGetLocaleInfo(8U);
				}
				return this.sNativeCountry;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000BE72D File Offset: 0x000BC92D
		internal string SISO3166CTRYNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sISO3166CountryName == null)
				{
					this.sISO3166CountryName = this.DoGetLocaleInfo(90U);
				}
				return this.sISO3166CountryName;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000BE74B File Offset: 0x000BC94B
		internal string SISO3166CTRYNAME2
		{
			[SecurityCritical]
			get
			{
				if (this.sISO3166CountryName2 == null)
				{
					this.sISO3166CountryName2 = this.DoGetLocaleInfo(104U);
				}
				return this.sISO3166CountryName2;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000BE769 File Offset: 0x000BC969
		internal string SABBREVCTRYNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sAbbrevCountry == null)
				{
					this.sAbbrevCountry = this.DoGetLocaleInfo(7U);
				}
				return this.sAbbrevCountry;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000BE786 File Offset: 0x000BC986
		private int IDEFAULTCOUNTRY
		{
			get
			{
				return this.DoGetLocaleInfoInt(10U);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000BE790 File Offset: 0x000BC990
		internal int IINPUTLANGUAGEHANDLE
		{
			get
			{
				if (this.iInputLanguageHandle == -1)
				{
					if (this.IsSupplementalCustomCulture)
					{
						this.iInputLanguageHandle = 1033;
					}
					else
					{
						this.iInputLanguageHandle = this.ILANGUAGE;
					}
				}
				return this.iInputLanguageHandle;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000BE7C4 File Offset: 0x000BC9C4
		internal string SCONSOLEFALLBACKNAME
		{
			[SecurityCritical]
			get
			{
				if (this.sConsoleFallbackName == null)
				{
					string a = this.DoGetLocaleInfo(110U);
					if (a == "es-ES_tradnl")
					{
						a = "es-ES";
					}
					this.sConsoleFallbackName = a;
				}
				return this.sConsoleFallbackName;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000BE802 File Offset: 0x000BCA02
		private bool ILEADINGZEROS
		{
			get
			{
				return this.DoGetLocaleInfoInt(18U) == 1;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000BE80F File Offset: 0x000BCA0F
		internal int[] WAGROUPING
		{
			[SecurityCritical]
			get
			{
				if (this.waGrouping == null || this.UseUserOverride)
				{
					this.waGrouping = CultureData.ConvertWin32GroupString(this.DoGetLocaleInfo(16U));
				}
				return this.waGrouping;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000BE83A File Offset: 0x000BCA3A
		internal string SNAN
		{
			[SecurityCritical]
			get
			{
				if (this.sNaN == null)
				{
					this.sNaN = this.DoGetLocaleInfo(105U);
				}
				return this.sNaN;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000BE858 File Offset: 0x000BCA58
		internal string SPOSINFINITY
		{
			[SecurityCritical]
			get
			{
				if (this.sPositiveInfinity == null)
				{
					this.sPositiveInfinity = this.DoGetLocaleInfo(106U);
				}
				return this.sPositiveInfinity;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000BE876 File Offset: 0x000BCA76
		internal string SNEGINFINITY
		{
			[SecurityCritical]
			get
			{
				if (this.sNegativeInfinity == null)
				{
					this.sNegativeInfinity = this.DoGetLocaleInfo(107U);
				}
				return this.sNegativeInfinity;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000BE894 File Offset: 0x000BCA94
		internal int INEGATIVEPERCENT
		{
			get
			{
				if (this.iNegativePercent == -1)
				{
					this.iNegativePercent = this.DoGetLocaleInfoInt(116U);
				}
				return this.iNegativePercent;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000BE8B3 File Offset: 0x000BCAB3
		internal int IPOSITIVEPERCENT
		{
			get
			{
				if (this.iPositivePercent == -1)
				{
					this.iPositivePercent = this.DoGetLocaleInfoInt(117U);
				}
				return this.iPositivePercent;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000BE8D2 File Offset: 0x000BCAD2
		internal string SPERCENT
		{
			[SecurityCritical]
			get
			{
				if (this.sPercent == null)
				{
					this.sPercent = this.DoGetLocaleInfo(118U);
				}
				return this.sPercent;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000BE8F0 File Offset: 0x000BCAF0
		internal string SPERMILLE
		{
			[SecurityCritical]
			get
			{
				if (this.sPerMille == null)
				{
					this.sPerMille = this.DoGetLocaleInfo(119U);
				}
				return this.sPerMille;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000BE90E File Offset: 0x000BCB0E
		internal string SCURRENCY
		{
			[SecurityCritical]
			get
			{
				if (this.sCurrency == null || this.UseUserOverride)
				{
					this.sCurrency = this.DoGetLocaleInfo(20U);
				}
				return this.sCurrency;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000BE934 File Offset: 0x000BCB34
		internal string SINTLSYMBOL
		{
			[SecurityCritical]
			get
			{
				if (this.sIntlMonetarySymbol == null)
				{
					this.sIntlMonetarySymbol = this.DoGetLocaleInfo(21U);
				}
				return this.sIntlMonetarySymbol;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000BE952 File Offset: 0x000BCB52
		internal string SENGLISHCURRENCY
		{
			[SecurityCritical]
			get
			{
				if (this.sEnglishCurrency == null)
				{
					this.sEnglishCurrency = this.DoGetLocaleInfo(4103U);
				}
				return this.sEnglishCurrency;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000BE973 File Offset: 0x000BCB73
		internal string SNATIVECURRENCY
		{
			[SecurityCritical]
			get
			{
				if (this.sNativeCurrency == null)
				{
					this.sNativeCurrency = this.DoGetLocaleInfo(4104U);
				}
				return this.sNativeCurrency;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000BE994 File Offset: 0x000BCB94
		internal int[] WAMONGROUPING
		{
			[SecurityCritical]
			get
			{
				if (this.waMonetaryGrouping == null || this.UseUserOverride)
				{
					this.waMonetaryGrouping = CultureData.ConvertWin32GroupString(this.DoGetLocaleInfo(24U));
				}
				return this.waMonetaryGrouping;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000BE9BF File Offset: 0x000BCBBF
		internal int IMEASURE
		{
			get
			{
				if (this.iMeasure == -1 || this.UseUserOverride)
				{
					this.iMeasure = this.DoGetLocaleInfoInt(13U);
				}
				return this.iMeasure;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000BE9E6 File Offset: 0x000BCBE6
		internal string SLIST
		{
			[SecurityCritical]
			get
			{
				if (this.sListSeparator == null || this.UseUserOverride)
				{
					this.sListSeparator = this.DoGetLocaleInfo(12U);
				}
				return this.sListSeparator;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x000BEA0C File Offset: 0x000BCC0C
		private int IPAPERSIZE
		{
			get
			{
				return this.DoGetLocaleInfoInt(4106U);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000BEA19 File Offset: 0x000BCC19
		internal string SAM1159
		{
			[SecurityCritical]
			get
			{
				if (this.sAM1159 == null || this.UseUserOverride)
				{
					this.sAM1159 = this.DoGetLocaleInfo(40U);
				}
				return this.sAM1159;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x000BEA3F File Offset: 0x000BCC3F
		internal string SPM2359
		{
			[SecurityCritical]
			get
			{
				if (this.sPM2359 == null || this.UseUserOverride)
				{
					this.sPM2359 = this.DoGetLocaleInfo(41U);
				}
				return this.sPM2359;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000BEA68 File Offset: 0x000BCC68
		internal string[] LongTimes
		{
			get
			{
				if (this.saLongTimes == null || this.UseUserOverride)
				{
					string[] array = this.DoEnumTimeFormats();
					if (array == null || array.Length == 0)
					{
						this.saLongTimes = CultureData.Invariant.saLongTimes;
					}
					else
					{
						this.saLongTimes = array;
					}
				}
				return this.saLongTimes;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x000BEABC File Offset: 0x000BCCBC
		internal string[] ShortTimes
		{
			get
			{
				if (this.saShortTimes == null || this.UseUserOverride)
				{
					string[] array = this.DoEnumShortTimeFormats();
					if (array == null || array.Length == 0)
					{
						array = this.DeriveShortTimesFromLong();
					}
					this.saShortTimes = array;
				}
				return this.saShortTimes;
			}
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000BEB04 File Offset: 0x000BCD04
		private string[] DeriveShortTimesFromLong()
		{
			string[] array = new string[this.LongTimes.Length];
			for (int i = 0; i < this.LongTimes.Length; i++)
			{
				array[i] = CultureData.StripSecondsFromPattern(this.LongTimes[i]);
			}
			return array;
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000BEB44 File Offset: 0x000BCD44
		private static string StripSecondsFromPattern(string time)
		{
			bool flag = false;
			int num = -1;
			for (int i = 0; i < time.Length; i++)
			{
				if (time[i] == '\'')
				{
					flag = !flag;
				}
				else if (time[i] == '\\')
				{
					i++;
				}
				else if (!flag)
				{
					char c = time[i];
					if (c <= 'h')
					{
						if (c != 'H' && c != 'h')
						{
							goto IL_D3;
						}
					}
					else if (c != 'm')
					{
						if (c == 's')
						{
							if (i - num <= 4 && i - num > 1 && time[num + 1] != '\'' && time[i - 1] != '\'' && num >= 0)
							{
								i = num + 1;
							}
							bool flag2;
							int indexOfNextTokenAfterSeconds = CultureData.GetIndexOfNextTokenAfterSeconds(time, i, out flag2);
							StringBuilder stringBuilder = new StringBuilder(time.Substring(0, i));
							if (flag2)
							{
								stringBuilder.Append(' ');
							}
							stringBuilder.Append(time.Substring(indexOfNextTokenAfterSeconds));
							time = stringBuilder.ToString();
							goto IL_D3;
						}
						goto IL_D3;
					}
					num = i;
				}
				IL_D3:;
			}
			return time;
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000BEC38 File Offset: 0x000BCE38
		private static int GetIndexOfNextTokenAfterSeconds(string time, int index, out bool containsSpace)
		{
			bool flag = false;
			containsSpace = false;
			while (index < time.Length)
			{
				char c = time[index];
				if (c <= 'H')
				{
					if (c != ' ')
					{
						if (c != '\'')
						{
							if (c == 'H')
							{
								goto IL_63;
							}
						}
						else
						{
							flag = !flag;
						}
					}
					else
					{
						containsSpace = true;
					}
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						if (c == 'h')
						{
							goto IL_63;
						}
					}
					else
					{
						index++;
						if (time[index] == ' ')
						{
							containsSpace = true;
						}
					}
				}
				else if (c == 'm' || c == 't')
				{
					goto IL_63;
				}
				IL_68:
				index++;
				continue;
				IL_63:
				if (!flag)
				{
					return index;
				}
				goto IL_68;
			}
			containsSpace = false;
			return index;
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x000BECC0 File Offset: 0x000BCEC0
		internal string[] SADURATION
		{
			[SecurityCritical]
			get
			{
				if (this.saDurationFormats == null)
				{
					string str = this.DoGetLocaleInfo(93U);
					this.saDurationFormats = new string[]
					{
						CultureData.ReescapeWin32String(str)
					};
				}
				return this.saDurationFormats;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000BECFF File Offset: 0x000BCEFF
		internal int IFIRSTDAYOFWEEK
		{
			get
			{
				if (this.iFirstDayOfWeek == -1 || this.UseUserOverride)
				{
					this.iFirstDayOfWeek = CultureData.ConvertFirstDayOfWeekMonToSun(this.DoGetLocaleInfoInt(4108U));
				}
				return this.iFirstDayOfWeek;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000BED2E File Offset: 0x000BCF2E
		internal int IFIRSTWEEKOFYEAR
		{
			get
			{
				if (this.iFirstWeekOfYear == -1 || this.UseUserOverride)
				{
					this.iFirstWeekOfYear = this.DoGetLocaleInfoInt(4109U);
				}
				return this.iFirstWeekOfYear;
			}
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000BED58 File Offset: 0x000BCF58
		internal string[] ShortDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saShortDates;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000BED66 File Offset: 0x000BCF66
		internal string[] LongDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saLongDates;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000BED74 File Offset: 0x000BCF74
		internal string[] YearMonths(int calendarId)
		{
			return this.GetCalendar(calendarId).saYearMonths;
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000BED82 File Offset: 0x000BCF82
		internal string[] DayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saDayNames;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000BED90 File Offset: 0x000BCF90
		internal string[] AbbreviatedDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevDayNames;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000BED9E File Offset: 0x000BCF9E
		internal string[] SuperShortDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saSuperShortDayNames;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000BEDAC File Offset: 0x000BCFAC
		internal string[] MonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthNames;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000BEDBA File Offset: 0x000BCFBA
		internal string[] GenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthGenitiveNames;
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000BEDC8 File Offset: 0x000BCFC8
		internal string[] AbbreviatedMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthNames;
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000BEDD6 File Offset: 0x000BCFD6
		internal string[] AbbreviatedGenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthGenitiveNames;
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000BEDE4 File Offset: 0x000BCFE4
		internal string[] LeapYearMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saLeapYearMonthNames;
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000BEDF2 File Offset: 0x000BCFF2
		internal string MonthDay(int calendarId)
		{
			return this.GetCalendar(calendarId).sMonthDay;
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000BEE00 File Offset: 0x000BD000
		internal int[] CalendarIds
		{
			get
			{
				if (this.waCalendars == null)
				{
					int[] array = new int[23];
					int num = CalendarData.nativeGetCalendars(this.sWindowsName, this.bUseOverrides, array);
					if (num == 0)
					{
						this.waCalendars = CultureData.Invariant.waCalendars;
					}
					else
					{
						if (this.sWindowsName == "zh-TW")
						{
							bool flag = false;
							for (int i = 0; i < num; i++)
							{
								if (array[i] == 4)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								num++;
								Array.Copy(array, 1, array, 2, 21);
								array[1] = 4;
							}
						}
						int[] destinationArray = new int[num];
						Array.Copy(array, destinationArray, num);
						this.waCalendars = destinationArray;
					}
				}
				return this.waCalendars;
			}
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000BEEB3 File Offset: 0x000BD0B3
		internal string CalendarName(int calendarId)
		{
			return this.GetCalendar(calendarId).sNativeName;
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000BEEC4 File Offset: 0x000BD0C4
		internal CalendarData GetCalendar(int calendarId)
		{
			int num = calendarId - 1;
			if (this.calendars == null)
			{
				this.calendars = new CalendarData[23];
			}
			CalendarData calendarData = this.calendars[num];
			if (calendarData == null || this.UseUserOverride)
			{
				calendarData = new CalendarData(this.sWindowsName, calendarId, this.UseUserOverride);
				if (CultureData.IsOsWin7OrPrior() && !this.IsSupplementalCustomCulture && !this.IsReplacementCulture)
				{
					calendarData.FixupWin7MonthDaySemicolonBug();
				}
				this.calendars[num] = calendarData;
			}
			return calendarData;
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000BEF38 File Offset: 0x000BD138
		internal int CurrentEra(int calendarId)
		{
			return this.GetCalendar(calendarId).iCurrentEra;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x000BEF46 File Offset: 0x000BD146
		internal bool IsRightToLeft
		{
			get
			{
				return this.IREADINGLAYOUT == 1;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000BEF51 File Offset: 0x000BD151
		private int IREADINGLAYOUT
		{
			get
			{
				if (this.iReadingLayout == -1)
				{
					this.iReadingLayout = this.DoGetLocaleInfoInt(112U);
				}
				return this.iReadingLayout;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000BEF70 File Offset: 0x000BD170
		internal string STEXTINFO
		{
			[SecuritySafeCritical]
			get
			{
				if (this.sTextInfo == null)
				{
					if (this.IsNeutralCulture || this.IsSupplementalCustomCulture)
					{
						string cultureName = this.DoGetLocaleInfo(123U);
						this.sTextInfo = CultureData.GetCultureData(cultureName, this.bUseOverrides).SNAME;
					}
					if (this.sTextInfo == null)
					{
						this.sTextInfo = this.SNAME;
					}
				}
				return this.sTextInfo;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000BEFCF File Offset: 0x000BD1CF
		internal string SCOMPAREINFO
		{
			[SecuritySafeCritical]
			get
			{
				if (this.sCompareInfo == null)
				{
					if (this.IsSupplementalCustomCulture)
					{
						this.sCompareInfo = this.DoGetLocaleInfo(123U);
					}
					if (this.sCompareInfo == null)
					{
						this.sCompareInfo = this.sWindowsName;
					}
				}
				return this.sCompareInfo;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x000BF009 File Offset: 0x000BD209
		internal bool IsSupplementalCustomCulture
		{
			get
			{
				return CultureData.IsCustomCultureId(this.ILANGUAGE);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x000BF016 File Offset: 0x000BD216
		private string SSCRIPTS
		{
			[SecuritySafeCritical]
			get
			{
				if (this.sScripts == null)
				{
					this.sScripts = this.DoGetLocaleInfo(108U);
				}
				return this.sScripts;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x000BF034 File Offset: 0x000BD234
		private string SOPENTYPELANGUAGETAG
		{
			[SecuritySafeCritical]
			get
			{
				return this.DoGetLocaleInfo(122U);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x000BF03E File Offset: 0x000BD23E
		private string FONTSIGNATURE
		{
			[SecuritySafeCritical]
			get
			{
				if (this.fontSignature == null)
				{
					this.fontSignature = this.DoGetLocaleInfo(88U);
				}
				return this.fontSignature;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000BF05C File Offset: 0x000BD25C
		private string SKEYBOARDSTOINSTALL
		{
			[SecuritySafeCritical]
			get
			{
				return this.DoGetLocaleInfo(94U);
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000BF066 File Offset: 0x000BD266
		internal int IDEFAULTANSICODEPAGE
		{
			get
			{
				if (this.iDefaultAnsiCodePage == -1)
				{
					this.iDefaultAnsiCodePage = this.DoGetLocaleInfoInt(4100U);
				}
				return this.iDefaultAnsiCodePage;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x000BF088 File Offset: 0x000BD288
		internal int IDEFAULTOEMCODEPAGE
		{
			get
			{
				if (this.iDefaultOemCodePage == -1)
				{
					this.iDefaultOemCodePage = this.DoGetLocaleInfoInt(11U);
				}
				return this.iDefaultOemCodePage;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000BF0A7 File Offset: 0x000BD2A7
		internal int IDEFAULTMACCODEPAGE
		{
			get
			{
				if (this.iDefaultMacCodePage == -1)
				{
					this.iDefaultMacCodePage = this.DoGetLocaleInfoInt(4113U);
				}
				return this.iDefaultMacCodePage;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000BF0C9 File Offset: 0x000BD2C9
		internal int IDEFAULTEBCDICCODEPAGE
		{
			get
			{
				if (this.iDefaultEbcdicCodePage == -1)
				{
					this.iDefaultEbcdicCodePage = this.DoGetLocaleInfoInt(4114U);
				}
				return this.iDefaultEbcdicCodePage;
			}
		}

		// Token: 0x06003199 RID: 12697
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LocaleNameToLCID(string localeName);

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000BF0EB File Offset: 0x000BD2EB
		internal int ILANGUAGE
		{
			get
			{
				if (this.iLanguage == 0)
				{
					this.iLanguage = CultureData.LocaleNameToLCID(this.sRealName);
				}
				return this.iLanguage;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000BF10C File Offset: 0x000BD30C
		internal bool IsWin32Installed
		{
			get
			{
				return this.bWin32Installed;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000BF114 File Offset: 0x000BD314
		internal bool IsFramework
		{
			get
			{
				return this.bFramework;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000BF11C File Offset: 0x000BD31C
		internal bool IsNeutralCulture
		{
			get
			{
				return this.bNeutral;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000BF124 File Offset: 0x000BD324
		internal bool IsInvariantCulture
		{
			get
			{
				return string.IsNullOrEmpty(this.SNAME);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000BF134 File Offset: 0x000BD334
		internal Calendar DefaultCalendar
		{
			get
			{
				int num = this.DoGetLocaleInfoInt(4105U);
				if (num == 0)
				{
					num = this.CalendarIds[0];
				}
				return CultureInfo.GetCalendarInstance(num);
			}
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000BF15F File Offset: 0x000BD35F
		internal string[] EraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saEraNames;
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000BF16D File Offset: 0x000BD36D
		internal string[] AbbrevEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEraNames;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000BF17B File Offset: 0x000BD37B
		internal string[] AbbreviatedEnglishEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEnglishEraNames;
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000BF18C File Offset: 0x000BD38C
		internal string TimeSeparator
		{
			[SecuritySafeCritical]
			get
			{
				if (this.sTimeSeparator == null || this.UseUserOverride)
				{
					string text = CultureData.ReescapeWin32String(this.DoGetLocaleInfo(4099U));
					if (string.IsNullOrEmpty(text))
					{
						text = this.LongTimes[0];
					}
					this.sTimeSeparator = CultureData.GetTimeSeparator(text);
				}
				return this.sTimeSeparator;
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000BF1DD File Offset: 0x000BD3DD
		internal string DateSeparator(int calendarId)
		{
			if (calendarId == 3 && !AppContextSwitches.EnforceLegacyJapaneseDateParsing)
			{
				return "/";
			}
			return CultureData.GetDateSeparator(this.ShortDates(calendarId)[0]);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000BF200 File Offset: 0x000BD400
		private static string UnescapeNlsString(string str, int start, int end)
		{
			StringBuilder stringBuilder = null;
			int num = start;
			while (num < str.Length && num <= end)
			{
				char c = str[num];
				if (c != '\'')
				{
					if (c != '\\')
					{
						if (stringBuilder != null)
						{
							stringBuilder.Append(str[num]);
						}
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str, start, num - start, str.Length);
						}
						num++;
						if (num < str.Length)
						{
							stringBuilder.Append(str[num]);
						}
					}
				}
				else if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str, start, num - start, str.Length);
				}
				num++;
			}
			if (stringBuilder == null)
			{
				return str.Substring(start, end - start + 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000BF2A8 File Offset: 0x000BD4A8
		internal static string ReescapeWin32String(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			bool flag = false;
			int i = 0;
			while (i < str.Length)
			{
				if (str[i] == '\'')
				{
					if (!flag)
					{
						flag = true;
						goto IL_91;
					}
					if (i + 1 >= str.Length || str[i + 1] != '\'')
					{
						flag = false;
						goto IL_91;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(str, 0, i, str.Length * 2);
					}
					stringBuilder.Append("\\'");
					i++;
				}
				else
				{
					if (str[i] != '\\')
					{
						goto IL_91;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(str, 0, i, str.Length * 2);
					}
					stringBuilder.Append("\\\\");
				}
				IL_A2:
				i++;
				continue;
				IL_91:
				if (stringBuilder != null)
				{
					stringBuilder.Append(str[i]);
					goto IL_A2;
				}
				goto IL_A2;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000BF374 File Offset: 0x000BD574
		internal static string[] ReescapeWin32Strings(string[] array)
		{
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = CultureData.ReescapeWin32String(array[i]);
				}
			}
			return array;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000BF39E File Offset: 0x000BD59E
		private static string GetTimeSeparator(string format)
		{
			return CultureData.GetSeparator(format, "Hhms");
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000BF3AB File Offset: 0x000BD5AB
		private static string GetDateSeparator(string format)
		{
			return CultureData.GetSeparator(format, "dyM");
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000BF3B8 File Offset: 0x000BD5B8
		private static string GetSeparator(string format, string timeParts)
		{
			int num = CultureData.IndexOfTimePart(format, 0, timeParts);
			if (num != -1)
			{
				char c = format[num];
				do
				{
					num++;
				}
				while (num < format.Length && format[num] == c);
				int num2 = num;
				if (num2 < format.Length)
				{
					int num3 = CultureData.IndexOfTimePart(format, num2, timeParts);
					if (num3 != -1)
					{
						return CultureData.UnescapeNlsString(format, num2, num3 - 1);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000BF41C File Offset: 0x000BD61C
		private static int IndexOfTimePart(string format, int startIndex, string timeParts)
		{
			bool flag = false;
			for (int i = startIndex; i < format.Length; i++)
			{
				if (!flag && timeParts.IndexOf(format[i]) != -1)
				{
					return i;
				}
				char c = format[i];
				if (c != '\'')
				{
					if (c == '\\' && i + 1 < format.Length)
					{
						i++;
						c = format[i];
						if (c != '\'' && c != '\\')
						{
							i--;
						}
					}
				}
				else
				{
					flag = !flag;
				}
			}
			return -1;
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000BF490 File Offset: 0x000BD690
		[SecurityCritical]
		private string DoGetLocaleInfo(uint lctype)
		{
			return this.DoGetLocaleInfo(this.sWindowsName, lctype);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000BF4A0 File Offset: 0x000BD6A0
		[SecurityCritical]
		private string DoGetLocaleInfo(string localeName, uint lctype)
		{
			if (!this.UseUserOverride)
			{
				lctype |= 2147483648U;
			}
			string text = CultureInfo.nativeGetLocaleInfoEx(localeName, lctype);
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000BF4D0 File Offset: 0x000BD6D0
		private int DoGetLocaleInfoInt(uint lctype)
		{
			if (!this.UseUserOverride)
			{
				lctype |= 2147483648U;
			}
			return CultureInfo.nativeGetLocaleInfoExInt(this.sWindowsName, lctype);
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000BF4FC File Offset: 0x000BD6FC
		private string[] DoEnumTimeFormats()
		{
			return CultureData.ReescapeWin32Strings(CultureData.nativeEnumTimeFormats(this.sWindowsName, 0U, this.UseUserOverride));
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000BF524 File Offset: 0x000BD724
		private string[] DoEnumShortTimeFormats()
		{
			return CultureData.ReescapeWin32Strings(CultureData.nativeEnumTimeFormats(this.sWindowsName, 2U, this.UseUserOverride));
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000BF54A File Offset: 0x000BD74A
		internal static bool IsCustomCultureId(int cultureId)
		{
			return cultureId == 3072 || cultureId == 4096;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000BF560 File Offset: 0x000BD760
		[SecurityCritical]
		internal void GetNFIValues(NumberFormatInfo nfi)
		{
			if (this.IsInvariantCulture)
			{
				nfi.positiveSign = this.sPositiveSign;
				nfi.negativeSign = this.sNegativeSign;
				nfi.nativeDigits = this.saNativeDigits;
				nfi.digitSubstitution = this.iDigitSubstitution;
				nfi.numberGroupSeparator = this.sThousandSeparator;
				nfi.numberDecimalSeparator = this.sDecimalSeparator;
				nfi.numberDecimalDigits = this.iDigits;
				nfi.numberNegativePattern = this.iNegativeNumber;
				nfi.currencySymbol = this.sCurrency;
				nfi.currencyGroupSeparator = this.sMonetaryThousand;
				nfi.currencyDecimalSeparator = this.sMonetaryDecimal;
				nfi.currencyDecimalDigits = this.iCurrencyDigits;
				nfi.currencyNegativePattern = this.iNegativeCurrency;
				nfi.currencyPositivePattern = this.iCurrency;
			}
			else
			{
				CultureData.nativeGetNumberFormatInfoValues(this.sWindowsName, nfi, this.UseUserOverride);
			}
			nfi.numberGroupSizes = this.WAGROUPING;
			nfi.currencyGroupSizes = this.WAMONGROUPING;
			nfi.percentNegativePattern = this.INEGATIVEPERCENT;
			nfi.percentPositivePattern = this.IPOSITIVEPERCENT;
			nfi.percentSymbol = this.SPERCENT;
			nfi.perMilleSymbol = this.SPERMILLE;
			nfi.negativeInfinitySymbol = this.SNEGINFINITY;
			nfi.positiveInfinitySymbol = this.SPOSINFINITY;
			nfi.nanSymbol = this.SNAN;
			nfi.percentDecimalDigits = nfi.numberDecimalDigits;
			nfi.percentDecimalSeparator = nfi.numberDecimalSeparator;
			nfi.percentGroupSizes = nfi.numberGroupSizes;
			nfi.percentGroupSeparator = nfi.numberGroupSeparator;
			if (nfi.positiveSign == null || nfi.positiveSign.Length == 0)
			{
				nfi.positiveSign = "+";
			}
			if (nfi.currencyDecimalSeparator == null || nfi.currencyDecimalSeparator.Length == 0)
			{
				nfi.currencyDecimalSeparator = nfi.numberDecimalSeparator;
			}
			if (932 == this.IDEFAULTANSICODEPAGE || 949 == this.IDEFAULTANSICODEPAGE)
			{
				nfi.ansiCurrencySymbol = "\\";
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000BF737 File Offset: 0x000BD937
		private static int ConvertFirstDayOfWeekMonToSun(int iTemp)
		{
			iTemp++;
			if (iTemp > 6)
			{
				iTemp = 0;
			}
			return iTemp;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000BF748 File Offset: 0x000BD948
		internal static string AnsiToLower(string testString)
		{
			StringBuilder stringBuilder = new StringBuilder(testString.Length);
			foreach (char c in testString)
			{
				stringBuilder.Append((c <= 'Z' && c >= 'A') ? (c - 'A' + 'a') : c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000BF79C File Offset: 0x000BD99C
		private static int[] ConvertWin32GroupString(string win32Str)
		{
			if (win32Str == null || win32Str.Length == 0)
			{
				return new int[]
				{
					3
				};
			}
			if (win32Str[0] == '0')
			{
				return new int[1];
			}
			int[] array;
			if (win32Str[win32Str.Length - 1] == '0')
			{
				array = new int[win32Str.Length / 2];
			}
			else
			{
				array = new int[win32Str.Length / 2 + 2];
				array[array.Length - 1] = 0;
			}
			int num = 0;
			int num2 = 0;
			while (num < win32Str.Length && num2 < array.Length)
			{
				if (win32Str[num] < '1' || win32Str[num] > '9')
				{
					return new int[]
					{
						3
					};
				}
				array[num2] = (int)(win32Str[num] - '0');
				num += 2;
				num2++;
			}
			return array;
		}

		// Token: 0x060031B6 RID: 12726
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool nativeInitCultureData(CultureData cultureData);

		// Token: 0x060031B7 RID: 12727
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool nativeGetNumberFormatInfoValues(string localeName, NumberFormatInfo nfi, bool useUserOverride);

		// Token: 0x060031B8 RID: 12728
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] nativeEnumTimeFormats(string localeName, uint dwFlags, bool useUserOverride);

		// Token: 0x060031B9 RID: 12729
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int nativeEnumCultureNames(int cultureTypes, ObjectHandleOnStack retStringArray);

		// Token: 0x040014E7 RID: 5351
		private const int undef = -1;

		// Token: 0x040014E8 RID: 5352
		private string sRealName;

		// Token: 0x040014E9 RID: 5353
		private string sWindowsName;

		// Token: 0x040014EA RID: 5354
		private string sName;

		// Token: 0x040014EB RID: 5355
		private string sParent;

		// Token: 0x040014EC RID: 5356
		private string sLocalizedDisplayName;

		// Token: 0x040014ED RID: 5357
		private string sEnglishDisplayName;

		// Token: 0x040014EE RID: 5358
		private string sNativeDisplayName;

		// Token: 0x040014EF RID: 5359
		private string sSpecificCulture;

		// Token: 0x040014F0 RID: 5360
		private string sISO639Language;

		// Token: 0x040014F1 RID: 5361
		private string sLocalizedLanguage;

		// Token: 0x040014F2 RID: 5362
		private string sEnglishLanguage;

		// Token: 0x040014F3 RID: 5363
		private string sNativeLanguage;

		// Token: 0x040014F4 RID: 5364
		private string sRegionName;

		// Token: 0x040014F5 RID: 5365
		private int iGeoId = -1;

		// Token: 0x040014F6 RID: 5366
		private string sLocalizedCountry;

		// Token: 0x040014F7 RID: 5367
		private string sEnglishCountry;

		// Token: 0x040014F8 RID: 5368
		private string sNativeCountry;

		// Token: 0x040014F9 RID: 5369
		private string sISO3166CountryName;

		// Token: 0x040014FA RID: 5370
		private string sPositiveSign;

		// Token: 0x040014FB RID: 5371
		private string sNegativeSign;

		// Token: 0x040014FC RID: 5372
		private string[] saNativeDigits;

		// Token: 0x040014FD RID: 5373
		private int iDigitSubstitution;

		// Token: 0x040014FE RID: 5374
		private int iLeadingZeros;

		// Token: 0x040014FF RID: 5375
		private int iDigits;

		// Token: 0x04001500 RID: 5376
		private int iNegativeNumber;

		// Token: 0x04001501 RID: 5377
		private int[] waGrouping;

		// Token: 0x04001502 RID: 5378
		private string sDecimalSeparator;

		// Token: 0x04001503 RID: 5379
		private string sThousandSeparator;

		// Token: 0x04001504 RID: 5380
		private string sNaN;

		// Token: 0x04001505 RID: 5381
		private string sPositiveInfinity;

		// Token: 0x04001506 RID: 5382
		private string sNegativeInfinity;

		// Token: 0x04001507 RID: 5383
		private int iNegativePercent = -1;

		// Token: 0x04001508 RID: 5384
		private int iPositivePercent = -1;

		// Token: 0x04001509 RID: 5385
		private string sPercent;

		// Token: 0x0400150A RID: 5386
		private string sPerMille;

		// Token: 0x0400150B RID: 5387
		private string sCurrency;

		// Token: 0x0400150C RID: 5388
		private string sIntlMonetarySymbol;

		// Token: 0x0400150D RID: 5389
		private string sEnglishCurrency;

		// Token: 0x0400150E RID: 5390
		private string sNativeCurrency;

		// Token: 0x0400150F RID: 5391
		private int iCurrencyDigits;

		// Token: 0x04001510 RID: 5392
		private int iCurrency;

		// Token: 0x04001511 RID: 5393
		private int iNegativeCurrency;

		// Token: 0x04001512 RID: 5394
		private int[] waMonetaryGrouping;

		// Token: 0x04001513 RID: 5395
		private string sMonetaryDecimal;

		// Token: 0x04001514 RID: 5396
		private string sMonetaryThousand;

		// Token: 0x04001515 RID: 5397
		private int iMeasure = -1;

		// Token: 0x04001516 RID: 5398
		private string sListSeparator;

		// Token: 0x04001517 RID: 5399
		private string sAM1159;

		// Token: 0x04001518 RID: 5400
		private string sPM2359;

		// Token: 0x04001519 RID: 5401
		private string sTimeSeparator;

		// Token: 0x0400151A RID: 5402
		private volatile string[] saLongTimes;

		// Token: 0x0400151B RID: 5403
		private volatile string[] saShortTimes;

		// Token: 0x0400151C RID: 5404
		private volatile string[] saDurationFormats;

		// Token: 0x0400151D RID: 5405
		private int iFirstDayOfWeek = -1;

		// Token: 0x0400151E RID: 5406
		private int iFirstWeekOfYear = -1;

		// Token: 0x0400151F RID: 5407
		private volatile int[] waCalendars;

		// Token: 0x04001520 RID: 5408
		private CalendarData[] calendars;

		// Token: 0x04001521 RID: 5409
		private int iReadingLayout = -1;

		// Token: 0x04001522 RID: 5410
		private string sTextInfo;

		// Token: 0x04001523 RID: 5411
		private string sCompareInfo;

		// Token: 0x04001524 RID: 5412
		private string sScripts;

		// Token: 0x04001525 RID: 5413
		private int iDefaultAnsiCodePage = -1;

		// Token: 0x04001526 RID: 5414
		private int iDefaultOemCodePage = -1;

		// Token: 0x04001527 RID: 5415
		private int iDefaultMacCodePage = -1;

		// Token: 0x04001528 RID: 5416
		private int iDefaultEbcdicCodePage = -1;

		// Token: 0x04001529 RID: 5417
		private int iLanguage;

		// Token: 0x0400152A RID: 5418
		private string sAbbrevLang;

		// Token: 0x0400152B RID: 5419
		private string sAbbrevCountry;

		// Token: 0x0400152C RID: 5420
		private string sISO639Language2;

		// Token: 0x0400152D RID: 5421
		private string sISO3166CountryName2;

		// Token: 0x0400152E RID: 5422
		private int iInputLanguageHandle = -1;

		// Token: 0x0400152F RID: 5423
		private string sConsoleFallbackName;

		// Token: 0x04001530 RID: 5424
		private string sKeyboardsToInstall;

		// Token: 0x04001531 RID: 5425
		private string fontSignature;

		// Token: 0x04001532 RID: 5426
		private bool bUseOverrides;

		// Token: 0x04001533 RID: 5427
		private bool bNeutral;

		// Token: 0x04001534 RID: 5428
		private bool bWin32Installed;

		// Token: 0x04001535 RID: 5429
		private bool bFramework;

		// Token: 0x04001536 RID: 5430
		private static volatile Dictionary<string, string> s_RegionNames;

		// Token: 0x04001537 RID: 5431
		private static volatile CultureData s_Invariant;

		// Token: 0x04001538 RID: 5432
		internal static volatile ResourceSet MscorlibResourceSet;

		// Token: 0x04001539 RID: 5433
		private static volatile Dictionary<string, CultureData> s_cachedCultures;

		// Token: 0x0400153A RID: 5434
		private static readonly Version s_win7Version = new Version(6, 1);

		// Token: 0x0400153B RID: 5435
		private static string s_RegionKey = "System\\CurrentControlSet\\Control\\Nls\\RegionMapping";

		// Token: 0x0400153C RID: 5436
		private static volatile Dictionary<string, CultureData> s_cachedRegions;

		// Token: 0x0400153D RID: 5437
		internal static volatile CultureInfo[] specificCultures;

		// Token: 0x0400153E RID: 5438
		internal static volatile string[] s_replacementCultureNames;

		// Token: 0x0400153F RID: 5439
		private const uint LOCALE_NOUSEROVERRIDE = 2147483648U;

		// Token: 0x04001540 RID: 5440
		private const uint LOCALE_RETURN_NUMBER = 536870912U;

		// Token: 0x04001541 RID: 5441
		private const uint LOCALE_RETURN_GENITIVE_NAMES = 268435456U;

		// Token: 0x04001542 RID: 5442
		private const uint LOCALE_SLOCALIZEDDISPLAYNAME = 2U;

		// Token: 0x04001543 RID: 5443
		private const uint LOCALE_SENGLISHDISPLAYNAME = 114U;

		// Token: 0x04001544 RID: 5444
		private const uint LOCALE_SNATIVEDISPLAYNAME = 115U;

		// Token: 0x04001545 RID: 5445
		private const uint LOCALE_SLOCALIZEDLANGUAGENAME = 111U;

		// Token: 0x04001546 RID: 5446
		private const uint LOCALE_SENGLISHLANGUAGENAME = 4097U;

		// Token: 0x04001547 RID: 5447
		private const uint LOCALE_SNATIVELANGUAGENAME = 4U;

		// Token: 0x04001548 RID: 5448
		private const uint LOCALE_SLOCALIZEDCOUNTRYNAME = 6U;

		// Token: 0x04001549 RID: 5449
		private const uint LOCALE_SENGLISHCOUNTRYNAME = 4098U;

		// Token: 0x0400154A RID: 5450
		private const uint LOCALE_SNATIVECOUNTRYNAME = 8U;

		// Token: 0x0400154B RID: 5451
		private const uint LOCALE_SABBREVLANGNAME = 3U;

		// Token: 0x0400154C RID: 5452
		private const uint LOCALE_ICOUNTRY = 5U;

		// Token: 0x0400154D RID: 5453
		private const uint LOCALE_SABBREVCTRYNAME = 7U;

		// Token: 0x0400154E RID: 5454
		private const uint LOCALE_IGEOID = 91U;

		// Token: 0x0400154F RID: 5455
		private const uint LOCALE_IDEFAULTLANGUAGE = 9U;

		// Token: 0x04001550 RID: 5456
		private const uint LOCALE_IDEFAULTCOUNTRY = 10U;

		// Token: 0x04001551 RID: 5457
		private const uint LOCALE_IDEFAULTCODEPAGE = 11U;

		// Token: 0x04001552 RID: 5458
		private const uint LOCALE_IDEFAULTANSICODEPAGE = 4100U;

		// Token: 0x04001553 RID: 5459
		private const uint LOCALE_IDEFAULTMACCODEPAGE = 4113U;

		// Token: 0x04001554 RID: 5460
		private const uint LOCALE_SLIST = 12U;

		// Token: 0x04001555 RID: 5461
		private const uint LOCALE_IMEASURE = 13U;

		// Token: 0x04001556 RID: 5462
		private const uint LOCALE_SDECIMAL = 14U;

		// Token: 0x04001557 RID: 5463
		private const uint LOCALE_STHOUSAND = 15U;

		// Token: 0x04001558 RID: 5464
		private const uint LOCALE_SGROUPING = 16U;

		// Token: 0x04001559 RID: 5465
		private const uint LOCALE_IDIGITS = 17U;

		// Token: 0x0400155A RID: 5466
		private const uint LOCALE_ILZERO = 18U;

		// Token: 0x0400155B RID: 5467
		private const uint LOCALE_INEGNUMBER = 4112U;

		// Token: 0x0400155C RID: 5468
		private const uint LOCALE_SNATIVEDIGITS = 19U;

		// Token: 0x0400155D RID: 5469
		private const uint LOCALE_SCURRENCY = 20U;

		// Token: 0x0400155E RID: 5470
		private const uint LOCALE_SINTLSYMBOL = 21U;

		// Token: 0x0400155F RID: 5471
		private const uint LOCALE_SMONDECIMALSEP = 22U;

		// Token: 0x04001560 RID: 5472
		private const uint LOCALE_SMONTHOUSANDSEP = 23U;

		// Token: 0x04001561 RID: 5473
		private const uint LOCALE_SMONGROUPING = 24U;

		// Token: 0x04001562 RID: 5474
		private const uint LOCALE_ICURRDIGITS = 25U;

		// Token: 0x04001563 RID: 5475
		private const uint LOCALE_IINTLCURRDIGITS = 26U;

		// Token: 0x04001564 RID: 5476
		private const uint LOCALE_ICURRENCY = 27U;

		// Token: 0x04001565 RID: 5477
		private const uint LOCALE_INEGCURR = 28U;

		// Token: 0x04001566 RID: 5478
		private const uint LOCALE_SDATE = 29U;

		// Token: 0x04001567 RID: 5479
		private const uint LOCALE_STIME = 30U;

		// Token: 0x04001568 RID: 5480
		private const uint LOCALE_SSHORTDATE = 31U;

		// Token: 0x04001569 RID: 5481
		private const uint LOCALE_SLONGDATE = 32U;

		// Token: 0x0400156A RID: 5482
		private const uint LOCALE_STIMEFORMAT = 4099U;

		// Token: 0x0400156B RID: 5483
		private const uint LOCALE_IDATE = 33U;

		// Token: 0x0400156C RID: 5484
		private const uint LOCALE_ILDATE = 34U;

		// Token: 0x0400156D RID: 5485
		private const uint LOCALE_ITIME = 35U;

		// Token: 0x0400156E RID: 5486
		private const uint LOCALE_ITIMEMARKPOSN = 4101U;

		// Token: 0x0400156F RID: 5487
		private const uint LOCALE_ICENTURY = 36U;

		// Token: 0x04001570 RID: 5488
		private const uint LOCALE_ITLZERO = 37U;

		// Token: 0x04001571 RID: 5489
		private const uint LOCALE_IDAYLZERO = 38U;

		// Token: 0x04001572 RID: 5490
		private const uint LOCALE_IMONLZERO = 39U;

		// Token: 0x04001573 RID: 5491
		private const uint LOCALE_S1159 = 40U;

		// Token: 0x04001574 RID: 5492
		private const uint LOCALE_S2359 = 41U;

		// Token: 0x04001575 RID: 5493
		private const uint LOCALE_ICALENDARTYPE = 4105U;

		// Token: 0x04001576 RID: 5494
		private const uint LOCALE_IOPTIONALCALENDAR = 4107U;

		// Token: 0x04001577 RID: 5495
		private const uint LOCALE_IFIRSTDAYOFWEEK = 4108U;

		// Token: 0x04001578 RID: 5496
		private const uint LOCALE_IFIRSTWEEKOFYEAR = 4109U;

		// Token: 0x04001579 RID: 5497
		private const uint LOCALE_SDAYNAME1 = 42U;

		// Token: 0x0400157A RID: 5498
		private const uint LOCALE_SDAYNAME2 = 43U;

		// Token: 0x0400157B RID: 5499
		private const uint LOCALE_SDAYNAME3 = 44U;

		// Token: 0x0400157C RID: 5500
		private const uint LOCALE_SDAYNAME4 = 45U;

		// Token: 0x0400157D RID: 5501
		private const uint LOCALE_SDAYNAME5 = 46U;

		// Token: 0x0400157E RID: 5502
		private const uint LOCALE_SDAYNAME6 = 47U;

		// Token: 0x0400157F RID: 5503
		private const uint LOCALE_SDAYNAME7 = 48U;

		// Token: 0x04001580 RID: 5504
		private const uint LOCALE_SABBREVDAYNAME1 = 49U;

		// Token: 0x04001581 RID: 5505
		private const uint LOCALE_SABBREVDAYNAME2 = 50U;

		// Token: 0x04001582 RID: 5506
		private const uint LOCALE_SABBREVDAYNAME3 = 51U;

		// Token: 0x04001583 RID: 5507
		private const uint LOCALE_SABBREVDAYNAME4 = 52U;

		// Token: 0x04001584 RID: 5508
		private const uint LOCALE_SABBREVDAYNAME5 = 53U;

		// Token: 0x04001585 RID: 5509
		private const uint LOCALE_SABBREVDAYNAME6 = 54U;

		// Token: 0x04001586 RID: 5510
		private const uint LOCALE_SABBREVDAYNAME7 = 55U;

		// Token: 0x04001587 RID: 5511
		private const uint LOCALE_SMONTHNAME1 = 56U;

		// Token: 0x04001588 RID: 5512
		private const uint LOCALE_SMONTHNAME2 = 57U;

		// Token: 0x04001589 RID: 5513
		private const uint LOCALE_SMONTHNAME3 = 58U;

		// Token: 0x0400158A RID: 5514
		private const uint LOCALE_SMONTHNAME4 = 59U;

		// Token: 0x0400158B RID: 5515
		private const uint LOCALE_SMONTHNAME5 = 60U;

		// Token: 0x0400158C RID: 5516
		private const uint LOCALE_SMONTHNAME6 = 61U;

		// Token: 0x0400158D RID: 5517
		private const uint LOCALE_SMONTHNAME7 = 62U;

		// Token: 0x0400158E RID: 5518
		private const uint LOCALE_SMONTHNAME8 = 63U;

		// Token: 0x0400158F RID: 5519
		private const uint LOCALE_SMONTHNAME9 = 64U;

		// Token: 0x04001590 RID: 5520
		private const uint LOCALE_SMONTHNAME10 = 65U;

		// Token: 0x04001591 RID: 5521
		private const uint LOCALE_SMONTHNAME11 = 66U;

		// Token: 0x04001592 RID: 5522
		private const uint LOCALE_SMONTHNAME12 = 67U;

		// Token: 0x04001593 RID: 5523
		private const uint LOCALE_SMONTHNAME13 = 4110U;

		// Token: 0x04001594 RID: 5524
		private const uint LOCALE_SABBREVMONTHNAME1 = 68U;

		// Token: 0x04001595 RID: 5525
		private const uint LOCALE_SABBREVMONTHNAME2 = 69U;

		// Token: 0x04001596 RID: 5526
		private const uint LOCALE_SABBREVMONTHNAME3 = 70U;

		// Token: 0x04001597 RID: 5527
		private const uint LOCALE_SABBREVMONTHNAME4 = 71U;

		// Token: 0x04001598 RID: 5528
		private const uint LOCALE_SABBREVMONTHNAME5 = 72U;

		// Token: 0x04001599 RID: 5529
		private const uint LOCALE_SABBREVMONTHNAME6 = 73U;

		// Token: 0x0400159A RID: 5530
		private const uint LOCALE_SABBREVMONTHNAME7 = 74U;

		// Token: 0x0400159B RID: 5531
		private const uint LOCALE_SABBREVMONTHNAME8 = 75U;

		// Token: 0x0400159C RID: 5532
		private const uint LOCALE_SABBREVMONTHNAME9 = 76U;

		// Token: 0x0400159D RID: 5533
		private const uint LOCALE_SABBREVMONTHNAME10 = 77U;

		// Token: 0x0400159E RID: 5534
		private const uint LOCALE_SABBREVMONTHNAME11 = 78U;

		// Token: 0x0400159F RID: 5535
		private const uint LOCALE_SABBREVMONTHNAME12 = 79U;

		// Token: 0x040015A0 RID: 5536
		private const uint LOCALE_SABBREVMONTHNAME13 = 4111U;

		// Token: 0x040015A1 RID: 5537
		private const uint LOCALE_SPOSITIVESIGN = 80U;

		// Token: 0x040015A2 RID: 5538
		private const uint LOCALE_SNEGATIVESIGN = 81U;

		// Token: 0x040015A3 RID: 5539
		private const uint LOCALE_IPOSSIGNPOSN = 82U;

		// Token: 0x040015A4 RID: 5540
		private const uint LOCALE_INEGSIGNPOSN = 83U;

		// Token: 0x040015A5 RID: 5541
		private const uint LOCALE_IPOSSYMPRECEDES = 84U;

		// Token: 0x040015A6 RID: 5542
		private const uint LOCALE_IPOSSEPBYSPACE = 85U;

		// Token: 0x040015A7 RID: 5543
		private const uint LOCALE_INEGSYMPRECEDES = 86U;

		// Token: 0x040015A8 RID: 5544
		private const uint LOCALE_INEGSEPBYSPACE = 87U;

		// Token: 0x040015A9 RID: 5545
		private const uint LOCALE_FONTSIGNATURE = 88U;

		// Token: 0x040015AA RID: 5546
		private const uint LOCALE_SISO639LANGNAME = 89U;

		// Token: 0x040015AB RID: 5547
		private const uint LOCALE_SISO3166CTRYNAME = 90U;

		// Token: 0x040015AC RID: 5548
		private const uint LOCALE_IDEFAULTEBCDICCODEPAGE = 4114U;

		// Token: 0x040015AD RID: 5549
		private const uint LOCALE_IPAPERSIZE = 4106U;

		// Token: 0x040015AE RID: 5550
		private const uint LOCALE_SENGCURRNAME = 4103U;

		// Token: 0x040015AF RID: 5551
		private const uint LOCALE_SNATIVECURRNAME = 4104U;

		// Token: 0x040015B0 RID: 5552
		private const uint LOCALE_SYEARMONTH = 4102U;

		// Token: 0x040015B1 RID: 5553
		private const uint LOCALE_SSORTNAME = 4115U;

		// Token: 0x040015B2 RID: 5554
		private const uint LOCALE_IDIGITSUBSTITUTION = 4116U;

		// Token: 0x040015B3 RID: 5555
		private const uint LOCALE_SNAME = 92U;

		// Token: 0x040015B4 RID: 5556
		private const uint LOCALE_SDURATION = 93U;

		// Token: 0x040015B5 RID: 5557
		private const uint LOCALE_SKEYBOARDSTOINSTALL = 94U;

		// Token: 0x040015B6 RID: 5558
		private const uint LOCALE_SSHORTESTDAYNAME1 = 96U;

		// Token: 0x040015B7 RID: 5559
		private const uint LOCALE_SSHORTESTDAYNAME2 = 97U;

		// Token: 0x040015B8 RID: 5560
		private const uint LOCALE_SSHORTESTDAYNAME3 = 98U;

		// Token: 0x040015B9 RID: 5561
		private const uint LOCALE_SSHORTESTDAYNAME4 = 99U;

		// Token: 0x040015BA RID: 5562
		private const uint LOCALE_SSHORTESTDAYNAME5 = 100U;

		// Token: 0x040015BB RID: 5563
		private const uint LOCALE_SSHORTESTDAYNAME6 = 101U;

		// Token: 0x040015BC RID: 5564
		private const uint LOCALE_SSHORTESTDAYNAME7 = 102U;

		// Token: 0x040015BD RID: 5565
		private const uint LOCALE_SISO639LANGNAME2 = 103U;

		// Token: 0x040015BE RID: 5566
		private const uint LOCALE_SISO3166CTRYNAME2 = 104U;

		// Token: 0x040015BF RID: 5567
		private const uint LOCALE_SNAN = 105U;

		// Token: 0x040015C0 RID: 5568
		private const uint LOCALE_SPOSINFINITY = 106U;

		// Token: 0x040015C1 RID: 5569
		private const uint LOCALE_SNEGINFINITY = 107U;

		// Token: 0x040015C2 RID: 5570
		private const uint LOCALE_SSCRIPTS = 108U;

		// Token: 0x040015C3 RID: 5571
		private const uint LOCALE_SPARENT = 109U;

		// Token: 0x040015C4 RID: 5572
		private const uint LOCALE_SCONSOLEFALLBACKNAME = 110U;

		// Token: 0x040015C5 RID: 5573
		private const uint LOCALE_IREADINGLAYOUT = 112U;

		// Token: 0x040015C6 RID: 5574
		private const uint LOCALE_INEUTRAL = 113U;

		// Token: 0x040015C7 RID: 5575
		private const uint LOCALE_INEGATIVEPERCENT = 116U;

		// Token: 0x040015C8 RID: 5576
		private const uint LOCALE_IPOSITIVEPERCENT = 117U;

		// Token: 0x040015C9 RID: 5577
		private const uint LOCALE_SPERCENT = 118U;

		// Token: 0x040015CA RID: 5578
		private const uint LOCALE_SPERMILLE = 119U;

		// Token: 0x040015CB RID: 5579
		private const uint LOCALE_SMONTHDAY = 120U;

		// Token: 0x040015CC RID: 5580
		private const uint LOCALE_SSHORTTIME = 121U;

		// Token: 0x040015CD RID: 5581
		private const uint LOCALE_SOPENTYPELANGUAGETAG = 122U;

		// Token: 0x040015CE RID: 5582
		private const uint LOCALE_SSORTLOCALE = 123U;

		// Token: 0x040015CF RID: 5583
		internal const uint TIME_NOSECONDS = 2U;
	}
}
