using System;
using System.Globalization;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000084 RID: 132
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiCultureInfo : CultureInfo
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000F354 File Offset: 0x0000D554
		private MapiCultureInfo(CultureInfo stringCulture, CultureInfo sortCulture, int codePage) : base(stringCulture.Name)
		{
			this.sortCultureInfo = sortCulture;
			this.codePage = codePage;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F370 File Offset: 0x0000D570
		public static CultureInfo CreateCultureInfo(int stringLCID, int sortLCID, int codePage)
		{
			if (stringLCID == 1024)
			{
				return CultureInfo.InvariantCulture;
			}
			CultureInfo result;
			try
			{
				CultureInfo cultureFromLcid = LocaleMap.GetCultureFromLcid(stringLCID);
				CultureInfo sortCulture = null;
				if (sortLCID != 1024)
				{
					try
					{
						sortCulture = LocaleMap.GetCultureFromLcid(sortLCID);
					}
					catch (ArgumentException)
					{
						sortCulture = null;
					}
				}
				MapiCultureInfo mapiCultureInfo = new MapiCultureInfo(cultureFromLcid, sortCulture, codePage);
				if (!mapiCultureInfo.IsNeutralCulture)
				{
					result = mapiCultureInfo;
				}
				else
				{
					CultureInfo specificCulture = LocaleMap.GetSpecificCulture(mapiCultureInfo.Name);
					result = new MapiCultureInfo(specificCulture, sortCulture, codePage);
				}
			}
			catch (Exception ex)
			{
				if (!(ex is CultureNotFoundException) && !(ex is ArgumentException))
				{
					throw;
				}
				result = new MapiCultureInfo(CultureInfo.InvariantCulture, CultureInfo.InvariantCulture, codePage);
			}
			return result;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000F420 File Offset: 0x0000D620
		public static CultureInfo AdjustFromClientRequest(CultureInfo clientRequested, CultureInfo currentSelection)
		{
			MapiCultureInfo mapiCultureInfo = clientRequested as MapiCultureInfo;
			if (mapiCultureInfo == null)
			{
				return currentSelection;
			}
			MapiCultureInfo mapiCultureInfo2 = currentSelection as MapiCultureInfo;
			if (mapiCultureInfo2 != null)
			{
				return currentSelection;
			}
			return new MapiCultureInfo(currentSelection, currentSelection, mapiCultureInfo.codePage);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F454 File Offset: 0x0000D654
		internal static void RetrieveConnectParameters(CultureInfo cultureInfo, out int stringLCID, out int sortLCID, out int codePage)
		{
			if (cultureInfo == null)
			{
				stringLCID = 1024;
				sortLCID = 1024;
				codePage = LocaleMap.GetANSICodePage(CultureInfo.InvariantCulture);
				return;
			}
			stringLCID = LocaleMap.GetLcidFromCulture(cultureInfo);
			sortLCID = LocaleMap.GetCompareLcidFromCulture(cultureInfo);
			MapiCultureInfo mapiCultureInfo = cultureInfo as MapiCultureInfo;
			if (mapiCultureInfo != null)
			{
				codePage = mapiCultureInfo.codePage;
				return;
			}
			codePage = LocaleMap.GetANSICodePage(cultureInfo);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000F4AA File Offset: 0x0000D6AA
		public override TextInfo TextInfo
		{
			get
			{
				if (this.sortCultureInfo == null)
				{
					return base.TextInfo;
				}
				return this.sortCultureInfo.TextInfo;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000F4C6 File Offset: 0x0000D6C6
		public override CompareInfo CompareInfo
		{
			get
			{
				if (this.sortCultureInfo == null)
				{
					return base.CompareInfo;
				}
				return this.sortCultureInfo.CompareInfo;
			}
		}

		// Token: 0x040004F6 RID: 1270
		private readonly CultureInfo sortCultureInfo;

		// Token: 0x040004F7 RID: 1271
		private readonly int codePage;
	}
}
