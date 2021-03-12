using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200011E RID: 286
	public abstract class TraceDataAggregator<TParameters>
	{
		// Token: 0x06000B2C RID: 2860
		internal abstract void Add(TParameters parameters);
	}
}
