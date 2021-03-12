using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000015 RID: 21
	internal static class CPUMemoryLogger
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000495B File Offset: 0x00002B5B
		static CPUMemoryLogger()
		{
			CPUMemoryLogger.ProcessorCount = Environment.ProcessorCount;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004994 File Offset: 0x00002B94
		internal static void Log()
		{
			if (TickDiffer.Elapsed(CPUMemoryLogger.lastLogTime).TotalMinutes > (double)AppSettings.Current.LogCPUMemoryIntervalInMinutes)
			{
				CPUMemoryLogger.lastLogTime = Environment.TickCount;
				HttpLogger.SafeSetLogger(ConfigurationCoreMetadata.CPU, (long)CPUMemoryLogger.ProcessCpuPerfCounter.GetValue() + "% * " + CPUMemoryLogger.ProcessorCount);
				HttpLogger.SafeSetLogger(ConfigurationCoreMetadata.Memory, CPUMemoryLogger.GetMemory());
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004A0C File Offset: 0x00002C0C
		private static string GetMemory()
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = string.Concat(new object[]
				{
					currentProcess.WorkingSet64 / 1000000L,
					"M/",
					currentProcess.PrivateMemorySize64 / 1000000L,
					"M"
				});
			}
			return result;
		}

		// Token: 0x04000057 RID: 87
		private static readonly ProcessCPURunningAveragePerfCounterReader ProcessCpuPerfCounter = new ProcessCPURunningAveragePerfCounterReader();

		// Token: 0x04000058 RID: 88
		private static readonly int ProcessorCount;

		// Token: 0x04000059 RID: 89
		private static int lastLogTime = TickDiffer.Subtract(Environment.TickCount, AppSettings.Current.LogCPUMemoryIntervalInMinutes * 60 * 1000);
	}
}
