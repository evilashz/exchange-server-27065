using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp.PerformanceCounters;
using Microsoft.Exchange.RpcClientAccess.Server;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000015 RID: 21
	internal class EmsmdbPerformanceCountersWrapper : IRcaPerformanceCounters
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006DB6 File Offset: 0x00004FB6
		public IExPerformanceCounter ActiveUserCount
		{
			get
			{
				return EmsmdbPerformanceCounters.ActiveUserCount;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006DBD File Offset: 0x00004FBD
		public IExPerformanceCounter AveragedLatency
		{
			get
			{
				return EmsmdbPerformanceCounters.AveragedLatency;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public IExPerformanceCounter BytesRead
		{
			get
			{
				return EmsmdbPerformanceCounters.BytesRead;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006DCB File Offset: 0x00004FCB
		public IExPerformanceCounter BytesWritten
		{
			get
			{
				return EmsmdbPerformanceCounters.BytesWritten;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006DD2 File Offset: 0x00004FD2
		public IExPerformanceCounter ClientBackgroundCallsFailed
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientBackgroundCallsFailed;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00006DD9 File Offset: 0x00004FD9
		public IExPerformanceCounter ClientBackgroundCallsSucceeded
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientBackgroundCallsSucceeded;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006DE0 File Offset: 0x00004FE0
		public IExPerformanceCounter ClientCallsAttempted
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientCallsAttempted;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006DE7 File Offset: 0x00004FE7
		public IExPerformanceCounter ClientCallsFailed
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientCallsFailed;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006DEE File Offset: 0x00004FEE
		public IExPerformanceCounter ClientCallsSlow1
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientCallsSlow1;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00006DF5 File Offset: 0x00004FF5
		public IExPerformanceCounter ClientCallsSlow2
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientCallsSlow2;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00006DFC File Offset: 0x00004FFC
		public IExPerformanceCounter ClientCallsSlow3
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientCallsSlow3;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00006E03 File Offset: 0x00005003
		public IExPerformanceCounter ClientCallsSucceeded
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientCallsSucceeded;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006E0A File Offset: 0x0000500A
		public IExPerformanceCounter ClientForegroundCallsFailed
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientForegroundCallsFailed;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006E11 File Offset: 0x00005011
		public IExPerformanceCounter ClientForegroundCallsSucceeded
		{
			get
			{
				return EmsmdbPerformanceCounters.ClientForegroundCallsSucceeded;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006E18 File Offset: 0x00005018
		public IExPerformanceCounter ConnectionCount
		{
			get
			{
				return EmsmdbPerformanceCounters.ConnectionCount;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006E1F File Offset: 0x0000501F
		public IExPerformanceCounter DispatchTaskActiveThreads
		{
			get
			{
				return EmsmdbPerformanceCounters.DispatchTaskActiveThreads;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006E26 File Offset: 0x00005026
		public IExPerformanceCounter DispatchTaskOperationsRate
		{
			get
			{
				return EmsmdbPerformanceCounters.DispatchTaskOperationsRate;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00006E2D File Offset: 0x0000502D
		public IExPerformanceCounter DispatchTaskQueueLength
		{
			get
			{
				return EmsmdbPerformanceCounters.DispatchTaskQueueLength;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006E34 File Offset: 0x00005034
		public IExPerformanceCounter DispatchTaskThreads
		{
			get
			{
				return EmsmdbPerformanceCounters.DispatchTaskThreads;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006E3B File Offset: 0x0000503B
		public IExPerformanceCounter OperationsRate
		{
			get
			{
				return EmsmdbPerformanceCounters.OperationsRate;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006E42 File Offset: 0x00005042
		public IExPerformanceCounter PacketsRate
		{
			get
			{
				return EmsmdbPerformanceCounters.PacketsRate;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006E49 File Offset: 0x00005049
		public IExPerformanceCounter Requests
		{
			get
			{
				return EmsmdbPerformanceCounters.Requests;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006E50 File Offset: 0x00005050
		public IExPerformanceCounter UncompressedBytesRead
		{
			get
			{
				return EmsmdbPerformanceCounters.UncompressedBytesRead;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006E57 File Offset: 0x00005057
		public IExPerformanceCounter UncompressedBytesWritten
		{
			get
			{
				return EmsmdbPerformanceCounters.UncompressedBytesWritten;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006E5E File Offset: 0x0000505E
		public IExPerformanceCounter UserCount
		{
			get
			{
				return EmsmdbPerformanceCounters.UserCount;
			}
		}
	}
}
