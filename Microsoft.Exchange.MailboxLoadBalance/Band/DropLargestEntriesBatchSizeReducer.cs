using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DropLargestEntriesBatchSizeReducer : DropEntriesBatchSizeReducer
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000604A File Offset: 0x0000424A
		public DropLargestEntriesBatchSizeReducer(ByteQuantifiedSize targetSize, ILogger logger) : base(targetSize, logger)
		{
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006054 File Offset: 0x00004254
		protected override IEnumerable<BandMailboxRebalanceData> SortResults(IEnumerable<BandMailboxRebalanceData> results)
		{
			return results.OrderBy(new Func<BandMailboxRebalanceData, ByteQuantifiedSize>(base.GetRebalanceDatumSize));
		}
	}
}
