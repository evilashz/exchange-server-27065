using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D76 RID: 3446
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WebServicesService : HttpService
	{
		// Token: 0x06007708 RID: 30472 RVA: 0x0020DA0E File Offset: 0x0020BC0E
		private WebServicesService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory) : base(serverInfo, ServiceType.WebServices, url, clientAccessType, authenticationMethod, virtualDirectory)
		{
			this.ServerDistinguishedName = virtualDirectory.Server.DistinguishedName;
			this.MRSProxyEnabled = virtualDirectory.MRSProxyEnabled;
		}

		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x06007709 RID: 30473 RVA: 0x0020DA3D File Offset: 0x0020BC3D
		// (set) Token: 0x0600770A RID: 30474 RVA: 0x0020DA45 File Offset: 0x0020BC45
		public string ServerDistinguishedName { get; private set; }

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x0600770B RID: 30475 RVA: 0x0020DA4E File Offset: 0x0020BC4E
		// (set) Token: 0x0600770C RID: 30476 RVA: 0x0020DA56 File Offset: 0x0020BC56
		public bool MRSProxyEnabled { get; private set; }

		// Token: 0x0600770D RID: 30477 RVA: 0x0020DA5F File Offset: 0x0020BC5F
		public static bool TryCreateWebServicesService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			EnumValidator.ThrowIfInvalid<ClientAccessType>(clientAccessType, "clientAccessType");
			EnumValidator.ThrowIfInvalid<AuthenticationMethod>(authenticationMethod, "authenticationMethod");
			if (virtualDirectory.IsWebServices)
			{
				service = new WebServicesService(serverInfo, url, clientAccessType, authenticationMethod, virtualDirectory);
				return true;
			}
			service = null;
			return false;
		}
	}
}
