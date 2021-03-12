using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E62 RID: 3682
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSyncProviderFactory : ISyncProviderFactory
	{
		// Token: 0x06007F8E RID: 32654 RVA: 0x0022E949 File Offset: 0x0022CB49
		public MailboxSyncProviderFactory(StoreSession storeSession, StoreObjectId folderId, bool allowTableRestrict)
		{
			this.storeSession = storeSession;
			this.folderId = folderId;
			this.allowTableRestrict = allowTableRestrict;
		}

		// Token: 0x06007F8F RID: 32655 RVA: 0x0022E966 File Offset: 0x0022CB66
		public MailboxSyncProviderFactory(StoreSession storeSession, bool allowTableRestrict)
		{
			this.storeSession = storeSession;
			this.folderId = null;
			this.allowTableRestrict = allowTableRestrict;
		}

		// Token: 0x06007F90 RID: 32656 RVA: 0x0022E983 File Offset: 0x0022CB83
		public MailboxSyncProviderFactory(StoreSession storeSession, StoreObjectId folderId)
		{
			this.storeSession = storeSession;
			this.folderId = folderId;
			this.allowTableRestrict = false;
		}

		// Token: 0x06007F91 RID: 32657 RVA: 0x0022E9A0 File Offset: 0x0022CBA0
		public MailboxSyncProviderFactory(StoreSession storeSession)
		{
			this.storeSession = storeSession;
			this.folderId = null;
			this.allowTableRestrict = false;
		}

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x06007F92 RID: 32658 RVA: 0x0022E9BD File Offset: 0x0022CBBD
		// (set) Token: 0x06007F93 RID: 32659 RVA: 0x0022E9C5 File Offset: 0x0022CBC5
		public QueryFilter IcsPropertyGroupFilter { get; set; }

		// Token: 0x17002203 RID: 8707
		// (get) Token: 0x06007F94 RID: 32660 RVA: 0x0022E9CE File Offset: 0x0022CBCE
		// (set) Token: 0x06007F95 RID: 32661 RVA: 0x0022E9D6 File Offset: 0x0022CBD6
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
			set
			{
				this.folder = value;
			}
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x06007F96 RID: 32662 RVA: 0x0022E9DF File Offset: 0x0022CBDF
		// (set) Token: 0x06007F97 RID: 32663 RVA: 0x0022E9E7 File Offset: 0x0022CBE7
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x17002205 RID: 8709
		// (get) Token: 0x06007F98 RID: 32664 RVA: 0x0022E9F0 File Offset: 0x0022CBF0
		// (set) Token: 0x06007F99 RID: 32665 RVA: 0x0022E9F8 File Offset: 0x0022CBF8
		public StoreSession StoreSession
		{
			get
			{
				return this.storeSession;
			}
			set
			{
				this.storeSession = value;
			}
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x0022EA01 File Offset: 0x0022CC01
		public void GenerateReadFlagChanges()
		{
			this.trackReadFlagChanges = true;
		}

		// Token: 0x06007F9B RID: 32667 RVA: 0x0022EA0A File Offset: 0x0022CC0A
		public void GenerateAssociatedMessageChanges()
		{
			this.trackAssociatedMessageChanges = true;
		}

		// Token: 0x06007F9C RID: 32668 RVA: 0x0022EA13 File Offset: 0x0022CC13
		public void ReturnNewestChangesFirst()
		{
			this.returnNewestChangesFirst = true;
		}

		// Token: 0x06007F9D RID: 32669 RVA: 0x0022EA1C File Offset: 0x0022CC1C
		public void GenerateConversationChanges()
		{
			this.trackConversations = true;
		}

		// Token: 0x06007F9E RID: 32670 RVA: 0x0022EA28 File Offset: 0x0022CC28
		public virtual ISyncProvider CreateSyncProvider(ISyncLogger syncLogger = null)
		{
			Folder folder;
			if (this.folder != null)
			{
				folder = this.folder;
				this.folder = null;
			}
			else
			{
				folder = Folder.Bind(this.storeSession, this.folderId);
			}
			return this.CreateSyncProvider(folder, this.trackReadFlagChanges, this.trackAssociatedMessageChanges, this.returnNewestChangesFirst, this.trackConversations, this.allowTableRestrict, true, syncLogger);
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x0022EA88 File Offset: 0x0022CC88
		public virtual ISyncProvider CreateSyncProvider(Folder folder, ISyncLogger syncLogger = null)
		{
			return this.CreateSyncProvider(folder, this.trackReadFlagChanges, this.trackAssociatedMessageChanges, this.returnNewestChangesFirst, this.trackConversations, this.allowTableRestrict, false, syncLogger);
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x0022EABC File Offset: 0x0022CCBC
		protected virtual ISyncProvider CreateSyncProvider(Folder folder, bool trackReadFlagChanges, bool trackAssociatedMessageChanges, bool returnNewestChangesFirst, bool trackConversations, bool allowTableRestrict, bool disposeFolder, ISyncLogger syncLogger = null)
		{
			MailboxSyncProvider mailboxSyncProvider = MailboxSyncProvider.Bind(folder, trackReadFlagChanges, trackAssociatedMessageChanges, returnNewestChangesFirst, trackConversations, allowTableRestrict, disposeFolder, syncLogger);
			mailboxSyncProvider.IcsPropertyGroupFilter = this.IcsPropertyGroupFilter;
			return mailboxSyncProvider;
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x0022EAE9 File Offset: 0x0022CCE9
		public byte[] GetCollectionIdBytes()
		{
			return this.folderId.GetBytes();
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x0022EAF6 File Offset: 0x0022CCF6
		public void SetCollectionIdFromBytes(byte[] collectionBytes)
		{
			this.folderId = StoreObjectId.Deserialize(collectionBytes);
		}

		// Token: 0x0400564A RID: 22090
		protected bool allowTableRestrict;

		// Token: 0x0400564B RID: 22091
		protected Folder folder;

		// Token: 0x0400564C RID: 22092
		protected StoreObjectId folderId;

		// Token: 0x0400564D RID: 22093
		protected StoreSession storeSession;

		// Token: 0x0400564E RID: 22094
		protected bool trackReadFlagChanges;

		// Token: 0x0400564F RID: 22095
		protected bool trackAssociatedMessageChanges;

		// Token: 0x04005650 RID: 22096
		protected bool returnNewestChangesFirst;

		// Token: 0x04005651 RID: 22097
		protected bool trackConversations;
	}
}
