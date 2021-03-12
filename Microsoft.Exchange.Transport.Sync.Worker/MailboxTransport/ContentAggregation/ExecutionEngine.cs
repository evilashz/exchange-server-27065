using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ExecutionEngine : IExecutionEngine
	{
		// Token: 0x06000331 RID: 817
		public abstract IAsyncResult BeginExecution(AggregationWorkItem workItem, AsyncCallback callback, object callbackState);

		// Token: 0x06000332 RID: 818
		public abstract AsyncOperationResult<SyncEngineResultData> EndExecution(IAsyncResult asyncResult);

		// Token: 0x06000333 RID: 819 RVA: 0x0000F369 File Offset: 0x0000D569
		public virtual void Cancel(IAsyncResult asyncResult)
		{
		}

		// Token: 0x040001B7 RID: 439
		internal static readonly Trace Tracer = ExTraceGlobals.SyncEngineTracer;
	}
}
