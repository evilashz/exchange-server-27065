using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200009A RID: 154
	internal class DumpsterQuotaEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0002D108 File Offset: 0x0002B308
		internal DumpsterQuotaEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0002D165 File Offset: 0x0002B365
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + base.MailboxDataForTags.MailboxSession.MailboxOwner.ToString() + " being processed by DumpsterQuotaEnforcer.";
			}
			return this.toString;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0002D1A0 File Offset: 0x0002B3A0
		protected override bool QueryIsEnabled()
		{
			if (base.MailboxDataForTags.AbsoluteLitigationHoldEnabled)
			{
				DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer>((long)this.GetHashCode(), "{0}: This user is under litigation hold. This user's dumpster will be skipped.", this);
				return false;
			}
			if (base.MailboxDataForTags.MailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot) == null)
			{
				DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer>((long)this.GetHashCode(), "{0}: This user has no Dumpster root folder. The mailbox will be skipped.", this);
				return false;
			}
			DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer>((long)this.GetHashCode(), "{0}: This user is not under litigation hold. His dumpster will be processed.", this);
			return true;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0002D218 File Offset: 0x0002B418
		protected override void InvokeInternal()
		{
			if (this.IsDumpsterOverQuota())
			{
				this.isOverQuota = true;
				DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer>((long)this.GetHashCode(), "{0}: This mailbox is over dumpster warning quota. Processing the mailbox.", this);
				ELCAssistant.PublishMonitoringResult(base.MailboxDataForTags.MailboxSession, null, ELCAssistant.NotificationType.DumpsterWarningQuota, string.Format("Mailbox: {0} is over dumpster warning quota.", base.MailboxDataForTags.MailboxSession.MailboxGuid));
				base.InvokeInternal();
				if (base.IsEnabled)
				{
					this.LogDumpsterCleanupEvent();
				}
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0002D290 File Offset: 0x0002B490
		protected override void CollectItemsToExpire()
		{
			foreach (DefaultFolderType defaultFolderType in DumpsterQuotaEnforcer.DumpsterFolders)
			{
				if (this.CollectItemsInFolder(defaultFolderType))
				{
					DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer, DefaultFolderType>((long)this.GetHashCode(), "{0}: CollectItemsToExpire returned false for folderType {1}.", this, defaultFolderType);
					return;
				}
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002D2D7 File Offset: 0x0002B4D7
		private bool CollectItemsInFolder(DefaultFolderType folderToCollect)
		{
			return this.ProcessFolderContents(folderToCollect, ItemQueryType.None) || this.ProcessFolderContents(folderToCollect, ItemQueryType.Associated);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0002D2F0 File Offset: 0x0002B4F0
		private bool ProcessFolderContents(DefaultFolderType folderToCollect, ItemQueryType itemQueryType)
		{
			DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer, DefaultFolderType, ItemQueryType>((long)this.GetHashCode(), "{0}: ProcessFolderContents: folderToCollect={1}, itemQueryType={2}.", this, folderToCollect, itemQueryType);
			bool flag = false;
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, folderToCollect))
			{
				int num = base.FolderItemTypeCount(folder, itemQueryType);
				this.itemsInDumpster += num;
				if (num <= 0)
				{
					DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer, string, ItemQueryType>((long)this.GetHashCode(), "{0}:{1} Folder is Empty of type {2}", this, folder.Id.ObjectId.ToHexEntryId(), itemQueryType);
					return false;
				}
				using (QueryResult queryResult = folder.ItemQuery(itemQueryType, null, new SortBy[]
				{
					new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Ascending)
				}, DumpsterQuotaEnforcer.PropertyColumns.PropertyDefinitions))
				{
					queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
					while (!flag)
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows.Length <= 0)
						{
							break;
						}
						foreach (object[] rawProperties in rows)
						{
							if (!this.EnlistItem(new PropertyArrayProxy(DumpsterQuotaEnforcer.PropertyColumns, rawProperties)))
							{
								flag = true;
								DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer, bool>((long)this.GetHashCode(), "{0}: EnlistItem returned false. theEnd={1}.", this, flag);
								break;
							}
						}
						base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
					}
				}
			}
			return flag;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0002D47C File Offset: 0x0002B67C
		private bool EnlistItem(PropertyArrayProxy itemProperties)
		{
			VersionedId versionedId = itemProperties[ItemSchema.Id] as VersionedId;
			if (versionedId == null)
			{
				DumpsterQuotaEnforcer.Tracer.TraceError<DumpsterQuotaEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return true;
			}
			object obj = itemProperties[ItemSchema.Size];
			if (obj == null)
			{
				DumpsterQuotaEnforcer.Tracer.TraceError<DumpsterQuotaEnforcer>((long)this.GetHashCode(), "{0}: We could not get size of this item. Skipping it.", this);
				return true;
			}
			this.deletedItemsSize += (ulong)((long)((int)obj));
			this.itemsExpired++;
			if (base.MailboxDataForTags.QueryBasedHoldEnabled)
			{
				ItemData itemData = new ItemData(versionedId, itemProperties.GetProperty<StoreObjectId>(StoreObjectSchema.ParentItemId), ItemData.EnforcerType.DumpsterQuotaEnforcer, itemProperties.GetProperty<int>(ItemSchema.Size));
				base.TagExpirationExecutor.AddToDoomedMoveToDiscoveryHoldsList(itemData, false);
			}
			else
			{
				ItemData itemData2 = new ItemData(versionedId, ItemData.EnforcerType.DumpsterQuotaEnforcer, itemProperties.GetProperty<int>(ItemSchema.Size));
				base.TagExpirationExecutor.AddToDoomedHardDeleteList(itemData2, false);
			}
			bool flag = this.dumpsterSize - this.deletedItemsSize > this.dumpsterQuota.Value.ToBytes();
			if (!flag)
			{
				DumpsterQuotaEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Collected sufficient items. dumspterSize={1}, deletedItemsSize={2}, itemId={3}.", new object[]
				{
					this,
					this.dumpsterSize,
					this.deletedItemsSize,
					versionedId
				});
			}
			return flag;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0002D5CD File Offset: 0x0002B7CD
		protected override void ExpireItemsAlready()
		{
			this.GetFolderStats(this.beforeFolderSize, this.beforeFolderCount);
			base.ExpireItemsAlready();
			this.GetFolderStats(this.afterFolderSize, this.afterFolderCount);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0002D5FC File Offset: 0x0002B7FC
		private bool IsDumpsterOverQuota()
		{
			bool? useDatabaseQuotaDefaults = base.MailboxDataForTags.ElcUserInformation.ADUser.UseDatabaseQuotaDefaults;
			if (useDatabaseQuotaDefaults != null && !useDatabaseQuotaDefaults.Value)
			{
				this.dumpsterQuota = base.MailboxDataForTags.ElcUserInformation.ADUser.RecoverableItemsWarningQuota;
				DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. RecoverableItemsWarningQuota from mailbox object = {2}.", this, useDatabaseQuotaDefaults, this.dumpsterQuota);
			}
			else
			{
				if (base.SysCleanupSubAssistant.DatabaseConfig.DatabaseDumpsterWarningQuota == null)
				{
					DumpsterQuotaEnforcer.Tracer.TraceError<DumpsterQuotaEnforcer>((long)this.GetHashCode(), "{0}: We could not get RecoverableItemsWarningQuota of this mailbox database. Skipping it.", this);
					return false;
				}
				this.dumpsterQuota = base.SysCleanupSubAssistant.DatabaseConfig.DatabaseDumpsterWarningQuota.Value;
				DumpsterQuotaEnforcer.Tracer.TraceDebug<DumpsterQuotaEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. Mailbox.RecoverableItemsWarningQuota from database object = {2}.", this, useDatabaseQuotaDefaults, this.dumpsterQuota);
			}
			if (this.dumpsterQuota.IsUnlimited)
			{
				return false;
			}
			ulong? num = base.MailboxDataForTags.MailboxSession.DumpsterSize;
			if (num != null)
			{
				this.dumpsterSize = num.Value;
				return this.dumpsterSize > this.dumpsterQuota.Value.ToBytes();
			}
			return false;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002D734 File Offset: 0x0002B934
		private void GetFolderStats(string[] folderSize, string[] folderCount)
		{
			for (int i = 0; i < DumpsterQuotaEnforcer.DumpsterFolders.Length; i++)
			{
				if (base.MailboxDataForTags.MailboxSession.GetDefaultFolderId(DumpsterQuotaEnforcer.DumpsterFolders[i]) == null)
				{
					folderSize[i] = "-";
					folderCount[i] = "-";
				}
				else
				{
					using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, DumpsterQuotaEnforcer.DumpsterFolders[i], new PropertyDefinition[]
					{
						FolderSchema.ExtendedSize
					}))
					{
						folderSize[i] = folder[FolderSchema.ExtendedSize].ToString();
						folderCount[i] = folder.ItemCount.ToString();
					}
				}
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0002D7EC File Offset: 0x0002B9EC
		private void LogDumpsterCleanupEvent()
		{
			base.MailboxDataForTags.MailboxSession.Mailbox.ForceReload(new PropertyDefinition[]
			{
				MailboxSchema.DumpsterQuotaUsedExtended
			});
			object obj = base.MailboxDataForTags.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.DumpsterQuotaUsedExtended);
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_DumpsterOverQuotaDeletedMails, null, new object[]
			{
				base.MailboxDataForTags.MailboxSession.MailboxOwner,
				this.dumpsterQuota,
				this.dumpsterSize,
				obj,
				string.Join(", ", this.beforeFolderSize),
				string.Join(", ", this.beforeFolderCount),
				string.Join(", ", this.afterFolderSize),
				string.Join(", ", this.afterFolderCount)
			});
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0002D8D1 File Offset: 0x0002BAD1
		protected override void StartPerfCounterCollect()
		{
			this.itemsInDumpster = 0;
			this.itemsExpired = 0;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			if (this.isOverQuota)
			{
				ELCPerfmon.TotalOverQuotaDumpsters.Increment();
			}
			ELCPerfmon.TotalOverQuotaDumpsterItems.IncrementBy((long)this.itemsInDumpster);
			ELCPerfmon.TotalOverQuotaDumpsterItemsDeleted.IncrementBy((long)this.itemsExpired);
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedByDumpsterQuotaEnforcer += (long)this.itemsExpired;
			base.MailboxDataForTags.StatisticsLogEntry.DumpsterQuotaEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x04000462 RID: 1122
		private const string CommaSeparator = ", ";

		// Token: 0x04000463 RID: 1123
		private static readonly Trace Tracer = ExTraceGlobals.DumpsterQuotaEnforcerTracer;

		// Token: 0x04000464 RID: 1124
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000465 RID: 1125
		protected static readonly DefaultFolderType[] DumpsterFolders = new DefaultFolderType[]
		{
			DefaultFolderType.RecoverableItemsRoot,
			DefaultFolderType.RecoverableItemsVersions,
			DefaultFolderType.RecoverableItemsPurges,
			DefaultFolderType.RecoverableItemsDeletions
		};

		// Token: 0x04000466 RID: 1126
		private static readonly PropertyDefinitionArray PropertyColumns = new PropertyDefinitionArray(new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Size,
			StoreObjectSchema.ParentItemId
		});

		// Token: 0x04000467 RID: 1127
		private ulong deletedItemsSize;

		// Token: 0x04000468 RID: 1128
		private Unlimited<ByteQuantifiedSize> dumpsterQuota;

		// Token: 0x04000469 RID: 1129
		private ulong dumpsterSize;

		// Token: 0x0400046A RID: 1130
		private string[] beforeFolderSize = new string[DumpsterQuotaEnforcer.DumpsterFolders.Length];

		// Token: 0x0400046B RID: 1131
		private string[] afterFolderSize = new string[DumpsterQuotaEnforcer.DumpsterFolders.Length];

		// Token: 0x0400046C RID: 1132
		private string[] beforeFolderCount = new string[DumpsterQuotaEnforcer.DumpsterFolders.Length];

		// Token: 0x0400046D RID: 1133
		private string[] afterFolderCount = new string[DumpsterQuotaEnforcer.DumpsterFolders.Length];

		// Token: 0x0400046E RID: 1134
		private string toString;

		// Token: 0x0400046F RID: 1135
		private bool isOverQuota;

		// Token: 0x04000470 RID: 1136
		private int itemsInDumpster;

		// Token: 0x04000471 RID: 1137
		private int itemsExpired;
	}
}
