using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000043 RID: 67
	internal sealed class RegisterDispatchTask : RpcHttpConnectionRegistrationDispatchTask
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000CF64 File Offset: 0x0000B164
		public RegisterDispatchTask(RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch, CancelableAsyncCallback asyncCallback, object asyncState, Guid associationGroupId, string token, string serverTarget, string sessionCookie, string clientIp, Guid requestId) : base("RegisterDispatchTask", rpcHttpConnectionRegistrationDispatch, asyncCallback, asyncState)
		{
			this.associationGroupId = associationGroupId;
			this.token = token;
			this.serverTarget = serverTarget;
			this.sessionCookie = sessionCookie;
			this.clientIp = clientIp;
			this.requestId = requestId;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		internal override int? InternalExecute()
		{
			return new int?(base.RpcHttpConnectionRegistrationDispatch.EcRegister(this.associationGroupId, this.token, this.serverTarget, this.sessionCookie, this.clientIp, this.requestId, out this.failureMessage, out this.failureDetails));
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		public int End(out string failureMessage, out string failureDetails)
		{
			int result = base.CheckCompletion();
			failureMessage = this.failureMessage;
			failureDetails = this.failureDetails;
			return result;
		}

		// Token: 0x04000131 RID: 305
		private readonly Guid associationGroupId;

		// Token: 0x04000132 RID: 306
		private readonly string token;

		// Token: 0x04000133 RID: 307
		private readonly string serverTarget;

		// Token: 0x04000134 RID: 308
		private readonly string sessionCookie;

		// Token: 0x04000135 RID: 309
		private readonly string clientIp;

		// Token: 0x04000136 RID: 310
		private readonly Guid requestId;

		// Token: 0x04000137 RID: 311
		private string failureMessage;

		// Token: 0x04000138 RID: 312
		private string failureDetails;
	}
}
