using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000044 RID: 68
	internal sealed class UnregisterDispatchTask : RpcHttpConnectionRegistrationDispatchTask
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000D019 File Offset: 0x0000B219
		public UnregisterDispatchTask(RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch, CancelableAsyncCallback asyncCallback, object asyncState, Guid associationGroupId, Guid requestId) : base("UnregisterDispatchTask", rpcHttpConnectionRegistrationDispatch, asyncCallback, asyncState)
		{
			this.associationGroupId = associationGroupId;
			this.requestId = requestId;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D039 File Offset: 0x0000B239
		internal override int? InternalExecute()
		{
			return new int?(base.RpcHttpConnectionRegistrationDispatch.EcUnregister(this.associationGroupId, this.requestId));
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000D057 File Offset: 0x0000B257
		public int End()
		{
			return base.CheckCompletion();
		}

		// Token: 0x04000139 RID: 313
		private readonly Guid associationGroupId;

		// Token: 0x0400013A RID: 314
		private readonly Guid requestId;
	}
}
