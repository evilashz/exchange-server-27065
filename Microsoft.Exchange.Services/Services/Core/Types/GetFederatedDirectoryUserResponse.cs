using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000079 RID: 121
	public class GetFederatedDirectoryUserResponse
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000F26C File Offset: 0x0000D46C
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000F274 File Offset: 0x0000D474
		public FederatedDirectoryGroupType[] Groups { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000F27D File Offset: 0x0000D47D
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000F285 File Offset: 0x0000D485
		public string PhotoUrl { get; set; }
	}
}
