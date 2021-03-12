using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000AE RID: 174
	internal class MigrateToArchiveEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00033048 File Offset: 0x00031248
		private int MaxItemsToMigrateInOneCycle
		{
			get
			{
				if (this.maxItemsToMigrateInOneCycle == null)
				{
					object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.MaxItemsToMigrateForEHA);
					if (obj != null && obj is int)
					{
						this.maxItemsToMigrateInOneCycle = new int?((int)obj);
					}
					else
					{
						this.maxItemsToMigrateInOneCycle = new int?(MigrateToArchiveEnforcer.DefaultMaxItemsToMigrateInOneCycle);
					}
				}
				return this.maxItemsToMigrateInOneCycle.Value;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000330AB File Offset: 0x000312AB
		internal MigrateToArchiveEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000330CC File Offset: 0x000312CC
		protected override bool QueryIsEnabled()
		{
			if (!base.MailboxDataForTags.ElcUserInformation.ProcessEhaMigratedMessages)
			{
				MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer>((long)this.GetHashCode(), "{0}: Organization's ProcessEhaMigratedMessages settings is set to false. This mailbox will not be processed for migration messages", this);
				return false;
			}
			if (base.MailboxDataForTags.MailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer>((long)this.GetHashCode(), "{0}: This is archive mailbox. This mailbox will not be processed for migration messages", this);
				return false;
			}
			return this.TryInitializeMigrationFolders(base.MailboxDataForTags.MailboxSession);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00033150 File Offset: 0x00031350
		protected override void CollectItemsToExpire()
		{
			foreach (DefaultFolderType defaultFolderType in this.folderIdsToProcess.Keys)
			{
				this.ProcessFolderType(defaultFolderType);
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000331A8 File Offset: 0x000313A8
		private bool TryInitializeMigrationFolders(MailboxSession session)
		{
			using (Folder folder = Folder.Bind(session, DefaultFolderType.Configuration))
			{
				StoreId folderId = this.GetFolderId(session, folder.Id, MigrateToArchiveEnforcer.MigrationFolderName);
				if (folderId == null)
				{
					MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer>((long)this.GetHashCode(), "{0}: MigrationFolder doesnot exist, hence skipping this mailbox for MigrateToArchiveEnforcer", this);
					return false;
				}
				StoreId folderId2 = this.GetFolderId(session, folderId, DefaultFolderType.Inbox.ToString());
				StoreId folderId3 = this.GetFolderId(session, folderId, DefaultFolderType.SentItems.ToString());
				if (folderId2 == null && folderId3 == null)
				{
					MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer>((long)this.GetHashCode(), "{0}: MigrationFolder subfolders donot exist, hence skipping this mailbox for MigrateToArchiveEnforcer", this);
					return false;
				}
				if (folderId2 != null)
				{
					this.folderIdsToProcess.Add(DefaultFolderType.Inbox, folderId2);
				}
				if (folderId3 != null)
				{
					this.folderIdsToProcess.Add(DefaultFolderType.SentItems, folderId3);
				}
				if (this.AreAllSubFoldersEmtpy())
				{
					MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer>((long)this.GetHashCode(), "{0}: Migration folder found but all subfolders under it are empty, hence this mailbox will not be processed for migration messages", this);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000332A4 File Offset: 0x000314A4
		private bool AreAllSubFoldersEmtpy()
		{
			foreach (DefaultFolderType key in this.folderIdsToProcess.Keys)
			{
				StoreId folderId = this.folderIdsToProcess[key];
				using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, folderId, new PropertyDefinition[]
				{
					FolderSchema.ItemCount
				}))
				{
					if (folder.ItemCount > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00033350 File Offset: 0x00031550
		private StoreId GetFolderId(MailboxSession session, StoreId rootFolderId, string ChildFolderName)
		{
			StoreId result;
			using (Folder folder = Folder.Bind(session, rootFolderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, MigrateToArchiveEnforcer.DataColumns))
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, ChildFolderName);
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows.Length <= 0)
						{
							MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer, string>((long)this.GetHashCode(), "{0}: Folder not found {1}", this, ChildFolderName);
							result = null;
						}
						else
						{
							StoreObjectId objectId = (rows[0][0] as VersionedId).ObjectId;
							string arg = rows[0][1] as string;
							MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer, string>((long)this.GetHashCode(), "{0}: Found MigrationFolder , Display Name {1}", this, arg);
							result = objectId;
						}
					}
					else
					{
						MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer, string>((long)this.GetHashCode(), "{0}: Folder not found {1}", this, ChildFolderName);
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00033444 File Offset: 0x00031644
		private void ProcessFolderType(DefaultFolderType defaultFolderType)
		{
			StoreId folderId = this.folderIdsToProcess[defaultFolderType];
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, folderId))
			{
				this.ProcessFolderContents(folder, defaultFolderType, ItemQueryType.None);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00033498 File Offset: 0x00031698
		private void ProcessFolderContents(Folder folder, DefaultFolderType folderTypeToCollect, ItemQueryType itemQueryType)
		{
			int num = base.FolderItemTypeCount(folder, itemQueryType);
			if (num <= 0)
			{
				MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer, string, ItemQueryType>((long)this.GetHashCode(), "{0}:{1} Folder is Empty of type {2}", this, folder.Id.ObjectId.ToHexEntryId(), itemQueryType);
				return;
			}
			using (QueryResult queryResult = folder.ItemQuery(itemQueryType, null, null, MigrateToArchiveEnforcer.PropertyColumns.PropertyDefinitions))
			{
				queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
				bool flag = false;
				while (!flag)
				{
					object[][] rows = queryResult.GetRows(100);
					if (rows.Length <= 0)
					{
						break;
					}
					foreach (object[] rawProperties in rows)
					{
						PropertyArrayProxy itemProperties = new PropertyArrayProxy(MigrateToArchiveEnforcer.PropertyColumns, rawProperties);
						this.EnlistItem(itemProperties, folderTypeToCollect);
						if (this.MaxItemsCollectedForThisCycle(folderTypeToCollect))
						{
							flag = true;
							break;
						}
					}
					base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
				}
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00033588 File Offset: 0x00031788
		private bool MaxItemsCollectedForThisCycle(DefaultFolderType folderType)
		{
			if (this.totalItemsMoved >= this.MaxItemsToMigrateInOneCycle)
			{
				MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer, string, int>((long)this.GetHashCode(), "{0}:{1} Migration Folder type {2} , Max items collected so far. Rest will be colleced in next ELC cycle", this, folderType.ToString(), this.totalItemsMoved);
				return true;
			}
			return false;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000335C4 File Offset: 0x000317C4
		private bool MaxItemsCollectedForThisCyclePerFolder(DefaultFolderType folderType)
		{
			if (folderType == DefaultFolderType.Inbox && this.itemsMovedInbox >= this.MaxItemsToMigrateInOneCycle)
			{
				MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer, string, int>((long)this.GetHashCode(), "{0}:{1} Migration Folder type {2} , Max items collected so far. Rest will be colleced in next ELC cycle", this, folderType.ToString(), this.itemsMovedInbox);
				return true;
			}
			if (folderType == DefaultFolderType.SentItems && this.itemsMovedSentItems >= this.MaxItemsToMigrateInOneCycle)
			{
				MigrateToArchiveEnforcer.Tracer.TraceDebug<MigrateToArchiveEnforcer, string, int>((long)this.GetHashCode(), "{0}:{1} Migration Folder type {2} , Max items collected so far. Rest will be colleced in next ELC cycle", this, folderType.ToString(), this.itemsMovedSentItems);
				return true;
			}
			return false;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0003364C File Offset: 0x0003184C
		private bool EnlistItem(PropertyArrayProxy itemProperties, DefaultFolderType folderTypeToCollect)
		{
			if (!(itemProperties[ItemSchema.Id] is VersionedId))
			{
				MigrateToArchiveEnforcer.Tracer.TraceError<MigrateToArchiveEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return true;
			}
			StoreObjectId storeObjectId = itemProperties[StoreObjectSchema.ParentItemId] as StoreObjectId;
			if (storeObjectId == null)
			{
				MigrateToArchiveEnforcer.Tracer.TraceError<MigrateToArchiveEnforcer>((long)this.GetHashCode(), "{0}: We could not get parent id of this item. Skipping it.", this);
				return true;
			}
			base.TagExpirationExecutor.AddToDoomedMoveToArchiveList(new ItemData((VersionedId)itemProperties[ItemSchema.Id], storeObjectId, (int)itemProperties[ItemSchema.Size]));
			if (folderTypeToCollect == DefaultFolderType.Inbox)
			{
				this.itemsMovedInbox++;
			}
			else
			{
				this.itemsMovedSentItems++;
			}
			this.totalItemsMoved++;
			return true;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00033715 File Offset: 0x00031915
		protected override void StartPerfCounterCollect()
		{
			this.itemsMovedInbox = 0;
			this.itemsMovedSentItems = 0;
			this.totalItemsMoved = 0;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0003372C File Offset: 0x0003192C
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			ELCPerfmon.TotalItemMovedToArchiveForMigration.IncrementBy((long)(this.itemsMovedSentItems + this.itemsMovedInbox));
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsMovedByMigrateToArchiveEnforcer += (long)this.totalItemsMoved;
			base.MailboxDataForTags.StatisticsLogEntry.MigrateToArchiveEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x040004DE RID: 1246
		internal static int DefaultMaxItemsToMigrateInOneCycle = int.MaxValue;

		// Token: 0x040004DF RID: 1247
		internal static readonly string MigrationFolderName = ElcGlobals.MigrationFolderName;

		// Token: 0x040004E0 RID: 1248
		private static readonly PropertyDefinition[] DataColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.DisplayName
		};

		// Token: 0x040004E1 RID: 1249
		private static readonly PropertyDefinitionArray PropertyColumns = new PropertyDefinitionArray(new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ParentItemId,
			ItemSchema.Size
		});

		// Token: 0x040004E2 RID: 1250
		private static readonly Trace Tracer = ExTraceGlobals.ExpirationTagEnforcerTracer;

		// Token: 0x040004E3 RID: 1251
		private Dictionary<DefaultFolderType, StoreId> folderIdsToProcess = new Dictionary<DefaultFolderType, StoreId>();

		// Token: 0x040004E4 RID: 1252
		private int totalItemsMoved;

		// Token: 0x040004E5 RID: 1253
		private int itemsMovedInbox;

		// Token: 0x040004E6 RID: 1254
		private int itemsMovedSentItems;

		// Token: 0x040004E7 RID: 1255
		private int? maxItemsToMigrateInOneCycle = null;
	}
}
