using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F73 RID: 3955
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MiniRecipientMailboxOwnerAdapter : MailboxOwnerAdapter
	{
		// Token: 0x06008728 RID: 34600 RVA: 0x002506E0 File Offset: 0x0024E8E0
		public MiniRecipientMailboxOwnerAdapter(MiniRecipient miniRecipient, IConstraintProvider constraintProvider, RecipientTypeDetails recipientTypeDetails, LogonType logonType) : base(constraintProvider, recipientTypeDetails, logonType)
		{
			this.miniRecipient = miniRecipient;
		}

		// Token: 0x06008729 RID: 34601 RVA: 0x002506F3 File Offset: 0x0024E8F3
		protected override IGenericADUser CalculateGenericADUser()
		{
			return new GenericADUser(this.miniRecipient);
		}

		// Token: 0x04005A42 RID: 23106
		private readonly MiniRecipient miniRecipient;
	}
}
