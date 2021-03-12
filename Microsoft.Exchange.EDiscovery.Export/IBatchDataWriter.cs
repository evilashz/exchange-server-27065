using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000C RID: 12
	internal interface IBatchDataWriter<T> : IDisposable
	{
		// Token: 0x06000049 RID: 73
		void WriteDataBatch(T dataBatch);
	}
}
