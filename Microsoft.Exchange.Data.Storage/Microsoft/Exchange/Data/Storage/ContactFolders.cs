using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004A3 RID: 1187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContactFolders
	{
		// Token: 0x060034C1 RID: 13505 RVA: 0x000D5607 File Offset: 0x000D3807
		internal ContactFolders(MyContactFolders myContactFolders, StoreObjectId myContactsSearchFolderId, StoreObjectId quickContactsFolderId, StoreObjectId favoritesFolderId)
		{
			this.myContactFolders = myContactFolders;
			this.myContactsSearchFolderId = myContactsSearchFolderId;
			this.quickContactsFolderId = quickContactsFolderId;
			this.favoritesFolderId = favoritesFolderId;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000D562C File Offset: 0x000D382C
		internal static ContactFolders Load(IXSOFactory xsoFactory, IMailboxSession mailboxSession)
		{
			MyContactFolders myContactFolders = new MyContactFolders(xsoFactory, mailboxSession);
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.MyContacts);
			StoreObjectId defaultFolderId2 = mailboxSession.GetDefaultFolderId(DefaultFolderType.QuickContacts);
			StoreObjectId defaultFolderId3 = mailboxSession.GetDefaultFolderId(DefaultFolderType.Favorites);
			return new ContactFolders(myContactFolders, defaultFolderId, defaultFolderId2, defaultFolderId3);
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x000D5665 File Offset: 0x000D3865
		public MyContactFolders MyContactFolders
		{
			get
			{
				return this.myContactFolders;
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x000D566D File Offset: 0x000D386D
		public StoreObjectId MyContactsSearchFolderId
		{
			get
			{
				return this.myContactsSearchFolderId;
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x000D5675 File Offset: 0x000D3875
		public StoreObjectId QuickContactsFolderId
		{
			get
			{
				return this.quickContactsFolderId;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000D567D File Offset: 0x000D387D
		public StoreObjectId FavoritesFolderId
		{
			get
			{
				return this.favoritesFolderId;
			}
		}

		// Token: 0x04001C11 RID: 7185
		private readonly MyContactFolders myContactFolders;

		// Token: 0x04001C12 RID: 7186
		private readonly StoreObjectId myContactsSearchFolderId;

		// Token: 0x04001C13 RID: 7187
		private readonly StoreObjectId quickContactsFolderId;

		// Token: 0x04001C14 RID: 7188
		private readonly StoreObjectId favoritesFolderId;
	}
}
