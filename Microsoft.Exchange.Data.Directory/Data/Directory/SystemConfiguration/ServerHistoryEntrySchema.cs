using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000629 RID: 1577
	internal class ServerHistoryEntrySchema : ADConfigurationObjectSchema
	{
		// Token: 0x04003386 RID: 13190
		internal static readonly ADPropertyDefinition TimestampRaw = new ADPropertyDefinition("TimestampRaw", ExchangeObjectVersion.Exchange2010, typeof(long), "msExchMSOForwardSyncCookieTimestamp", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003387 RID: 13191
		internal static readonly ADPropertyDefinition Timestamp = new ADPropertyDefinition("Timestamp", ExchangeObjectVersion.Exchange2010, typeof(DateTime), null, ADPropertyDefinitionFlags.Calculated, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerHistoryEntrySchema.TimestampRaw
		}, null, (IPropertyBag bag) => DateTime.FromFileTimeUtc((long)bag[ServerHistoryEntrySchema.TimestampRaw]), delegate(object value, IPropertyBag bag)
		{
			bag[ServerHistoryEntrySchema.TimestampRaw] = ((DateTime)value).ToFileTimeUtc();
		}, null, null);

		// Token: 0x04003388 RID: 13192
		internal static readonly ADPropertyDefinition Version = new ADPropertyDefinition("Version", ExchangeObjectVersion.Exchange2010, typeof(int), "VersionNumber", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003389 RID: 13193
		internal static readonly ADPropertyDefinition Data = new ADPropertyDefinition("Data", ExchangeObjectVersion.Exchange2010, typeof(byte[]), "msExchSyncCookie", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 262144)
		}, null, null);
	}
}
