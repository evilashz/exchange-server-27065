using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.CertificateAuthentication;

namespace Microsoft.Exchange.Configuration.CertificateAuthentication
{
	// Token: 0x02000006 RID: 6
	internal class Logger
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000032C0 File Offset: 0x000014C0
		static Logger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				Logger.processName = currentProcess.MainModule.ModuleName;
				Logger.processId = currentProcess.Id;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000330C File Offset: 0x0000150C
		internal static void LogVerbose(string message, params object[] args)
		{
			ExTraceGlobals.CertAuthTracer.Information(0L, message, args);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000331C File Offset: 0x0000151C
		internal static void LogVerbose(string message)
		{
			Logger.LogVerbose(message, new object[0]);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000332A File Offset: 0x0000152A
		internal static void LogError(string message, Exception exception)
		{
			ExTraceGlobals.CertAuthTracer.TraceError<string, Exception>(0L, "{0} - {1}", message, exception);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003340 File Offset: 0x00001540
		internal static void LogEvent(ExEventLog eventLogger, ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			if (messageArguments == null)
			{
				throw new ArgumentNullException("messageArguments");
			}
			object[] array = new object[messageArguments.Length + 2];
			array[0] = Logger.processName;
			array[1] = Logger.processId;
			messageArguments.CopyTo(array, 2);
			eventLogger.LogEvent(eventInfo, periodicKey, array);
		}

		// Token: 0x0400001C RID: 28
		private static string processName;

		// Token: 0x0400001D RID: 29
		private static int processId;
	}
}
