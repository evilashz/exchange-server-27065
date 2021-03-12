using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x0200010E RID: 270
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LocalLoadBalancingWithEntitiesExtractorFactory : RegularLoadBalancingExtractorFactory
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x00016793 File Offset: 0x00014993
		public LocalLoadBalancingWithEntitiesExtractorFactory(Band[] bands, IList<Guid> nonMovableOrgs, ILogger logger) : base(bands, nonMovableOrgs, logger)
		{
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001679E File Offset: 0x0001499E
		protected override TopologyExtractor CreateServerExtractor(DirectoryServer directoryServer)
		{
			return ParallelParentContainerExtractor.CreateExtractor(directoryServer, this, base.Logger);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000167AD File Offset: 0x000149AD
		protected override TopologyExtractor CreateDatabaseExtractor(DirectoryDatabase database)
		{
			return new DetailedMailboxInfoDatabaseExtractor(database, this, base.NonMovableOrganizations);
		}
	}
}
