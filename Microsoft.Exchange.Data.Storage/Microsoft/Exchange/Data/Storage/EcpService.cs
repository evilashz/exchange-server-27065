using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D4A RID: 3402
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EcpService : HttpService
	{
		// Token: 0x060075F2 RID: 30194 RVA: 0x00209C10 File Offset: 0x00207E10
		private EcpService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory) : base(serverInfo, ServiceType.ExchangeControlPanel, url, clientAccessType, authenticationMethod, virtualDirectory)
		{
			this.LiveIdAuthentication = virtualDirectory.LiveIdAuthentication;
			this.MetabasePath = virtualDirectory.MetabasePath;
			this.AdminEnabled = virtualDirectory.AdminEnabled;
			this.OwaOptionsEnabled = virtualDirectory.OwaOptionsEnabled;
		}

		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x060075F3 RID: 30195 RVA: 0x00209C5F File Offset: 0x00207E5F
		// (set) Token: 0x060075F4 RID: 30196 RVA: 0x00209C67 File Offset: 0x00207E67
		public bool LiveIdAuthentication { get; private set; }

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x060075F5 RID: 30197 RVA: 0x00209C70 File Offset: 0x00207E70
		// (set) Token: 0x060075F6 RID: 30198 RVA: 0x00209C78 File Offset: 0x00207E78
		public string MetabasePath { get; private set; }

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x060075F7 RID: 30199 RVA: 0x00209C81 File Offset: 0x00207E81
		// (set) Token: 0x060075F8 RID: 30200 RVA: 0x00209C89 File Offset: 0x00207E89
		public bool AdminEnabled { get; private set; }

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x060075F9 RID: 30201 RVA: 0x00209C92 File Offset: 0x00207E92
		// (set) Token: 0x060075FA RID: 30202 RVA: 0x00209C9A File Offset: 0x00207E9A
		public bool OwaOptionsEnabled { get; private set; }

		// Token: 0x060075FB RID: 30203 RVA: 0x00209CA3 File Offset: 0x00207EA3
		internal static bool TryCreateEcpService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (virtualDirectory.IsEcp)
			{
				service = new EcpService(serverInfo, url, clientAccessType, authenticationMethod, virtualDirectory);
				return true;
			}
			service = null;
			return false;
		}
	}
}
