using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000355 RID: 853
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoveContactFromImList
	{
		// Token: 0x06001808 RID: 6152 RVA: 0x000815FC File Offset: 0x0007F7FC
		public RemoveContactFromImList(IMailboxSession session, StoreId contactId, IXSOFactory xsoFactory)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (contactId == null)
			{
				throw new ArgumentNullException("contactId");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(session, xsoFactory);
			this.contactId = contactId;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0008164D File Offset: 0x0007F84D
		internal void Execute()
		{
			this.unifiedContactStoreUtilities.RemoveContactFromImList(this.contactId);
		}

		// Token: 0x0400101C RID: 4124
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x0400101D RID: 4125
		private readonly StoreId contactId;
	}
}
