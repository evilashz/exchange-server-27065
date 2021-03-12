using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001B3 RID: 435
	internal interface IResourceTracker
	{
		// Token: 0x060008A9 RID: 2217
		bool TryReserveMemory(int size);

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008AA RID: 2218
		int StreamSizeLimit { get; }
	}
}
