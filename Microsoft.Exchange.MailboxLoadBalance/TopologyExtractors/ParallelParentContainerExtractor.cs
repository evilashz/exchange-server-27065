using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000110 RID: 272
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ParallelParentContainerExtractor : TopologyExtractor
	{
		// Token: 0x060007F6 RID: 2038 RVA: 0x00016868 File Offset: 0x00014A68
		private ParallelParentContainerExtractor(DirectoryContainerParent directoryContainerObject, TopologyExtractorFactory extractorFactory, ILogger logger) : base(directoryContainerObject, extractorFactory)
		{
			this.logger = logger;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00016879 File Offset: 0x00014A79
		public static TopologyExtractor CreateExtractor(DirectoryContainerParent directoryContainerObject, TopologyExtractorFactory extractorFactory, ILogger logger)
		{
			if (LoadBalanceADSettings.Instance.Value.UseParallelDiscovery)
			{
				return new ParallelParentContainerExtractor(directoryContainerObject, extractorFactory, logger);
			}
			return new ParentContainerExtractor(directoryContainerObject, extractorFactory, logger);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000168D0 File Offset: 0x00014AD0
		public override LoadContainer ExtractTopology()
		{
			DirectoryContainerParent directoryContainerParent = (DirectoryContainerParent)base.DirectoryObject;
			this.logger.LogInformation("Retrieving topology for parent object {0} with {1} children.", new object[]
			{
				base.DirectoryObject.Identity,
				directoryContainerParent.Children.Count<DirectoryObject>()
			});
			LoadContainer container = new LoadContainer(base.DirectoryObject, ContainerType.Generic);
			IEnumerable<DirectoryObject> children = directoryContainerParent.Children;
			IEnumerable<ChildContainerExtractor> source = from child in children
			select new ChildContainerExtractor(child, container, this.logger, this.ExtractorFactory.GetExtractor(child));
			Parallel.ForEach<ChildContainerExtractor>(source, new Action<ChildContainerExtractor, ParallelLoopState>(this.ExtractChild));
			return container;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00016978 File Offset: 0x00014B78
		private void ExtractChild(ChildContainerExtractor childExtractor, ParallelLoopState state)
		{
			childExtractor.ExtractContainer();
		}

		// Token: 0x0400031D RID: 797
		private readonly ILogger logger;
	}
}
