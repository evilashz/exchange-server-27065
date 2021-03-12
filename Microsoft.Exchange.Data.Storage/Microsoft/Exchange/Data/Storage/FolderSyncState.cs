using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E1A RID: 3610
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderSyncState : SyncState, IFolderSyncState, ISyncState
	{
		// Token: 0x06007CD3 RID: 31955 RVA: 0x002281F4 File Offset: 0x002263F4
		protected FolderSyncState(SyncStateStorage syncStateStorage, StoreObject storeObject, FolderSyncStateMetadata syncStateMetadata, SyncStateInfo syncStateInfo, ISyncProviderFactory syncProviderFactory, bool newSyncState, ISyncLogger syncLogger = null) : base(syncStateStorage, storeObject, syncStateMetadata, syncStateInfo, newSyncState, syncLogger)
		{
			this.syncProviderFactory = syncProviderFactory;
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x06007CD4 RID: 31956 RVA: 0x0022820D File Offset: 0x0022640D
		public string SyncFolderId
		{
			get
			{
				base.CheckDisposed("get_SyncFolderId");
				return base.UniqueName;
			}
		}

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06007CD5 RID: 31957 RVA: 0x00228220 File Offset: 0x00226420
		// (set) Token: 0x06007CD6 RID: 31958 RVA: 0x0022824C File Offset: 0x0022644C
		public int? CustomVersion
		{
			get
			{
				base.CheckDisposed("get_CustomVersion");
				return base.GetData<NullableData<Int32Data, int>, int?>(SyncStateProp.CustomVersion, null);
			}
			set
			{
				base.CheckDisposed("set_CustomVersion");
				base[SyncStateProp.CustomVersion] = new NullableData<Int32Data, int>(value);
			}
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06007CD7 RID: 31959 RVA: 0x0022826A File Offset: 0x0022646A
		// (set) Token: 0x06007CD8 RID: 31960 RVA: 0x00228271 File Offset: 0x00226471
		public ISyncWatermark Watermark
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06007CD9 RID: 31961 RVA: 0x00228278 File Offset: 0x00226478
		public static void RegisterCustomDataVersioningHandler(FolderSyncState.HandleCustomDataVersioningDelegate handleCustomVersioning)
		{
			FolderSyncState.handleCustomDataVersioning = handleCustomVersioning;
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x00228280 File Offset: 0x00226480
		public void RegisterColdDataKey(string key)
		{
			base.CheckDisposed("RegisterColdDataKey");
			base.AddColdDataKey(key);
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x00228294 File Offset: 0x00226494
		public override void Commit()
		{
			base.CheckDisposed("Commit");
			this.CommitState(null, null);
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x002282AC File Offset: 0x002264AC
		public override void Load()
		{
			base.CheckDisposed("Load");
			this.Load(true, new PropertyDefinition[]
			{
				InternalSchema.SyncFolderSourceKey
			});
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x002282DC File Offset: 0x002264DC
		public void CommitState(PropertyDefinition[] properties, object[] values)
		{
			base.CheckDisposed("CommitState");
			if (this.syncProviderFactory == null)
			{
				base.Commit(properties, values);
				return;
			}
			byte[] collectionIdBytes = this.syncProviderFactory.GetCollectionIdBytes();
			if (properties == null)
			{
				base.Commit(new PropertyDefinition[]
				{
					InternalSchema.SyncFolderSourceKey
				}, new object[]
				{
					collectionIdBytes
				});
				return;
			}
			PropertyDefinition[] array = new PropertyDefinition[properties.Length + 1];
			object[] array2 = new object[values.Length + 1];
			array[0] = InternalSchema.SyncFolderSourceKey;
			array2[0] = collectionIdBytes;
			properties.CopyTo(array, 1);
			values.CopyTo(array2, 1);
			base.Commit(array, array2);
		}

		// Token: 0x06007CDE RID: 31966 RVA: 0x00228374 File Offset: 0x00226574
		public FolderSync GetFolderSync()
		{
			base.CheckDisposed("GetFolderSync");
			return this.GetFolderSync(ConflictResolutionPolicy.ServerWins);
		}

		// Token: 0x06007CDF RID: 31967 RVA: 0x00228388 File Offset: 0x00226588
		public FolderSync GetFolderSync(ConflictResolutionPolicy policy)
		{
			return this.GetFolderSync(policy, null);
		}

		// Token: 0x06007CE0 RID: 31968 RVA: 0x00228394 File Offset: 0x00226594
		public FolderSync GetFolderSync(ConflictResolutionPolicy policy, Func<ISyncProvider, FolderSyncState, ConflictResolutionPolicy, bool, FolderSync> creator)
		{
			base.CheckDisposed("GetFolderSync");
			EnumValidator.ThrowIfInvalid<ConflictResolutionPolicy>(policy, "policy");
			this.syncLogger.Information<int, ConflictResolutionPolicy>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncState::GetFolderSync. Hashcode = {0}, Policy = {1}.", this.GetHashCode(), policy);
			if (this.syncProviderFactory == null)
			{
				throw new InvalidOperationException("Must set a sync provider factory before calling GetFolderSync");
			}
			try
			{
				this.syncProvider = this.syncProviderFactory.CreateSyncProvider(null);
			}
			catch (ObjectNotFoundException)
			{
				return null;
			}
			if (creator != null)
			{
				return creator(this.syncProvider, this, policy, true);
			}
			return new FolderSync(this.syncProvider, this, policy, true);
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x0022843C File Offset: 0x0022663C
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FolderSyncState>(this);
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x00228444 File Offset: 0x00226644
		public StoreObjectId TryGetStoreObjectId()
		{
			base.CheckDisposed("TryGetStoreObjectId");
			byte[] collectionIdBytes = this.syncProviderFactory.GetCollectionIdBytes();
			if (collectionIdBytes == null)
			{
				return null;
			}
			return StoreObjectId.Deserialize(collectionIdBytes);
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x00228473 File Offset: 0x00226673
		internal static FolderSyncState GetSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, string syncFolderId, ISyncLogger syncLogger = null)
		{
			return FolderSyncState.GetSyncState(syncStateStorage, syncStateParentFolder, null, syncFolderId, syncLogger);
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x00228480 File Offset: 0x00226680
		internal static FolderSyncState GetSyncState(SyncStateStorage syncStateStorage, Folder deviceFolder, ISyncProviderFactory syncProviderFactory, Func<SyncStateStorage, StoreObject, FolderSyncStateMetadata, SyncStateInfo, ISyncProviderFactory, bool, ISyncLogger, FolderSyncState> creator, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			ArgumentValidator.ThrowIfNull("syncStateStorage", syncStateStorage);
			ArgumentValidator.ThrowIfNull("deviceFolder", deviceFolder);
			ArgumentValidator.ThrowIfNull("syncProviderFactory", syncProviderFactory);
			byte[] collectionIdBytes = syncProviderFactory.GetCollectionIdBytes();
			if (collectionIdBytes == null || collectionIdBytes.Length == 0)
			{
				throw new ArgumentException("SyncProviderFactory CollectionId bytes cannot be null or empty.");
			}
			StoreObjectId storeObjectId = null;
			try
			{
				storeObjectId = StoreObjectId.Deserialize(collectionIdBytes);
			}
			catch (ArgumentException innerException)
			{
				syncLogger.TraceError<string>(ExTraceGlobals.SyncTracer, 0L, "[FolderSyncState.GetSyncState(syncProviderFactory)] The IPMFolderBytes that the provider gave us are invalid for folder {0}", deviceFolder.DisplayName);
				throw new CorruptSyncStateException(ServerStrings.ExSyncStateCorrupted(deviceFolder.DisplayName), innerException);
			}
			FolderSyncStateMetadata folderSyncStateMetadata = null;
			if (!syncStateStorage.DeviceMetadata.SyncStatesByIPMFolderId.TryGetValue(storeObjectId, out folderSyncStateMetadata))
			{
				syncLogger.TraceDebug<DeviceIdentity, string>(ExTraceGlobals.SyncTracer, 0L, "[FolderSyncState.GetSyncState(syncProviderFactory)] Cache miss for device {0}, IPM folder Id {1}", syncStateStorage.DeviceMetadata.Id, storeObjectId.ToBase64String());
				return null;
			}
			SyncStateMetadata syncStateMetadata = folderSyncStateMetadata;
			StoreObject storeObject = SyncState.GetSyncStateStoreObject(deviceFolder, ref syncStateMetadata, syncLogger, new PropertyDefinition[]
			{
				InternalSchema.SyncFolderSourceKey
			});
			if (!object.ReferenceEquals(folderSyncStateMetadata, syncStateMetadata))
			{
				FolderSyncStateMetadata folderSyncStateMetadata2 = syncStateMetadata as FolderSyncStateMetadata;
				if (folderSyncStateMetadata2 == null)
				{
					syncLogger.TraceDebug<string, string>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderSyncState.GetSyncState] Device {0} has non-folder sync state for {1}.  Returning null.", deviceFolder.DisplayName, folderSyncStateMetadata.Name);
					if (storeObject != null)
					{
						storeObject.Dispose();
						storeObject = null;
					}
				}
				else
				{
					syncLogger.TraceDebug<string, string>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderSyncState.GetSyncState] Device {0} had  state folder sync state metadata for {1}.  Replacing.", deviceFolder.DisplayName, folderSyncStateMetadata.Name);
					folderSyncStateMetadata = folderSyncStateMetadata2;
				}
			}
			if (storeObject == null)
			{
				return null;
			}
			SyncStateInfo syncStateInfo = new FolderSyncStateInfo(folderSyncStateMetadata.Name);
			if (creator == null)
			{
				return new FolderSyncState(syncStateStorage, storeObject, folderSyncStateMetadata, syncStateInfo, syncProviderFactory, false, syncLogger);
			}
			return creator(syncStateStorage, storeObject, folderSyncStateMetadata, syncStateInfo, syncProviderFactory, false, syncLogger);
		}

		// Token: 0x06007CE5 RID: 31973 RVA: 0x00228618 File Offset: 0x00226818
		internal static FolderSyncState GetSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, ISyncProviderFactory syncProviderFactory, string syncStateName, ISyncLogger syncLogger = null)
		{
			return FolderSyncState.GetSyncState(syncStateStorage, syncStateParentFolder, syncProviderFactory, syncStateName, null, syncLogger);
		}

		// Token: 0x06007CE6 RID: 31974 RVA: 0x00228628 File Offset: 0x00226828
		private static FolderSyncStateMetadata GetFolderSyncStateMetadata(SyncStateStorage syncStateStorage, MailboxSession mailboxSession, string name, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			SyncStateMetadata syncState = syncStateStorage.DeviceMetadata.GetSyncState(mailboxSession, name, syncLogger);
			FolderSyncStateMetadata folderSyncStateMetadata = syncState as FolderSyncStateMetadata;
			if (folderSyncStateMetadata == null)
			{
				syncLogger.TraceDebug<SmtpAddress, string>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderSyncState.GetFolderSyncStateMetadata] SyncStateMetadata in place of FolderSyncStateMetadata for Mailbox: {0}, State: {1}.  Trying re-read...", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, name);
				syncStateStorage.DeviceMetadata.TryRemove(name, syncLogger);
				syncState = syncStateStorage.DeviceMetadata.GetSyncState(mailboxSession, name, syncLogger);
				folderSyncStateMetadata = (syncState as FolderSyncStateMetadata);
				if (folderSyncStateMetadata == null)
				{
					syncStateStorage.DeleteFolderSyncState(name);
					syncStateStorage.DeviceMetadata.TryRemove(name, null);
					throw new CorruptSyncStateException(ServerStrings.ExSyncStateCorrupted(name), new InvalidOperationException("SyncStateMetadata in place of FolderSyncStateMetadata"));
				}
				syncLogger.TraceDebug<string>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderSyncState.GetFolderSyncStateMetadata] Re-read of sync state {0} was successful.", name);
			}
			return folderSyncStateMetadata;
		}

		// Token: 0x06007CE7 RID: 31975 RVA: 0x002286E8 File Offset: 0x002268E8
		internal static FolderSyncState GetSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, ISyncProviderFactory syncProviderFactory, string syncStateName, Func<SyncStateStorage, StoreObject, FolderSyncStateMetadata, SyncStateInfo, ISyncProviderFactory, bool, ISyncLogger, FolderSyncState> creator, ISyncLogger syncLogger = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("syncStateName", syncStateName);
			SyncStateInfo syncStateInfo = new FolderSyncStateInfo(syncStateName);
			StoreObject syncStateStoreObject = SyncState.GetSyncStateStoreObject(syncStateStorage, syncStateParentFolder, syncStateInfo, syncLogger, new PropertyDefinition[]
			{
				InternalSchema.SyncFolderSourceKey
			});
			if (syncStateStoreObject == null)
			{
				return null;
			}
			byte[] valueOrDefault = syncStateStoreObject.GetValueOrDefault<byte[]>(InternalSchema.SyncFolderSourceKey);
			if (syncProviderFactory != null)
			{
				try
				{
					syncProviderFactory.SetCollectionIdFromBytes(valueOrDefault);
				}
				catch (ArgumentException innerException)
				{
					syncStateStorage.DeleteFolderSyncState(syncStateName);
					syncStateStorage.DeviceMetadata.TryRemove(syncStateName, null);
					throw new CorruptSyncStateException(ServerStrings.ExSyncStateCorrupted(syncStateName), innerException);
				}
			}
			FolderSyncStateMetadata folderSyncStateMetadata = FolderSyncState.GetFolderSyncStateMetadata(syncStateStorage, syncStateStoreObject.Session as MailboxSession, syncStateName, syncLogger);
			if (creator == null)
			{
				return new FolderSyncState(syncStateStorage, syncStateStoreObject, folderSyncStateMetadata, syncStateInfo, syncProviderFactory, false, syncLogger);
			}
			return creator(syncStateStorage, syncStateStoreObject, folderSyncStateMetadata, syncStateInfo, syncProviderFactory, false, syncLogger);
		}

		// Token: 0x06007CE8 RID: 31976 RVA: 0x002287B0 File Offset: 0x002269B0
		internal static FolderSyncState CreateSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, ISyncProviderFactory syncProviderFactory, string syncFolderId, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			SyncStateInfo syncStateInfo = new FolderSyncStateInfo(syncFolderId);
			StoreObject storeObject = SyncState.CreateSyncStateStoreObject(syncStateStorage, syncStateInfo, syncStateParentFolder, new PropertyDefinition[]
			{
				InternalSchema.SyncFolderSourceKey
			}, new object[]
			{
				syncProviderFactory.GetCollectionIdBytes()
			}, syncLogger);
			if (syncStateStorage.DeviceMetadata.TryRemove(syncStateInfo.UniqueName, syncLogger) != null)
			{
				syncLogger.TraceDebug<DeviceIdentity, string>(ExTraceGlobals.SyncTracer, 0L, "[FolderSyncState.CreateSyncState] Removed stale cached sync state metadata for device {0}, sync state {1}", syncStateStorage.DeviceMetadata.Id, syncStateInfo.UniqueName);
			}
			FolderSyncStateMetadata syncStateMetadata = (storeObject is Item) ? new FolderSyncStateMetadata(syncStateStorage.DeviceMetadata, syncStateInfo.UniqueName, syncStateStorage.SaveOnDirectItems ? null : storeObject.ParentId, storeObject.Id.ObjectId, StoreObjectId.Deserialize(syncProviderFactory.GetCollectionIdBytes())) : new FolderSyncStateMetadata(syncStateStorage.DeviceMetadata, syncStateInfo.UniqueName, storeObject.Id.ObjectId, null, StoreObjectId.Deserialize(syncProviderFactory.GetCollectionIdBytes()));
			return new FolderSyncState(syncStateStorage, storeObject, syncStateMetadata, syncStateInfo, syncProviderFactory, true, syncLogger);
		}

		// Token: 0x06007CE9 RID: 31977 RVA: 0x002288B0 File Offset: 0x00226AB0
		protected static StoreObject GetSyncStateStoreObject(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, SyncStateInfo syncStateInfo, byte[] identBytes, ISyncLogger syncLogger, params PropertyDefinition[] propsToReturn)
		{
			FolderSyncStateMetadata folderSyncStateMetadata = FolderSyncState.GetFolderSyncStateMetadata(syncStateStorage, syncStateParentFolder.Session as MailboxSession, syncStateInfo.UniqueName, syncLogger);
			if (folderSyncStateMetadata == null)
			{
				return null;
			}
			StoreObject storeObject = null;
			if (folderSyncStateMetadata.StorageType != StorageType.Folder)
			{
				if (folderSyncStateMetadata.StorageType != StorageType.Item)
				{
					if (folderSyncStateMetadata.StorageType != StorageType.DirectItem)
					{
						goto IL_85;
					}
				}
				try
				{
					storeObject = Microsoft.Exchange.Data.Storage.Item.Bind(syncStateParentFolder.Session, folderSyncStateMetadata.ItemSyncStateId, SyncState.AppendAdditionalProperties(propsToReturn));
					((Item)storeObject).OpenAsReadWrite();
					return storeObject;
				}
				catch
				{
					storeObject.Dispose();
					throw;
				}
				IL_85:
				throw new InvalidOperationException("Unsupported storage type for sync state");
			}
			storeObject = Folder.Bind(syncStateParentFolder.Session, folderSyncStateMetadata.FolderSyncStateId, SyncState.AppendAdditionalProperties(propsToReturn));
			return storeObject;
		}

		// Token: 0x06007CEA RID: 31978 RVA: 0x00228960 File Offset: 0x00226B60
		protected override void AddColdDataKeys()
		{
			base.AddColdDataKey(SyncStateProp.ClientState);
			base.AddColdDataKey(SyncStateProp.PrevServerManifest);
			base.AddColdDataKey(SyncStateProp.PrevDelayedServerOperationQueue);
			base.AddColdDataKey(SyncStateProp.PrevMaxWatermark);
			base.AddColdDataKey(SyncStateProp.PrevFilterId);
			base.AddColdDataKey(SyncStateProp.PrevSnapShotWatermark);
			base.AddColdDataKey(SyncStateProp.PrevLastSyncConversationMode);
		}

		// Token: 0x06007CEB RID: 31979 RVA: 0x002289BA File Offset: 0x00226BBA
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.syncProvider != null)
			{
				this.syncProvider.Dispose();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x002289D9 File Offset: 0x00226BD9
		protected override void Load(bool reloadFromBackend, params PropertyDefinition[] additionalPropsToLoad)
		{
			base.Load(reloadFromBackend, additionalPropsToLoad);
			FolderSyncState.handleCustomDataVersioning(this);
		}

		// Token: 0x04005563 RID: 21859
		private const ConflictResolutionPolicy DefaultConflictResolutionPolicy = ConflictResolutionPolicy.ServerWins;

		// Token: 0x04005564 RID: 21860
		private static FolderSyncState.HandleCustomDataVersioningDelegate handleCustomDataVersioning = delegate(FolderSyncState syncState)
		{
			if (syncState.CustomVersion != null)
			{
				syncState.HandleCorruptSyncState();
			}
		};

		// Token: 0x04005565 RID: 21861
		protected ISyncProvider syncProvider;

		// Token: 0x04005566 RID: 21862
		private ISyncProviderFactory syncProviderFactory;

		// Token: 0x02000E1B RID: 3611
		// (Invoke) Token: 0x06007CF0 RID: 31984
		public delegate void HandleCustomDataVersioningDelegate(FolderSyncState syncState);
	}
}
