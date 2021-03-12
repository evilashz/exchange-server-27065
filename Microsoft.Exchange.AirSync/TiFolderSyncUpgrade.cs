using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200028D RID: 653
	internal class TiFolderSyncUpgrade
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0008D484 File Offset: 0x0008B684
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x0008D48C File Offset: 0x0008B68C
		public MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
			set
			{
				this.mailboxSession = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0008D495 File Offset: 0x0008B695
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x0008D49D File Offset: 0x0008B69D
		public SyncStateStorage SyncStateStorage
		{
			get
			{
				return this.syncStateStorage;
			}
			set
			{
				this.syncStateStorage = value;
			}
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0008D4A8 File Offset: 0x0008B6A8
		public void UpdateLastFolderId(int folderId)
		{
			FolderIdMappingSyncStateInfo syncStateInfo = new FolderIdMappingSyncStateInfo();
			this.folderIdMappingSyncState = this.syncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]);
			FolderIdMapping folderIdMapping = (FolderIdMapping)this.folderIdMappingSyncState[CustomStateDatumType.IdMapping];
			folderIdMapping.IncreaseCounterTo((long)folderId);
			this.folderIdMappingSyncState.Commit();
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0008D4FC File Offset: 0x0008B6FC
		public Dictionary<string, StoreObjectId> Upgrade(Dictionary<string, FolderNode> folders, string synckeyIn, DeviceIdentity deviceIdentityIn, uint version, out Dictionary<string, StoreObjectType> contentTypeTable)
		{
			if (synckeyIn == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiFolderSyncUpgrade.Upgrade is null: field synckeyIn");
				throw new ArgumentNullException("synckeyIn");
			}
			if (deviceIdentityIn == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiFolderSyncUpgrade.Upgrade is null: field deviceIdentityIn");
				throw new ArgumentNullException("deviceIdentityIn");
			}
			this.deviceIdentity = deviceIdentityIn;
			this.folderIdMappingSyncState = null;
			Dictionary<string, StoreObjectId> dictionary = new Dictionary<string, StoreObjectId>();
			try
			{
				this.LoadE12SyncState();
				this.folderHierarchySync = this.syncState.GetFolderHierarchySync();
				FolderSyncUpgrade folderSyncUpgrade = new FolderSyncUpgrade(this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Root));
				dictionary = folderSyncUpgrade.Upgrade(this.folderHierarchySync, this.syncState, folders, out contentTypeTable);
				string text = synckeyIn;
				int num = text.LastIndexOf("}");
				if (num >= 0)
				{
					text = text.Substring(num + 1, text.Length - num - 1);
				}
				if (text.Length > 0)
				{
					int data = int.Parse(text, CultureInfo.InvariantCulture);
					this.syncState[CustomStateDatumType.SyncKey] = new Int32Data(data);
				}
				this.UpdateMapping(dictionary);
				AirSyncSyncStateTypeFactory.EnsureSyncStateTypesRegistered();
				this.syncState[CustomStateDatumType.AirSyncProtocolVersion] = new Int32Data((int)version);
				this.folderIdMappingSyncState.Commit();
				if (folders != null)
				{
					this.syncState.Commit();
				}
				contentTypeTable["EmailSyncFile"] = StoreObjectType.Folder;
				contentTypeTable["ContactsSyncFile"] = StoreObjectType.ContactsFolder;
				contentTypeTable["CalendarSyncFile"] = StoreObjectType.CalendarFolder;
				contentTypeTable["TasksSyncFile"] = StoreObjectType.TasksFolder;
			}
			finally
			{
				if (this.syncState != null)
				{
					this.syncState.Dispose();
				}
			}
			return dictionary;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0008D694 File Offset: 0x0008B894
		public void Close()
		{
			if (this.mailboxSession != null)
			{
				this.mailboxSession = null;
			}
			if (this.syncStateStorage != null)
			{
				this.syncStateStorage.Dispose();
				this.syncStateStorage = null;
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0008D6C0 File Offset: 0x0008B8C0
		private void LoadE12SyncState()
		{
			FolderIdMappingSyncStateInfo folderIdMappingSyncStateInfo = new FolderIdMappingSyncStateInfo();
			this.syncStateStorage = SyncStateStorage.Create(this.mailboxSession, this.deviceIdentity, StateStorageFeatures.ContentState, true, null);
			this.syncStateStorage.DeleteAllSyncStates();
			this.folderIdMappingSyncState = this.syncStateStorage.GetCustomSyncState(folderIdMappingSyncStateInfo, new PropertyDefinition[0]);
			if (this.folderIdMappingSyncState == null || this.folderIdMappingSyncState[CustomStateDatumType.IdMapping] == null)
			{
				this.syncStateStorage.DeleteFolderHierarchySyncState();
				this.folderIdMappingSyncState = this.syncStateStorage.CreateCustomSyncState(folderIdMappingSyncStateInfo);
				this.folderIdMappingSyncState[CustomStateDatumType.IdMapping] = new FolderIdMapping();
			}
			else
			{
				this.syncStateStorage.DeleteFolderHierarchySyncState();
			}
			this.folderIdMappingSyncState[SyncStateProp.Version] = new NullableData<Int32Data, int>(new int?(2));
			folderIdMappingSyncStateInfo.HandleSyncStateVersioning(this.folderIdMappingSyncState);
			this.syncState = this.syncStateStorage.CreateFolderHierarchySyncState();
			this.syncState.CustomVersion = new int?(2);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0008D7B8 File Offset: 0x0008B9B8
		private void UpdateMapping(Dictionary<string, StoreObjectId> mapping)
		{
			FolderIdMapping folderIdMapping = (FolderIdMapping)this.folderIdMappingSyncState[CustomStateDatumType.IdMapping];
			Dictionary<StoreObjectId, FolderStateEntry> data = ((GenericDictionaryData<StoreObjectIdData, StoreObjectId, FolderStateEntry>)this.syncState[SyncStateProp.ClientState]).Data;
			foreach (string text in mapping.Keys)
			{
				StoreObjectId storeObjectId = mapping[text];
				folderIdMapping.Add(MailboxSyncItemId.CreateForNewItem(storeObjectId), text);
				if (data.ContainsKey(storeObjectId))
				{
					FolderStateEntry folderStateEntry = data[storeObjectId];
					if (folderStateEntry.ChangeKey.Length == 1 && folderStateEntry.ChangeKey[0] == 1)
					{
						folderStateEntry.ChangeTrackingHash = "blah".GetHashCode();
					}
					else
					{
						folderStateEntry.ChangeTrackingHash = FolderCommand.ComputeChangeTrackingHash(this.mailboxSession, mapping[text], null);
					}
				}
			}
			folderIdMapping.CommitChanges();
			StoreObjectId defaultFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			mapping.Add("EmailSyncFile", defaultFolderId);
			defaultFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Contacts);
			mapping.Add("ContactsSyncFile", defaultFolderId);
			defaultFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
			mapping.Add("CalendarSyncFile", defaultFolderId);
			defaultFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Tasks);
			mapping.Add("TasksSyncFile", defaultFolderId);
		}

		// Token: 0x04000EB2 RID: 3762
		private MailboxSession mailboxSession;

		// Token: 0x04000EB3 RID: 3763
		private SyncStateStorage syncStateStorage;

		// Token: 0x04000EB4 RID: 3764
		private FolderHierarchySyncState syncState;

		// Token: 0x04000EB5 RID: 3765
		private FolderHierarchySync folderHierarchySync;

		// Token: 0x04000EB6 RID: 3766
		private CustomSyncState folderIdMappingSyncState;

		// Token: 0x04000EB7 RID: 3767
		private DeviceIdentity deviceIdentity;
	}
}
