using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E01 RID: 3585
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SyncState : ISyncState, IDisposeTrackable, IDisposable
	{
		// Token: 0x06007B44 RID: 31556 RVA: 0x0021FF30 File Offset: 0x0021E130
		protected SyncState(SyncStateStorage syncStateStorage, StoreObject storeObject, SyncStateMetadata metadata, SyncStateInfo syncStateInfo, bool syncStateIsNew, ISyncLogger syncLogger = null)
		{
			this.syncLogger = (syncLogger ?? TracingLogger.Singleton);
			using (DisposeGuard disposeGuard = this.Guard())
			{
				StorageGlobals.TraceConstructIDisposable(this);
				this.disposeTracker = this.GetDisposeTracker();
				if (!(storeObject is Folder) && !(storeObject is Item))
				{
					throw new ArgumentException("storeObject is of invalid type: " + storeObject.GetType(), "storeObject");
				}
				this.storeObject = storeObject;
				this.syncStateInfo = syncStateInfo;
				this.syncStateStorage = syncStateStorage;
				this.syncStateIsNew = syncStateIsNew;
				this.SyncStateMetadata = metadata;
				this.Load(false, new PropertyDefinition[0]);
				disposeGuard.Success();
			}
		}

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x06007B45 RID: 31557 RVA: 0x0021FFFC File Offset: 0x0021E1FC
		// (set) Token: 0x06007B46 RID: 31558 RVA: 0x00220004 File Offset: 0x0021E204
		public SyncStateMetadata SyncStateMetadata { get; private set; }

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x06007B47 RID: 31559 RVA: 0x00220010 File Offset: 0x0021E210
		// (set) Token: 0x06007B48 RID: 31560 RVA: 0x0022003C File Offset: 0x0021E23C
		public int? BackendVersion
		{
			get
			{
				this.CheckDisposed("get_BackendVersion");
				return this.GetData<NullableData<Int32Data, int>, int?>(SyncStateProp.Version, null);
			}
			private set
			{
				this.CheckDisposed("set_BackendVersion");
				this[SyncStateProp.Version] = new NullableData<Int32Data, int>(value);
			}
		}

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x06007B49 RID: 31561 RVA: 0x0022005A File Offset: 0x0021E25A
		// (set) Token: 0x06007B4A RID: 31562 RVA: 0x0022006D File Offset: 0x0021E26D
		public bool KeepCachedDataOnReload
		{
			get
			{
				this.CheckDisposed("get_KeepCachedDataOnReload");
				return this.keepCachedDataOnReload;
			}
			set
			{
				this.CheckDisposed("set_KeepCachedDataOnReload");
				this.keepCachedDataOnReload = value;
			}
		}

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x06007B4B RID: 31563 RVA: 0x00220084 File Offset: 0x0021E284
		public string UniqueName
		{
			get
			{
				this.CheckDisposed("get_UniqueName");
				if (this.syncStateInfo.UniqueName == null)
				{
					if (this.storeObject is Folder)
					{
						this.syncStateInfo.UniqueName = this.storeObject.GetValueOrDefault<string>(InternalSchema.DisplayName);
					}
					else
					{
						this.syncStateInfo.UniqueName = this.storeObject.GetValueOrDefault<string>(InternalSchema.Subject);
					}
				}
				return this.syncStateInfo.UniqueName;
			}
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x06007B4C RID: 31564 RVA: 0x002200F9 File Offset: 0x0021E2F9
		// (set) Token: 0x06007B4D RID: 31565 RVA: 0x00220111 File Offset: 0x0021E311
		public int Version
		{
			get
			{
				this.CheckDisposed("get_Version");
				return this.syncStateInfo.Version;
			}
			set
			{
				this.CheckDisposed("set_Version");
				throw new InvalidOperationException("SyncState.Version is not settable.  It should be set through SyncStateInfo");
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x06007B4E RID: 31566 RVA: 0x00220128 File Offset: 0x0021E328
		public StoreObject StoreObject
		{
			get
			{
				this.CheckDisposed("get_StoreObject");
				return this.storeObject;
			}
		}

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x06007B4F RID: 31567 RVA: 0x0022013B File Offset: 0x0021E33B
		public bool SyncStateIsNew
		{
			get
			{
				this.CheckDisposed("get_SyncStateIsNew");
				return this.syncStateIsNew;
			}
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x06007B50 RID: 31568 RVA: 0x0022014E File Offset: 0x0021E34E
		internal SyncStateStorage SyncStateStorage
		{
			get
			{
				this.CheckDisposed("get_SyncStateStorage");
				return this.syncStateStorage;
			}
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x06007B51 RID: 31569 RVA: 0x00220161 File Offset: 0x0021E361
		public int TotalSaveCount
		{
			get
			{
				this.CheckDisposed("get_TotalSaveCount");
				return this.totalSaveCount;
			}
		}

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x06007B52 RID: 31570 RVA: 0x00220174 File Offset: 0x0021E374
		public int ColdSaveCount
		{
			get
			{
				this.CheckDisposed("get_ColdSaveCount");
				return this.coldSaveCount;
			}
		}

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x06007B53 RID: 31571 RVA: 0x00220187 File Offset: 0x0021E387
		public int ColdCopyCount
		{
			get
			{
				this.CheckDisposed("get_ColdCopyCount");
				return this.coldCopyCount;
			}
		}

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x06007B54 RID: 31572 RVA: 0x0022019A File Offset: 0x0021E39A
		public int TotalLoadCount
		{
			get
			{
				this.CheckDisposed("get_TotalLoadCount");
				return this.totalLoadCount;
			}
		}

		// Token: 0x17002111 RID: 8465
		public ICustomSerializableBuilder this[string key]
		{
			get
			{
				this.CheckDisposed("get_this[]");
				GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTableForKey = this.GetSyncStateTableForKey(key);
				DerivedData<ICustomSerializableBuilder> derivedData;
				if (syncStateTableForKey.Data.TryGetValue(key, out derivedData))
				{
					return derivedData.Data;
				}
				return null;
			}
			set
			{
				this.CheckDisposed("set_this[]");
				this.GetSyncStateTableForKey(key).Data[key] = new DerivedData<ICustomSerializableBuilder>(value);
			}
		}

		// Token: 0x06007B57 RID: 31575 RVA: 0x0022020D File Offset: 0x0021E40D
		public virtual void Commit()
		{
			if (this.syncStateInfo.ReadOnly)
			{
				throw new ApplicationException("Commit should not be called when readOnly is set to true.");
			}
			this.Commit(null, null);
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x00220230 File Offset: 0x0021E430
		public bool Contains(string key)
		{
			this.CheckDisposed("Contains");
			GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTableForKey = this.GetSyncStateTableForKey(key);
			return syncStateTableForKey.Data.ContainsKey(key);
		}

		// Token: 0x06007B59 RID: 31577
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06007B5A RID: 31578 RVA: 0x0022025C File Offset: 0x0021E45C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x00220271 File Offset: 0x0021E471
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x00220280 File Offset: 0x0021E480
		public long GetColdStateCompressedSize()
		{
			this.CheckDisposed("GetColdStateCompressedSize");
			if (this.syncStateIsNew)
			{
				return 0L;
			}
			if (0L == this.hotDataBeginsAt)
			{
				return 0L;
			}
			if (0L == this.coldDataBeginsAt)
			{
				return 0L;
			}
			return this.hotDataBeginsAt - this.coldDataBeginsAt;
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x002202CC File Offset: 0x0021E4CC
		public RawT GetData<T, RawT>(string key, RawT defaultValue) where T : ComponentData<RawT>, new()
		{
			this.CheckDisposed("GetData");
			GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTableForKey = this.GetSyncStateTableForKey(key);
			DerivedData<ICustomSerializableBuilder> derivedData;
			if (!syncStateTableForKey.Data.TryGetValue(key, out derivedData))
			{
				return defaultValue;
			}
			if (derivedData == null || derivedData.Data == null)
			{
				return defaultValue;
			}
			T t = (T)((object)derivedData.Data);
			return t.Data;
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x00220324 File Offset: 0x0021E524
		public static RawT GetData<T, RawT>(string key, RawT defaultValue, GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTable) where T : ComponentData<RawT>, new()
		{
			DerivedData<ICustomSerializableBuilder> derivedData;
			if (!syncStateTable.Data.TryGetValue(key, out derivedData))
			{
				return defaultValue;
			}
			if (derivedData == null || derivedData.Data == null)
			{
				return defaultValue;
			}
			T t = (T)((object)derivedData.Data);
			return t.Data;
		}

		// Token: 0x06007B5F RID: 31583 RVA: 0x0022036C File Offset: 0x0021E56C
		public T GetData<T>(string key) where T : ICustomSerializable, new()
		{
			this.CheckDisposed("GetData");
			GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTableForKey = this.GetSyncStateTableForKey(key);
			DerivedData<ICustomSerializableBuilder> derivedData;
			if (!syncStateTableForKey.Data.TryGetValue(key, out derivedData))
			{
				return default(T);
			}
			if (derivedData == null || derivedData.Data == null)
			{
				return default(T);
			}
			if (derivedData.Data is T)
			{
				return (T)((object)derivedData.Data);
			}
			throw new InvalidOperationException(ServerStrings.ExMismatchedSyncStateDataType(typeof(T).ToString(), derivedData.GetType().ToString()));
		}

		// Token: 0x06007B60 RID: 31584 RVA: 0x002203FD File Offset: 0x0021E5FD
		public long GetHotStateCompressedSize()
		{
			this.CheckDisposed("GetHotStateCompressedSize");
			if (0L == this.hotDataBeginsAt)
			{
				return 0L;
			}
			return this.syncStateStorage.CompressedMemoryStream.Length - this.hotDataBeginsAt;
		}

		// Token: 0x06007B61 RID: 31585 RVA: 0x0022042E File Offset: 0x0021E62E
		public long GetTotalCompressedSize()
		{
			this.CheckDisposed("GetTotalCompressedSize");
			return this.syncStateStorage.CompressedMemoryStream.Length;
		}

		// Token: 0x06007B62 RID: 31586 RVA: 0x0022044B File Offset: 0x0021E64B
		public long GetLastCommittedSize()
		{
			this.CheckDisposed("GetLastCommittedSize");
			return this.lastCommittedSize;
		}

		// Token: 0x06007B63 RID: 31587 RVA: 0x0022045E File Offset: 0x0021E65E
		public long GetLastUncompressedSize()
		{
			this.CheckDisposed("GetLastUncompressedSize");
			return this.lastUncompressedSize;
		}

		// Token: 0x06007B64 RID: 31588 RVA: 0x00220471 File Offset: 0x0021E671
		public bool IsColdStateDeserialized()
		{
			this.CheckDisposed("IsColdStateDeserialized");
			return null != this.coldSyncStateTable;
		}

		// Token: 0x06007B65 RID: 31589 RVA: 0x0022048A File Offset: 0x0021E68A
		public virtual void Load()
		{
			this.Load(true, new PropertyDefinition[0]);
		}

		// Token: 0x06007B66 RID: 31590 RVA: 0x00220499 File Offset: 0x0021E699
		public void OnCommitStateModifications(FolderSyncStateUtil.CommitStateModificationsDelegate commitStateModificationsDelegate)
		{
			this.CheckDisposed("OnCommitStateModifications");
			this.commitStateModificationsDelegate = commitStateModificationsDelegate;
		}

		// Token: 0x06007B67 RID: 31591 RVA: 0x002204B0 File Offset: 0x0021E6B0
		public void Remove(string key)
		{
			this.CheckDisposed("Remove");
			GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTableForKey = this.GetSyncStateTableForKey(key);
			if (syncStateTableForKey.Data.ContainsKey(key))
			{
				syncStateTableForKey.Data.Remove(key);
			}
		}

		// Token: 0x06007B68 RID: 31592 RVA: 0x002204EB File Offset: 0x0021E6EB
		public void HandleCorruptSyncState()
		{
			this.CheckDisposed("HandleCorruptSyncState");
			this.HandleCorruptSyncState(null);
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x00220500 File Offset: 0x0021E700
		internal static StoreObject CreateSyncStateStoreObject(SyncStateStorage syncStateStorage, SyncStateInfo syncStateInfo, Folder syncStateParentFolder, PropertyDefinition[] properties, object[] values, ISyncLogger syncLogger = null)
		{
			StoreObject result = null;
			if (syncStateStorage.SaveOnDirectItems)
			{
				result = SyncState.CreateSyncStateItemInFolder(syncStateInfo, syncStateStorage.FolderId, syncStateParentFolder.Session as MailboxSession, properties, values, syncLogger);
			}
			else
			{
				using (Folder folder = SyncState.CreateSyncStateFolderInFolder(syncStateInfo, syncStateParentFolder, properties, values, syncLogger))
				{
					result = SyncState.CreateSyncStateItemInFolder(syncStateInfo, folder.StoreObjectId, syncStateParentFolder.Session as MailboxSession, properties, values, syncLogger);
				}
			}
			return result;
		}

		// Token: 0x06007B6A RID: 31594 RVA: 0x00220580 File Offset: 0x0021E780
		internal static StoreObject GetSyncStateStoreObject(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, SyncStateInfo syncStateInfo, ISyncLogger syncLogger, params PropertyDefinition[] propsToReturn)
		{
			SyncStateMetadata syncStateMetadata;
			return SyncState.GetSyncStateStoreObject(syncStateStorage, syncStateParentFolder, syncStateInfo, syncLogger, out syncStateMetadata, propsToReturn);
		}

		// Token: 0x06007B6B RID: 31595 RVA: 0x0022059A File Offset: 0x0021E79A
		internal static StoreObject GetSyncStateStoreObject(SyncStateStorage syncStateStorage, Folder syncStateParentFolder, SyncStateInfo syncStateInfo, ISyncLogger syncLogger, out SyncStateMetadata syncStateMetadata, params PropertyDefinition[] propsToReturn)
		{
			syncStateMetadata = syncStateStorage.DeviceMetadata.GetSyncState(syncStateParentFolder.Session as MailboxSession, syncStateInfo.UniqueName, syncLogger);
			return SyncState.GetSyncStateStoreObject(syncStateParentFolder, ref syncStateMetadata, syncLogger, propsToReturn);
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x002205C8 File Offset: 0x0021E7C8
		internal static StoreObject GetSyncStateStoreObject(Folder syncStateParentFolder, ref SyncStateMetadata syncStateMetadata, ISyncLogger syncLogger, params PropertyDefinition[] propsToReturn)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			if (syncStateMetadata == null)
			{
				return null;
			}
			StoreObject storeObject = null;
			if (syncStateMetadata.ItemSyncStateId != null)
			{
				try
				{
					try
					{
						storeObject = Microsoft.Exchange.Data.Storage.Item.Bind(syncStateParentFolder.Session, syncStateMetadata.ItemSyncStateId, SyncState.AppendAdditionalProperties(propsToReturn));
					}
					catch (ObjectNotFoundException)
					{
						syncLogger.TraceDebug<string, string>(ExTraceGlobals.SyncTracer, 0L, "[SyncState.GetSyncStateStorageObject] Cached sync state item does not exist.  Clearing from cache.  Name: {0}, ItemId: {1}", syncStateMetadata.Name, syncStateMetadata.ItemSyncStateId.ToBase64String());
						syncStateMetadata.ParentDevice.TryRemove(syncStateMetadata.Name, syncLogger);
						syncStateMetadata = syncStateMetadata.ParentDevice.GetSyncState(syncStateParentFolder.Session as MailboxSession, syncStateMetadata.Name, syncLogger);
						return (syncStateMetadata == null) ? null : SyncState.GetSyncStateStoreObject(syncStateParentFolder, ref syncStateMetadata, syncLogger, propsToReturn);
					}
					((Item)storeObject).OpenAsReadWrite();
					return storeObject;
				}
				catch
				{
					if (storeObject != null)
					{
						storeObject.Dispose();
						storeObject = null;
					}
					throw;
				}
			}
			try
			{
				storeObject = Folder.Bind(syncStateParentFolder.Session, syncStateMetadata.FolderSyncStateId, SyncState.AppendAdditionalProperties(propsToReturn));
			}
			catch (ObjectNotFoundException)
			{
				syncLogger.TraceDebug<string, string>(ExTraceGlobals.SyncTracer, 0L, "[SyncState.GetSyncStateStorageObject] Cached sync state folder does not exist.  Clearing from cache.  Name: {0}, ItemId: {1}", syncStateMetadata.Name, syncStateMetadata.FolderSyncStateId.ToBase64String());
				syncStateMetadata.ParentDevice.TryRemove(syncStateMetadata.Name, syncLogger);
				syncStateMetadata = syncStateMetadata.ParentDevice.GetSyncState(syncStateParentFolder.Session as MailboxSession, syncStateMetadata.Name, syncLogger);
				return (syncStateMetadata == null) ? null : SyncState.GetSyncStateStoreObject(syncStateParentFolder, ref syncStateMetadata, syncLogger, propsToReturn);
			}
			return storeObject;
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x00220754 File Offset: 0x0021E954
		internal bool IsColdDataKey(string key)
		{
			return this.coldDataKeys.ContainsKey(key);
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x00220764 File Offset: 0x0021E964
		protected static PropertyDefinition[] AppendAdditionalProperties(params PropertyDefinition[] additionalProperties)
		{
			if (additionalProperties == null)
			{
				return SyncState.storageLocationAsArray;
			}
			PropertyDefinition[] array = new PropertyDefinition[1 + additionalProperties.Length];
			array[0] = SyncStateInfo.StorageLocation;
			Array.Copy(additionalProperties, 0, array, 1, additionalProperties.Length);
			return array;
		}

		// Token: 0x06007B6F RID: 31599 RVA: 0x0022079A File Offset: 0x0021E99A
		protected void AddColdDataKey(string key)
		{
			this.coldDataKeys[key] = true;
		}

		// Token: 0x06007B70 RID: 31600 RVA: 0x002207A9 File Offset: 0x0021E9A9
		protected virtual void AddColdDataKeys()
		{
		}

		// Token: 0x06007B71 RID: 31601 RVA: 0x002207AB File Offset: 0x0021E9AB
		protected void CheckDisposed(string methodName)
		{
			if (this.syncStateIsDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06007B72 RID: 31602 RVA: 0x002207D0 File Offset: 0x0021E9D0
		private void WriteSyncStateToItem(PooledMemoryStream syncStateStream)
		{
			if (this.storeObject == null || this.storeObject is Folder)
			{
				throw new InvalidOperationException("Item storage can not be null here");
			}
			using (Stream stream = this.storeObject.OpenPropertyStream(SyncStateInfo.StorageLocation, PropertyOpenMode.Create))
			{
				this.syncLogger.TraceDebug<long, string>(ExTraceGlobals.SyncTracer, 0L, "SyncState::WriteSyncStateToItem. Saving {0} bytes of data to Item property stream for sync state {1}.", syncStateStream.Length, this.syncStateInfo.UniqueName);
				byte[] array = SyncState.transferBufferPool.Acquire();
				try
				{
					Util.StreamHandler.CopyStreamData(syncStateStream, stream, null, 0, array);
				}
				finally
				{
					SyncState.transferBufferPool.Release(array);
				}
			}
		}

		// Token: 0x06007B73 RID: 31603 RVA: 0x0022088C File Offset: 0x0021EA8C
		private void WriteSyncStateToFolder(PooledMemoryStream syncStateStream, out StoreObjectId deleteItemId)
		{
			deleteItemId = null;
			if (this.storeObject is Folder)
			{
				this.storeObject[SyncStateInfo.StorageLocation] = syncStateStream.ToArray();
				return;
			}
			using (Item item = this.storeObject as Item)
			{
				this.storeObject = Folder.Bind(this.storeObject.Session, this.storeObject.ParentId, new PropertyDefinition[]
				{
					SyncStateInfo.StorageLocation
				});
				this.storeObject[SyncStateInfo.StorageLocation] = syncStateStream.ToArray();
				deleteItemId = item.Id.ObjectId;
			}
		}

		// Token: 0x06007B74 RID: 31604 RVA: 0x0022093C File Offset: 0x0021EB3C
		private void CreateSyncStateDirectItem(PropertyDefinition[] properties, object[] values)
		{
			StoreObjectId storeObjectId = (this.storeObject is Folder) ? this.storeObject.StoreObjectId : this.storeObject.ParentId;
			using (this.storeObject)
			{
				this.storeObject = SyncState.CreateSyncStateItemInFolder(this.syncStateInfo, this.SyncStateMetadata.ParentDevice.DeviceFolderId, this.storeObject.Session as MailboxSession, properties, values, this.syncLogger);
				this.SyncStateMetadata.ItemSyncStateId = this.storeObject.Id.ObjectId;
				this.SyncStateMetadata.FolderSyncStateId = null;
				((Item)this.storeObject).OpenAsReadWrite();
			}
			this.storeObject.Session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
			{
				storeObjectId
			});
		}

		// Token: 0x06007B75 RID: 31605 RVA: 0x00220A20 File Offset: 0x0021EC20
		private void CreateSyncStateItemFromDirectItem(PropertyDefinition[] properties, object[] values)
		{
			using (Folder folder = Folder.Bind(this.storeObject.Session, this.SyncStateMetadata.ParentDevice.DeviceFolderId))
			{
				using (Folder folder2 = SyncState.CreateSyncStateFolderInFolder(this.syncStateInfo, folder, null, null, this.syncLogger))
				{
					this.storeObject.Session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
					{
						this.storeObject.Id
					});
					this.storeObject.Dispose();
					this.storeObject = SyncState.CreateSyncStateItemInFolder(this.syncStateInfo, folder2.StoreObjectId, folder2.Session as MailboxSession, properties, values, null);
					this.SyncStateMetadata.ItemSyncStateId = this.storeObject.StoreObjectId;
					this.SyncStateMetadata.FolderSyncStateId = folder2.StoreObjectId;
					((Item)this.storeObject).OpenAsReadWrite();
				}
			}
		}

		// Token: 0x06007B76 RID: 31606 RVA: 0x00220B28 File Offset: 0x0021ED28
		private void CreateSyncStateItem(PropertyDefinition[] properties, object[] values, out StoreObjectId clearBlobOnFolderId)
		{
			using (Folder folder = this.storeObject as Folder)
			{
				if (this.SyncStateMetadata.ItemSyncStateId != null)
				{
					ExTraceGlobals.SyncTracer.TraceDebug<string>((long)this.GetHashCode(), "[SyncState.CreateSyncStateItem] storeObject was folder, but metadata had Item.  Opening item.  SyncState: {0}", this.UniqueName);
					SyncStateMetadata syncStateMetadata = this.SyncStateMetadata;
					this.storeObject = SyncState.GetSyncStateStoreObject(folder, ref syncStateMetadata, this.syncLogger, properties);
					if (!object.ReferenceEquals(this.SyncStateMetadata, syncStateMetadata))
					{
						this.SyncStateMetadata = syncStateMetadata;
					}
				}
				else
				{
					ExTraceGlobals.SyncTracer.TraceDebug<string>((long)this.GetHashCode(), "[SyncState.CreateSyncStateItem] storeObject was folder and metadata had null Item.  Creating subitem.  SyncState: {0}", this.UniqueName);
					this.storeObject = SyncState.CreateSyncStateItemInFolder(this.syncStateInfo, folder.StoreObjectId, folder.Session as MailboxSession, properties, values, null);
					this.SyncStateMetadata.ItemSyncStateId = this.storeObject.Id.ObjectId;
				}
				((Item)this.storeObject).OpenAsReadWrite();
				clearBlobOnFolderId = folder.Id.ObjectId;
			}
		}

		// Token: 0x06007B77 RID: 31607 RVA: 0x00220C34 File Offset: 0x0021EE34
		private void SaveSyncStateItem(PropertyDefinition[] properties, object[] values, PooledMemoryStream syncStateStream, StoreObjectId clearBlobOnFolderId)
		{
			StoreObjectId storeObjectId = this.storeObject.StoreObjectId;
			StoreSession session = this.storeObject.Session;
			try
			{
				ConflictResolutionResult conflictResolutionResult = ((Item)this.storeObject).Save(this.syncStateInfo.SaveModeForSyncState);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new SaveConflictException(ServerStrings.SyncStateCollision(this.syncStateInfo.UniqueName), conflictResolutionResult);
				}
			}
			catch (SaveConflictException ex)
			{
				this.syncLogger.TraceDebug<int, string, string>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncState::Commit()::SaveConflictException1. Hashcode = {0}, name={1}, exception={2}", this.GetHashCode(), this.syncStateInfo.UniqueName, ex.ToString());
				throw new SyncStateSaveConflictException(ServerStrings.SyncStateCollision(this.syncStateInfo.UniqueName), ex);
			}
			if (clearBlobOnFolderId != null)
			{
				this.storeObject.Load(new PropertyDefinition[]
				{
					StoreObjectSchema.ParentItemId
				});
				StoreObjectId parentId = this.storeObject.ParentId;
				using (Folder folder = Folder.Bind(session, parentId))
				{
					if (clearBlobOnFolderId != null)
					{
						folder.Delete(SyncStateInfo.StorageLocation);
					}
					folder.Save();
				}
			}
		}

		// Token: 0x06007B78 RID: 31608 RVA: 0x00220D5C File Offset: 0x0021EF5C
		protected void Commit(PropertyDefinition[] properties, object[] values)
		{
			this.CheckDisposed("Commit");
			this.lastCommittedSize = 0L;
			StoreObjectId clearBlobOnFolderId = null;
			this.BackendVersion = new int?(this.Version);
			PooledMemoryStream pooledMemoryStream = this.Serialize();
			if (this.syncStateStorage.SaveOnDirectItems)
			{
				if (this.SyncStateMetadata.StorageType != StorageType.DirectItem)
				{
					this.CreateSyncStateDirectItem(properties, values);
				}
			}
			else if (this.SyncStateMetadata.StorageType == StorageType.DirectItem)
			{
				this.CreateSyncStateItemFromDirectItem(properties, values);
			}
			else if (this.SyncStateMetadata.StorageType == StorageType.Folder)
			{
				this.CreateSyncStateItem(properties, values, out clearBlobOnFolderId);
			}
			this.WriteSyncStateToItem(pooledMemoryStream);
			if (properties != null && values != null && properties.Length == values.Length)
			{
				for (int i = 0; i < properties.Length; i++)
				{
					this.storeObject[properties[i]] = values[i];
				}
			}
			try
			{
				this.SaveSyncStateItem(properties, values, pooledMemoryStream, clearBlobOnFolderId);
				if (pooledMemoryStream != null)
				{
					this.lastCommittedSize = pooledMemoryStream.Length;
				}
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new SyncStateNotFoundException(ServerStrings.SyncStateMissing(this.syncStateInfo.UniqueName), innerException);
			}
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x00220E64 File Offset: 0x0021F064
		protected void Deserialize(PropertyDefinition property)
		{
			this.CheckDisposed("Deserialize");
			this.syncStateStorage.CompressedMemoryStream.SetLength(0L);
			if (this.SyncStateMetadata.StorageType == StorageType.Folder)
			{
				object obj = this.storeObject.TryGetProperty(property);
				if (!PropertyError.IsPropertyError(obj))
				{
					byte[] array = obj as byte[];
					this.syncStateStorage.CompressedMemoryStream.Write(array, 0, array.Length);
				}
				else
				{
					if (PropertyError.IsPropertyNotFound(obj))
					{
						this.syncLogger.TraceDebug<string>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncState.Deserialize] Encountered PropertyError.NotFound trying to deserialize the folder sync state blob on state {0}.  Treating as empty sync state.", this.syncStateInfo.UniqueName);
						this.TreatAsNewSyncState();
						return;
					}
					if (!PropertyError.IsPropertyValueTooBig(obj))
					{
						throw PropertyError.ToException(new PropertyError[]
						{
							obj as PropertyError
						});
					}
					this.syncLogger.TraceError<int, string, PropertyDefinition>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "SyncState::Deserialize. Cannot load folder propback! Hashcode = {0}, Name = {1}, PropertyName = {2}", this.GetHashCode(), this.syncStateInfo.UniqueName, property);
					this.HandleCorruptSyncState();
				}
			}
			else
			{
				try
				{
					using (Stream stream = this.storeObject.OpenPropertyStream(property, PropertyOpenMode.ReadOnly))
					{
						byte[] array2 = SyncState.transferBufferPool.Acquire();
						try
						{
							long arg = Util.StreamHandler.CopyStreamData(stream, this.syncStateStorage.CompressedMemoryStream, null, 0, array2);
							ExTraceGlobals.SyncTracer.TraceDebug<long, string>((long)this.GetHashCode(), "[SyncState.Deserialize] Copied {0} bytes from the property stream for sync state {1}", arg, this.UniqueName);
						}
						finally
						{
							SyncState.transferBufferPool.Release(array2);
						}
					}
				}
				catch (ObjectNotFoundException arg2)
				{
					this.syncLogger.TraceDebug<string, ObjectNotFoundException>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncState.Deserialize] Encountered ObjectNotFoundException trying to deserialize the sync state blob for state {0}.  Treating as empty sync state.  Exception: {1}", this.syncStateInfo.UniqueName, arg2);
					this.TreatAsNewSyncState();
					return;
				}
			}
			this.syncStateStorage.CompressedMemoryStream.Seek(0L, SeekOrigin.Begin);
			if (this.syncStateStorage.CompressedMemoryStream.Length != 0L)
			{
				try
				{
					try
					{
						this.hotSyncStateTable = SyncState.InternalDeserializeData(this.syncStateStorage.CompressedMemoryStream, out this.internalVersion, out this.externalVersion, out this.hotDataBeginsAt, out this.coldDataBeginsAt, out this.coldDataKeys);
					}
					catch (EndOfStreamException innerException)
					{
						throw new CustomSerializationException(ServerStrings.ExSyncStateCorrupted("SyncState truncated"), innerException);
					}
					return;
				}
				catch (CustomSerializationException ex)
				{
					this.HandleCorruptSyncState(ex);
					return;
				}
			}
			this.syncLogger.TraceDebug(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[SyncState.Deserialize] Compressed memory stream has zero length for sync state {0}.  Was New? {1}. Metadata type: {2}, StoreObjectType: {3}. Date Created: {4}.  Treating as new sync state.", new object[]
			{
				this.syncStateInfo.UniqueName,
				this.syncStateIsNew,
				this.SyncStateMetadata.StorageType,
				this.storeObject.GetType().Name,
				this.storeObject.CreationTime
			});
			this.TreatAsNewSyncState();
		}

		// Token: 0x06007B7A RID: 31610 RVA: 0x0022115C File Offset: 0x0021F35C
		private void TreatAsNewSyncState()
		{
			this.coldSyncStateTable = new GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>>(new Dictionary<string, DerivedData<ICustomSerializableBuilder>>());
			this.CreateNewHotSyncStateTable();
			this.syncStateIsNew = true;
		}

		// Token: 0x06007B7B RID: 31611 RVA: 0x0022117C File Offset: 0x0021F37C
		public static GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> InternalDeserializeData(PooledMemoryStream compressedStream, out int internalVersion, out int externalVersion, out long hotDataBeginsAt, out long coldDataBeginsAt, out Dictionary<string, bool> coldKeys)
		{
			BinaryReader binaryReader = new BinaryReader(compressedStream);
			string text = binaryReader.ReadString();
			if (!int.TryParse(text, out internalVersion) && SyncStateTypeFactory.InternalSignature != text)
			{
				throw new CustomSerializationException(ServerStrings.ExSyncStateCorrupted("internal syncStateSignature"));
			}
			text = binaryReader.ReadString();
			if (!int.TryParse(text, out externalVersion) && SyncStateTypeFactory.ExternalSignature != text)
			{
				throw new CustomSerializationException(ServerStrings.ExSyncStateCorrupted("syncStateSignature"));
			}
			uint num = binaryReader.ReadUInt32();
			uint num2 = ComputeCRC.Compute(0U, compressedStream.GetBuffer(), (int)compressedStream.Position, (int)(compressedStream.Length - compressedStream.Position));
			if (num2 != num)
			{
				throw new CustomSerializationException(ServerStrings.ExSyncStateCorrupted("CRC"));
			}
			hotDataBeginsAt = binaryReader.ReadInt64();
			coldDataBeginsAt = binaryReader.ReadInt64();
			GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> genericDictionaryData = SyncState.DeserializeSyncStateTable(internalVersion, externalVersion, compressedStream, hotDataBeginsAt);
			coldKeys = SyncState.GetData<GenericDictionaryData<StringData, string, BooleanData, bool>, Dictionary<string, bool>>(SyncStateProp.ColdDataKeys, null, genericDictionaryData);
			return genericDictionaryData;
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x00221258 File Offset: 0x0021F458
		protected GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> GetSyncStateTableForKey(string key)
		{
			if (!this.IsColdDataKey(key))
			{
				return this.hotSyncStateTable;
			}
			if (this.coldSyncStateTable == null)
			{
				try
				{
					this.coldSyncStateTable = SyncState.DeserializeSyncStateTable(this.internalVersion, this.externalVersion, this.syncStateStorage.CompressedMemoryStream, this.coldDataBeginsAt);
				}
				catch (CustomSerializationException ex)
				{
					this.HandleCorruptSyncState(ex);
				}
				catch (EndOfStreamException ex2)
				{
					this.HandleCorruptSyncState(ex2);
				}
			}
			return this.coldSyncStateTable;
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x002212E0 File Offset: 0x0021F4E0
		protected byte[] GetOriginalColdSyncStateBytes()
		{
			long coldStateCompressedSize = this.GetColdStateCompressedSize();
			if (this.syncStateIsNew || coldStateCompressedSize == 0L)
			{
				return SyncState.emptyDictionaryCompressedBytes;
			}
			this.syncStateStorage.CompressedMemoryStream.Seek(this.coldDataBeginsAt, SeekOrigin.Begin);
			byte[] array = new byte[(int)coldStateCompressedSize];
			this.syncStateStorage.CompressedMemoryStream.Read(array, 0, (int)coldStateCompressedSize);
			return array;
		}

		// Token: 0x06007B7E RID: 31614 RVA: 0x00221340 File Offset: 0x0021F540
		private static byte[] GetEmptyDictionaryCompressedBytes()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTable = new GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>>(new Dictionary<string, DerivedData<ICustomSerializableBuilder>>());
				SyncState.SerializeSyncStateTable(syncStateTable, memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x0022138C File Offset: 0x0021F58C
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.storeObject != null)
				{
					this.storeObject.Dispose();
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
			this.storeObject = null;
			this.syncStateStorage = null;
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x002213C8 File Offset: 0x0021F5C8
		protected virtual void Load(bool reloadFromBackend, params PropertyDefinition[] additionalPropsToLoad)
		{
			this.CheckDisposed("Load");
			if (reloadFromBackend)
			{
				PropertyDefinition[] properties = SyncState.AppendAdditionalProperties(additionalPropsToLoad);
				this.storeObject.Load(properties);
				this.syncStateIsNew = false;
			}
			if (this.KeepCachedDataOnReload)
			{
				return;
			}
			if (this.syncStateIsNew)
			{
				this.TreatAsNewSyncState();
			}
			else
			{
				try
				{
					this.totalLoadCount++;
					this.Deserialize(SyncStateInfo.StorageLocation);
				}
				catch (Exception ex)
				{
					this.HandleCorruptSyncState(ex);
				}
			}
			int? num = null;
			try
			{
				bool flag = false;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(2164665661U, ref flag);
				if (flag)
				{
					int? backendVersion = this.BackendVersion;
					try
					{
						this[SyncStateProp.Version] = new NullableData<DateTimeData, ExDateTime>(new ExDateTime?(ExDateTime.UtcNow));
						num = this.BackendVersion;
					}
					finally
					{
						this.BackendVersion = backendVersion;
					}
				}
				num = this.BackendVersion;
			}
			catch (InvalidCastException ex2)
			{
				this.HandleCorruptSyncState(ex2);
			}
			if (num == null && !this.syncStateIsNew)
			{
				this.HandleCorruptSyncState();
			}
			if (num != null)
			{
				if (num.Value > this.Version)
				{
					throw new InvalidSyncStateVersionException(ServerStrings.ExNewerVersionedSyncState(this.UniqueName, num.Value, this.Version));
				}
				this.syncStateInfo.HandleSyncStateVersioning(this);
			}
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x00221524 File Offset: 0x0021F724
		protected PooledMemoryStream Serialize()
		{
			this.CheckDisposed("Serialize");
			this.totalSaveCount++;
			this.CommitStateModifications();
			byte[] array = null;
			if (this.coldSyncStateTable == null || this.coldSyncStateTable.Data == null || this.coldSyncStateTable.Data.Count == 0)
			{
				array = this.GetOriginalColdSyncStateBytes();
			}
			this.lastUncompressedSize = 0L;
			this.syncStateStorage.CompressedMemoryStream.SetLength(0L);
			BinaryWriter binaryWriter = new BinaryWriter(this.syncStateStorage.CompressedMemoryStream);
			binaryWriter.Write(SyncStateTypeFactory.InternalVersion.ToString(CultureInfo.InvariantCulture));
			binaryWriter.Write(SyncStateTypeFactory.ExternalVersion.ToString(CultureInfo.InvariantCulture));
			uint value = 0U;
			long position = this.syncStateStorage.CompressedMemoryStream.Position;
			binaryWriter.Write(value);
			long position2 = this.syncStateStorage.CompressedMemoryStream.Position;
			binaryWriter.Write(0UL);
			long position3 = this.syncStateStorage.CompressedMemoryStream.Position;
			binaryWriter.Write(0UL);
			this.coldDataBeginsAt = this.syncStateStorage.CompressedMemoryStream.Position;
			if (array != null)
			{
				this.coldCopyCount++;
				this.lastUncompressedSize += (long)array.Length;
				this.syncStateStorage.CompressedMemoryStream.Write(array, 0, array.Length);
			}
			else
			{
				this.coldSaveCount++;
				this.lastUncompressedSize += (long)SyncState.SerializeSyncStateTable(this.coldSyncStateTable, this.syncStateStorage.CompressedMemoryStream);
			}
			this.hotDataBeginsAt = this.syncStateStorage.CompressedMemoryStream.Position;
			this.lastUncompressedSize += (long)SyncState.SerializeSyncStateTable(this.hotSyncStateTable, this.syncStateStorage.CompressedMemoryStream);
			this.syncStateStorage.CompressedMemoryStream.Seek(position3, SeekOrigin.Begin);
			binaryWriter.Write(this.coldDataBeginsAt);
			this.syncStateStorage.CompressedMemoryStream.Seek(position2, SeekOrigin.Begin);
			binaryWriter.Write(this.hotDataBeginsAt);
			value = ComputeCRC.Compute(0U, this.syncStateStorage.CompressedMemoryStream.GetBuffer(), (int)position + 4, (int)(this.syncStateStorage.CompressedMemoryStream.Length - position - 4L));
			this.syncStateStorage.CompressedMemoryStream.Seek(position, SeekOrigin.Begin);
			binaryWriter.Write(value);
			this.syncStateStorage.CompressedMemoryStream.Seek(0L, SeekOrigin.Begin);
			return this.syncStateStorage.CompressedMemoryStream;
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x00221790 File Offset: 0x0021F990
		private static Folder CreateSyncStateFolderInFolder(SyncStateInfo syncStateInfo, Folder syncStateParentFolder, PropertyDefinition[] properties, object[] values, ISyncLogger syncLogger = null)
		{
			Folder folder = null;
			bool flag = false;
			Folder result;
			try
			{
				try
				{
					folder = Folder.Create(syncStateParentFolder.Session, syncStateParentFolder.Id, StoreObjectType.Folder, syncStateInfo.UniqueName, CreateMode.CreateNew);
					if (properties != null && values != null && properties.Length == values.Length)
					{
						for (int i = 0; i < properties.Length; i++)
						{
							folder[properties[i]] = values[i];
						}
					}
					folder.Save();
				}
				catch (ObjectExistedException innerException)
				{
					throw new SyncStateExistedException(ServerStrings.ExSyncStateAlreadyExists(syncStateInfo.UniqueName), innerException);
				}
				folder.Load(SyncState.AppendAdditionalProperties(properties));
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

		// Token: 0x06007B83 RID: 31619 RVA: 0x00221840 File Offset: 0x0021FA40
		private static Item CreateSyncStateItemInFolder(SyncStateInfo syncStateInfo, StoreObjectId parentFolderId, MailboxSession mailboxSession, PropertyDefinition[] properties, object[] values, ISyncLogger syncLogger = null)
		{
			MessageItem messageItem = null;
			bool flag = false;
			Item result;
			try
			{
				messageItem = MessageItem.Create(mailboxSession, parentFolderId);
				messageItem.ClassName = "Exchange.ContentsSyncData";
				messageItem[InternalSchema.Subject] = syncStateInfo.UniqueName;
				if (properties != null && values != null && properties.Length == values.Length)
				{
					for (int i = 0; i < properties.Length; i++)
					{
						messageItem[properties[i]] = values[i];
					}
				}
				messageItem.Save(SaveMode.NoConflictResolution);
				messageItem.Load(SyncState.AppendAdditionalProperties(properties));
				flag = true;
				result = messageItem;
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new SyncStateNotFoundException(ServerStrings.SyncStateMissing(syncStateInfo.UniqueName), innerException);
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return result;
		}

		// Token: 0x06007B84 RID: 31620 RVA: 0x002218FC File Offset: 0x0021FAFC
		private void CommitStateModifications()
		{
			if (this.commitStateModificationsDelegate != null)
			{
				this.commitStateModificationsDelegate();
			}
		}

		// Token: 0x06007B85 RID: 31621 RVA: 0x00221911 File Offset: 0x0021FB11
		private void CreateNewHotSyncStateTable()
		{
			this.hotSyncStateTable = new GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>>(new Dictionary<string, DerivedData<ICustomSerializableBuilder>>());
			this.coldDataKeys = new Dictionary<string, bool>(10);
			this.AddColdDataKeys();
			this[SyncStateProp.ColdDataKeys] = new GenericDictionaryData<StringData, string, BooleanData, bool>(this.coldDataKeys);
		}

		// Token: 0x06007B86 RID: 31622 RVA: 0x0022194C File Offset: 0x0021FB4C
		private static GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> DeserializeSyncStateTable(int internalVersion, int externalVersion, PooledMemoryStream memoryStream, long idxTable)
		{
			GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> genericDictionaryData = null;
			memoryStream.Seek(idxTable, SeekOrigin.Begin);
			SyncState.SyncStateAdapter syncStateAdapter = null;
			try
			{
				syncStateAdapter = SyncState.SyncStateAdapterPool.Acquire();
				syncStateAdapter.ComponentDataPool.InternalVersion = internalVersion;
				syncStateAdapter.ComponentDataPool.ExternalVersion = externalVersion;
				syncStateAdapter.Initialize(memoryStream, CompressionMode.Decompress);
				genericDictionaryData = new GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>>();
				genericDictionaryData.DeserializeData(syncStateAdapter.BinaryReader, syncStateAdapter.ComponentDataPool);
			}
			finally
			{
				if (syncStateAdapter != null)
				{
					syncStateAdapter.CloseStream();
					SyncState.SyncStateAdapterPool.Release(syncStateAdapter);
				}
			}
			return genericDictionaryData;
		}

		// Token: 0x06007B87 RID: 31623 RVA: 0x002219D4 File Offset: 0x0021FBD4
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.syncStateIsDisposed, disposing);
			if (!this.syncStateIsDisposed)
			{
				this.syncStateIsDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06007B88 RID: 31624 RVA: 0x002219FC File Offset: 0x0021FBFC
		private void HandleCorruptSyncState(Exception ex)
		{
			ExTraceGlobals.SyncTracer.TraceError<string, bool, string>((long)this.GetHashCode(), "[SyncState.HandleCorruptSyncState] SyncState {0} was corrupt.  Deleting all sync states? {1}.  Inner exception: {2}", this.syncStateInfo.UniqueName, !this.syncStateInfo.ReadOnly, (ex == null) ? "NULL" : ex.ToString());
			if (!this.syncStateInfo.ReadOnly)
			{
				this.syncStateStorage.DeleteAllSyncStates();
			}
			if (ex != null)
			{
				throw new CorruptSyncStateException(this.UniqueName, ServerStrings.ExSyncStateCorrupted(this.UniqueName), ex);
			}
			throw new CorruptSyncStateException(this.UniqueName, ServerStrings.ExSyncStateCorrupted(this.UniqueName));
		}

		// Token: 0x06007B89 RID: 31625 RVA: 0x00221A94 File Offset: 0x0021FC94
		private static int SerializeSyncStateTable(GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTable, Stream compressedStream)
		{
			SyncState.SyncStateAdapter syncStateAdapter = null;
			int uncompressedByteCount;
			try
			{
				syncStateAdapter = SyncState.SyncStateAdapterPool.Acquire();
				syncStateAdapter.ComponentDataPool.InternalVersion = SyncStateTypeFactory.InternalVersion;
				syncStateAdapter.ComponentDataPool.ExternalVersion = SyncStateTypeFactory.ExternalVersion;
				syncStateAdapter.Initialize(compressedStream, CompressionMode.Compress);
				syncStateTable.SerializeData(syncStateAdapter.BinaryWriter, syncStateAdapter.ComponentDataPool);
				uncompressedByteCount = syncStateAdapter.UncompressedByteCount;
			}
			finally
			{
				if (syncStateAdapter != null)
				{
					syncStateAdapter.CloseStream();
					SyncState.SyncStateAdapterPool.Release(syncStateAdapter);
				}
			}
			return uncompressedByteCount;
		}

		// Token: 0x040054C8 RID: 21704
		internal const uint LidCorruptBackEndVersionType = 2164665661U;

		// Token: 0x040054C9 RID: 21705
		private const long FolderPropertySizeLimit = 30720L;

		// Token: 0x040054CA RID: 21706
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040054CB RID: 21707
		private static readonly ThrottlingObjectPool<SyncState.SyncStateAdapter> SyncStateAdapterPool = new ThrottlingObjectPool<SyncState.SyncStateAdapter>(Environment.ProcessorCount);

		// Token: 0x040054CC RID: 21708
		private static BufferPool transferBufferPool = new BufferPool(4096, false);

		// Token: 0x040054CD RID: 21709
		private static readonly byte[] emptyDictionaryCompressedBytes = SyncState.GetEmptyDictionaryCompressedBytes();

		// Token: 0x040054CE RID: 21710
		private static readonly PropertyDefinition[] storageLocationAsArray = new PropertyDefinition[]
		{
			SyncStateInfo.StorageLocation
		};

		// Token: 0x040054CF RID: 21711
		private int internalVersion;

		// Token: 0x040054D0 RID: 21712
		private int externalVersion;

		// Token: 0x040054D1 RID: 21713
		private bool syncStateIsNew;

		// Token: 0x040054D2 RID: 21714
		private int totalSaveCount;

		// Token: 0x040054D3 RID: 21715
		private int coldSaveCount;

		// Token: 0x040054D4 RID: 21716
		private int coldCopyCount;

		// Token: 0x040054D5 RID: 21717
		private int totalLoadCount;

		// Token: 0x040054D6 RID: 21718
		private StoreObject storeObject;

		// Token: 0x040054D7 RID: 21719
		private SyncStateInfo syncStateInfo;

		// Token: 0x040054D8 RID: 21720
		private SyncStateStorage syncStateStorage;

		// Token: 0x040054D9 RID: 21721
		private long coldDataBeginsAt;

		// Token: 0x040054DA RID: 21722
		private long hotDataBeginsAt;

		// Token: 0x040054DB RID: 21723
		private Dictionary<string, bool> coldDataKeys = new Dictionary<string, bool>();

		// Token: 0x040054DC RID: 21724
		private GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> coldSyncStateTable;

		// Token: 0x040054DD RID: 21725
		private FolderSyncStateUtil.CommitStateModificationsDelegate commitStateModificationsDelegate;

		// Token: 0x040054DE RID: 21726
		private GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> hotSyncStateTable;

		// Token: 0x040054DF RID: 21727
		private bool syncStateIsDisposed;

		// Token: 0x040054E0 RID: 21728
		private bool keepCachedDataOnReload;

		// Token: 0x040054E1 RID: 21729
		private long lastCommittedSize;

		// Token: 0x040054E2 RID: 21730
		private long lastUncompressedSize;

		// Token: 0x040054E3 RID: 21731
		protected ISyncLogger syncLogger;

		// Token: 0x02000E02 RID: 3586
		private class SyncStateAdapter : Stream
		{
			// Token: 0x06007B8B RID: 31627 RVA: 0x00221B63 File Offset: 0x0021FD63
			public SyncStateAdapter()
			{
				this.componentDataPool = new ComponentDataPool();
			}

			// Token: 0x17002112 RID: 8466
			// (get) Token: 0x06007B8C RID: 31628 RVA: 0x00221B76 File Offset: 0x0021FD76
			private BufferedStream Stream
			{
				get
				{
					if (this.stream == null)
					{
						throw new InvalidOperationException("Stream is null.  Make sure to create a compressor or decompressor before using Reader/Writer!");
					}
					return this.stream;
				}
			}

			// Token: 0x17002113 RID: 8467
			// (get) Token: 0x06007B8D RID: 31629 RVA: 0x00221B91 File Offset: 0x0021FD91
			public BinaryReader BinaryReader
			{
				get
				{
					if (this.binaryReader == null)
					{
						this.binaryReader = new BinaryReader(this);
					}
					return this.binaryReader;
				}
			}

			// Token: 0x17002114 RID: 8468
			// (get) Token: 0x06007B8E RID: 31630 RVA: 0x00221BAD File Offset: 0x0021FDAD
			public BinaryWriter BinaryWriter
			{
				get
				{
					if (this.binaryWriter == null)
					{
						this.binaryWriter = new BinaryWriter(this, new UTF8Encoding(false, false));
					}
					return this.binaryWriter;
				}
			}

			// Token: 0x17002115 RID: 8469
			// (get) Token: 0x06007B8F RID: 31631 RVA: 0x00221BD0 File Offset: 0x0021FDD0
			public ComponentDataPool ComponentDataPool
			{
				get
				{
					return this.componentDataPool;
				}
			}

			// Token: 0x06007B90 RID: 31632 RVA: 0x00221BD8 File Offset: 0x0021FDD8
			public void Initialize(Stream underlyingStream, CompressionMode compressionMode)
			{
				if (this.stream != null)
				{
					throw new InvalidOperationException("You must close any outstanding streams before attempting to create a new one.");
				}
				this.UncompressedByteCount = 0;
				this.stream = new BufferedStream(new GZipStream(underlyingStream, compressionMode, true));
			}

			// Token: 0x06007B91 RID: 31633 RVA: 0x00221C07 File Offset: 0x0021FE07
			public void CloseStream()
			{
				if (this.stream != null)
				{
					this.stream.Flush();
					this.stream.Dispose();
					this.stream = null;
				}
			}

			// Token: 0x06007B92 RID: 31634 RVA: 0x00221C30 File Offset: 0x0021FE30
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.CloseStream();
					if (this.binaryReader != null)
					{
						this.binaryReader.Close();
						this.binaryReader = null;
					}
					if (this.binaryWriter != null)
					{
						this.binaryWriter.Close();
						this.binaryWriter = null;
					}
				}
				base.Dispose(disposing);
			}

			// Token: 0x17002116 RID: 8470
			// (get) Token: 0x06007B93 RID: 31635 RVA: 0x00221C81 File Offset: 0x0021FE81
			public override bool CanRead
			{
				get
				{
					return this.Stream.CanRead;
				}
			}

			// Token: 0x17002117 RID: 8471
			// (get) Token: 0x06007B94 RID: 31636 RVA: 0x00221C8E File Offset: 0x0021FE8E
			public override bool CanSeek
			{
				get
				{
					return this.Stream.CanSeek;
				}
			}

			// Token: 0x17002118 RID: 8472
			// (get) Token: 0x06007B95 RID: 31637 RVA: 0x00221C9B File Offset: 0x0021FE9B
			public override bool CanWrite
			{
				get
				{
					return this.Stream.CanWrite;
				}
			}

			// Token: 0x06007B96 RID: 31638 RVA: 0x00221CA8 File Offset: 0x0021FEA8
			public override void Flush()
			{
				this.Stream.Flush();
			}

			// Token: 0x17002119 RID: 8473
			// (get) Token: 0x06007B97 RID: 31639 RVA: 0x00221CB5 File Offset: 0x0021FEB5
			public override long Length
			{
				get
				{
					return this.Stream.Length;
				}
			}

			// Token: 0x1700211A RID: 8474
			// (get) Token: 0x06007B98 RID: 31640 RVA: 0x00221CC2 File Offset: 0x0021FEC2
			// (set) Token: 0x06007B99 RID: 31641 RVA: 0x00221CCF File Offset: 0x0021FECF
			public override long Position
			{
				get
				{
					return this.Stream.Position;
				}
				set
				{
					this.Stream.Position = value;
				}
			}

			// Token: 0x1700211B RID: 8475
			// (get) Token: 0x06007B9A RID: 31642 RVA: 0x00221CDD File Offset: 0x0021FEDD
			// (set) Token: 0x06007B9B RID: 31643 RVA: 0x00221CE5 File Offset: 0x0021FEE5
			public int UncompressedByteCount { get; private set; }

			// Token: 0x06007B9C RID: 31644 RVA: 0x00221CEE File Offset: 0x0021FEEE
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.Stream.Seek(offset, origin);
			}

			// Token: 0x06007B9D RID: 31645 RVA: 0x00221CFD File Offset: 0x0021FEFD
			public override void SetLength(long value)
			{
				this.Stream.SetLength(value);
			}

			// Token: 0x06007B9E RID: 31646 RVA: 0x00221D0C File Offset: 0x0021FF0C
			public override int Read(byte[] outBuffer, int offset, int count)
			{
				int num = this.stream.Read(outBuffer, offset, count);
				this.UncompressedByteCount += num;
				return num;
			}

			// Token: 0x06007B9F RID: 31647 RVA: 0x00221D37 File Offset: 0x0021FF37
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.stream.Write(buffer, offset, count);
				this.UncompressedByteCount += count;
			}

			// Token: 0x040054E5 RID: 21733
			private BufferedStream stream;

			// Token: 0x040054E6 RID: 21734
			private BinaryReader binaryReader;

			// Token: 0x040054E7 RID: 21735
			private BinaryWriter binaryWriter;

			// Token: 0x040054E8 RID: 21736
			private ComponentDataPool componentDataPool;
		}
	}
}
