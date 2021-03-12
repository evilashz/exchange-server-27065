using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x0200010A RID: 266
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CentralServerLoadBalancingExtractorFactory : RegularLoadBalancingExtractorFactory
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x000164A2 File Offset: 0x000146A2
		public CentralServerLoadBalancingExtractorFactory(IClientFactory clientFactory, Band[] bands, IList<Guid> nonMovableOrgs, ILogger logger) : base(bands, nonMovableOrgs, logger)
		{
			this.clientFactory = clientFactory;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000164B5 File Offset: 0x000146B5
		protected override TopologyExtractor CreateDagExtractor(DirectoryDatabaseAvailabilityGroup directoryDag)
		{
			return ParallelParentContainerExtractor.CreateExtractor(directoryDag, this, base.Logger);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000164C4 File Offset: 0x000146C4
		protected override TopologyExtractor CreateServerExtractor(DirectoryServer directoryServer)
		{
			return new RemoteServerTopologyExtractor(directoryServer, this, base.Bands, this.clientFactory);
		}

		// Token: 0x04000316 RID: 790
		private readonly IClientFactory clientFactory;
	}
}
