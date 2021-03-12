using System;
using System.Diagnostics;
using Microsoft.Exchange.Configuration.Core.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000008 RID: 8
	internal static class CoreLogger
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002AF8 File Offset: 0x00000CF8
		static CoreLogger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				CoreLogger.processName = currentProcess.MainModule.ModuleName;
				CoreLogger.processId = currentProcess.Id;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B70 File Offset: 0x00000D70
		internal static void ExecuteAndLog(string funcName, bool missionCritical, LatencyTracker latencyTracker, Action<Exception> onError, Action action)
		{
			Diagnostics.ExecuteAndLog(funcName, missionCritical, latencyTracker, Constants.CoreEventLogger, TaskEventLogConstants.Tuple_NonCrashingException, ExTraceGlobals.InstrumentationTracer, (object ex) => false, onError, action);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002BB8 File Offset: 0x00000DB8
		internal static T ExecuteAndLog<T>(string funcName, bool missionCritical, LatencyTracker latencyTracker, Action<Exception> onError, T defaultReturnValue, Func<T> func)
		{
			return Diagnostics.ExecuteAndLog<T>(funcName, missionCritical, latencyTracker, Constants.CoreEventLogger, TaskEventLogConstants.Tuple_NonCrashingException, ExTraceGlobals.InstrumentationTracer, (object ex) => false, onError, defaultReturnValue, func);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002BED File Offset: 0x00000DED
		internal static void TraceInformation(string formatString, params object[] args)
		{
			ExTraceGlobals.InstrumentationTracer.Information(0L, formatString + CoreLogger.appDomainTraceInfo, args);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C07 File Offset: 0x00000E07
		internal static void TraceDebug(string formatString, params object[] args)
		{
			ExTraceGlobals.InstrumentationTracer.TraceDebug(0L, formatString + CoreLogger.appDomainTraceInfo, args);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C21 File Offset: 0x00000E21
		internal static void TraceError(string formatString, params object[] args)
		{
			ExTraceGlobals.InstrumentationTracer.TraceError(0L, formatString + CoreLogger.appDomainTraceInfo, args);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C3C File Offset: 0x00000E3C
		internal static void LogEvent(ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			if (messageArguments == null)
			{
				throw new ArgumentNullException("messageArguments");
			}
			object[] array = new object[messageArguments.Length + 3];
			array[0] = CoreLogger.processName;
			array[1] = CoreLogger.processId;
			array[2] = CoreLogger.appDomainName;
			messageArguments.CopyTo(array, 3);
			Constants.CoreEventLogger.LogEvent(eventInfo, periodicKey, array);
		}

		// Token: 0x04000012 RID: 18
		private static readonly string appDomainName = AppDomain.CurrentDomain.FriendlyName;

		// Token: 0x04000013 RID: 19
		private static readonly string appDomainTraceInfo = " AppDomain:" + CoreLogger.appDomainName + ".";

		// Token: 0x04000014 RID: 20
		private static readonly string processName;

		// Token: 0x04000015 RID: 21
		private static readonly int processId;
	}
}
