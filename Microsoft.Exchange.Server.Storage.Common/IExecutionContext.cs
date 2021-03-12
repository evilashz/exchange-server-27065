using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200003D RID: 61
	public interface IExecutionContext
	{
		// Token: 0x06000469 RID: 1129
		TOperationData RecordOperation<TOperationData>(IOperationExecutionTrackable operation) where TOperationData : class, IExecutionTrackingData<TOperationData>, new();

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600046A RID: 1130
		IExecutionDiagnostics Diagnostics { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600046B RID: 1131
		bool IsMailboxOperationStarted { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600046C RID: 1132
		bool SkipDatabaseLogsFlush { get; }

		// Token: 0x0600046D RID: 1133
		void OnDatabaseFailure(bool isCriticalFailure, LID lid);
	}
}
