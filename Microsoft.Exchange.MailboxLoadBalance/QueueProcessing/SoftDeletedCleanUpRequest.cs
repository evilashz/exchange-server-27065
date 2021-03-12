using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000FE RID: 254
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedCleanUpRequest : BaseRequest, ILoadEntityVisitor
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x00015563 File Offset: 0x00013763
		public SoftDeletedCleanUpRequest(LoadContainer forestTopology, IClientFactory clientFactory, int threshold, ILogger logger)
		{
			this.forestTopology = forestTopology;
			this.clientFactory = clientFactory;
			this.threshold = threshold;
			this.logger = logger;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00015588 File Offset: 0x00013788
		public bool Visit(LoadContainer container)
		{
			if (container.ContainerType != ContainerType.Database)
			{
				return true;
			}
			LoadMetric instance = PhysicalSize.Instance;
			ByteQuantifiedSize sizeMetric = container.MaximumLoad.GetSizeMetric(instance);
			ByteQuantifiedSize byteQuantifiedSize = sizeMetric * this.threshold / 100;
			ByteQuantifiedSize sizeMetric2 = container.ConsumedLoad.GetSizeMetric(instance);
			this.logger.LogVerbose("Database {0} has maximum physical size {1}, SoftDeletedThreshold of {2}, target size {3}, consumed physical size {4}", new object[]
			{
				container,
				sizeMetric,
				this.threshold,
				byteQuantifiedSize,
				sizeMetric2
			});
			if (sizeMetric2 >= byteQuantifiedSize)
			{
				SoftDeletedDatabaseCleanupRequest request = new SoftDeletedDatabaseCleanupRequest(this.clientFactory, (DirectoryDatabase)container.DirectoryObject, byteQuantifiedSize);
				base.Queue.EnqueueRequest(request);
			}
			return false;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00015650 File Offset: 0x00013850
		public bool Visit(LoadEntity entity)
		{
			return false;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00015653 File Offset: 0x00013853
		protected override void ProcessRequest()
		{
			this.forestTopology.Accept(this);
		}

		// Token: 0x040002ED RID: 749
		private readonly IClientFactory clientFactory;

		// Token: 0x040002EE RID: 750
		private readonly LoadContainer forestTopology;

		// Token: 0x040002EF RID: 751
		private readonly int threshold;

		// Token: 0x040002F0 RID: 752
		private readonly ILogger logger;
	}
}
