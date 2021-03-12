using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000118 RID: 280
	public interface ITraceCollector<TKey, TParameters>
	{
		// Token: 0x06000AFB RID: 2811
		void Add(TKey key, TParameters parameters);
	}
}
