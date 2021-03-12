using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000318 RID: 792
	internal class GetInstantMessageProxySettings : InstantMessageCommandBase<ProxySettings[]>
	{
		// Token: 0x06001A54 RID: 6740 RVA: 0x00060E3A File Offset: 0x0005F03A
		public GetInstantMessageProxySettings(CallContext callContext, string[] userPrincipalNames) : base(callContext)
		{
			ExAssert.RetailAssert(userPrincipalNames != null, "userPrincipalNames is null");
			this.userPrincipalNames = userPrincipalNames;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00060E5C File Offset: 0x0005F05C
		protected override ProxySettings[] InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			if (this.userPrincipalNames.Length > 0)
			{
				return InstantMessageUtilities.GetProxySettings(this.userPrincipalNames, userContext);
			}
			return new ProxySettings[0];
		}

		// Token: 0x04000E94 RID: 3732
		private string[] userPrincipalNames;
	}
}
