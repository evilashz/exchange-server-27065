using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200002D RID: 45
	internal class NullRpcHttpConnectionRegistrationPerformanceCounters : IRpcHttpConnectionRegistrationPerformanceCounters
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000742D File Offset: 0x0000562D
		public IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskActiveThreads
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007434 File Offset: 0x00005634
		public IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskOperationsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000743B File Offset: 0x0000563B
		public IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskQueueLength
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007442 File Offset: 0x00005642
		public IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskThreads
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}
	}
}
