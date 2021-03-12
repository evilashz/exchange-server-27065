using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C7 RID: 199
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationFolder : MigrationStoreObject
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x0002C48A File Offset: 0x0002A68A
		internal MigrationFolder(Folder folder)
		{
			MigrationUtil.ThrowOnNullArgument(folder, "folder");
			this.Folder = folder;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002C4A4 File Offset: 0x0002A6A4
		public override string Name
		{
			get
			{
				return this.Folder.DisplayName;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002C4B1 File Offset: 0x0002A6B1
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0002C4B9 File Offset: 0x0002A6B9
		internal Folder Folder { get; private set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002C4C2 File Offset: 0x0002A6C2
		protected override StoreObject StoreObject
		{
			get
			{
				return this.Folder;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002C4CC File Offset: 0x0002A6CC
		public static bool RemoveFolder(MailboxSession mailboxSession, MigrationFolderName folderName)
		{
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
			StoreObjectId folderId = MigrationFolder.GetFolderId(mailboxSession, defaultFolderId, folderName.ToString());
			if (folderId == null)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "couldn't find folder with name {0} treating as success", new object[]
				{
					folderName
				});
				return true;
			}
			using (Folder folder = Folder.Bind(mailboxSession, folderId, MigrationFolder.FolderIdPropertyDefinition))
			{
				MigrationLogger.Log(MigrationEventType.Information, "About to remove all messages & subfolders from {0} with id {1}", new object[]
				{
					folderName,
					folderId
				});
				GroupOperationResult groupOperationResult = folder.DeleteAllObjects(DeleteItemFlags.HardDelete, true);
				if (groupOperationResult.OperationResult != OperationResult.Succeeded)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "unsuccessfully removed messages & subfolders from {0} with id {1} with result {2}", new object[]
					{
						folderName,
						folderId,
						groupOperationResult
					});
					return false;
				}
			}
			bool result;
			using (Folder folder2 = Folder.Bind(mailboxSession, defaultFolderId))
			{
				MigrationLogger.Log(MigrationEventType.Information, "About to remove folder {0} with id {1}", new object[]
				{
					folderName,
					folderId
				});
				AggregateOperationResult aggregateOperationResult = folder2.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
				{
					folderId
				});
				if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "unsuccessfully removed folder {0} with id {1} with result {2}", new object[]
					{
						folderName,
						folderId,
						aggregateOperationResult
					});
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002C648 File Offset: 0x0002A848
		public override void Save(SaveMode saveMode)
		{
			FolderSaveResult folderSaveResult = this.Folder.Save(saveMode);
			if (folderSaveResult.OperationResult == OperationResult.Succeeded)
			{
				return;
			}
			if (MigrationUtil.IsTransientException(folderSaveResult.Exception))
			{
				throw new FailedToSaveFolderTransientException(folderSaveResult.Exception.LocalizedString, folderSaveResult.Exception);
			}
			throw new FailedToSaveFolderPermanentException(folderSaveResult.Exception.LocalizedString, folderSaveResult.Exception);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002C6A8 File Offset: 0x0002A8A8
		internal static MigrationFolder GetFolder(MailboxSession mailboxSession, MigrationFolderName folderName)
		{
			MigrationFolder result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Folder folder = MigrationFolder.GetFolder(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Root), folderName.ToString());
				disposeGuard.Add<Folder>(folder);
				MigrationFolder migrationFolder = new MigrationFolder(folder);
				disposeGuard.Success();
				result = migrationFolder;
			}
			return result;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002C714 File Offset: 0x0002A914
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Folder != null)
			{
				this.Folder.Dispose();
				this.Folder = null;
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002C733 File Offset: 0x0002A933
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationFolder>(this);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002C73C File Offset: 0x0002A93C
		private static StoreObjectId GetFolderId(MailboxSession mailboxSession, StoreObjectId rootFolderId, string folderName)
		{
			MigrationUtil.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			MigrationUtil.ThrowOnNullArgument(rootFolderId, "rootFolderId");
			MigrationUtil.ThrowOnNullArgument(folderName, "folderName");
			try
			{
				using (Folder folder = Folder.Bind(mailboxSession, rootFolderId))
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
					{
						FolderSchema.Id,
						StoreObjectSchema.DisplayName
					}))
					{
						QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.DisplayName, folderName);
						if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
						{
							object[][] rows = queryResult.GetRows(1);
							if (rows.Length > 0)
							{
								VersionedId versionedId = (VersionedId)rows[0][0];
								return versionedId.ObjectId;
							}
						}
					}
				}
				MigrationLogger.Log(MigrationEventType.Warning, "Couldn't find subfolder {0}", new object[]
				{
					folderName
				});
			}
			catch (ObjectNotFoundException exception)
			{
				MigrationLogger.Log(MigrationEventType.Warning, exception, "Folder {0} missing, will try to create it", new object[]
				{
					folderName
				});
			}
			catch (StorageTransientException exception2)
			{
				MigrationLogger.Log(MigrationEventType.Warning, exception2, "Transient exception when trying to get Folder {0} will try to create it", new object[]
				{
					folderName
				});
			}
			return null;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002C880 File Offset: 0x0002AA80
		private static Folder GetFolder(MailboxSession mailboxSession, StoreObjectId rootFolderId, string folderName)
		{
			MigrationUtil.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			MigrationUtil.ThrowOnNullArgument(rootFolderId, "rootFolderId");
			MigrationUtil.ThrowOnNullArgument(folderName, "folderName");
			Folder result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Folder folder = null;
				StoreObjectId storeObjectId = MigrationFolder.GetFolderId(mailboxSession, rootFolderId, folderName);
				if (storeObjectId == null)
				{
					folder = Folder.Create(mailboxSession, rootFolderId, StoreObjectType.Folder, folderName, CreateMode.OpenIfExists);
					disposeGuard.Add<Folder>(folder);
					folder.Save();
					folder.Load(MigrationFolder.FolderIdPropertyDefinition);
					storeObjectId = folder.Id.ObjectId;
				}
				if (folder == null)
				{
					folder = Folder.Bind(mailboxSession, storeObjectId, MigrationFolder.FolderIdPropertyDefinition);
					disposeGuard.Add<Folder>(folder);
				}
				disposeGuard.Success();
				result = folder;
			}
			return result;
		}

		// Token: 0x04000416 RID: 1046
		internal static readonly PropertyDefinition[] FolderIdPropertyDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				StoreObjectSchema.DisplayName
			},
			MigrationStoreObject.IdPropertyDefinition
		});
	}
}
