using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200035A RID: 858
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoveImContactFromGroup
	{
		// Token: 0x06001818 RID: 6168 RVA: 0x00081928 File Offset: 0x0007FB28
		internal RemoveImContactFromGroup(IMailboxSession mailboxSession, StoreId contactId, StoreId groupId, IXSOFactory xsoFactory)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (contactId == null)
			{
				throw new ArgumentNullException("contactId");
			}
			if (groupId == null)
			{
				throw new ArgumentNullException("groupId");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			this.contactId = contactId;
			this.groupId = groupId;
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(mailboxSession, xsoFactory);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00081990 File Offset: 0x0007FB90
		internal void Execute()
		{
			this.unifiedContactStoreUtilities.RemoveContactFromGroup(this.contactId, this.groupId);
		}

		// Token: 0x04001022 RID: 4130
		private readonly StoreId contactId;

		// Token: 0x04001023 RID: 4131
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04001024 RID: 4132
		private StoreId groupId;
	}
}
