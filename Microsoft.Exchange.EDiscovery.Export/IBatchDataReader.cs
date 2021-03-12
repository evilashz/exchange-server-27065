using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000A RID: 10
	internal interface IBatchDataReader<T>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000041 RID: 65
		// (remove) Token: 0x06000042 RID: 66
		event EventHandler<DataBatchEventArgs<T>> DataBatchRead;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000043 RID: 67
		// (remove) Token: 0x06000044 RID: 68
		event EventHandler AbortingOnError;

		// Token: 0x06000045 RID: 69
		void StartReading();
	}
}
