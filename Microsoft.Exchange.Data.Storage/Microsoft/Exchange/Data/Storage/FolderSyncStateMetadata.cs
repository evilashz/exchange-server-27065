using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E70 RID: 3696
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderSyncStateMetadata : SyncStateMetadata
	{
		// Token: 0x06008015 RID: 32789 RVA: 0x002308FC File Offset: 0x0022EAFC
		public FolderSyncStateMetadata(DeviceSyncStateMetadata parentDevice, string name, StoreObjectId folderSyncStateId, StoreObjectId itemSyncStateId, StoreObjectId ipmFolderId) : this(parentDevice, name, folderSyncStateId, itemSyncStateId, 0L, 0, 0, false, 0, 0L, 0, 0, ipmFolderId)
		{
		}

		// Token: 0x1700221F RID: 8735
		// (get) Token: 0x06008016 RID: 32790 RVA: 0x00230920 File Offset: 0x0022EB20
		public bool HasValidNullSyncData
		{
			get
			{
				return this.AirSyncLocalCommitTime != 0L || this.AirSyncDeletedCountTotal != 0 || this.AirSyncSyncKey != 0 || this.AirSyncConversationMode || this.AirSyncFilter != 0 || this.AirSyncSettingsHash != 0 || this.AirSyncMaxItems != 0;
			}
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x00230970 File Offset: 0x0022EB70
		public FolderSyncStateMetadata(DeviceSyncStateMetadata parentDevice, string name, StoreObjectId folderSyncStateId, StoreObjectId itemSyncStateId, long localCommitTimeMax, int deletedCountTotal, int syncKey, bool conversationMode, int airSyncFilter, long airSyncLastSyncTime, int airSyncSettingsHash, int airSyncMaxItems, StoreObjectId ipmFolderId) : base(parentDevice, name, folderSyncStateId, itemSyncStateId)
		{
			ArgumentValidator.ThrowIfNull("ipmFolderId", ipmFolderId);
			ArgumentValidator.ThrowIfNull("parentDevice", parentDevice);
			this.IPMFolderId = ipmFolderId;
			this.AirSyncLocalCommitTime = localCommitTimeMax;
			this.AirSyncDeletedCountTotal = deletedCountTotal;
			this.AirSyncSyncKey = syncKey;
			this.AirSyncConversationMode = conversationMode;
			this.AirSyncFilter = airSyncFilter;
			this.AirSyncLastSyncTime = airSyncLastSyncTime;
			this.AirSyncSettingsHash = airSyncSettingsHash;
			this.AirSyncMaxItems = airSyncMaxItems;
		}

		// Token: 0x17002220 RID: 8736
		// (get) Token: 0x06008018 RID: 32792 RVA: 0x002309F2 File Offset: 0x0022EBF2
		// (set) Token: 0x06008019 RID: 32793 RVA: 0x002309FC File Offset: 0x0022EBFC
		public StoreObjectId IPMFolderId
		{
			get
			{
				return this.ipmFolderId;
			}
			private set
			{
				StoreObjectId oldId;
				lock (this.instanceLock)
				{
					oldId = this.ipmFolderId;
					this.ipmFolderId = value;
				}
				base.ParentDevice.ChangeIPMFolderId(this, oldId, null);
			}
		}

		// Token: 0x17002221 RID: 8737
		// (get) Token: 0x0600801A RID: 32794 RVA: 0x00230A54 File Offset: 0x0022EC54
		// (set) Token: 0x0600801B RID: 32795 RVA: 0x00230A5C File Offset: 0x0022EC5C
		public long AirSyncLocalCommitTime { get; private set; }

		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x0600801C RID: 32796 RVA: 0x00230A65 File Offset: 0x0022EC65
		// (set) Token: 0x0600801D RID: 32797 RVA: 0x00230A6D File Offset: 0x0022EC6D
		public int AirSyncDeletedCountTotal { get; private set; }

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x0600801E RID: 32798 RVA: 0x00230A76 File Offset: 0x0022EC76
		// (set) Token: 0x0600801F RID: 32799 RVA: 0x00230A7E File Offset: 0x0022EC7E
		public int AirSyncSyncKey { get; private set; }

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x06008020 RID: 32800 RVA: 0x00230A87 File Offset: 0x0022EC87
		// (set) Token: 0x06008021 RID: 32801 RVA: 0x00230A8F File Offset: 0x0022EC8F
		public bool AirSyncConversationMode { get; private set; }

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x06008022 RID: 32802 RVA: 0x00230A98 File Offset: 0x0022EC98
		// (set) Token: 0x06008023 RID: 32803 RVA: 0x00230AA0 File Offset: 0x0022ECA0
		public int AirSyncFilter { get; private set; }

		// Token: 0x17002226 RID: 8742
		// (get) Token: 0x06008024 RID: 32804 RVA: 0x00230AA9 File Offset: 0x0022ECA9
		// (set) Token: 0x06008025 RID: 32805 RVA: 0x00230AB1 File Offset: 0x0022ECB1
		public long AirSyncLastSyncTime { get; private set; }

		// Token: 0x17002227 RID: 8743
		// (get) Token: 0x06008026 RID: 32806 RVA: 0x00230ABA File Offset: 0x0022ECBA
		// (set) Token: 0x06008027 RID: 32807 RVA: 0x00230AC2 File Offset: 0x0022ECC2
		public int AirSyncSettingsHash { get; private set; }

		// Token: 0x17002228 RID: 8744
		// (get) Token: 0x06008028 RID: 32808 RVA: 0x00230ACB File Offset: 0x0022ECCB
		// (set) Token: 0x06008029 RID: 32809 RVA: 0x00230AD3 File Offset: 0x0022ECD3
		public int AirSyncMaxItems { get; private set; }

		// Token: 0x0600802A RID: 32810 RVA: 0x00230ADC File Offset: 0x0022ECDC
		public void UpdateRecipientInfoCacheNullSyncValues(long airSyncLocalCommitTime, int airSyncSyncKey, int airSyncMaxItems)
		{
			lock (this.instanceLock)
			{
				this.AirSyncLocalCommitTime = airSyncLocalCommitTime;
				this.AirSyncSyncKey = airSyncSyncKey;
				this.AirSyncMaxItems = airSyncMaxItems;
			}
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x00230B2C File Offset: 0x0022ED2C
		public void UpdateSyncCollectionNullSyncValues(bool conversationMode, int deletedCountTotal, int filter, long lastSyncTime, long localCommitTime, int settingsHash, int syncKey)
		{
			lock (this.instanceLock)
			{
				this.AirSyncConversationMode = conversationMode;
				this.AirSyncDeletedCountTotal = deletedCountTotal;
				this.AirSyncFilter = filter;
				this.AirSyncLastSyncTime = lastSyncTime;
				this.AirSyncLocalCommitTime = localCommitTime;
				this.AirSyncSettingsHash = settingsHash;
				this.AirSyncSyncKey = syncKey;
			}
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x00230B9C File Offset: 0x0022ED9C
		public IStorePropertyBag GetNullSyncPropertiesFromIPMFolder(MailboxSession mailboxSession)
		{
			IStorePropertyBag result;
			using (Folder folder = Folder.Bind(mailboxSession, this.IPMFolderId, FolderSyncStateMetadata.IPMFolderNullSyncProperties))
			{
				result = folder.PropertyBag.AsIStorePropertyBag();
			}
			return result;
		}

		// Token: 0x0400567F RID: 22143
		private StoreObjectId ipmFolderId;

		// Token: 0x04005680 RID: 22144
		private object instanceLock = new object();

		// Token: 0x04005681 RID: 22145
		public static readonly PropertyDefinition[] IPMFolderNullSyncProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.ChangeKey,
			FolderSchema.IsHidden,
			FolderSchema.ExtendedFolderFlags,
			FolderSchema.LocalCommitTimeMax,
			FolderSchema.DeletedCountTotal,
			StoreObjectSchema.ContainerClass,
			FolderSchema.DisplayName
		};
	}
}
