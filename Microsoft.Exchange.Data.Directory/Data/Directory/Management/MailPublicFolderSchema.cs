using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000734 RID: 1844
	internal class MailPublicFolderSchema : MailEnabledRecipientSchema
	{
		// Token: 0x0600587C RID: 22652 RVA: 0x0013AAAD File Offset: 0x00138CAD
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADPublicFolderSchema>();
		}

		// Token: 0x04003B93 RID: 15251
		public static readonly ADPropertyDefinition Contacts = ADPublicFolderSchema.Contacts;

		// Token: 0x04003B94 RID: 15252
		public static readonly ADPropertyDefinition ContentMailbox = ADRecipientSchema.DefaultPublicFolderMailbox;

		// Token: 0x04003B95 RID: 15253
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = ADPublicFolderSchema.DeliverToMailboxAndForward;

		// Token: 0x04003B96 RID: 15254
		public static readonly ADPropertyDefinition EntryId = ADPublicFolderSchema.EntryId;

		// Token: 0x04003B97 RID: 15255
		public static readonly ADPropertyDefinition ExternalEmailAddress = ADRecipientSchema.ExternalEmailAddress;

		// Token: 0x04003B98 RID: 15256
		public static readonly ADPropertyDefinition ForwardingAddress = ADRecipientSchema.ForwardingAddress;

		// Token: 0x04003B99 RID: 15257
		public static readonly ADPropertyDefinition PhoneticDisplayName = ADRecipientSchema.PhoneticDisplayName;
	}
}
