using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000064 RID: 100
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TagExpirationExecutor : ExpirationExecutor
	{
		// Token: 0x0600038A RID: 906 RVA: 0x00018AAB File Offset: 0x00016CAB
		internal TagExpirationExecutor(MailboxData mailboxData, ElcSubAssistant elcAssistant) : base(mailboxData, elcAssistant, TagExpirationExecutor.Tracer)
		{
			ExpirationExecutor.TracerPfd.TracePfd<int, TagExpirationExecutor>((long)this.GetHashCode(), "PFD IWE {0} {1} called", 30999, this);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00018AD6 File Offset: 0x00016CD6
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "TagExpiration executor for mailbox: " + base.MailboxData.MailboxSession.MailboxOwner.ToString();
			}
			return this.toString;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00018B0B File Offset: 0x00016D0B
		protected override void ExpireInBatches(List<ItemData> listToSend, ExpirationExecutor.Action retentionActionType)
		{
			this.InternalExpireInBatches(listToSend, retentionActionType, null, null);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00018B18 File Offset: 0x00016D18
		protected override void PrepareAndExpireInBatches(List<ItemData> listToSend, ExpirationExecutor.Action retentionActionType)
		{
			if (retentionActionType == ExpirationExecutor.Action.MoveToArchive && base.MailboxData.ArchiveProcessor != null)
			{
				listToSend.Sort(new Comparison<ItemData>(TagExpirationExecutor.CompareItemData));
				foreach (TagExpirationExecutor.ItemSet itemSet in TagExpirationExecutor.GetItemSets(listToSend))
				{
					List<string> collection = new List<string>();
					int num = 0;
					List<Exception> exceptions = base.MailboxData.Exceptions;
					base.MailboxData.ArchiveProcessor.MoveToArchive(itemSet, base.ElcAssistant, base.MailboxData.FolderArchiver, base.MailboxData.TotalErrors, ref exceptions, out this.foldersWithErrors, out num);
					base.MailboxData.TotalErrors += num;
					this.foldersWithErrors.AddRange(collection);
				}
				this.LogBadFolders();
				return;
			}
			if (retentionActionType == ExpirationExecutor.Action.MoveToDiscoveryHolds || retentionActionType == ExpirationExecutor.Action.MoveToMigratedMessages || retentionActionType == ExpirationExecutor.Action.MoveToPurges)
			{
				listToSend.Sort(new Comparison<ItemData>(TagExpirationExecutor.CompareItemData));
				foreach (TagExpirationExecutor.ItemSet itemSet2 in TagExpirationExecutor.GetItemSets(listToSend))
				{
					using (Folder folder = Folder.Bind(base.MailboxData.MailboxSession, itemSet2.FolderId))
					{
						this.InternalExpireInBatches(itemSet2.Items, retentionActionType, folder, null);
					}
				}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00018C9C File Offset: 0x00016E9C
		protected override void PrepareAndExpireInBatches(Dictionary<DefaultFolderType, List<ItemData>> listToSend, ExpirationExecutor.Action retentionActionType)
		{
			if (retentionActionType == ExpirationExecutor.Action.MoveToArchiveDumpster && base.MailboxData.ArchiveProcessor != null)
			{
				foreach (DefaultFolderType defaultFolderType in listToSend.Keys)
				{
					List<string> collection = new List<string>();
					int num = 0;
					List<Exception> exceptions = base.MailboxData.Exceptions;
					base.MailboxData.ArchiveProcessor.MoveToArchiveDumpster(defaultFolderType, listToSend[defaultFolderType], base.ElcAssistant, base.MailboxData.FolderArchiver, base.MailboxData.TotalErrors, ref exceptions, out this.foldersWithErrors, out num);
					base.MailboxData.TotalErrors += num;
					this.foldersWithErrors.AddRange(collection);
				}
				this.LogBadFolders();
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00018D7C File Offset: 0x00016F7C
		private static int CompareItemData(ItemData x, ItemData y)
		{
			if (x.ParentId == null && y.ParentId == null)
			{
				return 0;
			}
			if (x.ParentId == null)
			{
				return -1;
			}
			return x.ParentId.CompareTo(y.ParentId);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00019008 File Offset: 0x00017208
		private static IEnumerable<TagExpirationExecutor.ItemSet> GetItemSets(List<ItemData> listToSend)
		{
			ItemData startOfChunk = null;
			List<ItemData> items = null;
			foreach (ItemData itemData in listToSend)
			{
				if (startOfChunk != null && TagExpirationExecutor.CompareItemData(startOfChunk, itemData) != 0)
				{
					TagExpirationExecutor.ItemSet itemSet = new TagExpirationExecutor.ItemSet(startOfChunk.ParentId, items);
					yield return itemSet;
					startOfChunk = null;
				}
				if (startOfChunk == null)
				{
					startOfChunk = itemData;
					items = new List<ItemData>();
				}
				items.Add(itemData);
			}
			if (startOfChunk != null)
			{
				TagExpirationExecutor.ItemSet itemSet2 = new TagExpirationExecutor.ItemSet(startOfChunk.ParentId, items);
				yield return itemSet2;
			}
			yield break;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00019028 File Offset: 0x00017228
		private void InternalExpireInBatches(List<ItemData> listToSend, ExpirationExecutor.Action retentionActionType, Folder sourcefolder, Folder targetFolder)
		{
			int count = listToSend.Count;
			int num = 0;
			int i = 0;
			int num2 = 0;
			long num3 = 0L;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			int num12 = 0;
			OperationResult operationResult = OperationResult.Succeeded;
			Exception ex = null;
			ItemData[] sourceArray = listToSend.ToArray();
			int num13 = 0;
			int num14 = 0;
			try
			{
				while (i < count)
				{
					base.ElcAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxData.MailboxSession.MailboxOwner);
					int num15 = (count - i >= 100) ? 100 : (count - i);
					switch (retentionActionType)
					{
					case ExpirationExecutor.Action.SoftDelete:
					case ExpirationExecutor.Action.PermanentlyDelete:
					{
						VersionedId[] array = new VersionedId[num15];
						num3 += (long)base.CopyIdsToTmpArray(sourceArray, i, array, num15);
						num2 = num15;
						Dictionary<ItemData.EnforcerType, int> numberOfItemsProcessedByEachEnforcer = ItemData.GetNumberOfItemsProcessedByEachEnforcer(sourceArray, i, num15);
						AggregateOperationResult aggregateOperationResult;
						if (retentionActionType == ExpirationExecutor.Action.SoftDelete)
						{
							num8 = (numberOfItemsProcessedByEachEnforcer.ContainsKey(ItemData.EnforcerType.ExpirationTagEnforcer) ? numberOfItemsProcessedByEachEnforcer[ItemData.EnforcerType.ExpirationTagEnforcer] : 0);
							aggregateOperationResult = base.MailboxData.MailboxSession.Delete(DeleteItemFlags.SoftDelete | DeleteItemFlags.SuppressReadReceipt, array);
						}
						else
						{
							num11 = (numberOfItemsProcessedByEachEnforcer.ContainsKey(ItemData.EnforcerType.DiscoveryHoldEnforcer) ? numberOfItemsProcessedByEachEnforcer[ItemData.EnforcerType.DiscoveryHoldEnforcer] : 0);
							num9 = (numberOfItemsProcessedByEachEnforcer.ContainsKey(ItemData.EnforcerType.DumpsterExpirationEnforcer) ? numberOfItemsProcessedByEachEnforcer[ItemData.EnforcerType.DumpsterExpirationEnforcer] : 0);
							num10 = (numberOfItemsProcessedByEachEnforcer.ContainsKey(ItemData.EnforcerType.DumpsterQuotaEnforcer) ? numberOfItemsProcessedByEachEnforcer[ItemData.EnforcerType.DumpsterQuotaEnforcer] : 0);
							num8 = (numberOfItemsProcessedByEachEnforcer.ContainsKey(ItemData.EnforcerType.ExpirationTagEnforcer) ? numberOfItemsProcessedByEachEnforcer[ItemData.EnforcerType.ExpirationTagEnforcer] : 0);
							aggregateOperationResult = base.MailboxData.MailboxSession.Delete(DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt, array);
						}
						operationResult = aggregateOperationResult.OperationResult;
						ex = ElcExceptionHelper.ExtractExceptionsFromAggregateOperationResult(aggregateOperationResult);
						break;
					}
					case ExpirationExecutor.Action.MoveToDiscoveryHolds:
					case ExpirationExecutor.Action.MoveToMigratedMessages:
					case ExpirationExecutor.Action.MoveToPurges:
					{
						VersionedId[] array2 = new VersionedId[num15];
						num3 += (long)base.CopyIdsToTmpArray(sourceArray, i, array2, num15);
						num2 = num15;
						Dictionary<ItemData.EnforcerType, int> numberOfItemsProcessedByEachEnforcer2 = ItemData.GetNumberOfItemsProcessedByEachEnforcer(sourceArray, i, num15);
						if (num2 > 0)
						{
							if (sourcefolder != null)
							{
								StoreObjectId destinationFolderId;
								if (ExpirationExecutor.Action.MoveToPurges == retentionActionType)
								{
									num13 = (numberOfItemsProcessedByEachEnforcer2.ContainsKey(ItemData.EnforcerType.DumpsterExpirationEnforcer) ? numberOfItemsProcessedByEachEnforcer2[ItemData.EnforcerType.DumpsterExpirationEnforcer] : 0);
									destinationFolderId = base.MailboxData.MailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsPurges);
								}
								else if (ExpirationExecutor.Action.MoveToDiscoveryHolds == retentionActionType)
								{
									num9 = (numberOfItemsProcessedByEachEnforcer2.ContainsKey(ItemData.EnforcerType.DumpsterExpirationEnforcer) ? numberOfItemsProcessedByEachEnforcer2[ItemData.EnforcerType.DumpsterExpirationEnforcer] : 0);
									num10 = (numberOfItemsProcessedByEachEnforcer2.ContainsKey(ItemData.EnforcerType.DumpsterQuotaEnforcer) ? numberOfItemsProcessedByEachEnforcer2[ItemData.EnforcerType.DumpsterQuotaEnforcer] : 0);
									destinationFolderId = base.MailboxData.MailboxSession.CowSession.CheckAndCreateDiscoveryHoldsFolder(base.MailboxData.MailboxSession);
								}
								else
								{
									destinationFolderId = base.MailboxData.MailboxSession.CowSession.CheckAndCreateMigratedMessagesFolder();
								}
								if (this.IsMoveClearNrnFlightingEnabled())
								{
									sourcefolder.ClearNotReadNotificationPending(array2);
								}
								GroupOperationResult groupOperationResult = sourcefolder.MoveItems(destinationFolderId, array2);
								if (groupOperationResult.OperationResult == OperationResult.Succeeded)
								{
									TagExpirationExecutor.Tracer.TraceDebug<TagExpirationExecutor, string, int>((long)this.GetHashCode(), "{0}: Moved to {1} batch of {2} items.", this, retentionActionType.ToString(), num2);
								}
								else
								{
									operationResult = groupOperationResult.OperationResult;
									ex = groupOperationResult.Exception;
								}
							}
						}
						else
						{
							TagExpirationExecutor.Tracer.TraceDebug<TagExpirationExecutor>((long)this.GetHashCode(), "{0}: The tmpList was empty during this loop. Nothing to send, don't do anything.", this);
						}
						break;
					}
					}
					i += num15;
					num += num2;
					if (operationResult == OperationResult.Failed || operationResult == OperationResult.PartiallySucceeded)
					{
						TagExpirationExecutor.Tracer.TraceError((long)this.GetHashCode(), "{0}: An error occured when trying to expire a batch of {1} items. Expiration action is {2}. Result: {3}", new object[]
						{
							this,
							num2,
							retentionActionType,
							operationResult
						});
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ExpirationOfCurrentBatchFailed, null, new object[]
						{
							base.MailboxData.MailboxSession.MailboxOwner,
							retentionActionType.ToString(),
							(sourcefolder == null) ? string.Empty : sourcefolder.DisplayName,
							(targetFolder == null) ? string.Empty : targetFolder.DisplayName,
							(sourcefolder == null) ? string.Empty : sourcefolder.Id.ObjectId.ToHexEntryId(),
							(targetFolder == null) ? string.Empty : targetFolder.Id.ObjectId.ToHexEntryId(),
							(ex == null) ? string.Empty : ex.ToString()
						});
						num12++;
						base.MailboxData.ThrowIfErrorsOverLimit(ex);
					}
					else
					{
						num7 += num11;
						num5 += num9;
						num6 += num10;
						num4 += num8;
						num14 += num13;
					}
				}
			}
			finally
			{
				ELCPerfmon.TotalItemsExpired.IncrementBy((long)num);
				ELCPerfmon.TotalSizeItemsExpired.IncrementBy(num3);
				switch (retentionActionType)
				{
				case ExpirationExecutor.Action.SoftDelete:
					ELCPerfmon.TotalItemsSoftDeleted.IncrementBy((long)num);
					ELCPerfmon.TotalSizeItemsSoftDeleted.IncrementBy(num3);
					break;
				case ExpirationExecutor.Action.PermanentlyDelete:
					ELCPerfmon.TotalItemsPermanentlyDeleted.IncrementBy((long)num);
					ELCPerfmon.TotalSizeItemsPermanentlyDeleted.IncrementBy(num3);
					break;
				case ExpirationExecutor.Action.MoveToDiscoveryHolds:
					ELCPerfmon.TotalItemsMovedToDiscoveryHolds.IncrementBy((long)num);
					ELCPerfmon.TotalSizeItemsMovedToDiscoveryHolds.IncrementBy(num3);
					break;
				}
				base.MailboxData.StatisticsLogEntry.NumberOfItemsActuallyDeletedByDiscoveryHoldEnforcer += (long)num7;
				base.MailboxData.StatisticsLogEntry.NumberOfItemsActuallyDeletedByDumpsterExpirationEnforcer += (long)num5;
				base.MailboxData.StatisticsLogEntry.NumberOfItemsActuallyDeletedByDumpsterQuotaEnforcer += (long)num6;
				base.MailboxData.StatisticsLogEntry.NumberOfItemsActuallyDeletedByTag += (long)num4;
				base.MailboxData.StatisticsLogEntry.NumberOfBatchesFailedToExpireInExpirationExecutor += (long)num12;
				base.MailboxData.StatisticsLogEntry.NumberOfItemsActuallyMovedToPurgesByDumpsterExpirationEnforcer += (long)num14;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000195B4 File Offset: 0x000177B4
		private void LogBadFolders()
		{
			if (this.foldersWithErrors != null && this.foldersWithErrors.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string str in this.foldersWithErrors)
				{
					stringBuilder.Append(str + "\r\n");
				}
				TagExpirationExecutor.Tracer.TraceDebug<TagExpirationExecutor, StringBuilder>((long)this.GetHashCode(), "{0}: List of folders with oversized items: {1}.", this, stringBuilder);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_FoldersWithOversizedItems, null, new object[]
				{
					base.MailboxData.MailboxSession.MailboxOwner,
					stringBuilder.ToString()
				});
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00019684 File Offset: 0x00017884
		private bool IsMoveClearNrnFlightingEnabled()
		{
			bool result = false;
			if (base.MailboxData.MailboxSession != null)
			{
				VariantConfigurationSnapshot configuration = base.MailboxData.MailboxSession.MailboxOwner.GetConfiguration();
				if (configuration != null)
				{
					result = configuration.Ipaed.MoveClearNrn.Enabled;
				}
			}
			return result;
		}

		// Token: 0x040002EA RID: 746
		private static readonly Trace Tracer = ExTraceGlobals.TagExpirationExecutorTracer;

		// Token: 0x040002EB RID: 747
		private string toString;

		// Token: 0x040002EC RID: 748
		private List<string> foldersWithErrors;

		// Token: 0x02000065 RID: 101
		internal class ItemSet
		{
			// Token: 0x06000395 RID: 917 RVA: 0x000196DA File Offset: 0x000178DA
			internal ItemSet(StoreObjectId folderId, List<ItemData> items)
			{
				this.folderId = folderId;
				this.items = items;
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000396 RID: 918 RVA: 0x000196F0 File Offset: 0x000178F0
			public StoreObjectId FolderId
			{
				get
				{
					return this.folderId;
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000397 RID: 919 RVA: 0x000196F8 File Offset: 0x000178F8
			public List<ItemData> Items
			{
				get
				{
					return this.items;
				}
			}

			// Token: 0x040002ED RID: 749
			private readonly StoreObjectId folderId;

			// Token: 0x040002EE RID: 750
			private readonly List<ItemData> items;
		}
	}
}
