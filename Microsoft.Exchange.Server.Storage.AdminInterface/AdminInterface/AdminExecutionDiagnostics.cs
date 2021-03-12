using System;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.MapiDisp;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000003 RID: 3
	internal class AdminExecutionDiagnostics : RpcExecutionDiagnostics
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AdminExecutionDiagnostics(AdminMethod methodId, int operationDetail)
		{
			this.methodId = methodId;
			base.OpSource = ExecutionDiagnostics.OperationSource.AdminRpc;
			base.OpDetail = operationDetail;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020ED File Offset: 0x000002ED
		public override byte OpNumber
		{
			get
			{
				return (byte)this.methodId;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F5 File Offset: 0x000002F5
		public override uint TypeIdentifier
		{
			get
			{
				return 4U;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002100 File Offset: 0x00000300
		internal AdminExMonLogger AdminExMonLogger
		{
			get
			{
				return this.adminExmonLogger;
			}
			set
			{
				this.adminExmonLogger = value;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002109 File Offset: 0x00000309
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ "Admin".GetHashCode() ^ this.methodId.GetHashCode();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212D File Offset: 0x0000032D
		internal virtual void OnBeforeRpc(Guid databaseGuid, IRopSummaryCollector ropSummaryCollector)
		{
			base.DatabaseGuid = databaseGuid;
			base.SummaryCollector = ((ropSummaryCollector != null) ? ropSummaryCollector : RopSummaryCollector.Null);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002148 File Offset: 0x00000348
		internal new void OnRpcBegin()
		{
			base.OnRpcBegin();
			if (this.adminExmonLogger != null)
			{
				JET_THREADSTATS threadStats;
				Factory.GetDatabaseThreadStats(out threadStats);
				this.adminExmonLogger.BeginRpcProcessing(threadStats);
			}
			ILogTransactionInformation logTransactionInformationBlock = new LogTransactionInformationAdmin(this.methodId);
			base.LogTransactionInformationCollector.AddLogTransactionInformationBlock(logTransactionInformationBlock);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		internal void OnRpcEnd(ErrorCodeValue errorCode)
		{
			base.OnRpcEnd();
			if (this.adminExmonLogger != null)
			{
				JET_THREADSTATS threadStats;
				Factory.GetDatabaseThreadStats(out threadStats);
				this.adminExmonLogger.EndRpcProcessing((uint)this.methodId, threadStats, (uint)errorCode);
			}
			base.DumpDiagnosticIfNeeded();
			base.OnEndRpc(OperationType.Admin, ClientActivityStrings.Admin, (byte)this.methodId, (uint)errorCode, false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021DF File Offset: 0x000003DF
		internal override void EnablePerClientTypePerfCounterUpdate()
		{
			base.EnablePerClientTypePerfCounterUpdate();
			if (base.PerClientPerfInstance != null)
			{
				base.PerClientPerfInstance.AdminRPCsInProgress.Increment();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002200 File Offset: 0x00000400
		internal override void DisablePerClientTypePerfCounterUpdate()
		{
			if (base.PerClientPerfInstance != null)
			{
				base.PerClientPerfInstance.AdminRPCsInProgress.Decrement();
				base.PerClientPerfInstance.AdminRPCsRateOfExecuteTask.Increment();
			}
			base.DisablePerClientTypePerfCounterUpdate();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002232 File Offset: 0x00000432
		protected override void FormatOperationInformation(TraceContentBuilder cb, int indentLevel)
		{
			base.FormatOperationInformation(cb, indentLevel);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "AdminMethod: " + this.methodId);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002258 File Offset: 0x00000458
		protected override void GetSummaryInformation(Guid correlationId, ref ExecutionDiagnostics.LongOperationSummary summary)
		{
			base.GetSummaryInformation(correlationId, ref summary);
			summary.OperationType = "Admin";
			summary.OperationName = this.methodId.ToString();
		}

		// Token: 0x04000039 RID: 57
		private readonly AdminMethod methodId;

		// Token: 0x0400003A RID: 58
		private AdminExMonLogger adminExmonLogger;
	}
}
