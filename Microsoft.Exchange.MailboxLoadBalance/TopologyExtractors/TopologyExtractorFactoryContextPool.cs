using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000115 RID: 277
	internal class TopologyExtractorFactoryContextPool
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00017300 File Offset: 0x00015500
		public virtual TopologyExtractorFactoryContext GetContext(IClientFactory clientFactory, Band[] bands, IList<Guid> nonMovableOrgs, ILogger logger)
		{
			AnchorUtil.ThrowOnNullArgument(clientFactory, "clientFactory");
			TimeSpan instanceCacheExpirationPeriod = LoadBalanceADSettings.Instance.Value.IdleRunDelay + LoadBalanceADSettings.Instance.Value.IdleRunDelay;
			TopologyExtractorFactoryContext context2;
			lock (this.factoryInstances.GetSyncRoot<TopologyExtractorFactoryContextPool.ContextInstanceRecord>())
			{
				this.factoryInstances.RemoveAll((TopologyExtractorFactoryContextPool.ContextInstanceRecord factoryContext) => factoryContext.IsExpired(instanceCacheExpirationPeriod));
				TopologyExtractorFactoryContextPool.ContextInstanceRecord contextInstanceRecord = this.factoryInstances.FirstOrDefault((TopologyExtractorFactoryContextPool.ContextInstanceRecord context) => context.Matches(clientFactory, bands, nonMovableOrgs, logger));
				if (contextInstanceRecord == null)
				{
					contextInstanceRecord = new TopologyExtractorFactoryContextPool.ContextInstanceRecord(new TopologyExtractorFactoryContext(clientFactory, bands, nonMovableOrgs, logger));
					this.factoryInstances.Add(contextInstanceRecord);
				}
				contextInstanceRecord.UpdateLastAccessTimestamp();
				context2 = contextInstanceRecord.Context;
			}
			return context2;
		}

		// Token: 0x0400032A RID: 810
		private readonly List<TopologyExtractorFactoryContextPool.ContextInstanceRecord> factoryInstances = new List<TopologyExtractorFactoryContextPool.ContextInstanceRecord>();

		// Token: 0x02000116 RID: 278
		private class ContextInstanceRecord
		{
			// Token: 0x06000815 RID: 2069 RVA: 0x0001743F File Offset: 0x0001563F
			public ContextInstanceRecord(TopologyExtractorFactoryContext context)
			{
				this.Context = context;
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001744E File Offset: 0x0001564E
			// (set) Token: 0x06000817 RID: 2071 RVA: 0x00017456 File Offset: 0x00015656
			public TopologyExtractorFactoryContext Context { get; private set; }

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001745F File Offset: 0x0001565F
			// (set) Token: 0x06000819 RID: 2073 RVA: 0x00017467 File Offset: 0x00015667
			private DateTime LastAccess { get; set; }

			// Token: 0x0600081A RID: 2074 RVA: 0x00017470 File Offset: 0x00015670
			public bool IsExpired(TimeSpan expirationTimeout)
			{
				return this.LastAccess + expirationTimeout < TimeProvider.UtcNow;
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x00017488 File Offset: 0x00015688
			public bool Matches(IClientFactory clientFactory, Band[] bands, IList<Guid> nonMovableOrgs, ILogger logger)
			{
				return this.Context.Matches(clientFactory, bands, nonMovableOrgs, logger);
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x0001749A File Offset: 0x0001569A
			public void UpdateLastAccessTimestamp()
			{
				this.LastAccess = TimeProvider.UtcNow;
			}
		}
	}
}
