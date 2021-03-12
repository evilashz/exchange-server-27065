using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200029D RID: 669
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AddNewTelUriContactToGroup
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x00055F88 File Offset: 0x00054188
		internal AddNewTelUriContactToGroup(IMailboxSession mailboxSession, string telUriAddress, string imContactSipUriAddress, string imTelephoneNumber, StoreId groupId, IXSOFactory xsoFactory, IUnifiedContactStoreConfiguration configuration)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (telUriAddress == null)
			{
				throw new ArgumentNullException("telUriAddress");
			}
			if (telUriAddress.Length == 0)
			{
				throw new ArgumentException("telUriAddress was empty");
			}
			if (imContactSipUriAddress == null)
			{
				throw new ArgumentNullException("imContactSipUriAddress");
			}
			if (imContactSipUriAddress.Length == 0)
			{
				throw new ArgumentException("imContactSipUriAddress was empty");
			}
			if (imTelephoneNumber == null)
			{
				throw new ArgumentNullException("imTelephoneNumber");
			}
			if (imTelephoneNumber.Length == 0)
			{
				throw new ArgumentException("imTelephoneNumber was empty");
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
			this.telUriAddress = telUriAddress;
			this.imContactSipUriAddress = imContactSipUriAddress;
			this.imTelephoneNumber = imTelephoneNumber;
			this.groupId = groupId;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0005605C File Offset: 0x0005425C
		internal PersonId Execute()
		{
			StoreObjectId contactId;
			PersonId result;
			this.unifiedContactStoreUtilities.RetrieveOrCreateTelUriContact(this.telUriAddress, this.imContactSipUriAddress, this.imTelephoneNumber, out contactId, out result);
			this.unifiedContactStoreUtilities.AddContactToGroup(contactId, this.imTelephoneNumber, this.groupId);
			return result;
		}

		// Token: 0x04000CCB RID: 3275
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04000CCC RID: 3276
		private readonly string telUriAddress;

		// Token: 0x04000CCD RID: 3277
		private readonly string imContactSipUriAddress;

		// Token: 0x04000CCE RID: 3278
		private readonly string imTelephoneNumber;

		// Token: 0x04000CCF RID: 3279
		private readonly StoreId groupId;
	}
}
