using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001E6 RID: 486
	internal sealed class OABLogger : ActivityContextLogger, ITracer
	{
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0006E270 File Offset: 0x0006C470
		public static OABLogger Instance
		{
			get
			{
				if (OABLogger.instance == null)
				{
					lock (OABLogger.instanceConstructionLock)
					{
						if (OABLogger.instance == null)
						{
							OABLogger oablogger = new OABLogger();
							OABLogger.instance = oablogger;
						}
					}
				}
				return OABLogger.instance;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0006E2C8 File Offset: 0x0006C4C8
		public static Microsoft.Exchange.Diagnostics.Trace OABTracer
		{
			get
			{
				return OABLogger.diagnosticTracer;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0006E2CF File Offset: 0x0006C4CF
		protected override Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return OABLogger.diagnosticTracer;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0006E2D6 File Offset: 0x0006C4D6
		protected override string LogComponentName
		{
			get
			{
				return "OABGenerator";
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0006E2DD File Offset: 0x0006C4DD
		protected override string LogTypeName
		{
			get
			{
				return "OAB Generator Log";
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0006E2E4 File Offset: 0x0006C4E4
		protected override string FileNamePrefix
		{
			get
			{
				return "oabgen";
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0006E2EC File Offset: 0x0006C4EC
		protected override string ServerName
		{
			get
			{
				Server localServer = LocalServerCache.LocalServer;
				if (localServer == null)
				{
					return string.Empty;
				}
				return localServer.Name;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0006E30E File Offset: 0x0006C50E
		protected override int TimestampField
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0006E311 File Offset: 0x0006C511
		private OABLogger()
		{
		}

		// Token: 0x170004F1 RID: 1265
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x0006E319 File Offset: 0x0006C519
		public static int TraceId
		{
			set
			{
				OABLogger.traceId = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x0006E321 File Offset: 0x0006C521
		public static Guid OabGuid
		{
			set
			{
				OABLogger.oabGuid = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x0006E329 File Offset: 0x0006C529
		public static Guid AddressListGuid
		{
			set
			{
				OABLogger.addressListGuid = value;
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0006E331 File Offset: 0x0006C531
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			OABLogger.diagnosticTracer.TraceDebug<T0>(id, formatString, arg0);
			OABLogger.LogToFile<T0>(TraceType.DebugTrace, formatString, arg0);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0006E348 File Offset: 0x0006C548
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			OABLogger.diagnosticTracer.TraceDebug<T0, T1>(id, formatString, arg0, arg1);
			OABLogger.LogToFile<T0, T1>(TraceType.DebugTrace, formatString, arg0, arg1);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0006E363 File Offset: 0x0006C563
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			OABLogger.diagnosticTracer.TraceDebug<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
			OABLogger.LogToFile<T0, T1, T2>(TraceType.DebugTrace, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0006E382 File Offset: 0x0006C582
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			OABLogger.diagnosticTracer.TraceDebug(id, formatString, args);
			OABLogger.LogToFile(TraceType.DebugTrace, formatString, args);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0006E399 File Offset: 0x0006C599
		public void TraceDebug(long id, string message)
		{
			OABLogger.diagnosticTracer.TraceDebug(id, message);
			OABLogger.LogToFile(TraceType.DebugTrace, message);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0006E3AE File Offset: 0x0006C5AE
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			OABLogger.diagnosticTracer.TraceWarning<T0>(id, formatString, arg0);
			OABLogger.LogToFile<T0>(TraceType.WarningTrace, formatString, arg0);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0006E3C5 File Offset: 0x0006C5C5
		public void TraceWarning(long id, string message)
		{
			OABLogger.diagnosticTracer.TraceWarning(id, message);
			OABLogger.LogToFile(TraceType.WarningTrace, message);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0006E3DA File Offset: 0x0006C5DA
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			OABLogger.diagnosticTracer.TraceWarning(id, formatString, args);
			OABLogger.LogToFile(TraceType.WarningTrace, formatString, args);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0006E3F1 File Offset: 0x0006C5F1
		public void TraceError(long id, string message)
		{
			OABLogger.diagnosticTracer.TraceError(id, message);
			OABLogger.LogToFile(TraceType.ErrorTrace, message);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0006E406 File Offset: 0x0006C606
		public void TraceError(long id, string formatString, params object[] args)
		{
			OABLogger.diagnosticTracer.TraceError(id, formatString, args);
			OABLogger.LogToFile(TraceType.ErrorTrace, formatString, args);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0006E41D File Offset: 0x0006C61D
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			OABLogger.diagnosticTracer.TraceError<T0>(id, formatString, arg0);
			OABLogger.LogToFile<T0>(TraceType.ErrorTrace, formatString, arg0);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0006E434 File Offset: 0x0006C634
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			OABLogger.diagnosticTracer.TraceError<T0, T1>(id, formatString, arg0, arg1);
			OABLogger.LogToFile<T0, T1>(TraceType.ErrorTrace, formatString, arg0, arg1);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0006E44F File Offset: 0x0006C64F
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			OABLogger.diagnosticTracer.TraceError<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
			OABLogger.LogToFile<T0, T1, T2>(TraceType.ErrorTrace, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0006E46E File Offset: 0x0006C66E
		public void TracePerformance(long id, string message)
		{
			OABLogger.diagnosticTracer.TracePerformance(id, message);
			OABLogger.LogToFile(TraceType.PerformanceTrace, message);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0006E483 File Offset: 0x0006C683
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			OABLogger.diagnosticTracer.TracePerformance(id, formatString, args);
			OABLogger.LogToFile(TraceType.PerformanceTrace, formatString, args);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0006E49A File Offset: 0x0006C69A
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			OABLogger.diagnosticTracer.TracePerformance<T0>(id, formatString, arg0);
			OABLogger.LogToFile<T0>(TraceType.PerformanceTrace, formatString, arg0);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0006E4B1 File Offset: 0x0006C6B1
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			OABLogger.diagnosticTracer.TracePerformance<T0, T1>(id, formatString, arg0, arg1);
			OABLogger.LogToFile<T0, T1>(TraceType.PerformanceTrace, formatString, arg0, arg1);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0006E4CC File Offset: 0x0006C6CC
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			OABLogger.diagnosticTracer.TracePerformance<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
			OABLogger.LogToFile<T0, T1, T2>(TraceType.PerformanceTrace, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0006E4EB File Offset: 0x0006C6EB
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0006E4F2 File Offset: 0x0006C6F2
		public ITracer Compose(ITracer other)
		{
			return new CompositeTracer(this, other);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0006E4FB File Offset: 0x0006C6FB
		public bool IsTraceEnabled(TraceType traceType)
		{
			return OABLogger.diagnosticTracer.IsTraceEnabled(traceType);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0006E508 File Offset: 0x0006C708
		public static void LogEvent(ExEventLog.EventTuple eventTuple, params object[] args)
		{
			OABLogger.eventLog.LogEvent(eventTuple, null, args);
			string text = string.Format("Logged Event Id {0}.", eventTuple.EventId);
			if (args != null)
			{
				text += " Params: ";
				for (int i = 0; i < args.Length; i++)
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						(i == 0) ? string.Empty : "||",
						"{",
						i,
						"}"
					});
				}
			}
			string.Format(text, args);
			switch (eventTuple.EntryType)
			{
			case EventLogEntryType.Error:
				OABLogger.LogRecord(TraceType.ErrorTrace, text, args);
				return;
			case EventLogEntryType.Warning:
				OABLogger.LogRecord(TraceType.WarningTrace, text, args);
				return;
			default:
				OABLogger.LogRecord(TraceType.DebugTrace, text, args);
				return;
			}
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0006E5D4 File Offset: 0x0006C7D4
		public static void LogRecord(TraceType traceType, string formatString, params object[] args)
		{
			OABLogger.DiagnosticTrace(traceType, formatString, args);
			OABLogger.LogToFile(traceType, formatString, args);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0006E5E6 File Offset: 0x0006C7E6
		protected override string[] GetLogFields()
		{
			return Enum.GetNames(typeof(OABLogger.OABLogField));
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0006E5F7 File Offset: 0x0006C7F7
		protected override void InternalLogActivityEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0006E5FE File Offset: 0x0006C7FE
		protected override ActivityContextLogFileSettings GetLogFileSettings()
		{
			return OABLogFileSettings.Load();
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0006E608 File Offset: 0x0006C808
		private static void DiagnosticTrace(TraceType traceType, string formatString, params object[] args)
		{
			if (!OABLogger.diagnosticTracer.IsTraceEnabled(traceType))
			{
				return;
			}
			switch (traceType)
			{
			case TraceType.WarningTrace:
				OABLogger.diagnosticTracer.TraceWarning((long)OABLogger.traceId, formatString, args);
				return;
			case TraceType.ErrorTrace:
				OABLogger.diagnosticTracer.TraceError((long)OABLogger.traceId, formatString, args);
				return;
			default:
				if (traceType != TraceType.FunctionTrace)
				{
					OABLogger.diagnosticTracer.TraceDebug((long)OABLogger.traceId, formatString, args);
					return;
				}
				OABLogger.diagnosticTracer.TraceFunction((long)OABLogger.traceId, formatString, args);
				return;
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0006E688 File Offset: 0x0006C888
		private static void LogToFile<T0, T1, T2>(TraceType traceType, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!OABLogger.Instance.Enabled)
			{
				OABLogger.Instance.SafeTraceDebug(0L, "Mailbox assistant log is disabled, skip writing to the log file.", new object[0]);
				return;
			}
			OABLogger.LogToFile(traceType, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
		private static void LogToFile<T0, T1>(TraceType traceType, string formatString, T0 arg0, T1 arg1)
		{
			if (!OABLogger.Instance.Enabled)
			{
				OABLogger.Instance.SafeTraceDebug(0L, "Mailbox assistant log is disabled, skip writing to the log file.", new object[0]);
				return;
			}
			OABLogger.LogToFile(traceType, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0006E716 File Offset: 0x0006C916
		private static void LogToFile<T0>(TraceType traceType, string formatString, T0 arg0)
		{
			if (!OABLogger.Instance.Enabled)
			{
				OABLogger.Instance.SafeTraceDebug(0L, "Mailbox assistant log is disabled, skip writing to the log file.", new object[0]);
				return;
			}
			OABLogger.LogToFile(traceType, string.Format(formatString, arg0));
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0006E74E File Offset: 0x0006C94E
		private static void LogToFile(TraceType traceType, string formatString, params object[] args)
		{
			if (!OABLogger.Instance.Enabled)
			{
				OABLogger.Instance.SafeTraceDebug(0L, "Mailbox assistant log is disabled, skip writing to the log file.", new object[0]);
				return;
			}
			OABLogger.LogToFile(traceType, string.Format(formatString, args));
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0006E784 File Offset: 0x0006C984
		private static void LogToFile(TraceType traceType, string message)
		{
			if (!OABLogger.Instance.Enabled)
			{
				OABLogger.Instance.SafeTraceDebug(0L, "Mailbox assistant log is disabled, skip writing to the log file.", new object[0]);
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(OABLogger.logSchema);
			logRowFormatter[1] = traceType;
			logRowFormatter[2] = OABLogger.traceId;
			logRowFormatter[3] = OABLogger.oabGuid;
			logRowFormatter[4] = OABLogger.addressListGuid;
			logRowFormatter[5] = message;
			OABLogger.Instance.AppendLog(logRowFormatter);
		}

		// Token: 0x04000B71 RID: 2929
		public const string OABLogTypeName = "OAB Generator Log";

		// Token: 0x04000B72 RID: 2930
		private const string OABLogFileNamePrefix = "oabgen";

		// Token: 0x04000B73 RID: 2931
		private const string OABLogComponentName = "OABGenerator";

		// Token: 0x04000B74 RID: 2932
		private static OABLogger instance;

		// Token: 0x04000B75 RID: 2933
		private static object instanceConstructionLock = new object();

		// Token: 0x04000B76 RID: 2934
		private static readonly string[] oabLogFields = new string[]
		{
			"timestamp",
			"recordType",
			"traceId",
			"oabGuid",
			"addressListGuid",
			"message"
		};

		// Token: 0x04000B77 RID: 2935
		private static LogSchema logSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "OAB Generator Log", OABLogger.oabLogFields);

		// Token: 0x04000B78 RID: 2936
		private static Microsoft.Exchange.Diagnostics.Trace diagnosticTracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x04000B79 RID: 2937
		[ThreadStatic]
		private static int traceId;

		// Token: 0x04000B7A RID: 2938
		[ThreadStatic]
		private static Guid oabGuid;

		// Token: 0x04000B7B RID: 2939
		[ThreadStatic]
		private static Guid addressListGuid;

		// Token: 0x04000B7C RID: 2940
		private static readonly ExEventLog eventLog = Globals.Logger;

		// Token: 0x020001E7 RID: 487
		private enum OABLogField
		{
			// Token: 0x04000B7E RID: 2942
			Timestamp,
			// Token: 0x04000B7F RID: 2943
			RecordType,
			// Token: 0x04000B80 RID: 2944
			TraceId,
			// Token: 0x04000B81 RID: 2945
			OabGuid,
			// Token: 0x04000B82 RID: 2946
			AddressListGuid,
			// Token: 0x04000B83 RID: 2947
			Message
		}
	}
}
