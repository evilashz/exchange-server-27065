using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007D1 RID: 2001
	internal abstract class FullSyncPoller
	{
		// Token: 0x06006366 RID: 25446
		public abstract IEnumerable<string> GetFullSyncTenants();
	}
}
