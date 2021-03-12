using System;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x0200010F RID: 271
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxEntityExtractor : TopologyExtractor
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x000167BC File Offset: 0x000149BC
		public MailboxEntityExtractor(DirectoryObject directoryObject, TopologyExtractorFactory extractorFactory, Band[] bands) : base(directoryObject, extractorFactory)
		{
			AnchorUtil.ThrowOnNullArgument(bands, "bands");
			this.bands = bands;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000167D8 File Offset: 0x000149D8
		public override LoadEntity ExtractEntity()
		{
			LoadEntity loadEntity = new LoadEntity(base.DirectoryObject);
			DirectoryMailbox mailbox = (DirectoryMailbox)base.DirectoryObject;
			foreach (LoadMetric loadMetric in this.bands.Union(LoadMetricRepository.DefaultMetrics))
			{
				long unitsForMailbox = loadMetric.GetUnitsForMailbox(mailbox);
				loadEntity.ConsumedLoad[loadMetric] = unitsForMailbox;
			}
			return loadEntity;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001685C File Offset: 0x00014A5C
		public override LoadContainer ExtractTopology()
		{
			throw new NotSupportedException("Cannot extract topology from a mailbox.");
		}

		// Token: 0x0400031C RID: 796
		private readonly Band[] bands;
	}
}
