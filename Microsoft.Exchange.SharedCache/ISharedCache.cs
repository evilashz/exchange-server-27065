using System;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x02000007 RID: 7
	internal interface ISharedCache : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16
		string Name { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17
		PerfCounters PerformanceCounters { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18
		long Count { get; }

		// Token: 0x06000013 RID: 19
		CacheResponse Get(string key);

		// Token: 0x06000014 RID: 20
		CacheResponse Insert(string key, byte[] value, DateTime entryValidAsOfTime);

		// Token: 0x06000015 RID: 21
		CacheResponse Delete(string key);
	}
}
