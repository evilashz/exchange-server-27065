using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000249 RID: 585
	internal class FrontEndMiniRecipientSchema : ADObjectSchema
	{
		// Token: 0x04000DAD RID: 3501
		public static readonly ADPropertyDefinition RecipientType = ADRecipientSchema.RecipientType;

		// Token: 0x04000DAE RID: 3502
		public static readonly ADPropertyDefinition Database = ADMailboxRecipientSchema.Database;

		// Token: 0x04000DAF RID: 3503
		public static readonly ADPropertyDefinition ExchangeGuid = ADMailboxRecipientSchema.ExchangeGuid;

		// Token: 0x04000DB0 RID: 3504
		public static readonly ADPropertyDefinition ArchiveDatabase = IADMailStorageSchema.ArchiveDatabase;

		// Token: 0x04000DB1 RID: 3505
		public static readonly ADPropertyDefinition ArchiveGuid = IADMailStorageSchema.ArchiveGuid;

		// Token: 0x04000DB2 RID: 3506
		public static readonly ADPropertyDefinition LastExchangeChangedTime = IOriginatingTimestampSchema.LastExchangeChangedTime;
	}
}
