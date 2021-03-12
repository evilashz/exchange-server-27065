using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000096 RID: 150
	internal class DiscoveryHoldEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x0002B94E File Offset: 0x00029B4E
		internal DiscoveryHoldEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
			this.AllInPlaceHoldConfiguration = base.MailboxDataForTags.ElcUserInformation.InPlaceHoldConfiguration;
			if (this.AllInPlaceHoldConfiguration == null)
			{
				this.AllInPlaceHoldConfiguration = new List<InPlaceHoldConfiguration>();
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0002B981 File Offset: 0x00029B81
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + base.MailboxDataForTags.MailboxSession.MailboxOwner.ToString() + " being processed by DiscoveryHoldEnforcer.";
			}
			return this.toString;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0002B9BC File Offset: 0x00029BBC
		protected override bool QueryIsEnabled()
		{
			if (this.isEnabled != null)
			{
				return this.isEnabled.Value;
			}
			this.discoveryHoldsFolderId = this.GetDiscoveryHoldFolderId();
			if (base.MailboxDataForTags.AbsoluteLitigationHoldEnabled)
			{
				DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer>((long)this.GetHashCode(), "{0}: This user is under litigation hold. This user's dumpster will be skipped for discovery hold processing.", this);
				this.isEnabled = new bool?(false);
			}
			else if (base.MailboxDataForTags.ElcUserInformation.InPlaceHoldConfiguration == null)
			{
				DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer>((long)this.GetHashCode(), "{0}: there was something wrong when retrieving or validating the in-place hold configuration, skipping the discovery hold processing.", this);
				this.isEnabled = new bool?(false);
			}
			else if (this.discoveryHoldsFolderId == null)
			{
				DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer>((long)this.GetHashCode(), "{0}: This user has no discovery hold folder. This mailbox will be skipped for discovery hold processing.", this);
				this.isEnabled = new bool?(false);
			}
			else if (this.GetUpdateItemCountInDHFolder() <= 0)
			{
				DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0}:{1} Folder is Empty", this, this.discoveryHoldsFolderId.ToHexEntryId());
				this.isEnabled = new bool?(false);
			}
			else
			{
				DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer>((long)this.GetHashCode(), "{0}: This user is not under litigation hold and may have discovery hold queries defined. The dumpster will be processed for discovery holds.", this);
				this.isEnabled = new bool?(true);
			}
			return this.isEnabled.Value;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0002BAF1 File Offset: 0x00029CF1
		protected override void InvokeInternal()
		{
			DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer>((long)this.GetHashCode(), "{0}: Processing the mailbox for discovery hold queries.", this);
			base.InvokeInternal();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0002BB10 File Offset: 0x00029D10
		private StoreObjectId GetDiscoveryHoldFolderId()
		{
			return base.MailboxDataForTags.MailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDiscoveryHolds);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002BB24 File Offset: 0x00029D24
		private int GetUpdateItemCountInDHFolder()
		{
			this.GetFolderStats(this.discoveryHoldsFolderId, out this.beforeFolderSize, out this.beforeFolderCount);
			this.itemsInDiscoveryHoldsFolder += this.beforeFolderCount;
			return this.itemsInDiscoveryHoldsFolder;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002BB58 File Offset: 0x00029D58
		private bool IsOnLitigationHoldWithDuration()
		{
			return base.MailboxDataForTags.LitigationHoldEnabled && base.MailboxDataForTags.LitigationHoldDuration != null && base.MailboxDataForTags.LitigationHoldDuration.Value != Unlimited<EnhancedTimeSpan>.UnlimitedValue;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002BBA8 File Offset: 0x00029DA8
		protected override void CollectItemsToExpire()
		{
			if ((this.AllInPlaceHoldConfiguration != null && this.AllInPlaceHoldConfiguration.Count > 0) || this.IsOnLitigationHoldWithDuration())
			{
				this.ProcessFolderContents();
				return;
			}
			DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0}: Delete discovery holds folder and all messages in {1}.", this, this.discoveryHoldsFolderId.ToHexEntryId());
			this.itemsExpired = this.itemsInDiscoveryHoldsFolder;
			this.sizeOfExpiredItems = this.beforeFolderSize;
			GroupOperationResult groupOperationResult = base.MailboxDataForTags.MailboxSession.DeleteAllObjects(DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt, this.discoveryHoldsFolderId);
			if (groupOperationResult.OperationResult == OperationResult.Succeeded)
			{
				this.itemsActuallyExpired = this.itemsInDiscoveryHoldsFolder;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002BC48 File Offset: 0x00029E48
		private StoreId ConstructAndSubmitHoldSearchQuery()
		{
			QueryFilter searchCriteriaFilter = this.ConstructSearchFilter();
			return this.SubmitSearch(searchCriteriaFilter);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0002BC64 File Offset: 0x00029E64
		private StoreId SubmitSearch(QueryFilter searchCriteriaFilter)
		{
			StoreId result = null;
			MailboxSession mailboxSession = base.MailboxDataForTags.MailboxSession;
			if (searchCriteriaFilter != null)
			{
				if (!mailboxSession.Mailbox.IsContentIndexingEnabled)
				{
					DiscoveryHoldEnforcer.Tracer.TraceError<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0}: CI search failed on the mailbox {1}. CI is not enabled on the mailbox database", this, mailboxSession.ToString());
					throw new DiscoveryHoldSearchException(Strings.ErrorDiscoveryHoldsCIIndexDisabledOnDatabase(searchCriteriaFilter.ToString(), mailboxSession.ToString()));
				}
				DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0} : Creating search folder for hold queries for mailbox {1}", this, mailboxSession.ToString());
				using (SearchFolder searchFolder = SearchFolder.Create(mailboxSession, this.discoveryHoldsFolderId, DiscoveryHoldEnforcer.SearchDiscoveryHoldsFolderName, CreateMode.OpenIfExists))
				{
					searchFolder.Save();
					searchFolder.Load();
					result = searchFolder.Id;
					QueryFilter searchQuery = new CountFilter(10000U, searchCriteriaFilter);
					SearchFolderCriteria searchFolderCriteria = new SearchFolderCriteria(searchQuery, new StoreId[]
					{
						this.discoveryHoldsFolderId
					});
					searchFolderCriteria.DeepTraversal = true;
					searchFolderCriteria.UseCiForComplexQueries = true;
					searchFolderCriteria.FailNonContentIndexedSearch = true;
					int num = 0;
					bool flag = true;
					while (flag)
					{
						DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string, int>((long)this.GetHashCode(), "{0} : Begin search folder execution for mailbox {1}. Attempt#: {2}", this, mailboxSession.ToString(), num);
						IAsyncResult asyncResult = searchFolder.BeginApplyOneTimeSearch(searchFolderCriteria, null, null);
						bool flag2 = asyncResult.AsyncWaitHandle.WaitOne(60000, false);
						if (flag2)
						{
							DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0} : Search folder execution completed for mailbox {1}", this, mailboxSession.ToString());
							searchFolder.EndApplyOneTimeSearch(asyncResult);
							SearchFolderCriteria searchCriteria = searchFolder.GetSearchCriteria();
							if ((searchCriteria.SearchState & SearchState.FailNonContentIndexedSearch) == SearchState.FailNonContentIndexedSearch && (searchCriteria.SearchState & SearchState.Failed) == SearchState.Failed)
							{
								DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0} :  Discovery Hold search failed because CI is not running, search query was not served by CI for mailbox {1}", this, mailboxSession.ToString());
								throw new DiscoveryHoldSearchException(Strings.ErrorDiscoveryHoldsCIIndexNotRunning(searchCriteriaFilter.ToString(), mailboxSession.ToString()));
							}
							flag = false;
						}
						else
						{
							DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0} :  Discovery Hold Search folder execution has timed out for mailbox {1}", this, mailboxSession.ToString());
							if (num >= 3)
							{
								DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string, int>((long)this.GetHashCode(), "{0} :  Discovery Hold Search folder execution has timed out for mailbox {1} and exceeded max retry count {2}. Give up.", this, mailboxSession.ToString(), 3);
								throw new DiscoveryHoldSearchException(Strings.ErrorDiscoverySearchTimeout(60.ToString(), searchCriteriaFilter.ToString(), mailboxSession.ToString()));
							}
							flag = true;
							num++;
						}
					}
					searchFolder.Save();
					searchFolder.Load();
				}
			}
			return result;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		private QueryFilter ConstructSearchFilter()
		{
			List<QueryFilter> list = new List<QueryFilter>();
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.SearchIsPartiallyIndexed, true);
			foreach (InPlaceHoldConfiguration inPlaceHoldConfiguration in this.AllInPlaceHoldConfiguration)
			{
				if (inPlaceHoldConfiguration.QueryFilter != null)
				{
					list.Add(inPlaceHoldConfiguration.QueryFilter);
				}
			}
			if (this.IsOnLitigationHoldWithDuration())
			{
				QueryFilter retentionPeriodFilter = MailboxDiscoverySearch.GetRetentionPeriodFilter(base.MailboxDataForTags.LitigationHoldDuration.Value.Value);
				list.Add(retentionPeriodFilter);
			}
			QueryFilter result = null;
			if (list.Count > 0)
			{
				if (list.Count == 1)
				{
					result = QueryFilter.NotFilter(new OrFilter(new QueryFilter[]
					{
						list[0],
						queryFilter
					}));
				}
				else
				{
					list.Add(queryFilter);
					result = QueryFilter.NotFilter(QueryFilter.OrTogether(list.ToArray()));
				}
			}
			return result;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0002BFD8 File Offset: 0x0002A1D8
		private void DeleteObjectsInFolder(SearchFolder searchFolder)
		{
			GroupOperationResult groupOperationResult = searchFolder.DeleteAllObjects(DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt, true);
			OperationResult operationResult = groupOperationResult.OperationResult;
			Exception exception = groupOperationResult.Exception;
			if (operationResult == OperationResult.Failed || operationResult == OperationResult.PartiallySucceeded)
			{
				DiscoveryHoldEnforcer.Tracer.TraceError<DiscoveryHoldEnforcer, string, OperationResult>((long)this.GetHashCode(), "{0}: An error occured when trying to hard delete all messages in {1}. Operation Result: {2}", this, searchFolder.Id.ToString(), operationResult);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ExpirationOfMsgsInDiscoveryHoldsFolderFailed, null, new object[]
				{
					base.MailboxDataForTags.MailboxSession.MailboxOwner,
					DeleteItemFlags.HardDelete.ToString(),
					(searchFolder.Id == null) ? string.Empty : searchFolder.Id.ToString(),
					(exception == null) ? string.Empty : exception.ToString()
				});
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0002C098 File Offset: 0x0002A298
		private void ProcessFolderContents()
		{
			DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0}: Start processing discovery holds folder {1}", this, this.discoveryHoldsFolderId.ToHexEntryId());
			try
			{
				StoreId searchFolderId = this.ConstructAndSubmitHoldSearchQuery();
				this.ProcessFolderContents(base.MailboxDataForTags.MailboxSession, searchFolderId);
			}
			catch (StorageTransientException arg)
			{
				DiscoveryHoldEnforcer.Tracer.TraceError<DiscoveryHoldEnforcer, IExchangePrincipal, StorageTransientException>((long)this.GetHashCode(), "{0}: Discovery Holds Enforcer is unable to process mailbox {1} due to a storage transient error. Exception: {2}", this, base.MailboxDataForTags.MailboxSession.MailboxOwner, arg);
				throw;
			}
			catch (StoragePermanentException ex)
			{
				DiscoveryHoldEnforcer.Tracer.TraceError<DiscoveryHoldEnforcer, IExchangePrincipal, StoragePermanentException>((long)this.GetHashCode(), "{0}: Discovery Holds Enforcer is unable to process mailbox {1} due to a storage permanent error. Skipping this mailbox. Exception: {2}", this, base.MailboxDataForTags.MailboxSession.MailboxOwner, ex);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryHoldPermanentErrorSkipMailbox, null, new object[]
				{
					base.MailboxDataForTags.MailboxSession.MailboxOwner,
					ex.Message
				});
				throw new SkipException(ex);
			}
			catch (DiscoveryHoldSearchException ex2)
			{
				DiscoveryHoldEnforcer.Tracer.TraceError<DiscoveryHoldEnforcer, IExchangePrincipal, DiscoveryHoldSearchException>((long)this.GetHashCode(), "{0}: Discovery Holds Search failed on mailbox {1}. Exception: {2}", this, base.MailboxDataForTags.MailboxSession.MailboxOwner, ex2);
				base.MailboxDataForTags.StatisticsLogEntry.ExceptionType = ((ex2.InnerException != null) ? ex2.InnerException.GetType().ToString() : ex2.GetType().ToString());
				base.MailboxDataForTags.StatisticsLogEntry.AddExceptionToLog(ex2);
				ELCAssistant.PublishMonitoringResult(base.MailboxDataForTags.MailboxSession, ex2, ELCAssistant.NotificationType.Permanent, null);
				base.SysCleanupSubAssistant.ElcAssistantType.PerfCountersWrapper.Increment(ELCPerfmon.NumberOfDiscoveryHoldSearchExceptions, 1L);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0002C244 File Offset: 0x0002A444
		private void ProcessFolderContents(MailboxSession mailboxSession, StoreId searchFolderId)
		{
			bool flag = false;
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, searchFolderId))
			{
				if (folder.ItemCount > 0)
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, DiscoveryHoldEnforcer.ItemDataColumns))
					{
						while (!flag)
						{
							object[][] rows = queryResult.GetRows(100);
							if (rows.Length <= 0)
							{
								break;
							}
							foreach (object[] itemProperties in rows)
							{
								if (!this.EnlistItem(itemProperties))
								{
									flag = true;
									DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, bool>((long)this.GetHashCode(), "{0}: EnlistItem returned false. theEnd={1}.", this, flag);
									break;
								}
							}
							base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(mailboxSession.MailboxOwner);
						}
						goto IL_C0;
					}
				}
				DiscoveryHoldEnforcer.Tracer.TraceDebug<DiscoveryHoldEnforcer, string>((long)this.GetHashCode(), "{0}:{1} Search Folder is Empty, nothing to expire", this, searchFolderId.ToString());
				IL_C0:;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0002C33C File Offset: 0x0002A53C
		private bool EnlistItem(object[] itemProperties)
		{
			VersionedId versionedId = itemProperties[0] as VersionedId;
			if (versionedId == null)
			{
				DiscoveryHoldEnforcer.Tracer.TraceError<DiscoveryHoldEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return true;
			}
			object obj = itemProperties[1];
			if (obj == null)
			{
				DiscoveryHoldEnforcer.Tracer.TraceError<DiscoveryHoldEnforcer>((long)this.GetHashCode(), "{0}: We could not get size of this item. Skipping it.", this);
				return true;
			}
			int num = (int)obj;
			ItemData itemData = new ItemData(versionedId, ItemData.EnforcerType.DiscoveryHoldEnforcer, num);
			base.TagExpirationExecutor.AddToDoomedHardDeleteList(itemData, false);
			this.itemsExpired++;
			this.sizeOfExpiredItems += (long)num;
			return true;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0002C3CC File Offset: 0x0002A5CC
		private void GetFolderStats(StoreId folderId, out long folderSize, out int itemCount)
		{
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, folderId, new PropertyDefinition[]
			{
				FolderSchema.ExtendedSize
			}))
			{
				long? num = (long?)folder[FolderSchema.ExtendedSize];
				folderSize = ((num != null) ? num.Value : 0L);
				itemCount = folder.ItemCount;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002C448 File Offset: 0x0002A648
		protected override void StartPerfCounterCollect()
		{
			this.itemsInDiscoveryHoldsFolder = 0;
			this.itemsExpired = 0;
			this.sizeOfExpiredItems = 0L;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0002C460 File Offset: 0x0002A660
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			if (this.isEnabled != null)
			{
				if (!this.isEnabled.Value)
				{
					ELCPerfmon.TotalSkippedDumpsters.Increment();
				}
				ELCPerfmon.TotalExpiredDumpsterItems.IncrementBy((long)this.itemsExpired);
				base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedByDiscoveryHoldEnforcer += (long)this.itemsExpired;
				base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsInDiscoveryHoldFolderBeforeProcessing += (long)this.beforeFolderCount;
				base.MailboxDataForTags.StatisticsLogEntry.SizeOfDeletionByDiscoveryHoldEnforcer += this.sizeOfExpiredItems;
				base.MailboxDataForTags.StatisticsLogEntry.DiscoveryHoldFolderSizeBeforeProcessing += this.beforeFolderSize;
				base.MailboxDataForTags.StatisticsLogEntry.DiscoveryHoldEnforcerProcessingTime = timeElapsed;
				base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsActuallyDeletedByDiscoveryHoldEnforcer += (long)this.itemsActuallyExpired;
			}
		}

		// Token: 0x04000444 RID: 1092
		private const int ItemIdIndex = 0;

		// Token: 0x04000445 RID: 1093
		private const int ItemSizeIndex = 1;

		// Token: 0x04000446 RID: 1094
		private const int MaxRetry = 3;

		// Token: 0x04000447 RID: 1095
		private const int SearchTimeoutSeconds = 60;

		// Token: 0x04000448 RID: 1096
		private static readonly Trace Tracer = ExTraceGlobals.DiscoveryHoldEnforcerTracer;

		// Token: 0x04000449 RID: 1097
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400044A RID: 1098
		private static readonly string SearchDiscoveryHoldsFolderName = "SearchDiscoveryHoldsFolder";

		// Token: 0x0400044B RID: 1099
		private static readonly PropertyDefinition[] ItemDataColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Size
		};

		// Token: 0x0400044C RID: 1100
		private List<InPlaceHoldConfiguration> AllInPlaceHoldConfiguration;

		// Token: 0x0400044D RID: 1101
		private StoreObjectId discoveryHoldsFolderId;

		// Token: 0x0400044E RID: 1102
		private long beforeFolderSize;

		// Token: 0x0400044F RID: 1103
		private int beforeFolderCount;

		// Token: 0x04000450 RID: 1104
		private string toString;

		// Token: 0x04000451 RID: 1105
		private bool? isEnabled;

		// Token: 0x04000452 RID: 1106
		private int itemsInDiscoveryHoldsFolder;

		// Token: 0x04000453 RID: 1107
		private int itemsExpired;

		// Token: 0x04000454 RID: 1108
		private long sizeOfExpiredItems;

		// Token: 0x04000455 RID: 1109
		private int itemsActuallyExpired;
	}
}
