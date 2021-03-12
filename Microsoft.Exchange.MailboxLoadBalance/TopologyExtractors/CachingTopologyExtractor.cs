using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000106 RID: 262
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CachingTopologyExtractor : TopologyExtractor
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x00016104 File Offset: 0x00014304
		public CachingTopologyExtractor(TopologyExtractorFactory topologyExtractorFactory, DirectoryObject directoryObject, ILogger logger, TopologyExtractor topologyExtractor, ITimer timer) : base(directoryObject, topologyExtractorFactory)
		{
			AnchorUtil.ThrowOnNullArgument(logger, "logger");
			AnchorUtil.ThrowOnNullArgument(timer, "timer");
			this.directoryObject = directoryObject;
			this.logger = logger;
			this.timer = timer;
			this.topologyExtractor = (topologyExtractor ?? base.ExtractorFactory.GetExtractor(this.directoryObject));
			this.timer.SetAction(new Action(this.RefreshCachedValue), true);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001617C File Offset: 0x0001437C
		public override LoadEntity ExtractEntity()
		{
			this.logger.LogVerbose("Retrieving cached entity for {0}.", new object[]
			{
				this.directoryObject
			});
			this.EnsureCacheIsInitialized();
			return this.cachedValue;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000161B8 File Offset: 0x000143B8
		public override LoadContainer ExtractTopology()
		{
			this.logger.LogVerbose("Retrieving cached topology for {0}.", new object[]
			{
				this.directoryObject
			});
			this.EnsureCacheIsInitialized();
			return this.cachedValue;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000161F2 File Offset: 0x000143F2
		private void EnsureCacheIsInitialized()
		{
			while (this.cachedValue == null)
			{
				this.timer.WaitExecution(TimeSpan.FromMinutes(5.0));
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00016218 File Offset: 0x00014418
		private void RefreshCachedValue()
		{
			using (OperationTracker.Create(this.logger, "Refreshing topology for {0}.", new object[]
			{
				this.directoryObject
			}))
			{
				this.cachedValue = this.topologyExtractor.ExtractTopology();
				ExAssert.RetailAssert(this.cachedValue != null, "ExtractTopology for directoryObject: {0} should never return null.  TopologyExtractor: {1}", new object[]
				{
					this.directoryObject,
					this.topologyExtractor
				});
			}
		}

		// Token: 0x0400030C RID: 780
		private readonly DirectoryObject directoryObject;

		// Token: 0x0400030D RID: 781
		private readonly ILogger logger;

		// Token: 0x0400030E RID: 782
		private readonly ITimer timer;

		// Token: 0x0400030F RID: 783
		private readonly TopologyExtractor topologyExtractor;

		// Token: 0x04000310 RID: 784
		private LoadContainer cachedValue;
	}
}
