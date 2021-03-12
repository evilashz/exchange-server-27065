using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000E9 RID: 233
	internal sealed class SqlConnection : Connection
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x00030CE4 File Offset: 0x0002EEE4
		public SqlConnection(SqlDatabase database, string identification) : this(null, database, identification)
		{
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00030CF0 File Offset: 0x0002EEF0
		public SqlConnection(IDatabaseExecutionContext outerExecutionContext, SqlDatabase database, string identification) : base(outerExecutionContext, database, identification)
		{
			string text = (database == null) ? DatabaseLocation.GetMasterConnectionString() : database.ConnectionString;
			try
			{
				this.sqlConnection = SqlClientFactory.CreateSqlConnection(text);
				base.IsValid = true;
			}
			catch (SqlException ex)
			{
				base.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("connection", text, ex);
				throw this.ProcessSqlError(ex);
			}
			finally
			{
				if (!base.IsValid)
				{
					base.Dispose();
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00030D78 File Offset: 0x0002EF78
		public override bool TransactionStarted
		{
			get
			{
				return this.transaction != null;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00030D86 File Offset: 0x0002EF86
		public override int TransactionId
		{
			get
			{
				if (this.transaction != null)
				{
					return this.transaction.GetHashCode();
				}
				return 0;
			}
		}

		// Token: 0x1700022F RID: 559
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x00030D9D File Offset: 0x0002EF9D
		public bool HasDeadlocked
		{
			set
			{
				this.hasDeadlocked = true;
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00030DA6 File Offset: 0x0002EFA6
		public override void FlushDatabaseLogs(bool force)
		{
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00030DA8 File Offset: 0x0002EFA8
		internal static void LogSQLError(string operation, string sqlCommandText, Exception e)
		{
			string message = string.Format("{0}:Exception. Message:[{1}] Stack:[{2}] Command:[{3}]", new object[]
			{
				operation,
				e.Message,
				e.StackTrace,
				sqlCommandText
			});
			Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_SQLExceptionDetected, new object[]
			{
				e.Message,
				e.StackTrace,
				sqlCommandText
			});
			ExTraceGlobals.DbInteractionSummaryTracer.TraceError(0L, message);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00030E18 File Offset: 0x0002F018
		internal Reader ExecuteReader(ISqlCommand command, int numberOfStatements, Connection.TransactionOption transactionOption, int skipTo, SimpleQueryOperator simpleQueryOperator, bool disposeQueryOperator)
		{
			Reader result;
			try
			{
				command = this.BuildCommand(command, numberOfStatements, transactionOption, SqlConnection.SqlOperationType.ExecuteReader);
				using (base.TrackTimeInDatabase())
				{
					result = new SqlReader(command.ExecuteReader(), this, skipTo, simpleQueryOperator, disposeQueryOperator);
				}
			}
			catch (SqlException ex)
			{
				base.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("ExecuteReader", command.CommandText, ex);
				throw this.ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00030E9C File Offset: 0x0002F09C
		internal int ExecuteNonQuery(ISqlCommand command, int numberOfStatements, Connection.TransactionOption transactionOption)
		{
			int result;
			try
			{
				command = this.BuildCommand(command, numberOfStatements, transactionOption, SqlConnection.SqlOperationType.ExecuteNonQuery);
				int num;
				using (base.TrackTimeInDatabase())
				{
					num = command.ExecuteNonQuery();
				}
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(25);
					stringBuilder.Append("Rows affected: [");
					stringBuilder.Append(num);
					stringBuilder.Append("]");
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				result = num;
			}
			catch (SqlException ex)
			{
				base.OnExceptionCatch(ex);
				base.IsValid = false;
				SqlConnection.LogSQLError("ExecuteNonQuery", command.CommandText, ex);
				throw this.ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00030F64 File Offset: 0x0002F164
		internal object ExecuteScalar(ISqlCommand command, int numberOfStatements, Connection.TransactionOption transactionOption)
		{
			object obj = null;
			try
			{
				command = this.BuildCommand(command, numberOfStatements, transactionOption, SqlConnection.SqlOperationType.ExecuteScalar);
				if (ExTraceGlobals.DbIOTracer.IsTraceEnabled(TraceType.PerformanceTrace))
				{
					using (base.TrackTimeInDatabase())
					{
						using (SqlReader sqlReader = new SqlReader(command.ExecuteReader(), this, 0, null, false))
						{
							if (!sqlReader.Read())
							{
								obj = null;
							}
							else
							{
								obj = sqlReader.GetValueByOrdinal(0);
							}
						}
						goto IL_91;
					}
				}
				using (base.TrackTimeInDatabase())
				{
					obj = command.ExecuteScalar();
				}
				if (obj is DBNull || obj == null)
				{
					obj = null;
				}
				IL_91:;
			}
			catch (SqlException ex)
			{
				base.OnExceptionCatch(ex);
				base.IsValid = false;
				SqlConnection.LogSQLError("ExecuteScalar", command.CommandText, ex);
				throw this.ProcessSqlError(ex);
			}
			if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("Read Scalar: value:[");
				stringBuilder.AppendAsString(obj);
				stringBuilder.Append("]");
				ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			return obj;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000310AC File Offset: 0x0002F2AC
		internal void ClearPool()
		{
			this.sqlConnection.ClearPool();
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000310BC File Offset: 0x0002F2BC
		internal ISqlCommand CreateSqlCommand()
		{
			ISqlCommand sqlCommand = this.sqlConnection.CreateCommand();
			sqlCommand.CommandTimeout = 6000;
			return sqlCommand;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000310E4 File Offset: 0x0002F2E4
		internal Exception ProcessSqlError(SqlException e)
		{
			Exception ex = e;
			int number = e.Number;
			bool flag;
			string message;
			if (number <= 1205)
			{
				if (number != 208)
				{
					if (number != 1205)
					{
						goto IL_7C;
					}
					flag = false;
					message = "Database deadlock";
					this.HasDeadlocked = true;
					goto IL_84;
				}
			}
			else
			{
				if (number == 1222)
				{
					flag = false;
					message = "Database lock timeout";
					goto IL_84;
				}
				if (number != 20507 && number != 25819)
				{
					goto IL_7C;
				}
			}
			flag = true;
			message = "Database schema broken";
			ex = new DatabaseSchemaBroken(base.Database.DisplayName, e.Message, e);
			goto IL_84;
			IL_7C:
			flag = true;
			message = string.Empty;
			IL_84:
			ExTraceGlobals.DbInteractionDetailTracer.TraceError<string, string>(0L, "SQLException. Message is: {0} Stack trace is: {1}", ex.Message, ex.StackTrace);
			if (flag)
			{
				return ex;
			}
			return new NonFatalDatabaseException(message);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000311A0 File Offset: 0x0002F3A0
		protected override void OnCommit(byte[] logTransactionInformation)
		{
			try
			{
				if (this.transaction == null)
				{
					if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Transaction was not started - commit is a no-op");
					}
				}
				else if (!this.hasDeadlocked)
				{
					try
					{
						using (base.TrackTimeInDatabase())
						{
							this.transaction.Commit();
						}
					}
					catch (SqlException ex)
					{
						base.OnExceptionCatch(ex);
						SqlConnection.LogSQLError("Flush", "commit", ex);
						throw this.ProcessSqlError(ex);
					}
				}
			}
			finally
			{
				if (this.transaction != null)
				{
					this.transaction.Dispose();
					this.transaction = null;
				}
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00031268 File Offset: 0x0002F468
		protected override void OnAbort(byte[] logTransactionInformation)
		{
			if (this.transaction != null)
			{
				try
				{
					if (!this.hasDeadlocked && this.transaction != null)
					{
						try
						{
							using (base.TrackTimeInDatabase())
							{
								this.transaction.Rollback();
							}
						}
						catch (SqlException ex)
						{
							base.OnExceptionCatch(ex);
							base.IsValid = false;
							SqlConnection.LogSQLError("Abort", "Rollback", ex);
							throw this.ProcessSqlError(ex);
						}
					}
				}
				finally
				{
					this.transaction.Dispose();
					this.transaction = null;
				}
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00031318 File Offset: 0x0002F518
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlConnection>(this);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00031320 File Offset: 0x0002F520
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.sqlConnection != null && this.transaction != null && !this.hasDeadlocked)
				{
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Connection disposed without being committed - changes lost");
					try
					{
						base.Abort();
					}
					catch (Exception ex)
					{
						base.OnExceptionCatch(ex);
						SqlConnection.LogSQLError("Dispose", "Rollback", ex);
					}
				}
				this.Close();
				base.IsValid = false;
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("cn:[");
					stringBuilder.Append(this.GetHashCode());
					stringBuilder.Append("] ");
					stringBuilder.Append("Connection Disposed");
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
				}
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000313F4 File Offset: 0x0002F5F4
		protected override void Close()
		{
			if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("cn:[");
				stringBuilder.Append(this.GetHashCode());
				stringBuilder.Append("] ");
				stringBuilder.Append("Connection Closed");
				ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			if (this.transaction != null)
			{
				this.transaction.Dispose();
				this.transaction = null;
			}
			if (this.sqlConnection != null)
			{
				if (this.sqlConnection.State != ConnectionState.Closed)
				{
					this.sqlConnection.Close();
				}
				this.sqlConnection.Dispose();
				this.sqlConnection = null;
			}
			if (ExTraceGlobals.DbIOTracer.IsTraceEnabled(TraceType.PerformanceTrace) && this.ioStats != null)
			{
				foreach (TableLevelIOStats tableLevelIOStats in this.ioStats.IOStats.Values)
				{
					ExTraceGlobals.DbIOTracer.TraceDebug(0L, "Stats for table {0}. PhysicalReads:{1} LogicalReads:{2} ReadAhead:{3} LobPhysicalReads:{4} LobLogicalReads:{5} LobReadAhead:{6}", new object[]
					{
						tableLevelIOStats.TableName,
						tableLevelIOStats.PhysicalReads,
						tableLevelIOStats.LogicalReads,
						tableLevelIOStats.ReadAheads,
						tableLevelIOStats.LobPhysicalReads,
						tableLevelIOStats.LobLogicalReads,
						tableLevelIOStats.LobReadAheads
					});
				}
			}
			this.ioStats = null;
			base.Close();
			base.OwningThread = null;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00031598 File Offset: 0x0002F798
		private void SetupToCollectIO()
		{
			if (ExTraceGlobals.DbIOTracer.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (this.ioStats == null)
				{
					if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Connection: Set Statistics IO ON");
					}
					using (ISqlCommand sqlCommand = SqlClientFactory.CreateSqlCommand())
					{
						sqlCommand.CommandText = "set statistics IO on";
						sqlCommand.Connection = this.sqlConnection;
						sqlCommand.CommandType = CommandType.Text;
						sqlCommand.Transaction = this.transaction;
						sqlCommand.ExecuteNonQuery();
					}
					this.ioStats = new IOStatistics();
					this.sqlConnection.InfoMessage += this.InfoMessage;
					return;
				}
			}
			else if (this.ioStats != null)
			{
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Connection: Set Statistics IO OFF");
				}
				using (ISqlCommand sqlCommand2 = SqlClientFactory.CreateSqlCommand())
				{
					sqlCommand2.CommandText = "set statistics IO off";
					sqlCommand2.Connection = this.sqlConnection;
					sqlCommand2.CommandType = CommandType.Text;
					sqlCommand2.Transaction = this.transaction;
					sqlCommand2.ExecuteNonQuery();
				}
				this.ioStats = null;
				this.sqlConnection.InfoMessage -= this.InfoMessage;
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000316E8 File Offset: 0x0002F8E8
		private void Open()
		{
			if (this.sqlConnection.State != ConnectionState.Open)
			{
				this.sqlConnection.Open();
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("cn:[");
					stringBuilder.Append(this.GetHashCode());
					stringBuilder.Append("] ");
					stringBuilder.Append("Connection Opened");
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				base.OwningThread = Thread.CurrentThread;
				base.IsValid = true;
				this.hasDeadlocked = false;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00031780 File Offset: 0x0002F980
		private void BeginTransaction()
		{
			try
			{
				this.transactionTimeStamp = Interlocked.Increment(ref Connection.currentTransactionTimeStamp);
				this.transaction = this.sqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);
			}
			catch (SqlException ex)
			{
				base.OnExceptionCatch(ex);
				base.IsValid = false;
				SqlConnection.LogSQLError("BeginTransaction", "BeginTransaction", ex);
				throw this.ProcessSqlError(ex);
			}
			if (ExTraceGlobals.DbInteractionSummaryTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					stringBuilder.Append("cn:[");
					stringBuilder.Append(this.GetHashCode());
					stringBuilder.Append("] ");
				}
				stringBuilder.Append("Begin Transaction");
				ExTraceGlobals.DbInteractionSummaryTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00031854 File Offset: 0x0002FA54
		internal override void BeginTransactionIfNeeded(Connection.TransactionOption transactionOption)
		{
			this.Open();
			if (this.transaction == null && transactionOption == Connection.TransactionOption.NeedTransaction)
			{
				this.BeginTransaction();
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Connection Created");
				}
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0003188C File Offset: 0x0002FA8C
		private void InfoMessage(object sender, SqlInfoMessageEventArgs e)
		{
			for (int i = 0; i < e.Errors.Count; i++)
			{
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Connection: InfoMessage:" + e.Errors[i].Message);
				}
				if (e.Errors[i].Number == 3615)
				{
					this.ioStats.AddStats(e.Errors[i].Message);
				}
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00031917 File Offset: 0x0002FB17
		private ISqlCommand BuildCommand(ISqlCommand command, int numberOfStatements, Connection.TransactionOption transactionOption, SqlConnection.SqlOperationType sqlOperationType)
		{
			this.BeginTransactionIfNeeded(transactionOption);
			this.SetupToCollectIO();
			command.Transaction = this.transaction;
			this.TraceSQLBatch(sqlOperationType, command);
			return command;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0003193C File Offset: 0x0002FB3C
		private void TraceSQLBatch(SqlConnection.SqlOperationType sqlOperationType, ISqlCommand command)
		{
			if (ExTraceGlobals.DbInteractionSummaryTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("Connection.");
				stringBuilder.Append(sqlOperationType);
				stringBuilder.Append(": Command:[");
				stringBuilder.Append(command.CommandText);
				stringBuilder.Append("]");
				foreach (object obj in command.Parameters)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					stringBuilder.Append(" Param:[id=[");
					stringBuilder.Append(sqlParameter.ToString());
					stringBuilder.Append("]type=[");
					stringBuilder.Append(sqlParameter.SqlDbType.ToString());
					stringBuilder.Append("]value=[");
					if (sqlParameter.Value is byte[])
					{
						stringBuilder.AppendAsString((byte[])sqlParameter.Value);
					}
					else
					{
						stringBuilder.Append(sqlParameter.Value.ToString());
					}
					stringBuilder.Append("]]");
				}
				ExTraceGlobals.DbInteractionSummaryTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x04000355 RID: 853
		public const int MaxTimeout = 60000;

		// Token: 0x04000356 RID: 854
		private const int CommandTimeout = 6000;

		// Token: 0x04000357 RID: 855
		private ISqlConnection sqlConnection;

		// Token: 0x04000358 RID: 856
		private IOStatistics ioStats;

		// Token: 0x04000359 RID: 857
		private ISqlTransaction transaction;

		// Token: 0x0400035A RID: 858
		private bool hasDeadlocked;

		// Token: 0x020000EA RID: 234
		private enum SqlOperationType
		{
			// Token: 0x0400035C RID: 860
			ExecuteReader = 1,
			// Token: 0x0400035D RID: 861
			ExecuteScalar,
			// Token: 0x0400035E RID: 862
			ExecuteNonQuery
		}
	}
}
