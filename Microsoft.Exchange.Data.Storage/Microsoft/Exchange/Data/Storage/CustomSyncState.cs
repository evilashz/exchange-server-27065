using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E03 RID: 3587
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CustomSyncState : SyncState
	{
		// Token: 0x06007BA0 RID: 31648 RVA: 0x00221D55 File Offset: 0x0021FF55
		protected CustomSyncState(SyncStateStorage syncStateStorage, StoreObject storeObject, SyncStateMetadata syncStateMetadata, SyncStateInfo syncStateInfo, bool syncStateIsNew) : base(syncStateStorage, storeObject, syncStateMetadata, syncStateInfo, syncStateIsNew, null)
		{
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x00221D65 File Offset: 0x0021FF65
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CustomSyncState>(this);
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x00221D70 File Offset: 0x0021FF70
		internal static CustomSyncState CreateSyncState(SyncStateStorage syncStateStorage, SyncStateInfo syncStateInfo, Folder syncStateParentFolder, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			StoreObject storeObject = SyncState.CreateSyncStateStoreObject(syncStateStorage, syncStateInfo, syncStateParentFolder, null, null, syncLogger);
			if (syncStateStorage.DeviceMetadata.TryRemove(syncStateInfo.UniqueName, syncLogger) != null)
			{
				syncLogger.TraceDebug<DeviceIdentity, string>(ExTraceGlobals.SyncTracer, 0L, "[CustomSyncState.CreateSyncState] Removed stale cached sync state metadata for device {0}, sync state {1}", syncStateStorage.DeviceMetadata.Id, syncStateInfo.UniqueName);
			}
			SyncStateMetadata syncStateMetadata = (storeObject is Item) ? new SyncStateMetadata(syncStateStorage.DeviceMetadata, syncStateInfo.UniqueName, syncStateStorage.SaveOnDirectItems ? null : storeObject.ParentId, storeObject.Id.ObjectId) : new SyncStateMetadata(syncStateStorage.DeviceMetadata, syncStateInfo.UniqueName, storeObject.Id.ObjectId, null);
			return new CustomSyncState(syncStateStorage, storeObject, syncStateMetadata, syncStateInfo, true);
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x00221E29 File Offset: 0x00220029
		internal static CustomSyncState GetSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, SyncStateInfo syncStateInfo, ISyncLogger syncLogger = null)
		{
			return CustomSyncState.GetSyncState(syncStateStorage, syncStateParentFolder, syncStateInfo, syncLogger, null);
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x00221E38 File Offset: 0x00220038
		internal static CustomSyncState GetSyncState(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, SyncStateInfo syncStateInfo, ISyncLogger syncLogger, params PropertyDefinition[] propertiesToFetch)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			SyncStateMetadata syncStateMetadata = null;
			StoreObject syncStateStoreObject = SyncState.GetSyncStateStoreObject(syncStateStorage, syncStateParentFolder, syncStateInfo, syncLogger, out syncStateMetadata, propertiesToFetch);
			if (syncStateStoreObject == null)
			{
				return null;
			}
			return new CustomSyncState(syncStateStorage, syncStateStoreObject, syncStateMetadata, syncStateInfo, false);
		}
	}
}
