using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000078 RID: 120
	public sealed class GetFederatedDirectoryGroupResponse : ResponseMessage
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000F242 File Offset: 0x0000D442
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000F24A File Offset: 0x0000D44A
		public FederatedDirectoryIdentityDetailsType[] Members { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000F253 File Offset: 0x0000D453
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000F25B File Offset: 0x0000D45B
		public FederatedDirectoryIdentityDetailsType[] Owners { get; set; }
	}
}
