using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000272 RID: 626
	internal abstract class IADDistributionListSchema
	{
		// Token: 0x04001043 RID: 4163
		public static readonly ADPropertyDefinition ExpansionServer = new ADPropertyDefinition("ExpansionServer", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchExpansionServerName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001044 RID: 4164
		public static readonly ADPropertyDefinition RawManagedBy = new ADPropertyDefinition("RawManagedBy", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "managedBy", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001045 RID: 4165
		public static readonly ADPropertyDefinition Members = new ADPropertyDefinition("Members", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "member", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001046 RID: 4166
		public static readonly ADPropertyDefinition ReportToManagerEnabled = new ADPropertyDefinition("ReportToManagerEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "reportToOwner", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001047 RID: 4167
		public static readonly ADPropertyDefinition ReportToOriginatorEnabled = new ADPropertyDefinition("ReportToOriginatorEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "reportToOriginator", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001048 RID: 4168
		public static readonly ADPropertyDefinition SendOofMessageToOriginatorEnabled = new ADPropertyDefinition("SendOofMessageToOriginatorEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "oOFReplyToOriginator", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001049 RID: 4169
		public static readonly ADPropertyDefinition SendDeliveryReportsTo = new ADPropertyDefinition("SendDeliveryReportsTo", ExchangeObjectVersion.Exchange2003, typeof(DeliveryReportsReceiver), null, ADPropertyDefinitionFlags.Calculated, DeliveryReportsReceiver.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IADDistributionListSchema.ReportToManagerEnabled,
			IADDistributionListSchema.ReportToOriginatorEnabled
		}, new CustomFilterBuilderDelegate(ADGroup.SendDeliveryReportsToFilterBuilder), new GetterDelegate(ADGroup.SendDeliveryReportsToGetter), new SetterDelegate(ADGroup.SendDeliveryReportsToSetter), null, null);
	}
}
