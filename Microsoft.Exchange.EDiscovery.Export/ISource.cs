using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200005B RID: 91
	public interface ISource
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006D1 RID: 1745
		string Name { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006D2 RID: 1746
		string Id { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060006D3 RID: 1747
		string SourceFilter { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006D4 RID: 1748
		Uri ServiceEndpoint { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006D5 RID: 1749
		string LegacyExchangeDN { get; }
	}
}
