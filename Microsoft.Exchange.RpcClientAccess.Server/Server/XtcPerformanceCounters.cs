using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.PerformanceCounters;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000033 RID: 51
	internal class XtcPerformanceCounters : IXtcPerformanceCounters
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000077F5 File Offset: 0x000059F5
		public IExPerformanceCounter XTCDispatchTaskActiveThreads
		{
			get
			{
				return RpcClientAccessPerformanceCounters.XTCDispatchTaskActiveThreads;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000077FC File Offset: 0x000059FC
		public IExPerformanceCounter XTCDispatchTaskOperationsRate
		{
			get
			{
				return RpcClientAccessPerformanceCounters.XTCDispatchTaskOperationsRate;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007803 File Offset: 0x00005A03
		public IExPerformanceCounter XTCDispatchTaskQueueLength
		{
			get
			{
				return RpcClientAccessPerformanceCounters.XTCDispatchTaskQueueLength;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000780A File Offset: 0x00005A0A
		public IExPerformanceCounter XTCDispatchTaskThreads
		{
			get
			{
				return RpcClientAccessPerformanceCounters.XTCDispatchTaskThreads;
			}
		}
	}
}
