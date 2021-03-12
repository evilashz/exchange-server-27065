using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000DE RID: 222
	internal static class DiagCore
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0002A4FD File Offset: 0x000286FD
		public static TimeSpan DefaultEventSuppressionInterval
		{
			get
			{
				return DiagCore.s_defaultEventSuppressionInterval;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0002A504 File Offset: 0x00028704
		private static ExEventLog EventLog
		{
			get
			{
				return DiagCore.s_eventLog;
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002A50B File Offset: 0x0002870B
		public static ExEventLog TestGetEventLog()
		{
			return DiagCore.s_eventLog;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002A512 File Offset: 0x00028712
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002A514 File Offset: 0x00028714
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string formatString, params object[] parameters)
		{
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002A516 File Offset: 0x00028716
		public static void RetailAssert(bool condition, string formatString, params object[] parameters)
		{
			Dependencies.AssertRtl(condition, formatString, parameters);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002A520 File Offset: 0x00028720
		public static void AssertOrWatson(bool condition, string formatString, params object[] parameters)
		{
			if (condition)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder("ASSERT_WATSON: ");
			if (formatString != null)
			{
				if (parameters != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, formatString, parameters);
				}
				else
				{
					stringBuilder.Append(formatString);
				}
			}
			string text = stringBuilder.ToString();
			ExTraceGlobals.ReplayApiTracer.TraceError<string>(0L, "AssertOrWatson: Sending Watson report. {0}", text);
			Exception exception = null;
			try
			{
				throw new ExAssertException(text);
			}
			catch (ExAssertException ex)
			{
				exception = ex;
			}
			ExWatson.SendReport(exception, ReportOptions.None, "AssertOrWatson");
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002A5A0 File Offset: 0x000287A0
		public static string GetMessageWithHResult(this Exception ex)
		{
			return string.Format(DiagCore.s_exceptionMessageWithHrFormatString, ex.Message, Marshal.GetHRForException(ex));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002A5C0 File Offset: 0x000287C0
		public static void LogEvent(this ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			bool flag;
			tuple.LogEvent(periodicKey, out flag, messageArgs);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002A5D8 File Offset: 0x000287D8
		public static void LogEvent(this ExEventLog.EventTuple tuple, string periodicKey, out bool fEventSuppressed, params object[] messageArgs)
		{
			DiagCore.EventLog.LogEvent(tuple, periodicKey, out fEventSuppressed, messageArgs);
			if (!fEventSuppressed)
			{
				string text = tuple.EventLogToString(messageArgs);
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = string.Empty;
					if (messageArgs.Length > 0)
					{
						text2 = string.Join(",\n", messageArgs);
					}
					EventLogEntryType entryType = tuple.EntryType;
					ReplayCrimsonEvent replayCrimsonEvent;
					switch (entryType)
					{
					case EventLogEntryType.Error:
						goto IL_7C;
					case EventLogEntryType.Warning:
						replayCrimsonEvent = ReplayCrimsonEvents.AppLogMirrorWarning;
						goto IL_8A;
					case (EventLogEntryType)3:
						goto IL_84;
					case EventLogEntryType.Information:
						break;
					default:
						if (entryType != EventLogEntryType.SuccessAudit)
						{
							if (entryType != EventLogEntryType.FailureAudit)
							{
								goto IL_84;
							}
							goto IL_7C;
						}
						break;
					}
					replayCrimsonEvent = ReplayCrimsonEvents.AppLogMirrorInformational;
					goto IL_8A;
					IL_7C:
					replayCrimsonEvent = ReplayCrimsonEvents.AppLogMirrorError;
					goto IL_8A;
					IL_84:
					replayCrimsonEvent = ReplayCrimsonEvents.AppLogMirrorInformational;
					IL_8A:
					replayCrimsonEvent.LogGeneric(new object[]
					{
						DiagCore.GetEventViewerEventId(tuple),
						text,
						text2
					});
				}
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002A698 File Offset: 0x00028898
		internal static string EventLogToString(this ExEventLog.EventTuple eventTuple, params object[] arguments)
		{
			int num;
			return eventTuple.EventLogToString(out num, arguments);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002A6B0 File Offset: 0x000288B0
		internal static string EventLogToString(this ExEventLog.EventTuple eventTuple, out int gle, params object[] arguments)
		{
			string result = null;
			if (DiagCore.s_clusmsgFullPath == null)
			{
				DiagCore.s_clusmsgFullPath = Path.Combine(DiagCore.GetFolderPathOfExchangeBinaries(), "clusmsg.dll");
			}
			if (!DiagCore.FormatMessageFromModule(DiagCore.s_clusmsgFullPath, eventTuple.EventId, out result, out gle, arguments))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002A6F8 File Offset: 0x000288F8
		internal static string DbMsgEventTupleToString(ExEventLog.EventTuple eventTuple, params object[] arguments)
		{
			string result = null;
			if (DiagCore.s_exdbmsgFullPath == null)
			{
				DiagCore.s_exdbmsgFullPath = Path.Combine(DiagCore.GetFolderPathOfExchangeBinaries(), "exdbmsg.dll");
			}
			int num;
			if (!DiagCore.FormatMessageFromModule(DiagCore.s_exdbmsgFullPath, eventTuple.EventId, out result, out num, arguments))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002A740 File Offset: 0x00028940
		internal static string GetFolderPathOfExchangeBinaries()
		{
			string result = null;
			try
			{
				result = ExchangeSetupContext.BinPath;
			}
			catch (SetupVersionInformationCorruptException)
			{
				result = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			}
			return result;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002A77C File Offset: 0x0002897C
		internal static uint GetEventViewerEventId(ExEventLog.EventTuple eventTuple)
		{
			return eventTuple.EventId & 65535U;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002A78C File Offset: 0x0002898C
		internal static bool FormatMessageFromModule(string moduleName, uint msgId, out string msg, params object[] arguments)
		{
			int num;
			return DiagCore.FormatMessageFromModule(moduleName, msgId, out msg, out num, arguments);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002A7A4 File Offset: 0x000289A4
		internal static bool FormatMessageFromModule(string moduleName, uint msgId, out string msg, out int gle, params object[] objArguments)
		{
			return SharedDiag.FormatMessageFromModule(moduleName, msgId, out msg, out gle, objArguments);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002A7B1 File Offset: 0x000289B1
		internal static int GetThreadId()
		{
			return DiagnosticsNativeMethods.GetCurrentThreadId();
		}

		// Token: 0x040003BE RID: 958
		private static TimeSpan s_defaultEventSuppressionInterval = TimeSpan.FromSeconds((double)RegistryParameters.CrimsonPeriodicLoggingIntervalInSec);

		// Token: 0x040003BF RID: 959
		private static ExEventLog s_eventLog = new ExEventLog(ExTraceGlobals.ReplayApiTracer.Category, "MSExchangeRepl");

		// Token: 0x040003C0 RID: 960
		private static string s_clusmsgFullPath = null;

		// Token: 0x040003C1 RID: 961
		private static string s_exceptionMessageWithHrFormatString = "{0} [HResult: 0x{1:x}].";

		// Token: 0x040003C2 RID: 962
		private static string s_exdbmsgFullPath = null;
	}
}
