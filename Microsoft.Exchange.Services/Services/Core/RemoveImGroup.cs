using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200035C RID: 860
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoveImGroup
	{
		// Token: 0x0600181D RID: 6173 RVA: 0x00081A58 File Offset: 0x0007FC58
		public RemoveImGroup(IMailboxSession session, StoreId groupId, IXSOFactory xsoFactory)
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

		// Token: 0x0600181E RID: 6174 RVA: 0x00081AA9 File Offset: 0x0007FCA9
		public void Execute()
		{
			this.unifiedContactStoreUtilities.RemoveImGroup(this.groupId);
		}

		// Token: 0x04001025 RID: 4133
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04001026 RID: 4134
		private readonly StoreId groupId;
	}
}
