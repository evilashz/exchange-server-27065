using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200027D RID: 637
	internal class ADMicrosoftExchangeRecipientSchema : ADRecipientSchema
	{
		// Token: 0x040011DE RID: 4574
		public static readonly ADPropertyDefinition Database = IADMailStorageSchema.Database;

		// Token: 0x040011DF RID: 4575
		public static readonly ADPropertyDefinition DeletedItemFlags = IADMailStorageSchema.DeletedItemFlags;

		// Token: 0x040011E0 RID: 4576
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = IADMailStorageSchema.DeliverToMailboxAndForward;

		// Token: 0x040011E1 RID: 4577
		public static readonly ADPropertyDefinition ExchangeGuid = IADMailStorageSchema.ExchangeGuid;

		// Token: 0x040011E2 RID: 4578
		public static readonly ADPropertyDefinition MailboxContainerGuid = IADMailStorageSchema.MailboxContainerGuid;

		// Token: 0x040011E3 RID: 4579
		public static readonly ADPropertyDefinition ExchangeSecurityDescriptor = IADMailStorageSchema.ExchangeSecurityDescriptor;

		// Token: 0x040011E4 RID: 4580
		public static readonly ADPropertyDefinition ExternalOofOptions = IADMailStorageSchema.ExternalOofOptions;

		// Token: 0x040011E5 RID: 4581
		public static readonly ADPropertyDefinition RetainDeletedItemsFor = IADMailStorageSchema.RetainDeletedItemsFor;

		// Token: 0x040011E6 RID: 4582
		public static readonly ADPropertyDefinition IsMailboxEnabled = IADMailStorageSchema.IsMailboxEnabled;

		// Token: 0x040011E7 RID: 4583
		public static readonly ADPropertyDefinition OfflineAddressBook = IADMailStorageSchema.OfflineAddressBook;

		// Token: 0x040011E8 RID: 4584
		public static readonly ADPropertyDefinition ProhibitSendQuota = IADMailStorageSchema.ProhibitSendQuota;

		// Token: 0x040011E9 RID: 4585
		public static readonly ADPropertyDefinition ProhibitSendReceiveQuota = IADMailStorageSchema.ProhibitSendReceiveQuota;

		// Token: 0x040011EA RID: 4586
		public static readonly ADPropertyDefinition ServerLegacyDN = IADMailStorageSchema.ServerLegacyDN;

		// Token: 0x040011EB RID: 4587
		public static readonly ADPropertyDefinition ServerName = IADMailStorageSchema.ServerName;

		// Token: 0x040011EC RID: 4588
		public static readonly ADPropertyDefinition UseDatabaseQuotaDefaults = IADMailStorageSchema.UseDatabaseQuotaDefaults;

		// Token: 0x040011ED RID: 4589
		public static readonly ADPropertyDefinition IssueWarningQuota = IADMailStorageSchema.IssueWarningQuota;

		// Token: 0x040011EE RID: 4590
		public static readonly ADPropertyDefinition RulesQuota = IADMailStorageSchema.RulesQuota;
	}
}
