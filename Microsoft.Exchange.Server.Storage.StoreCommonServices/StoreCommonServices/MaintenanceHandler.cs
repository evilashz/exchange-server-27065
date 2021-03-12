using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000CA RID: 202
	public class MaintenanceHandler
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00027ECC File Offset: 0x000260CC
		private MaintenanceHandler()
		{
			this.databaseMaintenanceStates = new MaintenanceHandler.MaintenanceState[MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions.Count];
			this.databaseMaintenanceInProgress = new int[MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions.Count];
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00027F25 File Offset: 0x00026125
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x00027F2C File Offset: 0x0002612C
		public static bool AsyncMaintenanceSchedulingEnabled
		{
			get
			{
				return MaintenanceHandler.asyncMaintenanceSchedulingEnabled;
			}
			set
			{
				MaintenanceHandler.asyncMaintenanceSchedulingEnabled = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00027F34 File Offset: 0x00026134
		public static TimeSpan MailboxLockTimeout
		{
			get
			{
				return MaintenanceHandler.mailboxLockTimeout;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00027F3B File Offset: 0x0002613B
		private static MaintenanceHandler.RegistrationStateObject RegistrationState
		{
			get
			{
				return MaintenanceHandler.registrationStateHookable.Value;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00027F47 File Offset: 0x00026147
		public static IDatabaseMaintenance RegisterDatabaseMaintenance(Guid maintenanceId, RequiredMaintenanceResourceType requiredMaintenanceResourceType, MaintenanceHandler.DatabaseMaintenanceDelegate databaseMaintenanceDelegate, string maintenanceTaskName)
		{
			return MaintenanceHandler.RegisterDatabaseMaintenance(maintenanceId, requiredMaintenanceResourceType, databaseMaintenanceDelegate, maintenanceTaskName, 1);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00027F54 File Offset: 0x00026154
		public static IDatabaseMaintenance RegisterDatabaseMaintenance(Guid maintenanceId, RequiredMaintenanceResourceType requiredMaintenanceResourceType, MaintenanceHandler.DatabaseMaintenanceDelegate databaseMaintenanceDelegate, string maintenanceTaskName, int numberOfBatchesToSchedule)
		{
			MaintenanceHandler.DatabaseMaintenanceTaskDefinition databaseMaintenanceTaskDefinition = new MaintenanceHandler.DatabaseMaintenanceTaskDefinition(maintenanceId, MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions.Count, requiredMaintenanceResourceType, databaseMaintenanceDelegate, maintenanceTaskName, numberOfBatchesToSchedule);
			MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions.Add(databaseMaintenanceTaskDefinition);
			return databaseMaintenanceTaskDefinition;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00027F8D File Offset: 0x0002618D
		public static IMailboxMaintenance RegisterMailboxMaintenance(Guid maintenanceId, RequiredMaintenanceResourceType requiredMaintenanceResourceType, bool checkMailboxIsIdle, MaintenanceHandler.MailboxMaintenanceDelegate mailboxMaintenanceDelegate, string maintenanceTaskName)
		{
			return MaintenanceHandler.RegisterMailboxMaintenance(maintenanceId, requiredMaintenanceResourceType, checkMailboxIsIdle, mailboxMaintenanceDelegate, maintenanceTaskName, false);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00027F9C File Offset: 0x0002619C
		public static IMailboxMaintenance RegisterMailboxMaintenance(Guid maintenanceId, RequiredMaintenanceResourceType requiredMaintenanceResourceType, bool checkMailboxIsIdle, MaintenanceHandler.MailboxMaintenanceDelegate mailboxMaintenanceDelegate, string maintenanceTaskName, bool isFinal)
		{
			MaintenanceHandler.MailboxMaintenanceTaskDefinition mailboxMaintenanceTaskDefinition = new MaintenanceHandler.MailboxMaintenanceTaskDefinition(maintenanceId, MaintenanceHandler.RegistrationState.MailboxLevelMaintenanceDefinitions.Count, requiredMaintenanceResourceType, checkMailboxIsIdle, mailboxMaintenanceDelegate, maintenanceTaskName, isFinal);
			MaintenanceHandler.RegistrationState.MailboxLevelMaintenanceDefinitions.Add(mailboxMaintenanceTaskDefinition);
			return mailboxMaintenanceTaskDefinition;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00027FD8 File Offset: 0x000261D8
		public static void DoDatabaseMaintenance(Context context, DatabaseInfo databaseInfo, Guid maintenanceId)
		{
			MaintenanceHandler.DatabaseMaintenanceTaskDefinition databaseMaintenanceTaskDefinition = MaintenanceHandler.FindDatabaseMaintenanceDefinition(maintenanceId);
			if (databaseMaintenanceTaskDefinition == null)
			{
				throw new StoreException((LID)56584U, ErrorCodeValue.NotFound, "Invalid database maintenance ID");
			}
			MaintenanceHandler maintenanceHandler = MaintenanceHandler.GetMaintenanceHandler(context);
			if (Interlocked.CompareExchange(ref maintenanceHandler.databaseMaintenanceInProgress[databaseMaintenanceTaskDefinition.MaintenanceSlotIndex], 1, 0) != 0)
			{
				if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid>(52160L, "DoDatabaseMaintenance({0}) for database {1} is skipped because same maintenance is currently in progress.", databaseMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid);
				}
				return;
			}
			try
			{
				if (!AssistantActivityMonitor.Instance(context.Database)[databaseMaintenanceTaskDefinition.RequiredMaintenanceResourceType].AssistantIsActiveInLastMonitoringPeriod)
				{
					AssistantActivityMonitor.PublishActiveMonitoringNotification(databaseMaintenanceTaskDefinition.RequiredMaintenanceResourceType, context.Database.MdbName, ResultSeverityLevel.Informational);
				}
				AssistantActivityMonitor.Instance(context.Database).UpdateAssistantActivityState(databaseMaintenanceTaskDefinition.RequiredMaintenanceResourceType, false);
				if (!maintenanceHandler.databaseMaintenanceStates[databaseMaintenanceTaskDefinition.MaintenanceSlotIndex].MaintenanceRequired)
				{
					if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid>(33208L, "DoDatabaseMaintenance({0}) for database {1} called, but maintenance is not marked as required.", databaseMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid);
					}
					DiagnosticContext.TraceLocation((LID)60680U);
				}
				else if (MaintenanceHandler.ShouldStopDatabaseMaintenanceTask(context, maintenanceId))
				{
					if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid>(0L, "DoDatabaseMaintenance({0}) for database {1} called, but database maintenance tasks are not permitted to proceed, so the task is being pre-empted.", databaseMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid);
					}
					DiagnosticContext.TraceLocation((LID)52960U);
				}
				else
				{
					context.PerfInstance.RateOfDatabaseMaintenances.Increment();
					bool flag;
					try
					{
						maintenanceHandler.databaseMaintenanceStates[databaseMaintenanceTaskDefinition.MaintenanceSlotIndex].LastExecutionStarted = DateTime.UtcNow;
						databaseMaintenanceTaskDefinition.DatabaseMaintenanceDelegate(context, databaseInfo, out flag);
					}
					finally
					{
						maintenanceHandler.databaseMaintenanceStates[databaseMaintenanceTaskDefinition.MaintenanceSlotIndex].LastExecutionFinished = DateTime.UtcNow;
					}
					if (flag)
					{
						maintenanceHandler.databaseMaintenanceStates[databaseMaintenanceTaskDefinition.MaintenanceSlotIndex].MaintenanceRequired = false;
					}
					if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid, bool>(49080L, "DoDatabaseMaintenance({0}) for database {1} finished, completed={2}.", databaseMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid, flag);
					}
				}
			}
			finally
			{
				Interlocked.CompareExchange(ref maintenanceHandler.databaseMaintenanceInProgress[databaseMaintenanceTaskDefinition.MaintenanceSlotIndex], 0, 1);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002824C File Offset: 0x0002644C
		public static string GetDatabaseMaintenanceTaskName(Guid maintenanceId)
		{
			MaintenanceHandler.DatabaseMaintenanceTaskDefinition databaseMaintenanceTaskDefinition = MaintenanceHandler.FindDatabaseMaintenanceDefinition(maintenanceId);
			if (databaseMaintenanceTaskDefinition == null)
			{
				return null;
			}
			return databaseMaintenanceTaskDefinition.MaintenanceTaskName;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00028278 File Offset: 0x00026478
		public static void DoMailboxMaintenance(Context context, Guid maintenanceId, int mailboxNumber)
		{
			MaintenanceHandler.MailboxMaintenanceTaskDefinition mailboxMaintenanceTaskDefinition = MaintenanceHandler.FindMailboxMaintenanceDefinition(maintenanceId);
			if (mailboxMaintenanceTaskDefinition == null)
			{
				throw new StoreException((LID)44296U, ErrorCodeValue.NotFound, "Invalid mailbox maintenance ID");
			}
			MaintenanceHandler maintenanceHandler = MaintenanceHandler.GetMaintenanceHandler(context);
			AssistantActivityMonitor.Instance(context.Database).UpdateAssistantActivityState(mailboxMaintenanceTaskDefinition.RequiredMaintenanceResourceType, false);
			context.InitializeMailboxExclusiveOperation(mailboxNumber, ExecutionDiagnostics.OperationSource.MailboxMaintenance, mailboxMaintenanceTaskDefinition.CheckMailboxIsIdle ? TimeSpan.Zero : MaintenanceHandler.mailboxLockTimeout);
			bool commit = false;
			try
			{
				ErrorCode errorCode = context.StartMailboxOperation(MailboxCreation.DontAllow, true, true);
				if (errorCode != ErrorCode.NoError)
				{
					if (errorCode != ErrorCodeValue.Timeout)
					{
						if (errorCode == ErrorCodeValue.NotFound)
						{
							errorCode = ErrorCode.CreateUnknownMailbox((LID)40700U);
						}
						if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid, int>(49592L, "DoMailboxMaintenance({0}) for database {1} called with non existing mailbox. MailboxNumber={2}.", mailboxMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid, mailboxNumber);
						}
						throw new StoreException((LID)36104U, errorCode, "StartMailboxOperation failed");
					}
					if (mailboxMaintenanceTaskDefinition.CheckMailboxIsIdle)
					{
						DiagnosticContext.TraceLocation((LID)42844U);
						if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid, int>(49592L, "DoMailboxMaintenance({0}) for database {1}, MailboxNumber={2} skipped because mailbox is not idle.", mailboxMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid, mailboxNumber);
						}
					}
					else
					{
						DiagnosticContext.TraceLocation((LID)34652U);
						if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid, int>(44892L, "DoMailboxMaintenance({0}) for database {1}, MailboxNumber={2} skipped because mailbox lock contention.", mailboxMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid, mailboxNumber);
						}
					}
				}
				else
				{
					bool flag = false;
					MaintenanceHandler.MaintenanceState[] array = null;
					if (!context.LockedMailboxState.Quarantined)
					{
						array = (MaintenanceHandler.MaintenanceState[])context.LockedMailboxState.GetComponentData(MaintenanceHandler.RegistrationState.MailboxMaintenanceStatesSlot);
						if (array == null)
						{
							maintenanceHandler.mailboxNumberToMaintenanceStates.TryGetValue(context.LockedMailboxState.MailboxNumber, out array);
						}
						if (array == null || !array[mailboxMaintenanceTaskDefinition.MaintenanceSlotIndex].MaintenanceRequired)
						{
							if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid, int>(65464L, "DoMailboxMaintenance({0}) for database {1}, mailbox {2} called, but maintenance is not marked as required.", mailboxMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid, mailboxNumber);
							}
							DiagnosticContext.TraceLocation((LID)52488U);
							return;
						}
						if (MaintenanceHandler.ShouldStopMailboxMaintenanceTask(context, context.LockedMailboxState, maintenanceId))
						{
							if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid, int>(0L, "DoMailboxMaintenance({0}) for database {1}, mailbox {2} called, but mailbox maintenance tasks are not permitted to proceed, so the task is being pre-empted.", mailboxMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid, mailboxNumber);
							}
							DiagnosticContext.TraceLocation((LID)46816U);
							return;
						}
						try
						{
							array[mailboxMaintenanceTaskDefinition.MaintenanceSlotIndex].LastExecutionStarted = DateTime.UtcNow;
							mailboxMaintenanceTaskDefinition.MailboxMaintenanceDelegate(context, context.LockedMailboxState, out flag);
							commit = true;
						}
						finally
						{
							if (context.IsMailboxOperationStarted)
							{
								array[mailboxMaintenanceTaskDefinition.MaintenanceSlotIndex].LastExecutionFinished = DateTime.UtcNow;
							}
						}
						context.PerfInstance.RateOfMailboxMaintenances.Increment();
					}
					else if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.MaintenanceTracer.TraceDebug<string, Guid, int>(46720L, "DoMailboxMaintenance({0}) for database {1}, mailbox {2} called, but mailbox is quarantined.", mailboxMaintenanceTaskDefinition.MaintenanceTaskName, context.Database.MdbGuid, mailboxNumber);
					}
					if (!context.IsMailboxOperationStarted)
					{
						DiagnosticContext.TraceLocation((LID)59228U);
					}
					else
					{
						if (flag && array[mailboxMaintenanceTaskDefinition.MaintenanceSlotIndex].MaintenanceRequired)
						{
							context.PerfInstance.MailboxMaintenances.Decrement();
							array[mailboxMaintenanceTaskDefinition.MaintenanceSlotIndex].MaintenanceRequired = false;
							if (array.All((MaintenanceHandler.MaintenanceState state) => !state.MaintenanceRequired))
							{
								DiagnosticContext.TraceLocation((LID)57784U);
								MaintenanceHandler.MaintenanceState[] array2;
								maintenanceHandler.mailboxNumberToMaintenanceStates.TryRemove(mailboxNumber, out array2);
								context.LockedMailboxState.SetComponentData(MaintenanceHandler.RegistrationState.MailboxMaintenanceStatesSlot, null);
								context.PerfInstance.MailboxesWithMaintenances.Decrement();
							}
						}
						if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MaintenanceTracer.TraceDebug(57272L, "DoMailboxMaintenance({0}) for database {1}, mailbox {2} finished, completed = {3}.", new object[]
							{
								mailboxMaintenanceTaskDefinition.MaintenanceTaskName,
								context.Database.MdbGuid,
								mailboxNumber,
								flag
							});
						}
					}
				}
			}
			finally
			{
				if (context.IsMailboxOperationStarted)
				{
					context.EndMailboxOperation(commit, false);
				}
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002871C File Offset: 0x0002691C
		public static string GetMailboxMaintenanceTaskName(Guid maintenanceId)
		{
			MaintenanceHandler.MailboxMaintenanceTaskDefinition mailboxMaintenanceTaskDefinition = MaintenanceHandler.FindMailboxMaintenanceDefinition(maintenanceId);
			if (mailboxMaintenanceTaskDefinition == null)
			{
				return null;
			}
			return mailboxMaintenanceTaskDefinition.MaintenanceTaskName;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002873C File Offset: 0x0002693C
		public static IEnumerable<MaintenanceHandler.QueryableMaintenanceState> GetDatabaseMaintenanceSnapshot(Context context)
		{
			MaintenanceHandler.QueryableMaintenanceState[] array = new MaintenanceHandler.QueryableMaintenanceState[MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions.Count];
			MaintenanceHandler maintenanceHandler = MaintenanceHandler.GetMaintenanceHandler(context);
			for (int i = 0; i < MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions.Count; i++)
			{
				MaintenanceHandler.DatabaseMaintenanceTaskDefinition databaseMaintenanceTaskDefinition = MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions[i];
				MaintenanceHandler.MaintenanceState maintenanceState = maintenanceHandler.databaseMaintenanceStates[databaseMaintenanceTaskDefinition.MaintenanceSlotIndex];
				array[i] = new MaintenanceHandler.QueryableMaintenanceState(databaseMaintenanceTaskDefinition.MaintenanceId, databaseMaintenanceTaskDefinition.RequiredMaintenanceResourceType, databaseMaintenanceTaskDefinition.MaintenanceTaskName, maintenanceState.MaintenanceRequired, maintenanceState.LastRequested, maintenanceState.LastExecutionStarted, maintenanceState.LastExecutionFinished);
			}
			return array;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000287E0 File Offset: 0x000269E0
		public static IEnumerable<MaintenanceHandler.QueryableMailboxMaintenanceState> GetMailboxMaintenanceSnapshot(Context context)
		{
			List<MaintenanceHandler.QueryableMailboxMaintenanceState> list = new List<MaintenanceHandler.QueryableMailboxMaintenanceState>(10 * MaintenanceHandler.RegistrationState.MailboxLevelMaintenanceDefinitions.Count);
			MaintenanceHandler maintenanceHandler = MaintenanceHandler.GetMaintenanceHandler(context);
			foreach (KeyValuePair<int, MaintenanceHandler.MaintenanceState[]> keyValuePair in maintenanceHandler.mailboxNumberToMaintenanceStates)
			{
				for (int i = 0; i < keyValuePair.Value.Length; i++)
				{
					MaintenanceHandler.MailboxMaintenanceTaskDefinition mailboxMaintenanceTaskDefinition = MaintenanceHandler.RegistrationState.MailboxLevelMaintenanceDefinitions[i];
					MaintenanceHandler.MaintenanceState maintenanceState = keyValuePair.Value[i];
					list.Add(new MaintenanceHandler.QueryableMailboxMaintenanceState(mailboxMaintenanceTaskDefinition.MaintenanceId, mailboxMaintenanceTaskDefinition.RequiredMaintenanceResourceType, mailboxMaintenanceTaskDefinition.MaintenanceTaskName, mailboxMaintenanceTaskDefinition.IsFinal, keyValuePair.Key, maintenanceState.MaintenanceRequired, maintenanceState.LastRequested, maintenanceState.LastExecutionStarted, maintenanceState.LastExecutionFinished));
				}
			}
			return list;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000288D8 File Offset: 0x00026AD8
		public static List<MaintenanceHandler.MaintenanceToSchedule> GetScheduledMaintenances(Context context, RequiredMaintenanceResourceType requestedMaintenanceResourceType)
		{
			List<MaintenanceHandler.MaintenanceToSchedule> list = new List<MaintenanceHandler.MaintenanceToSchedule>(10);
			MaintenanceHandler maintenanceHandler = MaintenanceHandler.GetMaintenanceHandler(context);
			AssistantActivityMonitor.Instance(context.Database).UpdateAssistantActivityState(requestedMaintenanceResourceType, true);
			for (int i = 0; i < MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions.Count; i++)
			{
				MaintenanceHandler.DatabaseMaintenanceTaskDefinition databaseMaintenanceTaskDefinition = MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions[i];
				if (maintenanceHandler.databaseMaintenanceStates[databaseMaintenanceTaskDefinition.MaintenanceSlotIndex].MaintenanceRequired && databaseMaintenanceTaskDefinition.RequiredMaintenanceResourceType == requestedMaintenanceResourceType)
				{
					for (int j = 0; j < databaseMaintenanceTaskDefinition.NumberOfBatchesToSchedule; j++)
					{
						list.Add(new MaintenanceHandler.MaintenanceToSchedule(databaseMaintenanceTaskDefinition.MaintenanceId, 0, Guid.Empty));
					}
				}
				if ((long)list.Count >= (long)((ulong)MaintenanceHandler.maximumMaintenanceRecordsToReturn.Value))
				{
					break;
				}
			}
			foreach (KeyValuePair<int, MaintenanceHandler.MaintenanceState[]> keyValuePair in maintenanceHandler.mailboxNumberToMaintenanceStates)
			{
				if ((long)(list.Count + MaintenanceHandler.RegistrationState.MailboxLevelMaintenanceDefinitions.Count) > (long)((ulong)MaintenanceHandler.maximumMaintenanceRecordsToReturn.Value))
				{
					break;
				}
				MailboxState mailboxState = MailboxStateCache.Get(context, keyValuePair.Key);
				if (mailboxState == null || mailboxState.Quarantined)
				{
					if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.MaintenanceTracer.TraceDebug<Guid, string, string>(40888L, "GetScheduledMaintenances on database {0} called for mailbox {1}, no maintenance record will be returned becuase of {2}.", context.Database.MdbGuid, (mailboxState == null) ? "<N/A>" : mailboxState.MailboxGuid.ToString(), (mailboxState == null) ? "mailbox does not exist" : "Quarantined");
					}
				}
				else
				{
					for (int k = 0; k < keyValuePair.Value.Length; k++)
					{
						MaintenanceHandler.MailboxMaintenanceTaskDefinition mailboxMaintenanceTaskDefinition = MaintenanceHandler.RegistrationState.MailboxLevelMaintenanceDefinitions[k];
						if (keyValuePair.Value[k].MaintenanceRequired && mailboxMaintenanceTaskDefinition.RequiredMaintenanceResourceType == requestedMaintenanceResourceType)
						{
							list.Add(new MaintenanceHandler.MaintenanceToSchedule(mailboxMaintenanceTaskDefinition.MaintenanceId, keyValuePair.Key, mailboxState.MailboxGuid));
						}
					}
				}
			}
			if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MaintenanceTracer.TraceDebug<Guid, int>(63104L, "GetScheduledMaintenances for database {0} called. {1} maintenances returned.", context.Database.MdbGuid, list.Count);
			}
			return list;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00028B2C File Offset: 0x00026D2C
		public static bool ShouldStopDatabaseMaintenanceTask(Context context, Guid maintenanceId)
		{
			return context.Database.HasExclusiveLockContention() || context.Database.IsDatabaseEngineTooBusyForDatabaseMaintenanceTask(maintenanceId);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00028B49 File Offset: 0x00026D49
		public static bool ShouldStopMailboxMaintenanceTask(Context context, MailboxState mailboxState, Guid maintenanceId)
		{
			return context.Database.HasExclusiveLockContention() || context.Database.IsDatabaseEngineTooBusyForMailboxMaintenanceTask(mailboxState, maintenanceId);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00028B68 File Offset: 0x00026D68
		public static void ApplyMaintenanceToActiveAndDeletedMailboxes(Context context, ExecutionDiagnostics.OperationSource operationSource, Action<Context, MailboxState> maintenanceDelegate, out bool completed)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(context.Database);
			SearchCriteria searchCriteria = Factory.CreateSearchCriteriaCompare(mailboxTable.LastLogonTime, SearchCriteriaCompare.SearchRelOp.GreaterThan, Factory.CreateConstantColumn(DateTime.UtcNow.Subtract(TimeSpan.FromDays(2.0))));
			SearchCriteria searchCriteria2 = Factory.CreateSearchCriteriaCompare(mailboxTable.DeletedOn, SearchCriteriaCompare.SearchRelOp.GreaterThan, Factory.CreateConstantColumn(DateTime.UtcNow.Subtract(TimeSpan.FromDays(3.0))));
			IEnumerable<MailboxState> stateListSnapshot = MailboxStateCache.GetStateListSnapshot(context, Factory.CreateSearchCriteriaOr(new SearchCriteria[]
			{
				searchCriteria,
				searchCriteria2
			}));
			MaintenanceHandler.ApplyMaintenanceToMailboxes(context, stateListSnapshot, operationSource, true, maintenanceDelegate, out completed);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00028C14 File Offset: 0x00026E14
		public static void ApplyMaintenanceToMailboxes(Context context, IEnumerable<MailboxState> mailboxList, ExecutionDiagnostics.OperationSource operationSource, bool checkMailboxIsAccessible, Action<Context, MailboxState> maintenanceDelegate, out bool completed)
		{
			completed = true;
			foreach (MailboxState mailboxState in mailboxList)
			{
				bool flag;
				MaintenanceHandler.ApplyMaintenanceToOneMailbox(context, mailboxState.MailboxNumber, operationSource, checkMailboxIsAccessible, maintenanceDelegate, out flag);
				if (!flag)
				{
					completed = false;
				}
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00028C74 File Offset: 0x00026E74
		public static void ApplyMaintenanceToOneMailbox(Context context, int mailboxNumber, ExecutionDiagnostics.OperationSource operationSource, bool checkMailboxIsAccessible, Action<Context, MailboxState> maintenanceDelegate, out bool completed)
		{
			context.InitializeMailboxExclusiveOperation(mailboxNumber, ExecutionDiagnostics.OperationSource.MailboxMaintenance, MaintenanceHandler.mailboxLockTimeout);
			bool commit = false;
			try
			{
				ErrorCode errorCode = context.StartMailboxOperation(MailboxCreation.DontAllow, true, true);
				if (errorCode != ErrorCode.NoError)
				{
					if (errorCode == ErrorCodeValue.NotFound)
					{
						completed = true;
					}
					else
					{
						completed = false;
					}
					return;
				}
				if (checkMailboxIsAccessible && !context.LockedMailboxState.IsAccessible)
				{
					completed = true;
					return;
				}
				context.LockedMailboxState.AddReference();
				try
				{
					maintenanceDelegate(context, context.LockedMailboxState);
				}
				finally
				{
					context.LockedMailboxState.ReleaseReference();
				}
				commit = true;
			}
			finally
			{
				if (context.IsMailboxOperationStarted)
				{
					context.EndMailboxOperation(commit, false);
				}
			}
			completed = true;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00028D38 File Offset: 0x00026F38
		internal static void Initialize()
		{
			if (MaintenanceHandler.RegistrationState.MailboxMaintenanceStatesSlot == -1)
			{
				MaintenanceHandler.RegistrationState.MailboxMaintenanceStatesSlot = MailboxState.AllocateComponentDataSlot(true);
			}
			if (MaintenanceHandler.RegistrationState.MaintenanceHanderDatabaseSlot == -1)
			{
				MaintenanceHandler.RegistrationState.MaintenanceHanderDatabaseSlot = StoreDatabase.AllocateComponentDataSlot();
			}
			if (MaintenanceHandler.registrationStateForTest.MailboxMaintenanceStatesSlot == -1)
			{
				MaintenanceHandler.registrationStateForTest.MailboxMaintenanceStatesSlot = MailboxState.AllocateComponentDataSlot(true);
			}
			if (MaintenanceHandler.registrationStateForTest.MaintenanceHanderDatabaseSlot == -1)
			{
				MaintenanceHandler.registrationStateForTest.MaintenanceHanderDatabaseSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00028DB7 File Offset: 0x00026FB7
		internal static IDisposable SetRegistrationStateTestHook()
		{
			MaintenanceHandler.registrationStateForTest.DatabaseLevelMaintenanceDefinitions.Clear();
			MaintenanceHandler.registrationStateForTest.MailboxLevelMaintenanceDefinitions.Clear();
			return MaintenanceHandler.registrationStateHookable.SetTestHook(MaintenanceHandler.registrationStateForTest);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00028DE6 File Offset: 0x00026FE6
		internal static IDisposable SetDatabaseTaskRegistrationHook(Action<Guid> action)
		{
			return MaintenanceHandler.databaseTaskRegistrationHook.SetTestHook(action);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00028DF3 File Offset: 0x00026FF3
		internal static IDisposable SetDatabaseTaskExecutionHook(Action<Guid> action)
		{
			return MaintenanceHandler.databaseTaskExecutionHook.SetTestHook(action);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00028E00 File Offset: 0x00027000
		internal static void MountHandler(Context context)
		{
			StoreDatabase database = context.Database;
			MaintenanceHandler value = new MaintenanceHandler();
			database.ComponentData[MaintenanceHandler.RegistrationState.MaintenanceHanderDatabaseSlot] = value;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00028E30 File Offset: 0x00027030
		internal static void DismountHandler(StoreDatabase database)
		{
			database.ComponentData[MaintenanceHandler.RegistrationState.MaintenanceHanderDatabaseSlot] = null;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00028E48 File Offset: 0x00027048
		internal static void TestResetMailboxStateMaintenanceSlot(MailboxState mailboxState)
		{
			mailboxState.SetComponentData(MaintenanceHandler.RegistrationState.MailboxMaintenanceStatesSlot, null);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00028E5B File Offset: 0x0002705B
		internal static IDisposable SetMaximumNumberOfMaintenanceRecordsForTest(uint maximumNumberOfMaintenanceRecords)
		{
			return MaintenanceHandler.maximumMaintenanceRecordsToReturn.SetTestHook(maximumNumberOfMaintenanceRecords);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00028E68 File Offset: 0x00027068
		[Conditional("DEBUG")]
		private static void AssertStaticInitialized()
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00028E6A File Offset: 0x0002706A
		private static MaintenanceHandler GetMaintenanceHandler(Context context)
		{
			return (MaintenanceHandler)context.Database.ComponentData[MaintenanceHandler.RegistrationState.MaintenanceHanderDatabaseSlot];
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00028E8C File Offset: 0x0002708C
		private static MaintenanceHandler.DatabaseMaintenanceTaskDefinition FindDatabaseMaintenanceDefinition(Guid maintenanceId)
		{
			foreach (MaintenanceHandler.DatabaseMaintenanceTaskDefinition databaseMaintenanceTaskDefinition in MaintenanceHandler.RegistrationState.DatabaseLevelMaintenanceDefinitions)
			{
				if (maintenanceId == databaseMaintenanceTaskDefinition.MaintenanceId)
				{
					return databaseMaintenanceTaskDefinition;
				}
			}
			return null;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00028EF4 File Offset: 0x000270F4
		private static MaintenanceHandler.MailboxMaintenanceTaskDefinition FindMailboxMaintenanceDefinition(Guid maintenanceId)
		{
			foreach (MaintenanceHandler.MailboxMaintenanceTaskDefinition mailboxMaintenanceTaskDefinition in MaintenanceHandler.RegistrationState.MailboxLevelMaintenanceDefinitions)
			{
				if (maintenanceId == mailboxMaintenanceTaskDefinition.MaintenanceId)
				{
					return mailboxMaintenanceTaskDefinition;
				}
			}
			return null;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00028F5C File Offset: 0x0002715C
		private static bool MarkDatabaseForMaintenance(Context context, MaintenanceHandler.DatabaseMaintenanceTaskDefinition maintenanceDefinition)
		{
			bool result = false;
			MaintenanceHandler maintenanceHandler = MaintenanceHandler.GetMaintenanceHandler(context);
			if (!maintenanceHandler.databaseMaintenanceStates[maintenanceDefinition.MaintenanceSlotIndex].MaintenanceRequired)
			{
				result = true;
				maintenanceHandler.databaseMaintenanceStates[maintenanceDefinition.MaintenanceSlotIndex].LastRequested = DateTime.UtcNow;
				maintenanceHandler.databaseMaintenanceStates[maintenanceDefinition.MaintenanceSlotIndex].MaintenanceRequired = true;
			}
			if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MaintenanceTracer.TraceDebug<Guid, Guid>(53176L, "Database {0} marked for maintenance {1}.", context.Database.MdbGuid, maintenanceDefinition.MaintenanceId);
			}
			return result;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00028FF4 File Offset: 0x000271F4
		private static bool MarkMailboxForMaintenance(Context context, MailboxState mailboxState, MaintenanceHandler.MailboxMaintenanceTaskDefinition maintenanceDefinition)
		{
			bool result = false;
			MaintenanceHandler.MaintenanceState[] array = (MaintenanceHandler.MaintenanceState[])mailboxState.GetComponentData(MaintenanceHandler.RegistrationState.MailboxMaintenanceStatesSlot);
			if (array == null)
			{
				MaintenanceHandler maintenanceHandler = MaintenanceHandler.GetMaintenanceHandler(context);
				if (!maintenanceHandler.mailboxNumberToMaintenanceStates.TryGetValue(mailboxState.MailboxNumber, out array))
				{
					array = new MaintenanceHandler.MaintenanceState[MaintenanceHandler.RegistrationState.MailboxLevelMaintenanceDefinitions.Count];
					context.PerfInstance.MailboxesWithMaintenances.Increment();
				}
				mailboxState.SetComponentData(MaintenanceHandler.RegistrationState.MailboxMaintenanceStatesSlot, array);
				maintenanceHandler.mailboxNumberToMaintenanceStates[mailboxState.MailboxNumber] = array;
			}
			if (maintenanceDefinition.IsFinal)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].MaintenanceRequired)
					{
						array[i].MaintenanceRequired = false;
						context.PerfInstance.MailboxMaintenances.Decrement();
					}
				}
			}
			if (!array[maintenanceDefinition.MaintenanceSlotIndex].MaintenanceRequired)
			{
				context.PerfInstance.MailboxMaintenances.Increment();
				array[maintenanceDefinition.MaintenanceSlotIndex].LastRequested = DateTime.UtcNow;
				array[maintenanceDefinition.MaintenanceSlotIndex].MaintenanceRequired = true;
				result = true;
			}
			if (ExTraceGlobals.MaintenanceTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MaintenanceTracer.TraceDebug<int, Guid, Guid>(36792L, "MailboxNumber {0} on database {1} marked for maintenance {2}.", mailboxState.MailboxNumber, context.Database.MdbGuid, maintenanceDefinition.MaintenanceId);
			}
			return result;
		}

		// Token: 0x040004BB RID: 1211
		private static Hookable<MaintenanceHandler.RegistrationStateObject> registrationStateHookable = Hookable<MaintenanceHandler.RegistrationStateObject>.Create(false, new MaintenanceHandler.RegistrationStateObject());

		// Token: 0x040004BC RID: 1212
		private static Hookable<uint> maximumMaintenanceRecordsToReturn = Hookable<uint>.Create(false, 1000U);

		// Token: 0x040004BD RID: 1213
		private static Hookable<Action<Guid>> databaseTaskRegistrationHook = Hookable<Action<Guid>>.Create(true, delegate(Guid guid)
		{
		});

		// Token: 0x040004BE RID: 1214
		private static Hookable<Action<Guid>> databaseTaskExecutionHook = Hookable<Action<Guid>>.Create(true, delegate(Guid guid)
		{
		});

		// Token: 0x040004BF RID: 1215
		private static MaintenanceHandler.RegistrationStateObject registrationStateForTest = new MaintenanceHandler.RegistrationStateObject();

		// Token: 0x040004C0 RID: 1216
		private static bool asyncMaintenanceSchedulingEnabled = true;

		// Token: 0x040004C1 RID: 1217
		private static readonly TimeSpan mailboxLockTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x040004C2 RID: 1218
		private readonly MaintenanceHandler.MaintenanceState[] databaseMaintenanceStates;

		// Token: 0x040004C3 RID: 1219
		private readonly int[] databaseMaintenanceInProgress;

		// Token: 0x040004C4 RID: 1220
		private readonly ConcurrentDictionary<int, MaintenanceHandler.MaintenanceState[]> mailboxNumberToMaintenanceStates = new ConcurrentDictionary<int, MaintenanceHandler.MaintenanceState[]>(50, 1000);

		// Token: 0x020000CB RID: 203
		private enum DatabaseMaintenanceStatus
		{
			// Token: 0x040004C9 RID: 1225
			Idle,
			// Token: 0x040004CA RID: 1226
			InProgress
		}

		// Token: 0x020000CC RID: 204
		// (Invoke) Token: 0x06000863 RID: 2147
		public delegate void DatabaseMaintenanceDelegate(Context context, DatabaseInfo databaseInfo, out bool completed);

		// Token: 0x020000CD RID: 205
		// (Invoke) Token: 0x06000867 RID: 2151
		public delegate void MailboxMaintenanceDelegate(Context context, MailboxState lockedMailboxState, out bool completed);

		// Token: 0x020000CE RID: 206
		public class QueryableMaintenanceState
		{
			// Token: 0x0600086A RID: 2154 RVA: 0x000291F0 File Offset: 0x000273F0
			internal QueryableMaintenanceState(Guid maintenanceId, RequiredMaintenanceResourceType requiredMaintenanceResourceType, string maintenanceTaskName, bool maintenanceRequired, DateTime lastRequested, DateTime lastExecutionStarted, DateTime lastExecutionFinished)
			{
				this.maintenanceId = maintenanceId;
				this.requiredMaintenanceResourceType = requiredMaintenanceResourceType;
				this.maintenanceTaskName = maintenanceTaskName;
				this.maintenanceRequired = maintenanceRequired;
				this.lastRequested = lastRequested;
				this.lastExecutionStarted = lastExecutionStarted;
				this.lastExecutionFinished = lastExecutionFinished;
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x0600086B RID: 2155 RVA: 0x0002922D File Offset: 0x0002742D
			public Guid MaintenanceId
			{
				get
				{
					return this.maintenanceId;
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x0600086C RID: 2156 RVA: 0x00029235 File Offset: 0x00027435
			public RequiredMaintenanceResourceType RequiredMaintenanceResourceType
			{
				get
				{
					return this.requiredMaintenanceResourceType;
				}
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x0600086D RID: 2157 RVA: 0x0002923D File Offset: 0x0002743D
			public string MaintenanceTaskName
			{
				get
				{
					return this.maintenanceTaskName;
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x0600086E RID: 2158 RVA: 0x00029245 File Offset: 0x00027445
			public bool MaintenanceRequired
			{
				get
				{
					return this.maintenanceRequired;
				}
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002924D File Offset: 0x0002744D
			public DateTime LastRequested
			{
				get
				{
					return this.lastRequested;
				}
			}

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x06000870 RID: 2160 RVA: 0x00029255 File Offset: 0x00027455
			public DateTime LastExecutionStarted
			{
				get
				{
					return this.lastExecutionStarted;
				}
			}

			// Token: 0x17000222 RID: 546
			// (get) Token: 0x06000871 RID: 2161 RVA: 0x0002925D File Offset: 0x0002745D
			public DateTime LastExecutionFinished
			{
				get
				{
					return this.lastExecutionFinished;
				}
			}

			// Token: 0x040004CB RID: 1227
			private readonly Guid maintenanceId;

			// Token: 0x040004CC RID: 1228
			private readonly RequiredMaintenanceResourceType requiredMaintenanceResourceType;

			// Token: 0x040004CD RID: 1229
			private readonly string maintenanceTaskName;

			// Token: 0x040004CE RID: 1230
			private readonly bool maintenanceRequired;

			// Token: 0x040004CF RID: 1231
			private readonly DateTime lastRequested;

			// Token: 0x040004D0 RID: 1232
			private readonly DateTime lastExecutionStarted;

			// Token: 0x040004D1 RID: 1233
			private readonly DateTime lastExecutionFinished;
		}

		// Token: 0x020000CF RID: 207
		public class QueryableMailboxMaintenanceState : MaintenanceHandler.QueryableMaintenanceState
		{
			// Token: 0x06000872 RID: 2162 RVA: 0x00029265 File Offset: 0x00027465
			internal QueryableMailboxMaintenanceState(Guid maintenanceId, RequiredMaintenanceResourceType requiredMaintenanceResourceType, string maintenanceTaskName, bool isFinal, int mailboxNumber, bool maintenanceRequired, DateTime lastRequested, DateTime lastExecutionStarted, DateTime lastExecutionFinished) : base(maintenanceId, requiredMaintenanceResourceType, maintenanceTaskName, maintenanceRequired, lastRequested, lastExecutionStarted, lastExecutionFinished)
			{
				this.isFinal = isFinal;
				this.mailboxNumber = mailboxNumber;
			}

			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06000873 RID: 2163 RVA: 0x00029288 File Offset: 0x00027488
			public bool IsFinal
			{
				get
				{
					return this.isFinal;
				}
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x06000874 RID: 2164 RVA: 0x00029290 File Offset: 0x00027490
			public int MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x040004D2 RID: 1234
			private readonly bool isFinal;

			// Token: 0x040004D3 RID: 1235
			private readonly int mailboxNumber;
		}

		// Token: 0x020000D0 RID: 208
		public struct MaintenanceToSchedule
		{
			// Token: 0x06000875 RID: 2165 RVA: 0x00029298 File Offset: 0x00027498
			public MaintenanceToSchedule(Guid maintenanceId, int mailboxNumber, Guid mailboxGuid)
			{
				this.maintenanceId = maintenanceId;
				this.mailboxNumber = mailboxNumber;
				this.mailboxGuid = mailboxGuid;
			}

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x06000876 RID: 2166 RVA: 0x000292AF File Offset: 0x000274AF
			public Guid MaintenanceId
			{
				get
				{
					return this.maintenanceId;
				}
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x06000877 RID: 2167 RVA: 0x000292B7 File Offset: 0x000274B7
			public int MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06000878 RID: 2168 RVA: 0x000292BF File Offset: 0x000274BF
			public Guid MailboxGuid
			{
				get
				{
					return this.mailboxGuid;
				}
			}

			// Token: 0x040004D4 RID: 1236
			private Guid maintenanceId;

			// Token: 0x040004D5 RID: 1237
			private int mailboxNumber;

			// Token: 0x040004D6 RID: 1238
			private Guid mailboxGuid;
		}

		// Token: 0x020000D1 RID: 209
		private struct MaintenanceState
		{
			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06000879 RID: 2169 RVA: 0x000292C7 File Offset: 0x000274C7
			// (set) Token: 0x0600087A RID: 2170 RVA: 0x000292CF File Offset: 0x000274CF
			internal bool MaintenanceRequired
			{
				get
				{
					return this.maintenanceRequired;
				}
				set
				{
					this.maintenanceRequired = value;
				}
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x0600087B RID: 2171 RVA: 0x000292D8 File Offset: 0x000274D8
			// (set) Token: 0x0600087C RID: 2172 RVA: 0x000292E0 File Offset: 0x000274E0
			internal DateTime LastExecutionStarted
			{
				get
				{
					return this.lastExecutionStarted;
				}
				set
				{
					this.lastExecutionStarted = value;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x0600087D RID: 2173 RVA: 0x000292E9 File Offset: 0x000274E9
			// (set) Token: 0x0600087E RID: 2174 RVA: 0x000292F1 File Offset: 0x000274F1
			internal DateTime LastExecutionFinished
			{
				get
				{
					return this.lastExecutionFinished;
				}
				set
				{
					this.lastExecutionFinished = value;
				}
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x0600087F RID: 2175 RVA: 0x000292FA File Offset: 0x000274FA
			// (set) Token: 0x06000880 RID: 2176 RVA: 0x00029302 File Offset: 0x00027502
			internal DateTime LastRequested
			{
				get
				{
					return this.lastRequested;
				}
				set
				{
					this.lastRequested = value;
				}
			}

			// Token: 0x040004D7 RID: 1239
			private bool maintenanceRequired;

			// Token: 0x040004D8 RID: 1240
			private DateTime lastExecutionStarted;

			// Token: 0x040004D9 RID: 1241
			private DateTime lastExecutionFinished;

			// Token: 0x040004DA RID: 1242
			private DateTime lastRequested;
		}

		// Token: 0x020000D2 RID: 210
		private class MaintenanceTaskDefinition
		{
			// Token: 0x06000881 RID: 2177 RVA: 0x0002930B File Offset: 0x0002750B
			protected MaintenanceTaskDefinition(Guid maintenanceId, int maintenanceSlotIndex, RequiredMaintenanceResourceType requiredMaintenanceResourceType, string maintenanceTaskName)
			{
				this.maintenanceId = maintenanceId;
				this.maintenanceSlotIndex = maintenanceSlotIndex;
				this.requiredMaintenanceResourceType = requiredMaintenanceResourceType;
				this.maintenanceTaskName = maintenanceTaskName;
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x06000882 RID: 2178 RVA: 0x00029330 File Offset: 0x00027530
			internal Guid MaintenanceId
			{
				get
				{
					return this.maintenanceId;
				}
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x06000883 RID: 2179 RVA: 0x00029338 File Offset: 0x00027538
			internal int MaintenanceSlotIndex
			{
				get
				{
					return this.maintenanceSlotIndex;
				}
			}

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x00029340 File Offset: 0x00027540
			internal RequiredMaintenanceResourceType RequiredMaintenanceResourceType
			{
				get
				{
					return this.requiredMaintenanceResourceType;
				}
			}

			// Token: 0x1700022F RID: 559
			// (get) Token: 0x06000885 RID: 2181 RVA: 0x00029348 File Offset: 0x00027548
			internal string MaintenanceTaskName
			{
				get
				{
					return this.maintenanceTaskName;
				}
			}

			// Token: 0x040004DB RID: 1243
			private readonly Guid maintenanceId;

			// Token: 0x040004DC RID: 1244
			private readonly int maintenanceSlotIndex;

			// Token: 0x040004DD RID: 1245
			private readonly RequiredMaintenanceResourceType requiredMaintenanceResourceType;

			// Token: 0x040004DE RID: 1246
			private readonly string maintenanceTaskName;
		}

		// Token: 0x020000D3 RID: 211
		private class DatabaseMaintenanceTaskDefinition : MaintenanceHandler.MaintenanceTaskDefinition, IDatabaseMaintenance
		{
			// Token: 0x06000886 RID: 2182 RVA: 0x00029350 File Offset: 0x00027550
			internal DatabaseMaintenanceTaskDefinition(Guid maintenanceId, int maintenanceSlotIndex, RequiredMaintenanceResourceType requiredMaintenanceResourceType, MaintenanceHandler.DatabaseMaintenanceDelegate databaseMaintenanceDelegate, string maintenanceTaskName, int numberOfBatchesToSchedule) : base(maintenanceId, maintenanceSlotIndex, requiredMaintenanceResourceType, maintenanceTaskName)
			{
				this.databaseMaintenanceDelegate = databaseMaintenanceDelegate;
				this.numberOfBatchesToSchedule = numberOfBatchesToSchedule;
			}

			// Token: 0x17000230 RID: 560
			// (get) Token: 0x06000887 RID: 2183 RVA: 0x0002936D File Offset: 0x0002756D
			internal MaintenanceHandler.DatabaseMaintenanceDelegate DatabaseMaintenanceDelegate
			{
				get
				{
					return this.databaseMaintenanceDelegate;
				}
			}

			// Token: 0x17000231 RID: 561
			// (get) Token: 0x06000888 RID: 2184 RVA: 0x00029375 File Offset: 0x00027575
			internal int NumberOfBatchesToSchedule
			{
				get
				{
					return this.numberOfBatchesToSchedule;
				}
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x0002937D File Offset: 0x0002757D
			public bool MarkForMaintenance(Context context)
			{
				return MaintenanceHandler.MarkDatabaseForMaintenance(context, this);
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x00029386 File Offset: 0x00027586
			public void ScheduleMarkForMaintenance(Context context, TimeSpan interval)
			{
				this.ScheduleMarkForMaintenance(context, TimeSpan.Zero, interval);
			}

			// Token: 0x0600088B RID: 2187 RVA: 0x00029398 File Offset: 0x00027598
			public void ScheduleMarkForMaintenance(Context context, TimeSpan initialDelay, TimeSpan interval)
			{
				MaintenanceHandler.databaseTaskRegistrationHook.Value(base.MaintenanceId);
				RecurringTask<StoreDatabase> task = new RecurringTask<StoreDatabase>(TaskExecutionWrapper<StoreDatabase>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.MarkForMaintenance, context.ClientType, context.Database.MdbGuid), new TaskExecutionWrapper<StoreDatabase>.TaskCallback<Context>(this.MarkForMaintenanceTask)), context.Database, initialDelay, interval, false);
				context.Database.TaskList.Add(task, true);
			}

			// Token: 0x0600088C RID: 2188 RVA: 0x00029408 File Offset: 0x00027608
			private void MarkForMaintenanceTask(Context context, StoreDatabase database, Func<bool> shouldCallbackContinue)
			{
				if (!MaintenanceHandler.AsyncMaintenanceSchedulingEnabled)
				{
					return;
				}
				using (context.AssociateWithDatabase(database))
				{
					if (database.IsOnlineActive)
					{
						MaintenanceHandler.databaseTaskExecutionHook.Value(base.MaintenanceId);
						this.MarkForMaintenance(context);
					}
				}
			}

			// Token: 0x040004DF RID: 1247
			private readonly MaintenanceHandler.DatabaseMaintenanceDelegate databaseMaintenanceDelegate;

			// Token: 0x040004E0 RID: 1248
			private readonly int numberOfBatchesToSchedule;
		}

		// Token: 0x020000D4 RID: 212
		private class MailboxMaintenanceTaskDefinition : MaintenanceHandler.MaintenanceTaskDefinition, IMailboxMaintenance
		{
			// Token: 0x0600088D RID: 2189 RVA: 0x0002946C File Offset: 0x0002766C
			internal MailboxMaintenanceTaskDefinition(Guid maintenanceId, int maintenanceSlotIndex, RequiredMaintenanceResourceType requiredMaintenanceResourceType, bool checkMailboxIsIdle, MaintenanceHandler.MailboxMaintenanceDelegate mailboxMaintenanceDelegate, string maintenanceTaskName, bool isFinal) : base(maintenanceId, maintenanceSlotIndex, requiredMaintenanceResourceType, maintenanceTaskName)
			{
				this.mailboxMaintenanceDelegate = mailboxMaintenanceDelegate;
				this.checkMailboxIsIdle = checkMailboxIsIdle;
				this.isFinal = isFinal;
			}

			// Token: 0x17000232 RID: 562
			// (get) Token: 0x0600088E RID: 2190 RVA: 0x00029491 File Offset: 0x00027691
			internal MaintenanceHandler.MailboxMaintenanceDelegate MailboxMaintenanceDelegate
			{
				get
				{
					return this.mailboxMaintenanceDelegate;
				}
			}

			// Token: 0x17000233 RID: 563
			// (get) Token: 0x0600088F RID: 2191 RVA: 0x00029499 File Offset: 0x00027699
			internal bool CheckMailboxIsIdle
			{
				get
				{
					return this.checkMailboxIsIdle;
				}
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06000890 RID: 2192 RVA: 0x000294A1 File Offset: 0x000276A1
			internal bool IsFinal
			{
				get
				{
					return this.isFinal;
				}
			}

			// Token: 0x06000891 RID: 2193 RVA: 0x000294A9 File Offset: 0x000276A9
			public bool MarkForMaintenance(Context context, MailboxState mailboxState)
			{
				return MaintenanceHandler.MarkMailboxForMaintenance(context, mailboxState, this);
			}

			// Token: 0x040004E1 RID: 1249
			private readonly MaintenanceHandler.MailboxMaintenanceDelegate mailboxMaintenanceDelegate;

			// Token: 0x040004E2 RID: 1250
			private readonly bool checkMailboxIsIdle;

			// Token: 0x040004E3 RID: 1251
			private readonly bool isFinal;
		}

		// Token: 0x020000D5 RID: 213
		private class RegistrationStateObject
		{
			// Token: 0x040004E4 RID: 1252
			internal int MailboxMaintenanceStatesSlot = -1;

			// Token: 0x040004E5 RID: 1253
			internal int MaintenanceHanderDatabaseSlot = -1;

			// Token: 0x040004E6 RID: 1254
			internal List<MaintenanceHandler.DatabaseMaintenanceTaskDefinition> DatabaseLevelMaintenanceDefinitions = new List<MaintenanceHandler.DatabaseMaintenanceTaskDefinition>();

			// Token: 0x040004E7 RID: 1255
			internal List<MaintenanceHandler.MailboxMaintenanceTaskDefinition> MailboxLevelMaintenanceDefinitions = new List<MaintenanceHandler.MailboxMaintenanceTaskDefinition>();
		}
	}
}
