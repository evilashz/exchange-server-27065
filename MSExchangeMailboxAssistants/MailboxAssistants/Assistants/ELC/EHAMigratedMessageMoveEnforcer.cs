using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000AB RID: 171
	internal class EHAMigratedMessageMoveEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x00031F0A File Offset: 0x0003010A
		internal EHAMigratedMessageMoveEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00031F14 File Offset: 0x00030114
		protected override bool QueryIsEnabled()
		{
			if (!base.MailboxDataForTags.ElcUserInformation.ProcessEhaMigratedMessages)
			{
				EHAMigratedMessageMoveEnforcer.Tracer.TraceDebug<EHAMigratedMessageMoveEnforcer>((long)this.GetHashCode(), "{0}: This mailbox doesnot belong to eha migrated organization hence it will be skipped for migrated message processing.", this);
				return false;
			}
			this.discoveryHoldsFolderId = this.GetDiscoveryHoldFolderId();
			if (this.discoveryHoldsFolderId == null)
			{
				EHAMigratedMessageMoveEnforcer.Tracer.TraceDebug<EHAMigratedMessageMoveEnforcer>((long)this.GetHashCode(), "{0}: This user has no discovery hold folder. This mailbox will be skipped for discovery hold processing.", this);
				return false;
			}
			if (base.MailboxDataForTags.AbsoluteLitigationHoldEnabled)
			{
				EHAMigratedMessageMoveEnforcer.Tracer.TraceDebug<EHAMigratedMessageMoveEnforcer>((long)this.GetHashCode(), "{0}: This user is under litigation hold with litigationhold duration  set to unlimited. This user's dumpster will be skipped for discovery hold processing.", this);
				return false;
			}
			if (!base.MailboxDataForTags.LitigationHoldEnabled)
			{
				EHAMigratedMessageMoveEnforcer.Tracer.TraceDebug<EHAMigratedMessageMoveEnforcer>((long)this.GetHashCode(), "{0}: This user is not under litigation hold. This user's dumpster will be skipped for eha migrated message processing.", this);
				return false;
			}
			return true;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00031FC6 File Offset: 0x000301C6
		protected override void CollectItemsToExpire()
		{
			this.CollectMigratedItemsMarkedForHoldExpiration();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00031FCE File Offset: 0x000301CE
		private StoreObjectId GetDiscoveryHoldFolderId()
		{
			return base.MailboxDataForTags.MailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDiscoveryHolds);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00031FE4 File Offset: 0x000301E4
		private void CollectMigratedItemsMarkedForHoldExpiration()
		{
			base.SysCleanupSubAssistant.ThrowIfShuttingDown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			EHAMigratedMessageMoveEnforcer.Tracer.TraceDebug<EHAMigratedMessageMoveEnforcer>((long)this.GetHashCode(), "{0}: About to collect migrated items for expiration.", this);
			DumpsterFolderHelper.RunQueryOnDiscoveryHoldsFolder(base.MailboxDataForTags.MailboxSession, new SortBy(ItemSchema.EHAMigrationExpiryDate, SortOrder.Descending), new Func<QueryResult, bool>(this.MigratedMessageQueryProcessor), EHAMigratedMessageMoveEnforcer.ItemDataColumnsWithHoldExpirationDate);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00032050 File Offset: 0x00030250
		private bool MigratedMessageQueryProcessor(QueryResult queryResults)
		{
			PropertyIndexHolder propertyIndexHolder = new PropertyIndexHolder(EHAMigratedMessageMoveEnforcer.ItemDataColumnsWithHoldExpirationDate);
			queryResults.SeekToOffset(SeekReference.OriginBeginning, 0);
			bool flag = false;
			while (!flag)
			{
				object[][] rows = queryResults.GetRows(100);
				EHAMigratedMessageMoveEnforcer.Tracer.TraceDebug<EHAMigratedMessageMoveEnforcer, int>((long)this.GetHashCode(), "{0}: GetRows returned {1} items.", this, rows.Length);
				if (rows.Length <= 0)
				{
					break;
				}
				foreach (object[] array2 in rows)
				{
					if (array2[propertyIndexHolder.EHAMigrationExpiryDateIndex] == null || array2[propertyIndexHolder.EHAMigrationExpiryDateIndex] is PropertyError)
					{
						EHAMigratedMessageMoveEnforcer.Tracer.TraceDebug<EHAMigratedMessageMoveEnforcer>((long)this.GetHashCode(), "{0}: All items from here onwards would be null hence skipping all the rest.", this);
						flag = true;
						break;
					}
					this.EnlistItem(array2, propertyIndexHolder);
				}
				base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			}
			return true;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0003211C File Offset: 0x0003031C
		private void EnlistItem(object[] itemProperties, PropertyIndexHolder propertyIndexHolder)
		{
			string text;
			if (!this.PreScreenPassed(itemProperties, propertyIndexHolder, out text))
			{
				return;
			}
			ItemData itemData = new ItemData((VersionedId)itemProperties[propertyIndexHolder.IdIndex], (StoreObjectId)itemProperties[propertyIndexHolder.ParentItemIdIndex], (int)itemProperties[propertyIndexHolder.SizeIndex]);
			base.TagExpirationExecutor.AddToDoomedMoveToMigratedMessagesList(itemData);
			this.itemsMovedToMigratedMessagesFolder++;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00032180 File Offset: 0x00030380
		private bool PreScreenPassed(object[] itemProperties, PropertyIndexHolder propertyIndexHolder, out string itemClass)
		{
			itemClass = null;
			VersionedId versionedId = itemProperties[propertyIndexHolder.IdIndex] as VersionedId;
			if (versionedId == null)
			{
				EHAMigratedMessageMoveEnforcer.Tracer.TraceError<EHAMigratedMessageMoveEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return false;
			}
			if (!(itemProperties[propertyIndexHolder.ParentItemIdIndex] is StoreObjectId))
			{
				EHAMigratedMessageMoveEnforcer.Tracer.TraceError<EHAMigratedMessageMoveEnforcer>((long)this.GetHashCode(), "{0}: We could not get parent id of this item. Skipping it.", this);
				return false;
			}
			if (base.MailboxDataForTags.CorruptItemList.Contains(versionedId.ObjectId))
			{
				EHAMigratedMessageMoveEnforcer.Tracer.TraceError<EHAMigratedMessageMoveEnforcer, VersionedId>((long)this.GetHashCode(), "{0}: The item {1} is in the corrupt list. Skipping it.", this, versionedId);
				return false;
			}
			itemClass = (itemProperties[propertyIndexHolder.ItemClassIndex] as string);
			itemClass = ((itemClass == null) ? string.Empty : itemClass.ToLower(CultureInfo.InvariantCulture));
			return true;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0003223F File Offset: 0x0003043F
		protected override void StartPerfCounterCollect()
		{
			this.itemsMovedToMigratedMessagesFolder = 0;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00032248 File Offset: 0x00030448
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			ELCPerfmon.TotalItemsMovedDueToEHAExpiryDate.IncrementBy((long)this.itemsMovedToMigratedMessagesFolder);
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfMigratedItemsMovedDueToMigrationExpiryDate += (long)this.itemsMovedToMigratedMessagesFolder;
			base.MailboxDataForTags.StatisticsLogEntry.EHAMigratedMessageMoveEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x040004CB RID: 1227
		private static readonly PropertyDefinition[] ItemDataColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ParentItemId,
			ItemSchema.Size,
			StoreObjectSchema.ItemClass,
			ItemSchema.ReceivedTime,
			StoreObjectSchema.CreationTime,
			CalendarItemBaseSchema.CalendarItemType,
			CalendarItemInstanceSchema.EndTime,
			TaskSchema.IsTaskRecurring,
			ItemSchema.EHAMigrationExpiryDate
		};

		// Token: 0x040004CC RID: 1228
		private static readonly PropertyDefinition[] ItemDataColumnsWithHoldExpirationDate = new PropertyDefinition[]
		{
			ItemSchema.EHAMigrationExpiryDate,
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			StoreObjectSchema.ParentItemId,
			ItemSchema.Size
		};

		// Token: 0x040004CD RID: 1229
		private static readonly Trace Tracer = ExTraceGlobals.ExpirationTagEnforcerTracer;

		// Token: 0x040004CE RID: 1230
		private int itemsMovedToMigratedMessagesFolder;

		// Token: 0x040004CF RID: 1231
		private StoreObjectId discoveryHoldsFolderId;
	}
}
