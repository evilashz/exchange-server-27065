using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x0200012C RID: 300
	internal static class LocaleMap
	{
		// Token: 0x06000BD6 RID: 3030 RVA: 0x0006B306 File Offset: 0x00069506
		public static CultureInfo GetCultureFromLcid(int lcid)
		{
			return CultureInfo.GetCultureInfo(lcid);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0006B30E File Offset: 0x0006950E
		public static int GetLcidFromCulture(CultureInfo culture)
		{
			return culture.LCID;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0006B316 File Offset: 0x00069516
		public static int GetCompareLcidFromCulture(CultureInfo culture)
		{
			return culture.CompareInfo.LCID;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0006B323 File Offset: 0x00069523
		public static int GetANSICodePage(CultureInfo culture)
		{
			return culture.TextInfo.ANSICodePage;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0006B330 File Offset: 0x00069530
		public static CultureInfo GetSpecificCulture(string cultureName)
		{
			return CultureInfo.CreateSpecificCulture(cultureName);
		}
	}
}
