using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200007B RID: 123
	internal class FolderTupleRemoteArchive : FolderTuple
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000202D7 File Offset: 0x0001E4D7
		internal BaseFolderType Folder
		{
			get
			{
				return this.ewsFolder;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000202DF File Offset: 0x0001E4DF
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x000202E7 File Offset: 0x0001E4E7
		internal FolderIdType EwsFolderId
		{
			get
			{
				return this.ewsFolderId;
			}
			set
			{
				this.ewsFolderId = value;
				base.FolderId = StoreId.EwsIdToStoreObjectId(this.ewsFolderId.Id);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00020306 File Offset: 0x0001E506
		internal FolderIdType EwsParentId
		{
			get
			{
				return this.ewsParentId;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00020310 File Offset: 0x0001E510
		internal FolderTupleRemoteArchive(BaseFolderType folder, FolderIdType folderId, FolderIdType parentId, string displayName, object[] extendedProps) : base((folderId != null) ? StoreId.EwsIdToStoreObjectId(folderId.Id) : null, (parentId != null) ? StoreId.EwsIdToStoreObjectId(parentId.Id) : null, displayName, extendedProps)
		{
			this.ewsFolder = folder;
			this.ewsFolderId = folderId;
			this.ewsParentId = parentId;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0002035E File Offset: 0x0001E55E
		internal FolderTupleRemoteArchive(BaseFolderType folder, FolderIdType folderId, FolderIdType parentId, string displayName, object[] extendedProps, bool isRoot) : base(StoreId.EwsIdToStoreObjectId(folderId.Id), StoreId.EwsIdToStoreObjectId(parentId.Id), displayName, extendedProps, isRoot)
		{
			this.ewsFolder = folder;
			this.ewsFolderId = folderId;
			this.ewsParentId = parentId;
		}

		// Token: 0x04000390 RID: 912
		private readonly BaseFolderType ewsFolder;

		// Token: 0x04000391 RID: 913
		private FolderIdType ewsFolderId;

		// Token: 0x04000392 RID: 914
		private readonly FolderIdType ewsParentId;
	}
}
