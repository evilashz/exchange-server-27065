using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000CA RID: 202
	public class SearchFolder : Folder
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x000542C0 File Offset: 0x000524C0
		internal SearchFolder(Context context, Mailbox mailbox, ExchangeId existingFolderId) : this(context, mailbox, null, existingFolderId, false, false, ExchangeId.Null, ExchangeId.Null)
		{
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000542E4 File Offset: 0x000524E4
		private SearchFolder(Context context, Mailbox mailbox, Folder parentFolder, ExchangeId folderId, bool newFolder, bool isInstantSearch, ExchangeId originalParentFolderId, ExchangeId originalFolderId) : base(context, mailbox, parentFolder, folderId, newFolder, false, originalParentFolderId, originalFolderId)
		{
			if (newFolder)
			{
				base.SetColumn(context, base.FolderTable.QueryCriteria, new RestrictionTrue().Serialize());
				this.SetRestrictionFiltersNothing(new bool?(true));
				base.UpdatePerUserReadUnreadTrackingEnabled(context);
				if (isInstantSearch)
				{
					base.SetColumn(context, base.FolderTable.SearchState, 131072);
				}
			}
			this.diagnostics = SearchExecutionDiagnostics.Create(mailbox.SharedState.DatabaseGuid, mailbox.SharedState.MailboxGuid, mailbox.SharedState.MailboxNumber, folderId);
			this.diagnostics.UpdateClientType(context.ClientType);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00054395 File Offset: 0x00052595
		public static SearchFolder CreateSearchFolder(Context context, Folder parentFolder, ExchangeId folderId)
		{
			return SearchFolder.CreateSearchFolder(context, parentFolder, folderId, false, null);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000543A4 File Offset: 0x000525A4
		private static int CountBacklinksPerScopeFolder(Context context, Mailbox mailbox, ExchangeId scopeFolderId)
		{
			Folder folder = Folder.OpenFolder(context, mailbox, scopeFolderId);
			if (folder == null)
			{
				throw new CorruptSearchScopeException((LID)40732U, "Scope folder is missing.");
			}
			List<ExchangeId> list = new List<ExchangeId>(folder.GetSearchBacklinks(context, false));
			int count = list.Count;
			list = new List<ExchangeId>(folder.GetSearchBacklinks(context, true));
			return count + list.Count;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x000543FF File Offset: 0x000525FF
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00054411 File Offset: 0x00052611
		private SearchFolder.FoldersScope CachedQueryScope
		{
			get
			{
				return (SearchFolder.FoldersScope)base.GetComponentData(SearchFolder.cachedQueryScopeDataSlot);
			}
			set
			{
				base.SetComponentData(SearchFolder.cachedQueryScopeDataSlot, value);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0005441F File Offset: 0x0005261F
		internal SearchExecutionDiagnostics Diagnostics
		{
			get
			{
				return this.diagnostics;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00054427 File Offset: 0x00052627
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x00054439 File Offset: 0x00052639
		internal SearchFolder.InstantSearchResults SearchResults
		{
			get
			{
				return (SearchFolder.InstantSearchResults)base.GetComponentData(SearchFolder.instantSearchResultsSlot);
			}
			set
			{
				base.SetComponentData(SearchFolder.instantSearchResultsSlot, value);
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00054448 File Offset: 0x00052648
		internal new static void Initialize()
		{
			if (SearchFolder.cachedQueryScopeDataSlot == -1)
			{
				SearchFolder.cachedQueryScopeDataSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				SearchFolder.restrictionFiltersNothingSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				SearchFolder.hasCountRestrictionSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				SearchFolder.instantSearchResultsSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				SearchFolder.searchFolderAgeOutMaintenance = MaintenanceHandler.RegisterMailboxMaintenance(SearchFolder.SearchFolderAgeOutMaintenanceId, RequiredMaintenanceResourceType.Store, false, new MaintenanceHandler.MailboxMaintenanceDelegate(SearchFolder.AgeOutMailboxSearchFolders), "SearchFolder.AgeOutMailboxSearchFolders");
				SearchFolder.markMailboxForSearchFolderAgeOutMaintenance = MaintenanceHandler.RegisterDatabaseMaintenance(SearchFolder.MarkMailboxForSearchFolderAgeOutMaintenanceId, RequiredMaintenanceResourceType.Store, new MaintenanceHandler.DatabaseMaintenanceDelegate(SearchFolder.MarkMailboxesForSearchFolderAgeOut), "SearchFolder.MarkMailboxesForSearchFolderAgeOut");
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000544C8 File Offset: 0x000526C8
		public static void MountedEventHandler(Context context)
		{
			if (ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, Guid>(0L, "Database {0} ({1}): Scheduling search folder age-out maintenance task.", context.Database.MdbName, context.Database.MdbGuid);
			}
			SearchFolder.markMailboxForSearchFolderAgeOutMaintenance.ScheduleMarkForMaintenance(context, TimeSpan.FromDays(1.0));
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00054524 File Offset: 0x00052724
		public static void MarkMailboxesForSearchFolderAgeOut(Context context, DatabaseInfo databaseInfo, out bool completed)
		{
			bool flag = ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (SearchFolder.IsSearchFolderAgeOutEnabled(context))
			{
				if (flag)
				{
					ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, Guid>(0L, "Database {0} ({1}): Search folder age-out maintenance task invoked. Beginning scan for active mailboxes.", context.Database.MdbName, context.Database.MdbGuid);
				}
				MaintenanceHandler.ApplyMaintenanceToActiveAndDeletedMailboxes(context, ExecutionDiagnostics.OperationSource.SearchFolderAgeOut, new Action<Context, MailboxState>(SearchFolder.CheckMailboxForSearchFolderAgeOut), out completed);
				if (flag)
				{
					ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, Guid>(0L, "Database {0} ({1}): The search folder age-out maintenance task has finished scanning active mailboxes. Terminating task.", context.Database.MdbName, context.Database.MdbGuid);
					return;
				}
			}
			else
			{
				completed = true;
				if (flag)
				{
					ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, Guid>(0L, "Database {0} ({1}): Search folder age-out maintenance task invoked, but age-out is disabled. Terminating task.", context.Database.MdbName, context.Database.MdbGuid);
				}
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000545DC File Offset: 0x000527DC
		internal static bool IsSearchFolderAgeOutEnabled(Context context)
		{
			return context.DatabaseType != DatabaseType.Sql && !ConfigurationSchema.DisableSearchFolderAgeOut.Value;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000545F8 File Offset: 0x000527F8
		internal static void CheckMailboxForSearchFolderAgeOut(Context context, MailboxState mailboxState)
		{
			using (Mailbox mailbox = Mailbox.OpenMailbox(context, mailboxState))
			{
				SearchFolder.CheckMailboxForSearchFolderAgeOut(context, mailbox);
				mailbox.Save(context);
				mailbox.Disconnect();
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00054640 File Offset: 0x00052840
		internal static void CheckMailboxForSearchFolderAgeOut(Context context, Mailbox mailbox)
		{
			bool flag = ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag)
			{
				ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<Guid, MailboxStatus>(0L, "Evaluating mailbox {0} (status: {1}) for possible search folder age-out.", mailbox.SharedState.MailboxGuid, mailbox.SharedState.Status);
			}
			FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(context.Database);
			using (TableOperator tableOperatorForSearchFolderAgeOut = SearchFolder.GetTableOperatorForSearchFolderAgeOut(context, mailbox.SharedState.MailboxPartitionNumber, folderTable))
			{
				using (Reader reader = tableOperatorForSearchFolderAgeOut.ExecuteReader(false))
				{
					while (reader.Read())
					{
						TimeSpan timeSpan = SearchFolder.ComputeAgeOutTimeoutOfSearchFolder(context, folderTable, reader);
						if (timeSpan != TimeSpan.Zero)
						{
							byte[] binary = reader.GetBinary(folderTable.FolderId);
							ExchangeId searchFolderId = ExchangeId.CreateFrom26ByteArray(context, null, binary);
							bool flag2 = SearchFolder.CheckSearchFolderShouldBeAgedOut(context, mailbox, searchFolderId, reader, timeSpan);
							if (flag2)
							{
								if (flag)
								{
									ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<Guid>(0L, "Marking mailbox {0} for search folder age-out.", mailbox.SharedState.MailboxGuid);
								}
								mailbox.SharedState.AddReference();
								try
								{
									SearchFolder.searchFolderAgeOutMaintenance.MarkForMaintenance(context, mailbox.SharedState);
									break;
								}
								finally
								{
									mailbox.SharedState.ReleaseReference();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00054788 File Offset: 0x00052988
		internal static void AgeOutMailboxSearchFolders(Context context, MailboxState mailboxState, out bool completed)
		{
			if (!mailboxState.IsAccessible || !SearchFolder.IsSearchFolderAgeOutEnabled(context))
			{
				if (ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<Guid, MailboxStatus>(0L, "The search folder age-out task for mailbox {0} (status: {1}) was invoked, but the mailbox is inaccessible or age-out has been disabled. Terminating task.", mailboxState.MailboxGuid, mailboxState.Status);
				}
				completed = true;
				return;
			}
			using (Mailbox mailbox = Mailbox.OpenMailbox(context, mailboxState))
			{
				completed = SearchFolder.AgeOutMailboxSearchFolders(context, mailbox, ExchangeId.Null);
				mailbox.Save(context);
				mailbox.Disconnect();
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00054814 File Offset: 0x00052A14
		private static bool AgeOutMailboxSearchFolders(Context context, Mailbox mailbox, ExchangeId skipExchangeId)
		{
			bool flag = true;
			int num = 0;
			bool flag2 = ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag2)
			{
				ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, Guid, MailboxStatus>(0L, "Mailbox {0} (guid: {1}, status: {2}): Scanning search folders for age-out candidates.", mailbox.GetDisplayName(context), mailbox.MailboxGuid, mailbox.SharedState.Status);
			}
			FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(context.Database);
			using (TableOperator tableOperatorForSearchFolderAgeOut = SearchFolder.GetTableOperatorForSearchFolderAgeOut(context, mailbox.MailboxPartitionNumber, folderTable))
			{
				using (Reader reader = tableOperatorForSearchFolderAgeOut.ExecuteReader(false))
				{
					while (reader.Read())
					{
						TimeSpan timeSpan = SearchFolder.ComputeAgeOutTimeoutOfSearchFolder(context, folderTable, reader);
						if (timeSpan != TimeSpan.Zero)
						{
							byte[] binary = reader.GetBinary(folderTable.FolderId);
							ExchangeId exchangeId = ExchangeId.CreateFrom26ByteArray(context, mailbox.ReplidGuidMap, binary);
							if (!skipExchangeId.Equals(exchangeId) && SearchFolder.CheckSearchFolderShouldBeAgedOut(context, mailbox, exchangeId, reader, timeSpan))
							{
								SearchFolder.AgeOutOneSearchFolder(context, mailbox, exchangeId);
								num++;
								if (num % SearchFolder.searchFolderAgeOutChunkSize.Value == 0)
								{
									mailbox.Save(context);
									context.Commit();
									if (MaintenanceHandler.ShouldStopMailboxMaintenanceTask(context, mailbox.SharedState, SearchFolder.SearchFolderAgeOutMaintenanceId))
									{
										flag = false;
										break;
									}
								}
							}
						}
					}
				}
			}
			if (flag2)
			{
				ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, Guid, bool>(0L, "Mailbox {0} ({1}): Finished scanning search folders for age-out candidates. Terminating task (completed?=={2}).", mailbox.GetDisplayName(context), mailbox.MailboxGuid, flag);
			}
			return flag;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00054980 File Offset: 0x00052B80
		private static TableOperator GetTableOperatorForSearchFolderAgeOut(Context context, int mailboxPartitionNumber, FolderTable folderTable)
		{
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailboxPartitionNumber
			});
			SearchCriteria restriction = Factory.CreateSearchCriteriaCompare(folderTable.QueryCriteria, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(null, folderTable.QueryCriteria));
			Column[] columnsToFetch = new Column[]
			{
				folderTable.FolderId,
				folderTable.SearchState,
				folderTable.FolderCount,
				PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.AllowAgeOut),
				PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.SearchFolderAgeOutTimeout),
				PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.CreationTime)
			};
			return Factory.CreateTableOperator(context.Culture, context, folderTable.Table, folderTable.FolderPK, columnsToFetch, restriction, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00054A50 File Offset: 0x00052C50
		private static TimeSpan ComputeAgeOutTimeoutOfSearchFolder(Context context, FolderTable folderTable, Reader reader)
		{
			long @int = reader.GetInt64(folderTable.FolderCount);
			SearchState valueOrDefault = (SearchState)reader.GetNullableInt32(folderTable.SearchState).GetValueOrDefault(0);
			TimeSpan timeSpan = TimeSpan.Zero;
			if (!SearchFolder.IsSearchEvaluationInProgress(valueOrDefault) && @int == 0L)
			{
				Column column = PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.SearchFolderAgeOutTimeout);
				int valueOrDefault2 = reader.GetNullableInt32(column).GetValueOrDefault(0);
				if (valueOrDefault2 > 0)
				{
					timeSpan = TimeSpan.FromSeconds((double)valueOrDefault2);
				}
				else if (SearchFolder.IsInstantSearch(valueOrDefault))
				{
					timeSpan = SearchFolder.TimeoutForInstantSearch;
				}
				else
				{
					Column column2 = PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.AllowAgeOut);
					bool valueOrDefault3 = reader.GetNullableBoolean(column2).GetValueOrDefault(false);
					if (valueOrDefault3)
					{
						timeSpan = SearchFolder.TimeoutForAllowAgeOut;
					}
				}
			}
			if (SearchFolder.validateComputedAgeOutTimeoutTestHook.Value != null)
			{
				SearchFolder.validateComputedAgeOutTimeoutTestHook.Value(reader, timeSpan);
			}
			return timeSpan;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00054B28 File Offset: 0x00052D28
		private static bool CheckSearchFolderShouldBeAgedOut(Context context, Mailbox mailbox, ExchangeId searchFolderId, Reader reader, TimeSpan ageOutTimeout)
		{
			DateTime mostRecentAccessTime = SearchFolder.GetMostRecentAccessTime(context, mailbox.SharedState, searchFolderId, reader);
			return SearchFolder.ShouldSearchFolderBeAgedOut(context, mailbox, searchFolderId, mostRecentAccessTime, ageOutTimeout);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00054B50 File Offset: 0x00052D50
		private static DateTime GetMostRecentAccessTime(Context context, MailboxState mailboxState, ExchangeId searchFolderId, Reader reader)
		{
			DateTime dateTime = LogicalIndexCache.GetMostRecentFolderViewAccessTime(context, mailboxState, searchFolderId);
			if (dateTime == DateTime.MinValue)
			{
				Column column = PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.CreationTime);
				dateTime = reader.GetDateTime(column);
			}
			if (SearchFolder.getMostRecentAccessTimeTestHook.Value != null)
			{
				dateTime = SearchFolder.getMostRecentAccessTimeTestHook.Value(searchFolderId, dateTime);
			}
			return dateTime;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00054BF4 File Offset: 0x00052DF4
		private static bool ShouldSearchFolderBeAgedOut(Context context, Mailbox mailbox, ExchangeId searchFolderId, DateTime lastAccessTime, TimeSpan ageOutTimeout)
		{
			bool flag;
			if (mailbox.SharedState.IsUserAccessible)
			{
				int num = 1;
				SearchFolder searchFolder = Folder.OpenFolder(context, mailbox, searchFolderId) as SearchFolder;
				if (searchFolder.HasDoNotDeleteReferences)
				{
					return false;
				}
				if (!searchFolder.IsStaticSearch(context))
				{
					IList<ExchangeId> folders = searchFolder.GetQueryScope(context).Folders;
					int dynamicSearchFoldersCountMax = 0;
					searchFolder.ExecuteOnBacklinksCountPerScopeFolder(context, searchFolder.IsRecursiveSearch(context), folders, delegate(ExchangeId scopeFolderId, int dynamicSearchFoldersCount)
					{
						if (ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, int>(0L, "Folder {0} has {1} backlinks.", scopeFolderId.ToString(), dynamicSearchFoldersCount);
						}
						dynamicSearchFoldersCountMax = Math.Max(dynamicSearchFoldersCountMax, dynamicSearchFoldersCount);
					});
					int value = ConfigurationSchema.DynamicSearchFolderPerScopeCountReceiveQuota.Value;
					if (dynamicSearchFoldersCountMax > value / 2)
					{
						num = 8;
					}
					else if (dynamicSearchFoldersCountMax > value / 4)
					{
						num = 4;
					}
					else if (dynamicSearchFoldersCountMax > value / 8)
					{
						num = 2;
					}
					else
					{
						num = 1;
					}
				}
				DateTime utcNow = DateTime.UtcNow;
				if (utcNow >= lastAccessTime)
				{
					flag = (TimeSpan.FromSeconds((utcNow - lastAccessTime).TotalSeconds * (double)num) > ageOutTimeout);
				}
				else
				{
					flag = (TimeSpan.FromSeconds((lastAccessTime - utcNow).TotalSeconds * (double)num) > SearchFolder.TimestampClockSkewAllowance);
				}
				if (ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug(0L, "Evaluated search folder {0} with age-out timeout of {1}, last-access time of {2} and aging  coefficient {3} and determined that it {4} be aged-out (UtcNow=={5}).", new object[]
					{
						searchFolderId.ToString(),
						ageOutTimeout,
						lastAccessTime,
						num,
						flag ? "SHOULD" : "SHOULD NOT",
						utcNow
					});
				}
			}
			else
			{
				flag = true;
				if (ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, MailboxStatus>(0L, "Unconditionally forcing search folder {0} to be aged-out because the mailbox status is {1}.", searchFolderId.ToString(), mailbox.SharedState.Status);
				}
			}
			if (SearchFolder.shouldSearchFolderBeAgedOutTestHook.Value != null)
			{
				bool flag2 = flag;
				flag = SearchFolder.shouldSearchFolderBeAgedOutTestHook.Value(mailbox.SharedState, searchFolderId, flag);
				if (ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace) && flag2 != flag)
				{
					ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, string>(0L, "The test hook has overridden the age-out decision for search folder {0} to now indicate that the search folder {1} be aged-out.", searchFolderId.ToString(), flag ? "SHOULD" : "SHOULD NOT");
				}
			}
			return flag;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00054E24 File Offset: 0x00053024
		public static void AgeOutOneSearchFolder(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			SearchFolder searchFolder = Folder.OpenFolder(context, mailbox, folderId) as SearchFolder;
			if (searchFolder != null)
			{
				if (ExTraceGlobals.SearchFolderAgeOutTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderAgeOutTracer.TraceDebug<string, ExchangeId>(0L, "Age-out is deleting search folder {0} ({1}).", searchFolder.GetName(context), folderId);
				}
				Folder folder = null;
				if (ConfigurationSchema.MaterializedRestrictionSearchFolderConfigStage.Value == 2)
				{
					Folder parentFolder = searchFolder.GetParentFolder(context);
					if (parentFolder != null)
					{
						Folder parentFolder2 = parentFolder.GetParentFolder(context);
						if (parentFolder2 != null)
						{
							ExchangeId[] specialFolders = SpecialFoldersCache.GetSpecialFolders(context, mailbox);
							if (parentFolder2.GetId(context) == specialFolders[21])
							{
								int num = (int)parentFolder.GetPropertyValue(context, PropTag.Folder.FolderChildCount);
								if (num == 1)
								{
									folder = parentFolder;
								}
							}
						}
					}
				}
				searchFolder.Delete(context);
				if (folder != null)
				{
					folder.Delete(context);
				}
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00054EE0 File Offset: 0x000530E0
		public static SearchFolder CreateSearchFolder(Context context, Folder parentFolder, ExchangeId folderId, bool isInstantSearch, SearchFolder sourceFolder)
		{
			if (!parentFolder.CheckAlive(context))
			{
				throw new StoreException((LID)35448U, ErrorCodeValue.ObjectDeleted, "parentFolder is dead");
			}
			ExchangeId originalParentFolderId = ExchangeId.Null;
			ExchangeId originalFolderId = ExchangeId.Null;
			if (sourceFolder != null)
			{
				if (!sourceFolder.CheckAlive(context))
				{
					throw new StoreException((LID)51832U, ErrorCodeValue.ObjectDeleted, "sourceFolder is dead");
				}
				originalFolderId = sourceFolder.GetId(context);
				originalParentFolderId = ExchangeId.Zero;
				if (sourceFolder.GetParentFolder(context) != null)
				{
					originalParentFolderId = sourceFolder.GetParentFolder(context).GetId(context);
				}
			}
			if (folderId.IsNullOrZero)
			{
				folderId = parentFolder.Mailbox.GetNextObjectId(context);
			}
			SearchFolder searchFolder = new SearchFolder(context, parentFolder.Mailbox, parentFolder, folderId, true, isInstantSearch, originalParentFolderId, originalFolderId);
			searchFolder.Save(context);
			return searchFolder;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00054FA0 File Offset: 0x000531A0
		public static void TrackSearchFolderUpdate(Context context, Folder folder, TopMessage message, LogicalIndex.LogicalOperation operation, Guid? userIdentityContext, ModifiedSearchFolders modifiedSearchFolders)
		{
			using (context.GrantPartitionFullAccess())
			{
				switch (operation)
				{
				case LogicalIndex.LogicalOperation.Insert:
					SearchFolder.ProcessSearchFolderInsert(context, folder, message, false, userIdentityContext, modifiedSearchFolders);
					SearchFolder.ProcessSearchFolderInsert(context, folder, message, true, userIdentityContext, modifiedSearchFolders);
					break;
				case LogicalIndex.LogicalOperation.Update:
					SearchFolder.ProcessSearchFolderUpdate(context, folder, message, false, false, userIdentityContext, modifiedSearchFolders);
					SearchFolder.ProcessSearchFolderUpdate(context, folder, message, true, false, userIdentityContext, modifiedSearchFolders);
					break;
				case LogicalIndex.LogicalOperation.Delete:
					SearchFolder.ProcessSearchFolderDelete(context, folder, message, false, false, userIdentityContext, modifiedSearchFolders);
					SearchFolder.ProcessSearchFolderDelete(context, folder, message, true, false, userIdentityContext, modifiedSearchFolders);
					break;
				}
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00055044 File Offset: 0x00053244
		public static void UpdateRecursiveSearchBacklinksForMove(Context context, Folder folderToMove, Folder sourceParent, Folder destinationParent)
		{
			using (context.GrantPartitionFullAccess())
			{
				ExchangeId id = folderToMove.GetId(context);
				IList<ExchangeId> searchBacklinks = sourceParent.GetSearchBacklinks(context, true);
				IList<ExchangeId> searchBacklinks2 = destinationParent.GetSearchBacklinks(context, true);
				IList<ExchangeId> recursivelySearchedFolders = SearchFolder.FoldersScope.ExpandRecursivelySearchedFolders(context, folderToMove.Mailbox, new ExchangeId[]
				{
					id
				});
				foreach (ExchangeId exchangeId in searchBacklinks)
				{
					if (!searchBacklinks2.Contains(exchangeId))
					{
						SearchFolder searchFolder = Folder.OpenFolder(context, folderToMove.Mailbox, exchangeId) as SearchFolder;
						if (searchFolder == null)
						{
							SearchFolder.ReportCorruptSearchBacklink(context, sourceParent, exchangeId, true);
							throw new CorruptSearchBacklinkException((LID)45688U, "Backlink folder doesn't exist or is not a search folder.");
						}
						IList<ExchangeId> scopeFolders = searchFolder.GetScopeFolders(context);
						if (!scopeFolders.Contains(id))
						{
							searchFolder.RemoveFromRecursiveSearch(context, recursivelySearchedFolders);
							searchFolder.InvalidateCachedQueryScope(context);
						}
					}
				}
				foreach (ExchangeId exchangeId2 in searchBacklinks2)
				{
					if (!searchBacklinks.Contains(exchangeId2))
					{
						SearchFolder searchFolder2 = Folder.OpenFolder(context, folderToMove.Mailbox, exchangeId2) as SearchFolder;
						if (searchFolder2 == null)
						{
							SearchFolder.ReportCorruptSearchBacklink(context, destinationParent, exchangeId2, true);
							throw new CorruptSearchBacklinkException((LID)62072U, "Backlink folder doesn't exist or is not a search folder.");
						}
						IList<ExchangeId> scopeFolders2 = searchFolder2.GetScopeFolders(context);
						if (!scopeFolders2.Contains(id))
						{
							searchFolder2.AddToRecursiveSearch(context, recursivelySearchedFolders);
							searchFolder2.InvalidateCachedQueryScope(context);
						}
					}
				}
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00055218 File Offset: 0x00053418
		public static void ProcessFolderDeletion(Context context, Folder folder)
		{
			using (context.GrantPartitionFullAccess())
			{
				SearchFolder.RemoveFromSearchScopes(context, folder, false);
				SearchFolder.RemoveFromSearchScopes(context, folder, true);
				SearchFolder searchFolder = folder as SearchFolder;
				if (searchFolder != null)
				{
					if (!searchFolder.IsStaticSearch(context))
					{
						searchFolder.RemoveSearchBacklinks(context);
					}
					int? logicalIndexNumber = searchFolder.GetLogicalIndexNumber(context);
					if (logicalIndexNumber != null)
					{
						LogicalIndexCache.DeleteIndex(context, searchFolder.Mailbox, searchFolder.GetId(context), logicalIndexNumber.Value);
					}
				}
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000552A4 File Offset: 0x000534A4
		internal static void RemoveFromSearchScopes(Context context, Folder folder, bool recursiveSearches)
		{
			ExchangeId id = folder.GetId(context);
			IList<ExchangeId> searchBacklinks = folder.GetSearchBacklinks(context, recursiveSearches);
			foreach (ExchangeId exchangeId in searchBacklinks)
			{
				SearchFolder searchFolder = Folder.OpenFolder(context, folder.Mailbox, exchangeId) as SearchFolder;
				if (searchFolder == null)
				{
					SearchFolder.ReportCorruptSearchBacklink(context, folder, exchangeId, recursiveSearches);
					throw new CorruptSearchBacklinkException((LID)37496U, "Backlink folder doesn't exist or is not a search folder.");
				}
				List<ExchangeId> list = new List<ExchangeId>(searchFolder.GetScopeFolders(context));
				if (list.Remove(id))
				{
					if (searchFolder.IsRecursiveSearch(context) != recursiveSearches)
					{
						SearchFolder.ReportCorruptSearchBacklink(context, folder, exchangeId, recursiveSearches);
						throw new CorruptSearchBacklinkException((LID)53880U, "Type of backlink doesn't match type of search.");
					}
					searchFolder.SetColumn(context, folder.FolderTable.ScopeFolders, ExchangeIdListHelpers.BytesFromList(list, true));
					searchFolder.InvalidateCachedQueryScope(context);
				}
				else
				{
					if (!recursiveSearches)
					{
						SearchFolder.ReportCorruptSearchBacklink(context, folder, exchangeId, recursiveSearches);
						throw new CorruptSearchBacklinkException((LID)41592U, "Folder has backlink to a non-recursive search, but the search does not scope the folder");
					}
					searchFolder.InvalidateCachedQueryScope(context);
				}
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000553C0 File Offset: 0x000535C0
		public static bool ValidateDynamicSearchScope(Context context, Mailbox mailbox, ExchangeId searchFolderId)
		{
			SearchFolder searchFolder = Folder.OpenFolder(context, mailbox, searchFolderId) as SearchFolder;
			if (searchFolder == null)
			{
				return false;
			}
			SearchState searchState = searchFolder.GetSearchState(context);
			if (!SearchFolder.IsValidSearchState(searchState))
			{
				return false;
			}
			if (SearchFolder.IsStaticSearch(searchState))
			{
				return true;
			}
			if (SearchFolder.IsSearchEvaluationInProgress(searchState))
			{
				return true;
			}
			bool flag = SearchFolder.IsRecursiveSearch(searchState);
			IList<ExchangeId> list = flag ? SearchFolder.FoldersScope.ExpandRecursivelySearchedFolders(context, mailbox, searchFolder.GetScopeFolders(context)) : searchFolder.GetScopeFolders(context);
			IList<ExchangeId> folders = searchFolder.GetQueryScope(context).Folders;
			if (list.Count > 0)
			{
				if (!SearchFolder.IsRunningSearch(searchState))
				{
					return false;
				}
				foreach (ExchangeId exchangeId in list)
				{
					if (!folders.Contains(exchangeId))
					{
						return false;
					}
					Folder folder = Folder.OpenFolder(context, mailbox, exchangeId);
					if (folder == null)
					{
						return false;
					}
					if (!folder.GetSearchBacklinks(context, flag).Contains(searchFolderId))
					{
						return false;
					}
					if (folder.GetSearchBacklinks(context, !flag).Contains(searchFolderId))
					{
						return false;
					}
					SearchFolder searchFolder2 = folder as SearchFolder;
					if (searchFolder2 != null)
					{
						if (list.Count > 1)
						{
							return false;
						}
						if (flag)
						{
							return false;
						}
						if (!SearchFolder.IsValidSearchState(searchFolder2.GetSearchState(context)))
						{
							return false;
						}
					}
				}
				foreach (ExchangeId item in folders)
				{
					if (!list.Contains(item))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0005556C File Offset: 0x0005376C
		public static bool ValidateSearchBacklinks(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			Folder folder = Folder.OpenFolder(context, mailbox, folderId);
			return folder != null && SearchFolder.ValidateSearchBacklinks(context, folder, false) && SearchFolder.ValidateSearchBacklinks(context, folder, true);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000555A0 File Offset: 0x000537A0
		private static bool ValidateSearchBacklinks(Context context, Folder folder, bool recursive)
		{
			IList<ExchangeId> searchBacklinks = folder.GetSearchBacklinks(context, recursive);
			if (searchBacklinks.Count > 0)
			{
				ExchangeId id = folder.GetId(context);
				if (recursive && folder is SearchFolder)
				{
					return false;
				}
				foreach (ExchangeId exchangeId in searchBacklinks)
				{
					SearchFolder searchFolder = Folder.OpenFolder(context, folder.Mailbox, exchangeId) as SearchFolder;
					if (searchFolder == null)
					{
						return false;
					}
					if (searchFolder.IsStaticSearch(context))
					{
						return false;
					}
					if (!searchFolder.IsFolderInSearchScope(context, id))
					{
						return false;
					}
					if (!SearchFolder.ValidateDynamicSearchScope(context, folder.Mailbox, exchangeId))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00055660 File Offset: 0x00053860
		public static bool IsFolderBacklinkedToSearch(Context context, Mailbox mailbox, ExchangeId folderId, ExchangeId searchFolderId)
		{
			bool result = false;
			Folder folder = Folder.OpenFolder(context, mailbox, folderId);
			if (folder != null)
			{
				if (folder.GetSearchBacklinks(context, false).Contains(searchFolderId))
				{
					result = true;
				}
				else if (folder.GetSearchBacklinks(context, true).Contains(searchFolderId))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000556A4 File Offset: 0x000538A4
		public static bool CriteriaMustBeHandledByFullTextIndex(SearchCriteria criteria, Guid mdbGuid)
		{
			bool flag;
			return FullTextIndexSchema.Current.GetCriteriaFullTextFlavor(criteria, mdbGuid, out flag) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000556C4 File Offset: 0x000538C4
		private static void ReportCorruptSearchBacklink(Context context, Folder folderWithCorruptBacklink, ExchangeId searchFolderId, bool recursive)
		{
			Mailbox mailbox = folderWithCorruptBacklink.Mailbox;
			StoreDatabase database = mailbox.Database;
			Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_CorruptSearchBacklink, new object[]
			{
				database.MdbGuid,
				database.MdbName,
				mailbox.MailboxGuid,
				mailbox.GetDisplayName(context),
				mailbox.SharedState.TenantHint,
				folderWithCorruptBacklink.GetId(context),
				folderWithCorruptBacklink.GetName(context),
				searchFolderId,
				recursive
			});
			FaultInjection.InjectFault(SearchFolder.reportCorruptSearchBacklinkTestHook);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0005576C File Offset: 0x0005396C
		internal void Repopulate(Context context)
		{
			SetSearchCriteriaFlags searchCriteriaFlags = this.GetSearchCriteriaFlags(context) | SetSearchCriteriaFlags.Restart;
			IList<ExchangeId> scopeFolders = this.GetScopeFolders(context);
			byte[] serializedRestriction = this.GetSerializedRestriction(context);
			bool isInstantSearch = this.IsInstantSearch(context);
			this.SetSearchCriteria(context, serializedRestriction, scopeFolders, searchCriteriaFlags, isInstantSearch, false, false);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000557A8 File Offset: 0x000539A8
		internal SearchFolder.FoldersScope GetQueryScope(Context context)
		{
			if (this.CachedQueryScope == null)
			{
				this.UpdateCachedQueryScope(context);
			}
			return this.CachedQueryScope;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000557BF File Offset: 0x000539BF
		public int? GetLogicalIndexNumber(Context context)
		{
			return (int?)base.GetColumnValue(context, base.FolderTable.LogicalIndexNumber);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000557D8 File Offset: 0x000539D8
		public int GetAgeOutTimeout(Context context)
		{
			return ((int?)this.GetPropertyValue(context, PropTag.Folder.SearchFolderAgeOutTimeout)).GetValueOrDefault(0);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00055800 File Offset: 0x00053A00
		private static bool IsValidSearchState(SearchState searchState)
		{
			if (SearchFolder.IsInErrorState(searchState))
			{
				if (SearchFolder.IsSearchEvaluationInProgress(searchState) || SearchFolder.IsRunningSearch(searchState) || !SearchFolder.IsStaticSearch(searchState))
				{
					return false;
				}
			}
			else if (SearchFolder.IsSearchEvaluationInProgress(searchState))
			{
				if (!SearchFolder.IsRunningSearch(searchState))
				{
					return false;
				}
			}
			else if (SearchFolder.IsRunningSearch(searchState))
			{
				if (SearchFolder.IsStaticSearch(searchState))
				{
					return false;
				}
				if (!SearchFolder.IsCiSearch(searchState) && !SearchFolder.IsTwirSearch(searchState))
				{
					return false;
				}
			}
			else if (SearchFolder.IsStaticSearch(searchState))
			{
				if (!SearchFolder.IsCiSearch(searchState) && !SearchFolder.IsTwirSearch(searchState))
				{
					return false;
				}
			}
			else if (searchState != SearchState.None)
			{
			}
			return (!SearchFolder.IsCiSearch(searchState) || !SearchFolder.IsTwirSearch(searchState)) && (!SearchFolder.IsStatisticsOnlySearch(searchState) || SearchFolder.IsStaticSearch(searchState));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000558AF File Offset: 0x00053AAF
		public bool IsRunningSearch(Context context)
		{
			return SearchFolder.IsRunningSearch(this.GetSearchState(context));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000558BD File Offset: 0x00053ABD
		private static bool IsRunningSearch(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.Running);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000558C8 File Offset: 0x00053AC8
		public bool IsSearchEvaluationInProgress(Context context)
		{
			return SearchFolder.IsSearchEvaluationInProgress(this.GetSearchState(context));
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000558D6 File Offset: 0x00053AD6
		private static bool IsSearchEvaluationInProgress(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.Rebuild);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000558E1 File Offset: 0x00053AE1
		public bool IsRecursiveSearch(Context context)
		{
			return SearchFolder.IsRecursiveSearch(this.GetSearchState(context));
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000558EF File Offset: 0x00053AEF
		private static bool IsRecursiveSearch(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.Recursive);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000558FA File Offset: 0x00053AFA
		public bool IsForegroundSearch(Context context)
		{
			return SearchFolder.IsForegroundSearch(this.GetSearchState(context));
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00055908 File Offset: 0x00053B08
		private static bool IsForegroundSearch(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.Foreground);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00055914 File Offset: 0x00053B14
		public bool IsStaticSearch(Context context)
		{
			return SearchFolder.IsStaticSearch(this.GetSearchState(context));
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00055922 File Offset: 0x00053B22
		private static bool IsStaticSearch(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.Static);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00055931 File Offset: 0x00053B31
		public bool IsStatisticsOnlySearch(Context context)
		{
			return SearchFolder.IsStatisticsOnlySearch(this.GetSearchState(context));
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0005593F File Offset: 0x00053B3F
		private static bool IsStatisticsOnlySearch(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.StatisticsOnly);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0005594E File Offset: 0x00053B4E
		public bool IsCiSearch(Context context)
		{
			return SearchFolder.IsCiSearch(this.GetSearchState(context));
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0005595C File Offset: 0x00053B5C
		private static bool IsCiSearch(SearchState searchState)
		{
			return SearchState.None != (searchState & (SearchState.CiTotally | SearchState.CiWithTwirResidual | SearchState.TwirMostly));
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0005596B File Offset: 0x00053B6B
		public bool IsTwirSearch(Context context)
		{
			return SearchFolder.IsTwirSearch(this.GetSearchState(context));
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00055979 File Offset: 0x00053B79
		private static bool IsTwirSearch(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.TwirTotally);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00055988 File Offset: 0x00053B88
		public bool IsInstantSearch(Context context)
		{
			return SearchFolder.IsInstantSearch(this.GetSearchState(context));
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00055996 File Offset: 0x00053B96
		private static bool IsInstantSearch(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.InstantSearch);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000559A5 File Offset: 0x00053BA5
		public bool IsInErrorState(Context context)
		{
			return SearchFolder.IsInErrorState(this.GetSearchState(context));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000559B3 File Offset: 0x00053BB3
		private static bool IsInErrorState(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.Error);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000559C2 File Offset: 0x00053BC2
		public bool IsFullTextIndexQueryFailed(Context context)
		{
			return SearchFolder.IsFullTextIndexQueryFailed(this.GetSearchState(context));
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000559D0 File Offset: 0x00053BD0
		private static bool IsFullTextIndexQueryFailed(SearchState searchState)
		{
			return SearchState.None != (searchState & SearchState.FullTextIndexQueryFailed);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000559E0 File Offset: 0x00053BE0
		private static void ProcessSearchFolderDelete(Context context, Folder folder, TopMessage deletedMessage, bool recursive, bool forceProbing, Guid? userIdentityContext, ModifiedSearchFolders modifiedSearchFolders)
		{
			IList<ExchangeId> searchBacklinks = folder.GetSearchBacklinks(context, recursive);
			foreach (ExchangeId exchangeId in searchBacklinks)
			{
				SearchFolder searchFolder = Folder.OpenFolder(context, deletedMessage.Mailbox, exchangeId) as SearchFolder;
				if (searchFolder == null)
				{
					SearchFolder.ReportCorruptSearchBacklink(context, folder, exchangeId, recursive);
					throw new CorruptSearchBacklinkException((LID)33400U, "Backlink folder doesn't exist or is not a search folder.");
				}
				if (searchFolder.GetLogicalIndexNumber(context) != null && searchFolder.IsUpdateApplicableToFolder(context, userIdentityContext))
				{
					using (searchFolder.SetUserIdentityOnContext(context))
					{
						bool flag = false;
						bool flag2 = forceProbing || searchFolder.IsProbeForMessageLinkNeeded(context, folder);
						if (flag2)
						{
							if (searchFolder.LookupMessageByMid(context, deletedMessage.OriginalMessageID, new bool?(deletedMessage.GetIsHidden(context))) != null)
							{
								flag = true;
							}
						}
						else if (searchFolder.TestMessage(context, deletedMessage.OriginalBag))
						{
							flag = true;
						}
						if (flag)
						{
							searchFolder.RemoveMessageLink(context, deletedMessage, flag2, userIdentityContext, modifiedSearchFolders);
						}
					}
				}
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00055B10 File Offset: 0x00053D10
		private static void ProcessSearchFolderInsert(Context context, Folder folder, TopMessage insertedMessage, bool recursive, Guid? userIdentityContext, ModifiedSearchFolders modifiedSearchFolders)
		{
			IList<ExchangeId> searchBacklinks = folder.GetSearchBacklinks(context, recursive);
			foreach (ExchangeId exchangeId in searchBacklinks)
			{
				SearchFolder searchFolder = Folder.OpenFolder(context, insertedMessage.Mailbox, exchangeId) as SearchFolder;
				if (searchFolder == null)
				{
					SearchFolder.ReportCorruptSearchBacklink(context, folder, exchangeId, recursive);
					throw new CorruptSearchBacklinkException((LID)49784U, "Backlink folder doesn't exist or is not a search folder.");
				}
				if (searchFolder.GetLogicalIndexNumber(context) != null && searchFolder.IsUpdateApplicableToFolder(context, userIdentityContext))
				{
					using (searchFolder.SetUserIdentityOnContext(context))
					{
						if (searchFolder.TestMessage(context, insertedMessage))
						{
							searchFolder.AddMessageLink(context, insertedMessage, userIdentityContext, modifiedSearchFolders);
						}
					}
				}
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00055BEC File Offset: 0x00053DEC
		private static void ProcessSearchFolderUpdate(Context context, Folder folder, TopMessage updatedMessage, bool recursive, bool forceProbing, Guid? userIdentityContext, ModifiedSearchFolders modifiedSearchFolders)
		{
			IList<ExchangeId> searchBacklinks = folder.GetSearchBacklinks(context, recursive);
			foreach (ExchangeId exchangeId in searchBacklinks)
			{
				SearchFolder searchFolder = Folder.OpenFolder(context, updatedMessage.Mailbox, exchangeId) as SearchFolder;
				if (searchFolder == null)
				{
					SearchFolder.ReportCorruptSearchBacklink(context, folder, exchangeId, recursive);
					throw new CorruptSearchBacklinkException((LID)48632U, "Backlink folder doesn't exist or is not a search folder.");
				}
				if (searchFolder.GetLogicalIndexNumber(context) != null && searchFolder.IsUpdateApplicableToFolder(context, userIdentityContext))
				{
					using (searchFolder.SetUserIdentityOnContext(context))
					{
						bool flag = false;
						bool flag2 = forceProbing || searchFolder.IsProbeForMessageLinkNeeded(context, folder);
						if (flag2)
						{
							if (searchFolder.LookupMessageByMid(context, updatedMessage.OriginalMessageID, new bool?(updatedMessage.GetIsHidden(context))) != null)
							{
								flag = true;
							}
						}
						else if (searchFolder.TestMessage(context, updatedMessage.OriginalBag))
						{
							flag = true;
						}
						bool flag3 = (flag && SearchFolder.IsCiSearch(searchFolder.GetSearchState(context))) || searchFolder.TestMessage(context, updatedMessage);
						if (flag)
						{
							if (flag3)
							{
								searchFolder.UpdateMessageLink(context, updatedMessage, flag2, userIdentityContext, modifiedSearchFolders);
							}
							else
							{
								searchFolder.RemoveMessageLink(context, updatedMessage, flag2, userIdentityContext, modifiedSearchFolders);
							}
						}
						else if (flag3)
						{
							searchFolder.AddMessageLink(context, updatedMessage, userIdentityContext, modifiedSearchFolders);
						}
						if (userIdentityContext != null && (flag || flag3))
						{
							searchFolder.Save(context);
						}
					}
				}
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00056234 File Offset: 0x00054434
		private static IEnumerator<MailboxTaskQueue.TaskStepResult> TryDoSearchPopulation(MailboxTaskContext context, Mailbox mailbox, ExchangeId searchFolderId, SearchFolder.InstantSearchResults instantSearchResults, Func<bool> shouldTaskContinue)
		{
			if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<int, ExchangeId>(0L, "Mailbox {0}: Start population of search folder, ID {1}", mailbox.MailboxNumber, searchFolderId);
			}
			int populationAttempts;
			bool inSearchQueue = SearchQueue.IsInSearchQueue(context, mailbox, searchFolderId, out populationAttempts);
			if (inSearchQueue)
			{
				populationAttempts++;
				SearchQueue.UpdatePopulationAttempts(context, mailbox, searchFolderId, populationAttempts);
				mailbox.Save(context);
				context.Commit();
			}
			bool markSearchPopulationFailed = false;
			SearchFolder searchFolder = Folder.OpenFolder(context, mailbox, searchFolderId) as SearchFolder;
			if (searchFolder != null)
			{
				if (!mailbox.SharedState.IsUserAccessible || mailbox.SharedState.Quarantined)
				{
					markSearchPopulationFailed = true;
				}
				else if (searchFolder.IsSearchEvaluationInProgress(context))
				{
					if (!inSearchQueue)
					{
						markSearchPopulationFailed = true;
					}
					else if (!searchFolder.CheckAlive(context))
					{
						markSearchPopulationFailed = true;
					}
					else if (searchFolder.GetLogicalIndexNumber(context) == null)
					{
						markSearchPopulationFailed = true;
					}
					else if (populationAttempts > 2)
					{
						markSearchPopulationFailed = true;
					}
					else if (populationAttempts > 1 && searchFolder.IsInstantSearch(context))
					{
						markSearchPopulationFailed = true;
					}
					else
					{
						MailboxState mailboxState = searchFolder.Mailbox.SharedState;
						bool searchPopulationSucceeded = false;
						try
						{
							using (IEnumerator<MailboxTaskQueue.TaskStepResult> stepResults = searchFolder.DoSearchPopulation(context, instantSearchResults, shouldTaskContinue))
							{
								while (stepResults.MoveNext())
								{
									MailboxTaskQueue.TaskStepResult taskStepResult = stepResults.Current;
									yield return taskStepResult;
								}
							}
							searchPopulationSucceeded = true;
						}
						finally
						{
							if (!searchPopulationSucceeded)
							{
								if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<int, int, ExchangeId>(0L, "Mailbox {0}: Search population (attempt #{1}) of search folder {2} raised an exception. Another task will be queued to retry search population.", mailboxState.MailboxNumber, populationAttempts, searchFolderId);
								}
								bool flag = false;
								bool commit = false;
								try
								{
									if (!context.IsMailboxOperationStarted)
									{
										ErrorCode first = context.StartMailboxOperationForFailureHandling();
										if (first == ErrorCode.NoError)
										{
											flag = true;
										}
									}
									if (context.IsMailboxOperationStarted)
									{
										searchFolder.LaunchSearchPopulationTask(context, mailboxState, searchFolderId);
									}
									commit = true;
								}
								finally
								{
									if (flag)
									{
										context.EndMailboxOperation(commit);
									}
								}
							}
						}
					}
				}
			}
			if (markSearchPopulationFailed)
			{
				searchFolder.MarkSearchPopulationFailed(context, false);
				if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<int, ExchangeId, int>(0L, "Mailbox {0}: Search population of search folder {1} failed after {2} attempts. No further attempts will be made and the search folder was placed in the Error state.", mailbox.MailboxNumber, searchFolderId, inSearchQueue ? (populationAttempts - 1) : 0);
				}
			}
			if (inSearchQueue)
			{
				SearchQueue.RemoveFromSearchQueue(context, mailbox, searchFolderId);
			}
			yield break;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00056270 File Offset: 0x00054470
		private static void ExecuteAllFullTextIndexQueries(Context context, Mailbox mailbox, IList<StoreFullTextIndexQuery> fullTextIndexQueries, Func<bool> shouldTaskContinue, SearchExecutionDiagnostics diagnostics)
		{
			foreach (StoreFullTextIndexQuery storeFullTextIndexQuery in fullTextIndexQueries)
			{
				if (!shouldTaskContinue())
				{
					throw new StoreException((LID)57648U, ErrorCodeValue.DismountInProgress);
				}
				ErrorCode errorCode = FaultInjection.InjectError(SearchFolder.pulseForBaseViewPopulationTestHook);
				if (errorCode != ErrorCode.NoError)
				{
					throw new StoreException((LID)33072U, errorCode);
				}
				storeFullTextIndexQuery.ExecuteAll(context, mailbox.SharedState, diagnostics);
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0005630C File Offset: 0x0005450C
		private static void ExecuteRefillFullTextIndexQuery(Context context, Mailbox mailbox, StoreFullTextIndexQuery fullTextIndexQuery, bool needConversationDocumentId, Func<bool> shouldTaskContinue, SearchExecutionDiagnostics diagnostics)
		{
			if (!shouldTaskContinue())
			{
				throw new StoreException((LID)49456U, ErrorCodeValue.DismountInProgress);
			}
			ErrorCode errorCode = FaultInjection.InjectError(SearchFolder.pulseForBaseViewPopulationTestHook);
			if (errorCode != ErrorCode.NoError)
			{
				throw new StoreException((LID)48944U, errorCode);
			}
			fullTextIndexQuery.ExecuteOnePage(context, mailbox.SharedState, needConversationDocumentId, diagnostics);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00056378 File Offset: 0x00054578
		private static void EmptyPulseCallback(Context context, Func<bool> shouldTaskContinue)
		{
			if (!shouldTaskContinue())
			{
				throw new StoreException((LID)39776U, ErrorCodeValue.DismountInProgress);
			}
			ErrorCode errorCode = FaultInjection.InjectError(SearchFolder.pulseForBaseViewPopulationTestHook);
			if (errorCode != ErrorCode.NoError)
			{
				throw new StoreException((LID)56160U, errorCode);
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000563D0 File Offset: 0x000545D0
		private static IList<Column> GetColumnsToFetchForBaseViewPopulation(Context context, Mailbox mailbox, LogicalIndex logicalIndex)
		{
			if (logicalIndex == null)
			{
				throw new StoreException((LID)39808U, ErrorCodeValue.CorruptData, "Base view logical index was not found.");
			}
			IList<Column> list = new List<Column>(7);
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			ConstantColumn item = Factory.CreateConstantColumn(logicalIndex.MailboxPartitionNumber, logicalIndex.IndexTable.Columns[0]);
			ConstantColumn item2 = Factory.CreateConstantColumn(logicalIndex.LogicalIndexNumber, logicalIndex.IndexTable.Columns[1]);
			list.Add(item);
			list.Add(item2);
			list.Add(messageTable.IsHidden);
			list.Add(messageTable.MessageId);
			list.Add(messageTable.MessageDocumentId);
			if (context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
			{
				list.Add(messageTable.Size);
				list.Add(messageTable.IsRead);
				bool flag = true;
				if (mailbox.SharedState.SupportsPerUserFeatures)
				{
					list.Add(messageTable.FolderId);
					list.Add(messageTable.LcnCurrent);
					if (!flag)
					{
						list.Add(messageTable.IsRead);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000564EA File Offset: 0x000546EA
		protected override bool NeedBumpChangeNumber(StorePropTag propTag)
		{
			return !(propTag == PropTag.Folder.SearchFolderMsgCount) && base.NeedBumpChangeNumber(propTag);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00056504 File Offset: 0x00054704
		protected override bool NeedBumpChangeNumber(Column column)
		{
			return !(column == base.FolderTable.QueryCriteria) && !(column == base.FolderTable.SearchState) && !(column == base.FolderTable.SetSearchCriteriaFlags) && !(column == base.FolderTable.ScopeFolders) && !(column == base.FolderTable.LogicalIndexNumber) && base.NeedBumpChangeNumber(column);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0005657C File Offset: 0x0005477C
		private bool IsUpdateApplicableToFolder(Context context, Guid? userIdentityContext)
		{
			bool result = true;
			if (userIdentityContext != null)
			{
				Guid? nullableSearchGuid = this.GetNullableSearchGuid(context);
				result = (nullableSearchGuid == null || userIdentityContext == nullableSearchGuid);
			}
			return result;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000565E0 File Offset: 0x000547E0
		private void RemoveMessageLink(Context context, TopMessage deletedMessage, bool forceProbing, Guid? userIdentityContext, ModifiedSearchFolders modifiedSearchFolders)
		{
			Mailbox mailbox = deletedMessage.Mailbox;
			LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, mailbox, base.GetId(context), this.GetLogicalIndexNumber(context).Value);
			if (logicalIndex == null || logicalIndex.IsStale)
			{
				return;
			}
			Guid? nullableSearchGuid = this.GetNullableSearchGuid(context);
			if (userIdentityContext == null || nullableSearchGuid != null)
			{
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.LogicalIndex, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					logicalIndex.LogUpdate(context, deletedMessage, LogicalIndex.LogicalOperation.Delete);
				}
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.SearchFolder, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					base.ItemDeleted(context, deletedMessage);
				}
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.LogicalIndex, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					LogicalIndexCache.TrackIndexUpdate(context, mailbox, base.GetId(context), LogicalIndexType.SearchFolderMessages, LogicalIndex.LogicalOperation.Delete, deletedMessage);
				}
				modifiedSearchFolders.DeletedFrom.Add(base.GetId(context));
				SearchFolder.ProcessSearchFolderDelete(context, this, deletedMessage, false, forceProbing, userIdentityContext, modifiedSearchFolders);
				MessageDeletedNotificationEvent nev = NotificationEvents.CreateMessageDeletedEvent(context, deletedMessage, this, userIdentityContext);
				context.RiseNotificationEvent(nev);
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00056738 File Offset: 0x00054938
		private void AddMessageLink(Context context, TopMessage insertedMessage, Guid? userIdentityContext, ModifiedSearchFolders modifiedSearchFolders)
		{
			Mailbox mailbox = insertedMessage.Mailbox;
			LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, mailbox, base.GetId(context), this.GetLogicalIndexNumber(context).Value);
			if (logicalIndex == null || logicalIndex.IsStale)
			{
				return;
			}
			Guid? nullableSearchGuid = this.GetNullableSearchGuid(context);
			if (userIdentityContext == null || nullableSearchGuid != null)
			{
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.LogicalIndex, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					logicalIndex.LogUpdate(context, insertedMessage, LogicalIndex.LogicalOperation.Insert);
				}
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.SearchFolder, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					base.ItemInserted(context, insertedMessage);
				}
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.LogicalIndex, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					LogicalIndexCache.TrackIndexUpdate(context, mailbox, base.GetId(context), LogicalIndexType.SearchFolderMessages, LogicalIndex.LogicalOperation.Insert, insertedMessage);
				}
				if (modifiedSearchFolders.DeletedFrom.Contains(base.GetId(context)))
				{
					modifiedSearchFolders.DeletedFrom.Remove(base.GetId(context));
					modifiedSearchFolders.Updated.Add(base.GetId(context));
				}
				else
				{
					modifiedSearchFolders.InsertedInto.Add(base.GetId(context));
				}
				SearchFolder.ProcessSearchFolderInsert(context, this, insertedMessage, false, userIdentityContext, modifiedSearchFolders);
				MessageCreatedNotificationEvent nev = NotificationEvents.CreateMessageCreatedEvent(context, insertedMessage, this, userIdentityContext);
				context.RiseNotificationEvent(nev);
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000568CC File Offset: 0x00054ACC
		private void UpdateMessageLink(Context context, TopMessage updatedMessage, bool forceProbing, Guid? userIdentityContext, ModifiedSearchFolders modifiedSearchFolders)
		{
			Mailbox mailbox = updatedMessage.Mailbox;
			LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, mailbox, base.GetId(context), this.GetLogicalIndexNumber(context).Value);
			if (logicalIndex == null || logicalIndex.IsStale)
			{
				return;
			}
			Guid? nullableSearchGuid = this.GetNullableSearchGuid(context);
			if (userIdentityContext == null || nullableSearchGuid != null)
			{
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.LogicalIndex, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					if (logicalIndex.IsLogicalIndexAffected(context, updatedMessage))
					{
						logicalIndex.LogUpdate(context, updatedMessage, LogicalIndex.LogicalOperation.Update);
					}
				}
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.SearchFolder, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					base.ItemUpdated(context, updatedMessage);
				}
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.LogicalIndex, new Guid?(context.UserIdentity), mailbox.SharedState))
				{
					LogicalIndexCache.TrackIndexUpdate(context, mailbox, base.GetId(context), LogicalIndexType.SearchFolderMessages, LogicalIndex.LogicalOperation.Update, updatedMessage);
				}
				modifiedSearchFolders.Updated.Add(base.GetId(context));
				SearchFolder.ProcessSearchFolderUpdate(context, this, updatedMessage, false, forceProbing, userIdentityContext, modifiedSearchFolders);
			}
			MessageModifiedNotificationEvent nev = NotificationEvents.CreateMessageModifiedEvent(context, updatedMessage, this, userIdentityContext);
			context.RiseNotificationEvent(nev);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00056A30 File Offset: 0x00054C30
		private bool IsProbeForMessageLinkNeeded(Context context, Folder folder)
		{
			SearchState searchState = this.GetSearchState(context);
			if (SearchFolder.IsSearchEvaluationInProgress(searchState) || SearchFolder.IsCiSearch(searchState))
			{
				return true;
			}
			if (folder.IsPerUserReadUnreadTrackingEnabled)
			{
				Restriction restriction = this.GetRestriction(context);
				if (SearchFolder.RestrictionContainsPerUserConditions(restriction))
				{
					return true;
				}
			}
			return this.GetHasCountRestriction(context);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00056A80 File Offset: 0x00054C80
		private SearchCriteria GetCriteria(Context context, out int maxCount)
		{
			Restriction restriction = this.GetRestriction(context);
			this.diagnostics.OnGetRestriction(restriction);
			maxCount = this.GetMaxCount(restriction);
			SearchCriteria searchCriteria = restriction.ToSearchCriteria(base.Mailbox.Database, ObjectType.Message);
			return searchCriteria.InspectAndFix(null, (context.Culture == null) ? null : context.Culture.CompareInfo, true);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00056B00 File Offset: 0x00054D00
		private int GetMaxCount(Restriction restriction)
		{
			RestrictionCount restrictionCount = null;
			int result;
			if (restriction.HasClauseMeetingPredicate(delegate(Restriction clause)
			{
				restrictionCount = (clause as RestrictionCount);
				return restrictionCount != null;
			}))
			{
				result = restrictionCount.Count;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00056B40 File Offset: 0x00054D40
		private Restriction GetRestriction(Context context)
		{
			byte[] array = (byte[])base.GetColumnValue(context, base.FolderTable.QueryCriteria);
			int num = 0;
			return Restriction.Deserialize(context, array, ref num, array.Length, base.Mailbox, ObjectType.Message);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00056B7C File Offset: 0x00054D7C
		private SearchState GetSearchState(Context context)
		{
			object columnValue = base.GetColumnValue(context, base.FolderTable.SearchState);
			if (columnValue != null)
			{
				return (SearchState)((int)columnValue);
			}
			return SearchState.None;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00056BA8 File Offset: 0x00054DA8
		private SetSearchCriteriaFlags GetSearchCriteriaFlags(Context context)
		{
			object columnValue = base.GetColumnValue(context, base.FolderTable.SetSearchCriteriaFlags);
			if (columnValue != null)
			{
				return (SetSearchCriteriaFlags)((int)columnValue);
			}
			return SetSearchCriteriaFlags.None;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00056BD4 File Offset: 0x00054DD4
		private IList<ExchangeId> GetScopeFolders(Context context)
		{
			int num = 0;
			byte[] buff = (byte[])base.GetColumnValue(context, base.FolderTable.ScopeFolders);
			return ExchangeIdListHelpers.ListFromBytes(context, base.Mailbox.ReplidGuidMap, buff, ref num);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00056C11 File Offset: 0x00054E11
		private byte[] GetSerializedRestriction(Context context)
		{
			return (byte[])base.GetColumnValue(context, base.FolderTable.QueryCriteria);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00056C2C File Offset: 0x00054E2C
		private object GetParentDisplayColumnFunction(object[] columnValues)
		{
			Context currentOperationContext = base.Mailbox.CurrentOperationContext;
			Folder folder = null;
			ExchangeId id = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[0]);
			if (id.IsValid)
			{
				folder = Folder.OpenFolder(currentOperationContext, base.Mailbox, id);
			}
			if (folder != null)
			{
				return folder.GetName(currentOperationContext);
			}
			return null;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00056C84 File Offset: 0x00054E84
		internal SimpleQueryOperator BaseViewOperator(Context context, Mailbox mailbox, IList<Column> columnsToFetch, SearchCriteria criteria, bool? associated)
		{
			SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition = this.BaseViewOperatorDefinition(context, mailbox, columnsToFetch, criteria, associated);
			return simpleQueryOperatorDefinition.CreateOperator(context);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00056CA8 File Offset: 0x00054EA8
		internal SimpleQueryOperator.SimpleQueryOperatorDefinition BaseViewOperatorDefinition(Context context, Mailbox mailbox, IList<Column> columnsToFetch, SearchCriteria criteria, bool? associated)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
			LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, mailbox, base.GetId(context), this.GetLogicalIndexNumber(context).Value);
			SearchCriteria restriction = null;
			if (!logicalIndex.IsStale)
			{
				logicalIndex.UpdateIndex(context, LogicalIndex.CannotRepopulate);
			}
			else
			{
				restriction = Factory.CreateSearchCriteriaFalse();
			}
			IList<Column> columns = messageTable.MessagePK.Columns;
			StartStopKey startStopKey;
			if (associated == null)
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					logicalIndex.MailboxPartitionNumber,
					logicalIndex.LogicalIndexNumber
				});
			}
			else
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					logicalIndex.MailboxPartitionNumber,
					logicalIndex.LogicalIndexNumber,
					associated.Value
				});
			}
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(2);
			dictionary[messageTable.VirtualIsRead] = messageTable.IsRead;
			dictionary[messageTable.VirtualParentDisplay] = TopMessage.CreateVirtualParentDisplayFunctionColumn(messageTable, new Func<object[], object>(this.GetParentDisplayColumnFunction));
			IList<Column> columnsToFetch2 = columns;
			if (this.IsStaticSearch(context))
			{
				List<Column> list = new List<Column>(columns.Count + 1);
				list.AddRange(columns);
				Column column = logicalIndex.RenameDictionary[messageTable.MessageId];
				list.Add(column);
				columnsToFetch2 = list;
				SearchCriteria searchCriteria = Factory.CreateSearchCriteriaCompare(messageTable.MessageId, SearchCriteriaCompare.SearchRelOp.Equal, column);
				criteria = ((criteria == null) ? searchCriteria : Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					criteria,
					searchCriteria
				}));
			}
			TableOperator.TableOperatorDefinition outerQueryDefinition = new TableOperator.TableOperatorDefinition(context.Culture, logicalIndex.IndexTable, logicalIndex.IndexTable.PrimaryKeyIndex, columnsToFetch2, null, restriction, logicalIndex.RenameDictionary, 0, 0, new KeyRange[]
			{
				new KeyRange(startStopKey, startStopKey)
			}, false, true, true);
			return new JoinOperator.JoinOperatorDefinition(context.Culture, messageTable.Table, columnsToFetch, null, criteria, dictionary, 0, 0, columns, outerQueryDefinition, true);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00056EA4 File Offset: 0x000550A4
		private QueryPlanner GenerateQueryPlannerForBaseViewPopulation(Context context, SearchCriteria restriction, int maxRows, bool allowComplexQueryPlan)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, base.GetId(context), this.GetLogicalIndexNumber(context).Value);
			IList<Column> columnsToFetchForBaseViewPopulation = SearchFolder.GetColumnsToFetchForBaseViewPopulation(context, base.Mailbox, logicalIndex);
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(2);
			if (base.Mailbox.SharedState.SupportsPerUserFeatures)
			{
				dictionary[messageTable.VirtualIsRead] = Factory.CreateFunctionColumn("PerUserIsRead", typeof(bool), PropertyTypeHelper.SizeFromPropType(PropertyType.Boolean), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Boolean), base.Table, new Func<object[], object>(this.GetPerUserReadUnreadColumnFunction), "ComputePerUserIsRead", new Column[]
				{
					messageTable.FolderId,
					messageTable.IsRead,
					messageTable.LcnCurrent
				});
			}
			else
			{
				dictionary[messageTable.VirtualIsRead] = messageTable.IsRead;
			}
			dictionary[messageTable.VirtualParentDisplay] = TopMessage.CreateVirtualParentDisplayFunctionColumn(messageTable, new Func<object[], object>(this.GetParentDisplayColumnFunction));
			QueryPlanner.Hints hints = null;
			if (this.IsRecursiveSearch(context))
			{
				hints = new QueryPlanner.Hints
				{
					GetSupplementaryCriteria = new QueryPlanner.Hints.GetSupplementaryCriteriaDelegate(this.CalculateSupplementaryCriteria)
				};
			}
			return new QueryPlanner(context, messageTable.Table, null, restriction, null, null, columnsToFetchForBaseViewPopulation, null, dictionary, null, null, SortOrder.Empty, Bookmark.BOT, 0, maxRows, false, false, !this.IsTwirSearch(context), false, allowComplexQueryPlan, hints);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00057008 File Offset: 0x00055208
		internal SearchCriteria CalculateSupplementaryCriteria(Context context, SearchCriteria originalCriteria, int maxNumberOfTerms)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			if (maxNumberOfTerms == 0)
			{
				return null;
			}
			SearchCriteriaOr searchCriteriaOr = originalCriteria as SearchCriteriaOr;
			if (searchCriteriaOr == null || searchCriteriaOr.NestedCriteria == null || searchCriteriaOr.NestedCriteria.Length == 0)
			{
				return null;
			}
			HashSet<ExchangeId> hashSet = new HashSet<ExchangeId>();
			SearchCriteria[] nestedCriteria = searchCriteriaOr.NestedCriteria;
			int i = 0;
			while (i < nestedCriteria.Length)
			{
				SearchCriteria searchCriteria = nestedCriteria[i];
				SearchCriteriaCompare searchCriteriaCompare = searchCriteria as SearchCriteriaCompare;
				SearchCriteria result;
				if (searchCriteriaCompare == null)
				{
					result = null;
				}
				else
				{
					if (!(searchCriteriaCompare.Lhs != messageTable.FolderId) && searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal && searchCriteriaCompare.Rhs is ConstantColumn)
					{
						hashSet.Add(ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, (byte[])((ConstantColumn)searchCriteriaCompare.Rhs).Value));
						i++;
						continue;
					}
					result = null;
				}
				return result;
			}
			return this.CalculateComplementaryFolderCriteria(context, hashSet, maxNumberOfTerms);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000570EC File Offset: 0x000552EC
		internal SearchCriteria CalculateComplementaryFolderCriteria(Context context, HashSet<ExchangeId> folders, int maxNumberOfTerms)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			SearchFolder.FoldersScope queryScope = this.GetQueryScope(context);
			IReplidGuidMap replidGuidMap = base.Mailbox.ReplidGuidMap;
			List<SearchCriteriaCompare> list = new List<SearchCriteriaCompare>(maxNumberOfTerms);
			foreach (IFolderInformation folderInformation in queryScope.IndexableFolders)
			{
				ExchangeId item = ExchangeId.CreateFromInternalShortId(context, replidGuidMap, folderInformation.Fid);
				if (!folders.Contains(item))
				{
					if (list.Count >= maxNumberOfTerms)
					{
						break;
					}
					list.Add(Factory.CreateSearchCriteriaCompare(messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(item.To26ByteArray())));
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return Factory.CreateSearchCriteriaNot(Factory.CreateSearchCriteriaOr(list.ToArray()));
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000571BC File Offset: 0x000553BC
		private object GetPerUserReadUnreadColumnFunction(object[] columnValues)
		{
			Context currentOperationContext = base.Mailbox.CurrentOperationContext;
			ExchangeId exchangeId = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[0]);
			if (exchangeId == ExchangeId.Zero)
			{
				return ((bool)columnValues[1]).GetBoxed();
			}
			Folder folder = Folder.OpenFolder(currentOperationContext, base.Mailbox, exchangeId);
			if (folder == null)
			{
				return ((bool)columnValues[1]).GetBoxed();
			}
			ExchangeId changeNumber = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[2]);
			return TopMessage.GetPerUserReadUnreadColumnFunction(currentOperationContext, folder, (bool)columnValues[1], false, changeNumber).GetBoxed();
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0005725C File Offset: 0x0005545C
		private SearchCriteria GetRestrictionToUseForSearchPopulation(Context context, out SearchFolder nestedSearchFolder, out int maxCount)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			nestedSearchFolder = null;
			maxCount = 0;
			SearchFolder.FoldersScope queryScope = this.GetQueryScope(context);
			SearchCriteria searchCriteria = this.GetCriteria(context, out maxCount);
			if (queryScope.Folders.Count == 0)
			{
				searchCriteria = Factory.CreateSearchCriteriaFalse();
			}
			else
			{
				if (queryScope.Folders.Count == 1 && !this.IsRecursiveSearch(context))
				{
					nestedSearchFolder = (Folder.OpenFolder(context, base.Mailbox, queryScope.Folders[0]) as SearchFolder);
				}
				if (nestedSearchFolder != null)
				{
					if (nestedSearchFolder.GetLogicalIndexNumber(context) == null)
					{
						searchCriteria = Factory.CreateSearchCriteriaFalse();
						nestedSearchFolder = null;
					}
				}
				else
				{
					SearchCriteria[] array = new SearchCriteria[queryScope.Folders.Count];
					for (int i = 0; i < queryScope.Folders.Count; i++)
					{
						array[i] = Factory.CreateSearchCriteriaCompare(messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(queryScope.Folders[i].To26ByteArray()));
					}
					SearchCriteria searchCriteria2 = Factory.CreateSearchCriteriaOr(array);
					searchCriteria = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
					{
						searchCriteria,
						searchCriteria2
					});
				}
			}
			searchCriteria = this.RemoveNonContentIndexedPropertiesFromFullTextIndexSearch(context, searchCriteria);
			searchCriteria = MessageViewTable.RewriteMessageSearchCriteria(context, base.Mailbox, searchCriteria, SortOrder.Empty, false, false, ExchangeId.Null);
			if (nestedSearchFolder == null && this.SearchResults == null)
			{
				SearchCriteriaCompare searchCriteriaCompare = Factory.CreateSearchCriteriaCompare(messageTable.MailboxPartitionNumber, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(base.Mailbox.MailboxPartitionNumber, messageTable.MailboxPartitionNumber));
				searchCriteria = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					searchCriteria,
					searchCriteriaCompare
				});
			}
			return searchCriteria;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000575B8 File Offset: 0x000557B8
		private SearchCriteria RemoveNonContentIndexedPropertiesFromFullTextIndexSearch(Context context, SearchCriteria restriction)
		{
			string name = base.GetName(context);
			if (!this.IsTwirSearch(context) && name != null && (name.StartsWith("EWS Search ") || name.StartsWith("MS-OLK-BGPooledSearchFolder") || name.StartsWith("OWA Search ")))
			{
				List<StorePropTag> removedPropertyTags = null;
				restriction = restriction.InspectAndFix(delegate(SearchCriteria criteriaToInspect, CompareInfo compareInfo)
				{
					FullTextIndexSchema.FullTextIndexInfo fullTextIndexInfo = null;
					SearchCriteriaOr searchCriteriaOr = criteriaToInspect as SearchCriteriaOr;
					if (searchCriteriaOr == null)
					{
						return criteriaToInspect;
					}
					bool flag = true;
					bool flag2 = false;
					bool flag3 = false;
					foreach (SearchCriteria searchCriteria in searchCriteriaOr.NestedCriteria)
					{
						SearchCriteriaText searchCriteriaText = searchCriteria as SearchCriteriaText;
						Column lhs;
						if (searchCriteriaText == null)
						{
							SearchCriteriaCompare searchCriteriaCompare = searchCriteria as SearchCriteriaCompare;
							if (searchCriteriaCompare == null)
							{
								flag = false;
								break;
							}
							lhs = searchCriteriaCompare.Lhs;
						}
						else
						{
							lhs = searchCriteriaText.Lhs;
						}
						ExtendedPropertyColumn extendedPropertyColumn = lhs as ExtendedPropertyColumn;
						if (extendedPropertyColumn != null && FullTextIndexSchema.Current.IsPropertyInFullTextIndex(extendedPropertyColumn.StorePropTag.PropInfo.PropName, context.Database.MdbGuid, out fullTextIndexInfo))
						{
							flag2 = true;
						}
						else
						{
							flag3 = true;
						}
					}
					if (!flag || !flag2 || !flag3)
					{
						return criteriaToInspect;
					}
					List<SearchCriteria> list = new List<SearchCriteria>(searchCriteriaOr.NestedCriteria.Length);
					foreach (SearchCriteria searchCriteria2 in searchCriteriaOr.NestedCriteria)
					{
						SearchCriteriaText searchCriteriaText2 = searchCriteria2 as SearchCriteriaText;
						Column lhs2;
						if (searchCriteriaText2 == null)
						{
							SearchCriteriaCompare searchCriteriaCompare2 = searchCriteria2 as SearchCriteriaCompare;
							lhs2 = searchCriteriaCompare2.Lhs;
						}
						else
						{
							lhs2 = searchCriteriaText2.Lhs;
						}
						ExtendedPropertyColumn extendedPropertyColumn2 = lhs2 as ExtendedPropertyColumn;
						if (FullTextIndexSchema.Current.IsPropertyInFullTextIndex(extendedPropertyColumn2.StorePropTag.PropInfo.PropName, context.Database.MdbGuid, out fullTextIndexInfo))
						{
							list.Add(searchCriteria2);
						}
						else
						{
							if (removedPropertyTags == null)
							{
								removedPropertyTags = new List<StorePropTag>(5);
							}
							removedPropertyTags.Add(extendedPropertyColumn2.StorePropTag);
						}
					}
					return Factory.CreateSearchCriteriaOr(list.ToArray());
				}, (context.Culture == null) ? null : context.Culture.CompareInfo, true);
			}
			return restriction;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00057661 File Offset: 0x00055861
		internal void SetSearchCriteria(Context context, byte[] serializedRestriction, IList<ExchangeId> foldersToSearch, SetSearchCriteriaFlags searchCriteriaFlags)
		{
			this.SetSearchCriteria(context, serializedRestriction, foldersToSearch, searchCriteriaFlags, false, false, false);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000577B8 File Offset: 0x000559B8
		internal void SetSearchCriteria(Context context, byte[] serializedRestriction, IList<ExchangeId> foldersToSearch, SetSearchCriteriaFlags searchCriteriaFlags, bool isInstantSearch, bool allowOptimizedConversationSearch, bool skipSearchPopulation)
		{
			ExchangeId searchFolderId = base.GetId(context);
			if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.TraceSetSearchCriteriaCall(context, serializedRestriction, foldersToSearch, searchCriteriaFlags);
			}
			this.ValidateSearchCriteriaFlags(searchCriteriaFlags);
			SearchState searchState = this.GetSearchState(context);
			if (SearchFolder.IsSearchEvaluationInProgress(searchState))
			{
				throw new SearchEvaluationInProgressException((LID)65016U, "Search evaluation already in progress on the search folder");
			}
			bool flag = false;
			if (serializedRestriction != null)
			{
				int num = 0;
				Restriction restriction = Restriction.Deserialize(context, serializedRestriction, ref num, serializedRestriction.Length, base.Mailbox, ObjectType.Message);
				SearchCriteria searchCriteria = restriction.ToSearchCriteria(base.Mailbox.Database, ObjectType.Message);
				flag = SearchFolder.RestrictionContainsPerUserConditions(restriction);
				if (searchCriteria != null && (searchCriteriaFlags & (SetSearchCriteriaFlags.NonContentIndexed | SetSearchCriteriaFlags.Static)) == (SetSearchCriteriaFlags.NonContentIndexed | SetSearchCriteriaFlags.Static) && SearchFolder.CriteriaMustBeHandledByFullTextIndex(searchCriteria, context.Database.MdbGuid))
				{
					throw new InvalidParameterException((LID)45360U, "Cannot perform a TWIR-only, static search using a restriction that can be evaluated accurately only by CI.");
				}
			}
			if ((searchCriteriaFlags & SetSearchCriteriaFlags.Stop) != SetSearchCriteriaFlags.None)
			{
				if (SearchFolder.IsRunningSearch(searchState))
				{
					if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<ExchangeId, SearchState>(0L, "Search folder {0}: Running search will be stopped (current state: {1}).", searchFolderId, searchState);
					}
					this.RemoveSearchBacklinks(context);
					searchState |= SearchState.Static;
					searchState &= ~SearchState.Running;
					base.SetColumn(context, base.FolderTable.SearchState, (int)searchState);
					return;
				}
				if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: Stop requested, but search was not running, so nothing to do.", searchFolderId);
					return;
				}
			}
			else
			{
				List<ExchangeId> list = null;
				if (foldersToSearch != null)
				{
					list = new List<ExchangeId>(foldersToSearch);
					list.SortAndRemoveDuplicates<ExchangeId>();
				}
				IList<ExchangeId> scopeFolders = this.GetScopeFolders(context);
				bool flag2 = this.IsScopeChanged(list, scopeFolders);
				byte[] serializedRestriction2 = this.GetSerializedRestriction(context);
				bool flag3 = this.IsRestrictionChanged(context, serializedRestriction, serializedRestriction2);
				SearchState searchState2 = SearchFolder.CalculateNewSearchState(context, searchCriteriaFlags, searchState, flag2 || flag3, isInstantSearch);
				if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					this.TraceSearchCriteriaChanges(context, serializedRestriction2, flag3, scopeFolders, flag2, searchState, searchState2);
				}
				if (SearchFolder.IsSearchEvaluationInProgress(searchState2))
				{
					bool flag4 = (searchCriteriaFlags & SetSearchCriteriaFlags.Recursive) == SetSearchCriteriaFlags.Recursive;
					SearchFolder searchFolder = this.ValidateSearchScopeAndNestedSearchChain(context, list, flag4);
					if (searchFolder != null)
					{
						Guid? nullableSearchGuid = searchFolder.GetNullableSearchGuid(context);
						if (nullableSearchGuid != null)
						{
							if (nullableSearchGuid.Value != context.UserIdentity)
							{
								ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<ExchangeId, string>(0L, "Per-user search folder {0} scopes a search folder {1} for another user.", searchFolderId, list[0].ToString());
								throw new StoreException((LID)40696U, ErrorCodeValue.NoAccess, "Can not search a search folder owned by another user.");
							}
							flag = true;
						}
					}
					if (!SearchFolder.IsStaticSearch(searchState2))
					{
						int dynamicSearchFolderPerScopeCountReceiveQuota = ConfigurationSchema.DynamicSearchFolderPerScopeCountReceiveQuota.Value;
						bool backlinksNumberOverQuota = false;
						this.ExecuteOnBacklinksCountPerScopeFolder(context, flag4, list, delegate(ExchangeId scopeFolderId, int dynamicSearchFoldersCount)
						{
							if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<string, int>(0L, "Folder {0} has {1} backlinks.", scopeFolderId.ToString(), dynamicSearchFoldersCount);
							}
							if (dynamicSearchFoldersCount >= dynamicSearchFolderPerScopeCountReceiveQuota)
							{
								if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<string, int>(0L, "Search folder {0} configured as dymamic search folder scopes a folder with {1} backlinks.", searchFolderId.ToString(), dynamicSearchFoldersCount);
								}
								backlinksNumberOverQuota = true;
							}
						});
						if (backlinksNumberOverQuota)
						{
							if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<string, int>(0L, "Search folder {0} configured as dymamic search folder scopes a folder over backlinks quota {1}. Will try synchronous ageout.", searchFolderId.ToString(), dynamicSearchFolderPerScopeCountReceiveQuota);
							}
							SearchFolder.AgeOutMailboxSearchFolders(context, base.Mailbox, searchFolderId);
							this.ExecuteOnBacklinksCountPerScopeFolder(context, flag4, list, delegate(ExchangeId scopeFolderId, int dynamicSearchFoldersCount)
							{
								if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<string, int>(0L, "Folder {0} has {1} backlinks.", scopeFolderId.ToString(), dynamicSearchFoldersCount);
								}
								if (dynamicSearchFoldersCount >= dynamicSearchFolderPerScopeCountReceiveQuota)
								{
									if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<string, int>(0L, "Search folder {0} configured as dymamic search folder scopes a folder with {1} backlinks.", searchFolderId.ToString(), dynamicSearchFoldersCount);
									}
									DiagnosticContext.TraceDword((LID)40476U, (uint)dynamicSearchFoldersCount);
									DiagnosticContext.TraceDword((LID)56860U, (uint)dynamicSearchFolderPerScopeCountReceiveQuota);
									throw new StoreException((LID)65308U, ErrorCodeValue.DynamicSearchFoldersPerScopeCountReceiveQuotaExceeded);
								}
							});
						}
					}
					if (SearchFolder.IsRunningSearch(searchState))
					{
						this.RemoveSearchBacklinks(context);
					}
					base.SetColumn(context, base.FolderTable.SearchState, (int)searchState2);
					base.SetColumn(context, base.FolderTable.SetSearchCriteriaFlags, (int)searchCriteriaFlags);
					if (flag2)
					{
						base.SetColumn(context, base.FolderTable.ScopeFolders, ExchangeIdListHelpers.BytesFromList(list, true));
					}
					if (base.Mailbox.SharedState.SupportsPerUserFeatures)
					{
						if (flag)
						{
							this.SetProperty(context, PropTag.Folder.SearchGUID, context.UserIdentity.ToByteArray());
						}
						else
						{
							this.SetProperty(context, PropTag.Folder.SearchGUID, null);
						}
					}
					if (flag3)
					{
						base.SetColumn(context, base.FolderTable.QueryCriteria, serializedRestriction);
						this.SetRestrictionFiltersNothing(null);
						this.SetHasCountRestriction(null);
					}
					this.InvalidateCachedQueryScope(context);
					if (!this.IsStaticSearch(context))
					{
						LogicalIndex.AddLogicalIndexMaintenanceBreadcrumb(context, base.Mailbox.MailboxPartitionNumber, LogicalIndex.LogicalOperation.SetSearchCriteria, new object[]
						{
							base.GetId(context).To26ByteArray(),
							serializedRestriction,
							this.SerializeQueryScopeForBreadcrumb(context)
						});
					}
					if (this.GetLogicalIndexNumber(context) == null)
					{
						MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
						SortOrder sortOrder;
						Column[] nonKeyColumns;
						if (SearchFolder.skipAddingDocIdToBaseViewIndexKeyTestHook.Value)
						{
							sortOrder = (SortOrder)new SortOrderBuilder
							{
								{
									messageTable.IsHidden,
									true
								},
								{
									messageTable.MessageId,
									true
								}
							};
							nonKeyColumns = new Column[]
							{
								messageTable.MessageDocumentId
							};
						}
						else
						{
							sortOrder = (SortOrder)new SortOrderBuilder
							{
								{
									messageTable.IsHidden,
									true
								},
								{
									messageTable.MessageId,
									true
								},
								{
									messageTable.MessageDocumentId,
									true
								}
							};
							nonKeyColumns = Array<Column>.Empty;
						}
						LogicalIndex indexToUse = LogicalIndexCache.GetIndexToUse(context, base.Mailbox, searchFolderId, LogicalIndexType.SearchFolderBaseView, null, false, sortOrder, nonKeyColumns, null, messageTable.Table);
						base.SetColumn(context, base.FolderTable.LogicalIndexNumber, indexToUse.LogicalIndexNumber);
					}
					else
					{
						LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, searchFolderId, LogicalIndexType.SearchFolderMessages);
						LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, searchFolderId, LogicalIndexType.Conversations);
						LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, searchFolderId, LogicalIndexType.SearchFolderBaseView);
					}
					bool flag5 = false;
					if (!skipSearchPopulation)
					{
						if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<string, string>(0L, "Search folder {0}: Launching search population {1}synchronously.", searchFolderId.ToString(), flag5 ? string.Empty : "a");
						}
						SearchFolder.FoldersScope queryScope = this.GetQueryScope(context);
						if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "Search folder {0}: instantSearch={1}, clientType={2}, actionString={3}, scopeCount={4}, recursive={5}, recursiveIpm={6}.", new object[]
							{
								searchFolderId,
								isInstantSearch,
								context.ClientType,
								context.Diagnostics.ClientActionString,
								foldersToSearch.Count,
								this.IsRecursiveSearch(context),
								queryScope.IsRecursiveIpmSearch
							});
						}
						SearchFolder searchFolder2;
						int maxRows;
						SearchCriteria restrictionToUseForSearchPopulation = this.GetRestrictionToUseForSearchPopulation(context, out searchFolder2, out maxRows);
						if (base.Mailbox.Database.PhysicalDatabase.DatabaseType != DatabaseType.Jet || !ConfigurationSchema.EnableOptimizedConversationSearch.Value || searchFolder2 != null)
						{
							allowOptimizedConversationSearch = false;
						}
						if (allowOptimizedConversationSearch && ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: eligible for optimized conversation search.", searchFolderId);
						}
						this.SearchResults = (allowOptimizedConversationSearch ? new SearchFolder.InstantSearchResults() : null);
						if (searchFolder2 == null)
						{
							bool allowComplexQueryPlan = InTransitInfo.IsMoveDestination(InTransitInfo.GetInTransitStatus(base.Mailbox.SharedState)) && context.ClientType == ClientType.Migration;
							QueryPlanner queryPlanner = this.GenerateQueryPlannerForBaseViewPopulation(context, restrictionToUseForSearchPopulation, maxRows, allowComplexQueryPlan);
							SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition = queryPlanner.CreatePlanDefinition();
							if (this.SearchResults != null)
							{
								JoinOperator.JoinOperatorDefinition joinOperatorDefinition = simpleQueryOperatorDefinition as JoinOperator.JoinOperatorDefinition;
								if (joinOperatorDefinition == null || !StoreFullTextIndexHelper.IsFullTextIndexTableFunctionOperatorDefinition(joinOperatorDefinition.OuterQueryDefinition) || joinOperatorDefinition.OuterQueryDefinition.Criteria != null)
								{
									if (ExTraceGlobals.SearchFolderSearchCriteriaTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: optimized instant search impossible - unsupported query plan.", searchFolderId);
									}
									this.SearchResults = null;
								}
							}
						}
						SearchQueue.InsertIntoSearchQueue(context, base.Mailbox, searchFolderId);
						this.LaunchSearchPopulationTask(context, base.Mailbox.SharedState, searchFolderId);
					}
				}
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00057F90 File Offset: 0x00056190
		private void TraceSetSearchCriteriaCall(Context context, byte[] serializedRestriction, IList<ExchangeId> foldersToSearch, SetSearchCriteriaFlags searchCriteriaFlags)
		{
			int num = 0;
			Restriction restriction = null;
			if (serializedRestriction != null)
			{
				restriction = Restriction.Deserialize(context, serializedRestriction, ref num, serializedRestriction.Length, base.Mailbox, ObjectType.Message);
			}
			string text;
			if (foldersToSearch != null && foldersToSearch.Count > 0)
			{
				text = foldersToSearch[0].ToString();
				if (foldersToSearch.Count > 1)
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						" (and ",
						foldersToSearch.Count - 1,
						" others)"
					});
				}
			}
			else
			{
				text = "<none>";
			}
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<ExchangeId, string>(0L, "SetSearchCriteria on search folder {0} ({1}):", base.GetId(context), base.GetName(context));
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    Restriction: " + ((restriction == null) ? "<none>" : restriction.ToString()));
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    Scope folders: " + text);
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    Flags: " + searchCriteriaFlags);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000580A4 File Offset: 0x000562A4
		private void TraceSearchCriteriaChanges(Context context, byte[] existingRestriction, bool changedRestriction, IList<ExchangeId> existingScopeFolders, bool changedScope, SearchState existingSearchState, SearchState newSearchState)
		{
			int num = 0;
			Restriction restriction = Restriction.Deserialize(context, existingRestriction, ref num, existingRestriction.Length, base.Mailbox, ObjectType.Message);
			string text;
			if (existingScopeFolders != null && existingScopeFolders.Count > 0)
			{
				text = existingScopeFolders[0].ToString();
				if (existingScopeFolders.Count > 1)
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						" (and ",
						existingScopeFolders.Count - 1,
						" others)"
					});
				}
			}
			else
			{
				text = "<none>";
			}
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug<ExchangeId, string>(0L, "Search criteria changes for search folder {0} ({1}):", base.GetId(context), base.GetName(context));
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    Existing restriction: " + restriction.ToString());
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    Changed restriction: " + changedRestriction);
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    Existing scope folders: " + text);
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    Changed scope folders: " + changedScope);
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    Existing search state: " + existingSearchState);
			ExTraceGlobals.SearchFolderSearchCriteriaTracer.TraceDebug(0L, "    New search state: " + newSearchState);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00058204 File Offset: 0x00056404
		private void ValidateSearchCriteriaFlags(SetSearchCriteriaFlags searchFlags)
		{
			if (!EnumValidator.IsValidValue<SetSearchCriteriaFlags>(searchFlags))
			{
				throw new NotSupportedException((LID)40440U, "Unrecognized SetSearchCriteria flag specified.");
			}
			if ((searchFlags & SetSearchCriteriaFlags.Stop) != SetSearchCriteriaFlags.None && (searchFlags & SetSearchCriteriaFlags.Restart) != SetSearchCriteriaFlags.None)
			{
				throw new InvalidParameterException((LID)44536U, "Stop and Restart flags are mutually exclusive.");
			}
			if ((searchFlags & SetSearchCriteriaFlags.Recursive) != SetSearchCriteriaFlags.None && (searchFlags & SetSearchCriteriaFlags.Shallow) != SetSearchCriteriaFlags.None)
			{
				throw new InvalidParameterException((LID)42488U, "Recursive and Shallow flags are mutually exclusive.");
			}
			if ((searchFlags & SetSearchCriteriaFlags.Foreground) != SetSearchCriteriaFlags.None && (searchFlags & SetSearchCriteriaFlags.Background) != SetSearchCriteriaFlags.None)
			{
				throw new InvalidParameterException((LID)36344U, "Foreground and Background flags are mutually exclusive.");
			}
			if ((searchFlags & SetSearchCriteriaFlags.ContentIndexed) != SetSearchCriteriaFlags.None && (searchFlags & SetSearchCriteriaFlags.NonContentIndexed) != SetSearchCriteriaFlags.None)
			{
				throw new InvalidParameterException((LID)56824U, "ContentIndexed and NonContentIndexed flags are mutually exclusive.");
			}
			if ((searchFlags & SetSearchCriteriaFlags.StatisticsOnly) != SetSearchCriteriaFlags.None && (searchFlags & (SetSearchCriteriaFlags.ContentIndexed | SetSearchCriteriaFlags.Static)) != (SetSearchCriteriaFlags.ContentIndexed | SetSearchCriteriaFlags.Static))
			{
				throw new InvalidParameterException((LID)60920U, "StatisticsOnly flag must be accompanied by ContentIndexed and Static flags.");
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000582E0 File Offset: 0x000564E0
		private SearchFolder ValidateSearchScopeAndNestedSearchChain(Context context, IList<ExchangeId> scopeFolders, bool recursiveSearch)
		{
			uint num = 0U;
			SearchFolder searchFolder = null;
			foreach (ExchangeId exchangeId in scopeFolders)
			{
				Folder folder = Folder.OpenFolder(context, base.Mailbox, exchangeId);
				if (folder == null)
				{
					throw new ObjectNotFoundException((LID)52728U, base.Mailbox.MailboxGuid, "Scope folder does not exist.");
				}
				if (folder.IsInternalAccess(context))
				{
					throw new StoreException((LID)61260U, ErrorCodeValue.NoAccess, "InternalAccess folder cannot be in scope.");
				}
				searchFolder = (folder as SearchFolder);
				if (searchFolder != null)
				{
					if (scopeFolders.Count > 1)
					{
						throw new SearchFolderScopeViolationException((LID)46584U, "Searches that scope another search folder may only have a single scope folder.");
					}
					if (recursiveSearch)
					{
						throw new SearchFolderScopeViolationException((LID)62968U, "Searches cannot recursively scope a search folder.");
					}
					num = this.GetNestedSearchDepthAndCheckSearchEvaluationInProgress(context, exchangeId, this.IsStaticSearch(context));
				}
			}
			num += this.GetLongestBacklinkDepthAndCheckSearchEvaluationInProgress(context, 0U);
			if (num >= 10U)
			{
				throw new NestedSearchChainTooDeepException((LID)38392U, "Nested search chain is too long.");
			}
			return searchFolder;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000583FC File Offset: 0x000565FC
		private uint GetNestedSearchDepthAndCheckSearchEvaluationInProgress(Context context, ExchangeId scopeFolderId, bool isStaticSearch)
		{
			uint num = 0U;
			Folder folder = Folder.OpenFolder(context, base.Mailbox, scopeFolderId);
			if (folder == null && !isStaticSearch)
			{
				throw new CorruptSearchScopeException((LID)54776U, "Scope folder of a dynamic search is missing.");
			}
			SearchFolder searchFolder = folder as SearchFolder;
			if (searchFolder != null)
			{
				num = 1U;
				if (scopeFolderId.Equals(base.GetId(context)))
				{
					throw new SearchFolderScopeViolationException((LID)58872U, "A search folder recursively scopes itself (i.e., it forms a circular search chain).");
				}
				SearchState searchState = searchFolder.GetSearchState(context);
				if (searchState != SearchState.None)
				{
					if (SearchFolder.IsSearchEvaluationInProgress(searchState))
					{
						throw new SearchEvaluationInProgressException((LID)34296U, "Search evaluation in progress on a scope folder");
					}
					if (!SearchFolder.IsRecursiveSearch(searchState))
					{
						IList<ExchangeId> scopeFolders = searchFolder.GetScopeFolders(context);
						if (scopeFolders.Count == 1)
						{
							num += this.GetNestedSearchDepthAndCheckSearchEvaluationInProgress(context, scopeFolders[0], SearchFolder.IsStaticSearch(searchState));
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000584C4 File Offset: 0x000566C4
		private uint GetLongestBacklinkDepthAndCheckSearchEvaluationInProgress(Context context, uint backlinkDepth)
		{
			uint num = backlinkDepth;
			IList<ExchangeId> searchBacklinks = base.GetSearchBacklinks(context, false);
			for (int i = 0; i < searchBacklinks.Count; i++)
			{
				SearchFolder searchFolder = Folder.OpenFolder(context, base.Mailbox, searchBacklinks[i]) as SearchFolder;
				if (searchFolder == null)
				{
					SearchFolder.ReportCorruptSearchBacklink(context, this, searchBacklinks[i], false);
					throw new CorruptSearchBacklinkException((LID)50680U, "Backlink folder doesn't exist or is not a search folder.");
				}
				SearchState searchState = searchFolder.GetSearchState(context);
				if (SearchFolder.IsSearchEvaluationInProgress(searchState))
				{
					throw new SearchEvaluationInProgressException((LID)47608U, "Search evaluation in progress on a backlink folder");
				}
				uint longestBacklinkDepthAndCheckSearchEvaluationInProgress = searchFolder.GetLongestBacklinkDepthAndCheckSearchEvaluationInProgress(context, backlinkDepth + 1U);
				if (longestBacklinkDepthAndCheckSearchEvaluationInProgress > num)
				{
					num = longestBacklinkDepthAndCheckSearchEvaluationInProgress;
				}
			}
			return num;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0005856C File Offset: 0x0005676C
		private bool IsScopeChanged(IList<ExchangeId> newScopeFolders, IList<ExchangeId> existingScopeFolders)
		{
			bool result = false;
			if (newScopeFolders != null && newScopeFolders.Count > 0)
			{
				result = (existingScopeFolders == null || existingScopeFolders.Count <= 0 || !ValueHelper.ListsEqual<ExchangeId>(newScopeFolders, existingScopeFolders));
			}
			else if (existingScopeFolders == null || existingScopeFolders.Count <= 0)
			{
				throw new NotInitializedException((LID)63992U, "No scope folder(s) specified");
			}
			return result;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000585C8 File Offset: 0x000567C8
		private bool IsRestrictionChanged(Context context, byte[] newRestriction, byte[] existingRestriction)
		{
			bool result = false;
			if (existingRestriction != null)
			{
				if (newRestriction != null)
				{
					int num = 0;
					Restriction.Deserialize(context, newRestriction, ref num, newRestriction.Length, base.Mailbox, ObjectType.Message);
					result = !ValueHelper.ArraysEqual<byte>(newRestriction, existingRestriction);
				}
				return result;
			}
			throw new NotInitializedException((LID)39416U, "No restriction specified");
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00058618 File Offset: 0x00056818
		internal static SearchState CalculateNewSearchState(Context context, SetSearchCriteriaFlags searchCriteriaFlags, SearchState existingSearchState, bool changedScopeOrRestriction, bool isInstantSearch)
		{
			SearchState searchState = SearchState.None;
			bool flag = false;
			if ((searchCriteriaFlags & SetSearchCriteriaFlags.Recursive) != SetSearchCriteriaFlags.None)
			{
				searchState |= SearchState.Recursive;
			}
			if ((searchCriteriaFlags & SetSearchCriteriaFlags.Foreground) != SetSearchCriteriaFlags.None)
			{
				searchState |= SearchState.Foreground;
			}
			if ((searchCriteriaFlags & SetSearchCriteriaFlags.NonContentIndexed) != SetSearchCriteriaFlags.None)
			{
				searchState |= SearchState.TwirTotally;
			}
			if ((searchCriteriaFlags & SetSearchCriteriaFlags.Static) != SetSearchCriteriaFlags.None)
			{
				searchState |= SearchState.Static;
			}
			if ((searchCriteriaFlags & SetSearchCriteriaFlags.StatisticsOnly) != SetSearchCriteriaFlags.None)
			{
				searchState |= SearchState.StatisticsOnly;
			}
			if (isInstantSearch)
			{
				searchState |= SearchState.InstantSearch;
			}
			if ((searchCriteriaFlags & SetSearchCriteriaFlags.Restart) != SetSearchCriteriaFlags.None)
			{
				flag = true;
			}
			else if (changedScopeOrRestriction || (searchState & SearchState.Recursive) != (existingSearchState & SearchState.Recursive) || (searchState & SearchState.Static) != (existingSearchState & SearchState.Static) || (searchState & SearchState.StatisticsOnly) != (existingSearchState & SearchState.StatisticsOnly) || ((searchState & SearchState.TwirTotally) != SearchState.None && (existingSearchState & SearchState.TwirTotally) == SearchState.None) || (existingSearchState & SearchState.Error) != SearchState.None)
			{
				flag = true;
			}
			if (flag)
			{
				searchState |= (SearchState.Running | SearchState.Rebuild);
			}
			return searchState;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000586D8 File Offset: 0x000568D8
		private void RemoveSearchBacklinks(Context context)
		{
			IList<ExchangeId> folders = this.GetQueryScope(context).Folders;
			if (folders.Count > 0)
			{
				bool recursive = this.IsRecursiveSearch(context);
				foreach (ExchangeId scopeFolderId in folders)
				{
					this.RemoveOneBacklink(context, scopeFolderId, recursive);
				}
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00058740 File Offset: 0x00056940
		public override void Save(Context context)
		{
			Guid? nullableSearchGuid = this.GetNullableSearchGuid(context);
			if (nullableSearchGuid == null)
			{
				nullableSearchGuid = new Guid?(context.UserIdentity);
			}
			using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.SearchFolder, new Guid?(nullableSearchGuid.Value), base.Mailbox.SharedState))
			{
				base.Save(context);
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000587B4 File Offset: 0x000569B4
		public void ExecuteOnBacklinksCountPerScopeFolder(Context context, bool isRecursiveSearch, IList<ExchangeId> scopeFolders, Action<ExchangeId, int> executePerScopedFolder)
		{
			if (isRecursiveSearch)
			{
				IList<ExchangeId> scopeFolders2 = SearchFolder.FoldersScope.ExpandRecursivelySearchedFolders(context, base.Mailbox, scopeFolders);
				this.ExecuteOnBacklinksCountPerScopeFolder(context, scopeFolders2, executePerScopedFolder);
				return;
			}
			this.ExecuteOnBacklinksCountPerScopeFolder(context, scopeFolders, executePerScopedFolder);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000587E8 File Offset: 0x000569E8
		private void ExecuteOnBacklinksCountPerScopeFolder(Context context, IList<ExchangeId> scopeFolders, Action<ExchangeId, int> executePerScopedFolder)
		{
			if (scopeFolders.Count > 0)
			{
				foreach (ExchangeId exchangeId in scopeFolders)
				{
					int arg = SearchFolder.CountBacklinksPerScopeFolder(context, base.Mailbox, exchangeId);
					executePerScopedFolder(exchangeId, arg);
				}
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00058848 File Offset: 0x00056A48
		private void RemoveOneBacklink(Context context, ExchangeId scopeFolderId, bool recursive)
		{
			Folder folder = Folder.OpenFolder(context, base.Mailbox, scopeFolderId);
			if (folder == null)
			{
				throw new CorruptSearchScopeException((LID)55800U, "Scope folder is missing.");
			}
			List<ExchangeId> list = new List<ExchangeId>(folder.GetSearchBacklinks(context, recursive));
			if (!list.Remove(base.GetId(context)))
			{
				if (!this.IsSearchEvaluationInProgress(context))
				{
					SearchFolder.ReportCorruptSearchBacklink(context, folder, base.GetId(context), recursive);
					throw new CorruptSearchBacklinkException((LID)43512U, "Failed removing search backlink.");
				}
			}
			else
			{
				folder.SetSearchBacklinks(context, list, recursive);
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000588D0 File Offset: 0x00056AD0
		private void AddSearchBacklinks(Context context)
		{
			IList<ExchangeId> folders = this.GetQueryScope(context).Folders;
			SearchState searchState = this.GetSearchState(context);
			bool recursive = SearchFolder.IsRecursiveSearch(searchState);
			foreach (ExchangeId scopeFolderId in folders)
			{
				this.AddOneBacklink(context, scopeFolderId, recursive);
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0005893C File Offset: 0x00056B3C
		private void AddOneBacklink(Context context, ExchangeId scopeFolderId, bool recursive)
		{
			Folder folder = Folder.OpenFolder(context, base.Mailbox, scopeFolderId);
			if (folder == null)
			{
				throw new CorruptSearchScopeException((LID)59896U, "Scope folder is missing.");
			}
			List<ExchangeId> list = new List<ExchangeId>(folder.GetSearchBacklinks(context, recursive));
			if (!list.Contains(base.GetId(context)))
			{
				list.Add(base.GetId(context));
				list.Sort();
				folder.SetSearchBacklinks(context, list, recursive);
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000589A8 File Offset: 0x00056BA8
		internal void GetSearchCriteria(Context context, GetSearchCriteriaFlags flags, out byte[] serializedRestriction, out IList<ExchangeId> foldersToSearch, out SearchState searchState)
		{
			serializedRestriction = (((byte)(flags & Microsoft.Exchange.RpcClientAccess.Parser.GetSearchCriteriaFlags.Restriction) != 0) ? this.GetSerializedRestriction(context) : null);
			foldersToSearch = (((byte)(flags & Microsoft.Exchange.RpcClientAccess.Parser.GetSearchCriteriaFlags.FolderIds) != 0) ? this.GetScopeFolders(context) : null);
			searchState = this.GetSearchState(context);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000589D9 File Offset: 0x00056BD9
		private void LaunchSearchPopulationTask(Context context, MailboxState mailboxState, ExchangeId searchFolderId)
		{
			SearchFolder.LaunchSearchPopulationTask(context, mailboxState, searchFolderId, context.SecurityContext.UserSid, context.ClientType, context.Culture, this.diagnostics, this.SearchResults);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00058A7C File Offset: 0x00056C7C
		internal static void LaunchSearchPopulationTask(Context context, MailboxState mailboxState, ExchangeId searchFolderId, SecurityIdentifier userSid, ClientType clientType, CultureInfo culture, SearchExecutionDiagnostics diagnostics, SearchFolder.InstantSearchResults instantSearchResults)
		{
			if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<int, ExchangeId>(0L, "Mailbox {0}: Launch the task to populate search folder, ID {1}", mailboxState.MailboxNumber, searchFolderId);
			}
			if (mailboxState.SupportsPerUserFeatures)
			{
				InTransitStatus inTransitStatus = InTransitInfo.GetInTransitStatus(mailboxState);
				if (InTransitInfo.IsMoveDestination(inTransitStatus))
				{
					return;
				}
			}
			MailboxTaskQueue.LaunchMailboxTask<MailboxTaskContext>(context, MailboxTaskQueue.Priority.High, TaskTypeId.SearchFolderPopulation, mailboxState, userSid, clientType, culture, delegate(Context taskContext)
			{
				if (diagnostics != null)
				{
					diagnostics.OnBeforeSearchPopulationTaskStep();
					if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<int>(0L, "Mailbox {0}: Search population task scheduled", diagnostics.MailboxNumber);
					}
				}
			}, delegate(Context taskContext)
			{
				if (diagnostics != null)
				{
					diagnostics.OnInsideSearchPopulationTaskStep();
				}
			}, null, (MailboxTaskContext mailboxTaskContext, Func<bool> shouldTaskContinue) => SearchFolder.TryDoSearchPopulation(mailboxTaskContext, mailboxTaskContext.Mailbox, searchFolderId, instantSearchResults, shouldTaskContinue));
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00058B1C File Offset: 0x00056D1C
		internal static IDisposable SetSearchFolderAgeOutChunkSizeForTest(int chunkSize)
		{
			return SearchFolder.searchFolderAgeOutChunkSize.SetTestHook(chunkSize);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00058B29 File Offset: 0x00056D29
		internal static IDisposable SetDoSearchPopulationTestHook(Action action)
		{
			return SearchFolder.doSearchPopulationTestHook.SetTestHook(action);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00058B36 File Offset: 0x00056D36
		internal static IDisposable SetDoSearchPopulationParamsTestHook(Action<bool, int> action)
		{
			return SearchFolder.doSearchPopulationParamsTestHook.SetTestHook(action);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00058B43 File Offset: 0x00056D43
		internal static IDisposable SetProbingTestHook(Action action)
		{
			return SearchFolder.probingTestHook.SetTestHook(action);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00058B50 File Offset: 0x00056D50
		internal static IDisposable SetDoSearchPopulationInterruptControlTestHook(Func<IInterruptControl, IInterruptControl> action)
		{
			return SearchFolder.doSearchPopulationInterruptControlTestHook.SetTestHook(action);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00058B5D File Offset: 0x00056D5D
		internal static IDisposable SetFinishedSearchPopulationTestHook(Action action)
		{
			return SearchFolder.finishedSearchPopulationTestHook.SetTestHook(action);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00058B6A File Offset: 0x00056D6A
		internal static IDisposable SetPulseForFullTextIndexQueryTestHook(Func<ErrorCode> action)
		{
			return SearchFolder.pulseForFullTextIndexQueryTestHook.SetTestHook(action);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00058B77 File Offset: 0x00056D77
		internal static IDisposable SetPulseForBaseViewPopulationTestHook(Func<ErrorCode> action)
		{
			return SearchFolder.pulseForBaseViewPopulationTestHook.SetTestHook(action);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00058B84 File Offset: 0x00056D84
		internal static IDisposable SetValidateComputedAgeOutTimeoutTestHook(Action<Reader, TimeSpan> testDelegate)
		{
			return SearchFolder.validateComputedAgeOutTimeoutTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00058B91 File Offset: 0x00056D91
		internal static IDisposable SetGetMostRecentAccessTimeTestHook(Func<ExchangeId, DateTime, DateTime> testDelegate)
		{
			return SearchFolder.getMostRecentAccessTimeTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00058B9E File Offset: 0x00056D9E
		internal static IDisposable SetShouldSearchFolderBeAgedOutTestHook(Func<MailboxState, ExchangeId, bool, bool> testDelegate)
		{
			return SearchFolder.shouldSearchFolderBeAgedOutTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00058BAB File Offset: 0x00056DAB
		internal static IDisposable SetReportCorruptSearchBacklinkTestHook(Action testDelegate)
		{
			return SearchFolder.reportCorruptSearchBacklinkTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00058BB8 File Offset: 0x00056DB8
		internal static IDisposable SetSearchPopulationQueryPlanTestHook(Action<SimpleQueryOperator.SimpleQueryOperatorDefinition> testDelegate)
		{
			return SearchFolder.searchPopulationQueryPlanTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00058BC5 File Offset: 0x00056DC5
		internal static IDisposable SetSkipAddingDocIdToBaseViewIndexKeyTestHook()
		{
			return SearchFolder.skipAddingDocIdToBaseViewIndexKeyTestHook.SetTestHook(true);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00058BD2 File Offset: 0x00056DD2
		internal static IDisposable SetRepopulateNestedSearchTestHook(Action<Context, SearchFolder> testDelegate)
		{
			return SearchFolder.repopulateNestedSearchTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00058BFB File Offset: 0x00056DFB
		internal static bool RestrictionContainsPerUserConditions(Restriction restriction)
		{
			return restriction.HasClauseMeetingPredicate((Restriction clause) => clause.RefersToProperty(PropTag.Message.MessageFlags) || clause.RefersToProperty(PropTag.Message.Read));
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0005A714 File Offset: 0x00058914
		internal IEnumerator<MailboxTaskQueue.TaskStepResult> DoSearchPopulation(Context context, SearchFolder.InstantSearchResults instantSearchResults, Func<bool> shouldTaskContinue)
		{
			FaultInjection.InjectFault(SearchFolder.doSearchPopulationTestHook);
			this.SearchResults = instantSearchResults;
			context.Diagnostics.TraceElapsed((LID)41600U);
			ExchangeId searchFolderId = base.GetId(context);
			bool isTracingEnabled = ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace);
			bool isLoggingEnabled = FullTextIndexLogger.IsLoggingEnabled;
			Stopwatch stopWatch = isLoggingEnabled ? Stopwatch.StartNew() : null;
			if (isTracingEnabled)
			{
				ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<int, ExchangeId, string>(0L, "Mailbox {0}: Beginning population of search folder {1}.{2}", base.Mailbox.MailboxNumber, searchFolderId, base.GetName(context));
			}
			bool succeeded = false;
			IList<StoreFullTextIndexQuery> allFullTextQueries = null;
			IList<LogicalIndex> allSourceLogicalIndexes = null;
			LogicalIndex baseViewLogicalIndex = null;
			bool baseViewLogicalIndexLockedInCache = false;
			int totalMessagesLinked = 0;
			using (this.SetUserIdentityOnContext(context))
			{
				try
				{
					if (context is MailboxTaskContext && !SearchFolder.IsStaticSearch(this.GetSearchState(context)))
					{
						this.InvalidateCachedQueryScope(context);
					}
					SearchFolder nestedSearchFolder;
					int maxCount;
					SearchCriteria restriction = this.GetRestrictionToUseForSearchPopulation(context, out nestedSearchFolder, out maxCount);
					context.Diagnostics.TraceElapsed((LID)57984U);
					TimeSpan searchRestrictionCalculationTime = TimeSpan.Zero;
					if (isLoggingEnabled)
					{
						searchRestrictionCalculationTime = stopWatch.Elapsed;
					}
					if (!this.IsStaticSearch(context))
					{
						LogicalIndex.AddLogicalIndexMaintenanceBreadcrumb(context, base.Mailbox.MailboxPartitionNumber, LogicalIndex.LogicalOperation.SearchPopulationStarted, new object[]
						{
							searchFolderId.To26ByteArray(),
							this.SerializeQueryScopeForBreadcrumb(context)
						});
					}
					this.InvalidateIndexesForPopulation(context, false);
					SimpleQueryOperator.SimpleQueryOperatorDefinition populationQueryOperatorDefinition = null;
					baseViewLogicalIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, searchFolderId, this.GetLogicalIndexNumber(context).Value);
					baseViewLogicalIndex.LockInCache();
					baseViewLogicalIndexLockedInCache = true;
					if (nestedSearchFolder == null)
					{
						int num = ConfigurationSchema.MaxHitsForFullTextIndexSearches.Value;
						if (maxCount != 0)
						{
							num = Math.Min(maxCount, this.MaxCountForClient(context));
						}
						QueryPlanner queryPlanner = this.GenerateQueryPlannerForBaseViewPopulation(context, restriction, num, false);
						populationQueryOperatorDefinition = queryPlanner.CreatePlanDefinition();
						context.Diagnostics.TraceElapsed((LID)49792U);
						allFullTextQueries = StoreFullTextIndexHelper.CollectAllFullTextQueries(populationQueryOperatorDefinition);
						if (allFullTextQueries != null)
						{
							if (this.SearchResults != null)
							{
								JoinOperator.JoinOperatorDefinition joinOperatorDefinition = populationQueryOperatorDefinition as JoinOperator.JoinOperatorDefinition;
								if (joinOperatorDefinition == null || !StoreFullTextIndexHelper.IsFullTextIndexTableFunctionOperatorDefinition(joinOperatorDefinition.OuterQueryDefinition) || joinOperatorDefinition.OuterQueryDefinition.Criteria != null)
								{
									if (isTracingEnabled)
									{
										ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: Cannot do optimized conversation search - plan is unexpected.", searchFolderId);
									}
									this.SearchResults.IsValid = false;
									this.SearchResults = null;
									restriction = this.GetRestrictionToUseForSearchPopulation(context, out nestedSearchFolder, out maxCount);
									queryPlanner = this.GenerateQueryPlannerForBaseViewPopulation(context, restriction, num, false);
									populationQueryOperatorDefinition = queryPlanner.CreatePlanDefinition();
									allFullTextQueries = StoreFullTextIndexHelper.CollectAllFullTextQueries(populationQueryOperatorDefinition);
									context.Diagnostics.TraceElapsed((LID)38752U);
								}
							}
							maxCount = num;
						}
						if (allFullTextQueries == null)
						{
							if (isTracingEnabled)
							{
								ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: This is a TWIR search.", searchFolderId);
							}
							if (this.SearchResults != null)
							{
								this.SearchResults.IsValid = false;
								this.SearchResults = null;
								restriction = this.GetRestrictionToUseForSearchPopulation(context, out nestedSearchFolder, out maxCount);
							}
							queryPlanner = this.GenerateQueryPlannerForBaseViewPopulation(context, restriction, maxCount, false);
							populationQueryOperatorDefinition = queryPlanner.CreatePlanDefinition();
							context.Diagnostics.TraceElapsed((LID)55136U);
						}
					}
					else
					{
						if (isTracingEnabled)
						{
							ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: This is a nested TWIR search.", searchFolderId);
						}
						IList<Column> columnsToFetchForBaseViewPopulation = SearchFolder.GetColumnsToFetchForBaseViewPopulation(context, base.Mailbox, baseViewLogicalIndex);
						populationQueryOperatorDefinition = nestedSearchFolder.BaseViewOperatorDefinition(context, base.Mailbox, columnsToFetchForBaseViewPopulation, restriction, null);
						context.Diagnostics.TraceElapsed((LID)42848U);
					}
					if (SearchFolder.searchPopulationQueryPlanTestHook.Value != null)
					{
						SearchFolder.searchPopulationQueryPlanTestHook.Value(populationQueryOperatorDefinition);
					}
					if (isLoggingEnabled)
					{
						IList<ExchangeId> scopeFolders = this.GetScopeFolders(context);
						IList<ExchangeId> folders = this.GetQueryScope(context).Folders;
						ExchangeId exchangeId = (scopeFolders.Count > 0) ? scopeFolders[0] : ExchangeId.Null;
						this.diagnostics.OnInitiateQuery(searchFolderId, searchRestrictionCalculationTime, stopWatch.Elapsed, true, nestedSearchFolder != null, this.GetSearchState(context), this.GetSearchCriteriaFlags(context), maxCount, exchangeId, this.GetSpecialFolderNameForTracing(context, exchangeId), scopeFolders.Count, folders.Count, populationQueryOperatorDefinition);
						stopWatch.Restart();
					}
					if (SearchFolder.doSearchPopulationParamsTestHook.Value != null)
					{
						SearchFolder.doSearchPopulationParamsTestHook.Value(true, maxCount);
					}
					this.InitializePopulation(context, allFullTextQueries != null);
					context.Diagnostics.TraceElapsed((LID)60768U);
					if (isLoggingEnabled)
					{
						this.diagnostics.OnSearchOperation("INIT", stopWatch.Elapsed);
						stopWatch.Restart();
					}
					if (isTracingEnabled)
					{
						ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: Initialized for search population.", searchFolderId);
					}
					if (populationQueryOperatorDefinition != null)
					{
						if (this.SearchResults != null)
						{
							using (IEnumerator<MailboxTaskQueue.TaskStepResult> stepResults = this.DoOptimizedConversationSearch((MailboxTaskContext)context, populationQueryOperatorDefinition, allFullTextQueries, maxCount, shouldTaskContinue))
							{
								while (stepResults.MoveNext())
								{
									context.Diagnostics.TraceElapsed((LID)59232U);
									yield return stepResults.Current;
								}
							}
						}
						else
						{
							int numNormalMessagesLinked = 0;
							int numAssociatedMessagesLinked = 0;
							int numUnreadNormalMessagesLinked = 0;
							int numUnreadAssociatedMessagesLinked = 0;
							long sizeNormalMessagesLinked = 0L;
							long sizeAssociatedMessagesLinked = 0L;
							IList<Column> columnsToInsert = baseViewLogicalIndex.GetColumnsToInsertForRepopulation();
							if (allFullTextQueries == null)
							{
								baseViewLogicalIndex.LogIndexPopulationForIndexUpdateInstrumentation(context, populationQueryOperatorDefinition);
							}
							Action<object[]> insertRowAction = null;
							Column[] insertRowActionArgumentColumns = null;
							if (context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
							{
								MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
								insertRowActionArgumentColumns = new Column[]
								{
									messageTable.IsHidden,
									messageTable.VirtualIsRead,
									messageTable.Size
								};
								insertRowAction = delegate(object[] columnValues)
								{
									bool flag2 = (bool)columnValues[0];
									bool flag3 = (bool)columnValues[1];
									long num2 = (long)columnValues[2];
									if (flag2)
									{
										numAssociatedMessagesLinked++;
										sizeAssociatedMessagesLinked += num2;
										if (!flag3)
										{
											numUnreadAssociatedMessagesLinked++;
											return;
										}
									}
									else
									{
										numNormalMessagesLinked++;
										sizeNormalMessagesLinked += num2;
										if (!flag3)
										{
											numUnreadNormalMessagesLinked++;
										}
									}
								};
							}
							context.Diagnostics.TraceElapsed((LID)42336U);
							if (nestedSearchFolder != null)
							{
								allSourceLogicalIndexes = new List<LogicalIndex>(1);
								LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, nestedSearchFolder.GetId(context), nestedSearchFolder.GetLogicalIndexNumber(context).Value);
								logicalIndex.LockInCache();
								allSourceLogicalIndexes.Add(logicalIndex);
							}
							if (allSourceLogicalIndexes != null)
							{
								foreach (LogicalIndex logicalIndex2 in allSourceLogicalIndexes)
								{
									logicalIndex2.UpdateIndex(context, LogicalIndex.CannotRepopulate);
									if (logicalIndex2.IsStale)
									{
										throw new StoreException((LID)36284U, ErrorCodeValue.CallFailed);
									}
								}
							}
							context.Diagnostics.TraceElapsed((LID)34656U);
							bool oldBaseViewIndexSchema = baseViewLogicalIndex.LogicalSortOrder.Count == 2;
							using (InsertOperator insertOperator = Factory.CreateInsertFromSelectOperator(context.Culture, context, baseViewLogicalIndex.IndexTable, populationQueryOperatorDefinition.CreateOperator(context), columnsToInsert, insertRowAction, insertRowActionArgumentColumns, false, !oldBaseViewIndexSchema, true))
							{
								bool interruptsEnabled = false;
								if (!oldBaseViewIndexSchema)
								{
									IInterruptControl interruptControl = new SearchFolder.SearchInterruptControl(TimeSpan.FromSeconds(2.0), SearchFolder.BaseViewPopulationBatchSize, () => shouldTaskContinue() && !LockManager.HasContention(this.Mailbox.SharedState));
									if (SearchFolder.doSearchPopulationInterruptControlTestHook.Value != null)
									{
										interruptControl = SearchFolder.doSearchPopulationInterruptControlTestHook.Value(interruptControl);
									}
									interruptsEnabled = insertOperator.EnableInterrupts(interruptControl);
								}
								if (!interruptsEnabled)
								{
									if (isTracingEnabled)
									{
										ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: Operator is not interruptible.", searchFolderId);
									}
									if (allFullTextQueries != null)
									{
										if (isTracingEnabled)
										{
											ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: About to pulse mailbox lock to execute all full-text queries at once.", searchFolderId);
										}
										yield return MailboxTaskQueue.TaskStepResult.Result(delegate
										{
											SearchFolder.ExecuteAllFullTextIndexQueries(context, this.Mailbox, allFullTextQueries, shouldTaskContinue, this.diagnostics);
										});
										if (isTracingEnabled)
										{
											ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: Pulsed the mailbox lock", searchFolderId);
										}
										baseViewLogicalIndex.UpdateIndex(context, LogicalIndex.CannotRepopulate);
										if (baseViewLogicalIndex.IsStale)
										{
											throw new StoreException((LID)35680U, ErrorCodeValue.CallFailed);
										}
										this.InvalidateIndexesForPopulation(context, true);
										if (allSourceLogicalIndexes != null)
										{
											foreach (LogicalIndex logicalIndex3 in allSourceLogicalIndexes)
											{
												logicalIndex3.UpdateIndex(context, LogicalIndex.CannotRepopulate);
												if (logicalIndex3.IsStale)
												{
													throw new StoreException((LID)52064U, ErrorCodeValue.CallFailed);
												}
											}
										}
									}
								}
								for (;;)
								{
									context.Diagnostics.TraceElapsed((LID)51040U);
									context.GetConnection().NonFatalDuplicateKey = true;
									int messagesLinkedInThisChunk;
									try
									{
										messagesLinkedInThisChunk = (int)insertOperator.ExecuteScalar();
										totalMessagesLinked += messagesLinkedInThisChunk;
										if (isTracingEnabled)
										{
											ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, int, int>(0L, "Search folder {0}: Linked a chunk of {1} messages. Total messages linked: {2}.", searchFolderId, messagesLinkedInThisChunk, totalMessagesLinked);
										}
									}
									catch (DuplicateKeyException exception)
									{
										context.OnExceptionCatch(exception);
										Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PossibleDuplicateMID, new object[]
										{
											base.Mailbox.MailboxGuid,
											base.Mailbox.SharedState.DatabaseGuid
										});
										throw new StoreException((LID)48000U, ErrorCodeValue.DuplicateObject, "Possible duplicate MID detected.");
									}
									finally
									{
										context.GetConnection().NonFatalDuplicateKey = false;
									}
									if (messagesLinkedInThisChunk != 0)
									{
										if (context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
										{
											base.UpdateSearchFolderAggregateCountsForLinkedMessages(context, false, numNormalMessagesLinked, numUnreadNormalMessagesLinked, sizeNormalMessagesLinked);
											base.UpdateSearchFolderAggregateCountsForLinkedMessages(context, true, numAssociatedMessagesLinked, numUnreadAssociatedMessagesLinked, sizeAssociatedMessagesLinked);
											numNormalMessagesLinked = 0;
											numAssociatedMessagesLinked = 0;
											numUnreadNormalMessagesLinked = 0;
											numUnreadAssociatedMessagesLinked = 0;
											sizeNormalMessagesLinked = 0L;
											sizeAssociatedMessagesLinked = 0L;
										}
										if (isLoggingEnabled)
										{
											this.diagnostics.OnLinkedResults(messagesLinkedInThisChunk, messagesLinkedInThisChunk, totalMessagesLinked, stopWatch.Elapsed);
											stopWatch.Restart();
										}
										SearchPopulationMessagesLinkedNotificationEvent nev = new SearchPopulationMessagesLinkedNotificationEvent(base.Mailbox.Database, base.Mailbox.MailboxNumber, null, context.ClientType, searchFolderId);
										context.RiseNotificationEvent(nev);
									}
									context.Diagnostics.TraceElapsed((LID)64864U);
									if (insertOperator.Interrupted)
									{
										StoreFullTextIndexQuery fullTextQueryToRefill = null;
										if (allFullTextQueries != null)
										{
											foreach (StoreFullTextIndexQuery storeFullTextIndexQuery in allFullTextQueries)
											{
												if (storeFullTextIndexQuery.NeedsRefill)
												{
													fullTextQueryToRefill = storeFullTextIndexQuery;
												}
											}
										}
										if (isTracingEnabled)
										{
											ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, string>(0L, "Search folder {0}: About to pulse mailbox mailbox lock{1}.", searchFolderId, (fullTextQueryToRefill != null) ? " to refull full-text results" : string.Empty);
										}
										if (fullTextQueryToRefill != null)
										{
											yield return MailboxTaskQueue.TaskStepResult.Result(delegate
											{
												SearchFolder.ExecuteRefillFullTextIndexQuery(context, this.Mailbox, fullTextQueryToRefill, false, shouldTaskContinue, this.diagnostics);
											});
										}
										else
										{
											yield return MailboxTaskQueue.TaskStepResult.Result(delegate
											{
												SearchFolder.EmptyPulseCallback(context, shouldTaskContinue);
											});
										}
										context.Diagnostics.TraceElapsed((LID)47968U);
										if (isLoggingEnabled)
										{
											this.diagnostics.OnSearchOperation("SPLS", stopWatch.Elapsed);
											stopWatch.Restart();
										}
										if (isTracingEnabled)
										{
											ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0}: About to process the next batch of search hits after pulsing the mailbox lock.", searchFolderId);
										}
										baseViewLogicalIndex.UpdateIndex(context, LogicalIndex.CannotRepopulate);
										if (baseViewLogicalIndex.IsStale)
										{
											break;
										}
										this.InvalidateIndexesForPopulation(context, true);
										if (allSourceLogicalIndexes != null)
										{
											foreach (LogicalIndex logicalIndex4 in allSourceLogicalIndexes)
											{
												logicalIndex4.UpdateIndex(context, LogicalIndex.CannotRepopulate);
												if (logicalIndex4.IsStale)
												{
													throw new StoreException((LID)43872U, ErrorCodeValue.CallFailed);
												}
											}
										}
									}
									if (!insertOperator.Interrupted)
									{
										goto Block_83;
									}
								}
								throw new StoreException((LID)60256U, ErrorCodeValue.CallFailed);
								Block_83:
								context.Diagnostics.TraceElapsed((LID)64352U);
							}
							if (context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Sql)
							{
								this.SetMessageCountsAfterPopulation(context);
							}
						}
					}
					context.Diagnostics.TraceElapsed((LID)48480U);
					if (allFullTextQueries != null)
					{
						if (allFullTextQueries.Any((StoreFullTextIndexQuery ftiQuery) => ftiQuery.Failed))
						{
							this.MarkSearchPopulationFailed(context, true);
							goto IL_1458;
						}
					}
					this.CleanupPopulation(context);
					IL_1458:
					if (isLoggingEnabled)
					{
						this.diagnostics.OnCompleteQuery(totalMessagesLinked, maxCount != 0 && totalMessagesLinked >= maxCount, (uint)totalMessagesLinked, this.GetSearchState(context), context.Diagnostics.ClientActionString);
					}
					context.Diagnostics.SetFastWaitTime(this.diagnostics.GetFastTotalResponseTime());
					if (!this.IsStaticSearch(context))
					{
						LogicalIndex.AddLogicalIndexMaintenanceBreadcrumb(context, base.Mailbox.MailboxPartitionNumber, LogicalIndex.LogicalOperation.SearchPopulationEnded, new object[]
						{
							searchFolderId.To26ByteArray(),
							this.SerializeQueryScopeForBreadcrumb(context)
						});
					}
					succeeded = true;
				}
				finally
				{
					bool flag = false;
					bool commit = false;
					try
					{
						if (!context.IsMailboxOperationStarted)
						{
							ErrorCode first = context.StartMailboxOperationForFailureHandling();
							if (first == ErrorCode.NoError)
							{
								flag = true;
							}
						}
						if (context.IsMailboxOperationStarted)
						{
							if (allFullTextQueries != null)
							{
								foreach (StoreFullTextIndexQuery storeFullTextIndexQuery2 in allFullTextQueries)
								{
									storeFullTextIndexQuery2.Cleanup();
								}
							}
							if (baseViewLogicalIndexLockedInCache)
							{
								baseViewLogicalIndex.UnlockInCache();
							}
							if (allSourceLogicalIndexes != null)
							{
								foreach (LogicalIndex logicalIndex5 in allSourceLogicalIndexes)
								{
									logicalIndex5.UnlockInCache();
								}
							}
						}
						commit = true;
					}
					finally
					{
						if (flag)
						{
							context.EndMailboxOperation(commit, false, true);
						}
					}
					context.Diagnostics.TraceElapsed((LID)56672U);
					if (isTracingEnabled)
					{
						ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, string>(0L, "Search folder {0}: Completed population {1}successfully.", searchFolderId, succeeded ? string.Empty : "un");
					}
					FaultInjection.InjectFault(SearchFolder.finishedSearchPopulationTestHook);
				}
			}
			yield break;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0005A748 File Offset: 0x00058948
		private int MaxCountForClient(Context context)
		{
			int num = ConfigurationSchema.MaxHitsForFullTextIndexSearches.Value;
			ClientType clientType = context.ClientType;
			if (clientType != ClientType.Management)
			{
				if (clientType != ClientType.TimeBasedAssistants)
				{
					if (clientType == ClientType.EDiscoverySearch && num < 10000)
					{
						num = 10000;
					}
				}
				else if (num < 10000)
				{
					string name = base.GetName(context);
					if (name != null && name.Equals("SearchDiscoveryHoldsFolder", StringComparison.OrdinalIgnoreCase))
					{
						num = 10000;
					}
				}
			}
			else if (num < 10000)
			{
				string name2 = base.GetName(context);
				if (name2 != null && name2.StartsWith("SearchAuditMailboxFolder", StringComparison.OrdinalIgnoreCase))
				{
					num = 10000;
				}
			}
			return num;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0005A7D8 File Offset: 0x000589D8
		private string GetSpecialFolderNameForTracing(Context context, ExchangeId folderId)
		{
			ExchangeId[] specialFolders = SpecialFoldersCache.GetSpecialFolders(context, base.Mailbox);
			string text = null;
			if (folderId == specialFolders[1])
			{
				text = "Root";
			}
			else if (!base.Mailbox.IsPublicFolderMailbox)
			{
				if (folderId == specialFolders[9])
				{
					text = "TopOfIS";
				}
				else if (folderId == specialFolders[10])
				{
					text = "Inbox";
				}
			}
			return text ?? "N/A";
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0005B674 File Offset: 0x00059874
		private IEnumerator<MailboxTaskQueue.TaskStepResult> DoOptimizedConversationSearch(Context context, SimpleQueryOperator.SimpleQueryOperatorDefinition planDefinition, IList<StoreFullTextIndexQuery> allFullTextQueries, int maxMessagesToLink, Func<bool> shouldTaskContinue)
		{
			int totalMessagesLinked = 0;
			uint totalFullTextIndexRowsProcessed = 0U;
			bool isTracingEnabled = ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace);
			bool isLoggingEnabled = FullTextIndexLogger.IsLoggingEnabled;
			Stopwatch stopWatch = (isTracingEnabled || isLoggingEnabled) ? Stopwatch.StartNew() : null;
			ExchangeId searchFolderId = base.GetId(context);
			StoreFullTextIndexQuery fullTextIndexQuery = allFullTextQueries[0];
			context.Diagnostics.TraceElapsed((LID)44384U);
			if (isTracingEnabled)
			{
				ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, string, int>(0L, "Search folder {0} ({1}): Beginning optimized conversation search population. Maximum search hits is {2}.", searchFolderId, base.GetName(context), maxMessagesToLink);
			}
			bool succeeded = false;
			try
			{
				context.Diagnostics.TraceElapsed((LID)36192U);
				yield return MailboxTaskQueue.TaskStepResult.Result(delegate
				{
					SearchFolder.ExecuteRefillFullTextIndexQuery(context, this.Mailbox, fullTextIndexQuery, true, shouldTaskContinue, this.diagnostics);
				});
				IList<FullTextIndexRow> fullTextIndexRows = fullTextIndexQuery.Rows;
				MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
				SearchCriteria residualCriteria = null;
				SearchFolder.FoldersScope scope = this.GetQueryScope(context);
				if ((scope.Folders.Count != 1 && !scope.IsRecursiveIpmSearch) || !this.IsResidualCriteriaFidOnly(context, planDefinition.Criteria))
				{
					residualCriteria = planDefinition.Criteria;
				}
				List<SearchFolder.InstantSearchResultsEntry> conversationRows = new List<SearchFolder.InstantSearchResultsEntry>(maxMessagesToLink);
				Dictionary<int, SearchFolder.InstantSearchResultsEntry> knownConversations = new Dictionary<int, SearchFolder.InstantSearchResultsEntry>(maxMessagesToLink);
				List<SearchFolder.InstantSearchResultsEntry> searchResultEntriesToProcess = new List<SearchFolder.InstantSearchResultsEntry>(fullTextIndexRows.Count);
				Dictionary<Column, Column> renameDictionary = null;
				Dictionary<int, SearchFolder.InstantSearchResultsEntry> entriesWithoutConversationDocumentId = null;
				while (fullTextIndexRows.Count > 0)
				{
					uint fullTextIndexRowsProcessedThisBatch = 0U;
					int messagesLinkedThisBatch = 0;
					if (isTracingEnabled || isLoggingEnabled)
					{
						stopWatch.Stop();
						this.diagnostics.OnSearchOperation("SPLS", stopWatch.Elapsed);
						if (isTracingEnabled)
						{
							ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, int, long>(0L, "Search folder {0}: About to process the next batch of {1} search hits for optimized instant search (after pulsing the mailbox lock for {2}ms).", searchFolderId, fullTextIndexRows.Count, stopWatch.ElapsedMilliseconds);
						}
						stopWatch.Restart();
					}
					searchResultEntriesToProcess.Clear();
					for (int i = 0; i < fullTextIndexRows.Count; i++)
					{
						FullTextIndexRow fullTextIndexRow = fullTextIndexRows[i];
						if (fullTextIndexRow.ConversationDocumentId == null || fullTextIndexRow.ConversationDocumentId.Value != 0)
						{
							searchResultEntriesToProcess.Add(new SearchFolder.InstantSearchResultsEntry
							{
								SortPosition = 0,
								MessageDocumentId = fullTextIndexRow.DocumentId,
								ConversationDocumentId = ((residualCriteria != null) ? 0 : fullTextIndexRow.ConversationDocumentId.GetValueOrDefault(0)),
								Count = 0
							});
						}
						else
						{
							totalFullTextIndexRowsProcessed += 1U;
						}
					}
					((IRefillableTableContents)fullTextIndexQuery).MarkChunkConsumed();
					int startIndex = 0;
					while (startIndex < searchResultEntriesToProcess.Count)
					{
						int num = 0;
						int currentIndex;
						for (currentIndex = startIndex; currentIndex < searchResultEntriesToProcess.Count; currentIndex++)
						{
							SearchFolder.InstantSearchResultsEntry instantSearchResultsEntry = searchResultEntriesToProcess[currentIndex];
							if (instantSearchResultsEntry.ConversationDocumentId == 0)
							{
								if (entriesWithoutConversationDocumentId == null)
								{
									entriesWithoutConversationDocumentId = new Dictionary<int, SearchFolder.InstantSearchResultsEntry>(maxMessagesToLink / 2);
								}
								entriesWithoutConversationDocumentId.Add(instantSearchResultsEntry.MessageDocumentId, instantSearchResultsEntry);
							}
							else
							{
								num++;
							}
							if (totalMessagesLinked + num >= maxMessagesToLink || (entriesWithoutConversationDocumentId != null && entriesWithoutConversationDocumentId.Count >= ((residualCriteria != null) ? (SearchFolder.BaseViewPopulationBatchSize * 2) : (SearchFolder.BaseViewPopulationBatchSize + 2))))
							{
								currentIndex++;
								break;
							}
						}
						if (entriesWithoutConversationDocumentId != null && entriesWithoutConversationDocumentId.Count != 0)
						{
							if (isTracingEnabled)
							{
								ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, int>(0L, "Search folder {0}: need to fetch missing conversation document ID for {1} messages.", searchFolderId, entriesWithoutConversationDocumentId.Count);
							}
							List<KeyRange> list = new List<KeyRange>(entriesWithoutConversationDocumentId.Count);
							foreach (KeyValuePair<int, SearchFolder.InstantSearchResultsEntry> keyValuePair in entriesWithoutConversationDocumentId)
							{
								StartStopKey startStopKey = new StartStopKey(true, new object[]
								{
									base.Mailbox.MailboxPartitionNumber,
									keyValuePair.Key
								});
								list.Add(new KeyRange(startStopKey, startStopKey));
							}
							using (PreReadOperator preReadOperator = Factory.CreatePreReadOperator(context.Culture, context, messageTable.Table, messageTable.MessagePK, list, null, true))
							{
								preReadOperator.ExecuteScalar();
							}
							if (residualCriteria != null && renameDictionary == null)
							{
								renameDictionary = new Dictionary<Column, Column>(2);
								if (base.Mailbox.SharedState.SupportsPerUserFeatures)
								{
									renameDictionary[messageTable.VirtualIsRead] = Factory.CreateFunctionColumn("PerUserIsRead", typeof(bool), PropertyTypeHelper.SizeFromPropType(PropertyType.Boolean), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Boolean), base.Table, new Func<object[], object>(this.GetPerUserReadUnreadColumnFunction), "ComputePerUserIsRead", new Column[]
									{
										messageTable.FolderId,
										messageTable.IsRead,
										messageTable.LcnCurrent
									});
								}
								else
								{
									renameDictionary[messageTable.VirtualIsRead] = messageTable.IsRead;
								}
								renameDictionary[messageTable.VirtualParentDisplay] = TopMessage.CreateVirtualParentDisplayFunctionColumn(messageTable, new Func<object[], object>(this.GetParentDisplayColumnFunction));
							}
							using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessagePK, new Column[]
							{
								messageTable.MessageDocumentId,
								messageTable.ConversationDocumentId
							}, residualCriteria, renameDictionary, 0, 0, list, false, true))
							{
								using (Reader reader = tableOperator.ExecuteReader(false))
								{
									while (reader.Read())
									{
										int @int = reader.GetInt32(messageTable.MessageDocumentId);
										int? nullableInt = reader.GetNullableInt32(messageTable.ConversationDocumentId);
										SearchFolder.InstantSearchResultsEntry instantSearchResultsEntry2 = entriesWithoutConversationDocumentId[@int];
										instantSearchResultsEntry2.ConversationDocumentId = nullableInt.GetValueOrDefault(0);
									}
								}
							}
							entriesWithoutConversationDocumentId.Clear();
						}
						for (int j = startIndex; j < currentIndex; j++)
						{
							SearchFolder.InstantSearchResultsEntry instantSearchResultsEntry3 = searchResultEntriesToProcess[j];
							totalFullTextIndexRowsProcessed += 1U;
							fullTextIndexRowsProcessedThisBatch += 1U;
							if (instantSearchResultsEntry3.ConversationDocumentId != 0)
							{
								totalMessagesLinked++;
								messagesLinkedThisBatch++;
								SearchFolder.InstantSearchResultsEntry instantSearchResultsEntry4;
								if (!knownConversations.TryGetValue(instantSearchResultsEntry3.ConversationDocumentId, out instantSearchResultsEntry4))
								{
									instantSearchResultsEntry3.SortPosition = conversationRows.Count;
									knownConversations.Add(instantSearchResultsEntry3.ConversationDocumentId, instantSearchResultsEntry3);
									conversationRows.Add(instantSearchResultsEntry3);
									instantSearchResultsEntry4 = instantSearchResultsEntry3;
								}
								instantSearchResultsEntry4.Count++;
							}
						}
						if (isLoggingEnabled)
						{
							this.diagnostics.OnLinkedResults(messagesLinkedThisBatch, (int)((fullTextIndexRowsProcessedThisBatch <= 2147483647U) ? fullTextIndexRowsProcessedThisBatch : 2147483647U), totalMessagesLinked, stopWatch.Elapsed);
						}
						if (totalMessagesLinked >= maxMessagesToLink)
						{
							if (residualCriteria == null)
							{
								for (int k = currentIndex; k < searchResultEntriesToProcess.Count; k++)
								{
									SearchFolder.InstantSearchResultsEntry instantSearchResultsEntry5 = searchResultEntriesToProcess[k];
									totalFullTextIndexRowsProcessed += 1U;
									if (instantSearchResultsEntry5.ConversationDocumentId != 0)
									{
										totalMessagesLinked++;
										messagesLinkedThisBatch++;
										SearchFolder.InstantSearchResultsEntry instantSearchResultsEntry6;
										if (!knownConversations.TryGetValue(instantSearchResultsEntry5.ConversationDocumentId, out instantSearchResultsEntry6))
										{
											instantSearchResultsEntry5.SortPosition = conversationRows.Count;
											knownConversations.Add(instantSearchResultsEntry5.ConversationDocumentId, instantSearchResultsEntry5);
											conversationRows.Add(instantSearchResultsEntry5);
											instantSearchResultsEntry6 = instantSearchResultsEntry5;
										}
										instantSearchResultsEntry6.Count++;
									}
								}
								break;
							}
							break;
						}
						else
						{
							startIndex = currentIndex;
						}
					}
					if (isTracingEnabled || isLoggingEnabled)
					{
						stopWatch.Stop();
						if (isTracingEnabled)
						{
							ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug(0L, "Search folder {0}: Linked {1} search hits out of {2} rows evaluated in {3}ms in the current batch ({4} search hits have been linked in total). About to pulse mailbox lock and retrieve the next page of search hits from FAST.", new object[]
							{
								searchFolderId,
								messagesLinkedThisBatch,
								fullTextIndexRowsProcessedThisBatch,
								stopWatch.ElapsedMilliseconds,
								totalMessagesLinked
							});
						}
						stopWatch.Restart();
					}
					if (totalMessagesLinked >= maxMessagesToLink)
					{
						break;
					}
					yield return MailboxTaskQueue.TaskStepResult.Result(delegate
					{
						SearchFolder.ExecuteRefillFullTextIndexQuery(context, this.Mailbox, fullTextIndexQuery, true, shouldTaskContinue, this.diagnostics);
					});
					fullTextIndexRows = fullTextIndexQuery.Rows;
				}
				if (conversationRows.Count > maxMessagesToLink)
				{
					conversationRows.RemoveRange(maxMessagesToLink, conversationRows.Count - maxMessagesToLink);
				}
				this.SearchResults.ConversationRows = conversationRows;
				SearchPopulationMessagesLinkedNotificationEvent searchFolderNotification = new SearchPopulationMessagesLinkedNotificationEvent(base.Mailbox.Database, base.Mailbox.MailboxNumber, null, context.ClientType, searchFolderId);
				context.RiseNotificationEvent(searchFolderNotification);
				succeeded = true;
			}
			finally
			{
				if (isTracingEnabled)
				{
					if (!succeeded)
					{
						stopWatch.Stop();
						ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, long>(0L, "Search folder {0}: The last pulse of the mailbox lock took {1}ms and returned failure, so search population is being stopped prematurely.", searchFolderId, stopWatch.ElapsedMilliseconds);
					}
					else if (fullTextIndexQuery.Done)
					{
						stopWatch.Stop();
						ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, long>(0L, "Search folder {0}: Pulsed the mailbox lock for {1}ms and determined that there were no more search hits returned from FAST, so search population is about to be completed.", searchFolderId, stopWatch.ElapsedMilliseconds);
					}
				}
				fullTextIndexQuery.Cleanup();
			}
			yield break;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0005B6B8 File Offset: 0x000598B8
		private void PromotePropertiesInRestriction(Context context)
		{
			int num;
			SearchCriteria criteria = this.GetCriteria(context, out num);
			if (criteria == null)
			{
				return;
			}
			IList<ExchangeId> folders = this.GetQueryScope(context).Folders;
			foreach (ExchangeId id in folders)
			{
				Folder folder = Folder.OpenFolder(context, base.Mailbox, id);
				if (folder == null)
				{
					throw new CorruptSearchScopeException((LID)42080U, "Scope folder is missing.");
				}
				folder.PromoteProperties(context, false, criteria);
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0005B74C File Offset: 0x0005994C
		private void InitializePopulation(Context context, bool populateWithCi)
		{
			LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, base.GetId(context), this.GetLogicalIndexNumber(context).Value);
			logicalIndex.PrepareForRepopulation(context);
			SearchState searchState = this.GetSearchState(context);
			if (!SearchFolder.IsStaticSearch(searchState))
			{
				this.PromotePropertiesInRestriction(context);
			}
			if (!SearchFolder.IsStaticSearch(searchState))
			{
				this.AddSearchBacklinks(context);
			}
			SearchState searchState2 = populateWithCi ? SearchState.CiTotally : SearchState.TwirTotally;
			if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, SearchState>(0L, "Search folder {0}: Adding {1} to search state.", base.GetId(context), searchState2);
			}
			searchState &= ~(SearchState.CiTotally | SearchState.CiWithTwirResidual | SearchState.TwirMostly | SearchState.TwirTotally);
			searchState |= searchState2;
			base.SetColumn(context, base.FolderTable.SearchState, (int)searchState);
			base.InitializeSearchFolderAggregateCounts(context);
			logicalIndex.MarkBaseViewRepopulation(context);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0005B814 File Offset: 0x00059A14
		private void InvalidateIndexesForPopulation(Context context, bool excludeBaseView)
		{
			ExchangeId id = base.GetId(context);
			if (!excludeBaseView)
			{
				LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, id, LogicalIndexType.SearchFolderBaseView);
			}
			LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, id, LogicalIndexType.SearchFolderMessages);
			LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, id, LogicalIndexType.Conversations);
			IList<ExchangeId> searchBacklinks = base.GetSearchBacklinks(context, false);
			foreach (ExchangeId exchangeId in searchBacklinks)
			{
				SearchFolder searchFolder = Folder.OpenFolder(context, base.Mailbox, exchangeId) as SearchFolder;
				if (searchFolder == null)
				{
					SearchFolder.ReportCorruptSearchBacklink(context, this, exchangeId, false);
					throw new CorruptSearchBacklinkException((LID)54047U, "Backlink folder doesn't exist or is not a search folder.");
				}
				searchFolder.InvalidateIndexesForPopulation(context, false);
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0005B8D8 File Offset: 0x00059AD8
		private void CleanupPopulation(Context context)
		{
			SearchState searchState = this.GetSearchState(context);
			searchState &= ~SearchState.Rebuild;
			if (SearchFolder.IsStaticSearch(searchState))
			{
				searchState &= ~SearchState.Running;
			}
			base.SetColumn(context, base.FolderTable.SearchState, (int)searchState);
			this.Save(context);
			this.RepopulateNestedSearches(context);
			SearchCompleteNotificationEvent nev = new SearchCompleteNotificationEvent(base.Mailbox.Database, base.Mailbox.MailboxNumber, null, context.ClientType, base.GetId(context));
			context.RiseNotificationEvent(nev);
			if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId, long, long>(0L, "Search folder {0}: Linked in {1} normal messages and {2} FAI messages.", base.GetId(context), base.GetMessageCount(context), base.GetHiddenItemCount(context));
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0005B98C File Offset: 0x00059B8C
		private void SetMessageCountsAfterPopulation(Context context)
		{
			if (context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
			{
				return;
			}
			bool flag = false;
			Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
			LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, base.GetId(context), this.GetLogicalIndexNumber(context).Value);
			logicalIndex.UpdateIndex(context, LogicalIndex.CannotRepopulate);
			int num = 0;
			int num2 = 0;
			Column[] columnsToFetch = new Column[]
			{
				logicalIndex.IndexTable.Columns[0]
			};
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				logicalIndex.MailboxPartitionNumber,
				logicalIndex.LogicalIndexNumber,
				false
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, logicalIndex.IndexTable, logicalIndex.IndexTable.PrimaryKeyIndex, columnsToFetch, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, false))
			{
				using (CountOperator countOperator = Factory.CreateCountOperator(context.Culture, context, tableOperator, false))
				{
					num = (int)countOperator.ExecuteScalar();
				}
			}
			startStopKey = new StartStopKey(true, new object[]
			{
				logicalIndex.MailboxPartitionNumber,
				logicalIndex.LogicalIndexNumber,
				true
			});
			using (TableOperator tableOperator2 = Factory.CreateTableOperator(context.Culture, context, logicalIndex.IndexTable, logicalIndex.IndexTable.PrimaryKeyIndex, columnsToFetch, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, false))
			{
				using (CountOperator countOperator2 = Factory.CreateCountOperator(context.Culture, context, tableOperator2, false))
				{
					num2 = (int)countOperator2.ExecuteScalar();
				}
			}
			if (flag)
			{
				base.GetMessageCount(context);
				base.GetHiddenItemCount(context);
				return;
			}
			base.SetColumn(context, base.FolderTable.MessageCount, (long)num);
			base.SetColumn(context, base.FolderTable.HiddenItemCount, (long)num2);
			this.SetProperty(context, PropTag.Folder.SearchFolderMsgCount, num + num2);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0005BBE8 File Offset: 0x00059DE8
		private void RepopulateNestedSearches(Context context)
		{
			IList<ExchangeId> searchBacklinks = base.GetSearchBacklinks(context, false);
			foreach (ExchangeId exchangeId in searchBacklinks)
			{
				SearchFolder searchFolder = Folder.OpenFolder(context, base.Mailbox, exchangeId) as SearchFolder;
				if (searchFolder == null)
				{
					SearchFolder.ReportCorruptSearchBacklink(context, this, exchangeId, false);
					throw new CorruptSearchBacklinkException((LID)37360U, "Backlink folder doesn't exist or is not a search folder.");
				}
				if (SearchFolder.repopulateNestedSearchTestHook.Value != null)
				{
					SearchFolder.repopulateNestedSearchTestHook.Value(context, searchFolder);
				}
				searchFolder.Repopulate(context);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0005BC8C File Offset: 0x00059E8C
		internal void MarkSearchPopulationFailed(Context context, bool fullTextIndexQueryFailed)
		{
			SearchState searchState = this.GetSearchState(context);
			if (!this.IsStaticSearch(context))
			{
				try
				{
					this.RemoveSearchBacklinks(context);
				}
				catch (ObjectNotFoundException exception)
				{
					context.OnExceptionCatch(exception);
				}
				catch (CorruptSearchScopeException exception2)
				{
					context.OnExceptionCatch(exception2);
				}
				searchState |= SearchState.Static;
			}
			searchState &= ~SearchState.Rebuild;
			searchState &= ~SearchState.Running;
			if (fullTextIndexQueryFailed)
			{
				searchState |= SearchState.FullTextIndexQueryFailed;
			}
			searchState |= SearchState.Error;
			base.SetColumn(context, base.FolderTable.SearchState, (int)searchState);
			SearchCompleteNotificationEvent nev = new SearchCompleteNotificationEvent(base.Mailbox.Database, base.Mailbox.MailboxNumber, null, context.ClientType, base.GetId(context));
			context.RiseNotificationEvent(nev);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0005BD54 File Offset: 0x00059F54
		public bool TestMessage(Context context, ITWIR twir)
		{
			bool? flag = null;
			try
			{
				int num;
				flag = new bool?(this.GetCriteria(context, out num).Evaluate(twir, (context.Culture == null) ? null : context.Culture.CompareInfo));
			}
			catch (StoreException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				if (ex.Error != ErrorCodeValue.TooComplex)
				{
					DiagnosticContext.TraceLocation((LID)34400U);
					throw;
				}
				if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<ExchangeId>(0L, "Search folder {0} skipped one message during TWIR evaluation because the criteria is too complex.", base.GetId(context));
				}
			}
			return flag == true;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0005BE0C File Offset: 0x0005A00C
		public ICollection<FidMid> FilterMessages(Context context, List<FidMid> messages, bool? associated)
		{
			return this.FilterMessages(context, null, messages, associated);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0005BE18 File Offset: 0x0005A018
		public ICollection<FidMid> FilterMessages(Context context, LogicalIndex baseViewIndex, List<FidMid> messages, bool? associated)
		{
			ICollection<FidMid> collection;
			if (messages.Count > 4)
			{
				collection = new HashSet<FidMid>();
			}
			else
			{
				collection = new List<FidMid>(messages.Count);
			}
			if (baseViewIndex == null)
			{
				baseViewIndex = this.GetBaseViewLogicalIndex(context, false);
			}
			if (baseViewIndex == null || baseViewIndex.IsStale)
			{
				return collection;
			}
			SearchFolder searchFolder = this;
			IList<ExchangeId> folders = this.GetQueryScope(context).Folders;
			bool flag = this.GetRestrictionFiltersNothing(context);
			while (folders.Count == 1 && !searchFolder.IsRecursiveSearch(context))
			{
				SearchFolder searchFolder2 = Folder.OpenFolder(context, base.Mailbox, folders[0]) as SearchFolder;
				if (searchFolder2 == null)
				{
					break;
				}
				searchFolder = searchFolder2;
				folders = searchFolder2.GetQueryScope(context).Folders;
				flag &= searchFolder2.GetRestrictionFiltersNothing(context);
			}
			for (int i = 0; i < messages.Count; i++)
			{
				if (this.forceProbingForConversationViews || folders.Contains(messages[i].FolderId))
				{
					if (flag && !this.forceProbingForConversationViews)
					{
						collection.Add(messages[i]);
					}
					else if (this.LookupMessageByMid(context, baseViewIndex, messages[i].MessageId, associated) != null)
					{
						collection.Add(messages[i]);
					}
				}
			}
			return collection;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0005BF48 File Offset: 0x0005A148
		internal bool GetRestrictionFiltersNothing(Context context)
		{
			bool? restrictionFiltersNothing = (bool?)base.GetComponentData(SearchFolder.restrictionFiltersNothingSlot);
			if (restrictionFiltersNothing == null)
			{
				restrictionFiltersNothing = new bool?(this.GetRestriction(context) is RestrictionTrue);
				this.SetRestrictionFiltersNothing(restrictionFiltersNothing);
			}
			return restrictionFiltersNothing.Value;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0005BF93 File Offset: 0x0005A193
		internal void SetRestrictionFiltersNothing(bool? valueToSet)
		{
			base.SetComponentData(SearchFolder.restrictionFiltersNothingSlot, valueToSet);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0005BFA8 File Offset: 0x0005A1A8
		internal bool GetHasCountRestriction(Context context)
		{
			bool? hasCountRestriction = (bool?)base.GetComponentData(SearchFolder.hasCountRestrictionSlot);
			if (hasCountRestriction == null)
			{
				Restriction restriction = this.GetRestriction(context);
				int maxCount = this.GetMaxCount(restriction);
				hasCountRestriction = new bool?(maxCount != 0);
				this.SetHasCountRestriction(hasCountRestriction);
			}
			return hasCountRestriction.Value;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0005BFFB File Offset: 0x0005A1FB
		internal void SetHasCountRestriction(bool? valueToSet)
		{
			base.SetComponentData(SearchFolder.hasCountRestrictionSlot, valueToSet);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0005C010 File Offset: 0x0005A210
		internal int? LookupMessageByMid(Context context, ExchangeId messageId, bool? associated)
		{
			LogicalIndex baseViewLogicalIndex = this.GetBaseViewLogicalIndex(context, false);
			if (baseViewLogicalIndex == null)
			{
				return null;
			}
			return this.LookupMessageByMid(context, baseViewLogicalIndex, messageId, associated);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0005C040 File Offset: 0x0005A240
		internal int? LookupMessageByMid(Context context, LogicalIndex baseViewIndex, ExchangeId messageId, bool? associated)
		{
			if (baseViewIndex.IsStale)
			{
				return null;
			}
			int? result = null;
			if (SearchFolder.probingTestHook.Value != null)
			{
				SearchFolder.probingTestHook.Value();
			}
			if (baseViewIndex.OutstandingMaintenance)
			{
				baseViewIndex.UpdateIndex(context, LogicalIndex.CannotRepopulate);
			}
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
			Column[] array = new Column[]
			{
				baseViewIndex.RenameDictionary[messageTable.MessageDocumentId]
			};
			if (associated == null || !associated.Value)
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					baseViewIndex.MailboxPartitionNumber,
					baseViewIndex.LogicalIndexNumber,
					false,
					messageId.To26ByteArray()
				});
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, baseViewIndex.IndexTable, baseViewIndex.IndexTable.PrimaryKeyIndex, array, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (reader.Read())
						{
							result = new int?(reader.GetInt32(array[0]));
						}
					}
				}
			}
			if (result == null && (associated == null || associated.Value))
			{
				StartStopKey startStopKey2 = new StartStopKey(true, new object[]
				{
					baseViewIndex.MailboxPartitionNumber,
					baseViewIndex.LogicalIndexNumber,
					true,
					messageId.To26ByteArray()
				});
				using (TableOperator tableOperator2 = Factory.CreateTableOperator(context.Culture, context, baseViewIndex.IndexTable, baseViewIndex.IndexTable.PrimaryKeyIndex, array, null, null, 0, 1, new KeyRange(startStopKey2, startStopKey2), false, true))
				{
					using (Reader reader2 = tableOperator2.ExecuteReader(false))
					{
						if (reader2.Read())
						{
							result = new int?(reader2.GetInt32(array[0]));
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0005C298 File Offset: 0x0005A498
		internal LogicalIndex GetBaseViewLogicalIndex(Context context, bool current)
		{
			int? logicalIndexNumber = this.GetLogicalIndexNumber(context);
			if (logicalIndexNumber == null)
			{
				return null;
			}
			LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, base.GetId(context), logicalIndexNumber.Value);
			if (current)
			{
				if (logicalIndex.IsStale)
				{
					return null;
				}
				logicalIndex.UpdateIndex(context, LogicalIndex.CannotRepopulate);
			}
			return logicalIndex;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0005C2ED File Offset: 0x0005A4ED
		internal void InvalidateCachedQueryScope(Context context)
		{
			if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Invalidating cached query scope for {0}", base.GetId(context));
			}
			this.CachedQueryScope = null;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0005C31C File Offset: 0x0005A51C
		internal Guid? GetNullableSearchGuid(Context context)
		{
			byte[] array = (byte[])this.GetPropertyValue(context, PropTag.Folder.SearchGUID);
			if (array != null && array.Length == 16)
			{
				return new Guid?(new Guid(array));
			}
			return null;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0005C35C File Offset: 0x0005A55C
		private void UpdateCachedQueryScope(Context context)
		{
			this.CachedQueryScope = SearchFolder.FoldersScope.Create(context, this);
			if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId, string>(0L, "Updated cached query scope for {0}: {1}", base.GetId(context), this.CachedQueryScope.Folders.GetAsString<IList<ExchangeId>>());
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0005C3AC File Offset: 0x0005A5AC
		private void RemoveFromRecursiveSearch(Context context, IList<ExchangeId> recursivelySearchedFolders)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			ExchangeId id = base.GetId(context);
			bool flag = LogicalIndexCache.FolderHasConversationIndex(context, base.Mailbox, id);
			this.CheckRecursiveSearchScopeMayBeModified(context, recursivelySearchedFolders);
			foreach (ExchangeId scopeFolderId in recursivelySearchedFolders)
			{
				LogicalIndex.AddLogicalIndexMaintenanceBreadcrumb(context, base.Mailbox.MailboxPartitionNumber, LogicalIndex.LogicalOperation.RemovedFromSearchScope, new object[]
				{
					base.GetId(context).To26ByteArray(),
					scopeFolderId.To26ByteArray()
				});
				this.RemoveOneBacklink(context, scopeFolderId, true);
			}
			List<SearchCriteria> list = new List<SearchCriteria>(recursivelySearchedFolders.Count);
			for (int i = 0; i < recursivelySearchedFolders.Count; i++)
			{
				bool flag2 = false;
				Folder folder = Folder.OpenFolder(context, base.Mailbox, recursivelySearchedFolders[i]);
				if (folder != null && folder.GetMessageCount(context) == 0L && folder.GetHiddenItemCount(context) == 0L)
				{
					flag2 = true;
				}
				if (!flag2)
				{
					list.Add(Factory.CreateSearchCriteriaCompare(messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(recursivelySearchedFolders[i].To26ByteArray())));
				}
			}
			if (list.Count > 0)
			{
				using (SimpleQueryOperator simpleQueryOperator = this.BaseViewOperator(context, base.Mailbox, new List<Column>(1)
				{
					messageTable.MessageDocumentId
				}, Factory.CreateSearchCriteriaOr(list.ToArray()), null))
				{
					using (Reader reader = simpleQueryOperator.ExecuteReader(false))
					{
						while (reader.Read())
						{
							int @int = reader.GetInt32(messageTable.MessageDocumentId);
							using (TopMessage topMessage = TopMessage.OpenMessage(context, base.Mailbox, @int))
							{
								ModifiedSearchFolders modifiedSearchFolders = new ModifiedSearchFolders();
								this.RemoveMessageLink(context, topMessage, true, null, modifiedSearchFolders);
								if (flag)
								{
									byte[] messageConversationId = Conversations.GetMessageConversationId(context, topMessage);
									if (messageConversationId != null)
									{
										this.forceProbingForConversationViews = true;
										try
										{
											using (ConversationItem conversationItem = ConversationItem.OpenConversationItem(context, base.Mailbox, messageConversationId))
											{
												if (conversationItem != null)
												{
													ConversationMembers conversationMembers = conversationItem.GetConversationMembers(context, null, null);
													conversationItem.ModifiedMessage = topMessage;
													Conversations.TrackSearchFolderConversationIndexUpdate(context, base.Mailbox, conversationItem, conversationMembers, modifiedSearchFolders.DeletedFrom, LogicalIndex.LogicalOperation.Delete);
												}
											}
										}
										finally
										{
											this.forceProbingForConversationViews = false;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0005C664 File Offset: 0x0005A864
		private void AddToRecursiveSearch(Context context, IList<ExchangeId> recursivelySearchedFolders)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			ExchangeId id = base.GetId(context);
			bool flag = LogicalIndexCache.FolderHasConversationIndex(context, base.Mailbox, id);
			this.CheckRecursiveSearchScopeMayBeModified(context, recursivelySearchedFolders);
			using (this.SetUserIdentityOnContext(context))
			{
				foreach (ExchangeId exchangeId in recursivelySearchedFolders)
				{
					LogicalIndex.AddLogicalIndexMaintenanceBreadcrumb(context, base.Mailbox.MailboxPartitionNumber, LogicalIndex.LogicalOperation.AddedToSearchScope, new object[]
					{
						base.GetId(context).To26ByteArray(),
						exchangeId.To26ByteArray()
					});
					this.AddOneBacklink(context, exchangeId, true);
					bool flag2 = false;
					Folder folder = Folder.OpenFolder(context, base.Mailbox, exchangeId);
					if (folder != null && folder.GetMessageCount(context) == 0L && folder.GetHiddenItemCount(context) == 0L)
					{
						flag2 = true;
					}
					if (!flag2)
					{
						StartStopKey startStopKey = new StartStopKey(true, new object[]
						{
							base.Mailbox.MailboxPartitionNumber,
							exchangeId.To26ByteArray()
						});
						using (SimpleQueryOperator simpleQueryOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, new PhysicalColumn[]
						{
							messageTable.MessageDocumentId
						}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
						{
							using (Reader reader = simpleQueryOperator.ExecuteReader(false))
							{
								while (reader.Read())
								{
									int @int = reader.GetInt32(messageTable.MessageDocumentId);
									using (TopMessage topMessage = TopMessage.OpenMessage(context, base.Mailbox, @int))
									{
										ModifiedSearchFolders modifiedSearchFolders = new ModifiedSearchFolders();
										if (this.TestMessage(context, topMessage))
										{
											this.AddMessageLink(context, topMessage, null, modifiedSearchFolders);
											if (flag)
											{
												byte[] messageConversationId = Conversations.GetMessageConversationId(context, topMessage);
												if (messageConversationId != null)
												{
													this.forceProbingForConversationViews = true;
													try
													{
														using (ConversationItem conversationItem = ConversationItem.OpenConversationItem(context, base.Mailbox, messageConversationId))
														{
															if (conversationItem != null)
															{
																ConversationMembers conversationMembers = conversationItem.GetConversationMembers(context, null, null);
																conversationItem.ModifiedMessage = topMessage;
																Conversations.TrackSearchFolderConversationIndexUpdate(context, base.Mailbox, conversationItem, conversationMembers, modifiedSearchFolders.InsertedInto, LogicalIndex.LogicalOperation.Insert);
															}
														}
													}
													finally
													{
														this.forceProbingForConversationViews = false;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0005C97C File Offset: 0x0005AB7C
		private void CheckRecursiveSearchScopeMayBeModified(Context context, IList<ExchangeId> recursivelySearchedFolders)
		{
			SearchState searchState = this.GetSearchState(context);
			if (!SearchFolder.IsRecursiveSearch(searchState))
			{
				throw new CorruptSearchScopeException((LID)35320U, "Cannot modify the recursive search scope because the search is not recursive.");
			}
			if (SearchFolder.IsSearchEvaluationInProgress(searchState))
			{
				throw new SearchEvaluationInProgressException((LID)51704U, "Search folder is currently being populated.");
			}
			IList<ExchangeId> scopeFolders = this.GetScopeFolders(context);
			foreach (ExchangeId item in scopeFolders)
			{
				if (recursivelySearchedFolders.Contains(item))
				{
					throw new NotSupportedException((LID)45855U, "Don't currently support modifications to recursive search scopes which scope a folder implicitly (via a parent) and explicitly.");
				}
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0005CA28 File Offset: 0x0005AC28
		private bool IsFolderInSearchScope(Context context, ExchangeId folderId)
		{
			SearchFolder.FoldersScope queryScope = this.GetQueryScope(context);
			return queryScope.Folders.Contains(folderId);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0005CA4C File Offset: 0x0005AC4C
		private byte[] SerializeQueryScopeForBreadcrumb(Context context)
		{
			IList<ExchangeId> folders = this.GetQueryScope(context).Folders;
			if (folders.Count > 30)
			{
				return null;
			}
			List<object> list = new List<object>(folders.Count);
			foreach (ExchangeId exchangeId in folders)
			{
				list.Add(exchangeId.To26ByteArray());
			}
			return SerializedValue.Serialize(list);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0005CB90 File Offset: 0x0005AD90
		private bool IsResidualCriteriaFidOnly(Context context, SearchCriteria criteria)
		{
			bool unknownCriteria = false;
			if (criteria != null)
			{
				MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
				criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
				{
					if (!(criterion is SearchCriteriaTrue) && !(criterion is SearchCriteriaFalse) && !(criterion is SearchCriteriaAnd) && !(criterion is SearchCriteriaOr) && !(criterion is SearchCriteriaNot) && !(criterion is SearchCriteriaNear))
					{
						if (criterion is SearchCriteriaText || criterion is SearchCriteriaBitMask)
						{
							unknownCriteria = true;
						}
						else if (criterion is SearchCriteriaCompare)
						{
							SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
							if (searchCriteriaCompare.Lhs != messageTable.FolderId || !(searchCriteriaCompare.Rhs is ConstantColumn) || (searchCriteriaCompare.RelOp != SearchCriteriaCompare.SearchRelOp.Equal && searchCriteriaCompare.RelOp != SearchCriteriaCompare.SearchRelOp.NotEqual))
							{
								unknownCriteria = true;
							}
						}
					}
					return criterion;
				}, (context.Culture == null) ? null : context.Culture.CompareInfo, false);
			}
			return !unknownCriteria;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0005CC00 File Offset: 0x0005AE00
		private IDisposable SetUserIdentityOnContext(Context context)
		{
			Guid? nullableSearchGuid = this.GetNullableSearchGuid(context);
			if (nullableSearchGuid == null)
			{
				return null;
			}
			return context.CreateUserIdentityFrame(nullableSearchGuid.Value);
		}

		// Token: 0x0400050E RID: 1294
		private const int AvgSearchFoldersPerUser = 18;

		// Token: 0x0400050F RID: 1295
		private const int DefaultSearchFolderAgeOutChunkSize = 25;

		// Token: 0x04000510 RID: 1296
		internal const uint MaxNestedSearchDepth = 10U;

		// Token: 0x04000511 RID: 1297
		internal const int MaxSearchPopulationAttempts = 2;

		// Token: 0x04000512 RID: 1298
		internal const int EDiscoveryMaxMessagesToLinkOnFullTextIndexSearches = 10000;

		// Token: 0x04000513 RID: 1299
		internal const int AuditingMaxMessagesToLinkOnFullTextIndexSearches = 10000;

		// Token: 0x04000514 RID: 1300
		internal const string EDiscoveryTBASearchFolderName = "SearchDiscoveryHoldsFolder";

		// Token: 0x04000515 RID: 1301
		internal const string AuditingSearchFolderPrefix = "SearchAuditMailboxFolder";

		// Token: 0x04000516 RID: 1302
		public static readonly Guid SearchFolderAgeOutMaintenanceId = new Guid("{c9490642-e68b-4157-954e-540d81e0ed87}");

		// Token: 0x04000517 RID: 1303
		public static readonly Guid MarkMailboxForSearchFolderAgeOutMaintenanceId = new Guid("{db82d4f5-00a5-4d65-96bc-45b81285f12f}");

		// Token: 0x04000518 RID: 1304
		private static Hookable<int> searchFolderAgeOutChunkSize = Hookable<int>.Create(true, 25);

		// Token: 0x04000519 RID: 1305
		internal static readonly TimeSpan TimeoutForAllowAgeOut = TimeSpan.FromDays(45.0);

		// Token: 0x0400051A RID: 1306
		internal static readonly TimeSpan TimeoutForInstantSearch = TimeSpan.FromHours(2.0);

		// Token: 0x0400051B RID: 1307
		internal static readonly TimeSpan TimestampClockSkewAllowance = TimeSpan.FromDays(2.0);

		// Token: 0x0400051C RID: 1308
		internal static int BaseViewPopulationBatchSize = 50;

		// Token: 0x0400051D RID: 1309
		private static IMailboxMaintenance searchFolderAgeOutMaintenance;

		// Token: 0x0400051E RID: 1310
		private static IDatabaseMaintenance markMailboxForSearchFolderAgeOutMaintenance;

		// Token: 0x0400051F RID: 1311
		private static int cachedQueryScopeDataSlot = -1;

		// Token: 0x04000520 RID: 1312
		private static int restrictionFiltersNothingSlot = -1;

		// Token: 0x04000521 RID: 1313
		private static int hasCountRestrictionSlot = -1;

		// Token: 0x04000522 RID: 1314
		private static int instantSearchResultsSlot = -1;

		// Token: 0x04000523 RID: 1315
		private static readonly Hookable<Action> doSearchPopulationTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x04000524 RID: 1316
		private static readonly Hookable<Action<bool, int>> doSearchPopulationParamsTestHook = Hookable<Action<bool, int>>.Create(true, null);

		// Token: 0x04000525 RID: 1317
		private static readonly Hookable<Func<IInterruptControl, IInterruptControl>> doSearchPopulationInterruptControlTestHook = Hookable<Func<IInterruptControl, IInterruptControl>>.Create(true, null);

		// Token: 0x04000526 RID: 1318
		private static readonly Hookable<Action> finishedSearchPopulationTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x04000527 RID: 1319
		private static readonly Hookable<Func<ErrorCode>> pulseForFullTextIndexQueryTestHook = Hookable<Func<ErrorCode>>.Create(true, null);

		// Token: 0x04000528 RID: 1320
		private static readonly Hookable<Func<ErrorCode>> pulseForBaseViewPopulationTestHook = Hookable<Func<ErrorCode>>.Create(true, null);

		// Token: 0x04000529 RID: 1321
		private static readonly Hookable<Action> probingTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x0400052A RID: 1322
		private static readonly Hookable<Action<Reader, TimeSpan>> validateComputedAgeOutTimeoutTestHook = Hookable<Action<Reader, TimeSpan>>.Create(true, null);

		// Token: 0x0400052B RID: 1323
		private static readonly Hookable<Action<SimpleQueryOperator.SimpleQueryOperatorDefinition>> searchPopulationQueryPlanTestHook = Hookable<Action<SimpleQueryOperator.SimpleQueryOperatorDefinition>>.Create(true, null);

		// Token: 0x0400052C RID: 1324
		private static readonly Hookable<Func<ExchangeId, DateTime, DateTime>> getMostRecentAccessTimeTestHook = Hookable<Func<ExchangeId, DateTime, DateTime>>.Create(true, null);

		// Token: 0x0400052D RID: 1325
		private static readonly Hookable<Func<MailboxState, ExchangeId, bool, bool>> shouldSearchFolderBeAgedOutTestHook = Hookable<Func<MailboxState, ExchangeId, bool, bool>>.Create(true, null);

		// Token: 0x0400052E RID: 1326
		private static readonly Hookable<Action> reportCorruptSearchBacklinkTestHook = Hookable<Action>.Create(true, delegate()
		{
		});

		// Token: 0x0400052F RID: 1327
		private static readonly Hookable<bool> skipAddingDocIdToBaseViewIndexKeyTestHook = Hookable<bool>.Create(true, false);

		// Token: 0x04000530 RID: 1328
		private static readonly Hookable<Action<Context, SearchFolder>> repopulateNestedSearchTestHook = Hookable<Action<Context, SearchFolder>>.Create(true, null);

		// Token: 0x04000531 RID: 1329
		private bool forceProbingForConversationViews;

		// Token: 0x04000532 RID: 1330
		private SearchExecutionDiagnostics diagnostics;

		// Token: 0x020000CB RID: 203
		internal class InstantSearchResultsEntry
		{
			// Token: 0x06000B71 RID: 2929 RVA: 0x0005CD88 File Offset: 0x0005AF88
			public override string ToString()
			{
				return string.Format("[{0}:{1},{2}({3})]", new object[]
				{
					this.SortPosition,
					this.MessageDocumentId,
					this.ConversationDocumentId,
					this.Count
				});
			}

			// Token: 0x04000536 RID: 1334
			public int SortPosition;

			// Token: 0x04000537 RID: 1335
			public int MessageDocumentId;

			// Token: 0x04000538 RID: 1336
			public int ConversationDocumentId;

			// Token: 0x04000539 RID: 1337
			public int Count;
		}

		// Token: 0x020000CC RID: 204
		internal class InstantSearchResults : IComponentData
		{
			// Token: 0x06000B73 RID: 2931 RVA: 0x0005CDE7 File Offset: 0x0005AFE7
			public InstantSearchResults()
			{
				this.IsValid = true;
			}

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0005CDF6 File Offset: 0x0005AFF6
			// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0005CDFE File Offset: 0x0005AFFE
			public bool IsValid { get; set; }

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0005CE07 File Offset: 0x0005B007
			// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0005CE0F File Offset: 0x0005B00F
			public List<SearchFolder.InstantSearchResultsEntry> ConversationRows { get; set; }

			// Token: 0x06000B78 RID: 2936 RVA: 0x0005CE18 File Offset: 0x0005B018
			bool IComponentData.DoCleanup(Context context)
			{
				return false;
			}
		}

		// Token: 0x020000CD RID: 205
		internal class FoldersScope
		{
			// Token: 0x06000B79 RID: 2937 RVA: 0x0005CE1B File Offset: 0x0005B01B
			private FoldersScope(IList<ExchangeId> foldersInScope, IList<IFolderInformation> indexableFolders, bool recursiveIpmSearch)
			{
				this.foldersInScope = foldersInScope;
				this.indexableFolders = indexableFolders;
				this.recursiveIpmSearch = recursiveIpmSearch;
			}

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0005CE38 File Offset: 0x0005B038
			internal IList<ExchangeId> Folders
			{
				get
				{
					return this.foldersInScope;
				}
			}

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0005CE40 File Offset: 0x0005B040
			internal IList<IFolderInformation> IndexableFolders
			{
				get
				{
					return this.indexableFolders;
				}
			}

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0005CE48 File Offset: 0x0005B048
			internal bool IsRecursiveIpmSearch
			{
				get
				{
					return this.recursiveIpmSearch;
				}
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x0005CE74 File Offset: 0x0005B074
			internal static SearchFolder.FoldersScope Create(Context context, SearchFolder searchFolder)
			{
				IList<ExchangeId> scopeFolders = searchFolder.GetScopeFolders(context);
				if (!searchFolder.IsRecursiveSearch(context))
				{
					return new SearchFolder.FoldersScope(scopeFolders, null, false);
				}
				bool flag = false;
				if (scopeFolders.Count == 1)
				{
					ExchangeId[] specialFolders = SpecialFoldersCache.GetSpecialFolders(context, searchFolder.Mailbox);
					if (scopeFolders[0] == specialFolders[9])
					{
						flag = true;
					}
				}
				List<IFolderInformation> list = new List<IFolderInformation>(200);
				IList<ExchangeId> list2 = SearchFolder.FoldersScope.ExpandRecursivelySearchedFolders(context, searchFolder.Mailbox, scopeFolders, list);
				list.Sort((IFolderInformation f1, IFolderInformation f2) => f2.MessageCount.CompareTo(f1.MessageCount));
				return new SearchFolder.FoldersScope(list2, list, flag);
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x0005CF17 File Offset: 0x0005B117
			internal static IList<ExchangeId> ExpandRecursivelySearchedFolders(Context context, Mailbox mailbox, IList<ExchangeId> recursivelySearchedFolders)
			{
				return SearchFolder.FoldersScope.ExpandRecursivelySearchedFolders(context, mailbox, recursivelySearchedFolders, null);
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x0005CF40 File Offset: 0x0005B140
			private static IList<ExchangeId> ExpandRecursivelySearchedFolders(Context context, Mailbox mailbox, IList<ExchangeId> recursivelySearchedFolders, List<IFolderInformation> contentIndexableFolders)
			{
				IList<ExchangeId> result = ExchangeIdListHelpers.EmptyList;
				if (recursivelySearchedFolders.Count != 0)
				{
					Queue<ExchangeId> queue = new Queue<ExchangeId>(recursivelySearchedFolders);
					List<ExchangeId> list = new List<ExchangeId>(10);
					IReplidGuidMap replidGuidMap = mailbox.ReplidGuidMap;
					FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, mailbox, ExchangeShortId.Zero, FolderInformationType.Basic);
					if (contentIndexableFolders != null)
					{
						folderHierarchy.ForEachFolderInformation(context, delegate(IFolderInformation folderInformation)
						{
							if (folderInformation.IsPartOfContentIndexing)
							{
								contentIndexableFolders.Add(folderInformation);
							}
						});
					}
					while (queue.Count > 0)
					{
						ExchangeId item = queue.Dequeue();
						IFolderInformation folderInformation3 = folderHierarchy.Find(context, item.ToExchangeShortId());
						if (folderInformation3 == null)
						{
							throw new ObjectNotFoundException((LID)57976U, mailbox.MailboxGuid, "Folder is missing.");
						}
						if (!folderInformation3.IsSearchFolder && !folderInformation3.IsInternalAccess)
						{
							list.Add(item);
							foreach (IFolderInformation folderInformation2 in folderHierarchy.GetChildren(context, folderInformation3))
							{
								queue.Enqueue(ExchangeId.CreateFromInternalShortId(context, replidGuidMap, folderInformation2.Fid));
							}
						}
					}
					list.SortAndRemoveDuplicates<ExchangeId>();
					result = list;
				}
				return result;
			}

			// Token: 0x0400053C RID: 1340
			private readonly IList<ExchangeId> foldersInScope;

			// Token: 0x0400053D RID: 1341
			private readonly IList<IFolderInformation> indexableFolders;

			// Token: 0x0400053E RID: 1342
			private readonly bool recursiveIpmSearch;
		}

		// Token: 0x020000CE RID: 206
		internal class FullTextIndexRowDocumentIdComparer : IComparer<FullTextIndexRow>, IEqualityComparer<FullTextIndexRow>
		{
			// Token: 0x06000B81 RID: 2945 RVA: 0x0005D084 File Offset: 0x0005B284
			public int Compare(FullTextIndexRow x, FullTextIndexRow y)
			{
				return y.DocumentId - x.DocumentId;
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x0005D093 File Offset: 0x0005B293
			public bool Equals(FullTextIndexRow x, FullTextIndexRow y)
			{
				return x.DocumentId == y.DocumentId;
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x0005D0A3 File Offset: 0x0005B2A3
			public int GetHashCode(FullTextIndexRow x)
			{
				return x.DocumentId;
			}

			// Token: 0x04000540 RID: 1344
			public static readonly SearchFolder.FullTextIndexRowDocumentIdComparer Instance = new SearchFolder.FullTextIndexRowDocumentIdComparer();
		}

		// Token: 0x020000CF RID: 207
		internal class SearchInterruptControl : IInterruptControl
		{
			// Token: 0x06000B86 RID: 2950 RVA: 0x0005D0BF File Offset: 0x0005B2BF
			public SearchInterruptControl(TimeSpan timeBetweenInterrupts, int minReadCount, Func<bool> canContinue)
			{
				this.millisecondsBetweenInterrupts = (int)timeBetweenInterrupts.TotalMilliseconds;
				this.canContinue = canContinue;
				this.minReadCount = minReadCount;
				this.Reset();
			}

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0005D0E9 File Offset: 0x0005B2E9
			public int MillisecondsBetweenInterrupts
			{
				get
				{
					return this.millisecondsBetweenInterrupts;
				}
			}

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0005D0F1 File Offset: 0x0005B2F1
			public TimeSpan TimeBetweenInterrupts
			{
				get
				{
					return TimeSpan.FromMilliseconds((double)this.millisecondsBetweenInterrupts);
				}
			}

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0005D0FF File Offset: 0x0005B2FF
			public bool WantToInterrupt
			{
				get
				{
					return Environment.TickCount - this.nextInterruptMillisecondCount >= 0 || (this.readCount >= this.minReadCount && !this.canContinue());
				}
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x0005D130 File Offset: 0x0005B330
			public void RegisterRead(bool probe, TableClass tableClass)
			{
				this.readCount++;
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x0005D140 File Offset: 0x0005B340
			public void RegisterWrite(TableClass tableClass)
			{
			}

			// Token: 0x06000B8C RID: 2956 RVA: 0x0005D142 File Offset: 0x0005B342
			public void Reset()
			{
				this.nextInterruptMillisecondCount = Environment.TickCount + this.millisecondsBetweenInterrupts;
				this.readCount = 0;
			}

			// Token: 0x04000541 RID: 1345
			private readonly int millisecondsBetweenInterrupts;

			// Token: 0x04000542 RID: 1346
			private readonly Func<bool> canContinue;

			// Token: 0x04000543 RID: 1347
			private readonly int minReadCount;

			// Token: 0x04000544 RID: 1348
			private int readCount;

			// Token: 0x04000545 RID: 1349
			private int nextInterruptMillisecondCount;
		}
	}
}
