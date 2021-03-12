using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200008E RID: 142
	internal interface IActionData
	{
		// Token: 0x06000550 RID: 1360
		void Initialize(ExDateTime actionTime, string dataStr);

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000551 RID: 1361
		ExDateTime Time { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000552 RID: 1362
		string DataStr { get; }
	}
}
