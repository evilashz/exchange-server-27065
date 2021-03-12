using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001A9 RID: 425
	internal class ClientFailureEventArgs : ClientTimeStampedEventArgs
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001D9A7 File Offset: 0x0001BBA7
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0001D9AF File Offset: 0x0001BBAF
		public uint FailureCode { get; private set; }

		// Token: 0x0600087A RID: 2170 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		public ClientFailureEventArgs(uint blockTimeSinceRequest, ClientPerformanceEventType eventType, uint failureCode) : base(blockTimeSinceRequest, eventType)
		{
			this.FailureCode = failureCode;
		}
	}
}
