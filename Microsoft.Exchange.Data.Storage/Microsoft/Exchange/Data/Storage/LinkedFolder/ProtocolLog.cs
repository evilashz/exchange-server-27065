using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000986 RID: 2438
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ProtocolLog
	{
		// Token: 0x06005A03 RID: 23043 RVA: 0x0017514C File Offset: 0x0017334C
		private static void GetExceptionTypeAndDetails(Exception e, out List<string> types, out List<string> messages, out string chain, bool chainOnly)
		{
			Exception ex = e;
			chain = string.Empty;
			types = null;
			messages = null;
			if (!chainOnly)
			{
				types = new List<string>();
				messages = new List<string>();
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			for (;;)
			{
				string text = ex.GetType().ToString();
				string text2 = ex.Message;
				if (ex is SharePointException && ex.InnerException != null && ex.InnerException is WebException)
				{
					text = text + "; WebException:" + text2;
					text2 = text2 + "; DiagnosticInfo:" + ((SharePointException)ex).DiagnosticInfo;
				}
				if (!chainOnly)
				{
					types.Add(text);
					messages.Add(text2);
				}
				stringBuilder.Append("[Type:");
				stringBuilder.Append(text);
				stringBuilder.Append("]");
				stringBuilder.Append("[Message:");
				stringBuilder.Append(text2);
				stringBuilder.Append("]");
				stringBuilder.Append("[Stack:");
				stringBuilder.Append(string.IsNullOrEmpty(ex.StackTrace) ? string.Empty : ex.StackTrace.Replace("\r\n", string.Empty));
				stringBuilder.Append("]");
				if (ex.InnerException == null || num > 10)
				{
					break;
				}
				ex = ex.InnerException;
				num++;
			}
			chain = stringBuilder.ToString();
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x001752A0 File Offset: 0x001734A0
		private static void LogEvent(ProtocolLog.Component component, ProtocolLog.EventType eventType, LoggingContext loggingContext, string data, Exception exception)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new ArgumentNullException("data");
			}
			ProtocolLog.InitializeIfNeeded();
			LogRowFormatter logRowFormatter = new LogRowFormatter(ProtocolLog.LogSchema);
			logRowFormatter[1] = component;
			logRowFormatter[2] = eventType;
			if (loggingContext != null)
			{
				logRowFormatter[5] = loggingContext.MailboxGuid.ToString();
				logRowFormatter[3] = loggingContext.TransactionId.ToString();
				logRowFormatter[4] = loggingContext.User;
				logRowFormatter[6] = loggingContext.Context;
			}
			logRowFormatter[7] = data;
			if (exception != null)
			{
				List<string> list = null;
				List<string> list2 = null;
				string value = null;
				ProtocolLog.GetExceptionTypeAndDetails(exception, out list, out list2, out value, false);
				logRowFormatter[8] = list[0];
				logRowFormatter[9] = list2[0];
				if (list.Count > 1)
				{
					logRowFormatter[10] = list[list.Count - 1];
					logRowFormatter[11] = list2[list2.Count - 1];
				}
				logRowFormatter[12] = value;
			}
			ProtocolLog.instance.logInstance.Append(logRowFormatter, 0);
			if (loggingContext != null && loggingContext.LoggingStream != null)
			{
				try
				{
					logRowFormatter.Write(loggingContext.LoggingStream);
				}
				catch (StorageTransientException)
				{
				}
				catch (StoragePermanentException)
				{
				}
			}
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x00175408 File Offset: 0x00173608
		private static void InitializeIfNeeded()
		{
			if (!ProtocolLog.instance.initialized)
			{
				lock (ProtocolLog.instance.initializeLockObject)
				{
					if (!ProtocolLog.instance.initialized)
					{
						ProtocolLog.instance.Initialize();
						ProtocolLog.instance.initialized = true;
					}
				}
			}
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x00175474 File Offset: 0x00173674
		private void Initialize()
		{
			ProtocolLog.instance.logInstance = new Log(ProtocolLog.GetLogFileName(), new LogHeaderFormatter(ProtocolLog.LogSchema), "TeamMailboxSyncLog");
			ProtocolLog.instance.logInstance.Configure(Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\TeamMailbox\\"), ProtocolLog.LogMaxAge, 262144000L, 10485760L);
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x001754D8 File Offset: 0x001736D8
		public static void LogError(ProtocolLog.Component component, LoggingContext loggingContext, string data, Exception exception)
		{
			ProtocolLog.LogEvent(component, ProtocolLog.EventType.Error, loggingContext, data, exception);
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x001754E4 File Offset: 0x001736E4
		public static void LogInformation(ProtocolLog.Component component, LoggingContext loggingContext, string data)
		{
			ProtocolLog.LogEvent(component, ProtocolLog.EventType.Information, loggingContext, data, null);
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x001754F0 File Offset: 0x001736F0
		public static void LogCycleSuccess(ProtocolLog.Component component, LoggingContext loggingContext, string data)
		{
			ProtocolLog.LogEvent(component, ProtocolLog.EventType.CycleSuccess, loggingContext, data, null);
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x001754FC File Offset: 0x001736FC
		public static void LogCycleFailure(ProtocolLog.Component component, LoggingContext loggingContext, string data, Exception exception)
		{
			ProtocolLog.LogEvent(component, ProtocolLog.EventType.CycleFailure, loggingContext, data, exception);
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x00175508 File Offset: 0x00173708
		public static void LogStatistics(ProtocolLog.Component component, LoggingContext loggingContext, string data)
		{
			ProtocolLog.LogEvent(component, ProtocolLog.EventType.Statistics, loggingContext, data, null);
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x00175514 File Offset: 0x00173714
		public static string GetLogFileName()
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
				{
					currentProcess.ProcessName,
					"TeamMailboxSyncLog"
				});
			}
			return result;
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x00175570 File Offset: 0x00173770
		public static string GetExceptionLogString(Exception e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e is null");
			}
			string empty = string.Empty;
			List<string> list = null;
			List<string> list2 = null;
			ProtocolLog.GetExceptionTypeAndDetails(e, out list, out list2, out empty, true);
			return empty;
		}

		// Token: 0x0400317D RID: 12669
		internal const string CRLF = "\r\n";

		// Token: 0x0400317E RID: 12670
		private const string DefaultLogPath = "Logging\\TeamMailbox\\";

		// Token: 0x0400317F RID: 12671
		private const string LogType = "TeamMailbox Synchronization Log";

		// Token: 0x04003180 RID: 12672
		private const string LogComponent = "TeamMailboxSyncLog";

		// Token: 0x04003181 RID: 12673
		private const string LogSuffix = "TeamMailboxSyncLog";

		// Token: 0x04003182 RID: 12674
		private const int MaxLogDirectorySize = 262144000;

		// Token: 0x04003183 RID: 12675
		private const int MaxLogFileSize = 10485760;

		// Token: 0x04003184 RID: 12676
		private static readonly EnhancedTimeSpan LogMaxAge = EnhancedTimeSpan.FromDays(30.0);

		// Token: 0x04003185 RID: 12677
		private static readonly ProtocolLog instance = new ProtocolLog();

		// Token: 0x04003186 RID: 12678
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"component",
			"event-type",
			"transaction-id",
			"client",
			"sitemailbox-guid",
			"sharepoint-url",
			"data",
			"outerexception-type",
			"outerexception-message",
			"innerexception-type",
			"innerexception-message",
			"exceptionchain"
		};

		// Token: 0x04003187 RID: 12679
		private static readonly LogSchema LogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "TeamMailbox Synchronization Log", ProtocolLog.Fields);

		// Token: 0x04003188 RID: 12680
		private readonly object initializeLockObject = new object();

		// Token: 0x04003189 RID: 12681
		private Log logInstance;

		// Token: 0x0400318A RID: 12682
		private bool initialized;

		// Token: 0x02000987 RID: 2439
		private enum Field
		{
			// Token: 0x0400318C RID: 12684
			Time,
			// Token: 0x0400318D RID: 12685
			Component,
			// Token: 0x0400318E RID: 12686
			EventType,
			// Token: 0x0400318F RID: 12687
			TransactionId,
			// Token: 0x04003190 RID: 12688
			Client,
			// Token: 0x04003191 RID: 12689
			SiteMailboxGuid,
			// Token: 0x04003192 RID: 12690
			SharepointUrl,
			// Token: 0x04003193 RID: 12691
			Data,
			// Token: 0x04003194 RID: 12692
			OuterExceptionType,
			// Token: 0x04003195 RID: 12693
			OuterExceptionMessage,
			// Token: 0x04003196 RID: 12694
			InnerExceptionType,
			// Token: 0x04003197 RID: 12695
			InnerExceptionMessage,
			// Token: 0x04003198 RID: 12696
			ExceptionChain
		}

		// Token: 0x02000988 RID: 2440
		internal enum Component
		{
			// Token: 0x0400319A RID: 12698
			DocumentSync,
			// Token: 0x0400319B RID: 12699
			MembershipSync,
			// Token: 0x0400319C RID: 12700
			Maintenance,
			// Token: 0x0400319D RID: 12701
			MoMT,
			// Token: 0x0400319E RID: 12702
			Assistant,
			// Token: 0x0400319F RID: 12703
			SharepointAccessManager,
			// Token: 0x040031A0 RID: 12704
			Monitor
		}

		// Token: 0x02000989 RID: 2441
		internal enum EventType
		{
			// Token: 0x040031A2 RID: 12706
			Information,
			// Token: 0x040031A3 RID: 12707
			Error,
			// Token: 0x040031A4 RID: 12708
			Statistics,
			// Token: 0x040031A5 RID: 12709
			CycleSuccess,
			// Token: 0x040031A6 RID: 12710
			CycleFailure
		}
	}
}
