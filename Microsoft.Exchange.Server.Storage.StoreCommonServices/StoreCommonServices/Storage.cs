using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000139 RID: 313
	public static class Storage
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0003CFD4 File Offset: 0x0003B1D4
		public static TaskList TaskList
		{
			get
			{
				return Storage.taskList;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0003CFDB File Offset: 0x0003B1DB
		internal static void Initialize()
		{
			Storage.taskList = new TaskList();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0003CFE7 File Offset: 0x0003B1E7
		public static void Terminate()
		{
			if (Storage.taskList != null)
			{
				Storage.taskList.Dispose();
				Storage.taskList = null;
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0003D000 File Offset: 0x0003B200
		public static void SetExiting(bool exiting)
		{
			Storage.exiting = exiting;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0003D008 File Offset: 0x0003B208
		public static StoreDatabase FindDatabase(Guid mdbGuid)
		{
			StoreDatabase result = null;
			if (!Storage.databases.TryGetValue(mdbGuid, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0003D029 File Offset: 0x0003B229
		public static ICollection<StoreDatabase> GetDatabaseListSnapshot()
		{
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3536203069U);
			}
			return Storage.databases.Values;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0003D054 File Offset: 0x0003B254
		public static ErrorCode MountDatabase(Context context, StoreDatabase database, MountFlags flags)
		{
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				ErrorCodeValue errorCodeValue = ErrorCode.NoError;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<ErrorCodeValue>(3569757501U, ref errorCodeValue);
				if (errorCodeValue != ErrorCode.NoError)
				{
					return ErrorCode.CreateWithLid((LID)49704U, errorCodeValue);
				}
			}
			bool flag = false;
			int num = 0;
			while (!flag && (long)num < 100L)
			{
				StoreDatabase storeDatabase;
				if (Storage.databases.TryGetValue(database.MdbGuid, out storeDatabase))
				{
					database = storeDatabase;
					flag = true;
				}
				else if (Storage.databases.TryAdd(database.MdbGuid, database))
				{
					flag = true;
				}
				num++;
			}
			if (!flag)
			{
				return ErrorCode.CreateCancel((LID)37816U);
			}
			bool flag2 = false;
			try
			{
				ErrorCode first = database.MountDatabase(context, flags, ref flag2);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)54200U);
				}
			}
			finally
			{
				if (flag2)
				{
					Storage.databases.Remove(database.MdbGuid);
				}
			}
			if ((flags & MountFlags.LogReplay) == MountFlags.None)
			{
				IBinaryLogger logger = LoggerManager.GetLogger(LoggerType.ReferenceData);
				if (logger != null && logger.IsLoggingEnabled)
				{
					bool boolValue = false;
					bool boolValue2 = false;
					bool boolValue3 = false;
					bool boolValue4 = false;
					bool boolValue5 = false;
					try
					{
						database.GetSharedLock();
						boolValue = database.IsMaintenance;
						boolValue2 = database.IsOnlineActive;
						boolValue3 = database.IsOnlinePassive;
						boolValue4 = database.IsPublic;
						boolValue5 = database.IsRecovery;
					}
					finally
					{
						database.ReleaseSharedLock();
					}
					using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.DatabaseInfo, true, false, database.MdbGuid.GetHashCode(), database.MdbName, database.GetCurrentSchemaVersion(context).Value, boolValue, boolValue2, boolValue3, boolValue4, boolValue5, database.ForestName))
					{
						logger.TryWrite(traceBuffer);
					}
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0003D248 File Offset: 0x0003B448
		public static ErrorCode PurgeDatabaseCache(Context context, Guid mdbGuid)
		{
			StoreDatabase storeDatabase;
			if (!Storage.databases.TryGetValue(mdbGuid, out storeDatabase))
			{
				return ErrorCode.NoError;
			}
			storeDatabase.GetExclusiveLock();
			try
			{
				if (storeDatabase.IsOnlineActive || storeDatabase.IsOnlinePassive)
				{
					storeDatabase.ResetDatabaseEngine();
				}
			}
			finally
			{
				storeDatabase.ReleaseExclusiveLock();
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0003D2A4 File Offset: 0x0003B4A4
		public static ErrorCode ExtendDatabase(Context context, Guid mdbGuid)
		{
			StoreDatabase storeDatabase;
			if (!Storage.databases.TryGetValue(mdbGuid, out storeDatabase))
			{
				return ErrorCode.CreateNotFound((LID)56072U);
			}
			using (context.AssociateWithDatabaseExclusive(storeDatabase))
			{
				if (storeDatabase.IsOnlineActive)
				{
					storeDatabase.ExtendDatabase(context);
				}
				else if (storeDatabase.IsOnlinePassive)
				{
					return ErrorCode.CreateInvalidParameter((LID)61032U);
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0003D32C File Offset: 0x0003B52C
		public static ErrorCode ShrinkDatabase(Context context, Guid mdbGuid)
		{
			StoreDatabase storeDatabase;
			if (!Storage.databases.TryGetValue(mdbGuid, out storeDatabase))
			{
				return ErrorCode.CreateNotFound((LID)36456U);
			}
			using (context.AssociateWithDatabaseExclusive(storeDatabase))
			{
				if (storeDatabase.IsOnlineActive)
				{
					storeDatabase.ShrinkDatabase(context);
				}
				else if (storeDatabase.IsOnlinePassive)
				{
					return ErrorCode.CreateInvalidParameter((LID)52840U);
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		public static ErrorCode VersionStoreCleanup(Context context, Guid mdbGuid)
		{
			StoreDatabase storeDatabase;
			if (!Storage.databases.TryGetValue(mdbGuid, out storeDatabase))
			{
				return ErrorCode.CreateNotFound((LID)49612U);
			}
			using (context.AssociateWithDatabaseExclusive(storeDatabase))
			{
				if (storeDatabase.IsOnlineActive)
				{
					storeDatabase.VersionStoreCleanup(context);
				}
				else if (storeDatabase.IsOnlinePassive)
				{
					return ErrorCode.CreateInvalidParameter((LID)35836U);
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0003D43C File Offset: 0x0003B63C
		public static ErrorCode DismountDatabase(Context context, Guid mdbGuid)
		{
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2496015677U);
			}
			StoreDatabase storeDatabase;
			if (!Storage.databases.TryGetValue(mdbGuid, out storeDatabase))
			{
				return ErrorCode.CreateNotFound((LID)64968U);
			}
			ErrorCode errorCode = storeDatabase.DismountDatabase(context);
			if (errorCode != ErrorCode.NoError)
			{
				storeDatabase.DismountError = errorCode;
				return errorCode.Propagate((LID)41912U);
			}
			Storage.databases.Remove(storeDatabase.MdbGuid);
			return ErrorCode.NoError;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0003D4D8 File Offset: 0x0003B6D8
		public static void DismountAllDatabases()
		{
			Storage.ForEachDatabaseExecuteAsyncAndWait(TaskTypeId.DismountDatabase, delegate(Context context, StoreDatabase database, Func<bool> shouldCallbackContinue)
			{
				Storage.DismountDatabase(context, database.MdbGuid);
			});
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0003D4FE File Offset: 0x0003B6FE
		public static bool WhileNotExiting()
		{
			return !Storage.exiting;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0003D508 File Offset: 0x0003B708
		public static void ForEachDatabase(Context context, Storage.DatabaseEnumerationCallback enumCallback)
		{
			Storage.ForEachDatabase(context, true, enumCallback);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0003D514 File Offset: 0x0003B714
		public static void ForEachDatabase(Context context, bool activeDatabasesOnly, Storage.DatabaseEnumerationCallback enumCallback)
		{
			foreach (StoreDatabase storeDatabase in Storage.GetDatabaseListSnapshot())
			{
				if (Storage.exiting)
				{
					break;
				}
				using (context.AssociateWithDatabase(storeDatabase))
				{
					if (storeDatabase.IsOnlineActive || !activeDatabasesOnly)
					{
						enumCallback(context, storeDatabase, new Func<bool>(Storage.WhileNotExiting));
					}
				}
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		public static void ForEachDatabaseExecuteAsyncAndWait(TaskTypeId taskTypeId, TaskExecutionWrapper<StoreDatabase>.TaskCallback<Context> enumCallback)
		{
			using (TaskList taskList = new TaskList())
			{
				foreach (StoreDatabase storeDatabase in Storage.GetDatabaseListSnapshot())
				{
					SingleExecutionTask<StoreDatabase>.CreateSingleExecutionTask(taskList, TaskExecutionWrapper<StoreDatabase>.WrapExecute(new TaskDiagnosticInformation(taskTypeId, ClientType.System, storeDatabase.MdbGuid), enumCallback), storeDatabase, true);
				}
				taskList.WaitAndShutdown();
			}
		}

		// Token: 0x040006BF RID: 1727
		private static bool exiting;

		// Token: 0x040006C0 RID: 1728
		private static TaskList taskList;

		// Token: 0x040006C1 RID: 1729
		private static LockFreeDictionary<Guid, StoreDatabase> databases = new LockFreeDictionary<Guid, StoreDatabase>();

		// Token: 0x0200013A RID: 314
		// (Invoke) Token: 0x06000C02 RID: 3074
		public delegate void DatabaseEnumerationCallback(Context context, StoreDatabase database, Func<bool> shouldCallbackContinue);
	}
}
