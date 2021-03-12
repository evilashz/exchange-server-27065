using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.PerformanceCounters;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000032 RID: 50
	internal class RpcHttpConnectionRegistrationPerformanceCounters : IRpcHttpConnectionRegistrationPerformanceCounters
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000778E File Offset: 0x0000598E
		public IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskActiveThreads
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskActiveThreads;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007795 File Offset: 0x00005995
		public IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskOperationsRate
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskOperationsRate;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000779C File Offset: 0x0000599C
		public IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskQueueLength
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskQueueLength;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000077A3 File Offset: 0x000059A3
		public IExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskThreads
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskThreads;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000077AA File Offset: 0x000059AA
		public IExPerformanceCounter[] AllCounters
		{
			get
			{
				return this.allCounters;
			}
		}

		// Token: 0x040000A9 RID: 169
		private readonly IExPerformanceCounter[] allCounters = new IExPerformanceCounter[]
		{
			RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskActiveThreads,
			RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskOperationsRate,
			RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskQueueLength,
			RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskThreads
		};
	}
}
