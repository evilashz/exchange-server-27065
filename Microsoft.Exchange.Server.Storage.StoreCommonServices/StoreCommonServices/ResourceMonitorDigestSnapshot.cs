using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200010B RID: 267
	public struct ResourceMonitorDigestSnapshot
	{
		// Token: 0x040005D9 RID: 1497
		public ResourceDigestStats[][] TimeInServerDigest;

		// Token: 0x040005DA RID: 1498
		public ResourceDigestStats[][] LogRecordBytesDigest;
	}
}
