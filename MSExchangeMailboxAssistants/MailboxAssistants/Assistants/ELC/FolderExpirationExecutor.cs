using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200005D RID: 93
	internal class FolderExpirationExecutor : ExpirationExecutor
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0001405C File Offset: 0x0001225C
		internal FolderExpirationExecutor(ProvisionedFolder provisionedFolder, MailboxDataForFolders mailboxData, ElcFolderSubAssistant elcAssistant) : base(mailboxData, elcAssistant, FolderExpirationExecutor.Tracer)
		{
			this.provisionedFolder = provisionedFolder;
			this.expiryTimeList = new List<ItemData>(2000);
			this.moveDateStampingList = new List<ItemData>(2000);
			this.moveLists = new Dictionary<Guid, MovePolicyItems>();
			ExpirationExecutor.TracerPfd.TracePfd<int, FolderExpirationExecutor>((long)this.GetHashCode(), "PFD IWE {0} {1} called", 30999, this);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000140C4 File Offset: 0x000122C4
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Concat(new string[]
				{
					"FolderExpiration executor for mailbox '",
					base.MailboxData.MailboxSession.MailboxOwner.ToString(),
					"' on folder '",
					(this.provisionedFolder == null) ? string.Empty : this.provisionedFolder.DisplayName,
					"'"
				});
			}
			return this.toString;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001413F File Offset: 0x0001233F
		internal void AddToReportAndDoomedList(object[] itemProperties, PropertyIndexHolder propertyIndexHolder, ContentSetting settings, ItemData itemData, string itemClass, Dictionary<Guid, string> allPolicyTags)
		{
			base.AddToReport(settings.RetentionAction, itemProperties, propertyIndexHolder, allPolicyTags);
			this.AddToDoomedList(itemData, settings, itemClass);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001415C File Offset: 0x0001235C
		private void AddToDoomedList(ItemData itemData, ContentSetting elcPolicy, string itemClass)
		{
			switch (elcPolicy.RetentionAction)
			{
			case RetentionActionType.MoveToDeletedItems:
			case RetentionActionType.MoveToFolder:
				if (!this.moveLists.ContainsKey(elcPolicy.Guid))
				{
					this.moveLists[elcPolicy.Guid] = new MovePolicyItems(elcPolicy, this.provisionedFolder, (MailboxDataForFolders)base.MailboxData, itemClass);
				}
				this.moveLists[elcPolicy.Guid].AddItemToDestinationList(itemData);
				this.CheckAndProcessItemsOnBatchSizeReached(this.moveLists[elcPolicy.Guid].ItemList);
				break;
			case RetentionActionType.MarkAsPastRetentionLimit:
				this.expiryTimeList.Add(itemData);
				this.CheckAndProcessItemsOnBatchSizeReached(this.expiryTimeList);
				break;
			}
			base.AddToDoomedList(itemData, elcPolicy);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00014222 File Offset: 0x00012422
		internal void AddToMoveDateStampingList(ItemData item)
		{
			this.moveDateStampingList.Add(item);
			this.CheckAndProcessItemsOnBatchSizeReached(this.moveDateStampingList);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001423C File Offset: 0x0001243C
		internal override void ExecuteTheDoomed()
		{
			ICollection values = this.moveLists.Values;
			if (values.Count > 0)
			{
				foreach (object obj in values)
				{
					MovePolicyItems movePolicyItems = (MovePolicyItems)obj;
					if (movePolicyItems.SetDateWhileMoving)
					{
						this.ExpireInBatches(movePolicyItems.ItemList, ExpirationExecutor.Action.MoveToFolderAndSet, new StoreObjectId[]
						{
							movePolicyItems.DestinationFolderId
						});
					}
					else
					{
						this.ExpireInBatches(movePolicyItems.ItemList, ExpirationExecutor.Action.MoveToFolder, new StoreObjectId[]
						{
							movePolicyItems.DestinationFolderId
						});
					}
					FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, int, StoreObjectId>((long)this.GetHashCode(), "{0}: {1} items moved to destination folder: '{2}'.", this, movePolicyItems.ItemList.Count, movePolicyItems.DestinationFolderId);
					ExpirationExecutor.TracerPfd.TracePfd((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items moved to destination folder: '{3}'.", new object[]
					{
						22807,
						this,
						movePolicyItems.ItemList.Count,
						movePolicyItems.DestinationFolderId
					});
				}
			}
			ItemUpdater itemUpdater = new ItemUpdater((MailboxDataForFolders)base.MailboxData, this.provisionedFolder, base.ElcAssistant);
			if (this.expiryTimeList.Count > 0)
			{
				int num = itemUpdater.SetProperty(this.expiryTimeList, MessageItemSchema.ExpiryTime, base.MailboxData.UtcNow);
				ELCPerfmon.TotalItemsTagged.IncrementBy((long)num);
				ELCPerfmon.TotalItemsExpired.IncrementBy((long)num);
				FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, int, int>((long)this.GetHashCode(), "{0}: {1} items in ExpiryTime tag list. Setting property completed on {2} items.", this, this.expiryTimeList.Count, num);
			}
			if (this.moveDateStampingList.Count > 0)
			{
				CompositeProperty compositeProperty = new CompositeProperty(Server.Exchange2007MajorVersion, base.MailboxData.Now);
				byte[] bytes = compositeProperty.GetBytes();
				itemUpdater.SetProperty(this.moveDateStampingList, ItemSchema.ElcMoveDate, bytes);
				FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items in MoveDate stamping list. Setting property completed.", this, this.expiryTimeList.Count);
			}
			base.ExecuteTheDoomed();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00014468 File Offset: 0x00012668
		protected override void ExpireInBatches(List<ItemData> listToSend, ExpirationExecutor.Action retentionActionType)
		{
			this.ExpireInBatches(listToSend, retentionActionType, null);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00014474 File Offset: 0x00012674
		protected override void ExpireInBatches(List<ItemData> listToSend, ExpirationExecutor.Action retentionActionType, params StoreObjectId[] destinationFolderIds)
		{
			int count = listToSend.Count;
			int i = 0;
			int num = 0;
			OperationResult operationResult = OperationResult.Succeeded;
			bool errorOccurred = false;
			DateTime dateTime = DateTime.MinValue;
			try
			{
				while (i < count)
				{
					base.ElcAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxData.MailboxSession.MailboxOwner);
					int num2 = (count - i >= 100) ? 100 : (count - i);
					VersionedId[] array = new VersionedId[num2];
					num += base.CopyIdsToTmpArray(listToSend.ToArray(), i, array, num2);
					i += num2;
					this.provisionedFolder.CurrentItems = array;
					switch (retentionActionType)
					{
					case ExpirationExecutor.Action.MoveToFolder:
					{
						GroupOperationResult groupOperationResult = this.provisionedFolder.Folder.MoveItems(destinationFolderIds[0], array);
						operationResult = groupOperationResult.OperationResult;
						break;
					}
					case ExpirationExecutor.Action.MoveToFolderAndSet:
					{
						object[] values = new object[]
						{
							new CompositeProperty(Server.Exchange2007MajorVersion, base.MailboxData.Now).GetBytes()
						};
						GroupOperationResult groupOperationResult2 = this.provisionedFolder.Folder.UnsafeMoveItemsAndSetProperties(destinationFolderIds[0], array, new PropertyDefinition[]
						{
							ItemSchema.ElcMoveDate
						}, values);
						operationResult = groupOperationResult2.OperationResult;
						break;
					}
					case ExpirationExecutor.Action.SoftDelete:
					{
						AggregateOperationResult aggregateOperationResult = this.provisionedFolder.Folder.DeleteObjects(DeleteItemFlags.SoftDelete, array);
						operationResult = aggregateOperationResult.OperationResult;
						break;
					}
					case ExpirationExecutor.Action.PermanentlyDelete:
					{
						AggregateOperationResult aggregateOperationResult2 = this.provisionedFolder.Folder.DeleteObjects(DeleteItemFlags.HardDelete, array);
						operationResult = aggregateOperationResult2.OperationResult;
						break;
					}
					}
					if (operationResult == OperationResult.Failed || operationResult == OperationResult.PartiallySucceeded)
					{
						FolderExpirationExecutor.Tracer.TraceError((long)this.GetHashCode(), "{0}: An error occured when trying to expire a batch of {1} items. Expiration action is {2}. Result: {3}", new object[]
						{
							this,
							num2,
							retentionActionType,
							operationResult
						});
						errorOccurred = true;
						dateTime = ((dateTime == DateTime.MinValue) ? listToSend[i - num2].MessageReceivedDate : dateTime);
						base.MailboxData.ThrowIfErrorsOverLimit();
					}
				}
				int num3 = this.ProcessItemsSuccessfullyExpired(listToSend, errorOccurred, dateTime);
				ELCPerfmon.TotalItemsExpired.IncrementBy((long)num3);
				ELCPerfmon.TotalSizeItemsExpired.IncrementBy((long)num);
				switch (retentionActionType)
				{
				case ExpirationExecutor.Action.MoveToFolder:
				case ExpirationExecutor.Action.MoveToFolderAndSet:
					ELCPerfmon.TotalItemsMoved.IncrementBy((long)num3);
					ELCPerfmon.TotalSizeItemsMoved.IncrementBy((long)num);
					break;
				case ExpirationExecutor.Action.SoftDelete:
					ELCPerfmon.TotalItemsSoftDeleted.IncrementBy((long)num3);
					ELCPerfmon.TotalSizeItemsSoftDeleted.IncrementBy((long)num);
					break;
				case ExpirationExecutor.Action.PermanentlyDelete:
					ELCPerfmon.TotalItemsPermanentlyDeleted.IncrementBy((long)num3);
					ELCPerfmon.TotalSizeItemsPermanentlyDeleted.IncrementBy((long)num);
					break;
				}
			}
			catch (ObjectNotFoundException arg)
			{
				FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, ExpirationExecutor.Action, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: Either the source or the destination folder (if applicable) was not found. Skipping current action '{1}' for this folder. Exception: '{2}'", this, retentionActionType, arg);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00014734 File Offset: 0x00012934
		protected override void ClearProcessedItemList()
		{
			this.expiryTimeList.Clear();
			this.expiryTimeList.TrimExcess();
			this.moveDateStampingList.Clear();
			this.moveDateStampingList.TrimExcess();
			foreach (MovePolicyItems movePolicyItems in this.moveLists.Values)
			{
				movePolicyItems.ItemList.Clear();
				movePolicyItems.ItemList.TrimExcess();
			}
			base.ClearProcessedItemList();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000147D0 File Offset: 0x000129D0
		private int ProcessItemsSuccessfullyExpired(List<ItemData> originalItems, bool errorOccurred, DateTime messageDateInFirstErrorBatch)
		{
			if (!errorOccurred)
			{
				for (int i = 0; i < originalItems.Count; i++)
				{
					if (originalItems[i].ItemAuditLogData != null)
					{
						string nonLocalizedAssistantName = (base.ElcAssistant != null) ? base.ElcAssistant.ElcAssistantType.ToString() : "<null>";
						((MailboxDataForFolders)base.MailboxData).ElcAuditLog.Append(originalItems[i], nonLocalizedAssistantName);
					}
				}
				FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor>((long)this.GetHashCode(), "{0}: Expiration of all items in this folder succeeded, so logged everything.", this);
				return originalItems.Count;
			}
			FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor>((long)this.GetHashCode(), "{0}: Expiration of some items in this folder failed. Going to determine which ones succeeded.", this);
			int num = 0;
			List<object[]> list = null;
			try
			{
				using (QueryResult queryResult = this.provisionedFolder.Folder.ItemQuery(ItemQueryType.None, null, FolderExpirationExecutor.SortColumns, FolderExpirationExecutor.DataColumns))
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ReceivedTime, messageDateInFirstErrorBatch);
					queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
					list = new List<object[]>();
					for (;;)
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows.Length <= 0)
						{
							break;
						}
						list.AddRange(rows);
					}
				}
			}
			catch (InvalidFolderLanguageIdException arg)
			{
				FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, InvalidFolderLanguageIdException>((long)this.GetHashCode(), "{0}: This folder could not be opened. Audit logging for items expired in this folder will be skipped as we have no way of knowing which items were expired. Exception: {1}", this, arg);
				return 0;
			}
			int num2 = 0;
			int num3 = 0;
			while (num2 < originalItems.Count && num3 < list.Count)
			{
				if (list[num3][1] is ExDateTime && originalItems[num2].MessageReceivedDate != DateTime.MinValue)
				{
					DateTime dateTime = (DateTime)((ExDateTime)list[num3][1]);
					if (originalItems[num2].MessageReceivedDate > dateTime)
					{
						if (originalItems[num2].ItemAuditLogData != null)
						{
							string nonLocalizedAssistantName2 = (base.ElcAssistant != null) ? base.ElcAssistant.ElcAssistantType.ToString() : "<null>";
							((MailboxDataForFolders)base.MailboxData).ElcAuditLog.Append(originalItems[num2], nonLocalizedAssistantName2);
						}
						num++;
						num2++;
					}
					else if (originalItems[num2].MessageReceivedDate < dateTime)
					{
						num3++;
					}
					else if (originalItems[num2].MessageReceivedDate == dateTime)
					{
						VersionedId versionedId = list[num3][0] as VersionedId;
						if (versionedId == null)
						{
							FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, StoreObjectId>((long)this.GetHashCode(), "{0}: The 2 items have the same received date, but the remaining item does not have a VersionedId. Skipping original item. Id: {1}.", this, originalItems[num2].Id.ObjectId);
							num2++;
						}
						else if (originalItems[num2].Id.ObjectId.Equals(versionedId.ObjectId))
						{
							FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, StoreObjectId>((long)this.GetHashCode(), "{0}: The item {1} still exists. Skip logging it.", this, originalItems[num2].Id.ObjectId);
							num2++;
							num3++;
						}
						else
						{
							num3++;
						}
					}
				}
				else
				{
					FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor>((long)this.GetHashCode(), "{0}: ReceivedDate is missing on one of the items. We'll scan the entire list of remaining items to check if original item exists.", this);
					bool flag = false;
					for (int j = 0; j < list.Count; j++)
					{
						VersionedId versionedId2 = list[j][0] as VersionedId;
						if (versionedId2 == null)
						{
							FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, StoreObjectId>((long)this.GetHashCode(), "{0}: Item does not have a VersionedId. Skipping original item. Id: {1}.", this, originalItems[num2].Id.ObjectId);
							flag = true;
							break;
						}
						if (originalItems[num2].Id.ObjectId.Equals(versionedId2.ObjectId))
						{
							FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, StoreObjectId>((long)this.GetHashCode(), "{0}: The item {1} still exists. Skip logging it.", this, originalItems[num2].Id.ObjectId);
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						if (originalItems[num2].ItemAuditLogData != null)
						{
							string nonLocalizedAssistantName3 = (base.ElcAssistant != null) ? base.ElcAssistant.ElcAssistantType.ToString() : "<null>";
							((MailboxDataForFolders)base.MailboxData).ElcAuditLog.Append(originalItems[num2], nonLocalizedAssistantName3);
						}
						num++;
					}
					num2++;
				}
			}
			if (num2 != originalItems.Count)
			{
				for (int k = num2; k < originalItems.Count; k++)
				{
					if (originalItems[num2].ItemAuditLogData != null)
					{
						string nonLocalizedAssistantName4 = (base.ElcAssistant != null) ? base.ElcAssistant.ElcAssistantType.ToString() : "<null>";
						((MailboxDataForFolders)base.MailboxData).ElcAuditLog.Append(originalItems[num2], nonLocalizedAssistantName4);
					}
					num++;
				}
			}
			FolderExpirationExecutor.Tracer.TraceDebug<FolderExpirationExecutor, int, int>((long)this.GetHashCode(), "{0}: {1} out of {2} were successfully expired.", this, num, originalItems.Count);
			return num;
		}

		// Token: 0x040002B2 RID: 690
		private const int IdIndex = 0;

		// Token: 0x040002B3 RID: 691
		private const int ReceivedDateIndex = 1;

		// Token: 0x040002B4 RID: 692
		private static readonly Trace Tracer = ExTraceGlobals.ExpirationEnforcerTracer;

		// Token: 0x040002B5 RID: 693
		private static readonly SortBy[] SortColumns = new SortBy[]
		{
			new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
		};

		// Token: 0x040002B6 RID: 694
		private static readonly PropertyDefinition[] DataColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ReceivedTime
		};

		// Token: 0x040002B7 RID: 695
		private ProvisionedFolder provisionedFolder;

		// Token: 0x040002B8 RID: 696
		private List<ItemData> expiryTimeList;

		// Token: 0x040002B9 RID: 697
		private List<ItemData> moveDateStampingList;

		// Token: 0x040002BA RID: 698
		private Dictionary<Guid, MovePolicyItems> moveLists;

		// Token: 0x040002BB RID: 699
		private string toString;
	}
}
