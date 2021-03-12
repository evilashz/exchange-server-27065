using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorFolder : AnchorStoreObject
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00007D2E File Offset: 0x00005F2E
		internal AnchorFolder(AnchorContext context, Folder folder)
		{
			AnchorUtil.ThrowOnNullArgument(context, "context");
			AnchorUtil.ThrowOnNullArgument(folder, "folder");
			base.AnchorContext = context;
			this.Folder = folder;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00007D5A File Offset: 0x00005F5A
		public override string Name
		{
			get
			{
				return this.Folder.DisplayName;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00007D67 File Offset: 0x00005F67
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00007D6F File Offset: 0x00005F6F
		internal Folder Folder { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00007D78 File Offset: 0x00005F78
		protected override StoreObject StoreObject
		{
			get
			{
				return this.Folder;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007D80 File Offset: 0x00005F80
		public static bool RemoveFolder(AnchorContext context, MailboxSession mailboxSession, string folderName)
		{
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
			StoreObjectId folderId = AnchorFolder.GetFolderId(context, mailboxSession, defaultFolderId, folderName);
			if (folderId == null)
			{
				context.Logger.Log(MigrationEventType.Verbose, "couldn't find folder with name {0} treating as success", new object[]
				{
					folderName
				});
				return true;
			}
			using (Folder folder = Folder.Bind(mailboxSession, folderId, AnchorFolder.FolderIdPropertyDefinition))
			{
				context.Logger.Log(MigrationEventType.Information, "About to remove all messages & subfolders from {0} with id {1}", new object[]
				{
					folderName,
					folderId
				});
				GroupOperationResult groupOperationResult = folder.DeleteAllObjects(DeleteItemFlags.HardDelete, true);
				if (groupOperationResult.OperationResult != OperationResult.Succeeded)
				{
					context.Logger.Log(MigrationEventType.Warning, "unsuccessfully removed messages & subfolders from {0} with id {1} with result {2}", new object[]
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
				context.Logger.Log(MigrationEventType.Information, "About to remove folder {0} with id {1}", new object[]
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
					context.Logger.Log(MigrationEventType.Warning, "unsuccessfully removed folder {0} with id {1} with result {2}", new object[]
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

		// Token: 0x06000221 RID: 545 RVA: 0x00007EF8 File Offset: 0x000060F8
		public override void Save(SaveMode saveMode)
		{
			FolderSaveResult folderSaveResult = this.Folder.Save(saveMode);
			if (folderSaveResult.OperationResult == OperationResult.Succeeded)
			{
				return;
			}
			if (AnchorUtil.IsTransientException(folderSaveResult.Exception))
			{
				throw new MigrationTransientException(folderSaveResult.Exception.LocalizedString, folderSaveResult.Exception);
			}
			throw new MigrationPermanentException(folderSaveResult.Exception.LocalizedString, folderSaveResult.Exception);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007F58 File Offset: 0x00006158
		internal static AnchorFolder GetFolder(AnchorContext context, MailboxSession mailboxSession, string folderName)
		{
			AnchorFolder result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Folder folder = AnchorFolder.GetFolder(context, mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Root), folderName);
				disposeGuard.Add<Folder>(folder);
				AnchorFolder anchorFolder = new AnchorFolder(context, folder);
				disposeGuard.Success();
				result = anchorFolder;
			}
			return result;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007FBC File Offset: 0x000061BC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Folder != null)
			{
				this.Folder.Dispose();
				this.Folder = null;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007FDB File Offset: 0x000061DB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AnchorFolder>(this);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007FE4 File Offset: 0x000061E4
		private static StoreObjectId GetFolderId(AnchorContext context, MailboxSession mailboxSession, StoreObjectId rootFolderId, string folderName)
		{
			AnchorUtil.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			AnchorUtil.ThrowOnNullArgument(rootFolderId, "rootFolderId");
			AnchorUtil.ThrowOnNullArgument(folderName, "folderName");
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
				context.Logger.Log(MigrationEventType.Warning, "Couldn't find subfolder {0}", new object[]
				{
					folderName
				});
			}
			catch (ObjectNotFoundException exception)
			{
				context.Logger.Log(MigrationEventType.Warning, exception, "Folder {0} missing, will try to create it", new object[]
				{
					folderName
				});
			}
			catch (StorageTransientException exception2)
			{
				context.Logger.Log(MigrationEventType.Warning, exception2, "Transient exception when trying to get Folder {0} will try to create it", new object[]
				{
					folderName
				});
			}
			return null;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008140 File Offset: 0x00006340
		private static Folder GetFolder(AnchorContext context, MailboxSession mailboxSession, StoreObjectId rootFolderId, string folderName)
		{
			AnchorUtil.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			AnchorUtil.ThrowOnNullArgument(rootFolderId, "rootFolderId");
			AnchorUtil.ThrowOnNullArgument(folderName, "folderName");
			Folder result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Folder folder = null;
				StoreObjectId storeObjectId = AnchorFolder.GetFolderId(context, mailboxSession, rootFolderId, folderName);
				if (storeObjectId == null)
				{
					folder = Folder.Create(mailboxSession, rootFolderId, StoreObjectType.Folder, folderName, CreateMode.OpenIfExists);
					disposeGuard.Add<Folder>(folder);
					folder.Save();
					folder.Load(AnchorFolder.FolderIdPropertyDefinition);
					storeObjectId = folder.Id.ObjectId;
				}
				if (folder == null)
				{
					folder = Folder.Bind(mailboxSession, storeObjectId, AnchorFolder.FolderIdPropertyDefinition);
					disposeGuard.Add<Folder>(folder);
				}
				disposeGuard.Success();
				result = folder;
			}
			return result;
		}

		// Token: 0x04000095 RID: 149
		internal static readonly PropertyDefinition[] FolderIdPropertyDefinition = AnchorHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				StoreObjectSchema.DisplayName
			},
			AnchorStoreObject.IdPropertyDefinition
		});
	}
}
