using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200002A RID: 42
	internal interface IXtcPerformanceCounters
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600012E RID: 302
		IExPerformanceCounter XTCDispatchTaskActiveThreads { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600012F RID: 303
		IExPerformanceCounter XTCDispatchTaskOperationsRate { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000130 RID: 304
		IExPerformanceCounter XTCDispatchTaskQueueLength { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000131 RID: 305
		IExPerformanceCounter XTCDispatchTaskThreads { get; }
	}
}
