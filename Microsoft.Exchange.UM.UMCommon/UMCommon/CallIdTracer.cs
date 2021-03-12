using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200001C RID: 28
	internal class CallIdTracer
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00007D5C File Offset: 0x00005F5C
		static CallIdTracer()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", false))
			{
				CallIdTracer.traceToFileEnabled = (registryKey != null && registryKey.GetValue("TraceToFile") != null && registryKey.GetValueKind("TraceToFile") == RegistryValueKind.DWord && 1 == (int)registryKey.GetValue("TraceToFile", 0));
			}
			CallIdTracer.traceFileLogger = new TraceFileLogger(CallIdTracer.traceToFileEnabled);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007DEC File Offset: 0x00005FEC
		internal static void AddBreadcrumb(string message)
		{
			if (CallIdTracer.crumbedEnabled)
			{
				Breadcrumbs.AddBreadcrumb(message);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007DFC File Offset: 0x00005FFC
		internal static string FormatMessage(string message, params object[] args)
		{
			if (message == null)
			{
				message = "Null message";
			}
			else
			{
				try
				{
					if (args != null)
					{
						message = string.Format(CultureInfo.InvariantCulture, message, args);
					}
				}
				catch (FormatException)
				{
					message = string.Format("Badly formatted string - {0} args provided for message '{1}'", args.Length, message);
				}
			}
			return message;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007E54 File Offset: 0x00006054
		internal static string FormatMessageWithContextAndCallId(object context, string message)
		{
			if (CallId.Id != null)
			{
				message = string.Format(CultureInfo.InvariantCulture, "Call-ID {0} : {1}", new object[]
				{
					CallId.Id,
					message
				});
			}
			if (context != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "({0}) : {1}", new object[]
				{
					context.GetHashCode(),
					message
				});
			}
			return message;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007EBC File Offset: 0x000060BC
		internal static string FormatMessageWithPIIData(string message, PIIMessage[] piiData)
		{
			if (piiData == null)
			{
				return message;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("​");
			foreach (PIIMessage piimessage in piiData)
			{
				stringBuilder.Append(piimessage.ToString());
				stringBuilder.Append("​");
			}
			stringBuilder.Append(PIIType._Message.ToString());
			stringBuilder.Append("=");
			stringBuilder.Append(message);
			return stringBuilder.ToString();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007F39 File Offset: 0x00006139
		internal static void TraceDebug(Trace tracer, object context, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			CallIdTracer.traceFileLogger.TraceDebug(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TraceDebug((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007F78 File Offset: 0x00006178
		internal static void TraceDebug(Trace tracer, object context, PIIMessage[] data, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			message = CallIdTracer.FormatMessageWithPIIData(message, data);
			CallIdTracer.traceFileLogger.TraceDebug(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TraceDebug((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007FCC File Offset: 0x000061CC
		internal static void TraceDebug(Trace tracer, object context, PIIMessage data, string message, params object[] args)
		{
			PIIMessage[] data2 = new PIIMessage[]
			{
				data
			};
			CallIdTracer.TraceDebug(tracer, context, data2, message, args);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007FF1 File Offset: 0x000061F1
		internal static void TraceError(Trace tracer, object context, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			CallIdTracer.traceFileLogger.TraceError(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TraceError((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008030 File Offset: 0x00006230
		internal static void TraceError(Trace tracer, object context, PIIMessage[] data, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			message = CallIdTracer.FormatMessageWithPIIData(message, data);
			CallIdTracer.traceFileLogger.TraceError(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TraceError((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008084 File Offset: 0x00006284
		internal static void TraceError(Trace tracer, object context, PIIMessage data, string message, params object[] args)
		{
			PIIMessage[] data2 = new PIIMessage[]
			{
				data
			};
			CallIdTracer.TraceError(tracer, context, data2, message, args);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000080A9 File Offset: 0x000062A9
		internal static void TracePfd(Trace tracer, object context, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			CallIdTracer.traceFileLogger.TracePfd(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TracePfd((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000080E8 File Offset: 0x000062E8
		internal static void TracePfd(Trace tracer, object context, PIIMessage[] data, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			message = CallIdTracer.FormatMessageWithPIIData(message, data);
			CallIdTracer.traceFileLogger.TracePfd(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TracePfd((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000813C File Offset: 0x0000633C
		internal static void TracePfd(Trace tracer, object context, PIIMessage data, string message, params object[] args)
		{
			PIIMessage[] data2 = new PIIMessage[]
			{
				data
			};
			CallIdTracer.TracePfd(tracer, context, data2, message, args);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008161 File Offset: 0x00006361
		internal static void TraceWarning(Trace tracer, object context, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			CallIdTracer.traceFileLogger.TraceWarning(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TraceWarning((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000081A0 File Offset: 0x000063A0
		internal static void TraceWarning(Trace tracer, object context, PIIMessage[] data, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			message = CallIdTracer.FormatMessageWithPIIData(message, data);
			CallIdTracer.traceFileLogger.TraceWarning(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TraceWarning((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000081F4 File Offset: 0x000063F4
		internal static void TraceWarning(Trace tracer, object context, PIIMessage data, string message, params object[] args)
		{
			PIIMessage[] data2 = new PIIMessage[]
			{
				data
			};
			CallIdTracer.TraceWarning(tracer, context, data2, message, args);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008219 File Offset: 0x00006419
		internal static void TracePerformance(Trace tracer, object context, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			CallIdTracer.traceFileLogger.TracePerformance(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TracePerformance((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008258 File Offset: 0x00006458
		internal static void TracePerformance(Trace tracer, object context, PIIMessage[] data, string message, params object[] args)
		{
			message = CallIdTracer.FormatMessage(message, args);
			message = CallIdTracer.FormatMessageWithPIIData(message, data);
			CallIdTracer.traceFileLogger.TracePerformance(context, CallId.Id, message);
			message = CallIdTracer.FormatMessageWithContextAndCallId(context, message);
			CallIdTracer.AddBreadcrumb(message);
			tracer.TracePerformance((long)((context != null) ? context.GetHashCode() : 0), message);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000082AC File Offset: 0x000064AC
		internal static void Flush()
		{
			CallIdTracer.traceFileLogger.Flush();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000082B8 File Offset: 0x000064B8
		internal static void TracePerformance(Trace tracer, object context, PIIMessage data, string message, params object[] args)
		{
			PIIMessage[] data2 = new PIIMessage[]
			{
				data
			};
			CallIdTracer.TracePerformance(tracer, context, data2, message, args);
		}

		// Token: 0x04000095 RID: 149
		private static bool crumbedEnabled = true;

		// Token: 0x04000096 RID: 150
		private static bool traceToFileEnabled;

		// Token: 0x04000097 RID: 151
		private static TraceFileLogger traceFileLogger;
	}
}
