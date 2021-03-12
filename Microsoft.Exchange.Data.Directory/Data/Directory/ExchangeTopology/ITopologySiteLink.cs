using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x020006BB RID: 1723
	internal interface ITopologySiteLink
	{
		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x06004FA9 RID: 20393
		string Name { get; }

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x06004FAA RID: 20394
		ReadOnlyCollection<ITopologySite> TopologySites { get; }

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x06004FAB RID: 20395
		int Cost { get; }

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x06004FAC RID: 20396
		Unlimited<ByteQuantifiedSize> MaxMessageSize { get; }

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x06004FAD RID: 20397
		ulong AbsoluteMaxMessageSize { get; }
	}
}
