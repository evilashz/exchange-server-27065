using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.InstantMessaging;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001EE RID: 494
	public sealed class OwaDiagnostics
	{
		// Token: 0x06001170 RID: 4464 RVA: 0x00042E6B File Offset: 0x0004106B
		private OwaDiagnostics()
		{
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00042E73 File Offset: 0x00041073
		public static ExEventLog Logger
		{
			get
			{
				return OwaDiagnostics.logger.Value;
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00042E7F File Offset: 0x0004107F
		public static bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] eventLogParams)
		{
			return OwaDiagnostics.Logger.LogEvent(tuple, periodicKey, eventLogParams);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00042E8E File Offset: 0x0004108E
		public static bool LogEvent(ExEventLog.EventTuple tuple)
		{
			return OwaDiagnostics.Logger.LogEvent(tuple, string.Empty, new object[0]);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00042EA6 File Offset: 0x000410A6
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string format, params object[] parameters)
		{
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00042EAA File Offset: 0x000410AA
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00042EAC File Offset: 0x000410AC
		public static void TracePfd(int lid, string message, params object[] parameters)
		{
			if (!ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.PfdTrace))
			{
				return;
			}
			ExTraceGlobals.CoreTracer.TracePfd((long)lid, string.Concat(new object[]
			{
				"PFD OWA ",
				lid,
				" - ",
				message
			}), parameters);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00042F00 File Offset: 0x00041100
		public static void PublishMonitoringEventNotification(string serviceName, string component, string message, ResultSeverityLevel severity)
		{
			if (Globals.IsPreCheckinApp)
			{
				return;
			}
			if (component == Feature.InstantMessage.ToString())
			{
				if (severity == ResultSeverityLevel.Error || severity == ResultSeverityLevel.Critical)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceError(0L, message);
				}
				else if (severity == ResultSeverityLevel.Warning)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceWarning(0L, message);
				}
				else
				{
					ExTraceGlobals.InstantMessagingTracer.TraceInformation(0, 0L, message);
				}
			}
			EventNotificationItem eventNotificationItem = new EventNotificationItem(serviceName, component, null, message, severity);
			eventNotificationItem.Publish(false);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00042FA0 File Offset: 0x000411A0
		public static void SendWatsonReportsForGrayExceptions(GrayException.UserCodeDelegate tryCode, Func<Exception, bool> ignoreFunc)
		{
			GrayException.MapAndReportGrayExceptions(tryCode, delegate(Exception exception)
			{
				if (ignoreFunc(exception))
				{
					ExWatson.SetWatsonReportAlreadySent(exception);
					return true;
				}
				return GrayException.IsGrayException(exception);
			});
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00042FE4 File Offset: 0x000411E4
		public static void SendWatsonReportsForGrayExceptions(GrayException.UserCodeDelegate tryCode)
		{
			GrayException.MapAndReportGrayExceptions(tryCode, delegate(Exception exception)
			{
				if (OwaDiagnostics.CanIgnoreExceptionForWatsonReport(exception))
				{
					ExWatson.SetWatsonReportAlreadySent(exception);
					return true;
				}
				return GrayException.IsGrayException(exception);
			});
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0004300C File Offset: 0x0004120C
		public static bool CanIgnoreExceptionForWatsonReport(Exception exception)
		{
			return exception is InstantMessagingException || exception is OwaPermanentException || exception is OwaTransientException || exception is StoragePermanentException || exception is StorageTransientException || exception is ADTransientException || exception is DataValidationException || exception is DataSourceOperationException;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0004305C File Offset: 0x0004125C
		internal static void CheckAndSetThreadTracing(string userDN)
		{
			if (userDN == null)
			{
				throw new ArgumentNullException("userDN");
			}
			ExTraceConfiguration instance = ExTraceConfiguration.Instance;
			lock (OwaDiagnostics.traceLock)
			{
				if (OwaDiagnostics.traceVersion != instance.Version)
				{
					OwaDiagnostics.traceVersion = instance.Version;
					OwaDiagnostics.traceUserTable = null;
					List<string> list = null;
					if (instance.CustomParameters.TryGetValue(OwaDiagnostics.userDNKeyName, out list) && list != null)
					{
						OwaDiagnostics.traceUserTable = new Hashtable(list.Count, StringComparer.OrdinalIgnoreCase);
						for (int i = 0; i < list.Count; i++)
						{
							OwaDiagnostics.traceUserTable[list[i]] = list[i];
						}
					}
				}
				if (instance.PerThreadTracingConfigured && OwaDiagnostics.traceUserTable != null && OwaDiagnostics.traceUserTable[userDN] != null)
				{
					BaseTrace.CurrentThreadSettings.EnableTracing();
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Enabled filtered tracing for user DN = '{0}'", userDN);
				}
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00043158 File Offset: 0x00041358
		internal static void ClearThreadTracing()
		{
			if (BaseTrace.CurrentThreadSettings.IsEnabled)
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}

		// Token: 0x04000A49 RID: 2633
		private static readonly string userDNKeyName = "UserLegacyDN";

		// Token: 0x04000A4A RID: 2634
		private static Lazy<ExEventLog> logger = new Lazy<ExEventLog>(() => new ExEventLog(ExTraceGlobals.CoreTracer.Category, "MSExchange OWA"));

		// Token: 0x04000A4B RID: 2635
		private static int traceVersion = -1;

		// Token: 0x04000A4C RID: 2636
		private static object traceLock = new object();

		// Token: 0x04000A4D RID: 2637
		private static Hashtable traceUserTable;
	}
}
