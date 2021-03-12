using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000018 RID: 24
	public abstract class Connection : DisposableBase, IExecutionContext, IConnectionProvider
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		protected Connection(IDatabaseExecutionContext outerExecutionContext, Database database, string identification)
		{
			this.outerExecutionContext = outerExecutionContext;
			this.database = database;
			this.identification = identification;
			this.owningThread = Thread.CurrentThread;
			Factory.GetDatabaseThreadStats(out this.lastCapturedThreadStats);
			this.connectionStatistics = this.RecordOperation<DatabaseConnectionStatistics>(NullTrackableOperation.Instance);
			if (this.connectionStatistics == null)
			{
				this.connectionStatistics = new DatabaseConnectionStatistics();
			}
			this.connectionStatistics.Count++;
			if (ExTraceGlobals.DirtyObjectsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.dirtyObjects = new List<DataRow>(20);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000CBDB File Offset: 0x0000ADDB
		public int NumberOfDirtyObjects
		{
			get
			{
				return this.numberOfDirtyObjects;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C2 RID: 194
		public abstract bool TransactionStarted { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000CBE3 File Offset: 0x0000ADE3
		public long TransactionTimeStamp
		{
			get
			{
				return this.transactionTimeStamp;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C4 RID: 196
		public abstract int TransactionId { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000CBEB File Offset: 0x0000ADEB
		public Database Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000CBF3 File Offset: 0x0000ADF3
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000CBFB File Offset: 0x0000ADFB
		public bool IsValid
		{
			get
			{
				return this.valid;
			}
			protected set
			{
				this.valid = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000CC04 File Offset: 0x0000AE04
		public IDatabaseExecutionContext OuterExecutionContext
		{
			get
			{
				return this.outerExecutionContext;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000CC14 File Offset: 0x0000AE14
		public bool NonFatalDuplicateKey { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000CC1D File Offset: 0x0000AE1D
		internal RowStats RowStats
		{
			get
			{
				return this.connectionStatistics.RowStats;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000CC2A File Offset: 0x0000AE2A
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000CC32 File Offset: 0x0000AE32
		internal Thread OwningThread
		{
			get
			{
				return this.owningThread;
			}
			set
			{
				this.owningThread = value;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000CC3B File Offset: 0x0000AE3B
		public static void Initialize()
		{
			Connection.databaseOperationTimeoutDefinition = new ThreadManager.TimeoutDefinition(ConfigurationSchema.DatabaseOperationTimeout.Value, new Action<ThreadManager.ThreadInfo>(Connection.CrashOnTimeout));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000CC5D File Offset: 0x0000AE5D
		public Connection GetConnection()
		{
			return this;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000CC60 File Offset: 0x0000AE60
		private static void CrashOnTimeout(ThreadManager.ThreadInfo threadInfo)
		{
			throw new InvalidOperationException(string.Format("Possible hang detected. Operation: {0}. Client: {2}. MailboxGuid: {3}", threadInfo.MethodName, threadInfo.Client, threadInfo.MailboxGuid));
		}

		// Token: 0x060000D1 RID: 209
		protected abstract void OnCommit(byte[] logTransactionInformation);

		// Token: 0x060000D2 RID: 210 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public void BeginTransactionIfNeeded()
		{
			this.BeginTransactionIfNeeded(Connection.TransactionOption.NeedTransaction);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000CC91 File Offset: 0x0000AE91
		public void OnBeforeTableAccess(Connection.OperationType operationType, Table table, IList<object> partitionValues)
		{
			if (this.outerExecutionContext != null)
			{
				this.outerExecutionContext.OnBeforeTableAccess(operationType, table, partitionValues);
			}
		}

		// Token: 0x060000D4 RID: 212
		internal abstract void BeginTransactionIfNeeded(Connection.TransactionOption transactionOption);

		// Token: 0x060000D5 RID: 213 RVA: 0x0000CCA9 File Offset: 0x0000AEA9
		public void Commit()
		{
			this.Commit(null);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public void Commit(byte[] logTransactionInformation)
		{
			if (ExTraceGlobals.DbInteractionSummaryTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					stringBuilder.Append("cn:[");
					stringBuilder.Append(this.GetHashCode());
					stringBuilder.Append("] ");
				}
				stringBuilder.Append("Commit Transaction");
				ExTraceGlobals.DbInteractionSummaryTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			if (this.numberOfDirtyObjects != 0)
			{
				if (this.dirtyObjects != null)
				{
					foreach (DataRow dataRow in this.dirtyObjects)
					{
						StringBuilder stringBuilder2 = new StringBuilder(200);
						stringBuilder2.Append("Connection:Commit(): This data row has not been saved: ");
						dataRow.AppendDirtyTrackingInfoToString(stringBuilder2);
						ExTraceGlobals.DirtyObjectsTracer.TraceDebug(0L, stringBuilder2.ToString());
					}
				}
				Globals.AssertRetail(false, "Not all dirty objects have been saved!");
			}
			this.OnCommit(logTransactionInformation);
			if (!this.SkipDatabaseLogsFlush && ConfigurationSchema.LazyTransactionCommitTimeout.Value == TimeSpan.Zero)
			{
				this.hasCommittedDataRequiringFlush = true;
			}
			this.transactionTimeStamp = Interlocked.Increment(ref Connection.currentTransactionTimeStamp);
		}

		// Token: 0x060000D7 RID: 215
		protected abstract void OnAbort(byte[] logTransactionInformation);

		// Token: 0x060000D8 RID: 216 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		public void Abort()
		{
			this.Abort(null);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		public void Abort(byte[] logTransactionInformation)
		{
			if (ExTraceGlobals.DbInteractionSummaryTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					stringBuilder.Append("cn:[");
					stringBuilder.Append(this.GetHashCode());
					stringBuilder.Append("] ");
				}
				stringBuilder.Append("Abort Transaction");
				ExTraceGlobals.DbInteractionSummaryTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			this.OnAbort(logTransactionInformation);
			this.transactionTimeStamp = Interlocked.Increment(ref Connection.currentTransactionTimeStamp);
		}

		// Token: 0x060000DA RID: 218
		public abstract void FlushDatabaseLogs(bool force);

		// Token: 0x060000DB RID: 219 RVA: 0x0000CE84 File Offset: 0x0000B084
		public TOperationData RecordOperation<TOperationData>(IOperationExecutionTrackable operation) where TOperationData : class, IExecutionTrackingData<TOperationData>, new()
		{
			if (operation == null)
			{
				return default(TOperationData);
			}
			if (this.outerExecutionContext != null)
			{
				return this.outerExecutionContext.RecordOperation<TOperationData>(operation);
			}
			return default(TOperationData);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		public void OnExceptionCatch(Exception exception)
		{
			this.Diagnostics.OnExceptionCatch(exception);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000CECA File Offset: 0x0000B0CA
		public void OnDatabaseFailure(bool isCriticalFailure, LID lid)
		{
			if (this.outerExecutionContext != null)
			{
				this.outerExecutionContext.OnDatabaseFailure(isCriticalFailure, lid);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000CEE1 File Offset: 0x0000B0E1
		public IExecutionDiagnostics Diagnostics
		{
			get
			{
				if (this.outerExecutionContext != null)
				{
					return this.outerExecutionContext.Diagnostics;
				}
				return NullExecutionDiagnostics.Instance;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public bool IsMailboxOperationStarted
		{
			get
			{
				return this.outerExecutionContext != null && this.outerExecutionContext.IsMailboxOperationStarted;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000CF13 File Offset: 0x0000B113
		public bool SkipDatabaseLogsFlush
		{
			get
			{
				return this.outerExecutionContext != null && this.outerExecutionContext.SkipDatabaseLogsFlush;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000CF2C File Offset: 0x0000B12C
		internal void CountStatement(Connection.OperationType operationType)
		{
			if (this.database != null && this.database.PerfInstance != null)
			{
				switch (operationType)
				{
				case Connection.OperationType.Query:
					this.database.PerfInstance.NumberOfQueriesPerSec.Increment();
					return;
				case Connection.OperationType.Insert:
					this.database.PerfInstance.NumberOfInsertsPerSec.Increment();
					return;
				case Connection.OperationType.Update:
					this.database.PerfInstance.NumberOfUpdatesPerSec.Increment();
					return;
				case Connection.OperationType.Delete:
					this.database.PerfInstance.NumberOfDeletesPerSec.Increment();
					return;
				case Connection.OperationType.CreateTable:
				case Connection.OperationType.DeleteTable:
				case Connection.OperationType.Other:
					this.database.PerfInstance.NumberOfOthersPerSec.Increment();
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000CFED File Offset: 0x0000B1ED
		public void ReleaseThread()
		{
			this.owningThread = null;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000CFF6 File Offset: 0x0000B1F6
		public void ForceValid()
		{
			this.IsValid = true;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000CFFF File Offset: 0x0000B1FF
		internal void AddDirtyObject(DataRow dataRow)
		{
			if (this.dirtyObjects != null)
			{
				this.dirtyObjects.Add(dataRow);
			}
			this.numberOfDirtyObjects++;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000D023 File Offset: 0x0000B223
		internal void CleanDirtyObject(DataRow dataRow)
		{
			if (this.dirtyObjects != null)
			{
				this.dirtyObjects.Remove(dataRow);
			}
			this.numberOfDirtyObjects--;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000D048 File Offset: 0x0000B248
		internal void Suspend()
		{
			this.CaptureStatisticsChangeForOperation();
			if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("cn:[");
				stringBuilder.Append(this.GetHashCode());
				stringBuilder.Append("] ");
				stringBuilder.Append("Connection Suspended");
				ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		internal void Resume()
		{
			JET_THREADSTATS jet_THREADSTATS;
			Factory.GetDatabaseThreadStats(out jet_THREADSTATS);
			this.lastCapturedThreadStats = jet_THREADSTATS;
			if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("cn:[");
				stringBuilder.Append(this.GetHashCode());
				stringBuilder.Append("] ");
				stringBuilder.Append("Connection Resumed");
				ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000D127 File Offset: 0x0000B327
		internal void DumpRowStats()
		{
			this.connectionStatistics.DumpStatistics(this.database);
			this.dumpedTimeInDatabase = this.connectionStatistics.TimeInDatabase;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000D14B File Offset: 0x0000B34B
		internal Connection.DatabaseExecutionTrackingFrame TrackDbOperationExecution(DataAccessOperator operation)
		{
			return new Connection.DatabaseExecutionTrackingFrame(this, operation);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000D154 File Offset: 0x0000B354
		internal Connection.TimeInDatabaseTrackingFrame TrackTimeInDatabase()
		{
			return new Connection.TimeInDatabaseTrackingFrame(this);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000D15C File Offset: 0x0000B35C
		[Conditional("DEBUG")]
		internal void AssertTrackingTimeInDatabase()
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000D160 File Offset: 0x0000B360
		internal DatabaseOperationStatistics SetCurrentOperationStatisticsObject(DatabaseOperationStatistics newOperationData)
		{
			if (object.ReferenceEquals(this.currentOperationStatistics, newOperationData))
			{
				return newOperationData;
			}
			this.CaptureStatisticsChangeForOperation();
			DatabaseOperationStatistics result = this.currentOperationStatistics;
			this.currentOperationStatistics = newOperationData;
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000D194 File Offset: 0x0000B394
		public void IncrementRowStatsCounter(Table table, RowStatsCounterType counterIndex)
		{
			TableClass tableClass = (table != null) ? table.TableClass : TableClass.Temp;
			this.connectionStatistics.RowStats.IncrementCount(tableClass, counterIndex);
			if (this.currentOperationStatistics != null)
			{
				this.currentOperationStatistics.SmallRowStats.IncrementCount(tableClass, counterIndex);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		public void AddRowStatsCounter(Table table, RowStatsCounterType counterIndex, int count)
		{
			TableClass tableClass = (table != null) ? table.TableClass : TableClass.Temp;
			this.connectionStatistics.RowStats.AddCount(tableClass, counterIndex, count);
			if (this.currentOperationStatistics != null)
			{
				this.currentOperationStatistics.SmallRowStats.AddCount(tableClass, counterIndex, count);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000D230 File Offset: 0x0000B430
		private void AddTimeInDatabase(TimeSpan time)
		{
			this.timeInDatabaseChanged = true;
			if (time.Ticks == 0L)
			{
				time = TimeSpan.FromTicks(1L);
			}
			this.connectionStatistics.TimeInDatabase += time;
			if (this.connectionStatistics.TimeInDatabase - this.dumpedTimeInDatabase > Connection.statisticsDumpInterval)
			{
				this.DumpRowStats();
			}
			if (this.currentOperationStatistics != null)
			{
				this.currentOperationStatistics.TimeInDatabase += time;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000D2B6 File Offset: 0x0000B4B6
		public void IncrementOffPageBlobHits()
		{
			this.connectionStatistics.OffPageBlobHits++;
			if (this.currentOperationStatistics != null)
			{
				this.currentOperationStatistics.OffPageBlobHits++;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000D2E6 File Offset: 0x0000B4E6
		internal void CaptureStatisticsChangeForOperation()
		{
			if (this.timeInDatabaseChanged)
			{
				this.DoCaptureStatisticsChangeForOperation();
				this.timeInDatabaseChanged = false;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000D300 File Offset: 0x0000B500
		private void DoCaptureStatisticsChangeForOperation()
		{
			JET_THREADSTATS jet_THREADSTATS;
			Factory.GetDatabaseThreadStats(out jet_THREADSTATS);
			JET_THREADSTATS jet_THREADSTATS2 = jet_THREADSTATS - this.lastCapturedThreadStats;
			this.connectionStatistics.ThreadStats += jet_THREADSTATS2;
			if (this.currentOperationStatistics != null)
			{
				this.currentOperationStatistics.ThreadStats += jet_THREADSTATS2;
			}
			this.lastCapturedThreadStats = jet_THREADSTATS;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000D360 File Offset: 0x0000B560
		internal DatabaseOperationStatistics RecordOperationImpl(DataAccessOperator operation)
		{
			DatabaseOperationStatistics databaseOperationStatistics = this.RecordOperation<DatabaseOperationStatistics>(operation);
			if (databaseOperationStatistics != null)
			{
				databaseOperationStatistics.Count++;
				if (databaseOperationStatistics.Planner == null)
				{
					databaseOperationStatistics.Planner = operation.GetExecutionPlanner();
				}
			}
			return databaseOperationStatistics;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000D39B File Offset: 0x0000B59B
		protected virtual void Close()
		{
			this.DumpRowStats();
		}

		// Token: 0x04000077 RID: 119
		private static readonly TimeSpan statisticsDumpInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000078 RID: 120
		protected static ThreadManager.TimeoutDefinition databaseOperationTimeoutDefinition;

		// Token: 0x04000079 RID: 121
		private Database database;

		// Token: 0x0400007A RID: 122
		private string identification;

		// Token: 0x0400007B RID: 123
		private Thread owningThread;

		// Token: 0x0400007C RID: 124
		private bool valid;

		// Token: 0x0400007D RID: 125
		private int numberOfDirtyObjects;

		// Token: 0x0400007E RID: 126
		private List<DataRow> dirtyObjects;

		// Token: 0x0400007F RID: 127
		private IDatabaseExecutionContext outerExecutionContext;

		// Token: 0x04000080 RID: 128
		private DatabaseConnectionStatistics connectionStatistics;

		// Token: 0x04000081 RID: 129
		private DatabaseOperationStatistics currentOperationStatistics;

		// Token: 0x04000082 RID: 130
		private JET_THREADSTATS lastCapturedThreadStats;

		// Token: 0x04000083 RID: 131
		private bool timeInDatabaseChanged;

		// Token: 0x04000084 RID: 132
		private TimeSpan dumpedTimeInDatabase;

		// Token: 0x04000085 RID: 133
		protected bool hasCommittedDataRequiringFlush;

		// Token: 0x04000086 RID: 134
		protected long transactionTimeStamp;

		// Token: 0x04000087 RID: 135
		protected static long currentTransactionTimeStamp;

		// Token: 0x04000088 RID: 136
		private bool trackingTimeInDatabase;

		// Token: 0x02000019 RID: 25
		public enum OperationType
		{
			// Token: 0x0400008B RID: 139
			Query = 1,
			// Token: 0x0400008C RID: 140
			Insert,
			// Token: 0x0400008D RID: 141
			Update,
			// Token: 0x0400008E RID: 142
			Delete,
			// Token: 0x0400008F RID: 143
			CreateTable,
			// Token: 0x04000090 RID: 144
			DeleteTable,
			// Token: 0x04000091 RID: 145
			Other
		}

		// Token: 0x0200001A RID: 26
		public enum TransactionOption
		{
			// Token: 0x04000093 RID: 147
			NeedTransaction = 1,
			// Token: 0x04000094 RID: 148
			DontNeedTransaction,
			// Token: 0x04000095 RID: 149
			NoTransaction
		}

		// Token: 0x0200001B RID: 27
		internal struct DatabaseExecutionTrackingFrame : IDisposable
		{
			// Token: 0x060000F6 RID: 246 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
			internal DatabaseExecutionTrackingFrame(Connection connection, DataAccessOperator operation)
			{
				this.connection = connection;
				DatabaseOperationStatistics currentOperationStatisticsObject = this.connection.RecordOperationImpl(operation);
				this.savedOperationData = this.connection.SetCurrentOperationStatisticsObject(currentOperationStatisticsObject);
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x0000D3EB File Offset: 0x0000B5EB
			public void Dispose()
			{
				this.connection.SetCurrentOperationStatisticsObject(this.savedOperationData);
			}

			// Token: 0x04000096 RID: 150
			private readonly Connection connection;

			// Token: 0x04000097 RID: 151
			private DatabaseOperationStatistics savedOperationData;
		}

		// Token: 0x0200001C RID: 28
		internal struct TimeInDatabaseTrackingFrame : IDisposable
		{
			// Token: 0x060000F8 RID: 248 RVA: 0x0000D3FF File Offset: 0x0000B5FF
			internal TimeInDatabaseTrackingFrame(Connection connection)
			{
				if (connection.trackingTimeInDatabase)
				{
					this.connection = null;
					this.startTimeStamp = default(StopwatchStamp);
					return;
				}
				this.connection = connection;
				this.startTimeStamp = StopwatchStamp.GetStamp();
				connection.trackingTimeInDatabase = true;
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x0000D436 File Offset: 0x0000B636
			public void Dispose()
			{
				if (this.connection != null)
				{
					this.connection.AddTimeInDatabase(this.startTimeStamp.ElapsedTime);
					this.connection.trackingTimeInDatabase = false;
				}
			}

			// Token: 0x04000098 RID: 152
			private readonly Connection connection;

			// Token: 0x04000099 RID: 153
			private StopwatchStamp startTimeStamp;
		}
	}
}
