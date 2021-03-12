using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000262 RID: 610
	internal class RemovedMailboxSchema : DeletedObjectSchema
	{
		// Token: 0x04000E08 RID: 3592
		public new static readonly ADPropertyDefinition Name = new ADPropertyDefinition("Name", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.RawName
		}, new CustomFilterBuilderDelegate(RemovedMailbox.NameFilterBuilderDelegate), new GetterDelegate(RemovedMailbox.NameGetter), null, null, null);

		// Token: 0x04000E09 RID: 3593
		public static readonly ADPropertyDefinition PreviousDatabase = IADMailStorageSchema.PreviousDatabase;

		// Token: 0x04000E0A RID: 3594
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x04000E0B RID: 3595
		public static readonly ADPropertyDefinition ExchangeGuid = IADMailStorageSchema.ExchangeGuid;

		// Token: 0x04000E0C RID: 3596
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADRecipientSchema.LegacyExchangeDN;

		// Token: 0x04000E0D RID: 3597
		public static readonly ADPropertyDefinition SamAccountName = IADSecurityPrincipalSchema.SamAccountName;

		// Token: 0x04000E0E RID: 3598
		public static readonly ADPropertyDefinition WindowsLiveID = ADRecipientSchema.WindowsLiveID;

		// Token: 0x04000E0F RID: 3599
		public static readonly ADPropertyDefinition NetID = new ADPropertyDefinition("NetID", ExchangeObjectVersion.Exchange2010, typeof(NetID), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EmailAddresses
		}, null, new GetterDelegate(RemovedMailbox.NetIDGetter), null, null, null);

		// Token: 0x04000E10 RID: 3600
		public static readonly ADPropertyDefinition ConsumerNetID = new ADPropertyDefinition("ConsumerNetID", ExchangeObjectVersion.Exchange2010, typeof(NetID), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EmailAddresses
		}, null, new GetterDelegate(RemovedMailbox.ConsumerNetIDGetter), null, null, null);

		// Token: 0x04000E11 RID: 3601
		public static readonly ADPropertyDefinition IsPasswordResetRequired = new ADPropertyDefinition("IsPasswordResetRequired", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EmailAddresses
		}, null, new GetterDelegate(RemovedMailbox.IsPasswordResetRequiredGetter), null, null, null);
	}
}
