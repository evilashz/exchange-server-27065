using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000745 RID: 1861
	internal class RemoteMailboxSchema : MailUserSchema
	{
		// Token: 0x04003CCE RID: 15566
		public static readonly ADPropertyDefinition RemoteRoutingAddress = ADRecipientSchema.ExternalEmailAddress;

		// Token: 0x04003CCF RID: 15567
		public static readonly ADPropertyDefinition OnPremisesOrganizationalUnit = ADRecipientSchema.OrganizationalUnit;

		// Token: 0x04003CD0 RID: 15568
		public static readonly ADPropertyDefinition RemoteRecipientType = ADUserSchema.RemoteRecipientType;

		// Token: 0x04003CD1 RID: 15569
		public static readonly ADPropertyDefinition ArchiveState = ADUserSchema.ArchiveState;
	}
}
