using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000111 RID: 273
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ParentContainerExtractor : TopologyExtractor
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x00016980 File Offset: 0x00014B80
		public ParentContainerExtractor(DirectoryContainerParent directoryObject, TopologyExtractorFactory extractorFactory, ILogger logger) : base(directoryObject, extractorFactory)
		{
			this.logger = logger;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00016994 File Offset: 0x00014B94
		public override LoadContainer ExtractTopology()
		{
			this.logger.LogInformation("Retrieving topology for parent object {0}", new object[]
			{
				base.DirectoryObject.Identity
			});
			LoadContainer loadContainer = new LoadContainer(base.DirectoryObject, ContainerType.Generic);
			foreach (DirectoryObject directoryObject in ((DirectoryContainerParent)base.DirectoryObject).Children)
			{
				new ChildContainerExtractor(directoryObject, loadContainer, this.logger, base.ExtractorFactory.GetExtractor(directoryObject)).ExtractContainer();
			}
			return loadContainer;
		}

		// Token: 0x0400031E RID: 798
		private readonly ILogger logger;
	}
}
