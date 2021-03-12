using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000A4 RID: 164
	internal class MonitoringLogger : DisposeTrackableBase
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x000222A4 File Offset: 0x000204A4
		internal MonitoringLogger(ILogConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.configuration = configuration;
			string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			this.logSchema = new LogSchema("Microsoft Exchange Server", version, this.configuration.LogType, MonitoringLogger.Fields);
			this.log = new Log(this.configuration.LogPrefix, new LogHeaderFormatter(this.logSchema), this.configuration.LogComponent);
			this.log.Configure(this.configuration.LogPath, this.configuration.MaxLogAge, this.configuration.MaxLogDirectorySizeInBytes, this.configuration.MaxLogFileSizeInBytes);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00022365 File Offset: 0x00020565
		public void LogEvent(DateTime timestamp, string formatString, object[] parameters)
		{
			if (!this.configuration.IsLoggingEnabled)
			{
				return;
			}
			this.LogRow(timestamp, formatString, parameters);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002237E File Offset: 0x0002057E
		public void LogEvents(List<DateTime> timestamps, List<string> formatStrings, List<object[]> parameters)
		{
			if (!this.configuration.IsLoggingEnabled)
			{
				return;
			}
			this.LogRows(timestamps, formatStrings, parameters);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00022397 File Offset: 0x00020597
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MonitoringLogger>(this);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002239F File Offset: 0x0002059F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.log.Flush();
				this.log.Close();
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000223BC File Offset: 0x000205BC
		private LogRowFormatter CreateRow(DateTime time, string data)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema, false);
			logRowFormatter[0] = time;
			logRowFormatter[1] = data;
			return logRowFormatter;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000223EC File Offset: 0x000205EC
		private void LogRow(DateTime time, string formatString, object[] parameters)
		{
			string data = formatString;
			if (parameters != null && parameters.Length > 0)
			{
				data = string.Format(formatString, parameters);
			}
			LogRowFormatter row = this.CreateRow(time, data);
			this.log.Append(row, -1);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00022424 File Offset: 0x00020624
		private void LogRows(List<DateTime> timestamps, List<string> formatStrings, List<object[]> parameters)
		{
			List<LogRowFormatter> list = new List<LogRowFormatter>();
			for (int i = 0; i < timestamps.Count; i++)
			{
				string text = formatStrings[i];
				if (parameters[i] != null && parameters[i].Length > 0)
				{
					text = string.Format(text, parameters[i]);
				}
				list.Add(this.CreateRow(timestamps[i], text));
			}
			this.log.Append(list, -1);
		}

		// Token: 0x040003A9 RID: 937
		private const string SoftwareName = "Microsoft Exchange Server";

		// Token: 0x040003AA RID: 938
		private static readonly string[] Fields = new string[]
		{
			"TimeStamp",
			"Data"
		};

		// Token: 0x040003AB RID: 939
		private readonly LogSchema logSchema;

		// Token: 0x040003AC RID: 940
		private readonly Log log;

		// Token: 0x040003AD RID: 941
		private readonly ILogConfiguration configuration;

		// Token: 0x020000A5 RID: 165
		private enum Field
		{
			// Token: 0x040003AF RID: 943
			TimeStamp,
			// Token: 0x040003B0 RID: 944
			Data
		}
	}
}
