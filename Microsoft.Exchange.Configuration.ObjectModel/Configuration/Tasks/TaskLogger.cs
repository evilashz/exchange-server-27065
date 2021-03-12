using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TaskLogger
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002FBC File Offset: 0x000011BC
		static TaskLogger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				TaskLogger.processName = currentProcess.MainModule.ModuleName;
				TaskLogger.processId = currentProcess.Id;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003088 File Offset: 0x00001288
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000308F File Offset: 0x0000128F
		public static bool LogErrorAsWarning { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003097 File Offset: 0x00001297
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000309E File Offset: 0x0000129E
		public static bool LogAllAsInfo { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000030A6 File Offset: 0x000012A6
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000030AD File Offset: 0x000012AD
		public static bool IsPrereqLogging { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000030B5 File Offset: 0x000012B5
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000030BC File Offset: 0x000012BC
		public static bool IsSetupLogging
		{
			get
			{
				return TaskLogger.isSetupLogging;
			}
			set
			{
				TaskLogger.isSetupLogging = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000030C4 File Offset: 0x000012C4
		internal static bool IsFileLoggingEnabled
		{
			get
			{
				return TaskLogger.fileLoggingEnabled;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000030CB File Offset: 0x000012CB
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000030D2 File Offset: 0x000012D2
		private static IFormatProvider FormatProvider
		{
			get
			{
				return TaskLogger.formatProvider;
			}
			set
			{
				TaskLogger.formatProvider = value;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000030DA File Offset: 0x000012DA
		public static void UnmanagedLog(string s)
		{
			TaskLogger.Log(new LocalizedString(s));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000030E7 File Offset: 0x000012E7
		public static void Log(LocalizedString localizedString)
		{
			if (ExTraceGlobals.LogTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.LogTracer.Information(0L, TaskLogger.FormatLocalizedString(localizedString));
			}
			if (TaskLogger.fileLoggingEnabled)
			{
				TaskLogger.LogMessageString(TaskLogger.FormatLocalizedString(localizedString));
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000311A File Offset: 0x0000131A
		public static void LogWarning(LocalizedString localizedString)
		{
			if (ExTraceGlobals.LogTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.LogTracer.Information(0L, TaskLogger.FormatLocalizedString(localizedString));
			}
			if (TaskLogger.fileLoggingEnabled)
			{
				TaskLogger.LogWarningString(TaskLogger.FormatLocalizedString(localizedString));
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003150 File Offset: 0x00001350
		public static void LogError(Exception e)
		{
			while (e != null)
			{
				LocalizedException ex = e as LocalizedException;
				string message;
				if (ex != null && TaskLogger.FormatProvider != null)
				{
					IFormatProvider formatProvider = ex.FormatProvider;
					ex.FormatProvider = TaskLogger.FormatProvider;
					message = ex.Message;
					ex.FormatProvider = formatProvider;
				}
				else
				{
					message = e.Message;
				}
				ExTraceGlobals.ErrorTracer.TraceError<LocalizedString, string, string>(0L, "{0}{1}\n{2}", Strings.LogErrorPrefix, message, e.StackTrace);
				if (TaskLogger.fileLoggingEnabled)
				{
					TaskLogger.LogErrorString(message);
				}
				e = e.InnerException;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000031D0 File Offset: 0x000013D0
		public static void SendWatsonReport(Exception e)
		{
			TaskLogger.SendWatsonReport(e, null, null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000031DC File Offset: 0x000013DC
		public static void SendWatsonReport(Exception e, string taskName, PropertyBag boundParameters)
		{
			TaskLogger.StopFileLogging();
			bool flag = true;
			try
			{
				string sourceFileName = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, ConfigurationContext.Setup.SetupLogFileName);
				string text = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, ConfigurationContext.Setup.SetupLogFileNameForWatson);
				File.Copy(sourceFileName, text, true);
				int num = 0;
				while (!ExWatson.TryAddExtraFile(text) && num < 10)
				{
					Thread.Sleep(100);
					num++;
				}
			}
			catch (FileNotFoundException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (IOException)
			{
				flag = false;
				if (TaskLogger.IsFileLoggingEnabled)
				{
					TaskLogger.LogErrorString(Strings.ExchangeSetupCannotCopyWatson(ConfigurationContext.Setup.SetupLogFileName, ConfigurationContext.Setup.SetupLogFileNameForWatson));
				}
			}
			if (flag)
			{
				if (!string.IsNullOrEmpty(taskName))
				{
					ExWatson.AddExtraData("Task Name: " + taskName);
				}
				if (boundParameters != null)
				{
					StringBuilder stringBuilder = new StringBuilder("Parameters:\n");
					foreach (object obj in boundParameters)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Value is IList)
						{
							stringBuilder.AppendLine(string.Format("{0}:{1}", dictionaryEntry.Key, MultiValuedPropertyBase.FormatMultiValuedProperty(dictionaryEntry.Value as IList)));
						}
						else
						{
							stringBuilder.AppendLine(string.Format("{0}:'{1}'", dictionaryEntry.Key, (dictionaryEntry.Value == null) ? "<null>" : dictionaryEntry.Value.ToString()));
						}
					}
					ExWatson.AddExtraData(stringBuilder.ToString());
				}
				ExWatson.SendReport(e, ReportOptions.DoNotFreezeThreads, null);
			}
			try
			{
				TaskLogger.ResumeFileLogging();
			}
			catch (IOException)
			{
				if (TaskLogger.IsFileLoggingEnabled)
				{
					TaskLogger.LogErrorString(Strings.ExchangeSetupCannotResumeLog(ConfigurationContext.Setup.SetupLogFileName));
				}
				throw;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000033B8 File Offset: 0x000015B8
		public static void LogEnter()
		{
			if (!ExTraceGlobals.EnterExitTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				return;
			}
			StackTrace stackTrace = new StackTrace();
			MethodBase method = stackTrace.GetFrame(1).GetMethod();
			ExTraceGlobals.EnterExitTracer.Information(0L, Strings.LogFunctionEnter(method.ReflectedType, method.Name, ""));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003410 File Offset: 0x00001610
		public static void LogEnter(params object[] arguments)
		{
			if (!ExTraceGlobals.EnterExitTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				return;
			}
			StackTrace stackTrace = new StackTrace();
			MethodBase method = stackTrace.GetFrame(1).GetMethod();
			StringBuilder stringBuilder = new StringBuilder();
			if (arguments != null)
			{
				for (int i = 0; i < arguments.Length; i++)
				{
					stringBuilder.Append((arguments[i] != null) ? arguments[i].ToString() : "null");
					if (i + 1 < arguments.Length)
					{
						stringBuilder.Append(", ");
					}
				}
			}
			ExTraceGlobals.EnterExitTracer.Information(0L, Strings.LogFunctionEnter(method.ReflectedType, method.Name, stringBuilder.ToString()));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000034AC File Offset: 0x000016AC
		public static void LogExit()
		{
			if (!ExTraceGlobals.EnterExitTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				return;
			}
			StackTrace stackTrace = new StackTrace();
			MethodBase method = stackTrace.GetFrame(1).GetMethod();
			ExTraceGlobals.EnterExitTracer.Information(0L, Strings.LogFunctionExit(method.ReflectedType, method.Name));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000034FC File Offset: 0x000016FC
		public static void Trace(LocalizedString localizedString)
		{
			ExTraceGlobals.TraceTracer.Information(0L, TaskLogger.FormatLocalizedString(localizedString));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003510 File Offset: 0x00001710
		public static void Trace(string format, params object[] objects)
		{
			ExTraceGlobals.TraceTracer.Information(0L, format, objects);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003520 File Offset: 0x00001720
		public static void StartFileLogging(string path, string dataMiningPath = null)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			TaskLogger.indentationLevel = 0;
			TaskLogger.logFilePath = path;
			TaskLogger.dataMiningLogFilePath = dataMiningPath;
			TaskLogger.ResumeFileLogging();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000354C File Offset: 0x0000174C
		public static void StopFileLogging()
		{
			if (!TaskLogger.fileLoggingEnabled)
			{
				return;
			}
			TaskLogger.fileLoggingEnabled = false;
			TaskLogger.sw.Dispose();
			TaskLogger.sw = null;
			if (TaskLogger.swDataMining != null)
			{
				TaskLogger.swDataMining.Dispose();
				TaskLogger.swDataMining = null;
			}
			TaskLogger.FormatProvider = null;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003589 File Offset: 0x00001789
		public static void IncreaseIndentation(LocalizedString tag)
		{
			if (!TaskLogger.fileLoggingEnabled)
			{
				return;
			}
			TaskLogger.indentationLevel++;
			if (!string.IsNullOrEmpty(tag))
			{
				TaskLogger.Log(tag);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000035B2 File Offset: 0x000017B2
		public static void IncreaseIndentation()
		{
			TaskLogger.IncreaseIndentation(LocalizedString.Empty);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000035BE File Offset: 0x000017BE
		public static void DecreaseIndentation()
		{
			if (!TaskLogger.fileLoggingEnabled)
			{
				return;
			}
			TaskLogger.indentationLevel--;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000035D4 File Offset: 0x000017D4
		private static ExEventLog GetEventLogger(string hostName)
		{
			if (string.Compare(hostName, "Exchange Management Console", StringComparison.Ordinal) == 0)
			{
				return TaskLogger.emcEventLogger.Value;
			}
			return TaskLogger.cmdletIterationLogger.Value;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000035FC File Offset: 0x000017FC
		internal static void LogRbacEvent(ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			if (messageArguments == null)
			{
				throw new ArgumentNullException("messageArguments");
			}
			object[] array = new object[messageArguments.Length + 2];
			array[0] = TaskLogger.processName;
			array[1] = TaskLogger.processId;
			messageArguments.CopyTo(array, 2);
			TaskLogger.LogEvent(TaskLogger.rbacEventLogger.Value, eventInfo, periodicKey, array);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003651 File Offset: 0x00001851
		public static void LogEvent(string hostName, ExEventLog.EventTuple eventInfo, params object[] messageArguments)
		{
			TaskLogger.LogEvent(hostName, eventInfo, null, messageArguments);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000365C File Offset: 0x0000185C
		public static void LogEvent(string hostName, ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			TaskLogger.LogEvent(TaskLogger.GetEventLogger(hostName), eventInfo, periodicKey, messageArguments);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000366C File Offset: 0x0000186C
		public static void LogEvent(ExEventLog.EventTuple eventInfo, TaskInvocationInfo invocationInfo, string periodicKey, params object[] messageArguments)
		{
			if (TaskLogger.IsSetupLogging)
			{
				return;
			}
			if (messageArguments == null)
			{
				throw new ArgumentNullException("messageArguments");
			}
			object[] array = new object[messageArguments.Length + 3];
			array[0] = TaskLogger.processId;
			array[1] = Environment.CurrentManagedThreadId;
			array[2] = invocationInfo.DisplayName;
			messageArguments.CopyTo(array, 3);
			TaskLogger.LogEvent(invocationInfo.ShellHostName, eventInfo, periodicKey, array);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000036D4 File Offset: 0x000018D4
		public static void LogDataMiningMessage(string version, string task, DateTime startTime)
		{
			if (TaskLogger.indentationLevel == 0 && TaskLogger.fileLoggingEnabled && TaskLogger.swDataMining != null)
			{
				try
				{
					TimeSpan timeSpan = DateTime.UtcNow.Subtract(startTime);
					TaskLogger.swDataMining.WriteLine(string.Format("{0},{1},{2},{3},{4}", new object[]
					{
						startTime.ToString("MM/dd/yyyy HH:mm:ss"),
						timeSpan.Ticks,
						version,
						TaskLogger.indentationLevel,
						Regex.Replace(task.Replace(',', '_'), "15\\.[\\d|\\.]*", string.Empty)
					}));
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003788 File Offset: 0x00001988
		private static void LogEvent(ExEventLog logChannel, ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			logChannel.LogEvent(eventInfo, periodicKey, messageArguments);
			ExTraceGlobals.EventTracer.Information(0L, eventInfo.ToString(), messageArguments);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000037B0 File Offset: 0x000019B0
		internal static void ResumeFileLogging()
		{
			if (TaskLogger.logFilePath != null)
			{
				TaskLogger.FormatProvider = new CultureInfo("en-US");
				FileStream stream = new FileStream(TaskLogger.logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				TaskLogger.sw = new StreamWriter(stream);
				TaskLogger.sw.AutoFlush = true;
				TaskLogger.fileLoggingEnabled = true;
			}
			if (TaskLogger.dataMiningLogFilePath != null)
			{
				FileStream stream2 = new FileStream(TaskLogger.dataMiningLogFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				TaskLogger.swDataMining = new StreamWriter(stream2);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000381D File Offset: 0x00001A1D
		private static string FormatLocalizedString(LocalizedString locString)
		{
			if (TaskLogger.FormatProvider != null)
			{
				return locString.ToString(TaskLogger.FormatProvider);
			}
			return locString.ToString();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003840 File Offset: 0x00001A40
		private static void LogErrorString(string message)
		{
			if (TaskLogger.IsPrereqLogging)
			{
				TaskLogger.LogMessageString("[REQUIRED] " + message);
				return;
			}
			if (TaskLogger.LogAllAsInfo)
			{
				TaskLogger.LogMessageString(message);
				return;
			}
			if (TaskLogger.LogErrorAsWarning)
			{
				TaskLogger.LogWarningString(message);
				return;
			}
			TaskLogger.LogMessageString("[ERROR] " + message);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003891 File Offset: 0x00001A91
		private static void LogWarningString(string message)
		{
			if (TaskLogger.IsPrereqLogging)
			{
				TaskLogger.LogMessageString("[RECOMENDED] " + message);
				return;
			}
			if (TaskLogger.LogAllAsInfo)
			{
				TaskLogger.LogMessageString(message);
				return;
			}
			TaskLogger.LogMessageString("[WARNING] " + message);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000038CC File Offset: 0x00001ACC
		private static void LogMessageString(string message)
		{
			try
			{
				DateTime utcNow = DateTime.UtcNow;
				TaskLogger.sw.WriteLine(string.Format("[{0}.{1:0000}] [{2}] {3}", new object[]
				{
					utcNow.ToString("MM/dd/yyyy HH:mm:ss"),
					utcNow.Millisecond,
					TaskLogger.indentationLevel,
					message
				}));
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0400001B RID: 27
		internal const string MSExchangeManagementEventLogName = "MSExchange Management";

		// Token: 0x0400001C RID: 28
		private const string emcHostName = "Exchange Management Console";

		// Token: 0x0400001D RID: 29
		private const int minimumIndentationLevel = 0;

		// Token: 0x0400001E RID: 30
		private const int maximumIndentationLevel = 3;

		// Token: 0x0400001F RID: 31
		private const string errorTag = "[ERROR] ";

		// Token: 0x04000020 RID: 32
		private const string warningTag = "[WARNING] ";

		// Token: 0x04000021 RID: 33
		private const string prereqErrorTags = "[REQUIRED] ";

		// Token: 0x04000022 RID: 34
		private const string prereqWarningTags = "[RECOMENDED] ";

		// Token: 0x04000023 RID: 35
		private static readonly Lazy<ExEventLog> emcEventLogger = new Lazy<ExEventLog>(() => new ExEventLog(ExTraceGlobals.LogTracer.Category, "MSExchange Configuration Cmdlet - Management Console", "MSExchange Management"));

		// Token: 0x04000024 RID: 36
		private static readonly Lazy<ExEventLog> cmdletIterationLogger = new Lazy<ExEventLog>(() => new ExEventLog(ExTraceGlobals.LogTracer.Category, "MSExchange CmdletLogs", "MSExchange Management"));

		// Token: 0x04000025 RID: 37
		private static readonly Lazy<ExEventLog> rbacEventLogger = new Lazy<ExEventLog>(() => new ExEventLog(ExTraceGlobals.LogTracer.Category, "MSExchange RBAC"));

		// Token: 0x04000026 RID: 38
		private static int indentationLevel;

		// Token: 0x04000027 RID: 39
		private static bool fileLoggingEnabled = false;

		// Token: 0x04000028 RID: 40
		private static string logFilePath;

		// Token: 0x04000029 RID: 41
		private static string dataMiningLogFilePath;

		// Token: 0x0400002A RID: 42
		private static StreamWriter sw;

		// Token: 0x0400002B RID: 43
		private static StreamWriter swDataMining;

		// Token: 0x0400002C RID: 44
		private static bool isSetupLogging = false;

		// Token: 0x0400002D RID: 45
		private static IFormatProvider formatProvider;

		// Token: 0x0400002E RID: 46
		private static string processName;

		// Token: 0x0400002F RID: 47
		private static int processId;
	}
}
