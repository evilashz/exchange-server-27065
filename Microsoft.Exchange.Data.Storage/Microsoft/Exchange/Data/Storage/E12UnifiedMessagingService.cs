using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D49 RID: 3401
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class E12UnifiedMessagingService : HttpService
	{
		// Token: 0x060075F0 RID: 30192 RVA: 0x00209BE1 File Offset: 0x00207DE1
		private E12UnifiedMessagingService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory) : base(serverInfo, ServiceType.UnifiedMessaging, url, clientAccessType, authenticationMethod, virtualDirectory)
		{
		}

		// Token: 0x060075F1 RID: 30193 RVA: 0x00209BF1 File Offset: 0x00207DF1
		internal static bool TryCreateUnifiedMessagingService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (virtualDirectory.IsE12UM)
			{
				service = new E12UnifiedMessagingService(serverInfo, url, clientAccessType, authenticationMethod, virtualDirectory);
				return true;
			}
			service = null;
			return false;
		}
	}
}
