using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000088 RID: 136
	internal class MonitorResultLogger : DisposeTrackableBase
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0001D390 File Offset: 0x0001B590
		internal MonitorResultLogger(ILogConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.configuration = configuration;
			if (this.configuration.IsLoggingEnabled)
			{
				string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
				this.logSchema = new LogSchema("Microsoft Exchange Server", version, this.configuration.LogType, MonitorResultLogger.Fields);
				this.log = new Log(this.configuration.LogPrefix, new LogHeaderFormatter(this.logSchema), this.configuration.LogComponent);
				this.log.Configure(this.configuration.LogPath, this.configuration.MaxLogAge, this.configuration.MaxLogDirectorySizeInBytes, this.configuration.MaxLogFileSizeInBytes, Settings.ResultsLogBufferSizeInBytes, new TimeSpan(0, Settings.ResultsLogFlushIntervalInMinutes, 0));
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001D474 File Offset: 0x0001B674
		public void LogEvent(DateTime timestamp, Dictionary<string, object> tempResults)
		{
			if (!this.configuration.IsLoggingEnabled)
			{
				return;
			}
			LogRowFormatter row = this.CreateRow(timestamp, CrimsonHelper.Serialize(tempResults, true));
			this.log.Append(row, -1);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001D4AB File Offset: 0x0001B6AB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MonitorResultLogger>(this);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001D4B3 File Offset: 0x0001B6B3
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.log != null)
			{
				this.log.Flush();
				this.log.Close();
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		private LogRowFormatter CreateRow(DateTime time, string data)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema, true);
			logRowFormatter[0] = time;
			logRowFormatter[1] = data;
			return logRowFormatter;
		}

		// Token: 0x0400046C RID: 1132
		private const string SoftwareName = "Microsoft Exchange Server";

		// Token: 0x0400046D RID: 1133
		private static readonly string[] Fields = new string[]
		{
			"TimeStamp",
			"Data"
		};

		// Token: 0x0400046E RID: 1134
		private readonly LogSchema logSchema;

		// Token: 0x0400046F RID: 1135
		private readonly Log log;

		// Token: 0x04000470 RID: 1136
		private readonly ILogConfiguration configuration;

		// Token: 0x02000089 RID: 137
		private enum Field
		{
			// Token: 0x04000472 RID: 1138
			TimeStamp,
			// Token: 0x04000473 RID: 1139
			Data
		}
	}
}
