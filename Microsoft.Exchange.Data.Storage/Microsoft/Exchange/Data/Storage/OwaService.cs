using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D55 RID: 3413
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OwaService : HttpService
	{
		// Token: 0x06007634 RID: 30260 RVA: 0x0020A278 File Offset: 0x00208478
		private OwaService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory) : base(serverInfo, ServiceType.OutlookWebAccess, url, clientAccessType, authenticationMethod, virtualDirectory)
		{
			this.AnonymousFeaturesEnabled = (virtualDirectory.AnonymousFeaturesEnabled == true);
			this.FailbackUrl = virtualDirectory.FailbackUrl;
			this.IntegratedFeaturesEnabled = (virtualDirectory.IntegratedFeaturesEnabled != null && virtualDirectory.IntegratedFeaturesEnabled != null && virtualDirectory.IntegratedFeaturesEnabled.Value);
		}

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x06007635 RID: 30261 RVA: 0x0020A2FB File Offset: 0x002084FB
		// (set) Token: 0x06007636 RID: 30262 RVA: 0x0020A303 File Offset: 0x00208503
		public bool IntegratedFeaturesEnabled { get; private set; }

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x06007637 RID: 30263 RVA: 0x0020A30C File Offset: 0x0020850C
		// (set) Token: 0x06007638 RID: 30264 RVA: 0x0020A314 File Offset: 0x00208514
		public bool AnonymousFeaturesEnabled { get; private set; }

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x06007639 RID: 30265 RVA: 0x0020A31D File Offset: 0x0020851D
		// (set) Token: 0x0600763A RID: 30266 RVA: 0x0020A325 File Offset: 0x00208525
		public Uri FailbackUrl { get; private set; }

		// Token: 0x0600763B RID: 30267 RVA: 0x0020A32E File Offset: 0x0020852E
		internal static bool TryCreateOwaService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (virtualDirectory.IsOwa)
			{
				service = new OwaService(serverInfo, url, clientAccessType, authenticationMethod, virtualDirectory);
				return true;
			}
			service = null;
			return false;
		}
	}
}
