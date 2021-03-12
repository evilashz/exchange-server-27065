using System;

namespace Microsoft.Exchange.Data.ThrottlingService.Client
{
	// Token: 0x02000002 RID: 2
	internal interface IThrottlingClientPerformanceCounters
	{
		// Token: 0x06000001 RID: 1
		void AddRequestStatus(ThrottlingRpcResult result);

		// Token: 0x06000002 RID: 2
		void AddRequestStatus(ThrottlingRpcResult result, long requestTimeMsec);
	}
}
