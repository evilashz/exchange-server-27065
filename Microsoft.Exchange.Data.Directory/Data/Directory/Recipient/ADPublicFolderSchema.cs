using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200027A RID: 634
	internal class ADPublicFolderSchema : ADRecipientSchema
	{
		// Token: 0x040011B7 RID: 4535
		public static readonly ADPropertyDefinition Contacts = new ADPropertyDefinition("PublicFolderContacts", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "pFContacts", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040011B8 RID: 4536
		public static readonly ADPropertyDefinition Database = IADMailStorageSchema.Database;

		// Token: 0x040011B9 RID: 4537
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = IADMailStorageSchema.DeliverToMailboxAndForward;

		// Token: 0x040011BA RID: 4538
		public static readonly ADPropertyDefinition EntryId = new ADPropertyDefinition("EntryId", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchPublicFolderEntryId", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
