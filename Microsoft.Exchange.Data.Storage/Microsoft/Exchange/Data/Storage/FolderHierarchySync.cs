using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E06 RID: 3590
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderHierarchySync
	{
		// Token: 0x06007BCF RID: 31695 RVA: 0x00222BD0 File Offset: 0x00220DD0
		public FolderHierarchySync(MailboxSession storeSession, IFolderHierarchySyncState syncState, ChangeTrackingDelegate changeTrackingDelegate)
		{
			ExTraceGlobals.SyncTracer.Information<int>((long)this.GetHashCode(), "FolderHierarchySync::Constructor. HashCode = {0}.", this.GetHashCode());
			if (storeSession == null)
			{
				throw new ArgumentNullException("storeSession");
			}
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			this.changeTrackingDelegate = changeTrackingDelegate;
			this.storeSession = storeSession;
			this.syncState = syncState;
			if (this.ClientState == null)
			{
				this.ClientState = new Dictionary<StoreObjectId, FolderStateEntry>();
			}
			if (this.ServerManifest == null)
			{
				this.ServerManifest = new Dictionary<StoreObjectId, FolderManifestEntry>();
			}
		}

		// Token: 0x06007BD0 RID: 31696 RVA: 0x00222C58 File Offset: 0x00220E58
		internal static bool TryGetPropertyFromBag<T>(IStorePropertyBag propertyBag, PropertyDefinition propDef, ISyncLogger syncLogger, out T value)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			object obj = null;
			try
			{
				obj = propertyBag.TryGetProperty(propDef);
			}
			catch (NotInBagPropertyErrorException)
			{
				syncLogger.TraceError<string>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderHierarchySync.TryGetPropertyFromBag] NotInBag exception for property {0}.  Returning default value.", propDef.Name);
				value = default(T);
				return false;
			}
			if (obj is T)
			{
				value = (T)((object)obj);
				return true;
			}
			PropertyError propertyError = obj as PropertyError;
			if (propertyError != null)
			{
				syncLogger.TraceError<Type, string, PropertyErrorCode>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderHierarchySync.TryGetPropertyFromBag] Expected property of type {0} in bag for propDef {1}, but encountered error {2}.", typeof(T), propDef.Name, propertyError.PropertyErrorCode);
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
					syncLogger.TraceError(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderHierarchySync.TryGetPropertyFromBag] Tried to cast property '{0}' with value '{1}' to type '{2}', but the cast failed with error '{3}'.", new object[]
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

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x06007BD1 RID: 31697 RVA: 0x00222D74 File Offset: 0x00220F74
		// (set) Token: 0x06007BD2 RID: 31698 RVA: 0x00222DA4 File Offset: 0x00220FA4
		private Dictionary<StoreObjectId, FolderStateEntry> ClientState
		{
			get
			{
				if (!this.syncState.Contains(SyncStateProp.ClientState))
				{
					return null;
				}
				return ((GenericDictionaryData<StoreObjectIdData, StoreObjectId, FolderStateEntry>)this.syncState[SyncStateProp.ClientState]).Data;
			}
			set
			{
				this.syncState[SyncStateProp.ClientState] = new GenericDictionaryData<StoreObjectIdData, StoreObjectId, FolderStateEntry>(value);
			}
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x06007BD3 RID: 31699 RVA: 0x00222DBC File Offset: 0x00220FBC
		// (set) Token: 0x06007BD4 RID: 31700 RVA: 0x00222DEC File Offset: 0x00220FEC
		private Dictionary<StoreObjectId, FolderManifestEntry> ServerManifest
		{
			get
			{
				if (!this.syncState.Contains(SyncStateProp.CurServerManifest))
				{
					return null;
				}
				return ((GenericDictionaryData<StoreObjectIdData, StoreObjectId, FolderManifestEntry>)this.syncState[SyncStateProp.CurServerManifest]).Data;
			}
			set
			{
				this.syncState[SyncStateProp.CurServerManifest] = new GenericDictionaryData<StoreObjectIdData, StoreObjectId, FolderManifestEntry>(value);
			}
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x00222E04 File Offset: 0x00221004
		public bool IsValidClientOperation(StoreObjectId id, ChangeType changeType, Folder folder)
		{
			EnumValidator.ThrowIfInvalid<ChangeType>(changeType, "changeType");
			if (this.ClientState == null || id == null || folder == null)
			{
				return false;
			}
			if (changeType == ChangeType.Add)
			{
				return !this.ClientState.ContainsKey(id);
			}
			return this.ClientState.ContainsKey(id);
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x00222E44 File Offset: 0x00221044
		public void AcknowledgeServerOperations()
		{
			ExTraceGlobals.SyncTracer.Information((long)this.GetHashCode(), "Storage.FolderHierarchySync.AcknowledgeServerOperations");
			foreach (FolderManifestEntry folderManifestEntry in this.ServerManifest.Values)
			{
				switch (folderManifestEntry.ChangeType)
				{
				case ChangeType.Add:
				case ChangeType.Change:
					this.ClientState[folderManifestEntry.ItemId] = (FolderStateEntry)folderManifestEntry;
					break;
				case ChangeType.Delete:
					this.ClientState.Remove(folderManifestEntry.ItemId);
					break;
				}
			}
			this.ServerManifest.Clear();
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x00222F04 File Offset: 0x00221104
		public HierarchySyncOperations EnumerateServerOperations()
		{
			return this.EnumerateServerOperations(this.storeSession.GetDefaultFolderId(DefaultFolderType.Root), true);
		}

		// Token: 0x06007BD8 RID: 31704 RVA: 0x00222F1A File Offset: 0x0022111A
		public HierarchySyncOperations EnumerateServerOperations(StoreObjectId rootFolderId)
		{
			return this.EnumerateServerOperations(rootFolderId, true);
		}

		// Token: 0x06007BD9 RID: 31705 RVA: 0x00222F24 File Offset: 0x00221124
		public HierarchySyncOperations EnumerateServerOperations(StoreObjectId rootFolderId, bool excludeHiddenFolders)
		{
			return this.EnumerateServerOperations(rootFolderId, excludeHiddenFolders, FolderSyncStateMetadata.IPMFolderNullSyncProperties, null);
		}

		// Token: 0x06007BDA RID: 31706 RVA: 0x00222F34 File Offset: 0x00221134
		public HierarchySyncOperations EnumerateServerOperations(StoreObjectId rootFolderId, bool excludeHiddenFolders, PropertyDefinition[] propertiesToFetch, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			syncLogger.Information(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "Storage.FolderHierarchySync.EnumerateServerOperations(propDefs).");
			ArgumentValidator.ThrowIfNull("rootfolderId", rootFolderId);
			this.ServerManifest.Clear();
			Dictionary<StoreObjectId, FolderStateEntry> serverFolders = new Dictionary<StoreObjectId, FolderStateEntry>();
			using (Folder folder = Folder.Bind(this.storeSession, rootFolderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, propertiesToFetch))
				{
					IStorePropertyBag[] propertyBags;
					do
					{
						propertyBags = queryResult.GetPropertyBags(10000);
						for (int i = 0; i < propertyBags.Length; i++)
						{
							this.AddServerManifestEntry(propertyBags[i], excludeHiddenFolders, serverFolders, syncLogger);
						}
					}
					while (propertyBags.Length != 0);
				}
			}
			return this.PostEnumerateServerOperations(serverFolders, rootFolderId);
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x00223008 File Offset: 0x00221208
		public HierarchySyncOperations EnumerateServerOperations(StoreObjectId rootFolderId, bool excludeHiddenFolders, IEnumerable<IStorePropertyBag> rootFolderHierarchyList, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			syncLogger.Information(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "Storage.FolderHierarchySync.EnumerateServerOperations(hierarchy).");
			ArgumentValidator.ThrowIfNull("rootFolderId", rootFolderId);
			ArgumentValidator.ThrowIfNull("rootFolderHierarchyList", rootFolderHierarchyList);
			this.ServerManifest.Clear();
			Dictionary<StoreObjectId, FolderStateEntry> serverFolders = new Dictionary<StoreObjectId, FolderStateEntry>();
			foreach (IStorePropertyBag propertyBag in rootFolderHierarchyList)
			{
				this.AddServerManifestEntry(propertyBag, excludeHiddenFolders, serverFolders, syncLogger);
			}
			return this.PostEnumerateServerOperations(serverFolders, rootFolderId);
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x002230A8 File Offset: 0x002212A8
		private HierarchySyncOperations PostEnumerateServerOperations(Dictionary<StoreObjectId, FolderStateEntry> serverFolders, StoreObjectId rootFolderId)
		{
			Dictionary<StoreObjectId, FolderManifestEntry> dictionary = new Dictionary<StoreObjectId, FolderManifestEntry>();
			foreach (KeyValuePair<StoreObjectId, FolderStateEntry> keyValuePair in this.ClientState)
			{
				StoreObjectId key = keyValuePair.Key;
				FolderStateEntry value = keyValuePair.Value;
				if (!serverFolders.ContainsKey(key))
				{
					dictionary.Add(key, new FolderManifestEntry(key)
					{
						ChangeType = ChangeType.Delete,
						ParentId = value.ParentId
					});
				}
			}
			foreach (FolderManifestEntry folderManifestEntry in dictionary.Values)
			{
				if (dictionary.ContainsKey(folderManifestEntry.ParentId))
				{
					this.ClientState.Remove(folderManifestEntry.ItemId);
				}
				else if (this.ClientState.ContainsKey(folderManifestEntry.ParentId) || folderManifestEntry.ParentId.Equals(rootFolderId))
				{
					this.ServerManifest[folderManifestEntry.ItemId] = folderManifestEntry;
				}
			}
			return new HierarchySyncOperations(this, this.ServerManifest, false);
		}

		// Token: 0x06007BDD RID: 31709 RVA: 0x002231E0 File Offset: 0x002213E0
		public void RecordClientOperation(StoreObjectId folderId, ChangeType change, Folder folder)
		{
			EnumValidator.ThrowIfInvalid<ChangeType>(change, "change");
			ExTraceGlobals.SyncTracer.Information<StoreObjectId, ChangeType>((long)this.GetHashCode(), "Storage.FolderHierarchySync.RecordClientOperation. ItemId = {0}, ChangeType = {1}", folderId, change);
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (folder != null && !folderId.Equals(folder.Id.ObjectId))
			{
				throw new ArgumentException(ServerStrings.ExFolderDoesNotMatchFolderId);
			}
			if (change != ChangeType.Add && change != ChangeType.Change && change != ChangeType.Delete)
			{
				throw new ArgumentOutOfRangeException("change");
			}
			if ((change == ChangeType.Add || change == ChangeType.Change) && folder == null)
			{
				throw new ArgumentNullException("folder", ServerStrings.ExInvalidNullParameterForChangeTypes("folder", "ChangeType.Add, ChangeType.Change"));
			}
			switch (change)
			{
			case ChangeType.Add:
				if (this.ClientState.ContainsKey(folderId))
				{
					throw new ArgumentException(ServerStrings.ExFolderAlreadyExistsInClientState);
				}
				break;
			case ChangeType.Change:
			case ChangeType.Delete:
				if (!this.ClientState.ContainsKey(folderId))
				{
					throw new ArgumentException(ServerStrings.ExFolderNotFoundInClientState);
				}
				break;
			}
			switch (change)
			{
			case ChangeType.Add:
			case ChangeType.Change:
			{
				FolderStateEntry value = new FolderStateEntry(folder.ParentId, folder.GetValueOrDefault<byte[]>(InternalSchema.ChangeKey), this.changeTrackingDelegate(this.storeSession, folder.StoreObjectId, null));
				this.ClientState[folderId] = value;
				return;
			}
			case (ChangeType)3:
				break;
			case ChangeType.Delete:
				this.ClientState.Remove(folderId);
				break;
			default:
				return;
			}
		}

		// Token: 0x06007BDE RID: 31710 RVA: 0x00223344 File Offset: 0x00221544
		internal Folder GetFolder(FolderManifestEntry serverManifestEntry, params PropertyDefinition[] prefetchProperties)
		{
			if (ChangeType.Delete != serverManifestEntry.ChangeType)
			{
				PropertyDefinition[] array;
				if (prefetchProperties != null)
				{
					array = new PropertyDefinition[prefetchProperties.Length + 1];
					prefetchProperties.CopyTo(array, 1);
				}
				else
				{
					array = new PropertyDefinition[1];
				}
				array[0] = InternalSchema.ChangeKey;
				Folder folder = Folder.Bind(this.storeSession, serverManifestEntry.ItemId, array);
				serverManifestEntry.ChangeKey = folder.GetValueOrDefault<byte[]>(InternalSchema.ChangeKey);
				return folder;
			}
			throw new InvalidOperationException("Cannot GetFolder() on an folder that has been deleted");
		}

		// Token: 0x06007BDF RID: 31711 RVA: 0x002233B8 File Offset: 0x002215B8
		private void AddServerManifestEntry(IStorePropertyBag propertyBag, bool excludeHiddenFolders, Dictionary<StoreObjectId, FolderStateEntry> serverFolders, ISyncLogger syncLogger)
		{
			SharingSubscriptionData[] array = null;
			bool flag = false;
			if (FolderHierarchySync.TryGetPropertyFromBag<bool>(propertyBag, InternalSchema.IsHidden, syncLogger, out flag) && excludeHiddenFolders && flag)
			{
				return;
			}
			VersionedId versionedId;
			StoreObjectId parentId;
			byte[] array2;
			if (!FolderHierarchySync.TryGetPropertyFromBag<VersionedId>(propertyBag, FolderSchema.Id, syncLogger, out versionedId) || !FolderHierarchySync.TryGetPropertyFromBag<StoreObjectId>(propertyBag, StoreObjectSchema.ParentItemId, syncLogger, out parentId) || !FolderHierarchySync.TryGetPropertyFromBag<byte[]>(propertyBag, StoreObjectSchema.ChangeKey, syncLogger, out array2))
			{
				ExTraceGlobals.SyncTracer.Information((long)this.GetHashCode(), "Storage.FolderHierarchySync.AddServerManifestEntry. \nFolder is missing properties. Id , ParentId or ChangeKey");
				return;
			}
			int num = -1;
			serverFolders[versionedId.ObjectId] = null;
			FolderStateEntry folderStateEntry;
			if (this.ClientState.TryGetValue(versionedId.ObjectId, out folderStateEntry) && ArrayComparer<byte>.Comparer.Equals(folderStateEntry.ChangeKey, array2))
			{
				return;
			}
			if (this.changeTrackingDelegate != null)
			{
				num = this.changeTrackingDelegate(this.storeSession, versionedId.ObjectId, propertyBag);
				if (folderStateEntry != null && num == folderStateEntry.ChangeTrackingHash)
				{
					folderStateEntry.ChangeKey = array2;
					return;
				}
			}
			FolderManifestEntry folderManifestEntry = new FolderManifestEntry(versionedId.ObjectId);
			string className;
			FolderHierarchySync.TryGetPropertyFromBag<string>(propertyBag, StoreObjectSchema.ContainerClass, syncLogger, out className);
			folderManifestEntry.ChangeType = ((folderStateEntry != null) ? ChangeType.Change : ChangeType.Add);
			folderManifestEntry.ChangeKey = array2;
			folderManifestEntry.ParentId = parentId;
			folderManifestEntry.ChangeTrackingHash = num;
			folderManifestEntry.Hidden = flag;
			folderManifestEntry.ClassName = className;
			if (folderManifestEntry.ClassName == null)
			{
				folderManifestEntry.ClassName = string.Empty;
			}
			folderManifestEntry.DisplayName = (propertyBag[FolderSchema.DisplayName] as string);
			if (folderManifestEntry.DisplayName == null)
			{
				folderManifestEntry.DisplayName = string.Empty;
			}
			int num2;
			if (!FolderHierarchySync.TryGetPropertyFromBag<int>(propertyBag, FolderSchema.ExtendedFolderFlags, syncLogger, out num2))
			{
				num2 = 0;
			}
			bool flag2 = (num2 & 1073741824) != 0;
			if (flag2)
			{
				if (array == null)
				{
					using (SharingSubscriptionManager sharingSubscriptionManager = new SharingSubscriptionManager(this.storeSession))
					{
						array = sharingSubscriptionManager.GetAll();
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].LocalFolderId.Equals(folderManifestEntry.ItemId))
					{
						folderManifestEntry.Permissions = SyncPermissions.Readonly;
						folderManifestEntry.Owner = array[i].SharerIdentity;
						break;
					}
				}
			}
			this.ServerManifest.Add(folderManifestEntry.ItemId, folderManifestEntry);
		}

		// Token: 0x040054F5 RID: 21749
		private ChangeTrackingDelegate changeTrackingDelegate;

		// Token: 0x040054F6 RID: 21750
		private MailboxSession storeSession;

		// Token: 0x040054F7 RID: 21751
		private IFolderHierarchySyncState syncState;
	}
}
