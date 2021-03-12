using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000453 RID: 1107
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaServerTraceLogger : ExtensibleLogger
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x00087733 File Offset: 0x00085933
		public OwaServerTraceLogger() : base(new OwaServerTraceLogConfiguration())
		{
			ActivityContext.RegisterMetadata(typeof(OwaServerLogger.LoggerData));
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0008774F File Offset: 0x0008594F
		public static void Initialize()
		{
			if (OwaServerTraceLogger.instance == null)
			{
				OwaServerTraceLogger.instance = new OwaServerTraceLogger();
			}
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00087762 File Offset: 0x00085962
		public static void AppendToLog(ILogEvent logEvent)
		{
			if (OwaServerTraceLogger.instance != null)
			{
				OwaServerTraceLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00087778 File Offset: 0x00085978
		public static void SaveTraces()
		{
			Globals.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
			RequestDetailsLogger requestDetailsLogger = RequestDetailsLogger.Current;
			if (requestDetailsLogger != null && OwaServerTraceLogger.IsInterestingFailure(requestDetailsLogger.ActivityScope) && Globals.LogErrorTraces)
			{
				OwaServerTraceLogger.InternalSaveTraces(requestDetailsLogger.ActivityScope, Globals.TroubleshootingContext);
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000877BC File Offset: 0x000859BC
		public static bool IsOverPerfThreshold(double totalMilliSeconds)
		{
			return totalMilliSeconds > (OwaServerTraceLogger.instance.Configuration as OwaServerTraceLogConfiguration).OwaTraceLoggingThreshold;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000877D8 File Offset: 0x000859D8
		private static bool IsInterestingFailure(IActivityScope activityScope)
		{
			bool flag = OwaServerTraceLogger.IsOverPerfThreshold(activityScope.TotalMilliseconds);
			string property = activityScope.GetProperty(ServiceCommonMetadata.ErrorCode);
			string property2 = activityScope.GetProperty(ServiceCommonMetadata.GenericErrors);
			if (string.IsNullOrEmpty(property) && string.IsNullOrEmpty(property2))
			{
				return flag;
			}
			return property == null || !property.Contains("NotFound") || flag;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00087834 File Offset: 0x00085A34
		private static void InternalSaveTraces(IActivityScope activityScope, TroubleshootingContext troubleshootingContext)
		{
			IEnumerable<TraceEntry> traces = troubleshootingContext.GetTraces();
			string eventId = activityScope.GetProperty(ExtensibleLoggerMetadata.EventId) + "_Trace";
			foreach (TraceEntry entry in traces)
			{
				OwaServerTraceLogger.TraceLogEvent logEvent = new OwaServerTraceLogger.TraceLogEvent(activityScope, entry, eventId);
				OwaServerTraceLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x04001570 RID: 5488
		private static OwaServerTraceLogger instance;

		// Token: 0x02000454 RID: 1108
		private class TraceLogEvent : ILogEvent
		{
			// Token: 0x06002564 RID: 9572 RVA: 0x000878AC File Offset: 0x00085AAC
			public TraceLogEvent(IActivityScope activityScope, TraceEntry entry, string eventId)
			{
				this.activityScope = activityScope;
				this.entry = entry;
				this.eventId = eventId;
			}

			// Token: 0x170009D7 RID: 2519
			// (get) Token: 0x06002565 RID: 9573 RVA: 0x000878C9 File Offset: 0x00085AC9
			public string EventId
			{
				get
				{
					return this.eventId;
				}
			}

			// Token: 0x06002566 RID: 9574 RVA: 0x000878D4 File Offset: 0x00085AD4
			public virtual ICollection<KeyValuePair<string, object>> GetEventData()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				ExtensibleLogger.CopyProperty(this.activityScope, dictionary, OwaServerLogger.LoggerData.UserContext, UserContextCookie.UserContextCookiePrefix);
				ExtensibleLogger.CopyPIIProperty(this.activityScope, dictionary, OwaServerLogger.LoggerData.PrimarySmtpAddress, "PSA");
				dictionary.Add("ActivityID", this.activityScope.ActivityId.ToString());
				dictionary.Add("Component", this.entry.ComponentGuid.ToString());
				dictionary.Add("Type", this.entry.TraceType.ToString());
				dictionary.Add("Tag", this.entry.TraceTag.ToString());
				dictionary.Add("Time", this.entry.Timestamp);
				dictionary.Add("NativeThreadId", this.entry.NativeThreadId);
				dictionary.Add("Trace", this.entry.FormatString);
				return dictionary;
			}

			// Token: 0x04001571 RID: 5489
			private readonly IActivityScope activityScope;

			// Token: 0x04001572 RID: 5490
			private readonly TraceEntry entry;

			// Token: 0x04001573 RID: 5491
			private readonly string eventId;
		}
	}
}
