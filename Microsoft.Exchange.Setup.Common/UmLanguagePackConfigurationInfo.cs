using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200005C RID: 92
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UmLanguagePackConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0000FADC File Offset: 0x0000DCDC
		public static decimal GetUmLanguagePackSizeForCultureInfo(CultureInfo umlang)
		{
			decimal result = RequiredDiskSpaceStatistics.MaximumSizeOfOneLanguagePack;
			if (umlang != null)
			{
				string key = umlang.ToString().ToLower();
				if (UmLanguagePackConfigurationInfo.UmLanguagePackSizes.ContainsKey(key))
				{
					result = UmLanguagePackConfigurationInfo.UmLanguagePackSizes[key];
				}
			}
			return result;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000FB18 File Offset: 0x0000DD18
		static UmLanguagePackConfigurationInfo()
		{
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("de-de", RequiredDiskSpaceStatistics.DeDeLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("en-us", RequiredDiskSpaceStatistics.EnUsLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("en-au", RequiredDiskSpaceStatistics.EnAuLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("en-gb", RequiredDiskSpaceStatistics.EnGbLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("es-es", RequiredDiskSpaceStatistics.EsEsLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("es-mx", RequiredDiskSpaceStatistics.EsMxLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("fr-fr", RequiredDiskSpaceStatistics.FrFrLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("fr-ca", RequiredDiskSpaceStatistics.FrCaLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("it-it", RequiredDiskSpaceStatistics.ItItLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("ja-jp", RequiredDiskSpaceStatistics.JaJpLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("ko-kr", RequiredDiskSpaceStatistics.KoKrLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("nl-nl", RequiredDiskSpaceStatistics.NlNlLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("pt-br", RequiredDiskSpaceStatistics.PtBrLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("sv-se", RequiredDiskSpaceStatistics.SvSeLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("zh-cn", RequiredDiskSpaceStatistics.ZhCnLanguagePack);
			UmLanguagePackConfigurationInfo.UmLanguagePackSizes.Add("zh-tw", RequiredDiskSpaceStatistics.ZhTwLanguagePack);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000FC6F File Offset: 0x0000DE6F
		public override string Name
		{
			get
			{
				return UmLanguagePackConfigurationInfo.GetUmLanguagePackNameForCultureInfo(this.Culture);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		public override LocalizedString DisplayName
		{
			get
			{
				if (this.Culture == null)
				{
					return Strings.UmLanguagePackDisplayName;
				}
				return Strings.UmLanguagePackDisplayNameWithCulture(this.umlang.ToString());
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		public override decimal Size
		{
			get
			{
				if (this.Culture == null)
				{
					return RequiredDiskSpaceStatistics.MaximumSizeOfOneLanguagePack;
				}
				return UmLanguagePackConfigurationInfo.GetUmLanguagePackSizeForCultureInfo(this.Culture);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000FCB7 File Offset: 0x0000DEB7
		public CultureInfo Culture
		{
			get
			{
				return this.umlang;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000FCBF File Offset: 0x0000DEBF
		public UmLanguagePackConfigurationInfo(CultureInfo umlang)
		{
			this.umlang = umlang;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000FCCE File Offset: 0x0000DECE
		public UmLanguagePackConfigurationInfo()
		{
			this.umlang = null;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000FCDD File Offset: 0x0000DEDD
		public static string GetUmLanguagePackNameForCultureInfo(CultureInfo umlang)
		{
			if (umlang == null)
			{
				return "UmLanguagePack";
			}
			return "UmLanguagePack(" + umlang.ToString() + ")";
		}

		// Token: 0x0400018C RID: 396
		private CultureInfo umlang;

		// Token: 0x0400018D RID: 397
		private static Dictionary<string, decimal> UmLanguagePackSizes = new Dictionary<string, decimal>();
	}
}
