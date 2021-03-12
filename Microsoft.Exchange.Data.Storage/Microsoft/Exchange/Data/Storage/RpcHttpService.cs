using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D5A RID: 3418
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcHttpService : HttpService
	{
		// Token: 0x0600764C RID: 30284 RVA: 0x0020A6A4 File Offset: 0x002088A4
		private RpcHttpService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory) : base(serverInfo, ServiceType.RpcHttp, url, clientAccessType, authenticationMethod, virtualDirectory)
		{
			this.IISAuthenticationMethods = Service.ConvertToReadOnlyCollection<AuthenticationMethod>(virtualDirectory.IISAuthenticationMethods);
			this.ExternalClientAuthenticationMethod = virtualDirectory.ExternalClientAuthenticationMethod;
			this.InternalClientAuthenticationMethod = virtualDirectory.InternalClientAuthenticationMethod;
			this.XropUrl = virtualDirectory.XropUrl;
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x0600764D RID: 30285 RVA: 0x0020A6F8 File Offset: 0x002088F8
		// (set) Token: 0x0600764E RID: 30286 RVA: 0x0020A700 File Offset: 0x00208900
		public Uri XropUrl { get; private set; }

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x0600764F RID: 30287 RVA: 0x0020A709 File Offset: 0x00208909
		// (set) Token: 0x06007650 RID: 30288 RVA: 0x0020A711 File Offset: 0x00208911
		public ReadOnlyCollection<AuthenticationMethod> IISAuthenticationMethods { get; private set; }

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x06007651 RID: 30289 RVA: 0x0020A71A File Offset: 0x0020891A
		// (set) Token: 0x06007652 RID: 30290 RVA: 0x0020A722 File Offset: 0x00208922
		public AuthenticationMethod ExternalClientAuthenticationMethod { get; private set; }

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x06007653 RID: 30291 RVA: 0x0020A72B File Offset: 0x0020892B
		// (set) Token: 0x06007654 RID: 30292 RVA: 0x0020A733 File Offset: 0x00208933
		public AuthenticationMethod InternalClientAuthenticationMethod { get; private set; }

		// Token: 0x06007655 RID: 30293 RVA: 0x0020A73C File Offset: 0x0020893C
		public static bool TryCreateRpcHttpService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (virtualDirectory.IsRpcHttp)
			{
				service = new RpcHttpService(serverInfo, url, clientAccessType, authenticationMethod, virtualDirectory);
				return true;
			}
			service = null;
			return false;
		}
	}
}
