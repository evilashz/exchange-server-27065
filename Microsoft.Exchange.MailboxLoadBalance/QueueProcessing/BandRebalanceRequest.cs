using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.Logging;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000D7 RID: 215
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BandRebalanceRequest : BaseRequest
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00012F28 File Offset: 0x00011128
		public BandRebalanceRequest(BandMailboxRebalanceData rebalanceRequest, IClientFactory clientFactory, ILogger logger)
		{
			this.rebalanceRequest = rebalanceRequest;
			this.clientFactory = clientFactory;
			this.logger = logger;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00012F48 File Offset: 0x00011148
		protected override void ProcessRequest()
		{
			this.logger.LogInformation("Starting load balancing moves for request {0}", new object[]
			{
				this.rebalanceRequest
			});
			try
			{
				BandRebalanceLog.Write(this.rebalanceRequest);
				IOperationRetryManager operationRetryManager = LoadBalanceOperationRetryManager.Create(this.logger);
				operationRetryManager.Run(new Action(this.RequestRebalancingOnRemoteServer));
			}
			finally
			{
				this.logger.LogInformation("Done creating rebalancing request.", new object[0]);
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00012FC8 File Offset: 0x000111C8
		private void RequestRebalancingOnRemoteServer()
		{
			DirectoryDatabase database = (DirectoryDatabase)this.rebalanceRequest.SourceDatabase.DirectoryObject;
			using (ILoadBalanceService loadBalanceClientForDatabase = this.clientFactory.GetLoadBalanceClientForDatabase(database))
			{
				loadBalanceClientForDatabase.BeginMailboxMove(this.rebalanceRequest, PhysicalSize.Instance);
			}
		}

		// Token: 0x04000289 RID: 649
		private readonly BandMailboxRebalanceData rebalanceRequest;

		// Token: 0x0400028A RID: 650
		private readonly IClientFactory clientFactory;

		// Token: 0x0400028B RID: 651
		private readonly ILogger logger;
	}
}
