using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200002E RID: 46
	internal class NullXtcPerformanceCounters : IXtcPerformanceCounters
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007451 File Offset: 0x00005651
		public IExPerformanceCounter XTCDispatchTaskActiveThreads
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007458 File Offset: 0x00005658
		public IExPerformanceCounter XTCDispatchTaskOperationsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000745F File Offset: 0x0000565F
		public IExPerformanceCounter XTCDispatchTaskQueueLength
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007466 File Offset: 0x00005666
		public IExPerformanceCounter XTCDispatchTaskThreads
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}
	}
}
