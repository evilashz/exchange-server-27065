using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200006D RID: 109
	public class NullExecutionContext : IExecutionContext
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x000108FE File Offset: 0x0000EAFE
		public static IExecutionContext Instance
		{
			get
			{
				return NullExecutionContext.context;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00010908 File Offset: 0x0000EB08
		public TOperationData RecordOperation<TOperationData>(IOperationExecutionTrackable operation) where TOperationData : class, IExecutionTrackingData<TOperationData>, new()
		{
			return default(TOperationData);
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001091E File Offset: 0x0000EB1E
		public IExecutionDiagnostics Diagnostics
		{
			get
			{
				return NullExecutionContext.diagnostics;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00010925 File Offset: 0x0000EB25
		public bool IsMailboxOperationStarted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00010928 File Offset: 0x0000EB28
		public bool SkipDatabaseLogsFlush
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001092B File Offset: 0x0000EB2B
		public void OnDatabaseFailure(bool isCriticalFailure, LID lid)
		{
		}

		// Token: 0x04000601 RID: 1537
		private static IExecutionContext context = new NullExecutionContext();

		// Token: 0x04000602 RID: 1538
		private static IExecutionDiagnostics diagnostics = new NullExecutionDiagnostics();
	}
}
