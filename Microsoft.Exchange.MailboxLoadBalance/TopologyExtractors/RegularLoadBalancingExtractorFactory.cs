using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000109 RID: 265
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RegularLoadBalancingExtractorFactory : TopologyExtractorFactory
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x00016417 File Offset: 0x00014617
		public RegularLoadBalancingExtractorFactory(Band[] bands, IList<Guid> nonMovableOrganizations, ILogger logger) : base(logger)
		{
			AnchorUtil.ThrowOnNullArgument(bands, "bands");
			AnchorUtil.ThrowOnNullArgument(nonMovableOrganizations, "nonMovableOrganizations");
			this.Bands = bands;
			this.NonMovableOrganizations = nonMovableOrganizations;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00016444 File Offset: 0x00014644
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x0001644C File Offset: 0x0001464C
		private protected Band[] Bands { protected get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00016455 File Offset: 0x00014655
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0001645D File Offset: 0x0001465D
		private protected IList<Guid> NonMovableOrganizations { protected get; private set; }

		// Token: 0x060007DF RID: 2015 RVA: 0x00016466 File Offset: 0x00014666
		protected override TopologyExtractor CreateContainerParentExtractor(DirectoryContainerParent container)
		{
			return new ParentContainerExtractor(container, this, base.Logger);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00016475 File Offset: 0x00014675
		protected override TopologyExtractor CreateMailboxExtractor(DirectoryMailbox mailbox)
		{
			return new MailboxEntityExtractor(mailbox, this, this.Bands);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00016484 File Offset: 0x00014684
		protected override TopologyExtractor CreateDatabaseExtractor(DirectoryDatabase database)
		{
			return new FullMailboxInfoDatabaseTopologyExtractor(database, this, this.NonMovableOrganizations);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00016493 File Offset: 0x00014693
		protected override TopologyExtractor CreateServerExtractor(DirectoryServer directoryServer)
		{
			return ParallelParentContainerExtractor.CreateExtractor(directoryServer, this, base.Logger);
		}
	}
}
