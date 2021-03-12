using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E75 RID: 3701
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskGroupEntryInfo : FolderTreeDataInfo
	{
		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x06008085 RID: 32901 RVA: 0x00232772 File Offset: 0x00230972
		// (set) Token: 0x06008086 RID: 32902 RVA: 0x0023277A File Offset: 0x0023097A
		public string FolderName { get; private set; }

		// Token: 0x1700223B RID: 8763
		// (get) Token: 0x06008087 RID: 32903 RVA: 0x00232783 File Offset: 0x00230983
		// (set) Token: 0x06008088 RID: 32904 RVA: 0x0023278B File Offset: 0x0023098B
		public StoreObjectId TaskFolderId { get; private set; }

		// Token: 0x1700223C RID: 8764
		// (get) Token: 0x06008089 RID: 32905 RVA: 0x00232794 File Offset: 0x00230994
		// (set) Token: 0x0600808A RID: 32906 RVA: 0x0023279C File Offset: 0x0023099C
		public Guid ParentGroupClassId { get; private set; }

		// Token: 0x0600808B RID: 32907 RVA: 0x002327A5 File Offset: 0x002309A5
		public TaskGroupEntryInfo(string folderName, VersionedId id, StoreObjectId taskFolderId, Guid parentGroupId, byte[] taskFolderOrdinal, ExDateTime lastModifiedTime) : base(id, taskFolderOrdinal, lastModifiedTime)
		{
			Util.ThrowOnNullArgument(folderName, "folderName");
			this.TaskFolderId = taskFolderId;
			this.FolderName = folderName;
			this.ParentGroupClassId = parentGroupId;
		}
	}
}
