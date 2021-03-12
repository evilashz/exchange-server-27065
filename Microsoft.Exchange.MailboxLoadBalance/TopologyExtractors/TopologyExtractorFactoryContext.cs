using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000114 RID: 276
	internal class TopologyExtractorFactoryContext
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x000170A0 File Offset: 0x000152A0
		public TopologyExtractorFactoryContext(IClientFactory clientFactory, Band[] bands, IList<Guid> nonMovableOrgs, ILogger logger)
		{
			this.clientFactory = clientFactory;
			this.bands = (bands ?? Array<Band>.Empty);
			this.nonMovableOrgs = nonMovableOrgs;
			this.Logger = logger;
			this.localFactory = new Lazy<TopologyExtractorFactory>(new Func<TopologyExtractorFactory>(this.CreateLocalLoadBalancingFactory));
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000170F1 File Offset: 0x000152F1
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000170F9 File Offset: 0x000152F9
		private protected ILogger Logger { protected get; private set; }

		// Token: 0x06000808 RID: 2056 RVA: 0x00017102 File Offset: 0x00015302
		public virtual TopologyExtractorFactory GetEntitySelectorFactory()
		{
			return this.CreateEntitySelectorFactory();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001710A File Offset: 0x0001530A
		public virtual TopologyExtractorFactory GetLoadBalancingCentralFactory()
		{
			return new CentralServerLoadBalancingExtractorFactory(this.clientFactory, this.bands, this.nonMovableOrgs, this.Logger);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00017129 File Offset: 0x00015329
		public virtual TopologyExtractorFactory GetLoadBalancingLocalFactory(bool ignoreCache = false)
		{
			if (LoadBalanceADSettings.Instance.Value.LocalCacheRefreshPeriod == TimeSpan.Zero || ignoreCache)
			{
				return this.CreateRegularLoadBalancingLocalFactory();
			}
			return this.localFactory.Value;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00017174 File Offset: 0x00015374
		public bool Matches(IClientFactory requestedClientFactory, IEnumerable<Band> requestedBands, IEnumerable<Guid> requestedNonMovableOrgs, ILogger requestedLogger)
		{
			if (!object.Equals(this.clientFactory, requestedClientFactory))
			{
				return false;
			}
			Band[] array = (requestedBands == null) ? Array<Band>.Empty : ((requestedBands as Band[]) ?? requestedBands.ToArray<Band>());
			if (this.bands.IsNullOrEmpty<Band>() && !array.IsNullOrEmpty<Band>())
			{
				return false;
			}
			if (!array.IsNullOrEmpty<Band>())
			{
				if (!(from band in this.bands
				orderby band.Name
				select band).SequenceEqual(from band in array
				orderby band.Name
				select band))
				{
					return false;
				}
			}
			return (from guid in this.nonMovableOrgs
			orderby guid
			select guid).SequenceEqual(from guid in requestedNonMovableOrgs
			orderby guid
			select guid) && object.Equals(this.Logger, requestedLogger);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00017285 File Offset: 0x00015485
		protected virtual TopologyExtractorFactory CreateEntitySelectorFactory()
		{
			return new LocalLoadBalancingWithEntitiesExtractorFactory(this.bands, this.nonMovableOrgs, this.Logger);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001729E File Offset: 0x0001549E
		protected virtual TopologyExtractorFactory CreateLocalLoadBalancingFactory()
		{
			return new CachingTopologyExtractorFactory(this.Logger, this.CreateRegularLoadBalancingLocalFactory());
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000172B1 File Offset: 0x000154B1
		protected virtual TopologyExtractorFactory CreateRegularLoadBalancingLocalFactory()
		{
			return new RegularLoadBalancingExtractorFactory(this.bands, this.nonMovableOrgs, this.Logger);
		}

		// Token: 0x04000321 RID: 801
		private readonly Band[] bands;

		// Token: 0x04000322 RID: 802
		private readonly IClientFactory clientFactory;

		// Token: 0x04000323 RID: 803
		private readonly Lazy<TopologyExtractorFactory> localFactory;

		// Token: 0x04000324 RID: 804
		private readonly IList<Guid> nonMovableOrgs;
	}
}
