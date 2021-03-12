using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000376 RID: 886
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SetImGroup
	{
		// Token: 0x060018D4 RID: 6356 RVA: 0x00089128 File Offset: 0x00087328
		internal SetImGroup(IMailboxSession session, StoreId groupId, string newDisplayName, IXSOFactory xsoFactory)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (groupId == null)
			{
				throw new ArgumentNullException("groupId");
			}
			if (newDisplayName == null)
			{
				throw new ArgumentNullException("newDisplayName");
			}
			if (newDisplayName.Length == 0)
			{
				throw new ArgumentException("newDisplayName that was passed in was empty.", "newDisplayName");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(session, xsoFactory);
			this.groupId = groupId;
			this.newDisplayName = newDisplayName;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000891A8 File Offset: 0x000873A8
		internal void Execute()
		{
			this.unifiedContactStoreUtilities.ModifyGroup(this.groupId, this.newDisplayName);
		}

		// Token: 0x040010A5 RID: 4261
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x040010A6 RID: 4262
		private readonly StoreId groupId;

		// Token: 0x040010A7 RID: 4263
		private readonly string newDisplayName;
	}
}
