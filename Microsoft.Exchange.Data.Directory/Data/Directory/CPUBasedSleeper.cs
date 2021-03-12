using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009B2 RID: 2482
	internal static class CPUBasedSleeper
	{
		// Token: 0x06007290 RID: 29328 RVA: 0x0017B4EC File Offset: 0x001796EC
		static CPUBasedSleeper()
		{
			if ((long)CPUBasedSleeper.processCpuSlowDownThresholdEntry.Value < 25L || (long)CPUBasedSleeper.processCpuSlowDownThresholdEntry.Value > 100L)
			{
				CPUBasedSleeper.ProcessCpuSlowDownThreshold = 75U;
				return;
			}
			CPUBasedSleeper.ProcessCpuSlowDownThreshold = (uint)CPUBasedSleeper.processCpuSlowDownThresholdEntry.Value;
		}

		// Token: 0x17002867 RID: 10343
		// (get) Token: 0x06007291 RID: 29329 RVA: 0x0017B550 File Offset: 0x00179750
		// (set) Token: 0x06007292 RID: 29330 RVA: 0x0017B557 File Offset: 0x00179757
		internal static IPerfCounterReader ProcessCPUCounter { get; set; } = new ProcessCPURunningAveragePerfCounterReader();

		// Token: 0x06007293 RID: 29331 RVA: 0x0017B55F File Offset: 0x0017975F
		public static bool SleepIfNecessary(out int sleepTime, out float cpuPercent)
		{
			return CPUBasedSleeper.SleepIfNecessary(CPUBasedSleeper.ProcessCpuSlowDownThreshold, out sleepTime, out cpuPercent);
		}

		// Token: 0x06007294 RID: 29332 RVA: 0x0017B570 File Offset: 0x00179770
		public static bool SleepIfNecessary(uint cpuStartPercent, out int sleepTime, out float cpuPercent)
		{
			sleepTime = -1;
			cpuPercent = -1f;
			if (cpuStartPercent >= 100U || CPUBasedSleeper.ProcessCPUCounter == null)
			{
				return false;
			}
			cpuPercent = CPUBasedSleeper.ProcessCPUCounter.GetValue();
			if (cpuPercent >= cpuStartPercent)
			{
				int num = (int)(100U - cpuStartPercent);
				if (num > 0)
				{
					float num2 = 500f / (float)num;
					sleepTime = (int)((cpuPercent - cpuStartPercent) * num2);
				}
				if (sleepTime > 0)
				{
					Thread.Sleep(sleepTime);
				}
				else
				{
					sleepTime = -1;
				}
			}
			ThrottlingPerfCounterWrapper.UpdateAverageThreadSleepTime((long)Math.Max(sleepTime, 0));
			return sleepTime >= 0;
		}

		// Token: 0x04004A32 RID: 18994
		private const string AppSettingProcessCpuSlowDownThreshold = "ProcessCpuSlowDownThreshold";

		// Token: 0x04004A33 RID: 18995
		private const uint DefaultProcessCpuThreshold = 75U;

		// Token: 0x04004A34 RID: 18996
		private const uint MinimumCpuThreshold = 25U;

		// Token: 0x04004A35 RID: 18997
		private const uint MaximumCpuThreshold = 100U;

		// Token: 0x04004A36 RID: 18998
		private const int MaxSleepThrottleTime = 500;

		// Token: 0x04004A37 RID: 18999
		public static readonly uint ProcessCpuSlowDownThreshold;

		// Token: 0x04004A38 RID: 19000
		private static readonly IntAppSettingsEntry processCpuSlowDownThresholdEntry = new IntAppSettingsEntry("ProcessCpuSlowDownThreshold", 75, ExTraceGlobals.ClientThrottlingTracer);
	}
}
