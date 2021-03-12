using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000029 RID: 41
	internal interface IRpcHttpConnectionRegistrationPerformanceCounters
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600012A RID: 298
		IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskActiveThreads { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600012B RID: 299
		IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskOperationsRate { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600012C RID: 300
		IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskQueueLength { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600012D RID: 301
		IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskThreads { get; }
	}
}
