using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000069 RID: 105
	internal static class SharedDiag
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000D76C File Offset: 0x0000B96C
		public static TimeSpan DefaultEventSuppressionInterval
		{
			get
			{
				return SharedDiag.s_defaultEventSuppressionInterval;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000D773 File Offset: 0x0000B973
		private static ExEventLog EventLog
		{
			get
			{
				return SharedDiag.s_eventLog;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D77A File Offset: 0x0000B97A
		public static void TestResetFromDependencies()
		{
			SharedDiag.s_eventLog = SharedDependencies.DiagCoreImpl.EventLog;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D78B File Offset: 0x0000B98B
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D78D File Offset: 0x0000B98D
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string formatString, params object[] parameters)
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000D78F File Offset: 0x0000B98F
		public static void RetailAssert(bool condition, string formatString, params object[] parameters)
		{
			SharedDependencies.AssertRtl(condition, formatString, parameters);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000D79C File Offset: 0x0000B99C
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

		// Token: 0x06000320 RID: 800 RVA: 0x0000D81C File Offset: 0x0000BA1C
		public static string GetMessageWithHResult(this Exception ex)
		{
			return string.Format(SharedDiag.s_exceptionMessageWithHrFormatString, ex.Message, Marshal.GetHRForException(ex));
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public static void LogEvent(this ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			bool flag;
			tuple.LogEvent(periodicKey, out flag, messageArgs);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000D854 File Offset: 0x0000BA54
		public static void LogEvent(this ExEventLog.EventTuple tuple, string periodicKey, out bool fEventSuppressed, params object[] messageArgs)
		{
			SharedDiag.EventLog.LogEvent(tuple, periodicKey, out fEventSuppressed, messageArgs);
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
						SharedDiag.GetEventViewerEventId(tuple),
						text,
						text2
					});
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000D914 File Offset: 0x0000BB14
		internal static string EventLogToString(this ExEventLog.EventTuple eventTuple, params object[] arguments)
		{
			int num;
			return eventTuple.EventLogToString(out num, arguments);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000D92C File Offset: 0x0000BB2C
		internal static string EventLogToString(this ExEventLog.EventTuple eventTuple, out int gle, params object[] arguments)
		{
			string result = null;
			if (SharedDiag.s_clusmsgFullPath == null)
			{
				SharedDiag.s_clusmsgFullPath = Path.Combine(SharedDiag.GetFolderPathOfExchangeBinaries(), "clusmsg.dll");
			}
			if (!SharedDiag.FormatMessageFromModule(SharedDiag.s_clusmsgFullPath, eventTuple.EventId, out result, out gle, arguments))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000D974 File Offset: 0x0000BB74
		internal static string DbMsgEventTupleToString(ExEventLog.EventTuple eventTuple, params object[] arguments)
		{
			string result = null;
			if (SharedDiag.s_exdbmsgFullPath == null)
			{
				SharedDiag.s_exdbmsgFullPath = Path.Combine(SharedDiag.GetFolderPathOfExchangeBinaries(), "exdbmsg.dll");
			}
			int num;
			if (!SharedDiag.FormatMessageFromModule(SharedDiag.s_exdbmsgFullPath, eventTuple.EventId, out result, out num, arguments))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000D9BC File Offset: 0x0000BBBC
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

		// Token: 0x06000327 RID: 807 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		internal static uint GetEventViewerEventId(ExEventLog.EventTuple eventTuple)
		{
			return eventTuple.EventId & 65535U;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000DA08 File Offset: 0x0000BC08
		internal static bool FormatMessageFromModule(string moduleName, uint msgId, out string msg, params object[] arguments)
		{
			int num;
			return SharedDiag.FormatMessageFromModule(moduleName, msgId, out msg, out num, arguments);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000DA20 File Offset: 0x0000BC20
		internal static bool FormatMessageFromModule(string moduleName, uint msgId, out string msg, out int gle, params object[] objArguments)
		{
			string[] array = null;
			if (objArguments != null && objArguments.Length > 0)
			{
				array = new string[objArguments.Length];
				int num = 0;
				foreach (object obj in objArguments)
				{
					string text = string.Empty;
					if (obj != null)
					{
						text = obj.ToString();
					}
					array[num++] = text;
				}
			}
			bool result = false;
			msg = null;
			gle = 0;
			ModuleHandle moduleHandle = NativeMethods.LoadLibraryEx(moduleName, IntPtr.Zero, 2U);
			if (moduleHandle == null || moduleHandle.IsInvalid)
			{
				gle = Marshal.GetLastWin32Error();
				ExTraceGlobals.ReplayApiTracer.TraceError<string, uint, int>(0L, "Failed to LoadLibraryEx( {0} ) when interpreting {1}. Error code: {2}.", moduleName, msgId, gle);
				return false;
			}
			IntPtr[] array2 = null;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				uint dwFlags;
				if (array != null)
				{
					int num2 = array.Length;
					array2 = new IntPtr[num2];
					for (int j = 0; j < num2; j++)
					{
						array2[j] = Marshal.StringToHGlobalUni(array[j]);
					}
					intPtr = Marshal.AllocHGlobal(num2 * IntPtr.Size);
					Marshal.Copy(array2, 0, intPtr, num2);
					dwFlags = 10496U;
				}
				else
				{
					dwFlags = 2816U;
				}
				IntPtr zero = IntPtr.Zero;
				uint num3 = NativeMethods.FormatMessage(dwFlags, moduleHandle, msgId, 0U, ref zero, 0U, intPtr);
				if (num3 != 0U && zero != IntPtr.Zero)
				{
					msg = Marshal.PtrToStringUni(zero);
					Marshal.FreeHGlobal(zero);
					result = true;
					ExTraceGlobals.ReplayApiTracer.TraceDebug<uint, string>(0L, "FormatMessage( {0} ) was successful. Message: {1}.", msgId, msg);
				}
				else
				{
					gle = Marshal.GetLastWin32Error();
					ExTraceGlobals.ReplayApiTracer.TraceError<uint, int>(0L, "FormatMessage( {0} ) failed with GLE = {1} .", msgId, gle);
				}
			}
			finally
			{
				if (array2 != null)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						Marshal.FreeHGlobal(array2[k]);
					}
					Marshal.FreeHGlobal(intPtr);
				}
				moduleHandle.Close();
			}
			return result;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		internal static int GetThreadId()
		{
			return DiagnosticsNativeMethods.GetCurrentThreadId();
		}

		// Token: 0x040001E1 RID: 481
		private static TimeSpan s_defaultEventSuppressionInterval = TimeSpan.FromSeconds((double)RegistryParameters.CrimsonPeriodicLoggingIntervalInSec);

		// Token: 0x040001E2 RID: 482
		private static ExEventLog s_eventLog = SharedDependencies.DiagCoreImpl.EventLog;

		// Token: 0x040001E3 RID: 483
		private static string s_clusmsgFullPath = null;

		// Token: 0x040001E4 RID: 484
		private static string s_exceptionMessageWithHrFormatString = "{0} [HResult: 0x{1:x}].";

		// Token: 0x040001E5 RID: 485
		private static string s_exdbmsgFullPath = null;
	}
}
