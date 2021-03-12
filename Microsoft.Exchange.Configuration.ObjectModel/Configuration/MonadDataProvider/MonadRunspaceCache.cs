using System;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001DA RID: 474
	internal class MonadRunspaceCache : BasicRunspaceCache
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x000349CD File Offset: 0x00032BCD
		internal MonadRunspaceCache()
		{
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x000349D5 File Offset: 0x00032BD5
		internal ExDateTime LastTimeCacheUsed
		{
			get
			{
				return this.lastCacheAccess;
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000349DD File Offset: 0x00032BDD
		protected override bool AddRunspace(Runspace runspace)
		{
			this.lastCacheAccess = ExDateTime.UtcNow;
			return base.AddRunspace(runspace);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000349F1 File Offset: 0x00032BF1
		protected override Runspace RemoveRunspace()
		{
			this.lastCacheAccess = ExDateTime.UtcNow;
			return base.RemoveRunspace();
		}

		// Token: 0x040003CA RID: 970
		private ExDateTime lastCacheAccess;
	}
}
