using System;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000147 RID: 327
	public class TaskExecutionDiagnostics : ExecutionDiagnostics
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x00040480 File Offset: 0x0003E680
		public TaskExecutionDiagnostics(TaskTypeId taskTypeId, Guid clientActivityId, string clientComponentName, string clientProtocolName, string clientActionString)
		{
			ILogTransactionInformation logTransactionInformationBlock = new LogTransactionInformationTask(taskTypeId);
			base.LogTransactionInformationCollector.AddLogTransactionInformationBlock(logTransactionInformationBlock);
			this.taskTypeId = taskTypeId;
			base.SetClientActivityInfo(clientActivityId, clientComponentName, clientProtocolName, clientActionString);
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x000404B9 File Offset: 0x0003E6B9
		public override byte OpNumber
		{
			get
			{
				return (byte)this.taskTypeId;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x000404C1 File Offset: 0x0003E6C1
		public TaskTypeId TaskTypeId
		{
			get
			{
				return this.taskTypeId;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x000404C9 File Offset: 0x0003E6C9
		public override uint TypeIdentifier
		{
			get
			{
				return 1U;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x000404CC File Offset: 0x0003E6CC
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x000404D4 File Offset: 0x0003E6D4
		internal TaskExMonLogger TaskExMonLogger
		{
			get
			{
				return this.taskExmonLogger;
			}
			set
			{
				this.taskExmonLogger = value;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000404DD File Offset: 0x0003E6DD
		protected override void FormatOperationInformation(TraceContentBuilder cb, int indentLevel)
		{
			base.FormatOperationInformation(cb, indentLevel);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "TaskId: " + this.taskTypeId);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00040503 File Offset: 0x0003E703
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ "Task".GetHashCode() ^ this.taskTypeId.GetHashCode();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00040527 File Offset: 0x0003E727
		internal virtual void OnBeforeTask(IRopSummaryCollector ropSummaryCollector)
		{
			base.SummaryCollector = ((ropSummaryCollector != null) ? ropSummaryCollector : RopSummaryCollector.Null);
			base.OnBeginOperation();
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00040540 File Offset: 0x0003E740
		internal void OnTaskEnd()
		{
			base.OnEndOperation(OperationType.Task, ClientActivityStrings.Task, (byte)this.taskTypeId, 0U, false);
			base.DumpDiagnosticIfNeeded();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0004055C File Offset: 0x0003E75C
		protected override void GetSummaryInformation(Guid correlationId, ref ExecutionDiagnostics.LongOperationSummary summary)
		{
			base.GetSummaryInformation(correlationId, ref summary);
			summary.OperationType = "Task";
			summary.OperationName = this.taskTypeId.ToString();
		}

		// Token: 0x0400072F RID: 1839
		private readonly TaskTypeId taskTypeId;

		// Token: 0x04000730 RID: 1840
		private TaskExMonLogger taskExmonLogger;
	}
}
