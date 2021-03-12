using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D52 RID: 3410
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiHttpService : HttpService
	{
		// Token: 0x06007622 RID: 30242 RVA: 0x0020A0A4 File Offset: 0x002082A4
		private MapiHttpService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, MiniVirtualDirectory virtualDirectory) : base(serverInfo, ServiceType.MapiHttp, url, clientAccessType, AuthenticationMethod.None, virtualDirectory)
		{
			this.LastConfigurationTime = (virtualDirectory.WhenChangedUTC ?? (virtualDirectory.WhenCreatedUTC ?? DateTime.MinValue));
		}

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x06007623 RID: 30243 RVA: 0x0020A0FE File Offset: 0x002082FE
		// (set) Token: 0x06007624 RID: 30244 RVA: 0x0020A106 File Offset: 0x00208306
		public DateTime LastConfigurationTime { get; private set; }

		// Token: 0x06007625 RID: 30245 RVA: 0x0020A10F File Offset: 0x0020830F
		public static bool TryCreateMapiHttpService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			EnumValidator.ThrowIfInvalid<ClientAccessType>(clientAccessType, "clientAccessType");
			if (virtualDirectory.IsMapi)
			{
				service = new MapiHttpService(serverInfo, url, clientAccessType, virtualDirectory);
				return true;
			}
			service = null;
			return false;
		}
	}
}
