using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200027C RID: 636
	internal class ADSystemMailboxSchema : ADRecipientSchema
	{
		// Token: 0x040011CC RID: 4556
		public static readonly ADPropertyDefinition DeliveryMechanism = new ADPropertyDefinition("DeliveryMechanism", ExchangeObjectVersion.Exchange2003, typeof(DeliveryMechanisms), "deliveryMechanism", ADPropertyDefinitionFlags.PersistDefaultValue, DeliveryMechanisms.MessageStore, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(DeliveryMechanisms))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040011CD RID: 4557
		public static readonly ADPropertyDefinition RulesQuota = new ADPropertyDefinition("systemMailboxRulesQuota", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchMDBRulesQuota", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040011CE RID: 4558
		public static readonly ADPropertyDefinition RetainDeletedItemsFor = new ADPropertyDefinition("systemMailboxRetainDeletedItemsFor", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan?), "garbageCollPeriod", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040011CF RID: 4559
		public static readonly ADPropertyDefinition Database = IADMailStorageSchema.Database;

		// Token: 0x040011D0 RID: 4560
		public static readonly ADPropertyDefinition DeletedItemFlags = IADMailStorageSchema.DeletedItemFlags;

		// Token: 0x040011D1 RID: 4561
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = IADMailStorageSchema.DeliverToMailboxAndForward;

		// Token: 0x040011D2 RID: 4562
		public static readonly ADPropertyDefinition ExchangeGuid = IADMailStorageSchema.ExchangeGuid;

		// Token: 0x040011D3 RID: 4563
		public static readonly ADPropertyDefinition MailboxContainerGuid = IADMailStorageSchema.MailboxContainerGuid;

		// Token: 0x040011D4 RID: 4564
		public static readonly ADPropertyDefinition ExchangeSecurityDescriptor = IADMailStorageSchema.ExchangeSecurityDescriptor;

		// Token: 0x040011D5 RID: 4565
		public static readonly ADPropertyDefinition ExternalOofOptions = IADMailStorageSchema.ExternalOofOptions;

		// Token: 0x040011D6 RID: 4566
		public static readonly ADPropertyDefinition IsMailboxEnabled = IADMailStorageSchema.IsMailboxEnabled;

		// Token: 0x040011D7 RID: 4567
		public static readonly ADPropertyDefinition OfflineAddressBook = IADMailStorageSchema.OfflineAddressBook;

		// Token: 0x040011D8 RID: 4568
		public static readonly ADPropertyDefinition ProhibitSendQuota = IADMailStorageSchema.ProhibitSendQuota;

		// Token: 0x040011D9 RID: 4569
		public static readonly ADPropertyDefinition ProhibitSendReceiveQuota = IADMailStorageSchema.ProhibitSendReceiveQuota;

		// Token: 0x040011DA RID: 4570
		public static readonly ADPropertyDefinition ServerLegacyDN = IADMailStorageSchema.ServerLegacyDN;

		// Token: 0x040011DB RID: 4571
		public static readonly ADPropertyDefinition ServerName = IADMailStorageSchema.ServerName;

		// Token: 0x040011DC RID: 4572
		public static readonly ADPropertyDefinition UseDatabaseQuotaDefaults = IADMailStorageSchema.UseDatabaseQuotaDefaults;

		// Token: 0x040011DD RID: 4573
		public static readonly ADPropertyDefinition IssueWarningQuota = IADMailStorageSchema.IssueWarningQuota;
	}
}
