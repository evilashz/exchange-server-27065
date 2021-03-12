using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D53 RID: 3411
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MobileSyncService : HttpService
	{
		// Token: 0x06007626 RID: 30246 RVA: 0x0020A138 File Offset: 0x00208338
		private MobileSyncService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory) : base(serverInfo, ServiceType.MobileSync, url, clientAccessType, authenticationMethod, virtualDirectory)
		{
			this.VirtualDirectoryIdentity = virtualDirectory.ToString();
			this.IsCertEnrollEnabled = virtualDirectory.MobileClientCertificateProvisioningEnabled;
			this.CertificateAuthorityUrl = virtualDirectory.MobileClientCertificateAuthorityURL;
			this.CertEnrollTemplateName = virtualDirectory.MobileClientCertTemplateName;
		}

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x06007627 RID: 30247 RVA: 0x0020A187 File Offset: 0x00208387
		// (set) Token: 0x06007628 RID: 30248 RVA: 0x0020A18F File Offset: 0x0020838F
		public string VirtualDirectoryIdentity { get; private set; }

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x06007629 RID: 30249 RVA: 0x0020A198 File Offset: 0x00208398
		// (set) Token: 0x0600762A RID: 30250 RVA: 0x0020A1A0 File Offset: 0x002083A0
		public string CertificateAuthorityUrl { get; private set; }

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x0600762B RID: 30251 RVA: 0x0020A1A9 File Offset: 0x002083A9
		// (set) Token: 0x0600762C RID: 30252 RVA: 0x0020A1B1 File Offset: 0x002083B1
		public string CertEnrollTemplateName { get; private set; }

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x0600762D RID: 30253 RVA: 0x0020A1BA File Offset: 0x002083BA
		// (set) Token: 0x0600762E RID: 30254 RVA: 0x0020A1C2 File Offset: 0x002083C2
		public bool IsCertEnrollEnabled { get; private set; }

		// Token: 0x0600762F RID: 30255 RVA: 0x0020A1CB File Offset: 0x002083CB
		internal static bool TryCreateMobileSyncService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (virtualDirectory.IsMobile)
			{
				service = new MobileSyncService(serverInfo, url, clientAccessType, authenticationMethod, virtualDirectory);
				return true;
			}
			service = null;
			return false;
		}
	}
}
