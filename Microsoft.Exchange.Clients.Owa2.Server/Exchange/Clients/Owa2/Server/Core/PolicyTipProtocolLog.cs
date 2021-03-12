using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000233 RID: 563
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PolicyTipProtocolLog
	{
		// Token: 0x06001584 RID: 5508 RVA: 0x0004CC4C File Offset: 0x0004AE4C
		internal static void WriteToLog(string correlationId, string stage, string data, string extraData, TimeSpan elapsedTime, string outerExceptionType, string outerExceptionMessage, string innerExceptionType, string innerExceptionMessage, string exceptionChain)
		{
			PolicyTipProtocolLog.InitializeIfNeeded();
			LogRowFormatter logRowFormatter = new LogRowFormatter(PolicyTipProtocolLog.LogSchema);
			logRowFormatter[1] = Environment.MachineName;
			logRowFormatter[2] = correlationId;
			logRowFormatter[3] = stage;
			logRowFormatter[5] = data;
			logRowFormatter[6] = extraData;
			logRowFormatter[4] = elapsedTime.TotalMilliseconds;
			logRowFormatter[7] = outerExceptionType;
			logRowFormatter[8] = outerExceptionMessage;
			logRowFormatter[9] = innerExceptionType;
			logRowFormatter[10] = innerExceptionMessage;
			logRowFormatter[11] = exceptionChain;
			PolicyTipProtocolLog.instance.logInstance.Append(logRowFormatter, 0);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0004CCEC File Offset: 0x0004AEEC
		internal static string GetExceptionLogString(Exception e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e is null");
			}
			string empty = string.Empty;
			List<string> list = null;
			List<string> list2 = null;
			PolicyTipProtocolLog.GetExceptionTypeAndDetails(e, out list, out list2, out empty, true);
			return empty;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0004CD20 File Offset: 0x0004AF20
		internal static void GetExceptionTypeAndDetails(Exception e, out List<string> types, out List<string> messages, out string chain, bool chainOnly)
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

		// Token: 0x06001587 RID: 5511 RVA: 0x0004CE74 File Offset: 0x0004B074
		private static void InitializeIfNeeded()
		{
			if (!PolicyTipProtocolLog.instance.initialized)
			{
				lock (PolicyTipProtocolLog.instance.initializeLockObject)
				{
					if (!PolicyTipProtocolLog.instance.initialized)
					{
						PolicyTipProtocolLog.instance.Initialize();
						PolicyTipProtocolLog.instance.initialized = true;
					}
				}
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0004CEE0 File Offset: 0x0004B0E0
		private void Initialize()
		{
			PolicyTipProtocolLog.instance.logInstance = new Log(PolicyTipProtocolLog.GetLogFileName(), new LogHeaderFormatter(PolicyTipProtocolLog.LogSchema), "OwaPolicyTipLog");
			PolicyTipProtocolLog.instance.logInstance.Configure(Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\PolicyTip\\"), PolicyTipProtocolLog.LogMaxAge, 262144000L, 10485760L);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0004CF44 File Offset: 0x0004B144
		public static string GetLogFileName()
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
				{
					currentProcess.ProcessName,
					"OwaPolicyTipLog"
				});
			}
			return result;
		}

		// Token: 0x04000B9A RID: 2970
		internal const string CRLF = "\r\n";

		// Token: 0x04000B9B RID: 2971
		private const string DefaultLogPath = "Logging\\PolicyTip\\";

		// Token: 0x04000B9C RID: 2972
		private const string LogType = "PolicyTip Log";

		// Token: 0x04000B9D RID: 2973
		private const string LogComponent = "OwaPolicyTipLog";

		// Token: 0x04000B9E RID: 2974
		private const string LogSuffix = "OwaPolicyTipLog";

		// Token: 0x04000B9F RID: 2975
		private const int MaxLogDirectorySize = 262144000;

		// Token: 0x04000BA0 RID: 2976
		private const int MaxLogFileSize = 10485760;

		// Token: 0x04000BA1 RID: 2977
		private static readonly EnhancedTimeSpan LogMaxAge = EnhancedTimeSpan.FromDays(30.0);

		// Token: 0x04000BA2 RID: 2978
		private static readonly PolicyTipProtocolLog instance = new PolicyTipProtocolLog();

		// Token: 0x04000BA3 RID: 2979
		private static readonly string[] Fields = new string[]
		{
			"timestamp",
			"server",
			"correlation-id",
			"stage",
			"elapsed",
			"data",
			"extra-data",
			"outerexception-type",
			"outerexception-message",
			"innerexception-type",
			"innerexception-message",
			"exceptionchain"
		};

		// Token: 0x04000BA4 RID: 2980
		private static readonly LogSchema LogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "PolicyTip Log", PolicyTipProtocolLog.Fields);

		// Token: 0x04000BA5 RID: 2981
		private readonly object initializeLockObject = new object();

		// Token: 0x04000BA6 RID: 2982
		private Log logInstance;

		// Token: 0x04000BA7 RID: 2983
		private bool initialized;

		// Token: 0x02000234 RID: 564
		private enum Field
		{
			// Token: 0x04000BA9 RID: 2985
			TimeStamp,
			// Token: 0x04000BAA RID: 2986
			MachineName,
			// Token: 0x04000BAB RID: 2987
			CorrelationId,
			// Token: 0x04000BAC RID: 2988
			Stage,
			// Token: 0x04000BAD RID: 2989
			Elapsed,
			// Token: 0x04000BAE RID: 2990
			Data,
			// Token: 0x04000BAF RID: 2991
			ExtraData,
			// Token: 0x04000BB0 RID: 2992
			OuterExceptionType,
			// Token: 0x04000BB1 RID: 2993
			OuterExceptionMessage,
			// Token: 0x04000BB2 RID: 2994
			InnerExceptionType,
			// Token: 0x04000BB3 RID: 2995
			InnerExceptionMessage,
			// Token: 0x04000BB4 RID: 2996
			ExceptionChain
		}
	}
}
