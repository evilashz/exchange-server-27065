using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200012E RID: 302
	internal class UpdateQueryContext : QueryContext
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0003297B File Offset: 0x00030B7B
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x00032983 File Offset: 0x00030B83
		internal Dictionary<string, UpdateRequestAsset> UpdateRequestAssets { get; set; }
	}
}
