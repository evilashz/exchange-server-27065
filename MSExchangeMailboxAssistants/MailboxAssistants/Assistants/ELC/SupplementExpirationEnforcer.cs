using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200009F RID: 159
	internal class SupplementExpirationEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0002ED84 File Offset: 0x0002CF84
		internal SupplementExpirationEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002ED8E File Offset: 0x0002CF8E
		protected override void CollectItemsToExpire()
		{
			this.CollectItemsToExpireInIpm();
			this.CollectItemsToExpireInNonIpm();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002ED9C File Offset: 0x0002CF9C
		private void CollectItemsToExpireInIpm()
		{
			StoreId defaultFolderId = base.MailboxDataForTags.MailboxSession.GetDefaultFolderId(DefaultFolderType.AllItems);
			if (defaultFolderId == null)
			{
				SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer>((long)this.GetHashCode(), "{0}: The AllItems search folder does not exist. Skipping expiration of FAI items under IPM subtree.", this);
				return;
			}
			this.ProcessFolderContents(defaultFolderId, ItemQueryType.Associated);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002EDE0 File Offset: 0x0002CFE0
		private void CollectItemsToExpireInNonIpm()
		{
			using (IEnumerator<List<object[]>> folderHierarchy = FolderProcessor.GetFolderHierarchy(DefaultFolderType.Configuration, base.MailboxDataForTags.MailboxSession, SupplementExpirationEnforcer.FolderPropertyColumns.PropertyDefinitions))
			{
				while (folderHierarchy != null && folderHierarchy.MoveNext())
				{
					List<object[]> list = folderHierarchy.Current;
					if (list != null)
					{
						SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer>((long)this.GetHashCode(), "{0}: Got folder hierarchy under non-ipm subtree.", this);
						foreach (object[] rawProperties in list)
						{
							PropertyArrayProxy propertyArrayProxy = new PropertyArrayProxy(SupplementExpirationEnforcer.FolderPropertyColumns, rawProperties);
							if (!this.IsFolderSkippable(propertyArrayProxy))
							{
								VersionedId property = propertyArrayProxy.GetProperty<VersionedId>(FolderSchema.Id);
								this.ProcessFolderContents(property, ItemQueryType.None);
								this.ProcessFolderContents(property, ItemQueryType.Associated);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		private void EnlistItem(PropertyArrayProxy itemProperties)
		{
			VersionedId versionedId = itemProperties[ItemSchema.Id] as VersionedId;
			if (versionedId == null)
			{
				SupplementExpirationEnforcer.Tracer.TraceError<SupplementExpirationEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return;
			}
			try
			{
				if (!this.IsIndeedTaggedForExpiration(itemProperties))
				{
					SupplementExpirationEnforcer.Tracer.TraceError<SupplementExpirationEnforcer>((long)this.GetHashCode(), "{0}: Item does not have SystemData bit set. Skipping it.", this);
					return;
				}
			}
			catch (ArgumentOutOfRangeException arg)
			{
				SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, string, ArgumentOutOfRangeException>((long)this.GetHashCode(), "{0}: Corrupted Data. Skip current item {1}. Exception: {2}", this, versionedId.ObjectId.ToHexEntryId(), arg);
				return;
			}
			this.itemsExpired++;
			base.TagExpirationExecutor.AddToDoomedHardDeleteList(new ItemData(versionedId, (int)itemProperties[ItemSchema.Size]), false);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002EF8C File Offset: 0x0002D18C
		private bool IsIndeedTaggedForExpiration(PropertyArrayProxy itemProperties)
		{
			return FlagsMan.IsSystemDataSet(itemProperties[StoreObjectSchema.RetentionFlags]);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0002EFA0 File Offset: 0x0002D1A0
		private void ProcessFolderContents(StoreId folderId, ItemQueryType itemQueryType)
		{
			SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, StoreId, ItemQueryType>((long)this.GetHashCode(), "{0}: Starting to process folder {1} with query type {2}.", this, folderId, itemQueryType);
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, folderId))
			{
				if (base.FolderItemTypeCount(folder, itemQueryType) <= 0)
				{
					SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, string, ItemQueryType>((long)this.GetHashCode(), "{0}:{1} Folder is Empty of type {2}", this, folder.Id.ObjectId.ToHexEntryId(), itemQueryType);
				}
				else
				{
					using (QueryResult queryResult = folder.ItemQuery(itemQueryType, null, new SortBy[]
					{
						new SortBy(ItemSchema.RetentionDate, SortOrder.Descending)
					}, SupplementExpirationEnforcer.ItemPropertyColumns.PropertyDefinitions))
					{
						ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.RetentionDate, base.MailboxDataForTags.UtcNow.Date);
						queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
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
								PropertyArrayProxy propertyArrayProxy = new PropertyArrayProxy(SupplementExpirationEnforcer.ItemPropertyColumns, rawProperties);
								if (!(propertyArrayProxy[ItemSchema.RetentionDate] is ExDateTime))
								{
									SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer>((long)this.GetHashCode(), "{0}: Retention date is missing. Skipping items from here on.", this);
									flag = true;
									break;
								}
								this.EnlistItem(propertyArrayProxy);
							}
							base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
						}
					}
				}
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002F154 File Offset: 0x0002D354
		private bool IsFolderSkippable(PropertyArrayProxy folderProperties)
		{
			VersionedId versionedId = folderProperties[FolderSchema.Id] as VersionedId;
			if (versionedId == null)
			{
				SupplementExpirationEnforcer.Tracer.TraceError<SupplementExpirationEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this folder. Skipping it.", this);
				return true;
			}
			object obj = folderProperties[FolderSchema.MapiFolderType];
			if (obj is int && ((FolderType)obj & FolderType.Search) == FolderType.Search)
			{
				SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, VersionedId>((long)this.GetHashCode(), "{0}: The folder {1} is a search folder. Skipping it for expiration.", this, versionedId);
				return true;
			}
			if (!(folderProperties[FolderSchema.FolderFlags] is int))
			{
				SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, VersionedId>((long)this.GetHashCode(), "{0}: The FolderFlags prop doesn't not exist on folder {1}. Skipping it for expiration.", this, versionedId);
				return true;
			}
			StoreFolderFlags storeFolderFlags = (StoreFolderFlags)folderProperties[FolderSchema.FolderFlags];
			if ((storeFolderFlags & StoreFolderFlags.FolderIPM) == StoreFolderFlags.FolderIPM)
			{
				SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, VersionedId>((long)this.GetHashCode(), "{0}: The folder {1} is under IPM subtree. Skipping it for expiration.", this, versionedId);
				return true;
			}
			DefaultFolderType defaultFolderType = base.MailboxDataForTags.MailboxSession.IsDefaultFolderType(versionedId);
			if (defaultFolderType == DefaultFolderType.System || defaultFolderType == DefaultFolderType.AdminAuditLogs || defaultFolderType == DefaultFolderType.Audits || defaultFolderType == DefaultFolderType.RecoverableItemsRoot || defaultFolderType == DefaultFolderType.RecoverableItemsVersions || defaultFolderType == DefaultFolderType.RecoverableItemsDeletions || defaultFolderType == DefaultFolderType.RecoverableItemsPurges || defaultFolderType == DefaultFolderType.RecoverableItemsDiscoveryHolds || defaultFolderType == DefaultFolderType.RecoverableItemsMigratedMessages || defaultFolderType == DefaultFolderType.CalendarLogging)
			{
				SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, DefaultFolderType>((long)this.GetHashCode(), "{0}: This folder type is {1}. Skipping it for expiration.", this, defaultFolderType);
				return true;
			}
			if (base.MailboxDataForTags.ElcUserInformation.ProcessEhaMigratedMessages && !base.MailboxDataForTags.MailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, versionedId, new PropertyDefinition[]
				{
					FolderSchema.DisplayName
				}))
				{
					if (string.Compare(folder.DisplayName, ElcGlobals.MigrationFolderName, true) == 0 || string.Compare(folder.DisplayName, DefaultFolderType.Inbox.ToString(), true) == 0 || string.Compare(folder.DisplayName, DefaultFolderType.SentItems.ToString(), true) == 0)
					{
						SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, string>((long)this.GetHashCode(), "{0}: This is a migration folder in primary mailbox {1}. SupplementEnforcer skips it as it has a separate enforcer.", this, folder.DisplayName);
						return true;
					}
				}
			}
			int num = (int)folderProperties[FolderSchema.ItemCount];
			int num2 = (int)folderProperties[FolderSchema.AssociatedItemCount];
			if (num == 0 && num2 == 0)
			{
				SupplementExpirationEnforcer.Tracer.TraceDebug<SupplementExpirationEnforcer, VersionedId>((long)this.GetHashCode(), "{0}: The Folder {1} is empty. Skipping it for expiration", this, versionedId);
				return true;
			}
			return false;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002F3B0 File Offset: 0x0002D5B0
		protected override void StartPerfCounterCollect()
		{
			this.itemsExpired = 0;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002F3BC File Offset: 0x0002D5BC
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			ELCPerfmon.TotalExpiredSystemDataItems.IncrementBy((long)this.itemsExpired);
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedBySupplementExpirationEnforcer += (long)this.itemsExpired;
			base.MailboxDataForTags.StatisticsLogEntry.SupplementExpirationEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x04000487 RID: 1159
		private static readonly Trace Tracer = ExTraceGlobals.SupplementExpirationEnforcerTracer;

		// Token: 0x04000488 RID: 1160
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000489 RID: 1161
		private static readonly PropertyDefinitionArray ItemPropertyColumns = new PropertyDefinitionArray(new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.RetentionDate,
			StoreObjectSchema.RetentionFlags,
			ItemSchema.Size
		});

		// Token: 0x0400048A RID: 1162
		private static readonly PropertyDefinitionArray FolderPropertyColumns = new PropertyDefinitionArray(new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.FolderFlags,
			FolderSchema.MapiFolderType,
			FolderSchema.ItemCount,
			FolderSchema.AssociatedItemCount
		});

		// Token: 0x0400048B RID: 1163
		private int itemsExpired;
	}
}
