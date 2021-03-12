using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.FailFast;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x0200000C RID: 12
	internal class Logger
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002F60 File Offset: 0x00001160
		static Logger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				Logger.processName = currentProcess.MainModule.ModuleName;
				Logger.processId = currentProcess.Id;
			}
			Logger.eventLogger = new ExEventLog(ExTraceGlobals.FailFastCacheTracer.Category, "MSExchange FailFast Module");
			Logger.eventLogger.SetEventPeriod(300);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003024 File Offset: 0x00001224
		internal static void EnterFunction(Microsoft.Exchange.Diagnostics.Trace trace, string functionName)
		{
			trace.TraceFunction<string>(0L, Logger.traceFunctionEnterString, functionName);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003034 File Offset: 0x00001234
		internal static void ExitFunction(Microsoft.Exchange.Diagnostics.Trace trace, string functionName)
		{
			trace.TraceFunction<string>(0L, Logger.traceFunctionExitString, functionName);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003044 File Offset: 0x00001244
		internal static void TraceInformation(Microsoft.Exchange.Diagnostics.Trace trace, string formatString, params object[] args)
		{
			trace.Information(0L, formatString + Logger.appDomainTraceInfo, args);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000305A File Offset: 0x0000125A
		internal static void TraceDebug(Microsoft.Exchange.Diagnostics.Trace trace, string formatString, params object[] args)
		{
			trace.TraceDebug(0L, formatString + Logger.appDomainTraceInfo, args);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003070 File Offset: 0x00001270
		internal static void TraceError(Microsoft.Exchange.Diagnostics.Trace trace, string formatString, params object[] args)
		{
			trace.TraceError(0L, formatString + Logger.appDomainTraceInfo, args);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003088 File Offset: 0x00001288
		internal static void LogEvent(ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			if (messageArguments == null)
			{
				throw new ArgumentNullException("messageArguments");
			}
			object[] array = new object[messageArguments.Length + 3];
			array[0] = Logger.processName;
			array[1] = Logger.processId;
			array[2] = Logger.appDomainName;
			messageArguments.CopyTo(array, 3);
			Logger.eventLogger.LogEvent(eventInfo, periodicKey, array);
		}

		// Token: 0x04000025 RID: 37
		private static readonly string appDomainName = AppDomain.CurrentDomain.FriendlyName;

		// Token: 0x04000026 RID: 38
		private static readonly string appDomainTraceInfo = " AppDomain:" + Logger.appDomainName + ".";

		// Token: 0x04000027 RID: 39
		private static readonly string traceFunctionEnterString = "Enter Function: {0}." + Logger.appDomainTraceInfo;

		// Token: 0x04000028 RID: 40
		private static readonly string traceFunctionExitString = "Exit Function: {0}." + Logger.appDomainTraceInfo;

		// Token: 0x04000029 RID: 41
		private static readonly ExEventLog eventLogger;

		// Token: 0x0400002A RID: 42
		private static string processName;

		// Token: 0x0400002B RID: 43
		private static int processId;
	}
}
