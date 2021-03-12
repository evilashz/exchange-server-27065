using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000108 RID: 264
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CachingTopologyExtractorFactory : TopologyExtractorFactory
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x00016368 File Offset: 0x00014568
		public CachingTopologyExtractorFactory(ILogger logger, TopologyExtractorFactory realFactory) : base(logger)
		{
			this.realFactory = realFactory;
			this.extractors = new ConcurrentDictionary<Guid, TopologyExtractor>();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00016384 File Offset: 0x00014584
		public override TopologyExtractor GetExtractor(DirectoryObject directoryObject)
		{
			if (directoryObject is DirectoryMailbox)
			{
				return this.realFactory.GetExtractor(directoryObject);
			}
			TopologyExtractor value;
			if (!this.extractors.TryGetValue(directoryObject.Guid, out value))
			{
				TopologyExtractor extractor = this.realFactory.GetExtractor(directoryObject);
				if (extractor != null)
				{
					value = new CachingTopologyExtractor(this.realFactory, directoryObject, base.Logger, extractor, SimpleTimer.CreateTimer(LoadBalanceADSettings.Instance.Value.LocalCacheRefreshPeriod));
				}
				this.extractors.TryAdd(directoryObject.Guid, value);
			}
			return this.extractors[directoryObject.Guid];
		}

		// Token: 0x04000312 RID: 786
		private readonly ConcurrentDictionary<Guid, TopologyExtractor> extractors;

		// Token: 0x04000313 RID: 787
		private readonly TopologyExtractorFactory realFactory;
	}
}
