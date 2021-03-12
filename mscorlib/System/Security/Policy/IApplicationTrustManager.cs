using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200032E RID: 814
	[ComVisible(true)]
	public interface IApplicationTrustManager : ISecurityEncodable
	{
		// Token: 0x06002959 RID: 10585
		ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context);
	}
}
