using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001DD RID: 477
	internal interface IRpcHttpConnectionRegistrationAsyncDispatch
	{
		// Token: 0x06000A05 RID: 2565
		ICancelableAsyncResult BeginRegister(Guid associationGroupId, string token, string serverTarget, string sessionCookie, string clientIp, Guid requestId, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A06 RID: 2566
		int EndRegister(ICancelableAsyncResult result, out string failureMessage, out string failureDetails);

		// Token: 0x06000A07 RID: 2567
		ICancelableAsyncResult BeginUnregister(Guid associationGroupId, Guid requestId, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A08 RID: 2568
		int EndUnregister(ICancelableAsyncResult result);

		// Token: 0x06000A09 RID: 2569
		ICancelableAsyncResult BeginClear(CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A0A RID: 2570
		int EndClear(ICancelableAsyncResult result);
	}
}
