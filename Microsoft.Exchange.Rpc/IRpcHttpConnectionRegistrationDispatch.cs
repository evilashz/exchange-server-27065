using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001DE RID: 478
	internal interface IRpcHttpConnectionRegistrationDispatch
	{
		// Token: 0x06000A0B RID: 2571
		int Register(Guid associationGroupId, string token, string serverTarget, string sessionCookie, string clientIp, Guid requestId, out string failureMessage, out string failureDetails);

		// Token: 0x06000A0C RID: 2572
		int Unregister(Guid associationGroupId, Guid requestId);

		// Token: 0x06000A0D RID: 2573
		int Clear();
	}
}
