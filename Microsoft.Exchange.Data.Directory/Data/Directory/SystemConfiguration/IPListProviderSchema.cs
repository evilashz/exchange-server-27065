using System;
using System.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004CF RID: 1231
	internal sealed class IPListProviderSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002582 RID: 9602
		private const int EnabledMask = 1;

		// Token: 0x04002583 RID: 9603
		private const int AnyMatchMask = 2;

		// Token: 0x04002584 RID: 9604
		public static readonly ADPropertyDefinition ProviderFlags = new ADPropertyDefinition("ProviderFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMessageHygieneProviderFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002585 RID: 9605
		public static readonly ADPropertyDefinition LookupDomain = new ADPropertyDefinition("LookupDomain", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchMessageHygieneLookupDomain", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002586 RID: 9606
		public static readonly ADPropertyDefinition Priority = new ADPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMessageHygienePriority", ADPropertyDefinitionFlags.PersistDefaultValue, int.MaxValue, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002587 RID: 9607
		public static readonly ADPropertyDefinition Bitmask = new ADPropertyDefinition("Bitmask", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchMessageHygieneBitmask", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002588 RID: 9608
		public static readonly ADPropertyDefinition RejectionMessage = new ADPropertyDefinition("Rejectionmessage", ExchangeObjectVersion.Exchange2007, typeof(AsciiString), "msExchMessageHygieneRejectionMessage", ADPropertyDefinitionFlags.None, AsciiString.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 240)
		}, null, null);

		// Token: 0x04002589 RID: 9609
		public static readonly ADPropertyDefinition IPAddress = new ADPropertyDefinition("IPAddress", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchMessageHygieneIPAddress", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400258A RID: 9610
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IPListProviderSchema.ProviderFlags
		}, null, ADObject.FlagGetterDelegate(1, IPListProviderSchema.ProviderFlags), ADObject.FlagSetterDelegate(1, IPListProviderSchema.ProviderFlags), null, null);

		// Token: 0x0400258B RID: 9611
		public static readonly ADPropertyDefinition AnyMatch = new ADPropertyDefinition("AnyMatch", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IPListProviderSchema.ProviderFlags
		}, null, ADObject.FlagGetterDelegate(2, IPListProviderSchema.ProviderFlags), ADObject.FlagSetterDelegate(2, IPListProviderSchema.ProviderFlags), null, null);
	}
}
