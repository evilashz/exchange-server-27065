using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000045 RID: 69
	internal sealed class ClearDispatchTask : RpcHttpConnectionRegistrationDispatchTask
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000D05F File Offset: 0x0000B25F
		public ClearDispatchTask(RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch, CancelableAsyncCallback asyncCallback, object asyncState) : base("ClearDispatchTask", rpcHttpConnectionRegistrationDispatch, asyncCallback, asyncState)
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000D06F File Offset: 0x0000B26F
		internal override int? InternalExecute()
		{
			return new int?(base.RpcHttpConnectionRegistrationDispatch.EcClear());
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D081 File Offset: 0x0000B281
		public int End()
		{
			return base.CheckCompletion();
		}
	}
}
