using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.EventLog;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000078 RID: 120
	internal static class ProcessorAffinityHelper
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000A0A0 File Offset: 0x000082A0
		public static void SetProcessorAffinityForCts()
		{
			if (!ProcessorAffinityHelper.affinitySet)
			{
				lock (ProcessorAffinityHelper.lockObject)
				{
					if (!ProcessorAffinityHelper.affinitySet)
					{
						int num = Environment.ProcessorCount * SearchConfig.Instance.CtsProcessorAffinityPercentage / 100;
						if (num > 0)
						{
							using (Process currentProcess = Process.GetCurrentProcess())
							{
								if (currentProcess.ProcessName.IndexOf("NodeRunner", StringComparison.OrdinalIgnoreCase) >= 0 && Environment.CommandLine.IndexOf("ContentEngineNode", StringComparison.OrdinalIgnoreCase) >= 0)
								{
									try
									{
										ProcessorAffinityHelper.SetProcessorAffinity(currentProcess, num, 1);
									}
									catch (Exception ex)
									{
										ExEventLog exEventLog = new ExEventLog(ProcessorAffinityHelper.eventLogComponentGuid, "MSExchangeFastSearch");
										exEventLog.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_SetProcessorAffinityUnexpectedException, string.Empty, new object[]
										{
											ex.ToString()
										});
									}
								}
							}
						}
						ProcessorAffinityHelper.affinitySet = true;
					}
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000A1A4 File Offset: 0x000083A4
		private static void SetProcessorAffinity(Process process, int affinityCount, int avoidProcessorCount)
		{
			int processorCount = Environment.ProcessorCount;
			ulong num = 0UL;
			Random random = new Random();
			int num2 = 0;
			if (avoidProcessorCount > processorCount - affinityCount)
			{
				avoidProcessorCount = processorCount - affinityCount;
			}
			for (int i = 0; i < processorCount; i++)
			{
				if (i >= avoidProcessorCount)
				{
					if (random.Next(processorCount - i) < affinityCount - num2)
					{
						num |= 1UL << i;
						num2++;
					}
					if (num2 == affinityCount)
					{
						break;
					}
				}
			}
			process.ProcessorAffinity = (IntPtr)((long)num);
		}

		// Token: 0x0400014E RID: 334
		private const string NodeRunner = "NodeRunner";

		// Token: 0x0400014F RID: 335
		private const string ConentEngineNode = "ContentEngineNode";

		// Token: 0x04000150 RID: 336
		private const int ProcessorsToAvoid = 1;

		// Token: 0x04000151 RID: 337
		private const string EventLogServiceName = "MSExchangeFastSearch";

		// Token: 0x04000152 RID: 338
		private static readonly Guid eventLogComponentGuid = Guid.Parse("c87fb454-7dfe-4559-af8c-3905438e1398");

		// Token: 0x04000153 RID: 339
		private static readonly object lockObject = new object();

		// Token: 0x04000154 RID: 340
		private static bool affinitySet;
	}
}
