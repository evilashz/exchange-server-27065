using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200075E RID: 1886
	internal class SyncDynamicDistributionGroupSchema : DynamicDistributionGroupSchema
	{
		// Token: 0x04003E46 RID: 15942
		public static readonly ADPropertyDefinition BlockedSendersHash = ADRecipientSchema.BlockedSendersHash;

		// Token: 0x04003E47 RID: 15943
		public static readonly ADPropertyDefinition RecipientDisplayType = ADRecipientSchema.RecipientDisplayType;

		// Token: 0x04003E48 RID: 15944
		public static readonly ADPropertyDefinition SafeRecipientsHash = ADRecipientSchema.SafeRecipientsHash;

		// Token: 0x04003E49 RID: 15945
		public static readonly ADPropertyDefinition SafeSendersHash = ADRecipientSchema.SafeSendersHash;

		// Token: 0x04003E4A RID: 15946
		public static readonly ADPropertyDefinition EndOfList = SyncMailboxSchema.EndOfList;

		// Token: 0x04003E4B RID: 15947
		public static readonly ADPropertyDefinition Cookie = SyncMailboxSchema.Cookie;

		// Token: 0x04003E4C RID: 15948
		public static readonly ADPropertyDefinition DirSyncId = ADRecipientSchema.DirSyncId;
	}
}
