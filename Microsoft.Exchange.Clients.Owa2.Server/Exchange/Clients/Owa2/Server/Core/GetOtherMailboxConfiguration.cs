using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200031C RID: 796
	internal class GetOtherMailboxConfiguration : ServiceCommand<OwaOtherMailboxConfiguration>
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x000622CF File Offset: 0x000604CF
		public GetOtherMailboxConfiguration(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x000622D8 File Offset: 0x000604D8
		protected override OwaOtherMailboxConfiguration InternalExecute()
		{
			OwaOtherMailboxConfiguration owaOtherMailboxConfiguration = new OwaOtherMailboxConfiguration();
			owaOtherMailboxConfiguration.LoadAll(base.CallContext);
			return owaOtherMailboxConfiguration;
		}
	}
}
