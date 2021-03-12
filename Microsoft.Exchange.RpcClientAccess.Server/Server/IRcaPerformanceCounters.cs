using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000028 RID: 40
	internal interface IRcaPerformanceCounters
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000111 RID: 273
		IExPerformanceCounter ActiveUserCount { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000112 RID: 274
		IExPerformanceCounter AveragedLatency { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000113 RID: 275
		IExPerformanceCounter BytesRead { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000114 RID: 276
		IExPerformanceCounter BytesWritten { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000115 RID: 277
		IExPerformanceCounter ClientBackgroundCallsFailed { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000116 RID: 278
		IExPerformanceCounter ClientBackgroundCallsSucceeded { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000117 RID: 279
		IExPerformanceCounter ClientCallsAttempted { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000118 RID: 280
		IExPerformanceCounter ClientCallsFailed { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000119 RID: 281
		IExPerformanceCounter ClientCallsSlow1 { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011A RID: 282
		IExPerformanceCounter ClientCallsSlow2 { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011B RID: 283
		IExPerformanceCounter ClientCallsSlow3 { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011C RID: 284
		IExPerformanceCounter ClientCallsSucceeded { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011D RID: 285
		IExPerformanceCounter ClientForegroundCallsFailed { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600011E RID: 286
		IExPerformanceCounter ClientForegroundCallsSucceeded { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600011F RID: 287
		IExPerformanceCounter ConnectionCount { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000120 RID: 288
		IExPerformanceCounter DispatchTaskActiveThreads { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000121 RID: 289
		IExPerformanceCounter DispatchTaskOperationsRate { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000122 RID: 290
		IExPerformanceCounter DispatchTaskQueueLength { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000123 RID: 291
		IExPerformanceCounter DispatchTaskThreads { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000124 RID: 292
		IExPerformanceCounter OperationsRate { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000125 RID: 293
		IExPerformanceCounter PacketsRate { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000126 RID: 294
		IExPerformanceCounter Requests { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000127 RID: 295
		IExPerformanceCounter UncompressedBytesRead { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000128 RID: 296
		IExPerformanceCounter UncompressedBytesWritten { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000129 RID: 297
		IExPerformanceCounter UserCount { get; }
	}
}
