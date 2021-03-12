using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000358 RID: 856
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoveDistributionGroupFromImList
	{
		// Token: 0x06001813 RID: 6163 RVA: 0x00081834 File Offset: 0x0007FA34
		public RemoveDistributionGroupFromImList(IMailboxSession session, StoreId groupId, IXSOFactory xsoFactory)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (groupId == null)
			{
				throw new ArgumentNullException("groupId");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(session, xsoFactory);
			this.groupId = groupId;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00081885 File Offset: 0x0007FA85
		public void Execute()
		{
			this.unifiedContactStoreUtilities.RemoveDistributionGroupFromImList(this.groupId);
		}

		// Token: 0x04001020 RID: 4128
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04001021 RID: 4129
		private readonly StoreId groupId;
	}
}
