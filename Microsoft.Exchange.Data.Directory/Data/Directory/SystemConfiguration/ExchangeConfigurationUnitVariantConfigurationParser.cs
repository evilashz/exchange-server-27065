using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200068A RID: 1674
	internal static class ExchangeConfigurationUnitVariantConfigurationParser
	{
		// Token: 0x06004DE4 RID: 19940 RVA: 0x0011F1BB File Offset: 0x0011D3BB
		internal static string GetRampId(ExchangeConfigurationUnit configurationUnit)
		{
			if (configurationUnit == null)
			{
				throw new ArgumentNullException("configurationUnit");
			}
			return configurationUnit.ExternalDirectoryOrganizationId;
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0011F1D4 File Offset: 0x0011D3D4
		internal static bool IsFirstRelease(ExchangeConfigurationUnit configurationUnit)
		{
			return configurationUnit.ReleaseTrack == ReleaseTrack.FirstRelease;
		}
	}
}
