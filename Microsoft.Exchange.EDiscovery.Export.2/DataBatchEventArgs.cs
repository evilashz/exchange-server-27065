using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000B RID: 11
	internal class DataBatchEventArgs<T> : EventArgs
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A43 File Offset: 0x00000C43
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A4B File Offset: 0x00000C4B
		public T DataBatch { get; set; }
	}
}
