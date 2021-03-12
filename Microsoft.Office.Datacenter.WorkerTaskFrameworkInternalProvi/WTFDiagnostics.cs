using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000008 RID: 8
	public static class WTFDiagnostics
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000029C0 File Offset: 0x00000BC0
		public static ExEventLog EventLogger
		{
			get
			{
				return WTFDiagnostics.eventLogger;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000029C7 File Offset: 0x00000BC7
		private static TroubleshootingContext TroubleshootingContext
		{
			get
			{
				if (WTFDiagnostics.troubleshootingContext == null)
				{
					WTFDiagnostics.troubleshootingContext = new TroubleshootingContext("Active Monitoring");
				}
				return WTFDiagnostics.troubleshootingContext;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029E4 File Offset: 0x00000BE4
		public static void InMemoryTraceOperationCompleted()
		{
			if (ExTraceConfiguration.Instance.InMemoryTracingEnabled)
			{
				WTFDiagnostics.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029FC File Offset: 0x00000BFC
		public static void SendWatson(Exception exception)
		{
			WTFDiagnostics.SendWatson(exception, true);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A08 File Offset: 0x00000C08
		public static void SendWatson(Exception exception, bool terminating)
		{
			WTFLogger.Instance.Flush();
			if (ExTraceConfiguration.Instance.InMemoryTracingEnabled)
			{
				WTFDiagnostics.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
				WTFDiagnostics.TroubleshootingContext.SendExceptionReportWithTraces(exception, terminating);
				return;
			}
			if (exception != TroubleshootingContext.FaultInjectionInvalidOperationException)
			{
				ExWatson.SendReport(exception, terminating ? ReportOptions.ReportTerminateAfterSend : ReportOptions.None, null);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A58 File Offset: 0x00000C58
		public static void SendInMemoryTraceWatson(Exception exception)
		{
			if (ExTraceConfiguration.Instance.InMemoryTracingEnabled)
			{
				WTFDiagnostics.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
				WTFDiagnostics.TroubleshootingContext.SendTroubleshootingReportWithTraces(exception);
				return;
			}
			if (exception != TroubleshootingContext.FaultInjectionInvalidOperationException)
			{
				ExWatson.SendReport(exception, ReportOptions.DoNotCollectDumps, null);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static bool IsTraceEnabled(TraceType traceType, Trace tracer, TracingContext context)
		{
			return context != null && !context.IsDisabled && tracer.IsTraceEnabled(traceType);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public static WTFLogComponent MapTracerToLogComponent(Trace tracer)
		{
			WTFLogComponent wtflogComponent = null;
			if (!WTFDiagnostics.tracerToLogComponentMap.TryGetValue(tracer, out wtflogComponent))
			{
				if (WTFDiagnostics.IsLogComponentEnabled(tracer))
				{
					wtflogComponent = new WTFLogComponent(tracer.Category, tracer.TraceTag, string.Empty, true);
				}
				else
				{
					wtflogComponent = new WTFLogComponent(tracer.Category, tracer.TraceTag, string.Empty, false);
				}
				WTFDiagnostics.tracerToLogComponentMap.TryAdd(tracer, wtflogComponent);
			}
			return wtflogComponent;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B0C File Offset: 0x00000D0C
		public static void TraceInformation(Trace tracer, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, message, methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceInformation(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B68 File Offset: 0x00000D68
		public static void TraceInformation<T>(Trace tracer, TracingContext context, string formatString, T arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceInformation(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public static void TraceInformation<T0, T1>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceInformation(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C40 File Offset: 0x00000E40
		public static void TraceInformation<T0, T1, T2>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceInformation(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public static void TraceInformation<T0, T1, T2, T3>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceInformation(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D48 File Offset: 0x00000F48
		public static void TraceInformation<T0, T1, T2, T3, T4>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceInformation(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public static void TraceInformation(Trace tracer, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceInformation(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E48 File Offset: 0x00001048
		public static void TraceDebug(Trace tracer, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Debug, component, context, message, methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceDebug(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002EA4 File Offset: 0x000010A4
		public static void TraceDebug<T>(Trace tracer, TracingContext context, string formatString, T arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Debug, component, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceDebug(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F0C File Offset: 0x0000110C
		public static void TraceDebug<T0, T1>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Debug, component, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceDebug(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F7C File Offset: 0x0000117C
		public static void TraceDebug<T0, T1, T2>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Debug, component, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceDebug(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002FF4 File Offset: 0x000011F4
		public static void TraceDebug<T0, T1, T2, T3>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Debug, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceDebug(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003084 File Offset: 0x00001284
		public static void TraceDebug<T0, T1, T2, T3, T4>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Debug, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceDebug(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003120 File Offset: 0x00001320
		public static void TraceDebug(Trace tracer, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Debug, component, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceDebug(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003184 File Offset: 0x00001384
		public static void TraceError(Trace tracer, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Error, component, context, message, methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceError(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000031E0 File Offset: 0x000013E0
		public static void TraceError<T0>(Trace tracer, TracingContext context, string formatString, T0 arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Error, component, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceError(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003248 File Offset: 0x00001448
		public static void TraceError<T0, T1>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Error, component, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceError(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000032B8 File Offset: 0x000014B8
		public static void TraceError<T0, T1, T2>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Error, component, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceError(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003330 File Offset: 0x00001530
		public static void TraceError<T0, T1, T2, T3>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Error, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceError(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000033C0 File Offset: 0x000015C0
		public static void TraceError<T0, T1, T2, T3, T4>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Error, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceError(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000345C File Offset: 0x0000165C
		public static void TraceError(Trace tracer, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Error, component, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceError(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000034C0 File Offset: 0x000016C0
		public static void TraceWarning(Trace tracer, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Warning, component, context, message, methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceWarning(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000351C File Offset: 0x0000171C
		public static void TraceWarning<T0>(Trace tracer, TracingContext context, string formatString, T0 arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Warning, component, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceWarning(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003584 File Offset: 0x00001784
		public static void TraceWarning<T0, T1>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Warning, component, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceWarning(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000035F4 File Offset: 0x000017F4
		public static void TraceWarning<T0, T1, T2>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Warning, component, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceWarning(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000366C File Offset: 0x0000186C
		public static void TraceWarning<T0, T1, T2, T3>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Warning, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceWarning(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000036FC File Offset: 0x000018FC
		public static void TraceWarning<T0, T1, T2, T3, T4>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Warning, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceWarning(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003798 File Offset: 0x00001998
		public static void TraceWarning(Trace tracer, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Warning, component, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceWarning(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000037FC File Offset: 0x000019FC
		public static void TraceFunction(Trace tracer, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, message, methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceFunction(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003858 File Offset: 0x00001A58
		public static void TraceFunction<T0>(Trace tracer, TracingContext context, string formatString, T0 arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceFunction(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000038C0 File Offset: 0x00001AC0
		public static void TraceFunction<T0, T1>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceFunction(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003930 File Offset: 0x00001B30
		public static void TraceFunction<T0, T1, T2>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceFunction(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000039A8 File Offset: 0x00001BA8
		public static void TraceFunction<T0, T1, T2, T3>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceFunction(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003A38 File Offset: 0x00001C38
		public static void TraceFunction<T0, T1, T2, T3, T4>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceFunction(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public static void TraceFunction(Trace tracer, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
			tracer.TraceFunction(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003B38 File Offset: 0x00001D38
		public static void TracePfd(Trace tracer, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, message, methodName, sourceFilePath, sourceLineNumber);
			tracer.TracePfd(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003B94 File Offset: 0x00001D94
		public static void TracePfd<T0>(Trace tracer, TracingContext context, string formatString, T0 arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
			tracer.TracePfd(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003BFC File Offset: 0x00001DFC
		public static void TracePfd<T0, T1>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
			tracer.TracePfd(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C6C File Offset: 0x00001E6C
		public static void TracePfd<T0, T1, T2>(Trace tracer, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
			tracer.TracePfd(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public static void TracePfd(Trace tracer, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
			tracer.TracePfd(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003D48 File Offset: 0x00001F48
		public static void TracePerformance(Trace tracer, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogComponent component = WTFDiagnostics.MapTracerToLogComponent(tracer);
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(WTFLogger.LogLevel.Information, component, context, message, methodName, sourceFilePath, sourceLineNumber);
			tracer.TracePerformance(context.LId, (long)context.Id, workItemContext.ToString());
			WTFLogger.Instance.LogWithContext(component, workItemContext);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public static void TraceInformation(WTFLogComponent logComponent, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003DC5 File Offset: 0x00001FC5
		public static void TraceInformation<T>(WTFLogComponent logComponent, TracingContext context, string formatString, T arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003DF1 File Offset: 0x00001FF1
		public static void TraceInformation<T0, T1>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003E24 File Offset: 0x00002024
		public static void TraceInformation<T0, T1, T2>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003E60 File Offset: 0x00002060
		public static void TraceInformation<T0, T1, T2, T3>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003EC0 File Offset: 0x000020C0
		public static void TraceInformation<T0, T1, T2, T3, T4>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003F2A File Offset: 0x0000212A
		public static void TraceInformation(WTFLogComponent logComponent, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003F51 File Offset: 0x00002151
		public static void TraceDebug(WTFLogComponent logComponent, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogDebug(logComponent, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003F72 File Offset: 0x00002172
		public static void TraceDebug<T>(WTFLogComponent logComponent, TracingContext context, string formatString, T arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogDebug(logComponent, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003F9E File Offset: 0x0000219E
		public static void TraceDebug<T0, T1>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogDebug(logComponent, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003FD1 File Offset: 0x000021D1
		public static void TraceDebug<T0, T1, T2>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogDebug(logComponent, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000400C File Offset: 0x0000220C
		public static void TraceDebug<T0, T1, T2, T3>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogDebug(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000406C File Offset: 0x0000226C
		public static void TraceDebug<T0, T1, T2, T3, T4>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogDebug(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000040D6 File Offset: 0x000022D6
		public static void TraceDebug(WTFLogComponent logComponent, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogDebug(logComponent, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000040FD File Offset: 0x000022FD
		public static void TraceError(WTFLogComponent logComponent, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogError(logComponent, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000411E File Offset: 0x0000231E
		public static void TraceError<T0>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogError(logComponent, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000414A File Offset: 0x0000234A
		public static void TraceError<T0, T1>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogError(logComponent, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000417D File Offset: 0x0000237D
		public static void TraceError<T0, T1, T2>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogError(logComponent, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000041B8 File Offset: 0x000023B8
		public static void TraceError<T0, T1, T2, T3>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogError(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004218 File Offset: 0x00002418
		public static void TraceError<T0, T1, T2, T3, T4>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogError(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004282 File Offset: 0x00002482
		public static void TraceError(WTFLogComponent logComponent, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogError(logComponent, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000042A9 File Offset: 0x000024A9
		public static void TraceWarning(WTFLogComponent logComponent, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogWarning(logComponent, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000042CA File Offset: 0x000024CA
		public static void TraceWarning<T0>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogWarning(logComponent, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000042F6 File Offset: 0x000024F6
		public static void TraceWarning<T0, T1>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogWarning(logComponent, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004329 File Offset: 0x00002529
		public static void TraceWarning<T0, T1, T2>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogWarning(logComponent, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004364 File Offset: 0x00002564
		public static void TraceWarning<T0, T1, T2, T3>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogWarning(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000043C4 File Offset: 0x000025C4
		public static void TraceWarning<T0, T1, T2, T3, T4>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogWarning(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000442E File Offset: 0x0000262E
		public static void TraceWarning(WTFLogComponent logComponent, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogWarning(logComponent, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004455 File Offset: 0x00002655
		public static void TraceFunction(WTFLogComponent logComponent, TracingContext context, string message, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004476 File Offset: 0x00002676
		public static void TraceFunction<T0>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, arg0), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000044A2 File Offset: 0x000026A2
		public static void TraceFunction<T0, T1>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, arg0, arg1), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000044D5 File Offset: 0x000026D5
		public static void TraceFunction<T0, T1, T2>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, arg0, arg1, arg2), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004510 File Offset: 0x00002710
		public static void TraceFunction<T0, T1, T2, T3>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004570 File Offset: 0x00002770
		public static void TraceFunction<T0, T1, T2, T3, T4>(WTFLogComponent logComponent, TracingContext context, string formatString, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000045DA File Offset: 0x000027DA
		public static void TraceFunction(WTFLogComponent logComponent, TracingContext context, string formatString, object[] args, WTFLogger notUsed = null, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			if (context == null || context.IsDisabled)
			{
				return;
			}
			WTFLogger.Instance.LogInformation(logComponent, context, string.Format(formatString, args), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004601 File Offset: 0x00002801
		public static void FaultInjectionTraceTest(FaultInjectionLid lid)
		{
			Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework.ExTraceGlobals.FaultInjectionTracer.TraceTest((uint)lid);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000460E File Offset: 0x0000280E
		public static void FaultInjectionTraceTest<T>(FaultInjectionLid lid, ref T objectToChange)
		{
			Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework.ExTraceGlobals.FaultInjectionTracer.TraceTest<T>((uint)lid, ref objectToChange);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000461C File Offset: 0x0000281C
		public static void FaultInjectionTraceTest<T>(FaultInjectionLid lid, T objectToCompare)
		{
			Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework.ExTraceGlobals.FaultInjectionTracer.TraceTest<T>((uint)lid, objectToCompare);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000462C File Offset: 0x0000282C
		private static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null && exceptionType.Equals("System.InvalidOperationExceptionInMemory", StringComparison.OrdinalIgnoreCase))
			{
				result = TroubleshootingContext.FaultInjectionInvalidOperationException;
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004653 File Offset: 0x00002853
		private static bool IsLogComponentEnabled(Trace tracer)
		{
			return Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.RPSTracer != tracer && !(Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring.ExTraceGlobals.GenericHelperTracer.Category == tracer.Category);
		}

		// Token: 0x04000018 RID: 24
		private const string ActiveMonitoringComponent = "Active Monitoring";

		// Token: 0x04000019 RID: 25
		private static ExEventLog eventLogger = new ExEventLog(WTFLog.Core.Category, "Active Monitoring");

		// Token: 0x0400001A RID: 26
		private static TroubleshootingContext troubleshootingContext;

		// Token: 0x0400001B RID: 27
		private static ConcurrentDictionary<Trace, WTFLogComponent> tracerToLogComponentMap = new ConcurrentDictionary<Trace, WTFLogComponent>();
	}
}
