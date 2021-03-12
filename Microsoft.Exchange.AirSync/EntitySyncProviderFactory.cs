using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000271 RID: 625
	internal class EntitySyncProviderFactory : MailboxSyncProviderFactory
	{
		// Token: 0x0600174C RID: 5964 RVA: 0x0008AD2A File Offset: 0x00088F2A
		public EntitySyncProviderFactory(StoreSession storeSession) : this(storeSession, null, false)
		{
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0008AD35 File Offset: 0x00088F35
		public EntitySyncProviderFactory(StoreSession storeSession, StoreObjectId folderId) : this(storeSession, folderId, false)
		{
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0008AD40 File Offset: 0x00088F40
		public EntitySyncProviderFactory(StoreSession storeSession, bool allowTableRestrict) : this(storeSession, null, allowTableRestrict)
		{
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0008AD4B File Offset: 0x00088F4B
		public EntitySyncProviderFactory(StoreSession storeSession, StoreObjectId folderId, bool allowTableRestrict) : base(storeSession, folderId, allowTableRestrict)
		{
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0008AD56 File Offset: 0x00088F56
		protected override ISyncProvider CreateSyncProvider(Folder folder, bool trackReadFlagChanges, bool trackAssociatedMessageChanges, bool returnNewestChangesFirst, bool trackConversations, bool allowTableRestrict, bool disposeFolder, ISyncLogger syncLogger = null)
		{
			return new EntitySyncProvider(folder, trackReadFlagChanges, trackAssociatedMessageChanges, returnNewestChangesFirst, trackConversations, allowTableRestrict, disposeFolder);
		}
	}
}
