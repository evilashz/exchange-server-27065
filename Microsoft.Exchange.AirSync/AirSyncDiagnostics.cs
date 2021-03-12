using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000018 RID: 24
	internal static class AirSyncDiagnostics
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000BE4A File Offset: 0x0000A04A
		public static ExEventLog EventLogger
		{
			get
			{
				return AirSyncDiagnostics.eventLogger;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000BE51 File Offset: 0x0000A051
		public static LatencyDetectionContextFactory AirSyncLatencyDetectionContextFactory
		{
			get
			{
				return AirSyncDiagnostics.airSyncLatencyDetectionContextFactory;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000BE58 File Offset: 0x0000A058
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (AirSyncDiagnostics.faultInjectionTracer == null)
				{
					AirSyncDiagnostics.faultInjectionTracer = new FaultInjectionTrace(AirSyncDiagnostics.airSyncComponentGuid, AirSyncDiagnostics.tagFaultInjection);
					AirSyncDiagnostics.faultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(AirSyncDiagnostics.Callback));
				}
				return AirSyncDiagnostics.faultInjectionTracer;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000BE90 File Offset: 0x0000A090
		public static TroubleshootingContext TroubleshootingContext
		{
			get
			{
				if (AirSyncDiagnostics.troubleshootingContext == null)
				{
					AirSyncDiagnostics.troubleshootingContext = new TroubleshootingContext("MSExchange ActiveSync");
				}
				return AirSyncDiagnostics.troubleshootingContext;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public static bool CheckAndSetThreadTracing(string userDN)
		{
			if (userDN == null)
			{
				return false;
			}
			if (AirSyncDiagnostics.traceVersion != ExTraceConfiguration.Instance.Version)
			{
				AirSyncDiagnostics.LoadThreadTracingConfig();
			}
			if (!AirSyncDiagnostics.traceConfig.PerThreadTracingConfigured)
			{
				return false;
			}
			if (AirSyncDiagnostics.userDNList == null || !AirSyncDiagnostics.userDNList.Contains(userDN))
			{
				return false;
			}
			BaseTrace.CurrentThreadSettings.EnableTracing();
			return true;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BF07 File Offset: 0x0000A107
		public static void ClearThreadTracing()
		{
			if (BaseTrace.CurrentThreadSettings.IsEnabled)
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BF1F File Offset: 0x0000A11F
		public static void SetThreadTracing()
		{
			if (AirSyncDiagnostics.traceConfig.PerThreadTracingConfigured)
			{
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BF38 File Offset: 0x0000A138
		public static void LogPeriodicEvent(ExEventLog.EventTuple tuple, string eventKey, params string[] messageArgs)
		{
			AirSyncDiagnostics.TraceInfo<uint, string>(ExTraceGlobals.ProtocolTracer, null, "LogPeriodicEvent eventId:{0} eventKey:{1}", tuple.EventId, eventKey);
			if (messageArgs != null)
			{
				for (int i = 0; i < messageArgs.Length; i++)
				{
					if (messageArgs[i].Length > 32000)
					{
						messageArgs[i] = messageArgs[i].Remove(32000).TrimEnd(new char[0]);
					}
				}
			}
			if (!AirSyncDiagnostics.eventLogger.LogEvent(tuple, eventKey, messageArgs))
			{
				AirSyncDiagnostics.TraceError<uint>(ExTraceGlobals.ProtocolTracer, null, "Failed to log periodic event {0}", tuple.EventId);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		public static void LogEvent(ExEventLog.EventTuple tuple, params string[] messageArgs)
		{
			AirSyncDiagnostics.TraceInfo<uint>(ExTraceGlobals.ProtocolTracer, null, "LogEvent eventId:{0}", tuple.EventId);
			if (messageArgs != null)
			{
				for (int i = 0; i < messageArgs.Length; i++)
				{
					if (messageArgs[i].Length > 32000)
					{
						messageArgs[i] = messageArgs[i].Remove(32000).TrimEnd(new char[0]);
					}
				}
			}
			if (TestHooks.EventLog_LogEvent != null)
			{
				TestHooks.EventLog_LogEvent(tuple, null, messageArgs);
				return;
			}
			if (!AirSyncDiagnostics.eventLogger.LogEvent(tuple, null, messageArgs))
			{
				AirSyncDiagnostics.TraceError<uint>(ExTraceGlobals.ProtocolTracer, null, "Failed to log event {0}", tuple.EventId);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C05A File Offset: 0x0000A25A
		public static void Assert(bool condition, string format, params object[] parameters)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C05C File Offset: 0x0000A25C
		public static void Assert(bool condition)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C05E File Offset: 0x0000A25E
		public static void InMemoryTraceOperationCompleted()
		{
			if (AirSyncDiagnostics.IsInMemoryTracingEnabled())
			{
				AirSyncDiagnostics.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000C071 File Offset: 0x0000A271
		public static void SendWatson(Exception exception)
		{
			AirSyncDiagnostics.SendWatson(exception, true);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public static void SendWatson(Exception exception, bool terminating)
		{
			AirSyncDiagnostics.DoWithExtraWatsonData(delegate
			{
				if (AirSyncDiagnostics.IsInMemoryTracingEnabled())
				{
					AirSyncDiagnostics.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
					AirSyncDiagnostics.TroubleshootingContext.SendExceptionReportWithTraces(exception, terminating);
					return;
				}
				if (exception != TroubleshootingContext.FaultInjectionInvalidOperationException)
				{
					ExWatson.SendReport(exception, terminating ? ReportOptions.ReportTerminateAfterSend : ReportOptions.None, null);
				}
			});
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C114 File Offset: 0x0000A314
		private static void DoWithExtraWatsonData(Action action)
		{
			Command currentCommand = Command.CurrentCommand;
			string text = null;
			if (currentCommand != null && GlobalSettings.IncludeRequestInWatson)
			{
				text = "Request: \r\n" + currentCommand.Request.GetHeadersAsString();
				if (currentCommand.Request.XmlDocument != null && currentCommand.Request.XmlDocument.DocumentElement != null)
				{
					text = text + "\r\n" + currentCommand.Request.XmlDocument.DocumentElement.OuterXml;
				}
				else
				{
					text += "\r\n[No Body]";
				}
			}
			WatsonExtraDataReportAction watsonExtraDataReportAction = string.IsNullOrEmpty(text) ? null : new WatsonExtraDataReportAction(text);
			if (watsonExtraDataReportAction != null)
			{
				ExWatson.RegisterReportAction(watsonExtraDataReportAction, WatsonActionScope.Thread);
			}
			try
			{
				action();
			}
			finally
			{
				if (watsonExtraDataReportAction != null)
				{
					ExWatson.UnregisterReportAction(watsonExtraDataReportAction, WatsonActionScope.Thread);
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000C220 File Offset: 0x0000A420
		public static void SendInMemoryTraceWatson(Exception exception)
		{
			AirSyncDiagnostics.DoWithExtraWatsonData(delegate
			{
				if (AirSyncDiagnostics.IsInMemoryTracingEnabled())
				{
					AirSyncDiagnostics.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
					AirSyncDiagnostics.TroubleshootingContext.SendTroubleshootingReportWithTraces(exception);
					return;
				}
				if (exception != TroubleshootingContext.FaultInjectionInvalidOperationException)
				{
					ExWatson.SendReport(exception, ReportOptions.DoNotCollectDumps, null);
				}
			});
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C24C File Offset: 0x0000A44C
		public static ISyncLogger GetSyncLogger()
		{
			IAirSyncContext airSyncContext = (Command.CurrentCommand == null) ? null : Command.CurrentCommand.Context;
			if (airSyncContext != null)
			{
				return airSyncContext;
			}
			return TracingLogger.Singleton;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C27A File Offset: 0x0000A47A
		public static void TraceInfo(Trace tracer, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().Information(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000C295 File Offset: 0x0000A495
		public static void TraceInfo(Trace tracer, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().Information(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000C2B1 File Offset: 0x0000A4B1
		public static void TraceInfo<T0>(Trace tracer, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().Information<T0>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C2CD File Offset: 0x0000A4CD
		public static void TraceInfo<T0, T1>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().Information<T0, T1>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C2EB File Offset: 0x0000A4EB
		public static void TraceInfo<T0, T1, T2>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().Information<T0, T1, T2>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C30B File Offset: 0x0000A50B
		public static void TraceDebug(Trace tracer, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C326 File Offset: 0x0000A526
		public static void TraceDebug(Trace tracer, int lid, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C342 File Offset: 0x0000A542
		public static void TraceDebug(Trace tracer, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C35E File Offset: 0x0000A55E
		public static void TraceDebug<T0>(Trace tracer, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug<T0>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C37A File Offset: 0x0000A57A
		public static void TraceDebug(Trace tracer, int lid, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C398 File Offset: 0x0000A598
		public static void TraceDebug<T0>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug<T0>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
		public static void TraceDebug<T0, T1>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug<T0, T1>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		public static void TraceDebug<T0, T1>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug<T0, T1>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		public static void TraceDebug<T0, T1, T2>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug<T0, T1, T2>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000C414 File Offset: 0x0000A614
		public static void TraceDebug<T0, T1, T2>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceDebug<T0, T1, T2>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000C436 File Offset: 0x0000A636
		public static void TraceError(Trace tracer, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000C451 File Offset: 0x0000A651
		public static void TraceError(Trace tracer, int lid, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C46D File Offset: 0x0000A66D
		public static void TraceError(Trace tracer, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C489 File Offset: 0x0000A689
		public static void TraceError<T0>(Trace tracer, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError<T0>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000C4A5 File Offset: 0x0000A6A5
		public static void TraceError(Trace tracer, int lid, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000C4C3 File Offset: 0x0000A6C3
		public static void TraceError<T0>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError<T0>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000C4E1 File Offset: 0x0000A6E1
		public static void TraceError<T0, T1>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError<T0, T1>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000C4FF File Offset: 0x0000A6FF
		public static void TraceError<T0, T1>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError<T0, T1>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000C51F File Offset: 0x0000A71F
		public static void TraceError<T0, T1, T2>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError<T0, T1, T2>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000C53F File Offset: 0x0000A73F
		public static void TraceError<T0, T1, T2>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceError<T0, T1, T2>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000C561 File Offset: 0x0000A761
		public static void TraceFunction(Trace tracer, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000C57C File Offset: 0x0000A77C
		public static void TraceFunction(Trace tracer, int lid, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000C598 File Offset: 0x0000A798
		public static void TraceFunction(Trace tracer, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		public static void TraceFunction<T0>(Trace tracer, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction<T0>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		public static void TraceFunction(Trace tracer, int lid, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000C5EE File Offset: 0x0000A7EE
		public static void TraceFunction<T0>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction<T0>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000C60C File Offset: 0x0000A80C
		public static void TraceFunction<T0, T1>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction<T0, T1>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000C62A File Offset: 0x0000A82A
		public static void TraceFunction<T0, T1>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction<T0, T1>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000C64A File Offset: 0x0000A84A
		public static void TraceFunction<T0, T1, T2>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction<T0, T1, T2>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000C66A File Offset: 0x0000A86A
		public static void TraceFunction<T0, T1, T2>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceFunction<T0, T1, T2>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C68C File Offset: 0x0000A88C
		public static void TracePfd(Trace tracer, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000C6A7 File Offset: 0x0000A8A7
		public static void TracePfd(Trace tracer, int lid, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000C6C3 File Offset: 0x0000A8C3
		public static void TracePfd(Trace tracer, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		public static void TracePfd<T0>(Trace tracer, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd<T0>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000C6FB File Offset: 0x0000A8FB
		public static void TracePfd(Trace tracer, int lid, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000C719 File Offset: 0x0000A919
		public static void TracePfd<T0>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd<T0>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000C737 File Offset: 0x0000A937
		public static void TracePfd<T0, T1>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd<T0, T1>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000C755 File Offset: 0x0000A955
		public static void TracePfd<T0, T1>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd<T0, T1>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000C775 File Offset: 0x0000A975
		public static void TracePfd<T0, T1, T2>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd<T0, T1, T2>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C795 File Offset: 0x0000A995
		public static void TracePfd<T0, T1, T2>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TracePfd<T0, T1, T2>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C7B7 File Offset: 0x0000A9B7
		public static void TraceWarning(Trace tracer, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		public static void TraceWarning(Trace tracer, int lid, object objectToHash, string message)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), message);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C7EE File Offset: 0x0000A9EE
		public static void TraceWarning(Trace tracer, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000C80A File Offset: 0x0000AA0A
		public static void TraceWarning<T0>(Trace tracer, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning<T0>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C826 File Offset: 0x0000AA26
		public static void TraceWarning(Trace tracer, int lid, object objectToHash, string formatString, params object[] args)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, args);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C844 File Offset: 0x0000AA44
		public static void TraceWarning<T0>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning<T0>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C862 File Offset: 0x0000AA62
		public static void TraceWarning<T0, T1>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning<T0, T1>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C880 File Offset: 0x0000AA80
		public static void TraceWarning<T0, T1>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning<T0, T1>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		public static void TraceWarning<T0, T1, T2>(Trace tracer, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning<T0, T1, T2>(tracer, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		public static void TraceWarning<T0, T1, T2>(Trace tracer, int lid, object objectToHash, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			AirSyncDiagnostics.GetSyncLogger().TraceWarning<T0, T1, T2>(tracer, lid, (long)((objectToHash == null) ? 0 : objectToHash.GetHashCode()), formatString, arg0, arg1, arg2);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C8E2 File Offset: 0x0000AAE2
		public static bool IsTraceEnabled(TraceType traceType, Trace tracer)
		{
			return tracer.IsTraceEnabled(traceType);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		public static void TraceBinaryData(Trace tracer, object obj, byte[] bytes, int length)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (!tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(48);
			StringBuilder stringBuilder2 = new StringBuilder(16);
			int i = 0;
			while (i < length)
			{
				stringBuilder.Length = 0;
				stringBuilder2.Length = 0;
				int j = 0;
				while (i < length)
				{
					if (j >= 16)
					{
						break;
					}
					stringBuilder.Append(bytes[i].ToString("X2", CultureInfo.InvariantCulture));
					stringBuilder.Append(' ');
					stringBuilder2.Append((char)((bytes[i] < 32 || bytes[i] > 127) ? 46 : bytes[i]));
					j++;
					i++;
				}
				while (j < 16)
				{
					stringBuilder.Append("   ");
					stringBuilder2.Append(' ');
					j++;
				}
				AirSyncDiagnostics.TraceDebug<StringBuilder, StringBuilder>(tracer, obj, "{0} {1}", stringBuilder, stringBuilder2);
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		public static void TraceXmlBody(Trace tracer, object obj, XmlDocument xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (!tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return;
			}
			string text = AirSyncUtility.BuildOuterXml(xml);
			string[] separator = new string[]
			{
				"\r\n"
			};
			string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			foreach (string arg in array)
			{
				AirSyncDiagnostics.TraceDebug<string>(tracer, obj, "    {0}", arg);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000CA36 File Offset: 0x0000AC36
		public static bool IsInMemoryTracingEnabled()
		{
			if (AirSyncDiagnostics.traceVersion != ExTraceConfiguration.Instance.Version)
			{
				AirSyncDiagnostics.LoadThreadTracingConfig();
			}
			return AirSyncDiagnostics.traceConfig.InMemoryTracingEnabled;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000CA58 File Offset: 0x0000AC58
		public static T TraceTest<T>(uint faultLid)
		{
			T result = default(T);
			AirSyncDiagnostics.FaultInjectionTracer.TraceTest<T>(faultLid, ref result);
			return result;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000CA7B File Offset: 0x0000AC7B
		public static void FaultInjectionPoint(uint faultLid, Action productAction, Action faultInjectionAction)
		{
			if (AirSyncDiagnostics.TraceTest<bool>(faultLid))
			{
				faultInjectionAction();
				return;
			}
			productAction();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000CA92 File Offset: 0x0000AC92
		private static void LoadThreadTracingConfig()
		{
			AirSyncDiagnostics.traceConfig = ExTraceConfiguration.Instance;
			AirSyncDiagnostics.traceVersion = AirSyncDiagnostics.traceConfig.Version;
			AirSyncDiagnostics.traceConfig.CustomParameters.TryGetValue("UserDN", out AirSyncDiagnostics.userDNList);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		private static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null)
			{
				if (exceptionType.Equals("System.FormatException", StringComparison.OrdinalIgnoreCase))
				{
					result = Constants.FaultInjectionFormatException;
				}
				else if (exceptionType.Equals("System.InvalidOperationExceptionInMemory", StringComparison.OrdinalIgnoreCase))
				{
					result = TroubleshootingContext.FaultInjectionInvalidOperationException;
				}
			}
			return result;
		}

		// Token: 0x040001FB RID: 507
		public const string AssemblyVersion = "15.00.1497.010";

		// Token: 0x040001FC RID: 508
		private const string AirSyncComponent = "MSExchange ActiveSync";

		// Token: 0x040001FD RID: 509
		private const string UserDNParamString = "UserDN";

		// Token: 0x040001FE RID: 510
		private const int MaxEventLogPerStringLength = 32000;

		// Token: 0x040001FF RID: 511
		private static readonly TimeSpan DefaultMinAirSyncThreshold = TimeSpan.FromMinutes(3.0);

		// Token: 0x04000200 RID: 512
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.RequestsTracer.Category, "MSExchange ActiveSync");

		// Token: 0x04000201 RID: 513
		private static ExTraceConfiguration traceConfig;

		// Token: 0x04000202 RID: 514
		private static int traceVersion = -1;

		// Token: 0x04000203 RID: 515
		private static List<string> userDNList;

		// Token: 0x04000204 RID: 516
		private static LatencyDetectionContextFactory airSyncLatencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory("AirSync", AirSyncDiagnostics.DefaultMinAirSyncThreshold, AirSyncDiagnostics.DefaultMinAirSyncThreshold);

		// Token: 0x04000205 RID: 517
		private static FaultInjectionTrace faultInjectionTracer;

		// Token: 0x04000206 RID: 518
		private static Guid airSyncComponentGuid = new Guid("5e88fb2c-0a36-41f2-a710-c911bfe18e44");

		// Token: 0x04000207 RID: 519
		private static int tagFaultInjection = 14;

		// Token: 0x04000208 RID: 520
		private static TroubleshootingContext troubleshootingContext;
	}
}
