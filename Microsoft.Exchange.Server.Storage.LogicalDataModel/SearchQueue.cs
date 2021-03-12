﻿using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000D0 RID: 208
	internal static class SearchQueue
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x0005D15D File Offset: 0x0005B35D
		internal static IDisposable SetDrainSearchQueueTaskSkippedTestHook(Action action)
		{
			return SearchQueue.drainSearchQueueTaskSkipped.SetTestHook(action);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0005D16C File Offset: 0x0005B36C
		public static void InsertIntoSearchQueue(Context context, Mailbox mailbox, ExchangeId searchFolderId)
		{
			bool flag = false;
			if (mailbox.SharedState.SupportsPerUserFeatures)
			{
				InTransitStatus inTransitStatus = InTransitInfo.GetInTransitStatus(mailbox.SharedState);
				flag = InTransitInfo.IsMoveDestination(inTransitStatus);
			}
			byte[] array = new byte[context.SecurityContext.UserSid.BinaryLength];
			context.SecurityContext.UserSid.GetBinaryForm(array, 0);
			SearchQueueTable searchQueueTable = DatabaseSchema.SearchQueueTable(context.Database);
			byte[] array2 = PropertyBlob.BuildBlob(new uint[]
			{
				65556U,
				131083U
			}, new object[]
			{
				SearchQueue.GetDatabaseMountId(context),
				flag
			});
			using (InsertOperator insertOperator = Factory.CreateInsertOperator(context.Culture, context, searchQueueTable.Table, null, new Column[]
			{
				searchQueueTable.MailboxNumber,
				searchQueueTable.SearchFolderId,
				searchQueueTable.UserSid,
				searchQueueTable.ClientType,
				searchQueueTable.LCID,
				searchQueueTable.CreationTime,
				searchQueueTable.PopulationAttempts,
				searchQueueTable.ExtensionBlob
			}, new object[]
			{
				mailbox.MailboxNumber,
				searchFolderId.To26ByteArray(),
				array,
				(int)context.ClientType,
				CultureHelper.GetLcidFromCulture(context.Culture),
				mailbox.UtcNow,
				0,
				array2
			}, null, true))
			{
				insertOperator.ExecuteScalar();
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0005D318 File Offset: 0x0005B518
		public static void UpdateSearchQueueRow(Context context, Mailbox mailbox, ExchangeId searchFolderId, Func<TableOperator, DataAccessOperator> updateOperation)
		{
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxNumber,
				searchFolderId.To26ByteArray()
			});
			SearchQueue.UpdateSearchQueueRows(context, mailbox.SharedState, new KeyRange(startStopKey, startStopKey), updateOperation);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0005D364 File Offset: 0x0005B564
		public static int UpdateSearchQueueRows(Context context, MailboxState mailboxState, KeyRange keyRange, Func<TableOperator, DataAccessOperator> updateOperation)
		{
			SearchQueueTable searchQueueTable = DatabaseSchema.SearchQueueTable(context.Database);
			int result;
			using (DataAccessOperator dataAccessOperator = updateOperation(Factory.CreateTableOperator(context.Culture, context, searchQueueTable.Table, searchQueueTable.SearchQueuePK, null, null, null, 0, 0, keyRange, false, true)))
			{
				object obj = dataAccessOperator.ExecuteScalar();
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0005D3F4 File Offset: 0x0005B5F4
		public static void RemoveFromSearchQueue(Context context, Mailbox mailbox, ExchangeId searchFolderId)
		{
			if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<int, ExchangeId>(0L, "Mailbox {0}: Remove search folder {1} from SearchQueue table.", mailbox.MailboxNumber, searchFolderId);
			}
			SearchQueue.UpdateSearchQueueRow(context, mailbox, searchFolderId, (TableOperator tableOperator) => Factory.CreateDeleteOperator(context.Culture, context, tableOperator, true));
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0005D4BC File Offset: 0x0005B6BC
		public static void UpdatePopulationAttempts(Context context, Mailbox mailbox, ExchangeId searchFolderId, int populationAttempts)
		{
			SearchQueueTable searchQueueTable = DatabaseSchema.SearchQueueTable(context.Database);
			byte[] extensionBlob = PropertyBlob.BuildBlob(new uint[]
			{
				65556U,
				131083U
			}, new object[]
			{
				SearchQueue.GetDatabaseMountId(context),
				false
			});
			SearchQueue.UpdateSearchQueueRow(context, mailbox, searchFolderId, (TableOperator tableOperator) => Factory.CreateUpdateOperator(context.Culture, context, tableOperator, new Column[]
			{
				searchQueueTable.PopulationAttempts,
				searchQueueTable.ExtensionBlob
			}, new object[]
			{
				populationAttempts,
				extensionBlob
			}, true));
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0005D554 File Offset: 0x0005B754
		public static void RemoveAllEntriesForMailbox(Context context, Mailbox mailbox)
		{
			SearchQueueTable searchQueueTable = DatabaseSchema.SearchQueueTable(context.Database);
			mailbox.RemoveMailboxEntriesFromTable(context, searchQueueTable.Table);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0005D57C File Offset: 0x0005B77C
		public static bool IsInSearchQueue(Context context, Mailbox mailbox, ExchangeId searchFolderId, out int populationAttempts)
		{
			bool result = false;
			SearchQueueTable searchQueueTable = DatabaseSchema.SearchQueueTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxNumber,
				searchFolderId.To26ByteArray()
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, searchQueueTable.Table, searchQueueTable.SearchQueuePK, new Column[]
			{
				searchQueueTable.PopulationAttempts
			}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (reader.Read())
					{
						populationAttempts = reader.GetInt32(searchQueueTable.PopulationAttempts);
						result = true;
					}
					else
					{
						populationAttempts = 0;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0005D65C File Offset: 0x0005B85C
		private static long GetDatabaseMountId(Context context)
		{
			return context.Database.MountId;
		}

		// Token: 0x04000546 RID: 1350
		private const uint DummyPropTagForDatabaseMountId = 65556U;

		// Token: 0x04000547 RID: 1351
		private const uint DummyPropTagForCreatedByMRS = 131083U;

		// Token: 0x04000548 RID: 1352
		private static Hookable<Action> drainSearchQueueTaskSkipped = Hookable<Action>.Create(true, null);

		// Token: 0x020000D1 RID: 209
		internal static class DrainSearchQueueTask
		{
			// Token: 0x06000B97 RID: 2967 RVA: 0x0005D684 File Offset: 0x0005B884
			public static void Launch(StoreDatabase database)
			{
				if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<Guid>(0L, "Database {0}: LaunchDrainSearchQueueTask.", database.MdbGuid);
				}
				SingleExecutionTask<StoreDatabase>.CreateSingleExecutionTask(database.TaskList, TaskExecutionWrapper<StoreDatabase>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.DrainSearchQueue, ClientType.System, database.MdbGuid), new TaskExecutionWrapper<StoreDatabase>.TaskCallback<Context>(SearchQueue.DrainSearchQueueTask.ExecuteOnDatabase)), database, true);
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x0005D6E4 File Offset: 0x0005B8E4
			public static void Launch(MailboxState mailboxState)
			{
				if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<Guid>(0L, "Mailbox {0}: LaunchDrainSearchQueueTask.", mailboxState.MailboxGuid);
				}
				StoreDatabase database = mailboxState.Database;
				SingleExecutionTask<MailboxState>.CreateSingleExecutionTask(database.TaskList, TaskExecutionWrapper<MailboxState>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.DrainSearchQueue, ClientType.System, mailboxState.MailboxGuid), new TaskExecutionWrapper<MailboxState>.TaskCallback<Context>(SearchQueue.DrainSearchQueueTask.ExecuteOnMailbox)), mailboxState, true);
			}

			// Token: 0x06000B99 RID: 2969 RVA: 0x0005D74C File Offset: 0x0005B94C
			private static void ExecuteOnDatabase(Context context, StoreDatabase database, Func<bool> shouldTaskContinue)
			{
				if (shouldTaskContinue())
				{
					if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<Guid>(0L, "Database {0}: Execute the task to drain the entries in the SearchQueue table.", database.MdbGuid);
					}
					using (context.AssociateWithDatabase(database))
					{
						if (database.IsOnlineActive)
						{
							SearchQueue.DrainSearchQueueTask.RunDrainSearchQueue(context, KeyRange.AllRowsRange, false, shouldTaskContinue);
						}
					}
				}
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x0005D7C4 File Offset: 0x0005B9C4
			private static void ExecuteOnMailbox(Context context, MailboxState mailboxState, Func<bool> shouldTaskContinue)
			{
				if (shouldTaskContinue())
				{
					StoreDatabase database = mailboxState.Database;
					if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<Guid>(0L, "mailbox {0}: Execute the task to drain the entries in the SearchQueue table.", mailboxState.MailboxGuid);
					}
					using (context.AssociateWithDatabase(database))
					{
						if (database.IsOnlineActive)
						{
							StartStopKey startStopKey = new StartStopKey(true, new object[]
							{
								mailboxState.MailboxNumber
							});
							KeyRange keyRange = new KeyRange(startStopKey, startStopKey);
							SearchQueue.DrainSearchQueueTask.RunDrainSearchQueue(context, keyRange, true, shouldTaskContinue);
						}
					}
				}
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x0005D86C File Offset: 0x0005BA6C
			private static void RunDrainSearchQueue(Context context, KeyRange keyRange, bool executeMRSEntries, Func<bool> shouldTaskContinue)
			{
				if (ExTraceGlobals.SearchFolderPopulationTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SearchFolderPopulationTracer.TraceDebug<Guid, string>(0L, "Database {0}: Execute RunDrainSearchQueue. KeyRange: {1}", context.Database.MdbGuid, keyRange.ToString());
				}
				SearchQueueTable searchQueueTable = DatabaseSchema.SearchQueueTable(context.Database);
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, searchQueueTable.Table, searchQueueTable.SearchQueuePK, new Column[]
				{
					searchQueueTable.MailboxNumber,
					searchQueueTable.SearchFolderId,
					searchQueueTable.UserSid,
					searchQueueTable.ClientType,
					searchQueueTable.LCID,
					searchQueueTable.ExtensionBlob
				}, null, null, 0, 0, keyRange, false, false))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						while (reader.Read() && shouldTaskContinue())
						{
							byte[] binary = reader.GetBinary(searchQueueTable.SearchFolderId);
							int @int = reader.GetInt32(searchQueueTable.MailboxNumber);
							context.InitializeMailboxExclusiveOperation(@int, ExecutionDiagnostics.OperationSource.MailboxTask, LockManager.InfiniteTimeout);
							bool commit = false;
							try
							{
								ErrorCode first = context.StartMailboxOperation();
								if (first != ErrorCode.NoError || context.LockedMailboxState.IsSoftDeleted || context.LockedMailboxState.IsHardDeleted)
								{
									FaultInjection.InjectFault(SearchQueue.drainSearchQueueTaskSkipped);
								}
								else
								{
									PropertyBlob.BlobReader blobReader = new PropertyBlob.BlobReader(reader.GetBinary(searchQueueTable.ExtensionBlob), 0);
									object propertyValueByTag = blobReader.GetPropertyValueByTag(65556U);
									object propertyValueByTag2 = blobReader.GetPropertyValueByTag(131083U);
									long num = 0L;
									bool flag = false;
									if (propertyValueByTag != null)
									{
										num = (long)propertyValueByTag;
									}
									if (propertyValueByTag2 != null)
									{
										flag = (bool)propertyValueByTag2;
									}
									if (executeMRSEntries == flag)
									{
										if (num != context.Database.MountId || executeMRSEntries)
										{
											context.LockedMailboxState.AddReference();
											try
											{
												SearchFolder.LaunchSearchPopulationTask(context, context.LockedMailboxState, ExchangeId.CreateFrom26ByteArray(context, null, binary), new SecurityIdentifier(reader.GetBinary(searchQueueTable.UserSid), 0), (ClientType)reader.GetInt32(searchQueueTable.ClientType), CultureHelper.GetCultureFromLcid(reader.GetInt32(searchQueueTable.LCID)), null, null);
											}
											finally
											{
												context.LockedMailboxState.ReleaseReference();
											}
											commit = true;
										}
									}
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
					}
				}
			}
		}
	}
}
