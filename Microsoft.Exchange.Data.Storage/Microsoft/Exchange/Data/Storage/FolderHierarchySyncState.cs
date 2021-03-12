using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E08 RID: 3592
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderHierarchySyncState : SyncState, IFolderHierarchySyncState, ISyncState
	{
		// Token: 0x06007BE2 RID: 31714 RVA: 0x002235F0 File Offset: 0x002217F0
		protected FolderHierarchySyncState(SyncStateStorage syncStateStorage, StoreObject storeObject, SyncStateMetadata syncStateMetadata, SyncStateInfo syncStateInfo, bool newSyncState, ISyncLogger syncLogger = null) : base(syncStateStorage, storeObject, syncStateMetadata, syncStateInfo, newSyncState, syncLogger)
		{
		}

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x06007BE3 RID: 31715 RVA: 0x00223604 File Offset: 0x00221804
		// (set) Token: 0x06007BE4 RID: 31716 RVA: 0x00223630 File Offset: 0x00221830
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

		// Token: 0x06007BE5 RID: 31717 RVA: 0x0022364E File Offset: 0x0022184E
		public static void RegisterCustomDataVersioningHandler(FolderHierarchySyncState.HandleCustomDataVersioningDelegate handleCustomVersioning)
		{
			FolderHierarchySyncState.handleCustomDataVersioning = handleCustomVersioning;
		}

		// Token: 0x06007BE6 RID: 31718 RVA: 0x00223656 File Offset: 0x00221856
		public FolderHierarchySync GetFolderHierarchySync()
		{
			return this.GetFolderHierarchySync(null);
		}

		// Token: 0x06007BE7 RID: 31719 RVA: 0x00223660 File Offset: 0x00221860
		public FolderHierarchySync GetFolderHierarchySync(ChangeTrackingDelegate changeTrackingDelegate)
		{
			base.CheckDisposed("GetFolderHierarchySync");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "FolderHierarchySyncState::GetFolderHierarchySync. Hashcode = {0}", this.GetHashCode());
			MailboxSession mailboxSession = base.StoreObject.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new NotSupportedException();
			}
			return new FolderHierarchySync(mailboxSession, this, changeTrackingDelegate);
		}

		// Token: 0x06007BE8 RID: 31720 RVA: 0x002236BC File Offset: 0x002218BC
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FolderHierarchySyncState>(this);
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x002236C4 File Offset: 0x002218C4
		internal static FolderHierarchySyncState CreateSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			SyncStateInfo syncStateInfo = new FolderHierarchySyncStateInfo();
			StoreObject storeObject = SyncState.CreateSyncStateStoreObject(syncStateStorage, syncStateInfo, syncStateParentFolder, null, null, syncLogger);
			if (syncStateStorage.DeviceMetadata.TryRemove(syncStateInfo.UniqueName, syncLogger) != null)
			{
				syncLogger.TraceDebug<DeviceIdentity, string>(ExTraceGlobals.SyncTracer, 0L, "[FolderHierarchySyncState.CreateSyncState] Removed stale cached sync state metadata for device {0}, sync state {1}", syncStateStorage.DeviceMetadata.Id, syncStateInfo.UniqueName);
			}
			SyncStateMetadata syncStateMetadata = (storeObject is Item) ? new SyncStateMetadata(syncStateStorage.DeviceMetadata, syncStateInfo.UniqueName, syncStateStorage.SaveOnDirectItems ? null : storeObject.ParentId, storeObject.Id.ObjectId) : new SyncStateMetadata(syncStateStorage.DeviceMetadata, syncStateInfo.UniqueName, storeObject.Id.ObjectId, null);
			return new FolderHierarchySyncState(syncStateStorage, storeObject, syncStateMetadata, syncStateInfo, true, syncLogger);
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x00223784 File Offset: 0x00221984
		internal static FolderHierarchySyncState GetSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, ISyncLogger syncLogger = null)
		{
			return FolderHierarchySyncState.GetSyncState(syncStateStorage, syncStateParentFolder, null, syncLogger);
		}

		// Token: 0x06007BEB RID: 31723 RVA: 0x00223790 File Offset: 0x00221990
		internal static FolderHierarchySyncState GetSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, StoreObjectId storeObjectId, ISyncLogger syncLogger = null)
		{
			SyncStateInfo syncStateInfo = new FolderHierarchySyncStateInfo();
			StoreObject syncStateStoreObject = SyncState.GetSyncStateStoreObject(syncStateStorage, syncStateParentFolder, syncStateInfo, syncLogger, new PropertyDefinition[0]);
			if (syncStateStoreObject == null)
			{
				return null;
			}
			SyncStateMetadata syncState = syncStateStorage.DeviceMetadata.GetSyncState(syncStateParentFolder.Session as MailboxSession, syncStateInfo.UniqueName, syncLogger);
			return new FolderHierarchySyncState(syncStateStorage, syncStateStoreObject, syncState, syncStateInfo, false, syncLogger);
		}

		// Token: 0x06007BEC RID: 31724 RVA: 0x002237E1 File Offset: 0x002219E1
		protected override void Load(bool reloadFromBackend, params PropertyDefinition[] additionalPropsToLoad)
		{
			base.Load(reloadFromBackend, additionalPropsToLoad);
			FolderHierarchySyncState.handleCustomDataVersioning(this);
		}

		// Token: 0x040054F8 RID: 21752
		private static FolderHierarchySyncState.HandleCustomDataVersioningDelegate handleCustomDataVersioning = delegate(FolderHierarchySyncState syncState)
		{
			if (syncState.CustomVersion != null)
			{
				syncState.HandleCorruptSyncState();
			}
		};

		// Token: 0x02000E09 RID: 3593
		// (Invoke) Token: 0x06007BF0 RID: 31728
		public delegate void HandleCustomDataVersioningDelegate(FolderHierarchySyncState syncState);
	}
}
