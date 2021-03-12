using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200030E RID: 782
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetImItems
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x00072774 File Offset: 0x00070974
		public GetImItems(IMailboxSession session, StoreId[] contactIds, StoreId[] groupIds, ExtendedPropertyUri[] extendedProperties, IXSOFactory xsoFactory)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(session, xsoFactory);
			this.contactIds = contactIds;
			this.groupIds = groupIds;
			this.extendedProperties = extendedProperties;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000727C8 File Offset: 0x000709C8
		public RawImItemList Execute()
		{
			return this.unifiedContactStoreUtilities.GetImItems(this.contactIds, this.groupIds, this.extendedProperties);
		}

		// Token: 0x04000ED9 RID: 3801
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04000EDA RID: 3802
		private readonly StoreId[] contactIds;

		// Token: 0x04000EDB RID: 3803
		private readonly StoreId[] groupIds;

		// Token: 0x04000EDC RID: 3804
		private readonly ExtendedPropertyUri[] extendedProperties;
	}
}
