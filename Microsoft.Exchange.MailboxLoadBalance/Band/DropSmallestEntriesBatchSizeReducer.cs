using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DropSmallestEntriesBatchSizeReducer : DropEntriesBatchSizeReducer
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00006068 File Offset: 0x00004268
		public DropSmallestEntriesBatchSizeReducer(ByteQuantifiedSize targetSize, ILogger logger) : base(targetSize, logger)
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006072 File Offset: 0x00004272
		protected override IEnumerable<BandMailboxRebalanceData> SortResults(IEnumerable<BandMailboxRebalanceData> results)
		{
			return results.OrderByDescending(new Func<BandMailboxRebalanceData, ByteQuantifiedSize>(base.GetRebalanceDatumSize));
		}
	}
}
