using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003F3 RID: 1011
	internal class DeliveryAgentConnectorSchema : MailGatewaySchema
	{
		// Token: 0x04001F13 RID: 7955
		public static readonly ADPropertyDefinition MailGatewayFlags = new ADPropertyDefinition("MailGatewayFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMailGatewayFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F14 RID: 7956
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DeliveryAgentConnectorSchema.MailGatewayFlags
		}, null, ADObject.FlagGetterDelegate(DeliveryAgentConnectorSchema.MailGatewayFlags, 1), ADObject.FlagSetterDelegate(DeliveryAgentConnectorSchema.MailGatewayFlags, 1), null, null);

		// Token: 0x04001F15 RID: 7957
		public static readonly ADPropertyDefinition DeliveryProtocol = new ADPropertyDefinition("DeliveryProtocol", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTransportDeliveryAgentDeliveryProtocol", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F16 RID: 7958
		public static readonly ADPropertyDefinition MaxConcurrentConnections = new ADPropertyDefinition("MaxConcurrentConnections", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportDeliveryAgentMaxConcurrentConnections", ADPropertyDefinitionFlags.PersistDefaultValue, 5, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F17 RID: 7959
		public static readonly ADPropertyDefinition MaxMessagesPerConnection = new ADPropertyDefinition("MaxMessagesPerConnection", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportDeliveryAgentMaxMessagesPerConnection", ADPropertyDefinitionFlags.PersistDefaultValue, 20, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);
	}
}
