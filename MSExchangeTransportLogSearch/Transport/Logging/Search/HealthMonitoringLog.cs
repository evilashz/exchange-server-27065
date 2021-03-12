using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HealthMonitoringLog : DisposeTrackableBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public void Configure(LogSearchAppConfig.HealthMonitoringLogAppConfig config)
		{
			this.logger.Configure(config.HealthMonitoringLogPath, config.HealthMonitoringLogMaxAge, config.HealthMonitoringLogMaxDirectorySize, config.HealthMonitoringLogMaxFileSize);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public void LogEvent(HealthMonitoringEvents eventId, params KeyValuePair<string, object>[] eventData)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(HealthMonitoringLog.healthMonitoringSchema);
			logRowFormatter[1] = HealthMonitoringEventsStrings.StringMap[eventId];
			logRowFormatter[2] = eventData;
			this.logger.Append(logRowFormatter, 0);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002137 File Offset: 0x00000337
		public void Close()
		{
			base.CheckDisposed();
			if (this.logger != null)
			{
				this.logger.Close();
				this.logger = null;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002159 File Offset: 0x00000359
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002164 File Offset: 0x00000364
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<HealthMonitoringLog>(this);
		}

		// Token: 0x04000001 RID: 1
		private static LogSchema healthMonitoringSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Health Monitoring Log", new string[]
		{
			"TimeStamp",
			"EventId",
			"EventData"
		});

		// Token: 0x04000002 RID: 2
		private Log logger = new Log("HEALTHLOG", new LogHeaderFormatter(HealthMonitoringLog.healthMonitoringSchema), "HealthMonitoringLogs");
	}
}
