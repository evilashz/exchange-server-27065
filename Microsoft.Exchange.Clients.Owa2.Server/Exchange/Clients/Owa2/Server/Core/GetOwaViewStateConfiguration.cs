using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000320 RID: 800
	internal class GetOwaViewStateConfiguration : ServiceCommand<OwaViewStateConfiguration>
	{
		// Token: 0x06001A96 RID: 6806 RVA: 0x000636E6 File Offset: 0x000618E6
		public GetOwaViewStateConfiguration(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000636F0 File Offset: 0x000618F0
		protected override OwaViewStateConfiguration InternalExecute()
		{
			OwaViewStateConfiguration owaViewStateConfiguration = new OwaViewStateConfiguration();
			owaViewStateConfiguration.LoadAll(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession());
			return owaViewStateConfiguration;
		}
	}
}
