using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000D4 RID: 212
	internal class SubobjectCleanup
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x0005E0E1 File Offset: 0x0005C2E1
		private SubobjectCleanup()
		{
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0005E0E9 File Offset: 0x0005C2E9
		private bool IsUrgentMaintenanceRequired
		{
			get
			{
				return this.totalNumberOfEntriesEstimate > ConfigurationSchema.SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold.Value || this.totalSizeEstimate > ConfigurationSchema.SubobjectCleanupUrgentMaintenanceTotalSizeThreshold.Value;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0005E1B4 File Offset: 0x0005C3B4
		internal static void Initialize()
		{
			if (SubobjectCleanup.subobjectCleanupDataSlot == -1)
			{
				SubobjectCleanup.subobjectCleanupDataSlot = StoreDatabase.AllocateComponentDataSlot();
				SubobjectCleanup.subobjectCleanupMaintenance = MaintenanceHandler.RegisterDatabaseMaintenance(SubobjectCleanup.SubobjectCleanupMaintenanceId, RequiredMaintenanceResourceType.Store, new MaintenanceHandler.DatabaseMaintenanceDelegate(SubobjectCleanup.MaintenanceCleanupTombstoneTable), "SubobjectCleanup.MaintenanceCleanupTombstoneTable", 10);
				MaintenanceHandler.RegisterDatabaseMaintenance(SubobjectCleanup.SubobjectCleanupUrgentMaintenanceId, RequiredMaintenanceResourceType.Store, delegate(Context context, DatabaseInfo databaseInfo, out bool completed)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Urgent tombstone table cleanup should never get invoked as a database maintenance task.");
					completed = true;
				}, "SubobjectCleanup.UrgentTombstoneTableCleanup");
				TombstoneTableDiagnostics.InitializeUpgraderAction(delegate(Context context)
				{
					TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
					tombstoneTable.Table.AddColumn(context, tombstoneTable.SizeEstimate);
					tombstoneTable.Table.AddColumn(context, tombstoneTable.ClientType);
				}, delegate(StoreDatabase database)
				{
					TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(database);
					tombstoneTable.SizeEstimate.MinVersion = TombstoneTableDiagnostics.Instance.To.Value;
					tombstoneTable.ClientType.MinVersion = TombstoneTableDiagnostics.Instance.To.Value;
				});
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0005E266 File Offset: 0x0005C466
		internal static void MountEventHandler(Context context)
		{
			context.Database.ComponentData[SubobjectCleanup.subobjectCleanupDataSlot] = new SubobjectCleanup();
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0005E2A0 File Offset: 0x0005C4A0
		internal static void MountedEventHandler(Context context, StoreDatabase database)
		{
			SubobjectCleanup.subobjectCleanupMaintenance.ScheduleMarkForMaintenance(context, TimeSpan.FromDays(1.0));
			TaskDiagnosticInformation diagnosticInformation = new TaskDiagnosticInformation(TaskTypeId.UrgentTombstoneTableCleanup, ClientType.System, database.MdbGuid);
			Task<StoreDatabase>.TaskCallback callback = TaskExecutionWrapper<StoreDatabase>.WrapExecute<SubobjectCleanup.TombstoneTableCleanupTaskContext>(diagnosticInformation, new TaskExecutionWrapper<StoreDatabase>.TaskCallback<SubobjectCleanup.TombstoneTableCleanupTaskContext>(SubobjectCleanup.UrgentTombstoneTableCleanup), () => new SubobjectCleanup.TombstoneTableCleanupTaskContext(diagnosticInformation), delegate(SubobjectCleanup.TombstoneTableCleanupTaskContext taskContext)
			{
				taskContext.Dispose();
			});
			SubobjectCleanup subobjectCleanup = SubobjectCleanup.GetSubobjectCleanup(context);
			subobjectCleanup.urgentTombstoneTableCleanupTask = new RecurringTask<StoreDatabase>(callback, database, TimeSpan.FromMinutes(5.0), false);
			database.TaskList.Add(subobjectCleanup.urgentTombstoneTableCleanupTask, false);
			SingleExecutionTask<StoreDatabase>.CreateSingleExecutionTask(context.Database.TaskList, TaskExecutionWrapper<StoreDatabase>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.CalculateTombstoneTableSize, ClientType.System, context.Database.MdbGuid), new TaskExecutionWrapper<StoreDatabase>.TaskCallback<Context>(SubobjectCleanup.CalculateNumberOfEntriesAndSize)), context.Database, true);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0005E393 File Offset: 0x0005C593
		internal static void DismountEventHandler(StoreDatabase database)
		{
			database.ComponentData[SubobjectCleanup.subobjectCleanupDataSlot] = null;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0005E3A8 File Offset: 0x0005C5A8
		internal static SubobjectCleanup GetSubobjectCleanup(Context context)
		{
			return context.Database.ComponentData[SubobjectCleanup.subobjectCleanupDataSlot] as SubobjectCleanup;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0005E3D1 File Offset: 0x0005C5D1
		internal static long TotalNumberOfEntriesEstimate(Context context)
		{
			return SubobjectCleanup.GetSubobjectCleanup(context).totalNumberOfEntriesEstimate;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0005E3DE File Offset: 0x0005C5DE
		internal static long TotalSizeEstimate(Context context)
		{
			return SubobjectCleanup.GetSubobjectCleanup(context).totalSizeEstimate;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0005E3EC File Offset: 0x0005C5EC
		internal static void ResetCountersForTest(Context context)
		{
			SubobjectCleanup subobjectCleanup = SubobjectCleanup.GetSubobjectCleanup(context);
			subobjectCleanup.totalNumberOfEntriesEstimate = 0L;
			subobjectCleanup.totalSizeEstimate = 0L;
			if (context.PerfInstance != null)
			{
				context.PerfInstance.TopMessagesInTombstone.Reset();
				context.PerfInstance.SubobjectsInTombstone.Reset();
				context.PerfInstance.TotalObjectsSizeInTombstone.Reset();
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0005E448 File Offset: 0x0005C648
		internal static void NotifyCleanupMaintenanceIsRequired(Context context)
		{
			SubobjectCleanup.subobjectCleanupMaintenance.MarkForMaintenance(context);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0005E456 File Offset: 0x0005C656
		internal static long InidFromDocId(int documentId)
		{
			return (long)((ulong)documentId | 140737488355328UL);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0005E464 File Offset: 0x0005C664
		internal static bool IsDocIdInid(long inid)
		{
			return (inid & 140737488355328L) != 0L;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0005E478 File Offset: 0x0005C678
		internal static IDisposable SetUrgentTombstoneTableCleanupPerformedTestHook(Action<bool, bool> testDelegate)
		{
			return SubobjectCleanup.urgentTombstoneTableCleanupPerformedTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0005E488 File Offset: 0x0005C688
		public static void AddMessageToTombstone(Context context, TopMessage message)
		{
			TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
			bool flag = TombstoneTableDiagnostics.IsReady(context, context.Database);
			int documentId = message.GetDocumentId(context);
			long num = SubobjectCleanup.InidFromDocId(documentId);
			Column[] columnsToInsert;
			object[] valuesToInsert;
			if (flag)
			{
				columnsToInsert = new Column[]
				{
					tombstoneTable.MailboxNumber,
					tombstoneTable.Inid,
					tombstoneTable.SizeEstimate,
					tombstoneTable.ClientType
				};
				valuesToInsert = new object[]
				{
					message.Mailbox.MailboxNumber,
					num,
					message.CurrentSize,
					(int)context.ClientType
				};
			}
			else
			{
				columnsToInsert = new Column[]
				{
					tombstoneTable.MailboxNumber,
					tombstoneTable.Inid
				};
				valuesToInsert = new object[]
				{
					message.Mailbox.MailboxNumber,
					num
				};
			}
			using (InsertOperator insertOperator = Factory.CreateInsertOperator(context.Culture, context, tombstoneTable.Table, null, columnsToInsert, valuesToInsert, null, true))
			{
				insertOperator.ExecuteScalar();
			}
			SubobjectCleanup.NotifyCleanupMaintenanceIsRequired(context);
			SubobjectCleanup.RegisterCountersUpdates(context, 1, 0, flag ? message.CurrentSize : 0L);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0005E5E4 File Offset: 0x0005C7E4
		public static void AddTombstone(Context context, Item parentItem, long inid, long size)
		{
			TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
			bool flag = TombstoneTableDiagnostics.IsReady(context, context.Database);
			Column[] columnsToInsert;
			object[] valuesToInsert;
			if (flag)
			{
				columnsToInsert = new Column[]
				{
					tombstoneTable.MailboxNumber,
					tombstoneTable.Inid,
					tombstoneTable.SizeEstimate,
					tombstoneTable.ClientType
				};
				valuesToInsert = new object[]
				{
					parentItem.Mailbox.MailboxNumber,
					inid,
					size,
					(int)context.ClientType
				};
			}
			else
			{
				columnsToInsert = new Column[]
				{
					tombstoneTable.MailboxNumber,
					tombstoneTable.Inid
				};
				valuesToInsert = new object[]
				{
					parentItem.Mailbox.MailboxNumber,
					inid
				};
			}
			using (InsertOperator insertOperator = Factory.CreateInsertOperator(context.Culture, context, tombstoneTable.Table, null, columnsToInsert, valuesToInsert, null, true))
			{
				insertOperator.ExecuteScalar();
			}
			parentItem.SubobjectReferenceState.OnAddToTombstone(inid);
			SubobjectCleanup.RegisterCountersUpdates(context, 0, 1, flag ? size : 0L);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0005E728 File Offset: 0x0005C928
		public static void RemoveTombstone(Context context, Item parentItem, long inid)
		{
			TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
			bool flag = TombstoneTableDiagnostics.IsReady(context, context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				parentItem.Mailbox.MailboxNumber,
				inid
			});
			long num = 0L;
			if (flag)
			{
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, tombstoneTable.Table, tombstoneTable.TombstonePK, new Column[]
				{
					tombstoneTable.SizeEstimate
				}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (reader.Read())
						{
							num = reader.GetNullableInt64(tombstoneTable.SizeEstimate).GetValueOrDefault(0L);
						}
					}
				}
			}
			SubobjectCleanup.RemoveTombstoneRow(context, tombstoneTable, startStopKey);
			parentItem.SubobjectReferenceState.OnRemoveFromTombstone(inid);
			SubobjectCleanup.RegisterCountersUpdates(context, 0, -1, flag ? (-num) : 0L);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0005E84C File Offset: 0x0005CA4C
		public static void UpdateTombstonedSize(Context context, Item parentItem, long inid, long newSize)
		{
			if (!TombstoneTableDiagnostics.IsReady(context, context.Database))
			{
				return;
			}
			TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				parentItem.Mailbox.MailboxNumber,
				inid
			});
			long num = 0L;
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, tombstoneTable.Table, tombstoneTable.TombstonePK, new Column[]
			{
				tombstoneTable.SizeEstimate
			}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (reader.Read())
					{
						num = reader.GetNullableInt64(tombstoneTable.SizeEstimate).GetValueOrDefault(0L);
					}
				}
			}
			using (TableOperator tableOperator2 = Factory.CreateTableOperator(context.Culture, context, tombstoneTable.Table, tombstoneTable.TombstonePK, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(context.Culture, context, tableOperator2, new Column[]
				{
					tombstoneTable.SizeEstimate
				}, new object[]
				{
					newSize
				}, true))
				{
					updateOperator.ExecuteScalar();
				}
			}
			SubobjectCleanup.RegisterCountersUpdates(context, 0, 0, -num + newSize);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0005E9EC File Offset: 0x0005CBEC
		internal static void TestCleanupAll(Context context, int chunkSize)
		{
			bool flag = false;
			while (!flag)
			{
				SubobjectCleanup.TombstoneTableCleanupProgress tombstoneTableCleanupProgress = default(SubobjectCleanup.TombstoneTableCleanupProgress);
				SubobjectCleanup.CleanupTombstoneTable(context, chunkSize, ref tombstoneTableCleanupProgress, out flag);
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0005EA14 File Offset: 0x0005CC14
		private static void RemoveTombstoneRow(Context context, TombstoneTable tombstoneTable, StartStopKey startStopKey)
		{
			using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, tombstoneTable.Table, tombstoneTable.TombstonePK, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true), true))
			{
				deleteOperator.ExecuteScalar();
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0005EA78 File Offset: 0x0005CC78
		internal static void CalculateNumberOfEntriesAndSize(Context context, StoreDatabase database, Func<bool> shouldTaskContinue)
		{
			using (context.AssociateWithDatabase(database))
			{
				if (context.Database.IsOnlineActive)
				{
					SubobjectCleanup subobjectCleanup = SubobjectCleanup.GetSubobjectCleanup(context);
					subobjectCleanup.totalNumberOfEntriesEstimate = 0L;
					subobjectCleanup.totalSizeEstimate = 0L;
					TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
					bool flag = TombstoneTableDiagnostics.IsReady(context, context.Database);
					Column[] columnsToFetch;
					if (flag)
					{
						columnsToFetch = new Column[]
						{
							tombstoneTable.Inid,
							tombstoneTable.SizeEstimate
						};
					}
					else
					{
						columnsToFetch = new Column[]
						{
							tombstoneTable.Inid
						};
					}
					context.BeginTransactionIfNeeded();
					using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, tombstoneTable.Table, tombstoneTable.TombstonePK, columnsToFetch, null, null, 0, 0, KeyRange.AllRowsRange, false, true))
					{
						using (Reader reader = tableOperator.ExecuteReader(false))
						{
							long num = 0L;
							long num2 = 0L;
							long num3 = 0L;
							while (reader.Read())
							{
								long @int = reader.GetInt64(tombstoneTable.Inid);
								if (SubobjectCleanup.IsDocIdInid(@int))
								{
									num += 1L;
								}
								else
								{
									num2 += 1L;
								}
								if (flag)
								{
									num3 += reader.GetNullableInt64(tombstoneTable.SizeEstimate).GetValueOrDefault(0L);
								}
								if ((num + num2) % 1000L == 0L && !shouldTaskContinue())
								{
									return;
								}
							}
							SubobjectCleanup.UpdateCounters(context, false, num, num2, num3);
						}
					}
				}
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0005EC40 File Offset: 0x0005CE40
		private static void MaintenanceCleanupTombstoneTable(Context context, DatabaseInfo databaseInfo, out bool completed)
		{
			SubobjectCleanup.TombstoneTableCleanupProgress tombstoneTableCleanupProgress = default(SubobjectCleanup.TombstoneTableCleanupProgress);
			SubobjectCleanup.CleanupTombstoneTable(context, 100, ref tombstoneTableCleanupProgress, out completed);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0005EC60 File Offset: 0x0005CE60
		private static void UrgentTombstoneTableCleanup(SubobjectCleanup.TombstoneTableCleanupTaskContext context, StoreDatabase database, Func<bool> shouldCallbackContinue)
		{
			using (context.AssociateWithDatabase(database))
			{
				SubobjectCleanup subobjectCleanup = SubobjectCleanup.GetSubobjectCleanup(context);
				bool flag = false;
				bool flag2 = false;
				if (subobjectCleanup.IsUrgentMaintenanceRequired && context.Database.IsOnlineActive)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_UrgentTombstoneCleanupDispatched, new object[]
					{
						context.Database.MdbName,
						context.Database.MdbGuid.ToString(),
						subobjectCleanup.totalNumberOfEntriesEstimate.ToString(),
						subobjectCleanup.totalSizeEstimate.ToString()
					});
					if (ExTraceGlobals.SubobjectCleanupTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubobjectCleanupTracer.TraceDebug<string, long, long>(0L, "Urgent tombstone table cleanup started on database '{0}': EntriesEstimate={1}, SizeEstimate={2}", context.Database.MdbName, subobjectCleanup.totalNumberOfEntriesEstimate, subobjectCleanup.totalSizeEstimate);
					}
					SubobjectCleanup.TombstoneTableCleanupProgress tombstoneTableCleanupProgress = default(SubobjectCleanup.TombstoneTableCleanupProgress);
					DateTime utcNow = DateTime.UtcNow;
					while (!flag && shouldCallbackContinue() && !MaintenanceHandler.ShouldStopDatabaseMaintenanceTask(context, SubobjectCleanup.SubobjectCleanupUrgentMaintenanceId))
					{
						SubobjectCleanup.CleanupTombstoneTable(context, 100, ref tombstoneTableCleanupProgress, out flag);
					}
					DateTime utcNow2 = DateTime.UtcNow;
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_UrgentTombstoneCleanupSummary, new object[]
					{
						context.Database.MdbName,
						context.Database.MdbGuid.ToString(),
						tombstoneTableCleanupProgress.DeletedTopMessages.ToString(),
						tombstoneTableCleanupProgress.DeletedSubobjects.ToString(),
						tombstoneTableCleanupProgress.DeletedSize.ToString(),
						subobjectCleanup.totalNumberOfEntriesEstimate.ToString(),
						subobjectCleanup.totalSizeEstimate.ToString(),
						(utcNow2 - utcNow).TotalSeconds.ToString(),
						flag.ToString(),
						tombstoneTableCleanupProgress.SubobjectsInUse.ToString(),
						tombstoneTableCleanupProgress.MailboxesQuarantined.ToString(),
						tombstoneTableCleanupProgress.MailboxesLocked.ToString(),
						tombstoneTableCleanupProgress.MailboxesMissing.ToString()
					});
					if (!subobjectCleanup.IsUrgentMaintenanceRequired)
					{
						flag2 = true;
					}
					else if (flag && tombstoneTableCleanupProgress.SubobjectsInUse == 0 && tombstoneTableCleanupProgress.MailboxesLocked == 0)
					{
						flag2 = true;
					}
					if (ExTraceGlobals.SubobjectCleanupTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubobjectCleanupTracer.TraceDebug(0L, "Urgent tombstone table cleanup ended on database '{0}': completed={1}, suspendTask={2}, EntriesEstimate={3}, SizeEstimate={4}", new object[]
						{
							context.Database.MdbName,
							flag,
							flag2,
							subobjectCleanup.totalNumberOfEntriesEstimate,
							subobjectCleanup.totalSizeEstimate
						});
					}
				}
				else
				{
					flag = true;
					flag2 = true;
				}
				if (flag2)
				{
					subobjectCleanup.urgentTombstoneTableCleanupTask.Stop();
				}
				if (SubobjectCleanup.urgentTombstoneTableCleanupPerformedTestHook.Value != null)
				{
					SubobjectCleanup.urgentTombstoneTableCleanupPerformedTestHook.Value(flag, flag2);
				}
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0005EF54 File Offset: 0x0005D154
		private static void CleanupTombstoneTable(Context context, int maxRows, ref SubobjectCleanup.TombstoneTableCleanupProgress runningProgress, out bool completed)
		{
			if (!context.Database.IsOnlineActive)
			{
				completed = true;
				return;
			}
			if (ExTraceGlobals.SubobjectCleanupTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SubobjectCleanupTracer.TraceDebug<ClientType>(0L, "Cleanup TombstoneTable has been called from ClientType: {0}", context.ClientType);
			}
			bool flag = TombstoneTableDiagnostics.IsReady(context, context.Database);
			int i = 0;
			List<SubobjectCleanup.TombstoneRecord> list = null;
			int lastMailboxNumber = runningProgress.LastMailboxNumber;
			long num = runningProgress.LastInid;
			StartStopKey scanStartKey = new StartStopKey(false, new object[]
			{
				lastMailboxNumber,
				num
			});
			while (i < maxRows)
			{
				if (context.Database.HasExclusiveLockContention())
				{
					completed = false;
					return;
				}
				if (!SubobjectCleanup.GetStartingTombstoneTableEntryForCleanup(context, scanStartKey, out lastMailboxNumber, out num))
				{
					if (ExTraceGlobals.SubobjectCleanupTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubobjectCleanupTracer.TraceDebug(0L, "TombstoneTable cleanup is exiting after cleaning up {0} items this chunk because there are no more tombstone table entries to be processed. Running totals: DeletedTopMessages:{1} DeletedSubobjects:{2} DeletedSize:{3} SubobjectsInUse:{4} MailboxesQuarantined:{5} MailboxesLocked:{6} MailboxesMissing:{7}", new object[]
						{
							i,
							runningProgress.DeletedTopMessages,
							runningProgress.DeletedSubobjects,
							runningProgress.DeletedSize,
							runningProgress.SubobjectsInUse,
							runningProgress.MailboxesQuarantined,
							runningProgress.MailboxesLocked,
							runningProgress.MailboxesMissing
						});
					}
					completed = true;
					return;
				}
				context.InitializeMailboxExclusiveOperation(lastMailboxNumber, ExecutionDiagnostics.OperationSource.SubobjectsCleanup, MaintenanceHandler.MailboxLockTimeout);
				bool commit = false;
				try
				{
					ErrorCode errorCode = context.StartMailboxOperation(MailboxCreation.DontAllow, false, true);
					if (errorCode != ErrorCode.NoError || context.LockedMailboxState.Quarantined)
					{
						scanStartKey = new StartStopKey(false, new object[]
						{
							lastMailboxNumber
						});
						if (errorCode == ErrorCode.NoError && context.LockedMailboxState.Quarantined)
						{
							runningProgress.MailboxesQuarantined++;
						}
						else if (errorCode == ErrorCodeValue.Timeout)
						{
							runningProgress.MailboxesLocked++;
						}
						else
						{
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Tombstone table entry does not have an owning mailbox. Something is gravely amiss!");
							runningProgress.MailboxesMissing++;
						}
					}
					else
					{
						MailboxState lockedMailboxState = context.LockedMailboxState;
						if (list == null)
						{
							list = new List<SubobjectCleanup.TombstoneRecord>(Math.Max(maxRows, 16));
						}
						else
						{
							list.Clear();
						}
						SubobjectCleanup.CollectTombstonesForMailbox(context, lastMailboxNumber, num, maxRows - i, flag, list);
						int num2 = 0;
						int num3 = 0;
						long num4 = 0L;
						if (list.Count != 0)
						{
							SubobjectReferenceState state = SubobjectReferenceState.GetState(lockedMailboxState);
							foreach (SubobjectCleanup.TombstoneRecord tombstoneRecord in list)
							{
								num = tombstoneRecord.Inid;
								if (SubobjectCleanup.IsDocIdInid(num))
								{
									SubobjectCleanup.RemoveTombstonedMessage(context, lockedMailboxState, num);
									i++;
									num2++;
									num4 += tombstoneRecord.Size;
								}
								else if (state == null || !state.IsReferenced(num))
								{
									SubobjectCleanup.RemoveTombstonedSubobject(context, lockedMailboxState, num);
									i++;
									num3++;
									num4 += tombstoneRecord.Size;
								}
								else
								{
									runningProgress.SubobjectsInUse++;
								}
							}
							if (num2 > 0 || num3 > 0)
							{
								SubobjectCleanup.RegisterCountersUpdates(context, -num2, -num3, flag ? (-num4) : 0L);
								runningProgress.DeletedTopMessages += num2;
								runningProgress.DeletedSubobjects += num3;
								runningProgress.DeletedSize += num4;
							}
						}
						runningProgress.LastMailboxNumber = lastMailboxNumber;
						runningProgress.LastInid = num;
						scanStartKey = new StartStopKey(false, new object[]
						{
							lastMailboxNumber,
							num
						});
						commit = true;
					}
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(commit);
					}
				}
			}
			completed = (i < maxRows);
			if (ExTraceGlobals.SubobjectCleanupTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SubobjectCleanupTracer.TraceDebug(0L, "TombstoneTable cleanup is exiting after cleaning up {0} items this chunk (completed?=={1}). Running Totals: DeletedTopMessages:{2} DeletedSubobjects:{3} DeletedSize:{4} SubobjectsInUse:{5} MailboxesQuarantined:{6} MailboxesLocked:{7} MailboxesMissing:{8}", new object[]
				{
					i,
					completed,
					runningProgress.DeletedTopMessages,
					runningProgress.DeletedSubobjects,
					runningProgress.DeletedSize,
					runningProgress.SubobjectsInUse,
					runningProgress.MailboxesQuarantined,
					runningProgress.MailboxesLocked,
					runningProgress.MailboxesMissing
				});
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0005F3DC File Offset: 0x0005D5DC
		private static bool GetStartingTombstoneTableEntryForCleanup(Context context, StartStopKey scanStartKey, out int mailboxNumber, out long inid)
		{
			TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, tombstoneTable.Table, tombstoneTable.TombstonePK, new Column[]
			{
				tombstoneTable.MailboxNumber,
				tombstoneTable.Inid
			}, null, null, 0, 1, new KeyRange(scanStartKey, StartStopKey.Empty), false, true))
			{
				try
				{
					context.BeginTransactionIfNeeded();
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (!reader.Read())
						{
							mailboxNumber = 0;
							inid = 0L;
							return false;
						}
						mailboxNumber = reader.GetInt32(tombstoneTable.MailboxNumber);
						inid = reader.GetInt64(tombstoneTable.Inid);
					}
				}
				finally
				{
					context.Commit();
				}
			}
			return true;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0005F4C8 File Offset: 0x0005D6C8
		private static void CollectTombstonesForMailbox(Context context, int mailboxNumber, long startingInid, int maxRows, bool tombstoneDiagnosticEnabled, List<SubobjectCleanup.TombstoneRecord> tombstoneList)
		{
			TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
			StartStopKey startKey = new StartStopKey(true, new object[]
			{
				mailboxNumber,
				startingInid
			});
			StartStopKey stopKey = new StartStopKey(true, new object[]
			{
				mailboxNumber
			});
			Column[] columnsToFetch;
			if (tombstoneDiagnosticEnabled)
			{
				columnsToFetch = new Column[]
				{
					tombstoneTable.Inid,
					tombstoneTable.SizeEstimate
				};
			}
			else
			{
				columnsToFetch = new Column[]
				{
					tombstoneTable.Inid
				};
			}
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, tombstoneTable.Table, tombstoneTable.TombstonePK, columnsToFetch, null, null, 0, maxRows, new KeyRange(startKey, stopKey), false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						if (tombstoneDiagnosticEnabled)
						{
							tombstoneList.Add(new SubobjectCleanup.TombstoneRecord
							{
								Inid = reader.GetInt64(tombstoneTable.Inid),
								Size = reader.GetNullableInt64(tombstoneTable.SizeEstimate).GetValueOrDefault(0L)
							});
						}
						else
						{
							tombstoneList.Add(new SubobjectCleanup.TombstoneRecord
							{
								Inid = reader.GetInt64(tombstoneTable.Inid),
								Size = 0L
							});
						}
					}
				}
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0005F650 File Offset: 0x0005D850
		private static void RemoveTombstonedMessage(Context context, MailboxState mailboxState, long inid)
		{
			MessageTable messageTable = DatabaseSchema.MessageTable(context.Database);
			TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailboxState.MailboxPartitionNumber,
				(int)(inid & -140737488355329L)
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessagePK, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, tableOperator, true))
				{
					deleteOperator.ExecuteScalar();
				}
			}
			StartStopKey startStopKey2 = new StartStopKey(true, new object[]
			{
				mailboxState.MailboxNumber,
				inid
			});
			SubobjectCleanup.RemoveTombstoneRow(context, tombstoneTable, startStopKey2);
			if (context.PerfInstance != null)
			{
				context.PerfInstance.TopMessagesCleanedRate.Increment();
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0005F770 File Offset: 0x0005D970
		private static void RemoveTombstonedSubobject(Context context, MailboxState mailboxState, long inid)
		{
			AttachmentTable attachmentTable = DatabaseSchema.AttachmentTable(context.Database);
			TombstoneTable tombstoneTable = DatabaseSchema.TombstoneTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailboxState.MailboxPartitionNumber,
				inid
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, attachmentTable.Table, attachmentTable.AttachmentPK, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, tableOperator, true))
				{
					deleteOperator.ExecuteScalar();
				}
			}
			StartStopKey startStopKey2 = new StartStopKey(true, new object[]
			{
				mailboxState.MailboxNumber,
				inid
			});
			SubobjectCleanup.RemoveTombstoneRow(context, tombstoneTable, startStopKey2);
			if (context.PerfInstance != null)
			{
				context.PerfInstance.SubobjectsCleanedRate.Increment();
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0005F884 File Offset: 0x0005DA84
		private static void RegisterCountersUpdates(Context context, int topMessagesDelta, int subobjectsDelta, long sizeDelta)
		{
			context.RegisterStateObject(new SubobjectCleanup.CountersUpdateRegistration
			{
				TopMessagesDelta = (long)topMessagesDelta,
				SubobjectsDelta = (long)subobjectsDelta,
				SizeDelta = sizeDelta
			});
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0005F8B8 File Offset: 0x0005DAB8
		private static void UpdateCounters(Context context, bool updateSubobjectDeleteRate, long topMessagesDelta, long subobjectsDelta, long sizeDelta)
		{
			SubobjectCleanup subobjectCleanup = SubobjectCleanup.GetSubobjectCleanup(context);
			if (context.PerfInstance != null)
			{
				context.PerfInstance.TopMessagesInTombstone.IncrementBy(topMessagesDelta);
				context.PerfInstance.SubobjectsInTombstone.IncrementBy(subobjectsDelta);
				context.PerfInstance.TotalObjectsSizeInTombstone.IncrementBy(sizeDelta);
				if (updateSubobjectDeleteRate && subobjectsDelta > 0L)
				{
					context.PerfInstance.SubobjectsDeletedRate.Increment();
				}
			}
			long num = topMessagesDelta + subobjectsDelta;
			Interlocked.Add(ref subobjectCleanup.totalNumberOfEntriesEstimate, num);
			Interlocked.Add(ref subobjectCleanup.totalSizeEstimate, sizeDelta);
			if (num > 0L && subobjectCleanup.IsUrgentMaintenanceRequired)
			{
				subobjectCleanup.urgentTombstoneTableCleanupTask.Start();
			}
		}

		// Token: 0x04000567 RID: 1383
		internal const ulong MessageEntryIdentificationFlag = 140737488355328UL;

		// Token: 0x04000568 RID: 1384
		public static readonly Guid SubobjectCleanupMaintenanceId = new Guid("{94196d5c-e792-466d-8f8d-e72ae0dd780f}");

		// Token: 0x04000569 RID: 1385
		public static readonly Guid SubobjectCleanupUrgentMaintenanceId = new Guid("{ecb20c7e-2942-40bc-92b2-acdf8948ab1a}");

		// Token: 0x0400056A RID: 1386
		private static IDatabaseMaintenance subobjectCleanupMaintenance;

		// Token: 0x0400056B RID: 1387
		private static int subobjectCleanupDataSlot = -1;

		// Token: 0x0400056C RID: 1388
		private static readonly Hookable<Action<bool, bool>> urgentTombstoneTableCleanupPerformedTestHook = Hookable<Action<bool, bool>>.Create(true, null);

		// Token: 0x0400056D RID: 1389
		private RecurringTask<StoreDatabase> urgentTombstoneTableCleanupTask;

		// Token: 0x0400056E RID: 1390
		private long totalNumberOfEntriesEstimate;

		// Token: 0x0400056F RID: 1391
		private long totalSizeEstimate;

		// Token: 0x020000D5 RID: 213
		private class CountersUpdateRegistration : IStateObject
		{
			// Token: 0x06000BC8 RID: 3016 RVA: 0x0005F98D File Offset: 0x0005DB8D
			void IStateObject.OnBeforeCommit(Context context)
			{
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x0005F98F File Offset: 0x0005DB8F
			void IStateObject.OnCommit(Context context)
			{
				SubobjectCleanup.UpdateCounters(context, true, this.TopMessagesDelta, this.SubobjectsDelta, this.SizeDelta);
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x0005F9AA File Offset: 0x0005DBAA
			void IStateObject.OnAbort(Context context)
			{
			}

			// Token: 0x04000574 RID: 1396
			public long TopMessagesDelta;

			// Token: 0x04000575 RID: 1397
			public long SubobjectsDelta;

			// Token: 0x04000576 RID: 1398
			public long SizeDelta;
		}

		// Token: 0x020000D6 RID: 214
		private struct TombstoneRecord
		{
			// Token: 0x04000577 RID: 1399
			public long Inid;

			// Token: 0x04000578 RID: 1400
			public long Size;
		}

		// Token: 0x020000D7 RID: 215
		private class TombstoneTableCleanupTaskContext : Context
		{
			// Token: 0x06000BCC RID: 3020 RVA: 0x0005F9B4 File Offset: 0x0005DBB4
			public TombstoneTableCleanupTaskContext(TaskDiagnosticInformation diagnosticInformation) : base(new TaskExecutionDiagnostics(diagnosticInformation.TaskTypeId, diagnosticInformation.ClientActivityId, diagnosticInformation.ClientComponentName, diagnosticInformation.ClientProtocolName, diagnosticInformation.ClientActionString))
			{
				base.SkipDatabaseLogsFlush = true;
			}
		}

		// Token: 0x020000D8 RID: 216
		private struct TombstoneTableCleanupProgress
		{
			// Token: 0x04000579 RID: 1401
			public int LastMailboxNumber;

			// Token: 0x0400057A RID: 1402
			public long LastInid;

			// Token: 0x0400057B RID: 1403
			public int DeletedTopMessages;

			// Token: 0x0400057C RID: 1404
			public int DeletedSubobjects;

			// Token: 0x0400057D RID: 1405
			public long DeletedSize;

			// Token: 0x0400057E RID: 1406
			public int SubobjectsInUse;

			// Token: 0x0400057F RID: 1407
			public int MailboxesQuarantined;

			// Token: 0x04000580 RID: 1408
			public int MailboxesLocked;

			// Token: 0x04000581 RID: 1409
			public int MailboxesMissing;
		}
	}
}
