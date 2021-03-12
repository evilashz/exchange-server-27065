using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200050B RID: 1291
	internal class ForeignConnectorSchema : MailGatewaySchema
	{
		// Token: 0x04002702 RID: 9986
		public static readonly ADPropertyDefinition MailGatewayFlags = new ADPropertyDefinition("MailGatewayFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMailGatewayFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002703 RID: 9987
		public static readonly ADPropertyDefinition DropDirectory = new ADPropertyDefinition("DropDirectory", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTransportDropDirectoryName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002704 RID: 9988
		public static readonly ADPropertyDefinition DropDirectoryQuota = new ADPropertyDefinition("DropDirectoryQuota", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.MegabyteQuantifierProvider, "msExchTransportDropDirectoryQuota", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromMB(1UL), ByteQuantifiedSize.FromMB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002705 RID: 9989
		public static readonly ADPropertyDefinition RelayDsnRequired = new ADPropertyDefinition("RelayDsnRequired", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ForeignConnectorSchema.MailGatewayFlags
		}, null, ADObject.FlagGetterDelegate(ForeignConnectorSchema.MailGatewayFlags, 1), ADObject.FlagSetterDelegate(ForeignConnectorSchema.MailGatewayFlags, 1), null, null);

		// Token: 0x04002706 RID: 9990
		public static readonly ADPropertyDefinition Disabled = new ADPropertyDefinition("Disabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ForeignConnectorSchema.MailGatewayFlags
		}, null, ADObject.FlagGetterDelegate(ForeignConnectorSchema.MailGatewayFlags, 2), ADObject.FlagSetterDelegate(ForeignConnectorSchema.MailGatewayFlags, 2), null, null);
	}
}
