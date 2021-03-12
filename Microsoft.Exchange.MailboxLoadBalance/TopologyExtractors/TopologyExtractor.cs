using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000105 RID: 261
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class TopologyExtractor
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x000160AD File Offset: 0x000142AD
		protected TopologyExtractor(DirectoryObject directoryObject, TopologyExtractorFactory extractorFactory)
		{
			AnchorUtil.ThrowOnNullArgument(directoryObject, "directoryObject");
			AnchorUtil.ThrowOnNullArgument(extractorFactory, "extractorFactory");
			this.DirectoryObject = directoryObject;
			this.ExtractorFactory = extractorFactory;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x000160D9 File Offset: 0x000142D9
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x000160E1 File Offset: 0x000142E1
		private protected DirectoryObject DirectoryObject { protected get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x000160EA File Offset: 0x000142EA
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x000160F2 File Offset: 0x000142F2
		private protected TopologyExtractorFactory ExtractorFactory { protected get; private set; }

		// Token: 0x060007C7 RID: 1991
		public abstract LoadContainer ExtractTopology();

		// Token: 0x060007C8 RID: 1992 RVA: 0x000160FB File Offset: 0x000142FB
		public virtual LoadEntity ExtractEntity()
		{
			return this.ExtractTopology();
		}
	}
}
