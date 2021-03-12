using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000625 RID: 1573
	internal class ForwardSyncCookieHeaderSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04003375 RID: 13173
		protected const int CookieTypeBitPosition = 0;

		// Token: 0x04003376 RID: 13174
		protected const int CookieTypeBitLength = 4;

		// Token: 0x04003377 RID: 13175
		protected const int IsSyncPropertySetUpgradingBitPosition = 4;

		// Token: 0x04003378 RID: 13176
		internal static readonly ADPropertyDefinition TimestampRaw = new ADPropertyDefinition("TimestampRaw", ExchangeObjectVersion.Exchange2010, typeof(long), "msExchMSOForwardSyncCookieTimestamp", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003379 RID: 13177
		internal static readonly ADPropertyDefinition Timestamp = new ADPropertyDefinition("Timestamp", ExchangeObjectVersion.Exchange2010, typeof(DateTime), null, ADPropertyDefinitionFlags.Calculated, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ForwardSyncCookieHeaderSchema.TimestampRaw
		}, null, (IPropertyBag bag) => DateTime.FromFileTimeUtc((long)bag[ForwardSyncCookieHeaderSchema.TimestampRaw]), delegate(object value, IPropertyBag bag)
		{
			bag[ForwardSyncCookieHeaderSchema.TimestampRaw] = ((DateTime)value).ToFileTimeUtc();
		}, null, null);

		// Token: 0x0400337A RID: 13178
		internal static readonly ADPropertyDefinition Type = ADObject.BitfieldProperty("Type", 0, 4, SharedPropertyDefinitions.ProvisioningFlags);
	}
}
