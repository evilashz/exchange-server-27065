using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D57 RID: 3415
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Pop3Service : EmailTransportService
	{
		// Token: 0x06007647 RID: 30279 RVA: 0x0020A64F File Offset: 0x0020884F
		private Pop3Service(TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniEmailTransport emailTransport) : base(serverInfo, ServiceType.Pop3, clientAccessType, authenticationMethod, emailTransport)
		{
		}

		// Token: 0x06007648 RID: 30280 RVA: 0x0020A65E File Offset: 0x0020885E
		internal static bool TryCreatePop3Service(MiniEmailTransport emailTransport, TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (emailTransport.IsPop3)
			{
				service = new Pop3Service(serverInfo, clientAccessType, authenticationMethod, emailTransport);
				return true;
			}
			service = null;
			return false;
		}
	}
}
