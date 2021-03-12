using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D46 RID: 3398
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AvailabilityForeignConnectorService : HttpService
	{
		// Token: 0x060075E2 RID: 30178 RVA: 0x00209838 File Offset: 0x00207A38
		private AvailabilityForeignConnectorService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory) : base(serverInfo, ServiceType.AvailabilityForeignConnector, url, clientAccessType, authenticationMethod, virtualDirectory)
		{
			this.AvailabilityForeignConnectorType = virtualDirectory.AvailabilityForeignConnectorType;
			this.AvailabilityForeignConnectorDomains = new ReadOnlyCollection<string>(virtualDirectory.AvailabilityForeignConnectorDomains.ToArray());
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x060075E3 RID: 30179 RVA: 0x0020986C File Offset: 0x00207A6C
		// (set) Token: 0x060075E4 RID: 30180 RVA: 0x00209874 File Offset: 0x00207A74
		public string AvailabilityForeignConnectorType { get; private set; }

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x060075E5 RID: 30181 RVA: 0x0020987D File Offset: 0x00207A7D
		// (set) Token: 0x060075E6 RID: 30182 RVA: 0x00209885 File Offset: 0x00207A85
		public ReadOnlyCollection<string> AvailabilityForeignConnectorDomains { get; private set; }

		// Token: 0x060075E7 RID: 30183 RVA: 0x0020988E File Offset: 0x00207A8E
		internal static bool TryCreateAvailabilityForeignConnectorService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (virtualDirectory.IsAvailabilityForeignConnector)
			{
				service = new AvailabilityForeignConnectorService(serverInfo, url, clientAccessType, authenticationMethod, virtualDirectory);
				return true;
			}
			service = null;
			return false;
		}
	}
}
