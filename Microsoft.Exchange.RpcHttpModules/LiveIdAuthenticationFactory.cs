using System;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x0200000C RID: 12
	internal class LiveIdAuthenticationFactory : ILiveIdAuthenticationFactory
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002524 File Offset: 0x00000724
		public ILiveIdBasicAuthentication CreateLiveIdAuthentication()
		{
			return new LiveIdBasicAuthentication
			{
				ApplicationName = "Microsoft.Exchange.Rpc.BackEnd"
			};
		}

		// Token: 0x0400000A RID: 10
		internal const string ApplicationName = "Microsoft.Exchange.Rpc.BackEnd";
	}
}
