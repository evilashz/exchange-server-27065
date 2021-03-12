using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001A8 RID: 424
	internal class ClientTimeStampedEventArgs : ClientPerformanceEventArgs
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001D966 File Offset: 0x0001BB66
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0001D96E File Offset: 0x0001BB6E
		public ExDateTime TimeStamp { get; private set; }

		// Token: 0x06000877 RID: 2167 RVA: 0x0001D978 File Offset: 0x0001BB78
		public ClientTimeStampedEventArgs(uint blockTimeSinceRequest, ClientPerformanceEventType eventType) : base(eventType)
		{
			this.TimeStamp = ExDateTime.UtcNow.Subtract(TimeSpan.FromMilliseconds(blockTimeSinceRequest));
		}
	}
}
