using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000113 RID: 275
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoteServerTopologyExtractor : TopologyExtractor
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x00017036 File Offset: 0x00015236
		public RemoteServerTopologyExtractor(DirectoryObject directoryObject, TopologyExtractorFactory extractorFactory, Band[] bands, IClientFactory clientFactory) : base(directoryObject, extractorFactory)
		{
			this.bands = bands;
			this.clientFactory = clientFactory;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00017050 File Offset: 0x00015250
		public override LoadContainer ExtractTopology()
		{
			LoadContainer localServerData;
			using (ILoadBalanceService loadBalanceClientForServer = this.clientFactory.GetLoadBalanceClientForServer((DirectoryServer)base.DirectoryObject, true))
			{
				localServerData = loadBalanceClientForServer.GetLocalServerData(this.bands);
			}
			return localServerData;
		}

		// Token: 0x0400031F RID: 799
		private readonly Band[] bands;

		// Token: 0x04000320 RID: 800
		private readonly IClientFactory clientFactory;
	}
}
