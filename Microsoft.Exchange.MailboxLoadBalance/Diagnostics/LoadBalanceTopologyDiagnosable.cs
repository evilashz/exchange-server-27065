using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceTopologyDiagnosable : LoadBalanceDiagnosableBase<LoadBalanceTopologyDiagnosableArgument, LoadContainer>
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000A3E2 File Offset: 0x000085E2
		public LoadBalanceTopologyDiagnosable(LoadBalanceAnchorContext loadBalanceContext) : base(loadBalanceContext.Logger)
		{
			this.context = loadBalanceContext;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000A3F8 File Offset: 0x000085F8
		protected Band[] Bands
		{
			get
			{
				if (this.bands == null)
				{
					using (ILoadBalanceService loadBalanceClientForCentralServer = this.context.ClientFactory.GetLoadBalanceClientForCentralServer())
					{
						this.bands = loadBalanceClientForCentralServer.GetActiveBands();
					}
				}
				return this.bands;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A44C File Offset: 0x0000864C
		protected override LoadContainer ProcessDiagnostic()
		{
			if (base.Arguments.ShowDatabase)
			{
				return this.GetDatabaseContainer();
			}
			if (base.Arguments.ShowServer)
			{
				return this.GetServerContainer();
			}
			if (base.Arguments.ShowDag)
			{
				return this.GetDagContainer();
			}
			if (base.Arguments.ShowForest)
			{
				return this.GetForestContainer();
			}
			if (base.Arguments.ShowForestHeatMap)
			{
				return this.context.HeatMap.GetLoadTopology();
			}
			if (base.Arguments.ShowLocalServerHeatMap)
			{
				return this.context.LocalServerHeatMap.GetLoadTopology();
			}
			return null;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000A500 File Offset: 0x00008700
		private LoadContainer GetDagContainer()
		{
			DirectoryDatabaseAvailabilityGroup directoryDatabaseAvailabilityGroup = this.context.Directory.GetDatabaseAvailabilityGroups().FirstOrDefault((DirectoryDatabaseAvailabilityGroup d) => d.Guid == base.Arguments.DagGuid);
			if (directoryDatabaseAvailabilityGroup == null)
			{
				throw new DagNotFoundException(base.Arguments.DagGuid.ToString());
			}
			return this.GetTopologyForDirectoryObject(directoryDatabaseAvailabilityGroup);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A558 File Offset: 0x00008758
		private LoadContainer GetDatabaseContainer()
		{
			DirectoryDatabase database = this.context.Directory.GetDatabase(base.Arguments.DatabaseGuid);
			if (database == null)
			{
				throw new DatabaseNotFoundPermanentException(base.Arguments.DatabaseGuid.ToString());
			}
			return this.GetTopologyForDirectoryObject(database);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000A5AC File Offset: 0x000087AC
		private TopologyExtractorFactory GetExtractorFactory()
		{
			TopologyExtractorFactoryContext topologyExtractorFactoryContext = this.context.TopologyExtractorFactoryContextPool.GetContext(this.context.ClientFactory, this.Bands, Array<Guid>.Empty, base.Logger);
			if (!base.Arguments.Verbose)
			{
				return topologyExtractorFactoryContext.GetLoadBalancingCentralFactory();
			}
			return topologyExtractorFactoryContext.GetEntitySelectorFactory();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000A600 File Offset: 0x00008800
		private LoadContainer GetForestContainer()
		{
			DirectoryForest localForest = this.context.Directory.GetLocalForest();
			return this.GetTopologyForDirectoryObject(localForest);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000A628 File Offset: 0x00008828
		private LoadContainer GetServerContainer()
		{
			DirectoryServer server = this.context.Directory.GetServer(base.Arguments.ServerGuid);
			if (server == null)
			{
				throw new ServerNotFoundException(base.Arguments.ServerGuid.ToString());
			}
			return this.GetTopologyForDirectoryObject(server);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A67A File Offset: 0x0000887A
		private LoadContainer GetTopologyForDirectoryObject(DirectoryObject directoryObject)
		{
			return this.GetExtractorFactory().GetExtractor(directoryObject).ExtractTopology();
		}

		// Token: 0x04000116 RID: 278
		private readonly LoadBalanceAnchorContext context;

		// Token: 0x04000117 RID: 279
		private Band[] bands;
	}
}
