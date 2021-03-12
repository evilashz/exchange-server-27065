using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200027B RID: 635
	internal class ADSystemAttendantMailboxSchema : ADRecipientSchema
	{
		// Token: 0x040011BB RID: 4539
		public static readonly ADPropertyDefinition Database = IADMailStorageSchema.Database;

		// Token: 0x040011BC RID: 4540
		public static readonly ADPropertyDefinition DeletedItemFlags = IADMailStorageSchema.DeletedItemFlags;

		// Token: 0x040011BD RID: 4541
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = IADMailStorageSchema.DeliverToMailboxAndForward;

		// Token: 0x040011BE RID: 4542
		public static readonly ADPropertyDefinition ExchangeGuid = IADMailStorageSchema.ExchangeGuid;

		// Token: 0x040011BF RID: 4543
		public static readonly ADPropertyDefinition MailboxContainerGuid = IADMailStorageSchema.MailboxContainerGuid;

		// Token: 0x040011C0 RID: 4544
		public static readonly ADPropertyDefinition ExchangeSecurityDescriptor = IADMailStorageSchema.ExchangeSecurityDescriptor;

		// Token: 0x040011C1 RID: 4545
		public static readonly ADPropertyDefinition ExternalOofOptions = IADMailStorageSchema.ExternalOofOptions;

		// Token: 0x040011C2 RID: 4546
		public static readonly ADPropertyDefinition RetainDeletedItemsFor = IADMailStorageSchema.RetainDeletedItemsFor;

		// Token: 0x040011C3 RID: 4547
		public static readonly ADPropertyDefinition IsMailboxEnabled = IADMailStorageSchema.IsMailboxEnabled;

		// Token: 0x040011C4 RID: 4548
		public static readonly ADPropertyDefinition OfflineAddressBook = IADMailStorageSchema.OfflineAddressBook;

		// Token: 0x040011C5 RID: 4549
		public static readonly ADPropertyDefinition ProhibitSendQuota = IADMailStorageSchema.ProhibitSendQuota;

		// Token: 0x040011C6 RID: 4550
		public static readonly ADPropertyDefinition ProhibitSendReceiveQuota = IADMailStorageSchema.ProhibitSendReceiveQuota;

		// Token: 0x040011C7 RID: 4551
		public static readonly ADPropertyDefinition ServerLegacyDN = IADMailStorageSchema.ServerLegacyDN;

		// Token: 0x040011C8 RID: 4552
		public static readonly ADPropertyDefinition ServerName = IADMailStorageSchema.ServerName;

		// Token: 0x040011C9 RID: 4553
		public static readonly ADPropertyDefinition UseDatabaseQuotaDefaults = IADMailStorageSchema.UseDatabaseQuotaDefaults;

		// Token: 0x040011CA RID: 4554
		public static readonly ADPropertyDefinition IssueWarningQuota = IADMailStorageSchema.IssueWarningQuota;

		// Token: 0x040011CB RID: 4555
		public static readonly ADPropertyDefinition RulesQuota = IADMailStorageSchema.RulesQuota;
	}
}
