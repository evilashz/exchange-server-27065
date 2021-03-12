using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;

namespace Microsoft.Exchange.Directory.TopologyService.AsyncContexts
{
	// Token: 0x02000022 RID: 34
	[DebuggerDisplay("{DomainId.ToString()}")]
	internal class DiscoverServerFromRemoteDomainContext
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000A73A File Offset: 0x0000893A
		public DiscoverServerFromRemoteDomainContext(ADObjectId domainId)
		{
			this.DomainId = domainId;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000A749 File Offset: 0x00008949
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000A751 File Offset: 0x00008951
		public ADObjectId DomainId { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000A75A File Offset: 0x0000895A
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000A762 File Offset: 0x00008962
		public ServerInfo ServerInfoResult { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000A76B File Offset: 0x0000896B
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000A773 File Offset: 0x00008973
		public Exception DiscoveryException { get; set; }
	}
}
