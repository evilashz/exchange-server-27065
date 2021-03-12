using System;
using System.Data.SqlClient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PhysicalAccessSql;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000138 RID: 312
	public class CheckpointSmoother
	{
		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003CDD6 File Offset: 0x0003AFD6
		public CheckpointSmoother(StoreDatabase database, Func<bool> shouldCallbackContinue)
		{
			this.database = database;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0003CDE8 File Offset: 0x0003AFE8
		public void SmoothCheckpoint()
		{
			using (Connection connection = Factory.CreateConnection(null, this.database.PhysicalDatabase, "CheckpointSmoother"))
			{
				using (Microsoft.Exchange.Server.Storage.PhysicalAccessSql.SqlCommand sqlCommand = new Microsoft.Exchange.Server.Storage.PhysicalAccessSql.SqlCommand(connection, "CHECKPOINT 300", Connection.OperationType.Other))
				{
					sqlCommand.ExecuteScalar(Connection.TransactionOption.NoTransaction);
				}
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0003CE58 File Offset: 0x0003B058
		internal static void SmoothCheckpoint(Context context, StoreDatabase database, Func<bool> shouldCallbackContinue)
		{
			if (shouldCallbackContinue())
			{
				database.GetSharedLock(context.Diagnostics);
				try
				{
					if (database.IsOnlineActive)
					{
						CheckpointSmoother checkpointSmoother = new CheckpointSmoother(database, shouldCallbackContinue);
						checkpointSmoother.SmoothCheckpoint();
					}
					else if (ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.BadPlanDetectionTracer.TraceError<Guid>(0L, "Could not connect to database {0}.  Skipping checkpoint smoothing.", database.MdbGuid);
					}
				}
				catch (SqlException ex)
				{
					context.OnExceptionCatch(ex);
					if (ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.BadPlanDetectionTracer.TraceError<Type, string, string>(0L, "SqlException in checkpoint smoothing. Type:[{0}] Message:[{1}] StackTrace:[{2}]", ex.GetType(), ex.Message, ex.StackTrace);
					}
				}
				catch (StoreException ex2)
				{
					context.OnExceptionCatch(ex2);
					if (ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.BadPlanDetectionTracer.TraceError<Type, string, string>(0L, "Exception in checkpoint smoothing. Type:[{0}] Message:[{1}] StackTrace:[{2}]", ex2.GetType(), ex2.Message, ex2.StackTrace);
					}
				}
				finally
				{
					database.ReleaseSharedLock();
				}
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0003CF60 File Offset: 0x0003B160
		internal static void MountEventHandler(StoreDatabase database)
		{
			RecurringTask<StoreDatabase> task = new RecurringTask<StoreDatabase>(TaskExecutionWrapper<StoreDatabase>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.CheckpointSmoother, ClientType.System, database.MdbGuid), new TaskExecutionWrapper<StoreDatabase>.TaskCallback<Context>(CheckpointSmoother.SmoothCheckpoint)), database, CheckpointSmoother.checkpointInitialDelay, CheckpointSmoother.checkpointInterval, false);
			database.TaskList.Add(task, true);
		}

		// Token: 0x040006BC RID: 1724
		private static TimeSpan checkpointInitialDelay = TimeSpan.FromMinutes(5.0);

		// Token: 0x040006BD RID: 1725
		private static TimeSpan checkpointInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x040006BE RID: 1726
		private readonly StoreDatabase database;
	}
}
