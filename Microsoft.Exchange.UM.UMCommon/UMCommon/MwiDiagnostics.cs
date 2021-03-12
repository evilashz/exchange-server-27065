using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000CB RID: 203
	internal class MwiDiagnostics
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0001A114 File Offset: 0x00018314
		internal static MwiLoadBalancerPerformanceCountersInstance GetInstance(string instanceName)
		{
			MwiLoadBalancerPerformanceCountersInstance result = null;
			try
			{
				result = MwiLoadBalancerPerformanceCounters.GetInstance(instanceName);
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.MWITracer, 0, "MwiDiagnostics: Unable to get instance {0}: {1}", new object[]
				{
					instanceName,
					ex
				});
			}
			return result;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001A168 File Offset: 0x00018368
		internal static void IncrementCounter(ExPerformanceCounter perfCounter)
		{
			if (perfCounter != null)
			{
				perfCounter.Increment();
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001A174 File Offset: 0x00018374
		internal static void SetCounterValue(ExPerformanceCounter perfCounter, long counterValue)
		{
			if (perfCounter != null)
			{
				perfCounter.RawValue = counterValue;
			}
		}
	}
}
