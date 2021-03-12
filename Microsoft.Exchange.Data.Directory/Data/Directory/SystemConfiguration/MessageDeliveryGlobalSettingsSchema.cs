using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004B5 RID: 1205
	internal class MessageDeliveryGlobalSettingsSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002530 RID: 9520
		public static readonly ADPropertyDefinition MaxReceiveSize = new ADPropertyDefinition("MaxReceiveSize", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "delivContLength", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002531 RID: 9521
		public static readonly ADPropertyDefinition MaxSendSize = new ADPropertyDefinition("MaxSendSize", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "submissionContLength", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002532 RID: 9522
		public static readonly ADPropertyDefinition MaxRecipientEnvelopeLimit = new ADPropertyDefinition("MaxRecipientEnvelopeLimit", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<int>), "msExchRecipLimit", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
