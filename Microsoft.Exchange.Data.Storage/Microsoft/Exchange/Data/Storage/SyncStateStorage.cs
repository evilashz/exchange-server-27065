using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E34 RID: 3636
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncStateStorage : IDisposeTrackable, IDisposable
	{
		// Token: 0x06007DDC RID: 32220 RVA: 0x0022A5D4 File Offset: 0x002287D4
		private SyncStateStorage(Folder folder, DeviceSyncStateMetadata deviceMetadata, ISyncLogger syncLogger = null)
		{
			this.syncLogger = (syncLogger ?? TracingLogger.Singleton);
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.folder = folder;
			this.DeviceMetadata = deviceMetadata;
			if (this.folder.DisplayName != this.DeviceMetadata.Id.CompositeKey)
			{
				throw new ArgumentException(string.Format("[SyncStateStorage.ctor] The folder name '{0}' and the metadata name '{1}' should match.", this.folder.DisplayName, deviceMetadata.Id));
			}
			this.creationTime = (ExDateTime)this.folder.TryGetProperty(StoreObjectSchema.CreationTime);
			long num = 0L;
			long.TryParse(this.folder.GetValueOrDefault<string>(SyncStateStorage.airsyncLockProp, "0"), NumberStyles.Any, CultureInfo.InvariantCulture, out num);
			this.airsyncLock = ((num == 0L) ? ExDateTime.MinValue : ExDateTime.FromBinary(num));
			this.syncRootFolderId = folder.ParentId;
		}

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06007DDD RID: 32221 RVA: 0x0022A6C3 File Offset: 0x002288C3
		// (set) Token: 0x06007DDE RID: 32222 RVA: 0x0022A6CB File Offset: 0x002288CB
		public bool SaveOnDirectItems { get; set; }

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06007DDF RID: 32223 RVA: 0x0022A6D4 File Offset: 0x002288D4
		// (set) Token: 0x06007DE0 RID: 32224 RVA: 0x0022A6DC File Offset: 0x002288DC
		public DeviceSyncStateMetadata DeviceMetadata { get; private set; }

		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06007DE1 RID: 32225 RVA: 0x0022A6E5 File Offset: 0x002288E5
		public DeviceIdentity DeviceIdentity
		{
			get
			{
				return this.DeviceMetadata.Id;
			}
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x06007DE2 RID: 32226 RVA: 0x0022A6F2 File Offset: 0x002288F2
		public ExDateTime CreationTime
		{
			get
			{
				this.CheckDisposed("CreationTime");
				return this.creationTime;
			}
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x06007DE3 RID: 32227 RVA: 0x0022A705 File Offset: 0x00228905
		public StoreObjectId FolderId
		{
			get
			{
				this.CheckDisposed("FolderId");
				return this.folder.Id.ObjectId;
			}
		}

		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x06007DE4 RID: 32228 RVA: 0x0022A722 File Offset: 0x00228922
		public StoreObjectId SyncRootFolderId
		{
			get
			{
				this.CheckDisposed("SyncRootFolderId");
				return this.syncRootFolderId;
			}
		}

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x06007DE5 RID: 32229 RVA: 0x0022A735 File Offset: 0x00228935
		public bool SyncAllowedForDevice
		{
			get
			{
				this.CheckDisposed("SyncAllowedForDevice");
				return ExDateTime.Compare(ExDateTime.UtcNow, this.airsyncLock) >= 0;
			}
		}

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x06007DE6 RID: 32230 RVA: 0x0022A758 File Offset: 0x00228958
		public IExchangePrincipal MailboxOwner
		{
			get
			{
				this.CheckDisposed("MailboxOwner");
				MailboxSession mailboxSession = this.folder.Session as MailboxSession;
				if (mailboxSession == null)
				{
					throw new NotSupportedException();
				}
				return mailboxSession.MailboxOwner;
			}
		}

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x06007DE7 RID: 32231 RVA: 0x0022A790 File Offset: 0x00228990
		public PooledMemoryStream CompressedMemoryStream
		{
			get
			{
				this.CheckDisposed("CompressedMemoryStream");
				if (this.compressedMemoryStream == null)
				{
					this.compressedMemoryStream = new PooledMemoryStream(102400);
				}
				return this.compressedMemoryStream;
			}
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x0022A7BC File Offset: 0x002289BC
		public static SyncStateStorage Bind(MailboxSession mailboxSession, DeviceIdentity deviceIdentity, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			SyncStateTypeFactory.GetInstance().RegisterInternalBuilders();
			UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(mailboxSession, syncLogger);
			DeviceSyncStateMetadata device = userSyncStateMetadata.GetDevice(mailboxSession, deviceIdentity, syncLogger);
			if (device != null)
			{
				return SyncStateStorage.GetSyncStateStorage(mailboxSession, device, syncLogger);
			}
			return null;
		}

		// Token: 0x06007DE9 RID: 32233 RVA: 0x0022A80C File Offset: 0x00228A0C
		public static SyncStateStorage Create(MailboxSession mailboxSession, DeviceIdentity deviceIdentity, StateStorageFeatures features, ISyncLogger syncLogger = null)
		{
			return SyncStateStorage.Create(mailboxSession, deviceIdentity, features, false, syncLogger);
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x0022A818 File Offset: 0x00228A18
		public static SyncStateStorage Create(MailboxSession mailboxSession, DeviceIdentity deviceIdentity, StateStorageFeatures features, bool onlySetPropsIfAlreadyExists, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			EnumValidator.ThrowIfInvalid<StateStorageFeatures>(features, "features");
			SyncStateTypeFactory.GetInstance().RegisterInternalBuilders();
			UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(mailboxSession, syncLogger);
			DeviceSyncStateMetadata deviceSyncStateMetadata = userSyncStateMetadata.GetDevice(mailboxSession, deviceIdentity, syncLogger);
			SyncStateStorage syncStateStorage = (deviceSyncStateMetadata == null) ? null : SyncStateStorage.GetSyncStateStorage(mailboxSession, deviceSyncStateMetadata, syncLogger);
			if (syncStateStorage == null || onlySetPropsIfAlreadyExists)
			{
				Folder folder = null;
				SyncStateStorage syncStateStorage2 = null;
				bool flag = false;
				try
				{
					folder = SyncStateStorage.CreateAndSaveFolder(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.SyncRoot), CreateMode.OpenIfExists, deviceIdentity.CompositeKey, null, (syncStateStorage == null) ? null : syncStateStorage.folder, syncLogger);
					if (deviceSyncStateMetadata != null && deviceSyncStateMetadata.DeviceFolderId != folder.Id.ObjectId)
					{
						userSyncStateMetadata.TryRemove(deviceSyncStateMetadata.Id, syncLogger);
						deviceSyncStateMetadata = null;
					}
					if (deviceSyncStateMetadata == null)
					{
						deviceSyncStateMetadata = new DeviceSyncStateMetadata(mailboxSession, folder.Id.ObjectId, syncLogger);
						deviceSyncStateMetadata = userSyncStateMetadata.GetOrAdd(deviceSyncStateMetadata);
					}
					syncStateStorage2 = new SyncStateStorage(folder, deviceSyncStateMetadata, syncLogger);
					flag = true;
					return syncStateStorage2;
				}
				finally
				{
					if (!flag)
					{
						if (syncStateStorage2 != null)
						{
							syncStateStorage2.Dispose();
							syncStateStorage2 = null;
						}
						if (folder != null)
						{
							folder.Dispose();
							folder = null;
						}
					}
				}
				return syncStateStorage;
			}
			return syncStateStorage;
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x0022A930 File Offset: 0x00228B30
		public static bool DeleteSyncStateStorage(MailboxSession mailboxSession, DeviceIdentity deviceIdentity, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			syncLogger.Information<DeviceIdentity>(ExTraceGlobals.SyncTracer, 0L, "SyncStateStorage::DeleteSyncStateStorage. Need to delete folder {0}", deviceIdentity);
			SyncStateTypeFactory.GetInstance().RegisterInternalBuilders();
			UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(mailboxSession, syncLogger);
			List<DeviceSyncStateMetadata> allDevices = userSyncStateMetadata.GetAllDevices(mailboxSession, false, syncLogger);
			bool result = false;
			if (allDevices != null)
			{
				foreach (DeviceSyncStateMetadata deviceSyncStateMetadata in allDevices)
				{
					syncLogger.Information<DeviceIdentity>(ExTraceGlobals.SyncTracer, 0L, "SyncStateStorage::DeleteSyncStateStorage. Found syncstate folder {0}", deviceSyncStateMetadata.Id);
					if (string.Compare(deviceSyncStateMetadata.Id.Protocol, deviceIdentity.Protocol, StringComparison.Ordinal) == 0)
					{
						if (deviceSyncStateMetadata.Id.Equals(deviceIdentity))
						{
							mailboxSession.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
							{
								deviceSyncStateMetadata.DeviceFolderId
							});
							userSyncStateMetadata.TryRemove(deviceSyncStateMetadata.Id, syncLogger);
						}
						else
						{
							syncLogger.Information(ExTraceGlobals.SyncTracer, 0L, "SyncStateStorage::DeleteSyncStateStorage. found more devices with same protocol");
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06007DEC RID: 32236 RVA: 0x0022AA50 File Offset: 0x00228C50
		public static bool DeleteSyncStateStorage(MailboxSession mailboxSession, StoreObjectId deviceFolderId, DeviceIdentity deviceIdentity, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("folderId", deviceFolderId);
			bool result = false;
			SyncStateTypeFactory.GetInstance().RegisterInternalBuilders();
			UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(mailboxSession, syncLogger);
			AggregateOperationResult aggregateOperationResult = mailboxSession.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
			{
				deviceFolderId
			});
			syncLogger.Information<string>(ExTraceGlobals.SyncTracer, 0L, "SyncStateStorage::DeleteSyncStateStorage. Result = {0}", aggregateOperationResult.OperationResult.ToString());
			userSyncStateMetadata.TryRemove(deviceIdentity, syncLogger);
			List<DeviceSyncStateMetadata> allDevices = userSyncStateMetadata.GetAllDevices(mailboxSession, false, syncLogger);
			if (allDevices != null)
			{
				foreach (DeviceSyncStateMetadata deviceSyncStateMetadata in allDevices)
				{
					syncLogger.Information<DeviceIdentity, DeviceIdentity>(ExTraceGlobals.SyncTracer, 0L, "SyncStateStorage::DeleteSyncStateStorage. Found device folder '{0}', Looking for folder '{1}'", deviceSyncStateMetadata.Id, deviceIdentity);
					if (deviceSyncStateMetadata.Id.Equals(deviceIdentity))
					{
						try
						{
							aggregateOperationResult = mailboxSession.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
							{
								deviceSyncStateMetadata.DeviceFolderId
							});
							syncLogger.Information<DeviceIdentity, DeviceIdentity, string>(ExTraceGlobals.SyncTracer, 0L, "SyncStateStorage::DeleteSyncStateStorage. try Deleting SyncState folder again.folderName:{0}, originalFolder:{1}, result:{2}", deviceSyncStateMetadata.Id, deviceIdentity, aggregateOperationResult.OperationResult.ToString());
							continue;
						}
						catch
						{
							syncLogger.Information(ExTraceGlobals.SyncTracer, 0L, "SyncStateStorage::DeleteSyncStateStorage. Error deleting the sync state folder.");
							continue;
						}
					}
					if (deviceSyncStateMetadata.Id.IsProtocol("AirSync"))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x0022ABD8 File Offset: 0x00228DD8
		public static SyncStateStorage.SyncStateStorageEnumerator GetEnumerator(MailboxSession mailboxSession, ISyncLogger syncLogger = null)
		{
			return new SyncStateStorage.SyncStateStorageEnumerator(mailboxSession, syncLogger);
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x0022ABE4 File Offset: 0x00228DE4
		public static void UpdateMailboxLoggingEnabled(MailboxSession mailboxSession, bool mailboxLoggingEnabled, ISyncLogger syncLogger = null)
		{
			using (Folder syncFolderRoot = SyncStateStorage.GetSyncFolderRoot(mailboxSession, syncLogger))
			{
				if (mailboxLoggingEnabled)
				{
					syncFolderRoot[SyncStateStorage.airsyncMailboxLoggingEnabledProp] = ExDateTime.UtcNow;
				}
				else
				{
					syncFolderRoot.Delete(SyncStateStorage.airsyncMailboxLoggingEnabledProp);
				}
				syncFolderRoot.Save();
				syncFolderRoot.Load();
				Folder folder = null;
				try
				{
					folder = SyncStateStorage.CreateAndSaveFolder(mailboxSession, syncFolderRoot.Id.ObjectId, CreateMode.OpenIfExists, SyncStateStorage.MailboxLoggingTriggerFolder, null, null, syncLogger);
					if (folder != null)
					{
						syncFolderRoot.DeleteObjects(DeleteItemFlags.SoftDelete, new StoreId[]
						{
							folder.Id.ObjectId
						});
					}
				}
				finally
				{
					if (folder != null)
					{
						folder.Dispose();
					}
				}
			}
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x0022ACA0 File Offset: 0x00228EA0
		public static bool GetMailboxLoggingEnabled(MailboxSession mailboxSession, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			if (mailboxSession.IsConnected)
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.SyncRoot);
				if (defaultFolderId == null)
				{
					return false;
				}
				using (Folder folder = Folder.Bind(mailboxSession, defaultFolderId, SyncStateStorage.loggingEnabledAsArray))
				{
					return SyncStateStorage.IsMailboxLoggingEnabled(folder);
				}
			}
			syncLogger.TraceDebug(ExTraceGlobals.SyncProcessTracer, 0L, "[SyncStateStorage.GetMailboxLoggingEnabled] MailboxSession was not connected - defaulting to false since we can't write to the mailbox.");
			return false;
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x0022AD14 File Offset: 0x00228F14
		public CustomSyncState CreateCustomSyncState(SyncStateInfo syncStateInfo)
		{
			this.CheckDisposed("CreateCustomSyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::CreateCustomSyncState. Hashcode = {0}", this.GetHashCode());
			if (syncStateInfo == null)
			{
				throw new ArgumentNullException("syncStateInfo");
			}
			return CustomSyncState.CreateSyncState(this, syncStateInfo, this.folder, this.syncLogger);
		}

		// Token: 0x06007DF1 RID: 32241 RVA: 0x0022AD70 File Offset: 0x00228F70
		public FolderHierarchySyncState CreateFolderHierarchySyncState()
		{
			this.CheckDisposed("CreateFolderHierarchySyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::CreateFolderHierarchySyncState. Hashcode = {0}", this.GetHashCode());
			return FolderHierarchySyncState.CreateSyncState(this, this.folder, this.syncLogger);
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x0022ADBC File Offset: 0x00228FBC
		public FolderSyncState CreateFolderSyncState(ISyncProviderFactory syncProviderFactory, string syncFolderId)
		{
			this.CheckDisposed("CreateFolderSyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::CreateFolderSyncState. Hashcode = {0}", this.GetHashCode());
			ArgumentValidator.ThrowIfNull("syncProviderFactory", syncProviderFactory);
			ArgumentValidator.ThrowIfNull("syncFolderId", syncFolderId);
			return FolderSyncState.CreateSyncState(this, this.folder, syncProviderFactory, syncFolderId, this.syncLogger);
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x0022AE20 File Offset: 0x00229020
		public bool DeleteAllSyncStatesIfMoved()
		{
			long num = 0L;
			using (QueryResult queryResult = this.folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
			{
				AirSyncStateSchema.MetadataLastSyncTime
			}))
			{
				for (;;)
				{
					object[][] rows = queryResult.GetRows(100);
					if (rows == null || rows.Length == 0)
					{
						break;
					}
					foreach (object[] array2 in rows)
					{
						if (!(array2[0] is PropertyError))
						{
							long num2 = (long)array2[0];
							if (num2 > num)
							{
								num = num2;
							}
						}
					}
				}
			}
			if (this.CreationTime.UtcTicks > num)
			{
				this.syncLogger.TraceDebug<ExDateTime, long>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "Detected mailbox moved! syncStateStorage.CreationTime = {0}, lastSyncTime = {1}. Deleting all SyncState...", this.CreationTime, num);
				this.DeleteAllSyncStates();
				return true;
			}
			return false;
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x0022AEF8 File Offset: 0x002290F8
		public GroupOperationResult DeleteAllSyncStates()
		{
			this.CheckDisposed("DeleteAllSyncStates");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.RemoveAllSyncStates] HashCode = {0}.", this.GetHashCode());
			this.DeviceMetadata.RemoveAll();
			GroupOperationResult groupOperationResult = this.folder.DeleteAllObjects(DeleteItemFlags.SoftDelete);
			this.syncLogger.Information<OperationResult, LocalizedException>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.RemoveAllSyncStates] Result: '{0}', Exception: '{1}' \n", groupOperationResult.OperationResult, groupOperationResult.Exception);
			return groupOperationResult;
		}

		// Token: 0x06007DF5 RID: 32245 RVA: 0x0022AF74 File Offset: 0x00229174
		public AggregateOperationResult DeleteCustomSyncState(SyncStateInfo syncStateInfo)
		{
			this.CheckDisposed("DeleteCustomSyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.DeleteCustomSyncState] Hashcode = {0}", this.GetHashCode());
			ArgumentValidator.ThrowIfNull("syncStateInfo", syncStateInfo);
			return this.InternalDeleteSyncState(syncStateInfo.UniqueName);
		}

		// Token: 0x06007DF6 RID: 32246 RVA: 0x0022AFC8 File Offset: 0x002291C8
		private AggregateOperationResult InternalDeleteSyncState(string name)
		{
			this.CheckDisposed("DeleteCustomSyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::InternalDeleteSyncState. Hashcode = {0}", this.GetHashCode());
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			SyncStateMetadata syncState = this.DeviceMetadata.GetSyncState(this.folder.Session as MailboxSession, name, this.syncLogger);
			this.DeviceMetadata.TryRemove(name, this.syncLogger);
			AggregateOperationResult result = null;
			if (syncState != null)
			{
				if (syncState.FolderSyncStateId == null && syncState.ItemSyncStateId != null)
				{
					this.syncLogger.Information<string>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.InternalDeleteSyncState] Deleting direct item sync state {0}", name);
					result = this.folder.DeleteObjects(DeleteItemFlags.SoftDelete, new StoreId[]
					{
						syncState.ItemSyncStateId
					});
				}
				else if (syncState.FolderSyncStateId != null)
				{
					this.syncLogger.Information<string>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.InternalDeleteSyncState] Deleting sync folder for sync state {0}", name);
					result = this.folder.DeleteObjects(DeleteItemFlags.SoftDelete, new StoreId[]
					{
						syncState.FolderSyncStateId
					});
				}
				else
				{
					this.syncLogger.Information<string>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.InternalDeleteSyncState] Metadata had null for both item and folder id.  Weird - not deleting anything.  Sync State: {0}", name);
				}
				StoreObjectId storeObjectId = (syncState.FolderSyncStateId != null) ? syncState.FolderSyncStateId : syncState.ItemSyncStateId;
				if (storeObjectId != null)
				{
					this.TraceAggregateOperationResultFromDelete(result, name, storeObjectId);
				}
			}
			return result;
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x0022B124 File Offset: 0x00229324
		public AggregateOperationResult DeleteFolderHierarchySyncState()
		{
			this.CheckDisposed("DeleteFolderHierarchySyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::DeleteFolderHierarchySyncState. Hashcode = {0}", this.GetHashCode());
			FolderHierarchySyncStateInfo folderHierarchySyncStateInfo = new FolderHierarchySyncStateInfo();
			return this.InternalDeleteSyncState(folderHierarchySyncStateInfo.UniqueName);
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x0022B170 File Offset: 0x00229370
		public AggregateOperationResult DeleteFolderSyncState(string syncFolderName)
		{
			this.CheckDisposed("DeleteFolderSyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::DeleteFolderSyncState. Hashcode = {0}", this.GetHashCode());
			ArgumentValidator.ThrowIfNullOrEmpty("syncFolderName", syncFolderName);
			return this.InternalDeleteSyncState(syncFolderName);
		}

		// Token: 0x06007DF9 RID: 32249 RVA: 0x0022B1BC File Offset: 0x002293BC
		public AggregateOperationResult DeleteFolderSyncState(ISyncProviderFactory syncFactory)
		{
			this.CheckDisposed("DeleteFolderSyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::DeleteFolderSyncState. Hashcode = {0}", this.GetHashCode());
			ArgumentValidator.ThrowIfNull("syncFactory", syncFactory);
			StoreObjectId storeObjectId = StoreObjectId.Deserialize(syncFactory.GetCollectionIdBytes());
			FolderSyncStateMetadata folderSyncStateMetadata;
			if (this.DeviceMetadata.SyncStatesByIPMFolderId.TryGetValue(storeObjectId, out folderSyncStateMetadata))
			{
				return this.InternalDeleteSyncState(folderSyncStateMetadata.Name);
			}
			this.syncLogger.TraceDebug<string>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.DeleteFolderSyncState] Did not find cached mapping for IPM Folder Id {0}", storeObjectId.ToBase64String());
			return null;
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x0022B252 File Offset: 0x00229452
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SyncStateStorage>(this);
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x0022B25A File Offset: 0x0022945A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x0022B26F File Offset: 0x0022946F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x0022B280 File Offset: 0x00229480
		public CustomSyncState GetCustomSyncState(SyncStateInfo syncStateInfo, params PropertyDefinition[] additionalPropsToFetch)
		{
			this.CheckDisposed("GetCustomSyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::GetCustomSyncState. Hashcode = {0}", this.GetHashCode());
			this.GetMailboxSession();
			CustomSyncState result = null;
			try
			{
				result = CustomSyncState.GetSyncState(this, this.folder, syncStateInfo, this.syncLogger, additionalPropsToFetch);
			}
			catch (ObjectNotFoundException arg)
			{
				this.syncLogger.TraceDebug<string, ObjectNotFoundException>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.GetCustomSyncState] Hark! SyncState {0} was not found.  Exception: {1}", syncStateInfo.UniqueName, arg);
			}
			return result;
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x0022B314 File Offset: 0x00229514
		private MailboxSession GetMailboxSession()
		{
			MailboxSession mailboxSession = this.folder.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new NotSupportedException();
			}
			return mailboxSession;
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x0022B33C File Offset: 0x0022953C
		public FolderHierarchySyncState GetFolderHierarchySyncState()
		{
			this.CheckDisposed("GetFolderHierarchySyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::GetFolderHierarchySyncState. Hashcode = {0}", this.GetHashCode());
			this.GetMailboxSession();
			FolderHierarchySyncState result = null;
			try
			{
				result = FolderHierarchySyncState.GetSyncState(this, this.folder, this.syncLogger);
			}
			catch (ObjectNotFoundException arg)
			{
				this.syncLogger.TraceDebug<string, ObjectNotFoundException>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.GetFolderHierarchySyncState] Hark! SyncState {0} was not found.  Exception: {1}", "FolderHierarchy", arg);
			}
			return result;
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x0022B3CC File Offset: 0x002295CC
		public FolderSyncState GetFolderSyncState(string syncStateName)
		{
			return this.GetFolderSyncState(null, syncStateName);
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x0022B3D6 File Offset: 0x002295D6
		public FolderSyncState GetFolderSyncState(ISyncProviderFactory syncProviderFactory)
		{
			return this.GetFolderSyncState(syncProviderFactory, null);
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x0022B3E0 File Offset: 0x002295E0
		public FolderSyncState GetFolderSyncState(ISyncProviderFactory syncProviderFactory, string syncStateName)
		{
			return this.GetFolderSyncState(syncProviderFactory, syncStateName, null);
		}

		// Token: 0x06007E03 RID: 32259 RVA: 0x0022B3EC File Offset: 0x002295EC
		public FolderSyncState GetFolderSyncState(ISyncProviderFactory syncProviderFactory, string syncFolderName, Func<SyncStateStorage, StoreObject, FolderSyncStateMetadata, SyncStateInfo, ISyncProviderFactory, bool, ISyncLogger, FolderSyncState> creator)
		{
			this.CheckDisposed("GetFolderSyncState");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::GetFolderSyncState. Hashcode = {0}", this.GetHashCode());
			if (syncProviderFactory == null && string.IsNullOrEmpty(syncFolderName))
			{
				throw new ArgumentNullException("syncProviderFactory and syncFolderName");
			}
			FolderSyncState result;
			try
			{
				result = (string.IsNullOrEmpty(syncFolderName) ? FolderSyncState.GetSyncState(this, this.folder, syncProviderFactory, creator, this.syncLogger) : FolderSyncState.GetSyncState(this, this.folder, syncProviderFactory, syncFolderName, creator, this.syncLogger));
			}
			catch (ObjectNotFoundException arg)
			{
				this.syncLogger.TraceDebug<string, ObjectNotFoundException>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncStateStorage.GetFolderSyncState] Hark! SyncState '{0}' was not found.  Exception: {1}", (syncFolderName == null) ? "<Null>" : syncFolderName, arg);
				result = null;
			}
			return result;
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x0022B4B0 File Offset: 0x002296B0
		public int CountFolderSyncStates()
		{
			this.CheckDisposed("CountFolderSyncStates");
			this.syncLogger.Information<int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::CountFolderSyncStates. HashCode = {0}.", this.GetHashCode());
			return this.DeviceMetadata.FolderSyncStateCount;
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x0022B4EA File Offset: 0x002296EA
		public static string ConstructSyncFolderName(string protocol, string deviceType, string deviceId)
		{
			return string.Format("{0}-{1}-{2}", protocol, deviceType, deviceId);
		}

		// Token: 0x06007E06 RID: 32262 RVA: 0x0022B4FC File Offset: 0x002296FC
		private static Folder CreateAndSaveFolder(MailboxSession mailboxSession, StoreObjectId containerId, CreateMode createMode, string displayName, string containerClass, Folder folderIn, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			Folder folder = null;
			bool flag = false;
			Folder result;
			try
			{
				if (folderIn == null)
				{
					folder = Folder.Create(mailboxSession, containerId, StoreObjectType.Folder, displayName, createMode);
					folder[SyncStateStorage.airsyncLockProp] = "0";
				}
				else
				{
					folder = folderIn;
				}
				if (containerClass != null)
				{
					folder[InternalSchema.ContainerClass] = containerClass;
				}
				StoreObjectId storeObjectId = null;
				if (!folder.IsNew)
				{
					storeObjectId = folder.Id.ObjectId;
				}
				FolderSaveResult folderSaveResult = folder.Save();
				if (folderSaveResult.OperationResult != OperationResult.Succeeded)
				{
					syncLogger.TraceDebug<string, FolderSaveResult>(ExTraceGlobals.SyncTracer, 0L, "SyncStateStorage::CreateAndSaveFolder. Failed to create folder {0}, due to {1}.", displayName, folderSaveResult);
					if (storeObjectId == null)
					{
						folder.Load(null);
						storeObjectId = folder.StoreObjectId;
					}
					mailboxSession.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
					{
						storeObjectId
					});
					throw folderSaveResult.ToException(ServerStrings.ExCannotCreateFolder(folderSaveResult));
				}
				folder.Load(SyncStateStorage.loggingEnabledAndCreateTimeAsArray);
				flag = true;
				result = folder;
			}
			finally
			{
				if (!flag && folder != null)
				{
					folder.Dispose();
					folder = null;
				}
			}
			return result;
		}

		// Token: 0x06007E07 RID: 32263 RVA: 0x0022B5F0 File Offset: 0x002297F0
		internal static Folder GetSyncFolderRoot(MailboxSession mailboxSession, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			StoreObjectId storeObjectId = mailboxSession.GetDefaultFolderId(DefaultFolderType.SyncRoot);
			if (storeObjectId == null)
			{
				syncLogger.TraceDebug<Guid>(ExTraceGlobals.SyncTracer, 0L, "[SyncStateStorage.GetSyncFolderRoot] ExchangeSyncData folder missing for mailbox {0}.  Creating it now.", mailboxSession.MailboxGuid);
				storeObjectId = mailboxSession.CreateDefaultFolder(DefaultFolderType.SyncRoot);
			}
			return Folder.Bind(mailboxSession, storeObjectId, SyncStateStorage.loggingEnabledAsArray);
		}

		// Token: 0x06007E08 RID: 32264 RVA: 0x0022B640 File Offset: 0x00229840
		private static SyncStateStorage GetSyncStateStorage(MailboxSession session, DeviceSyncStateMetadata deviceMetadata, ISyncLogger syncLogger = null)
		{
			if (deviceMetadata == null)
			{
				return null;
			}
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			Folder folder = null;
			SyncStateStorage syncStateStorage = null;
			bool flag = false;
			SyncStateStorage result;
			try
			{
				try
				{
					folder = Folder.Bind(session, deviceMetadata.DeviceFolderId, SyncStateStorage.loggingEnabledAndCreateTimeAsArray);
					syncStateStorage = new SyncStateStorage(folder, deviceMetadata, syncLogger);
				}
				catch (ObjectNotFoundException)
				{
					syncLogger.TraceDebug<DeviceSyncStateMetadata>(ExTraceGlobals.SyncTracer, 0L, "[SyncStateStorage.Create] Did not find SyncStateStorage for device {0}.  Removing from cache.", deviceMetadata);
					UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(session, syncLogger);
					userSyncStateMetadata.TryRemove(deviceMetadata.Id, syncLogger);
					deviceMetadata = userSyncStateMetadata.GetDevice(session, deviceMetadata.Id, syncLogger);
					if (deviceMetadata != null)
					{
						folder = Folder.Bind(session, deviceMetadata.DeviceFolderId, SyncStateStorage.loggingEnabledAndCreateTimeAsArray);
						syncStateStorage = new SyncStateStorage(folder, deviceMetadata, syncLogger);
					}
				}
				flag = true;
				result = syncStateStorage;
			}
			finally
			{
				if (!flag)
				{
					if (syncStateStorage != null)
					{
						syncStateStorage.Dispose();
						syncStateStorage = null;
					}
					if (folder != null)
					{
						folder.Dispose();
						folder = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x0022B720 File Offset: 0x00229920
		internal static bool IsMailboxLoggingEnabled(Folder rootFolder)
		{
			ExDateTime? loggingEnabledTime = SyncStateStorage.GetLoggingEnabledTime(rootFolder);
			return loggingEnabledTime != null && ExDateTime.UtcNow < loggingEnabledTime.Value.IncrementDays(SyncStateStorage.loggingExpirationInDays);
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x0022B760 File Offset: 0x00229960
		internal static ExDateTime? GetLoggingEnabledTime(Folder rootFolder)
		{
			if (rootFolder == null)
			{
				return null;
			}
			return rootFolder.GetValueAsNullable<ExDateTime>(SyncStateStorage.airsyncMailboxLoggingEnabledProp);
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x0022B788 File Offset: 0x00229988
		private void TraceAggregateOperationResultFromDelete(AggregateOperationResult result, string syncStateName, StoreObjectId id)
		{
			string text = null;
			if (result == null)
			{
				text = string.Format("SyncState '{0}' with ID = '{1}' doesn't exist.", syncStateName, id);
			}
			else if (!result.OperationResult.Equals(OperationResult.Succeeded))
			{
				StringBuilder stringBuilder = new StringBuilder(500);
				stringBuilder.AppendFormat("SyncState '{0}' is not deleted properly. Result: '{1}' ", syncStateName, result);
				stringBuilder.Append("GroupOperationResult: \n\n");
				if (result.GroupOperationResults != null)
				{
					foreach (GroupOperationResult groupOperationResult in result.GroupOperationResults)
					{
						stringBuilder.AppendFormat("Result: '{0}', Exception: '{1}' \n", groupOperationResult.OperationResult, groupOperationResult.Exception);
					}
				}
				text = stringBuilder.ToString();
			}
			if (text != null)
			{
				this.syncLogger.Information(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), text);
			}
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x0022B84E File Offset: 0x00229A4E
		private void CheckDisposed(string methodName)
		{
			if (this.disposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x0022B870 File Offset: 0x00229A70
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.disposed, disposing);
			if (!this.disposed)
			{
				this.disposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x0022B898 File Offset: 0x00229A98
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.folder != null)
				{
					this.folder.Dispose();
				}
				if (this.compressedMemoryStream != null)
				{
					this.compressedMemoryStream.Dispose();
					this.compressedMemoryStream = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
			this.folder = null;
		}

		// Token: 0x040055CE RID: 21966
		public static readonly string MailboxLoggingTriggerFolder = "MailboxLoggingTriggerFolder";

		// Token: 0x040055CF RID: 21967
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040055D0 RID: 21968
		private static int loggingExpirationInDays = 3;

		// Token: 0x040055D1 RID: 21969
		private static StorePropertyDefinition airsyncLockProp = GuidNamePropertyDefinition.CreateCustom("AirSyncLock", typeof(string), WellKnownPropertySet.AirSync, "AirSync:AirSyncLock", PropertyFlags.None);

		// Token: 0x040055D2 RID: 21970
		internal static StorePropertyDefinition airsyncMailboxLoggingEnabledProp = GuidNamePropertyDefinition.CreateCustom("AirSyncMailboxLoggingEnabled", typeof(ExDateTime), WellKnownPropertySet.AirSync, "AirSync:AirSyncMailboxLoggingEnabled", PropertyFlags.None);

		// Token: 0x040055D3 RID: 21971
		private static readonly PropertyDefinition[] loggingEnabledAsArray = new PropertyDefinition[]
		{
			SyncStateStorage.airsyncMailboxLoggingEnabledProp
		};

		// Token: 0x040055D4 RID: 21972
		private static readonly PropertyDefinition[] loggingEnabledAndCreateTimeAsArray = new PropertyDefinition[]
		{
			StoreObjectSchema.CreationTime,
			SyncStateStorage.airsyncLockProp
		};

		// Token: 0x040055D5 RID: 21973
		private ExDateTime airsyncLock;

		// Token: 0x040055D6 RID: 21974
		private ExDateTime creationTime;

		// Token: 0x040055D7 RID: 21975
		private ISyncLogger syncLogger;

		// Token: 0x040055D8 RID: 21976
		private Folder folder;

		// Token: 0x040055D9 RID: 21977
		private bool disposed;

		// Token: 0x040055DA RID: 21978
		private StoreObjectId syncRootFolderId;

		// Token: 0x040055DB RID: 21979
		private PooledMemoryStream compressedMemoryStream;

		// Token: 0x02000E35 RID: 3637
		internal class SyncStateStorageEnumerator : IEnumerator, IDisposable
		{
			// Token: 0x06007E10 RID: 32272 RVA: 0x0022B988 File Offset: 0x00229B88
			public SyncStateStorageEnumerator(MailboxSession mailboxSession, ISyncLogger syncLogger = null)
			{
				this.syncLogger = (syncLogger ?? TracingLogger.Singleton);
				UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(mailboxSession, this.syncLogger);
				this.devices = userSyncStateMetadata.GetAllDevices(mailboxSession, true, this.syncLogger);
				this.index = -1;
				this.mailboxSession = mailboxSession;
			}

			// Token: 0x170021C8 RID: 8648
			// (get) Token: 0x06007E11 RID: 32273 RVA: 0x0022B9DF File Offset: 0x00229BDF
			public object Current
			{
				get
				{
					this.CheckDisposed("get_Current");
					return this.current;
				}
			}

			// Token: 0x06007E12 RID: 32274 RVA: 0x0022B9F2 File Offset: 0x00229BF2
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x06007E13 RID: 32275 RVA: 0x0022B9FC File Offset: 0x00229BFC
			public bool MoveNext()
			{
				this.CheckDisposed("MoveNext");
				if (this.current != null)
				{
					this.current.Dispose();
					this.current = null;
				}
				if (this.devices == null || this.index >= this.devices.Count)
				{
					return false;
				}
				this.index++;
				if (this.index >= this.devices.Count)
				{
					return false;
				}
				this.current = SyncStateStorage.GetSyncStateStorage(this.mailboxSession, this.devices[this.index], this.syncLogger);
				return this.current != null || this.MoveNext();
			}

			// Token: 0x06007E14 RID: 32276 RVA: 0x0022BAA6 File Offset: 0x00229CA6
			public void Reset()
			{
				this.CheckDisposed("Reset");
				if (this.current != null)
				{
					this.current.Dispose();
				}
				this.current = null;
				this.index = -1;
			}

			// Token: 0x06007E15 RID: 32277 RVA: 0x0022BAD4 File Offset: 0x00229CD4
			private void CheckDisposed(string methodName)
			{
				if (this.disposed)
				{
					this.syncLogger.TraceDebug<string, Type, int>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorageEnumerator::{0}. Object type = {1}, hashcode = {2} was already disposed.", methodName, base.GetType(), this.GetHashCode());
					throw new ObjectDisposedException(base.GetType().ToString());
				}
			}

			// Token: 0x06007E16 RID: 32278 RVA: 0x0022BB24 File Offset: 0x00229D24
			private void Dispose(bool disposing)
			{
				this.syncLogger.Information<int, Type, bool>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncStateStorage::Dispose. HashCode = {0}, type = {1}, disposing = {2}.", this.GetHashCode(), base.GetType(), disposing);
				if (!this.disposed)
				{
					this.disposed = true;
					this.InternalDispose(disposing);
				}
			}

			// Token: 0x06007E17 RID: 32279 RVA: 0x0022BB70 File Offset: 0x00229D70
			private void InternalDispose(bool disposing)
			{
				if (disposing && this.current != null)
				{
					this.current.Dispose();
				}
				this.mailboxSession = null;
				this.current = null;
			}

			// Token: 0x040055DE RID: 21982
			private SyncStateStorage current;

			// Token: 0x040055DF RID: 21983
			private int index;

			// Token: 0x040055E0 RID: 21984
			private bool disposed;

			// Token: 0x040055E1 RID: 21985
			private List<DeviceSyncStateMetadata> devices;

			// Token: 0x040055E2 RID: 21986
			private MailboxSession mailboxSession;

			// Token: 0x040055E3 RID: 21987
			private ISyncLogger syncLogger;
		}

		// Token: 0x02000E36 RID: 3638
		private class MemoryStream100K : MemoryStream
		{
			// Token: 0x06007E18 RID: 32280 RVA: 0x0022BB96 File Offset: 0x00229D96
			public MemoryStream100K() : base(102400)
			{
			}
		}
	}
}
