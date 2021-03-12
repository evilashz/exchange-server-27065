using System;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000009 RID: 9
	internal interface ILiveIdAuthenticationFactory
	{
		// Token: 0x0600001E RID: 30
		ILiveIdBasicAuthentication CreateLiveIdAuthentication();
	}
}
