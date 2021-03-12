using System;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000011 RID: 17
	internal class EventLogger : IEventLogger
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000046FA File Offset: 0x000028FA
		private EventLogger()
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004704 File Offset: 0x00002904
		private EventLogger(Guid eventLogAppGuid, string serviceName)
		{
			if (eventLogAppGuid == Guid.Empty)
			{
				throw new ArgumentException("EventLogger is not configured.", "eventLogAppGuid");
			}
			if (string.IsNullOrWhiteSpace(serviceName))
			{
				throw new ArgumentException("EventLogger is not configured.", "serviceName");
			}
			this.eventLog = new ExEventLog(eventLogAppGuid, serviceName);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004759 File Offset: 0x00002959
		internal static IEventLogger Logger
		{
			get
			{
				if (EventLogger.instance == null)
				{
					EventLogger.instance = new EventLogger();
				}
				return EventLogger.instance;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004774 File Offset: 0x00002974
		public static void Configure(Guid eventLogAppGuid, string eventSource)
		{
			lock (EventLogger.instanceMutex)
			{
				if (EventLogger.instance != null && EventLogger.instance.eventLog != null && EventLogger.eventSource != eventSource)
				{
					throw new InvalidOperationException("EventLogger is configured for a different event source already.");
				}
				EventLogger.instance = new EventLogger(eventLogAppGuid, eventSource);
				EventLogger.eventSource = eventSource;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000047EC File Offset: 0x000029EC
		public void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			if (this.eventLog != null)
			{
				this.eventLog.LogEvent(tuple, periodicKey, messageArgs);
			}
		}

		// Token: 0x0400007A RID: 122
		private static readonly object instanceMutex = new object();

		// Token: 0x0400007B RID: 123
		private static EventLogger instance;

		// Token: 0x0400007C RID: 124
		private static string eventSource;

		// Token: 0x0400007D RID: 125
		private readonly ExEventLog eventLog;
	}
}
