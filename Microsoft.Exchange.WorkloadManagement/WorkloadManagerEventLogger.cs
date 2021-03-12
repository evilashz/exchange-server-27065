using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200000D RID: 13
	internal static class WorkloadManagerEventLogger
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00002FE0 File Offset: 0x000011E0
		public static void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			if (string.IsNullOrEmpty(WorkloadManagerEventLogger.processName))
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					WorkloadManagerEventLogger.processName = currentProcess.ProcessName;
					WorkloadManagerEventLogger.processId = currentProcess.Id;
				}
			}
			object[] array = new object[messageArgs.Length + 2];
			array[0] = WorkloadManagerEventLogger.processName;
			array[1] = WorkloadManagerEventLogger.processId;
			Array.Copy(messageArgs, 0, array, 2, messageArgs.Length);
			WorkloadManagerEventLogger.logger.LogEvent(tuple, periodicKey, array);
		}

		// Token: 0x04000035 RID: 53
		private static readonly ExEventLog logger = new ExEventLog(ExTraceGlobals.CommonTracer.Category, "MSExchange WorkloadManagement");

		// Token: 0x04000036 RID: 54
		private static string processName = string.Empty;

		// Token: 0x04000037 RID: 55
		private static int processId;
	}
}
