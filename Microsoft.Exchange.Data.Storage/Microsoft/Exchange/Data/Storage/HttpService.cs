using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D45 RID: 3397
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class HttpService : Service
	{
		// Token: 0x060075D8 RID: 30168 RVA: 0x00209741 File Offset: 0x00207941
		internal HttpService(TopologyServerInfo serverInfo, ServiceType serviceType, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory) : base(serverInfo, serviceType, clientAccessType, authenticationMethod)
		{
			this.Url = url;
			this.IsFrontEnd = HttpService.IsFrontEndRole(virtualDirectory, serverInfo);
			this.ADObjectId = virtualDirectory.Id;
		}

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x060075D9 RID: 30169 RVA: 0x00209771 File Offset: 0x00207971
		// (set) Token: 0x060075DA RID: 30170 RVA: 0x00209779 File Offset: 0x00207979
		public ADObjectId ADObjectId { get; private set; }

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x060075DB RID: 30171 RVA: 0x00209782 File Offset: 0x00207982
		// (set) Token: 0x060075DC RID: 30172 RVA: 0x0020978A File Offset: 0x0020798A
		public Uri Url { get; private set; }

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x060075DD RID: 30173 RVA: 0x00209793 File Offset: 0x00207993
		// (set) Token: 0x060075DE RID: 30174 RVA: 0x0020979B File Offset: 0x0020799B
		public bool IsFrontEnd { get; private set; }

		// Token: 0x060075DF RID: 30175 RVA: 0x002097A4 File Offset: 0x002079A4
		public override string ToString()
		{
			return string.Format("Service. Type = {0}. ClientAccessType = {1}. Url = {2}. AuthenticationMethod = {3}. IsFrontEnd = {4}", new object[]
			{
				base.ServiceType,
				base.ClientAccessType,
				this.Url,
				base.AuthenticationMethod,
				this.IsFrontEnd
			});
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x00209804 File Offset: 0x00207A04
		internal static bool IsFrontEndRole(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo)
		{
			return (serverInfo.Role & ServerRole.Cafe) == ServerRole.Cafe && !virtualDirectory.Name.Contains("(Exchange Back End)");
		}

		// Token: 0x060075E1 RID: 30177 RVA: 0x00209826 File Offset: 0x00207A26
		internal static bool TryCreateHttpService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			service = new HttpService(serverInfo, ServiceType.Invalid, url, clientAccessType, authenticationMethod, virtualDirectory);
			return true;
		}

		// Token: 0x040051C2 RID: 20930
		private const string BackEndNameSegment = "(Exchange Back End)";
	}
}
