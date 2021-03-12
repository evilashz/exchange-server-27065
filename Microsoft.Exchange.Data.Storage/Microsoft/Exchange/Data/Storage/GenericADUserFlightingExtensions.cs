using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000797 RID: 1943
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class GenericADUserFlightingExtensions
	{
		// Token: 0x06004920 RID: 18720 RVA: 0x00131ED5 File Offset: 0x001300D5
		public static VariantConfigurationSnapshot GetConfiguration(this IGenericADUser adUser)
		{
			return VariantConfiguration.GetSnapshot(adUser.GetContext(null), null, null);
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x00131EE8 File Offset: 0x001300E8
		public static IConstraintProvider GetContext(this IGenericADUser adUser, ExchangeConfigurationUnit configurationUnit = null)
		{
			if (configurationUnit == null)
			{
				string rampId = string.IsNullOrEmpty(adUser.ExternalDirectoryObjectId) ? "Global" : adUser.ExternalDirectoryObjectId;
				return new GenericADUserConstraintProvider(adUser, rampId, false);
			}
			string rampId2 = ExchangeConfigurationUnitVariantConfigurationParser.GetRampId(configurationUnit);
			bool isFirstRelease = ExchangeConfigurationUnitVariantConfigurationParser.IsFirstRelease(configurationUnit);
			return new GenericADUserConstraintProvider(adUser, rampId2, isFirstRelease);
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x00131F32 File Offset: 0x00130132
		public static IConstraintProvider GetContext(this IGenericADUser adUser, string rampId, bool isFirstRelease)
		{
			return new GenericADUserConstraintProvider(adUser, rampId, isFirstRelease);
		}
	}
}
