using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x02000100 RID: 256
	internal class Diagnostics
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		static Diagnostics()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				Diagnostics.processName = currentProcess.MainModule.ModuleName;
				Diagnostics.processId = currentProcess.Id;
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001DD20 File Offset: 0x0001BF20
		internal static string GetGenericErrorKey(string key, bool isUnhandledException)
		{
			if (isUnhandledException)
			{
				return key + "_UnhandledException";
			}
			return key;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001DD32 File Offset: 0x0001BF32
		internal static void ReportException(Exception exception, ExEventLog exEventLog, ExEventLog.EventTuple eventTuple, ExWatson.IsExceptionInteresting knownExceptions, object eventObject, Trace trace, string traceFormat)
		{
			Diagnostics.LogExceptionWithTrace(exEventLog, eventTuple, null, trace, eventObject, traceFormat, exception);
			if (Diagnostics.IsSendReportValid(exception, knownExceptions))
			{
				ExWatson.HandleException(new UnhandledExceptionEventArgs(exception, false), ReportOptions.None);
				ExWatson.SetWatsonReportAlreadySent(exception);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001DD60 File Offset: 0x0001BF60
		internal static bool LogExceptionWithTrace(ExEventLog exEventLog, ExEventLog.EventTuple tuple, string periodicKey, Trace tagTracer, object thisObject, string traceFormat, Exception exception)
		{
			tagTracer.TraceError<Exception>((long)((thisObject == null) ? 0 : thisObject.GetHashCode()), traceFormat, exception);
			string text = string.Format(traceFormat, exception.ToString());
			if (text.Length > 32000)
			{
				text = text.Substring(0, 2000) + "...\n" + text.Substring(text.Length - 20000, 20000);
			}
			return exEventLog.LogEvent(tuple, periodicKey, new object[]
			{
				Diagnostics.processId,
				Diagnostics.processName,
				text
			});
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001DE10 File Offset: 0x0001C010
		internal static void ExecuteAndLog(string funcName, bool missionCritical, LatencyTracker latencyTracker, ExEventLog eventLog, ExEventLog.EventTuple eventTuple, Trace tracer, ExWatson.IsExceptionInteresting isExceptionInteresting, Action<Exception> onError, Action action)
		{
			Diagnostics.ExecuteAndLog<bool>(funcName, missionCritical, latencyTracker, eventLog, eventTuple, tracer, isExceptionInteresting, onError, true, delegate()
			{
				action();
				return true;
			});
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001DE4C File Offset: 0x0001C04C
		internal static T ExecuteAndLog<T>(string funcName, bool missionCritical, LatencyTracker latencyTracker, ExEventLog eventLog, ExEventLog.EventTuple eventTuple, Trace tracer, ExWatson.IsExceptionInteresting isExceptionInteresting, Action<Exception> onError, T defaultReturnValue, Func<T> func)
		{
			bool flag = false;
			T result;
			try
			{
				tracer.TraceDebug<Func<T>>(0L, "[{0}] Enter.", func);
				if (latencyTracker != null)
				{
					flag = latencyTracker.StartInternalTracking(funcName);
				}
				result = func();
			}
			catch (Exception ex)
			{
				if (onError != null)
				{
					onError(ex);
				}
				Diagnostics.ReportException(ex, eventLog, eventTuple, isExceptionInteresting, null, tracer, string.Format("Func {0} throws Exception {{0}}.", funcName));
				if (missionCritical)
				{
					throw;
				}
				result = defaultReturnValue;
			}
			finally
			{
				if (flag)
				{
					latencyTracker.EndInternalTracking(funcName);
				}
				tracer.TraceDebug<Func<T>>(0L, "[{0}] Exit.", func);
			}
			return result;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		private static bool IsSendReportValid(Exception exception, ExWatson.IsExceptionInteresting isExceptionInteresting)
		{
			if (ExWatson.IsWatsonReportAlreadySent(exception))
			{
				return false;
			}
			bool flag = isExceptionInteresting == null || isExceptionInteresting(exception);
			ExTraceGlobals.InstrumentationTracer.TraceDebug<bool>(0L, "IsSendReportValid isSendReportValid: {0}", flag);
			return flag;
		}

		// Token: 0x040004B8 RID: 1208
		internal const string UnHandledErrorType = "UnHandled";

		// Token: 0x040004B9 RID: 1209
		internal const string UnhandledExceptionSuffix = "_UnhandledException";

		// Token: 0x040004BA RID: 1210
		private static readonly string processName;

		// Token: 0x040004BB RID: 1211
		private static readonly int processId;
	}
}
