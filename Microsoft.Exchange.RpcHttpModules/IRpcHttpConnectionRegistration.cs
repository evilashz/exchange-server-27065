using System;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000004 RID: 4
	public interface IRpcHttpConnectionRegistration
	{
		// Token: 0x06000008 RID: 8
		int Register(Guid associationGroupId, string token, string serverTarget, string sessionCookie, string clientIp, Guid requestId, out string failureMessage, out string failureDetails);

		// Token: 0x06000009 RID: 9
		void Unregister(Guid associationGroupId, Guid requestId);

		// Token: 0x0600000A RID: 10
		void Clear();
	}
}
