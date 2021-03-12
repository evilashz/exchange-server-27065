using System;
using System.Diagnostics;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RedirectionModule;

namespace Microsoft.Exchange.Configuration.RedirectionModule
{
	// Token: 0x02000005 RID: 5
	internal class Logger
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002EB8 File Offset: 0x000010B8
		static Logger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				Logger.processName = currentProcess.MainModule.ModuleName;
				Logger.processId = currentProcess.Id;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002F54 File Offset: 0x00001154
		internal static void LogVerbose(TraceSource traceSource, string message, params object[] args)
		{
			traceSource.TraceEvent(TraceEventType.Verbose, 0, message, args);
			ExTraceGlobals.RedirectionTracer.Information(0L, message, args);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F6F File Offset: 0x0000116F
		internal static void LogVerbose(TraceSource traceSource, string message)
		{
			Logger.LogVerbose(traceSource, message, new object[0]);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002F7E File Offset: 0x0000117E
		internal static void LogWarning(TraceSource traceSource, string message)
		{
			traceSource.TraceEvent(TraceEventType.Warning, 0, message);
			ExTraceGlobals.RedirectionTracer.TraceWarning(0L, message);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002F98 File Offset: 0x00001198
		internal static void LogError(ExEventLog eventLogger, TraceSource traceSource, string message, Exception exception)
		{
			Logger.LogError(eventLogger, traceSource, message, exception, null, null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002FB8 File Offset: 0x000011B8
		internal static void LogError(ExEventLog eventLogger, TraceSource traceSource, string message, Exception exception, ExEventLog.EventTuple? eventInfo, string user)
		{
			traceSource.TraceEvent(TraceEventType.Error, 0, "{0} - {1}", new object[]
			{
				message,
				exception
			});
			ExTraceGlobals.RedirectionTracer.TraceError<string, Exception>(0L, "{0} - {1}", message, exception);
			if (eventInfo != null)
			{
				Logger.LogEvent(eventLogger, eventInfo.Value, user, new object[]
				{
					user,
					exception
				});
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003020 File Offset: 0x00001220
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

		// Token: 0x0600001D RID: 29 RVA: 0x00003074 File Offset: 0x00001274
		internal static void GenerateErrorMessage(HttpResponse response, ExEventLog eventLogger, ExEventLog.EventTuple tuple, Exception error, string tenant)
		{
			Logger.LogEvent(eventLogger, tuple, null, new object[]
			{
				error,
				tenant
			});
			response.Clear();
			response.StatusCode = 500;
			response.ContentType = "application/soap+xml;charset=UTF-8";
			response.Write(error.Message);
			response.End();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000030C8 File Offset: 0x000012C8
		internal static void EnterFunction(Microsoft.Exchange.Diagnostics.Trace trace, string functionName)
		{
			trace.TraceFunction<string>(0L, Logger.traceFunctionEnterString, functionName);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000030D8 File Offset: 0x000012D8
		internal static void ExitFunction(Microsoft.Exchange.Diagnostics.Trace trace, string functionName)
		{
			trace.TraceFunction<string>(0L, Logger.traceFunctionExitString, functionName);
		}

		// Token: 0x0400000F RID: 15
		private static readonly string appDomainName = AppDomain.CurrentDomain.FriendlyName;

		// Token: 0x04000010 RID: 16
		private static readonly string appDomainTraceInfo = " AppDomain:" + Logger.appDomainName + ".";

		// Token: 0x04000011 RID: 17
		private static readonly string traceFunctionEnterString = "Enter Function: {0}." + Logger.appDomainTraceInfo;

		// Token: 0x04000012 RID: 18
		private static readonly string traceFunctionExitString = "Exit Function: {0}." + Logger.appDomainTraceInfo;

		// Token: 0x04000013 RID: 19
		private static string processName;

		// Token: 0x04000014 RID: 20
		private static int processId;
	}
}
