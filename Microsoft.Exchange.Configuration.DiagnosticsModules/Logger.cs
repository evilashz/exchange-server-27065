using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.DiagnosticsModules
{
	// Token: 0x02000005 RID: 5
	internal class Logger
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002518 File Offset: 0x00000718
		static Logger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				Logger.processName = currentProcess.MainModule.ModuleName;
				Logger.processId = currentProcess.Id;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025B4 File Offset: 0x000007B4
		internal static void EnterFunction(Microsoft.Exchange.Diagnostics.Trace trace, string functionName)
		{
			trace.TraceFunction<string>(0L, Logger.traceFunctionEnterString, functionName);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025C4 File Offset: 0x000007C4
		internal static void ExitFunction(Microsoft.Exchange.Diagnostics.Trace trace, string functionName)
		{
			trace.TraceFunction<string>(0L, Logger.traceFunctionExitString, functionName);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025D4 File Offset: 0x000007D4
		internal static void TraceInformation(Microsoft.Exchange.Diagnostics.Trace trace, string formatString, params object[] args)
		{
			trace.Information(0L, formatString, args);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025E0 File Offset: 0x000007E0
		internal static void LogEvent(ExEventLog eventLogger, ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			if (eventLogger == null)
			{
				return;
			}
			if (messageArguments == null)
			{
				throw new ArgumentNullException("messageArguments");
			}
			object[] array = new object[messageArguments.Length + 2];
			array[0] = Logger.processName;
			array[1] = Logger.processId;
			messageArguments.CopyTo(array, 2);
			eventLogger.LogEvent(eventInfo, periodicKey, array);
		}

		// Token: 0x04000006 RID: 6
		private static readonly string appDomainName = AppDomain.CurrentDomain.FriendlyName;

		// Token: 0x04000007 RID: 7
		private static readonly string appDomainTraceInfo = " AppDomain:" + Logger.appDomainName + ".";

		// Token: 0x04000008 RID: 8
		private static readonly string traceFunctionEnterString = "Enter Function: {0}." + Logger.appDomainTraceInfo;

		// Token: 0x04000009 RID: 9
		private static readonly string traceFunctionExitString = "Exit Function: {0}." + Logger.appDomainTraceInfo;

		// Token: 0x0400000A RID: 10
		private static string processName;

		// Token: 0x0400000B RID: 11
		private static int processId;
	}
}
