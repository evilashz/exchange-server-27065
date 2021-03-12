using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000689 RID: 1673
	internal static class ClientCultures
	{
		// Token: 0x06001E55 RID: 7765 RVA: 0x000381AC File Offset: 0x000363AC
		static ClientCultures()
		{
			CultureInfo[] installedLanguagePackCultures = LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.Client);
			ClientCultures.dsnlocalizedLanguages = new Dictionary<int, bool>(installedLanguagePackCultures.Length);
			foreach (CultureInfo cultureInfo in installedLanguagePackCultures)
			{
				ClientCultures.dsnlocalizedLanguages[cultureInfo.LCID] = true;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000381F8 File Offset: 0x000363F8
		public static List<CultureInfo> SupportedCultureInfos
		{
			get
			{
				if (ClientCultures.supportedCultureInfos == null)
				{
					ClientCultures.supportedCultureInfos = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));
				}
				return ClientCultures.supportedCultureInfos;
			}
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00038218 File Offset: 0x00036418
		internal static CultureInfo GetPreferredCultureInfo(IEnumerable<CultureInfo> cultures)
		{
			foreach (CultureInfo cultureInfo in cultures)
			{
				if (ClientCultures.SupportedCultureInfos.Contains(cultureInfo))
				{
					return cultureInfo;
				}
			}
			return null;
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00038270 File Offset: 0x00036470
		public static bool IsSupportedCulture(CultureInfo culture)
		{
			return ClientCultures.SupportedCultureInfos.Contains(culture);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0003827D File Offset: 0x0003647D
		public static bool IsSupportedCulture(int lcid)
		{
			return lcid > 0 && ClientCultures.IsSupportedCulture(CultureInfo.GetCultureInfo(lcid));
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00038290 File Offset: 0x00036490
		public static CultureInfo[] GetAllSupportedDsnLanguages()
		{
			CultureInfo[] array = new CultureInfo[ClientCultures.dsnlocalizedLanguages.Count];
			int num = 0;
			foreach (int culture in ClientCultures.dsnlocalizedLanguages.Keys)
			{
				array[num++] = CultureInfo.GetCultureInfo(culture);
			}
			return array;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00038300 File Offset: 0x00036500
		public static bool IsCultureSupportedForDsn(CultureInfo culture)
		{
			CultureInfo parent = culture.Parent;
			return ClientCultures.dsnlocalizedLanguages.ContainsKey(culture.LCID) || (parent.IsNeutralCulture && ClientCultures.dsnlocalizedLanguages.ContainsKey(parent.LCID));
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00038342 File Offset: 0x00036542
		public static bool IsCultureSupportedForDsnCustomization(CultureInfo culture)
		{
			return LanguagePackInfo.expectedCultureLcids.Contains(culture.LCID);
		}

		// Token: 0x04001E3E RID: 7742
		public const string DefaultClientLanguage = "en-US";

		// Token: 0x04001E3F RID: 7743
		private static Dictionary<int, bool> dsnlocalizedLanguages;

		// Token: 0x04001E40 RID: 7744
		private static List<CultureInfo> supportedCultureInfos = null;
	}
}
