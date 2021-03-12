using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000795 RID: 1941
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExchangePrincipalFlightingExtensions
	{
		// Token: 0x06004917 RID: 18711 RVA: 0x00131CF0 File Offset: 0x0012FEF0
		public static VariantConfigurationSnapshot GetConfiguration(this IExchangePrincipal exchangePrincipal)
		{
			return VariantConfiguration.GetSnapshot(exchangePrincipal.GetContext(null), null, null);
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x00131D00 File Offset: 0x0012FF00
		public static IConstraintProvider GetContext(this IExchangePrincipal exchangePrincipal, ExchangeConfigurationUnit configurationUnit = null)
		{
			if (configurationUnit == null)
			{
				return new ExchangePrincipalConstraintProvider(exchangePrincipal, "Global", false);
			}
			string rampId = ExchangeConfigurationUnitVariantConfigurationParser.GetRampId(configurationUnit);
			bool isFirstRelease = ExchangeConfigurationUnitVariantConfigurationParser.IsFirstRelease(configurationUnit);
			return new ExchangePrincipalConstraintProvider(exchangePrincipal, rampId, isFirstRelease);
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00131D33 File Offset: 0x0012FF33
		public static IConstraintProvider GetContext(this IExchangePrincipal exchangePrincipal, string rampId, bool isFirstRelease)
		{
			return new ExchangePrincipalConstraintProvider(exchangePrincipal, rampId, isFirstRelease);
		}
	}
}
