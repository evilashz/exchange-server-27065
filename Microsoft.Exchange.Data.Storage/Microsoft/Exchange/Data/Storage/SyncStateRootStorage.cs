using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E37 RID: 3639
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncStateRootStorage : DisposeTrackableBase
	{
		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x06007E19 RID: 32281 RVA: 0x0022BBA3 File Offset: 0x00229DA3
		public StoreObjectId FolderId
		{
			get
			{
				base.CheckDisposed();
				return this.syncStateStorage.FolderId;
			}
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x0022BBB6 File Offset: 0x00229DB6
		private SyncStateRootStorage(SyncStateStorage syncStateStorage)
		{
			if (syncStateStorage == null)
			{
				throw new ArgumentNullException("syncStateStorage");
			}
			this.syncStateStorage = syncStateStorage;
		}

		// Token: 0x06007E1B RID: 32283 RVA: 0x0022BBD4 File Offset: 0x00229DD4
		public static SyncStateRootStorage GetOrCreateSyncStateRootStorage(MailboxSession mailboxSession, string protocol, ISyncLogger syncLogger = null)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (protocol == null)
			{
				throw new ArgumentNullException("protocol");
			}
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			string protocol2 = protocol + "Root";
			DeviceIdentity deviceIdentity = new DeviceIdentity("RootDeviceId", "RootDeviceType", protocol2);
			SyncStateStorage syncStateStorage = SyncStateStorage.Bind(mailboxSession, deviceIdentity, syncLogger);
			if (syncStateStorage == null)
			{
				syncStateStorage = SyncStateStorage.Create(mailboxSession, deviceIdentity, StateStorageFeatures.ContentState, syncLogger);
			}
			return new SyncStateRootStorage(syncStateStorage);
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x0022BC40 File Offset: 0x00229E40
		public CustomSyncState CreateCustomSyncState(SyncStateInfo syncStateInfo)
		{
			base.CheckDisposed();
			return this.syncStateStorage.CreateCustomSyncState(syncStateInfo);
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x0022BC54 File Offset: 0x00229E54
		public AggregateOperationResult DeleteCustomSyncState(SyncStateInfo syncStateInfo)
		{
			base.CheckDisposed();
			return this.syncStateStorage.DeleteCustomSyncState(syncStateInfo);
		}

		// Token: 0x06007E1E RID: 32286 RVA: 0x0022BC68 File Offset: 0x00229E68
		public CustomSyncState GetCustomSyncState(SyncStateInfo syncStateInfo)
		{
			base.CheckDisposed();
			return this.syncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]);
		}

		// Token: 0x06007E1F RID: 32287 RVA: 0x0022BC82 File Offset: 0x00229E82
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.syncStateStorage != null)
			{
				this.syncStateStorage.Dispose();
				this.syncStateStorage = null;
			}
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x0022BCA1 File Offset: 0x00229EA1
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncStateRootStorage>(this);
		}

		// Token: 0x040055E4 RID: 21988
		private const string RootProtocolPostFix = "Root";

		// Token: 0x040055E5 RID: 21989
		private const string RootDeviceType = "RootDeviceType";

		// Token: 0x040055E6 RID: 21990
		private const string RootDeviceId = "RootDeviceId";

		// Token: 0x040055E7 RID: 21991
		private SyncStateStorage syncStateStorage;
	}
}
