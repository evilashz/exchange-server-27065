using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E6A RID: 3690
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeviceSyncStateMetadata
	{
		// Token: 0x06007FD2 RID: 32722 RVA: 0x0022F4B0 File Offset: 0x0022D6B0
		public DeviceSyncStateMetadata(MailboxSession mailboxSession, StoreObjectId syncStateFolderId, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("syncStateFolderId", syncStateFolderId);
			this.DeviceFolderId = syncStateFolderId;
			this.LastAccessUtc = ExDateTime.UtcNow;
			using (Folder folder = Folder.Bind(mailboxSession, syncStateFolderId, new PropertyDefinition[]
			{
				FolderSchema.ItemCount,
				FolderSchema.ChildCount
			}))
			{
				this.Id = new DeviceIdentity(folder.DisplayName);
				if (folder.ItemCount > 0)
				{
					this.ProcessSyncStateItems(folder, syncLogger);
				}
				if (folder.HasSubfolders)
				{
					this.ProcessSyncStateFolders(folder, syncLogger);
				}
			}
		}

		// Token: 0x06007FD3 RID: 32723 RVA: 0x0022F584 File Offset: 0x0022D784
		private void ProcessSyncStateItems(Folder deviceFolder, ISyncLogger syncLogger)
		{
			using (QueryResult queryResult = deviceFolder.ItemQuery(ItemQueryType.None, null, null, DeviceSyncStateMetadata.NullSyncPropertiesItems))
			{
				for (;;)
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(100);
					if (propertyBags == null || propertyBags.Length == 0)
					{
						break;
					}
					foreach (IStorePropertyBag storePropertyBag in propertyBags)
					{
						string displayName = (string)storePropertyBag.TryGetProperty(ItemSchema.Subject);
						SyncStateMetadata metadataFromPropertyBag = this.GetMetadataFromPropertyBag(storePropertyBag, displayName, syncLogger);
						if (metadataFromPropertyBag != null)
						{
							StoreObjectId objectId = ((VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
							metadataFromPropertyBag.FolderSyncStateId = null;
							metadataFromPropertyBag.ItemSyncStateId = objectId;
							syncLogger.TraceDebug<SyncStateMetadata>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.ProcessSyncStateItems] Found Item SyncState: {0}", metadataFromPropertyBag);
							this.syncStateMap.TryAdd(metadataFromPropertyBag.Name, metadataFromPropertyBag);
						}
						else
						{
							syncLogger.TraceDebug<DeviceIdentity>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.ProcessSyncStateItems] Discovered unusable sync state for device {0}", this.Id);
						}
					}
				}
			}
		}

		// Token: 0x06007FD4 RID: 32724 RVA: 0x0022F690 File Offset: 0x0022D890
		private void ProcessSyncStateFolders(Folder deviceFolder, ISyncLogger syncLogger)
		{
			using (QueryResult queryResult = deviceFolder.FolderQuery(FolderQueryFlags.None, null, null, DeviceSyncStateMetadata.NullSyncPropertiesFolders))
			{
				for (;;)
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(100);
					if (propertyBags == null || propertyBags.Length == 0)
					{
						break;
					}
					foreach (IStorePropertyBag propertyBag in propertyBags)
					{
						SyncStateMetadata metadataFromPropertyBag = this.GetMetadataFromPropertyBag(deviceFolder.Session as MailboxSession, propertyBag, syncLogger);
						if (metadataFromPropertyBag != null)
						{
							syncLogger.TraceDebug<SyncStateMetadata>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.ProcessSyncStateFolders] Found SyncState: {0}", metadataFromPropertyBag);
							this.syncStateMap.TryAdd(metadataFromPropertyBag.Name, metadataFromPropertyBag);
						}
						else
						{
							syncLogger.TraceDebug<DeviceIdentity>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.ProcessSyncStateFolders] Discovered unusable sync state for device {0}", this.Id);
						}
					}
				}
			}
		}

		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x06007FD5 RID: 32725 RVA: 0x0022F764 File Offset: 0x0022D964
		// (set) Token: 0x06007FD6 RID: 32726 RVA: 0x0022F76C File Offset: 0x0022D96C
		public FolderHierarchyChangeDetector.SyncHierarchyManifestState SyncHierarchyManifestState { get; private set; }

		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x06007FD7 RID: 32727 RVA: 0x0022F775 File Offset: 0x0022D975
		// (set) Token: 0x06007FD8 RID: 32728 RVA: 0x0022F77D File Offset: 0x0022D97D
		public object PingFolderList { get; set; }

		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x06007FD9 RID: 32729 RVA: 0x0022F786 File Offset: 0x0022D986
		public IDictionary<string, SyncStateMetadata> SyncStates
		{
			get
			{
				return this.syncStateMap;
			}
		}

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x06007FDA RID: 32730 RVA: 0x0022F78E File Offset: 0x0022D98E
		public IDictionary<StoreObjectId, FolderSyncStateMetadata> SyncStatesByIPMFolderId
		{
			get
			{
				return this.ipmToFolderSyncStateMap;
			}
		}

		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x06007FDB RID: 32731 RVA: 0x0022F796 File Offset: 0x0022D996
		// (set) Token: 0x06007FDC RID: 32732 RVA: 0x0022F79E File Offset: 0x0022D99E
		public ExDateTime LastAccessUtc { get; set; }

		// Token: 0x1700220F RID: 8719
		// (get) Token: 0x06007FDD RID: 32733 RVA: 0x0022F7A7 File Offset: 0x0022D9A7
		// (set) Token: 0x06007FDE RID: 32734 RVA: 0x0022F7AF File Offset: 0x0022D9AF
		public byte[] LastCachedSyncRequest { get; private set; }

		// Token: 0x17002210 RID: 8720
		// (get) Token: 0x06007FDF RID: 32735 RVA: 0x0022F7B8 File Offset: 0x0022D9B8
		// (set) Token: 0x06007FE0 RID: 32736 RVA: 0x0022F7C0 File Offset: 0x0022D9C0
		public string LastSyncRequestRandomString { get; private set; }

		// Token: 0x17002211 RID: 8721
		// (get) Token: 0x06007FE1 RID: 32737 RVA: 0x0022F7C9 File Offset: 0x0022D9C9
		// (set) Token: 0x06007FE2 RID: 32738 RVA: 0x0022F7D1 File Offset: 0x0022D9D1
		public bool ClientCanSendUpEmptyRequests { get; private set; }

		// Token: 0x06007FE3 RID: 32739 RVA: 0x0022F7DC File Offset: 0x0022D9DC
		public void SaveSyncStatusData(string lastSyncRequestRandomString, byte[] lastCachableWbxmlDocument, bool clientCanSendUpEmtpyRequests)
		{
			lock (this.syncStatusInstanceLock)
			{
				this.LastSyncRequestRandomString = lastSyncRequestRandomString;
				this.LastCachedSyncRequest = lastCachableWbxmlDocument;
				this.ClientCanSendUpEmptyRequests = clientCanSendUpEmtpyRequests;
			}
		}

		// Token: 0x17002212 RID: 8722
		// (get) Token: 0x06007FE4 RID: 32740 RVA: 0x0022F82C File Offset: 0x0022DA2C
		public int FolderSyncStateCount
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<string, SyncStateMetadata> keyValuePair in this.syncStateMap)
				{
					if (keyValuePair.Value is FolderSyncStateMetadata)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x0022F888 File Offset: 0x0022DA88
		public void RecordLatestFolderHierarchySnapshot(FolderHierarchyChangeDetector.SyncHierarchyManifestState lastState)
		{
			this.SyncHierarchyManifestState = lastState;
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x0022F894 File Offset: 0x0022DA94
		public void RecordLatestFolderHierarchySnapshot(MailboxSession mailboxSession, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			FolderHierarchyChangeDetector.SyncHierarchyManifestState syncHierarchyManifestState = this.SyncHierarchyManifestState;
			bool catchup = false;
			if (syncHierarchyManifestState == null)
			{
				syncLogger.TraceDebug(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.RecordLatestFolderHierarchySnapshot] Last ICS snapshot was null.  Doing a catchup sync.");
				syncHierarchyManifestState = new FolderHierarchyChangeDetector.SyncHierarchyManifestState();
				this.SyncHierarchyManifestState = syncHierarchyManifestState;
				catchup = true;
			}
			FolderHierarchyChangeDetector.RunICSManifestSync(catchup, syncHierarchyManifestState, mailboxSession, syncLogger);
		}

		// Token: 0x06007FE7 RID: 32743 RVA: 0x0022F8E8 File Offset: 0x0022DAE8
		public FolderHierarchyChangeDetector.MailboxChangesManifest GetFolderHierarchyICSChanges(MailboxSession mailboxSession, out FolderHierarchyChangeDetector.SyncHierarchyManifestState latestState, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			latestState = this.SyncHierarchyManifestState;
			if (latestState == null)
			{
				syncLogger.TraceDebug(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetFolderHierarchyICSChanges] Old ICS state was missing from cache.  Must do expensive check instead.");
				return null;
			}
			latestState = latestState.Clone();
			FolderHierarchyChangeDetector.MailboxChangesManifest mailboxChangesManifest = FolderHierarchyChangeDetector.RunICSManifestSync(false, latestState, mailboxSession, syncLogger);
			syncLogger.TraceDebug<string, string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetFolderHierarchyICSChanges] Changes: {0}, Deletes: {1}", (mailboxChangesManifest == null || mailboxChangesManifest.ChangedFolders == null) ? "<NULL>" : mailboxChangesManifest.ChangedFolders.Count.ToString(), (mailboxChangesManifest == null || mailboxChangesManifest.DeletedFolders == null) ? "<NULL>" : mailboxChangesManifest.DeletedFolders.Count.ToString());
			return mailboxChangesManifest;
		}

		// Token: 0x06007FE8 RID: 32744 RVA: 0x0022F99C File Offset: 0x0022DB9C
		public SyncStateMetadata TryAdd(SyncStateMetadata syncStateMetadata, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			SyncStateMetadata syncStateMetadata2 = this.syncStateMap.GetOrAdd(syncStateMetadata.Name, syncStateMetadata);
			if (syncStateMetadata.GetType().IsSubclassOf(syncStateMetadata2.GetType()))
			{
				syncLogger.TraceDebug<string, string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.TryAdd] New sync state metadata instance ({0}) is subclass of cached one ({1}).  Replacing.", syncStateMetadata.GetType().Name, syncStateMetadata2.GetType().Name);
				this.syncStateMap[syncStateMetadata.Name] = syncStateMetadata;
				syncStateMetadata2 = syncStateMetadata;
			}
			FolderSyncStateMetadata folderSyncStateMetadata = syncStateMetadata2 as FolderSyncStateMetadata;
			if (folderSyncStateMetadata != null && folderSyncStateMetadata.IPMFolderId != null)
			{
				syncLogger.TraceDebug<string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.TryAdd] Encountered FolderSyncStateMetadata instance for '{0}'.  Adding source key to reverse mapping.", syncStateMetadata.Name);
				this.ipmToFolderSyncStateMap[folderSyncStateMetadata.IPMFolderId] = folderSyncStateMetadata;
			}
			return syncStateMetadata2;
		}

		// Token: 0x06007FE9 RID: 32745 RVA: 0x0022FA5C File Offset: 0x0022DC5C
		public void ChangeIPMFolderId(FolderSyncStateMetadata folderSyncStateMetadata, StoreObjectId oldId, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			if (oldId != null)
			{
				FolderSyncStateMetadata folderSyncStateMetadata2;
				this.ipmToFolderSyncStateMap.TryRemove(oldId, out folderSyncStateMetadata2);
			}
			syncLogger.TraceDebug<string, string, string>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.ChangeIPMFolderId] IPM Folder Id for collection '{0}' changed from '{1}' to '{2}'", folderSyncStateMetadata.Name, (oldId == null) ? "<NULL>" : oldId.ToBase64String(), (folderSyncStateMetadata.IPMFolderId == null) ? "<NULL>" : folderSyncStateMetadata.IPMFolderId.ToBase64String());
			if (folderSyncStateMetadata.IPMFolderId != null)
			{
				this.ipmToFolderSyncStateMap.TryAdd(folderSyncStateMetadata.IPMFolderId, folderSyncStateMetadata);
			}
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x0022FAE8 File Offset: 0x0022DCE8
		public SyncStateMetadata TryRemove(string name, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			SyncStateMetadata syncStateMetadata;
			bool arg = this.syncStateMap.TryRemove(name, out syncStateMetadata);
			syncLogger.TraceDebug<string, bool>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.TryRemove] Removing '{0}'.  Success? {1}", name, arg);
			FolderSyncStateMetadata folderSyncStateMetadata = syncStateMetadata as FolderSyncStateMetadata;
			if (folderSyncStateMetadata != null && folderSyncStateMetadata.IPMFolderId != null)
			{
				this.ipmToFolderSyncStateMap.TryRemove(folderSyncStateMetadata.IPMFolderId, out folderSyncStateMetadata);
			}
			return syncStateMetadata;
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x0022FB4E File Offset: 0x0022DD4E
		public void RemoveAll()
		{
			this.syncStateMap.Clear();
			this.ipmToFolderSyncStateMap.Clear();
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x0022FB68 File Offset: 0x0022DD68
		public SyncStateMetadata GetSyncState(MailboxSession mailboxSession, string name, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			SyncStateMetadata result;
			if (this.syncStateMap.TryGetValue(name, out result))
			{
				syncLogger.TraceDebug<string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncState] Cache hit for sync state: {0}", name);
				return result;
			}
			syncLogger.TraceDebug<string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncState] Cache MISS for sync state: {0}", name);
			using (Folder folder = Folder.Bind(mailboxSession, this.DeviceFolderId, new PropertyDefinition[]
			{
				FolderSchema.ItemCount,
				FolderSchema.ChildCount
			}))
			{
				int itemCount = folder.ItemCount;
				syncLogger.TraceDebug<string, int, bool>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncState] deviceFolder {0} has {1} items and subfolders? {2}", folder.DisplayName, itemCount, folder.HasSubfolders);
				SyncStateMetadata syncStateMetadata = null;
				if (itemCount > 0)
				{
					syncStateMetadata = this.GetSyncStateItemMetadata(mailboxSession, folder, name, syncLogger);
					if (syncStateMetadata != null)
					{
						syncLogger.TraceDebug<SyncStateMetadata>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncState] Retrieved DIRECT item sync state: {0}", syncStateMetadata);
						syncStateMetadata.FolderSyncStateId = null;
					}
				}
				if (syncStateMetadata == null && folder.HasSubfolders)
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, new SortBy[]
					{
						new SortBy(FolderSchema.DisplayName, SortOrder.Ascending)
					}, DeviceSyncStateMetadata.NullSyncPropertiesFolders))
					{
						if (queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, name)))
						{
							IStorePropertyBag propertyBag = queryResult.GetPropertyBags(1)[0];
							syncStateMetadata = this.GetMetadataFromPropertyBag(mailboxSession, propertyBag, syncLogger);
							if (syncStateMetadata != null)
							{
								syncLogger.TraceDebug<SyncStateMetadata>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncState] Retrieved sub folder sync state: {0}", syncStateMetadata);
							}
						}
					}
				}
				if (syncStateMetadata != null)
				{
					SyncStateMetadata syncStateMetadata2 = this.syncStateMap.GetOrAdd(syncStateMetadata.Name, syncStateMetadata);
					if (syncStateMetadata2.StorageType != syncStateMetadata.StorageType)
					{
						syncLogger.TraceDebug<StorageType, StorageType>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncState] Metadata was already cached but had store type: {0}.  New instance was: {1}.  Using new instance.", syncStateMetadata2.StorageType, syncStateMetadata.StorageType);
						this.syncStateMap[syncStateMetadata.Name] = syncStateMetadata;
						syncStateMetadata2 = syncStateMetadata;
					}
					return syncStateMetadata2;
				}
				syncLogger.TraceDebug<string, Guid>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncState] Cache miss for sync state {0}, Mailbox {1}, but search did not find it.", name, mailboxSession.MailboxGuid);
			}
			return null;
		}

		// Token: 0x06007FED RID: 32749 RVA: 0x0022FD9C File Offset: 0x0022DF9C
		private SyncStateMetadata GetMetadataFromPropertyBag(MailboxSession mailboxSession, IStorePropertyBag propertyBag, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			StoreObjectId objectId = ((VersionedId)propertyBag.TryGetProperty(FolderSchema.Id)).ObjectId;
			int num = (int)propertyBag.TryGetProperty(FolderSchema.ItemCount);
			string text = (string)propertyBag.TryGetProperty(FolderSchema.DisplayName);
			syncLogger.TraceDebug<string, int>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetMetadataFromPropertyBag] SyncState {0} has {1} children.", text, num);
			SyncStateMetadata syncStateMetadata;
			if (num > 0)
			{
				syncStateMetadata = this.GetSyncStateItemMetadata(mailboxSession, objectId, text, syncLogger);
				if (syncStateMetadata != null)
				{
					syncLogger.TraceDebug(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetMetadataFromPropertyBag] Sync state was from item.");
					return syncStateMetadata;
				}
			}
			syncLogger.TraceDebug(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetMetadataFromPropertyBag] Sync state was from folder.");
			syncStateMetadata = this.GetMetadataFromPropertyBag(propertyBag, text, syncLogger);
			syncStateMetadata.FolderSyncStateId = objectId;
			syncStateMetadata.ItemSyncStateId = null;
			return syncStateMetadata;
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x0022FE64 File Offset: 0x0022E064
		public SyncStateMetadata GetMetadataFromPropertyBag(IStorePropertyBag propertyBag, string displayName, ISyncLogger syncLogger = null)
		{
			byte[] byteArray = null;
			if (!DeviceSyncStateMetadata.TryGetPropertyFromBag<byte[]>(propertyBag, InternalSchema.SyncFolderSourceKey, out byteArray, syncLogger))
			{
				syncLogger.TraceDebug<string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetMetadataFromPropertyBag] Creating custom sync state metadata for folder '{0}'", displayName);
				return new SyncStateMetadata(this, displayName, null, null);
			}
			StoreObjectId storeObjectId = StoreObjectId.Deserialize(byteArray);
			syncLogger.TraceDebug<string, StoreObjectId>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetMetadataFromPropertyBag] Found SyncFolderSourceKey for {0}, so it is a FolderSyncState: {1}", displayName, storeObjectId);
			long localCommitTimeMax = 0L;
			int deletedCountTotal = 0;
			int syncKey = 0;
			int airSyncFilter = 0;
			bool conversationMode = false;
			int airSyncSettingsHash = 0;
			int airSyncMaxItems = 0;
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			bool flag = snapshot != null && snapshot.DataStorage.IgnoreInessentialMetaDataLoadErrors != null && snapshot.DataStorage.IgnoreInessentialMetaDataLoadErrors.Enabled;
			syncLogger.TraceDebug<bool>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetMetadataFromPropertyBag] ignoreInessentialMetaDataLoadErrors: {0}", flag);
			long airSyncLastSyncTime;
			bool flag2 = DeviceSyncStateMetadata.TryGetPropertyFromBag<long>(propertyBag, AirSyncStateSchema.MetadataLastSyncTime, out airSyncLastSyncTime, syncLogger) && DeviceSyncStateMetadata.TryGetPropertyFromBag<long>(propertyBag, AirSyncStateSchema.MetadataLocalCommitTimeMax, out localCommitTimeMax, syncLogger) && DeviceSyncStateMetadata.TryGetPropertyFromBag<int>(propertyBag, AirSyncStateSchema.MetadataDeletedCountTotal, out deletedCountTotal, syncLogger) && DeviceSyncStateMetadata.TryGetPropertyFromBag<int>(propertyBag, AirSyncStateSchema.MetadataSyncKey, out syncKey, syncLogger) && DeviceSyncStateMetadata.TryGetPropertyFromBag<int>(propertyBag, AirSyncStateSchema.MetadataFilter, out airSyncFilter, syncLogger) && DeviceSyncStateMetadata.TryGetPropertyFromBag<bool>(propertyBag, AirSyncStateSchema.MetadataConversationMode, out conversationMode, syncLogger) && DeviceSyncStateMetadata.TryGetPropertyFromBag<int>(propertyBag, AirSyncStateSchema.MetadataSettingsHash, out airSyncSettingsHash, syncLogger);
			bool flag3 = flag2 && DeviceSyncStateMetadata.TryGetPropertyFromBag<int>(propertyBag, AirSyncStateSchema.MetadataMaxItems, out airSyncMaxItems, syncLogger);
			flag2 = (flag ? flag2 : flag3);
			if (flag2)
			{
				syncLogger.TraceDebug<string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetMetadataFromPropertyBag] Creating FolderSync metadata for folder '{0}'", displayName);
				return new FolderSyncStateMetadata(this, displayName, null, null, localCommitTimeMax, deletedCountTotal, syncKey, conversationMode, airSyncFilter, airSyncLastSyncTime, airSyncSettingsHash, airSyncMaxItems, storeObjectId);
			}
			syncLogger.TraceDebug<string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetMetadataFromPropertyBag] Failed to get nullSync properties for sync folder '{0}'.", displayName);
			return new FolderSyncStateMetadata(this, displayName, null, null, 0L, 0, 0, false, 0, 0L, 0, 0, storeObjectId);
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x0023002C File Offset: 0x0022E22C
		private SyncStateMetadata GetSyncStateItemMetadata(MailboxSession mailboxSession, StoreObjectId parentFolderId, string displayName, ISyncLogger syncLogger = null)
		{
			SyncStateMetadata syncStateItemMetadata;
			using (Folder folder = Folder.Bind(mailboxSession, parentFolderId, DeviceSyncStateMetadata.NullSyncPropertiesFolders))
			{
				syncStateItemMetadata = this.GetSyncStateItemMetadata(mailboxSession, folder, displayName, syncLogger);
			}
			return syncStateItemMetadata;
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x00230070 File Offset: 0x0022E270
		private SyncStateMetadata GetSyncStateItemMetadata(MailboxSession mailboxSession, Folder parentFolder, string displayName, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			using (QueryResult queryResult = parentFolder.ItemQuery(ItemQueryType.None, null, new SortBy[]
			{
				new SortBy(ItemSchema.Subject, SortOrder.Ascending)
			}, DeviceSyncStateMetadata.NullSyncPropertiesItems))
			{
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Subject, displayName)))
				{
					IStorePropertyBag storePropertyBag = queryResult.GetPropertyBags(1)[0];
					StoreObjectId objectId = ((VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
					syncLogger.TraceDebug<string, SmtpAddress, string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncStateItemMetadata] Sync state '{0}' for mailbox '{1}' is stored on item id '{2}'", displayName, mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, objectId.ToBase64String());
					SyncStateMetadata metadataFromPropertyBag = this.GetMetadataFromPropertyBag(storePropertyBag, displayName, syncLogger);
					metadataFromPropertyBag.FolderSyncStateId = parentFolder.Id.ObjectId;
					metadataFromPropertyBag.ItemSyncStateId = objectId;
					return metadataFromPropertyBag;
				}
			}
			syncLogger.TraceDebug<string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[DeviceSyncStateMetadata.GetSyncStateItemMetadata] Did not find child item with name {0}", displayName);
			return null;
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x00230174 File Offset: 0x0022E374
		private static bool TryGetPropertyFromBag<T>(IStorePropertyBag propertyBag, PropertyDefinition propDef, out T value, ISyncLogger syncLogger = null)
		{
			object obj = propertyBag.TryGetProperty(propDef);
			if (obj is T)
			{
				value = (T)((object)obj);
				return true;
			}
			PropertyError propertyError = obj as PropertyError;
			if (propertyError != null)
			{
				syncLogger.TraceError<Type, string, PropertyErrorCode>(ExTraceGlobals.SyncProcessTracer, 0L, "[DeviceSyncStateMetadata.TryGetPropertyFromBag] Expected property of type {0} in bag for propDef {1}, but encountered error {2}.", typeof(T), propDef.Name, propertyError.PropertyErrorCode);
			}
			else
			{
				try
				{
					value = (T)((object)obj);
					return true;
				}
				catch (InvalidCastException ex)
				{
					syncLogger.TraceError(ExTraceGlobals.SyncProcessTracer, 0L, "[DeviceSyncStateMetadata.TryGetPropertyFromBag] Tried to cast property '{0}' with value '{1}' to type '{2}', but the cast failed with error '{3}'.", new object[]
					{
						propDef.Name,
						(obj == null) ? "<NULL>" : obj,
						typeof(T).FullName,
						ex
					});
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x17002213 RID: 8723
		// (get) Token: 0x06007FF2 RID: 32754 RVA: 0x00230250 File Offset: 0x0022E450
		// (set) Token: 0x06007FF3 RID: 32755 RVA: 0x00230258 File Offset: 0x0022E458
		public DeviceIdentity Id { get; private set; }

		// Token: 0x17002214 RID: 8724
		// (get) Token: 0x06007FF4 RID: 32756 RVA: 0x00230261 File Offset: 0x0022E461
		// (set) Token: 0x06007FF5 RID: 32757 RVA: 0x00230269 File Offset: 0x0022E469
		public StoreObjectId DeviceFolderId { get; private set; }

		// Token: 0x06007FF6 RID: 32758 RVA: 0x00230272 File Offset: 0x0022E472
		public override string ToString()
		{
			return string.Format("Device: {0} - {1}", this.Id, this.DeviceFolderId.ToBase64String());
		}

		// Token: 0x04005665 RID: 22117
		public static readonly PropertyDefinition[] NullSyncPropertiesFolders = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName,
			FolderSchema.ItemCount,
			FolderSchema.SyncFolderSourceKey,
			AirSyncStateSchema.MetadataLastSyncTime,
			AirSyncStateSchema.MetadataLocalCommitTimeMax,
			AirSyncStateSchema.MetadataDeletedCountTotal,
			AirSyncStateSchema.MetadataSyncKey,
			AirSyncStateSchema.MetadataFilter,
			AirSyncStateSchema.MetadataMaxItems,
			AirSyncStateSchema.MetadataConversationMode,
			AirSyncStateSchema.MetadataSettingsHash
		};

		// Token: 0x04005666 RID: 22118
		public static readonly PropertyDefinition[] NullSyncPropertiesItems = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Subject,
			FolderSchema.SyncFolderSourceKey,
			AirSyncStateSchema.MetadataLastSyncTime,
			AirSyncStateSchema.MetadataLocalCommitTimeMax,
			AirSyncStateSchema.MetadataDeletedCountTotal,
			AirSyncStateSchema.MetadataSyncKey,
			AirSyncStateSchema.MetadataFilter,
			AirSyncStateSchema.MetadataMaxItems,
			AirSyncStateSchema.MetadataConversationMode,
			AirSyncStateSchema.MetadataSettingsHash
		};

		// Token: 0x04005667 RID: 22119
		private ConcurrentDictionary<string, SyncStateMetadata> syncStateMap = new ConcurrentDictionary<string, SyncStateMetadata>();

		// Token: 0x04005668 RID: 22120
		private ConcurrentDictionary<StoreObjectId, FolderSyncStateMetadata> ipmToFolderSyncStateMap = new ConcurrentDictionary<StoreObjectId, FolderSyncStateMetadata>();

		// Token: 0x04005669 RID: 22121
		private object syncStatusInstanceLock = new object();
	}
}
