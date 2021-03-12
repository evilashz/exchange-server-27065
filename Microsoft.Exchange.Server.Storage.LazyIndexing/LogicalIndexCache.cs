using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LazyIndexing;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000025 RID: 37
	public class LogicalIndexCache : LockableMailboxComponent, IComponentData
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x000101D8 File Offset: 0x0000E3D8
		internal LogicalIndexCache(Context context, MailboxState mailboxState)
		{
			this.mailboxLockName = mailboxState;
			if (LogicalIndexCache.folderCachePerfCounters == null)
			{
				StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(context.Database);
				LogicalIndexCache.folderCachePerfCounters = new CachePerformanceCounters<StorePerDatabasePerformanceCountersInstance>(() => databaseInstance, (StorePerDatabasePerformanceCountersInstance instance) => instance.LogicalIndexSize, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfLogicalIndexLookups, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfLogicalIndexMisses, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfLogicalIndexHits, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfLogicalIndexInserts, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfLogicalIndexDeletes, (StorePerDatabasePerformanceCountersInstance instance) => instance.SizeOfLogicalIndexExpirationQueue);
			}
			this.folderIdToFolderIndexCache = new SingleKeyCache<ExchangeId, LogicalIndexCache.FolderIndexCache>(new LRU2WithTimeToLiveExpirationPolicy<ExchangeId>(LogicalIndexCache.NumberOfCachedFoldersPerMailbox, LogicalIndexCache.TimeToLive, false), LogicalIndexCache.folderCachePerfCounters);
			IMailboxContext mailboxContext = context.GetMailboxContext(mailboxState.MailboxNumber);
			if (mailboxContext.GetCreatedByMove(context))
			{
				this.updateIndexDirectly = true;
			}
			this.mailboxCreationTime = mailboxContext.GetCreationTime(context);
			this.InitializeCache(context);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00010345 File Offset: 0x0000E545
		public MailboxLockNameBase MailboxLockName
		{
			get
			{
				return this.mailboxLockName;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0001034D File Offset: 0x0000E54D
		public long EstimatedOldestMaintenanceRecord
		{
			get
			{
				return this.estimatedOldestMaintenanceRecord;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00010355 File Offset: 0x0000E555
		public long EstimatedNewestMaintenanceRecord
		{
			get
			{
				return this.estimatedNewestMaintenanceRecord;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0001035D File Offset: 0x0000E55D
		public override MailboxComponentId MailboxComponentId
		{
			get
			{
				return MailboxComponentId.LogicalIndexCache;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00010360 File Offset: 0x0000E560
		public override Guid DatabaseGuid
		{
			get
			{
				return this.mailboxLockName.DatabaseGuid;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0001036D File Offset: 0x0000E56D
		public override int MailboxPartitionNumber
		{
			get
			{
				return this.mailboxLockName.MailboxPartitionNumber;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0001037A File Offset: 0x0000E57A
		public override LockManager.LockType ReaderLockType
		{
			get
			{
				return LockManager.LockType.LogicalIndexCacheShared;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0001037E File Offset: 0x0000E57E
		public override LockManager.LockType WriterLockType
		{
			get
			{
				return LockManager.LockType.LogicalIndexCacheExclusive;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00010382 File Offset: 0x0000E582
		internal static LogicalIndexCache.ApplyMaintenanceSettings ApplyMaintenanceParameters
		{
			get
			{
				return LogicalIndexCache.applyMaintenanceParameters.Value;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001038E File Offset: 0x0000E58E
		internal static IDisposable SetLogicalIndexCleanupChunkSizeForTest(int chunkSize)
		{
			return LogicalIndexCache.logicalIndexCleanupChunkSize.SetTestHook(chunkSize);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001039B File Offset: 0x0000E59B
		internal static IDisposable SetMarkMailboxForLogicalIndexCleanupTestHook(Func<MailboxState, bool, bool> testDelegate)
		{
			return LogicalIndexCache.markMailboxForLogicalIndexCleanupTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000103A8 File Offset: 0x0000E5A8
		internal static IDisposable SetForceMailboxLogicalIndexCleanupTestHook(Func<MailboxState, bool, bool> testDelegate)
		{
			return LogicalIndexCache.forceMailboxLogicalIndexCleanupTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000103B5 File Offset: 0x0000E5B5
		internal static IDisposable SetCleanupOneLogicalIndexTestHook(Action<MailboxState, LogicalIndex, bool> testDelegate)
		{
			return LogicalIndexCache.cleanupOneLogicalIndexTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000103C2 File Offset: 0x0000E5C2
		internal bool UpdateIndexDirectly
		{
			get
			{
				return this.updateIndexDirectly;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000103CA File Offset: 0x0000E5CA
		internal SingleKeyCache<ExchangeId, LogicalIndexCache.FolderIndexCache> FolderIdToFolderIndexCacheForTest
		{
			get
			{
				return this.folderIdToFolderIndexCache;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000103FC File Offset: 0x0000E5FC
		public static void Initialize()
		{
			LogicalIndexCache.NumberOfCachedFoldersPerMailbox = ConfigurationSchema.LogicalIndexCacheSize.Value;
			LogicalIndexCache.TimeToLive = ConfigurationSchema.LogicalIndexCacheTimeToLive.Value;
			LogicalIndexCache.maxIdleCleanupPeriod = ConfigurationSchema.MaxIdleCleanupPeriod.Value;
			LogicalIndexCache.applyMaintenanceParameters = Hookable<LogicalIndexCache.ApplyMaintenanceSettings>.Create(false, new LogicalIndexCache.ApplyMaintenanceSettings
			{
				StopMaintenanceThreshold = ConfigurationSchema.StopMaintenanceThreshold.Value,
				WlmMaintenanceThreshold = ConfigurationSchema.WlmMaintenanceThreshold.Value,
				NumberOfRecordsToMaintain = ConfigurationSchema.NumberOfRecordsToMaintain.Value,
				NumberOfRecordsToReadFromMaintenanceTable = ConfigurationSchema.NumberOfRecordsToReadFromMaintenanceTable.Value,
				MaintenanceTimePeriodToKeep = ConfigurationSchema.MaintenanceTimePeriodToKeep.Value,
				WlmMinNumberOfChunksToProceed = ConfigurationSchema.WlmMinNumberOfChunksToProceed.Value
			});
			if (LogicalIndexCache.logicalIndexCacheDataSlot == -1)
			{
				LogicalIndexCache.logicalIndexCacheDataSlot = MailboxState.AllocateComponentDataSlot(false);
				LogicalIndexCache.logicalIndexCleanupMaintenance = MaintenanceHandler.RegisterMailboxMaintenance(LogicalIndexCache.LogicalIndexCleanupMaintenanceId, RequiredMaintenanceResourceType.Store, false, new MaintenanceHandler.MailboxMaintenanceDelegate(LogicalIndexCache.CleanupLogicalIndexes), "LogicalIndexCache.CleanupLogicalIndexes");
				LogicalIndexCache.markLogicalIndexForCleanupMaintenance = MaintenanceHandler.RegisterDatabaseMaintenance(LogicalIndexCache.MarkLogicalIndexForCleanupMaintenanceId, RequiredMaintenanceResourceType.Store, new MaintenanceHandler.DatabaseMaintenanceDelegate(LogicalIndexCache.MarkLogicalIndicesForCleanup), "LogicalIndexCache.MarkLogicalIndicesForCleanup");
				LogicalIndexCache.applyingMaintenanceTableMaintenance = MaintenanceHandler.RegisterMailboxMaintenance(LogicalIndexCache.ApplyingMaintenanceTableMaintenanceId, RequiredMaintenanceResourceType.Store, true, new MaintenanceHandler.MailboxMaintenanceDelegate(LogicalIndexCache.ApplyMaintenanceTable), "LogicalIndexCache.ApplyMaintenanceTable");
				Mailbox.RegisterOnDisconnectAction(delegate(Context context, Mailbox mailbox)
				{
					LogicalIndexCache cacheForMailboxDoNotCreate = LogicalIndexCache.GetCacheForMailboxDoNotCreate(context, mailbox.SharedState);
					if (cacheForMailboxDoNotCreate != null)
					{
						cacheForMailboxDoNotCreate.OnMailboxDisconnect(context, mailbox);
					}
				});
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00010545 File Offset: 0x0000E745
		public static void MountedEventHandler(Context context)
		{
			if (context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
			{
				LogicalIndexCache.markLogicalIndexForCleanupMaintenance.ScheduleMarkForMaintenance(context, TimeSpan.FromDays(1.0));
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00010574 File Offset: 0x0000E774
		public static IEnumerable<LogicalIndex> GetIndicesForFolder(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			return cacheForFolder.Values;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00010599 File Offset: 0x0000E799
		public static bool IsCacheAvailable(Context context, Mailbox mailbox)
		{
			return LogicalIndexCache.GetCacheForMailboxDoNotCreate(context, mailbox.SharedState) != null;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public static void DiscardCacheForMailbox(Context context, MailboxState mailboxState)
		{
			LogicalIndexCache logicalIndexCache = mailboxState.GetComponentData(LogicalIndexCache.logicalIndexCacheDataSlot) as LogicalIndexCache;
			if (logicalIndexCache != null)
			{
				if (ExTraceGlobals.PseudoIndexTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.PseudoIndexTracer.TraceDebug(0L, "Discarding logical index cache for mailbox " + mailboxState.MailboxNumber);
				}
				if (((IComponentData)logicalIndexCache).DoCleanup(context))
				{
					mailboxState.SetComponentData(LogicalIndexCache.logicalIndexCacheDataSlot, null);
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00010614 File Offset: 0x0000E814
		public static List<IIndex> GetIndexesInScope(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType, Column conditionalIndexColumn, bool conditionalIndexValue, SortOrder sortOrder, IList<Column> nonKeyColumns, CategorizationInfo categorizationInfo, Table table, bool matchingOnly, bool existingOnly)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			if (context.IsSharedMailboxOperation)
			{
				using (context.MailboxComponentReadOperation(cacheForMailbox))
				{
					bool flag;
					List<IIndex> indexesInScopeDoNotCreate = LogicalIndexCache.GetIndexesInScopeDoNotCreate(context, mailbox, folderId, cacheForFolder, indexType, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns, categorizationInfo, table, matchingOnly, out flag);
					if (flag || existingOnly)
					{
						return indexesInScopeDoNotCreate;
					}
				}
			}
			List<IIndex> result;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame2 = context.MailboxComponentWriteOperation(cacheForMailbox))
			{
				bool flag2;
				List<IIndex> list = LogicalIndexCache.GetIndexesInScopeDoNotCreate(context, mailbox, folderId, cacheForFolder, indexType, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns, categorizationInfo, table, matchingOnly, out flag2);
				if (flag2 || existingOnly)
				{
					result = list;
				}
				else
				{
					if ((matchingOnly || list != null) && (indexType == LogicalIndexType.Messages || indexType == LogicalIndexType.Conversations || indexType == LogicalIndexType.SearchFolderMessages))
					{
						cacheForFolder.ConsolidateIndexes(context, indexType, 0, conditionalIndexColumn, conditionalIndexValue, ref sortOrder, ref nonKeyColumns, list);
					}
					LogicalIndex item = cacheForFolder.CreateIndex(context, mailbox.SharedState, indexType, 0, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns, categorizationInfo, table, false);
					if (list == null)
					{
						list = new List<IIndex>(1);
					}
					list.Insert(0, item);
					mailboxComponentOperationFrame2.Success();
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00010744 File Offset: 0x0000E944
		public static List<IIndex> GetIndexesInScope(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType, Column conditionalIndexColumn, bool conditionalIndexValue, SortOrder sortOrder, IList<Column> nonKeyColumns, Table table)
		{
			return LogicalIndexCache.GetIndexesInScope(context, mailbox, folderId, indexType, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns, null, table, false, false);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00010768 File Offset: 0x0000E968
		public static LogicalIndex GetIndexToUse(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType, Column conditionalIndexColumn, bool conditionalIndexValue, SortOrder sortOrder, IList<Column> nonKeyColumns, CategorizationInfo categorizationInfo, Table table)
		{
			List<IIndex> indexesInScope = LogicalIndexCache.GetIndexesInScope(context, mailbox, folderId, indexType, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns, categorizationInfo, table, true, false);
			return (LogicalIndex)indexesInScope[0];
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0001079C File Offset: 0x0000E99C
		public static LogicalIndex GetIndexToUseDoNotCreate(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType, Column conditionalIndexColumn, bool conditionalIndexValue, SortOrder sortOrder, IList<Column> nonKeyColumns, CategorizationInfo categorizationInfo, Table table)
		{
			List<IIndex> indexesInScope = LogicalIndexCache.GetIndexesInScope(context, mailbox, folderId, indexType, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns, categorizationInfo, table, true, true);
			if (indexesInScope != null)
			{
				return (LogicalIndex)indexesInScope[0];
			}
			return null;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000107D4 File Offset: 0x0000E9D4
		public static LogicalIndex FindIndex(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType, int indexSignature)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			LogicalIndex result;
			using (context.MailboxComponentReadOperation(cacheForMailbox))
			{
				result = LogicalIndexCache.FindIndexInternal(context, mailbox, folderId, cacheForFolder, indexType, indexSignature);
			}
			return result;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001082C File Offset: 0x0000EA2C
		public static LogicalIndex CreateIndex(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType, int indexSignature, Column conditionalIndexColumn, bool conditionalIndexValue, SortOrder sortOrder, IList<Column> nonKeyColumns, CategorizationInfo categorizationInfo, Table table, bool markCurrent)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			LogicalIndex result;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = context.MailboxComponentWriteOperation(cacheForMailbox))
			{
				LogicalIndex logicalIndex = cacheForFolder.CreateIndex(context, mailbox.SharedState, indexType, indexSignature, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns, categorizationInfo, table, markCurrent);
				mailboxComponentOperationFrame.Success();
				result = logicalIndex;
			}
			return result;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000108A0 File Offset: 0x0000EAA0
		public static void TrackIndexUpdate(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType, LogicalIndex.LogicalOperation operation, IColumnValueBag updatedPropBag)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			foreach (LogicalIndex logicalIndex in cacheForFolder.Values)
			{
				if (indexType == logicalIndex.IndexType && !logicalIndex.IsStale)
				{
					logicalIndex.TrackIndexUpdate(context, folderId, updatedPropBag, operation);
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00010928 File Offset: 0x0000EB28
		public static bool FolderHasConversationIndex(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			bool result;
			using (context.MailboxComponentReadOperation(cacheForMailbox))
			{
				result = cacheForFolder.Values.Any((LogicalIndex logicalIndex) => LogicalIndexType.Conversations == logicalIndex.IndexType);
			}
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0001099C File Offset: 0x0000EB9C
		public static bool InvalidateIndexes(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType, Column conditionalIndexColumn, bool conditionalIndexValue, DateTime lastReferenceDateThreshold)
		{
			bool result = false;
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			foreach (LogicalIndex logicalIndex in cacheForFolder.Values)
			{
				if (logicalIndex.FolderId == folderId && logicalIndex.IndexType == indexType && !logicalIndex.IsStale && (conditionalIndexColumn == null || (conditionalIndexColumn == logicalIndex.ConditionalIndexColumn && conditionalIndexValue == logicalIndex.ConditionalIndexValue)) && logicalIndex.LastReferenceDate < lastReferenceDateThreshold)
				{
					logicalIndex.InvalidateIndex(context, false);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00010A5C File Offset: 0x0000EC5C
		public static bool InvalidateIndexes(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType)
		{
			return LogicalIndexCache.InvalidateIndexes(context, mailbox, folderId, indexType, null, false, DateTime.MaxValue);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00010A70 File Offset: 0x0000EC70
		public static void InvalidateIndexesForFolderPropertyChange(Context context, Mailbox mailbox, ExchangeId folderId, StorePropTag propTag)
		{
			Column item = PropertySchema.MapToColumn(mailbox.Database, ObjectType.Message, propTag);
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			foreach (LogicalIndex logicalIndex in cacheForFolder.Values)
			{
				if (logicalIndex.FolderId == folderId && (logicalIndex.IndexType == LogicalIndexType.Messages || logicalIndex.IndexType == LogicalIndexType.SearchFolderMessages || logicalIndex.IndexType == LogicalIndexType.CategoryHeaders) && !logicalIndex.IsStale && logicalIndex.Columns.Contains(item))
				{
					logicalIndex.InvalidateIndex(context, false);
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00010B24 File Offset: 0x0000ED24
		public static bool VerifyIfIndexTypeExists(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexType indexType)
		{
			bool result = false;
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			using (context.MailboxComponentReadOperation(cacheForMailbox))
			{
				foreach (LogicalIndex logicalIndex in cacheForFolder.Values)
				{
					if (logicalIndex.FolderId == folderId && logicalIndex.IndexType == indexType && !logicalIndex.IsStale)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00010BCC File Offset: 0x0000EDCC
		public static LogicalIndex GetLogicalIndex(Context context, Mailbox mailbox, ExchangeId folderId, int logicalIndexNumber)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			LogicalIndex logicalIndex;
			using (context.MailboxComponentReadOperation(cacheForMailbox))
			{
				logicalIndex = cacheForFolder.GetLogicalIndex(logicalIndexNumber);
			}
			return logicalIndex;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00010C1C File Offset: 0x0000EE1C
		public static void DeleteIndex(Context context, Mailbox mailbox, ExchangeId folderId, int logicalIndexNumber)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
			LogicalIndex logicalIndex = cacheForFolder.GetLogicalIndex(logicalIndexNumber);
			if (cacheForMailbox.IsIndexLockedInCache(logicalIndex))
			{
				if (ExTraceGlobals.PseudoIndexTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.PseudoIndexTracer.TraceDebug<int>(0L, "Cannot delete index {0} locked in cache", logicalIndexNumber);
				}
				throw new StoreException((LID)62304U, ErrorCodeValue.Busy);
			}
			cacheForFolder.DeleteIndex(context, logicalIndexNumber);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00010C88 File Offset: 0x0000EE88
		public static void DeleteLogicalIndexes(Context context, Mailbox mailbox)
		{
			List<LogicalIndexCache.LogicalIndexInfo> listOfLogicalIndexes = LogicalIndexCache.GetListOfLogicalIndexes(context, mailbox.SharedState, ExchangeId.Zero);
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			foreach (LogicalIndexCache.LogicalIndexInfo logicalIndexInfo in listOfLogicalIndexes)
			{
				LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, logicalIndexInfo.FolderId);
				cacheForFolder.DeleteIndex(context, logicalIndexInfo.LogicalIndexNumber);
			}
			mailbox.RemoveMailboxEntriesFromTable(context, DatabaseSchema.PseudoIndexMaintenanceTable(mailbox.Database).Table);
			LogicalIndexCache.DiscardCacheForMailbox(context, mailbox.SharedState);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00010D2C File Offset: 0x0000EF2C
		public void SetEstimatedOldestMaintenanceRecord(Context context, long value)
		{
			this.estimatedOldestMaintenanceRecord = value;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00010D35 File Offset: 0x0000EF35
		public void SetEstimatedNewestMaintenanceRecord(Context context, long value)
		{
			this.estimatedNewestMaintenanceRecord = value;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00010D40 File Offset: 0x0000EF40
		public bool IsAnyIndexLockedInCache()
		{
			bool result;
			using (LockManager.Lock(this.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
			{
				result = (this.foldersLockedInCache != null);
			}
			return result;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00010D8C File Offset: 0x0000EF8C
		public bool IsIndexLockedInCache(LogicalIndex logicalIndex)
		{
			bool result;
			using (LockManager.Lock(this.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
			{
				result = (this.foldersLockedInCache != null && this.foldersLockedInCache.ContainsKey(logicalIndex.FolderId));
			}
			return result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00010DE8 File Offset: 0x0000EFE8
		public void LockIndexInCache(LogicalIndex logicalIndex)
		{
			using (LockManager.Lock(this.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
			{
				if (this.foldersLockedInCache == null)
				{
					this.foldersLockedInCache = new Dictionary<ExchangeId, KeyValuePair<LogicalIndexCache.FolderIndexCache, int>>();
				}
				int num = 1;
				KeyValuePair<LogicalIndexCache.FolderIndexCache, int> keyValuePair;
				if (this.foldersLockedInCache.TryGetValue(logicalIndex.FolderId, out keyValuePair))
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(object.ReferenceEquals(keyValuePair.Key, logicalIndex.FolderCache), "Replaced folder index cache object locked in cache?");
					num = keyValuePair.Value + 1;
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num < 1000, "Run-away index locked in cache refcount");
				}
				this.foldersLockedInCache[logicalIndex.FolderId] = new KeyValuePair<LogicalIndexCache.FolderIndexCache, int>(logicalIndex.FolderCache, num);
				if (ExTraceGlobals.PseudoIndexTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.PseudoIndexTracer.TraceDebug<int, ExchangeId, int>(0L, "Locking index {0} for folder {1} in cache, resulting refcount {2}", logicalIndex.LogicalIndexNumber, logicalIndex.FolderId, num);
				}
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		public void UnlockIndexInCache(LogicalIndex logicalIndex)
		{
			using (LockManager.Lock(this.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
			{
				KeyValuePair<LogicalIndexCache.FolderIndexCache, int> keyValuePair;
				if (this.foldersLockedInCache != null && this.foldersLockedInCache.TryGetValue(logicalIndex.FolderId, out keyValuePair))
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(object.ReferenceEquals(keyValuePair.Key, logicalIndex.FolderCache), "Replaced folder index cache object locked in cache?");
					int num = keyValuePair.Value - 1;
					if (num > 0)
					{
						this.foldersLockedInCache[logicalIndex.FolderId] = new KeyValuePair<LogicalIndexCache.FolderIndexCache, int>(logicalIndex.FolderCache, num);
					}
					else if (this.foldersLockedInCache.Count == 1)
					{
						this.foldersLockedInCache = null;
					}
					else
					{
						this.foldersLockedInCache.Remove(logicalIndex.FolderId);
					}
					if (ExTraceGlobals.PseudoIndexTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.PseudoIndexTracer.TraceDebug<int, ExchangeId, int>(0L, "Unlocking index {0} for folder {1} in cache, resulting refcount {2}", logicalIndex.LogicalIndexNumber, logicalIndex.FolderId, num);
					}
				}
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public static void MarkLogicalIndicesForCleanup(Context context, DatabaseInfo databaseInfo, out bool completed)
		{
			MaintenanceHandler.ApplyMaintenanceToActiveAndDeletedMailboxes(context, ExecutionDiagnostics.OperationSource.LogicalIndexCleanup, new Action<Context, MailboxState>(LogicalIndexCache.CheckMailboxForLogicalIndexCleanup), out completed);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00010FE2 File Offset: 0x0000F1E2
		public static void CleanupLogicalIndexes(Context context, MailboxState mailboxState, out bool completed)
		{
			LogicalIndexCache.CleanupLogicalIndexes(context, mailboxState, LogicalIndexCache.maxIdleCleanupPeriod, out completed);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		public static void CleanupLogicalIndexes(Context context, MailboxState mailboxState, TimeSpan maxIdleCleanup, out bool completed)
		{
			completed = true;
			if (!mailboxState.IsAccessible)
			{
				return;
			}
			LogicalIndexCache cacheForMailboxDoNotCreate = LogicalIndexCache.GetCacheForMailboxDoNotCreate(context, mailboxState);
			if (cacheForMailboxDoNotCreate != null && cacheForMailboxDoNotCreate.IsAnyIndexLockedInCache())
			{
				completed = false;
				return;
			}
			using (Mailbox mailbox = Mailbox.OpenMailbox(context, mailboxState))
			{
				List<LogicalIndexCache.LogicalIndexInfo> listOfLogicalIndexes = LogicalIndexCache.GetListOfLogicalIndexes(context, mailboxState, ExchangeId.Zero);
				completed = LogicalIndexCache.CleanupLogicalIndexes(context, mailbox, listOfLogicalIndexes, maxIdleCleanup, true);
				context.Commit();
				if (cacheForMailboxDoNotCreate != null && completed)
				{
					long minFirstUpdateRecord = cacheForMailboxDoNotCreate.GetMinFirstUpdateRecord(context);
					cacheForMailboxDoNotCreate.CleanupMaintenance(context, minFirstUpdateRecord);
				}
				mailbox.Disconnect();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00011084 File Offset: 0x0000F284
		public static DateTime GetMostRecentFolderViewAccessTime(Context context, MailboxState mailboxState, ExchangeId folderId)
		{
			DateTime dateTime = DateTime.MinValue;
			List<LogicalIndexCache.LogicalIndexInfo> listOfLogicalIndexes = LogicalIndexCache.GetListOfLogicalIndexes(context, mailboxState, folderId);
			foreach (LogicalIndexCache.LogicalIndexInfo logicalIndexInfo in listOfLogicalIndexes)
			{
				if (logicalIndexInfo.IndexType != LogicalIndexType.SearchFolderBaseView && dateTime < logicalIndexInfo.LastReferenceDate)
				{
					dateTime = logicalIndexInfo.LastReferenceDate;
				}
			}
			return dateTime;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000110FC File Offset: 0x0000F2FC
		public override bool IsValidTableOperation(Context context, Connection.OperationType operationType, Table table, IList<object> partitionValues)
		{
			if (operationType == Connection.OperationType.CreateTable || operationType == Connection.OperationType.DeleteTable)
			{
				return this.TestExclusiveLock();
			}
			if (table.Equals(DatabaseSchema.PseudoIndexControlTable(context.Database).Table) || table.Equals(DatabaseSchema.PseudoIndexMaintenanceTable(context.Database).Table) || table.Equals(DatabaseSchema.PseudoIndexDefinitionTable(context.Database).Table))
			{
				if (operationType == Connection.OperationType.Query)
				{
					return this.TestSharedLock() || this.TestExclusiveLock();
				}
				if (operationType == Connection.OperationType.Insert)
				{
					return this.TestExclusiveLock();
				}
			}
			return false;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00011184 File Offset: 0x0000F384
		internal static LogicalIndexCache GetCacheForMailbox(Context context, Mailbox mailbox)
		{
			LogicalIndexCache logicalIndexCache = LogicalIndexCache.GetCacheForMailboxDoNotCreate(context, mailbox.SharedState);
			if (logicalIndexCache == null)
			{
				if (ExTraceGlobals.PseudoIndexTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.PseudoIndexTracer.TraceDebug(0L, "Creating logical index cache for mailbox " + mailbox.SharedState.MailboxNumber);
				}
				logicalIndexCache = new LogicalIndexCache(context, mailbox.SharedState);
				LogicalIndexCache logicalIndexCache2 = (LogicalIndexCache)mailbox.SharedState.CompareExchangeComponentData(LogicalIndexCache.logicalIndexCacheDataSlot, null, logicalIndexCache);
				if (logicalIndexCache2 != null)
				{
					logicalIndexCache = logicalIndexCache2;
				}
			}
			return logicalIndexCache;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00011200 File Offset: 0x0000F400
		internal static LogicalIndexCache.FolderIndexCache GetCacheForFolderForTest(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			return cacheForMailbox.GetCacheForFolder(context, mailbox, folderId);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001121E File Offset: 0x0000F41E
		internal static IDisposable SetApplyMaintenanceParametersTestHook(LogicalIndexCache.ApplyMaintenanceSettings applyMaintenanceParameters)
		{
			return LogicalIndexCache.applyMaintenanceParameters.SetTestHook(applyMaintenanceParameters);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0001122C File Offset: 0x0000F42C
		internal static void ApplyMaintenanceTable(Context context, MailboxState mailboxState, out bool completed)
		{
			completed = false;
			try
			{
				if (context.PerfInstance != null)
				{
					context.PerfInstance.NumberOfActiveWlmLogicalIndexMaintenanceTableMaintenances.Increment();
				}
				bool flag = true;
				int num = 0;
				while (mailboxState.IsUserAccessible)
				{
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, mailboxState))
					{
						LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
						if (cacheForMailbox.IsAnyIndexLockedInCache())
						{
							return;
						}
						cacheForMailbox.ApplyMaintenanceChunk(context, mailbox, out completed);
					}
					num++;
					if (LockManager.HasContention(mailboxState) && num >= LogicalIndexCache.ApplyMaintenanceParameters.WlmMinNumberOfChunksToProceed)
					{
						flag = false;
					}
					if (context.Database.HasExclusiveLockContention())
					{
						flag = false;
					}
					if (flag && !completed)
					{
						ErrorCode first = context.PulseMailboxOperation();
						flag = (first == ErrorCode.NoError);
					}
					if (!flag || completed || MaintenanceHandler.ShouldStopMailboxMaintenanceTask(context, mailboxState, LogicalIndexCache.ApplyingMaintenanceTableMaintenanceId))
					{
						return;
					}
				}
				completed = true;
			}
			finally
			{
				if (context.PerfInstance != null)
				{
					context.PerfInstance.NumberOfActiveWlmLogicalIndexMaintenanceTableMaintenances.Decrement();
					if (completed)
					{
						context.PerfInstance.NumberOfMailboxesMarkedForWlmLogicalIndexMaintenanceTableMaintenance.Decrement();
					}
				}
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00011348 File Offset: 0x0000F548
		internal void CheckMaintenanceSize(Context context)
		{
			long num = this.EstimatedNewestMaintenanceRecord - this.EstimatedOldestMaintenanceRecord + 1L;
			if (num > (long)LogicalIndexCache.ApplyMaintenanceParameters.WlmMaintenanceThreshold)
			{
				Mailbox mailbox = context.PrimaryMailboxContext as Mailbox;
				if (mailbox != null && mailbox.SharedState.IsMailboxLockedExclusively())
				{
					bool flag = LogicalIndexCache.applyingMaintenanceTableMaintenance.MarkForMaintenance(context, mailbox.SharedState);
					if (flag && context.PerfInstance != null)
					{
						context.PerfInstance.NumberOfMailboxesMarkedForWlmLogicalIndexMaintenanceTableMaintenance.Increment();
					}
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00011414 File Offset: 0x0000F614
		internal void ApplyMaintenanceChunk(Context context, Mailbox mailbox, out bool completed)
		{
			completed = false;
			List<LogicalIndexCache.LogicalIndexInfo> listOfLogicalIndexes = LogicalIndexCache.GetListOfLogicalIndexes(context, mailbox.SharedState, ExchangeId.Zero);
			if (!LogicalIndexCache.CleanupLogicalIndexes(context, mailbox, listOfLogicalIndexes, LogicalIndexCache.maxIdleCleanupPeriod, false))
			{
				return;
			}
			long minFirstUpdateRecord = this.GetMinFirstUpdateRecord(context);
			long num = this.EstimatedNewestMaintenanceRecord - minFirstUpdateRecord + 1L;
			if (num <= (long)LogicalIndexCache.ApplyMaintenanceParameters.StopMaintenanceThreshold)
			{
				this.CleanupMaintenance(context, minFirstUpdateRecord);
				completed = true;
				return;
			}
			Dictionary<int, LogicalIndexCache.LogicalIndexInfo> indexNumberToIndexInfo = new Dictionary<int, LogicalIndexCache.LogicalIndexInfo>(100);
			foreach (LogicalIndexCache.LogicalIndexInfo value in listOfLogicalIndexes)
			{
				indexNumberToIndexInfo.Add(value.LogicalIndexNumber, value);
			}
			bool flag = false;
			int num2 = 0;
			int num3 = 0;
			long num4 = minFirstUpdateRecord - 1L;
			Dictionary<int, Queue<LogicalIndex.MaintRecord>> dictionary = new Dictionary<int, Queue<LogicalIndex.MaintRecord>>(100);
			Queue<LogicalIndex.MaintRecord> queue = new Queue<LogicalIndex.MaintRecord>(LogicalIndexCache.ApplyMaintenanceParameters.NumberOfRecordsToMaintain);
			while (!flag)
			{
				LogicalIndex.ReadMaintenanceTable(context, this.MailboxPartitionNumber, null, num4 + 1L, queue, LogicalIndexCache.ApplyMaintenanceParameters.NumberOfRecordsToMaintain);
				if (queue.Count < LogicalIndexCache.ApplyMaintenanceParameters.NumberOfRecordsToMaintain)
				{
					flag = true;
				}
				num3 += queue.Count;
				if (num3 >= LogicalIndexCache.ApplyMaintenanceParameters.NumberOfRecordsToReadFromMaintenanceTable)
				{
					flag = true;
				}
				while (queue.Count > 0)
				{
					LogicalIndex.MaintRecord item = queue.Dequeue();
					num4 = item.UpdateRecordId;
					LogicalIndexCache.LogicalIndexInfo logicalIndexInfo;
					if (indexNumberToIndexInfo.TryGetValue(item.LogicalIndexNumber, out logicalIndexInfo) && logicalIndexInfo.FirstUpdateRecord <= item.UpdateRecordId && logicalIndexInfo.FirstUpdateRecord != -1L)
					{
						Queue<LogicalIndex.MaintRecord> queue2;
						if (!dictionary.TryGetValue(item.LogicalIndexNumber, out queue2))
						{
							queue2 = new Queue<LogicalIndex.MaintRecord>(32);
							dictionary.Add(item.LogicalIndexNumber, queue2);
						}
						queue2.Enqueue(item);
						num2++;
					}
				}
				if (num2 >= LogicalIndexCache.ApplyMaintenanceParameters.NumberOfRecordsToMaintain)
				{
					flag = true;
				}
			}
			List<LogicalIndexCache.FolderIdAndIndexNumber> list = (from indexNumber in dictionary.Keys
			select new LogicalIndexCache.FolderIdAndIndexNumber(indexNumberToIndexInfo[indexNumber].FolderId, indexNumber)).ToList<LogicalIndexCache.FolderIdAndIndexNumber>();
			list.Sort((LogicalIndexCache.FolderIdAndIndexNumber e1, LogicalIndexCache.FolderIdAndIndexNumber e2) => e1.FolderId.CompareTo(e2.FolderId));
			foreach (LogicalIndexCache.FolderIdAndIndexNumber folderIdAndIndexNumber in list)
			{
				queue = dictionary[folderIdAndIndexNumber.IndexNumber];
				LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, mailbox, folderIdAndIndexNumber.FolderId, folderIdAndIndexNumber.IndexNumber);
				if (logicalIndex != null)
				{
					do
					{
						logicalIndex.ApplyMaintenanceChunk(context, queue, 512, true);
					}
					while (queue.Count != 0 && !logicalIndex.IsStale);
				}
			}
			PseudoIndexControlTable pseudoIndexControlTable = DatabaseSchema.PseudoIndexControlTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				this.MailboxPartitionNumber
			});
			SearchCriteriaAnd restriction = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
			{
				Factory.CreateSearchCriteriaCompare(pseudoIndexControlTable.FirstUpdateRecord, SearchCriteriaCompare.SearchRelOp.GreaterThanEqual, Factory.CreateConstantColumn(this.EstimatedOldestMaintenanceRecord)),
				Factory.CreateSearchCriteriaCompare(pseudoIndexControlTable.FirstUpdateRecord, SearchCriteriaCompare.SearchRelOp.LessThanEqual, Factory.CreateConstantColumn(num4))
			});
			using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, pseudoIndexControlTable.Table, pseudoIndexControlTable.PseudoIndexControlPK, null, restriction, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, false), new Column[]
			{
				pseudoIndexControlTable.FirstUpdateRecord
			}, new object[]
			{
				num4 + 1L
			}, false))
			{
				updateOperator.ExecuteScalar();
			}
			LogicalIndexCache.DiscardCacheForMailbox(context, mailbox.SharedState);
			context.Commit();
			this.CleanupMaintenance(context, num4 + 1L);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000117F0 File Offset: 0x0000F9F0
		private static void CheckMailboxForLogicalIndexCleanup(Context context, MailboxState mailboxState)
		{
			List<LogicalIndexCache.LogicalIndexInfo> listOfLogicalIndexes = LogicalIndexCache.GetListOfLogicalIndexes(context, mailboxState, ExchangeId.Zero);
			if (listOfLogicalIndexes.Count > 0)
			{
				bool flag = false;
				if (mailboxState.IsUserAccessible)
				{
					DateTime utcNow = DateTime.UtcNow;
					using (List<LogicalIndexCache.LogicalIndexInfo>.Enumerator enumerator = listOfLogicalIndexes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							LogicalIndexCache.LogicalIndexInfo logicalIndexInfo = enumerator.Current;
							if (utcNow - logicalIndexInfo.LastReferenceDate >= LogicalIndexCache.maxIdleCleanupPeriod)
							{
								flag = true;
								break;
							}
						}
						goto IL_73;
					}
				}
				flag = true;
				IL_73:
				if (LogicalIndexCache.markMailboxForLogicalIndexCleanupTestHook.Value != null)
				{
					flag = LogicalIndexCache.markMailboxForLogicalIndexCleanupTestHook.Value(mailboxState, flag);
				}
				if (flag)
				{
					mailboxState.AddReference();
					try
					{
						LogicalIndexCache.logicalIndexCleanupMaintenance.MarkForMaintenance(context, mailboxState);
					}
					finally
					{
						mailboxState.ReleaseReference();
					}
				}
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000118CC File Offset: 0x0000FACC
		private static LogicalIndex FindIndexInternal(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexCache.FolderIndexCache folderCache, LogicalIndexType indexType, int indexSignature)
		{
			LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			return folderCache.FindIndex(indexType, indexSignature);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000118E0 File Offset: 0x0000FAE0
		private static bool CleanupLogicalIndexes(Context context, Mailbox mailbox, List<LogicalIndexCache.LogicalIndexInfo> listOfIndexInfos, TimeSpan maxIdleCleanup, bool permitBringNonDeletableIndicesUpToDate)
		{
			bool result = true;
			int num = 0;
			DateTime utcNow = DateTime.UtcNow;
			bool flag = !mailbox.SharedState.IsUserAccessible;
			if (LogicalIndexCache.forceMailboxLogicalIndexCleanupTestHook.Value != null)
			{
				flag = LogicalIndexCache.forceMailboxLogicalIndexCleanupTestHook.Value(mailbox.SharedState, flag);
			}
			LogicalIndexCache cacheForMailbox = LogicalIndexCache.GetCacheForMailbox(context, mailbox);
			foreach (LogicalIndexCache.LogicalIndexInfo logicalIndexInfo in listOfIndexInfos)
			{
				if (flag || utcNow - logicalIndexInfo.LastReferenceDate >= maxIdleCleanup)
				{
					LogicalIndexCache.FolderIndexCache cacheForFolder = cacheForMailbox.GetCacheForFolder(context, mailbox, logicalIndexInfo.FolderId);
					LogicalIndex logicalIndex = cacheForFolder.GetLogicalIndex(logicalIndexInfo.LogicalIndexNumber);
					if (logicalIndex != null)
					{
						if (logicalIndex.IndexType == LogicalIndexType.SearchFolderBaseView || logicalIndex.IndexType == LogicalIndexType.ConversationDeleteHistory)
						{
							if (!permitBringNonDeletableIndicesUpToDate)
							{
								continue;
							}
							if (!logicalIndex.IsStale)
							{
								logicalIndex.ApplyMaintenanceToIndex(context, false, true, 2147483647L);
							}
						}
						else
						{
							if (LogicalIndexCache.cleanupOneLogicalIndexTestHook.Value != null)
							{
								LogicalIndexCache.cleanupOneLogicalIndexTestHook.Value(mailbox.SharedState, logicalIndex, flag);
							}
							cacheForFolder.DeleteIndex(context, logicalIndex.LogicalIndexNumber);
						}
						num++;
						if (num % LogicalIndexCache.logicalIndexCleanupChunkSize.Value == 0)
						{
							context.Commit();
							if (MaintenanceHandler.ShouldStopMailboxMaintenanceTask(context, mailbox.SharedState, LogicalIndexCache.LogicalIndexCleanupMaintenanceId))
							{
								result = false;
								break;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00011A50 File Offset: 0x0000FC50
		private static LogicalIndexCache GetCacheForMailboxDoNotCreate(Context context, MailboxState mailboxState)
		{
			return mailboxState.GetComponentData(LogicalIndexCache.logicalIndexCacheDataSlot) as LogicalIndexCache;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00011A64 File Offset: 0x0000FC64
		internal LogicalIndexCache.FolderIndexCache GetCacheForFolder(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			LogicalIndexCache.FolderIndexCache folderIndexCache;
			using (LockManager.Lock(this.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
			{
				folderIndexCache = this.folderIdToFolderIndexCache.Find(folderId, false);
				KeyValuePair<LogicalIndexCache.FolderIndexCache, int> keyValuePair;
				if (folderIndexCache == null && this.foldersLockedInCache != null && this.foldersLockedInCache.TryGetValue(folderId, out keyValuePair))
				{
					folderIndexCache = keyValuePair.Key;
					this.folderIdToFolderIndexCache.Insert(folderId, folderIndexCache, false);
				}
			}
			if (folderIndexCache == null)
			{
				using (MailboxComponentOperationFrame mailboxComponentOperationFrame = context.MailboxComponentWriteOperation(this))
				{
					using (LockManager.Lock(this.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
					{
						folderIndexCache = this.folderIdToFolderIndexCache.Find(folderId, false);
					}
					if (folderIndexCache == null)
					{
						folderIndexCache = new LogicalIndexCache.FolderIndexCache(context, this, mailbox, folderId);
						using (LockManager.Lock(this.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
						{
							this.folderIdToFolderIndexCache.Insert(folderId, folderIndexCache, false);
						}
					}
					mailboxComponentOperationFrame.Success();
				}
			}
			return folderIndexCache;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00011B90 File Offset: 0x0000FD90
		internal LogicalIndexCache.FolderIndexCache GetCacheForFolderDoNotLoad(ExchangeId folderId)
		{
			LogicalIndexCache.FolderIndexCache result;
			using (LockManager.Lock(this.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
			{
				LogicalIndexCache.FolderIndexCache folderIndexCache = this.folderIdToFolderIndexCache.Find(folderId, false);
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(folderIndexCache != null, "folder cache must be loaded");
				result = folderIndexCache;
			}
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		[Conditional("DEBUG")]
		private static void ValidateNoCulturallySignificantColumns(SortOrder sortOrder)
		{
			for (int i = 0; i < sortOrder.Count; i++)
			{
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00011C10 File Offset: 0x0000FE10
		[Conditional("DEBUG")]
		private static void ValidateSingleMVProp(SortOrder sortOrder)
		{
			int num = 0;
			for (int i = 0; i < sortOrder.Count; i++)
			{
				PropertyColumn propertyColumn = sortOrder[i].Column as PropertyColumn;
				if (propertyColumn != null && propertyColumn.StorePropTag.IsMultiValueInstance)
				{
					num++;
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00011C68 File Offset: 0x0000FE68
		private static List<IIndex> GetIndexesInScopeDoNotCreate(Context context, Mailbox mailbox, ExchangeId folderId, LogicalIndexCache.FolderIndexCache folderCache, LogicalIndexType indexType, Column conditionalIndexColumn, bool conditionalIndexValue, SortOrder sortOrder, IList<Column> nonKeyColumns, CategorizationInfo categorizationInfo, Table table, bool matchingOnly, out bool foundMatchingIndex)
		{
			foundMatchingIndex = false;
			List<IIndex> list = null;
			LogicalIndex logicalIndex = null;
			bool flag = false;
			foreach (LogicalIndex logicalIndex2 in folderCache.Values)
			{
				if (logicalIndex2.IndexInScope(context, folderId, indexType, conditionalIndexColumn, conditionalIndexValue) && logicalIndex2.CompatibleMvExplosionColumn(context, indexType, sortOrder) && (CultureHelper.GetLcidFromCulture(logicalIndex2.GetCulture()) == CultureHelper.GetLcidFromCulture(context.Culture) || !logicalIndex2.IsCultureSensitive()))
				{
					if (logicalIndex2.CanUseIndex(context, folderId, indexType, sortOrder, nonKeyColumns, conditionalIndexColumn, conditionalIndexValue, categorizationInfo))
					{
						foundMatchingIndex = true;
						if (logicalIndex2.IsStale)
						{
							if (!flag && (logicalIndex == null || logicalIndex.LogicalIndexNumber < logicalIndex2.LogicalIndexNumber))
							{
								logicalIndex = logicalIndex2;
							}
						}
						else if (!flag || logicalIndex.LogicalIndexNumber < logicalIndex2.LogicalIndexNumber)
						{
							flag = true;
							logicalIndex = logicalIndex2;
						}
					}
					else if (!matchingOnly && !logicalIndex2.IsStale)
					{
						if (list == null)
						{
							list = new List<IIndex>(Math.Min(10, folderCache.Count));
						}
						list.Add(logicalIndex2);
					}
				}
			}
			if (logicalIndex != null)
			{
				if (list == null)
				{
					list = new List<IIndex>(1);
				}
				list.Insert(0, logicalIndex);
			}
			return list;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00011D98 File Offset: 0x0000FF98
		private static List<LogicalIndexCache.LogicalIndexInfo> GetListOfLogicalIndexes(Context context, MailboxState mailboxState, ExchangeId folderId)
		{
			PseudoIndexControlTable pseudoIndexControlTable = DatabaseSchema.PseudoIndexControlTable(context.Database);
			StartStopKey startStopKey;
			if (folderId.IsValid)
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					mailboxState.MailboxPartitionNumber,
					folderId.To26ByteArray()
				});
			}
			else
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					mailboxState.MailboxPartitionNumber
				});
			}
			List<LogicalIndexCache.LogicalIndexInfo> result;
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, pseudoIndexControlTable.Table, pseudoIndexControlTable.PseudoIndexControlPK, new Column[]
			{
				pseudoIndexControlTable.FolderId,
				pseudoIndexControlTable.LogicalIndexNumber,
				pseudoIndexControlTable.LastReferenceDate,
				pseudoIndexControlTable.FirstUpdateRecord,
				pseudoIndexControlTable.IndexType
			}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, false))
			{
				List<LogicalIndexCache.LogicalIndexInfo> list = new List<LogicalIndexCache.LogicalIndexInfo>(100);
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						byte[] binary = reader.GetBinary(pseudoIndexControlTable.FolderId);
						int @int = reader.GetInt32(pseudoIndexControlTable.LogicalIndexNumber);
						DateTime dateTime = reader.GetDateTime(pseudoIndexControlTable.LastReferenceDate);
						long int2 = reader.GetInt64(pseudoIndexControlTable.FirstUpdateRecord);
						LogicalIndexType int3 = (LogicalIndexType)reader.GetInt32(pseudoIndexControlTable.IndexType);
						list.Add(new LogicalIndexCache.LogicalIndexInfo(@int, ExchangeId.CreateFrom26ByteArray(null, null, binary), dateTime, int2, int3));
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00011F2C File Offset: 0x0001012C
		private void InitializeCache(Context context)
		{
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = context.MailboxComponentWriteOperation(this))
			{
				this.RefreshMaintenanceCache(context, true, true);
				mailboxComponentOperationFrame.Success();
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00011F74 File Offset: 0x00010174
		private long GetMinFirstUpdateRecord(Context context)
		{
			long num = this.EstimatedNewestMaintenanceRecord + 1L;
			PseudoIndexControlTable pseudoIndexControlTable = DatabaseSchema.PseudoIndexControlTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				this.MailboxPartitionNumber
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, pseudoIndexControlTable.Table, pseudoIndexControlTable.PseudoIndexControlPK, new Column[]
			{
				pseudoIndexControlTable.FirstUpdateRecord
			}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, false))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						long @int = reader.GetInt64(pseudoIndexControlTable.FirstUpdateRecord);
						if (@int != -1L)
						{
							num = Math.Min(num, @int);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0001205C File Offset: 0x0001025C
		private void CleanupMaintenance(Context context, long minFirstUpdateRecord)
		{
			DateTime utcNow = DateTime.UtcNow;
			double num = (utcNow > this.mailboxCreationTime) ? (utcNow - this.mailboxCreationTime).TotalDays : 0.0;
			long num2 = (long)((double)this.EstimatedNewestMaintenanceRecord / (num + 1.0));
			long num3 = (long)((double)num2 * LogicalIndexCache.ApplyMaintenanceParameters.MaintenanceTimePeriodToKeep.TotalDays);
			long num4 = Math.Min(this.EstimatedNewestMaintenanceRecord - num3, minFirstUpdateRecord);
			if (num4 > this.EstimatedOldestMaintenanceRecord)
			{
				PseudoIndexMaintenanceTable pseudoIndexMaintenanceTable = DatabaseSchema.PseudoIndexMaintenanceTable(context.Database);
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					this.MailboxPartitionNumber
				});
				bool flag;
				do
				{
					flag = false;
					StartStopKey startKey = startStopKey;
					StartStopKey stopKey = new StartStopKey(false, new object[]
					{
						this.MailboxPartitionNumber,
						num4
					});
					using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, pseudoIndexMaintenanceTable.Table, pseudoIndexMaintenanceTable.Table.PrimaryKeyIndex, new PhysicalColumn[]
					{
						pseudoIndexMaintenanceTable.UpdateRecordNumber
					}, null, null, LogicalIndexCache.maxMaintenanceRowsToDeleteAtATime, 1, new KeyRange(startKey, stopKey), false, false))
					{
						using (Reader reader = tableOperator.ExecuteReader(false))
						{
							if (reader.Read())
							{
								flag = true;
								long @int = reader.GetInt64(pseudoIndexMaintenanceTable.UpdateRecordNumber);
								stopKey = new StartStopKey(false, new object[]
								{
									this.MailboxPartitionNumber,
									@int
								});
								startStopKey = new StartStopKey(true, new object[]
								{
									this.MailboxPartitionNumber,
									@int
								});
							}
						}
					}
					using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, pseudoIndexMaintenanceTable.Table, pseudoIndexMaintenanceTable.Table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(startKey, stopKey), false, false), false))
					{
						deleteOperator.ExecuteScalar();
					}
					context.Commit();
				}
				while (flag);
				this.RefreshMaintenanceCache(context, true, true);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000122C0 File Offset: 0x000104C0
		private void RefreshMaintenanceCache(Context context, bool min, bool max)
		{
			PseudoIndexMaintenanceTable pseudoIndexMaintenanceTable = DatabaseSchema.PseudoIndexMaintenanceTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				this.MailboxPartitionNumber
			});
			if (min)
			{
				long value = 0L;
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, pseudoIndexMaintenanceTable.Table, pseudoIndexMaintenanceTable.Table.PrimaryKeyIndex, new Column[]
				{
					pseudoIndexMaintenanceTable.UpdateRecordNumber
				}, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, false))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (reader.Read())
						{
							value = reader.GetInt64(pseudoIndexMaintenanceTable.UpdateRecordNumber);
						}
					}
				}
				this.SetEstimatedOldestMaintenanceRecord(context, value);
			}
			if (max)
			{
				long value2 = 0L;
				using (TableOperator tableOperator2 = Factory.CreateTableOperator(context.Culture, context, pseudoIndexMaintenanceTable.Table, pseudoIndexMaintenanceTable.Table.PrimaryKeyIndex, new Column[]
				{
					pseudoIndexMaintenanceTable.UpdateRecordNumber
				}, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), true, false))
				{
					using (Reader reader2 = tableOperator2.ExecuteReader(false))
					{
						if (reader2.Read())
						{
							value2 = reader2.GetInt64(pseudoIndexMaintenanceTable.UpdateRecordNumber);
						}
					}
				}
				this.SetEstimatedNewestMaintenanceRecord(context, value2);
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0001244C File Offset: 0x0001064C
		private void OnMailboxDisconnect(Context context, Mailbox mailbox)
		{
			if (!mailbox.SharedState.IsMailboxLockedExclusively())
			{
				return;
			}
			this.folderIdToFolderIndexCache.EvictionCheckpoint();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00012467 File Offset: 0x00010667
		bool IComponentData.DoCleanup(Context context)
		{
			if (this.IsAnyIndexLockedInCache())
			{
				this.folderIdToFolderIndexCache.Reset();
				return false;
			}
			return true;
		}

		// Token: 0x04000109 RID: 265
		private const int AvgLogicalIndexPerMailbox = 100;

		// Token: 0x0400010A RID: 266
		private const int DefaultLogicalIndexCleanupChunkSize = 25;

		// Token: 0x0400010B RID: 267
		public static readonly Guid LogicalIndexCleanupMaintenanceId = new Guid("{818429a5-c7c8-4546-8cad-c71efaf3c219}");

		// Token: 0x0400010C RID: 268
		public static readonly Guid MarkLogicalIndexForCleanupMaintenanceId = new Guid("{8dda68d9-e1c4-4b97-a884-bf0ab208cf5c}");

		// Token: 0x0400010D RID: 269
		public static readonly Guid ApplyingMaintenanceTableMaintenanceId = new Guid("{f4946920-3356-4f2d-bfb0-e72f14af6f56}");

		// Token: 0x0400010E RID: 270
		internal static int NumberOfCachedFoldersPerMailbox = 24;

		// Token: 0x0400010F RID: 271
		internal static TimeSpan TimeToLive = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000110 RID: 272
		private static TimeSpan maxIdleCleanupPeriod;

		// Token: 0x04000111 RID: 273
		private static int maxMaintenanceRowsToDeleteAtATime = 500;

		// Token: 0x04000112 RID: 274
		private static IMailboxMaintenance applyingMaintenanceTableMaintenance;

		// Token: 0x04000113 RID: 275
		private static IMailboxMaintenance logicalIndexCleanupMaintenance;

		// Token: 0x04000114 RID: 276
		private static IDatabaseMaintenance markLogicalIndexForCleanupMaintenance;

		// Token: 0x04000115 RID: 277
		private static int logicalIndexCacheDataSlot = -1;

		// Token: 0x04000116 RID: 278
		private static ICachePerformanceCounters folderCachePerfCounters;

		// Token: 0x04000117 RID: 279
		private static Hookable<LogicalIndexCache.ApplyMaintenanceSettings> applyMaintenanceParameters;

		// Token: 0x04000118 RID: 280
		private static Hookable<int> logicalIndexCleanupChunkSize = Hookable<int>.Create(true, 25);

		// Token: 0x04000119 RID: 281
		private static readonly Hookable<Func<MailboxState, bool, bool>> markMailboxForLogicalIndexCleanupTestHook = Hookable<Func<MailboxState, bool, bool>>.Create(true, null);

		// Token: 0x0400011A RID: 282
		private static readonly Hookable<Func<MailboxState, bool, bool>> forceMailboxLogicalIndexCleanupTestHook = Hookable<Func<MailboxState, bool, bool>>.Create(true, null);

		// Token: 0x0400011B RID: 283
		private static readonly Hookable<Action<MailboxState, LogicalIndex, bool>> cleanupOneLogicalIndexTestHook = Hookable<Action<MailboxState, LogicalIndex, bool>>.Create(true, null);

		// Token: 0x0400011C RID: 284
		private readonly bool updateIndexDirectly;

		// Token: 0x0400011D RID: 285
		private readonly DateTime mailboxCreationTime;

		// Token: 0x0400011E RID: 286
		private MailboxLockNameBase mailboxLockName;

		// Token: 0x0400011F RID: 287
		private SingleKeyCache<ExchangeId, LogicalIndexCache.FolderIndexCache> folderIdToFolderIndexCache;

		// Token: 0x04000120 RID: 288
		private Dictionary<ExchangeId, KeyValuePair<LogicalIndexCache.FolderIndexCache, int>> foldersLockedInCache;

		// Token: 0x04000121 RID: 289
		private long estimatedOldestMaintenanceRecord;

		// Token: 0x04000122 RID: 290
		private long estimatedNewestMaintenanceRecord;

		// Token: 0x02000026 RID: 38
		private struct DatabaseAndMailboxNumber
		{
			// Token: 0x06000200 RID: 512 RVA: 0x00012515 File Offset: 0x00010715
			internal DatabaseAndMailboxNumber(StoreDatabase database, int mailboxNumber)
			{
				this.database = database;
				this.mailboxNumber = mailboxNumber;
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000201 RID: 513 RVA: 0x00012525 File Offset: 0x00010725
			internal StoreDatabase Database
			{
				get
				{
					return this.database;
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000202 RID: 514 RVA: 0x0001252D File Offset: 0x0001072D
			internal int MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x0400012D RID: 301
			private readonly StoreDatabase database;

			// Token: 0x0400012E RID: 302
			private readonly int mailboxNumber;
		}

		// Token: 0x02000027 RID: 39
		private struct FolderIdAndIndexNumber
		{
			// Token: 0x06000203 RID: 515 RVA: 0x00012535 File Offset: 0x00010735
			internal FolderIdAndIndexNumber(ExchangeId folderId, int indexNumber)
			{
				this.folderId = folderId;
				this.indexNumber = indexNumber;
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x06000204 RID: 516 RVA: 0x00012545 File Offset: 0x00010745
			internal ExchangeId FolderId
			{
				get
				{
					return this.folderId;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x06000205 RID: 517 RVA: 0x0001254D File Offset: 0x0001074D
			internal int IndexNumber
			{
				get
				{
					return this.indexNumber;
				}
			}

			// Token: 0x0400012F RID: 303
			private readonly ExchangeId folderId;

			// Token: 0x04000130 RID: 304
			private readonly int indexNumber;
		}

		// Token: 0x02000028 RID: 40
		private struct LogicalIndexInfo
		{
			// Token: 0x06000206 RID: 518 RVA: 0x00012555 File Offset: 0x00010755
			internal LogicalIndexInfo(int logicalIndexNumber, ExchangeId folderId, DateTime lastReferenceDate, long firstUpdateRecord, LogicalIndexType indexType)
			{
				this.logicalIndexNumber = logicalIndexNumber;
				this.folderId = folderId;
				this.lastReferenceDate = lastReferenceDate;
				this.firstUpdateRecord = firstUpdateRecord;
				this.indexType = indexType;
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x06000207 RID: 519 RVA: 0x0001257C File Offset: 0x0001077C
			internal int LogicalIndexNumber
			{
				get
				{
					return this.logicalIndexNumber;
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x06000208 RID: 520 RVA: 0x00012584 File Offset: 0x00010784
			internal ExchangeId FolderId
			{
				get
				{
					return this.folderId;
				}
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x06000209 RID: 521 RVA: 0x0001258C File Offset: 0x0001078C
			internal DateTime LastReferenceDate
			{
				get
				{
					return this.lastReferenceDate;
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x0600020A RID: 522 RVA: 0x00012594 File Offset: 0x00010794
			internal long FirstUpdateRecord
			{
				get
				{
					return this.firstUpdateRecord;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x0600020B RID: 523 RVA: 0x0001259C File Offset: 0x0001079C
			internal LogicalIndexType IndexType
			{
				get
				{
					return this.indexType;
				}
			}

			// Token: 0x04000131 RID: 305
			private int logicalIndexNumber;

			// Token: 0x04000132 RID: 306
			private ExchangeId folderId;

			// Token: 0x04000133 RID: 307
			private DateTime lastReferenceDate;

			// Token: 0x04000134 RID: 308
			private long firstUpdateRecord;

			// Token: 0x04000135 RID: 309
			private LogicalIndexType indexType;
		}

		// Token: 0x02000029 RID: 41
		internal class FolderIndexCache : Dictionary<int, LogicalIndex>, IStateObject
		{
			// Token: 0x0600020C RID: 524 RVA: 0x000125A4 File Offset: 0x000107A4
			internal FolderIndexCache(Context context, LogicalIndexCache logicalIndexCache, Mailbox mailbox, ExchangeId folderId)
			{
				this.logicalIndexCache = logicalIndexCache;
				this.folderId = folderId;
				this.LoadFolderCache(context, mailbox);
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x0600020D RID: 525 RVA: 0x000125C3 File Offset: 0x000107C3
			internal LogicalIndexCache LogicalIndexCache
			{
				get
				{
					return this.logicalIndexCache;
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x0600020E RID: 526 RVA: 0x000125CB File Offset: 0x000107CB
			internal ExchangeId FolderId
			{
				get
				{
					return this.folderId;
				}
			}

			// Token: 0x0600020F RID: 527 RVA: 0x000125D3 File Offset: 0x000107D3
			void IStateObject.OnBeforeCommit(Context context)
			{
			}

			// Token: 0x06000210 RID: 528 RVA: 0x000125D5 File Offset: 0x000107D5
			void IStateObject.OnCommit(Context context)
			{
			}

			// Token: 0x06000211 RID: 529 RVA: 0x000125D8 File Offset: 0x000107D8
			void IStateObject.OnAbort(Context context)
			{
				using (LockManager.Lock(this.logicalIndexCache.folderIdToFolderIndexCache, LockManager.LockType.LeafMonitorLock))
				{
					this.logicalIndexCache.folderIdToFolderIndexCache.Remove(this.folderId, false);
				}
			}

			// Token: 0x06000212 RID: 530 RVA: 0x00012630 File Offset: 0x00010830
			internal LogicalIndex GetLogicalIndex(int logicalIndexNumber)
			{
				LogicalIndex result;
				if (!base.TryGetValue(logicalIndexNumber, out result))
				{
					return null;
				}
				return result;
			}

			// Token: 0x06000213 RID: 531 RVA: 0x0001264C File Offset: 0x0001084C
			internal LogicalIndex FindIndex(LogicalIndexType indexType, int indexSignature)
			{
				foreach (LogicalIndex logicalIndex in base.Values)
				{
					if (logicalIndex.IndexType == indexType && logicalIndex.IndexSignature == indexSignature)
					{
						return logicalIndex;
					}
				}
				return null;
			}

			// Token: 0x06000214 RID: 532 RVA: 0x000126B4 File Offset: 0x000108B4
			internal LogicalIndex CreateIndex(Context context, MailboxState mailboxState, LogicalIndexType indexType, int indexSignature, Column conditionalIndexColumn, bool conditionalIndexValue, SortOrder sortOrder, IList<Column> nonKeyColumns, CategorizationInfo categorizationInfo, Table table, bool markCurrent)
			{
				LogicalIndex logicalIndex = LogicalIndex.CreateIndex(context, mailboxState, this, this.folderId, indexType, indexSignature, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns, categorizationInfo, table, markCurrent);
				base[logicalIndex.LogicalIndexNumber] = logicalIndex;
				return logicalIndex;
			}

			// Token: 0x06000215 RID: 533 RVA: 0x000126F0 File Offset: 0x000108F0
			public LogicalIndex GetIndexToUseForPopulation(Context context, LogicalIndexType indexType, Column conditionalIndexColumn, bool conditionalIndexValue, SortOrder sortOrder, IList<Column> nonKeyColumns, int logicalIndexNumber)
			{
				LogicalIndex result;
				using (context.MailboxComponentReadOperation(this.logicalIndexCache))
				{
					foreach (LogicalIndex logicalIndex in base.Values)
					{
						if (logicalIndex.LogicalIndexNumber != logicalIndexNumber && logicalIndex.CanUseIndexForPopulation(context, this.folderId, indexType, conditionalIndexColumn, conditionalIndexValue, sortOrder, nonKeyColumns))
						{
							return logicalIndex;
						}
					}
					result = null;
				}
				return result;
			}

			// Token: 0x06000216 RID: 534 RVA: 0x00012790 File Offset: 0x00010990
			internal void ConsolidateIndexes(Context context, LogicalIndexType indexType, int indexSignature, Column conditionalIndexColumn, bool conditionalIndexValue, ref SortOrder sortOrder, ref IList<Column> nonKeyColumns, List<IIndex> indexList)
			{
				bool flag = !context.TransactionStarted;
				LogicalIndex logicalIndex;
				do
				{
					logicalIndex = null;
					foreach (LogicalIndex logicalIndex2 in base.Values)
					{
						bool flag2;
						if (!logicalIndex2.IsStale && !logicalIndex2.IsInvalidatePending && !this.LogicalIndexCache.IsIndexLockedInCache(logicalIndex2) && logicalIndex2.IndexInScope(context, this.folderId, indexType, conditionalIndexColumn, conditionalIndexValue) && (SortOrder.IsMatch(sortOrder, logicalIndex2.LogicalSortOrder, logicalIndex2.ConstantColumns, out flag2) || SortOrder.IsMatch(logicalIndex2.LogicalSortOrder, sortOrder, logicalIndex2.ConstantColumns, out flag2)) && (CultureHelper.GetLcidFromCulture(logicalIndex2.GetCulture()) == CultureHelper.GetLcidFromCulture(context.Culture) || !logicalIndex2.IsCultureSensitive()))
						{
							logicalIndex = logicalIndex2;
							break;
						}
					}
					if (logicalIndex != null)
					{
						bool flag2;
						if (!SortOrder.IsMatch(logicalIndex.LogicalSortOrder, sortOrder, logicalIndex.ConstantColumns, out flag2))
						{
							sortOrder = logicalIndex.LogicalSortOrder;
						}
						if (nonKeyColumns != null && logicalIndex.NonKeyColumns != null)
						{
							List<Column> list = new List<Column>(logicalIndex.NonKeyColumns.Count + 4);
							for (int i = 0; i < logicalIndex.NonKeyColumns.Count; i++)
							{
								Column column = logicalIndex.NonKeyColumns[i];
								if (column.MaxLength > PhysicalIndex.MaxSortColumnLength(column.Type) || !sortOrder.Contains(column))
								{
									list.Add(column);
								}
							}
							for (int j = 0; j < nonKeyColumns.Count; j++)
							{
								Column column2 = nonKeyColumns[j];
								if (!list.Contains(column2) && (column2.MaxLength > PhysicalIndex.MaxSortColumnLength(column2.Type) || !sortOrder.Contains(column2)))
								{
									list.Add(column2);
								}
							}
							nonKeyColumns = list.ToArray();
						}
						else if (logicalIndex.NonKeyColumns != null)
						{
							nonKeyColumns = logicalIndex.NonKeyColumns;
						}
						if (indexList != null)
						{
							indexList.Remove(logicalIndex);
						}
						logicalIndex.InvalidateIndex(context, true);
					}
				}
				while (logicalIndex != null);
				if (flag)
				{
					context.Commit();
				}
			}

			// Token: 0x06000217 RID: 535 RVA: 0x000129D8 File Offset: 0x00010BD8
			internal void DeleteIndex(Context context, int logicalIndexNumber)
			{
				this.DeleteIndexImpl(logicalIndexNumber, delegate(LogicalIndex logicalIndex)
				{
					logicalIndex.DeleteIndex(context);
				});
			}

			// Token: 0x06000218 RID: 536 RVA: 0x00012A1C File Offset: 0x00010C1C
			internal void DeleteIndexNoLock(Context context, int logicalIndexNumber)
			{
				this.DeleteIndexImpl(logicalIndexNumber, delegate(LogicalIndex logicalIndex)
				{
					logicalIndex.DeleteIndexNoLock(context);
				});
			}

			// Token: 0x06000219 RID: 537 RVA: 0x00012A4C File Offset: 0x00010C4C
			private void DeleteIndexImpl(int logicalIndexNumber, Action<LogicalIndex> indexDeletionAction)
			{
				LogicalIndex logicalIndex;
				if (base.TryGetValue(logicalIndexNumber, out logicalIndex))
				{
					indexDeletionAction(logicalIndex);
					base.Remove(logicalIndex.LogicalIndexNumber);
				}
			}

			// Token: 0x0600021A RID: 538 RVA: 0x00012A78 File Offset: 0x00010C78
			private void LoadFolderCache(Context context, Mailbox mailbox)
			{
				List<LogicalIndexCache.LogicalIndexInfo> listOfLogicalIndexes = LogicalIndexCache.GetListOfLogicalIndexes(context, mailbox.SharedState, this.folderId);
				foreach (LogicalIndexCache.LogicalIndexInfo logicalIndexInfo in listOfLogicalIndexes)
				{
					LogicalIndex logicalIndex = LogicalIndex.LoadIndex(context, this, logicalIndexInfo.FolderId, logicalIndexInfo.LogicalIndexNumber, mailbox);
					base.Add(logicalIndex.LogicalIndexNumber, logicalIndex);
				}
				foreach (LogicalIndexCache.LogicalIndexInfo logicalIndexInfo2 in listOfLogicalIndexes)
				{
					LogicalIndex logicalIndex2;
					if (base.TryGetValue(logicalIndexInfo2.LogicalIndexNumber, out logicalIndex2) && !logicalIndex2.IsCompatibleWithCurrentSchema(context))
					{
						if (!context.IsStateObjectRegistered(this))
						{
							context.RegisterStateObject(this);
						}
						this.DeleteIndex(context, logicalIndex2.LogicalIndexNumber);
					}
				}
			}

			// Token: 0x04000136 RID: 310
			private readonly LogicalIndexCache logicalIndexCache;

			// Token: 0x04000137 RID: 311
			private readonly ExchangeId folderId;
		}

		// Token: 0x0200002A RID: 42
		internal class ApplyMaintenanceSettings
		{
			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x0600021B RID: 539 RVA: 0x00012B68 File Offset: 0x00010D68
			// (set) Token: 0x0600021C RID: 540 RVA: 0x00012B70 File Offset: 0x00010D70
			internal int StopMaintenanceThreshold
			{
				get
				{
					return this.stopMaintenanceThreshold;
				}
				set
				{
					this.stopMaintenanceThreshold = value;
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x0600021D RID: 541 RVA: 0x00012B79 File Offset: 0x00010D79
			// (set) Token: 0x0600021E RID: 542 RVA: 0x00012B81 File Offset: 0x00010D81
			internal int WlmMaintenanceThreshold
			{
				get
				{
					return this.wlmMaintenanceThreshold;
				}
				set
				{
					this.wlmMaintenanceThreshold = value;
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x0600021F RID: 543 RVA: 0x00012B8A File Offset: 0x00010D8A
			// (set) Token: 0x06000220 RID: 544 RVA: 0x00012B92 File Offset: 0x00010D92
			internal int NumberOfRecordsToMaintain
			{
				get
				{
					return this.numberOfRecordsToMaintain;
				}
				set
				{
					this.numberOfRecordsToMaintain = value;
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x06000221 RID: 545 RVA: 0x00012B9B File Offset: 0x00010D9B
			// (set) Token: 0x06000222 RID: 546 RVA: 0x00012BA3 File Offset: 0x00010DA3
			internal int NumberOfRecordsToReadFromMaintenanceTable
			{
				get
				{
					return this.numberOfRecordsToReadFromMaintenanceTable;
				}
				set
				{
					this.numberOfRecordsToReadFromMaintenanceTable = value;
				}
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x06000223 RID: 547 RVA: 0x00012BAC File Offset: 0x00010DAC
			// (set) Token: 0x06000224 RID: 548 RVA: 0x00012BB4 File Offset: 0x00010DB4
			internal TimeSpan MaintenanceTimePeriodToKeep
			{
				get
				{
					return this.maintenanceTimePeriodToKeep;
				}
				set
				{
					this.maintenanceTimePeriodToKeep = value;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000225 RID: 549 RVA: 0x00012BBD File Offset: 0x00010DBD
			// (set) Token: 0x06000226 RID: 550 RVA: 0x00012BC5 File Offset: 0x00010DC5
			internal int WlmMinNumberOfChunksToProceed
			{
				get
				{
					return this.wlmMinNumberOfChunksToProceed;
				}
				set
				{
					this.wlmMinNumberOfChunksToProceed = value;
				}
			}

			// Token: 0x04000138 RID: 312
			private int stopMaintenanceThreshold;

			// Token: 0x04000139 RID: 313
			private int wlmMaintenanceThreshold;

			// Token: 0x0400013A RID: 314
			private int numberOfRecordsToMaintain;

			// Token: 0x0400013B RID: 315
			private int numberOfRecordsToReadFromMaintenanceTable;

			// Token: 0x0400013C RID: 316
			private TimeSpan maintenanceTimePeriodToKeep;

			// Token: 0x0400013D RID: 317
			private int wlmMinNumberOfChunksToProceed;
		}
	}
}
