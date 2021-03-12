using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000063 RID: 99
	public class ExecutionDiagnostics : IExecutionDiagnostics, ILockStatistics
	{
		// Token: 0x0600038D RID: 909 RVA: 0x00018F50 File Offset: 0x00017150
		public ExecutionDiagnostics()
		{
			this.executionStart = StopwatchStamp.GetStamp();
			this.chunkStatistics = ExecutionDiagnostics.ChunkStatisticsContainer.Create();
			this.operationStatistics = ExecutionDiagnostics.OperationStatisticsContainer.Create();
			this.rpcStatistics = ExecutionDiagnostics.RpcStatisticsContainer.Create();
			this.digestCollector = ResourceMonitorDigest.NullCollector;
			this.ropSummaryCollector = RopSummaryCollector.Null;
			this.instanceIdentifier = TimingContext.GetContextIdentifier();
			this.OnBeginChunk();
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00018FC9 File Offset: 0x000171C9
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00018FD1 File Offset: 0x000171D1
		public bool? DatabaseRepaired { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00018FDA File Offset: 0x000171DA
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00018FE2 File Offset: 0x000171E2
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
			set
			{
				this.mailboxGuid = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00018FEB File Offset: 0x000171EB
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00018FF3 File Offset: 0x000171F3
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
			set
			{
				this.mailboxNumber = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00018FFC File Offset: 0x000171FC
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00019004 File Offset: 0x00017204
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
			set
			{
				this.databaseGuid = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0001900D File Offset: 0x0001720D
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00019015 File Offset: 0x00017215
		public ExecutionDiagnostics.OperationSource OpSource
		{
			get
			{
				return this.operationSource;
			}
			internal set
			{
				this.operationSource = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0001901E File Offset: 0x0001721E
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00019026 File Offset: 0x00017226
		public int OpDetail
		{
			get
			{
				return this.operationDetail;
			}
			internal set
			{
				this.operationDetail = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0001902F File Offset: 0x0001722F
		public ClientType ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00019037 File Offset: 0x00017237
		public virtual byte OpNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001903A File Offset: 0x0001723A
		public Guid ClientActivityId
		{
			get
			{
				return this.clientActivityId;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00019042 File Offset: 0x00017242
		public string ClientComponentName
		{
			get
			{
				return this.clientComponentName;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001904A File Offset: 0x0001724A
		public string ClientProtocolName
		{
			get
			{
				return this.clientProtocolName;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00019052 File Offset: 0x00017252
		public string ClientActionString
		{
			get
			{
				return this.clientActionString;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001905A File Offset: 0x0001725A
		public uint ExpandedClientActionStringId
		{
			get
			{
				return this.expandedClientActionStringId;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00019062 File Offset: 0x00017262
		public bool SharedLock
		{
			get
			{
				return this.sharedLock;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0001906A File Offset: 0x0001726A
		public TestCaseId TestCaseId
		{
			get
			{
				return this.testCaseId;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00019074 File Offset: 0x00017274
		public ExecutionDiagnostics.IExecutionDiagnosticsStatistics OperationStatistics
		{
			get
			{
				if (this.operationStatistics.Count == 0U)
				{
					if (this.chunkStatistics.Started)
					{
						this.chunkStatistics.Stop(this.executionStart.ElapsedTime);
						this.TraceElapsed((LID)44988U);
					}
					return this.chunkStatistics;
				}
				return this.operationStatistics;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000190D0 File Offset: 0x000172D0
		public ExecutionDiagnostics.IExecutionDiagnosticsStatistics RpcStatistics
		{
			get
			{
				if (this.rpcStatistics.Count == 0U)
				{
					if (this.chunkStatistics.Started)
					{
						this.chunkStatistics.Stop(this.executionStart.ElapsedTime);
						this.TraceElapsed((LID)61372U);
					}
					return this.chunkStatistics;
				}
				return this.rpcStatistics;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001912A File Offset: 0x0001732A
		public RowStats RowStatistics
		{
			get
			{
				return this.OperationStatistics.DatabaseCollector.RowStats;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0001913C File Offset: 0x0001733C
		internal LogTransactionInformationCollector LogTransactionInformationCollector
		{
			get
			{
				if (this.logTransactionInformationCollector == null)
				{
					this.logTransactionInformationCollector = new LogTransactionInformationCollector();
				}
				return this.logTransactionInformationCollector;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00019157 File Offset: 0x00017357
		protected ExecutionDiagnostics.IExecutionDiagnosticsStatistics ChunkStatistics
		{
			get
			{
				return this.chunkStatistics;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001915F File Offset: 0x0001735F
		protected virtual bool HasClientActivityDataToLog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00019162 File Offset: 0x00017362
		protected IDigestCollector ActivityCollector
		{
			get
			{
				return this.digestCollector;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0001916A File Offset: 0x0001736A
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00019172 File Offset: 0x00017372
		protected IRopSummaryCollector SummaryCollector
		{
			get
			{
				return this.ropSummaryCollector;
			}
			set
			{
				this.ropSummaryCollector = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001917B File Offset: 0x0001737B
		internal StorePerClientTypePerformanceCountersInstance PerClientPerfInstance
		{
			get
			{
				return this.perClientPerfInstance;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00019184 File Offset: 0x00017384
		protected virtual bool HasDataToLog
		{
			get
			{
				return this.IsLongOperation || this.IsResourceIntensive || (this.chunkStatistics.DatabaseTracker.HasDataToLog && ConfigurationSchema.DiagnosticsThresholdDatabaseTime.Value <= this.chunkStatistics.DatabaseCollector.TotalTime) || (this.chunkStatistics.LockTracker.HasDataToLog && ConfigurationSchema.DiagnosticsThresholdLockTime.Value <= this.chunkStatistics.LockTotalTime) || (this.chunkStatistics.DirectoryTracker.HasDataToLog && (ConfigurationSchema.DiagnosticsThresholdDirectoryTime.Value <= this.chunkStatistics.DirectoryTracker.GetTotalTime() || ConfigurationSchema.DiagnosticsThresholdDirectoryCalls.Value <= this.chunkStatistics.DirectoryTracker.GetAggregatedOperationData().Count));
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00019268 File Offset: 0x00017468
		private bool IsLongOperation
		{
			get
			{
				return ConfigurationSchema.DiagnosticsThresholdInteractionTime.Value <= this.InteractionTotal || ConfigurationSchema.DiagnosticsThresholdChunkElapsedTime.Value <= this.chunkStatistics.ElapsedTime;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001929D File Offset: 0x0001749D
		public virtual TimeSpan InteractionTotal
		{
			get
			{
				return this.chunkStatistics.DatabaseCollector.TotalTime + this.chunkStatistics.LockTotalTime + this.chunkStatistics.DirectoryTotalTime;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000192D0 File Offset: 0x000174D0
		protected bool IsResourceIntensive
		{
			get
			{
				return ConfigurationSchema.DiagnosticsThresholdPagesPreread.Value <= this.chunkStatistics.DatabaseCollector.ThreadStats.cPagePreread || ConfigurationSchema.DiagnosticsThresholdPagesRead.Value <= this.chunkStatistics.DatabaseCollector.ThreadStats.cPageRead || ConfigurationSchema.DiagnosticsThresholdPagesDirtied.Value <= this.chunkStatistics.DatabaseCollector.ThreadStats.cPageDirtied;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00019348 File Offset: 0x00017548
		public string DiagnosticInformationForWatsonReport
		{
			get
			{
				TraceContentBuilder traceContentBuilder = TraceContentBuilder.Create();
				this.FormatCommonInformation(traceContentBuilder, 0, Guid.Empty);
				ExecutionDiagnostics.FormatLine(traceContentBuilder, 0, "Database Repaired?: " + ((this.DatabaseRepaired != null) ? this.DatabaseRepaired.ToString() : "<unknown>"));
				this.FormatDiagnosticInformation(traceContentBuilder, 0);
				this.FormatOperationInformation(traceContentBuilder, 0);
				this.FormatClientActivityDiagnosticInformation(traceContentBuilder, 0);
				IStoreSimpleQueryTarget<ThreadManager.ThreadDiagnosticInfo> instance = ThreadManager.Instance;
				ExecutionDiagnostics.FormatLine(traceContentBuilder, 0, "Threads:");
				foreach (ThreadManager.ThreadDiagnosticInfo threadDiagnosticInfo in instance.GetRows(null))
				{
					ExecutionDiagnostics.FormatLine(traceContentBuilder, 1, string.Format("Id={0}, Method={1}, Client={2}, Mailbox={3}, Status={4}, StartUtcTime={5}, Duration={6}", new object[]
					{
						threadDiagnosticInfo.NativeId,
						threadDiagnosticInfo.MethodName,
						threadDiagnosticInfo.Client,
						threadDiagnosticInfo.MailboxGuid,
						threadDiagnosticInfo.Status,
						threadDiagnosticInfo.StartUtcTime,
						threadDiagnosticInfo.Duration
					}));
				}
				return traceContentBuilder.ToString();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00019494 File Offset: 0x00017694
		public bool InMailboxOperationContext
		{
			get
			{
				return this.inMailboxOperationContext;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001949C File Offset: 0x0001769C
		public virtual uint TypeIdentifier
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001949F File Offset: 0x0001769F
		int IExecutionDiagnostics.MailboxNumber
		{
			get
			{
				return this.MailboxNumber;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000194A7 File Offset: 0x000176A7
		byte IExecutionDiagnostics.OperationId
		{
			get
			{
				return this.OpNumber;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000194AF File Offset: 0x000176AF
		byte IExecutionDiagnostics.OperationType
		{
			get
			{
				return (byte)this.OpSource;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000194B7 File Offset: 0x000176B7
		byte IExecutionDiagnostics.ClientType
		{
			get
			{
				return (byte)this.ClientType;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000194BF File Offset: 0x000176BF
		byte IExecutionDiagnostics.OperationFlags
		{
			get
			{
				return (byte)(this.OpDetail % 1000);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000194CE File Offset: 0x000176CE
		int IExecutionDiagnostics.CorrelationId
		{
			get
			{
				return (int)this.ExpandedClientActionStringId;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000194D6 File Offset: 0x000176D6
		public static IBinaryLogger GetLogger(LoggerType loggerType)
		{
			return LoggerManager.GetLogger(loggerType);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000194E0 File Offset: 0x000176E0
		public void SetClientActivityInfo(Guid activityId, string componentName, string protocolName, string actionString)
		{
			this.clientActivityId = activityId;
			this.clientComponentName = componentName;
			this.clientProtocolName = protocolName;
			this.clientActionString = actionString;
			string str = string.Format("{0}.{1}", componentName, actionString);
			this.expandedClientActionStringId = ClientActivityStrings.GetStringId(str);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00019524 File Offset: 0x00017724
		public void OnExceptionCatch(Exception exception)
		{
			this.OnExceptionCatch(exception, null);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00019530 File Offset: 0x00017730
		public void OnExceptionCatch(Exception exception, object diagnosticData)
		{
			ErrorHelper.OnExceptionCatch((byte)this.OpSource, this.OpNumber, (byte)this.ClientType, this.databaseGuid.GetHashCode(), this.MailboxNumber, exception, diagnosticData);
			int value = ConfigurationSchema.MaximumNumberOfExceptions.Value;
			if (value == 0 || (this.exceptionHistory != null && this.exceptionHistory.Count == value))
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Too many exceptions");
			}
			if (this.exceptionHistory == null)
			{
				this.exceptionHistory = new List<Exception>(value);
			}
			this.exceptionHistory.Add(exception);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000195BC File Offset: 0x000177BC
		public virtual void OnUnhandledException(Exception exception)
		{
			string arg;
			string text;
			string arg2;
			ErrorHelper.GetExceptionSummary(exception, out arg, out text, out arg2);
			this.TryPrequarantineMailbox(string.Format("{0}: {1}", arg, arg2));
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000195E7 File Offset: 0x000177E7
		public byte GetClientType()
		{
			return (byte)this.ClientType;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000195EF File Offset: 0x000177EF
		public byte GetOperation()
		{
			return this.OpNumber;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000195F8 File Offset: 0x000177F8
		public void OnAfterLockAcquisition(LockManager.LockType lockType, bool locked, bool contested, ILockStatistics owner, TimeSpan waited)
		{
			byte ownerClientType = (contested && owner != null) ? owner.GetClientType() : 0;
			byte ownerOperation = (contested && owner != null) ? owner.GetOperation() : 0;
			LockAcquisitionTracker lockAcquisitionTracker = LockAcquisitionTracker.Create(lockType, locked, contested, ownerClientType, ownerOperation, waited);
			LockAcquisitionTracker.Data data = this.RecordOperation<LockAcquisitionTracker.Data>(lockAcquisitionTracker);
			if (data != null)
			{
				data.Aggregate(lockAcquisitionTracker.Tracked);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00019653 File Offset: 0x00017853
		public void SetFastWaitTime(TimeSpan fastWaitTime)
		{
			this.chunkStatistics.FastWaitTime = fastWaitTime;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00019664 File Offset: 0x00017864
		public override int GetHashCode()
		{
			return this.ClientType.GetHashCode() ^ this.OpSource.GetHashCode() ^ this.chunkStatistics.LockTracker.GetHashCode() ^ this.chunkStatistics.DirectoryTracker.GetHashCode();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000196B4 File Offset: 0x000178B4
		public void UpdateClientType(ClientType clientType)
		{
			if (this.clientType == clientType)
			{
				return;
			}
			if (this.clientType != ClientType.MaxValue)
			{
				this.DisablePerClientTypePerfCounterUpdate();
			}
			this.clientType = clientType;
			if (this.clientType != ClientType.MaxValue)
			{
				this.EnablePerClientTypePerfCounterUpdate();
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000196E7 File Offset: 0x000178E7
		public void UpdateTestCaseId(TestCaseId testCaseId)
		{
			this.testCaseId = testCaseId;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000196F0 File Offset: 0x000178F0
		public virtual TOperationData RecordOperation<TOperationData>(IOperationExecutionTrackable operation) where TOperationData : class, IExecutionTrackingData<TOperationData>, new()
		{
			if (typeof(TOperationData) == typeof(DatabaseOperationStatistics))
			{
				return (TOperationData)((object)this.chunkStatistics.DatabaseTracker.RecordOperation(operation));
			}
			if (typeof(TOperationData) == typeof(DatabaseConnectionStatistics))
			{
				return (TOperationData)((object)this.chunkStatistics.DatabaseCollector);
			}
			if (typeof(TOperationData) == typeof(LockAcquisitionTracker.Data))
			{
				return (TOperationData)((object)this.chunkStatistics.LockTracker.RecordOperation(operation));
			}
			if (typeof(TOperationData) == typeof(ExecutionDiagnostics.DirectoryTrackingData))
			{
				return (TOperationData)((object)this.chunkStatistics.DirectoryTracker.RecordOperation(operation));
			}
			return default(TOperationData);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000197C8 File Offset: 0x000179C8
		public virtual void FormatCommonInformation(TraceContentBuilder cb, int indentLevel, Guid correlationId)
		{
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Correlation ID: " + correlationId.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Build Number: " + ExWatson.ApplicationVersion.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Database GUID: " + this.DatabaseGuid);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Database Hash: " + this.DatabaseGuid.GetHashCode().ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Mailbox GUID: " + this.MailboxGuid);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Mailbox Number: " + this.MailboxNumber.ToString());
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Operation source: " + this.OpSource);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Client Type: " + this.clientType);
			if (!string.IsNullOrEmpty(this.clientProtocolName))
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Client Protocol: " + this.clientProtocolName);
			}
			if (!string.IsNullOrEmpty(this.clientComponentName))
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Client Component: " + this.clientComponentName);
			}
			if (!string.IsNullOrEmpty(this.clientActionString))
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Client Action: " + this.clientActionString);
			}
			if (this.clientActivityId != Guid.Empty)
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Client Activity: " + this.clientActivityId);
			}
			if (this.testCaseId.IsNotNull)
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Test case id: " + this.testCaseId.ToString());
			}
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Hash Code: " + this.GetHashCode().ToString());
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000199B2 File Offset: 0x00017BB2
		internal void ResetLogTransactionInformationCollector()
		{
			this.logTransactionInformationCollector = null;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000199BC File Offset: 0x00017BBC
		internal virtual void OnStartMailboxOperation(Guid databaseGuid, int mailboxNumber, Guid mailboxGuid, ExecutionDiagnostics.OperationSource operationSource, IDigestCollector digestCollector, IRopSummaryCollector ropSummaryCollector, bool sharedLock)
		{
			this.mailboxGuid = mailboxGuid;
			this.mailboxNumber = mailboxNumber;
			this.databaseGuid = databaseGuid;
			this.operationSource = operationSource;
			this.digestCollector = ((digestCollector != null) ? digestCollector : ResourceMonitorDigest.NullCollector);
			this.ropSummaryCollector = ((ropSummaryCollector != null) ? ropSummaryCollector : RopSummaryCollector.Null);
			this.inMailboxOperationContext = true;
			this.sharedLock = sharedLock;
			this.OnBeginChunk();
			if (ExTraceGlobals.MailboxLockTracer.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				TraceContentBuilder traceContentBuilder = TraceContentBuilder.Create();
				traceContentBuilder.Append("Lock Mailbox:");
				ExecutionDiagnostics.FormatLine(traceContentBuilder, 0, "Common info:");
				this.FormatCommonInformation(traceContentBuilder, 1, Guid.Empty);
				ExTraceGlobals.MailboxLockTracer.TracePerformance(0L, traceContentBuilder.ToString());
			}
			this.diagnosticDumped = false;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00019A71 File Offset: 0x00017C71
		internal virtual void OnBeforeEndMailboxOperation()
		{
			this.OnEndChunk();
			this.DumpDiagnosticIfNeeded();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00019A80 File Offset: 0x00017C80
		internal virtual void OnAfterEndMailboxOperation()
		{
			ResourceDigestStats activity = new ResourceDigestStats(ref this.chunkStatistics.DatabaseCollector.ThreadStats);
			this.digestCollector.LogActivity(activity);
			if (ExTraceGlobals.MailboxLockTracer.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				TraceContentBuilder traceContentBuilder = TraceContentBuilder.Create();
				traceContentBuilder.Append("Unlock Mailbox: ");
				ExecutionDiagnostics.FormatLine(traceContentBuilder, 0, "Common info:");
				this.FormatCommonInformation(traceContentBuilder, 1, Guid.Empty);
				ExTraceGlobals.MailboxLockTracer.TracePerformance(0L, traceContentBuilder.ToString());
			}
			this.inMailboxOperationContext = false;
			this.OnBeginChunk();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00019B08 File Offset: 0x00017D08
		public void OnBeginChunk()
		{
			if (!this.chunkStatistics.Started)
			{
				this.chunkStatistics.Reset();
				this.chunkStatistics.Start(this.executionStart.ElapsedTime);
				this.TraceReset();
				this.TraceStart((LID)58848U);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00019B59 File Offset: 0x00017D59
		public void OnBeginMailboxTaskQueueChunk()
		{
			this.chunkStatistics.Reset();
			this.chunkStatistics.Start(this.executionStart.ElapsedTime);
			this.TraceReset();
			this.TraceStart((LID)53180U);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00019B92 File Offset: 0x00017D92
		public void OnBeginOperation()
		{
			this.operationStatistics.Reset();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00019B9F File Offset: 0x00017D9F
		public void OnBeginRpc()
		{
			this.rpcStatistics.Reset();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00019BAC File Offset: 0x00017DAC
		public void OnEndChunk()
		{
			if (this.chunkStatistics.Started)
			{
				this.chunkStatistics.Stop(this.executionStart.ElapsedTime);
				this.TraceElapsed((LID)62432U);
			}
			this.operationStatistics.Aggregate(this.chunkStatistics);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00019BFD File Offset: 0x00017DFD
		public void OnEndMailboxTaskQueueChunk()
		{
			this.OnEndChunk();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00019C08 File Offset: 0x00017E08
		public void OnEndOperation(OperationType operationType, uint activityid, byte operationId, uint errorCode, bool isNewActivity)
		{
			this.ropSummaryCollector.Add(new RopTraceKey(operationType, this.MailboxNumber, this.ClientType, activityid, operationId, (uint)this.OpDetail, this.SharedLock), new RopSummaryParameters(this.OperationStatistics.ElapsedTime, errorCode, isNewActivity, this.OperationStatistics.DatabaseCollector.ThreadStats, (uint)this.OperationStatistics.DirectoryCount, (uint)this.OperationStatistics.DatabaseCollector.OffPageBlobHits, this.OperationStatistics.CpuKernelTime, this.OperationStatistics.CpuUserTime, this.OperationStatistics.Count, this.OperationStatistics.MaximumChunkTime, this.OperationStatistics.LockTotalTime, this.OperationStatistics.DirectoryTotalTime, this.OperationStatistics.DatabaseCollector.TotalTime, this.OperationStatistics.FastWaitTime));
			this.rpcStatistics.Aggregate(this.operationStatistics);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00019CF0 File Offset: 0x00017EF0
		public void OnEndRpc(OperationType operationType, uint activityid, byte operationId, uint errorCode, bool isNewActivity)
		{
			this.ropSummaryCollector.Add(new RopTraceKey(operationType, this.MailboxNumber, this.ClientType, activityid, operationId, (uint)this.OpDetail, this.SharedLock), new RopSummaryParameters(this.RpcStatistics.ElapsedTime, errorCode, isNewActivity, this.RpcStatistics.DatabaseCollector.ThreadStats, (uint)this.RpcStatistics.DirectoryCount, (uint)this.RpcStatistics.DatabaseCollector.OffPageBlobHits, this.RpcStatistics.CpuKernelTime, this.RpcStatistics.CpuUserTime, this.RpcStatistics.Count, this.RpcStatistics.MaximumChunkTime, this.RpcStatistics.LockTotalTime, this.RpcStatistics.DirectoryTotalTime, this.RpcStatistics.DatabaseCollector.TotalTime, this.RpcStatistics.FastWaitTime));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00019DC4 File Offset: 0x00017FC4
		public void OnTransactionAbort()
		{
			ErrorHelper.AddBreadcrumb(BreadcrumbKind.Abort, (byte)this.OpSource, this.OpNumber, (byte)this.ClientType, this.databaseGuid.GetHashCode(), this.MailboxNumber, 0, null);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00019DF7 File Offset: 0x00017FF7
		public void TraceReset()
		{
			TimingContext.Reset();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00019DFE File Offset: 0x00017FFE
		public void TraceStart(LID lid)
		{
			TimingContext.TraceStart(lid, this.TypeIdentifier, this.instanceIdentifier);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00019E12 File Offset: 0x00018012
		public void TraceElapsed(LID lid)
		{
			TimingContext.TraceElapsed(lid, this.TypeIdentifier, this.instanceIdentifier);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00019E28 File Offset: 0x00018028
		internal void TryPrequarantineMailbox(string reason)
		{
			if (this.DatabaseGuid == Guid.Empty || this.MailboxGuid == Guid.Empty)
			{
				return;
			}
			MailboxQuarantineProvider.Instance.PrequarantineMailbox(this.DatabaseGuid, this.MailboxGuid, reason);
			Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MailboxPrequarantined, new object[]
			{
				this.MailboxGuid.ToString(),
				this.DatabaseGuid.ToString(),
				reason
			});
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00019EB5 File Offset: 0x000180B5
		internal void SetClientActionStringForTest(string clientAction)
		{
			this.clientActionString = clientAction;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00019EBE File Offset: 0x000180BE
		internal virtual void EnablePerClientTypePerfCounterUpdate()
		{
			this.perClientPerfInstance = PerformanceCounterFactory.GetClientTypeInstance(this.clientType);
			if (PerClientTypeTracing.IsConfigured && PerClientTypeTracing.IsEnabled(this.clientType))
			{
				PerClientTypeTracing.TurnOn();
				this.perClientTracingEnabled = true;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00019EF4 File Offset: 0x000180F4
		internal virtual void DisablePerClientTypePerfCounterUpdate()
		{
			if (this.perClientTracingEnabled)
			{
				this.perClientTracingEnabled = false;
				PerClientTypeTracing.TurnOff();
			}
			if (this.perClientPerfInstance != null)
			{
				this.perClientPerfInstance.JetPageReferencedRate.IncrementBy((long)this.OperationStatistics.DatabaseCollector.ThreadStats.cPageReferenced);
				this.perClientPerfInstance.JetPageReadRate.IncrementBy((long)this.OperationStatistics.DatabaseCollector.ThreadStats.cPageRead);
				this.perClientPerfInstance.JetPagePrereadRate.IncrementBy((long)this.OperationStatistics.DatabaseCollector.ThreadStats.cPagePreread);
				this.perClientPerfInstance.JetPageDirtiedRate.IncrementBy((long)this.OperationStatistics.DatabaseCollector.ThreadStats.cPageDirtied);
				this.perClientPerfInstance.JetPageReDirtiedRate.IncrementBy((long)this.OperationStatistics.DatabaseCollector.ThreadStats.cPageRedirtied);
				this.perClientPerfInstance.JetLogRecordRate.IncrementBy((long)this.OperationStatistics.DatabaseCollector.ThreadStats.cLogRecord);
				this.perClientPerfInstance.JetLogRecordBytesRate.IncrementBy((long)((ulong)this.OperationStatistics.DatabaseCollector.ThreadStats.cbLogRecord));
			}
			this.perClientPerfInstance = null;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001A038 File Offset: 0x00018238
		internal void ClearExceptionHistory()
		{
			if (this.exceptionHistory != null)
			{
				this.exceptionHistory.Clear();
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001A04D File Offset: 0x0001824D
		protected static void FormatLine(TraceContentBuilder cb, int indentLevel, string line)
		{
			cb.Indent(indentLevel);
			cb.AppendLine(line);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001A060 File Offset: 0x00018260
		protected static void FormatThresholdLine(TraceContentBuilder cb, int indentLevel, string label, long value, long threshold, string unit)
		{
			ExecutionDiagnostics.FormatLine(cb, indentLevel, string.Concat(new string[]
			{
				label,
				": ",
				value.ToString("N0", CultureInfo.InvariantCulture),
				" (",
				threshold.ToString("N0", CultureInfo.InvariantCulture),
				") ",
				unit
			}));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001A0F9 File Offset: 0x000182F9
		protected LockAcquisitionTracker.Data GetLockAcquisitionData(ExecutionDiagnostics.LockCategory category)
		{
			return this.chunkStatistics.LockTracker.GetAggregatedOperationData<ExecutionDiagnostics.LockCategory>(delegate(ExecutionDiagnostics.ExecutionTracker<LockAcquisitionTracker.Data>.ExecutionEntryKey key, ExecutionDiagnostics.LockCategory contextCategory)
			{
				LockAcquisitionTracker.Key key2 = key.TrackingKey as LockAcquisitionTracker.Key;
				return key2 != null && ExecutionDiagnostics.GetLockCategory(key2.LockType) == contextCategory;
			}, category);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001A12C File Offset: 0x0001832C
		protected void DumpDiagnosticIfNeeded()
		{
			if (this.chunkStatistics.Started)
			{
				this.chunkStatistics.Stop(this.executionStart.ElapsedTime);
				this.TraceElapsed((LID)36796U);
			}
			Guid correlationId = Guid.NewGuid();
			if (ExTraceGlobals.ExecutionDiagnosticsTracer.IsTraceEnabled(TraceType.DebugTrace) && this.chunkStatistics.DatabaseCollector.TimeInDatabase != TimeSpan.Zero)
			{
				ExTraceGlobals.ExecutionDiagnosticsTracer.TraceDebug(0L, this.GetDetailContent(correlationId).ToString());
			}
			IBinaryLogger logger = ExecutionDiagnostics.GetLogger(LoggerType.LongOperation);
			if (this.DumpDiagnosticIfNeeded(logger, LoggerManager.TraceGuids.LongOperationDetail, correlationId))
			{
				ExecutionDiagnostics.LongOperationSummary summaryContent = this.GetSummaryContent(correlationId);
				using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.LongOperationSummary, true, false, summaryContent.DatabaseGuid.ToString(), summaryContent.MailboxGuid.ToString(), summaryContent.ClientType, summaryContent.OperationSource, summaryContent.OperationType, summaryContent.OperationName, summaryContent.OperationDetail, summaryContent.ChunkElapsedTime, summaryContent.InteractionTotal, summaryContent.PagesPreread, summaryContent.PagesRead, summaryContent.PagesDirtied, summaryContent.LogBytesWritten, summaryContent.SortOrderCount, summaryContent.NumberOfPlansExecuted, summaryContent.PlansExecutionTime, summaryContent.NumberOfDirectoryOperations, summaryContent.DirectoryOperationsTime, summaryContent.NumberOfLocksAttempted, summaryContent.NumberOfLocksSucceeded, summaryContent.LocksWaitTime, summaryContent.IsLongOperation, summaryContent.IsResourceIntensive, summaryContent.IsContested, summaryContent.TimeInDatabase, summaryContent.ClientProtocol, summaryContent.ClientComponent, summaryContent.ClientAction, summaryContent.CorrelationId.ToString(), summaryContent.BuildNumber, summaryContent.TimeInCpuKernel, summaryContent.TimeInCpuUser, summaryContent.HashCode))
				{
					logger.TryWrite(traceBuffer);
				}
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001A310 File Offset: 0x00018510
		protected bool DumpClientActivityDiagnosticIfNeeded()
		{
			if (this.ClientActivityId == Guid.Empty)
			{
				return false;
			}
			if (!this.HasClientActivityDataToLog)
			{
				return false;
			}
			IBinaryLogger logger = ExecutionDiagnostics.GetLogger(LoggerType.HeavyClientActivity);
			if (logger == null || !logger.IsLoggingEnabled)
			{
				return false;
			}
			Guid correlationId = Guid.NewGuid();
			TraceContentBuilder traceContentBuilder = TraceContentBuilder.Create();
			traceContentBuilder.AppendLine();
			ExecutionDiagnostics.FormatLine(traceContentBuilder, 0, "Common Info:");
			this.FormatCommonInformation(traceContentBuilder, 1, correlationId);
			this.FormatClientActivityThresholdInformation(traceContentBuilder, 1);
			this.FormatClientActivityDiagnosticInformation(traceContentBuilder, 0);
			ExecutionDiagnostics.TruncateContent(traceContentBuilder);
			List<string> list = traceContentBuilder.ToWideString();
			for (int i = 0; i < list.Count; i++)
			{
				using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.HeavyClientActivityDetail, true, false, correlationId.ToString(), i, list.Count, list[i]))
				{
					ExecutionDiagnostics.WriteDataToLog(logger, traceBuffer);
				}
			}
			ExecutionDiagnostics.HeavyClientActivitySummary heavyClientActivitySummary = default(ExecutionDiagnostics.HeavyClientActivitySummary);
			this.GetClientActivitySummaryInformation(correlationId, ref heavyClientActivitySummary);
			using (TraceBuffer traceBuffer2 = TraceRecord.Create(LoggerManager.TraceGuids.HeavyClientActivitySummary, true, false, heavyClientActivitySummary.DatabaseGuid.ToString(), heavyClientActivitySummary.MailboxGuid.ToString(), heavyClientActivitySummary.ClientType, heavyClientActivitySummary.OperationSource, heavyClientActivitySummary.Activity, heavyClientActivitySummary.TotalRpcCalls, heavyClientActivitySummary.TotalRops, heavyClientActivitySummary.CorrelationId.ToString()))
			{
				logger.TryWrite(traceBuffer2);
			}
			return true;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001A498 File Offset: 0x00018698
		protected bool DumpDiagnosticIfNeeded(IBinaryLogger logger, Guid recordGuid, Guid correlationId)
		{
			if (logger == null || !logger.IsLoggingEnabled)
			{
				return false;
			}
			if (this.diagnosticDumped || !this.HasDataToLog)
			{
				return false;
			}
			TraceContentBuilder detailContent = this.GetDetailContent(correlationId);
			List<string> list = detailContent.ToWideString();
			for (int i = 0; i < list.Count; i++)
			{
				using (TraceBuffer traceBuffer = TraceRecord.Create(recordGuid, true, false, correlationId.ToString(), i, list.Count, list[i]))
				{
					ExecutionDiagnostics.WriteDataToLog(logger, traceBuffer);
					this.diagnosticDumped = true;
				}
			}
			return true;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001A534 File Offset: 0x00018734
		protected virtual void FormatClientActivityThresholdInformation(TraceContentBuilder cb, int indentLevel)
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001A538 File Offset: 0x00018738
		protected virtual void FormatThresholdInformation(TraceContentBuilder cb, int indentLevel)
		{
			long value = (long)this.chunkStatistics.DatabaseCollector.TotalTime.TotalMilliseconds;
			long value2 = (long)this.InteractionTotal.TotalMilliseconds;
			int cPagePreread = this.chunkStatistics.DatabaseCollector.ThreadStats.cPagePreread;
			int cPageRead = this.chunkStatistics.DatabaseCollector.ThreadStats.cPageRead;
			int cPageDirtied = this.chunkStatistics.DatabaseCollector.ThreadStats.cPageDirtied;
			long threshold = (long)ConfigurationSchema.DiagnosticsThresholdDatabaseTime.Value.TotalMilliseconds;
			long threshold2 = (long)ConfigurationSchema.DiagnosticsThresholdInteractionTime.Value.TotalMilliseconds;
			int value3 = ConfigurationSchema.DiagnosticsThresholdPagesPreread.Value;
			int value4 = ConfigurationSchema.DiagnosticsThresholdPagesRead.Value;
			int value5 = ConfigurationSchema.DiagnosticsThresholdPagesDirtied.Value;
			long value6 = (long)this.chunkStatistics.LockTracker.GetTotalTime().TotalMilliseconds;
			long threshold3 = (long)ConfigurationSchema.DiagnosticsThresholdLockTime.Value.TotalMilliseconds;
			long value7 = (long)this.chunkStatistics.DirectoryTracker.GetTotalTime().TotalMilliseconds;
			long threshold4 = (long)ConfigurationSchema.DiagnosticsThresholdDirectoryTime.Value.TotalMilliseconds;
			int count = this.chunkStatistics.DirectoryTracker.GetAggregatedOperationData().Count;
			int value8 = ConfigurationSchema.DiagnosticsThresholdDirectoryCalls.Value;
			long value9 = (long)this.chunkStatistics.ElapsedTime.TotalMilliseconds;
			long threshold5 = (long)ConfigurationSchema.DiagnosticsThresholdChunkElapsedTime.Value.TotalMilliseconds;
			ExecutionDiagnostics.FormatLine(cb, 0, "Diagnostic Thresholds:");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "AD Operations", (long)count, (long)value8, string.Empty);
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Total AD Interaction", value7, threshold4, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Total LK Interaction", value6, threshold3, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Total DB Interaction", value, threshold, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Interaction Total", value2, threshold2, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Chunk elapsed time", value9, threshold5, "ms");
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Pages Preread", (long)cPagePreread, (long)value3, string.Empty);
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Pages Read", (long)cPageRead, (long)value4, string.Empty);
			ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Pages Dirtied", (long)cPageDirtied, (long)value5, string.Empty);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001A788 File Offset: 0x00018988
		protected virtual void FormatOperationInformation(TraceContentBuilder cb, int indentLevel)
		{
			uint cbLogRecord = (uint)this.chunkStatistics.DatabaseCollector.ThreadStats.cbLogRecord;
			ExecutionDiagnostics.FormatLine(cb, 0, "Additional Operation Information:");
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Lock Contested: " + (this.chunkStatistics.LockTracker.GetAggregatedOperationData().NumberContested > 0));
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Long Operation: " + this.IsLongOperation);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Resource Intensive: " + this.IsResourceIntensive);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Log Bytes Written: " + cbLogRecord.ToString("N0", CultureInfo.InvariantCulture));
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Kernel CPU: " + this.chunkStatistics.CpuKernelTime.TotalMilliseconds.ToString(CultureInfo.InvariantCulture) + " ms");
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "User CPU: " + this.chunkStatistics.CpuUserTime.TotalMilliseconds.ToString(CultureInfo.InvariantCulture) + " ms");
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001A8AC File Offset: 0x00018AAC
		protected virtual void FormatDiagnosticInformation(TraceContentBuilder cb, int indentLevel)
		{
			if (this.chunkStatistics.DirectoryTracker.HasDataToLog)
			{
				this.chunkStatistics.DirectoryTracker.FormatData(cb, indentLevel, "Executed AD queries:", this.IsResourceIntensive);
			}
			if (this.chunkStatistics.LockTracker.HasDataToLog)
			{
				this.chunkStatistics.LockTracker.FormatData(cb, indentLevel, "Attempted Lock acquisitions:", this.IsResourceIntensive);
			}
			if (!this.chunkStatistics.DatabaseCollector.RowStats.IsEmpty)
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Logical IO statistics:");
				cb.Indent(indentLevel + 1);
				this.chunkStatistics.DatabaseCollector.AppendToTraceContentBuilder(cb);
				cb.AppendLine();
			}
			byte[] array;
			TimingContext.ExtractInfo(out array);
			if (array != null && array.Length > 0)
			{
				ExecutionDiagnostics.FormatLine(cb, indentLevel, "Timing trace:");
				ExecutionDiagnostics.FormatTimingInformation(cb, indentLevel, array);
			}
			if (this.chunkStatistics.DatabaseTracker.HasDataToLog)
			{
				this.chunkStatistics.DatabaseTracker.FormatData(cb, indentLevel, "Executed DB plans:", this.IsResourceIntensive);
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001A9AD File Offset: 0x00018BAD
		protected virtual void FormatClientActivityDiagnosticInformation(TraceContentBuilder cb, int indentLevel)
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001A9B0 File Offset: 0x00018BB0
		protected virtual void GetSummaryInformation(Guid correlationId, ref ExecutionDiagnostics.LongOperationSummary summary)
		{
			summary.CorrelationId = correlationId;
			summary.BuildNumber = ExWatson.ApplicationVersion.ToString();
			summary.MailboxGuid = this.MailboxGuid;
			summary.DatabaseGuid = this.DatabaseGuid;
			summary.ClientType = this.ClientType.ToString();
			summary.OperationSource = this.OpSource.ToString();
			summary.ClientProtocol = this.ClientProtocolName;
			summary.ClientComponent = this.ClientComponentName;
			summary.ClientAction = this.ClientActionString;
			summary.ChunkElapsedTime = (long)this.chunkStatistics.ElapsedTime.TotalMilliseconds;
			summary.InteractionTotal = (long)this.InteractionTotal.TotalMilliseconds;
			summary.TimeInDatabase = (long)this.chunkStatistics.DatabaseCollector.TotalTime.TotalMilliseconds;
			summary.PagesPreread = this.chunkStatistics.DatabaseCollector.ThreadStats.cPagePreread;
			summary.PagesRead = this.chunkStatistics.DatabaseCollector.ThreadStats.cPageRead;
			summary.PagesDirtied = this.chunkStatistics.DatabaseCollector.ThreadStats.cPageDirtied;
			summary.LogBytesWritten = ((this.chunkStatistics.DatabaseCollector.ThreadStats.cbLogRecord >= 0) ? this.chunkStatistics.DatabaseCollector.ThreadStats.cbLogRecord : int.MaxValue);
			summary.IsLongOperation = this.IsLongOperation;
			summary.IsResourceIntensive = this.IsResourceIntensive;
			summary.TimeInCpuKernel = (long)this.chunkStatistics.CpuKernelTime.TotalMilliseconds;
			summary.TimeInCpuUser = (long)this.chunkStatistics.CpuUserTime.TotalMilliseconds;
			summary.HashCode = this.GetHashCode();
			if (this.chunkStatistics.DatabaseTracker.HasDataToLog)
			{
				summary.NumberOfPlansExecuted = this.chunkStatistics.DatabaseTracker.GetTotalCount();
				summary.PlansExecutionTime = (long)this.chunkStatistics.DatabaseTracker.GetTotalTime().TotalMilliseconds;
			}
			if (this.chunkStatistics.LockTracker.HasDataToLog)
			{
				LockAcquisitionTracker.Data aggregatedOperationData = this.chunkStatistics.LockTracker.GetAggregatedOperationData();
				summary.NumberOfLocksAttempted = aggregatedOperationData.Count;
				summary.NumberOfLocksSucceeded = aggregatedOperationData.NumberSucceeded;
				summary.LocksWaitTime = (long)aggregatedOperationData.TotalTime.TotalMilliseconds;
				summary.IsContested = (aggregatedOperationData.NumberContested > 0);
			}
			if (this.chunkStatistics.DirectoryTracker.HasDataToLog)
			{
				ExecutionDiagnostics.DirectoryTrackingData aggregatedOperationData2 = this.chunkStatistics.DirectoryTracker.GetAggregatedOperationData();
				summary.NumberOfDirectoryOperations = aggregatedOperationData2.Count;
				summary.DirectoryOperationsTime = (long)aggregatedOperationData2.ExecutionTime.TotalMilliseconds;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001AC60 File Offset: 0x00018E60
		protected virtual void GetClientActivitySummaryInformation(Guid correlationId, ref ExecutionDiagnostics.HeavyClientActivitySummary summary)
		{
			summary.CorrelationId = correlationId;
			summary.MailboxGuid = this.MailboxGuid;
			summary.DatabaseGuid = this.DatabaseGuid;
			summary.ClientType = this.ClientType.ToString();
			summary.OperationSource = this.OpSource.ToString();
			summary.Activity = this.GetClientActivityString();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		private static void WriteDataToLog(IBinaryLogger logger, TraceBuffer buffer)
		{
			if (!logger.TryWrite(buffer))
			{
				StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(null);
				if (databaseInstance != null)
				{
					databaseInstance.LostDiagnosticEntries.Increment();
				}
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		private static void TruncateContent(TraceContentBuilder cb)
		{
			if (cb.Length > 131052)
			{
				while (cb.Length > 131052)
				{
					cb.Remove();
				}
				cb.Append("<TRUNCATED>");
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001AD20 File Offset: 0x00018F20
		private static void FormatTimingInformation(TraceContentBuilder cb, int indentLevel, byte[] buffer)
		{
			if (cb != null)
			{
				DateTime? dateTime = null;
				foreach (TimingContext.LocationAndTimeRecord locationAndTimeRecord in TimingContext.LocationAndTimeRecord.Parse(buffer))
				{
					cb.Indent(indentLevel + 1);
					cb.Append("LID ");
					cb.Append(locationAndTimeRecord.Lid & DiagnosticContext.ContextLidMask);
					cb.Append(", TID ");
					cb.Append(locationAndTimeRecord.Tid);
					cb.Append(", DID ");
					cb.Append(((DiagnosticSource)locationAndTimeRecord.Did).ToString());
					cb.Append(", CID ");
					cb.Append(locationAndTimeRecord.Cid);
					cb.Append(", ");
					if ((locationAndTimeRecord.Lid & DiagnosticContext.ContextSignatureMask) == 813694976U)
					{
						dateTime = new DateTime?(new DateTime((long)locationAndTimeRecord.Info, DateTimeKind.Utc));
						cb.Append(dateTime.Value.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fffffff"));
						cb.Append(" UTC");
					}
					else if (dateTime != null)
					{
						dateTime = new DateTime?(dateTime.Value.AddTicks((long)locationAndTimeRecord.Info));
						cb.Append(dateTime.Value.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fffffff"));
						cb.Append(" UTC, +");
						cb.Append(TimeSpan.FromTicks((long)locationAndTimeRecord.Info).TotalMilliseconds);
						cb.Append(" ms");
					}
					else
					{
						cb.Append(TimeSpan.FromTicks((long)locationAndTimeRecord.Info).TotalMilliseconds);
						cb.Append(" ms");
					}
					cb.AppendLine();
				}
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001AEFC File Offset: 0x000190FC
		private static ExecutionDiagnostics.LockCategory GetLockCategory(LockManager.LockType lockType)
		{
			switch (lockType)
			{
			case LockManager.LockType.MailboxExclusive:
				return ExecutionDiagnostics.LockCategory.Mailbox;
			case LockManager.LockType.UserInformationExclusive:
			case LockManager.LockType.UserExclusive:
			case LockManager.LockType.PerUserExclusive:
				break;
			case LockManager.LockType.LogicalIndexCacheExclusive:
				return ExecutionDiagnostics.LockCategory.Component;
			case LockManager.LockType.LogicalIndexExclusive:
				return ExecutionDiagnostics.LockCategory.Component;
			case LockManager.LockType.PerUserCacheExclusive:
				return ExecutionDiagnostics.LockCategory.Component;
			case LockManager.LockType.ChangeNumberAndIdCountersExclusive:
				return ExecutionDiagnostics.LockCategory.Component;
			case LockManager.LockType.MailboxComponentsExclusive:
				return ExecutionDiagnostics.LockCategory.Component;
			default:
				switch (lockType)
				{
				case LockManager.LockType.MailboxShared:
					return ExecutionDiagnostics.LockCategory.Mailbox;
				case LockManager.LockType.LogicalIndexCacheShared:
					return ExecutionDiagnostics.LockCategory.Component;
				case LockManager.LockType.LogicalIndexShared:
					return ExecutionDiagnostics.LockCategory.Component;
				case LockManager.LockType.PerUserCacheShared:
					return ExecutionDiagnostics.LockCategory.Component;
				case LockManager.LockType.ChangeNumberAndIdCountersShared:
					return ExecutionDiagnostics.LockCategory.Component;
				case LockManager.LockType.MailboxComponentsShared:
					return ExecutionDiagnostics.LockCategory.Component;
				}
				break;
			}
			return ExecutionDiagnostics.LockCategory.Other;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001AF80 File Offset: 0x00019180
		private TraceContentBuilder GetDetailContent(Guid correlationId)
		{
			TraceContentBuilder traceContentBuilder = TraceContentBuilder.Create(ExecutionDiagnostics.maxChunkListSize);
			ExecutionDiagnostics.FormatLine(traceContentBuilder, 0, "Common Info:");
			this.FormatCommonInformation(traceContentBuilder, 1, correlationId);
			this.FormatThresholdInformation(traceContentBuilder, 1);
			this.FormatOperationInformation(traceContentBuilder, 1);
			this.FormatDiagnosticInformation(traceContentBuilder, 0);
			ExecutionDiagnostics.TruncateContent(traceContentBuilder);
			return traceContentBuilder;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001AFCC File Offset: 0x000191CC
		private ExecutionDiagnostics.LongOperationSummary GetSummaryContent(Guid correlationId)
		{
			ExecutionDiagnostics.LongOperationSummary result = default(ExecutionDiagnostics.LongOperationSummary);
			this.GetSummaryInformation(correlationId, ref result);
			return result;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001AFEC File Offset: 0x000191EC
		private string GetClientActivityString()
		{
			return string.Format("{0}{1}{2}{3}{4}", new object[]
			{
				this.clientComponentName ?? string.Empty,
				string.IsNullOrEmpty(this.clientComponentName) ? string.Empty : ".",
				this.clientActionString ?? string.Empty,
				string.IsNullOrEmpty(this.clientActionString) ? string.Empty : ".",
				this.clientActivityId
			});
		}

		// Token: 0x0400031C RID: 796
		private const int MaxContentLength = 131052;

		// Token: 0x0400031D RID: 797
		private static int maxChunkListSize = (int)Math.Ceiling(131052.0 / (double)TraceContentBuilder.MaximumChunkLength) + 1;

		// Token: 0x0400031E RID: 798
		private Guid mailboxGuid;

		// Token: 0x0400031F RID: 799
		private int mailboxNumber;

		// Token: 0x04000320 RID: 800
		private Guid databaseGuid;

		// Token: 0x04000321 RID: 801
		private ExecutionDiagnostics.OperationSource operationSource;

		// Token: 0x04000322 RID: 802
		private int operationDetail;

		// Token: 0x04000323 RID: 803
		private ClientType clientType = ClientType.MaxValue;

		// Token: 0x04000324 RID: 804
		private Guid clientActivityId;

		// Token: 0x04000325 RID: 805
		private string clientComponentName;

		// Token: 0x04000326 RID: 806
		private string clientProtocolName;

		// Token: 0x04000327 RID: 807
		private string clientActionString;

		// Token: 0x04000328 RID: 808
		private uint expandedClientActionStringId;

		// Token: 0x04000329 RID: 809
		private bool sharedLock;

		// Token: 0x0400032A RID: 810
		private IDigestCollector digestCollector;

		// Token: 0x0400032B RID: 811
		private IRopSummaryCollector ropSummaryCollector;

		// Token: 0x0400032C RID: 812
		private StorePerClientTypePerformanceCountersInstance perClientPerfInstance;

		// Token: 0x0400032D RID: 813
		private LogTransactionInformationCollector logTransactionInformationCollector;

		// Token: 0x0400032E RID: 814
		private ExecutionDiagnostics.ChunkStatisticsContainer chunkStatistics;

		// Token: 0x0400032F RID: 815
		private ExecutionDiagnostics.OperationStatisticsContainer operationStatistics;

		// Token: 0x04000330 RID: 816
		private ExecutionDiagnostics.RpcStatisticsContainer rpcStatistics;

		// Token: 0x04000331 RID: 817
		private bool perClientTracingEnabled;

		// Token: 0x04000332 RID: 818
		private bool inMailboxOperationContext;

		// Token: 0x04000333 RID: 819
		private TestCaseId testCaseId = TestCaseId.GetInProcessTestCaseId();

		// Token: 0x04000334 RID: 820
		private List<Exception> exceptionHistory;

		// Token: 0x04000335 RID: 821
		private StopwatchStamp executionStart;

		// Token: 0x04000336 RID: 822
		private bool diagnosticDumped;

		// Token: 0x04000337 RID: 823
		private readonly uint instanceIdentifier;

		// Token: 0x02000064 RID: 100
		internal class ExecutionTracker<TOperationData> where TOperationData : class, IExecutionTrackingData<TOperationData>, new()
		{
			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001B094 File Offset: 0x00019294
			public bool HasDataToLog
			{
				get
				{
					return this.executionEntries != null && this.executionEntries.Count != 0;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001B0B1 File Offset: 0x000192B1
			internal ConcurrentDictionary<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey, TOperationData> ExecutionEntries
			{
				get
				{
					return this.executionEntries;
				}
			}

			// Token: 0x060003F5 RID: 1013 RVA: 0x0001B0B9 File Offset: 0x000192B9
			public void Reset()
			{
				if (this.executionEntries != null)
				{
					this.executionEntries.Clear();
				}
			}

			// Token: 0x060003F6 RID: 1014 RVA: 0x0001B0D0 File Offset: 0x000192D0
			public int GetTotalCount()
			{
				int num = 0;
				if (this.executionEntries != null)
				{
					foreach (KeyValuePair<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey, TOperationData> keyValuePair in this.executionEntries)
					{
						int num2 = num;
						TOperationData value = keyValuePair.Value;
						num = num2 + value.Count;
					}
				}
				return num;
			}

			// Token: 0x060003F7 RID: 1015 RVA: 0x0001B13C File Offset: 0x0001933C
			public TimeSpan GetTotalTime()
			{
				TimeSpan timeSpan = TimeSpan.Zero;
				if (this.executionEntries != null)
				{
					foreach (KeyValuePair<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey, TOperationData> keyValuePair in this.executionEntries)
					{
						TimeSpan t = timeSpan;
						TOperationData value = keyValuePair.Value;
						timeSpan = t + value.TotalTime;
					}
				}
				return timeSpan;
			}

			// Token: 0x060003F8 RID: 1016 RVA: 0x0001B1B3 File Offset: 0x000193B3
			public TOperationData GetAggregatedOperationData()
			{
				return this.GetAggregatedOperationData<object>((ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey key, object context) => true, null);
			}

			// Token: 0x060003F9 RID: 1017 RVA: 0x0001B1DC File Offset: 0x000193DC
			public void FormatData(TraceContentBuilder cb, int indentLevel, string title, bool includeDetails)
			{
				if (!this.HasDataToLog)
				{
					return;
				}
				ExecutionDiagnostics.FormatLine(cb, indentLevel, title);
				foreach (KeyValuePair<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey, TOperationData> keyValuePair in this.executionEntries)
				{
					this.FormatEntry(cb, indentLevel, keyValuePair.Key, keyValuePair.Value);
					if (includeDetails)
					{
						TOperationData value = keyValuePair.Value;
						value.AppendDetailsToTraceContentBuilder(cb, indentLevel + 2);
					}
				}
			}

			// Token: 0x060003FA RID: 1018 RVA: 0x0001B268 File Offset: 0x00019468
			public TOperationData RecordOperation(IOperationExecutionTrackable operation)
			{
				if (this.executionEntries == null)
				{
					this.executionEntries = new ConcurrentDictionary<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey, TOperationData>(20, 100);
				}
				ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey key = new ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey(operation.GetTrackingKey());
				TOperationData toperationData;
				if (!this.executionEntries.TryGetValue(key, out toperationData))
				{
					toperationData = Activator.CreateInstance<TOperationData>();
					this.executionEntries.GetOrAdd(key, toperationData);
				}
				return toperationData;
			}

			// Token: 0x060003FB RID: 1019 RVA: 0x0001B2C0 File Offset: 0x000194C0
			public override int GetHashCode()
			{
				int num = 0;
				if (this.executionEntries != null)
				{
					foreach (ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey executionEntryKey in this.executionEntries.Keys)
					{
						num ^= executionEntryKey.GetSimpleHashCode();
					}
				}
				return num;
			}

			// Token: 0x060003FC RID: 1020 RVA: 0x0001B320 File Offset: 0x00019520
			internal void FormatEntry(TraceContentBuilder cb, int indentLevel, ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey key, TOperationData value)
			{
				cb.Indent(indentLevel + 1);
				value.AppendToTraceContentBuilder(cb);
				cb.Append(", ");
				cb.Append(value.Count);
				cb.Append(", ");
				cb.Append(key.ToString());
				cb.AppendLine();
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x0001B388 File Offset: 0x00019588
			internal TOperationData GetAggregatedOperationData<TContext>(Func<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey, TContext, bool> predicate, TContext context)
			{
				TOperationData result = Activator.CreateInstance<TOperationData>();
				if (this.executionEntries != null)
				{
					foreach (KeyValuePair<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey, TOperationData> keyValuePair in this.executionEntries)
					{
						if (predicate(keyValuePair.Key, context))
						{
							result.Aggregate(keyValuePair.Value);
						}
					}
				}
				return result;
			}

			// Token: 0x0400033A RID: 826
			private ConcurrentDictionary<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey, TOperationData> executionEntries;

			// Token: 0x02000065 RID: 101
			internal struct ExecutionEntryKey : IEquatable<ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey>
			{
				// Token: 0x06000400 RID: 1024 RVA: 0x0001B40C File Offset: 0x0001960C
				internal ExecutionEntryKey(IOperationExecutionTrackingKey trackingKey)
				{
					this.trackingKey = trackingKey;
				}

				// Token: 0x17000107 RID: 263
				// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001B415 File Offset: 0x00019615
				internal IOperationExecutionTrackingKey TrackingKey
				{
					get
					{
						return this.trackingKey;
					}
				}

				// Token: 0x06000402 RID: 1026 RVA: 0x0001B41D File Offset: 0x0001961D
				public override string ToString()
				{
					return this.trackingKey.TrackingKeyToString();
				}

				// Token: 0x06000403 RID: 1027 RVA: 0x0001B42A File Offset: 0x0001962A
				public override bool Equals(object obj)
				{
					return obj is ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey && this.Equals((ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey)obj);
				}

				// Token: 0x06000404 RID: 1028 RVA: 0x0001B442 File Offset: 0x00019642
				public override int GetHashCode()
				{
					return this.trackingKey.GetTrackingKeyHashValue();
				}

				// Token: 0x06000405 RID: 1029 RVA: 0x0001B44F File Offset: 0x0001964F
				public int GetSimpleHashCode()
				{
					return this.trackingKey.GetSimpleHashValue();
				}

				// Token: 0x06000406 RID: 1030 RVA: 0x0001B45C File Offset: 0x0001965C
				public bool Equals(ExecutionDiagnostics.ExecutionTracker<TOperationData>.ExecutionEntryKey other)
				{
					return this.trackingKey.IsTrackingKeyEqualTo(other.trackingKey);
				}

				// Token: 0x0400033C RID: 828
				private IOperationExecutionTrackingKey trackingKey;
			}
		}

		// Token: 0x02000066 RID: 102
		public enum OperationSource : byte
		{
			// Token: 0x0400033E RID: 830
			Mapi,
			// Token: 0x0400033F RID: 831
			AdminRpc,
			// Token: 0x04000340 RID: 832
			LogicalIndexCleanup,
			// Token: 0x04000341 RID: 833
			MailboxTask,
			// Token: 0x04000342 RID: 834
			MailboxCleanup,
			// Token: 0x04000343 RID: 835
			MailboxQuarantine,
			// Token: 0x04000344 RID: 836
			MapiTimedEvent,
			// Token: 0x04000345 RID: 837
			MailboxMaintenance,
			// Token: 0x04000346 RID: 838
			OnlineIntegrityCheck,
			// Token: 0x04000347 RID: 839
			LogicalIndexMaintenanceTableTask,
			// Token: 0x04000348 RID: 840
			SearchFolderAgeOut,
			// Token: 0x04000349 RID: 841
			SubobjectsCleanup,
			// Token: 0x0400034A RID: 842
			PerUserCacheFlush,
			// Token: 0x0400034B RID: 843
			SimpleQueryTarget
		}

		// Token: 0x02000067 RID: 103
		public struct HeavyClientActivitySummary
		{
			// Token: 0x0400034C RID: 844
			public Guid DatabaseGuid;

			// Token: 0x0400034D RID: 845
			public Guid MailboxGuid;

			// Token: 0x0400034E RID: 846
			public string ClientType;

			// Token: 0x0400034F RID: 847
			public string OperationSource;

			// Token: 0x04000350 RID: 848
			public string Activity;

			// Token: 0x04000351 RID: 849
			public uint TotalRpcCalls;

			// Token: 0x04000352 RID: 850
			public uint TotalRops;

			// Token: 0x04000353 RID: 851
			public Guid CorrelationId;
		}

		// Token: 0x02000068 RID: 104
		public struct LongOperationSummary
		{
			// Token: 0x04000354 RID: 852
			public Guid DatabaseGuid;

			// Token: 0x04000355 RID: 853
			public Guid MailboxGuid;

			// Token: 0x04000356 RID: 854
			public string ClientType;

			// Token: 0x04000357 RID: 855
			public string OperationSource;

			// Token: 0x04000358 RID: 856
			public string OperationType;

			// Token: 0x04000359 RID: 857
			public string OperationName;

			// Token: 0x0400035A RID: 858
			public string OperationDetail;

			// Token: 0x0400035B RID: 859
			public long ChunkElapsedTime;

			// Token: 0x0400035C RID: 860
			public long InteractionTotal;

			// Token: 0x0400035D RID: 861
			public long TimeInDatabase;

			// Token: 0x0400035E RID: 862
			public int PagesPreread;

			// Token: 0x0400035F RID: 863
			public int PagesRead;

			// Token: 0x04000360 RID: 864
			public int PagesDirtied;

			// Token: 0x04000361 RID: 865
			public int LogBytesWritten;

			// Token: 0x04000362 RID: 866
			public int SortOrderCount;

			// Token: 0x04000363 RID: 867
			public int NumberOfPlansExecuted;

			// Token: 0x04000364 RID: 868
			public long PlansExecutionTime;

			// Token: 0x04000365 RID: 869
			public int NumberOfDirectoryOperations;

			// Token: 0x04000366 RID: 870
			public long DirectoryOperationsTime;

			// Token: 0x04000367 RID: 871
			public int NumberOfLocksAttempted;

			// Token: 0x04000368 RID: 872
			public int NumberOfLocksSucceeded;

			// Token: 0x04000369 RID: 873
			public long LocksWaitTime;

			// Token: 0x0400036A RID: 874
			public bool IsLongOperation;

			// Token: 0x0400036B RID: 875
			public bool IsResourceIntensive;

			// Token: 0x0400036C RID: 876
			public bool IsContested;

			// Token: 0x0400036D RID: 877
			public string ClientProtocol;

			// Token: 0x0400036E RID: 878
			public string ClientComponent;

			// Token: 0x0400036F RID: 879
			public string ClientAction;

			// Token: 0x04000370 RID: 880
			public Guid CorrelationId;

			// Token: 0x04000371 RID: 881
			public string BuildNumber;

			// Token: 0x04000372 RID: 882
			public long TimeInCpuKernel;

			// Token: 0x04000373 RID: 883
			public long TimeInCpuUser;

			// Token: 0x04000374 RID: 884
			public int HashCode;
		}

		// Token: 0x02000069 RID: 105
		public class DirectoryTrackingData : IExecutionTrackingData<ExecutionDiagnostics.DirectoryTrackingData>
		{
			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001B478 File Offset: 0x00019678
			// (set) Token: 0x06000409 RID: 1033 RVA: 0x0001B480 File Offset: 0x00019680
			public TimeSpan ExecutionTime { get; set; }

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001B489 File Offset: 0x00019689
			// (set) Token: 0x0600040B RID: 1035 RVA: 0x0001B491 File Offset: 0x00019691
			public int Count { get; set; }

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001B49A File Offset: 0x0001969A
			public TimeSpan TotalTime
			{
				get
				{
					return this.ExecutionTime;
				}
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0001B4A2 File Offset: 0x000196A2
			public void Aggregate(ExecutionDiagnostics.DirectoryTrackingData dataToAggregate)
			{
				this.ExecutionTime += dataToAggregate.ExecutionTime;
				this.Count += dataToAggregate.Count;
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0001B4CE File Offset: 0x000196CE
			public void Reset()
			{
				this.Count = 0;
				this.ExecutionTime = TimeSpan.Zero;
			}

			// Token: 0x0600040F RID: 1039 RVA: 0x0001B4E4 File Offset: 0x000196E4
			public void AppendToTraceContentBuilder(TraceContentBuilder cb)
			{
				cb.Append(((long)this.ExecutionTime.TotalMicroseconds()).ToString("N0", CultureInfo.InvariantCulture));
				cb.Append(" us");
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x0001B520 File Offset: 0x00019720
			public void AppendDetailsToTraceContentBuilder(TraceContentBuilder cb, int indentLevel)
			{
			}
		}

		// Token: 0x0200006A RID: 106
		public interface IExecutionDiagnosticsStatistics
		{
			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000411 RID: 1041
			TimeSpan ElapsedTime { get; }

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000412 RID: 1042
			TimeSpan MaximumChunkTime { get; }

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000413 RID: 1043
			TimeSpan FastWaitTime { get; }

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000414 RID: 1044
			TimeSpan CpuKernelTime { get; }

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000415 RID: 1045
			TimeSpan CpuUserTime { get; }

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000416 RID: 1046
			uint Count { get; }

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000417 RID: 1047
			DatabaseConnectionStatistics DatabaseCollector { get; }

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000418 RID: 1048
			int LockCount { get; }

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000419 RID: 1049
			TimeSpan LockTotalTime { get; }

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x0600041A RID: 1050
			int DirectoryCount { get; }

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x0600041B RID: 1051
			TimeSpan DirectoryTotalTime { get; }
		}

		// Token: 0x0200006B RID: 107
		public class ChunkStatisticsContainer : ExecutionDiagnostics.IExecutionDiagnosticsStatistics
		{
			// Token: 0x0600041C RID: 1052 RVA: 0x0001B522 File Offset: 0x00019722
			private ChunkStatisticsContainer()
			{
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001B52A File Offset: 0x0001972A
			public TimeSpan ElapsedTime
			{
				get
				{
					return this.elapsedTime;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001B532 File Offset: 0x00019732
			public TimeSpan MaximumChunkTime
			{
				get
				{
					return this.elapsedTime;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001B53A File Offset: 0x0001973A
			// (set) Token: 0x06000420 RID: 1056 RVA: 0x0001B542 File Offset: 0x00019742
			public TimeSpan FastWaitTime
			{
				get
				{
					return this.fastWaitTime;
				}
				set
				{
					this.fastWaitTime = value;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001B54B File Offset: 0x0001974B
			public TimeSpan CpuKernelTime
			{
				get
				{
					return this.cpuKernelTime;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001B553 File Offset: 0x00019753
			public TimeSpan CpuUserTime
			{
				get
				{
					return this.cpuUserTime;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001B55B File Offset: 0x0001975B
			public DatabaseConnectionStatistics DatabaseCollector
			{
				get
				{
					return this.databaseCollector;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001B563 File Offset: 0x00019763
			public int LockCount
			{
				get
				{
					return this.lockCollector.Count;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001B570 File Offset: 0x00019770
			public TimeSpan LockTotalTime
			{
				get
				{
					return this.lockCollector.TotalTime;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001B57D File Offset: 0x0001977D
			public int DirectoryCount
			{
				get
				{
					return this.directoryCollector.Count;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001B58A File Offset: 0x0001978A
			public TimeSpan DirectoryTotalTime
			{
				get
				{
					return this.directoryCollector.TotalTime;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001B597 File Offset: 0x00019797
			public uint Count
			{
				get
				{
					return 1U;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001B59A File Offset: 0x0001979A
			public bool Started
			{
				get
				{
					return this.started;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001B5A2 File Offset: 0x000197A2
			internal ExecutionDiagnostics.ExecutionTracker<DatabaseOperationStatistics> DatabaseTracker
			{
				get
				{
					return this.databaseTracker;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001B5AA File Offset: 0x000197AA
			internal ExecutionDiagnostics.ExecutionTracker<LockAcquisitionTracker.Data> LockTracker
			{
				get
				{
					return this.lockTracker;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001B5B2 File Offset: 0x000197B2
			internal ExecutionDiagnostics.ExecutionTracker<ExecutionDiagnostics.DirectoryTrackingData> DirectoryTracker
			{
				get
				{
					return this.directoryTracker;
				}
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x0001B5BC File Offset: 0x000197BC
			public static ExecutionDiagnostics.ChunkStatisticsContainer Create()
			{
				return new ExecutionDiagnostics.ChunkStatisticsContainer
				{
					databaseTracker = new ExecutionDiagnostics.ExecutionTracker<DatabaseOperationStatistics>(),
					databaseCollector = new DatabaseConnectionStatistics(),
					lockTracker = new ExecutionDiagnostics.ExecutionTracker<LockAcquisitionTracker.Data>(),
					lockCollector = new LockAcquisitionTracker.Data(),
					directoryTracker = new ExecutionDiagnostics.ExecutionTracker<ExecutionDiagnostics.DirectoryTrackingData>(),
					directoryCollector = new ExecutionDiagnostics.DirectoryTrackingData()
				};
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x0001B612 File Offset: 0x00019812
			public void Start(TimeSpan startElapsedTime)
			{
				this.startTime = startElapsedTime;
				this.cpuSuccess = ThreadTimes.GetFromCurrentThread(out this.cpuKernelStart, out this.cpuUserStart);
				this.started = true;
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x0001B63C File Offset: 0x0001983C
			public void Stop(TimeSpan stopElapsedTime)
			{
				TimeSpan zero = TimeSpan.Zero;
				TimeSpan zero2 = TimeSpan.Zero;
				this.elapsedTime = stopElapsedTime - this.startTime;
				this.started = false;
				if (this.cpuSuccess && ThreadTimes.GetFromCurrentThread(out zero, out zero2))
				{
					this.cpuKernelTime = zero - this.cpuKernelStart;
					this.cpuUserTime = zero2 - this.cpuUserStart;
				}
				this.lockCollector.Aggregate(this.lockTracker.GetAggregatedOperationData());
				this.directoryCollector.Aggregate(this.directoryTracker.GetAggregatedOperationData());
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x0001B6D4 File Offset: 0x000198D4
			public void Reset()
			{
				this.startTime = TimeSpan.Zero;
				this.elapsedTime = TimeSpan.Zero;
				this.fastWaitTime = TimeSpan.Zero;
				this.cpuKernelTime = TimeSpan.Zero;
				this.cpuUserTime = TimeSpan.Zero;
				this.cpuSuccess = false;
				this.cpuKernelStart = TimeSpan.Zero;
				this.cpuUserStart = TimeSpan.Zero;
				this.databaseTracker.Reset();
				this.databaseCollector.Reset();
				this.lockTracker.Reset();
				this.lockCollector.Reset();
				this.directoryTracker.Reset();
				this.directoryCollector.Reset();
			}

			// Token: 0x04000377 RID: 887
			private TimeSpan startTime;

			// Token: 0x04000378 RID: 888
			private TimeSpan elapsedTime;

			// Token: 0x04000379 RID: 889
			private TimeSpan fastWaitTime;

			// Token: 0x0400037A RID: 890
			private TimeSpan cpuKernelTime;

			// Token: 0x0400037B RID: 891
			private TimeSpan cpuUserTime;

			// Token: 0x0400037C RID: 892
			private bool cpuSuccess;

			// Token: 0x0400037D RID: 893
			private TimeSpan cpuKernelStart;

			// Token: 0x0400037E RID: 894
			private TimeSpan cpuUserStart;

			// Token: 0x0400037F RID: 895
			private ExecutionDiagnostics.ExecutionTracker<DatabaseOperationStatistics> databaseTracker;

			// Token: 0x04000380 RID: 896
			private DatabaseConnectionStatistics databaseCollector;

			// Token: 0x04000381 RID: 897
			private ExecutionDiagnostics.ExecutionTracker<LockAcquisitionTracker.Data> lockTracker;

			// Token: 0x04000382 RID: 898
			private LockAcquisitionTracker.Data lockCollector;

			// Token: 0x04000383 RID: 899
			private ExecutionDiagnostics.ExecutionTracker<ExecutionDiagnostics.DirectoryTrackingData> directoryTracker;

			// Token: 0x04000384 RID: 900
			private ExecutionDiagnostics.DirectoryTrackingData directoryCollector;

			// Token: 0x04000385 RID: 901
			private bool started;
		}

		// Token: 0x0200006C RID: 108
		public class OperationStatisticsContainer : ExecutionDiagnostics.IExecutionDiagnosticsStatistics
		{
			// Token: 0x06000431 RID: 1073 RVA: 0x0001B777 File Offset: 0x00019977
			private OperationStatisticsContainer()
			{
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x0001B780 File Offset: 0x00019980
			public static ExecutionDiagnostics.OperationStatisticsContainer Create()
			{
				return new ExecutionDiagnostics.OperationStatisticsContainer
				{
					databaseCollector = new DatabaseConnectionStatistics()
				};
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x0001B7A0 File Offset: 0x000199A0
			public void Reset()
			{
				this.elapsedTime = TimeSpan.Zero;
				this.fastWaitTime = TimeSpan.Zero;
				this.cpuKernelTime = TimeSpan.Zero;
				this.cpuUserTime = TimeSpan.Zero;
				this.counter = 0U;
				this.maximumChunkTime = TimeSpan.Zero;
				this.databaseCollector.Reset();
				this.lockCount = 0;
				this.lockTotalTime = TimeSpan.Zero;
				this.directoryCount = 0;
				this.directoryTotalTime = TimeSpan.Zero;
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x0001B81C File Offset: 0x00019A1C
			public void Aggregate(ExecutionDiagnostics.ChunkStatisticsContainer chunk)
			{
				this.elapsedTime += chunk.ElapsedTime;
				this.fastWaitTime += chunk.FastWaitTime;
				this.cpuKernelTime += chunk.CpuKernelTime;
				this.cpuUserTime += chunk.CpuUserTime;
				this.counter += chunk.Count;
				this.maximumChunkTime = TimeSpan.FromTicks(Math.Max(this.maximumChunkTime.Ticks, chunk.MaximumChunkTime.Ticks));
				this.databaseCollector.Aggregate(chunk.DatabaseCollector);
				this.lockCount += chunk.LockCount;
				this.lockTotalTime += chunk.LockTotalTime;
				this.directoryCount += chunk.DirectoryCount;
				this.directoryTotalTime += chunk.DirectoryTotalTime;
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001B926 File Offset: 0x00019B26
			public TimeSpan ElapsedTime
			{
				get
				{
					return this.elapsedTime;
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001B92E File Offset: 0x00019B2E
			public TimeSpan MaximumChunkTime
			{
				get
				{
					return this.maximumChunkTime;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001B936 File Offset: 0x00019B36
			public TimeSpan FastWaitTime
			{
				get
				{
					return this.fastWaitTime;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001B93E File Offset: 0x00019B3E
			public TimeSpan CpuKernelTime
			{
				get
				{
					return this.cpuKernelTime;
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001B946 File Offset: 0x00019B46
			public TimeSpan CpuUserTime
			{
				get
				{
					return this.cpuUserTime;
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001B94E File Offset: 0x00019B4E
			public uint Count
			{
				get
				{
					return this.counter;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001B956 File Offset: 0x00019B56
			public DatabaseConnectionStatistics DatabaseCollector
			{
				get
				{
					return this.databaseCollector;
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001B95E File Offset: 0x00019B5E
			public int LockCount
			{
				get
				{
					return this.lockCount;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001B966 File Offset: 0x00019B66
			public TimeSpan LockTotalTime
			{
				get
				{
					return this.lockTotalTime;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001B96E File Offset: 0x00019B6E
			public int DirectoryCount
			{
				get
				{
					return this.directoryCount;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001B976 File Offset: 0x00019B76
			public TimeSpan DirectoryTotalTime
			{
				get
				{
					return this.directoryTotalTime;
				}
			}

			// Token: 0x04000386 RID: 902
			private TimeSpan elapsedTime;

			// Token: 0x04000387 RID: 903
			private TimeSpan fastWaitTime;

			// Token: 0x04000388 RID: 904
			private TimeSpan cpuKernelTime;

			// Token: 0x04000389 RID: 905
			private TimeSpan cpuUserTime;

			// Token: 0x0400038A RID: 906
			private uint counter;

			// Token: 0x0400038B RID: 907
			private TimeSpan maximumChunkTime;

			// Token: 0x0400038C RID: 908
			private DatabaseConnectionStatistics databaseCollector;

			// Token: 0x0400038D RID: 909
			private int lockCount;

			// Token: 0x0400038E RID: 910
			private TimeSpan lockTotalTime;

			// Token: 0x0400038F RID: 911
			private int directoryCount;

			// Token: 0x04000390 RID: 912
			private TimeSpan directoryTotalTime;
		}

		// Token: 0x0200006D RID: 109
		public class RpcStatisticsContainer : ExecutionDiagnostics.IExecutionDiagnosticsStatistics
		{
			// Token: 0x06000440 RID: 1088 RVA: 0x0001B97E File Offset: 0x00019B7E
			private RpcStatisticsContainer()
			{
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x0001B988 File Offset: 0x00019B88
			public static ExecutionDiagnostics.RpcStatisticsContainer Create()
			{
				return new ExecutionDiagnostics.RpcStatisticsContainer
				{
					databaseCollector = new DatabaseConnectionStatistics()
				};
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x0001B9A8 File Offset: 0x00019BA8
			public void Reset()
			{
				this.elapsedTime = TimeSpan.Zero;
				this.fastWaitTime = TimeSpan.Zero;
				this.cpuKernelTime = TimeSpan.Zero;
				this.cpuUserTime = TimeSpan.Zero;
				this.counter = 0U;
				this.maximumChunkTime = TimeSpan.Zero;
				this.databaseCollector.Reset();
				this.lockCount = 0;
				this.lockTotalTime = TimeSpan.Zero;
				this.directoryCount = 0;
				this.directoryTotalTime = TimeSpan.Zero;
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x0001BA24 File Offset: 0x00019C24
			public void Aggregate(ExecutionDiagnostics.OperationStatisticsContainer operation)
			{
				this.elapsedTime += operation.ElapsedTime;
				this.fastWaitTime += operation.FastWaitTime;
				this.cpuKernelTime += operation.CpuKernelTime;
				this.cpuUserTime += operation.CpuUserTime;
				this.counter += operation.Count;
				this.maximumChunkTime = TimeSpan.FromTicks(Math.Max(this.maximumChunkTime.Ticks, operation.MaximumChunkTime.Ticks));
				this.databaseCollector.Aggregate(operation.DatabaseCollector);
				this.lockCount += operation.LockCount;
				this.lockTotalTime += operation.LockTotalTime;
				this.directoryCount += operation.DirectoryCount;
				this.directoryTotalTime += operation.DirectoryTotalTime;
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001BB2E File Offset: 0x00019D2E
			public TimeSpan ElapsedTime
			{
				get
				{
					return this.elapsedTime;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001BB36 File Offset: 0x00019D36
			public TimeSpan MaximumChunkTime
			{
				get
				{
					return this.maximumChunkTime;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001BB3E File Offset: 0x00019D3E
			public TimeSpan FastWaitTime
			{
				get
				{
					return this.fastWaitTime;
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001BB46 File Offset: 0x00019D46
			public TimeSpan CpuKernelTime
			{
				get
				{
					return this.cpuKernelTime;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001BB4E File Offset: 0x00019D4E
			public TimeSpan CpuUserTime
			{
				get
				{
					return this.cpuUserTime;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001BB56 File Offset: 0x00019D56
			public uint Count
			{
				get
				{
					return this.counter;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001BB5E File Offset: 0x00019D5E
			public DatabaseConnectionStatistics DatabaseCollector
			{
				get
				{
					return this.databaseCollector;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001BB66 File Offset: 0x00019D66
			public int LockCount
			{
				get
				{
					return this.lockCount;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001BB6E File Offset: 0x00019D6E
			public TimeSpan LockTotalTime
			{
				get
				{
					return this.lockTotalTime;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x0600044D RID: 1101 RVA: 0x0001BB76 File Offset: 0x00019D76
			public int DirectoryCount
			{
				get
				{
					return this.directoryCount;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001BB7E File Offset: 0x00019D7E
			public TimeSpan DirectoryTotalTime
			{
				get
				{
					return this.directoryTotalTime;
				}
			}

			// Token: 0x04000391 RID: 913
			private TimeSpan elapsedTime;

			// Token: 0x04000392 RID: 914
			private TimeSpan fastWaitTime;

			// Token: 0x04000393 RID: 915
			private TimeSpan cpuKernelTime;

			// Token: 0x04000394 RID: 916
			private TimeSpan cpuUserTime;

			// Token: 0x04000395 RID: 917
			private uint counter;

			// Token: 0x04000396 RID: 918
			private TimeSpan maximumChunkTime;

			// Token: 0x04000397 RID: 919
			private DatabaseConnectionStatistics databaseCollector;

			// Token: 0x04000398 RID: 920
			private int lockCount;

			// Token: 0x04000399 RID: 921
			private TimeSpan lockTotalTime;

			// Token: 0x0400039A RID: 922
			private int directoryCount;

			// Token: 0x0400039B RID: 923
			private TimeSpan directoryTotalTime;
		}

		// Token: 0x0200006E RID: 110
		protected enum LockCategory
		{
			// Token: 0x0400039D RID: 925
			Mailbox,
			// Token: 0x0400039E RID: 926
			Component,
			// Token: 0x0400039F RID: 927
			Other
		}
	}
}
