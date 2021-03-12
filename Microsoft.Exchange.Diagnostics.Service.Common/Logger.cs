using System;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200000E RID: 14
	public static class Logger
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00004810 File Offset: 0x00002A10
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00004817 File Offset: 0x00002A17
		public static ILog Log
		{
			get
			{
				return Microsoft.ExLogAnalyzer.Log.Instance;
			}
			set
			{
				Microsoft.ExLogAnalyzer.Log.Instance = value;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004820 File Offset: 0x00002A20
		public static void Cleanup()
		{
			IDisposable disposable = Logger.Log as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			Logger.Log = null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004848 File Offset: 0x00002A48
		public static bool LogEvent(ExEventLog.EventTuple tuple, params object[] args)
		{
			EventLogger eventLogger = TriggerHandler.Instance as EventLogger;
			if (eventLogger != null)
			{
				return eventLogger.LogEvent(tuple, args);
			}
			Logger.Log.LogErrorMessage("Unable to log event, event log source not configured.", new object[0]);
			return false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004882 File Offset: 0x00002A82
		public static void LogErrorMessage(string format, params object[] args)
		{
			Logger.Log.LogErrorMessage(format, args);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004890 File Offset: 0x00002A90
		public static void LogWarningMessage(string format, params object[] args)
		{
			Logger.Log.LogWarningMessage(format, args);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000489E File Offset: 0x00002A9E
		public static void LogInformationMessage(string format, params object[] args)
		{
			Logger.Log.LogInformationMessage(format, args);
		}
	}
}
