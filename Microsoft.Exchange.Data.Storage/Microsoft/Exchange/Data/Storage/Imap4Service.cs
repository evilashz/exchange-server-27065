using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D51 RID: 3409
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Imap4Service : EmailTransportService
	{
		// Token: 0x06007620 RID: 30240 RVA: 0x0020A078 File Offset: 0x00208278
		private Imap4Service(TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniEmailTransport emailTransport) : base(serverInfo, ServiceType.Imap4, clientAccessType, authenticationMethod, emailTransport)
		{
		}

		// Token: 0x06007621 RID: 30241 RVA: 0x0020A087 File Offset: 0x00208287
		internal static bool TryCreateImap4Service(MiniEmailTransport emailTransport, TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (emailTransport.IsImap4)
			{
				service = new Imap4Service(serverInfo, clientAccessType, authenticationMethod, emailTransport);
				return true;
			}
			service = null;
			return false;
		}
	}
}
