using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002EF RID: 751
	internal class TenantOutboundConnectorsRetrievalException : Exception
	{
		// Token: 0x06002152 RID: 8530 RVA: 0x0007E478 File Offset: 0x0007C678
		public TenantOutboundConnectorsRetrievalException(ADOperationResult result)
		{
			this.Result = result;
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x0007E487 File Offset: 0x0007C687
		// (set) Token: 0x06002154 RID: 8532 RVA: 0x0007E48F File Offset: 0x0007C68F
		public ADOperationResult Result { get; private set; }
	}
}
