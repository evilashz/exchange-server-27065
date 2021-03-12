using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000273 RID: 627
	internal abstract class IOriginatingTimestampSchema
	{
		// Token: 0x0400104A RID: 4170
		public static readonly ADPropertyDefinition LastExchangeChangedTime = new ADPropertyDefinition("LastExchangeChangedTime", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "msExchLastExchangeChangedTime", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.LastExchangeChangedTime);
	}
}
