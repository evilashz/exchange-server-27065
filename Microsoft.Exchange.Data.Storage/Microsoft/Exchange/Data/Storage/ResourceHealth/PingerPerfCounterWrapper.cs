using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B34 RID: 2868
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PingerPerfCounterWrapper
	{
		// Token: 0x060067C3 RID: 26563 RVA: 0x001B69D8 File Offset: 0x001B4BD8
		static PingerPerfCounterWrapper()
		{
			string instanceName;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				instanceName = string.Format("{0}({1})", currentProcess.ProcessName, currentProcess.Id);
			}
			PingerPerfCounterWrapper.instance = MSExchangeDatabasePinger.GetInstance(instanceName);
			PingerPerfCounterWrapper.perfCounterUpdateTimer = new Timer(delegate(object state)
			{
				PingerPerfCounterWrapper.instance.PingsPerMinute.RawValue = (long)((ulong)PingerPerfCounterWrapper.pingsPerMinute.GetValue());
				PingerPerfCounterWrapper.instance.CacheSize.RawValue = (long)PingerCache.Singleton.Count;
			}, null, TimeSpan.Zero, TimeSpan.FromMinutes(1.0));
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x001B6A7C File Offset: 0x001B4C7C
		public static void PingSuccessful()
		{
			PingerPerfCounterWrapper.pingsPerMinute.Add(1U);
			PingerPerfCounterWrapper.instance.PingsPerMinute.RawValue = (long)((ulong)PingerPerfCounterWrapper.pingsPerMinute.GetValue());
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x001B6AA3 File Offset: 0x001B4CA3
		public static void PingFailed()
		{
			PingerPerfCounterWrapper.instance.FailedPings.Increment();
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x001B6AB5 File Offset: 0x001B4CB5
		public static void PingTimedOut()
		{
			PingerPerfCounterWrapper.instance.PingTimeouts.Increment();
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x001B6AC7 File Offset: 0x001B4CC7
		internal static MSExchangeDatabasePingerInstance GetInstanceForTest()
		{
			return PingerPerfCounterWrapper.instance;
		}

		// Token: 0x04003AD0 RID: 15056
		private static MSExchangeDatabasePingerInstance instance;

		// Token: 0x04003AD1 RID: 15057
		private static FixedTimeSum pingsPerMinute = new FixedTimeSum(15000, 4);

		// Token: 0x04003AD2 RID: 15058
		private static Timer perfCounterUpdateTimer;
	}
}
