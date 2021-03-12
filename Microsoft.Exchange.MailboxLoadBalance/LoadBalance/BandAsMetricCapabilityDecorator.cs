using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x02000096 RID: 150
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BandAsMetricCapabilityDecorator : MissingCapabilityLoadBalanceClientDecorator
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0000E218 File Offset: 0x0000C418
		public BandAsMetricCapabilityDecorator(ILoadBalanceService service, LoadBalanceAnchorContext serviceContext, DirectoryServer targetServer) : base(service, targetServer)
		{
			AnchorUtil.ThrowOnNullArgument(serviceContext, "serviceContext");
			this.serviceContext = serviceContext;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000E234 File Offset: 0x0000C434
		public override DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryDatabase database)
		{
			DatabaseSizeInfo databaseSpaceData;
			using (IPhysicalDatabase physicalDatabaseConnection = this.serviceContext.ClientFactory.GetPhysicalDatabaseConnection(database))
			{
				databaseSpaceData = physicalDatabaseConnection.GetDatabaseSpaceData();
			}
			return databaseSpaceData;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000E278 File Offset: 0x0000C478
		public override LoadContainer GetLocalServerData(Band[] bands)
		{
			IList<Guid> nonMovableOrgsList = LoadBalanceUtils.GetNonMovableOrgsList(this.serviceContext.Settings);
			ILogger logger = this.serviceContext.Logger;
			TopologyExtractorFactoryContext context = this.serviceContext.TopologyExtractorFactoryContextPool.GetContext(this.serviceContext.ClientFactory, bands, nonMovableOrgsList, logger);
			TopologyExtractorFactory loadBalancingLocalFactory = context.GetLoadBalancingLocalFactory(true);
			return loadBalancingLocalFactory.GetExtractor(base.TargetServer).ExtractTopology();
		}

		// Token: 0x040001BB RID: 443
		private readonly LoadBalanceAnchorContext serviceContext;
	}
}
