using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003F2 RID: 1010
	internal class MailGatewaySchema : SendConnectorSchema
	{
		// Token: 0x04001F0D RID: 7949
		public static readonly ADPropertyDefinition AddressSpaces = new ADPropertyDefinition("AddressSpaces", ExchangeObjectVersion.Exchange2003, typeof(AddressSpace), "routingList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F0E RID: 7950
		public static readonly ADPropertyDefinition ConnectedDomains = new ADPropertyDefinition("ConnectedDomains", ExchangeObjectVersion.Exchange2003, typeof(ConnectedDomain), "connectedDomains", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F0F RID: 7951
		public static readonly ADPropertyDefinition DeliveryMechanism = new ADPropertyDefinition("DeliveryMechanism", ExchangeObjectVersion.Exchange2003, typeof(int), "deliveryMechanism", ADPropertyDefinitionFlags.PersistDefaultValue, 2, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 3)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F10 RID: 7952
		public static readonly ADPropertyDefinition Comment = new ADPropertyDefinition("Comment", ExchangeObjectVersion.Exchange2003, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F11 RID: 7953
		public static readonly ADPropertyDefinition IsScopedConnector = new ADPropertyDefinition("IsScopedConnector", ExchangeObjectVersion.Exchange2003, typeof(bool?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F12 RID: 7954
		public static readonly ADPropertyDefinition IsSmtpConnector = new ADPropertyDefinition("IsSmtpConnector", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.ObjectClass
		}, null, new GetterDelegate(MailGateway.IsSmtpConnectorGetter), null, null, null);
	}
}
