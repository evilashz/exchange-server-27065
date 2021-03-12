using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000E5 RID: 229
	internal interface IEseDatabaseReader : IDisposable
	{
		// Token: 0x06000944 RID: 2372
		void ForceNewLog();

		// Token: 0x06000945 RID: 2373
		byte[] ReadOnePage(long pageNumber, out long lowGen, out long highGen);

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000946 RID: 2374
		long PageSize { get; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000947 RID: 2375
		string DatabaseName { get; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000948 RID: 2376
		Guid DatabaseGuid { get; }

		// Token: 0x06000949 RID: 2377
		long ReadPageSize();
	}
}
