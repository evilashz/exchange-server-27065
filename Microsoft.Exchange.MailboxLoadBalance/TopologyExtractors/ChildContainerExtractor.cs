using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x0200010B RID: 267
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ChildContainerExtractor
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x000164D9 File Offset: 0x000146D9
		public ChildContainerExtractor(DirectoryObject child, LoadContainer loadContainer, ILogger logger, TopologyExtractor extractor)
		{
			this.child = child;
			this.loadContainer = loadContainer;
			this.logger = logger;
			this.childExtractor = extractor;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00016500 File Offset: 0x00014700
		public void ExtractContainer()
		{
			IOperationRetryManager operationRetryManager = LoadBalanceOperationRetryManager.Create(this.logger);
			operationRetryManager.TryRun(new Action(this.ExtractContainerAndAddToParent));
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001652C File Offset: 0x0001472C
		private void ExtractContainerAndAddToParent()
		{
			this.logger.LogInformation("Retrieving topology for child object {0} using extractor {1}", new object[]
			{
				this.child.Identity,
				this.childExtractor
			});
			LoadContainer loadContainer = this.childExtractor.ExtractTopology();
			this.loadContainer.AddChild(loadContainer);
			this.loadContainer.ConsumedLoad += loadContainer.ConsumedLoad;
			this.loadContainer.MaximumLoad += loadContainer.MaximumLoad;
			this.loadContainer.CommittedLoad += loadContainer.CommittedLoad;
		}

		// Token: 0x04000317 RID: 791
		private readonly DirectoryObject child;

		// Token: 0x04000318 RID: 792
		private readonly LoadContainer loadContainer;

		// Token: 0x04000319 RID: 793
		private readonly ILogger logger;

		// Token: 0x0400031A RID: 794
		private readonly TopologyExtractor childExtractor;
	}
}
