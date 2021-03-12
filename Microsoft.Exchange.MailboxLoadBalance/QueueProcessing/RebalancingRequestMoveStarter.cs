using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000E3 RID: 227
	internal class RebalancingRequestMoveStarter : IRebalancingRequestProcessor
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x00013C03 File Offset: 0x00011E03
		public RebalancingRequestMoveStarter(IClientFactory clientFactory, ILogger logger, IRequestQueueManager queueManager)
		{
			this.clientFactory = clientFactory;
			this.logger = logger;
			this.queueManager = queueManager;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00013C20 File Offset: 0x00011E20
		public void ProcessRebalanceRequest(BandMailboxRebalanceData rebalanceRequest)
		{
			AnchorUtil.ThrowOnNullArgument(rebalanceRequest, "rebalanceRequest");
			BandRebalanceRequest request = new BandRebalanceRequest(rebalanceRequest, this.clientFactory, this.logger);
			LoadContainer sourceDatabase = rebalanceRequest.SourceDatabase;
			IRequestQueue processingQueue = this.GetProcessingQueue(sourceDatabase);
			processingQueue.EnqueueRequest(request);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00013C61 File Offset: 0x00011E61
		private IRequestQueue GetProcessingQueue(LoadContainer container)
		{
			if (container.Parent == null)
			{
				return this.queueManager.GetProcessingQueue(container);
			}
			if (container.DirectoryObjectIdentity.ObjectType == DirectoryObjectType.DatabaseAvailabilityGroup)
			{
				return this.queueManager.GetProcessingQueue(container);
			}
			return this.GetProcessingQueue(container.Parent);
		}

		// Token: 0x040002AA RID: 682
		private readonly IClientFactory clientFactory;

		// Token: 0x040002AB RID: 683
		private readonly ILogger logger;

		// Token: 0x040002AC RID: 684
		private readonly IRequestQueueManager queueManager;
	}
}
