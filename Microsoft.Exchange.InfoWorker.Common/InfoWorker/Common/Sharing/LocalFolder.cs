using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Sharing;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200026F RID: 623
	internal sealed class LocalFolder : IDisposable
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x00052651 File Offset: 0x00050851
		private LocalFolder(MailboxSession mailboxSession, Folder folder, string remoteFolderId, Item binding)
		{
			this.mailboxSession = mailboxSession;
			this.folder = folder;
			this.remoteFolderId = remoteFolderId;
			this.binding = binding;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00052681 File Offset: 0x00050881
		public StoreId Id
		{
			get
			{
				return this.folder.Id;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0005268E File Offset: 0x0005088E
		public string RemoteFolderId
		{
			get
			{
				return this.remoteFolderId;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00052696 File Offset: 0x00050896
		public StoreObjectType Type
		{
			get
			{
				return this.folder.Id.ObjectId.ObjectType;
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000526B5 File Offset: 0x000508B5
		public static LocalFolder Bind(MailboxSession mailboxSession, StoreId folderId)
		{
			return LocalFolder.Bind(mailboxSession, folderId, delegate(LocalFolder folder)
			{
				folder.Initialize();
			});
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000526DD File Offset: 0x000508DD
		public static LocalFolder BindOnly(MailboxSession mailboxSession, StoreId folderId)
		{
			return LocalFolder.Bind(mailboxSession, folderId, delegate(LocalFolder folder)
			{
			});
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00052704 File Offset: 0x00050904
		private static LocalFolder Bind(MailboxSession mailboxSession, StoreId folderId, LocalFolder.ProcessFolderDelegate processFolder)
		{
			SharingBindingManager sharingBindingManager = new SharingBindingManager(mailboxSession);
			SharingBindingData sharingBindingDataInFolder = sharingBindingManager.GetSharingBindingDataInFolder(folderId);
			if (sharingBindingDataInFolder == null)
			{
				LocalFolder.Tracer.TraceError<IExchangePrincipal, StoreId>(0L, "{0}: Unable to find the binding for folder {1}, fail sync", mailboxSession.MailboxOwner, folderId);
				throw new SubscriptionNotFoundException();
			}
			bool flag = false;
			Item item = null;
			Folder folder = null;
			LocalFolder localFolder = null;
			try
			{
				item = LocalFolder.BindToBindingMessage(mailboxSession, sharingBindingDataInFolder.Id);
				folder = Folder.Bind(mailboxSession, folderId, LocalFolder.extraProperties);
				localFolder = new LocalFolder(mailboxSession, folder, sharingBindingDataInFolder.RemoteFolderId, item);
				processFolder(localFolder);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (localFolder != null)
					{
						localFolder.Dispose();
					}
					else
					{
						if (item != null)
						{
							item.Dispose();
						}
						if (folder != null)
						{
							folder.Dispose();
						}
					}
					localFolder = null;
				}
			}
			return localFolder;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000527C0 File Offset: 0x000509C0
		private static Item BindToBindingMessage(MailboxSession mailboxSession, StoreId itemId)
		{
			return Item.Bind(mailboxSession, itemId, LocalFolder.extraProperties);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000527CE File Offset: 0x000509CE
		public void Dispose()
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
			}
			if (this.binding != null)
			{
				this.binding.Dispose();
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x000527F8 File Offset: 0x000509F8
		public StoreId GetLocalIdFromRemoteId(string remoteId)
		{
			LocalFolder.LocalItem localItem;
			if (this.localItems.TryGetValue(remoteId, out localItem))
			{
				return localItem.Id;
			}
			return null;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0005281D File Offset: 0x00050A1D
		public void DeleteAllItems()
		{
			LocalFolder.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: deleting all items from folder.", this);
			this.folder.DeleteAllObjects(DeleteItemFlags.HardDelete);
			this.itemsToDelete.Clear();
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0005284E File Offset: 0x00050A4E
		public void SelectItemToDelete(StoreId itemId)
		{
			this.itemsToDelete.Add(itemId);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0005285C File Offset: 0x00050A5C
		public void DeleteSelectedItems()
		{
			List<StoreId> list = new List<StoreId>(100);
			foreach (StoreId item in this.itemsToDelete)
			{
				if (list.Count == 100)
				{
					this.DeleteItems(list.ToArray());
					list.Clear();
				}
				list.Add(item);
			}
			if (list.Count > 0)
			{
				this.DeleteItems(list.ToArray());
			}
			this.itemsToDelete.Clear();
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00052960 File Offset: 0x00050B60
		public void SaveSyncState(string syncState)
		{
			LocalFolder.Tracer.TraceDebug<LocalFolder, string>((long)this.GetHashCode(), "{0}: Saving sync state: {1}", this, syncState);
			this.SafeUpdateBindingItem(delegate(Item binding)
			{
				using (Stream stream = binding.OpenPropertyStream(SharingSchema.ExternalSharingSyncState, PropertyOpenMode.Create))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						streamWriter.Write(syncState);
					}
				}
			});
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000529AC File Offset: 0x00050BAC
		public void UpdateLastAttemptedSyncTime()
		{
			LocalFolder.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: Updating the last attempted sync time.", this);
			ExDateTime now = ExDateTime.Now;
			this.folder[FolderSchema.SubscriptionLastAttemptedSyncTime] = now;
			this.folder.Save();
			this.folder.Load();
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00052A24 File Offset: 0x00050C24
		public void UpdateLastSyncTimes()
		{
			LocalFolder.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: Updating the last attempted and successful sync times.", this);
			ExDateTime now = ExDateTime.Now;
			this.folder[FolderSchema.SubscriptionLastAttemptedSyncTime] = now;
			this.folder[FolderSchema.SubscriptionLastSuccessfulSyncTime] = now;
			this.folder.Save();
			this.folder.Load();
			this.SafeUpdateBindingItem(delegate(Item binding)
			{
				binding[BindingItemSchema.SharingLastSync] = now;
			});
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00052AB8 File Offset: 0x00050CB8
		public string LoadSyncState()
		{
			string text;
			try
			{
				using (Stream stream = this.binding.OpenPropertyStream(SharingSchema.ExternalSharingSyncState, PropertyOpenMode.ReadOnly))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						text = streamReader.ReadToEnd();
					}
				}
			}
			catch (PropertyErrorException)
			{
				LocalFolder.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: Got a PropertyError exception trying to fetch the syncstate, starting over.", this);
				text = null;
			}
			catch (ObjectNotFoundException)
			{
				LocalFolder.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: No sync state found on the folder", this);
				text = null;
			}
			LocalFolder.Tracer.TraceDebug<LocalFolder, string>((long)this.GetHashCode(), "{0}: Current sync state: {1}", this, text);
			return text;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00052B91 File Offset: 0x00050D91
		public void DeleteSyncState()
		{
			this.SafeUpdateBindingItem(delegate(Item binding)
			{
				binding.Delete(SharingSchema.ExternalSharingSyncState);
			});
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00052BB6 File Offset: 0x00050DB6
		public void SaveLevelOfDetails(LevelOfDetails levelOfDetails)
		{
			this.folder[SharingSchema.ExternalSharingLevelOfDetails] = levelOfDetails;
			this.folder.Save();
			this.folder.Load();
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00052BE8 File Offset: 0x00050DE8
		public LevelOfDetails LoadLevelOfDetails()
		{
			object obj = this.folder.TryGetProperty(SharingSchema.ExternalSharingLevelOfDetails);
			if (obj == null || obj is PropertyError || !Enum.IsDefined(typeof(LevelOfDetails), obj))
			{
				return LevelOfDetails.Unknown;
			}
			return (LevelOfDetails)obj;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00052C2C File Offset: 0x00050E2C
		public void ProcessAllItems(LocalFolder.ProcessItem processItem)
		{
			using (QueryResult queryResult = this.folder.ItemQuery(ItemQueryType.None, null, null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				LocalFolder.Tracer.TraceDebug<LocalFolder, int>((long)this.GetHashCode(), "{0}: Inspecting {1} items for excessive data.", this, queryResult.EstimatedRowCount);
				for (;;)
				{
					object[][] rows = queryResult.GetRows(200);
					if (rows.Length == 0)
					{
						break;
					}
					foreach (object[] array2 in rows)
					{
						VersionedId versionedId = array2[0] as VersionedId;
						if (versionedId != null)
						{
							LocalFolder.Tracer.TraceDebug<LocalFolder, VersionedId>((long)this.GetHashCode(), "{0}: Processing item {1} due to excessive data.", this, versionedId);
							processItem(versionedId);
						}
					}
				}
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00052CF0 File Offset: 0x00050EF0
		private void SafeUpdateBindingItem(Action<Item> stampBindingItem)
		{
			stampBindingItem(this.binding);
			ConflictResolutionResult conflictResolutionResult = this.binding.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				LocalFolder.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: Conflict occurs when saving the binding message. Reload and try saving again.", this);
				StoreObjectId objectId = this.binding.Id.ObjectId;
				this.binding.Dispose();
				this.binding = LocalFolder.BindToBindingMessage(this.mailboxSession, objectId);
				this.binding.OpenAsReadWrite();
				stampBindingItem(this.binding);
				this.binding.Save(SaveMode.NoConflictResolution);
			}
			this.binding.Load();
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00052D93 File Offset: 0x00050F93
		private void DeleteItems(StoreId[] itemIds)
		{
			LocalFolder.Tracer.TraceDebug<LocalFolder, ArrayTracer<StoreId>>((long)this.GetHashCode(), "{0}: deleting items from folder: {1}", this, new ArrayTracer<StoreId>(itemIds));
			this.folder.DeleteObjects(DeleteItemFlags.HardDelete, itemIds);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00052DC0 File Offset: 0x00050FC0
		private void Initialize()
		{
			this.localItems = this.LoadLocalItemsAndDeleteDuplicates();
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00052DD0 File Offset: 0x00050FD0
		private Dictionary<string, LocalFolder.LocalItem> LoadLocalItemsAndDeleteDuplicates()
		{
			Dictionary<string, LocalFolder.LocalItem> result;
			using (QueryResult queryResult = this.folder.ItemQuery(ItemQueryType.None, null, null, new PropertyDefinition[]
			{
				ItemSchema.Id,
				SharingSchema.ExternalSharingMasterId,
				StoreObjectSchema.CreationTime
			}))
			{
				Dictionary<string, LocalFolder.LocalItem> dictionary = new Dictionary<string, LocalFolder.LocalItem>();
				bool flag = false;
				for (;;)
				{
					object[][] rows = queryResult.GetRows(200);
					if (rows.Length == 0)
					{
						break;
					}
					foreach (object[] array2 in rows)
					{
						VersionedId versionedId = array2[0] as VersionedId;
						string text = array2[1] as string;
						ExDateTime creationTime = (ExDateTime)array2[2];
						if (versionedId != null)
						{
							if (string.IsNullOrEmpty(text))
							{
								LocalFolder.Tracer.TraceError<LocalFolder, StoreObjectId>((long)this.GetHashCode(), "{0}: Found item {1} without remote item id.", this, versionedId.ObjectId);
							}
							else
							{
								LocalFolder.Tracer.TraceDebug<LocalFolder, StoreObjectId, string>((long)this.GetHashCode(), "{0}: Found item {1} with remote item id {2}.", this, versionedId.ObjectId, text);
								LocalFolder.LocalItem localItem = new LocalFolder.LocalItem(versionedId, creationTime);
								try
								{
									dictionary.Add(text, localItem);
								}
								catch (ArgumentException)
								{
									LocalFolder.Tracer.TraceError<LocalFolder, string, StoreObjectId>((long)this.GetHashCode(), "{0}: there is already a local item with the same remote id. remoteId={1}, localId={2}", this, text, versionedId.ObjectId);
									flag = true;
									LocalFolder.LocalItem localItem2;
									if (dictionary.TryGetValue(text, out localItem2))
									{
										int num = localItem2.CreationTime.CompareTo(localItem.CreationTime);
										if (num < 0)
										{
											dictionary.Remove(text);
											this.SelectItemToDelete(localItem2.Id);
											dictionary.Add(text, localItem);
										}
										else
										{
											this.SelectItemToDelete(localItem.Id);
										}
									}
								}
							}
						}
					}
				}
				if (flag)
				{
					LocalFolder.Tracer.TraceError<LocalFolder>((long)this.GetHashCode(), "{0}: Duplicates have been found. Deleting items now.", this);
					this.DeleteSelectedItems();
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x04000BB1 RID: 2993
		private const int ItemsQueryBatch = 200;

		// Token: 0x04000BB2 RID: 2994
		private const int DeleteBatchNumber = 100;

		// Token: 0x04000BB3 RID: 2995
		private static readonly Trace Tracer = ExTraceGlobals.LocalFolderTracer;

		// Token: 0x04000BB4 RID: 2996
		private static PropertyDefinition[] extraProperties = new PropertyDefinition[]
		{
			FolderSchema.DisplayName,
			SharingSchema.ExternalSharingLevelOfDetails,
			SharingSchema.ExternalSharingSyncState
		};

		// Token: 0x04000BB5 RID: 2997
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000BB6 RID: 2998
		private Dictionary<string, LocalFolder.LocalItem> localItems;

		// Token: 0x04000BB7 RID: 2999
		private List<StoreId> itemsToDelete = new List<StoreId>();

		// Token: 0x04000BB8 RID: 3000
		private Folder folder;

		// Token: 0x04000BB9 RID: 3001
		private string remoteFolderId;

		// Token: 0x04000BBA RID: 3002
		private Item binding;

		// Token: 0x02000270 RID: 624
		// (Invoke) Token: 0x060011D2 RID: 4562
		private delegate void ProcessFolderDelegate(LocalFolder folder);

		// Token: 0x02000271 RID: 625
		// (Invoke) Token: 0x060011D6 RID: 4566
		public delegate void ProcessItem(StoreId localItemId);

		// Token: 0x02000272 RID: 626
		private sealed class LocalItem
		{
			// Token: 0x060011D9 RID: 4569 RVA: 0x00053000 File Offset: 0x00051200
			public LocalItem(StoreId id, ExDateTime creationTime)
			{
				this.Id = id;
				this.CreationTime = creationTime;
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x060011DA RID: 4570 RVA: 0x00053016 File Offset: 0x00051216
			// (set) Token: 0x060011DB RID: 4571 RVA: 0x0005301E File Offset: 0x0005121E
			public StoreId Id { get; private set; }

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x060011DC RID: 4572 RVA: 0x00053027 File Offset: 0x00051227
			// (set) Token: 0x060011DD RID: 4573 RVA: 0x0005302F File Offset: 0x0005122F
			public ExDateTime CreationTime { get; private set; }
		}
	}
}
