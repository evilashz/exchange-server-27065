using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200029B RID: 667
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AddNewImContactToGroup
	{
		// Token: 0x060011B7 RID: 4535 RVA: 0x00055DAC File Offset: 0x00053FAC
		internal AddNewImContactToGroup(IMailboxSession mailboxSession, string imAddress, string displayName, StoreId groupId, IXSOFactory xsoFactory, IUnifiedContactStoreConfiguration configuration)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (imAddress == null)
			{
				throw new ArgumentNullException("imAddress");
			}
			if (imAddress.Length == 0)
			{
				throw new ArgumentException("imAddress was empty");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(mailboxSession, xsoFactory, configuration);
			this.imAddress = imAddress;
			this.displayName = (string.IsNullOrEmpty(displayName) ? imAddress : displayName);
			this.groupId = groupId;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00055E40 File Offset: 0x00054040
		internal PersonId Execute()
		{
			StoreObjectId contactId;
			PersonId result;
			this.unifiedContactStoreUtilities.RetrieveOrCreateContact(this.imAddress, this.displayName, out contactId, out result);
			this.unifiedContactStoreUtilities.AddContactToGroup(contactId, this.displayName, this.groupId);
			return result;
		}

		// Token: 0x04000CC7 RID: 3271
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04000CC8 RID: 3272
		private readonly string imAddress;

		// Token: 0x04000CC9 RID: 3273
		private readonly string displayName;

		// Token: 0x04000CCA RID: 3274
		private readonly StoreId groupId;
	}
}
