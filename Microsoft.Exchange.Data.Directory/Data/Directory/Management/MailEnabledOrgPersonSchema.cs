using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000726 RID: 1830
	internal abstract class MailEnabledOrgPersonSchema : MailEnabledRecipientSchema
	{
		// Token: 0x04003AA5 RID: 15013
		public static readonly ADPropertyDefinition Extensions = UMMailboxSchema.Extensions;

		// Token: 0x04003AA6 RID: 15014
		public static readonly ADPropertyDefinition HasPicture = ADRecipientSchema.HasPicture;

		// Token: 0x04003AA7 RID: 15015
		public static readonly ADPropertyDefinition HasSpokenName = ADRecipientSchema.HasSpokenName;
	}
}
