using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000297 RID: 663
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AddImContactToGroup
	{
		// Token: 0x060011AD RID: 4525 RVA: 0x00055B34 File Offset: 0x00053D34
		internal AddImContactToGroup(IMailboxSession mailboxSession, StoreId contactId, StoreId groupId, IXSOFactory xsoFactory, IUnifiedContactStoreConfiguration configuration)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (contactId == null)
			{
				throw new ArgumentNullException("contactId");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.contactId = contactId;
			this.groupId = groupId;
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(mailboxSession, xsoFactory, configuration);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00055BA0 File Offset: 0x00053DA0
		internal void Execute()
		{
			string displayName = this.unifiedContactStoreUtilities.RetrieveContactDisplayName(this.contactId);
			this.unifiedContactStoreUtilities.AddContactToGroup(this.contactId, displayName, this.groupId);
		}

		// Token: 0x04000CC2 RID: 3266
		private readonly StoreId contactId;

		// Token: 0x04000CC3 RID: 3267
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04000CC4 RID: 3268
		private StoreId groupId;
	}
}
