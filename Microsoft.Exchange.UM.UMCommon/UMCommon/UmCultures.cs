using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000172 RID: 370
	internal class UmCultures
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x0002AFDE File Offset: 0x000291DE
		private UmCultures()
		{
			this.promptCultures = UmCultures.BuildSupportedPromptCultures();
			this.clientCultures = UmCultures.BuildSupportedClientCultures();
			this.disambiguousLanguageFamilies = this.BuildDisambiguousLanguageFamilies();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002B008 File Offset: 0x00029208
		internal static int IndexOfIETFLanguage(List<CultureInfo> list, CultureInfo key)
		{
			if (list == null || key == null)
			{
				return -1;
			}
			string ietfLanguageTag = key.IetfLanguageTag;
			for (int i = 0; i < list.Count; i++)
			{
				string ietfLanguageTag2 = list[i].IetfLanguageTag;
				if (ietfLanguageTag.Equals(ietfLanguageTag2, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002B050 File Offset: 0x00029250
		internal static int IndexOfParentIETFLanguage(List<CultureInfo> list, CultureInfo key)
		{
			if (list == null || key == null || key.Parent == null)
			{
				return -1;
			}
			string ietfLanguageTag = key.Parent.IetfLanguageTag;
			for (int i = 0; i < list.Count; i++)
			{
				string ietfLanguageTag2 = list[i].IetfLanguageTag;
				if (ietfLanguageTag.Equals(ietfLanguageTag2, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002B0A4 File Offset: 0x000292A4
		internal static int IndexOfThreeLetterISOLanguage(List<CultureInfo> list, CultureInfo key)
		{
			if (list == null || key == null)
			{
				return -1;
			}
			string threeLetterISOLanguageName = key.ThreeLetterISOLanguageName;
			for (int i = 0; i < list.Count; i++)
			{
				string threeLetterISOLanguageName2 = list[i].ThreeLetterISOLanguageName;
				if (threeLetterISOLanguageName.Equals(threeLetterISOLanguageName2, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002B0EC File Offset: 0x000292EC
		internal static int IndexOfThreeLetterWindowsLanguageName(List<CultureInfo> list, CultureInfo key)
		{
			if (list == null || key == null)
			{
				return -1;
			}
			string threeLetterWindowsLanguageName = key.ThreeLetterWindowsLanguageName;
			for (int i = 0; i < list.Count; i++)
			{
				string threeLetterWindowsLanguageName2 = list[i].ThreeLetterWindowsLanguageName;
				if (threeLetterWindowsLanguageName.Equals(threeLetterWindowsLanguageName2, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002B134 File Offset: 0x00029334
		internal static int IndexOfTwoLetterISOLanguage(List<CultureInfo> list, CultureInfo key)
		{
			if (list == null || key == null)
			{
				return -1;
			}
			string twoLetterISOLanguageName = key.TwoLetterISOLanguageName;
			for (int i = 0; i < list.Count; i++)
			{
				string twoLetterISOLanguageName2 = list[i].TwoLetterISOLanguageName;
				if (twoLetterISOLanguageName.Equals(twoLetterISOLanguageName2, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002B17C File Offset: 0x0002937C
		internal static int IndexOfFallbackLanguage(List<CultureInfo> list, CultureInfo key)
		{
			int result = -1;
			if (list != null && key != null)
			{
				string twoLetterISOLanguageName = key.TwoLetterISOLanguageName;
				CultureInfo item = null;
				if (UmCultures.TryCreateFallbackCulture(twoLetterISOLanguageName, out item))
				{
					result = list.IndexOf(item);
				}
			}
			return result;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002B1B0 File Offset: 0x000293B0
		internal static bool TryCreateFallbackCulture(string keyIso, out CultureInfo fallback)
		{
			fallback = null;
			try
			{
				fallback = CultureInfo.CreateSpecificCulture(keyIso);
			}
			catch (ArgumentException)
			{
			}
			return null != fallback;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002B1E8 File Offset: 0x000293E8
		internal static void InvalidatePromptCulture(CultureInfo culture)
		{
			int num = UmCultures.GetSingleton().promptCultures.IndexOf(culture);
			if (num >= 0)
			{
				UmCultures.GetSingleton().promptCultures.RemoveAt(num);
			}
			if (UmCultures.SortedSupportedPromptCulturesLoader.SortedPromptCultures.ContainsKey(culture))
			{
				UmCultures.SortedSupportedPromptCulturesLoader.SortedPromptCultures.Remove(culture);
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002B233 File Offset: 0x00029433
		internal static bool IsPromptCultureAvailable(CultureInfo culture)
		{
			return UmCultures.GetSingleton().promptCultures.IndexOf(culture) >= 0;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002B24B File Offset: 0x0002944B
		internal static List<CultureInfo> GetSupportedPromptCultures()
		{
			return UmCultures.GetSingleton().promptCultures;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002B257 File Offset: 0x00029457
		internal static List<CultureInfo> GetSortedSupportedPromptCultures(CultureInfo key)
		{
			return UmCultures.SortedSupportedPromptCulturesLoader.SortedPromptCultures[key];
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002B264 File Offset: 0x00029464
		internal static List<CultureInfo> GetSupportedClientCultures()
		{
			return UmCultures.GetSingleton().clientCultures;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002B270 File Offset: 0x00029470
		internal static CultureInfo GetBestSupportedPromptCulture(CultureInfo language)
		{
			if (language == null)
			{
				throw new ArgumentException("The proposed language is null");
			}
			return UmCultures.GetBestSupportedCulture(UmCultures.GetSupportedPromptCultures(), language);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002B28C File Offset: 0x0002948C
		internal static CultureInfo GetPreferredClientCulture(CultureInfo[] preferredCultures)
		{
			if (preferredCultures.Length > 0)
			{
				List<CultureInfo> list = UmCultures.GetSingleton().clientCultures;
				foreach (CultureInfo cultureInfo in preferredCultures)
				{
					if (list.Contains(cultureInfo))
					{
						return cultureInfo;
					}
				}
				if (Utils.RunningInTestMode)
				{
					return preferredCultures[0];
				}
			}
			return null;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002B2E0 File Offset: 0x000294E0
		internal static CultureInfo GetBestSupportedCulture(List<CultureInfo> cultures, CultureInfo language)
		{
			if (cultures == null || language == null)
			{
				return null;
			}
			if (0 <= cultures.IndexOf(language))
			{
				return language;
			}
			int index;
			if (-1 != (index = UmCultures.IndexOfIETFLanguage(cultures, language)))
			{
				return cultures[index];
			}
			if (-1 != (index = UmCultures.IndexOfFallbackLanguage(cultures, language)))
			{
				return cultures[index];
			}
			if (-1 != (index = UmCultures.IndexOfThreeLetterWindowsLanguageName(cultures, language)))
			{
				return cultures[index];
			}
			if (-1 != (index = UmCultures.IndexOfThreeLetterISOLanguage(cultures, language)))
			{
				return cultures[index];
			}
			if (-1 != (index = UmCultures.IndexOfTwoLetterISOLanguage(cultures, language)))
			{
				return cultures[index];
			}
			if (-1 != (index = UmCultures.IndexOfParentIETFLanguage(cultures, language)))
			{
				return cultures[index];
			}
			return null;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002B37C File Offset: 0x0002957C
		internal static CultureInfo GetDisambiguousLanguageFamily(CultureInfo language)
		{
			return UmCultures.GetSingleton().disambiguousLanguageFamilies[language];
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002B38E File Offset: 0x0002958E
		internal static int GetLanguagePromptLCID(CultureInfo lang)
		{
			if (lang.Name == "es-MX")
			{
				return 58378;
			}
			return lang.LCID;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002B3AE File Offset: 0x000295AE
		internal static CultureInfo GetGrxmlCulture(CultureInfo lang)
		{
			return lang;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002B3B1 File Offset: 0x000295B1
		private static UmCultures GetSingleton()
		{
			if (UmCultures.singleton == null)
			{
				UmCultures.singleton = new UmCultures();
			}
			return UmCultures.singleton;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002B3CC File Offset: 0x000295CC
		private static Dictionary<CultureInfo, List<CultureInfo>> BuildSortedSupportedPromptCultures()
		{
			ResourceManager resourceManager = new ResourceManager("Microsoft.Exchange.UM.Prompts.Prompts.Strings", Assembly.Load("Microsoft.Exchange.UM.Prompts"));
			Dictionary<CultureInfo, List<CultureInfo>> dictionary = new Dictionary<CultureInfo, List<CultureInfo>>();
			foreach (CultureInfo cultureInfo in UmCultures.GetSupportedPromptCultures())
			{
				SortedDictionary<string, CultureInfo> sortedDictionary = new SortedDictionary<string, CultureInfo>(StringComparer.Create(cultureInfo, true));
				foreach (CultureInfo cultureInfo2 in UmCultures.GetSupportedPromptCultures())
				{
					string @string = resourceManager.GetString(string.Format(CultureInfo.InvariantCulture, "Language-{0}", new object[]
					{
						UmCultures.GetLanguagePromptLCID(cultureInfo2)
					}), cultureInfo);
					sortedDictionary.Add(@string, cultureInfo2);
				}
				dictionary.Add(cultureInfo, new List<CultureInfo>());
				foreach (CultureInfo item in sortedDictionary.Values)
				{
					dictionary[cultureInfo].Add(item);
				}
			}
			return dictionary;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002B518 File Offset: 0x00029718
		private static List<CultureInfo> BuildSupportedPromptCultures()
		{
			return new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.UnifiedMessaging));
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002B525 File Offset: 0x00029725
		private static List<CultureInfo> BuildSupportedClientCultures()
		{
			return new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002B534 File Offset: 0x00029734
		private Dictionary<CultureInfo, CultureInfo> BuildDisambiguousLanguageFamilies()
		{
			Dictionary<CultureInfo, CultureInfo> dictionary = new Dictionary<CultureInfo, CultureInfo>();
			Dictionary<string, CultureInfo> dictionary2 = new Dictionary<string, CultureInfo>();
			foreach (CultureInfo cultureInfo in this.promptCultures)
			{
				if (!dictionary2.ContainsKey(cultureInfo.TwoLetterISOLanguageName))
				{
					dictionary.Add(cultureInfo, cultureInfo.Parent);
					dictionary2.Add(cultureInfo.TwoLetterISOLanguageName, cultureInfo);
				}
				else
				{
					dictionary.Add(cultureInfo, cultureInfo);
					CultureInfo cultureInfo2 = dictionary2[cultureInfo.TwoLetterISOLanguageName];
					dictionary[cultureInfo2] = cultureInfo2;
				}
			}
			return dictionary;
		}

		// Token: 0x04000649 RID: 1609
		private static UmCultures singleton;

		// Token: 0x0400064A RID: 1610
		private List<CultureInfo> promptCultures;

		// Token: 0x0400064B RID: 1611
		private List<CultureInfo> clientCultures;

		// Token: 0x0400064C RID: 1612
		private Dictionary<CultureInfo, CultureInfo> disambiguousLanguageFamilies;

		// Token: 0x02000173 RID: 371
		private static class SortedSupportedPromptCulturesLoader
		{
			// Token: 0x170002C5 RID: 709
			// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002B5D8 File Offset: 0x000297D8
			internal static Dictionary<CultureInfo, List<CultureInfo>> SortedPromptCultures
			{
				get
				{
					return UmCultures.SortedSupportedPromptCulturesLoader.sortedPromptCultures;
				}
			}

			// Token: 0x0400064D RID: 1613
			private static Dictionary<CultureInfo, List<CultureInfo>> sortedPromptCultures = UmCultures.BuildSortedSupportedPromptCultures();
		}
	}
}
