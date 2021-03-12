using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200009C RID: 156
	internal sealed class GrammarFileNames
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x000154C8 File Offset: 0x000136C8
		internal static string GetFileNameForGALUser()
		{
			return "gal";
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000154CF File Offset: 0x000136CF
		internal static string GetFileNameForDL()
		{
			return "distributionList";
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000154D8 File Offset: 0x000136D8
		internal static string GetFileNameForDialPlan(UMDialPlan dp)
		{
			ValidateArgument.NotNull(dp, "UMDialPlan");
			return dp.Id.ObjectGuid.ToString();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001550C File Offset: 0x0001370C
		internal static string GetFileNameForQBDN(ADObjectId objectId)
		{
			ValidateArgument.NotNull(objectId, "ADObjectId");
			return objectId.ObjectGuid.ToString();
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00015538 File Offset: 0x00013738
		internal static string GetFileNameForCustomAddressList(ADObjectId objectId)
		{
			return GrammarFileNames.GetFileNameForQBDN(objectId);
		}

		// Token: 0x0400034D RID: 845
		internal const string DiskGrammarCacheDirectoryName = "Cache";
	}
}
