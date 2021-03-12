using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RemotePowershellBackendCmdletProxy;

namespace Microsoft.Exchange.Configuration.RemotePowershellBackendCmdletProxy
{
	// Token: 0x02000002 RID: 2
	internal class Logger
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static Logger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				Logger.processName = currentProcess.MainModule.ModuleName;
				Logger.processId = currentProcess.Id;
			}
			Logger.eventLogger = new ExEventLog(ExTraceGlobals.RemotePowershellBackendCmdletProxyModuleTracer.Category, "MSExchange RemotePowershell BackendCmdletProxy Module");
			Logger.eventLogger.SetEventPeriod(300);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002158 File Offset: 0x00000358
		internal static void EnterFunction(Microsoft.Exchange.Diagnostics.Trace trace, string functionName)
		{
			trace.TraceFunction<string>(0L, Logger.traceFunctionEnterString, functionName);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002168 File Offset: 0x00000368
		internal static void ExitFunction(Microsoft.Exchange.Diagnostics.Trace trace, string functionName)
		{
			trace.TraceFunction<string>(0L, Logger.traceFunctionExitString, functionName);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002178 File Offset: 0x00000378
		internal static void TraceInformation(Microsoft.Exchange.Diagnostics.Trace trace, string formatString, params object[] args)
		{
			trace.Information(0L, formatString, args);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002184 File Offset: 0x00000384
		internal static void TraceDebug(Microsoft.Exchange.Diagnostics.Trace trace, string formatString, params object[] args)
		{
			trace.TraceDebug(0L, formatString, args);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002190 File Offset: 0x00000390
		internal static void TraceError(Microsoft.Exchange.Diagnostics.Trace trace, string formatString, params object[] args)
		{
			trace.TraceError(0L, formatString, args);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000219C File Offset: 0x0000039C
		internal static void LogEvent(ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			if (messageArguments == null)
			{
				throw new ArgumentNullException("messageArguments");
			}
			object[] array = new object[messageArguments.Length + 2];
			array[0] = Logger.processName;
			array[1] = Logger.processId;
			messageArguments.CopyTo(array, 2);
			Logger.eventLogger.LogEvent(eventInfo, periodicKey, array);
		}

		// Token: 0x04000001 RID: 1
		private static readonly string traceFunctionEnterString = "Enter Function: {0}.";

		// Token: 0x04000002 RID: 2
		private static readonly string traceFunctionExitString = "Exit Function: {0}.";

		// Token: 0x04000003 RID: 3
		private static readonly ExEventLog eventLogger;

		// Token: 0x04000004 RID: 4
		private static string processName;

		// Token: 0x04000005 RID: 5
		private static int processId;
	}
}
