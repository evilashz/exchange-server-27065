using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000AC RID: 172
	internal class EHAMigratedMessageDeletionEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x00032343 File Offset: 0x00030543
		internal EHAMigratedMessageDeletionEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00032350 File Offset: 0x00030550
		protected override bool QueryIsEnabled()
		{
			if (!base.MailboxDataForTags.ElcUserInformation.ProcessEhaMigratedMessages)
			{
				EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: This mailbox doesnot belong to eha migrated organization hence it will be skipped for migrated message processing.", this);
				return false;
			}
			this.migratedMessagesFolderId = this.GetMigratedMessageFolderId();
			if (this.migratedMessagesFolderId == null)
			{
				EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: This user has no migrated messages folder. This mailbox will be skipped for migrated messages processing.", this);
				return false;
			}
			if (base.MailboxDataForTags.AbsoluteLitigationHoldEnabled)
			{
				EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: This user is under litigation hold with litigationhold duration  set to unlimited. This user's dumpster will be skipped for discovery hold processing.", this);
				return false;
			}
			if (base.MailboxDataForTags.LitigationHoldEnabled)
			{
				return true;
			}
			if (this.migratedMessagesFolderId == null)
			{
				EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: This user is not under litigation hold. This user's dumpster will be skipped for migrated messages processing.", this);
				return false;
			}
			this.itemsInMigratedMessagesFolder = this.GetUpdatedItemCountInMigratedMessagesFolder();
			if (this.itemsInMigratedMessagesFolder <= 0)
			{
				EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: This user is not under litigation hold. This user's dumpster will be skipped for migrated messages processing. Migrated messages folder exists but item count is zero", this);
				return false;
			}
			EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: This user is not under litigation hold. This user's dumpster is not skipped because migrated messages folder exists and item count is greater than zero, we need to clean up this folder", this);
			return true;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00032454 File Offset: 0x00030654
		protected override void CollectItemsToExpire()
		{
			if (base.MailboxDataForTags.LitigationHoldEnabled)
			{
				this.CollectMigratedItemsMarkedForHoldExpiration();
				return;
			}
			EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer, string>((long)this.GetHashCode(), "{0}: Delete migrated messages folder and all messages in {1}.", this, this.migratedMessagesFolderId.ToHexEntryId());
			base.MailboxDataForTags.MailboxSession.DeleteAllObjects(DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt, this.migratedMessagesFolderId);
			this.migratedItemsDeleted = this.itemsInMigratedMessagesFolder;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000324C0 File Offset: 0x000306C0
		private int GetUpdatedItemCountInMigratedMessagesFolder()
		{
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, DefaultFolderType.RecoverableItemsMigratedMessages))
			{
				this.itemsInMigratedMessagesFolder += folder.ItemCount;
			}
			return this.itemsInMigratedMessagesFolder;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00032518 File Offset: 0x00030718
		private StoreObjectId GetMigratedMessageFolderId()
		{
			return base.MailboxDataForTags.MailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsMigratedMessages);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0003252C File Offset: 0x0003072C
		private void CollectMigratedItemsMarkedForHoldExpiration()
		{
			base.SysCleanupSubAssistant.ThrowIfShuttingDown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: About to collect migrated items for expiration.", this);
			DumpsterFolderHelper.RunQueryOnMigratedMessagesFolder(base.MailboxDataForTags.MailboxSession, new SortBy(ItemSchema.EHAMigrationExpiryDate, SortOrder.Descending), new Func<QueryResult, bool>(this.MigratedMessageQueryProcessor), EHAMigratedMessageDeletionEnforcer.ItemDataColumnsWithHoldExpirationDate);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00032598 File Offset: 0x00030798
		private bool MigratedMessageQueryProcessor(QueryResult queryResults)
		{
			PropertyIndexHolder propertyIndexHolder = new PropertyIndexHolder(EHAMigratedMessageDeletionEnforcer.ItemDataColumnsWithHoldExpirationDate);
			ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.EHAMigrationExpiryDate, base.MailboxDataForTags.UtcNow.Date);
			if (queryResults.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
			{
				bool flag = false;
				while (!flag)
				{
					object[][] rows = queryResults.GetRows(100);
					EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer, int>((long)this.GetHashCode(), "{0}: GetRows returned {1} items.", this, rows.Length);
					if (rows.Length <= 0)
					{
						break;
					}
					foreach (object[] array2 in rows)
					{
						if (array2[propertyIndexHolder.EHAMigrationExpiryDateIndex] == null || array2[propertyIndexHolder.EHAMigrationExpiryDateIndex] is PropertyError)
						{
							EHAMigratedMessageDeletionEnforcer.Tracer.TraceDebug<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: All items from here onwards would be null hence skipping all the rest.", this);
							flag = true;
							break;
						}
						this.EnlistItem(array2, propertyIndexHolder);
					}
					base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
				}
			}
			return true;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00032690 File Offset: 0x00030890
		private void EnlistItem(object[] itemProperties, PropertyIndexHolder propertyIndexHolder)
		{
			string text;
			if (!this.PreScreenPassed(itemProperties, propertyIndexHolder, out text))
			{
				return;
			}
			ItemData itemData = new ItemData((VersionedId)itemProperties[propertyIndexHolder.IdIndex], (StoreObjectId)itemProperties[propertyIndexHolder.ParentItemIdIndex], (int)itemProperties[propertyIndexHolder.SizeIndex]);
			base.TagExpirationExecutor.AddToDoomedHardDeleteList(itemData, false);
			this.migratedItemsDeleted++;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000326F4 File Offset: 0x000308F4
		private bool PreScreenPassed(object[] itemProperties, PropertyIndexHolder propertyIndexHolder, out string itemClass)
		{
			itemClass = null;
			VersionedId versionedId = itemProperties[propertyIndexHolder.IdIndex] as VersionedId;
			if (versionedId == null)
			{
				EHAMigratedMessageDeletionEnforcer.Tracer.TraceError<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return false;
			}
			if (!(itemProperties[propertyIndexHolder.ParentItemIdIndex] is StoreObjectId))
			{
				EHAMigratedMessageDeletionEnforcer.Tracer.TraceError<EHAMigratedMessageDeletionEnforcer>((long)this.GetHashCode(), "{0}: We could not get parent id of this item. Skipping it.", this);
				return false;
			}
			if (base.MailboxDataForTags.CorruptItemList.Contains(versionedId.ObjectId))
			{
				EHAMigratedMessageDeletionEnforcer.Tracer.TraceError<EHAMigratedMessageDeletionEnforcer, VersionedId>((long)this.GetHashCode(), "{0}: The item {1} is in the corrupt list. Skipping it.", this, versionedId);
				return false;
			}
			itemClass = (itemProperties[propertyIndexHolder.ItemClassIndex] as string);
			itemClass = ((itemClass == null) ? string.Empty : itemClass.ToLower(CultureInfo.InvariantCulture));
			return true;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000327B3 File Offset: 0x000309B3
		protected override void StartPerfCounterCollect()
		{
			this.migratedItemsDeleted = 0;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000327BC File Offset: 0x000309BC
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			ELCPerfmon.TotalItemsDeletedDueToEHAExpiryDate.IncrementBy((long)this.migratedItemsDeleted);
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfMigratedItemsDeletedDueToMigrationExpiryDate += (long)this.migratedItemsDeleted;
			base.MailboxDataForTags.StatisticsLogEntry.EHAMigratedMessageDeletionEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x040004D0 RID: 1232
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

		// Token: 0x040004D1 RID: 1233
		private static readonly PropertyDefinition[] ItemDataColumnsWithHoldExpirationDate = new PropertyDefinition[]
		{
			ItemSchema.EHAMigrationExpiryDate,
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			StoreObjectSchema.ParentItemId,
			ItemSchema.Size
		};

		// Token: 0x040004D2 RID: 1234
		private static readonly Trace Tracer = ExTraceGlobals.ExpirationTagEnforcerTracer;

		// Token: 0x040004D3 RID: 1235
		private int migratedItemsDeleted;

		// Token: 0x040004D4 RID: 1236
		private StoreObjectId migratedMessagesFolderId;

		// Token: 0x040004D5 RID: 1237
		private int itemsInMigratedMessagesFolder;
	}
}
