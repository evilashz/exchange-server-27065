using System;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.EventLogs;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000016 RID: 22
	internal static class Diagnostics
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003D4D File Offset: 0x00001F4D
		internal static ExEventLog Logger
		{
			get
			{
				return Diagnostics.logger;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003D54 File Offset: 0x00001F54
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
				HttpProxyGlobals.ProtocolType,
				text
			});
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003DE0 File Offset: 0x00001FE0
		internal static bool LogEventWithTrace(ExEventLog.EventTuple tuple, string periodicKey, Trace tagTracer, object thisObject, string traceFormat, params object[] messageArgs)
		{
			tagTracer.TraceDebug((long)((thisObject == null) ? 0 : thisObject.GetHashCode()), traceFormat, messageArgs);
			return Diagnostics.logger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E07 File Offset: 0x00002007
		internal static void SendWatsonReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(methodDelegate, null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E9C File Offset: 0x0000209C
		internal static void SendWatsonReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate, Diagnostics.LastChanceExceptionHandler exceptionHandler)
		{
			WatsonReportAction action = Diagnostics.RegisterAdditionalWatsonData();
			try
			{
				ExWatson.SendReportOnUnhandledException(methodDelegate, delegate(object exception)
				{
					bool flag = Diagnostics.SendWatsonReports.Value;
					Exception ex = exception as Exception;
					if (ex != null)
					{
						if (exceptionHandler != null)
						{
							exceptionHandler(ex);
						}
						ExTraceGlobals.ExceptionTracer.TraceError<Exception>(0L, "Encountered unhandled exception: {0}", ex);
						flag = Diagnostics.IsSendReportValid(ex);
						if (flag)
						{
							Diagnostics.LogExceptionWithTrace(FrontEndHttpProxyEventLogConstants.Tuple_InternalServerError, null, ExTraceGlobals.ExceptionTracer, null, "Encountered unhandled exception: {0}", ex);
							ExWatson.SetWatsonReportAlreadySent(ex);
						}
					}
					ExTraceGlobals.ExceptionTracer.TraceError<bool>(0L, "SendWatsonReportOnUnhandledException isSendReportValid: {0}", flag);
					return flag;
				}, ReportOptions.None);
			}
			finally
			{
				ExWatson.UnregisterReportAction(action, WatsonActionScope.Thread);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003F1B File Offset: 0x0000211B
		internal static void TraceErrorOnUnhandledException(ExWatson.MethodDelegate methodDelegate)
		{
			ExWatson.SendReportOnUnhandledException(methodDelegate, delegate(object exception)
			{
				ExTraceGlobals.ExceptionTracer.TraceError(0L, "Unhandled exception, Exception details: {0}", new object[]
				{
					exception
				});
				return false;
			});
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F40 File Offset: 0x00002140
		internal static void InitializeWatsonReporting()
		{
			ExTraceGlobals.ExceptionTracer.TraceDebug<bool, bool>(0L, "sendWatsonReports: {0} filterExceptionsFromWatsonReporting: {1}", Diagnostics.SendWatsonReports.Value, Diagnostics.FilterExceptionsFromWatsonReport.Value);
			ExWatson.Register(ExEnvironment.IsTest ? "E12" : "E12IIS");
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003F80 File Offset: 0x00002180
		internal static void ReportException(Exception exception, ExEventLog.EventTuple eventTuple, object eventObject, string traceFormat)
		{
			bool flag = exception is AccessViolationException;
			if (Diagnostics.IsSendReportValid(exception))
			{
				Diagnostics.LogExceptionWithTrace(eventTuple, null, ExTraceGlobals.ExceptionTracer, eventObject, traceFormat, exception);
				ExWatson.HandleException(new UnhandledExceptionEventArgs(exception, flag), ReportOptions.None);
				ExWatson.SetWatsonReportAlreadySent(exception);
			}
			else
			{
				ExTraceGlobals.ExceptionTracer.TraceError<Exception>(0L, traceFormat, exception);
			}
			if (flag)
			{
				Environment.Exit(1);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003FDC File Offset: 0x000021DC
		internal static string BuildFullExceptionTrace(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (exception != null)
			{
				stringBuilder.AppendFormat("Exception Class: \"{0}\", Message: \"{1}\"\n", exception.GetType().FullName, exception.Message);
				exception = exception.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004020 File Offset: 0x00002220
		internal static WatsonReportAction RegisterAdditionalWatsonData()
		{
			string watsonExtraData = Diagnostics.GetWatsonExtraData();
			WatsonReportAction watsonReportAction = new WatsonExtraDataReportAction(watsonExtraData);
			ExWatson.RegisterReportAction(watsonReportAction, WatsonActionScope.Thread);
			return watsonReportAction;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004042 File Offset: 0x00002242
		private static string GetWatsonExtraData()
		{
			return "<none>";
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000404C File Offset: 0x0000224C
		private static bool IsSendReportValid(Exception exception)
		{
			if (ExWatson.IsWatsonReportAlreadySent(exception))
			{
				return false;
			}
			bool flag = Diagnostics.SendWatsonReports.Value;
			if (flag && Diagnostics.FilterExceptionsFromWatsonReport.Value)
			{
				if (exception is HttpException)
				{
					flag = false;
				}
				else if (exception is System.ServiceModel.QuotaExceededException)
				{
					flag = false;
				}
				else if (exception is DataValidationException)
				{
					flag = false;
				}
				else if (exception is DataSourceOperationException)
				{
					flag = false;
				}
				else if (exception is StoragePermanentException || exception is StorageTransientException)
				{
					flag = false;
				}
				else if (exception is ServiceDiscoveryTransientException)
				{
					flag = false;
				}
				else if (exception is IOException)
				{
					flag = false;
				}
				else if (exception is OutOfMemoryException)
				{
					flag = false;
				}
				else if (exception is ADTransientException)
				{
					flag = false;
				}
				else if (exception is ThreadAbortException)
				{
					flag = false;
				}
				else if (exception.StackTrace.Contains("Microsoft.Exchange.Diagnostics.FaultInjection.FaultInjectionTrace.InjectException"))
				{
					flag = false;
				}
			}
			ExTraceGlobals.ExceptionTracer.TraceDebug<bool>(0L, "IsSendReportValid isSendReportValid: {0}", flag);
			return flag;
		}

		// Token: 0x04000083 RID: 131
		private const string FaultInjectionFrame = "Microsoft.Exchange.Diagnostics.FaultInjection.FaultInjectionTrace.InjectException";

		// Token: 0x04000084 RID: 132
		private const string EventSource = "MSExchange Front End HTTP Proxy";

		// Token: 0x04000085 RID: 133
		private static readonly BoolAppSettingsEntry SendWatsonReports = new BoolAppSettingsEntry(HttpProxySettings.Prefix("SendWatsonReports"), true, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000086 RID: 134
		private static readonly BoolAppSettingsEntry FilterExceptionsFromWatsonReport = new BoolAppSettingsEntry(HttpProxySettings.Prefix("FilterExceptionsFromWatsonReport"), true, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000087 RID: 135
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.ExceptionTracer.Category, "MSExchange Front End HTTP Proxy");

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x06000077 RID: 119
		public delegate void LastChanceExceptionHandler(Exception unhandledException);
	}
}
