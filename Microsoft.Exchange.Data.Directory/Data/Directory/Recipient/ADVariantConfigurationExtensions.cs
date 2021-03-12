using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000284 RID: 644
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ADVariantConfigurationExtensions
	{
		// Token: 0x06001EA2 RID: 7842 RVA: 0x000895E0 File Offset: 0x000877E0
		public static IConstraintProvider GetContext(this ADUser user, ExchangeConfigurationUnit configurationUnit = null)
		{
			if (configurationUnit == null)
			{
				return new ADUserConstraintProvider(user, "Global", false);
			}
			string rampId = ExchangeConfigurationUnitVariantConfigurationParser.GetRampId(configurationUnit);
			bool isFirstRelease = ExchangeConfigurationUnitVariantConfigurationParser.IsFirstRelease(configurationUnit);
			return new ADUserConstraintProvider(user, rampId, isFirstRelease);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00089613 File Offset: 0x00087813
		public static IConstraintProvider GetContext(this ADUser user, string rampId, bool isFirstRelease)
		{
			return new ADUserConstraintProvider(user, rampId, isFirstRelease);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00089620 File Offset: 0x00087820
		public static IConstraintProvider GetContext(this MiniRecipient recipient, ExchangeConfigurationUnit configurationUnit = null)
		{
			if (configurationUnit == null)
			{
				return new MiniRecipientConstraintProvider(recipient, "Global", false);
			}
			string rampId = ExchangeConfigurationUnitVariantConfigurationParser.GetRampId(configurationUnit);
			bool isFirstRelease = ExchangeConfigurationUnitVariantConfigurationParser.IsFirstRelease(configurationUnit);
			return new MiniRecipientConstraintProvider(recipient, rampId, isFirstRelease);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00089653 File Offset: 0x00087853
		public static IConstraintProvider GetContext(this MiniRecipient recipient, string rampId, bool isFirstRelease)
		{
			return new MiniRecipientConstraintProvider(recipient, rampId, isFirstRelease);
		}
	}
}
