using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x020006BA RID: 1722
	internal interface ITopologySite
	{
		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x06004FA7 RID: 20391
		string Name { get; }

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x06004FA8 RID: 20392
		ReadOnlyCollection<ITopologySiteLink> TopologySiteLinks { get; }
	}
}
