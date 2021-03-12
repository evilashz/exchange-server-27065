using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.SharedCache;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x0200000E RID: 14
	internal static class Diagnostics
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000326C File Offset: 0x0000146C
		static Diagnostics()
		{
			if (!bool.TryParse(ConfigurationManager.AppSettings["SendWatsonReports"], out Diagnostics.SendWatsonReports))
			{
				Diagnostics.SendWatsonReports = true;
			}
			if (!bool.TryParse(ConfigurationManager.AppSettings["FilterExceptionsFromWatsonReport"], out Diagnostics.FilterExceptionsFromWatsonReport))
			{
				Diagnostics.FilterExceptionsFromWatsonReport = true;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000032D4 File Offset: 0x000014D4
		internal static ExEventLog Logger
		{
			get
			{
				return Diagnostics.logger;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000032DC File Offset: 0x000014DC
		internal static void ReportException(Exception exception, ExEventLog.EventTuple eventTuple, bool terminateProcess, object eventObject, string traceFormat)
		{
			if (Diagnostics.IsSendReportValid(exception))
			{
				Diagnostics.LogExceptionWithTrace(eventTuple, null, ExTraceGlobals.ServerTracer, eventObject, traceFormat, exception);
				ExWatson.HandleException(new UnhandledExceptionEventArgs(exception, terminateProcess), ReportOptions.None);
				ExWatson.SetWatsonReportAlreadySent(exception);
			}
			else
			{
				ExTraceGlobals.ServerTracer.TraceError<Exception>(0L, traceFormat, exception);
			}
			if (terminateProcess)
			{
				Environment.Exit(1);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003330 File Offset: 0x00001530
		internal static bool LogExceptionWithTrace(ExEventLog.EventTuple tuple, string periodicKey, Trace tagTracer, object thisObject, string traceFormat, Exception exception)
		{
			tagTracer.TraceError<Exception>((long)((thisObject == null) ? 0 : thisObject.GetHashCode()), traceFormat, exception);
			string text = exception.ToString();
			if (text.Length > 32000)
			{
				text = text.Substring(0, 2000) + "...\n" + text.Substring(text.Length - 20000, 20000);
			}
			return Diagnostics.logger.LogEvent(tuple, periodicKey, new object[]
			{
				text
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000033B0 File Offset: 0x000015B0
		private static bool IsSendReportValid(Exception exception)
		{
			if (ExWatson.IsWatsonReportAlreadySent(exception))
			{
				return false;
			}
			bool flag = Diagnostics.SendWatsonReports;
			if (flag && Diagnostics.FilterExceptionsFromWatsonReport && exception.StackTrace.Contains("Microsoft.Exchange.Diagnostics.FaultInjection.FaultInjectionTrace.InjectException"))
			{
				flag = false;
			}
			ExTraceGlobals.ServerTracer.TraceDebug<bool>(0L, "IsSendReportValid isSendReportValid: {0}", flag);
			return flag;
		}

		// Token: 0x04000024 RID: 36
		private const string FaultInjectionFrame = "Microsoft.Exchange.Diagnostics.FaultInjection.FaultInjectionTrace.InjectException";

		// Token: 0x04000025 RID: 37
		private const string EventSource = "MSExchange Shared Cache";

		// Token: 0x04000026 RID: 38
		private static readonly bool SendWatsonReports;

		// Token: 0x04000027 RID: 39
		private static readonly bool FilterExceptionsFromWatsonReport;

		// Token: 0x04000028 RID: 40
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.ServerTracer.Category, "MSExchange Shared Cache");
	}
}
