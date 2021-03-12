using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.PerformanceCounters;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000030 RID: 48
	internal class RcaPerformanceCounters : IRcaPerformanceCounters
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000760C File Offset: 0x0000580C
		public IExPerformanceCounter ActiveUserCount
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ActiveUserCount;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007613 File Offset: 0x00005813
		public IExPerformanceCounter AveragedLatency
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCAveragedLatency;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000761A File Offset: 0x0000581A
		public IExPerformanceCounter BytesRead
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCBytesRead;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00007621 File Offset: 0x00005821
		public IExPerformanceCounter BytesWritten
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCBytesWritten;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007628 File Offset: 0x00005828
		public IExPerformanceCounter ClientBackgroundCallsFailed
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientBackgroundRpcFailed;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000762F File Offset: 0x0000582F
		public IExPerformanceCounter ClientBackgroundCallsSucceeded
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientBackgroundRpcSucceeded;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007636 File Offset: 0x00005836
		public IExPerformanceCounter ClientCallsAttempted
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientRpcAttempted;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000763D File Offset: 0x0000583D
		public IExPerformanceCounter ClientCallsFailed
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientRpcFailed;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007644 File Offset: 0x00005844
		public IExPerformanceCounter ClientCallsSlow1
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientRpcSlow1;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000764B File Offset: 0x0000584B
		public IExPerformanceCounter ClientCallsSlow2
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientRpcSlow2;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007652 File Offset: 0x00005852
		public IExPerformanceCounter ClientCallsSlow3
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientRpcSlow3;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007659 File Offset: 0x00005859
		public IExPerformanceCounter ClientCallsSucceeded
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientRpcSucceeded;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007660 File Offset: 0x00005860
		public IExPerformanceCounter ClientForegroundCallsFailed
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientForegroundRpcFailed;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007667 File Offset: 0x00005867
		public IExPerformanceCounter ClientForegroundCallsSucceeded
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ClientForegroundRpcSucceeded;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000766E File Offset: 0x0000586E
		public IExPerformanceCounter ConnectionCount
		{
			get
			{
				return RpcClientAccessPerformanceCounters.ConnectionCount;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007675 File Offset: 0x00005875
		public IExPerformanceCounter DispatchTaskActiveThreads
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCDispatchTaskActiveThreads;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000767C File Offset: 0x0000587C
		public IExPerformanceCounter DispatchTaskOperationsRate
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCDispatchTaskOperationsRate;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007683 File Offset: 0x00005883
		public IExPerformanceCounter DispatchTaskQueueLength
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCDispatchTaskQueueLength;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000768A File Offset: 0x0000588A
		public IExPerformanceCounter DispatchTaskThreads
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCDispatchTaskThreads;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007691 File Offset: 0x00005891
		public IExPerformanceCounter OperationsRate
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCOperationsRate;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00007698 File Offset: 0x00005898
		public IExPerformanceCounter PacketsRate
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCPacketsRate;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000769F File Offset: 0x0000589F
		public IExPerformanceCounter Requests
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCRequests;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000076A6 File Offset: 0x000058A6
		public IExPerformanceCounter UncompressedBytesRead
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCUncompressedBytesRead;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000076AD File Offset: 0x000058AD
		public IExPerformanceCounter UncompressedBytesWritten
		{
			get
			{
				return RpcClientAccessPerformanceCounters.RPCUncompressedBytesWritten;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000076B4 File Offset: 0x000058B4
		public IExPerformanceCounter UserCount
		{
			get
			{
				return RpcClientAccessPerformanceCounters.UserCount;
			}
		}
	}
}
