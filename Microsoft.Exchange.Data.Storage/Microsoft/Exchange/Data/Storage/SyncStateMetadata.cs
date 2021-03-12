using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E6F RID: 3695
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncStateMetadata
	{
		// Token: 0x0600800A RID: 32778 RVA: 0x002307F8 File Offset: 0x0022E9F8
		public SyncStateMetadata(DeviceSyncStateMetadata parentDevice, string name, StoreObjectId folderSyncStateId, StoreObjectId itemSyncStateId)
		{
			ArgumentValidator.ThrowIfNull("parentDevice", parentDevice);
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			this.ParentDevice = parentDevice;
			this.Name = name;
			this.FolderSyncStateId = folderSyncStateId;
			this.ItemSyncStateId = itemSyncStateId;
			this.ParentDevice.TryAdd(this, null);
		}

		// Token: 0x1700221A RID: 8730
		// (get) Token: 0x0600800B RID: 32779 RVA: 0x0023084C File Offset: 0x0022EA4C
		// (set) Token: 0x0600800C RID: 32780 RVA: 0x00230854 File Offset: 0x0022EA54
		public DeviceSyncStateMetadata ParentDevice { get; private set; }

		// Token: 0x1700221B RID: 8731
		// (get) Token: 0x0600800D RID: 32781 RVA: 0x0023085D File Offset: 0x0022EA5D
		// (set) Token: 0x0600800E RID: 32782 RVA: 0x00230865 File Offset: 0x0022EA65
		public string Name { get; private set; }

		// Token: 0x1700221C RID: 8732
		// (get) Token: 0x0600800F RID: 32783 RVA: 0x0023086E File Offset: 0x0022EA6E
		// (set) Token: 0x06008010 RID: 32784 RVA: 0x00230876 File Offset: 0x0022EA76
		public StoreObjectId FolderSyncStateId { get; set; }

		// Token: 0x1700221D RID: 8733
		// (get) Token: 0x06008011 RID: 32785 RVA: 0x0023087F File Offset: 0x0022EA7F
		// (set) Token: 0x06008012 RID: 32786 RVA: 0x00230887 File Offset: 0x0022EA87
		public StoreObjectId ItemSyncStateId { get; set; }

		// Token: 0x1700221E RID: 8734
		// (get) Token: 0x06008013 RID: 32787 RVA: 0x00230890 File Offset: 0x0022EA90
		public StorageType StorageType
		{
			get
			{
				if (this.ItemSyncStateId == null)
				{
					return StorageType.Folder;
				}
				if (this.FolderSyncStateId != null)
				{
					return StorageType.Item;
				}
				return StorageType.DirectItem;
			}
		}

		// Token: 0x06008014 RID: 32788 RVA: 0x002308A8 File Offset: 0x0022EAA8
		public override string ToString()
		{
			StoreObjectId storeObjectId = (this.StorageType == StorageType.Folder) ? this.FolderSyncStateId : this.ItemSyncStateId;
			string arg = (storeObjectId == null) ? "NULL" : storeObjectId.ToBase64String();
			return string.Format("{0}[{1}]- {2}", this.Name, this.StorageType, arg);
		}
	}
}
