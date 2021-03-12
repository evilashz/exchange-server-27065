using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IExecutionEngine
	{
		// Token: 0x0600032E RID: 814
		IAsyncResult BeginExecution(AggregationWorkItem workItem, AsyncCallback callback, object callbackState);

		// Token: 0x0600032F RID: 815
		AsyncOperationResult<SyncEngineResultData> EndExecution(IAsyncResult asyncResult);

		// Token: 0x06000330 RID: 816
		void Cancel(IAsyncResult asyncResult);
	}
}
