using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200012C RID: 300
	internal class TokenRenewQueryContext : QueryContext
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x000328E3 File Offset: 0x00030AE3
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x000328EB File Offset: 0x00030AEB
		internal List<TokenRenewRequestAsset> TokenRenewRequestAssets { get; set; }
	}
}
