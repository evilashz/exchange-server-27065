using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000AD RID: 173
	internal class EHAHiddenFolderCleanupEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x000328B8 File Offset: 0x00030AB8
		internal EHAHiddenFolderCleanupEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
			object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.EHAHiddenFolderCleanupBatchSizeForELC);
			if (obj is int)
			{
				this.maxItemsToDeleteInOneCycle = (int)obj;
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00032908 File Offset: 0x00030B08
		protected override bool QueryIsEnabled()
		{
			if (base.MailboxDataForTags.MailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer>((long)this.GetHashCode(), "{0}: This is archive mailbox. This mailbox will not be processed for EHA Hidden folder cleanup", this);
				return false;
			}
			if (base.MailboxDataForTags.MailboxSession.MailboxOwner.GetArchiveMailbox() != null)
			{
				EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer>((long)this.GetHashCode(), "{0}: User has an archive mailbox. This mailbox will not be processed for EHA Hidden folder cleanup", this);
				return false;
			}
			ADUser aduser = base.MailboxDataForTags.ElcUserInformation.ADUser;
			if (aduser == null)
			{
				EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer>((long)this.GetHashCode(), "{0}: Could not read user's AD information to verify if the user has a disable archive. This mailbox will not be processed for EHA Hidden folder cleanup", this);
				return false;
			}
			if (!object.Equals(aduser.DisabledArchiveGuid, Guid.Empty))
			{
				EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, Guid>((long)this.GetHashCode(), "{0}: User has a disable archive guid present guid {1}. This mailbox will not be processed for EHA Hidden folder cleanup", this, aduser.DisabledArchiveGuid);
				return false;
			}
			return this.GatherMigrationFoldersIfExist(base.MailboxDataForTags.MailboxSession);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000329F8 File Offset: 0x00030BF8
		protected override void CollectItemsToExpire()
		{
			if (this.folderIdsToProcess.Count != 0)
			{
				Exception arg = null;
				if (!base.MailboxDataForTags.SetEHAHiddenFolderCleanupWatermark(out arg))
				{
					EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, Exception>((long)this.GetHashCode(), "{0}: Failed to set EHA Hidden folder cleanup watermark expcetion: {1} ", this, arg);
					return;
				}
				foreach (DefaultFolderType folderToCollect in this.folderIdsToProcess.Keys)
				{
					int num = this.ItemsLeftToCollect();
					if (num <= 0)
					{
						break;
					}
					this.CollectItemsInFolder(folderToCollect);
				}
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00032A94 File Offset: 0x00030C94
		protected override void ExpireItemsAlready()
		{
			base.ExpireItemsAlready();
			this.folderIdsToProcess.Clear();
			if (!this.GatherMigrationFoldersIfExist(base.MailboxDataForTags.MailboxSession))
			{
				Exception arg = null;
				if (!base.MailboxDataForTags.ClearEHAHiddenFolderCleanupWatermark(out arg))
				{
					EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, Exception>((long)this.GetHashCode(), "{0}: Failed to clear EHA Hidden folder cleanup watermark expcetion: {1} ", this, arg);
				}
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00032AF0 File Offset: 0x00030CF0
		private bool GatherMigrationFoldersIfExist(MailboxSession session)
		{
			using (Folder folder = Folder.Bind(session, DefaultFolderType.Configuration))
			{
				StoreId folderId = this.GetFolderId(session, folder.Id, EHAHiddenFolderCleanupEnforcer.MigrationFolderName);
				if (folderId == null)
				{
					EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer>((long)this.GetHashCode(), "{0}: MigrationFolder does not exist, hence skipping this mailbox for EHA Hidden folder cleanup", this);
					return false;
				}
				StoreId folderId2 = this.GetFolderId(session, folderId, DefaultFolderType.Inbox.ToString());
				StoreId folderId3 = this.GetFolderId(session, folderId, DefaultFolderType.SentItems.ToString());
				if (folderId2 == null && folderId3 == null)
				{
					EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer>((long)this.GetHashCode(), "{0}: MigrationFolder subfolders doesn't exist, hence skipping this mailbox for EHA Hidden folder cleanup", this);
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
			}
			return true;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00032BC4 File Offset: 0x00030DC4
		private StoreId GetFolderId(MailboxSession session, StoreId rootFolderId, string ChildFolderName)
		{
			StoreId result;
			using (Folder folder = Folder.Bind(session, rootFolderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, EHAHiddenFolderCleanupEnforcer.DataColumns))
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, ChildFolderName);
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows.Length <= 0)
						{
							EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, string>((long)this.GetHashCode(), "{0}: Folder not found {1}", this, ChildFolderName);
							result = null;
						}
						else
						{
							StoreObjectId objectId = (rows[0][0] as VersionedId).ObjectId;
							string arg = rows[0][1] as string;
							EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, string>((long)this.GetHashCode(), "{0}: Found subfolder , Display Name {1}", this, arg);
							result = objectId;
						}
					}
					else
					{
						EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, string>((long)this.GetHashCode(), "{0}: Folder not found {1}", this, ChildFolderName);
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00032CB8 File Offset: 0x00030EB8
		private void CollectItemsInFolder(DefaultFolderType folderToCollect)
		{
			EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, DefaultFolderType>((long)this.GetHashCode(), "{0}: CollectItemsInFolder: folderToCollect={1}.", this, folderToCollect);
			StoreId storeId = this.folderIdsToProcess[folderToCollect];
			int num = 0;
			int num2 = this.ItemsLeftToCollect();
			if (storeId != null)
			{
				using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, storeId))
				{
					int num3 = base.FolderItemTypeCount(folder, ItemQueryType.None);
					base.SysCleanupSubAssistant.ThrottleStoreCall();
					if (num3 <= 0 || num3 <= num2)
					{
						EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, DefaultFolderType, int>((long)this.GetHashCode(), "{0}: Deleting {1} folder with {2} items", this, folderToCollect, num3);
						num = num3;
						folder.Load(new PropertyDefinition[]
						{
							FolderSchema.ExtendedSize
						});
						object obj = folder.TryGetProperty(FolderSchema.ExtendedSize);
						if (obj is long)
						{
							long num4 = (long)obj;
							base.TagExpirationExecutor.AddToDoomedHardDeleteList(new ItemData(folder.Id, (int)num4), true);
						}
					}
					else
					{
						base.SysCleanupSubAssistant.ThrottleStoreCall();
						using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, EHAHiddenFolderCleanupEnforcer.PropertyColumns.PropertyDefinitions))
						{
							queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
							bool flag = true;
							while (flag)
							{
								int num5 = num2 - num;
								if (num5 > 0)
								{
									object[][] rows = queryResult.GetRows(num5, out flag);
									foreach (object[] rawProperties in rows)
									{
										PropertyArrayProxy propertyArrayProxy = new PropertyArrayProxy(EHAHiddenFolderCleanupEnforcer.PropertyColumns, rawProperties);
										EHAHiddenFolderCleanupEnforcer.Tracer.TraceDebug<EHAHiddenFolderCleanupEnforcer, VersionedId, DefaultFolderType>((long)this.GetHashCode(), "{0}: Deleting item id [{1}] in folder {2} ", this, (VersionedId)propertyArrayProxy[ItemSchema.Id], folderToCollect);
										base.TagExpirationExecutor.AddToDoomedHardDeleteList(new ItemData((VersionedId)propertyArrayProxy[ItemSchema.Id], (StoreObjectId)propertyArrayProxy[StoreObjectSchema.ParentItemId], (int)propertyArrayProxy[ItemSchema.Size]), true);
									}
									num += rows.Length;
								}
								else
								{
									flag = false;
								}
							}
						}
					}
				}
			}
			this.IncrementFolderCollectCount(folderToCollect, num);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00032EF8 File Offset: 0x000310F8
		private void IncrementFolderCollectCount(DefaultFolderType folderType, int itemsCollected)
		{
			if (folderType == DefaultFolderType.Inbox)
			{
				this.itemsDeletedFromInbox += itemsCollected;
				return;
			}
			if (folderType != DefaultFolderType.SentItems)
			{
				return;
			}
			this.itemsDeletedFromSent += itemsCollected;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00032F30 File Offset: 0x00031130
		private int ItemsLeftToCollect()
		{
			int num = this.maxItemsToDeleteInOneCycle - (this.itemsDeletedFromInbox + this.itemsDeletedFromSent);
			if (num >= 0)
			{
				return num;
			}
			return 0;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00032F59 File Offset: 0x00031159
		protected override void StartPerfCounterCollect()
		{
			this.itemsDeletedFromInbox = 0;
			this.itemsDeletedFromSent = 0;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00032F6C File Offset: 0x0003116C
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			ELCPerfmon.TotalItemsDeletedByEHAHiddenFolderCleanupEnforcer.IncrementBy((long)(this.itemsDeletedFromInbox + this.itemsDeletedFromSent));
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedFromInboxByEHAHiddenFolderCleanupEnforcer += (long)this.itemsDeletedFromInbox;
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedFromSentByEHAHiddenFolderCleanupEnforcer += (long)this.itemsDeletedFromSent;
			base.MailboxDataForTags.StatisticsLogEntry.EHAHiddenFolderCleanupEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x040004D6 RID: 1238
		internal static readonly string MigrationFolderName = ElcGlobals.MigrationFolderName;

		// Token: 0x040004D7 RID: 1239
		private static readonly PropertyDefinition[] DataColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.DisplayName
		};

		// Token: 0x040004D8 RID: 1240
		private static readonly PropertyDefinitionArray PropertyColumns = new PropertyDefinitionArray(new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ParentItemId,
			ItemSchema.Size
		});

		// Token: 0x040004D9 RID: 1241
		private static readonly Trace Tracer = ExTraceGlobals.EHAHiddenFolderCleanupEnforcerTracer;

		// Token: 0x040004DA RID: 1242
		private readonly int maxItemsToDeleteInOneCycle = 2000;

		// Token: 0x040004DB RID: 1243
		private Dictionary<DefaultFolderType, StoreId> folderIdsToProcess = new Dictionary<DefaultFolderType, StoreId>();

		// Token: 0x040004DC RID: 1244
		private int itemsDeletedFromInbox;

		// Token: 0x040004DD RID: 1245
		private int itemsDeletedFromSent;
	}
}
