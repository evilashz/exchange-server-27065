using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000031 RID: 49
	internal static class RpcClientAccessPerformanceCountersWrapper
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000076C3 File Offset: 0x000058C3
		public static IRcaPerformanceCounters RcaPerformanceCounters
		{
			get
			{
				return RpcClientAccessPerformanceCountersWrapper.rcaPerformanceCounters;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000076CA File Offset: 0x000058CA
		public static IRpcHttpConnectionRegistrationPerformanceCounters RpcHttpConnectionRegistrationPerformanceCounters
		{
			get
			{
				return RpcClientAccessPerformanceCountersWrapper.rpcHttpConnectionRegistrationPerformanceCounters;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000076D1 File Offset: 0x000058D1
		public static IXtcPerformanceCounters XtcPerformanceCounters
		{
			get
			{
				return RpcClientAccessPerformanceCountersWrapper.xtcPerformanceCounters;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000076D8 File Offset: 0x000058D8
		public static void Initialize(IRcaPerformanceCounters rcaPerformanceCounters, IRpcHttpConnectionRegistrationPerformanceCounters rpcHttpConnectionRegistrationPerformanceCounters, IXtcPerformanceCounters xtcPerformanceCounters)
		{
			RpcClientAccessPerformanceCountersWrapper.rcaPerformanceCounters = rcaPerformanceCounters;
			RpcClientAccessPerformanceCountersWrapper.InitializeCounters(RpcClientAccessPerformanceCountersWrapper.rcaPerformanceCounters);
			RpcClientAccessPerformanceCountersWrapper.rpcHttpConnectionRegistrationPerformanceCounters = rpcHttpConnectionRegistrationPerformanceCounters;
			RpcClientAccessPerformanceCountersWrapper.InitializeCounters(RpcClientAccessPerformanceCountersWrapper.rpcHttpConnectionRegistrationPerformanceCounters);
			RpcClientAccessPerformanceCountersWrapper.xtcPerformanceCounters = xtcPerformanceCounters;
			RpcClientAccessPerformanceCountersWrapper.InitializeCounters(RpcClientAccessPerformanceCountersWrapper.xtcPerformanceCounters);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000770C File Offset: 0x0000590C
		private static void InitializeCounters(object performanceCounters)
		{
			Type typeFromHandle = typeof(IExPerformanceCounter);
			foreach (PropertyInfo propertyInfo in performanceCounters.GetType().GetProperties())
			{
				if (typeFromHandle.IsAssignableFrom(propertyInfo.PropertyType))
				{
					IExPerformanceCounter exPerformanceCounter = propertyInfo.GetValue(performanceCounters, null) as IExPerformanceCounter;
					if (exPerformanceCounter != null)
					{
						exPerformanceCounter.RawValue = 0L;
					}
				}
			}
		}

		// Token: 0x040000A6 RID: 166
		private static IRcaPerformanceCounters rcaPerformanceCounters = new NullRcaPerformanceCounters();

		// Token: 0x040000A7 RID: 167
		private static IRpcHttpConnectionRegistrationPerformanceCounters rpcHttpConnectionRegistrationPerformanceCounters = new NullRpcHttpConnectionRegistrationPerformanceCounters();

		// Token: 0x040000A8 RID: 168
		private static IXtcPerformanceCounters xtcPerformanceCounters = new NullXtcPerformanceCounters();
	}
}
