using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200007A RID: 122
	internal class FolderTupleCrossServerArchive : FolderTuple
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00020219 File Offset: 0x0001E419
		internal Folder Folder
		{
			get
			{
				return this.ewsFolder;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00020221 File Offset: 0x0001E421
		internal FolderId EwsParentId
		{
			get
			{
				return this.ewsParentId;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00020229 File Offset: 0x0001E429
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00020231 File Offset: 0x0001E431
		internal FolderId EwsFolderId
		{
			get
			{
				return this.ewsFolderId;
			}
			set
			{
				this.ewsFolderId = value;
				base.FolderId = StoreId.EwsIdToStoreObjectId(this.ewsFolderId.UniqueId);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00020250 File Offset: 0x0001E450
		internal FolderTupleCrossServerArchive(Folder folder, FolderId folderId, FolderId parentId, string displayName, object[] extendedProps) : base((folderId != null) ? StoreId.EwsIdToStoreObjectId(folderId.UniqueId) : null, (parentId != null) ? StoreId.EwsIdToStoreObjectId(parentId.UniqueId) : null, displayName, extendedProps)
		{
			this.ewsFolder = folder;
			this.ewsFolderId = folderId;
			this.ewsParentId = parentId;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0002029E File Offset: 0x0001E49E
		internal FolderTupleCrossServerArchive(Folder folder, FolderId folderId, FolderId parentId, string displayName, object[] extendedProps, bool isRoot) : base(StoreId.EwsIdToStoreObjectId(folderId.UniqueId), StoreId.EwsIdToStoreObjectId(parentId.UniqueId), displayName, extendedProps, isRoot)
		{
			this.ewsFolder = folder;
			this.ewsFolderId = folderId;
			this.ewsParentId = parentId;
		}

		// Token: 0x0400038D RID: 909
		private readonly FolderId ewsParentId;

		// Token: 0x0400038E RID: 910
		private readonly Folder ewsFolder;

		// Token: 0x0400038F RID: 911
		private FolderId ewsFolderId;
	}
}
